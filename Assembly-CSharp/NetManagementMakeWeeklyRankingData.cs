using System;
using LitJson;

// Token: 0x02000770 RID: 1904
public class NetManagementMakeWeeklyRankingData : NetBase
{
	// Token: 0x060032AC RID: 12972 RVA: 0x00119C08 File Offset: 0x00117E08
	protected override void DoRequest()
	{
		base.SetAction("Management/makeWeeklyRankingData");
	}

	// Token: 0x060032AD RID: 12973 RVA: 0x00119C18 File Offset: 0x00117E18
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x060032AE RID: 12974 RVA: 0x00119C1C File Offset: 0x00117E1C
	protected override void DoDidSuccessEmulation()
	{
	}
}
