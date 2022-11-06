using System;
using System.Collections.Generic;
using Message;
using SaveData;
using UnityEngine;

// Token: 0x0200052B RID: 1323
public class SettingPartsSnsAdditional : MonoBehaviour
{
	// Token: 0x060028E8 RID: 10472 RVA: 0x000FCCA0 File Offset: 0x000FAEA0
	private void Start()
	{
	}

	// Token: 0x060028E9 RID: 10473 RVA: 0x000FCCA4 File Offset: 0x000FAEA4
	public void PlayStart()
	{
		this.PlayStart(null, null, SettingPartsSnsAdditional.Mode.WAIT_TO_LOAD_END);
	}

	// Token: 0x060028EA RID: 10474 RVA: 0x000FCCB0 File Offset: 0x000FAEB0
	public void PlayStart(string gameObjectName, string functionName, SettingPartsSnsAdditional.Mode mode)
	{
		if (this.m_isEnd)
		{
			GameObject gameObject = GameObject.Find(gameObjectName);
			if (gameObject != null)
			{
				gameObject.SendMessage(functionName);
			}
			return;
		}
		if (this.m_isStart && !this.m_isEnd)
		{
			if (this.m_mode < mode)
			{
				this.m_mode = mode;
				if (this.m_mode == SettingPartsSnsAdditional.Mode.WAIT_TO_LOAD_END)
				{
					NetMonitor instance = NetMonitor.Instance;
					if (instance != null)
					{
						instance.StartMonitor(null);
					}
				}
			}
			SettingPartsSnsAdditional.CallbackInfo item;
			item.gameObjectName = gameObjectName;
			item.functionName = functionName;
			this.m_callbackList.Add(item);
			return;
		}
		this.m_isStart = true;
		this.m_mode = mode;
		if (this.m_mode == SettingPartsSnsAdditional.Mode.WAIT_TO_LOAD_END)
		{
			NetMonitor instance2 = NetMonitor.Instance;
			if (instance2 != null)
			{
				instance2.StartMonitor(null);
			}
		}
		SettingPartsSnsAdditional.CallbackInfo item2;
		item2.gameObjectName = gameObjectName;
		item2.functionName = functionName;
		this.m_callbackList.Add(item2);
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null && socialInterface.IsLoggedIn)
		{
			global::Debug.Log("SettingPartsSnsAdditional:PlayStart");
			socialInterface.RequestPermission(base.gameObject);
		}
		else
		{
			global::Debug.Log("SettingPartsSnsAdditional:NotLoggedIn");
			this.m_isEnd = false;
		}
	}

	// Token: 0x17000567 RID: 1383
	// (get) Token: 0x060028EB RID: 10475 RVA: 0x000FCDF0 File Offset: 0x000FAFF0
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x060028EC RID: 10476 RVA: 0x000FCDF8 File Offset: 0x000FAFF8
	private void RequestPermissionEndCallback(MsgSocialNormalResponse msg)
	{
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null && socialInterface.IsLoggedIn)
		{
			global::Debug.Log("SettingPartsSnsAdditional:PlayStart");
			socialInterface.RequestMyProfile(base.gameObject);
		}
	}

	// Token: 0x060028ED RID: 10477 RVA: 0x000FCE40 File Offset: 0x000FB040
	private void RequestMyProfileEndCallback(MsgSocialMyProfileResponse msg)
	{
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null && socialInterface.IsLoggedIn)
		{
			socialInterface.RequestFriendList(base.gameObject);
		}
		global::Debug.Log("SettingPartsSnsAdditional:RequestMyProfileEndCallback");
	}

	// Token: 0x060028EE RID: 10478 RVA: 0x000FCE88 File Offset: 0x000FB088
	private void RequestFriendListEndCallback(MsgSocialFriendListResponse msg)
	{
		global::Debug.Log("SettingPartsSnsAdditional:RequestFriendListEndCallback");
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		PlayerImageManager playerImageManager = GameObjectUtil.FindGameObjectComponent<PlayerImageManager>("PlayerImageManager");
		if (socialInterface != null && playerImageManager != null)
		{
			List<string> list = new List<string>();
			List<SocialUserData> friendWithMeList = socialInterface.FriendWithMeList;
			if (friendWithMeList != null)
			{
				foreach (SocialUserData socialUserData in friendWithMeList)
				{
					if (socialUserData != null)
					{
						if (!socialUserData.IsSilhouette)
						{
							playerImageManager.GetPlayerImage(socialUserData.Id, socialUserData.Url, null);
							global::Debug.Log("sns picture add: " + socialUserData.Id + ", " + socialUserData.Url);
						}
						list.Add(socialUserData.Id);
					}
				}
				global::Debug.Log("SettingPartsSnsAdditional:GetPlayerImage");
			}
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null && socialInterface != null)
			{
				loggedInServerInterface.RequestServerGetFriendUserIdList(list, base.gameObject);
			}
		}
	}

	// Token: 0x060028EF RID: 10479 RVA: 0x000FCFC8 File Offset: 0x000FB1C8
	private void RequestGameDataEndCallback(MsgSocialCustomUserDataResponse msg)
	{
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		SocialUserData myProfile = socialInterface.MyProfile;
		if (myProfile.Id == msg.m_userData.Id)
		{
			global::Debug.Log("SettingPartsSnsLogin:myProfile throwed");
			string text = string.Empty;
			if (SystemSaveManager.GetGameID() != "0")
			{
				text = SystemSaveManager.GetGameID();
			}
			bool flag = false;
			if (!msg.m_isCreated)
			{
				flag = true;
			}
			else if (text != myProfile.CustomData.GameId)
			{
				socialInterface.DeleteGameData(base.gameObject);
				flag = true;
			}
			if (flag)
			{
				global::Debug.Log("SettingPartsSnsLogin:Created Game Data");
				socialInterface.CreateMyGameData(text, base.gameObject);
			}
			else
			{
				this.CreateGameDataEndCallback(null);
			}
		}
	}

	// Token: 0x060028F0 RID: 10480 RVA: 0x000FD090 File Offset: 0x000FB290
	private void CreateGameDataEndCallback(MsgSocialNormalResponse msg)
	{
		global::Debug.Log("SettingPartsSnsLogin:CreatedGameDataWasFinished");
		if (this.m_mode == SettingPartsSnsAdditional.Mode.WAIT_TO_LOAD_END)
		{
			NetMonitor instance = NetMonitor.Instance;
			if (instance != null)
			{
				instance.EndMonitorForward(null, null, null);
				instance.EndMonitorBackward();
			}
		}
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null)
		{
			socialInterface.IsEnableFriendInfo = true;
		}
		this.m_isEnd = true;
		foreach (SettingPartsSnsAdditional.CallbackInfo callbackInfo in this.m_callbackList)
		{
			string gameObjectName = callbackInfo.gameObjectName;
			if (!string.IsNullOrEmpty(gameObjectName))
			{
				string functionName = callbackInfo.functionName;
				if (!string.IsNullOrEmpty(functionName))
				{
					GameObject gameObject = GameObject.Find(gameObjectName);
					if (gameObject != null)
					{
						gameObject.SendMessage(functionName);
					}
				}
			}
		}
		this.m_callbackList.Clear();
		GameObject gameObject2 = GameObject.Find("ui_mm_ranking_page(Clone)");
		if (gameObject2 != null)
		{
			gameObject2.SendMessage("OnSettingPartsSnsAdditional");
		}
	}

	// Token: 0x060028F1 RID: 10481 RVA: 0x000FD1D0 File Offset: 0x000FB3D0
	private void ServerGetFriendUserIdList_Succeeded(MsgGetFriendUserIdListSucceed msg)
	{
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null)
		{
			string id = socialInterface.MyProfile.Id;
			global::Debug.Log("mySnsUserId = " + id);
			bool flag = false;
			List<ServerUserTransformData> transformDataList = msg.m_transformDataList;
			if (transformDataList == null)
			{
				global::Debug.Log("ServerGetFriendUserIdList_Succeeded: DataList is null");
				this.ProcessEnd();
				return;
			}
			foreach (ServerUserTransformData serverUserTransformData in transformDataList)
			{
				if (serverUserTransformData != null)
				{
					if (serverUserTransformData.m_facebookId == id && serverUserTransformData.m_userId == SystemSaveManager.GetGameID())
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				global::Debug.Log("ServerGetFriendUserIdList_Succeeded: MyId is not Registered");
				transformDataList.Add(new ServerUserTransformData
				{
					m_facebookId = id,
					m_userId = SystemSaveManager.GetGameID()
				});
				id = socialInterface.MyProfile.Id;
			}
			foreach (ServerUserTransformData serverUserTransformData2 in transformDataList)
			{
				if (serverUserTransformData2 != null)
				{
					foreach (SocialUserData socialUserData in socialInterface.FriendWithMeList)
					{
						if (socialUserData != null)
						{
							if (serverUserTransformData2.m_facebookId == socialUserData.Id && string.IsNullOrEmpty(socialUserData.CustomData.GameId))
							{
								socialUserData.CustomData.GameId = serverUserTransformData2.m_userId;
							}
						}
					}
				}
			}
			if (!flag)
			{
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null && socialInterface != null)
				{
					loggedInServerInterface.RequestServerSetFacebookScopedId(id, base.gameObject);
				}
			}
			else
			{
				global::Debug.Log("ServerGetFriendUserIdList_Succeeded: MyId is already Registered");
				this.ProcessEnd();
			}
		}
	}

	// Token: 0x060028F2 RID: 10482 RVA: 0x000FD43C File Offset: 0x000FB63C
	private void ServerSetFacebookScopedId_Succeeded(MsgSetFacebookScopedIdSucceed msg)
	{
		this.ProcessEnd();
	}

	// Token: 0x060028F3 RID: 10483 RVA: 0x000FD444 File Offset: 0x000FB644
	private void ProcessEnd()
	{
		global::Debug.Log("SettingPartsSnsLogin:CreatedGameDataWasFinished");
		if (this.m_mode == SettingPartsSnsAdditional.Mode.WAIT_TO_LOAD_END)
		{
			NetMonitor instance = NetMonitor.Instance;
			if (instance != null)
			{
				instance.EndMonitorForward(null, null, null);
				instance.EndMonitorBackward();
			}
		}
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null)
		{
			socialInterface.IsEnableFriendInfo = true;
		}
		this.m_isEnd = true;
		foreach (SettingPartsSnsAdditional.CallbackInfo callbackInfo in this.m_callbackList)
		{
			string gameObjectName = callbackInfo.gameObjectName;
			if (!string.IsNullOrEmpty(gameObjectName))
			{
				string functionName = callbackInfo.functionName;
				if (!string.IsNullOrEmpty(functionName))
				{
					GameObject gameObject = GameObject.Find(gameObjectName);
					if (gameObject != null)
					{
						gameObject.SendMessage(functionName);
					}
				}
			}
		}
		this.m_callbackList.Clear();
		GameObject gameObject2 = GameObject.Find("ui_mm_ranking_page(Clone)");
		if (gameObject2 != null)
		{
			gameObject2.SendMessage("OnSettingPartsSnsAdditional");
		}
	}

	// Token: 0x04002450 RID: 9296
	private bool m_isStart;

	// Token: 0x04002451 RID: 9297
	private bool m_isEnd;

	// Token: 0x04002452 RID: 9298
	private SettingPartsSnsAdditional.Mode m_mode;

	// Token: 0x04002453 RID: 9299
	private List<SettingPartsSnsAdditional.CallbackInfo> m_callbackList = new List<SettingPartsSnsAdditional.CallbackInfo>();

	// Token: 0x04002454 RID: 9300
	private bool m_isEndRequestMyProfile;

	// Token: 0x04002455 RID: 9301
	private bool m_isEndRequestFriendProfile;

	// Token: 0x0200052C RID: 1324
	public enum Mode
	{
		// Token: 0x04002457 RID: 9303
		NONE,
		// Token: 0x04002458 RID: 9304
		BACK_GROUND_LOAD,
		// Token: 0x04002459 RID: 9305
		WAIT_TO_LOAD_END
	}

	// Token: 0x0200052D RID: 1325
	private struct CallbackInfo
	{
		// Token: 0x0400245A RID: 9306
		public string gameObjectName;

		// Token: 0x0400245B RID: 9307
		public string functionName;
	}
}
