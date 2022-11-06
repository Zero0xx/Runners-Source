using System;
using System.Collections.Generic;
using Text;

// Token: 0x020004F9 RID: 1273
public class RankingServerInfoConverter
{
	// Token: 0x060025DF RID: 9695 RVA: 0x000E6378 File Offset: 0x000E4578
	public RankingServerInfoConverter(string serverMessageInfo)
	{
		this.Setup(serverMessageInfo);
	}

	// Token: 0x060025E1 RID: 9697 RVA: 0x000E638C File Offset: 0x000E458C
	public void Setup(string serverMessageInfo)
	{
		this.m_orgServerMessageInfo = serverMessageInfo;
		RankingServerInfoConverter.ms_lastServerMessageInfo = serverMessageInfo;
		string[] array = this.m_orgServerMessageInfo.Split(new char[]
		{
			','
		});
		if (array != null && array.Length > 0)
		{
			Debug.Log("orgServerMessageInfo=" + this.m_orgServerMessageInfo);
			if (array.Length > 0)
			{
				this.m_scoreRanking = int.Parse(array[0]);
			}
			if (array.Length > 1)
			{
				this.m_totalScoreRanking = int.Parse(array[1]);
			}
			else
			{
				this.m_totalScoreRanking = -1;
			}
			if (array.Length > 2)
			{
				this.m_rivalRanking = int.Parse(array[2]);
			}
			else
			{
				this.m_rivalRanking = -1;
			}
			if (array.Length > 3)
			{
				this.m_totalRivalRanking = int.Parse(array[3]);
			}
			else
			{
				this.m_totalRivalRanking = -1;
			}
			if (array.Length > 4)
			{
				this.m_sendPresent = int.Parse(array[4]);
			}
			else
			{
				this.m_sendPresent = -1;
			}
			if (array.Length > 5)
			{
				this.m_startDt = NetUtil.GetLocalDateTime(long.Parse(array[5]));
			}
			if (array.Length > 6)
			{
				this.m_endDt = NetUtil.GetLocalDateTime(long.Parse(array[6]));
			}
			if (array.Length > 7)
			{
				this.m_league = int.Parse(array[7]);
			}
			else
			{
				this.m_league = -1;
			}
			if (array.Length > 8)
			{
				this.m_oldLeague = int.Parse(array[8]);
			}
			else
			{
				this.m_oldLeague = -1;
			}
			if (array.Length > 9)
			{
				this.m_numLeagueMember = int.Parse(array[9]);
			}
			else
			{
				this.m_numLeagueMember = -1;
			}
			if (array.Length > 10)
			{
				this.m_sendPresentRival = int.Parse(array[10]);
			}
			else
			{
				this.m_sendPresentRival = -1;
			}
		}
		else
		{
			this.m_orgServerMessageInfo = null;
		}
	}

	// Token: 0x170004F4 RID: 1268
	// (get) Token: 0x060025E2 RID: 9698 RVA: 0x000E6558 File Offset: 0x000E4758
	public bool isInit
	{
		get
		{
			return this.m_orgServerMessageInfo != null;
		}
	}

	// Token: 0x170004F5 RID: 1269
	// (get) Token: 0x060025E3 RID: 9699 RVA: 0x000E6568 File Offset: 0x000E4768
	public RankingServerInfoConverter.ResultType leagueResult
	{
		get
		{
			RankingServerInfoConverter.ResultType result = RankingServerInfoConverter.ResultType.Stay;
			if (!this.isInit)
			{
				return RankingServerInfoConverter.ResultType.Error;
			}
			if (this.m_league != this.m_oldLeague)
			{
				if (this.m_league > this.m_oldLeague)
				{
					result = RankingServerInfoConverter.ResultType.Up;
				}
				else
				{
					result = RankingServerInfoConverter.ResultType.Down;
				}
			}
			return result;
		}
	}

	// Token: 0x170004F6 RID: 1270
	// (get) Token: 0x060025E4 RID: 9700 RVA: 0x000E65B0 File Offset: 0x000E47B0
	public LeagueType currentLeague
	{
		get
		{
			return (LeagueType)this.m_league;
		}
	}

	// Token: 0x170004F7 RID: 1271
	// (get) Token: 0x060025E5 RID: 9701 RVA: 0x000E65B8 File Offset: 0x000E47B8
	public LeagueType oldLeague
	{
		get
		{
			return (LeagueType)this.m_oldLeague;
		}
	}

	// Token: 0x170004F8 RID: 1272
	// (get) Token: 0x060025E6 RID: 9702 RVA: 0x000E65C0 File Offset: 0x000E47C0
	public DateTime startDt
	{
		get
		{
			return this.m_startDt;
		}
	}

	// Token: 0x170004F9 RID: 1273
	// (get) Token: 0x060025E7 RID: 9703 RVA: 0x000E65C8 File Offset: 0x000E47C8
	public DateTime endDt
	{
		get
		{
			return this.m_endDt;
		}
	}

