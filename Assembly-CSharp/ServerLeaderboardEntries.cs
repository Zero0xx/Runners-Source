using System;
using System.Collections.Generic;

// Token: 0x02000803 RID: 2051
public class ServerLeaderboardEntries
{
	// Token: 0x060036E1 RID: 14049 RVA: 0x00122804 File Offset: 0x00120A04
	public ServerLeaderboardEntries()
	{
		this.m_leaderboardEntries = new List<ServerLeaderboardEntry>();
		this.m_mode = 0;
		this.m_first = -1;
		this.m_count = 0;
		this.m_rankingType = -1;
		this.m_index = 0;
		this.m_eventId = 0;
	}

	// Token: 0x060036E2 RID: 14050 RVA: 0x00122844 File Offset: 0x00120A44
	public void CopyTo(ServerLeaderboardEntries to)
	{
		if (this.m_myLeaderboardEntry != null)
		{
			if (to.m_myLeaderboardEntry == null)
			{
				to.m_myLeaderboardEntry = new ServerLeaderboardEntry();
			}
			this.m_myLeaderboardEntry.CopyTo(to.m_myLeaderboardEntry);
		}
		else
		{
			to.m_myLeaderboardEntry = null;
		}
		to.m_leaderboardEntries.Clear();
		foreach (ServerLeaderboardEntry serverLeaderboardEntry in this.m_leaderboardEntries)
		{
			ServerLeaderboardEntry serverLeaderboardEntry2 = new ServerLeaderboardEntry();
			serverLeaderboardEntry.CopyTo(serverLeaderboardEntry2);
			to.m_leaderboardEntries.Add(serverLeaderboardEntry2);
		}
		to.m_resultTotalEntries = this.m_resultTotalEntries;
		to.m_resetTime = this.m_resetTime;
		to.m_startTime = this.m_startTime;
		to.m_startIndex = this.m_startIndex;
		to.m_mode = this.m_mode;
		to.m_first = this.m_first;
		to.m_count = this.m_count;
		to.m_index = this.m_index;
		to.m_rankingType = this.m_rankingType;
		to.m_eventId = this.m_eventId;
		if (this.m_friendIdList != null)
		{
			to.m_friendIdList = new string[this.m_friendIdList.Length];
			this.m_friendIdList.CopyTo(to.m_friendIdList, 0);
		}
		else
		{
			to.m_friendIdList = null;
		}
	}

