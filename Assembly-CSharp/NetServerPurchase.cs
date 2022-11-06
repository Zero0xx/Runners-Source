using System;
using LitJson;

// Token: 0x020007AE RID: 1966
public class NetServerPurchase : NetBase
{
	// Token: 0x06003408 RID: 13320 RVA: 0x0011C880 File Offset: 0x0011AA80
	public NetServerPurchase() : this(false)
	{
	}

	// Token: 0x06003409 RID: 13321 RVA: 0x0011C88C File Offset: 0x0011AA8C
	public NetServerPurchase(bool isSuccess)
	{
		this.paramPurchaseResult = isSuccess;
	}

	// Token: 0x0600340A RID: 13322 RVA: 0x0011C89C File Offset: 0x0011AA9C
	protected override void DoRequest()
	{
		base.SetAction("Store/purchase");
		this.SetParameter_PurchaseResult();
	}

	// Token: 0x0600340B RID: 13323 RVA: 0x0011C8B0 File Offset: 0x0011AAB0
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
		if (!this.paramPurchaseResult)
		{
			base.state = NetBase.emState.UnavailableFailed;
			base.resultStCd = ServerInterface.StatusCode.HspPurchaseError;
		}
	}

	// Token: 0x0600340C RID: 13324 RVA: 0x0011C8E4 File Offset: 0x0011AAE4
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x1700071C RID: 1820
	// (get) Token: 0x0600340D RID: 13325 RVA: 0x0011C8E8 File Offset: 0x0011AAE8
	// (set) Token: 0x0600340E RID: 13326 RVA: 0x0011C8F0 File Offset: 0x0011AAF0
	public bool paramPurchaseResult { get; set; }

	// Token: 0x0600340F RID: 13327 RVA: 0x0011C8FC File Offset: 0x0011AAFC
	private void SetParameter_PurchaseResult()
	{
		base.WriteActionParamValue("isSuccess", (!this.paramPurchaseResult) ? 0 : 1);
	}

	// Token: 0x1700071D RID: 1821
	// (get) Token: 0x06003410 RID: 13328 RVA: 0x0011C92C File Offset: 0x0011AB2C
	// (set) Token: 0x06003411 RID: 13329 RVA: 0x0011C934 File Offset: 0x0011AB34
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x06003412 RID: 13330 RVA: 0x0011C940 File Offset: 0x0011AB40
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}
}