	// Token: 0x170004FA RID: 1274
	// (get) Token: 0x060025E8 RID: 9704 RVA: 0x000E65D0 File Offset: 0x000E47D0
	public string rankingResultAllText
	{
		get
		{
			if (!this.isInit)
			{
				return null;
			}
			string text;
			if (this.m_sendPresent <= 0)
			{
				text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_result_text_4").text;
			}
			else
			{
				text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_result_text_3").text;
			}
			return TextUtility.Replaces(text, new Dictionary<string, string>
			{
				{
					"{PARAM1}",
					this.m_scoreRanking.ToString()
				},
				{
					"{PARAM3}",
					this.m_totalScoreRanking.ToString()
				}
			});
		}
	}

	// Token: 0x170004FB RID: 1275
	// (get) Token: 0x060025E9 RID: 9705 RVA: 0x000E6668 File Offset: 0x000E4868
	public string rankingResultLeagueText
	{
		get
		{
			if (!this.isInit)
			{
				return null;
			}
			string text;
			if (this.m_sendPresentRival <= 0)
			{
				text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_result_text_2").text;
			}
			else
			{
				text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_result_text_1").text;
			}
			return TextUtility.Replaces(text, new Dictionary<string, string>
			{
				{
					"{PARAM1}",
					this.m_rivalRanking.ToString()
				},
				{
					"{PARAM2}",
					this.m_numLeagueMember.ToString()
				},
				{
					"{PARAM3}",
					this.m_totalRivalRanking.ToString()
				},
				{
					"{PARAM4}",
					this.m_numLeagueMember.ToString()
				}
			});
		}
	}

	// Token: 0x060025EA RID: 9706 RVA: 0x000E672C File Offset: 0x000E492C
	public static RankingServerInfoConverter CreateLastServerInfo()
	{
		RankingServerInfoConverter result = null;
		if (!string.IsNullOrEmpty(RankingServerInfoConverter.ms_lastServerMessageInfo))
		{
			result = new RankingServerInfoConverter(RankingServerInfoConverter.ms_lastServerMessageInfo);
		}
		return result;
	}

	// Token: 0x170004FC RID: 1276
	// (get) Token: 0x060025EB RID: 9707 RVA: 0x000E6758 File Offset: 0x000E4958
	// (set) Token: 0x060025EC RID: 9708 RVA: 0x000E6760 File Offset: 0x000E4960
	public static string lastServerMessageInfo
	{
		get
		{
			return RankingServerInfoConverter.ms_lastServerMessageInfo;
		}
		set
		{
			RankingServerInfoConverter.ms_lastServerMessageInfo = value;
		}
	}

	// Token: 0x04002220 RID: 8736
	private static string ms_lastServerMessageInfo;

	// Token: 0x04002221 RID: 8737
	private string m_orgServerMessageInfo;

	// Token: 0x04002222 RID: 8738
	private int m_scoreRanking;

	// Token: 0x04002223 RID: 8739
	private int m_totalScoreRanking;

	// Token: 0x04002224 RID: 8740
	private int m_rivalRanking;

	// Token: 0x04002225 RID: 8741
	private int m_totalRivalRanking;

	// Token: 0x04002226 RID: 8742
	private int m_sendPresent;

	// Token: 0x04002227 RID: 8743
	private DateTime m_startDt;

	// Token: 0x04002228 RID: 8744
	private DateTime m_endDt;

	// Token: 0x04002229 RID: 8745
	private int m_league;

	// Token: 0x0400222A RID: 8746
	private int m_oldLeague;

	// Token: 0x0400222B RID: 8747
	private int m_numLeagueMember;

	// Token: 0x0400222C RID: 8748
	private int m_sendPresentRival;

	// Token: 0x020004FA RID: 1274
	public enum MSG_INFO
	{
		// Token: 0x0400222E RID: 8750
		ScoreRanking,
		// Token: 0x0400222F RID: 8751
		TotalScoreRanking,
		// Token: 0x04002230 RID: 8752
		RivalRanking,
		// Token: 0x04002231 RID: 8753
		TotalRivalRanking,
		// Token: 0x04002232 RID: 8754
		SendPresent,
		// Token: 0x04002233 RID: 8755
		StartDt,
		// Token: 0x04002234 RID: 8756
		EndDt,
		// Token: 0x04002235 RID: 8757
		League,
		// Token: 0x04002236 RID: 8758
		OldLeague,
		// Token: 0x04002237 RID: 8759
		NumLeagueMember,
		// Token: 0x04002238 RID: 8760
		SendPresentRival,
		// Token: 0x04002239 RID: 8761
		NUM
	}

	// Token: 0x020004FB RID: 1275
	public enum ResultType
	{
		// Token: 0x0400223B RID: 8763
		Up,
		// Token: 0x0400223C RID: 8764
		Stay,
		// Token: 0x0400223D RID: 8765
		Down,
		// Token: 0x0400223E RID: 8766
		Error,
		// Token: 0x0400223F RID: 8767
		NUM
	}
}
