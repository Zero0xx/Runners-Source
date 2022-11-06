using System;

// Token: 0x02000A0E RID: 2574
public class SocialResult
{
	// Token: 0x17000936 RID: 2358
	// (get) Token: 0x06004413 RID: 17427 RVA: 0x0015FE28 File Offset: 0x0015E028
	// (set) Token: 0x06004414 RID: 17428 RVA: 0x0015FE30 File Offset: 0x0015E030
	public bool IsError
	{
		get
		{
			return this.m_isError;
		}
		set
		{
			this.m_isError = value;
		}
	}

	// Token: 0x17000937 RID: 2359
	// (get) Token: 0x06004415 RID: 17429 RVA: 0x0015FE3C File Offset: 0x0015E03C
	// (set) Token: 0x06004416 RID: 17430 RVA: 0x0015FE44 File Offset: 0x0015E044
	public int ResultId
	{
		get
		{
			return this.m_resultId;
		}
		set
		{
			this.m_resultId = value;
		}
	}

	// Token: 0x17000938 RID: 2360
	// (get) Token: 0x06004417 RID: 17431 RVA: 0x0015FE50 File Offset: 0x0015E050
	// (set) Token: 0x06004418 RID: 17432 RVA: 0x0015FE58 File Offset: 0x0015E058
	public string Result
	{
		get
		{
			return this.m_result;
		}
		set
		{
			this.m_result = value;
		}
	}

	// Token: 0x04003970 RID: 14704
	private bool m_isError;

	// Token: 0x04003971 RID: 14705
	private int m_resultId;

	// Token: 0x04003972 RID: 14706
	private string m_result;
}
