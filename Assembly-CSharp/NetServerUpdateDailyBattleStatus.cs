using System;
using LitJson;

// Token: 0x020006BD RID: 1725
public class NetServerUpdateDailyBattleStatus : NetBase
{
	// Token: 0x06002E57 RID: 11863 RVA: 0x0011118C File Offset: 0x0010F38C
	protected override void DoRequest()
	{
		base.SetAction("Battle/updateDailyBattleStatus");
	}

	// Token: 0x06002E58 RID: 11864 RVA: 0x0011119C File Offset: 0x0010F39C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_BattleData(jdata);
	}

	// Token: 0x06002E59 RID: 11865 RVA: 0x001111A8 File Offset: 0x0010F3A8
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x1700060C RID: 1548
	// (get) Token: 0x06002E5A RID: 11866 RVA: 0x001111AC File Offset: 0x0010F3AC
	// (set) Token: 0x06002E5B RID: 11867 RVA: 0x001111B4 File Offset: 0x0010F3B4
	public ServerDailyBattleStatus battleDataStatus { get; private set; }

	// Token: 0x1700060D RID: 1549
	// (get) Token: 0x06002E5C RID: 11868 RVA: 0x001111C0 File Offset: 0x0010F3C0
	// (set) Token: 0x06002E5D RID: 11869 RVA: 0x001111C8 File Offset: 0x0010F3C8
	public DateTime endTime { get; private set; }

	// Token: 0x1700060E RID: 1550
	// (get) Token: 0x06002E5E RID: 11870 RVA: 0x001111D4 File Offset: 0x0010F3D4
	// (set) Token: 0x06002E5F RID: 11871 RVA: 0x001111DC File Offset: 0x0010F3DC
	public bool rewardFlag { get; private set; }

	// Token: 0x1700060F RID: 1551
	// (get) Token: 0x06002E60 RID: 11872 RVA: 0x001111E8 File Offset: 0x0010F3E8
	// (set) Token: 0x06002E61 RID: 11873 RVA: 0x001111F0 File Offset: 0x0010F3F0
	public ServerDailyBattleDataPair rewardBattleDataPair { get; private set; }

	// Token: 0x06002E62 RID: 11874 RVA: 0x001111FC File Offset: 0x0010F3FC
	private void GetResponse_BattleData(JsonData jdata)
	{
		this.endTime = NetUtil.AnalyzeDateTimeJson(jdata, "endTime");
		this.battleDataStatus = NetUtil.AnalyzeDailyBattleStatusJson(jdata, "battleStatus");
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
