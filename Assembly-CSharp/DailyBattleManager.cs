using System;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000A36 RID: 2614
public class DailyBattleManager : SingletonGameObject<DailyBattleManager>
{
	// Token: 0x17000960 RID: 2400
	// (get) Token: 0x06004569 RID: 17769 RVA: 0x00164B08 File Offset: 0x00162D08
	public ServerDailyBattleStatus currentStatus
	{
		get
		{
			return this.m_currentStatus;
		}
	}

	// Token: 0x17000961 RID: 2401
	// (get) Token: 0x0600456A RID: 17770 RVA: 0x00164B10 File Offset: 0x00162D10
	public ServerDailyBattleDataPair currentDataPair
	{
		get
		{
			return this.m_currentDataPair;
		}
	}

	// Token: 0x17000962 RID: 2402
	// (get) Token: 0x0600456B RID: 17771 RVA: 0x00164B18 File Offset: 0x00162D18
	public List<ServerDailyBattleDataPair> currentDataPairList
	{
		get
		{
			return this.m_currentDataPairList;
		}
	}

	// Token: 0x17000963 RID: 2403
	// (get) Token: 0x0600456C RID: 17772 RVA: 0x00164B20 File Offset: 0x00162D20
	public List<ServerDailyBattlePrizeData> currentPrizeList
	{
		get
		{
			return this.m_currentPrizeList;
		}
	}

	// Token: 0x17000964 RID: 2404
	// (get) Token: 0x0600456D RID: 17773 RVA: 0x00164B28 File Offset: 0x00162D28
	public DateTime currentEndTime
	{
		get
		{
			return this.m_currentEndTime;
		}
	}

	// Token: 0x17000965 RID: 2405
	// (get) Token: 0x0600456E RID: 17774 RVA: 0x00164B30 File Offset: 0x00162D30
	public bool currentRewardFlag
	{
		get
		{
			return this.m_currentRewardFlag;
		}
	}

	// Token: 0x0600456F RID: 17775 RVA: 0x00164B38 File Offset: 0x00162D38
	public TimeSpan GetLimitTimeSpan()
	{
		return this.m_currentEndTime - NetBase.GetCurrentTime();
	}

	// Token: 0x17000966 RID: 2406
	// (get) Token: 0x06004570 RID: 17776 RVA: 0x00164B4C File Offset: 0x00162D4C
	public int currentWinFlag
	{
		get
		{
			int result = 0;
			if (this.m_currentDataPair != null && this.m_currentDataPair.myBattleData != null && !string.IsNullOrEmpty(this.m_currentDataPair.myBattleData.userId))
			{
				if (this.m_currentDataPair.rivalBattleData != null && !string.IsNullOrEmpty(this.m_currentDataPair.rivalBattleData.userId))
				{
					if (this.m_currentDataPair.myBattleData.maxScore > this.m_currentDataPair.rivalBattleData.maxScore)
					{
						result = 3;
					}
					else if (this.m_currentDataPair.myBattleData.maxScore == this.m_currentDataPair.rivalBattleData.maxScore)
					{
						result = 2;
					}
					else
					{
						result = 1;
					}
				}
				else
				{
					result = 4;
				}
			}
			return result;
		}
	}

	// Token: 0x17000967 RID: 2407
	// (get) Token: 0x06004571 RID: 17777 RVA: 0x00164C1C File Offset: 0x00162E1C
	public Dictionary<int, ServerConsumedCostData> resetCostList
	{
		get
		{
			if (this.m_resetCostList == null)
			{
				List<ServerConsumedCostData> costList = ServerInterface.CostList;
				if (costList != null)
				{
					this.m_resetCostList = new Dictionary<int, ServerConsumedCostData>();
					foreach (ServerConsumedCostData serverConsumedCostData in costList)
					{
						switch (serverConsumedCostData.consumedItemId)
						{
						case 980000:
							if (!this.m_resetCostList.ContainsKey(0))
							{
								this.m_resetCostList.Add(0, serverConsumedCostData);
							}
							break;
						case 980001:
							if (!this.m_resetCostList.ContainsKey(1))
							{
								this.m_resetCostList.Add(1, serverConsumedCostData);
							}
							break;
						case 980002:
							if (!this.m_resetCostList.ContainsKey(2))
							{
								this.m_resetCostList.Add(2, serverConsumedCostData);
							}
							break;
						}
					}
				}
			}
			return this.m_resetCostList;
		}
	}

	// Token: 0x17000968 RID: 2408
	// (get) Token: 0x06004572 RID: 17778 RVA: 0x00164D2C File Offset: 0x00162F2C
	public bool isLoading
	{
		get
		{
			return this.m_isFirstSetupReq || this.m_isResultSetupReq;
		}
	}

	// Token: 0x17000969 RID: 2409
	// (get) Token: 0x06004573 RID: 17779 RVA: 0x00164D48 File Offset: 0x00162F48
	// (set) Token: 0x06004574 RID: 17780 RVA: 0x00164D68 File Offset: 0x00162F68
	public static bool isDailyBattleDispUpdateFlag
	{
		get
		{
			return SingletonGameObject<DailyBattleManager>.Instance != null && SingletonGameObject<DailyBattleManager>.Instance.isDispUpdateFlag;
		}
		set
		{
			if (SingletonGameObject<DailyBattleManager>.Instance != null)
			{
				SingletonGameObject<DailyBattleManager>.Instance.isDispUpdateFlag = value;
			}
		}
	}

	// Token: 0x1700096A RID: 2410
	// (get) Token: 0x06004575 RID: 17781 RVA: 0x00164D88 File Offset: 0x00162F88
	// (set) Token: 0x06004576 RID: 17782 RVA: 0x00164D90 File Offset: 0x00162F90
	public bool isDispUpdateFlag
	{
		get
		{
			return this.m_isDispUpdate;
		}
		set
		{
			if (!value)
			{
				this.m_isDispUpdate = value;
			}
		}
	}

	// Token: 0x06004577 RID: 17783 RVA: 0x00164DA0 File Offset: 0x00162FA0
	private void Init()
	{
		this.m_dataInitTime = NetBase.GetCurrentTime();
		this.m_isFirstSetupReq = false;
		this.m_isResultSetupReq = false;
		this.m_isDispUpdate = false;
		DateTime currentEndTime = this.m_dataInitTime.AddHours(1.0);
		this.m_currentStatus = null;
		this.m_currentDataPair = null;
		this.m_currentDataPairList = null;
		this.m_currentPrizeList = null;
		this.m_currentEndTime = currentEndTime;
		if (this.m_chainRequestList != null)
		{
			this.m_chainRequestList.Clear();
		}
		if (this.m_chainRequestKeys != null)
		{
			this.m_chainRequestKeys.Clear();
		}
		DateTime time = this.m_dataInitTime.AddSeconds(-606.0);
		for (int i = 0; i < 7; i++)
		{
			this.UpdateDataLastTime((DailyBattleManager.DATA_TYPE)i, time);
		}
		this.m_isDataInit = true;
	}

