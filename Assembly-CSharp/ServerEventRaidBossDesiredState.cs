using System;

// Token: 0x020007DC RID: 2012
public class ServerEventRaidBossDesiredState
{
	// Token: 0x06003594 RID: 13716 RVA: 0x0011FE88 File Offset: 0x0011E088
	public ServerEventRaidBossDesiredState()
	{
		this.m_desireId = string.Empty;
		this.m_name = string.Empty;
		this.m_numRank = 0;
		this.m_loginTime = 0;
		this.m_charaId = 0;
		this.m_charaLevel = 0;
		this.m_subCharaId = 0;
		this.m_subCharaLevel = 0;
		this.m_mainChaoId = 0;
		this.m_mainChaoLevel = 0;
		this.m_subChaoId = 0;
		this.m_subChaoLevel = 0;
		this.m_language = 0;
		this.m_league = 0;
		this.m_numBeatedEnterprise = 0;
	}

	// Token: 0x1700077B RID: 1915
	// (get) Token: 0x06003595 RID: 13717 RVA: 0x0011FF0C File Offset: 0x0011E10C
	// (set) Token: 0x06003596 RID: 13718 RVA: 0x0011FF14 File Offset: 0x0011E114
	public string DesireId
	{
		get
		{
			return this.m_desireId;
		}
		set
		{
			this.m_desireId = value;
		}
	}

	// Token: 0x1700077C RID: 1916
	// (get) Token: 0x06003597 RID: 13719 RVA: 0x0011FF20 File Offset: 0x0011E120
	// (set) Token: 0x06003598 RID: 13720 RVA: 0x0011FF28 File Offset: 0x0011E128
	public string UserName
	{
		get
		{
			return this.m_name;
		}
		set
		{
			this.m_name = value;
		}
	}

	// Token: 0x1700077D RID: 1917
	// (get) Token: 0x06003599 RID: 13721 RVA: 0x0011FF34 File Offset: 0x0011E134
	// (set) Token: 0x0600359A RID: 13722 RVA: 0x0011FF3C File Offset: 0x0011E13C
	public int NumRank
	{
		get
		{
			return this.m_numRank;
		}
		set
		{
			this.m_numRank = value;
		}
	}

	// Token: 0x1700077E RID: 1918
	// (get) Token: 0x0600359B RID: 13723 RVA: 0x0011FF48 File Offset: 0x0011E148
	// (set) Token: 0x0600359C RID: 13724 RVA: 0x0011FF50 File Offset: 0x0011E150
	public int LoginTime
	{
		get
		{
			return this.m_loginTime;
		}
		set
		{
			this.m_loginTime = value;
		}
	}

	// Token: 0x1700077F RID: 1919
	// (get) Token: 0x0600359D RID: 13725 RVA: 0x0011FF5C File Offset: 0x0011E15C
	// (set) Token: 0x0600359E RID: 13726 RVA: 0x0011FF64 File Offset: 0x0011E164
	public int CharaId
	{
		get
		{
			return this.m_charaId;
		}
		set
		{
			this.m_charaId = value;
		}
	}

	// Token: 0x17000780 RID: 1920
	// (get) Token: 0x0600359F RID: 13727 RVA: 0x0011FF70 File Offset: 0x0011E170
	// (set) Token: 0x060035A0 RID: 13728 RVA: 0x0011FF78 File Offset: 0x0011E178
	public int CharaLevel
	{
		get
		{
			return this.m_charaLevel;
		}
		set
		{
			this.m_charaLevel = value;
		}
	}

	// Token: 0x17000781 RID: 1921
	// (get) Token: 0x060035A1 RID: 13729 RVA: 0x0011FF84 File Offset: 0x0011E184
	// (set) Token: 0x060035A2 RID: 13730 RVA: 0x0011FF8C File Offset: 0x0011E18C
	public int SubCharaId
	{
		get
		{
			return this.m_subCharaId;
		}
		set
		{
			this.m_subCharaId = value;
		}
	}

	// Token: 0x17000782 RID: 1922
	// (get) Token: 0x060035A3 RID: 13731 RVA: 0x0011FF98 File Offset: 0x0011E198
	// (set) Token: 0x060035A4 RID: 13732 RVA: 0x0011FFA0 File Offset: 0x0011E1A0
	public int SubCharaLevel
	{
		get
		{
			return this.m_subCharaLevel;
		}
		set
		{
			this.m_subCharaLevel = value;
		}
	}

	// Token: 0x17000783 RID: 1923
	// (get) Token: 0x060035A5 RID: 13733 RVA: 0x0011FFAC File Offset: 0x0011E1AC
	// (set) Token: 0x060035A6 RID: 13734 RVA: 0x0011FFB4 File Offset: 0x0011E1B4
	public int MainChaoId
	{
		get
		{
			return this.m_mainChaoId;
		}
		set
		{
			this.m_mainChaoId = value;
		}
	}

