using System;
using LitJson;

// Token: 0x02000744 RID: 1860
public class NetServerGetLeagueData : NetBase
{
	// Token: 0x06003191 RID: 12689 RVA: 0x001177E4 File Offset: 0x001159E4
	public NetServerGetLeagueData(int mode)
	{
		this.paramMode = mode;
		base.SetSecureFlag(false);
	}

	// Token: 0x06003192 RID: 12690 RVA: 0x001177FC File Offset: 0x001159FC
	protected override void DoRequest()
	{
		base.SetAction("Leaderboard/getLeagueData");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string getLeagueDataString = instance.GetGetLeagueDataString(this.paramMode);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(getLeagueDataString);
		}
	}

	// Token: 0x06003193 RID: 12691 RVA: 0x00117844 File Offset: 0x00115A44
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_LeagueData(jdata);
	}

	// Token: 0x06003194 RID: 12692 RVA: 0x00117850 File Offset: 0x00115A50
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x06003195 RID: 12693 RVA: 0x00117854 File Offset: 0x00115A54
	private void SetParameter_Mode()
	{
		base.WriteActionParamValue("mode", this.paramMode);
	}

	// Token: 0x170006AD RID: 1709
	// (get) Token: 0x06003196 RID: 12694 RVA: 0x0011786C File Offset: 0x00115A6C
	// (set) Token: 0x06003197 RID: 12695 RVA: 0x00117874 File Offset: 0x00115A74
	public int paramMode { get; set; }

	// Token: 0x170006AE RID: 1710
	// (get) Token: 0x06003198 RID: 12696 RVA: 0x00117880 File Offset: 0x00115A80
	// (set) Token: 0x06003199 RID: 12697 RVA: 0x00117888 File Offset: 0x00115A88
	public ServerLeagueData leagueData { get; set; }

	// Token: 0x0600319A RID: 12698 RVA: 0x00117894 File Offset: 0x00115A94
	private void GetResponse_LeagueData(JsonData jdata)
	{
		this.leagueData = NetUtil.AnalyzeLeagueData(jdata, "leagueData");
	}
}
