using System;
using Facebook;

// Token: 0x020009F7 RID: 2551
public class FacebookTaskRequestAction : SocialTaskBase
{
	// Token: 0x06004358 RID: 17240 RVA: 0x0015DAC0 File Offset: 0x0015BCC0
	public FacebookTaskRequestAction()
	{
		this.m_isEndProcess = false;
	}

	// Token: 0x06004359 RID: 17241 RVA: 0x0015DADC File Offset: 0x0015BCDC
	public void Request(string userId, string actionName, FacebookTaskRequestAction.TaskFinishedCallback callback)
	{
		this.m_userId = userId;
		this.m_actionName = actionName;
		this.m_callback = callback;
	}

	// Token: 0x0600435A RID: 17242 RVA: 0x0015DAF4 File Offset: 0x0015BCF4
	protected override void OnStartProcess()
	{
		if (!FacebookUtil.IsLoggedIn())
		{
			this.m_isEndProcess = true;
			return;
		}
		string query = FacebookUtil.FBVersionString + this.m_userId + "/" + this.m_actionName;
		FB.API(query, HttpMethod.GET, new FacebookDelegate(this.RequestActionEndCallback), null);
	}

	// Token: 0x0600435B RID: 17243 RVA: 0x0015DB48 File Offset: 0x0015BD48
	protected override void OnUpdate()
	{
	}

	// Token: 0x0600435C RID: 17244 RVA: 0x0015DB4C File Offset: 0x0015BD4C
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x0600435D RID: 17245 RVA: 0x0015DB54 File Offset: 0x0015BD54
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x0600435E RID: 17246 RVA: 0x0015DB5C File Offset: 0x0015BD5C
	private void RequestActionEndCallback(FBResult fbResult)
	{
		this.m_isEndProcess = true;
		if (fbResult == null)
		{
			return;
		}
		Debug.Log("Facebook.Object: " + fbResult.Text);
		this.m_callback(fbResult.Text);
	}

	// Token: 0x04003918 RID: 14616
	private readonly string TaskName = "FacebookTaskRequestAction";

	// Token: 0x04003919 RID: 14617
	private bool m_isEndProcess;

	// Token: 0x0400391A RID: 14618
	private string m_userId;

	// Token: 0x0400391B RID: 14619
	private string m_actionName;

	// Token: 0x0400391C RID: 14620
	private FacebookTaskRequestAction.TaskFinishedCallback m_callback;

	// Token: 0x02000AAA RID: 2730
	// (Invoke) Token: 0x060048E2 RID: 18658
	public delegate void TaskFinishedCallback(string responseText);
}
