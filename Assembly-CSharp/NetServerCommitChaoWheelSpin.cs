using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x02000697 RID: 1687
public class NetServerCommitChaoWheelSpin : NetBase
{
	// Token: 0x06002D6C RID: 11628 RVA: 0x0010FCD4 File Offset: 0x0010DED4
	public NetServerCommitChaoWheelSpin(int count)
	{
		this.paramCount = count;
	}

	// Token: 0x06002D6D RID: 11629 RVA: 0x0010FCE4 File Offset: 0x0010DEE4
	protected override void DoRequest()
	{
		base.SetAction("Chao/commitChaoWheelSpin");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string commitChaoWheelSpinString = instance.GetCommitChaoWheelSpinString(this.paramCount);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(commitChaoWheelSpinString);
		}
	}

	// Token: 0x06002D6E RID: 11630 RVA: 0x0010FD2C File Offset: 0x0010DF2C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
		this.GetResponse_CharacterState(jdata);
		this.GetResponse_ChaoState(jdata);
		this.GetResponse_ChaoWheelOptions(jdata);
		this.GetResponse_ChaoWheelResult(jdata);
	}

	// Token: 0x06002D6F RID: 11631 RVA: 0x0010FD5C File Offset: 0x0010DF5C
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x06002D70 RID: 11632 RVA: 0x0010FD60 File Offset: 0x0010DF60
	private void SetParameter_ChaoWheelSpin()
	{
		base.WriteActionParamValue("count", this.paramCount);
	}

	// Token: 0x170005E2 RID: 1506
	// (get) Token: 0x06002D71 RID: 11633 RVA: 0x0010FD78 File Offset: 0x0010DF78
	// (set) Token: 0x06002D72 RID: 11634 RVA: 0x0010FD80 File Offset: 0x0010DF80
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x170005E3 RID: 1507
	// (get) Token: 0x06002D73 RID: 11635 RVA: 0x0010FD8C File Offset: 0x0010DF8C
	// (set) Token: 0x06002D74 RID: 11636 RVA: 0x0010FD94 File Offset: 0x0010DF94
	public ServerCharacterState[] resultCharacterState { get; private set; }

	// Token: 0x170005E4 RID: 1508
	// (get) Token: 0x06002D75 RID: 11637 RVA: 0x0010FDA0 File Offset: 0x0010DFA0
	// (set) Token: 0x06002D76 RID: 11638 RVA: 0x0010FDA8 File Offset: 0x0010DFA8
	public List<ServerChaoState> resultChaoState { get; private set; }

	// Token: 0x170005E5 RID: 1509
	// (get) Token: 0x06002D77 RID: 11639 RVA: 0x0010FDB4 File Offset: 0x0010DFB4
	// (set) Token: 0x06002D78 RID: 11640 RVA: 0x0010FDBC File Offset: 0x0010DFBC
	public ServerChaoWheelOptions resultChaoWheelOptions { get; private set; }

	// Token: 0x170005E6 RID: 1510
	// (get) Token: 0x06002D79 RID: 11641 RVA: 0x0010FDC8 File Offset: 0x0010DFC8
	// (set) Token: 0x06002D7A RID: 11642 RVA: 0x0010FDD0 File Offset: 0x0010DFD0
	public ServerSpinResultGeneral resultSpinResultGeneral { get; private set; }

	// Token: 0x06002D7B RID: 11643 RVA: 0x0010FDDC File Offset: 0x0010DFDC
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}

	// Token: 0x06002D7C RID: 11644 RVA: 0x0010FDF0 File Offset: 0x0010DFF0
	private void GetResponse_CharacterState(JsonData jdata)
	{
		this.resultCharacterState = NetUtil.AnalyzePlayerState_CharactersStates(jdata);
	}

	// Token: 0x06002D7D RID: 11645 RVA: 0x0010FE00 File Offset: 0x0010E000
	private void GetResponse_ChaoState(JsonData jdata)
	{
		this.resultChaoState = NetUtil.AnalyzePlayerState_ChaoStates(jdata);
	}

	// Token: 0x06002D7E RID: 11646 RVA: 0x0010FE10 File Offset: 0x0010E010
	private void GetResponse_ChaoWheelOptions(JsonData jdata)
	{
		this.resultChaoWheelOptions = NetUtil.AnalyzeChaoWheelOptionsJson(jdata, "chaoWheelOptions");
	}

	// Token: 0x06002D7F RID: 11647 RVA: 0x0010FE24 File Offset: 0x0010E024
	private void GetResponse_ChaoWheelResult(JsonData jdata)
	{
		this.resultSpinResultGeneral = NetUtil.AnalyzeSpinResultJson(jdata, "chaoSpinResultList");
	}

	// Token: 0x040029D2 RID: 10706
	public int paramCount;
}
