using System;
using Message;
using UnityEngine;

// Token: 0x020009FB RID: 2555
public class FacebookTaskDeleteGameData : SocialTaskBase
{
	// Token: 0x06004376 RID: 17270 RVA: 0x0015E0FC File Offset: 0x0015C2FC
	public FacebookTaskDeleteGameData()
	{
		this.m_callbackObject = null;
		this.m_isEndProcess = false;
	}

	// Token: 0x06004377 RID: 17271 RVA: 0x0015E120 File Offset: 0x0015C320
	public void Request(SocialTaskManager manager, GameObject callbackObject)
	{
		this.m_manager = manager;
		this.m_callbackObject = callbackObject;
	}

	// Token: 0x06004378 RID: 17272 RVA: 0x0015E130 File Offset: 0x0015C330
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
				this.m_callbackObject.SendMessage("DeleteGameDataEndCallback", value, SendMessageOptions.DontRequireReceiver);
			}
			return;
		}
		if (this.m_manager != null)
		{
			FacebookTaskDeleteObject facebookTaskDeleteObject = new FacebookTaskDeleteObject();
			facebookTaskDeleteObject.Request(new FacebookTaskDeleteObject.TaskFinishedCallback(this.DeleteObjectEndCallback));
			this.m_manager.RequestProcess(facebookTaskDeleteObject);
		}
		this.m_isEndProcess = true;
	}

	// Token: 0x06004379 RID: 17273 RVA: 0x0015E1B8 File Offset: 0x0015C3B8
	protected override void OnUpdate()
	{
	}

	// Token: 0x0600437A RID: 17274 RVA: 0x0015E1BC File Offset: 0x0015C3BC
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x0600437B RID: 17275 RVA: 0x0015E1C4 File Offset: 0x0015C3C4
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x0600437C RID: 17276 RVA: 0x0015E1CC File Offset: 0x0015C3CC
	private void DeleteObjectEndCallback()
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
			myProfile.CustomData.ObjectId = string.Empty;
			myProfile.CustomData.ActionId = string.Empty;
			if (this.m_callbackObject != null)
			{
				MsgSocialNormalResponse value = new MsgSocialNormalResponse();
				this.m_callbackObject.SendMessage("DeleteGameDataEndCallback", value, SendMessageOptions.DontRequireReceiver);
			}
		}
		else if (this.m_callbackObject != null)
		{
			MsgSocialNormalResponse value2 = new MsgSocialNormalResponse();
			this.m_callbackObject.SendMessage("DeleteGameDataEndCallback", value2, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0400392B RID: 14635
	private readonly string TaskName = "FacebookTaskDeleteGameData";

	// Token: 0x0400392C RID: 14636
	private SocialTaskManager m_manager;

	// Token: 0x0400392D RID: 14637
	private GameObject m_callbackObject;

	// Token: 0x0400392E RID: 14638
	private bool m_isEndProcess;
}
