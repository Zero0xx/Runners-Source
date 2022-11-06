using System;

// Token: 0x020004FF RID: 1279
public class LeagueTypeParam
{
	// Token: 0x06002632 RID: 9778 RVA: 0x000E8F38 File Offset: 0x000E7138
	public LeagueTypeParam(LeagueCategory category, string categoryName)
	{
		this.m_category = category;
		this.m_categoryName = categoryName;
	}

	// Token: 0x04002296 RID: 8854
	public LeagueCategory m_category;

	// Token: 0x04002297 RID: 8855
	public string m_categoryName;
}
