using System;
using LitJson;

// Token: 0x020007B0 RID: 1968
public class NetServerRingExchange : NetBase
{
	// Token: 0x0600341E RID: 13342 RVA: 0x0011CA1C File Offset: 0x0011AC1C
	public NetServerRingExchange() : this(0, 0)
	{
	}

	// Token: 0x0600341F RID: 13343 RVA: 0x0011CA28 File Offset: 0x0011AC28
	public NetServerRingExchange(int itemId, int itemNum)
	{
		this.itemId = itemId;
		this.itemNum = itemNum;
	}

	// Token: 0x06003420 RID: 13344 RVA: 0x0011CA40 File Offset: 0x0011AC40
	protected override void DoRequest()
	{
		base.SetAction("Store/ringExchange");
		this.SetParameter_ItemData();
	}

	// Token: 0x06003421 RID: 13345 RVA: 0x0011CA54 File Offset: 0x0011AC54
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
	}

	// Token: 0x06003422 RID: 13346 RVA: 0x0011CA60 File Offset: 0x0011AC60
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000720 RID: 1824
	// (get) Token: 0x06003423 RID: 13347 RVA: 0x0011CA64 File Offset: 0x0011AC64
	// (set) Token: 0x06003424 RID: 13348 RVA: 0x0011CA6C File Offset: 0x0011AC6C
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x06003425 RID: 13349 RVA: 0x0011CA78 File Offset: 0x0011AC78
	private void SetParameter_ItemData()
	{
		base.WriteActionParamValue("itemId", this.itemId);
		base.WriteActionParamValue("itemNum", this.itemNum);
	}

	// Token: 0x06003426 RID: 13350 RVA: 0x0011CAB4 File Offset: 0x0011ACB4
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}

	// Token: 0x04002C06 RID: 11270
	public int itemId;

	// Token: 0x04002C07 RID: 11271
	public int itemNum;
}
