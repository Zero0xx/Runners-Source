using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x02000717 RID: 1815
public class NetServerGetCostList : NetBase
{
	// Token: 0x06003036 RID: 12342 RVA: 0x001146BC File Offset: 0x001128BC
	protected override void DoRequest()
	{
		base.SetAction("Game/getCostList");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x06003037 RID: 12343 RVA: 0x001146F4 File Offset: 0x001128F4
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_CostList(jdata);
	}

	// Token: 0x06003038 RID: 12344 RVA: 0x00114700 File Offset: 0x00112900
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000660 RID: 1632
	// (get) Token: 0x06003039 RID: 12345 RVA: 0x00114704 File Offset: 0x00112904
	// (set) Token: 0x0600303A RID: 12346 RVA: 0x0011470C File Offset: 0x0011290C
	public List<ServerConsumedCostData> resultCostList { get; set; }

	// Token: 0x0600303B RID: 12347 RVA: 0x00114718 File Offset: 0x00112918
	private void GetResponse_CostList(JsonData jdata)
	{
		this.resultCostList = NetUtil.AnalyzeConsumedCostDataList(jdata);
	}
}
