using System;
using Facebook;
using Message;
using UnityEngine;

// Token: 0x02000A03 RID: 2563
public class FacebookTaskRequestInviteList : SocialTaskBase
{
	// Token: 0x060043AB RID: 17323 RVA: 0x0015EFA8 File Offset: 0x0015D1A8
	public FacebookTaskRequestInviteList()
	{
		this.m_callbackObject = null;
		this.m_isEndProcess = false;
	}

	// Token: 0x060043AC RID: 17324 RVA: 0x0015EFCC File Offset: 0x0015D1CC
	public void Request(GameObject callbackObject)
	{
		this.m_callbackObject = callbackObject;
	}

	// Token: 0x060043AD RID: 17325 RVA: 0x0015EFD8 File Offset: 0x0015D1D8
	protected override void OnStartProcess()
	{
		if (!FacebookUtil.IsLoggedIn())
		{
			this.m_isEndProcess = true;
		}
		string query = FacebookUtil.FBVersionString + "me/apprequests";
		FB.API(query, HttpMethod.GET, new FacebookDelegate(this.RequestInviteListEndCallback), null);
	}

	// Token: 0x060043AE RID: 17326 RVA: 0x0015F020 File Offset: 0x0015D220
	protected override void OnUpdate()
	{
	}

	// Token: 0x060043AF RID: 17327 RVA: 0x0015F024 File Offset: 0x0015D224
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x060043B0 RID: 17328 RVA: 0x0015F02C File Offset: 0x0015D22C
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x060043B1 RID: 17329 RVA: 0x0015F034 File Offset: 0x0015D234
	private void RequestInviteListEndCallback(FBResult fbResult)
	{
		this.m_isEndProcess = true;
		bool flag = true;
		string text = string.Empty;
		if (fbResult == null)
		{
			flag = false;
		}
		else
		{
			text = fbResult.Text;
		}
		if (flag)
		{
			SocialResult socialResult = new SocialResult();
			socialResult.ResultId = 0;
			socialResult.Result = fbResult.Text;
			socialResult.IsError = false;
			MsgSocialFriendListResponse msgSocialFriendListResponse = new MsgSocialFriendListResponse();
			msgSocialFriendListResponse.m_result = socialResult;
			msgSocialFriendListResponse.m_friends = FacebookUtil.GetInvitedFriendList(text);
			SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
			if (socialInterface != null)
			{
				socialInterface.InvitedFriendList = msgSocialFriendListResponse.m_friends;
			}
			if (this.m_callbackObject != null)
			{
				this.m_callbackObject.SendMessage("RequestInviteListEndCallback", msgSocialFriendListResponse, SendMessageOptions.DontRequireReceiver);
				this.m_callbackObject = null;
			}
		}
		else if (this.m_callbackObject != null)
		{
			this.m_callbackObject.SendMessage("RequestInviteListEndCallback", null, SendMessageOptions.DontRequireReceiver);
			this.m_callbackObject = null;
		}
		global::Debug.Log("Facebook AppRequest is finished");
	}

	// Token: 0x04003945 RID: 14661
	private readonly string TaskName = "FacebookTaskRequestInviteList";

	// Token: 0x04003946 RID: 14662
	private GameObject m_callbackObject;

	// Token: 0x04003947 RID: 14663
	private bool m_isEndProcess;
}
