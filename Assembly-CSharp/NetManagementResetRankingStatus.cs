using System;
using LitJson;

// Token: 0x02000771 RID: 1905
public class NetManagementResetRankingStatus : NetBase
{
	// Token: 0x060032B0 RID: 12976 RVA: 0x00119C28 File Offset: 0x00117E28
	protected override void DoRequest()
	{
		base.SetAction("Management/resetRankingStatus");
	}

	// Token: 0x060032B1 RID: 12977 RVA: 0x00119C38 File Offset: 0x00117E38
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x060032B2 RID: 12978 RVA: 0x00119C3C File Offset: 0x00117E3C
	protected override void DoDidSuccessEmulation()
	{
	}
}
