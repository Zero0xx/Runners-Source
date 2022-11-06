using System;
using Message;
using UnityEngine;

// Token: 0x020009FA RID: 2554
public class FacebookTaskCreateGameData : SocialTaskBase
{
	// Token: 0x0600436E RID: 17262 RVA: 0x0015DEE8 File Offset: 0x0015C0E8
	public FacebookTaskCreateGameData()
	{
		this.m_callbackObject = null;
		this.m_isEndProcess = false;
	}

	// Token: 0x0600436F RID: 17263 RVA: 0x0015DF0C File Offset: 0x0015C10C
	public void Request(string gameId, SocialTaskManager manager, GameObject callbackObject)
	{
		this.m_gameId = gameId;
		this.m_manager = manager;
		this.m_callbackObject = callbackObject;
	}

	// Token: 0x06004370 RID: 17264 RVA: 0x0015DF24 File Offset: 0x0015C124
	protected override void OnStartProcess()
	{
		bool flag = true;
		if (!FacebookUtil.IsLoggedIn())
		{
			flag = false;
		}
		if (!flag)
		{
			this.m_isEndProcess = true;
			if (this.m_callbackObject != null)
			{
				MsgSocialNormalResponse value = new MsgSocialNormalResponse();
				this.m_callbackObject.SendMessage("CreateGameDataEndCallback", value, SendMessageOptions.DontRequireReceiver);
			}
			return;
		}
		if (this.m_manager != null)
		{
			FacebookTaskCreateObject facebookTaskCreateObject = new FacebookTaskCreateObject();
			string text = "{\"app_id\":203227836537595,\"type\":\"testrunners:gamedata\",\"url\":\"http://samples.ogp.me/215083468685365\",\"title\":\"Sample GameData\",\"image\":\"https://fbstatic-a.akamaihd.net/images/devsite/attachment_blank.png\",data:{\"game_id\":\"" + this.m_gameId + "\"},\"description\":\"\"}";
			global::Debug.Log(text);
			facebookTaskCreateObject.Request("gamedata", text, new FacebookTaskCreateObject.TaskFinishedCallback(this.CreateObjectEndCallback));
			this.m_manager.RequestProcess(facebookTaskCreateObject);
		}
		this.m_isEndProcess = true;
	}

	// Token: 0x06004371 RID: 17265 RVA: 0x0015DFD0 File Offset: 0x0015C1D0
	protected override void OnUpdate()
	{
	}

	// Token: 0x06004372 RID: 17266 RVA: 0x0015DFD4 File Offset: 0x0015C1D4
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x06004373 RID: 17267 RVA: 0x0015DFDC File Offset: 0x0015C1DC
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x06004374 RID: 17268 RVA: 0x0015DFE4 File Offset: 0x0015C1E4
	private void CreateObjectEndCallback(string idStr)
	{
		bool flag = true;
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface == null)
		{
			flag = false;
		}
		if (flag)
		{
			SocialUserData myProfile = socialInterface.MyProfile;
			myProfile.CustomData.ObjectId = idStr;
			if (this.m_manager != null)
			{
				FacebookTaskCreateAction facebookTaskCreateAction = new FacebookTaskCreateAction();
				facebookTaskCreateAction.Request("store", "gamedata", idStr, new FacebookTaskCreateAction.TaskFinishedCallback(this.CreateActionEndCallback));
				this.m_manager.RequestProcess(facebookTaskCreateAction);
			}
		}
		else if (this.m_callbackObject != null)
		{
			MsgSocialNormalResponse value = new MsgSocialNormalResponse();
			this.m_callbackObject.SendMessage("CreateGameDataEndCallback", value, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06004375 RID: 17269 RVA: 0x0015E090 File Offset: 0x0015C290
	private void CreateActionEndCallback(string idStr)
	{
		bool flag = true;
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface == null)
		{
			flag = false;
		}
		if (flag)
		{
			SocialUserData myProfile = socialInterface.MyProfile;
			myProfile.CustomData.ActionId = idStr;
		}
		if (this.m_callbackObject != null)
		{
			MsgSocialNormalResponse value = new MsgSocialNormalResponse();
			this.m_callbackObject.SendMessage("CreateGameDataEndCallback", value, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x04003926 RID: 14630
	private readonly string TaskName = "FacebookTaskCreateGameData";

	// Token: 0x04003927 RID: 14631
	private SocialTaskManager m_manager;

	// Token: 0x04003928 RID: 14632
	private GameObject m_callbackObject;

	// Token: 0x04003929 RID: 14633
	private bool m_isEndProcess;

	// Token: 0x0400392A RID: 14634
	private string m_gameId;
}
