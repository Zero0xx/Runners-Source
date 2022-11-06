using System;

// Token: 0x0200065F RID: 1631
public class MileageMapBGData : IComparable
{
	// Token: 0x170005A0 RID: 1440
	// (get) Token: 0x06002BDB RID: 11227 RVA: 0x0010AD14 File Offset: 0x00108F14
	// (set) Token: 0x06002BDC RID: 11228 RVA: 0x0010AD1C File Offset: 0x00108F1C
	public int id { get; set; }

	// Token: 0x170005A1 RID: 1441
	// (get) Token: 0x06002BDD RID: 11229 RVA: 0x0010AD28 File Offset: 0x00108F28
	// (set) Token: 0x06002BDE RID: 11230 RVA: 0x0010AD30 File Offset: 0x00108F30
	public string texture_name { get; set; }

	// Token: 0x170005A2 RID: 1442
	// (get) Token: 0x06002BDF RID: 11231 RVA: 0x0010AD3C File Offset: 0x00108F3C
	// (set) Token: 0x06002BE0 RID: 11232 RVA: 0x0010AD44 File Offset: 0x00108F44
	public string window_texture_name { get; set; }

	// Token: 0x06002BE1 RID: 11233 RVA: 0x0010AD50 File Offset: 0x00108F50
	public int CompareTo(object obj)
	{
		if (this == (MileageMapBGData)obj)
		{
			return 0;
		}
		return -1;
	}
}
