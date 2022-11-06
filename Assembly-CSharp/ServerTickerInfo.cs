using System;
using System.Collections.Generic;

// Token: 0x02000824 RID: 2084
public class ServerTickerInfo
{
	// Token: 0x17000851 RID: 2129
	// (get) Token: 0x060037F6 RID: 14326 RVA: 0x00127AEC File Offset: 0x00125CEC
	// (set) Token: 0x060037F7 RID: 14327 RVA: 0x00127AF4 File Offset: 0x00125CF4
	public bool ExistNewData
	{
		get
		{
			return this.m_existNewData;
		}
		set
		{
			this.m_existNewData = value;
		}
	}

	// Token: 0x17000852 RID: 2130
	// (get) Token: 0x060037F8 RID: 14328 RVA: 0x00127B00 File Offset: 0x00125D00
	// (set) Token: 0x060037F9 RID: 14329 RVA: 0x00127B08 File Offset: 0x00125D08
	public int TickerIndex
	{
		get
		{
			return this.m_tickerIndex;
		}
		set
		{
			if (this.m_tickerIndex != value)
			{
				this.m_existNewData = true;
				this.m_tickerIndex = value;
			}
			else
			{
				this.m_existNewData = false;
			}
		}
	}

	// Token: 0x17000853 RID: 2131
	// (get) Token: 0x060037FA RID: 14330 RVA: 0x00127B3C File Offset: 0x00125D3C
	// (set) Token: 0x060037FB RID: 14331 RVA: 0x00127B44 File Offset: 0x00125D44
	public List<ServerTickerData> Data
	{
		get
		{
			return this.m_data;
		}
		private set
		{
		}
	}

	// Token: 0x060037FC RID: 14332 RVA: 0x00127B48 File Offset: 0x00125D48
	public void Init(int tickerIndex)
	{
		this.m_existNewData = true;
		this.m_tickerIndex = tickerIndex;
	}

	// Token: 0x04002F4B RID: 12107
	private bool m_existNewData;

	// Token: 0x04002F4C RID: 12108
	private int m_tickerIndex;

	// Token: 0x04002F4D RID: 12109
	private List<ServerTickerData> m_data = new List<ServerTickerData>();
}
