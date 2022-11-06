using System;

// Token: 0x02000219 RID: 537
public class RaidBossAttackRateTable : IComparable
{
	// Token: 0x06000E02 RID: 3586 RVA: 0x000515B0 File Offset: 0x0004F7B0
	public RaidBossAttackRateTable()
	{
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x000515B8 File Offset: 0x0004F7B8
	public RaidBossAttackRateTable(float[] data)
	{
		this.attackRate = data;
	}

	// Token: 0x17000210 RID: 528
	// (get) Token: 0x06000E04 RID: 3588 RVA: 0x000515C8 File Offset: 0x0004F7C8
	// (set) Token: 0x06000E05 RID: 3589 RVA: 0x000515D0 File Offset: 0x0004F7D0
	public float[] attackRate { get; set; }

	// Token: 0x06000E06 RID: 3590 RVA: 0x000515DC File Offset: 0x0004F7DC
	public int CompareTo(object obj)
	{
		if (this == (RaidBossAttackRateTable)obj)
		{
			return 0;
		}
		return -1;
	}
}
