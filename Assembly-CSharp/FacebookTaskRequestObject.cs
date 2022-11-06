using System;
using Facebook;

// Token: 0x020009F8 RID: 2552
public class FacebookTaskRequestObject : SocialTaskBase
{
	// Token: 0x0600435F RID: 17247 RVA: 0x0015DBA0 File Offset: 0x0015BDA0
	public FacebookTaskRequestObject()
	{
		this.m_isEndProcess = false;
	}

	// Token: 0x06004360 RID: 17248 RVA: 0x0015DBBC File Offset: 0x0015BDBC
	public void Request(string objectId, FacebookTaskRequestObject.TaskFinishedCallback callback)
	{
		this.m_objectId = objectId;
		this.m_callback = callback;
	}

	// Token: 0x06004361 RID: 17249 RVA: 0x0015DBCC File Offset: 0x0015BDCC
	protected override void OnStartProcess()
	{
		string query = FacebookUtil.FBVersionString + this.m_objectId;
		FB.API(query, HttpMethod.GET, new FacebookDelegate(this.RequestObjectEndCallback), null);
	}

	// Token: 0x06004362 RID: 17250 RVA: 0x0015DC04 File Offset: 0x0015BE04
	protected override void OnUpdate()
	{
	}

	// Token: 0x06004363 RID: 17251 RVA: 0x0015DC08 File Offset: 0x0015BE08
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x06004364 RID: 17252 RVA: 0x0015DC10 File Offset: 0x0015BE10
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x06004365 RID: 17253 RVA: 0x0015DC18 File Offset: 0x0015BE18
	private void RequestObjectEndCallback(FBResult fbResult)
	{
		this.m_isEndProcess = true;
		if (fbResult == null)
		{
			this.m_callback(null);
			return;
		}
		if (fbResult.Text == null)
		{
			this.m_callback(null);
			return;
		}
		Debug.Log("Facebook.GetAction:" + fbResult.Text);
		this.m_callback(fbResult.Text);
	}

	// Token: 0x0400391D RID: 14621
	private readonly string TaskName = "FacebookTaskRequestObject";

	// Token: 0x0400391E RID: 14622
	private bool m_isEndProcess;

	// Token: 0x0400391F RID: 14623
	private string m_objectId;

	// Token: 0x04003920 RID: 14624
	private FacebookTaskRequestObject.TaskFinishedCallback m_callback;

	// Token: 0x02000AAB RID: 2731
	// (Invoke) Token: 0x060048E6 RID: 18662
	public delegate void TaskFinishedCallback(string responseText);
}
