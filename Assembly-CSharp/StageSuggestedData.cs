using System;

// Token: 0x0200067C RID: 1660
public class StageSuggestedData : IComparable
{
	// Token: 0x170005B1 RID: 1457
	// (get) Token: 0x06002C50 RID: 11344 RVA: 0x0010C390 File Offset: 0x0010A590
	// (set) Token: 0x06002C51 RID: 11345 RVA: 0x0010C398 File Offset: 0x0010A598
	public int id { get; set; }

	// Token: 0x170005B2 RID: 1458
	// (get) Token: 0x06002C52 RID: 11346 RVA: 0x0010C3A4 File Offset: 0x0010A5A4
	// (set) Token: 0x06002C53 RID: 11347 RVA: 0x0010C3AC File Offset: 0x0010A5AC
	public CharacterAttribute[] charaAttribute { get; set; }

	// Token: 0x06002C54 RID: 11348 RVA: 0x0010C3B8 File Offset: 0x0010A5B8
	public int CompareTo(object obj)
	{
		if (this == (StageSuggestedData)obj)
		{
			return 0;
		}
		return -1;
	}
}
