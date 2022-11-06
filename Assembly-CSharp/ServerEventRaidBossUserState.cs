using System;

// Token: 0x020007DF RID: 2015
public class ServerEventRaidBossUserState
{
	// Token: 0x060035CD RID: 13773 RVA: 0x00120328 File Offset: 0x0011E528
	public ServerEventRaidBossUserState()
	{
		this.m_wrestleId = string.Empty;
		this.m_name = string.Empty;
		this.m_grade = 0;
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
		this.m_wrestleCount = 0;
		this.m_wrestleDamage = 0;
		this.m_wrestleBeatFlg = false;
	}

	// Token: 0x17000795 RID: 1941
	// (get) Token: 0x060035CE RID: 13774 RVA: 0x001203C4 File Offset: 0x0011E5C4
	// (set) Token: 0x060035CF RID: 13775 RVA: 0x001203CC File Offset: 0x0011E5CC
	public string WrestleId
	{
		get
		{
			return this.m_wrestleId;
		}
		set
		{
			this.m_wrestleId = value;
		}
	}

	// Token: 0x17000796 RID: 1942
	// (get) Token: 0x060035D0 RID: 13776 RVA: 0x001203D8 File Offset: 0x0011E5D8
	// (set) Token: 0x060035D1 RID: 13777 RVA: 0x001203E0 File Offset: 0x0011E5E0
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

	// Token: 0x17000797 RID: 1943
	// (get) Token: 0x060035D2 RID: 13778 RVA: 0x001203EC File Offset: 0x0011E5EC
	// (set) Token: 0x060035D3 RID: 13779 RVA: 0x001203F4 File Offset: 0x0011E5F4
	public int Grade
	{
		get
		{
			return this.m_grade;
		}
		set
		{
			this.m_grade = value;
		}
	}

	// Token: 0x17000798 RID: 1944
	// (get) Token: 0x060035D4 RID: 13780 RVA: 0x00120400 File Offset: 0x0011E600
	// (set) Token: 0x060035D5 RID: 13781 RVA: 0x00120408 File Offset: 0x0011E608
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

	// Token: 0x17000799 RID: 1945
	// (get) Token: 0x060035D6 RID: 13782 RVA: 0x00120414 File Offset: 0x0011E614
	// (set) Token: 0x060035D7 RID: 13783 RVA: 0x0012041C File Offset: 0x0011E61C
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

	// Token: 0x1700079A RID: 1946
	// (get) Token: 0x060035D8 RID: 13784 RVA: 0x00120428 File Offset: 0x0011E628
	// (set) Token: 0x060035D9 RID: 13785 RVA: 0x00120430 File Offset: 0x0011E630
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

	// Token: 0x1700079B RID: 1947
	// (get) Token: 0x060035DA RID: 13786 RVA: 0x0012043C File Offset: 0x0011E63C
	// (set) Token: 0x060035DB RID: 13787 RVA: 0x00120444 File Offset: 0x0011E644
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

	// Token: 0x1700079C RID: 1948
	// (get) Token: 0x060035DC RID: 13788 RVA: 0x00120450 File Offset: 0x0011E650
	// (set) Token: 0x060035DD RID: 13789 RVA: 0x00120458 File Offset: 0x0011E658
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

	// Token: 0x1700079D RID: 1949
	// (get) Token: 0x060035DE RID: 13790 RVA: 0x00120464 File Offset: 0x0011E664
	// (set) Token: 0x060035DF RID: 13791 RVA: 0x0012046C File Offset: 0x0011E66C
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

	// Token: 0x1700079E RID: 1950
	// (get) Token: 0x060035E0 RID: 13792 RVA: 0x00120478 File Offset: 0x0011E678
	// (set) Token: 0x060035E1 RID: 13793 RVA: 0x00120480 File Offset: 0x0011E680
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

	// Token: 0x1700079F RID: 1951
	// (get) Token: 0x060035E2 RID: 13794 RVA: 0x0012048C File Offset: 0x0011E68C
	// (set) Token: 0x060035E3 RID: 13795 RVA: 0x00120494 File Offset: 0x0011E694
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

	// Token: 0x170007A0 RID: 1952
	// (get) Token: 0x060035E4 RID: 13796 RVA: 0x001204A0 File Offset: 0x0011E6A0
	// (set) Token: 0x060035E5 RID: 13797 RVA: 0x001204A8 File Offset: 0x0011E6A8
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

