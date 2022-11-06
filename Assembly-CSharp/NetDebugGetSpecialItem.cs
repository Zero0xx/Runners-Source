using System;
using LitJson;

// Token: 0x020006D5 RID: 1749
public class NetDebugGetSpecialItem : NetBase
{
	// Token: 0x06002EBD RID: 11965 RVA: 0x0011211C File Offset: 0x0011031C
	public NetDebugGetSpecialItem() : this(0, 0)
	{
	}

	// Token: 0x06002EBE RID: 11966 RVA: 0x00112128 File Offset: 0x00110328
	public NetDebugGetSpecialItem(int itemId, int addQuantity)
	{
		this.paramItemId = itemId;
		this.paramAddQuantity = addQuantity;
	}

	// Token: 0x06002EBF RID: 11967 RVA: 0x00112140 File Offset: 0x00110340
	protected override void DoRequest()
	{
		base.SetAction("Debug/getSpecialItem");
		this.SetParameter_Item();
	}

	// Token: 0x06002EC0 RID: 11968 RVA: 0x00112154 File Offset: 0x00110354
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06002EC1 RID: 11969 RVA: 0x00112158 File Offset: 0x00110358
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x1700061D RID: 1565
	// (get) Token: 0x06002EC2 RID: 11970 RVA: 0x0011215C File Offset: 0x0011035C
	// (set) Token: 0x06002EC3 RID: 11971 RVA: 0x00112164 File Offset: 0x00110364
	public int paramItemId { get; set; }

	// Token: 0x1700061E RID: 1566
	// (get) Token: 0x06002EC4 RID: 11972 RVA: 0x00112170 File Offset: 0x00110370
	// (set) Token: 0x06002EC5 RID: 11973 RVA: 0x00112178 File Offset: 0x00110378
	public int paramAddQuantity { get; set; }

	// Token: 0x06002EC6 RID: 11974 RVA: 0x00112184 File Offset: 0x00110384
	private void SetParameter_Item()
	{
		base.WriteActionParamValue("addItemId", this.paramItemId);
		base.WriteActionParamValue("addNumItem", this.paramAddQuantity);
	}
}
