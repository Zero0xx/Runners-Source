using System;
using Facebook;
using Message;
using UnityEngine;

// Token: 0x020009FC RID: 2556
public class FacebookTaskInitialize : SocialTaskBase
{
	// Token: 0x0600437D RID: 17277 RVA: 0x0015E27C File Offset: 0x0015C47C
	public FacebookTaskInitialize()
	{
		this.m_callbackObject = null;
		this.m_isEndProcess = false;
	}

	// Token: 0x0600437F RID: 17279 RVA: 0x0015E2A4 File Offset: 0x0015C4A4
	public void Request(GameObject callbackObject)
	{
		this.m_callbackObject = callbackObject;
	}

	// Token: 0x06004380 RID: 17280 RVA: 0x0015E2B0 File Offset: 0x0015C4B0
	protected override void OnStartProcess()
	{
		if (FacebookTaskInitialize.m_isAlreadyInit)
		{
			this.InitEndCallback();
			return;
		}
		FB.Init(new InitDelegate(this.InitEndCallback), null, null);
		FacebookTaskInitialize.m_isAlreadyInit = true;
	}

	// Token: 0x06004381 RID: 17281 RVA: 0x0015E2E8 File Offset: 0x0015C4E8
	protected override void OnUpdate()
	{
	}

	// Token: 0x06004382 RID: 17282 RVA: 0x0015E2EC File Offset: 0x0015C4EC
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x06004383 RID: 17283 RVA: 0x0015E2F4 File Offset: 0x0015C4F4
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x06004384 RID: 17284 RVA: 0x0015E2FC File Offset: 0x0015C4FC
	private void InitEndCallback()
	{
		this.m_isEndProcess = true;
		global::Debug.Log("FacebookInitialize:Facebook login is " + FB.IsLoggedIn.ToString());
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null)
		{
			socialInterface.IsLoggedIn = FB.IsLoggedIn;
			socialInterface.IsInitialized = true;
		}
		if (this.m_callbackObject == null)
		{
			return;
		}
		SocialResult socialResult = new SocialResult();
		socialResult.IsError = false;
		socialResult.ResultId = 0;
		socialResult.Result = string.Empty;
		MsgSocialNormalResponse msgSocialNormalResponse = new MsgSocialNormalResponse();
		msgSocialNormalResponse.m_result = socialResult;
		this.m_callbackObject.SendMessage("InitEndCallback", msgSocialNormalResponse, SendMessageOptions.DontRequireReceiver);
		this.m_callbackObject = null;
	}

	// Token: 0x0400392F RID: 14639
	private readonly string TaskName = "FacebookTaskInitialize";

	// Token: 0x04003930 RID: 14640
	private GameObject m_callbackObject;

	// Token: 0x04003931 RID: 14641
	private bool m_isEndProcess;

	// Token: 0x04003932 RID: 14642
	private static bool m_isAlreadyInit;
}