	// Token: 0x06004578 RID: 17784 RVA: 0x00164E68 File Offset: 0x00163068
	public ServerDailyBattleDataPair GetRewardDataPair(bool reset = false)
	{
		ServerDailyBattleDataPair result = null;
		if (this.m_currentRewardFlag && this.m_currentRewardDataPair != null)
		{
			result = new ServerDailyBattleDataPair(this.m_currentRewardDataPair);
			if (reset)
			{
				this.m_currentRewardDataPair = null;
				this.m_currentRewardFlag = false;
			}
		}
		return result;
	}

	// Token: 0x06004579 RID: 17785 RVA: 0x00164EB0 File Offset: 0x001630B0
	public bool RestReward()
	{
		bool result = false;
		if (this.m_currentRewardFlag && this.m_currentRewardDataPair != null)
		{
			result = true;
		}
		this.m_currentRewardDataPair = null;
		this.m_currentRewardFlag = false;
		return result;
	}

	// Token: 0x0600457A RID: 17786 RVA: 0x00164EE8 File Offset: 0x001630E8
	public void FirstSetup(DailyBattleManager.CallbackSetupInfo callback)
	{
		this.Init();
		this.m_isFirstSetupReq = true;
		this.m_isDispUpdate = false;
		this.m_callbackSetup = null;
		this.m_callbackSetupInfo = callback;
		this.SetChainRequest(DailyBattleManager.REQ_TYPE.UPD_STATUS, DailyBattleManager.REQ_TYPE.GET_DATA);
	}

	// Token: 0x0600457B RID: 17787 RVA: 0x00164F18 File Offset: 0x00163118
	public void FirstSetup(DailyBattleManager.CallbackSetup callback)
	{
		this.Init();
		this.m_isFirstSetupReq = true;
		this.m_isDispUpdate = false;
		this.m_callbackSetup = callback;
		this.m_callbackSetupInfo = null;
		this.SetChainRequest(DailyBattleManager.REQ_TYPE.UPD_STATUS, DailyBattleManager.REQ_TYPE.GET_DATA);
	}

	// Token: 0x0600457C RID: 17788 RVA: 0x00164F48 File Offset: 0x00163148
	public void FirstSetup()
	{
		this.Init();
		this.m_isFirstSetupReq = true;
		this.m_isDispUpdate = false;
		this.m_callbackSetup = null;
		this.m_callbackSetupInfo = null;
		this.SetChainRequest(DailyBattleManager.REQ_TYPE.UPD_STATUS, DailyBattleManager.REQ_TYPE.GET_DATA);
	}

	// Token: 0x0600457D RID: 17789 RVA: 0x00164F78 File Offset: 0x00163178
	public void ResultSetup(DailyBattleManager.CallbackSetupInfo callback)
	{
		this.m_isResultSetupReq = true;
		this.m_isDispUpdate = false;
		this.m_callbackSetup = null;
		this.m_callbackSetupInfo = callback;
		this.SetChainRequest(DailyBattleManager.REQ_TYPE.POST_RESULT, DailyBattleManager.REQ_TYPE.GET_STATUS);
	}

	// Token: 0x0600457E RID: 17790 RVA: 0x00164FA0 File Offset: 0x001631A0
	public void ResultSetup(DailyBattleManager.CallbackSetup callback)
	{
		this.m_isResultSetupReq = true;
		this.m_isDispUpdate = false;
		this.m_callbackSetup = callback;
		this.m_callbackSetupInfo = null;
		this.SetChainRequest(DailyBattleManager.REQ_TYPE.POST_RESULT, DailyBattleManager.REQ_TYPE.GET_STATUS);
	}

	// Token: 0x0600457F RID: 17791 RVA: 0x00164FC8 File Offset: 0x001631C8
	public void ResultSetup()
	{
		this.m_isResultSetupReq = true;
		this.m_isDispUpdate = false;
		this.m_callbackSetup = null;
		this.m_callbackSetupInfo = null;
		this.SetChainRequest(DailyBattleManager.REQ_TYPE.POST_RESULT, DailyBattleManager.REQ_TYPE.GET_STATUS);
	}

