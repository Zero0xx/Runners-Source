using System;
using LitJson;

// Token: 0x02000746 RID: 1862
public class NetServerGetWeeklyLeaderboardOptions : NetBase
{
	// Token: 0x060031A9 RID: 12713 RVA: 0x001179BC File Offset: 0x00115BBC
	public NetServerGetWeeklyLeaderboardOptions(int mode)
	{
		this.paramMode = mode;
		base.SetSecureFlag(false);
	}

	// Token: 0x060031AA RID: 12714 RVA: 0x001179D4 File Offset: 0x00115BD4
	protected override void DoRequest()
	{
		base.SetAction("Leaderboard/getWeeklyLeaderboardOptions");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string getWeeklyLeaderboardOptionsString = instance.GetGetWeeklyLeaderboardOptionsString(this.paramMode);
			base.WriteJsonString(getWeeklyLeaderboardOptionsString);
		}
	}

	// Token: 0x060031AB RID: 12715 RVA: 0x00117A14 File Offset: 0x00115C14
	protected void SetParameter_NetServerGetWeeklyLeaderboardOptions()
	{
		this.SetParameter_Option();
	}

	// Token: 0x060031AC RID: 12716 RVA: 0x00117A1C File Offset: 0x00115C1C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_WeeklyLeaderboardOptions(jdata);
	}

	// Token: 0x060031AD RID: 12717 RVA: 0x00117A28 File Offset: 0x00115C28
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170006B2 RID: 1714
	// (get) Token: 0x060031AE RID: 12718 RVA: 0x00117A2C File Offset: 0x00115C2C
	// (set) Token: 0x060031AF RID: 12719 RVA: 0x00117A34 File Offset: 0x00115C34
	public int paramMode { get; set; }

	// Token: 0x170006B3 RID: 1715
	// (get) Token: 0x060031B0 RID: 12720 RVA: 0x00117A40 File Offset: 0x00115C40
	// (set) Token: 0x060031B1 RID: 12721 RVA: 0x00117A48 File Offset: 0x00115C48
	public ServerWeeklyLeaderboardOptions weeklyLeaderboardOptions { get; set; }

	// Token: 0x060031B2 RID: 12722 RVA: 0x00117A54 File Offset: 0x00115C54
	private void SetParameter_Option()
	{
		base.WriteActionParamValue("mode", this.paramMode);
	}

	// Token: 0x060031B3 RID: 12723 RVA: 0x00117A6C File Offset: 0x00115C6C
	private void GetResponse_WeeklyLeaderboardOptions(JsonData jdata)
	{
		this.weeklyLeaderboardOptions = NetUtil.AnalyzeWeeklyLeaderboardOptions(jdata);
	}
}
