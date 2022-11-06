using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x02000795 RID: 1941
public class NetServerCommitWheelSpinGeneral : NetBase
{
	// Token: 0x0600337D RID: 13181 RVA: 0x0011BA50 File Offset: 0x00119C50
	public NetServerCommitWheelSpinGeneral(int eventId, int spinId, int spinCostItemId, int spinNum)
	{
		this.paramEventId = eventId;
		this.paramSpinId = spinId;
		this.paramSpinCostItemId = spinCostItemId;
		this.paramSpinNum = spinNum;
	}

	// Token: 0x0600337E RID: 13182 RVA: 0x0011BA78 File Offset: 0x00119C78
	protected override void DoRequest()
	{
		base.SetAction("RaidbossSpin/commitRaidbossWheelSpin");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string commitWheelSpinGeneralString = instance.GetCommitWheelSpinGeneralString(this.paramEventId, this.paramSpinCostItemId, this.paramSpinId, this.paramSpinNum);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(commitWheelSpinGeneralString);
		}
	}

	// Token: 0x0600337F RID: 13183 RVA: 0x0011BAD4 File Offset: 0x00119CD4
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
		this.GetResponse_CharacterState(jdata);
		this.GetResponse_ChaoState(jdata);
		this.GetResponse_WheelOptionsGen(jdata);
		this.GetResponse_WheelResultGen(jdata);
	}

	// Token: 0x06003380 RID: 13184 RVA: 0x0011BB04 File Offset: 0x00119D04
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x06003381 RID: 13185 RVA: 0x0011BB08 File Offset: 0x00119D08
	private void SetParameter()
	{
		base.WriteActionParamValue("eventId", this.paramEventId);
		base.WriteActionParamValue("id", this.paramSpinId);
		base.WriteActionParamValue("costItemId", this.paramSpinCostItemId);
		base.WriteActionParamValue("num", this.paramSpinNum);
	}

	// Token: 0x17000709 RID: 1801
	// (get) Token: 0x06003382 RID: 13186 RVA: 0x0011BB70 File Offset: 0x00119D70
	// (set) Token: 0x06003383 RID: 13187 RVA: 0x0011BB78 File Offset: 0x00119D78
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x1700070A RID: 1802
	// (get) Token: 0x06003384 RID: 13188 RVA: 0x0011BB84 File Offset: 0x00119D84
	// (set) Token: 0x06003385 RID: 13189 RVA: 0x0011BB8C File Offset: 0x00119D8C
	public ServerCharacterState[] resultCharacterState { get; private set; }

	// Token: 0x1700070B RID: 1803
	// (get) Token: 0x06003386 RID: 13190 RVA: 0x0011BB98 File Offset: 0x00119D98
	// (set) Token: 0x06003387 RID: 13191 RVA: 0x0011BBA0 File Offset: 0x00119DA0
	public List<ServerChaoState> resultChaoState { get; private set; }

	// Token: 0x1700070C RID: 1804
	// (get) Token: 0x06003388 RID: 13192 RVA: 0x0011BBAC File Offset: 0x00119DAC
	// (set) Token: 0x06003389 RID: 13193 RVA: 0x0011BBB4 File Offset: 0x00119DB4
	public ServerWheelOptionsGeneral resultWheelOptionsGen { get; private set; }

	// Token: 0x1700070D RID: 1805
	// (get) Token: 0x0600338A RID: 13194 RVA: 0x0011BBC0 File Offset: 0x00119DC0
	// (set) Token: 0x0600338B RID: 13195 RVA: 0x0011BBC8 File Offset: 0x00119DC8
	public ServerSpinResultGeneral resultWheelResultGen { get; private set; }

	// Token: 0x0600338C RID: 13196 RVA: 0x0011BBD4 File Offset: 0x00119DD4
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}

	// Token: 0x0600338D RID: 13197 RVA: 0x0011BBE8 File Offset: 0x00119DE8
	private void GetResponse_CharacterState(JsonData jdata)
	{
		this.resultCharacterState = NetUtil.AnalyzePlayerState_CharactersStates(jdata);
	}

	// Token: 0x0600338E RID: 13198 RVA: 0x0011BBF8 File Offset: 0x00119DF8
	private void GetResponse_ChaoState(JsonData jdata)
	{
		this.resultChaoState = NetUtil.AnalyzePlayerState_ChaoStates(jdata);
	}

	// Token: 0x0600338F RID: 13199 RVA: 0x0011BC08 File Offset: 0x00119E08
	private void GetResponse_WheelOptionsGen(JsonData jdata)
	{
		this.resultWheelOptionsGen = NetUtil.AnalyzeWheelOptionsGeneralJson(jdata, "raidbossWheelOptions");
	}

	// Token: 0x06003390 RID: 13200 RVA: 0x0011BC1C File Offset: 0x00119E1C
	private void GetResponse_WheelResultGen(JsonData jdata)
	{
		this.resultWheelResultGen = NetUtil.AnalyzeSpinResultGeneralJson(jdata, "raidbossSpinResult");
	}

	// Token: 0x04002BCE RID: 11214
	public int paramEventId;

	// Token: 0x04002BCF RID: 11215
	public int paramSpinId;

	// Token: 0x04002BD0 RID: 11216
	public int paramSpinCostItemId;

	// Token: 0x04002BD1 RID: 11217
	public int paramSpinNum;
}
