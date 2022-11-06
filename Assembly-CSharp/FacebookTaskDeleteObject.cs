using System;
using Facebook;

// Token: 0x020009F6 RID: 2550
public class FacebookTaskDeleteObject : SocialTaskBase
{
	// Token: 0x06004351 RID: 17233 RVA: 0x0015D9EC File Offset: 0x0015BBEC
	public FacebookTaskDeleteObject()
	{
		this.m_isEndProcess = false;
	}

	// Token: 0x06004352 RID: 17234 RVA: 0x0015DA08 File Offset: 0x0015BC08
	public void Request(FacebookTaskDeleteObject.TaskFinishedCallback callback)
	{
		this.m_callback = callback;
	}

	// Token: 0x06004353 RID: 17235 RVA: 0x0015DA14 File Offset: 0x0015BC14
	protected override void OnStartProcess()
	{
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface == null)
		{
			return;
		}
		SocialUserData myProfile = socialInterface.MyProfile;
		string objectId = myProfile.CustomData.ObjectId;
		string query = FacebookUtil.FBVersionString + objectId;
		FB.API(query, HttpMethod.DELETE, new FacebookDelegate(this.DeleteObjectEndCallback), null);
	}

	// Token: 0x06004354 RID: 17236 RVA: 0x0015DA70 File Offset: 0x0015BC70
	protected override void OnUpdate()
	{
	}

	// Token: 0x06004355 RID: 17237 RVA: 0x0015DA74 File Offset: 0x0015BC74
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x06004356 RID: 17238 RVA: 0x0015DA7C File Offset: 0x0015BC7C
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x06004357 RID: 17239 RVA: 0x0015DA84 File Offset: 0x0015BC84
	private void DeleteObjectEndCallback(FBResult fbResult)
	{
		this.m_isEndProcess = true;
		if (fbResult == null)
		{
			return;
		}
		Debug.Log("Facebook.DeleteObject:" + fbResult.Text);
		this.m_callback();
	}

	// Token: 0x04003912 RID: 14610
	private readonly string TaskName = "FacebookTaskDeleteObject";

	// Token: 0x04003913 RID: 14611
	private bool m_isEndProcess;

	// Token: 0x04003914 RID: 14612
	private string m_actionName;

	// Token: 0x04003915 RID: 14613
	private string m_postObjectName;

	// Token: 0x04003916 RID: 14614
	private string m_postObjectId;

	// Token: 0x04003917 RID: 14615
	private FacebookTaskDeleteObject.TaskFinishedCallback m_callback;

	// Token: 0x02000AA9 RID: 2729
	// (Invoke) Token: 0x060048DE RID: 18654
	public delegate void TaskFinishedCallback();
}
