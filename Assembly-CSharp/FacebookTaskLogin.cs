using System;
using Facebook;
using Message;
using UnityEngine;

// Token: 0x020009FE RID: 2558
public class FacebookTaskLogin : SocialTaskBase
{
	// Token: 0x0600438C RID: 17292 RVA: 0x0015E544 File Offset: 0x0015C744
	public FacebookTaskLogin()
	{
		this.m_callbackObject = null;
		this.m_isEndProcess = false;
	}

	// Token: 0x0600438D RID: 17293 RVA: 0x0015E568 File Offset: 0x0015C768
	public void Request(GameObject callbackObject)
	{
		this.m_callbackObject = callbackObject;
	}

	// Token: 0x0600438E RID: 17294 RVA: 0x0015E574 File Offset: 0x0015C774
	protected override void OnStartProcess()
	{
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null && !socialInterface.IsInitialized)
		{
			this.m_callbackObject.SendMessage("LoginEndCallback", null, SendMessageOptions.DontRequireReceiver);
			return;
		}
		string text = FacebookUtil.PermissionString[0];
		text += ",";
		text += FacebookUtil.PermissionString[1];
		FB.Login(text, new FacebookDelegate(this.LoginEndCallback));
	}

	// Token: 0x0600438F RID: 17295 RVA: 0x0015E5EC File Offset: 0x0015C7EC
	protected override void OnUpdate()
	{
	}

	// Token: 0x06004390 RID: 17296 RVA: 0x0015E5F0 File Offset: 0x0015C7F0
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x06004391 RID: 17297 RVA: 0x0015E5F8 File Offset: 0x0015C7F8
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x06004392 RID: 17298 RVA: 0x0015E600 File Offset: 0x0015C800
	private void LoginEndCallback(FBResult fbResult)
	{
		this.m_isEndProcess = true;
		this.m_fbResult = fbResult;
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null)
		{
			socialInterface.IsLoggedIn = FB.IsLoggedIn;
			if (socialInterface.IsLoggedIn)
			{
				FoxManager.SendLtvPoint(FoxLtvType.FacebookLogIn);
			}
		}
		global::Debug.Log("Facebook Access Token: " + FB.AccessToken);
		if (this.m_callbackObject == null)
		{
			return;
		}
		if (fbResult == null)
		{
			this.m_callbackObject.SendMessage("LoginEndCallback", null, SendMessageOptions.DontRequireReceiver);
			this.m_callbackObject = null;
			return;
		}
		if (fbResult.Text == null)
		{
			this.m_callbackObject.SendMessage("LoginEndCallback", null, SendMessageOptions.DontRequireReceiver);
			this.m_callbackObject = null;
			return;
		}
		MsgSocialNormalResponse msgSocialNormalResponse = new MsgSocialNormalResponse();
		msgSocialNormalResponse.m_result = new SocialResult
		{
			IsError = false,
			ResultId = 0,
			Result = this.m_fbResult.Error
		};
		this.m_callbackObject.SendMessage("LoginEndCallback", msgSocialNormalResponse, SendMessageOptions.DontRequireReceiver);
		this.m_callbackObject = null;
		global::Debug.Log("Facebook Login is finished");
	}

	// Token: 0x04003936 RID: 14646
	private readonly string TaskName = "FacebookTaskLogin";

	// Token: 0x04003937 RID: 14647
	private GameObject m_callbackObject;

	// Token: 0x04003938 RID: 14648
	private FBResult m_fbResult;

	// Token: 0x04003939 RID: 14649
	private bool m_isEndProcess;
}
