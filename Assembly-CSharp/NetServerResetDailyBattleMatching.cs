using System;
using LitJson;

// Token: 0x020006BC RID: 1724
public class NetServerResetDailyBattleMatching : NetBase
{
	// Token: 0x06002E47 RID: 11847 RVA: 0x00111064 File Offset: 0x0010F264
	public NetServerResetDailyBattleMatching(int type)
	{
		this.matchingType = type;
	}

	// Token: 0x06002E48 RID: 11848 RVA: 0x00111074 File Offset: 0x0010F274
	protected override void DoRequest()
	{
		base.SetAction("Battle/resetDailyBattleMatching");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string jsonString = instance.ResetDailyBattleMatchingString(this.matchingType);
			base.WriteJsonString(jsonString);
		}
	}

	// Token: 0x06002E49 RID: 11849 RVA: 0x001110B4 File Offset: 0x0010F2B4
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
		this.GetResponse_BattleData(jdata);
	}

	// Token: 0x06002E4A RID: 11850 RVA: 0x001110C4 File Offset: 0x0010F2C4
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000608 RID: 1544
	// (get) Token: 0x06002E4C RID: 11852 RVA: 0x001110D4 File Offset: 0x0010F2D4
	// (set) Token: 0x06002E4B RID: 11851 RVA: 0x001110C8 File Offset: 0x0010F2C8
	public int matchingType { private get; set; }

	// Token: 0x06002E4D RID: 11853 RVA: 0x001110DC File Offset: 0x0010F2DC
	private void SetParameter_MatchingType()
	{
		base.WriteActionParamValue("type", this.matchingType);
	}

	// Token: 0x17000609 RID: 1545
	// (get) Token: 0x06002E4E RID: 11854 RVA: 0x001110F4 File Offset: 0x0010F2F4
	// (set) Token: 0x06002E4F RID: 11855 RVA: 0x001110FC File Offset: 0x0010F2FC
	public ServerPlayerState playerState { get; private set; }

	// Token: 0x1700060A RID: 1546
	// (get) Token: 0x06002E50 RID: 11856 RVA: 0x00111108 File Offset: 0x0010F308
	// (set) Token: 0x06002E51 RID: 11857 RVA: 0x00111110 File Offset: 0x0010F310
	public ServerDailyBattleDataPair battleDataPair { get; private set; }

	// Token: 0x1700060B RID: 1547
	// (get) Token: 0x06002E52 RID: 11858 RVA: 0x0011111C File Offset: 0x0010F31C
	// (set) Token: 0x06002E53 RID: 11859 RVA: 0x00111124 File Offset: 0x0010F324
	public DateTime endTime { get; private set; }

	// Token: 0x06002E54 RID: 11860 RVA: 0x00111130 File Offset: 0x0010F330
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.playerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}

	// Token: 0x06002E55 RID: 11861 RVA: 0x00111144 File Offset: 0x0010F344
	private void GetResponse_BattleData(JsonData jdata)
	{
		this.endTime = NetUtil.AnalyzeDateTimeJson(jdata, "endTime");
		this.battleDataPair = NetUtil.AnalyzeDailyBattleDataPairJson(jdata, "battleData", "rivalBattleData", "startTime", "endTime");
	}
}
