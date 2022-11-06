using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x02000745 RID: 1861
public class NetServerGetLeagueOperatorData : NetBase
{
	// Token: 0x0600319B RID: 12699 RVA: 0x001178A8 File Offset: 0x00115AA8
	public NetServerGetLeagueOperatorData(int mode)
	{
		this.paramMode = mode;
		base.SetSecureFlag(false);
	}

	// Token: 0x0600319C RID: 12700 RVA: 0x001178C0 File Offset: 0x00115AC0
	protected override void DoRequest()
	{
		base.SetAction("Leaderboard/getLeagueOperatorData");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string getLeagueOperatorDataString = instance.GetGetLeagueOperatorDataString(this.paramMode, -1);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(getLeagueOperatorDataString);
		}
	}

	// Token: 0x0600319D RID: 12701 RVA: 0x0011790C File Offset: 0x00115B0C
	protected void SetParameter_NetServerGetLeagueOperatorData()
	{
		this.SetParameter_League();
	}

	// Token: 0x0600319E RID: 12702 RVA: 0x00117914 File Offset: 0x00115B14
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_LeagueOperatorData(jdata);
	}

	// Token: 0x0600319F RID: 12703 RVA: 0x00117920 File Offset: 0x00115B20
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170006AF RID: 1711
	// (get) Token: 0x060031A0 RID: 12704 RVA: 0x00117924 File Offset: 0x00115B24
	// (set) Token: 0x060031A1 RID: 12705 RVA: 0x0011792C File Offset: 0x00115B2C
	public int paramMode { get; set; }

	// Token: 0x170006B0 RID: 1712
	// (get) Token: 0x060031A2 RID: 12706 RVA: 0x00117938 File Offset: 0x00115B38
	// (set) Token: 0x060031A3 RID: 12707 RVA: 0x00117940 File Offset: 0x00115B40
	public List<ServerLeagueOperatorData> leagueOperatorData { get; set; }

	// Token: 0x170006B1 RID: 1713
	// (get) Token: 0x060031A4 RID: 12708 RVA: 0x0011794C File Offset: 0x00115B4C
	// (set) Token: 0x060031A5 RID: 12709 RVA: 0x00117954 File Offset: 0x00115B54
	public int mode { get; set; }

	// Token: 0x060031A6 RID: 12710 RVA: 0x00117960 File Offset: 0x00115B60
	private void SetParameter_League()
	{
		base.WriteActionParamValue("league", -1);
	}

	// Token: 0x060031A7 RID: 12711 RVA: 0x00117974 File Offset: 0x00115B74
	private void SetParameter_Mode()
	{
		base.WriteActionParamValue("mode", this.paramMode);
	}

	// Token: 0x060031A8 RID: 12712 RVA: 0x0011798C File Offset: 0x00115B8C
	private void GetResponse_LeagueOperatorData(JsonData jdata)
	{
		this.leagueOperatorData = NetUtil.AnalyzeLeagueDatas(jdata, "leagueOperatorList");
		this.mode = NetUtil.GetJsonInt(jdata, "leagueId");
	}
}