	// Token: 0x060036E3 RID: 14051 RVA: 0x001229BC File Offset: 0x00120BBC
	public bool CompareParam(int mode, int first, int count, int index, int rankingType, int eventId, string[] friendIdList)
	{
		if (mode != this.m_mode || first != this.m_first || count != this.m_count || rankingType != this.m_rankingType || index != this.m_index || eventId != this.m_eventId)
		{
			return false;
		}
		if (friendIdList == null && this.m_friendIdList == null)
		{
			return true;
		}
		if (friendIdList == null || this.m_friendIdList == null || friendIdList.Length != this.m_friendIdList.Length)
		{
			return false;
		}
		for (int i = 0; i < friendIdList.Length; i++)
		{
			if (friendIdList[i] != this.m_friendIdList[i])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060036E4 RID: 14052 RVA: 0x00122A80 File Offset: 0x00120C80
	public bool IsRivalHighScore()
	{
		return ServerLeaderboardEntries.IsRivalHighScore(this.m_first, this.m_rankingType);
	}

	// Token: 0x060036E5 RID: 14053 RVA: 0x00122A94 File Offset: 0x00120C94
	public bool IsRivalRanking()
	{
		return this.m_rankingType == 4 || this.m_rankingType == 5;
	}

	// Token: 0x060036E6 RID: 14054 RVA: 0x00122AB4 File Offset: 0x00120CB4
	public static bool IsRivalHighScore(int first, int rankingType)
	{
		return first == 0 && rankingType == 4;
	}

	// Token: 0x060036E7 RID: 14055 RVA: 0x00122AC8 File Offset: 0x00120CC8
	public ServerLeaderboardEntry GetRankTop()
	{
		if (this.m_leaderboardEntries != null && this.m_leaderboardEntries.Count > 0 && this.m_leaderboardEntries[0].m_grade == 1)
		{
			return this.m_leaderboardEntries[0];
		}
		return null;
	}

	// Token: 0x060036E8 RID: 14056 RVA: 0x00122B18 File Offset: 0x00120D18
	public bool IsNext()
	{
		bool result = false;
		if (this.m_leaderboardEntries != null && this.m_count <= this.m_leaderboardEntries.Count)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x060036E9 RID: 14057 RVA: 0x00122B4C File Offset: 0x00120D4C
	public bool GetNextRanking(ref int top, ref int count, int margin)
	{
		if (!this.IsNext())
		{
			return false;
		}
		top = this.m_count - margin + 1;
		if (top < 1)
		{
			count = margin + count + top;
			top = 1;
		}
		else
		{
			count = margin + count;
		}
		return true;
	}

	// Token: 0x060036EA RID: 14058 RVA: 0x00122B88 File Offset: 0x00120D88
	public bool IsPrev()
	{
		bool result = false;
		if (this.m_leaderboardEntries != null && this.m_leaderboardEntries.Count > 0)
		{
			if (this.m_first > 1)
			{
				result = true;
			}
			else if (this.m_first == 0 && this.m_leaderboardEntries[0].m_grade > 1)
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x060036EB RID: 14059 RVA: 0x00122BEC File Offset: 0x00120DEC
	public bool GetPrevRanking(ref int top, ref int count, int margin)
	{
		if (!this.IsPrev())
		{
			return false;
		}
		top = this.m_first - count;
		if (top < 1)
		{
			count += top - 1;
			top = 1;
		}
		return true;
	}

	// Token: 0x060036EC RID: 14060 RVA: 0x00122C1C File Offset: 0x00120E1C
	public bool IsReload()
	{
		bool result = false;
		DateTime localDateTime = NetUtil.GetLocalDateTime((long)this.m_resetTime);
		if (localDateTime != default(DateTime) && NetUtil.GetCurrentTime() > localDateTime)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x060036ED RID: 14061 RVA: 0x00122C60 File Offset: 0x00120E60
	public TimeSpan GetResetTimeSpan()
	{
		TimeSpan result = default(TimeSpan);
		DateTime localDateTime = NetUtil.GetLocalDateTime((long)this.m_resetTime);
		if (localDateTime != default(DateTime))
		{
			return localDateTime - NetUtil.GetCurrentTime();
		}
		return result;
	}

	// Token: 0x060036EE RID: 14062 RVA: 0x00122CA4 File Offset: 0x00120EA4
	public bool UpdateSendChallenge(string id)
	{
		bool result = false;
		if (this.m_leaderboardEntries != null && this.m_leaderboardEntries.Count > 0)
		{
			foreach (ServerLeaderboardEntry serverLeaderboardEntry in this.m_leaderboardEntries)
			{
				if (id == serverLeaderboardEntry.m_hspId)
				{
					serverLeaderboardEntry.m_energyFlg = true;
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x04002E35 RID: 11829
	public ServerLeaderboardEntry m_myLeaderboardEntry;

	// Token: 0x04002E36 RID: 11830
	public List<ServerLeaderboardEntry> m_leaderboardEntries;

	// Token: 0x04002E37 RID: 11831
	public int m_resultTotalEntries;

	// Token: 0x04002E38 RID: 11832
	public int m_resetTime;

	// Token: 0x04002E39 RID: 11833
	public int m_startTime;

	// Token: 0x04002E3A RID: 11834
	public int m_startIndex;

	// Token: 0x04002E3B RID: 11835
	public int m_mode;

	// Token: 0x04002E3C RID: 11836
	public int m_first;

	// Token: 0x04002E3D RID: 11837
	public int m_count;

	// Token: 0x04002E3E RID: 11838
	public int m_index;

	// Token: 0x04002E3F RID: 11839
	public int m_rankingType;

	// Token: 0x04002E40 RID: 11840
	public int m_eventId;

	// Token: 0x04002E41 RID: 11841
	public string[] m_friendIdList;
}
