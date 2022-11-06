using System;

// Token: 0x020009FF RID: 2559
public class FacebookTaskLogout : SocialTaskBase
{
	// Token: 0x06004393 RID: 17299 RVA: 0x0015E710 File Offset: 0x0015C910
	public FacebookTaskLogout()
	{
		this.m_isEndProcess = false;
	}

	// Token: 0x06004394 RID: 17300 RVA: 0x0015E72C File Offset: 0x0015C92C
	public void Request()
	{
	}

	// Token: 0x06004395 RID: 17301 RVA: 0x0015E730 File Offset: 0x0015C930
	protected override void OnStartProcess()
	{
		if (!FacebookUtil.IsLoggedIn())
		{
			this.m_isEndProcess = true;
			return;
		}
		FB.Logout();
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null)
		{
			socialInterface.IsLoggedIn = false;
		}
		this.m_isEndProcess = true;
	}

	// Token: 0x06004396 RID: 17302 RVA: 0x0015E77C File Offset: 0x0015C97C
	protected override void OnUpdate()
	{
	}

	// Token: 0x06004397 RID: 17303 RVA: 0x0015E780 File Offset: 0x0015C980
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x06004398 RID: 17304 RVA: 0x0015E788 File Offset: 0x0015C988
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x0400393A RID: 14650
	private readonly string TaskName = "FacebookTaskLogout";

	// Token: 0x0400393B RID: 14651
	private bool m_isEndProcess;
}
