using System;

// Token: 0x0200010D RID: 269
public struct LeaderboardEntryNativeParam
{
	// Token: 0x060007F2 RID: 2034 RVA: 0x0002F054 File Offset: 0x0002D254
	public void Init(int mode, int first, int count, int type, int eventId)
	{
		this.mode = mode;
		this.first = first;
		this.count = count;
		this.type = type;
		this.eventId = eventId;
	}

	// Token: 0x04000608 RID: 1544
	public int mode;

	// Token: 0x04000609 RID: 1545
	public int first;

	// Token: 0x0400060A RID: 1546
	public int count;

	// Token: 0x0400060B RID: 1547
	public int type;

	// Token: 0x0400060C RID: 1548
	public int eventId;
}
