using System;
using LitJson;

// Token: 0x020006B7 RID: 1719
public class NetServerGetDailyBattleData : NetBase
{
	// Token: 0x06002E1A RID: 11802 RVA: 0x00110D78 File Offset: 0x0010EF78
	protected override void DoRequest()
	{
		base.SetAction("Battle/getDailyBattleData");
	}

	// Token: 0x06002E1B RID: 11803 RVA: 0x00110D88 File Offset: 0x0010EF88
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_BattleData(jdata);
	}

	// Token: 0x06002E1C RID: 11804 RVA: 0x00110D94 File Offset: 0x0010EF94
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170005FE RID: 1534
	// (get) Token: 0x06002E1D RID: 11805 RVA: 0x00110D98 File Offset: 0x0010EF98
	// (set) Token: 0x06002E1E RID: 11806 RVA: 0x00110DA0 File Offset: 0x0010EFA0
	public ServerDailyBattleDataPair battleDataPair { get; private set; }

	// Token: 0x06002E1F RID: 11807 RVA: 0x00110DAC File Offset: 0x0010EFAC
	private void GetResponse_BattleData(JsonData jdata)
	{
		this.battleDataPair = NetUtil.AnalyzeDailyBattleDataPairJson(jdata, "battleData", "rivalBattleData", "startTime", "endTime");
	}
}
