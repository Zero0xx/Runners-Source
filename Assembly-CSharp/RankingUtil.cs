using System;
using System.Collections.Generic;
using App;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x02000500 RID: 1280
public class RankingUtil
{
	// Token: 0x170004FE RID: 1278
	// (get) Token: 0x06002635 RID: 9781 RVA: 0x000E90B0 File Offset: 0x000E72B0
	public static RankingUtil.RankingMode currentRankingMode
	{
		get
		{
			RankingUtil.RankingMode result = RankingUtil.RankingMode.ENDLESS;
			if (RankingUtil.m_currentRankingMode != RankingUtil.RankingMode.COUNT)
			{
				result = RankingUtil.m_currentRankingMode;
			}
			else
			{
				global::Debug.Log("RankingUtil currentMode error !!!!!");
			}
			return result;
		}
	}

	// Token: 0x06002636 RID: 9782 RVA: 0x000E90E0 File Offset: 0x000E72E0
	public static void SetCurrentRankingMode(RankingUtil.RankingMode mode)
	{
		RankingUtil.m_currentRankingMode = mode;
		global::Debug.Log("RankingUtil SetCurrentRankingMode  currentRankingMode:" + RankingUtil.m_currentRankingMode);
	}

	// Token: 0x06002637 RID: 9783 RVA: 0x000E9104 File Offset: 0x000E7304
	public static bool IsRankingUserFrontAndBack(RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType, int page)
	{
		bool result = false;
		if (rankerType != RankingUtil.RankingRankerType.RIVAL && page == 0 && rankerType == RankingUtil.RankingRankerType.SP_ALL)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06002638 RID: 9784 RVA: 0x000E912C File Offset: 0x000E732C
	public static int GetRankingCode(RankingUtil.RankingMode rankingMode, RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType)
	{
		int num = -1;
		if (rankingMode != RankingUtil.RankingMode.COUNT && scoreType != RankingUtil.RankingScoreType.NONE && rankerType != RankingUtil.RankingRankerType.COUNT)
		{
			num = (int)((RankingUtil.RankingMode)1000 * rankingMode);
			num += RankingUtil.GetRankingType(scoreType, rankerType);
		}
		return num;
	}

	// Token: 0x06002639 RID: 9785 RVA: 0x000E9164 File Offset: 0x000E7364
	public static int GetRankingType(RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType)
	{
		if (scoreType == RankingUtil.RankingScoreType.NONE || rankerType == RankingUtil.RankingRankerType.COUNT)
		{
			return -1;
		}
		return (int)(rankerType * RankingUtil.RankingRankerType.RIVAL + (int)scoreType);
	}

	// Token: 0x0600263A RID: 9786 RVA: 0x000E918C File Offset: 0x000E738C
	public static RankingUtil.RankingMode GetRankerMode(int rankingType)
	{
		RankingUtil.RankingMode result = RankingUtil.RankingMode.ENDLESS;
		if (rankingType >= 1000)
		{
			result = (RankingUtil.RankingMode)(rankingType / 1000);
		}
		return result;
	}

	// Token: 0x0600263B RID: 9787 RVA: 0x000E91B0 File Offset: 0x000E73B0
	public static RankingUtil.RankingScoreType GetRankerScoreType(int rankingType)
	{
		rankingType %= 1000;
		return (RankingUtil.RankingScoreType)(rankingType % 2);
	}

	// Token: 0x0600263C RID: 9788 RVA: 0x000E91D0 File Offset: 0x000E73D0
	public static RankingUtil.RankingRankerType GetRankerType(int rankingType)
	{
		rankingType %= 1000;
		return (RankingUtil.RankingRankerType)(rankingType / 2);
	}

	// Token: 0x0600263D RID: 9789 RVA: 0x000E91F0 File Offset: 0x000E73F0
	public static string GetLeagueName(LeagueType type)
	{
		if (type < LeagueType.NUM)
		{
			TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "league_" + (uint)type);
			if (text != null)
			{
				return text.text;
			}
		}
		return string.Empty;
	}

	// Token: 0x0600263E RID: 9790 RVA: 0x000E9238 File Offset: 0x000E7438
	public static LeagueCategory GetLeagueCategory(LeagueType type)
	{
		if (type < LeagueType.NUM)
		{
			return RankingUtil.LEAGUE_PARAMS[(int)type].m_category;
		}
		return LeagueCategory.NONE;
	}

	// Token: 0x0600263F RID: 9791 RVA: 0x000E9250 File Offset: 0x000E7450
	public static string GetLeagueCategoryName(LeagueType type)
	{
		if (type < LeagueType.NUM)
		{
			return RankingUtil.LEAGUE_PARAMS[(int)type].m_categoryName;
		}
		return string.Empty;
	}

	// Token: 0x06002640 RID: 9792 RVA: 0x000E926C File Offset: 0x000E746C
	public static uint GetLeagueCategoryClass(LeagueType type)
	{
		if (type < LeagueType.NUM)
		{
			return (uint)(type % LeagueType.E_M);
		}
		return 0U;
	}

	// Token: 0x06002641 RID: 9793 RVA: 0x000E9288 File Offset: 0x000E7488
	public static void GetMyLeagueData(RankingUtil.RankingMode rankingMode, ref int leagueIndex, ref int upCount, ref int downCount)
	{
		if (SingletonGameObject<RankingManager>.Instance != null)
		{
			RankingUtil.RankingDataSet rankingDataSet = SingletonGameObject<RankingManager>.Instance.GetRankingDataSet(rankingMode);
			if (rankingDataSet != null && rankingDataSet.leagueData != null)
			{
				leagueIndex = rankingDataSet.leagueData.leagueId;
				upCount = rankingDataSet.leagueData.numUp;
				downCount = rankingDataSet.leagueData.numDown;
			}
		}
	}

