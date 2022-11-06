using System;
using LitJson;

// Token: 0x020007AF RID: 1967
public class NetServerRedStarExchange : NetBase
{
	// Token: 0x06003413 RID: 13331 RVA: 0x0011C954 File Offset: 0x0011AB54
	public NetServerRedStarExchange() : this(0)
	{
	}

	// Token: 0x06003414 RID: 13332 RVA: 0x0011C960 File Offset: 0x0011AB60
	public NetServerRedStarExchange(int storeItemId)
	{
		this.paramStoreItemId = storeItemId;
	}

	// Token: 0x06003415 RID: 13333 RVA: 0x0011C970 File Offset: 0x0011AB70
	protected override void DoRequest()
	{
		base.SetAction("Store/redstarExchange");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string redStarExchangeString = instance.GetRedStarExchangeString(this.paramStoreItemId);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(redStarExchangeString);
		}
	}

	// Token: 0x06003416 RID: 13334 RVA: 0x0011C9B8 File Offset: 0x0011ABB8
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
	}

	// Token: 0x06003417 RID: 13335 RVA: 0x0011C9C4 File Offset: 0x0011ABC4
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x1700071E RID: 1822
	// (get) Token: 0x06003418 RID: 13336 RVA: 0x0011C9C8 File Offset: 0x0011ABC8
	// (set) Token: 0x06003419 RID: 13337 RVA: 0x0011C9D0 File Offset: 0x0011ABD0
	public int paramStoreItemId { get; set; }

	// Token: 0x0600341A RID: 13338 RVA: 0x0011C9DC File Offset: 0x0011ABDC
	private void SetParameter_StoreItemId()
	{
		base.WriteActionParamValue("itemId", this.paramStoreItemId);
	}

	// Token: 0x1700071F RID: 1823
	// (get) Token: 0x0600341B RID: 13339 RVA: 0x0011C9F4 File Offset: 0x0011ABF4
	// (set) Token: 0x0600341C RID: 13340 RVA: 0x0011C9FC File Offset: 0x0011ABFC
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x0600341D RID: 13341 RVA: 0x0011CA08 File Offset: 0x0011AC08
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}
}
