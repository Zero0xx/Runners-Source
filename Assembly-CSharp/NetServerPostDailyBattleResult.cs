using System;
using LitJson;

// Token: 0x020006BB RID: 1723
public class NetServerPostDailyBattleResult : NetBase
{
	// Token: 0x06002E3B RID: 11835 RVA: 0x00110F68 File Offset: 0x0010F168
	protected override void DoRequest()
	{
		base.SetAction("Battle/postDailyBattleResult");
	}

	// Token: 0x06002E3C RID: 11836 RVA: 0x00110F78 File Offset: 0x0010F178
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_BattleData(jdata);
	}

	// Token: 0x06002E3D RID: 11837 RVA: 0x00110F84 File Offset: 0x0010F184
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000604 RID: 1540
	// (get) Token: 0x06002E3E RID: 11838 RVA: 0x00110F88 File Offset: 0x0010F188
	// (set) Token: 0x06002E3F RID: 11839 RVA: 0x00110F90 File Offset: 0x0010F190
	public ServerDailyBattleStatus battleStatus { get; private set; }

	// Token: 0x17000605 RID: 1541
	// (get) Token: 0x06002E40 RID: 11840 RVA: 0x00110F9C File Offset: 0x0010F19C
	// (set) Token: 0x06002E41 RID: 11841 RVA: 0x00110FA4 File Offset: 0x0010F1A4
	public ServerDailyBattleDataPair battleDataPair { get; private set; }

	// Token: 0x17000606 RID: 1542
	// (get) Token: 0x06002E42 RID: 11842 RVA: 0x00110FB0 File Offset: 0x0010F1B0
	// (set) Token: 0x06002E43 RID: 11843 RVA: 0x00110FB8 File Offset: 0x0010F1B8
	public bool rewardFlag { get; private set; }

	// Token: 0x17000607 RID: 1543
	// (get) Token: 0x06002E44 RID: 11844 RVA: 0x00110FC4 File Offset: 0x0010F1C4
	// (set) Token: 0x06002E45 RID: 11845 RVA: 0x00110FCC File Offset: 0x0010F1CC
	public ServerDailyBattleDataPair rewardBattleDataPair { get; private set; }

	// Token: 0x06002E46 RID: 11846 RVA: 0x00110FD8 File Offset: 0x0010F1D8
	private void GetResponse_BattleData(JsonData jdata)
	{
		this.battleDataPair = NetUtil.AnalyzeDailyBattleDataPairJson(jdata, "battleData", "rivalBattleData", "startTime", "endTime");
		this.battleStatus = NetUtil.AnalyzeDailyBattleStatusJson(jdata, "battleStatus");
		bool jsonBoolean = NetUtil.GetJsonBoolean(jdata, "rewardFlag");
		if (jsonBoolean)
		{
			this.rewardFlag = true;
			this.rewardBattleDataPair = NetUtil.AnalyzeDailyBattleDataPairJson(jdata, "rewardBattleData", "rewardRivalBattleData", "rewardStartTime", "rewardEndTime");
		}
		else
		{
			this.rewardFlag = false;
			this.rewardBattleDataPair = null;
		}
	}
}
