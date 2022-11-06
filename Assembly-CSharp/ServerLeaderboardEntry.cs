using System;
using App;

// Token: 0x02000804 RID: 2052
public class ServerLeaderboardEntry
{
	// Token: 0x060036EF RID: 14063 RVA: 0x00122D3C File Offset: 0x00120F3C
	public ServerLeaderboardEntry()
	{
		this.m_hspId = "0123456789abcdef";
		this.m_score = 0L;
		this.m_hiScore = 0L;
		this.m_userData = 0;
		this.m_name = "0123456789abcdef";
		this.m_url = "0123456789abcdef";
		this.m_energyFlg = false;
		this.m_grade = 0;
		this.m_gradeChanged = 0;
		this.m_expireTime = 0L;
	}

	// Token: 0x060036F0 RID: 14064 RVA: 0x00122DA4 File Offset: 0x00120FA4
	public void CopyTo(ServerLeaderboardEntry to)
	{
		to.m_gradeChanged = ((to.m_grade == 0) ? 0 : (to.m_grade - this.m_grade));
		to.m_hspId = this.m_hspId;
		to.m_score = this.m_score;
		to.m_hiScore = this.m_hiScore;
		to.m_userData = this.m_userData;
		to.m_name = this.m_name;
		to.m_url = this.m_url;
		to.m_energyFlg = this.m_energyFlg;
		to.m_grade = this.m_grade;
		to.m_expireTime = this.m_expireTime;
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
		to.m_leagueIndex = this.m_leagueIndex;
	}

	// Token: 0x060036F1 RID: 14065 RVA: 0x00122ED4 File Offset: 0x001210D4
	public ServerLeaderboardEntry Clone()
	{
		return new ServerLeaderboardEntry
		{
			m_gradeChanged = this.m_gradeChanged,
			m_hspId = this.m_hspId,
			m_score = this.m_score,
			m_hiScore = this.m_hiScore,
			m_userData = this.m_userData,
			m_name = this.m_name,
			m_url = this.m_url,
			m_energyFlg = this.m_energyFlg,
			m_grade = this.m_grade,
			m_expireTime = this.m_expireTime,
			m_numRank = this.m_numRank,
			m_loginTime = this.m_loginTime,
			m_charaId = this.m_charaId,
			m_charaLevel = this.m_charaLevel,
			m_subCharaId = this.m_subCharaId,
			m_subCharaLevel = this.m_subCharaLevel,
			m_mainChaoId = this.m_mainChaoId,
			m_mainChaoLevel = this.m_mainChaoLevel,
			m_subChaoId = this.m_subChaoId,
			m_subChaoLevel = this.m_subChaoLevel,
			m_language = this.m_language,
			m_leagueIndex = this.m_leagueIndex
		};
	}

	// Token: 0x060036F2 RID: 14066 RVA: 0x00122FF0 File Offset: 0x001211F0
	public bool IsSentEnergy()
	{
		return this.m_energyFlg;
	}

	// Token: 0x04002E42 RID: 11842
	public string m_hspId;

	// Token: 0x04002E43 RID: 11843
	public long m_score;

	// Token: 0x04002E44 RID: 11844
	public long m_hiScore;

	// Token: 0x04002E45 RID: 11845
	public int m_userData;

	// Token: 0x04002E46 RID: 11846
	public string m_name;

	// Token: 0x04002E47 RID: 11847
	public string m_url;

	// Token: 0x04002E48 RID: 11848
	public bool m_energyFlg;

	// Token: 0x04002E49 RID: 11849
	public int m_grade;

	// Token: 0x04002E4A RID: 11850
	public int m_gradeChanged;

	// Token: 0x04002E4B RID: 11851
	public long m_expireTime;

	// Token: 0x04002E4C RID: 11852
	public int m_numRank;

	// Token: 0x04002E4D RID: 11853
	public long m_loginTime;

	// Token: 0x04002E4E RID: 11854
	public int m_charaId;

	// Token: 0x04002E4F RID: 11855
	public int m_charaLevel;

	// Token: 0x04002E50 RID: 11856
	public int m_subCharaId;

	// Token: 0x04002E51 RID: 11857
	public int m_subCharaLevel;

	// Token: 0x04002E52 RID: 11858
	public int m_mainChaoId;

	// Token: 0x04002E53 RID: 11859
	public int m_mainChaoLevel;

	// Token: 0x04002E54 RID: 11860
	public int m_subChaoId;

	// Token: 0x04002E55 RID: 11861
	public int m_subChaoLevel;

	// Token: 0x04002E56 RID: 11862
	public Env.Language m_language;

	// Token: 0x04002E57 RID: 11863
	public int m_leagueIndex;
}
