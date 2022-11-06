using System;
using LitJson;

// Token: 0x02000780 RID: 1920
public class NetServerRetrievePlayerState : NetBase
{
	// Token: 0x0600331E RID: 13086 RVA: 0x0011AEF4 File Offset: 0x001190F4
	protected override void DoRequest()
	{
		base.SetAction("Player/getPlayerState");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x0600331F RID: 13087 RVA: 0x0011AF2C File Offset: 0x0011912C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
	}

	// Token: 0x06003320 RID: 13088 RVA: 0x0011AF38 File Offset: 0x00119138
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170006FB RID: 1787
	// (get) Token: 0x06003321 RID: 13089 RVA: 0x0011AF3C File Offset: 0x0011913C
	// (set) Token: 0x06003322 RID: 13090 RVA: 0x0011AF44 File Offset: 0x00119144
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x06003323 RID: 13091 RVA: 0x0011AF50 File Offset: 0x00119150
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}
}
