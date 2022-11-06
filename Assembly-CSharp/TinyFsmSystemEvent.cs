using System;

// Token: 0x020002C6 RID: 710
public class TinyFsmSystemEvent
{
	// Token: 0x0600143C RID: 5180 RVA: 0x0006CF64 File Offset: 0x0006B164
	protected TinyFsmSystemEvent(int sig)
	{
		this.m_sig = sig;
	}

	// Token: 0x17000356 RID: 854
	// (get) Token: 0x0600143D RID: 5181 RVA: 0x0006CF74 File Offset: 0x0006B174
	public int Signal
	{
		get
		{
			return this.m_sig;
		}
	}

	// Token: 0x0400119F RID: 4511
	private int m_sig;
}
