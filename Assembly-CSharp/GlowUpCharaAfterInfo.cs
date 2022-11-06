using System;
using System.Collections.Generic;

// Token: 0x0200039E RID: 926
public class GlowUpCharaAfterInfo
{
	// Token: 0x170003EA RID: 1002
	// (get) Token: 0x06001B31 RID: 6961 RVA: 0x000A1214 File Offset: 0x0009F414
	// (set) Token: 0x06001B32 RID: 6962 RVA: 0x000A121C File Offset: 0x0009F41C
	public int level
	{
		get
		{
			return this.m_level;
		}
		set
		{
			this.m_level = value;
		}
	}

	// Token: 0x170003EB RID: 1003
	// (get) Token: 0x06001B33 RID: 6963 RVA: 0x000A1228 File Offset: 0x0009F428
	// (set) Token: 0x06001B34 RID: 6964 RVA: 0x000A1230 File Offset: 0x0009F430
	public int levelUpCost
	{
		get
		{
			return this.m_levelUpCost;
		}
		set
		{
			this.m_levelUpCost = value;
		}
	}

	// Token: 0x170003EC RID: 1004
	// (get) Token: 0x06001B35 RID: 6965 RVA: 0x000A123C File Offset: 0x0009F43C
	// (set) Token: 0x06001B36 RID: 6966 RVA: 0x000A1244 File Offset: 0x0009F444
	public int exp
	{
		get
		{
			return this.m_exp;
		}
		set
		{
			this.m_exp = value;
		}
	}

	// Token: 0x170003ED RID: 1005
	// (get) Token: 0x06001B37 RID: 6967 RVA: 0x000A1250 File Offset: 0x0009F450
	// (set) Token: 0x06001B38 RID: 6968 RVA: 0x000A1258 File Offset: 0x0009F458
	public List<AbilityType> abilityList
	{
		get
		{
			return this.m_abilityList;
		}
		set
		{
			this.m_abilityList = value;
		}
	}

	// Token: 0x170003EE RID: 1006
	// (get) Token: 0x06001B39 RID: 6969 RVA: 0x000A1264 File Offset: 0x0009F464
	// (set) Token: 0x06001B3A RID: 6970 RVA: 0x000A126C File Offset: 0x0009F46C
	public List<int> abilityListExp
	{
		get
		{
			return this.m_abilityListExp;
		}
		set
		{
			this.m_abilityListExp = value;
		}
	}

	// Token: 0x040018C6 RID: 6342
	private int m_level;

	// Token: 0x040018C7 RID: 6343
	private int m_levelUpCost;

	// Token: 0x040018C8 RID: 6344
	private int m_exp;

	// Token: 0x040018C9 RID: 6345
	private List<AbilityType> m_abilityList;

	// Token: 0x040018CA RID: 6346
	private List<int> m_abilityListExp;
}
