using System;
using LitJson;

// Token: 0x02000797 RID: 1943
public class NetServerGetPrizeWheelSpinGeneral : NetBase
{
	// Token: 0x06003399 RID: 13209 RVA: 0x0011BDBC File Offset: 0x00119FBC
	public NetServerGetPrizeWheelSpinGeneral(int eventId, int spinType)
	{
		this.paramEventId = eventId;
		this.paramSpinType = spinType;
	}

	// Token: 0x0600339A RID: 13210 RVA: 0x0011BDD4 File Offset: 0x00119FD4
	protected override void DoRequest()
	{
		base.SetAction("RaidbossSpin/getPrizeRaidbossWheelSpin");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string getPrizeWheelSpinGeneralString = instance.GetGetPrizeWheelSpinGeneralString(this.paramEventId, this.paramSpinType);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(getPrizeWheelSpinGeneralString);
		}
	}

	// Token: 0x0600339B RID: 13211 RVA: 0x0011BE24 File Offset: 0x0011A024
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PrizeWheelSpinGeneral(jdata);
	}

	// Token: 0x0600339C RID: 13212 RVA: 0x0011BE30 File Offset: 0x0011A030
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x0600339D RID: 13213 RVA: 0x0011BE34 File Offset: 0x0011A034
	private void SetParameter_WheelSpinGeneral()
	{
		base.WriteActionParamValue("eventId", this.paramEventId);
		base.WriteActionParamValue("raidbossWheelSpinType", this.paramSpinType);
	}

	// Token: 0x0600339E RID: 13214 RVA: 0x0011BE70 File Offset: 0x0011A070
	private void GetResponse_PrizeWheelSpinGeneral(JsonData jdata)
	{
		this.resultPrizeState = NetUtil.AnalyzePrizeWheelSpinGeneral(jdata);
	}

	// Token: 0x04002BDA RID: 11226
	public int paramEventId;

	// Token: 0x04002BDB RID: 11227
	public int paramSpinType;

	// Token: 0x04002BDC RID: 11228
	public ServerPrizeState resultPrizeState;
}