	// Token: 0x06004580 RID: 17792 RVA: 0x00164FF0 File Offset: 0x001631F0
	private bool CheckChainOrMultiRequestType(DailyBattleManager.REQ_TYPE reqType)
	{
		bool result = true;
		if (reqType == DailyBattleManager.REQ_TYPE.NUM || reqType == DailyBattleManager.REQ_TYPE.GET_DATA_HISTORY || reqType == DailyBattleManager.REQ_TYPE.RESET_MATCHING)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06004581 RID: 17793 RVA: 0x00165018 File Offset: 0x00163218
	private bool IsChainRequest(DailyBattleManager.REQ_TYPE reqType)
	{
		bool result = false;
		if (this.m_chainRequestKeys != null && this.m_chainRequestList.Count > 0 && this.m_chainRequestList.ContainsKey(reqType) && this.m_chainRequestList[reqType] <= 1)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06004582 RID: 17794 RVA: 0x0016506C File Offset: 0x0016326C
	private bool NextChainRequest()
	{
		bool flag = false;
		if (this.m_chainRequestKeys != null && this.m_chainRequestKeys.Count > 0)
		{
			int num = 0;
			for (int i = 0; i < this.m_chainRequestKeys.Count; i++)
			{
				DailyBattleManager.REQ_TYPE req_TYPE = this.m_chainRequestKeys[i];
				int num2 = this.m_chainRequestList[req_TYPE];
				if (num2 >= 2)
				{
					num++;
				}
				if (num2 <= 0)
				{
					this.m_chainRequestList[req_TYPE] = 1;
					this.Request(req_TYPE);
					flag = true;
					break;
				}
			}
			if (!flag && num >= this.m_chainRequestKeys.Count)
			{
				if (this.m_isFirstSetupReq)
				{
					global::Debug.Log(" FirstSetup end !!!!!!!!!!!!!!!!!!!!!!!!!!!");
				}
				if (this.m_isResultSetupReq)
				{
					global::Debug.Log(" ResultSetup end !!!!!!!!!!!!!!!!!!!!!!!!!!!");
					if (this.currentDataPair != null && this.currentDataPair.myBattleData != null && this.currentDataPair.rivalBattleData != null && !string.IsNullOrEmpty(this.currentDataPair.myBattleData.userId))
					{
						global::Debug.Log(" ResultSetup end   starTime:" + this.currentDataPair.starDateString + " endTime:" + this.currentDataPair.endDateString);
					}
				}
				this.m_isDispUpdate = true;
				this.m_isResultSetupReq = false;
				this.m_isFirstSetupReq = false;
				this.m_chainRequestList.Clear();
				this.m_chainRequestKeys.Clear();
				if (this.m_callbackSetupInfo != null)
				{
					this.m_callbackSetupInfo(this.currentStatus, this.currentDataPair, this.currentEndTime, this.currentRewardFlag, this.currentWinFlag);
				}
				if (this.m_callbackSetup != null)
				{
					this.m_callbackSetup();
				}
			}
		}
		return flag;
	}

	// Token: 0x06004583 RID: 17795 RVA: 0x00165228 File Offset: 0x00163428
	private bool SetChainRequest(DailyBattleManager.REQ_TYPE req0, DailyBattleManager.REQ_TYPE req1)
	{
		return this.SetChainRequest(new List<DailyBattleManager.REQ_TYPE>
		{
			req0,
			req1
		});
	}

	// Token: 0x06004584 RID: 17796 RVA: 0x00165250 File Offset: 0x00163450
	private bool SetChainRequest(DailyBattleManager.REQ_TYPE req0, DailyBattleManager.REQ_TYPE req1, DailyBattleManager.REQ_TYPE req2)
	{
		return this.SetChainRequest(new List<DailyBattleManager.REQ_TYPE>
		{
			req0,
			req1,
			req2
		});
	}

	// Token: 0x06004585 RID: 17797 RVA: 0x00165280 File Offset: 0x00163480
	private bool SetChainRequest(List<DailyBattleManager.REQ_TYPE> reqList)
	{
		if (reqList == null)
		{
			return false;
		}
		if (reqList.Count <= 0)
		{
			return false;
		}
		for (int i = 0; i < reqList.Count; i++)
		{
			if (!this.CheckChainOrMultiRequestType(reqList[i]))
			{
				return false;
			}
		}
		bool result = false;
		if (this.m_chainRequestList == null || this.m_chainRequestList.Count <= 0)
		{
			if (this.m_chainRequestList == null)
			{
				this.m_chainRequestList = new Dictionary<DailyBattleManager.REQ_TYPE, int>();
			}
			if (this.m_chainRequestKeys == null)
			{
				this.m_chainRequestKeys = new List<DailyBattleManager.REQ_TYPE>();
			}
			for (int j = 0; j < reqList.Count; j++)
			{
				this.m_chainRequestList.Add(reqList[j], 0);
				this.m_chainRequestKeys.Add(reqList[j]);
			}
			this.m_chainRequestList[this.m_chainRequestKeys[0]] = 1;
			this.Request(this.m_chainRequestKeys[0]);
			result = true;
		}
		return result;
	}

	// Token: 0x06004586 RID: 17798 RVA: 0x00165384 File Offset: 0x00163584
	private void SetCurrentStatus(ServerDailyBattleStatus status)
	{
		if (this.m_currentStatus == null)
		{
			this.m_currentStatus = new ServerDailyBattleStatus();
		}
		status.CopyTo(this.m_currentStatus);
		this.UpdateDataLastTime(DailyBattleManager.DATA_TYPE.STATUS, NetBase.GetCurrentTime());
	}

	// Token: 0x06004587 RID: 17799 RVA: 0x001653C0 File Offset: 0x001635C0
	private void SetCurrentDataPair(ServerDailyBattleDataPair data)
	{
		if (this.m_currentDataPair == null)
		{
			this.m_currentDataPair = new ServerDailyBattleDataPair();
		}
		data.CopyTo(this.m_currentDataPair);
		this.UpdateDataLastTime(DailyBattleManager.DATA_TYPE.DATA_PAIR, NetBase.GetCurrentTime());
	}

	// Token: 0x06004588 RID: 17800 RVA: 0x001653FC File Offset: 0x001635FC
	private void SetCurrentDataPairList(List<ServerDailyBattleDataPair> list)
	{
		if (this.m_currentDataPairList == null)
		{
			this.m_currentDataPairList = new List<ServerDailyBattleDataPair>();
		}
		else
		{
			this.m_currentDataPairList.Clear();
		}
		if (list != null && list.Count > 0)
		{
			DateTime dateTime = NetBase.GetCurrentTime();
			TimeSpan t = list[0].endTime - list[0].starTime;
			for (int i = 0; i < list.Count; i++)
			{
				ServerDailyBattleDataPair serverDailyBattleDataPair = list[i];
				if (!serverDailyBattleDataPair.isToday)
				{
					ServerDailyBattleDataPair serverDailyBattleDataPair2 = new ServerDailyBattleDataPair();
					serverDailyBattleDataPair.CopyTo(serverDailyBattleDataPair2);
					if (dateTime.Ticks > serverDailyBattleDataPair2.endTime.Ticks && t.TotalSeconds > 0.0)
					{
						TimeSpan t2 = dateTime - serverDailyBattleDataPair2.endTime;
						if (t2 >= t)
						{
							global::Debug.Log(string.Concat(new object[]
							{
								string.Empty,
								i,
								" span:",
								t2.TotalHours,
								"h  currentEnd:",
								dateTime.ToString(),
								" end:",
								serverDailyBattleDataPair2.endTime
							}));
							ServerDailyBattleDataPair item = new ServerDailyBattleDataPair(serverDailyBattleDataPair2.endTime, dateTime);
							this.m_currentDataPairList.Add(item);
						}
					}
					this.m_currentDataPairList.Add(serverDailyBattleDataPair2);
				}
				dateTime = serverDailyBattleDataPair.starTime;
			}
		}
		this.UpdateDataLastTime(DailyBattleManager.DATA_TYPE.DATA_PAIR_LIST, NetBase.GetCurrentTime());
	}

	// Token: 0x06004589 RID: 17801 RVA: 0x00165584 File Offset: 0x00163784
	private void SetCurrentPrizeList(List<ServerDailyBattlePrizeData> list)
	{
		if (this.m_currentPrizeList == null)
		{
			this.m_currentPrizeList = new List<ServerDailyBattlePrizeData>();
		}
		else
		{
			this.m_currentPrizeList.Clear();
		}
		if (list != null && list.Count > 0)
		{
			foreach (ServerDailyBattlePrizeData serverDailyBattlePrizeData in list)
			{
				ServerDailyBattlePrizeData serverDailyBattlePrizeData2 = new ServerDailyBattlePrizeData();
				serverDailyBattlePrizeData.CopyTo(serverDailyBattlePrizeData2);
				this.m_currentPrizeList.Add(serverDailyBattlePrizeData2);
			}
		}
		this.UpdateDataLastTime(DailyBattleManager.DATA_TYPE.PRIZE_LIST, NetBase.GetCurrentTime());
	}

	// Token: 0x0600458A RID: 17802 RVA: 0x0016563C File Offset: 0x0016383C
	private void SetCurrentEndTime(DateTime time)
	{
		this.m_currentEndTime = time;
		this.UpdateDataLastTime(DailyBattleManager.DATA_TYPE.END_TIME, NetBase.GetCurrentTime());
	}

	// Token: 0x0600458B RID: 17803 RVA: 0x00165654 File Offset: 0x00163854
	private void SetCurrentRewardFlag(bool flg)
	{
		if (flg || !this.m_currentRewardFlag)
		{
			this.m_currentRewardFlag = flg;
			this.UpdateDataLastTime(DailyBattleManager.DATA_TYPE.REWARD_FLAG, NetBase.GetCurrentTime());
		}
	}

	// Token: 0x0600458C RID: 17804 RVA: 0x00165688 File Offset: 0x00163888
	private void SetCurrentRewardDataPair(ServerDailyBattleDataPair rewardDataPair)
	{
		if (rewardDataPair != null || this.m_currentRewardDataPair == null)
		{
			this.m_currentRewardDataPair = rewardDataPair;
			this.UpdateDataLastTime(DailyBattleManager.DATA_TYPE.REWARD_DATA_PAIR, NetBase.GetCurrentTime());
		}
	}

	// Token: 0x0600458D RID: 17805 RVA: 0x001656BC File Offset: 0x001638BC
	private void Update()
	{
		if (!this.m_isDataInit)
		{
			this.Init();
		}
	}

	// Token: 0x0600458E RID: 17806 RVA: 0x001656D0 File Offset: 0x001638D0
	private void UpdateDataLastTime(DailyBattleManager.DATA_TYPE type, DateTime time)
	{
		if (this.m_currentDataLastUpdateTimeList == null)
		{
			this.m_currentDataLastUpdateTimeList = new Dictionary<DailyBattleManager.DATA_TYPE, DateTime>();
		}
		if (this.m_currentDataLastUpdateTimeList.ContainsKey(type))
		{
			this.m_currentDataLastUpdateTimeList[type] = time;
		}
		else
		{
			this.m_currentDataLastUpdateTimeList.Add(type, time);
		}
	}

	// Token: 0x0600458F RID: 17807 RVA: 0x00165724 File Offset: 0x00163924
	public bool IsExpirationData(DailyBattleManager.DATA_TYPE type)
	{
		bool result = false;
		if (this.m_currentDataLastUpdateTimeList == null || type == DailyBattleManager.DATA_TYPE.NUM)
		{
			return false;
		}
		if (this.m_currentDataLastUpdateTimeList.ContainsKey(type) && !GeneralUtil.IsOverTimeMinute(this.m_currentDataLastUpdateTimeList[type], 3))
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06004590 RID: 17808 RVA: 0x00165774 File Offset: 0x00163974
	private void StartRequest(DailyBattleManager.REQ_TYPE type)
	{
		if (this.m_isRequestList == null)
		{
			this.m_isRequestList = new Dictionary<DailyBattleManager.REQ_TYPE, bool>();
		}
		if (this.m_requestTimeList == null)
		{
			this.m_requestTimeList = new Dictionary<DailyBattleManager.REQ_TYPE, DateTime>();
		}
		if (type != DailyBattleManager.REQ_TYPE.NUM)
		{
			if (!this.m_isRequestList.ContainsKey(type))
			{
				this.m_isRequestList.Add(type, true);
			}
			else
			{
				this.m_isRequestList[type] = true;
			}
			if (!this.m_requestTimeList.ContainsKey(type))
			{
				this.m_requestTimeList.Add(type, NetBase.GetCurrentTime());
			}
			else
			{
				this.m_requestTimeList[type] = NetBase.GetCurrentTime();
			}
		}
	}

	// Token: 0x06004591 RID: 17809 RVA: 0x0016581C File Offset: 0x00163A1C
	private void EndRequest(DailyBattleManager.REQ_TYPE type)
	{
		if (this.m_isRequestList == null)
		{
			this.m_isRequestList = new Dictionary<DailyBattleManager.REQ_TYPE, bool>();
		}
		if (this.m_requestTimeList == null)
		{
			this.m_requestTimeList = new Dictionary<DailyBattleManager.REQ_TYPE, DateTime>();
		}
		if (type != DailyBattleManager.REQ_TYPE.NUM)
		{
			if (!this.m_isRequestList.ContainsKey(type))
			{
				this.m_isRequestList.Add(type, false);
			}
			else
			{
				this.m_isRequestList[type] = false;
			}
		}
	}

	// Token: 0x06004592 RID: 17810 RVA: 0x0016588C File Offset: 0x00163A8C
	private bool IsRequestPossible(DailyBattleManager.REQ_TYPE type)
	{
		bool result = false;
		if (this.m_isRequestList == null || this.m_requestTimeList == null)
		{
			return true;
		}
		if (type != DailyBattleManager.REQ_TYPE.NUM)
		{
			if (this.m_isRequestList.ContainsKey(type) && this.m_requestTimeList.ContainsKey(type))
			{
				if (!this.m_isRequestList[type])
				{
					if (type != DailyBattleManager.REQ_TYPE.POST_RESULT && type != DailyBattleManager.REQ_TYPE.RESET_MATCHING)
					{
						if (GeneralUtil.IsOverTimeSecond(this.m_requestTimeList[type], 2))
						{
							result = true;
						}
					}
					else
					{
						result = true;
					}
				}
				else if (type != DailyBattleManager.REQ_TYPE.POST_RESULT && type != DailyBattleManager.REQ_TYPE.RESET_MATCHING)
				{
					if (GeneralUtil.IsOverTimeSecond(this.m_requestTimeList[type], 60))
					{
						this.EndRequest(type);
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
		}
		return result;
	}

	// Token: 0x06004593 RID: 17811 RVA: 0x00165960 File Offset: 0x00163B60
	public bool IsDataReload(DailyBattleManager.DATA_TYPE type)
	{
		bool result = true;
		if (this.m_currentDataLastUpdateTimeList != null && this.m_currentDataLastUpdateTimeList.ContainsKey(type))
		{
			DateTime baseTime = this.m_currentDataLastUpdateTimeList[type];
			if (!GeneralUtil.IsOverTimeSecond(baseTime, 600))
			{
				result = false;
				if (type != DailyBattleManager.DATA_TYPE.END_TIME)
				{
					DateTime currentTime = NetBase.GetCurrentTime();
					DateTime currentEndTime = this.currentEndTime;
					if (currentTime.Ticks > currentEndTime.Ticks)
					{
						result = true;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06004594 RID: 17812 RVA: 0x001659D4 File Offset: 0x00163BD4
	private bool Request(DailyBattleManager.REQ_TYPE type)
	{
		bool result = false;
		if (this.CheckChainOrMultiRequestType(type))
		{
			result = true;
			switch (type)
			{
			case DailyBattleManager.REQ_TYPE.GET_STATUS:
				this.RequestGetStatus(null);
				break;
			case DailyBattleManager.REQ_TYPE.UPD_STATUS:
				this.RequestUpdateStatus(null);
				break;
			case DailyBattleManager.REQ_TYPE.POST_RESULT:
				this.RequestPostResult(null);
				break;
			case DailyBattleManager.REQ_TYPE.GET_PRIZE:
				this.RequestGetPrize(null);
				break;
			case DailyBattleManager.REQ_TYPE.GET_DATA:
				this.RequestGetData(null);
				break;
			default:
				result = false;
				break;
			}
		}
		return result;
	}

	// Token: 0x06004595 RID: 17813 RVA: 0x00165A5C File Offset: 0x00163C5C
	public bool RequestGetStatus(DailyBattleManager.CallbackGetStatus callback)
	{
		if (this.IsRequestGetStatus())
		{
			this.StartRequest(DailyBattleManager.REQ_TYPE.GET_STATUS);
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				this.m_callbackGetStatus = callback;
				loggedInServerInterface.RequestServerGetDailyBattleStatus(base.gameObject);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004596 RID: 17814 RVA: 0x00165AA4 File Offset: 0x00163CA4
	public bool IsRequestGetStatus()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		return loggedInServerInterface != null && this.IsRequestPossible(DailyBattleManager.REQ_TYPE.GET_STATUS);
	}

	// Token: 0x06004597 RID: 17815 RVA: 0x00165ACC File Offset: 0x00163CCC
	public bool RequestUpdateStatus(DailyBattleManager.CallbackGetStatus callback)
	{
		if (this.IsRequestUpdateStatus())
		{
			this.StartRequest(DailyBattleManager.REQ_TYPE.UPD_STATUS);
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				this.m_callbackGetStatus = callback;
				loggedInServerInterface.RequestServerUpdateDailyBattleStatus(base.gameObject);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004598 RID: 17816 RVA: 0x00165B14 File Offset: 0x00163D14
	public bool IsRequestUpdateStatus()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		return loggedInServerInterface != null && this.IsRequestPossible(DailyBattleManager.REQ_TYPE.UPD_STATUS);
	}

	// Token: 0x06004599 RID: 17817 RVA: 0x00165B3C File Offset: 0x00163D3C
	public bool RequestPostResult(DailyBattleManager.CallbackPostResult callback)
	{
		if (this.IsRequestPostResult())
		{
			this.StartRequest(DailyBattleManager.REQ_TYPE.POST_RESULT);
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				this.m_callbackPostResult = callback;
				loggedInServerInterface.RequestServerPostDailyBattleResult(base.gameObject);
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600459A RID: 17818 RVA: 0x00165B84 File Offset: 0x00163D84
	public bool IsRequestPostResult()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		return loggedInServerInterface != null && this.IsRequestPossible(DailyBattleManager.REQ_TYPE.POST_RESULT);
	}

	// Token: 0x0600459B RID: 17819 RVA: 0x00165BAC File Offset: 0x00163DAC
	public bool RequestGetData(DailyBattleManager.CallbackGetData callback)
	{
		if (this.IsRequestGetData())
		{
			this.StartRequest(DailyBattleManager.REQ_TYPE.GET_DATA);
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				this.m_callbackGetData = callback;
				loggedInServerInterface.RequestServerGetDailyBattleData(base.gameObject);
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600459C RID: 17820 RVA: 0x00165BF4 File Offset: 0x00163DF4
	public bool IsEndTimeOver()
	{
		return NetBase.GetCurrentTime().Ticks >= this.m_currentEndTime.Ticks;
	}

	// Token: 0x0600459D RID: 17821 RVA: 0x00165C24 File Offset: 0x00163E24
	public bool IsReload(DailyBattleManager.REQ_TYPE reqType, double waitMinutes = 1.0)
	{
		bool result = true;
		if (this.m_requestTimeList != null && this.m_requestTimeList.ContainsKey(reqType))
		{
			DateTime currentTime = NetBase.GetCurrentTime();
			DateTime d = this.m_requestTimeList[reqType];
			if ((currentTime - d).TotalMinutes < waitMinutes)
			{
				result = false;
			}
		}
		return result;
	}

	// Token: 0x0600459E RID: 17822 RVA: 0x00165C7C File Offset: 0x00163E7C
	public bool IsDataInit(DailyBattleManager.REQ_TYPE reqType)
	{
		bool result = false;
		if (this.m_requestTimeList != null && this.m_requestTimeList.ContainsKey(reqType))
		{
			result = true;
		}
		return result;
	}

	// Token: 0x0600459F RID: 17823 RVA: 0x00165CAC File Offset: 0x00163EAC
	public bool IsRequestGetData()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		return loggedInServerInterface != null && this.IsRequestPossible(DailyBattleManager.REQ_TYPE.GET_DATA);
	}

	// Token: 0x060045A0 RID: 17824 RVA: 0x00165CD4 File Offset: 0x00163ED4
	public bool RequestGetPrize(DailyBattleManager.CallbackGetPrize callback)
	{
		if (this.IsRequestGetPrize())
		{
			this.StartRequest(DailyBattleManager.REQ_TYPE.GET_PRIZE);
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				this.m_callbackGetPrize = callback;
				loggedInServerInterface.RequestServerGetPrizeDailyBattle(base.gameObject);
				return true;
			}
		}
		return false;
	}

	// Token: 0x060045A1 RID: 17825 RVA: 0x00165D1C File Offset: 0x00163F1C
	public bool IsRequestGetPrize()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		return loggedInServerInterface != null && this.IsRequestPossible(DailyBattleManager.REQ_TYPE.GET_PRIZE);
	}

	// Token: 0x060045A2 RID: 17826 RVA: 0x00165D44 File Offset: 0x00163F44
	public bool RequestGetDataHistory(int count, DailyBattleManager.CallbackGetDataHistory callback)
	{
		if (this.IsRequestGetDataHistory())
		{
			this.StartRequest(DailyBattleManager.REQ_TYPE.GET_DATA_HISTORY);
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				this.m_callbackGetDataHistory = callback;
				loggedInServerInterface.RequestServerGetDailyBattleDataHistory(count, base.gameObject);
				return true;
			}
		}
		return false;
	}

	// Token: 0x060045A3 RID: 17827 RVA: 0x00165D8C File Offset: 0x00163F8C
	public bool IsRequestGetDataHistory()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		return loggedInServerInterface != null && this.IsRequestPossible(DailyBattleManager.REQ_TYPE.GET_DATA_HISTORY);
	}

	// Token: 0x060045A4 RID: 17828 RVA: 0x00165DB4 File Offset: 0x00163FB4
	public bool RequestResetMatching(int type, DailyBattleManager.CallbackResetMatching callback)
	{
		if (this.IsRequestResetMatching())
		{
			this.StartRequest(DailyBattleManager.REQ_TYPE.RESET_MATCHING);
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				this.m_callbackResetMatching = callback;
				loggedInServerInterface.RequestServerResetDailyBattleMatching(type, base.gameObject);
				return true;
			}
		}
		return false;
	}

	// Token: 0x060045A5 RID: 17829 RVA: 0x00165DFC File Offset: 0x00163FFC
	public bool IsRequestResetMatching()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		return loggedInServerInterface != null && this.IsRequestPossible(DailyBattleManager.REQ_TYPE.RESET_MATCHING);
	}

	// Token: 0x060045A6 RID: 17830 RVA: 0x00165E24 File Offset: 0x00164024
	public void Dump()
	{
		global::Debug.Log("DailyBattleManager  Dump ============================================================================================");
		if (this.m_currentStatus != null)
		{
			this.m_currentStatus.Dump();
		}
		else
		{
			global::Debug.Log("ServerDailyBattleStatus  null");
		}
		if (this.m_currentDataPair != null)
		{
			this.m_currentDataPair.Dump();
		}
		else
		{
			global::Debug.Log("ServerDailyBattleDataPair  null");
		}
		if (this.m_currentDataPairList != null)
		{
			global::Debug.Log(string.Format("dataPairList:{0}", this.m_currentDataPairList.Count));
		}
		else
		{
			global::Debug.Log(string.Format("dataPairList:{0}", 0));
		}
		if (this.m_currentPrizeList != null)
		{
			global::Debug.Log(string.Format("prizeList:{0}", this.m_currentPrizeList.Count));
		}
		else
		{
			global::Debug.Log(string.Format("prizeList:{0}", 0));
		}
		global::Debug.Log(string.Format("rewardFlag:{0}", this.m_currentRewardFlag));
		global::Debug.Log(string.Format("endTime:{0}", this.m_currentEndTime.ToString()));
		global::Debug.Log("DailyBattleManager  Dump --------------------------------------------------------------------------------------------");
	}

	// Token: 0x060045A7 RID: 17831 RVA: 0x00165F4C File Offset: 0x0016414C
	private void ServerGetDailyBattleStatus_Succeeded(MsgGetDailyBattleStatusSucceed msg)
	{
		if (msg != null)
		{
			this.SetCurrentStatus(msg.battleStatus);
			this.SetCurrentEndTime(msg.endTime);
			this.EndRequest(DailyBattleManager.REQ_TYPE.GET_STATUS);
			if (this.IsChainRequest(DailyBattleManager.REQ_TYPE.GET_STATUS))
			{
				this.m_chainRequestList[DailyBattleManager.REQ_TYPE.GET_STATUS] = 2;
				this.NextChainRequest();
			}
			if (this.m_callbackGetStatus != null)
			{
				this.m_callbackGetStatus(msg.battleStatus, msg.endTime, false, null);
			}
		}
		this.m_callbackGetStatus = null;
		if (this.m_showLog)
		{
			global::Debug.Log("DailyBattleManager ServerGetDailyBattleStatus_Succeeded !!!!!");
			this.Dump();
		}
	}

	// Token: 0x060045A8 RID: 17832 RVA: 0x00165FE4 File Offset: 0x001641E4
	private void ServerGetDailyBattleStatus_Failed(MsgServerConnctFailed msg)
	{
		global::Debug.Log("DailyBattleManager ServerGetDailyBattleStatus_Failed !!!!!");
		this.EndRequest(DailyBattleManager.REQ_TYPE.GET_STATUS);
		if (this.IsChainRequest(DailyBattleManager.REQ_TYPE.GET_STATUS))
		{
			this.m_chainRequestList[DailyBattleManager.REQ_TYPE.GET_STATUS] = 3;
			this.NextChainRequest();
		}
		if (this.m_callbackGetStatus != null)
		{
			this.m_callbackGetStatus(null, NetBase.GetCurrentTime(), false, null);
		}
		this.m_callbackGetStatus = null;
	}

	// Token: 0x060045A9 RID: 17833 RVA: 0x00166048 File Offset: 0x00164248
	private void ServerUpdateDailyBattleStatus_Succeeded(MsgUpdateDailyBattleStatusSucceed msg)
	{
		if (msg != null)
		{
			this.SetCurrentStatus(msg.battleStatus);
			this.SetCurrentEndTime(msg.endTime);
			this.SetCurrentRewardFlag(msg.rewardFlag);
			this.SetCurrentRewardDataPair(msg.rewardBattleDataPair);
			this.EndRequest(DailyBattleManager.REQ_TYPE.UPD_STATUS);
			if (this.IsChainRequest(DailyBattleManager.REQ_TYPE.UPD_STATUS))
			{
				this.m_chainRequestList[DailyBattleManager.REQ_TYPE.UPD_STATUS] = 2;
				this.NextChainRequest();
			}
			if (this.m_callbackGetStatus != null)
			{
				this.m_callbackGetStatus(msg.battleStatus, msg.endTime, msg.rewardFlag, msg.rewardBattleDataPair);
			}
		}
		this.m_callbackGetStatus = null;
		if (this.m_showLog)
		{
			global::Debug.Log("DailyBattleManager ServerUpdateDailyBattleStatus_Succeeded !!!!!");
			this.Dump();
		}
	}

	// Token: 0x060045AA RID: 17834 RVA: 0x00166104 File Offset: 0x00164304
	private void ServerUpdateDailyBattleStatus_Failed(MsgServerConnctFailed msg)
	{
		global::Debug.Log("DailyBattleManager ServerUpdateDailyBattleStatus_Failed !!!!!");
		this.EndRequest(DailyBattleManager.REQ_TYPE.UPD_STATUS);
		if (this.IsChainRequest(DailyBattleManager.REQ_TYPE.UPD_STATUS))
		{
			this.m_chainRequestList[DailyBattleManager.REQ_TYPE.UPD_STATUS] = 3;
			this.NextChainRequest();
		}
		if (this.m_callbackGetStatus != null)
		{
			this.m_callbackGetStatus(null, NetBase.GetCurrentTime(), false, null);
		}
		this.m_callbackGetStatus = null;
	}

	// Token: 0x060045AB RID: 17835 RVA: 0x00166168 File Offset: 0x00164368
	private void ServerPostDailyBattleResult_Succeeded(MsgPostDailyBattleResultSucceed msg)
	{
		if (msg != null)
		{
			this.SetCurrentDataPair(msg.battleDataPair);
			this.SetCurrentRewardFlag(msg.rewardFlag);
			this.SetCurrentRewardDataPair(msg.rewardBattleDataPair);
			this.EndRequest(DailyBattleManager.REQ_TYPE.POST_RESULT);
			if (this.IsChainRequest(DailyBattleManager.REQ_TYPE.POST_RESULT))
			{
				this.m_chainRequestList[DailyBattleManager.REQ_TYPE.POST_RESULT] = 2;
				this.NextChainRequest();
			}
			if (this.m_callbackPostResult != null)
			{
				this.m_callbackPostResult(msg.battleDataPair, msg.rewardFlag, msg.rewardBattleDataPair);
			}
		}
		this.m_callbackPostResult = null;
		if (this.m_showLog)
		{
			global::Debug.Log("DailyBattleManager ServerPostDailyBattleResult_Succeeded !!!!!");
			this.Dump();
		}
	}

	// Token: 0x060045AC RID: 17836 RVA: 0x00166210 File Offset: 0x00164410
	private void ServerPostDailyBattleResult_Failed(MsgServerConnctFailed msg)
	{
		global::Debug.Log("DailyBattleManager ServerPostDailyBattleResult_Failed !!!!!");
		this.EndRequest(DailyBattleManager.REQ_TYPE.POST_RESULT);
		if (this.IsChainRequest(DailyBattleManager.REQ_TYPE.POST_RESULT))
		{
			this.m_chainRequestList[DailyBattleManager.REQ_TYPE.POST_RESULT] = 3;
			this.NextChainRequest();
		}
		if (this.m_callbackPostResult != null)
		{
			this.m_callbackPostResult(null, false, null);
		}
		this.m_callbackPostResult = null;
	}

	// Token: 0x060045AD RID: 17837 RVA: 0x00166270 File Offset: 0x00164470
	private void ServerGetDailyBattleData_Succeeded(MsgGetDailyBattleDataSucceed msg)
	{
		if (msg != null)
		{
			this.SetCurrentDataPair(msg.battleDataPair);
			this.EndRequest(DailyBattleManager.REQ_TYPE.GET_DATA);
			if (this.IsChainRequest(DailyBattleManager.REQ_TYPE.GET_DATA))
			{
				this.m_chainRequestList[DailyBattleManager.REQ_TYPE.GET_DATA] = 2;
				this.NextChainRequest();
			}
			if (this.m_callbackGetData != null)
			{
				this.m_callbackGetData(msg.battleDataPair);
			}
		}
		this.m_callbackGetData = null;
		if (this.m_showLog)
		{
			global::Debug.Log("DailyBattleManager ServerGetDailyBattleData_Succeeded !!!!!");
			this.Dump();
		}
	}

	// Token: 0x060045AE RID: 17838 RVA: 0x001662F4 File Offset: 0x001644F4
	private void ServerGetDailyBattleData_Failed(MsgServerConnctFailed msg)
	{
		global::Debug.Log("DailyBattleManager ServerGetDailyBattleData_Failed !!!!!");
		this.EndRequest(DailyBattleManager.REQ_TYPE.GET_DATA);
		if (this.IsChainRequest(DailyBattleManager.REQ_TYPE.GET_DATA))
		{
			this.m_chainRequestList[DailyBattleManager.REQ_TYPE.GET_DATA] = 3;
			this.NextChainRequest();
		}
		if (this.m_callbackGetData != null)
		{
			this.m_callbackGetData(null);
		}
		this.m_callbackGetData = null;
	}

	// Token: 0x060045AF RID: 17839 RVA: 0x00166350 File Offset: 0x00164550
	private void ServerGetPrizeDailyBattle_Succeeded(MsgGetPrizeDailyBattleSucceed msg)
	{
		if (msg != null)
		{
			this.SetCurrentPrizeList(msg.battlePrizeDataList);
			this.EndRequest(DailyBattleManager.REQ_TYPE.GET_PRIZE);
			if (this.IsChainRequest(DailyBattleManager.REQ_TYPE.GET_PRIZE))
			{
				this.m_chainRequestList[DailyBattleManager.REQ_TYPE.GET_PRIZE] = 2;
				this.NextChainRequest();
			}
			if (this.m_callbackGetPrize != null)
			{
				this.m_callbackGetPrize(msg.battlePrizeDataList);
			}
		}
		this.m_callbackGetPrize = null;
		if (this.m_showLog)
		{
			global::Debug.Log("DailyBattleManager ServerGetPrizeDailyBattle_Succeeded !!!!!");
			this.Dump();
		}
	}

	// Token: 0x060045B0 RID: 17840 RVA: 0x001663D4 File Offset: 0x001645D4
	private void ServerGetPrizeDailyBattle_Failed(MsgServerConnctFailed msg)
	{
		global::Debug.Log("DailyBattleManager ServerGetPrizeDailyBattle_Failed !!!!!");
		this.EndRequest(DailyBattleManager.REQ_TYPE.GET_PRIZE);
		if (this.IsChainRequest(DailyBattleManager.REQ_TYPE.GET_PRIZE))
		{
			this.m_chainRequestList[DailyBattleManager.REQ_TYPE.GET_PRIZE] = 3;
			this.NextChainRequest();
		}
		if (this.m_callbackGetPrize != null)
		{
			this.m_callbackGetPrize(null);
		}
		this.m_callbackGetPrize = null;
	}

	// Token: 0x060045B1 RID: 17841 RVA: 0x00166430 File Offset: 0x00164630
	private void ServerGetDailyBattleDataHistory_Succeeded(MsgGetDailyBattleDataHistorySucceed msg)
	{
		if (msg != null)
		{
			this.SetCurrentDataPairList(msg.battleDataPairList);
			this.EndRequest(DailyBattleManager.REQ_TYPE.GET_DATA_HISTORY);
			if (this.m_callbackGetDataHistory != null)
			{
				this.m_callbackGetDataHistory(msg.battleDataPairList);
			}
		}
		this.m_callbackGetDataHistory = null;
		if (this.m_showLog)
		{
			global::Debug.Log("DailyBattleManager ServerGetDailyBattleDataHistory_Succeeded !!!!!");
			this.Dump();
		}
	}

	// Token: 0x060045B2 RID: 17842 RVA: 0x00166494 File Offset: 0x00164694
	private void ServerGetDailyBattleDataHistory_Failed(MsgServerConnctFailed msg)
	{
		global::Debug.Log("DailyBattleManager ServerGetDailyBattleDataHistory_Failed !!!!!");
		this.EndRequest(DailyBattleManager.REQ_TYPE.GET_DATA_HISTORY);
		if (this.m_callbackGetDataHistory != null)
		{
			this.m_callbackGetDataHistory(null);
		}
		this.m_callbackGetDataHistory = null;
	}

	// Token: 0x060045B3 RID: 17843 RVA: 0x001664C8 File Offset: 0x001646C8
	private void ServerResetDailyBattleMatching_Succeeded(MsgResetDailyBattleMatchingSucceed msg)
	{
		if (msg != null)
		{
			this.SetCurrentDataPair(msg.battleDataPair);
			this.SetCurrentEndTime(msg.endTime);
			this.EndRequest(DailyBattleManager.REQ_TYPE.RESET_MATCHING);
			if (this.m_callbackResetMatching != null)
			{
				this.m_callbackResetMatching(msg.playerState, msg.battleDataPair, msg.endTime);
			}
		}
		this.m_callbackResetMatching = null;
		if (this.m_showLog)
		{
			global::Debug.Log("DailyBattleManager ServerResetDailyBattleMatching_Succeeded !!!!!");
			this.Dump();
		}
	}

	// Token: 0x060045B4 RID: 17844 RVA: 0x00166544 File Offset: 0x00164744
	private void ServerResetDailyBattleMatching_Failed(MsgServerConnctFailed msg)
	{
		global::Debug.Log("DailyBattleManager ServerResetDailyBattleMatching_Failed !!!!!");
		this.EndRequest(DailyBattleManager.REQ_TYPE.RESET_MATCHING);
		if (this.m_callbackResetMatching != null)
		{
			this.m_callbackResetMatching(null, null, NetBase.GetCurrentTime());
		}
		this.m_callbackResetMatching = null;
	}

	// Token: 0x040039EA RID: 14826
	private const int REQUEST_LOAD_DELAY = 2;

	// Token: 0x040039EB RID: 14827
	private const int REQUEST_RELOAD_DELAY = 60;

	// Token: 0x040039EC RID: 14828
	private const int DATA_AUTO_RELOAD_TIME = 600;

	// Token: 0x040039ED RID: 14829
	[SerializeField]
	private bool m_showLog;

	// Token: 0x040039EE RID: 14830
	private DailyBattleManager.CallbackGetStatus m_callbackGetStatus;

	// Token: 0x040039EF RID: 14831
	private DailyBattleManager.CallbackPostResult m_callbackPostResult;

	// Token: 0x040039F0 RID: 14832
	private DailyBattleManager.CallbackGetData m_callbackGetData;

	// Token: 0x040039F1 RID: 14833
	private DailyBattleManager.CallbackGetDataHistory m_callbackGetDataHistory;

	// Token: 0x040039F2 RID: 14834
	private DailyBattleManager.CallbackGetPrize m_callbackGetPrize;

	// Token: 0x040039F3 RID: 14835
	private DailyBattleManager.CallbackResetMatching m_callbackResetMatching;

	// Token: 0x040039F4 RID: 14836
	private DailyBattleManager.CallbackSetupInfo m_callbackSetupInfo;

	// Token: 0x040039F5 RID: 14837
	private DailyBattleManager.CallbackSetup m_callbackSetup;

	// Token: 0x040039F6 RID: 14838
	private Dictionary<DailyBattleManager.REQ_TYPE, bool> m_isRequestList;

	// Token: 0x040039F7 RID: 14839
	private Dictionary<DailyBattleManager.REQ_TYPE, DateTime> m_requestTimeList;

	// Token: 0x040039F8 RID: 14840
	private bool m_isDataInit;

	// Token: 0x040039F9 RID: 14841
	private bool m_isFirstSetupReq;

	// Token: 0x040039FA RID: 14842
	private bool m_isResultSetupReq;

	// Token: 0x040039FB RID: 14843
	private bool m_isDispUpdate;

	// Token: 0x040039FC RID: 14844
	private DateTime m_dataInitTime;

	// Token: 0x040039FD RID: 14845
	private ServerDailyBattleStatus m_currentStatus;

	// Token: 0x040039FE RID: 14846
	private ServerDailyBattleDataPair m_currentDataPair;

	// Token: 0x040039FF RID: 14847
	private List<ServerDailyBattleDataPair> m_currentDataPairList;

	// Token: 0x04003A00 RID: 14848
	private List<ServerDailyBattlePrizeData> m_currentPrizeList;

	// Token: 0x04003A01 RID: 14849
	private DateTime m_currentEndTime;

	// Token: 0x04003A02 RID: 14850
	private bool m_currentRewardFlag;

	// Token: 0x04003A03 RID: 14851
	private ServerDailyBattleDataPair m_currentRewardDataPair;

	// Token: 0x04003A04 RID: 14852
	private Dictionary<int, ServerConsumedCostData> m_resetCostList;

	// Token: 0x04003A05 RID: 14853
	private Dictionary<DailyBattleManager.REQ_TYPE, int> m_chainRequestList;

	// Token: 0x04003A06 RID: 14854
	private List<DailyBattleManager.REQ_TYPE> m_chainRequestKeys;

	// Token: 0x04003A07 RID: 14855
	private Dictionary<DailyBattleManager.DATA_TYPE, DateTime> m_currentDataLastUpdateTimeList;

	// Token: 0x02000A37 RID: 2615
	public enum REQ_TYPE
	{
		// Token: 0x04003A09 RID: 14857
		GET_STATUS,
		// Token: 0x04003A0A RID: 14858
		UPD_STATUS,
		// Token: 0x04003A0B RID: 14859
		POST_RESULT,
		// Token: 0x04003A0C RID: 14860
		GET_PRIZE,
		// Token: 0x04003A0D RID: 14861
		GET_DATA,
		// Token: 0x04003A0E RID: 14862
		GET_DATA_HISTORY,
		// Token: 0x04003A0F RID: 14863
		RESET_MATCHING,
		// Token: 0x04003A10 RID: 14864
		NUM
	}

	// Token: 0x02000A38 RID: 2616
	public enum DATA_TYPE
	{
		// Token: 0x04003A12 RID: 14866
		STATUS,
		// Token: 0x04003A13 RID: 14867
		DATA_PAIR,
		// Token: 0x04003A14 RID: 14868
		DATA_PAIR_LIST,
		// Token: 0x04003A15 RID: 14869
		PRIZE_LIST,
		// Token: 0x04003A16 RID: 14870
		END_TIME,
		// Token: 0x04003A17 RID: 14871
		REWARD_FLAG,
		// Token: 0x04003A18 RID: 14872
		REWARD_DATA_PAIR,
		// Token: 0x04003A19 RID: 14873
		NUM
	}

	// Token: 0x02000AAC RID: 2732
	// (Invoke) Token: 0x060048EA RID: 18666
	public delegate void CallbackGetStatus(ServerDailyBattleStatus status, DateTime endTime, bool rewardFlag, ServerDailyBattleDataPair rewardDataPair);

	// Token: 0x02000AAD RID: 2733
	// (Invoke) Token: 0x060048EE RID: 18670
	public delegate void CallbackPostResult(ServerDailyBattleDataPair dataPair, bool rewardFlag, ServerDailyBattleDataPair rewardDataPair);

	// Token: 0x02000AAE RID: 2734
	// (Invoke) Token: 0x060048F2 RID: 18674
	public delegate void CallbackGetData(ServerDailyBattleDataPair dataPair);

	// Token: 0x02000AAF RID: 2735
	// (Invoke) Token: 0x060048F6 RID: 18678
	public delegate void CallbackGetDataHistory(List<ServerDailyBattleDataPair> dataPairList);

	// Token: 0x02000AB0 RID: 2736
	// (Invoke) Token: 0x060048FA RID: 18682
	public delegate void CallbackGetPrize(List<ServerDailyBattlePrizeData> prizeList);

	// Token: 0x02000AB1 RID: 2737
	// (Invoke) Token: 0x060048FE RID: 18686
	public delegate void CallbackResetMatching(ServerPlayerState playerStatus, ServerDailyBattleDataPair dataPair, DateTime endTime);

	// Token: 0x02000AB2 RID: 2738
	// (Invoke) Token: 0x06004902 RID: 18690
	public delegate void CallbackSetupInfo(ServerDailyBattleStatus status, ServerDailyBattleDataPair dataPair, DateTime endTime, bool rewardFlag, int winFlag);

	// Token: 0x02000AB3 RID: 2739
	// (Invoke) Token: 0x06004906 RID: 18694
	public delegate void CallbackSetup();
}
