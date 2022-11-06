using System;

// Token: 0x02000678 RID: 1656
public class MileageMapPointData : IComparable
{
	// Token: 0x170005AA RID: 1450
	// (get) Token: 0x06002C19 RID: 11289 RVA: 0x0010B75C File Offset: 0x0010995C
	// (set) Token: 0x06002C1A RID: 11290 RVA: 0x0010B764 File Offset: 0x00109964
	public int id { get; set; }

	// Token: 0x170005AB RID: 1451
	// (get) Token: 0x06002C1B RID: 11291 RVA: 0x0010B770 File Offset: 0x00109970
	// (set) Token: 0x06002C1C RID: 11292 RVA: 0x0010B778 File Offset: 0x00109978
	public string texture_name { get; set; }

	// Token: 0x06002C1D RID: 11293 RVA: 0x0010B784 File Offset: 0x00109984
	public int CompareTo(object obj)
	{
		if (this == (MileageMapPointData)obj)
		{
			return 0;
		}
		return -1;
	}
}