	// Token: 0x170007A1 RID: 1953
	// (get) Token: 0x060035E6 RID: 13798 RVA: 0x001204B4 File Offset: 0x0011E6B4
	// (set) Token: 0x060035E7 RID: 13799 RVA: 0x001204BC File Offset: 0x0011E6BC
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

	// Token: 0x170007A2 RID: 1954
	// (get) Token: 0x060035E8 RID: 13800 RVA: 0x001204C8 File Offset: 0x0011E6C8
	// (set) Token: 0x060035E9 RID: 13801 RVA: 0x001204D0 File Offset: 0x0011E6D0
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

	// Token: 0x170007A3 RID: 1955
	// (get) Token: 0x060035EA RID: 13802 RVA: 0x001204DC File Offset: 0x0011E6DC
	// (set) Token: 0x060035EB RID: 13803 RVA: 0x001204E4 File Offset: 0x0011E6E4
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

	// Token: 0x170007A4 RID: 1956
	// (get) Token: 0x060035EC RID: 13804 RVA: 0x001204F0 File Offset: 0x0011E6F0
	// (set) Token: 0x060035ED RID: 13805 RVA: 0x001204F8 File Offset: 0x0011E6F8
	public int WrestleCount
	{
		get
		{
			return this.m_wrestleCount;
		}
		set
		{
			this.m_wrestleCount = value;
		}
	}

	// Token: 0x170007A5 RID: 1957
	// (get) Token: 0x060035EE RID: 13806 RVA: 0x00120504 File Offset: 0x0011E704
	// (set) Token: 0x060035EF RID: 13807 RVA: 0x0012050C File Offset: 0x0011E70C
	public int WrestleDamage
	{
		get
		{
			return this.m_wrestleDamage;
		}
		set
		{
			this.m_wrestleDamage = value;
		}
	}

	// Token: 0x170007A6 RID: 1958
	// (get) Token: 0x060035F0 RID: 13808 RVA: 0x00120518 File Offset: 0x0011E718
	// (set) Token: 0x060035F1 RID: 13809 RVA: 0x00120520 File Offset: 0x0011E720
	public bool WrestleBeatFlg
	{
		get
		{
			return this.m_wrestleBeatFlg;
		}
		set
		{
			this.m_wrestleBeatFlg = value;
		}
	}

	// Token: 0x060035F2 RID: 13810 RVA: 0x0012052C File Offset: 0x0011E72C
	public void CopyTo(ServerEventRaidBossUserState to)
	{
		to.m_wrestleId = this.m_wrestleId;
		to.m_name = this.m_name;
		to.m_grade = this.m_grade;
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
		to.m_wrestleCount = this.m_wrestleCount;
		to.m_wrestleDamage = this.m_wrestleDamage;
		to.m_wrestleBeatFlg = this.m_wrestleBeatFlg;
	}

	// Token: 0x04002D69 RID: 11625
	private string m_wrestleId;

	// Token: 0x04002D6A RID: 11626
	private string m_name;

	// Token: 0x04002D6B RID: 11627
	private int m_grade;

	// Token: 0x04002D6C RID: 11628
	private int m_numRank;

	// Token: 0x04002D6D RID: 11629
	private int m_loginTime;

	// Token: 0x04002D6E RID: 11630
	private int m_charaId;

	// Token: 0x04002D6F RID: 11631
	private int m_charaLevel;

	// Token: 0x04002D70 RID: 11632
	private int m_subCharaId;

	// Token: 0x04002D71 RID: 11633
	private int m_subCharaLevel;

	// Token: 0x04002D72 RID: 11634
	private int m_mainChaoId;

	// Token: 0x04002D73 RID: 11635
	private int m_mainChaoLevel;

	// Token: 0x04002D74 RID: 11636
	private int m_subChaoId;

	// Token: 0x04002D75 RID: 11637
	private int m_subChaoLevel;

	// Token: 0x04002D76 RID: 11638
	private int m_language;

	// Token: 0x04002D77 RID: 11639
	private int m_league;

	// Token: 0x04002D78 RID: 11640
	private int m_wrestleCount;

	// Token: 0x04002D79 RID: 11641
	private int m_wrestleDamage;

	// Token: 0x04002D7A RID: 11642
	private bool m_wrestleBeatFlg;
}
