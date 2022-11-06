using System;
using LitJson;

// Token: 0x020006B9 RID: 1721
public class NetServerGetDailyBattleStatus : NetBase
{
	// Token: 0x06002E2B RID: 11819 RVA: 0x00110E98 File Offset: 0x0010F098
	protected override void DoRequest()
	{
		base.SetAction("Battle/getDailyBattleStatus");
	}

	// Token: 0x06002E2C RID: 11820 RVA: 0x00110EA8 File Offset: 0x0010F0A8
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_BattleData(jdata);
	}

	// Token: 0x06002E2D RID: 11821 RVA: 0x00110EB4 File Offset: 0x0010F0B4
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000601 RID: 1537
	// (get) Token: 0x06002E2E RID: 11822 RVA: 0x00110EB8 File Offset: 0x0010F0B8
	// (set) Token: 0x06002E2F RID: 11823 RVA: 0x00110EC0 File Offset: 0x0010F0C0
	public ServerDailyBattleStatus battleStatus { get; private set; }

	// Token: 0x17000602 RID: 1538
	// (get) Token: 0x06002E30 RID: 11824 RVA: 0x00110ECC File Offset: 0x0010F0CC
	// (set) Token: 0x06002E31 RID: 11825 RVA: 0x00110ED4 File Offset: 0x0010F0D4
	public DateTime endTime { get; private set; }

	// Token: 0x06002E32 RID: 11826 RVA: 0x00110EE0 File Offset: 0x0010F0E0
	private void GetResponse_BattleData(JsonData jdata)
	{
		this.endTime = NetUtil.AnalyzeDateTimeJson(jdata, "endTime");
		this.battleStatus = NetUtil.AnalyzeDailyBattleStatusJson(jdata, "battleStatus");
	}
}