	// Token: 0x17000784 RID: 1924
	// (get) Token: 0x060035A7 RID: 13735 RVA: 0x0011FFC0 File Offset: 0x0011E1C0
	// (set) Token: 0x060035A8 RID: 13736 RVA: 0x0011FFC8 File Offset: 0x0011E1C8
	public int MainChaoLevel
	{
		get
		{
			return this.m_mainChaoLevel;
		}
		set
		{
			this.m_mainChaoLevel = value;
		}
	}

	// Token: 0x17000785 RID: 1925
	// (get) Token: 0x060035A9 RID: 13737 RVA: 0x0011FFD4 File Offset: 0x0011E1D4
	// (set) Token: 0x060035AA RID: 13738 RVA: 0x0011FFDC File Offset: 0x0011E1DC
	public int SubChaoId
	{
		get
		{
			return this.m_subChaoId;
		}
		set
		{
			this.m_subChaoId = value;
		}
	}

	// Token: 0x17000786 RID: 1926
	// (get) Token: 0x060035AB RID: 13739 RVA: 0x0011FFE8 File Offset: 0x0011E1E8
	// (set) Token: 0x060035AC RID: 13740 RVA: 0x0011FFF0 File Offset: 0x0011E1F0
	public int SubChaoLevel
	{
		get
		{
			return this.m_subChaoLevel;
		}
		set
		{
			this.m_subChaoLevel = value;
		}
	}

	// Token: 0x17000787 RID: 1927
	// (get) Token: 0x060035AD RID: 13741 RVA: 0x0011FFFC File Offset: 0x0011E1FC
	// (set) Token: 0x060035AE RID: 13742 RVA: 0x00120004 File Offset: 0x0011E204
	public int Language
	{
		get
		{
			return this.m_language;
		}
		set
		{
			this.m_language = value;
		}
	}

	// Token: 0x17000788 RID: 1928
	// (get) Token: 0x060035AF RID: 13743 RVA: 0x00120010 File Offset: 0x0011E210
	// (set) Token: 0x060035B0 RID: 13744 RVA: 0x00120018 File Offset: 0x0011E218
	public int League
	{
		get
		{
			return this.m_league;
		}
		set
		{
			this.m_league = value;
		}
	}

	// Token: 0x17000789 RID: 1929
	// (get) Token: 0x060035B1 RID: 13745 RVA: 0x00120024 File Offset: 0x0011E224
	// (set) Token: 0x060035B2 RID: 13746 RVA: 0x0012002C File Offset: 0x0011E22C
	public int NumBeatedEnterprise
	{
		get
		{
			return this.m_numBeatedEnterprise;
		}
		set
		{
			this.m_numBeatedEnterprise = value;
		}
	}

	// Token: 0x060035B3 RID: 13747 RVA: 0x00120038 File Offset: 0x0011E238
	public void CopyTo(ServerEventRaidBossDesiredState to)
	{
		to.m_desireId = this.m_desireId;
		to.m_name = this.m_name;
		to.m_numRank = this.m_numRank;
		to.m_loginTime = this.m_loginTime;
		to.m_charaId = this.m_charaId;
		to.m_charaLevel = this.m_charaLevel;
		to.m_subCharaId = this.m_subCharaId;
		to.m_subCharaLevel = this.m_subCharaLevel;
		to.m_mainChaoId = this.m_mainChaoId;
		to.m_mainChaoLevel = this.m_mainChaoLevel;
		to.m_subChaoId = this.m_subChaoId;
		to.m_subChaoLevel = this.m_subChaoLevel;
		to.m_language = this.m_language;
		to.m_league = this.m_league;
		to.m_numBeatedEnterprise = this.m_numBeatedEnterprise;
	}

	// Token: 0x04002D49 RID: 11593
	private string m_desireId;

	// Token: 0x04002D4A RID: 11594
	private string m_name;

	// Token: 0x04002D4B RID: 11595
	private int m_numRank;

	// Token: 0x04002D4C RID: 11596
	private int m_loginTime;

	// Token: 0x04002D4D RID: 11597
	private int m_charaId;

	// Token: 0x04002D4E RID: 11598
	private int m_charaLevel;

	// Token: 0x04002D4F RID: 11599
	private int m_subCharaId;

	// Token: 0x04002D50 RID: 11600
	private int m_subCharaLevel;

	// Token: 0x04002D51 RID: 11601
	private int m_mainChaoId;

	// Token: 0x04002D52 RID: 11602
	private int m_mainChaoLevel;

	// Token: 0x04002D53 RID: 11603
	private int m_subChaoId;

	// Token: 0x04002D54 RID: 11604
	private int m_subChaoLevel;

	// Token: 0x04002D55 RID: 11605
	private int m_language;

	// Token: 0x04002D56 RID: 11606
	private int m_league;

	// Token: 0x04002D57 RID: 11607
	private int m_numBeatedEnterprise;
}
