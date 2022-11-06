using System;
using System.Collections.Generic;
using Message;

// Token: 0x020004E9 RID: 1257
public class RankingDataContainer
{
	// Token: 0x06002570 RID: 9584 RVA: 0x000E1FA8 File Offset: 0x000E01A8
	public RankingDataContainer()
	{
		this.Reset();
	}

	// Token: 0x06002571 RID: 9585 RVA: 0x000E1FB8 File Offset: 0x000E01B8
	public void Reset(RankingUtil.RankingRankerType type)
	{
		if (this.m_rankingData != null && this.m_rankingData.Count > 0 && this.m_rankingData.ContainsKey(type))
		{
			this.m_rankingData[type].Clear();
			this.m_rankingData[type] = new Dictionary<RankingUtil.RankingScoreType, List<MsgGetLeaderboardEntriesSucceed>>();
		}
	}

	// Token: 0x06002572 RID: 9586 RVA: 0x000E2014 File Offset: 0x000E0214
	public void Reset()
	{
		if (this.m_rankingData != null && this.m_rankingData.Count > 0)
		{
			for (int i = 0; i < 6; i++)
			{
				RankingUtil.RankingRankerType key = (RankingUtil.RankingRankerType)i;
				this.m_rankingData[key].Clear();
				this.m_rankingData[key] = new Dictionary<RankingUtil.RankingScoreType, List<MsgGetLeaderboardEntriesSucceed>>();
			}
		}
		else
		{
			this.m_rankingData = new Dictionary<RankingUtil.RankingRankerType, Dictionary<RankingUtil.RankingScoreType, List<MsgGetLeaderboardEntriesSucceed>>>();
			this.m_rankingUserOldRank = null;
			for (int j = 0; j < 6; j++)
			{
				RankingUtil.RankingRankerType key2 = (RankingUtil.RankingRankerType)j;
				this.m_rankingData.Add(key2, new Dictionary<RankingUtil.RankingScoreType, List<MsgGetLeaderboardEntriesSucceed>>());
			}
		}
	}

