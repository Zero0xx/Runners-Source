using System;
using System.Collections.Generic;
using Facebook;
using LitJson;
using Message;
using SaveData;
using UnityEngine;

// Token: 0x02000A01 RID: 2561
public class FacebookTaskRequestFriendList : SocialTaskBase
{
	// Token: 0x0600439C RID: 17308 RVA: 0x0015E7A0 File Offset: 0x0015C9A0
	public FacebookTaskRequestFriendList()
	{
		this.m_callbackObject = null;
		this.m_isEndProcess = false;
	}

	// Token: 0x0600439D RID: 17309 RVA: 0x0015E7C4 File Offset: 0x0015C9C4
	public void Request(GameObject callbackObject)
	{
		this.m_callbackObject = callbackObject;
	}

	// Token: 0x0600439E RID: 17310 RVA: 0x0015E7D0 File Offset: 0x0015C9D0
	protected override void OnStartProcess()
	{
		bool flag = true;
		if (!FacebookUtil.IsLoggedIn())
		{
			flag = false;
		}
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null && !socialInterface.IsGrantedPermission[1])
		{
			flag = false;
		}
		if (!flag)
		{
			this.m_isEndProcess = true;
			MsgSocialFriendListResponse msgSocialFriendListResponse = new MsgSocialFriendListResponse();
			msgSocialFriendListResponse.m_result = null;
			msgSocialFriendListResponse.m_friends = null;
			this.m_callbackObject.SendMessage("RequestFriendListEndCallback", msgSocialFriendListResponse, SendMessageOptions.DontRequireReceiver);
			return;
		}
		string str = FacebookUtil.MaxFBFriends.ToString();
		string text = FacebookUtil.FBVersionString + "me?fields=friends.limit(" + str + "){installed,id,name,picture}";
		text = Uri.EscapeUriString(text);
		FB.API(text, HttpMethod.GET, new FacebookDelegate(this.RequestFriendListEndCallback), null);
	}

	// Token: 0x0600439F RID: 17311 RVA: 0x0015E890 File Offset: 0x0015CA90
	protected override void OnUpdate()
	{
	}

	// Token: 0x060043A0 RID: 17312 RVA: 0x0015E894 File Offset: 0x0015CA94
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x060043A1 RID: 17313 RVA: 0x0015E89C File Offset: 0x0015CA9C
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x060043A2 RID: 17314 RVA: 0x0015E8A4 File Offset: 0x0015CAA4
	private void RequestFriendListEndCallback(FBResult fbResult)
	{
		this.m_isEndProcess = true;
		if (this.m_callbackObject == null)
		{
			return;
		}
		if (fbResult == null)
		{
			return;
		}
		string text = fbResult.Text;
		global::Debug.Log("Facebook.RequestFriendListResult:" + text);
		JsonData jsonData = null;
		JsonData jsonData2 = JsonMapper.ToObject(text);
		if (jsonData2 != null)
		{
			try
			{
				JsonData jsonObject = NetUtil.GetJsonObject(jsonData2, "friends");
				if (jsonObject != null)
				{
					jsonData = NetUtil.GetJsonArray(jsonObject, "data");
				}
			}
			catch (Exception ex)
			{
				MsgSocialFriendListResponse msgSocialFriendListResponse = new MsgSocialFriendListResponse();
				msgSocialFriendListResponse.m_result = new SocialResult
				{
					ResultId = 0,
					Result = fbResult.Text,
					IsError = true
				};
				msgSocialFriendListResponse.m_friends = null;
				this.m_callbackObject.SendMessage("RequestFriendListEndCallback", msgSocialFriendListResponse, SendMessageOptions.DontRequireReceiver);
				this.m_callbackObject = null;
			}
		}
		if (jsonData == null)
		{
			MsgSocialFriendListResponse msgSocialFriendListResponse2 = new MsgSocialFriendListResponse();
			msgSocialFriendListResponse2.m_result = new SocialResult();
			msgSocialFriendListResponse2.m_friends = null;
			this.m_callbackObject.SendMessage("RequestFriendListEndCallback", msgSocialFriendListResponse2, SendMessageOptions.DontRequireReceiver);
			this.m_callbackObject = null;
			return;
		}
		List<SocialUserData> list = new List<SocialUserData>();
		List<SocialUserData> list2 = new List<SocialUserData>();
		int count = jsonData.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jsonData3 = jsonData[i];
			if (jsonData3 != null)
			{
				bool flag = false;
				SocialUserData userData = FacebookUtil.GetUserData(jsonData3, ref flag);
				if (userData != null)
				{
					if (flag)
					{
						list.Add(userData);
					}
					else
					{
						list2.Add(userData);
					}
				}
			}
		}
		global::Debug.Log("FacebookTaskRequestFriendList.InstalledFriendList.Count = " + list.Count.ToString());
		SocialResult socialResult = new SocialResult();
		socialResult.ResultId = 0;
		socialResult.Result = fbResult.Text;
		socialResult.IsError = false;
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null)
		{
			socialInterface.AllFriendList = list;
			SystemData systemdata = SystemSaveManager.Instance.GetSystemdata();
			if (!systemdata.IsFlagStatus(SystemData.FlagStatus.FACEBOOK_FRIEND_INIT))
			{
				systemdata.SetFlagStatus(SystemData.FlagStatus.FACEBOOK_FRIEND_INIT, true);
				List<SocialUserData> list3 = new List<SocialUserData>();
				for (int j = 0; j < FacebookUtil.MaxFBRankingFriends; j++)
				{
					if (socialInterface.AllFriendList.Count <= j)
					{
						break;
					}
					SocialUserData socialUserData = list[j];
					if (socialUserData != null)
					{
						list3.Add(socialUserData);
					}
				}
				FacebookUtil.SaveFriendIdList(list3);
			}
			List<SocialUserData> list4 = new List<SocialUserData>();
			if (systemdata != null && systemdata.fbFriends != null)
			{
				List<string> fbFriends = systemdata.fbFriends;
				foreach (string text2 in fbFriends)
				{
					if (text2 != null)
					{
						foreach (SocialUserData socialUserData2 in list)
						{
							if (socialUserData2 != null)
							{
								if (socialUserData2.Id == text2)
								{
									list4.Add(socialUserData2);
								}
							}
						}
					}
				}
			}
			socialInterface.FriendList = list4;
			socialInterface.NotInstalledFriendList = list2;
		}
		MsgSocialFriendListResponse msgSocialFriendListResponse3 = new MsgSocialFriendListResponse();
		msgSocialFriendListResponse3.m_result = socialResult;
		msgSocialFriendListResponse3.m_friends = list;
		this.m_callbackObject.SendMessage("RequestFriendListEndCallback", msgSocialFriendListResponse3, SendMessageOptions.DontRequireReceiver);
		this.m_callbackObject = null;
		global::Debug.Log("Facebook Request FriendList is finished");
	}

	// Token: 0x0400393C RID: 14652
	private readonly string TaskName = "FacebookTaskRequestFriendList";

	// Token: 0x0400393D RID: 14653
	private GameObject m_callbackObject;

	// Token: 0x0400393E RID: 14654
	private bool m_isEndProcess;
}
