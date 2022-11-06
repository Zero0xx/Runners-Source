using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x02000720 RID: 1824
public class NetServerQuickModePostGameResults : NetBase
{
	// Token: 0x060030E2 RID: 12514 RVA: 0x00115EBC File Offset: 0x001140BC
	public NetServerQuickModePostGameResults(ServerQuickModeGameResults gameResults)
	{
		this.m_paramGameResults = gameResults;
	}

	// Token: 0x060030E3 RID: 12515 RVA: 0x00115EF8 File Offset: 0x001140F8
	protected override void DoRequest()
	{
		base.SetAction("Game/quickPostGameResults");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string quickModePostGameResultString = instance.GetQuickModePostGameResultString(this.m_paramGameResults);
			Debug.Log("NetServerQuickModePostGameResults.json = " + quickModePostGameResultString);
			base.WriteJsonString(quickModePostGameResultString);
		}
	}

	// Token: 0x060030E4 RID: 12516 RVA: 0x00115F48 File Offset: 0x00114148
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
		this.GetResponse_CharacterState(jdata);
		this.GetResponse_ChaoState(jdata);
		this.GetResponse_PlayCharacterState(jdata);
		this.GetResponse_DailyMissionIncentives(jdata);
		this.GetResponse_MessageList(jdata);
	}

	// Token: 0x060030E5 RID: 12517 RVA: 0x00115F80 File Offset: 0x00114180
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000692 RID: 1682
	// (get) Token: 0x060030E6 RID: 12518 RVA: 0x00115F84 File Offset: 0x00114184
	// (set) Token: 0x060030E7 RID: 12519 RVA: 0x00115F8C File Offset: 0x0011418C
	public ServerQuickModeGameResults m_paramGameResults { get; set; }

	// Token: 0x060030E8 RID: 12520 RVA: 0x00115F98 File Offset: 0x00114198
	private void SetParameter_Suspended()
	{
		base.WriteActionParamValue("closed", (!this.m_paramGameResults.m_isSuspended) ? 0 : 1);
	}

	// Token: 0x060030E9 RID: 12521 RVA: 0x00115FC4 File Offset: 0x001141C4
	private void SetParameter_Score()
	{
		base.WriteActionParamValue("score", this.m_paramGameResults.m_score);
	}

	// Token: 0x060030EA RID: 12522 RVA: 0x00115FE4 File Offset: 0x001141E4
	private void SetParameter_Rings()
	{
		base.WriteActionParamValue("numRings", this.m_paramGameResults.m_numRings);
		base.WriteActionParamValue("numFailureRings", this.m_paramGameResults.m_numFailureRings);
		base.WriteActionParamValue("numRedStarRings", this.m_paramGameResults.m_numRedStarRings);
	}

	// Token: 0x060030EB RID: 12523 RVA: 0x00116044 File Offset: 0x00114244
	private void SetParameter_Distance()
	{
		base.WriteActionParamValue("distance", this.m_paramGameResults.m_distance);
	}

	// Token: 0x060030EC RID: 12524 RVA: 0x00116064 File Offset: 0x00114264
	private void SetParameter_DailyMission()
	{
		base.WriteActionParamValue("dailyChallengeValue", this.m_paramGameResults.m_dailyMissionValue);
		base.WriteActionParamValue("dailyChallengeComplete", (!this.m_paramGameResults.m_dailyMissionComplete) ? 0 : 1);
	}

	// Token: 0x060030ED RID: 12525 RVA: 0x001160B4 File Offset: 0x001142B4
	private void SetParameter_NumAnimals()
	{
		base.WriteActionParamValue("numAnimals", this.m_paramGameResults.m_numAnimals);
	}

	// Token: 0x17000693 RID: 1683
	// (get) Token: 0x060030EE RID: 12526 RVA: 0x001160D4 File Offset: 0x001142D4
	// (set) Token: 0x060030EF RID: 12527 RVA: 0x001160DC File Offset: 0x001142DC
	public ServerPlayerState m_resultPlayerState { get; private set; }

	// Token: 0x17000694 RID: 1684
	// (get) Token: 0x060030F0 RID: 12528 RVA: 0x001160E8 File Offset: 0x001142E8
	// (set) Token: 0x060030F1 RID: 12529 RVA: 0x001160F0 File Offset: 0x001142F0
	public ServerCharacterState[] m_resultCharacterState { get; private set; }

	// Token: 0x17000695 RID: 1685
	// (get) Token: 0x060030F2 RID: 12530 RVA: 0x001160FC File Offset: 0x001142FC
	// (set) Token: 0x060030F3 RID: 12531 RVA: 0x00116104 File Offset: 0x00114304
	public List<ServerChaoState> m_resultChaoState { get; private set; }

	// Token: 0x17000696 RID: 1686
	// (get) Token: 0x060030F4 RID: 12532 RVA: 0x00116110 File Offset: 0x00114310
	// (set) Token: 0x060030F5 RID: 12533 RVA: 0x00116118 File Offset: 0x00114318
	public ServerPlayCharacterState[] m_resultPlayCharacterState { get; private set; }

	// Token: 0x17000697 RID: 1687
	// (get) Token: 0x060030F6 RID: 12534 RVA: 0x00116124 File Offset: 0x00114324
	public int totalMessage
	{
		get
		{
			if (this.m_messageEntryList != null)
			{
				return this.m_messageEntryList.Count;
			}
			return 0;
		}
	}

	// Token: 0x17000698 RID: 1688
	// (get) Token: 0x060030F7 RID: 12535 RVA: 0x00116140 File Offset: 0x00114340
	public int totalOperatorMessage
	{
		get
		{
			if (this.m_operatorMessageEntryList != null)
			{
				return this.m_operatorMessageEntryList.Count;
			}
			return 0;
		}
	}

	// Token: 0x060030F8 RID: 12536 RVA: 0x0011615C File Offset: 0x0011435C
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.m_resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}

	// Token: 0x060030F9 RID: 12537 RVA: 0x00116170 File Offset: 0x00114370
	private void GetResponse_CharacterState(JsonData jdata)
	{
		this.m_resultCharacterState = NetUtil.AnalyzePlayerState_CharactersStates(jdata);
	}

	// Token: 0x060030FA RID: 12538 RVA: 0x00116180 File Offset: 0x00114380
	private void GetResponse_ChaoState(JsonData jdata)
	{
		this.m_resultChaoState = NetUtil.AnalyzePlayerState_ChaoStates(jdata);
	}

	// Token: 0x060030FB RID: 12539 RVA: 0x00116190 File Offset: 0x00114390
	private void GetResponse_PlayCharacterState(JsonData jdata)
	{
		this.m_resultPlayCharacterState = NetUtil.AnalyzePlayerState_PlayCharactersStates(jdata);
	}

	// Token: 0x060030FC RID: 12540 RVA: 0x001161A0 File Offset: 0x001143A0
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

	// Token: 0x060030FD RID: 12541 RVA: 0x001161F0 File Offset: 0x001143F0
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
	}

	// Token: 0x04002B09 RID: 11017
	public List<ServerItemState> m_dailyMissionIncentiveList = new List<ServerItemState>();

	// Token: 0x04002B0A RID: 11018
	public List<ServerMessageEntry> m_messageEntryList = new List<ServerMessageEntry>();

	// Token: 0x04002B0B RID: 11019
	public List<ServerOperatorMessageEntry> m_operatorMessageEntryList = new List<ServerOperatorMessageEntry>();
}
