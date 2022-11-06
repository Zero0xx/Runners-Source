using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x020006FF RID: 1791
public class NetServerGetFacebookIncentive : NetBase
{
	// Token: 0x06002FC8 RID: 12232 RVA: 0x00113CA8 File Offset: 0x00111EA8
	public NetServerGetFacebookIncentive() : this(0, 0)
	{
	}

	// Token: 0x06002FC9 RID: 12233 RVA: 0x00113CB4 File Offset: 0x00111EB4
	public NetServerGetFacebookIncentive(int incentiveType, int achievementCount)
	{
		this.incentiveType = incentiveType;
		this.achievementCount = achievementCount;
	}

	// Token: 0x06002FCA RID: 12234 RVA: 0x00113CCC File Offset: 0x00111ECC
	protected override void DoRequest()
	{
		base.SetAction("Friend/getFacebookIncentive");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string getFacebookIncentiveString = instance.GetGetFacebookIncentiveString(this.incentiveType, this.achievementCount);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(getFacebookIncentiveString);
		}
	}

	// Token: 0x06002FCB RID: 12235 RVA: 0x00113D1C File Offset: 0x00111F1C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
		this.GetResponse_Incentive(jdata);
	}

	// Token: 0x06002FCC RID: 12236 RVA: 0x00113D2C File Offset: 0x00111F2C
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000653 RID: 1619
	// (get) Token: 0x06002FCD RID: 12237 RVA: 0x00113D30 File Offset: 0x00111F30
	// (set) Token: 0x06002FCE RID: 12238 RVA: 0x00113D38 File Offset: 0x00111F38
	public int incentiveType { get; set; }

	// Token: 0x17000654 RID: 1620
	// (get) Token: 0x06002FCF RID: 12239 RVA: 0x00113D44 File Offset: 0x00111F44
	// (set) Token: 0x06002FD0 RID: 12240 RVA: 0x00113D4C File Offset: 0x00111F4C
	public int achievementCount { get; set; }

	// Token: 0x06002FD1 RID: 12241 RVA: 0x00113D58 File Offset: 0x00111F58
	private void SetParameter_Data()
	{
		base.WriteActionParamValue("type", this.incentiveType);
		base.WriteActionParamValue("achievementCount", this.achievementCount);
	}

	// Token: 0x17000655 RID: 1621
	// (get) Token: 0x06002FD2 RID: 12242 RVA: 0x00113D94 File Offset: 0x00111F94
	// (set) Token: 0x06002FD3 RID: 12243 RVA: 0x00113D9C File Offset: 0x00111F9C
	public ServerPlayerState playerState { get; set; }

	// Token: 0x17000656 RID: 1622
	// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x00113DA8 File Offset: 0x00111FA8
	// (set) Token: 0x06002FD5 RID: 12245 RVA: 0x00113DB0 File Offset: 0x00111FB0
	public List<ServerPresentState> incentive { get; set; }

	// Token: 0x06002FD6 RID: 12246 RVA: 0x00113DBC File Offset: 0x00111FBC
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.playerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}

	// Token: 0x06002FD7 RID: 12247 RVA: 0x00113DD0 File Offset: 0x00111FD0
	private void GetResponse_Incentive(JsonData jdata)
	{
		this.incentive = new List<ServerPresentState>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "incentive");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			ServerPresentState item = NetUtil.AnalyzePresentStateJson(jsonArray[i], string.Empty);
			this.incentive.Add(item);
		}
	}
}
