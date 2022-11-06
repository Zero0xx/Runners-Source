using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x0200069A RID: 1690
public class NetServerGetFirstLaunchChao : NetBase
{
	// Token: 0x06002D96 RID: 11670 RVA: 0x0010FFE4 File Offset: 0x0010E1E4
	protected override void DoRequest()
	{
		base.SetAction("Chao/getFirstLaunchChao");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x06002D97 RID: 11671 RVA: 0x0011001C File Offset: 0x0010E21C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
		this.GetResponse_ChaoState(jdata);
	}

	// Token: 0x06002D98 RID: 11672 RVA: 0x0011002C File Offset: 0x0010E22C
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170005EB RID: 1515
	// (get) Token: 0x06002D99 RID: 11673 RVA: 0x00110030 File Offset: 0x0010E230
	// (set) Token: 0x06002D9A RID: 11674 RVA: 0x00110038 File Offset: 0x0010E238
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x170005EC RID: 1516
	// (get) Token: 0x06002D9B RID: 11675 RVA: 0x00110044 File Offset: 0x0010E244
	// (set) Token: 0x06002D9C RID: 11676 RVA: 0x0011004C File Offset: 0x0010E24C
	public List<ServerChaoState> resultChaoState { get; private set; }

	// Token: 0x06002D9D RID: 11677 RVA: 0x00110058 File Offset: 0x0010E258
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}

	// Token: 0x06002D9E RID: 11678 RVA: 0x0011006C File Offset: 0x0010E26C
	private void GetResponse_ChaoState(JsonData jdata)
	{
		this.resultChaoState = NetUtil.AnalyzePlayerState_ChaoStates(jdata);
	}
}
