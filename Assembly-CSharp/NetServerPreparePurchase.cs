using System;
using LitJson;

// Token: 0x020007AD RID: 1965
public class NetServerPreparePurchase : NetBase
{
	// Token: 0x06003400 RID: 13312 RVA: 0x0011C81C File Offset: 0x0011AA1C
	public NetServerPreparePurchase() : this(0)
	{
	}

	// Token: 0x06003401 RID: 13313 RVA: 0x0011C828 File Offset: 0x0011AA28
	public NetServerPreparePurchase(int itemId)
	{
		this.paramItemId = itemId;
	}

	// Token: 0x06003402 RID: 13314 RVA: 0x0011C838 File Offset: 0x0011AA38
	protected override void DoRequest()
	{
		base.SetAction("Store/preparePurchase");
		this.SetParameter_ItemId();
	}

	// Token: 0x06003403 RID: 13315 RVA: 0x0011C84C File Offset: 0x0011AA4C
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06003404 RID: 13316 RVA: 0x0011C850 File Offset: 0x0011AA50
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x1700071B RID: 1819
	// (get) Token: 0x06003405 RID: 13317 RVA: 0x0011C854 File Offset: 0x0011AA54
	// (set) Token: 0x06003406 RID: 13318 RVA: 0x0011C85C File Offset: 0x0011AA5C
	public int paramItemId { get; set; }

	// Token: 0x06003407 RID: 13319 RVA: 0x0011C868 File Offset: 0x0011AA68
	private void SetParameter_ItemId()
	{
		base.WriteActionParamValue("itemId", this.paramItemId);
	}
}
