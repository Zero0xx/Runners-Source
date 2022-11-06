using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x020006E3 RID: 1763
public class NetServerEventUpdateGameResults : NetBase
{
	// Token: 0x06002F5F RID: 12127 RVA: 0x00112E44 File Offset: 0x00111044
	public NetServerEventUpdateGameResults(ServerEventGameResults eventGameResults)
	{
		this.m_paramEventGameResults = eventGameResults;
	}

	// Token: 0x06002F60 RID: 12128 RVA: 0x00112E80 File Offset: 0x00111080
	protected override void DoRequest()
	{
		base.SetAction("Event/eventUpdateGameResults");
		this.SetParameter_Rings();
		this.SetParameter_Suspended();
		this.SetParameter_DailyMission();
		this.SetParameter_EventRaidBoss();
	}

	// Token: 0x06002F61 RID: 12129 RVA: 0x00112EB0 File Offset: 0x001110B0
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
		this.GetResponse_PlayCharacterState(jdata);
		this.GetResponse_WheelOptions(jdata);
		this.GetResponse_DailyMissionIncentives(jdata);
		this.GetResponse_MessageList(jdata);
		this.GetResponse_EventRaidBoss(jdata);
	}

	// Token: 0x06002F62 RID: 12130 RVA: 0x00112EE8 File Offset: 0x001110E8
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x06002F63 RID: 12131 RVA: 0x00112EEC File Offset: 0x001110EC
	private void SetParameter_Suspended()
	{
		base.WriteActionParamValue("closed", (!this.m_paramEventGameResults.m_isSuspended) ? 0 : 1);
	}

	// Token: 0x06002F64 RID: 12132 RVA: 0x00112F18 File Offset: 0x00111118
	private void SetParameter_Rings()
	{
		base.WriteActionParamValue("numRings", this.m_paramEventGameResults.m_numRings);
		base.WriteActionParamValue("numRedStarRings", this.m_paramEventGameResults.m_numRedStarRings);
		base.WriteActionParamValue("numFailureRings", this.m_paramEventGameResults.m_numFailureRings);
	}

	// Token: 0x06002F65 RID: 12133 RVA: 0x00112F78 File Offset: 0x00111178
	private void SetParameter_DailyMission()
	{
		base.WriteActionParamValue("dailyChallengeValue", this.m_paramEventGameResults.m_dailyMissionValue);
		base.WriteActionParamValue("dailyChallengeComplete", (!this.m_paramEventGameResults.m_dailyMissionComplete) ? 0 : 1);
	}

	// Token: 0x06002F66 RID: 12134 RVA: 0x00112FC8 File Offset: 0x001111C8
	private void SetParameter_EventRaidBoss()
	{
		base.WriteActionParamValue("eventId", this.m_paramEventGameResults.m_eventId);
		base.WriteActionParamValue("eventValue", this.m_paramEventGameResults.m_eventValue);
		base.WriteActionParamValue("raidbossId", this.m_paramEventGameResults.m_raidBossId);
		base.WriteActionParamValue("raidbossDamage", this.m_paramEventGameResults.m_raidBossDamage);
		base.WriteActionParamValue("raidbossBeatFlg", (!this.m_paramEventGameResults.m_isRaidBossBeat) ? 0 : 1);
	}

	// Token: 0x17000641 RID: 1601
	// (get) Token: 0x06002F67 RID: 12135 RVA: 0x00113068 File Offset: 0x00111268
	public ServerPlayerState PlayerState
	{
		get
		{
			return this.m_playerState;
		}
	}

	// Token: 0x17000642 RID: 1602
	// (get) Token: 0x06002F68 RID: 12136 RVA: 0x00113070 File Offset: 0x00111270
	public ServerPlayCharacterState[] PlayerCharacterState
	{
		get
		{
			return this.m_playCharacterState;
		}
	}

	// Token: 0x17000643 RID: 1603
	// (get) Token: 0x06002F69 RID: 12137 RVA: 0x00113078 File Offset: 0x00111278
	public ServerWheelOptions WheelOptions
	{
		get
		{
			return this.m_wheelOptions;
		}
	}

	// Token: 0x17000644 RID: 1604
	// (get) Token: 0x06002F6A RID: 12138 RVA: 0x00113080 File Offset: 0x00111280
	public List<ServerItemState> DailyMissionIncentiveList
	{
		get
		{
			return this.m_dailyMissionIncentiveList;
		}
	}

	// Token: 0x17000645 RID: 1605
	// (get) Token: 0x06002F6B RID: 12139 RVA: 0x00113088 File Offset: 0x00111288
	public List<ServerMessageEntry> MessageEntryList
	{
		get
		{
			return this.m_messageEntryList;
		}
	}

	// Token: 0x17000646 RID: 1606
	// (get) Token: 0x06002F6C RID: 12140 RVA: 0x00113090 File Offset: 0x00111290
	public int TotalMessage
	{
		get
		{
			return this.m_totalMessage;
		}
	}

	// Token: 0x17000647 RID: 1607
	// (get) Token: 0x06002F6D RID: 12141 RVA: 0x00113098 File Offset: 0x00111298
	public List<ServerOperatorMessageEntry> OperatorMessageEntryList
	{
		get
		{
			return this.m_operatorMessageEntryList;
		}
	}

	// Token: 0x17000648 RID: 1608
	// (get) Token: 0x06002F6E RID: 12142 RVA: 0x001130A0 File Offset: 0x001112A0
	public int TotalOperatorMessage
	{
		get
		{
			return this.m_totalOperatorMessage;
		}
	}

	// Token: 0x17000649 RID: 1609
	// (get) Token: 0x06002F6F RID: 12143 RVA: 0x001130A8 File Offset: 0x001112A8
	public List<ServerItemState> EventIncentiveList
	{
		get
		{
			return this.m_eventIncentiveList;
		}
	}

	// Token: 0x1700064A RID: 1610
	// (get) Token: 0x06002F70 RID: 12144 RVA: 0x001130B0 File Offset: 0x001112B0
	public ServerEventState EventState
	{
		get
		{
			return this.m_eventState;
		}
	}

	// Token: 0x1700064B RID: 1611
	// (get) Token: 0x06002F71 RID: 12145 RVA: 0x001130B8 File Offset: 0x001112B8
	public ServerEventRaidBossBonus RaidBossBonus
	{
		get
		{
			return this.m_raidBossBonus;
		}
	}

	// Token: 0x06002F72 RID: 12146 RVA: 0x001130C0 File Offset: 0x001112C0
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.m_playerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}

	// Token: 0x06002F73 RID: 12147 RVA: 0x001130D4 File Offset: 0x001112D4
	private void GetResponse_PlayCharacterState(JsonData jdata)
	{
		this.m_playCharacterState = NetUtil.AnalyzePlayerState_PlayCharactersStates(jdata);
	}

	// Token: 0x06002F74 RID: 12148 RVA: 0x001130E4 File Offset: 0x001112E4
	private void GetResponse_WheelOptions(JsonData jdata)
	{
		this.m_wheelOptions = NetUtil.AnalyzeWheelOptionsJson(jdata, "wheelOptions");
		if (this.m_wheelOptions.m_numJackpotRing > 0)
		{
			RouletteManager.numJackpotRing = this.m_wheelOptions.m_numJackpotRing;
			Debug.Log("numJackpotRing : " + RouletteManager.numJackpotRing);
		}
	}

	// Token: 0x06002F75 RID: 12149 RVA: 0x0011313C File Offset: 0x0011133C
	private void GetResponse_DailyMissionIncentives(JsonData jdata)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "dailyChallengeIncentive");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			ServerItemState item = NetUtil.AnalyzeItemStateJson(jsonArray[i], string.Empty);
			this.m_dailyMissionIncentiveList.Add(item);
		}
	}

	// Token: 0x06002F76 RID: 12150 RVA: 0x0011318C File Offset: 0x0011138C
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

	// Token: 0x06002F77 RID: 12151 RVA: 0x00113258 File Offset: 0x00111458
	private void GetResponse_EventRaidBoss(JsonData jdata)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "eventIncentiveList");
		if (jsonArray != null)
		{
			int count = jsonArray.Count;
			for (int i = 0; i < count; i++)
			{
				ServerItemState item = NetUtil.AnalyzeItemStateJson(jsonArray[i], string.Empty);
				this.m_eventIncentiveList.Add(item);
			}
		}
		this.m_eventState = NetUtil.AnalyzeEventState(jdata);
		this.m_raidBossBonus = NetUtil.AnalyzeEventRaidBossBonus(jdata);
	}

	// Token: 0x04002A77 RID: 10871
	private ServerEventGameResults m_paramEventGameResults;

	// Token: 0x04002A78 RID: 10872
	private ServerPlayerState m_playerState;

	// Token: 0x04002A79 RID: 10873
	private ServerPlayCharacterState[] m_playCharacterState;

	// Token: 0x04002A7A RID: 10874
	private ServerWheelOptions m_wheelOptions;

	// Token: 0x04002A7B RID: 10875
	private List<ServerItemState> m_dailyMissionIncentiveList = new List<ServerItemState>();

	// Token: 0x04002A7C RID: 10876
	private List<ServerMessageEntry> m_messageEntryList = new List<ServerMessageEntry>();

	// Token: 0x04002A7D RID: 10877
	private List<ServerOperatorMessageEntry> m_operatorMessageEntryList = new List<ServerOperatorMessageEntry>();

	// Token: 0x04002A7E RID: 10878
	private int m_totalMessage;

	// Token: 0x04002A7F RID: 10879
	private int m_totalOperatorMessage;

	// Token: 0x04002A80 RID: 10880
	private List<ServerItemState> m_eventIncentiveList = new List<ServerItemState>();

	// Token: 0x04002A81 RID: 10881
	private ServerEventState m_eventState;

	// Token: 0x04002A82 RID: 10882
	private ServerEventRaidBossBonus m_raidBossBonus;
}
