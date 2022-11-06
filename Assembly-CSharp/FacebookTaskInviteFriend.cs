using System;
using System.Collections.Generic;
using Facebook;
using Message;
using Text;
using UnityEngine;

// Token: 0x020009FD RID: 2557
public class FacebookTaskInviteFriend : SocialTaskBase
{
	// Token: 0x06004385 RID: 17285 RVA: 0x0015E3AC File Offset: 0x0015C5AC
	public FacebookTaskInviteFriend()
	{
		this.m_callbackObject = null;
		this.m_isEndProcess = false;
	}

	// Token: 0x06004386 RID: 17286 RVA: 0x0015E3D0 File Offset: 0x0015C5D0
	public void Request(GameObject callbackObject)
	{
		this.m_callbackObject = callbackObject;
	}

	// Token: 0x06004387 RID: 17287 RVA: 0x0015E3DC File Offset: 0x0015C5DC
	protected override void OnStartProcess()
	{
		if (!FacebookUtil.IsLoggedIn())
		{
			this.m_isEndProcess = true;
			return;
		}
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface == null)
		{
			if (this.m_callbackObject != null)
			{
				this.m_callbackObject.SendMessage("InviteFriendEndCallback", null, SendMessageOptions.DontRequireReceiver);
				this.m_callbackObject = null;
			}
			return;
		}
		string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "SnsFeed", "gw_invite_friend_caption").text;
		string text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "SnsFeed", "gw_invite_friend_text").text;
		SocialUserData myProfile = socialInterface.MyProfile;
		FB.AppRequest(text2, null, new List<object>
		{
			"app_non_users"
		}, null, null, myProfile.CustomData.GameId, text, new FacebookDelegate(this.InviteFriendEndCallback));
	}

	// Token: 0x06004388 RID: 17288 RVA: 0x0015E4B0 File Offset: 0x0015C6B0
	protected override void OnUpdate()
	{
	}

	// Token: 0x06004389 RID: 17289 RVA: 0x0015E4B4 File Offset: 0x0015C6B4
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x0600438A RID: 17290 RVA: 0x0015E4BC File Offset: 0x0015C6BC
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x0600438B RID: 17291 RVA: 0x0015E4C4 File Offset: 0x0015C6C4
	private void InviteFriendEndCallback(FBResult fbResult)
	{
		this.m_isEndProcess = true;
		if (fbResult == null)
		{
			return;
		}
		if (this.m_callbackObject == null)
		{
			return;
		}
		SocialResult socialResult = new SocialResult();
		socialResult.ResultId = 0;
		socialResult.Result = fbResult.Text;
		socialResult.IsError = false;
		MsgSocialNormalResponse msgSocialNormalResponse = new MsgSocialNormalResponse();
		msgSocialNormalResponse.m_result = socialResult;
		this.m_callbackObject.SendMessage("InviteFriendEndCallback", msgSocialNormalResponse, SendMessageOptions.DontRequireReceiver);
		this.m_callbackObject = null;
		global::Debug.Log("Facebook AppRequest is finished");
	}

	// Token: 0x04003933 RID: 14643
	private readonly string TaskName = "FacebookTaskInviteFriend";

	// Token: 0x04003934 RID: 14644
	private GameObject m_callbackObject;

	// Token: 0x04003935 RID: 14645
	private bool m_isEndProcess;
}
