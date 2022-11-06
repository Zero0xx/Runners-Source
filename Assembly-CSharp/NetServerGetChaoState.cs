using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x0200077E RID: 1918
public class NetServerGetChaoState : NetBase
{
	// Token: 0x06003310 RID: 13072 RVA: 0x0011AE0C File Offset: 0x0011900C
	protected override void DoRequest()
	{
		base.SetAction("Player/getChaoState");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x06003311 RID: 13073 RVA: 0x0011AE44 File Offset: 0x00119044
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_ChaoState(jdata);
	}

	// Token: 0x06003312 RID: 13074 RVA: 0x0011AE50 File Offset: 0x00119050
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170006F9 RID: 1785
	// (get) Token: 0x06003313 RID: 13075 RVA: 0x0011AE54 File Offset: 0x00119054
	// (set) Token: 0x06003314 RID: 13076 RVA: 0x0011AE5C File Offset: 0x0011905C
	public List<ServerChaoState> resultChaoState { get; private set; }

	// Token: 0x06003315 RID: 13077 RVA: 0x0011AE68 File Offset: 0x00119068
	private void GetResponse_ChaoState(JsonData jdata)
	{
		this.resultChaoState = NetUtil.AnalyzePlayerState_ChaoStates(jdata);
	}
}
