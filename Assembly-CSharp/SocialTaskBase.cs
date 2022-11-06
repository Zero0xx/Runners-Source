using System;

// Token: 0x02000A0F RID: 2575
public abstract class SocialTaskBase
{
	// Token: 0x06004419 RID: 17433 RVA: 0x0015FE64 File Offset: 0x0015E064
	public SocialTaskBase()
	{
		this.m_state = SocialTaskBase.ProcessState.IDLE;
	}

	// Token: 0x0600441A RID: 17434 RVA: 0x0015FE74 File Offset: 0x0015E074
	public bool IsDone()
	{
		return this.m_state == SocialTaskBase.ProcessState.END;
	}

	// Token: 0x0600441B RID: 17435 RVA: 0x0015FE88 File Offset: 0x0015E088
	public void Update()
	{
		switch (this.m_state)
		{
		case SocialTaskBase.ProcessState.IDLE:
			this.OnStartProcess();
			this.m_state = SocialTaskBase.ProcessState.PROCESSING;
			break;
		case SocialTaskBase.ProcessState.PROCESSING:
			this.OnUpdate();
			if (this.OnIsEndProcess())
			{
				this.m_state = SocialTaskBase.ProcessState.END;
			}
			break;
		}
	}

	// Token: 0x0600441C RID: 17436 RVA: 0x0015FEE8 File Offset: 0x0015E0E8
	public string GetTaskName()
	{
		return this.OnGetTaskName();
	}

	// Token: 0x0600441D RID: 17437
	protected abstract void OnStartProcess();

	// Token: 0x0600441E RID: 17438
	protected abstract void OnUpdate();

	// Token: 0x0600441F RID: 17439
	protected abstract bool OnIsEndProcess();

	// Token: 0x06004420 RID: 17440
	protected abstract string OnGetTaskName();

	// Token: 0x04003973 RID: 14707
	private SocialTaskBase.ProcessState m_state;

	// Token: 0x02000A10 RID: 2576
	public enum ProcessState
	{
		// Token: 0x04003975 RID: 14709
		NONE = -1,
		// Token: 0x04003976 RID: 14710
		IDLE,
		// Token: 0x04003977 RID: 14711
		PROCESSING,
		// Token: 0x04003978 RID: 14712
		END
	}
}