	// Token: 0x06002642 RID: 9794 RVA: 0x000E92EC File Offset: 0x000E74EC
	public static void SetLeagueObject(RankingUtil.RankingMode rankingMode, ref UISprite icon0, ref UISprite icon1, ref UILabel rankText0, ref UILabel rankText1)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		RankingUtil.GetMyLeagueData(rankingMode, ref num, ref num2, ref num3);
		icon0.spriteName = RankingUtil.GetLeagueIconNameL((LeagueType)num);
		icon1.spriteName = RankingUtil.GetLeagueIconNameL2((LeagueType)num);
		rankText0.text = TextUtility.Replaces(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_league_tab_1").text, new Dictionary<string, string>
		{
			{
				"{PARAM_1}",
				RankingUtil.GetLeagueName((LeagueType)num)
			}
		});
		string value = string.Empty;
		if (RankingManager.EndlessRivalRankingScoreType == RankingUtil.RankingScoreType.HIGH_SCORE)
		{
			value = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ui_Lbl_high_score").text;
		}
		else
		{
			value = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ui_Lbl_total_score").text;
		}
		if (num2 == 0)
		{
			rankText1.text = TextUtility.Replaces(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_league_tab_4").text, new Dictionary<string, string>
			{
				{
					"{SCORE}",
					value
				},
				{
					"{PARAM_1}",
					num3.ToString()
				}
			});
		}
		else if (num3 == 0)
		{
			rankText1.text = TextUtility.Replaces(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_league_tab_3").text, new Dictionary<string, string>
			{
				{
					"{SCORE}",
					value
				},
				{
					"{PARAM_1}",
					num2.ToString()
				}
			});
		}
		else
		{
			rankText1.text = TextUtility.Replaces(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_league_tab_2").text, new Dictionary<string, string>
			{
				{
					"{SCORE}",
					value
				},
				{
					"{PARAM_1}",
					num2.ToString()
				},
				{
					"{PARAM_2}",
					num3.ToString()
				}
			});
		}
	}

	// Token: 0x06002643 RID: 9795 RVA: 0x000E94AC File Offset: 0x000E76AC
	public static void SetLeagueObjectForMainMenu(RankingUtil.RankingMode rankingMode, UISprite icon0, UISprite icon1, UILabel rankingText)
	{
		int leagueType = 0;
		int num = 0;
		int num2 = 0;
		RankingUtil.GetMyLeagueData(rankingMode, ref leagueType, ref num, ref num2);
		icon0.spriteName = RankingUtil.GetLeagueIconName((LeagueType)leagueType);
		icon1.spriteName = RankingUtil.GetLeagueIconName2((LeagueType)leagueType);
		int num3 = 0;
		RankingUtil.RankingRankerType rankType = RankingUtil.RankingRankerType.RIVAL;
		RankingUtil.RankingScoreType scoreType = RankingUtil.RankingScoreType.HIGH_SCORE;
		if (rankingMode == RankingUtil.RankingMode.ENDLESS)
		{
			scoreType = RankingManager.EndlessRivalRankingScoreType;
		}
		else if (rankingMode == RankingUtil.RankingMode.QUICK)
		{
			scoreType = RankingManager.QuickRivalRankingScoreType;
		}
		RankingUtil.Ranker myRank = RankingManager.GetMyRank(rankingMode, rankType, scoreType);
		if (myRank != null)
		{
			num3 = myRank.rankIndex + 1;
		}
		rankingText.text = num3.ToString("00");
	}

	// Token: 0x06002644 RID: 9796 RVA: 0x000E953C File Offset: 0x000E773C
	public static string GetLeagueIconName(LeagueType leagueType)
	{
		return "ui_ranking_league_icon_s_" + RankingUtil.GetLeagueCategoryName(leagueType).ToLower();
	}

	// Token: 0x06002645 RID: 9797 RVA: 0x000E9554 File Offset: 0x000E7754
	public static string GetLeagueIconName2(LeagueType leagueType)
	{
		return "ui_ranking_league_icon_s_" + RankingUtil.GetLeagueCategoryClass(leagueType).ToString();
	}

	// Token: 0x06002646 RID: 9798 RVA: 0x000E957C File Offset: 0x000E777C
	public static string GetLeagueIconNameL(LeagueType leagueType)
	{
		return "ui_ranking_league_icon_" + RankingUtil.GetLeagueCategoryName(leagueType).ToLower();
	}

	// Token: 0x06002647 RID: 9799 RVA: 0x000E9594 File Offset: 0x000E7794
	public static string GetLeagueIconNameL2(LeagueType leagueType)
	{
		return "ui_ranking_league_icon_" + RankingUtil.GetLeagueCategoryClass(leagueType).ToString();
	}

	// Token: 0x06002648 RID: 9800 RVA: 0x000E95BC File Offset: 0x000E77BC
	public static Texture2D GetProfilePictureTexture(RankingUtil.Ranker ranker, Action<Texture2D> callback)
	{
		return RankingUtil.GetProfilePictureTexture(ranker.id, callback);
	}

	// Token: 0x06002649 RID: 9801 RVA: 0x000E95CC File Offset: 0x000E77CC
	public static Texture2D GetProfilePictureTexture(string userId, Action<Texture2D> callback)
	{
		if (RankingUtil.s_socialInterface == null)
		{
			RankingUtil.s_socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		}
		global::Debug.Log("GetProfilePictureTexture.gameId:" + userId);
		string text = null;
		if (RankingUtil.s_socialInterface != null)
		{
			SocialUserData socialUserDataFromGameId = SocialInterface.GetSocialUserDataFromGameId(RankingUtil.s_socialInterface.FriendWithMeList, userId);
			if (socialUserDataFromGameId != null)
			{
				text = socialUserDataFromGameId.Id;
			}
		}
		global::Debug.Log("GetProfilePictureTexture.socialId:" + text);
		PlayerImageManager playerImageManager = GameObjectUtil.FindGameObjectComponent<PlayerImageManager>("PlayerImageManager");
		if (playerImageManager != null)
		{
			return playerImageManager.GetPlayerImage(text, string.Empty, callback);
		}
		return null;
	}

	// Token: 0x0600264A RID: 9802 RVA: 0x000E9670 File Offset: 0x000E7870
	public static List<RankingUtil.Ranker> GetRankerList(MsgGetLeaderboardEntriesSucceed msg)
	{
		List<RankingUtil.Ranker> list = new List<RankingUtil.Ranker>();
		if (msg != null && msg.m_leaderboardEntries != null)
		{
			ServerLeaderboardEntries leaderboardEntries = msg.m_leaderboardEntries;
			if (leaderboardEntries.m_myLeaderboardEntry != null)
			{
				list.Add(new RankingUtil.Ranker(leaderboardEntries.m_myLeaderboardEntry));
			}
			else
			{
				list.Add(null);
			}
			if (leaderboardEntries.m_leaderboardEntries != null && leaderboardEntries.m_leaderboardEntries.Count > 0)
			{
				List<ServerLeaderboardEntry> leaderboardEntries2 = leaderboardEntries.m_leaderboardEntries;
				if (leaderboardEntries2 != null)
				{
					int num = leaderboardEntries2.Count;
					if (!leaderboardEntries.IsRivalRanking() && leaderboardEntries.IsNext())
					{
						num = leaderboardEntries2.Count - 1;
					}
					for (int i = 0; i < leaderboardEntries2.Count; i++)
					{
						if (i >= num)
						{
							break;
						}
						list.Add(new RankingUtil.Ranker(leaderboardEntries2[i]));
					}
				}
			}
		}
		else
		{
			list.Add(null);
		}
		return list;
	}

	// Token: 0x0600264B RID: 9803 RVA: 0x000E9760 File Offset: 0x000E7960
	public static MsgGetLeaderboardEntriesSucceed InitRankingMsg(MsgGetLeaderboardEntriesSucceed msg)
	{
		int num = 4;
		MsgGetLeaderboardEntriesSucceed msgGetLeaderboardEntriesSucceed = new MsgGetLeaderboardEntriesSucceed();
		msgGetLeaderboardEntriesSucceed.m_leaderboardEntries = new ServerLeaderboardEntries();
		msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_resetTime = msg.m_leaderboardEntries.m_resetTime;
		msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_startTime = msg.m_leaderboardEntries.m_startTime;
		msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_startIndex = msg.m_leaderboardEntries.m_startIndex;
		msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_first = msg.m_leaderboardEntries.m_first;
		msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_count = msg.m_leaderboardEntries.m_count;
		if (msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_count > num)
		{
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_count = num;
		}
		msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_rankingType = msg.m_leaderboardEntries.m_rankingType;
		if (msg.m_leaderboardEntries.m_friendIdList == null)
		{
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_friendIdList = null;
		}
		else
		{
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_friendIdList = new string[msg.m_leaderboardEntries.m_friendIdList.Length];
		}
		if (msg.m_leaderboardEntries.m_myLeaderboardEntry == null)
		{
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_myLeaderboardEntry = null;
		}
		else
		{
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_myLeaderboardEntry = msg.m_leaderboardEntries.m_myLeaderboardEntry.Clone();
		}
		if (msg.m_leaderboardEntries.m_leaderboardEntries == null)
		{
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_leaderboardEntries = null;
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_resultTotalEntries = 0;
		}
		else
		{
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_leaderboardEntries = new List<ServerLeaderboardEntry>();
			int num2 = 0;
			foreach (ServerLeaderboardEntry serverLeaderboardEntry in msg.m_leaderboardEntries.m_leaderboardEntries)
			{
				msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_leaderboardEntries.Add(serverLeaderboardEntry.Clone());
				num2++;
				if (num <= num2)
				{
					break;
				}
			}
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_resultTotalEntries = msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_leaderboardEntries.Count;
		}
		return msgGetLeaderboardEntriesSucceed;
	}

	// Token: 0x0600264C RID: 9804 RVA: 0x000E9978 File Offset: 0x000E7B78
	public static MsgGetLeaderboardEntriesSucceed CopyRankingMsg(MsgGetLeaderboardEntriesSucceed msg, MsgGetLeaderboardEntriesSucceed org = null)
	{
		MsgGetLeaderboardEntriesSucceed msgGetLeaderboardEntriesSucceed = new MsgGetLeaderboardEntriesSucceed();
		msgGetLeaderboardEntriesSucceed.m_leaderboardEntries = new ServerLeaderboardEntries();
		msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_resetTime = msg.m_leaderboardEntries.m_resetTime;
		msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_startTime = msg.m_leaderboardEntries.m_startTime;
		msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_startIndex = msg.m_leaderboardEntries.m_startIndex;
		msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_first = msg.m_leaderboardEntries.m_first;
		msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_count = msg.m_leaderboardEntries.m_count;
		msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_rankingType = msg.m_leaderboardEntries.m_rankingType;
		if (msg.m_leaderboardEntries.m_friendIdList == null)
		{
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_friendIdList = null;
		}
		else
		{
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_friendIdList = new string[msg.m_leaderboardEntries.m_friendIdList.Length];
		}
		if (msg.m_leaderboardEntries.m_myLeaderboardEntry == null)
		{
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_myLeaderboardEntry = null;
		}
		else
		{
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_myLeaderboardEntry = msg.m_leaderboardEntries.m_myLeaderboardEntry.Clone();
		}
		if (msg.m_leaderboardEntries.m_leaderboardEntries == null)
		{
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_leaderboardEntries = null;
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_resultTotalEntries = 0;
		}
		else
		{
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_leaderboardEntries = new List<ServerLeaderboardEntry>();
			foreach (ServerLeaderboardEntry serverLeaderboardEntry in msg.m_leaderboardEntries.m_leaderboardEntries)
			{
				msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_leaderboardEntries.Add(serverLeaderboardEntry.Clone());
			}
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_resultTotalEntries = msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_leaderboardEntries.Count;
		}
		if (org == null || (msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_first == org.m_leaderboardEntries.m_first && msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_count == org.m_leaderboardEntries.m_count))
		{
			return msgGetLeaderboardEntriesSucceed;
		}
		bool flag = false;
		bool flag2 = false;
		int num = msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_first + msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_resultTotalEntries;
		int num2 = org.m_leaderboardEntries.m_first + org.m_leaderboardEntries.m_resultTotalEntries;
		if (msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_first <= org.m_leaderboardEntries.m_first && num >= num2)
		{
			return msgGetLeaderboardEntriesSucceed;
		}
		if (num == num2)
		{
			if (msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_count <= msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_resultTotalEntries)
			{
				flag = true;
			}
		}
		else if (num > num2)
		{
			if (msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_count <= msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_resultTotalEntries)
			{
				flag = true;
			}
		}
		else if (org.m_leaderboardEntries.m_count <= org.m_leaderboardEntries.m_resultTotalEntries)
		{
			flag = true;
		}
		List<ServerLeaderboardEntry> list = new List<ServerLeaderboardEntry>();
		if (msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_first > org.m_leaderboardEntries.m_first)
		{
			flag2 = true;
			foreach (ServerLeaderboardEntry item in org.m_leaderboardEntries.m_leaderboardEntries)
			{
				list.Add(item);
			}
		}
		else
		{
			flag2 = false;
			foreach (ServerLeaderboardEntry item2 in msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_leaderboardEntries)
			{
				list.Add(item2);
			}
		}
		if (flag2)
		{
			int num3 = msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_first - 1;
			int num4 = num2;
			if (num > num2)
			{
				num4 = num;
			}
			int count = list.Count;
			for (int i = num3; i < num4; i++)
			{
				if (i < count)
				{
					if (i - num3 < msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_leaderboardEntries.Count)
					{
						list[i] = msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_leaderboardEntries[i - num3];
					}
				}
				else if (i - num3 < msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_leaderboardEntries.Count)
				{
					list.Add(msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_leaderboardEntries[i - num3]);
				}
			}
		}
		else
		{
			int num3 = msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_first - org.m_leaderboardEntries.m_first;
			int num4 = num - msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_first;
			for (int j = num3; j < num4; j++)
			{
				if (j >= list.Count && j - num3 < org.m_leaderboardEntries.m_leaderboardEntries.Count)
				{
					list.Add(org.m_leaderboardEntries.m_leaderboardEntries[j - num3]);
				}
			}
		}
		if (msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_first > org.m_leaderboardEntries.m_first)
		{
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_first = org.m_leaderboardEntries.m_first;
		}
		msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_leaderboardEntries = list;
		msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_resultTotalEntries = list.Count;
		if (flag)
		{
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_count = list.Count;
		}
		else
		{
			msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_count = list.Count - 1;
		}
		return msgGetLeaderboardEntriesSucceed;
	}

	// Token: 0x0600264D RID: 9805 RVA: 0x000E9F28 File Offset: 0x000E8128
	public static string[] GetFriendIdList()
	{
		string[] result = null;
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null && socialInterface.IsLoggedIn)
		{
			result = SocialInterface.GetGameIdList(socialInterface.FriendList).ToArray();
		}
		return result;
	}

	// Token: 0x0600264E RID: 9806 RVA: 0x000E9F6C File Offset: 0x000E816C
	public static string GetResetTime(TimeSpan span, bool isHeadText = true)
	{
		string text = string.Empty;
		bool flag = span.Ticks <= 0L;
		if (flag)
		{
			text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_sumup").text;
		}
		else
		{
			if (isHeadText)
			{
				text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_reset").text + "\n";
			}
			text += TextUtility.Replaces(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", (span.Days <= 0) ? ((span.Hours <= 0) ? ((span.Minutes <= 0) ? "ranking_reset_seconds" : "ranking_reset_minutes") : "ranking_reset_hours") : "ranking_reset_days").text, new Dictionary<string, string>
			{
				{
					"{DAYS}",
					span.Days.ToString()
				},
				{
					"{HOURS}",
					span.Hours.ToString()
				},
				{
					"{MINUTES}",
					span.Minutes.ToString()
				}
			});
		}
		return text;
	}

	// Token: 0x0600264F RID: 9807 RVA: 0x000EA094 File Offset: 0x000E8294
	public static bool ShowRankingChangeWindow(RankingUtil.RankingMode rankingMode)
	{
		global::Debug.Log("ShowRankingChangeWindow");
		if (RankingResultBitWindow.Instance != null)
		{
			RankingResultBitWindow.Instance.Open(rankingMode);
			return true;
		}
		global::Debug.Log("ShowRankingChangeWindow error?");
		return false;
	}

	// Token: 0x06002650 RID: 9808 RVA: 0x000EA0D4 File Offset: 0x000E82D4
	public static bool IsEndRankingChangeWindow()
	{
		if (RankingResultBitWindow.Instance != null)
		{
			return RankingResultBitWindow.Instance.IsEnd;
		}
		global::Debug.Log("IsEndRankingChangeWindow error?");
		return false;
	}

	// Token: 0x06002651 RID: 9809 RVA: 0x000EA108 File Offset: 0x000E8308
	public static string GetFriendIconSpriteName(RankingUtil.Ranker ranker)
	{
		if (ranker == null || !ranker.isFriend)
		{
			return string.Empty;
		}
		if (ranker.isSentEnergy)
		{
			return "ui_ranking_scroll_icon_friend_1";
		}
		return "ui_ranking_scroll_icon_friend_0";
	}

	// Token: 0x06002652 RID: 9810 RVA: 0x000EA138 File Offset: 0x000E8338
	public static string GetFriendIconSpriteName(RaidBossUser user)
	{
		if (user == null || !user.isFriend)
		{
			return string.Empty;
		}
		if (user.isSentEnergy)
		{
			return "ui_ranking_scroll_icon_friend_1";
		}
		return "ui_ranking_scroll_icon_friend_0";
	}

	// Token: 0x06002653 RID: 9811 RVA: 0x000EA168 File Offset: 0x000E8368
	public static void DebugRankingChange()
	{
		global::Debug.Log("DebugRankingChange !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
		RankingUtil.RankingRankerType rankingRankerType = RankingUtil.RankingRankerType.RIVAL;
		RankingUtil.RankingScoreType endlessRivalRankingScoreType = RankingManager.EndlessRivalRankingScoreType;
		int num = -1;
		int num2 = -1;
		RankingUtil.RankChange rankingRankChange = SingletonGameObject<RankingManager>.Instance.GetRankingRankChange(RankingUtil.RankingMode.ENDLESS, endlessRivalRankingScoreType, rankingRankerType, out num, out num2);
		string text = string.Concat(new object[]
		{
			"set rankerType:",
			rankingRankerType,
			"  scoreType:",
			endlessRivalRankingScoreType
		});
		text = text + "\n  change:" + rankingRankChange;
		if (rankingRankChange != RankingUtil.RankChange.NONE)
		{
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"   ",
				num2,
				" → ",
				num
			});
		}
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "debug_ranking_change",
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = "Debug",
			message = text
		});
	}

	// Token: 0x06002654 RID: 9812 RVA: 0x000EA25C File Offset: 0x000E845C
	public static void DebugCurrentRanking(bool isEvent, long score)
	{
		long num;
		long num2;
		int type;
		bool currentRankingStatus = RankingManager.GetCurrentRankingStatus(RankingUtil.RankingMode.ENDLESS, isEvent, out num, out num2, out type);
		long num3 = score;
		if (isEvent)
		{
			num3 = score + num;
		}
		int num4 = 0;
		bool flag;
		long num5;
		long num6;
		int currentHighScoreRank = RankingManager.GetCurrentHighScoreRank(RankingUtil.RankingMode.ENDLESS, isEvent, ref num3, out flag, out num5, out num6, out num4);
		string text = string.Concat(new object[]
		{
			"set  isEvent:",
			isEvent,
			"  score:",
			score
		});
		text += "\n◆GetCurrentRankingStatus";
		text = text + "\n\u3000\u3000isStatus:" + currentRankingStatus;
		text = text + "\n\u3000\u3000myScore:" + num;
		text = text + "\n\u3000\u3000myLeague:" + RankingUtil.GetLeagueName((LeagueType)type);
		text = text + "\n◆GetCurrentHighScoreRank    currentScore:" + num3;
		text = text + "\n\u3000\u3000rank:" + currentHighScoreRank;
		text = text + "\n\u3000\u3000isHighScore:" + flag;
		text = text + "\n\u3000\u3000nextRankScore:" + num5;
		text = text + "\n\u3000\u3000prveRankScore:" + num6;
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "debug_info",
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = "Debug",
			message = text
		});
	}

	// Token: 0x04002298 RID: 8856
	public const int RANKING_GET_LIMIT = 30000;

	// Token: 0x04002299 RID: 8857
	private static readonly LeagueTypeParam[] LEAGUE_PARAMS = new LeagueTypeParam[]
	{
		new LeagueTypeParam(LeagueCategory.F, "F"),
		new LeagueTypeParam(LeagueCategory.F, "F"),
		new LeagueTypeParam(LeagueCategory.F, "F"),
		new LeagueTypeParam(LeagueCategory.E, "E"),
		new LeagueTypeParam(LeagueCategory.E, "E"),
		new LeagueTypeParam(LeagueCategory.E, "E"),
		new LeagueTypeParam(LeagueCategory.D, "D"),
		new LeagueTypeParam(LeagueCategory.D, "D"),
		new LeagueTypeParam(LeagueCategory.D, "D"),
		new LeagueTypeParam(LeagueCategory.C, "C"),
		new LeagueTypeParam(LeagueCategory.C, "C"),
		new LeagueTypeParam(LeagueCategory.C, "C"),
		new LeagueTypeParam(LeagueCategory.B, "B"),
		new LeagueTypeParam(LeagueCategory.B, "B"),
		new LeagueTypeParam(LeagueCategory.B, "B"),
		new LeagueTypeParam(LeagueCategory.A, "A"),
		new LeagueTypeParam(LeagueCategory.A, "A"),
		new LeagueTypeParam(LeagueCategory.A, "A"),
		new LeagueTypeParam(LeagueCategory.S, "S"),
		new LeagueTypeParam(LeagueCategory.S, "S"),
		new LeagueTypeParam(LeagueCategory.S, "S")
	};

	// Token: 0x0400229A RID: 8858
	public static SocialInterface s_socialInterface = null;

	// Token: 0x0400229B RID: 8859
	private static RankingUtil.RankingMode m_currentRankingMode = RankingUtil.RankingMode.COUNT;

	// Token: 0x02000501 RID: 1281
	public enum UserDataType
	{
		// Token: 0x0400229D RID: 8861
		RANKING,
		// Token: 0x0400229E RID: 8862
		RAID_BOSS,
		// Token: 0x0400229F RID: 8863
		DAILY_BATTLE,
		// Token: 0x040022A0 RID: 8864
		RANK_UP
	}

	// Token: 0x02000502 RID: 1282
	public enum RankingMode
	{
		// Token: 0x040022A2 RID: 8866
		ENDLESS,
		// Token: 0x040022A3 RID: 8867
		QUICK,
		// Token: 0x040022A4 RID: 8868
		COUNT
	}

	// Token: 0x02000503 RID: 1283
	public enum RankingScoreType
	{
		// Token: 0x040022A6 RID: 8870
		HIGH_SCORE,
		// Token: 0x040022A7 RID: 8871
		TOTAL_SCORE,
		// Token: 0x040022A8 RID: 8872
		NONE
	}

	// Token: 0x02000504 RID: 1284
	public enum RankChange
	{
		// Token: 0x040022AA RID: 8874
		NONE,
		// Token: 0x040022AB RID: 8875
		STAY,
		// Token: 0x040022AC RID: 8876
		UP,
		// Token: 0x040022AD RID: 8877
		DOWN
	}

	// Token: 0x02000505 RID: 1285
	public enum RankingRankerType
	{
		// Token: 0x040022AF RID: 8879
		FRIEND,
		// Token: 0x040022B0 RID: 8880
		ALL,
		// Token: 0x040022B1 RID: 8881
		RIVAL,
		// Token: 0x040022B2 RID: 8882
		HISTORY,
		// Token: 0x040022B3 RID: 8883
		SP_ALL,
		// Token: 0x040022B4 RID: 8884
		SP_FRIEND,
		// Token: 0x040022B5 RID: 8885
		COUNT
	}

	// Token: 0x02000506 RID: 1286
	public class Ranker
	{
		// Token: 0x06002655 RID: 9813 RVA: 0x000EA3B8 File Offset: 0x000E85B8
		public Ranker(ServerDailyBattleData user)
		{
			this.id = user.userId;
			this.score = user.maxScore;
			this.hiScore = user.maxScore;
			this.userName = user.name;
			this.isSentEnergy = user.isSentEnergy;
			this.rankIndex = user.goOnWin;
			this.rankIndexChanged = user.goOnWin;
			this.mapRank = user.numRank;
			this.loginTime = NetUtil.GetLocalDateTime(user.loginTime);
			ServerItem serverItem = new ServerItem((ServerItem.Id)user.charaId);
			this.charaType = serverItem.charaType;
			ServerItem serverItem2 = new ServerItem((ServerItem.Id)user.subCharaId);
			this.charaSubType = serverItem2.charaType;
			this.charaLevel = user.charaLevel;
			ServerItem serverItem3 = new ServerItem((ServerItem.Id)user.mainChaoId);
			this.mainChaoId = serverItem3.chaoId;
			ServerItem serverItem4 = new ServerItem((ServerItem.Id)user.subChaoId);
			this.subChaoId = serverItem4.chaoId;
			this.mainChaoLevel = user.mainChaoLevel;
			this.subChaoLevel = user.subChaoLevel;
			this.leagueIndex = user.league;
			this.language = user.language;
			this.userDataType = RankingUtil.UserDataType.DAILY_BATTLE;
			if (RankingUtil.Ranker.s_socialInterface == null)
			{
				RankingUtil.Ranker.s_socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
			}
			if (RankingUtil.Ranker.s_socialInterface != null)
			{
				this.isFriend = (SocialInterface.GetSocialUserDataFromGameId(RankingUtil.Ranker.s_socialInterface.FriendList, this.id) != null);
			}
			else
			{
				this.isFriend = (this.isSentEnergy || UnityEngine.Random.Range(0, 3) != 0);
			}
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x000EA564 File Offset: 0x000E8764
		public Ranker(RaidBossUser user)
		{
			this.id = user.id;
			this.score = 0L;
			this.hiScore = 0L;
			this.userName = user.userName;
			this.isSentEnergy = user.isSentEnergy;
			this.rankIndex = user.rankIndex;
			this.rankIndexChanged = user.rankIndexChanged;
			this.mapRank = user.mapRank;
			this.loginTime = user.loginTime;
			this.charaType = user.charaType;
			this.charaSubType = user.charaSubType;
			this.charaLevel = user.charaLevel;
			this.mainChaoId = user.mainChaoId;
			this.subChaoId = user.subChaoId;
			this.mainChaoLevel = user.mainChaoLevel;
			this.subChaoLevel = user.subChaoLevel;
			this.leagueIndex = user.leagueIndex;
			this.language = user.language;
			this.userDataType = RankingUtil.UserDataType.RAID_BOSS;
			if (RankingUtil.Ranker.s_socialInterface == null)
			{
				RankingUtil.Ranker.s_socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
			}
			if (RankingUtil.Ranker.s_socialInterface != null)
			{
				this.isFriend = (SocialInterface.GetSocialUserDataFromGameId(RankingUtil.Ranker.s_socialInterface.FriendList, this.id) != null);
			}
			else
			{
				this.isFriend = (this.isSentEnergy || UnityEngine.Random.Range(0, 3) != 0);
			}
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000EA6CC File Offset: 0x000E88CC
		public Ranker(ServerLeaderboardEntry serverLeaderboardEntry)
		{
			this.id = serverLeaderboardEntry.m_hspId;
			this.score = serverLeaderboardEntry.m_score;
			this.hiScore = serverLeaderboardEntry.m_hiScore;
			this.userName = serverLeaderboardEntry.m_name;
			serverLeaderboardEntry.m_url = string.Empty;
			serverLeaderboardEntry.m_userData = 0;
			this.isSentEnergy = serverLeaderboardEntry.m_energyFlg;
			this.rankIndex = serverLeaderboardEntry.m_grade - 1;
			this.rankIndexChanged = serverLeaderboardEntry.m_gradeChanged;
			this.mapRank = serverLeaderboardEntry.m_numRank;
			this.loginTime = NetUtil.GetLocalDateTime(serverLeaderboardEntry.m_loginTime);
			ServerItem serverItem = new ServerItem((ServerItem.Id)serverLeaderboardEntry.m_charaId);
			this.charaType = serverItem.charaType;
			ServerItem serverItem2 = new ServerItem((ServerItem.Id)serverLeaderboardEntry.m_subCharaId);
			this.charaSubType = serverItem2.charaType;
			this.charaLevel = serverLeaderboardEntry.m_charaLevel;
			ServerItem serverItem3 = new ServerItem((ServerItem.Id)serverLeaderboardEntry.m_mainChaoId);
			this.mainChaoId = serverItem3.chaoId;
			ServerItem serverItem4 = new ServerItem((ServerItem.Id)serverLeaderboardEntry.m_subChaoId);
			this.subChaoId = serverItem4.chaoId;
			this.mainChaoLevel = serverLeaderboardEntry.m_mainChaoLevel;
			this.subChaoLevel = serverLeaderboardEntry.m_subChaoLevel;
			this.leagueIndex = serverLeaderboardEntry.m_leagueIndex;
			this.language = serverLeaderboardEntry.m_language;
			this.userDataType = RankingUtil.UserDataType.RANKING;
			if (RankingUtil.Ranker.s_socialInterface == null)
			{
				RankingUtil.Ranker.s_socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
			}
			if (RankingUtil.Ranker.s_socialInterface != null)
			{
				this.isFriend = (SocialInterface.GetSocialUserDataFromGameId(RankingUtil.Ranker.s_socialInterface.FriendList, this.id) != null);
			}
			else
			{
				this.isFriend = (this.isSentEnergy || UnityEngine.Random.Range(0, 3) != 0);
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x000EA88C File Offset: 0x000E8A8C
		// (set) Token: 0x06002659 RID: 9817 RVA: 0x000EA894 File Offset: 0x000E8A94
		public int rankIndex { get; set; }

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600265A RID: 9818 RVA: 0x000EA8A0 File Offset: 0x000E8AA0
		// (set) Token: 0x0600265B RID: 9819 RVA: 0x000EA8A8 File Offset: 0x000E8AA8
		public int rankIndexChanged { get; set; }

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600265C RID: 9820 RVA: 0x000EA8B4 File Offset: 0x000E8AB4
		// (set) Token: 0x0600265D RID: 9821 RVA: 0x000EA8BC File Offset: 0x000E8ABC
		public long score { get; set; }

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x000EA8C8 File Offset: 0x000E8AC8
		// (set) Token: 0x0600265F RID: 9823 RVA: 0x000EA8D0 File Offset: 0x000E8AD0
		public long hiScore { get; set; }

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x000EA8DC File Offset: 0x000E8ADC
		// (set) Token: 0x06002661 RID: 9825 RVA: 0x000EA8E4 File Offset: 0x000E8AE4
		public int mapRank { private get; set; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x000EA8F0 File Offset: 0x000E8AF0
		public string dispMapRank
		{
			get
			{
				return (this.mapRank + 1).ToString("D3");
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06002663 RID: 9827 RVA: 0x000EA914 File Offset: 0x000E8B14
		// (set) Token: 0x06002664 RID: 9828 RVA: 0x000EA91C File Offset: 0x000E8B1C
		public string userName { get; set; }

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x000EA928 File Offset: 0x000E8B28
		// (set) Token: 0x06002666 RID: 9830 RVA: 0x000EA930 File Offset: 0x000E8B30
		public bool isFriend { get; set; }

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06002667 RID: 9831 RVA: 0x000EA93C File Offset: 0x000E8B3C
		// (set) Token: 0x06002668 RID: 9832 RVA: 0x000EA944 File Offset: 0x000E8B44
		public bool isSentEnergy { get; set; }

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06002669 RID: 9833 RVA: 0x000EA950 File Offset: 0x000E8B50
		// (set) Token: 0x0600266A RID: 9834 RVA: 0x000EA958 File Offset: 0x000E8B58
		public CharaType charaType { get; set; }

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x000EA964 File Offset: 0x000E8B64
		// (set) Token: 0x0600266C RID: 9836 RVA: 0x000EA96C File Offset: 0x000E8B6C
		public CharaType charaSubType { get; set; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600266D RID: 9837 RVA: 0x000EA978 File Offset: 0x000E8B78
		// (set) Token: 0x0600266E RID: 9838 RVA: 0x000EA980 File Offset: 0x000E8B80
		public int charaLevel { get; set; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600266F RID: 9839 RVA: 0x000EA98C File Offset: 0x000E8B8C
		// (set) Token: 0x06002670 RID: 9840 RVA: 0x000EA994 File Offset: 0x000E8B94
		public int mainChaoId { get; set; }

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06002671 RID: 9841 RVA: 0x000EA9A0 File Offset: 0x000E8BA0
		// (set) Token: 0x06002672 RID: 9842 RVA: 0x000EA9A8 File Offset: 0x000E8BA8
		public int subChaoId { get; set; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06002673 RID: 9843 RVA: 0x000EA9B4 File Offset: 0x000E8BB4
		// (set) Token: 0x06002674 RID: 9844 RVA: 0x000EA9BC File Offset: 0x000E8BBC
		public int mainChaoLevel { get; set; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06002675 RID: 9845 RVA: 0x000EA9C8 File Offset: 0x000E8BC8
		// (set) Token: 0x06002676 RID: 9846 RVA: 0x000EA9D0 File Offset: 0x000E8BD0
		public int subChaoLevel { get; set; }

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x000EA9DC File Offset: 0x000E8BDC
		// (set) Token: 0x06002678 RID: 9848 RVA: 0x000EA9E4 File Offset: 0x000E8BE4
		public Env.Language language { get; set; }

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x000EA9F0 File Offset: 0x000E8BF0
		// (set) Token: 0x0600267A RID: 9850 RVA: 0x000EA9F8 File Offset: 0x000E8BF8
		public int leagueIndex { get; set; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x000EAA04 File Offset: 0x000E8C04
		// (set) Token: 0x0600267C RID: 9852 RVA: 0x000EAA0C File Offset: 0x000E8C0C
		public string id { get; set; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600267D RID: 9853 RVA: 0x000EAA18 File Offset: 0x000E8C18
		// (set) Token: 0x0600267E RID: 9854 RVA: 0x000EAA20 File Offset: 0x000E8C20
		public DateTime loginTime { get; set; }

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x000EAA2C File Offset: 0x000E8C2C
		// (set) Token: 0x06002680 RID: 9856 RVA: 0x000EAA34 File Offset: 0x000E8C34
		public RankingUtil.UserDataType userDataType { get; set; }

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x000EAA40 File Offset: 0x000E8C40
		// (set) Token: 0x06002682 RID: 9858 RVA: 0x000EAA48 File Offset: 0x000E8C48
		public bool isBoxCollider
		{
			get
			{
				return this.m_isBoxCollider;
			}
			set
			{
				this.m_isBoxCollider = value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06002683 RID: 9859 RVA: 0x000EAA54 File Offset: 0x000E8C54
		public int rankIconIndex
		{
			get
			{
				return (this.rankIndex >= 1) ? ((this.rankIndex >= 10) ? ((this.rankIndex >= 50) ? ((this.rankIndex >= 150) ? -1 : 3) : 2) : 1) : 0;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06002684 RID: 9860 RVA: 0x000EAAB0 File Offset: 0x000E8CB0
		public int mainChaoRarity
		{
			get
			{
				return this.mainChaoId / 1000;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06002685 RID: 9861 RVA: 0x000EAAC0 File Offset: 0x000E8CC0
		public int subChaoRarity
		{
			get
			{
				return this.subChaoId / 1000;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x000EAAD0 File Offset: 0x000E8CD0
		public bool isMy
		{
			get
			{
				bool result = false;
				if (!string.IsNullOrEmpty(this.id))
				{
					string gameID = SystemSaveManager.GetGameID();
					if (!string.IsNullOrEmpty(gameID) && this.id == gameID)
					{
						result = true;
					}
				}
				return result;
			}
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x000EAB14 File Offset: 0x000E8D14
		public bool CheckRankerIdentity(RankingUtil.Ranker target)
		{
			bool result = false;
			if (this.score == target.score && this.userName == target.userName && this.id == target.id && this.rankIndex == target.rankIndex)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x040022B6 RID: 8886
		public static SocialInterface s_socialInterface;

		// Token: 0x040022B7 RID: 8887
		private bool m_isBoxCollider = true;
	}

	// Token: 0x02000507 RID: 1287
	public class RankingDataSet
	{
		// Token: 0x06002688 RID: 9864 RVA: 0x000EAB74 File Offset: 0x000E8D74
		public RankingDataSet(ServerWeeklyLeaderboardOptions leaderboardOptions)
		{
			this.Setup(leaderboardOptions);
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06002689 RID: 9865 RVA: 0x000EAB8C File Offset: 0x000E8D8C
		public RankingUtil.RankingMode rankingMode
		{
			get
			{
				return this.m_rankingMode;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x000EAB94 File Offset: 0x000E8D94
		public RankingUtil.RankingScoreType targetRivalScoreType
		{
			get
			{
				return this.m_rankingTargetRivalScoreType;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600268B RID: 9867 RVA: 0x000EAB9C File Offset: 0x000E8D9C
		public RankingUtil.RankingScoreType targetSpScoreType
		{
			get
			{
				return this.m_rankingTargetSpScoreType;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x000EABA4 File Offset: 0x000E8DA4
		public ServerWeeklyLeaderboardOptions weeklyLeaderboardOptions
		{
			get
			{
				return this.m_rankingWeeklyLeaderboardOptions;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x000EABAC File Offset: 0x000E8DAC
		public RankingDataContainer dataContainer
		{
			get
			{
				return this.m_rankingDataContainer;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x000EABB4 File Offset: 0x000E8DB4
		public ServerLeagueData leagueData
		{
			get
			{
				return this.m_leagueData;
			}
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000EABBC File Offset: 0x000E8DBC
		public void Setup(ServerWeeklyLeaderboardOptions leaderboardOptions)
		{
			int num = leaderboardOptions.mode;
			if (num < 0 || num >= 2)
			{
				num = 0;
			}
			this.m_rankingMode = (RankingUtil.RankingMode)num;
			this.m_rankingTargetRivalScoreType = leaderboardOptions.rankingScoreType;
			this.m_rankingTargetSpScoreType = RankingUtil.RankingScoreType.TOTAL_SCORE;
			this.m_rankingWeeklyLeaderboardOptions = new ServerWeeklyLeaderboardOptions();
			leaderboardOptions.CopyTo(this.m_rankingWeeklyLeaderboardOptions);
			this.m_rankingDataContainer = new RankingDataContainer();
			this.m_leagueData = null;
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x000EAC24 File Offset: 0x000E8E24
		public void SetLeagueData(ServerLeagueData data)
		{
			this.m_leagueData = new ServerLeagueData();
			data.CopyTo(this.m_leagueData);
			global::Debug.Log(string.Concat(new object[]
			{
				"RankingDataSet SetLeagueData mode:",
				this.m_leagueData.rankinMode,
				" leagueId:",
				this.m_leagueData.leagueId,
				"  groupId:",
				this.m_leagueData.groupId,
				" !!!"
			}));
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000EACB4 File Offset: 0x000E8EB4
		public void Reset(RankingUtil.RankingRankerType type)
		{
			if (this.m_rankingDataContainer != null)
			{
				this.m_rankingDataContainer.Reset(type);
			}
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x000EACD0 File Offset: 0x000E8ED0
		public void Reset()
		{
			if (this.m_rankingDataContainer != null)
			{
				this.m_rankingDataContainer.Reset();
			}
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x000EACE8 File Offset: 0x000E8EE8
		public void SaveRanking()
		{
			if (this.m_rankingDataContainer != null)
			{
				this.m_rankingDataContainer.SavePlayerRanking();
			}
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000EAD00 File Offset: 0x000E8F00
		public bool UpdateSendChallengeList(RankingUtil.RankingRankerType type, string id)
		{
			bool result = false;
			if (this.m_rankingDataContainer != null)
			{
				result = this.m_rankingDataContainer.UpdateSendChallengeList(type, id);
			}
			return result;
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000EAD2C File Offset: 0x000E8F2C
		public RankingUtil.RankChange GetRankChange(RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType)
		{
			RankingUtil.RankChange result = RankingUtil.RankChange.NONE;
			if (this.m_rankingDataContainer != null)
			{
				result = this.m_rankingDataContainer.GetRankChange(scoreType, rankerType);
			}
			return result;
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000EAD58 File Offset: 0x000E8F58
		public RankingUtil.RankChange GetRankChange(RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType, out int currentRank, out int oldRank)
		{
			RankingUtil.RankChange result = RankingUtil.RankChange.NONE;
			currentRank = -1;
			oldRank = -1;
			if (this.m_rankingDataContainer != null)
			{
				result = this.m_rankingDataContainer.GetRankChange(scoreType, rankerType, out currentRank, out oldRank);
			}
			return result;
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x000EAD8C File Offset: 0x000E8F8C
		public void ResetRankChange(RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType)
		{
			if (this.m_rankingDataContainer != null)
			{
				this.m_rankingDataContainer.ResetRankChange(scoreType, rankerType);
			}
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x000EADA8 File Offset: 0x000E8FA8
		public void AddRankerList(MsgGetLeaderboardEntriesSucceed msg)
		{
			if (this.m_rankingDataContainer != null)
			{
				this.m_rankingDataContainer.AddRankerList(msg);
			}
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000EADC4 File Offset: 0x000E8FC4
		public List<RankingUtil.Ranker> GetRankerList(RankingUtil.RankingRankerType rankerType, RankingUtil.RankingScoreType scoreType, int page)
		{
			List<RankingUtil.Ranker> result = null;
			if (this.m_rankingDataContainer != null)
			{
				result = this.m_rankingDataContainer.GetRankerList(rankerType, scoreType, page);
			}
			return result;
		}

		// Token: 0x040022CC RID: 8908
		private RankingUtil.RankingMode m_rankingMode;

		// Token: 0x040022CD RID: 8909
		private RankingUtil.RankingScoreType m_rankingTargetRivalScoreType;

		// Token: 0x040022CE RID: 8910
		private RankingUtil.RankingScoreType m_rankingTargetSpScoreType = RankingUtil.RankingScoreType.TOTAL_SCORE;

		// Token: 0x040022CF RID: 8911
		private ServerWeeklyLeaderboardOptions m_rankingWeeklyLeaderboardOptions;

		// Token: 0x040022D0 RID: 8912
		private RankingDataContainer m_rankingDataContainer;

		// Token: 0x040022D1 RID: 8913
		private ServerLeagueData m_leagueData;
	}
}
