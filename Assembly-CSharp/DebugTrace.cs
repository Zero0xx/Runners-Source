using System;

// Token: 0x020001AF RID: 431
public class DebugTrace
{
	// Token: 0x06000C68 RID: 3176 RVA: 0x00046FA4 File Offset: 0x000451A4
	public DebugTrace(string trace)
	{
		this.m_text = trace;
	}

	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00046FB4 File Offset: 0x000451B4
	// (set) Token: 0x06000C6A RID: 3178 RVA: 0x00046FBC File Offset: 0x000451BC
	public string text
	{
		get
		{
			return this.m_text;
		}
		private set
		{
		}
	}

	// Token: 0x040009A9 RID: 2473
	private string m_text;
}
