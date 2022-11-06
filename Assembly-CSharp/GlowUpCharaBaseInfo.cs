using System;

// Token: 0x0200039D RID: 925
public class GlowUpCharaBaseInfo
{
	// Token: 0x170003E5 RID: 997
	// (get) Token: 0x06001B26 RID: 6950 RVA: 0x000A11A8 File Offset: 0x0009F3A8
	// (set) Token: 0x06001B27 RID: 6951 RVA: 0x000A11B0 File Offset: 0x0009F3B0
	public CharaType charaType
	{
		get
		{
			return this.m_charaType;
		}
		set
		{
			this.m_charaType = value;
		}
	}

	// Token: 0x170003E6 RID: 998
	// (get) Token: 0x06001B28 RID: 6952 RVA: 0x000A11BC File Offset: 0x0009F3BC
	// (set) Token: 0x06001B29 RID: 6953 RVA: 0x000A11C4 File Offset: 0x0009F3C4
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

	// Token: 0x170003E7 RID: 999
	// (get) Token: 0x06001B2A RID: 6954 RVA: 0x000A11D0 File Offset: 0x0009F3D0
	// (set) Token: 0x06001B2B RID: 6955 RVA: 0x000A11D8 File Offset: 0x0009F3D8
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

	// Token: 0x170003E8 RID: 1000
	// (get) Token: 0x06001B2C RID: 6956 RVA: 0x000A11E4 File Offset: 0x0009F3E4
	// (set) Token: 0x06001B2D RID: 6957 RVA: 0x000A11EC File Offset: 0x0009F3EC
	public int currentExp
	{
		get
		{
			return this.m_currentExp;
		}
		set
		{
			this.m_currentExp = value;
		}
	}

	// Token: 0x170003E9 RID: 1001
	// (get) Token: 0x06001B2E RID: 6958 RVA: 0x000A11F8 File Offset: 0x0009F3F8
	// (set) Token: 0x06001B2F RID: 6959 RVA: 0x000A1200 File Offset: 0x0009F400
	public bool IsActive
	{
		get
		{
			return this.m_isActive;
		}
		set
		{
			this.m_isActive = value;
		}
	}

	// Token: 0x040018C1 RID: 6337
	private CharaType m_charaType;

	// Token: 0x040018C2 RID: 6338
	private int m_level;

	// Token: 0x040018C3 RID: 6339
	private int m_levelUpCost;

	// Token: 0x040018C4 RID: 6340
	private int m_currentExp;

	// Token: 0x040018C5 RID: 6341
	private bool m_isActive;
}
