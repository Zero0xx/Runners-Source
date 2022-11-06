using System;
using LitJson;

// Token: 0x02000799 RID: 1945
public class NetServerGetWheelOptionsGeneral : NetBase
{
	// Token: 0x060033A6 RID: 13222 RVA: 0x0011BF0C File Offset: 0x0011A10C
	public NetServerGetWheelOptionsGeneral(int eventId, int spinId)
	{
		this.paramEventId = eventId;
		this.paramSpinId = spinId;
	}

	// Token: 0x060033A7 RID: 13223 RVA: 0x0011BF24 File Offset: 0x0011A124
	protected override void DoRequest()
	{
		base.SetAction("RaidbossSpin/getRaidbossWheelOptions");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string getWheelSpinGeneralString = instance.GetGetWheelSpinGeneralString(this.paramEventId, this.paramSpinId);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(getWheelSpinGeneralString);
		}
	}

	// Token: 0x060033A8 RID: 13224 RVA: 0x0011BF74 File Offset: 0x0011A174
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_WheelOptionsGeneral(jdata);
	}

	// Token: 0x060033A9 RID: 13225 RVA: 0x0011BF80 File Offset: 0x0011A180
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x060033AA RID: 13226 RVA: 0x0011BF84 File Offset: 0x0011A184
	private void SetParameter()
	{
		base.WriteActionParamValue("eventId", this.paramEventId);
		base.WriteActionParamValue("id", this.paramSpinId);
	}

	// Token: 0x17000710 RID: 1808
	// (get) Token: 0x060033AB RID: 13227 RVA: 0x0011BFC0 File Offset: 0x0011A1C0
	// (set) Token: 0x060033AC RID: 13228 RVA: 0x0011BFC8 File Offset: 0x0011A1C8
	public ServerWheelOptionsGeneral resultWheelOptionsGeneral { get; private set; }

	// Token: 0x060033AD RID: 13229 RVA: 0x0011BFD4 File Offset: 0x0011A1D4
	private void GetResponse_WheelOptionsGeneral(JsonData jdata)
	{
		this.resultWheelOptionsGeneral = NetUtil.AnalyzeWheelOptionsGeneralJson(jdata, "raidbossWheelOptions");
	}

	// Token: 0x04002BDE RID: 11230
	public int paramEventId;

	// Token: 0x04002BDF RID: 11231
	public int paramSpinId;
}
