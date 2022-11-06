using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x020006B8 RID: 1720
public class NetServerGetDailyBattleDataHistory : NetBase
{
	// Token: 0x06002E20 RID: 11808 RVA: 0x00110DDC File Offset: 0x0010EFDC
	public NetServerGetDailyBattleDataHistory(int count)
	{
		this.historyCount = count;
	}

	// Token: 0x06002E21 RID: 11809 RVA: 0x00110DEC File Offset: 0x0010EFEC
	protected override void DoRequest()
	{
		base.SetAction("Battle/getDailyBattleDataHistory");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string dailyBattleDataHistoryString = instance.GetDailyBattleDataHistoryString(this.historyCount);
			base.WriteJsonString(dailyBattleDataHistoryString);
		}
	}

	// Token: 0x06002E22 RID: 11810 RVA: 0x00110E2C File Offset: 0x0010F02C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_BattleData(jdata);
	}

	// Token: 0x06002E23 RID: 11811 RVA: 0x00110E38 File Offset: 0x0010F038
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170005FF RID: 1535
	// (get) Token: 0x06002E25 RID: 11813 RVA: 0x00110E48 File Offset: 0x0010F048
	// (set) Token: 0x06002E24 RID: 11812 RVA: 0x00110E3C File Offset: 0x0010F03C
	public int historyCount { private get; set; }

	// Token: 0x06002E26 RID: 11814 RVA: 0x00110E50 File Offset: 0x0010F050
	private void SetParameter_HistoryCount()
	{
		base.WriteActionParamValue("count", this.historyCount);
	}

	// Token: 0x17000600 RID: 1536
	// (get) Token: 0x06002E27 RID: 11815 RVA: 0x00110E68 File Offset: 0x0010F068
	// (set) Token: 0x06002E28 RID: 11816 RVA: 0x00110E70 File Offset: 0x0010F070
	public List<ServerDailyBattleDataPair> battleDataPairList { get; private set; }

	// Token: 0x06002E29 RID: 11817 RVA: 0x00110E7C File Offset: 0x0010F07C
	private void GetResponse_BattleData(JsonData jdata)
	{
		this.battleDataPairList = NetUtil.AnalyzeDailyBattleDataPairListJson(jdata, "battleDataList");
	}
}
