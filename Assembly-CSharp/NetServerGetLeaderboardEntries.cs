using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x02000743 RID: 1859
public class NetServerGetLeaderboardEntries : NetBase
{
	// Token: 0x0600315F RID: 12639 RVA: 0x00117144 File Offset: 0x00115344
	public NetServerGetLeaderboardEntries() : this(0, -1, -1, -1, -1, -1, null)
	{
		base.SetSecureFlag(false);
	}

	// Token: 0x06003160 RID: 12640 RVA: 0x00117168 File Offset: 0x00115368
	public NetServerGetLeaderboardEntries(int mode, int first, int count, int index, int rankingType, int eventId, string[] friendIdList)
	{
		this.paramMode = mode;
		this.paramFirst = first;
		this.paramCount = count;
		this.paramIndex = index;
		this.paramEventId = eventId;
		this.paramRankingType = rankingType;
		this.paramFriendIdList = friendIdList;
		base.SetSecureFlag(false);
	}

	// Token: 0x06003161 RID: 12641 RVA: 0x001171B8 File Offset: 0x001153B8
	protected override void DoRequest()
	{
		base.SetAction("Leaderboard/getWeeklyLeaderboardEntries");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			List<string> list = new List<string>();
			if (this.paramFriendIdList != null)
			{
				foreach (string item in this.paramFriendIdList)
				{
					list.Add(item);
				}
			}
			string getWeeklyLeaderboardEntries = instance.GetGetWeeklyLeaderboardEntries(this.paramMode, this.paramFirst, this.paramCount, this.paramRankingType, list, this.paramEventId);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(getWeeklyLeaderboardEntries);
		}
	}

	// Token: 0x06003162 RID: 12642 RVA: 0x00117258 File Offset: 0x00115458
	protected void SetParameter_LeaderboardEntries()
	{
		this.SetParameter_Mode();
		this.SetParameter_First();
		this.SetParameter_Count();
		this.SetParameter_RankingType();
		this.SetParameter_FriendIdList();
		this.SetParameter_EventId();
	}

	// Token: 0x06003163 RID: 12643 RVA: 0x0011728C File Offset: 0x0011548C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_LeaderboardEntries(jdata);
	}

	// Token: 0x06003164 RID: 12644 RVA: 0x00117298 File Offset: 0x00115498
	protected void GetResponse_LeaderboardEntries(JsonData jdata)
	{
		this.GetResponse_MyEntry(jdata);
		this.GetResponse_EntriesList(jdata);
		this.GetResponse_TotalEntries(jdata);
		this.GetResponse_ResetTime(jdata);
		this.GetResponse_StartTime(jdata);
		this.GetResponse_StartIndex(jdata);
		this.leaderboardEntries = new ServerLeaderboardEntries
		{
			m_myLeaderboardEntry = this.resultMyLeaderboardEntry,
			m_resultTotalEntries = this.resultTotalEntries,
			m_leaderboardEntries = this.resultLeaderboardEntriesList,
			m_resetTime = this.resetTime,
			m_startTime = this.startTime,
			m_startIndex = this.startIndex,
			m_mode = this.paramMode,
			m_first = this.paramFirst,
			m_count = this.paramCount,
			m_index = this.paramIndex,
			m_rankingType = this.paramRankingType,
			m_eventId = this.paramEventId,
			m_friendIdList = this.paramFriendIdList
		};
		this.leaderboardEntries.CopyTo(ServerInterface.LeaderboardEntries);
		if (this.IsRivalHighScore())
		{
			this.leaderboardEntries.CopyTo(ServerInterface.LeaderboardEntriesRivalHighScore);
		}
		if (ServerLeaderboardEntries.IsRivalHighScore(0, this.leaderboardEntries.m_rankingType))
		{
			ServerLeaderboardEntry rankTop = this.leaderboardEntries.GetRankTop();
			if (rankTop != null)
			{
				rankTop.CopyTo(ServerInterface.LeaderboardEntryRivalHighScoreTop);
			}
		}
	}

	// Token: 0x06003165 RID: 12645 RVA: 0x001173D8 File Offset: 0x001155D8
	protected override void DoDidSuccessEmulation()
	{
		this.resultTotalEntries = this.paramCount;
		this.resultLeaderboardEntriesList = new List<ServerLeaderboardEntry>(this.paramCount);
		this.resultMyLeaderboardEntry = new ServerLeaderboardEntry();
		this.resultMyLeaderboardEntry.m_grade = 0;
		int paramFirst = this.paramFirst;
		int num = paramFirst;
		while (num < 125 && num < paramFirst + this.paramCount)
		{
			ServerLeaderboardEntry serverLeaderboardEntry = new ServerLeaderboardEntry();
			serverLeaderboardEntry.m_hspId = "Xeen_" + string.Format("{0:D4}", num);
			serverLeaderboardEntry.m_grade = num + 1;
			serverLeaderboardEntry.m_score = (long)(10000 - num * 100);
			serverLeaderboardEntry.m_name = "Xeen_" + string.Format("{0:D4}", num);
			serverLeaderboardEntry.m_url = string.Empty;
			this.resultLeaderboardEntriesList.Add(serverLeaderboardEntry);
			num++;
		}
	}

	// Token: 0x1700069E RID: 1694
	// (get) Token: 0x06003166 RID: 12646 RVA: 0x001174B8 File Offset: 0x001156B8
	// (set) Token: 0x06003167 RID: 12647 RVA: 0x001174C0 File Offset: 0x001156C0
	public int paramMode { get; set; }

	// Token: 0x1700069F RID: 1695
	// (get) Token: 0x06003168 RID: 12648 RVA: 0x001174CC File Offset: 0x001156CC
	// (set) Token: 0x06003169 RID: 12649 RVA: 0x001174D4 File Offset: 0x001156D4
	public int paramFirst { get; set; }

	// Token: 0x170006A0 RID: 1696
	// (get) Token: 0x0600316A RID: 12650 RVA: 0x001174E0 File Offset: 0x001156E0
	// (set) Token: 0x0600316B RID: 12651 RVA: 0x001174E8 File Offset: 0x001156E8
	public int paramCount { get; set; }

	// Token: 0x170006A1 RID: 1697
	// (get) Token: 0x0600316C RID: 12652 RVA: 0x001174F4 File Offset: 0x001156F4
	// (set) Token: 0x0600316D RID: 12653 RVA: 0x001174FC File Offset: 0x001156FC
	public int paramIndex { get; set; }

	// Token: 0x170006A2 RID: 1698
	// (get) Token: 0x0600316E RID: 12654 RVA: 0x00117508 File Offset: 0x00115708
	// (set) Token: 0x0600316F RID: 12655 RVA: 0x00117510 File Offset: 0x00115710
	public int paramEventId { get; set; }

	// Token: 0x170006A3 RID: 1699
	// (get) Token: 0x06003170 RID: 12656 RVA: 0x0011751C File Offset: 0x0011571C
	// (set) Token: 0x06003171 RID: 12657 RVA: 0x00117524 File Offset: 0x00115724
	public int paramRankingType { get; set; }

	// Token: 0x170006A4 RID: 1700
	// (get) Token: 0x06003172 RID: 12658 RVA: 0x00117530 File Offset: 0x00115730
	// (set) Token: 0x06003173 RID: 12659 RVA: 0x00117538 File Offset: 0x00115738
	public string[] paramFriendIdList { get; set; }

	// Token: 0x06003174 RID: 12660 RVA: 0x00117544 File Offset: 0x00115744
	public bool IsRivalHighScore()
	{
		return ServerLeaderboardEntries.IsRivalHighScore(this.paramFirst, this.paramRankingType);
	}

	// Token: 0x06003175 RID: 12661 RVA: 0x00117558 File Offset: 0x00115758
	private void SetParameter_Mode()
	{
		base.WriteActionParamValue("mode", this.paramMode);
	}

	// Token: 0x06003176 RID: 12662 RVA: 0x00117570 File Offset: 0x00115770
	private void SetParameter_First()
	{
		if (-1 < this.paramFirst && -1 < this.paramCount)
		{
			base.WriteActionParamValue("first", this.paramFirst);
		}
	}

	// Token: 0x06003177 RID: 12663 RVA: 0x001175AC File Offset: 0x001157AC
	private void SetParameter_Count()
	{
		if (-1 < this.paramFirst && -1 < this.paramCount)
		{
			base.WriteActionParamValue("count", this.paramCount);
		}
	}

	// Token: 0x06003178 RID: 12664 RVA: 0x001175E8 File Offset: 0x001157E8
	private void SetParameter_RankingType()
	{
		base.WriteActionParamValue("type", this.paramRankingType);
	}

	// Token: 0x06003179 RID: 12665 RVA: 0x00117600 File Offset: 0x00115800
	private void SetParameter_FriendIdList()
	{
		if (this.paramFriendIdList != null && this.paramFriendIdList.Length != 0)
		{
			base.WriteActionParamArray("friendIdList", new List<object>(this.paramFriendIdList));
		}
	}

	// Token: 0x0600317A RID: 12666 RVA: 0x0011763C File Offset: 0x0011583C
	private void SetParameter_EventId()
	{
		base.WriteActionParamValue("eventId", this.paramEventId);
	}

	// Token: 0x170006A5 RID: 1701
	// (get) Token: 0x0600317B RID: 12667 RVA: 0x00117654 File Offset: 0x00115854
	// (set) Token: 0x0600317C RID: 12668 RVA: 0x0011765C File Offset: 0x0011585C
	private int resultTotalEntries { get; set; }

	// Token: 0x170006A6 RID: 1702
	// (get) Token: 0x0600317D RID: 12669 RVA: 0x00117668 File Offset: 0x00115868
	// (set) Token: 0x0600317E RID: 12670 RVA: 0x00117670 File Offset: 0x00115870
	private ServerLeaderboardEntry resultMyLeaderboardEntry { get; set; }

	// Token: 0x170006A7 RID: 1703
	// (get) Token: 0x0600317F RID: 12671 RVA: 0x0011767C File Offset: 0x0011587C
	private int resultEntries
	{
		get
		{
			if (this.resultLeaderboardEntriesList != null)
			{
				return this.resultLeaderboardEntriesList.Count;
			}
			return 0;
		}
	}

	// Token: 0x170006A8 RID: 1704
	// (get) Token: 0x06003180 RID: 12672 RVA: 0x00117698 File Offset: 0x00115898
	// (set) Token: 0x06003181 RID: 12673 RVA: 0x001176A0 File Offset: 0x001158A0
	private int resetTime { get; set; }

	// Token: 0x170006A9 RID: 1705
	// (get) Token: 0x06003182 RID: 12674 RVA: 0x001176AC File Offset: 0x001158AC
	// (set) Token: 0x06003183 RID: 12675 RVA: 0x001176B4 File Offset: 0x001158B4
	private int startTime { get; set; }

	// Token: 0x170006AA RID: 1706
	// (get) Token: 0x06003184 RID: 12676 RVA: 0x001176C0 File Offset: 0x001158C0
	// (set) Token: 0x06003185 RID: 12677 RVA: 0x001176C8 File Offset: 0x001158C8
	private int startIndex { get; set; }

	// Token: 0x170006AB RID: 1707
	// (get) Token: 0x06003186 RID: 12678 RVA: 0x001176D4 File Offset: 0x001158D4
	// (set) Token: 0x06003187 RID: 12679 RVA: 0x001176DC File Offset: 0x001158DC
	public ServerLeaderboardEntries leaderboardEntries { get; protected set; }

	// Token: 0x170006AC RID: 1708
	// (get) Token: 0x06003188 RID: 12680 RVA: 0x001176E8 File Offset: 0x001158E8
	// (set) Token: 0x06003189 RID: 12681 RVA: 0x001176F0 File Offset: 0x001158F0
	protected List<ServerLeaderboardEntry> resultLeaderboardEntriesList { get; set; }

	// Token: 0x0600318A RID: 12682 RVA: 0x001176FC File Offset: 0x001158FC
	public ServerLeaderboardEntry GetResultLeaderboardEntry(int index)
	{
		if (0 <= index && this.resultEntries > index)
		{
			return this.resultLeaderboardEntriesList[index];
		}
		return null;
	}

	// Token: 0x0600318B RID: 12683 RVA: 0x00117720 File Offset: 0x00115920
	private void GetResponse_MyEntry(JsonData jdata)
	{
		this.resultMyLeaderboardEntry = NetUtil.AnalyzeLeaderboardEntryJson(jdata, "playerEntry");
	}

	// Token: 0x0600318C RID: 12684 RVA: 0x00117734 File Offset: 0x00115934
	private void GetResponse_EntriesList(JsonData jdata)
	{
		this.resultLeaderboardEntriesList = new List<ServerLeaderboardEntry>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "entriesList");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerLeaderboardEntry item = NetUtil.AnalyzeLeaderboardEntryJson(jdata2, string.Empty);
			this.resultLeaderboardEntriesList.Add(item);
		}
	}

	// Token: 0x0600318D RID: 12685 RVA: 0x00117794 File Offset: 0x00115994
	private void GetResponse_TotalEntries(JsonData jdata)
	{
		this.resultTotalEntries = NetUtil.GetJsonInt(jdata, "totalEntries");
	}

	// Token: 0x0600318E RID: 12686 RVA: 0x001177A8 File Offset: 0x001159A8
	private void GetResponse_ResetTime(JsonData jdata)
	{
		this.resetTime = NetUtil.GetJsonInt(jdata, "resetTime");
	}

	// Token: 0x0600318F RID: 12687 RVA: 0x001177BC File Offset: 0x001159BC
	private void GetResponse_StartTime(JsonData jdata)
	{
		this.startTime = NetUtil.GetJsonInt(jdata, "startTime");
	}

	// Token: 0x06003190 RID: 12688 RVA: 0x001177D0 File Offset: 0x001159D0
	private void GetResponse_StartIndex(JsonData jdata)
	{
		this.startIndex = NetUtil.GetJsonInt(jdata, "startIndex");
	}
}
