using System;
using LitJson;

// Token: 0x020007AA RID: 1962
public class NetServerBuyIos : NetBase
{
	// Token: 0x060033DE RID: 13278 RVA: 0x0011C550 File Offset: 0x0011A750
	public NetServerBuyIos() : this(string.Empty)
	{
	}

	// Token: 0x060033DF RID: 13279 RVA: 0x0011C560 File Offset: 0x0011A760
	public NetServerBuyIos(string receiptData)
	{
		this.receipt_data = receiptData;
	}

	// Token: 0x060033E0 RID: 13280 RVA: 0x0011C570 File Offset: 0x0011A770
	protected override void DoRequest()
	{
		base.SetAction("Store/buyIos");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string buyIosString = instance.GetBuyIosString(this.receipt_data);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(buyIosString);
		}
	}

	// Token: 0x060033E1 RID: 13281 RVA: 0x0011C5B8 File Offset: 0x0011A7B8
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
	}

	// Token: 0x060033E2 RID: 13282 RVA: 0x0011C5C4 File Offset: 0x0011A7C4
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000715 RID: 1813
	// (get) Token: 0x060033E3 RID: 13283 RVA: 0x0011C5C8 File Offset: 0x0011A7C8
	// (set) Token: 0x060033E4 RID: 13284 RVA: 0x0011C5D0 File Offset: 0x0011A7D0
	public string receipt_data { get; set; }

	// Token: 0x17000716 RID: 1814
	// (get) Token: 0x060033E5 RID: 13285 RVA: 0x0011C5DC File Offset: 0x0011A7DC
	// (set) Token: 0x060033E6 RID: 13286 RVA: 0x0011C5E4 File Offset: 0x0011A7E4
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x060033E7 RID: 13287 RVA: 0x0011C5F0 File Offset: 0x0011A7F0
	private void SetParameter_ReceiptData()
	{
		base.WriteActionParamValue("receipt_data", this.receipt_data);
	}

	// Token: 0x060033E8 RID: 13288 RVA: 0x0011C604 File Offset: 0x0011A804
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}
}
