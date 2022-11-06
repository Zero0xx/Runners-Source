using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x0200071A RID: 1818
public class NetServerGetMenuData : NetBase
{
	// Token: 0x0600305A RID: 12378 RVA: 0x00114A50 File Offset: 0x00112C50
	protected override void DoRequest()
	{
		base.SetAction("Game/getMenuData");
	}

	// Token: 0x0600305B RID: 12379 RVA: 0x00114A60 File Offset: 0x00112C60
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
		this.GetResponse_WheelOptions(jdata);
		this.GetResponse_ChaoWheelOptions(jdata);
		this.GetResponse_SubCharaRingExchange(jdata);
		this.GetResponse_DailyMission(jdata);
		this.GetResponse_MileageState(jdata);
		this.GetResponse_MessageList(jdata);
		this.GetResponse_RSRingItemRsRringStateList(jdata);
		this.GetResponse_RSRingItemRingStateList(jdata);
		this.GetResponse_RSRingItemEnergyStateList(jdata);
		this.GetResponse_MonthPurchase(jdata);
		this.GetResponse_Birthday(jdata);
		this.GetResponse_ConsumedCostList(jdata);
		NetUtil.GetResponse_CampaignList(jdata);
	}

	// Token: 0x0600305C RID: 12380 RVA: 0x00114AD0 File Offset: 0x00112CD0
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000668 RID: 1640
	// (get) Token: 0x0600305D RID: 12381 RVA: 0x00114AD4 File Offset: 0x00112CD4
	public ServerPlayerState PlayerState
	{
		get
		{
			return this.m_playerState;
		}
	}

	// Token: 0x17000669 RID: 1641
	// (get) Token: 0x0600305E RID: 12382 RVA: 0x00114ADC File Offset: 0x00112CDC
	public ServerWheelOptions WheelOptions
	{
		get
		{
			return this.m_wheelOptions;
		}
	}

	// Token: 0x1700066A RID: 1642
	// (get) Token: 0x0600305F RID: 12383 RVA: 0x00114AE4 File Offset: 0x00112CE4
	public ServerChaoWheelOptions ChaoWheelOptions
	{
		get
		{
			return this.m_chaoWheelOptions;
		}
	}

	// Token: 0x1700066B RID: 1643
	// (get) Token: 0x06003060 RID: 12384 RVA: 0x00114AEC File Offset: 0x00112CEC
	public int SubCharaRingExchange
	{
		get
		{
			return this.m_subCharaRingExchange;
		}
	}

	// Token: 0x1700066C RID: 1644
	// (get) Token: 0x06003061 RID: 12385 RVA: 0x00114AF4 File Offset: 0x00112CF4
	public ServerDailyChallengeState DailyChallengeState
	{
		get
		{
			return this.m_dailyChallengeIncentive;
		}
	}

	// Token: 0x1700066D RID: 1645
	// (get) Token: 0x06003062 RID: 12386 RVA: 0x00114AFC File Offset: 0x00112CFC
	public ServerMileageMapState MileageMapState
	{
		get
		{
			return this.m_resultMileageMapState;
		}
	}

	// Token: 0x1700066E RID: 1646
	// (get) Token: 0x06003063 RID: 12387 RVA: 0x00114B04 File Offset: 0x00112D04
	public List<ServerMileageFriendEntry> MileageFriendEntryList
	{
		get
		{
			return this.m_resultMileageFriendList;
		}
	}

	// Token: 0x1700066F RID: 1647
	// (get) Token: 0x06003064 RID: 12388 RVA: 0x00114B0C File Offset: 0x00112D0C
	public List<ServerMessageEntry> MessageEntryList
	{
		get
		{
			return this.m_messageEntryList;
		}
	}

	// Token: 0x17000670 RID: 1648
	// (get) Token: 0x06003065 RID: 12389 RVA: 0x00114B14 File Offset: 0x00112D14
	public List<ServerOperatorMessageEntry> OperatorMessageEntryList
	{
		get
		{
			return this.m_operatorMessageEntryList;
		}
	}

	// Token: 0x17000671 RID: 1649
	// (get) Token: 0x06003066 RID: 12390 RVA: 0x00114B1C File Offset: 0x00112D1C
	public int TotalMessage
	{
		get
		{
			return this.m_totalMessage;
		}
	}

	// Token: 0x17000672 RID: 1650
	// (get) Token: 0x06003067 RID: 12391 RVA: 0x00114B24 File Offset: 0x00112D24
	public int TotalOperatorMessage
	{
		get
		{
			return this.m_totalOperatorMessage;
		}
	}

	// Token: 0x17000673 RID: 1651
	// (get) Token: 0x06003068 RID: 12392 RVA: 0x00114B2C File Offset: 0x00112D2C
	public List<ServerRedStarItemState> RedstarItemStateList
	{
		get
		{
			return this.m_redstarItemStateList;
		}
	}

	// Token: 0x17000674 RID: 1652
	// (get) Token: 0x06003069 RID: 12393 RVA: 0x00114B34 File Offset: 0x00112D34
	public List<ServerRedStarItemState> RingItemStateList
	{
		get
		{
			return this.m_ringItemStateList;
		}
	}

	// Token: 0x17000675 RID: 1653
	// (get) Token: 0x0600306A RID: 12394 RVA: 0x00114B3C File Offset: 0x00112D3C
	public List<ServerRedStarItemState> EnergyItemStateList
	{
		get
		{
			return this.m_energyItemStateList;
		}
	}

	// Token: 0x17000676 RID: 1654
	// (get) Token: 0x0600306B RID: 12395 RVA: 0x00114B44 File Offset: 0x00112D44
	public int RedstarTotalItems
	{
		get
		{
			return this.m_redstarTotalItems;
		}
	}

	// Token: 0x17000677 RID: 1655
	// (get) Token: 0x0600306C RID: 12396 RVA: 0x00114B4C File Offset: 0x00112D4C
	public int RingTotalItems
	{
		get
		{
			return this.m_ringTotalItems;
		}
	}

	// Token: 0x17000678 RID: 1656
	// (get) Token: 0x0600306D RID: 12397 RVA: 0x00114B54 File Offset: 0x00112D54
	public int EnergyTotalItems
	{
		get
		{
			return this.m_energyTotalItems;
		}
	}

	// Token: 0x17000679 RID: 1657
	// (get) Token: 0x0600306E RID: 12398 RVA: 0x00114B5C File Offset: 0x00112D5C
	public int MonthPurchase
	{
		get
		{
			return this.m_monthPurchase;
		}
	}

	// Token: 0x1700067A RID: 1658
	// (get) Token: 0x0600306F RID: 12399 RVA: 0x00114B64 File Offset: 0x00112D64
	public string BirthDay
	{
		get
		{
			return this.m_birthDay;
		}
	}

	// Token: 0x1700067B RID: 1659
	// (get) Token: 0x06003070 RID: 12400 RVA: 0x00114B6C File Offset: 0x00112D6C
	public List<ServerConsumedCostData> ConsumedCostList
	{
		get
		{
			return this.m_consumedCostList;
		}
	}

	// Token: 0x06003071 RID: 12401 RVA: 0x00114B74 File Offset: 0x00112D74
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.m_playerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}

	// Token: 0x06003072 RID: 12402 RVA: 0x00114B88 File Offset: 0x00112D88
	private void GetResponse_WheelOptions(JsonData jdata)
	{
		this.m_wheelOptions = NetUtil.AnalyzeWheelOptionsJson(jdata, "wheelOptions");
		if (this.m_wheelOptions != null && this.m_wheelOptions.m_numJackpotRing > 0)
		{
			RouletteManager.numJackpotRing = this.m_wheelOptions.m_numJackpotRing;
			Debug.Log("numJackpotRing : " + RouletteManager.numJackpotRing);
		}
	}

	// Token: 0x06003073 RID: 12403 RVA: 0x00114BEC File Offset: 0x00112DEC
	private void GetResponse_ChaoWheelOptions(JsonData jdata)
	{
		this.m_chaoWheelOptions = NetUtil.AnalyzeChaoWheelOptionsJson(jdata, "chaoWheelOptions");
	}

	// Token: 0x06003074 RID: 12404 RVA: 0x00114C00 File Offset: 0x00112E00
	private void GetResponse_SubCharaRingExchange(JsonData jdata)
	{
		this.m_subCharaRingExchange = NetUtil.GetJsonInt(jdata, "subCharaRingExchange");
	}

	// Token: 0x06003075 RID: 12405 RVA: 0x00114C14 File Offset: 0x00112E14
	private void GetResponse_DailyMission(JsonData jdata)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "dailyChallengeIncentive");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerDailyChallengeIncentive item = NetUtil.AnalyzeDailyMissionIncentiveJson(jdata2, string.Empty);
			this.m_dailyChallengeIncentive.m_incentiveList.Add(item);
		}
		this.m_dailyChallengeIncentive.m_numIncentiveCont = NetUtil.GetJsonInt(jdata, "numDailyChalCont");
		this.m_dailyChallengeIncentive.m_numDailyChalDay = NetUtil.GetJsonInt(jdata, "numDailyChalDay");
	}

	// Token: 0x06003076 RID: 12406 RVA: 0x00114C9C File Offset: 0x00112E9C
	private void GetResponse_MileageState(JsonData jdata)
	{
		this.m_resultMileageMapState = NetUtil.AnalyzeMileageMapStateJson(jdata, "mileageMapState");
	}

	// Token: 0x06003077 RID: 12407 RVA: 0x00114CB0 File Offset: 0x00112EB0
	private void GetResponse_MileageFriendList(JsonData jdata)
	{
		this.m_resultMileageFriendList = NetUtil.AnalyzeMileageFriendListJson(jdata, "mileageFriendList");
	}

	// Token: 0x06003078 RID: 12408 RVA: 0x00114CC4 File Offset: 0x00112EC4
	private void GetResponse_MessageList(JsonData jdata)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "messageList");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerMessageEntry item = NetUtil.AnalyzeMessageEntryJson(jdata2, string.Empty);
			this.m_messageEntryList.Add(item);
		}
		JsonData jsonArray2 = NetUtil.GetJsonArray(jdata, "operatorMessageList");
		int count2 = jsonArray2.Count;
		for (int j = 0; j < count2; j++)
		{
			JsonData jdata3 = jsonArray2[j];
			ServerOperatorMessageEntry item2 = NetUtil.AnalyzeOperatorMessageEntryJson(jdata3, string.Empty);
			this.m_operatorMessageEntryList.Add(item2);
		}
		this.m_totalMessage = NetUtil.GetJsonInt(jdata, "totalMessage");
		this.m_totalOperatorMessage = NetUtil.GetJsonInt(jdata, "totalOperatorMessage");
	}

	// Token: 0x06003079 RID: 12409 RVA: 0x00114D90 File Offset: 0x00112F90
	private void GetResponse_RSRingItemRsRringStateList(JsonData jdata)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "redstarItemList");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerRedStarItemState item = NetUtil.AnalyzeRedStarItemStateJson(jdata2, string.Empty);
			this.m_redstarItemStateList.Add(item);
		}
		this.m_redstarTotalItems = NetUtil.GetJsonInt(jdata, "redstarTotalItems");
	}

	// Token: 0x0600307A RID: 12410 RVA: 0x00114DF8 File Offset: 0x00112FF8
	private void GetResponse_RSRingItemRingStateList(JsonData jdata)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "ringItemList");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerRedStarItemState item = NetUtil.AnalyzeRedStarItemStateJson(jdata2, string.Empty);
			this.m_ringItemStateList.Add(item);
		}
		this.m_ringTotalItems = NetUtil.GetJsonInt(jdata, "ringTotalItems");
	}

	// Token: 0x0600307B RID: 12411 RVA: 0x00114E60 File Offset: 0x00113060
	private void GetResponse_RSRingItemEnergyStateList(JsonData jdata)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "energyItemList");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerRedStarItemState item = NetUtil.AnalyzeRedStarItemStateJson(jdata2, string.Empty);
			this.m_energyItemStateList.Add(item);
		}
		this.m_energyTotalItems = NetUtil.GetJsonInt(jdata, "energyTotalItems");
	}

	// Token: 0x0600307C RID: 12412 RVA: 0x00114EC8 File Offset: 0x001130C8
	private void GetResponse_MonthPurchase(JsonData jdata)
	{
		this.m_monthPurchase = NetUtil.GetJsonInt(jdata, "monthPurchase");
	}

	// Token: 0x0600307D RID: 12413 RVA: 0x00114EDC File Offset: 0x001130DC
	private void GetResponse_Birthday(JsonData jdata)
	{
		this.m_birthDay = NetUtil.GetJsonString(jdata, "birthday");
	}

	// Token: 0x0600307E RID: 12414 RVA: 0x00114EF0 File Offset: 0x001130F0
	private void GetResponse_ConsumedCostList(JsonData jdata)
	{
		this.m_consumedCostList = NetUtil.AnalyzeConsumedCostDataList(jdata);
	}

	// Token: 0x04002AD9 RID: 10969
	private ServerPlayerState m_playerState = new ServerPlayerState();

	// Token: 0x04002ADA RID: 10970
	private ServerWheelOptions m_wheelOptions = new ServerWheelOptions(null);

	// Token: 0x04002ADB RID: 10971
	private ServerChaoWheelOptions m_chaoWheelOptions = new ServerChaoWheelOptions();

	// Token: 0x04002ADC RID: 10972
	private int m_subCharaRingExchange;

	// Token: 0x04002ADD RID: 10973
	private ServerDailyChallengeState m_dailyChallengeIncentive = new ServerDailyChallengeState();

	// Token: 0x04002ADE RID: 10974
	private ServerMileageMapState m_resultMileageMapState = new ServerMileageMapState();

	// Token: 0x04002ADF RID: 10975
	private List<ServerMileageFriendEntry> m_resultMileageFriendList = new List<ServerMileageFriendEntry>();

	// Token: 0x04002AE0 RID: 10976
	private List<ServerMessageEntry> m_messageEntryList = new List<ServerMessageEntry>();

	// Token: 0x04002AE1 RID: 10977
	private List<ServerOperatorMessageEntry> m_operatorMessageEntryList = new List<ServerOperatorMessageEntry>();

	// Token: 0x04002AE2 RID: 10978
	private int m_totalMessage;

	// Token: 0x04002AE3 RID: 10979
	private int m_totalOperatorMessage;

	// Token: 0x04002AE4 RID: 10980
	private List<ServerRedStarItemState> m_redstarItemStateList = new List<ServerRedStarItemState>();

	// Token: 0x04002AE5 RID: 10981
	private int m_redstarTotalItems;

	// Token: 0x04002AE6 RID: 10982
	private List<ServerRedStarItemState> m_ringItemStateList = new List<ServerRedStarItemState>();

	// Token: 0x04002AE7 RID: 10983
	private int m_ringTotalItems;

	// Token: 0x04002AE8 RID: 10984
	private List<ServerRedStarItemState> m_energyItemStateList = new List<ServerRedStarItemState>();

	// Token: 0x04002AE9 RID: 10985
	private int m_energyTotalItems;

	// Token: 0x04002AEA RID: 10986
	private int m_monthPurchase;

	// Token: 0x04002AEB RID: 10987
	private string m_birthDay = string.Empty;

	// Token: 0x04002AEC RID: 10988
	private List<ServerConsumedCostData> m_consumedCostList = new List<ServerConsumedCostData>();
}
