using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x020006BA RID: 1722
public class NetServerGetPrizeDailyBattle : NetBase
{
	// Token: 0x06002E34 RID: 11828 RVA: 0x00110F18 File Offset: 0x0010F118
	protected override void DoRequest()
	{
		base.SetAction("Battle/getPrizeDailyBattle");
	}

	// Token: 0x06002E35 RID: 11829 RVA: 0x00110F28 File Offset: 0x0010F128
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_BattleData(jdata);
	}

	// Token: 0x06002E36 RID: 11830 RVA: 0x00110F34 File Offset: 0x0010F134
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000603 RID: 1539
	// (get) Token: 0x06002E37 RID: 11831 RVA: 0x00110F38 File Offset: 0x0010F138
	// (set) Token: 0x06002E38 RID: 11832 RVA: 0x00110F40 File Offset: 0x0010F140
	public List<ServerDailyBattlePrizeData> battleDataPrizeList { get; private set; }

	// Token: 0x06002E39 RID: 11833 RVA: 0x00110F4C File Offset: 0x0010F14C
	private void GetResponse_BattleData(JsonData jdata)
	{
		this.battleDataPrizeList = NetUtil.AnalyzeDailyBattlePrizeDataJson(jdata, "battlePrizeDataList");
	}
}
