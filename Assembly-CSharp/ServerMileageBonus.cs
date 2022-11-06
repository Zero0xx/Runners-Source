using System;

// Token: 0x020007E3 RID: 2019
public class ServerMileageBonus
{
	// Token: 0x06003610 RID: 13840 RVA: 0x00120814 File Offset: 0x0011EA14
	public ServerMileageBonus()
	{
		this.BonusType = ServerMileageBonus.emBonusType.Score;
		this.NumType = ServerConstants.NumType.Number;
		this.NumBonus = 0;
	}

	// Token: 0x170007B3 RID: 1971
	// (get) Token: 0x06003611 RID: 13841 RVA: 0x0012083C File Offset: 0x0011EA3C
	// (set) Token: 0x06003612 RID: 13842 RVA: 0x00120844 File Offset: 0x0011EA44
	public ServerMileageBonus.emBonusType BonusType { get; set; }

	// Token: 0x170007B4 RID: 1972
	// (get) Token: 0x06003613 RID: 13843 RVA: 0x00120850 File Offset: 0x0011EA50
	// (set) Token: 0x06003614 RID: 13844 RVA: 0x00120858 File Offset: 0x0011EA58
	public ServerConstants.NumType NumType { get; set; }

	// Token: 0x170007B5 RID: 1973
	// (get) Token: 0x06003615 RID: 13845 RVA: 0x00120864 File Offset: 0x0011EA64
	// (set) Token: 0x06003616 RID: 13846 RVA: 0x0012086C File Offset: 0x0011EA6C
	public int NumBonus { get; set; }

	// Token: 0x020007E4 RID: 2020
	public enum emBonusType
	{
		// Token: 0x04002D8A RID: 11658
		Score,
		// Token: 0x04002D8B RID: 11659
		Ring
	}
}
