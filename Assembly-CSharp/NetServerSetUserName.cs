using System;
using LitJson;

// Token: 0x02000781 RID: 1921
public class NetServerSetUserName : NetBase
{
	// Token: 0x06003324 RID: 13092 RVA: 0x0011AF64 File Offset: 0x00119164
	public NetServerSetUserName(string userName)
	{
		this.userName = userName;
	}

	// Token: 0x06003325 RID: 13093 RVA: 0x0011AF74 File Offset: 0x00119174
	protected override void DoRequest()
	{
		base.SetAction("Player/setUserName");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string setUserNameString = instance.GetSetUserNameString(this.userName);
			base.WriteJsonString(setUserNameString);
		}
	}

	// Token: 0x06003326 RID: 13094 RVA: 0x0011AFB4 File Offset: 0x001191B4
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
	}

	// Token: 0x06003327 RID: 13095 RVA: 0x0011AFC0 File Offset: 0x001191C0
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170006FC RID: 1788
	// (get) Token: 0x06003329 RID: 13097 RVA: 0x0011AFD0 File Offset: 0x001191D0
	// (set) Token: 0x06003328 RID: 13096 RVA: 0x0011AFC4 File Offset: 0x001191C4
	public string userName { private get; set; }

	// Token: 0x0600332A RID: 13098 RVA: 0x0011AFD8 File Offset: 0x001191D8
	private void SetParameter_UserName()
	{
		base.WriteActionParamValue("userName", this.userName);
	}

	// Token: 0x170006FD RID: 1789
	// (get) Token: 0x0600332B RID: 13099 RVA: 0x0011AFEC File Offset: 0x001191EC
	// (set) Token: 0x0600332C RID: 13100 RVA: 0x0011AFF4 File Offset: 0x001191F4
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x0600332D RID: 13101 RVA: 0x0011B000 File Offset: 0x00119200
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}
}