	// Token: 0x06002573 RID: 9587 RVA: 0x000E20B0 File Offset: 0x000E02B0
	public void SavePlayerRanking()
	{
		if (this.m_rankingUserOldRank == null)
		{
			this.m_rankingUserOldRank = new Dictionary<RankingUtil.RankingRankerType, Dictionary<RankingUtil.RankingScoreType, int>>();
			for (int i = 0; i < 6; i++)
			{
				RankingUtil.RankingRankerType key = (RankingUtil.RankingRankerType)i;
				this.m_rankingUserOldRank.Add(key, new Dictionary<RankingUtil.RankingScoreType, int>());
				this.m_rankingUserOldRank[key].Add(RankingUtil.RankingScoreType.HIGH_SCORE, -1);
				this.m_rankingUserOldRank[key].Add(RankingUtil.RankingScoreType.TOTAL_SCORE, -1);
			}
		}
		else
		{
			for (int j = 0; j < 6; j++)
			{
				RankingUtil.RankingRankerType key2 = (RankingUtil.RankingRankerType)j;
				this.m_rankingUserOldRank[key2][RankingUtil.RankingScoreType.HIGH_SCORE] = -1;
				this.m_rankingUserOldRank[key2][RankingUtil.RankingScoreType.TOTAL_SCORE] = -1;
			}
		}
		if (this.m_rankingData != null && this.m_rankingData.Count > 0)
		{
			for (int k = 0; k < 6; k++)
			{
				RankingUtil.RankingRankerType key3 = (RankingUtil.RankingRankerType)k;
				if (this.m_rankingData.ContainsKey(key3))
				{
					if (this.m_rankingData[key3].ContainsKey(RankingUtil.RankingScoreType.HIGH_SCORE))
					{
						List<MsgGetLeaderboardEntriesSucceed> list = this.m_rankingData[key3][RankingUtil.RankingScoreType.HIGH_SCORE];
						if (list != null && list.Count > 0 && list[0] != null && list[0].m_leaderboardEntries != null && list[0].m_leaderboardEntries.m_myLeaderboardEntry != null)
						{
							ServerLeaderboardEntry myLeaderboardEntry = list[0].m_leaderboardEntries.m_myLeaderboardEntry;
							if (this.m_rankingUserOldRank != null && this.m_rankingUserOldRank.ContainsKey(key3) && this.m_rankingUserOldRank[key3] != null && this.m_rankingUserOldRank[key3].ContainsKey(RankingUtil.RankingScoreType.HIGH_SCORE))
							{
								this.m_rankingUserOldRank[key3][RankingUtil.RankingScoreType.HIGH_SCORE] = myLeaderboardEntry.m_grade;
							}
						}
					}
					if (this.m_rankingData[key3].ContainsKey(RankingUtil.RankingScoreType.TOTAL_SCORE))
					{
						List<MsgGetLeaderboardEntriesSucceed> list2 = this.m_rankingData[key3][RankingUtil.RankingScoreType.TOTAL_SCORE];
						if (list2 != null && list2.Count > 0 && list2[0] != null && list2[0].m_leaderboardEntries != null && list2[0].m_leaderboardEntries.m_myLeaderboardEntry != null)
						{
							ServerLeaderboardEntry myLeaderboardEntry2 = list2[0].m_leaderboardEntries.m_myLeaderboardEntry;
							if (this.m_rankingUserOldRank != null && this.m_rankingUserOldRank.ContainsKey(key3) && this.m_rankingUserOldRank[key3] != null && this.m_rankingUserOldRank[key3].ContainsKey(RankingUtil.RankingScoreType.TOTAL_SCORE))
							{
								this.m_rankingUserOldRank[key3][RankingUtil.RankingScoreType.TOTAL_SCORE] = myLeaderboardEntry2.m_grade;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06002574 RID: 9588 RVA: 0x000E2380 File Offset: 0x000E0580
	public void SavePlayerRankingDummy(RankingUtil.RankingRankerType rankType, RankingUtil.RankingScoreType scoreType, int dammyRank)
	{
		if (this.m_rankingUserOldRank == null)
		{
			this.m_rankingUserOldRank = new Dictionary<RankingUtil.RankingRankerType, Dictionary<RankingUtil.RankingScoreType, int>>();
			for (int i = 0; i < 6; i++)
			{
				RankingUtil.RankingRankerType key = (RankingUtil.RankingRankerType)i;
				this.m_rankingUserOldRank.Add(key, new Dictionary<RankingUtil.RankingScoreType, int>());
				this.m_rankingUserOldRank[key].Add(RankingUtil.RankingScoreType.HIGH_SCORE, -1);
				this.m_rankingUserOldRank[key].Add(RankingUtil.RankingScoreType.TOTAL_SCORE, -1);
			}
		}
		else
		{
			for (int j = 0; j < 6; j++)
			{
				RankingUtil.RankingRankerType key2 = (RankingUtil.RankingRankerType)j;
				this.m_rankingUserOldRank[key2][RankingUtil.RankingScoreType.HIGH_SCORE] = -1;
				this.m_rankingUserOldRank[key2][RankingUtil.RankingScoreType.TOTAL_SCORE] = -1;
			}
		}
		if (this.m_rankingData != null && this.m_rankingData.Count > 0)
		{
			for (int k = 0; k < 6; k++)
			{
				RankingUtil.RankingRankerType rankingRankerType = (RankingUtil.RankingRankerType)k;
				if (this.m_rankingData.ContainsKey(rankingRankerType))
				{
					if (this.m_rankingData[rankingRankerType].ContainsKey(RankingUtil.RankingScoreType.HIGH_SCORE))
					{
						List<MsgGetLeaderboardEntriesSucceed> list = this.m_rankingData[rankingRankerType][RankingUtil.RankingScoreType.HIGH_SCORE];
						if (list != null && list.Count > 0 && list[0] != null && list[0].m_leaderboardEntries != null && list[0].m_leaderboardEntries.m_myLeaderboardEntry != null)
						{
							ServerLeaderboardEntry myLeaderboardEntry = list[0].m_leaderboardEntries.m_myLeaderboardEntry;
							if (this.m_rankingUserOldRank != null && this.m_rankingUserOldRank.ContainsKey(rankingRankerType) && this.m_rankingUserOldRank[rankingRankerType] != null && this.m_rankingUserOldRank[rankingRankerType].ContainsKey(RankingUtil.RankingScoreType.HIGH_SCORE))
							{
								if (rankingRankerType == rankType && scoreType == RankingUtil.RankingScoreType.HIGH_SCORE)
								{
									this.m_rankingUserOldRank[rankingRankerType][RankingUtil.RankingScoreType.HIGH_SCORE] = dammyRank;
								}
								else
								{
									this.m_rankingUserOldRank[rankingRankerType][RankingUtil.RankingScoreType.HIGH_SCORE] = myLeaderboardEntry.m_grade;
								}
							}
						}
					}
					if (this.m_rankingData[rankingRankerType].ContainsKey(RankingUtil.RankingScoreType.TOTAL_SCORE))
					{
						List<MsgGetLeaderboardEntriesSucceed> list2 = this.m_rankingData[rankingRankerType][RankingUtil.RankingScoreType.TOTAL_SCORE];
						if (list2 != null && list2.Count > 0 && list2[0] != null && list2[0].m_leaderboardEntries != null && list2[0].m_leaderboardEntries.m_myLeaderboardEntry != null)
						{
							ServerLeaderboardEntry myLeaderboardEntry2 = list2[0].m_leaderboardEntries.m_myLeaderboardEntry;
							if (this.m_rankingUserOldRank != null && this.m_rankingUserOldRank.ContainsKey(rankingRankerType) && this.m_rankingUserOldRank[rankingRankerType] != null && this.m_rankingUserOldRank[rankingRankerType].ContainsKey(RankingUtil.RankingScoreType.TOTAL_SCORE))
							{
								if (rankingRankerType == rankType && scoreType == RankingUtil.RankingScoreType.TOTAL_SCORE)
								{
									this.m_rankingUserOldRank[rankingRankerType][RankingUtil.RankingScoreType.TOTAL_SCORE] = dammyRank;
								}
								else
								{
									this.m_rankingUserOldRank[rankingRankerType][RankingUtil.RankingScoreType.TOTAL_SCORE] = myLeaderboardEntry2.m_grade;
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06002575 RID: 9589 RVA: 0x000E269C File Offset: 0x000E089C
	public bool UpdateSendChallengeList(RankingUtil.RankingRankerType type, string id)
	{
		bool result = false;
		if (this.m_rankingData != null && this.m_rankingData.ContainsKey(type) && this.m_rankingData[type] != null && this.m_rankingData[type].Count > 0)
		{
			if (this.m_rankingData[type].ContainsKey(RankingUtil.RankingScoreType.HIGH_SCORE) && this.m_rankingData[type][RankingUtil.RankingScoreType.HIGH_SCORE].Count > 0)
			{
				int count = this.m_rankingData[type][RankingUtil.RankingScoreType.HIGH_SCORE].Count;
				if (this.m_rankingData[type][RankingUtil.RankingScoreType.HIGH_SCORE][0] != null && this.m_rankingData[type][RankingUtil.RankingScoreType.HIGH_SCORE][0].m_leaderboardEntries != null)
				{
					result = this.m_rankingData[type][RankingUtil.RankingScoreType.HIGH_SCORE][0].m_leaderboardEntries.UpdateSendChallenge(id);
				}
				if (type != RankingUtil.RankingRankerType.RIVAL)
				{
					if (count > 1 && this.m_rankingData[type][RankingUtil.RankingScoreType.HIGH_SCORE][1] != null && this.m_rankingData[type][RankingUtil.RankingScoreType.HIGH_SCORE][1].m_leaderboardEntries != null)
					{
						bool flag = this.m_rankingData[type][RankingUtil.RankingScoreType.HIGH_SCORE][1].m_leaderboardEntries.UpdateSendChallenge(id);
						if (flag)
						{
							result = flag;
						}
					}
					if (count > 2 && this.m_rankingData[type][RankingUtil.RankingScoreType.HIGH_SCORE][2] != null && this.m_rankingData[type][RankingUtil.RankingScoreType.HIGH_SCORE][2].m_leaderboardEntries != null)
					{
						bool flag2 = this.m_rankingData[type][RankingUtil.RankingScoreType.HIGH_SCORE][2].m_leaderboardEntries.UpdateSendChallenge(id);
						if (flag2)
						{
							result = flag2;
						}
					}
				}
			}
			if (this.m_rankingData[type].ContainsKey(RankingUtil.RankingScoreType.TOTAL_SCORE) && this.m_rankingData[type][RankingUtil.RankingScoreType.TOTAL_SCORE].Count > 0)
			{
				int count2 = this.m_rankingData[type][RankingUtil.RankingScoreType.TOTAL_SCORE].Count;
				if (this.m_rankingData[type][RankingUtil.RankingScoreType.TOTAL_SCORE][0] != null && this.m_rankingData[type][RankingUtil.RankingScoreType.TOTAL_SCORE][0].m_leaderboardEntries != null)
				{
					bool flag3 = this.m_rankingData[type][RankingUtil.RankingScoreType.TOTAL_SCORE][0].m_leaderboardEntries.UpdateSendChallenge(id);
					if (flag3)
					{
						result = flag3;
					}
				}
				if (type != RankingUtil.RankingRankerType.RIVAL)
				{
					if (count2 > 1 && this.m_rankingData[type][RankingUtil.RankingScoreType.TOTAL_SCORE][1] != null && this.m_rankingData[type][RankingUtil.RankingScoreType.TOTAL_SCORE][1].m_leaderboardEntries != null)
					{
						bool flag4 = this.m_rankingData[type][RankingUtil.RankingScoreType.TOTAL_SCORE][1].m_leaderboardEntries.UpdateSendChallenge(id);
						if (flag4)
						{
							result = flag4;
						}
					}
					if (count2 > 2 && this.m_rankingData[type][RankingUtil.RankingScoreType.TOTAL_SCORE][2] != null && this.m_rankingData[type][RankingUtil.RankingScoreType.TOTAL_SCORE][2].m_leaderboardEntries != null)
					{
						bool flag5 = this.m_rankingData[type][RankingUtil.RankingScoreType.TOTAL_SCORE][2].m_leaderboardEntries.UpdateSendChallenge(id);
						if (flag5)
						{
							result = flag5;
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06002576 RID: 9590 RVA: 0x000E2A38 File Offset: 0x000E0C38
	public RankingUtil.RankChange GetRankChange(RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType)
	{
		RankingUtil.RankChange result = RankingUtil.RankChange.NONE;
		if (this.m_rankingUserOldRank != null && this.m_rankingUserOldRank.ContainsKey(rankerType) && this.m_rankingData != null && this.m_rankingData.ContainsKey(rankerType) && this.m_rankingUserOldRank[rankerType].ContainsKey(scoreType) && this.m_rankingData[rankerType].ContainsKey(scoreType) && this.m_rankingData[rankerType][scoreType].Count > 0)
		{
			int num = this.m_rankingUserOldRank[rankerType][scoreType];
			if (num >= 0)
			{
				MsgGetLeaderboardEntriesSucceed msgGetLeaderboardEntriesSucceed = this.m_rankingData[rankerType][scoreType][0];
				if (msgGetLeaderboardEntriesSucceed != null && msgGetLeaderboardEntriesSucceed.m_leaderboardEntries != null && msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_myLeaderboardEntry != null)
				{
					if (msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_myLeaderboardEntry.m_grade == num)
					{
						result = RankingUtil.RankChange.STAY;
					}
					else if (msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_myLeaderboardEntry.m_grade < num)
					{
						result = RankingUtil.RankChange.UP;
					}
					else if (msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_myLeaderboardEntry.m_grade > num)
					{
						result = RankingUtil.RankChange.DOWN;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06002577 RID: 9591 RVA: 0x000E2B74 File Offset: 0x000E0D74
	public RankingUtil.RankChange GetRankChange(RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType, out int currentRank, out int oldRank)
	{
		RankingUtil.RankChange result = RankingUtil.RankChange.NONE;
		currentRank = -1;
		oldRank = -1;
		if (this.m_rankingData != null && this.m_rankingData.ContainsKey(rankerType) && this.m_rankingData[rankerType].ContainsKey(scoreType) && this.m_rankingData[rankerType][scoreType].Count > 0)
		{
			MsgGetLeaderboardEntriesSucceed msgGetLeaderboardEntriesSucceed = this.m_rankingData[rankerType][scoreType][0];
			if (this.m_rankingUserOldRank != null && this.m_rankingUserOldRank.ContainsKey(rankerType) && this.m_rankingUserOldRank[rankerType].ContainsKey(scoreType))
			{
				oldRank = this.m_rankingUserOldRank[rankerType][scoreType];
				if (oldRank >= 0)
				{
					if (msgGetLeaderboardEntriesSucceed != null && msgGetLeaderboardEntriesSucceed.m_leaderboardEntries != null && msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_myLeaderboardEntry != null)
					{
						currentRank = msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_myLeaderboardEntry.m_grade;
						if (msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_myLeaderboardEntry.m_grade == oldRank)
						{
							result = RankingUtil.RankChange.STAY;
						}
						else if (msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_myLeaderboardEntry.m_grade < oldRank)
						{
							result = RankingUtil.RankChange.UP;
						}
						else if (msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_myLeaderboardEntry.m_grade > oldRank)
						{
							result = RankingUtil.RankChange.DOWN;
						}
					}
				}
				else
				{
					currentRank = msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_myLeaderboardEntry.m_grade;
					oldRank = currentRank;
				}
			}
			else
			{
				currentRank = msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_myLeaderboardEntry.m_grade;
				oldRank = currentRank;
			}
		}
		return result;
	}

	// Token: 0x06002578 RID: 9592 RVA: 0x000E2D08 File Offset: 0x000E0F08
	public void ResetRankChange(RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType)
	{
		if (this.m_rankingUserOldRank != null && this.m_rankingUserOldRank.ContainsKey(rankerType) && this.m_rankingUserOldRank[rankerType].ContainsKey(scoreType))
		{
			this.m_rankingUserOldRank[rankerType][scoreType] = -1;
		}
	}

	// Token: 0x06002579 RID: 9593 RVA: 0x000E2D5C File Offset: 0x000E0F5C
	public bool IsRankerType(RankingUtil.RankingRankerType rankerType)
	{
		bool result = false;
		if (rankerType != RankingUtil.RankingRankerType.COUNT && this.m_rankingData != null && this.m_rankingData.ContainsKey(rankerType))
		{
			result = true;
		}
		return result;
	}

	// Token: 0x0600257A RID: 9594 RVA: 0x000E2D94 File Offset: 0x000E0F94
	public bool IsRankerType(RankingUtil.RankingRankerType rankerType, out Dictionary<RankingUtil.RankingScoreType, List<MsgGetLeaderboardEntriesSucceed>> data)
	{
		bool result = false;
		data = null;
		if (this.IsRankerType(rankerType))
		{
			data = this.m_rankingData[rankerType];
			result = true;
		}
		return result;
	}

	// Token: 0x0600257B RID: 9595 RVA: 0x000E2DC4 File Offset: 0x000E0FC4
	public bool IsRankerListNext(RankingUtil.RankingRankerType rankerType, RankingUtil.RankingScoreType scoreType)
	{
		bool result = false;
		List<MsgGetLeaderboardEntriesSucceed> list;
		if (this.IsRankerAndScoreType(rankerType, scoreType, out list, 1) && list != null && list.Count > 1)
		{
			MsgGetLeaderboardEntriesSucceed msgGetLeaderboardEntriesSucceed = list[1];
			if (msgGetLeaderboardEntriesSucceed != null && msgGetLeaderboardEntriesSucceed.m_leaderboardEntries != null)
			{
				result = msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.IsNext();
			}
		}
		return result;
	}

	// Token: 0x0600257C RID: 9596 RVA: 0x000E2E1C File Offset: 0x000E101C
	public bool IsRankerListPrev(RankingUtil.RankingRankerType rankerType, RankingUtil.RankingScoreType scoreType)
	{
		bool result = false;
		List<MsgGetLeaderboardEntriesSucceed> list;
		if (this.IsRankerAndScoreType(rankerType, scoreType, out list, 1) && list != null && list.Count > 1)
		{
			MsgGetLeaderboardEntriesSucceed msgGetLeaderboardEntriesSucceed = list[1];
			if (msgGetLeaderboardEntriesSucceed != null && msgGetLeaderboardEntriesSucceed.m_leaderboardEntries != null)
			{
				result = msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.IsPrev();
			}
		}
		return result;
	}

	// Token: 0x0600257D RID: 9597 RVA: 0x000E2E74 File Offset: 0x000E1074
	public bool IsRankerAndScoreType(RankingUtil.RankingRankerType rankerType, RankingUtil.RankingScoreType scoreType, int page = -1)
	{
		bool result = false;
		if (rankerType != RankingUtil.RankingRankerType.COUNT && this.m_rankingData != null && this.m_rankingData.ContainsKey(rankerType) && this.m_rankingData[rankerType] != null && this.m_rankingData[rankerType].Count > 0 && this.m_rankingData[rankerType].ContainsKey(scoreType))
		{
			if (page < 0 || rankerType == RankingUtil.RankingRankerType.RIVAL)
			{
				if (this.m_rankingData[rankerType][scoreType].Count > 0)
				{
					result = true;
				}
			}
			else if (this.m_rankingData[rankerType][scoreType].Count >= page + 1)
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x0600257E RID: 9598 RVA: 0x000E2F3C File Offset: 0x000E113C
	public bool IsRankerAndScoreType(RankingUtil.RankingRankerType rankerType, RankingUtil.RankingScoreType scoreType, out List<MsgGetLeaderboardEntriesSucceed> data, int page = -1)
	{
		bool result = false;
		data = null;
		if (this.IsRankerAndScoreType(rankerType, scoreType, page))
		{
			data = this.m_rankingData[rankerType][scoreType];
			result = true;
		}
		return result;
	}

	// Token: 0x0600257F RID: 9599 RVA: 0x000E2F74 File Offset: 0x000E1174
	public List<RankingUtil.Ranker> GetRankerList(RankingUtil.RankingRankerType rankerType, RankingUtil.RankingScoreType scoreType, int page)
	{
		List<RankingUtil.Ranker> result = null;
		List<MsgGetLeaderboardEntriesSucceed> list;
		if (this.IsRankerAndScoreType(rankerType, scoreType, out list, -1) && list != null && list.Count > 0 && list.Count > page)
		{
			result = RankingUtil.GetRankerList(list[page]);
		}
		return result;
	}

	// Token: 0x06002580 RID: 9600 RVA: 0x000E2FC0 File Offset: 0x000E11C0
	public bool IsRankerListReload(RankingUtil.RankingRankerType rankerType, RankingUtil.RankingScoreType scoreType)
	{
		List<MsgGetLeaderboardEntriesSucceed> list;
		bool result;
		if (this.IsRankerAndScoreType(rankerType, scoreType, out list, 0))
		{
			if (list != null & list.Count > 0)
			{
				if (list[0] != null)
				{
					ServerLeaderboardEntries leaderboardEntries = list[0].m_leaderboardEntries;
					result = leaderboardEntries.IsReload();
				}
				else
				{
					result = true;
				}
			}
			else
			{
				result = true;
			}
		}
		else
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06002581 RID: 9601 RVA: 0x000E302C File Offset: 0x000E122C
	public TimeSpan GetResetTimeSpan(RankingUtil.RankingRankerType rankerType, RankingUtil.RankingScoreType scoreType)
	{
		MsgGetLeaderboardEntriesSucceed rankerListOrg = this.GetRankerListOrg(rankerType, scoreType, 0);
		TimeSpan result;
		if (rankerListOrg != null)
		{
			result = rankerListOrg.m_leaderboardEntries.GetResetTimeSpan();
		}
		else
		{
			result = NetUtil.GetCurrentTime() - NetUtil.GetCurrentTime();
		}
		return result;
	}

	// Token: 0x06002582 RID: 9602 RVA: 0x000E306C File Offset: 0x000E126C
	public MsgGetLeaderboardEntriesSucceed GetRankerListOrg(RankingUtil.RankingRankerType rankerType, RankingUtil.RankingScoreType scoreType, int page = 0)
	{
		MsgGetLeaderboardEntriesSucceed result = null;
		if (rankerType == RankingUtil.RankingRankerType.RIVAL)
		{
			page = 0;
		}
		List<MsgGetLeaderboardEntriesSucceed> list;
		if (this.IsRankerAndScoreType(rankerType, scoreType, out list, page) && list != null)
		{
			if (page > 1)
			{
				page = 1;
			}
			if (list.Count > page)
			{
				result = list[page];
			}
		}
		return result;
	}

	// Token: 0x06002583 RID: 9603 RVA: 0x000E30BC File Offset: 0x000E12BC
	public void AddRankerList(MsgGetLeaderboardEntriesSucceed msg)
	{
		ServerLeaderboardEntries leaderboardEntries = msg.m_leaderboardEntries;
		int rankerPage = this.GetRankerPage(msg);
		int rankingType = leaderboardEntries.m_rankingType;
		int num = rankingType % 2;
		RankingUtil.RankingRankerType rankingRankerType = (RankingUtil.RankingRankerType)(rankingType / 2);
		RankingUtil.RankingScoreType key = (RankingUtil.RankingScoreType)num;
		Dictionary<RankingUtil.RankingScoreType, List<MsgGetLeaderboardEntriesSucceed>> dictionary;
		this.IsRankerType(rankingRankerType, out dictionary);
		if (dictionary != null)
		{
			if (!dictionary.ContainsKey(key))
			{
				dictionary.Add(key, new List<MsgGetLeaderboardEntriesSucceed>());
			}
			if (rankerPage == 0)
			{
				if (dictionary[key].Count == 0)
				{
					dictionary[key].Add(RankingUtil.CopyRankingMsg(msg, null));
				}
				else
				{
					dictionary[key][0] = RankingUtil.CopyRankingMsg(msg, null);
				}
				if (rankingRankerType != RankingUtil.RankingRankerType.RIVAL)
				{
					if (dictionary[key].Count == 1)
					{
						dictionary[key].Add(RankingUtil.CopyRankingMsg(msg, null));
					}
					else
					{
						dictionary[key][1] = RankingUtil.CopyRankingMsg(msg, null);
					}
				}
			}
			else if (rankerPage == 1)
			{
				if (dictionary[key].Count == 0)
				{
					dictionary[key].Add(RankingUtil.InitRankingMsg(msg));
					dictionary[key].Add(RankingUtil.CopyRankingMsg(msg, null));
				}
				else if (dictionary[key].Count == 1)
				{
					dictionary[key][0].m_leaderboardEntries.m_resetTime = msg.m_leaderboardEntries.m_resetTime;
					dictionary[key][0].m_leaderboardEntries.m_startTime = msg.m_leaderboardEntries.m_startTime;
					if (rankingRankerType != RankingUtil.RankingRankerType.RIVAL && msg.m_leaderboardEntries.m_myLeaderboardEntry != null && msg.m_leaderboardEntries.m_myLeaderboardEntry.m_score > 0L && msg.m_leaderboardEntries.m_myLeaderboardEntry.m_grade > 0)
					{
						msg.m_leaderboardEntries.m_myLeaderboardEntry.CopyTo(dictionary[key][0].m_leaderboardEntries.m_myLeaderboardEntry);
					}
					dictionary[key].Add(RankingUtil.CopyRankingMsg(msg, null));
				}
				else
				{
					dictionary[key][0].m_leaderboardEntries.m_resetTime = msg.m_leaderboardEntries.m_resetTime;
					dictionary[key][0].m_leaderboardEntries.m_startTime = msg.m_leaderboardEntries.m_startTime;
					if (rankingRankerType != RankingUtil.RankingRankerType.RIVAL && msg.m_leaderboardEntries.m_myLeaderboardEntry != null && msg.m_leaderboardEntries.m_myLeaderboardEntry.m_score > 0L && msg.m_leaderboardEntries.m_myLeaderboardEntry.m_grade > 0)
					{
						msg.m_leaderboardEntries.m_myLeaderboardEntry.CopyTo(dictionary[key][0].m_leaderboardEntries.m_myLeaderboardEntry);
					}
					dictionary[key][1] = RankingUtil.CopyRankingMsg(msg, null);
				}
			}
			else if (rankerPage == 2)
			{
				if (dictionary[key].Count == 0)
				{
					dictionary[key].Add(null);
					dictionary[key].Add(RankingUtil.CopyRankingMsg(msg, null));
				}
				else if (dictionary[key].Count == 1)
				{
					dictionary[key][0].m_leaderboardEntries.m_resetTime = msg.m_leaderboardEntries.m_resetTime;
					dictionary[key][0].m_leaderboardEntries.m_startTime = msg.m_leaderboardEntries.m_startTime;
					if (rankingRankerType != RankingUtil.RankingRankerType.RIVAL && msg.m_leaderboardEntries.m_myLeaderboardEntry != null && msg.m_leaderboardEntries.m_myLeaderboardEntry.m_score > 0L && msg.m_leaderboardEntries.m_myLeaderboardEntry.m_grade > 0)
					{
						msg.m_leaderboardEntries.m_myLeaderboardEntry.CopyTo(dictionary[key][0].m_leaderboardEntries.m_myLeaderboardEntry);
					}
					dictionary[key].Add(RankingUtil.CopyRankingMsg(msg, null));
				}
				else
				{
					dictionary[key][0].m_leaderboardEntries.m_resetTime = msg.m_leaderboardEntries.m_resetTime;
					dictionary[key][0].m_leaderboardEntries.m_startTime = msg.m_leaderboardEntries.m_startTime;
					if (dictionary[key][1] != null)
					{
						dictionary[key][1] = RankingUtil.CopyRankingMsg(msg, dictionary[key][1]);
					}
					else
					{
						dictionary[key][1] = RankingUtil.CopyRankingMsg(msg, null);
					}
					if (rankingRankerType != RankingUtil.RankingRankerType.RIVAL && msg.m_leaderboardEntries.m_myLeaderboardEntry != null && msg.m_leaderboardEntries.m_myLeaderboardEntry.m_score > 0L && msg.m_leaderboardEntries.m_myLeaderboardEntry.m_grade > 0)
					{
						msg.m_leaderboardEntries.m_myLeaderboardEntry.CopyTo(dictionary[key][0].m_leaderboardEntries.m_myLeaderboardEntry);
					}
					msg = dictionary[key][1];
				}
			}
			else if (dictionary[key].Count == 0)
			{
				dictionary[key].Add(null);
				dictionary[key].Add(null);
				dictionary[key].Add(RankingUtil.CopyRankingMsg(msg, null));
			}
			else if (dictionary[key].Count == 1)
			{
				dictionary[key][0].m_leaderboardEntries.m_resetTime = msg.m_leaderboardEntries.m_resetTime;
				dictionary[key][0].m_leaderboardEntries.m_startTime = msg.m_leaderboardEntries.m_startTime;
				dictionary[key].Add(null);
				dictionary[key].Add(RankingUtil.CopyRankingMsg(msg, null));
				if (rankingRankerType != RankingUtil.RankingRankerType.RIVAL && msg.m_leaderboardEntries.m_myLeaderboardEntry != null && msg.m_leaderboardEntries.m_myLeaderboardEntry.m_score > 0L && msg.m_leaderboardEntries.m_myLeaderboardEntry.m_grade > 0)
				{
					msg.m_leaderboardEntries.m_myLeaderboardEntry.CopyTo(dictionary[key][0].m_leaderboardEntries.m_myLeaderboardEntry);
				}
			}
			else if (dictionary[key].Count == 2)
			{
				dictionary[key][0].m_leaderboardEntries.m_resetTime = msg.m_leaderboardEntries.m_resetTime;
				dictionary[key][0].m_leaderboardEntries.m_startTime = msg.m_leaderboardEntries.m_startTime;
				dictionary[key].Add(RankingUtil.CopyRankingMsg(msg, null));
				if (rankingRankerType != RankingUtil.RankingRankerType.RIVAL && msg.m_leaderboardEntries.m_myLeaderboardEntry != null && msg.m_leaderboardEntries.m_myLeaderboardEntry.m_score > 0L && msg.m_leaderboardEntries.m_myLeaderboardEntry.m_grade > 0)
				{
					msg.m_leaderboardEntries.m_myLeaderboardEntry.CopyTo(dictionary[key][0].m_leaderboardEntries.m_myLeaderboardEntry);
				}
			}
			else
			{
				dictionary[key][0].m_leaderboardEntries.m_resetTime = msg.m_leaderboardEntries.m_resetTime;
				dictionary[key][0].m_leaderboardEntries.m_startTime = msg.m_leaderboardEntries.m_startTime;
				dictionary[key][2] = RankingUtil.CopyRankingMsg(msg, null);
				if (rankingRankerType != RankingUtil.RankingRankerType.RIVAL && msg.m_leaderboardEntries.m_myLeaderboardEntry != null && msg.m_leaderboardEntries.m_myLeaderboardEntry.m_score > 0L && msg.m_leaderboardEntries.m_myLeaderboardEntry.m_grade > 0)
				{
					msg.m_leaderboardEntries.m_myLeaderboardEntry.CopyTo(dictionary[key][0].m_leaderboardEntries.m_myLeaderboardEntry);
				}
			}
		}
	}

	// Token: 0x06002584 RID: 9604 RVA: 0x000E38E4 File Offset: 0x000E1AE4
	private int GetRankerPage(MsgGetLeaderboardEntriesSucceed msg)
	{
		int result = 0;
		if (msg != null && msg.m_leaderboardEntries != null)
		{
			result = msg.m_leaderboardEntries.m_index;
			if (msg.m_leaderboardEntries.IsRivalRanking())
			{
				result = 0;
			}
		}
		return result;
	}

	// Token: 0x06002585 RID: 9605 RVA: 0x000E3924 File Offset: 0x000E1B24
	public int GetCurrentHighScoreRank(bool isEvent, ref long currentScore, out bool isHighScore, out long nextRankScore, out long prveRankScore, out int nextRank)
	{
		nextRankScore = -1L;
		prveRankScore = -1L;
		isHighScore = false;
		nextRank = 0;
		bool flag = false;
		RankingUtil.RankingScoreType scoreType;
		RankingUtil.RankingRankerType rankingRankerType;
		List<RankingUtil.Ranker> rankerList;
		if (isEvent)
		{
			scoreType = RankingManager.EndlessSpecialRankingScoreType;
			rankingRankerType = RankingUtil.RankingRankerType.SP_ALL;
			if (rankingRankerType == RankingUtil.RankingRankerType.RIVAL)
			{
				rankerList = this.GetRankerList(rankingRankerType, scoreType, 0);
			}
			else
			{
				rankerList = this.GetRankerList(rankingRankerType, scoreType, 2);
				if (rankerList == null)
				{
					rankerList = this.GetRankerList(rankingRankerType, scoreType, 1);
				}
				else
				{
					flag = true;
				}
			}
			if (rankerList == null || rankerList.Count <= 0)
			{
				isHighScore = true;
				return 0;
			}
			if (rankerList[0] == null)
			{
				Debug.Log("RankingManager GetCurrentHighScoreRank  first try!");
				isHighScore = true;
			}
			else if (rankerList[0].score <= 0L)
			{
				Debug.Log("RankingManager GetCurrentHighScoreRank  first try!");
				isHighScore = true;
			}
			if (isHighScore)
			{
				return 0;
			}
		}
		else
		{
			scoreType = RankingManager.EndlessRivalRankingScoreType;
			rankingRankerType = RankingUtil.RankingRankerType.RIVAL;
			if (rankingRankerType == RankingUtil.RankingRankerType.RIVAL)
			{
				rankerList = this.GetRankerList(rankingRankerType, scoreType, 0);
			}
			else
			{
				rankerList = this.GetRankerList(rankingRankerType, scoreType, 2);
				if (rankerList == null)
				{
					rankerList = this.GetRankerList(rankingRankerType, scoreType, 1);
				}
				else
				{
					flag = true;
				}
			}
			if (rankerList != null && rankerList.Count > 0)
			{
				if (rankerList[0] == null)
				{
					Debug.Log("RankingManager GetCurrentHighScoreRank  first try!");
					isHighScore = true;
				}
				else if (rankerList[0].score <= 0L)
				{
					Debug.Log("RankingManager GetCurrentHighScoreRank  first try!");
					isHighScore = true;
				}
			}
		}
		RankingUtil.Ranker ranker = rankerList[0];
		if (rankerList != null)
		{
			if (ranker != null)
			{
				if (currentScore > ranker.hiScore)
				{
					isHighScore = true;
				}
			}
			else
			{
				isHighScore = true;
			}
		}
		int num = this.CheckRankingList(rankingRankerType, scoreType, rankerList, ref currentScore, ref isHighScore, out nextRankScore, out prveRankScore);
		if (num > 1 && nextRankScore == -1L && flag)
		{
			isHighScore = true;
			rankerList = this.GetRankerList(rankingRankerType, scoreType, 1);
			if (rankerList != null && rankerList.Count > 1)
			{
				if (rankerList[rankerList.Count - 1].score > currentScore)
				{
					this.CheckRankingList(rankingRankerType, scoreType, rankerList, ref currentScore, ref isHighScore, out nextRankScore, out prveRankScore);
					nextRank = rankerList[rankerList.Count - 1].rankIndex + 1;
				}
				else
				{
					num = this.CheckRankingList(rankingRankerType, scoreType, rankerList, ref currentScore, ref isHighScore, out nextRankScore, out prveRankScore);
					nextRank = num - 1;
				}
			}
			else
			{
				nextRank = num - 1;
			}
		}
		else
		{
			nextRank = num - 1;
		}
		return num;
	}

	// Token: 0x06002586 RID: 9606 RVA: 0x000E3B80 File Offset: 0x000E1D80
	private int CheckRankingList(RankingUtil.RankingRankerType rankerType, RankingUtil.RankingScoreType scoreType, List<RankingUtil.Ranker> list, ref long currentScore, ref bool isHighScore, out long nextRankScore, out long prveRankScore)
	{
		nextRankScore = -1L;
		prveRankScore = -1L;
		RankingUtil.Ranker ranker = null;
		int num2;
		if (list != null && list.Count > 1)
		{
			ranker = list[0];
			if (ranker != null && list[1] != null)
			{
				int num = ranker.rankIndex + 1;
				if (scoreType == RankingUtil.RankingScoreType.TOTAL_SCORE)
				{
					currentScore += ranker.score;
				}
				if (list.Count == 2 && ranker.id == list[1].id)
				{
					num2 = num;
					nextRankScore = 0L;
					prveRankScore = 0L;
				}
				else
				{
					num2 = num;
					if (isHighScore || scoreType == RankingUtil.RankingScoreType.TOTAL_SCORE)
					{
						long num3 = 0L;
						bool flag = false;
						for (int i = 1; i < list.Count; i++)
						{
							RankingUtil.Ranker ranker2 = list[i];
							if (ranker2 != null)
							{
								if (ranker2.id == list[0].id)
								{
									num2 = num;
									if (num2 <= 1)
									{
										nextRankScore = 0L;
									}
									else
									{
										nextRankScore = list[i - 1].score - currentScore + 1L;
										if (nextRankScore < 0L)
										{
											nextRankScore = -1L;
										}
									}
									if (list.Count > i + 1)
									{
										prveRankScore = currentScore - list[i + 1].score;
										if (prveRankScore < 0L)
										{
											prveRankScore = 0L;
										}
									}
									else
									{
										prveRankScore = 0L;
									}
									break;
								}
								long score = ranker2.score;
								if (score < currentScore)
								{
									num2 = ranker2.rankIndex + 1;
									if (num2 <= 1)
									{
										nextRankScore = 0L;
									}
									else
									{
										nextRankScore = list[i - 1].score - currentScore + 1L;
										if (nextRankScore < 0L)
										{
											nextRankScore = -1L;
										}
									}
									if (num2 >= list.Count - 2)
									{
										prveRankScore = currentScore - list[i].score;
										if (prveRankScore < 0L)
										{
											prveRankScore = 0L;
										}
									}
									else
									{
										prveRankScore = currentScore - list[i + 1].score;
										if (num3 > 0L && num3 == currentScore)
										{
											prveRankScore = currentScore - list[i].score;
											if (prveRankScore < 0L)
											{
												prveRankScore = 0L;
											}
										}
									}
									flag = true;
									break;
								}
								if (num < ranker2.rankIndex + 1)
								{
									num2 = ranker2.rankIndex + 2;
								}
								else
								{
									num2 = ranker2.rankIndex + 1;
								}
								if (num2 <= 1)
								{
									nextRankScore = 0L;
								}
								else
								{
									nextRankScore = list[i].score - currentScore + 1L;
									if (nextRankScore < 0L)
									{
										nextRankScore = -1L;
									}
								}
								if (num2 >= list.Count - 2)
								{
									prveRankScore = 0L;
								}
								else
								{
									prveRankScore = currentScore - list[i + 1].score;
								}
								num3 = score;
							}
						}
						if (flag)
						{
							Debug.Log("RankingManager GetCurrentHighScoreRank : rank Out of range");
						}
					}
				}
			}
			else if (list.Count == 1)
			{
				num2 = 1;
				isHighScore = true;
				nextRankScore = 0L;
				prveRankScore = 0L;
			}
			else if (list.Count > 1 && ranker == null)
			{
				isHighScore = true;
				num2 = 1;
				long num4 = 0L;
				for (int j = 1; j < list.Count; j++)
				{
					RankingUtil.Ranker ranker3 = list[j];
					if (ranker3 != null)
					{
						long score2 = ranker3.score;
						if (score2 < currentScore)
						{
							num2 = ranker3.rankIndex + 1;
							if (num2 <= 1)
							{
								nextRankScore = 0L;
							}
							else
							{
								nextRankScore = list[j - 1].score - currentScore + 1L;
								if (nextRankScore < 0L)
								{
									nextRankScore = -1L;
								}
							}
							if (num2 >= list.Count - 2)
							{
								prveRankScore = currentScore - list[j].score;
								if (prveRankScore < 0L)
								{
									prveRankScore = 0L;
								}
							}
							else
							{
								prveRankScore = currentScore - list[j + 1].score;
								if (num4 > 0L && num4 == currentScore)
								{
									prveRankScore = currentScore - list[j].score;
									if (prveRankScore < 0L)
									{
										prveRankScore = 0L;
									}
								}
							}
							break;
						}
						num2 = ranker3.rankIndex + 2;
						if (list.Count > j)
						{
							nextRankScore = list[j].score - currentScore + 1L;
							if (nextRankScore < 0L)
							{
								nextRankScore = -1L;
							}
						}
						if (num2 >= list.Count - 2)
						{
							prveRankScore = 0L;
						}
						else
						{
							prveRankScore = currentScore - list[j + 1].score;
						}
						num4 = score2;
					}
				}
			}
			else
			{
				num2 = 1;
				isHighScore = true;
				nextRankScore = 0L;
				prveRankScore = 0L;
			}
		}
		else
		{
			num2 = 1;
			isHighScore = true;
			nextRankScore = 0L;
			prveRankScore = 0L;
		}
		if (scoreType == RankingUtil.RankingScoreType.TOTAL_SCORE && ranker != null && num2 > 1 && ranker.rankIndex + 1 < num2)
		{
			num2 = ranker.rankIndex + 1;
		}
		return num2;
	}

	// Token: 0x040021A1 RID: 8609
	private Dictionary<RankingUtil.RankingRankerType, Dictionary<RankingUtil.RankingScoreType, List<MsgGetLeaderboardEntriesSucceed>>> m_rankingData;

	// Token: 0x040021A2 RID: 8610
	private Dictionary<RankingUtil.RankingRankerType, Dictionary<RankingUtil.RankingScoreType, int>> m_rankingUserOldRank;
}
