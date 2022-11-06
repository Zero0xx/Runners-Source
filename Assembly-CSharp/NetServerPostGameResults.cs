using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x0200071E RID: 1822
public class NetServerPostGameResults : NetBase
{
	// Token: 0x0600309C RID: 12444 RVA: 0x0011528C File Offset: 0x0011348C
	public NetServerPostGameResults(ServerGameResults gameResults)
	{
		this.m_paramGameResults = gameResults;
	}

	// Token: 0x0600309D RID: 12445 RVA: 0x001152B4 File Offset: 0x001134B4
	protected override void DoRequest()
	{
		base.SetAction("Game/postGameResults");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string postGameResultString = instance.GetPostGameResultString(this.m_paramGameResults);
			Debug.Log("NetServerPostGameResults.json = " + postGameResultString);
			base.WriteJsonString(postGameResultString);
		}
	}

	// Token: 0x0600309E RID: 12446 RVA: 0x00115304 File Offset: 0x00113504
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
		this.GetResponse_CharacterState(jdata);
		this.GetResponse_ChaoState(jdata);
		this.GetResponse_PlayCharacterState(jdata);
		this.GetResponse_MileageMapState(jdata);
		this.GetResponse_DailyMissionIncentives(jdata);
		this.GetResponse_MileageIncentives(jdata);
		this.GetResponse_MessageList(jdata);
		this.GetResponse_Event(jdata);
		this.GetResponse_WheelOptions(jdata);
	}

	// Token: 0x0600309F RID: 12447 RVA: 0x00115358 File Offset: 0x00113558
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000681 RID: 1665
	// (get) Token: 0x060030A0 RID: 12448 RVA: 0x0011535C File Offset: 0x0011355C
	// (set) Token: 0x060030A1 RID: 12449 RVA: 0x00115364 File Offset: 0x00113564
	public ServerGameResults m_paramGameResults { get; set; }

	// Token: 0x060030A2 RID: 12450 RVA: 0x00115370 File Offset: 0x00113570
	private void SetParameter_Suspended()
	{
		base.WriteActionParamValue("closed", (!this.m_paramGameResults.m_isSuspended) ? 0 : 1);
	}

	// Token: 0x060030A3 RID: 12451 RVA: 0x0011539C File Offset: 0x0011359C
	private void SetParameter_Score()
	{
		base.WriteActionParamValue("score", this.m_paramGameResults.m_score);
		base.WriteActionParamValue("stageMaxScore", this.m_paramGameResults.m_maxChapterScore);
	}

	// Token: 0x060030A4 RID: 12452 RVA: 0x001153E0 File Offset: 0x001135E0
	private void SetParameter_Rings()
	{
		base.WriteActionParamValue("numRings", this.m_paramGameResults.m_numRings);
		base.WriteActionParamValue("numFailureRings", this.m_paramGameResults.m_numFailureRings);
		base.WriteActionParamValue("numRedStarRings", this.m_paramGameResults.m_numRedStarRings);
	}

	// Token: 0x060030A5 RID: 12453 RVA: 0x00115440 File Offset: 0x00113640
	private void SetParameter_Distance()
	{
		base.WriteActionParamValue("distance", this.m_paramGameResults.m_distance);
	}

	// Token: 0x060030A6 RID: 12454 RVA: 0x00115460 File Offset: 0x00113660
	private void SetParameter_DailyMission()
	{
		base.WriteActionParamValue("dailyChallengeValue", this.m_paramGameResults.m_dailyMissionValue);
		base.WriteActionParamValue("dailyChallengeComplete", (!this.m_paramGameResults.m_dailyMissionComplete) ? 0 : 1);
	}

	// Token: 0x060030A7 RID: 12455 RVA: 0x001154B0 File Offset: 0x001136B0
	private void SetParameter_NumAnimals()
	{
		base.WriteActionParamValue("numAnimals", this.m_paramGameResults.m_numAnimals);
	}

	// Token: 0x060030A8 RID: 12456 RVA: 0x001154D0 File Offset: 0x001136D0
	private void SetParameter_Mileage()
	{
		base.WriteActionParamValue("reachPoint", this.m_paramGameResults.m_reachPoint);
		base.WriteActionParamValue("chapterClear", (!this.m_paramGameResults.m_clearChapter) ? 0 : 1);
		base.WriteActionParamValue("numBossAttack", this.m_paramGameResults.m_numBossAttack);
	}

	// Token: 0x060030A9 RID: 12457 RVA: 0x0011553C File Offset: 0x0011373C
	private void SetParameter_ChaoEggPresent()
	{
		base.WriteActionParamValue("getChaoEgg", (!this.m_paramGameResults.m_chaoEggPresent) ? 0 : 1);
	}

	// Token: 0x060030AA RID: 12458 RVA: 0x00115568 File Offset: 0x00113768
	private void SetParameter_BossDestroyed()
	{
		base.WriteActionParamValue("bossDestroyed", (!this.m_paramGameResults.m_isBossDestroyed) ? 0 : 1);
	}

	// Token: 0x060030AB RID: 12459 RVA: 0x00115594 File Offset: 0x00113794
	private void SetParameter_Event()
	{
		int? eventId = this.m_paramGameResults.m_eventId;
		if (eventId != null)
		{
			base.WriteActionParamValue("eventId", this.m_paramGameResults.m_eventId);
			long? eventValue = this.m_paramGameResults.m_eventValue;
			if (eventValue != null)
			{
				base.WriteActionParamValue("eventValue", this.m_paramGameResults.m_eventValue);
			}
		}
	}

	// Token: 0x17000682 RID: 1666
	// (get) Token: 0x060030AC RID: 12460 RVA: 0x00115608 File Offset: 0x00113808
	// (set) Token: 0x060030AD RID: 12461 RVA: 0x00115610 File Offset: 0x00113810
	public ServerPlayerState m_resultPlayerState { get; private set; }

	// Token: 0x17000683 RID: 1667
	// (get) Token: 0x060030AE RID: 12462 RVA: 0x0011561C File Offset: 0x0011381C
	// (set) Token: 0x060030AF RID: 12463 RVA: 0x00115624 File Offset: 0x00113824
	public ServerCharacterState[] resultCharacterState { get; private set; }

	// Token: 0x17000684 RID: 1668
	// (get) Token: 0x060030B0 RID: 12464 RVA: 0x00115630 File Offset: 0x00113830
	// (set) Token: 0x060030B1 RID: 12465 RVA: 0x00115638 File Offset: 0x00113838
	public List<ServerChaoState> resultChaoState { get; private set; }

	// Token: 0x17000685 RID: 1669
	// (get) Token: 0x060030B2 RID: 12466 RVA: 0x00115644 File Offset: 0x00113844
	// (set) Token: 0x060030B3 RID: 12467 RVA: 0x0011564C File Offset: 0x0011384C
	public ServerPlayCharacterState[] resultPlayCharacterState { get; private set; }

	// Token: 0x17000686 RID: 1670
	// (get) Token: 0x060030B4 RID: 12468 RVA: 0x00115658 File Offset: 0x00113858
	// (set) Token: 0x060030B5 RID: 12469 RVA: 0x00115660 File Offset: 0x00113860
	public List<ServerMileageIncentive> m_resultMileageIncentive { get; private set; }

	// Token: 0x17000687 RID: 1671
	// (get) Token: 0x060030B6 RID: 12470 RVA: 0x0011566C File Offset: 0x0011386C
	// (set) Token: 0x060030B7 RID: 12471 RVA: 0x00115674 File Offset: 0x00113874
	public ServerMileageMapState m_resultMileageMapState { get; private set; }

	// Token: 0x17000688 RID: 1672
	// (get) Token: 0x060030B8 RID: 12472 RVA: 0x00115680 File Offset: 0x00113880
	// (set) Token: 0x060030B9 RID: 12473 RVA: 0x00115688 File Offset: 0x00113888
	public List<ServerItemState> m_resultDailyMissionIncentiveList { get; set; }

	// Token: 0x17000689 RID: 1673
	// (get) Token: 0x060030BA RID: 12474 RVA: 0x00115694 File Offset: 0x00113894
	public int resultDailyMissionIncentives
	{
		get
		{
			if (this.m_resultDailyMissionIncentiveList != null)
			{
				return this.m_resultDailyMissionIncentiveList.Count;
			}
			return 0;
		}
	}

	// Token: 0x1700068A RID: 1674
	// (get) Token: 0x060030BB RID: 12475 RVA: 0x001156B0 File Offset: 0x001138B0
	public int resultMileageIncentives
	{
		get
		{
			if (this.m_resultMileageIncentive != null)
			{
				return this.m_resultMileageIncentive.Count;
			}
			return 0;
		}
	}

	// Token: 0x060030BC RID: 12476 RVA: 0x001156CC File Offset: 0x001138CC
	public ServerItemState GetResultDailyMissionIncentive(int index)
	{
		if (0 <= index && this.resultDailyMissionIncentives > index)
		{
			return this.m_resultDailyMissionIncentiveList[index];
		}
		return null;
	}

	// Token: 0x060030BD RID: 12477 RVA: 0x001156F0 File Offset: 0x001138F0
	public ServerMileageIncentive GetResultMileageIncentive(int index)
	{
		if (0 <= index && this.resultMileageIncentives > index)
		{
			return this.m_resultMileageIncentive[index];
		}
		return null;
	}

	// Token: 0x060030BE RID: 12478 RVA: 0x00115714 File Offset: 0x00113914
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.m_resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}

	// Token: 0x060030BF RID: 12479 RVA: 0x00115728 File Offset: 0x00113928
	private void GetResponse_WheelOptions(JsonData jdata)
	{
		ServerWheelOptions serverWheelOptions = NetUtil.AnalyzeWheelOptionsJson(jdata, "wheelOptions");
		if (serverWheelOptions.m_numJackpotRing > 0)
		{
			RouletteManager.numJackpotRing = serverWheelOptions.m_numJackpotRing;
			Debug.Log("!!!! numJackpotRing : " + RouletteManager.numJackpotRing);
		}
	}

	// Token: 0x060030C0 RID: 12480 RVA: 0x00115774 File Offset: 0x00113974
	private void GetResponse_CharacterState(JsonData jdata)
	{
		this.resultCharacterState = NetUtil.AnalyzePlayerState_CharactersStates(jdata);
	}

	// Token: 0x060030C1 RID: 12481 RVA: 0x00115784 File Offset: 0x00113984
	private void GetResponse_ChaoState(JsonData jdata)
	{
		this.resultChaoState = NetUtil.AnalyzePlayerState_ChaoStates(jdata);
	}

	// Token: 0x060030C2 RID: 12482 RVA: 0x00115794 File Offset: 0x00113994
	private void GetResponse_PlayCharacterState(JsonData jdata)
	{
		this.resultPlayCharacterState = NetUtil.AnalyzePlayerState_PlayCharactersStates(jdata);
	}

	// Token: 0x060030C3 RID: 12483 RVA: 0x001157A4 File Offset: 0x001139A4
	private void GetResponse_MileageMapState(JsonData jdata)
	{
		this.m_resultMileageMapState = NetUtil.AnalyzeMileageMapStateJson(jdata, "mileageMapState");
	}

	// Token: 0x060030C4 RID: 12484 RVA: 0x001157B8 File Offset: 0x001139B8
	private void GetResponse_DailyMissionIncentives(JsonData jdata)
	{
		this.m_resultDailyMissionIncentiveList = new List<ServerItemState>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "dailyChallengeIncentive");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			ServerItemState item = NetUtil.AnalyzeItemStateJson(jsonArray[i], string.Empty);
			this.m_resultDailyMissionIncentiveList.Add(item);
		}
	}

	// Token: 0x060030C5 RID: 12485 RVA: 0x00115814 File Offset: 0x00113A14
	private void GetResponse_MileageIncentives(JsonData jdata)
	{
		this.m_resultMileageIncentive = new List<ServerMileageIncentive>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "mileageIncentiveList");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			ServerMileageIncentive item = NetUtil.AnalyzeMileageIncentiveJson(jsonArray[i], string.Empty);
			this.m_resultMileageIncentive.Add(item);
		}
	}

	// Token: 0x060030C6 RID: 12486 RVA: 0x00115870 File Offset: 0x00113A70
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

	// Token: 0x060030C7 RID: 12487 RVA: 0x0011593C File Offset: 0x00113B3C
	private void GetResponse_Event(JsonData jdata)
	{
		this.m_resultEventIncentiveList = new List<ServerItemState>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "eventIncentiveList");
		if (jsonArray != null)
		{
			int count = jsonArray.Count;
			for (int i = 0; i < count; i++)
			{
				ServerItemState item = NetUtil.AnalyzeItemStateJson(jsonArray[i], string.Empty);
				this.m_resultEventIncentiveList.Add(item);
			}
		}
		this.m_resultEventState = NetUtil.AnalyzeEventState(jdata);
	}

	// Token: 0x04002AF4 RID: 10996
	public List<ServerMessageEntry> m_messageEntryList = new List<ServerMessageEntry>();

	// Token: 0x04002AF5 RID: 10997
	public List<ServerOperatorMessageEntry> m_operatorMessageEntryList = new List<ServerOperatorMessageEntry>();

	// Token: 0x04002AF6 RID: 10998
	public int m_totalMessage;

	// Token: 0x04002AF7 RID: 10999
	public int m_totalOperatorMessage;

	// Token: 0x04002AF8 RID: 11000
	public List<ServerItemState> m_resultEventIncentiveList;

	// Token: 0x04002AF9 RID: 11001
	public ServerEventState m_resultEventState;
}
