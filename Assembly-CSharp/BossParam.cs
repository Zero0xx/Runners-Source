using System;
using SaveData;

// Token: 0x0200085C RID: 2140
public class BossParam
{
	// Token: 0x06003A39 RID: 14905 RVA: 0x00133414 File Offset: 0x00131614
	public BossParam(string in_name, SystemData.FlagStatus flagStatus, HudTutorial.Id tutorialID, BossCharaType bossCharaType, BossCategory category, int number1, int number2)
	{
		this.m_name = in_name;
		this.m_flagStatus = flagStatus;
		this.m_tutorialID = tutorialID;
		this.m_bossCharaType = bossCharaType;
		this.m_bossCategory = category;
		this.m_layerNumber = number1;
		this.m_indexNumber = number2;
	}

	// Token: 0x04003178 RID: 12664
	public string m_name;

	// Token: 0x04003179 RID: 12665
	public SystemData.FlagStatus m_flagStatus;

	// Token: 0x0400317A RID: 12666
	public HudTutorial.Id m_tutorialID;

	// Token: 0x0400317B RID: 12667
	public BossCharaType m_bossCharaType;

	// Token: 0x0400317C RID: 12668
	public BossCategory m_bossCategory;

	// Token: 0x0400317D RID: 12669
	public int m_layerNumber;

	// Token: 0x0400317E RID: 12670
	public int m_indexNumber;
}
