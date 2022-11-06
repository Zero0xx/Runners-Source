using System;

// Token: 0x02000823 RID: 2083
public class ServerTickerData
{
	// Token: 0x1700084D RID: 2125
	// (get) Token: 0x060037EF RID: 14319 RVA: 0x00127A58 File Offset: 0x00125C58
	public long Id
	{
		get
		{
			return this.m_id;
		}
	}

	// Token: 0x1700084E RID: 2126
	// (get) Token: 0x060037F0 RID: 14320 RVA: 0x00127A60 File Offset: 0x00125C60
	public long Start
	{
		get
		{
			return this.m_start;
		}
	}

	// Token: 0x1700084F RID: 2127
	// (get) Token: 0x060037F1 RID: 14321 RVA: 0x00127A68 File Offset: 0x00125C68
	public long End
	{
		get
		{
			return this.m_end;
		}
	}

	// Token: 0x17000850 RID: 2128
	// (get) Token: 0x060037F2 RID: 14322 RVA: 0x00127A70 File Offset: 0x00125C70
	public string Param
	{
		get
		{
			return this.m_param;
		}
	}

	// Token: 0x060037F3 RID: 14323 RVA: 0x00127A78 File Offset: 0x00125C78
	public void Init(long id, long start, long end, string param)
	{
		this.m_id = id;
		this.m_start = start;
		this.m_end = end;
		this.m_param = param;
	}

	// Token: 0x060037F4 RID: 14324 RVA: 0x00127A98 File Offset: 0x00125C98
	public void CopyTo(ServerTickerData to)
	{
		to.m_id = this.m_id;
		to.m_start = this.m_start;
		to.m_end = this.m_end;
		to.m_param = this.m_param;
	}

	// Token: 0x04002F47 RID: 12103
	private long m_id;

	// Token: 0x04002F48 RID: 12104
	private long m_start;

	// Token: 0x04002F49 RID: 12105
	private long m_end;

	// Token: 0x04002F4A RID: 12106
	private string m_param;
}
