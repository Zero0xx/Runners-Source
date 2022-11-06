using System;

// Token: 0x0200020D RID: 525
public class EventMission
{
	// Token: 0x06000DCA RID: 3530 RVA: 0x00050FFC File Offset: 0x0004F1FC
	public EventMission(string name, long point, int reward, int index)
	{
		this.m_name = name;
		this.m_point = point;
		this.m_reward = reward;
		this.m_index = index;
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x00051038 File Offset: 0x0004F238
	public bool IsAttainment(long point)
	{
		bool result = false;
		if (point >= this.m_point)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00051058 File Offset: 0x0004F258
	public string name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00051060 File Offset: 0x0004F260
	public long point
	{
		get
		{
			return this.m_point;
		}
	}

	// Token: 0x170001FA RID: 506
	// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00051068 File Offset: 0x0004F268
	public int reward
	{
		get
		{
			return this.m_reward;
		}
	}

	// Token: 0x170001FB RID: 507
	// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00051070 File Offset: 0x0004F270
	public int index
	{
		get
		{
			return this.m_index;
		}
	}

	// Token: 0x04000BC1 RID: 3009
	private string m_name = string.Empty;

	// Token: 0x04000BC2 RID: 3010
	private long m_point;

	// Token: 0x04000BC3 RID: 3011
	private int m_reward;

	// Token: 0x04000BC4 RID: 3012
	private int m_index;
}
