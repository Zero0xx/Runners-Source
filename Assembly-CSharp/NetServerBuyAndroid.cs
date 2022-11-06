using System;
using LitJson;

// Token: 0x020007A9 RID: 1961
public class NetServerBuyAndroid : NetBase
{
	// Token: 0x060033D1 RID: 13265 RVA: 0x0011C444 File Offset: 0x0011A644
	public NetServerBuyAndroid() : this(string.Empty, string.Empty)
	{
	}

	// Token: 0x060033D2 RID: 13266 RVA: 0x0011C458 File Offset: 0x0011A658
	public NetServerBuyAndroid(string receiptData, string signature)
	{
		this.receipt_data = receiptData;
		this.signature = signature;
	}

	// Token: 0x060033D3 RID: 13267 RVA: 0x0011C470 File Offset: 0x0011A670
	protected override void DoRequest()
	{
		base.SetAction("Store/buyAndroid");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string buyAndroidString = instance.GetBuyAndroidString(this.receipt_data, this.signature);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(buyAndroidString);
		}
	}

	// Token: 0x060033D4 RID: 13268 RVA: 0x0011C4C0 File Offset: 0x0011A6C0
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
	}

	// Token: 0x060033D5 RID: 13269 RVA: 0x0011C4CC File Offset: 0x0011A6CC
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000712 RID: 1810
	// (get) Token: 0x060033D6 RID: 13270 RVA: 0x0011C4D0 File Offset: 0x0011A6D0
	// (set) Token: 0x060033D7 RID: 13271 RVA: 0x0011C4D8 File Offset: 0x0011A6D8
	public string receipt_data { get; set; }

	// Token: 0x17000713 RID: 1811
	// (get) Token: 0x060033D8 RID: 13272 RVA: 0x0011C4E4 File Offset: 0x0011A6E4
	// (set) Token: 0x060033D9 RID: 13273 RVA: 0x0011C4EC File Offset: 0x0011A6EC
	public string signature { get; set; }

	// Token: 0x17000714 RID: 1812
	// (get) Token: 0x060033DA RID: 13274 RVA: 0x0011C4F8 File Offset: 0x0011A6F8
	// (set) Token: 0x060033DB RID: 13275 RVA: 0x0011C500 File Offset: 0x0011A700
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x060033DC RID: 13276 RVA: 0x0011C50C File Offset: 0x0011A70C
	private void SetParameter_ReceiptData()
	{
		base.WriteActionParamValue("receipt_data", this.receipt_data);
		base.WriteActionParamValue("receipt_signature", this.signature);
	}

	// Token: 0x060033DD RID: 13277 RVA: 0x0011C53C File Offset: 0x0011A73C
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}
}
