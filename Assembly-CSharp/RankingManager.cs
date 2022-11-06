using System;
using System.Collections.Generic;
using DataTable;
using Message;
using UnityEngine;

// Token: 0x02000A4C RID: 2636
public class RankingManager : SingletonGameObject<RankingManager>
{
	// Token: 0x1700097B RID: 2427
	// (get) Token: 0x0600466C RID: 18028 RVA: 0x0016FE74 File Offset: 0x0016E074
	public int eventId
	{
		get
		{
			return this.m_eventId;
		}
	}

	// Token: 0x1700097C RID: 2428
	// (get) Token: 0x0600466D RID: 18029 RVA: 0x0016FE7C File Offset: 0x0016E07C
	public static RankingUtil.RankingScoreType EndlessRivalRankingScoreType
	{
		get
		{
			RankingUtil.RankingScoreType result = RankingUtil.RankingScoreType.HIGH_SCORE;
			if (SingletonGameObject<RankingManager>.Instance != null)
			{
				RankingUtil.RankingDataSet rankingDataSet = SingletonGameObject<RankingManager>.Instance.GetRankingDataSet(RankingUtil.RankingMode.ENDLESS);
				if (rankingDataSet != null)
				{
					result = rankingDataSet.targetRivalScoreType;
				}
			}
			return result;
		}
	}

	// Token: 0x1700097D RID: 2429
	// (get) Token: 0x0600466E RID: 18030 RVA: 0x0016FEB8 File Offset: 0x0016E0B8
	public static RankingUtil.RankingScoreType QuickRivalRankingScoreType
	{
		get
		{
			RankingUtil.RankingScoreType result = RankingUtil.RankingScoreType.HIGH_SCORE;
			if (SingletonGameObject<RankingManager>.Instance != null)
			{
				RankingUtil.RankingDataSet rankingDataSet = SingletonGameObject<RankingManager>.Instance.GetRankingDataSet(RankingUtil.RankingMode.QUICK);
				if (rankingDataSet != null)
				{
					result = rankingDataSet.targetRivalScoreType;
				}
			}
			return result;
		}
	}

	// Token: 0x1700097E RID: 2430
	// (get) Token: 0x0600466F RID: 18031 RVA: 0x0016FEF4 File Offset: 0x0016E0F4
	public static RankingUtil.RankingScoreType EndlessSpecialRankingScoreType
	{
		get
		{
			RankingUtil.RankingScoreType result = RankingUtil.RankingScoreType.TOTAL_SCORE;
			if (SingletonGameObject<RankingManager>.Instance != null)
			{
				RankingUtil.RankingDataSet rankingDataSet = SingletonGameObject<RankingManager>.Instance.GetRankingDataSet(RankingUtil.RankingMode.ENDLESS);
				if (rankingDataSet != null)
				{
					result = rankingDataSet.targetSpScoreType;
				}
			}
			return result;
		}
	}

	// Token: 0x1700097F RID: 2431
	// (get) Token: 0x06004670 RID: 18032 RVA: 0x0016FF30 File Offset: 0x0016E130
	public RankingUtil.RankingMode mode
	{
		get
		{
			return this.m_mode;
		}
	}

	// Token: 0x17000980 RID: 2432
	// (get) Token: 0x06004671 RID: 18033 RVA: 0x0016FF38 File Offset: 0x0016E138
	public RankingUtil.RankingScoreType scoreType
	{
		get
		{
			return this.m_scoreType;
		}
	}

	// Token: 0x17000981 RID: 2433
	// (get) Token: 0x06004672 RID: 18034 RVA: 0x0016FF40 File Offset: 0x0016E140
	public RankingUtil.RankingRankerType rankerType
	{
		get
		{
			return this.m_rankerType;
		}
	}

	// Token: 0x17000982 RID: 2434
	// (get) Token: 0x06004673 RID: 18035 RVA: 0x0016FF48 File Offset: 0x0016E148
	public bool isSpRankingInit
	{
		get
		{
			return this.m_isSpRankingInit;
		}
	}

	// Token: 0x17000983 RID: 2435
	// (get) Token: 0x06004674 RID: 18036 RVA: 0x0016FF50 File Offset: 0x0016E150
	public bool isLoading
	{
		get
		{
			if (this.m_isLoading)
			{
				float num = Mathf.Abs(this.m_getRankingLastTime - Time.realtimeSinceStartup);
				return num > 0.15f;
			}
			return false;
		}
	}

	// Token: 0x17000984 RID: 2436
	// (get) Token: 0x06004675 RID: 18037 RVA: 0x0016FF84 File Offset: 0x0016E184
	public bool isChaoTextureLoading
	{
		get
		{
			return this.m_chaoTextureObject != null && this.m_chaoTextureObject.Count > 0;
		}
	}

	// Token: 0x17000985 RID: 2437
	// (get) Token: 0x06004676 RID: 18038 RVA: 0x0016FFA8 File Offset: 0x0016E1A8
	public bool isRankingInit
	{
		get
		{
			return this.m_isRankingInit;
		}
	}

	// Token: 0x17000986 RID: 2438
	// (get) Token: 0x06004677 RID: 18039 RVA: 0x0016FFB0 File Offset: 0x0016E1B0
	// (set) Token: 0x06004678 RID: 18040 RVA: 0x0016FFB8 File Offset: 0x0016E1B8
	public bool isRankingPageCheck
	{
		get
		{
			return this.m_isRankingPageCheck;
		}
		set
		{
			this.m_isRankingPageCheck = value;
		}
	}

	// Token: 0x17000987 RID: 2439
	// (get) Token: 0x06004679 RID: 18041 RVA: 0x0016FFC4 File Offset: 0x0016E1C4
	public bool isReset
	{
		get
		{
			return this.m_isReset;
		}
	}

	// Token: 0x0600467A RID: 18042 RVA: 0x0016FFCC File Offset: 0x0016E1CC
	public void SetRankingDataSet(ServerWeeklyLeaderboardOptions leaderboardOptions)
	{
		if (this.m_rankingDataSet == null)
		{
			this.m_rankingDataSet = new Dictionary<RankingUtil.RankingMode, RankingUtil.RankingDataSet>();
		}
		int num = leaderboardOptions.mode;
		if (num < 0 || num >= 2)
		{
			num = 0;
		}
		if (this.m_rankingDataSet.ContainsKey((RankingUtil.RankingMode)num))
		{
			this.m_rankingDataSet[(RankingUtil.RankingMode)num].Setup(leaderboardOptions);
		}
		else
		{
			RankingUtil.RankingDataSet value = new RankingUtil.RankingDataSet(leaderboardOptions);
			this.m_rankingDataSet.Add((RankingUtil.RankingMode)num, value);
		}
	}

	// Token: 0x0600467B RID: 18043 RVA: 0x00170044 File Offset: 0x0016E244
	public RankingUtil.RankingDataSet GetRankingDataSet(RankingUtil.RankingMode rankingMode)
	{
		RankingUtil.RankingDataSet result = null;
		if (this.m_rankingDataSet != null && this.m_rankingDataSet.ContainsKey(rankingMode))
		{
			result = this.m_rankingDataSet[rankingMode];
		}
		return result;
	}

	// Token: 0x0600467C RID: 18044 RVA: 0x00170080 File Offset: 0x0016E280
	private RankingDataContainer GetRankingDataContainer(RankingUtil.RankingMode rankingMode)
	{
		RankingDataContainer result = null;
		if (this.m_rankingDataSet != null && this.m_rankingDataSet.ContainsKey(rankingMode))
		{
			result = this.m_rankingDataSet[rankingMode].dataContainer;
		}
		return result;
	}

	// Token: 0x0600467D RID: 18045 RVA: 0x001700C0 File Offset: 0x0016E2C0
	public bool Init(RankingManager.CallbackRankingData callbackNormalAll, RankingManager.CallbackRankingData callbackEventAll = null)
	{
		global::Debug.Log("! RankingManager:Init isLoading:" + this.isLoading);
		this.m_initLoadCount = 0;
		if (this.isLoading)
		{
			return false;
		}
		this.m_callbackBakNormalAll = null;
		this.m_callbackBakEventAll = null;
		if (this.m_rankingDataSet != null && this.m_rankingDataSet.Count >= 2)
		{
			return this.RankingInit(callbackNormalAll, callbackEventAll);
		}
		this.m_callbackBakNormalAll = callbackNormalAll;
		this.m_callbackBakEventAll = callbackEventAll;
		ServerInterface.LoggedInServerInterface.RequestServerGetWeeklyLeaderboardOptions(0, base.gameObject);
		return true;
	}

