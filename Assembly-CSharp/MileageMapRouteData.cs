using System;

// Token: 0x02000679 RID: 1657
public class MileageMapRouteData : IComparable
{
	// Token: 0x170005AC RID: 1452
	// (get) Token: 0x06002C1F RID: 11295 RVA: 0x0010B7A0 File Offset: 0x001099A0
	// (set) Token: 0x06002C20 RID: 11296 RVA: 0x0010B7A8 File Offset: 0x001099A8
	public int id { get; set; }

	// Token: 0x170005AD RID: 1453
	// (get) Token: 0x06002C21 RID: 11297 RVA: 0x0010B7B4 File Offset: 0x001099B4
	// (set) Token: 0x06002C22 RID: 11298 RVA: 0x0010B7BC File Offset: 0x001099BC
	public MileageBonus ability_type { get; set; }

	// Token: 0x170005AE RID: 1454
	// (get) Token: 0x06002C23 RID: 11299 RVA: 0x0010B7C8 File Offset: 0x001099C8
	// (set) Token: 0x06002C24 RID: 11300 RVA: 0x0010B7D0 File Offset: 0x001099D0
	public float ability_value { get; set; }

	// Token: 0x170005AF RID: 1455
	// (get) Token: 0x06002C25 RID: 11301 RVA: 0x0010B7DC File Offset: 0x001099DC
	// (set) Token: 0x06002C26 RID: 11302 RVA: 0x0010B7E4 File Offset: 0x001099E4
	public int effect_flag { get; set; }

	// Token: 0x170005B0 RID: 1456
	// (get) Token: 0x06002C27 RID: 11303 RVA: 0x0010B7F0 File Offset: 0x001099F0
	// (set) Token: 0x06002C28 RID: 11304 RVA: 0x0010B7F8 File Offset: 0x001099F8
	public string texture_name { get; set; }

	// Token: 0x06002C29 RID: 11305 RVA: 0x0010B804 File Offset: 0x00109A04
	public int CompareTo(object obj)
	{
		if (this == (MileageMapRouteData)obj)
		{
			return 0;
		}
		return -1;
	}
}