	// Token: 0x0600467E RID: 18046 RVA: 0x00170150 File Offset: 0x0016E350
	private void ServerGetWeeklyLeaderboardOptions_Succeeded(MsgGetWeeklyLeaderboardOptions msg)
	{
		global::Debug.Log("RankingManager: ServerGetWeeklyLeaderboardOptions_Succeeded  mode:" + msg.m_weeklyLeaderboardOptions.mode);
		this.m_initLoadCount++;
		if (this.m_rankingDataSet != null && this.m_rankingDataSet.Count >= 2)
		{
			this.m_initLoadCount = 0;
			ServerInterface.LoggedInServerInterface.RequestServerGetLeagueData(0, base.gameObject);
		}
		else if (this.m_initLoadCount > 2)
		{
			global::Debug.Log("RankingManager: ServerGetWeeklyLeaderboardOptions_Succeeded error !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			ServerWeeklyLeaderboardOptions serverWeeklyLeaderboardOptions = new ServerWeeklyLeaderboardOptions();
			msg.m_weeklyLeaderboardOptions.CopyTo(serverWeeklyLeaderboardOptions);
			serverWeeklyLeaderboardOptions.mode = 1;
			this.SetRankingDataSet(serverWeeklyLeaderboardOptions);
			this.m_initLoadCount = 0;
			ServerInterface.LoggedInServerInterface.RequestServerGetLeagueData(0, base.gameObject);
		}
		else
		{
			ServerInterface.LoggedInServerInterface.RequestServerGetWeeklyLeaderboardOptions(msg.m_weeklyLeaderboardOptions.mode + 1, base.gameObject);
		}
	}

	// Token: 0x0600467F RID: 18047 RVA: 0x00170234 File Offset: 0x0016E434
	private void ServerGetLeagueData_Succeeded(MsgGetLeagueDataSucceed msg)
	{
		global::Debug.Log("RankingManager: ServerGetLeagueData_Succeeded count:" + this.m_initLoadCount);
		this.m_initLoadCount++;
		int nextLeagueDataMode = this.GetNextLeagueDataMode();
		if (nextLeagueDataMode < 0)
		{
			RankingLeagueTable.SetupRankingLeagueTable();
			this.RankingInit(null, this.m_callbackBakEventAll);
		}
		else if (this.m_initLoadCount > 2)
		{
			global::Debug.Log("RankingManager: ServerGetLeagueData_Succeeded error !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			if (this.m_rankingDataSet != null && this.m_rankingDataSet.Count > 0)
			{
				Dictionary<RankingUtil.RankingMode, RankingUtil.RankingDataSet>.KeyCollection keys = this.m_rankingDataSet.Keys;
				foreach (RankingUtil.RankingMode key in keys)
				{
					if (this.m_rankingDataSet[key].leagueData == null)
					{
						this.m_rankingDataSet[key].SetLeagueData(this.m_rankingDataSet[RankingUtil.RankingMode.ENDLESS].leagueData);
					}
				}
			}
			RankingLeagueTable.SetupRankingLeagueTable();
			this.RankingInit(null, this.m_callbackBakEventAll);
		}
		else
		{
			ServerInterface.LoggedInServerInterface.RequestServerGetLeagueData(nextLeagueDataMode, base.gameObject);
		}
	}

	// Token: 0x06004680 RID: 18048 RVA: 0x0017037C File Offset: 0x0016E57C
	private bool RankingInit(RankingManager.CallbackRankingData callbackNormalAll, RankingManager.CallbackRankingData callbackEventAll)
	{
		global::Debug.Log("! RankingManager:RankingInit isLoading:" + this.isLoading);
		if (this.isLoading)
		{
			return false;
		}
		this.m_isRankingPageCheck = false;
		this.m_page = -1;
		this.m_eventId = 0;
		this.m_getRankingLastTime = 0f;
		EventManager.EventType type = EventManager.Instance.Type;
		this.m_isRankingInit = true;
		this.ResetRankingData(RankingUtil.RankingMode.ENDLESS);
		this.ResetRankingData(RankingUtil.RankingMode.QUICK);
		this.m_isSpRankingInit = false;
		RankingUtil.RankingMode rankingMode = RankingUtil.RankingMode.ENDLESS;
		RankingUtil.RankingScoreType scoreType = RankingUtil.RankingScoreType.HIGH_SCORE;
		this.m_chainGetRankingCodeList = new List<int>();
		if (this.m_rankingDataSet != null && this.m_rankingDataSet.Count > 0)
		{
			int num = 0;
			Dictionary<RankingUtil.RankingMode, RankingUtil.RankingDataSet>.KeyCollection keys = this.m_rankingDataSet.Keys;
			foreach (RankingUtil.RankingMode rankingMode2 in keys)
			{
				if (num == 0)
				{
					rankingMode = rankingMode2;
					scoreType = this.m_rankingDataSet[rankingMode2].targetRivalScoreType;
				}
				int rankingCode = RankingUtil.GetRankingCode(rankingMode2, this.m_rankingDataSet[rankingMode2].targetRivalScoreType, RankingUtil.RankingRankerType.RIVAL);
				if (rankingCode >= 0)
				{
					this.m_chainGetRankingCodeList.Add(rankingCode);
				}
				num++;
			}
		}
		if (callbackNormalAll == null)
		{
			callbackNormalAll = new RankingManager.CallbackRankingData(this.DefaultCallback);
		}
		this.GetRanking(rankingMode, scoreType, RankingUtil.RankingRankerType.RIVAL, 0, callbackNormalAll);
		if (type == EventManager.EventType.SPECIAL_STAGE)
		{
			if (callbackEventAll == null)
			{
				callbackEventAll = new RankingManager.CallbackRankingData(this.EventRankingInitCallback);
			}
			this.GetRanking(RankingUtil.RankingMode.ENDLESS, RankingManager.EndlessSpecialRankingScoreType, RankingUtil.RankingRankerType.SP_ALL, 0, callbackEventAll);
		}
		else if (callbackEventAll != null)
		{
			List<RankingUtil.Ranker> rankerList = new List<RankingUtil.Ranker>();
			callbackEventAll(rankerList, RankingManager.EndlessSpecialRankingScoreType, RankingUtil.RankingRankerType.SP_ALL, 0, false, false, true);
		}
		return true;
	}

	// Token: 0x06004681 RID: 18049 RVA: 0x00170544 File Offset: 0x0016E744
	public bool InitNormal(RankingUtil.RankingMode rankingMode, RankingManager.CallbackRankingData callback)
	{
		RankingUtil.RankingScoreType scoreType;
		if (rankingMode == RankingUtil.RankingMode.ENDLESS)
		{
			scoreType = RankingManager.EndlessRivalRankingScoreType;
		}
		else
		{
			scoreType = RankingManager.QuickRivalRankingScoreType;
		}
		global::Debug.Log("! RankingManager:InitNormal isLoading:" + this.isLoading);
		if (this.isLoading)
		{
			return false;
		}
		this.m_isRankingPageCheck = false;
		this.m_page = -1;
		this.m_eventId = 0;
		this.m_getRankingLastTime = 0f;
		this.m_isRankingInit = true;
		this.ResetRankingData(RankingUtil.RankingMode.ENDLESS);
		this.m_isSpRankingInit = false;
		if (callback == null)
		{
			callback = new RankingManager.CallbackRankingData(this.DefaultCallback);
		}
		this.GetRanking(rankingMode, scoreType, RankingUtil.RankingRankerType.RIVAL, 0, callback);
		return true;
	}

	// Token: 0x06004682 RID: 18050 RVA: 0x001705E8 File Offset: 0x0016E7E8
	public bool InitSp(RankingManager.CallbackRankingData callback)
	{
		global::Debug.Log("! RankingManager:InitSp isLoading:" + this.isLoading);
		if (this.isLoading)
		{
			return false;
		}
		this.m_page = -1;
		this.m_eventId = 0;
		this.m_getRankingLastTime = 0f;
		EventManager.EventType type = EventManager.Instance.Type;
		this.m_isRankingInit = true;
		this.ResetRankingData(RankingUtil.RankingMode.ENDLESS);
		this.m_isSpRankingInit = false;
		if (type == EventManager.EventType.SPECIAL_STAGE)
		{
			if (callback == null)
			{
				callback = new RankingManager.CallbackRankingData(this.EventRankingInitCallback);
			}
			this.GetRanking(RankingUtil.RankingMode.ENDLESS, RankingManager.EndlessSpecialRankingScoreType, RankingUtil.RankingRankerType.SP_ALL, 0, callback);
		}
		else if (callback != null)
		{
			callback(null, RankingManager.EndlessSpecialRankingScoreType, RankingUtil.RankingRankerType.SP_ALL, 0, false, false, true);
		}
		return true;
	}

	// Token: 0x06004683 RID: 18051 RVA: 0x0017069C File Offset: 0x0016E89C
	private void Update()
	{
		if (this.m_chaoTextureLoadTime >= 0f)
		{
			float deltaTime = Time.deltaTime;
			if (this.m_chaoTextureObject != null && this.m_chaoTextureObject.Count > 0)
			{
				if (this.m_chaoTextureLoad != null && this.m_chaoTextureLoad.Count > 0)
				{
					int[] array = new int[this.m_chaoTextureLoad.Count];
					this.m_chaoTextureLoad.Keys.CopyTo(array, 0);
					List<int> list = null;
					foreach (int num in array)
					{
						Dictionary<int, float> chaoTextureLoad;
						Dictionary<int, float> dictionary = chaoTextureLoad = this.m_chaoTextureLoad;
						int key2;
						int key = key2 = num;
						float num2 = chaoTextureLoad[key2];
						dictionary[key] = num2 - deltaTime;
						if (this.m_chaoTextureLoad[num] <= 0f && this.m_chaoTextureObject.ContainsKey(num) && this.m_chaoTextureList != null && this.m_chaoTextureList.ContainsKey(num))
						{
							List<UITexture> list2 = this.m_chaoTextureObject[num];
							if (list2 != null && list2.Count > 0)
							{
								foreach (UITexture uitexture in list2)
								{
									uitexture.mainTexture = this.m_chaoTextureList[num];
									uitexture.alpha = 1f;
								}
							}
							if (list == null)
							{
								list = new List<int>();
							}
							list.Add(num);
						}
					}
					if (list != null)
					{
						foreach (int key3 in list)
						{
							this.m_chaoTextureLoad.Remove(key3);
						}
					}
				}
			}
			else
			{
				this.m_chaoTextureLoadEndTime += deltaTime;
			}
			this.m_chaoTextureLoadTime += deltaTime;
			if (this.m_chaoTextureLoadEndTime > 5f && this.m_chaoTextureLoad != null && this.m_chaoTextureLoad.Count <= 0)
			{
				this.m_chaoTextureLoadTime = -1f;
			}
		}
	}

	// Token: 0x06004684 RID: 18052 RVA: 0x00170900 File Offset: 0x0016EB00
	public ServerLeagueData GetLeagueData(RankingUtil.RankingMode rankingMode)
	{
		ServerLeagueData result = null;
		if (this.m_rankingDataSet != null && this.m_rankingDataSet.Count > 0 && this.m_rankingDataSet.ContainsKey(rankingMode))
		{
			result = this.m_rankingDataSet[rankingMode].leagueData;
		}
		return result;
	}

	// Token: 0x06004685 RID: 18053 RVA: 0x00170950 File Offset: 0x0016EB50
	public bool SetLeagueData(ServerLeagueData data)
	{
		bool result = false;
		if (this.m_rankingDataSet != null && this.m_rankingDataSet.Count > 0)
		{
			Dictionary<RankingUtil.RankingMode, RankingUtil.RankingDataSet>.KeyCollection keys = this.m_rankingDataSet.Keys;
			foreach (RankingUtil.RankingMode rankingMode in keys)
			{
				if (rankingMode == data.rankinMode)
				{
					this.m_rankingDataSet[rankingMode].SetLeagueData(data);
					result = true;
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06004686 RID: 18054 RVA: 0x001709FC File Offset: 0x0016EBFC
	public int GetNextLeagueDataMode()
	{
		int result = -1;
		if (this.m_rankingDataSet != null && this.m_rankingDataSet.Count > 0)
		{
			Dictionary<RankingUtil.RankingMode, RankingUtil.RankingDataSet>.KeyCollection keys = this.m_rankingDataSet.Keys;
			foreach (RankingUtil.RankingMode rankingMode in keys)
			{
				if (this.m_rankingDataSet[rankingMode].leagueData == null)
				{
					result = (int)rankingMode;
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06004687 RID: 18055 RVA: 0x00170AA0 File Offset: 0x0016ECA0
	public void Reset(RankingUtil.RankingMode mode, RankingUtil.RankingRankerType type)
	{
		if (this.m_rankingDataSet != null && this.m_rankingDataSet.ContainsKey(mode))
		{
			this.m_rankingDataSet[mode].Reset(type);
		}
	}

	// Token: 0x06004688 RID: 18056 RVA: 0x00170ADC File Offset: 0x0016ECDC
	private void ResetRankingData(RankingUtil.RankingMode mode)
	{
		if (this.m_rankingDataSet != null)
		{
			if (this.m_rankingDataSet.ContainsKey(mode))
			{
				this.m_rankingDataSet[mode].Reset();
			}
		}
		else
		{
			this.m_rankingDataSet = new Dictionary<RankingUtil.RankingMode, RankingUtil.RankingDataSet>();
		}
		RankingUI.SetLoading();
		if (EventManager.Instance.Type == EventManager.EventType.SPECIAL_STAGE)
		{
			SpecialStageWindow.SetLoading();
		}
		this.m_callbacks = null;
		this.m_callbacks = new List<RankingManager.CallbackData>();
		this.m_isReset = true;
		this.ResetChaoTexture();
	}

	// Token: 0x06004689 RID: 18057 RVA: 0x00170B60 File Offset: 0x0016ED60
	public RankingUtil.RankChange GetRankingRankChange(RankingUtil.RankingMode rankingMode, RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType)
	{
		RankingUtil.RankChange result = RankingUtil.RankChange.NONE;
		RankingDataContainer rankingDataContainer = this.GetRankingDataContainer(rankingMode);
		if (rankingDataContainer != null)
		{
			result = rankingDataContainer.GetRankChange(scoreType, rankerType);
		}
		return result;
	}

	// Token: 0x0600468A RID: 18058 RVA: 0x00170B88 File Offset: 0x0016ED88
	public RankingUtil.RankChange GetRankingRankChange(RankingUtil.RankingMode rankingMode, RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType, out int currentRank, out int oldRank)
	{
		RankingUtil.RankChange result = RankingUtil.RankChange.NONE;
		currentRank = -1;
		oldRank = -1;
		RankingDataContainer rankingDataContainer = this.GetRankingDataContainer(rankingMode);
		if (rankingDataContainer != null)
		{
			result = rankingDataContainer.GetRankChange(scoreType, rankerType, out currentRank, out oldRank);
		}
		return result;
	}

	// Token: 0x0600468B RID: 18059 RVA: 0x00170BBC File Offset: 0x0016EDBC
	public RankingUtil.RankChange GetRankingRankChange(RankingUtil.RankingMode rankingMode)
	{
		RankingUtil.RankingDataSet rankingDataSet = this.GetRankingDataSet(rankingMode);
		RankingUtil.RankingRankerType rankerType = RankingUtil.RankingRankerType.RIVAL;
		RankingUtil.RankingScoreType targetRivalScoreType = rankingDataSet.targetRivalScoreType;
		return this.GetRankingRankChange(rankingMode, targetRivalScoreType, rankerType);
	}

	// Token: 0x0600468C RID: 18060 RVA: 0x00170BE4 File Offset: 0x0016EDE4
	public RankingUtil.RankChange GetRankingRankChange(RankingUtil.RankingMode rankingMode, out int currentRank, out int oldRank)
	{
		RankingUtil.RankingDataSet rankingDataSet = this.GetRankingDataSet(rankingMode);
		RankingUtil.RankingRankerType rankerType = RankingUtil.RankingRankerType.RIVAL;
		RankingUtil.RankingScoreType targetRivalScoreType = rankingDataSet.targetRivalScoreType;
		return this.GetRankingRankChange(rankingMode, targetRivalScoreType, rankerType, out currentRank, out oldRank);
	}

	// Token: 0x0600468D RID: 18061 RVA: 0x00170C10 File Offset: 0x0016EE10
	public void ResetRankingRankChange(RankingUtil.RankingMode rankingMode)
	{
		RankingUtil.RankingDataSet rankingDataSet = this.GetRankingDataSet(rankingMode);
		if (rankingDataSet != null)
		{
			RankingUtil.RankingRankerType rankerType = RankingUtil.RankingRankerType.RIVAL;
			RankingUtil.RankingScoreType targetRivalScoreType = rankingDataSet.targetRivalScoreType;
			rankingDataSet.ResetRankChange(targetRivalScoreType, rankerType);
		}
	}

	// Token: 0x0600468E RID: 18062 RVA: 0x00170C3C File Offset: 0x0016EE3C
	public bool GetRanking(RankingUtil.RankingMode rankingMode, RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType, int page, RankingManager.CallbackRankingData callback)
	{
		if (!this.m_isRankingInit)
		{
			return false;
		}
		RankingDataContainer rankingDataContainer = this.GetRankingDataContainer(rankingMode);
		this.m_isReset = false;
		if (rankerType == RankingUtil.RankingRankerType.RIVAL)
		{
			page = 0;
		}
		bool flag = false;
		if (this.isLoading && this.m_scoreType == scoreType && this.m_rankerType == rankerType && (this.m_page == page || page == 0) && this.m_callbacks != null)
		{
			if (this.m_callbacks.Count > 0)
			{
				flag = true;
				foreach (RankingManager.CallbackData callbackData in this.m_callbacks)
				{
					if (callbackData.rankingType == RankingUtil.GetRankingType(scoreType, rankerType) && (callbackData.getPage == page || page == 0))
					{
						flag = false;
						callbackData.getPage = page;
						callbackData.callback = callback;
						return false;
					}
				}
			}
			else
			{
				flag = true;
			}
			if (flag && callback != null)
			{
				List<RankingUtil.Ranker> list = null;
				if (page > 1)
				{
					page--;
				}
				if (this.IsRankingList(rankingMode, scoreType, rankerType, page))
				{
					list = this.GetRankerList(rankingMode, scoreType, rankerType, page);
				}
				if (list != null && rankingDataContainer != null)
				{
					MsgGetLeaderboardEntriesSucceed rankerListOrg = rankingDataContainer.GetRankerListOrg(rankerType, scoreType, page);
					callback(list, scoreType, rankerType, page, rankerListOrg.m_leaderboardEntries.IsNext(), rankerListOrg.m_leaderboardEntries.IsPrev(), true);
					return true;
				}
				return false;
			}
		}
		if (page < 0)
		{
			return false;
		}
		bool flag2 = this.IsRankingList(rankingMode, scoreType, rankerType, page);
		int rankingType = RankingUtil.GetRankingType(scoreType, rankerType);
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		bool flag3 = true;
		MsgGetLeaderboardEntriesSucceed msgGetLeaderboardEntriesSucceed = null;
		if (flag2)
		{
			msgGetLeaderboardEntriesSucceed = rankingDataContainer.GetRankerListOrg(rankerType, scoreType, page);
			if (msgGetLeaderboardEntriesSucceed == null)
			{
				flag3 = true;
			}
			else if (page == 0)
			{
				flag3 = rankingDataContainer.IsRankerListReload(rankerType, scoreType);
			}
			else if (page > 0)
			{
				flag3 = true;
			}
		}
		if (page >= 2)
		{
			flag3 = true;
		}
		if (flag3)
		{
			this.m_isLoading = true;
			this.m_scoreType = scoreType;
			this.m_rankerType = rankerType;
			this.m_page = page;
			this.m_eventId = EventManager.Instance.Id;
			if (callback != null && this.m_callbacks != null)
			{
				RankingManager.CallbackData item = new RankingManager.CallbackData(callback, rankingType, page);
				this.m_callbacks.Add(item);
				if (this.m_callbacks.Count > 256)
				{
					this.m_callbacks.RemoveAt(0);
				}
			}
			int rankingTop = this.GetRankingTop(rankingMode, rankerType, scoreType, page);
			int rankingSize = RankingManager.GetRankingSize(rankerType, rankingTop, page);
			string[] friendIdList = RankingUtil.GetFriendIdList();
			if (loggedInServerInterface != null)
			{
				loggedInServerInterface.RequestServerGetLeaderboardEntries((int)rankingMode, rankingTop, rankingSize, page, rankingType, this.m_eventId, friendIdList, base.gameObject);
			}
		}
		else if (flag2 && callback != null)
		{
			List<RankingUtil.Ranker> rankerList = this.GetRankerList(rankingMode, scoreType, rankerType, page);
			if (rankerList != null)
			{
				callback(rankerList, scoreType, rankerType, page, msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.IsNext(), msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.IsPrev(), true);
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600468F RID: 18063 RVA: 0x00170F78 File Offset: 0x0016F178
	public bool GetRankingScroll(RankingUtil.RankingMode rankingMode, bool isNext, RankingManager.CallbackRankingData callback)
	{
		if (!this.m_isRankingInit)
		{
			return false;
		}
		RankingDataContainer rankingDataContainer = this.GetRankingDataContainer(rankingMode);
		this.m_isReset = false;
		bool result = false;
		if (!this.isLoading && this.m_page > 0)
		{
			RankingUtil.RankingScoreType scoreType = this.m_scoreType;
			RankingUtil.RankingRankerType rankerType = this.m_rankerType;
			if (rankingDataContainer != null)
			{
				bool flag = false;
				int num = 1;
				int num2 = 70;
				int rankingType = RankingUtil.GetRankingType(scoreType, rankerType);
				Dictionary<RankingUtil.RankingScoreType, List<MsgGetLeaderboardEntriesSucceed>> dictionary;
				rankingDataContainer.IsRankerType(rankerType, out dictionary);
				if (dictionary != null && dictionary.Count > 0 && dictionary.ContainsKey(scoreType) && dictionary[scoreType].Count > 1)
				{
					MsgGetLeaderboardEntriesSucceed msgGetLeaderboardEntriesSucceed = dictionary[scoreType][1];
					if (msgGetLeaderboardEntriesSucceed != null && msgGetLeaderboardEntriesSucceed.m_leaderboardEntries != null)
					{
						if (isNext)
						{
							flag = msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.IsNext();
							msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.GetNextRanking(ref num, ref num2, 20);
						}
						else
						{
							flag = msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.IsPrev();
							msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.GetPrevRanking(ref num, ref num2, 20);
						}
						if (flag)
						{
							if (msgGetLeaderboardEntriesSucceed.m_leaderboardEntries.m_count > 30000)
							{
								return false;
							}
							if (num + num2 > 30000)
							{
								int num3 = num + num2 - 30000;
								num2 = num2 - num3 + 2;
							}
						}
					}
				}
				if (flag)
				{
					if (callback != null)
					{
						RankingManager.CallbackData item = new RankingManager.CallbackData(callback, rankingType, 2);
						this.m_callbacks.Add(item);
						if (this.m_callbacks.Count > 256)
						{
							this.m_callbacks.RemoveAt(0);
						}
					}
					result = true;
					this.m_isLoading = true;
					ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
					string[] friendIdList = RankingUtil.GetFriendIdList();
					if (loggedInServerInterface != null)
					{
						global::Debug.Log(string.Concat(new object[]
						{
							"RankingManager:RequestServerGetLeaderboardEntries   rankTop:",
							num,
							"  rankSize:",
							num2,
							"  type:",
							rankingType,
							" eventId:",
							this.m_eventId
						}));
						loggedInServerInterface.RequestServerGetLeaderboardEntries((int)rankingMode, num, num2, 2, rankingType, this.m_eventId, friendIdList, base.gameObject);
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06004690 RID: 18064 RVA: 0x001711B8 File Offset: 0x0016F3B8
	public static void SavePlayerRankingData(RankingUtil.RankingMode rankingMode)
	{
		if (SingletonGameObject<RankingManager>.Instance != null)
		{
			SingletonGameObject<RankingManager>.Instance.SavePlayerRankingDataOrg(rankingMode);
		}
	}

	// Token: 0x06004691 RID: 18065 RVA: 0x001711D8 File Offset: 0x0016F3D8
	private void SavePlayerRankingDataOrg(RankingUtil.RankingMode rankingMode)
	{
		if (this.m_rankingDataSet != null)
		{
			Dictionary<RankingUtil.RankingMode, RankingUtil.RankingDataSet>.KeyCollection keys = this.m_rankingDataSet.Keys;
			foreach (RankingUtil.RankingMode rankingMode2 in keys)
			{
				if (rankingMode2 == rankingMode)
				{
					this.m_rankingDataSet[rankingMode2].SaveRanking();
				}
				else
				{
					this.m_rankingDataSet[rankingMode2].Reset();
				}
			}
		}
	}

	// Token: 0x06004692 RID: 18066 RVA: 0x00171278 File Offset: 0x0016F478
	public void SavePlayerRankingDataDummy(RankingUtil.RankingMode rankingMode, RankingUtil.RankingRankerType rankType, RankingUtil.RankingScoreType scoreType, int dammyRank)
	{
		RankingDataContainer rankingDataContainer = this.GetRankingDataContainer(rankingMode);
		if (rankingDataContainer != null)
		{
			rankingDataContainer.SavePlayerRankingDummy(rankType, scoreType, dammyRank);
		}
	}

	// Token: 0x06004693 RID: 18067 RVA: 0x001712A0 File Offset: 0x0016F4A0
	public TimeSpan GetRankigResetTimeSpan(RankingUtil.RankingMode rankingMode, RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType)
	{
		RankingDataContainer rankingDataContainer = this.GetRankingDataContainer(rankingMode);
		if (rankingDataContainer == null)
		{
			return NetUtil.GetCurrentTime().AddMinutes(1.0) - NetUtil.GetCurrentTime();
		}
		return rankingDataContainer.GetResetTimeSpan(rankerType, scoreType);
	}

	// Token: 0x06004694 RID: 18068 RVA: 0x001712E4 File Offset: 0x0016F4E4
	public static int GetRankingMax(RankingUtil.RankingRankerType rankerType, int page = 0)
	{
		return RankingManager.GetRankingSize(rankerType, 1, page) - 1;
	}

	// Token: 0x06004695 RID: 18069 RVA: 0x001712F0 File Offset: 0x0016F4F0
	public static RankingUtil.Ranker GetMyRank(RankingUtil.RankingMode rankingMode, RankingUtil.RankingRankerType rankType, RankingUtil.RankingScoreType scoreType)
	{
		if (SingletonGameObject<RankingManager>.Instance != null)
		{
			return SingletonGameObject<RankingManager>.Instance.GetMyRankOrg(rankingMode, rankType, scoreType);
		}
		return null;
	}

	// Token: 0x06004696 RID: 18070 RVA: 0x00171314 File Offset: 0x0016F514
	private RankingUtil.Ranker GetMyRankOrg(RankingUtil.RankingMode rankingMode, RankingUtil.RankingRankerType rankType, RankingUtil.RankingScoreType scoreType)
	{
		RankingUtil.Ranker result = null;
		List<RankingUtil.Ranker> rankerList = this.GetRankerList(rankingMode, scoreType, rankType, 0);
		if (rankerList != null && rankerList.Count > 0)
		{
			RankingUtil.Ranker ranker = rankerList[0];
			if (ranker != null)
			{
				result = ranker;
			}
		}
		return result;
	}

	// Token: 0x06004697 RID: 18071 RVA: 0x00171354 File Offset: 0x0016F554
	public static long GetMyHiScore(RankingUtil.RankingMode rankingMode, bool isEvent)
	{
		long result = 0L;
		if (SingletonGameObject<RankingManager>.Instance != null)
		{
			RankingUtil.RankingRankerType rankType;
			RankingUtil.RankingScoreType scoreType;
			if (isEvent)
			{
				rankType = RankingUtil.RankingRankerType.SP_ALL;
				scoreType = RankingManager.EndlessSpecialRankingScoreType;
			}
			else
			{
				rankType = RankingUtil.RankingRankerType.RIVAL;
				scoreType = RankingManager.EndlessRivalRankingScoreType;
			}
			RankingUtil.Ranker myRankOrg = SingletonGameObject<RankingManager>.Instance.GetMyRankOrg(rankingMode, rankType, scoreType);
			if (myRankOrg != null)
			{
				result = myRankOrg.hiScore;
			}
		}
		return result;
	}

	// Token: 0x06004698 RID: 18072 RVA: 0x001713AC File Offset: 0x0016F5AC
	public static RankingUtil.RankingScoreType GetCurrentRankingScoreType(RankingUtil.RankingMode rankingMode, bool isEvent)
	{
		RankingUtil.RankingScoreType result = RankingUtil.RankingScoreType.HIGH_SCORE;
		if (SingletonGameObject<RankingManager>.Instance != null)
		{
			RankingUtil.RankingDataSet rankingDataSet = SingletonGameObject<RankingManager>.Instance.GetRankingDataSet(rankingMode);
			if (rankingDataSet != null)
			{
				if (isEvent)
				{
					result = rankingDataSet.targetSpScoreType;
				}
				else
				{
					result = rankingDataSet.targetRivalScoreType;
				}
			}
		}
		return result;
	}

	// Token: 0x06004699 RID: 18073 RVA: 0x001713F8 File Offset: 0x0016F5F8
	public static int GetCurrentMyLeagueMax(RankingUtil.RankingMode rankingMode)
	{
		if (SingletonGameObject<RankingManager>.Instance != null)
		{
			return SingletonGameObject<RankingManager>.Instance.GetCurrentMyLeagueMaxOrg(rankingMode);
		}
		return 0;
	}

	// Token: 0x0600469A RID: 18074 RVA: 0x00171418 File Offset: 0x0016F618
	private int GetCurrentMyLeagueMaxOrg(RankingUtil.RankingMode rankingMode)
	{
		int result = 0;
		List<RankingUtil.Ranker> rankerList = this.GetRankerList(rankingMode, RankingManager.EndlessRivalRankingScoreType, RankingUtil.RankingRankerType.RIVAL, 0);
		if (rankerList != null)
		{
			result = rankerList.Count - 1;
		}
		return result;
	}

	// Token: 0x0600469B RID: 18075 RVA: 0x00171448 File Offset: 0x0016F648
	public static bool GetCurrentRankingStatus(RankingUtil.RankingMode rankingMode, bool isEvent, out long myScore, out long myHiScore, out int myLeague)
	{
		if (SingletonGameObject<RankingManager>.Instance != null)
		{
			return SingletonGameObject<RankingManager>.Instance.GetCurrentRankingStatusOrg(rankingMode, isEvent, out myScore, out myHiScore, out myLeague);
		}
		myScore = 0L;
		myLeague = 0;
		myHiScore = 0L;
		return false;
	}

	// Token: 0x0600469C RID: 18076 RVA: 0x00171484 File Offset: 0x0016F684
	private bool GetCurrentRankingStatusOrg(RankingUtil.RankingMode rankingMode, bool isEvent, out long myScore, out long myHiScore, out int myLeague)
	{
		bool result = false;
		myScore = 0L;
		myHiScore = 0L;
		myLeague = 0;
		List<RankingUtil.Ranker> rankerList;
		if (isEvent)
		{
			rankerList = this.GetRankerList(rankingMode, RankingManager.EndlessSpecialRankingScoreType, RankingUtil.RankingRankerType.SP_ALL, 0);
		}
		else
		{
			rankerList = this.GetRankerList(rankingMode, RankingManager.EndlessRivalRankingScoreType, RankingUtil.RankingRankerType.RIVAL, 0);
		}
		if (rankerList != null && rankerList.Count > 0)
		{
			RankingUtil.Ranker ranker = rankerList[0];
			if (ranker != null)
			{
				myScore = ranker.score;
				myHiScore = ranker.hiScore;
				myLeague = ranker.leagueIndex;
				result = true;
			}
		}
		return result;
	}

	// Token: 0x0600469D RID: 18077 RVA: 0x00171508 File Offset: 0x0016F708
	public static int GetCurrentHighScoreRank(RankingUtil.RankingMode rankingMode, bool isEvent, ref long currentScore, out bool isHighScore, out long nextRankScore, out long prveRankScore, out int nextRank)
	{
		if (SingletonGameObject<RankingManager>.Instance != null)
		{
			return SingletonGameObject<RankingManager>.Instance.GetCurrentHighScoreRankOrg(rankingMode, isEvent, ref currentScore, out isHighScore, out nextRankScore, out prveRankScore, out nextRank);
		}
		isHighScore = false;
		nextRankScore = 0L;
		prveRankScore = 0L;
		nextRank = -1;
		return -1;
	}

	// Token: 0x0600469E RID: 18078 RVA: 0x0017154C File Offset: 0x0016F74C
	private int GetCurrentHighScoreRankOrg(RankingUtil.RankingMode rankingMode, bool isEvent, ref long currentScore, out bool isHighScore, out long nextRankScore, out long prveRankScore, out int nextRank)
	{
		RankingDataContainer rankingDataContainer = this.GetRankingDataContainer(rankingMode);
		if (rankingDataContainer != null)
		{
			return rankingDataContainer.GetCurrentHighScoreRank(isEvent, ref currentScore, out isHighScore, out nextRankScore, out prveRankScore, out nextRank);
		}
		isHighScore = false;
		nextRankScore = 0L;
		prveRankScore = 0L;
		nextRank = -1;
		return -1;
	}

	// Token: 0x0600469F RID: 18079 RVA: 0x0017158C File Offset: 0x0016F78C
	public static bool IsRankingInAggregate(RankingUtil.RankingMode rankingMode, RankingUtil.RankingRankerType rankType, RankingUtil.RankingScoreType scoreType)
	{
		bool result = false;
		if (SingletonGameObject<RankingManager>.Instance != null)
		{
			result = SingletonGameObject<RankingManager>.Instance.IsRankingInAggregateOrg(rankingMode, rankType, scoreType);
		}
		return result;
	}

	// Token: 0x060046A0 RID: 18080 RVA: 0x001715BC File Offset: 0x0016F7BC
	private bool IsRankingInAggregateOrg(RankingUtil.RankingMode rankingMode, RankingUtil.RankingRankerType rankType, RankingUtil.RankingScoreType scoreType)
	{
		bool result = false;
		if (this.GetRankigResetTimeSpan(rankingMode, scoreType, rankType).Ticks <= 0L)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x060046A1 RID: 18081 RVA: 0x001715E8 File Offset: 0x0016F7E8
	public static void UpdateSendChallenge(string id)
	{
		if (SingletonGameObject<RankingManager>.Instance != null)
		{
			SingletonGameObject<RankingManager>.Instance.UpdateSendChallengeOrg(id);
		}
	}

	// Token: 0x060046A2 RID: 18082 RVA: 0x00171608 File Offset: 0x0016F808
	private void UpdateSendChallengeOrg(string id)
	{
		EventManager.EventType type = EventManager.Instance.Type;
		bool flag = this.UpdateSendChallengeRankingList(RankingUtil.RankingRankerType.RIVAL, id);
		if (flag)
		{
			RankingUI.UpdateSendChallenge(RankingUtil.RankingRankerType.RIVAL, id);
		}
		flag = this.UpdateSendChallengeRankingList(RankingUtil.RankingRankerType.FRIEND, id);
		if (flag)
		{
			RankingUI.UpdateSendChallenge(RankingUtil.RankingRankerType.FRIEND, id);
		}
		flag = this.UpdateSendChallengeRankingList(RankingUtil.RankingRankerType.ALL, id);
		if (flag)
		{
			RankingUI.UpdateSendChallenge(RankingUtil.RankingRankerType.ALL, id);
		}
		flag = this.UpdateSendChallengeRankingList(RankingUtil.RankingRankerType.HISTORY, id);
		if (flag)
		{
			RankingUI.UpdateSendChallenge(RankingUtil.RankingRankerType.HISTORY, id);
		}
		if (type == EventManager.EventType.SPECIAL_STAGE)
		{
			flag = this.UpdateSendChallengeRankingList(RankingUtil.RankingRankerType.SP_FRIEND, id);
			if (flag)
			{
				SpecialStageWindow.UpdateSendChallenge(RankingUtil.RankingRankerType.SP_FRIEND, id);
			}
			flag = this.UpdateSendChallengeRankingList(RankingUtil.RankingRankerType.SP_ALL, id);
			if (flag)
			{
				SpecialStageWindow.UpdateSendChallenge(RankingUtil.RankingRankerType.SP_ALL, id);
			}
		}
	}

	// Token: 0x060046A3 RID: 18083 RVA: 0x001716AC File Offset: 0x0016F8AC
	private bool UpdateSendChallengeRankingList(RankingUtil.RankingRankerType type, string id)
	{
		bool result = false;
		if (this.m_rankingDataSet != null)
		{
			Dictionary<RankingUtil.RankingMode, RankingUtil.RankingDataSet>.KeyCollection keys = this.m_rankingDataSet.Keys;
			foreach (RankingUtil.RankingMode key in keys)
			{
				if (this.m_rankingDataSet[key].UpdateSendChallengeList(type, id))
				{
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x060046A4 RID: 18084 RVA: 0x0017173C File Offset: 0x0016F93C
	private void DefaultCallback(List<RankingUtil.Ranker> rankerList, RankingUtil.RankingScoreType score, RankingUtil.RankingRankerType type, int page, bool isNext, bool isPrev, bool isCashData)
	{
		RankingUI.Setup();
	}

	// Token: 0x060046A5 RID: 18085 RVA: 0x00171744 File Offset: 0x0016F944
	private void EventRankingInitCallback(List<RankingUtil.Ranker> rankerList, RankingUtil.RankingScoreType score, RankingUtil.RankingRankerType type, int page, bool isNext, bool isPrev, bool isCashData)
	{
		global::Debug.Log(string.Concat(new object[]
		{
			" ! RankingManager:NullCallback   type:",
			type,
			"  page:",
			page,
			"  isNext:",
			isNext,
			"  isPrev:",
			isPrev,
			"  num:",
			rankerList.Count
		}));
		this.m_isSpRankingInit = true;
		SpecialStageWindow.RankingSetup(false);
	}

	// Token: 0x060046A6 RID: 18086 RVA: 0x001717D0 File Offset: 0x0016F9D0
	public void EventRankingSameRankCallback(List<RankingUtil.Ranker> rankerList, RankingUtil.RankingScoreType score, RankingUtil.RankingRankerType type, int page, bool isNext, bool isPrev, bool isCashData)
	{
		global::Debug.Log(string.Concat(new object[]
		{
			" ! RankingManager:EventRankingSameRankCallback   type:",
			type,
			"  page:",
			page,
			"  isNext:",
			isNext,
			"  isPrev:",
			isPrev,
			"  num:",
			rankerList.Count
		}));
	}

	// Token: 0x060046A7 RID: 18087 RVA: 0x0017184C File Offset: 0x0016FA4C
	public void RankingSameRankCallback(List<RankingUtil.Ranker> rankerList, RankingUtil.RankingScoreType score, RankingUtil.RankingRankerType type, int page, bool isNext, bool isPrev, bool isCashData)
	{
		global::Debug.Log(string.Concat(new object[]
		{
			" ! RankingManager:RankingSameRankCallback   type:",
			type,
			"  page:",
			page,
			"  isNext:",
			isNext,
			"  isPrev:",
			isPrev,
			"  num:",
			rankerList.Count
		}));
	}

	// Token: 0x060046A8 RID: 18088 RVA: 0x001718C8 File Offset: 0x0016FAC8
	private void ServerGetLeaderboardEntries_Succeeded(MsgGetLeaderboardEntriesSucceed msg)
	{
		ServerLeaderboardEntries leaderboardEntries = msg.m_leaderboardEntries;
		int num = msg.m_leaderboardEntries.m_mode;
		int rankerPage = this.GetRankerPage(msg);
		int rankingType = leaderboardEntries.m_rankingType;
		if (num < 0 || num >= 2)
		{
			num = 0;
		}
		RankingUtil.RankingMode rankingMode = (RankingUtil.RankingMode)num;
		global::Debug.Log(string.Concat(new object[]
		{
			" RankingManager:ServerGetLeaderboardEntries_Succeeded mode:",
			rankingMode,
			" Count:",
			msg.m_leaderboardEntries.m_leaderboardEntries.Count
		}));
		if (this.m_rankingDataSet != null && this.m_rankingDataSet.ContainsKey(rankingMode))
		{
			this.m_rankingDataSet[rankingMode].AddRankerList(msg);
			if (RankingUtil.IsRankingUserFrontAndBack(this.scoreType, this.rankerType, rankerPage))
			{
				RankingManager.CallbackRankingData callback;
				if (this.rankerType == RankingUtil.RankingRankerType.SP_ALL || this.rankerType == RankingUtil.RankingRankerType.SP_FRIEND)
				{
					callback = new RankingManager.CallbackRankingData(this.EventRankingSameRankCallback);
				}
				else
				{
					callback = new RankingManager.CallbackRankingData(this.RankingSameRankCallback);
				}
				this.GetRanking(rankingMode, this.scoreType, this.rankerType, 3, callback);
			}
			if (this.m_callbacks == null)
			{
				this.m_callbacks = new List<RankingManager.CallbackData>();
			}
			if (this.m_callbacks.Count > 0)
			{
				RankingManager.CallbackData callbackData = null;
				for (int i = 0; i < this.m_callbacks.Count; i++)
				{
					if (this.m_callbacks[i].Check(rankingType, rankerPage))
					{
						callbackData = this.m_callbacks[i];
						break;
					}
				}
				if (callbackData != null && callbackData.callback != null)
				{
					leaderboardEntries = msg.m_leaderboardEntries;
					List<RankingUtil.Ranker> rankerList;
					if (this.rankerType == RankingUtil.RankingRankerType.RIVAL || rankerPage == 0)
					{
						rankerList = this.m_rankingDataSet[rankingMode].GetRankerList(this.rankerType, this.scoreType, 0);
					}
					else
					{
						rankerList = this.m_rankingDataSet[rankingMode].GetRankerList(this.rankerType, this.scoreType, 1);
					}
					callbackData.callback(rankerList, this.scoreType, this.rankerType, rankerPage, leaderboardEntries.IsNext(), leaderboardEntries.IsPrev(), false);
					this.m_callbacks.Remove(callbackData);
				}
			}
			global::Debug.Log(" RankingManager:ServerGetLeaderboardEntries_Succeeded  chainGetRankingCodeList:" + this.m_chainGetRankingCodeList.Count);
			if (this.m_chainGetRankingCodeList != null && this.m_chainGetRankingCodeList.Count > 0)
			{
				RankingUtil.RankingScoreType rankerScoreType = RankingUtil.GetRankerScoreType(rankingType);
				RankingUtil.RankingRankerType rankerType = RankingUtil.GetRankerType(rankingType);
				int rankingCode = RankingUtil.GetRankingCode(rankingMode, rankerScoreType, rankerType);
				if (this.m_chainGetRankingCodeList.Contains(rankingCode))
				{
					this.m_chainGetRankingCodeList.Remove(rankingCode);
					if (this.m_chainGetRankingCodeList.Count > 0)
					{
						this.m_isLoading = false;
						int rankingType2 = this.m_chainGetRankingCodeList[0];
						RankingUtil.RankingMode rankerMode = RankingUtil.GetRankerMode(rankingType2);
						RankingUtil.RankingScoreType rankerScoreType2 = RankingUtil.GetRankerScoreType(rankingType2);
						RankingUtil.RankingRankerType rankerType2 = RankingUtil.GetRankerType(rankingType2);
						this.GetRanking(rankerMode, rankerScoreType2, rankerType2, 0, new RankingManager.CallbackRankingData(this.DefaultCallback));
					}
					else
					{
						global::Debug.Log(" RankingManager:ServerGetLeaderboardEntries_Succeeded  chain end " + (this.m_callbackBakNormalAll != null));
						if (this.m_callbackBakNormalAll != null)
						{
							List<RankingUtil.Ranker> rankerList2;
							if (this.rankerType == RankingUtil.RankingRankerType.RIVAL || rankerPage == 0)
							{
								rankerList2 = this.m_rankingDataSet[rankingMode].GetRankerList(this.rankerType, this.scoreType, 0);
							}
							else
							{
								rankerList2 = this.m_rankingDataSet[rankingMode].GetRankerList(this.rankerType, this.scoreType, 1);
							}
							this.m_callbackBakNormalAll(rankerList2, this.scoreType, this.rankerType, rankerPage, leaderboardEntries.IsNext(), leaderboardEntries.IsPrev(), false);
							this.m_callbackBakNormalAll = null;
						}
						this.m_chainGetRankingCodeList.Clear();
					}
				}
			}
		}
		this.m_getRankingLastTime = Time.realtimeSinceStartup;
		this.m_isLoading = false;
	}

	// Token: 0x060046A9 RID: 18089 RVA: 0x00171CB4 File Offset: 0x0016FEB4
	private void ServerGetLeaderboardEntries_Failed()
	{
		global::Debug.Log(" RankingManager:ServerGetLeaderboardEntries_Failed()");
		this.m_getRankingLastTime = Time.realtimeSinceStartup;
		this.m_isLoading = false;
	}

	// Token: 0x060046AA RID: 18090 RVA: 0x00171CD4 File Offset: 0x0016FED4
	private List<RankingUtil.Ranker> GetRankerList(RankingUtil.RankingMode rankingMode, RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType, int page = 0)
	{
		List<RankingUtil.Ranker> result = null;
		if (rankerType == RankingUtil.RankingRankerType.RIVAL)
		{
			page = 0;
		}
		if (this.m_rankingDataSet != null && this.m_rankingDataSet.ContainsKey(rankingMode))
		{
			result = this.m_rankingDataSet[rankingMode].GetRankerList(rankerType, scoreType, page);
		}
		return result;
	}

	// Token: 0x060046AB RID: 18091 RVA: 0x00171D20 File Offset: 0x0016FF20
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

	// Token: 0x060046AC RID: 18092 RVA: 0x00171D60 File Offset: 0x0016FF60
	private int GetRankingTop(RankingUtil.RankingMode rankingMode, RankingUtil.RankingRankerType rankerType, RankingUtil.RankingScoreType scoreType, int page = 0)
	{
		int num = 1;
		if (page >= 3)
		{
			RankingDataContainer rankingDataContainer = this.GetRankingDataContainer(rankingMode);
			if (rankingDataContainer != null)
			{
				Dictionary<RankingUtil.RankingScoreType, List<MsgGetLeaderboardEntriesSucceed>> dictionary;
				rankingDataContainer.IsRankerType(rankerType, out dictionary);
				if (dictionary != null && dictionary.ContainsKey(scoreType))
				{
					List<MsgGetLeaderboardEntriesSucceed> list = dictionary[scoreType];
					if (list != null && list.Count > 0)
					{
						ServerLeaderboardEntry serverLeaderboardEntry = null;
						if (list[0] != null)
						{
							serverLeaderboardEntry = list[0].m_leaderboardEntries.m_myLeaderboardEntry;
						}
						else if (list.Count > 1 && list[1] != null)
						{
							serverLeaderboardEntry = list[1].m_leaderboardEntries.m_myLeaderboardEntry;
						}
						if (serverLeaderboardEntry != null)
						{
							num = serverLeaderboardEntry.m_grade - 50;
							if (num < 1)
							{
								num = 1;
							}
						}
					}
				}
			}
		}
		return num;
	}

	// Token: 0x060046AD RID: 18093 RVA: 0x00171E2C File Offset: 0x0017002C
	private static int GetRankingSize(RankingUtil.RankingRankerType rankerType, int top, int page)
	{
		if (rankerType == RankingUtil.RankingRankerType.COUNT || page < 0)
		{
			return -1;
		}
		int result = 0;
		switch (rankerType)
		{
		case RankingUtil.RankingRankerType.FRIEND:
		case RankingUtil.RankingRankerType.ALL:
		case RankingUtil.RankingRankerType.HISTORY:
		case RankingUtil.RankingRankerType.SP_ALL:
		case RankingUtil.RankingRankerType.SP_FRIEND:
			result = 4;
			break;
		case RankingUtil.RankingRankerType.RIVAL:
			result = 71;
			break;
		}
		if (page > 0)
		{
			result = 71;
			if (page >= 3)
			{
				result = 100;
			}
		}
		return result;
	}

	// Token: 0x060046AE RID: 18094 RVA: 0x00171E94 File Offset: 0x00170094
	public bool IsRankingTop(RankingUtil.RankingMode rankingMode, RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType)
	{
		bool result = false;
		if (scoreType == RankingUtil.RankingScoreType.NONE || rankerType == RankingUtil.RankingRankerType.COUNT)
		{
			return false;
		}
		RankingDataContainer rankingDataContainer = this.GetRankingDataContainer(rankingMode);
		if (rankingDataContainer != null)
		{
			result = rankingDataContainer.IsRankerAndScoreType(rankerType, scoreType, -1);
		}
		return result;
	}

	// Token: 0x060046AF RID: 18095 RVA: 0x00171ECC File Offset: 0x001700CC
	private bool IsRankingList(RankingUtil.RankingMode rankingMode, RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType, int page = 0)
	{
		bool result = false;
		if (scoreType == RankingUtil.RankingScoreType.NONE || rankerType == RankingUtil.RankingRankerType.COUNT || page < 0)
		{
			return false;
		}
		RankingDataContainer rankingDataContainer = this.GetRankingDataContainer(rankingMode);
		if (rankingDataContainer != null)
		{
			result = rankingDataContainer.IsRankerAndScoreType(rankerType, scoreType, page);
		}
		return result;
	}

	// Token: 0x060046B0 RID: 18096 RVA: 0x00171F10 File Offset: 0x00170110
	public List<RankingUtil.Ranker> GetCacheRankingList(RankingUtil.RankingMode rankingMode, RankingUtil.RankingScoreType scoreType, RankingUtil.RankingRankerType rankerType)
	{
		List<RankingUtil.Ranker> list = null;
		RankingDataContainer rankingDataContainer = this.GetRankingDataContainer(rankingMode);
		if (rankingDataContainer != null)
		{
			if (rankerType == RankingUtil.RankingRankerType.RIVAL)
			{
				list = rankingDataContainer.GetRankerList(rankerType, scoreType, 0);
			}
			else
			{
				list = rankingDataContainer.GetRankerList(rankerType, scoreType, 1);
				List<RankingUtil.Ranker> rankerList = rankingDataContainer.GetRankerList(rankerType, scoreType, 2);
				if (list != null && rankerList != null && rankerList.Count > 1)
				{
					for (int i = 1; i < rankerList.Count; i++)
					{
						list.Add(rankerList[i]);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x060046B1 RID: 18097 RVA: 0x00171F94 File Offset: 0x00170194
	public Texture GetChaoTexture(int chaoId, UITexture chaoTexture, float delay = 0.2f, bool isDefaultTexture = false)
	{
		Texture result = null;
		if (chaoTexture == null)
		{
			return null;
		}
		if (this.m_chaoTextureList != null && this.m_chaoTextureList.ContainsKey(chaoId))
		{
			result = this.m_chaoTextureList[chaoId];
			chaoTexture.mainTexture = this.m_chaoTextureList[chaoId];
			chaoTexture.alpha = 1f;
		}
		else
		{
			Texture loadedTexture = ChaoTextureManager.Instance.GetLoadedTexture(chaoId);
			if (loadedTexture != null)
			{
				if (this.m_chaoTextureList == null)
				{
					this.m_chaoTextureList = new Dictionary<int, Texture>();
				}
				this.m_chaoTextureList.Add(chaoId, loadedTexture);
				result = this.m_chaoTextureList[chaoId];
				chaoTexture.mainTexture = this.m_chaoTextureList[chaoId];
				chaoTexture.alpha = 1f;
			}
			else
			{
				if (isDefaultTexture || chaoTexture.alpha > 0f)
				{
					chaoTexture.mainTexture = ChaoTextureManager.Instance.m_defaultTexture;
				}
				if (this.m_chaoTextureLoad == null)
				{
					this.m_chaoTextureLoad = new Dictionary<int, float>();
				}
				if (this.m_chaoTextureList == null)
				{
					this.m_chaoTextureList = new Dictionary<int, Texture>();
				}
				if (this.m_chaoTextureObject == null)
				{
					this.m_chaoTextureObject = new Dictionary<int, List<UITexture>>();
				}
				if (!this.m_chaoTextureObject.ContainsKey(chaoId))
				{
					List<UITexture> value = new List<UITexture>();
					this.m_chaoTextureObject.Add(chaoId, value);
				}
				this.m_chaoTextureObject[chaoId].Add(chaoTexture);
				if (this.m_chaoTextureLoad.Count <= 0)
				{
					ChaoTextureManager.CallbackInfo.LoadFinishCallback callback = new ChaoTextureManager.CallbackInfo.LoadFinishCallback(this.ChaoLoadFinishCallback);
					ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(null, callback, false);
					ChaoTextureManager.Instance.GetTexture(chaoId, info);
					this.m_chaoTextureLoadTime = 0f;
				}
				else if (!this.m_chaoTextureLoad.ContainsKey(chaoId))
				{
					this.m_chaoTextureLoad.Add(chaoId, delay);
				}
				else if (delay > 0f && this.m_chaoTextureLoad[chaoId] < delay * 0.15f)
				{
					this.m_chaoTextureLoad[chaoId] = delay * 0.15f;
				}
				this.m_chaoTextureLoadEndTime = 0f;
			}
		}
		return result;
	}

	// Token: 0x060046B2 RID: 18098 RVA: 0x001721B0 File Offset: 0x001703B0
	public void ResetChaoTexture()
	{
		if (this.m_chaoTextureLoad != null)
		{
			this.m_chaoTextureLoad.Clear();
		}
		if (this.m_chaoTextureList != null)
		{
			this.m_chaoTextureList.Clear();
		}
		if (this.m_chaoTextureObject != null)
		{
			this.m_chaoTextureObject.Clear();
		}
		this.m_chaoTextureLoadTime = -1f;
		this.m_chaoTextureLoadEndTime = 0f;
		this.m_chaoTextureLoad = new Dictionary<int, float>();
		this.m_chaoTextureList = new Dictionary<int, Texture>();
		this.m_chaoTextureObject = new Dictionary<int, List<UITexture>>();
	}

	// Token: 0x060046B3 RID: 18099 RVA: 0x00172238 File Offset: 0x00170438
	private void ChaoLoadFinishCallback(Texture tex)
	{
		if (tex == null)
		{
			return;
		}
		string[] array = tex.name.Split(new char[]
		{
			'_'
		});
		int num = int.Parse(array[array.Length - 1]);
		if (this.m_chaoTextureObject != null && this.m_chaoTextureLoad != null && this.m_chaoTextureObject.ContainsKey(num))
		{
			bool flag = true;
			if (this.m_chaoTextureLoad.ContainsKey(num))
			{
				if (this.m_chaoTextureLoad[num] > 0f)
				{
					flag = false;
				}
				else
				{
					this.m_chaoTextureLoad.Remove(num);
				}
			}
			if (flag)
			{
				List<UITexture> list = this.m_chaoTextureObject[num];
				if (list != null && list.Count > 0)
				{
					foreach (UITexture uitexture in list)
					{
						if (uitexture != null)
						{
							uitexture.mainTexture = tex;
							uitexture.alpha = 1f;
						}
					}
				}
				this.m_chaoTextureObject.Remove(num);
			}
		}
		if (num >= 0 && this.m_chaoTextureList != null && !this.m_chaoTextureList.ContainsKey(num))
		{
			this.m_chaoTextureList.Add(num, tex);
		}
		int num2 = -1;
		if (this.m_chaoTextureLoad != null && this.m_chaoTextureLoad.Count > 0)
		{
			int[] array2 = new int[this.m_chaoTextureLoad.Count];
			this.m_chaoTextureLoad.Keys.CopyTo(array2, 0);
			foreach (int num3 in array2)
			{
				if (!this.m_chaoTextureList.ContainsKey(num3))
				{
					num2 = num3;
					break;
				}
			}
			if (num2 >= 0)
			{
				ChaoTextureManager.CallbackInfo.LoadFinishCallback callback = new ChaoTextureManager.CallbackInfo.LoadFinishCallback(this.ChaoLoadFinishCallback);
				ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(null, callback, false);
				ChaoTextureManager.Instance.GetTexture(num, info);
			}
		}
	}

	// Token: 0x04003AC7 RID: 15047
	public const long CHAO_ID_OFFSET = 1000000L;

	// Token: 0x04003AC8 RID: 15048
	public const float CHAO_TEX_LOAD_DELAY = 0.25f;

	// Token: 0x04003AC9 RID: 15049
	public const float AUTO_RELOAD_TIME = 5f;

	// Token: 0x04003ACA RID: 15050
	private const int CALLBACK_STACK_MAX = 256;

	// Token: 0x04003ACB RID: 15051
	public const int PAGE0_RANKER_COUNT = 3;

	// Token: 0x04003ACC RID: 15052
	private const int PAGE_RANKER_COUNT_INIT = 70;

	// Token: 0x04003ACD RID: 15053
	private const int PAGE_RANKER_COUNT_MARGIN = 20;

	// Token: 0x04003ACE RID: 15054
	private const int PAGE_RANKER_COUNT_SAME = 100;

	// Token: 0x04003ACF RID: 15055
	private Dictionary<RankingUtil.RankingMode, RankingUtil.RankingDataSet> m_rankingDataSet;

	// Token: 0x04003AD0 RID: 15056
	private RankingUtil.RankingMode m_mode;

	// Token: 0x04003AD1 RID: 15057
	private RankingUtil.RankingScoreType m_scoreType;

	// Token: 0x04003AD2 RID: 15058
	private RankingUtil.RankingRankerType m_rankerType = RankingUtil.RankingRankerType.ALL;

	// Token: 0x04003AD3 RID: 15059
	private bool m_isLoading;

	// Token: 0x04003AD4 RID: 15060
	private float m_getRankingLastTime;

	// Token: 0x04003AD5 RID: 15061
	private int m_page;

	// Token: 0x04003AD6 RID: 15062
	private int m_eventId;

	// Token: 0x04003AD7 RID: 15063
	private bool m_isSpRankingInit;

	// Token: 0x04003AD8 RID: 15064
	private bool m_isReset = true;

	// Token: 0x04003AD9 RID: 15065
	private bool m_isRankingInit;

	// Token: 0x04003ADA RID: 15066
	private bool m_isRankingPageCheck;

	// Token: 0x04003ADB RID: 15067
	private List<RankingManager.CallbackData> m_callbacks = new List<RankingManager.CallbackData>();

	// Token: 0x04003ADC RID: 15068
	private RankingManager.CallbackRankingData m_callbackBakNormalAll;

	// Token: 0x04003ADD RID: 15069
	private RankingManager.CallbackRankingData m_callbackBakEventAll;

	// Token: 0x04003ADE RID: 15070
	private float m_chaoTextureLoadTime = -1f;

	// Token: 0x04003ADF RID: 15071
	private float m_chaoTextureLoadEndTime = -1f;

	// Token: 0x04003AE0 RID: 15072
	private Dictionary<int, float> m_chaoTextureLoad;

	// Token: 0x04003AE1 RID: 15073
	private Dictionary<int, Texture> m_chaoTextureList;

	// Token: 0x04003AE2 RID: 15074
	private Dictionary<int, List<UITexture>> m_chaoTextureObject;

	// Token: 0x04003AE3 RID: 15075
	private int m_initLoadCount;

	// Token: 0x04003AE4 RID: 15076
	private List<int> m_chainGetRankingCodeList;

	// Token: 0x02000A4D RID: 2637
	private class CallbackData
	{
		// Token: 0x060046B4 RID: 18100 RVA: 0x00172460 File Offset: 0x00170660
		public CallbackData(RankingManager.CallbackRankingData target, int ranking, int page)
		{
			this.callback = target;
			this.rankingType = ranking;
			this.getPage = page;
			this.startTime = Time.realtimeSinceStartup;
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x060046B5 RID: 18101 RVA: 0x00172494 File Offset: 0x00170694
		// (set) Token: 0x060046B6 RID: 18102 RVA: 0x0017249C File Offset: 0x0017069C
		public RankingManager.CallbackRankingData callback { get; set; }

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060046B7 RID: 18103 RVA: 0x001724A8 File Offset: 0x001706A8
		// (set) Token: 0x060046B8 RID: 18104 RVA: 0x001724B0 File Offset: 0x001706B0
		public int rankingType { get; set; }

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060046B9 RID: 18105 RVA: 0x001724BC File Offset: 0x001706BC
		// (set) Token: 0x060046BA RID: 18106 RVA: 0x001724C4 File Offset: 0x001706C4
		public int getPage { get; set; }

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060046BB RID: 18107 RVA: 0x001724D0 File Offset: 0x001706D0
		// (set) Token: 0x060046BC RID: 18108 RVA: 0x001724D8 File Offset: 0x001706D8
		public float startTime { get; set; }

		// Token: 0x060046BD RID: 18109 RVA: 0x001724E4 File Offset: 0x001706E4
		public bool Check(int ranking, int page)
		{
			bool result = false;
			if (ranking == this.rankingType && this.getPage == page)
			{
				result = true;
			}
			return result;
		}
	}

	// Token: 0x02000AB6 RID: 2742
	// (Invoke) Token: 0x06004912 RID: 18706
	public delegate void CallbackRankingData(List<RankingUtil.Ranker> rankerList, RankingUtil.RankingScoreType score, RankingUtil.RankingRankerType type, int page, bool isNext, bool isPrev, bool isCashData);
}
