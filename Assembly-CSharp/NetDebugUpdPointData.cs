using System;
using LitJson;

// Token: 0x020006DC RID: 1756
public class NetDebugUpdPointData : NetBase
{
	// Token: 0x06002F04 RID: 12036 RVA: 0x001126F0 File Offset: 0x001108F0
	public NetDebugUpdPointData() : this(0, 0, 0, 0, 0, 0)
	{
	}

	// Token: 0x06002F05 RID: 12037 RVA: 0x00112700 File Offset: 0x00110900
	public NetDebugUpdPointData(int addEnergyFree, int addEnergyBuy, int addRingFree, int addRingBuy, int addRedStarRingFree, int addRedStarRingBuy)
	{
		this.paramAddEnergyFree = addEnergyFree;
		this.paramAddEnergyBuy = addEnergyBuy;
		this.paramAddRingFree = addRingFree;
		this.paramAddRingBuy = addRingBuy;
		this.paramAddRedRingFree = addRedStarRingFree;
		this.paramAddRedRingBuy = addRedStarRingBuy;
	}

	// Token: 0x06002F06 RID: 12038 RVA: 0x00112740 File Offset: 0x00110940
	protected override void DoRequest()
	{
		base.SetAction("Debug/updPointData");
		this.SetParameter_AddEnergy();
		this.SetParameter_AddRing();
		this.SetParameter_AddRedStarRing();
	}

	// Token: 0x06002F07 RID: 12039 RVA: 0x00112760 File Offset: 0x00110960
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06002F08 RID: 12040 RVA: 0x00112764 File Offset: 0x00110964
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x1700062B RID: 1579
	// (get) Token: 0x06002F09 RID: 12041 RVA: 0x00112768 File Offset: 0x00110968
	// (set) Token: 0x06002F0A RID: 12042 RVA: 0x00112770 File Offset: 0x00110970
	public int paramAddEnergyFree { get; set; }

	// Token: 0x1700062C RID: 1580
	// (get) Token: 0x06002F0B RID: 12043 RVA: 0x0011277C File Offset: 0x0011097C
	// (set) Token: 0x06002F0C RID: 12044 RVA: 0x00112784 File Offset: 0x00110984
	public int paramAddEnergyBuy { get; set; }

	// Token: 0x1700062D RID: 1581
	// (get) Token: 0x06002F0D RID: 12045 RVA: 0x00112790 File Offset: 0x00110990
	// (set) Token: 0x06002F0E RID: 12046 RVA: 0x00112798 File Offset: 0x00110998
	public int paramAddRingFree { get; set; }

	// Token: 0x1700062E RID: 1582
	// (get) Token: 0x06002F0F RID: 12047 RVA: 0x001127A4 File Offset: 0x001109A4
	// (set) Token: 0x06002F10 RID: 12048 RVA: 0x001127AC File Offset: 0x001109AC
	public int paramAddRingBuy { get; set; }

	// Token: 0x1700062F RID: 1583
	// (get) Token: 0x06002F11 RID: 12049 RVA: 0x001127B8 File Offset: 0x001109B8
	// (set) Token: 0x06002F12 RID: 12050 RVA: 0x001127C0 File Offset: 0x001109C0
	public int paramAddRedRingFree { get; set; }

	// Token: 0x17000630 RID: 1584
	// (get) Token: 0x06002F13 RID: 12051 RVA: 0x001127CC File Offset: 0x001109CC
	// (set) Token: 0x06002F14 RID: 12052 RVA: 0x001127D4 File Offset: 0x001109D4
	public int paramAddRedRingBuy { get; set; }

	// Token: 0x06002F15 RID: 12053 RVA: 0x001127E0 File Offset: 0x001109E0
	private void SetParameter_AddEnergy()
	{
		base.WriteActionParamValue("addEnergyFree", this.paramAddEnergyFree);
		base.WriteActionParamValue("addEnergyBuy", this.paramAddEnergyBuy);
	}

	// Token: 0x06002F16 RID: 12054 RVA: 0x0011281C File Offset: 0x00110A1C
	private void SetParameter_AddRing()
	{
		base.WriteActionParamValue("addRingFree", this.paramAddRingFree);
		base.WriteActionParamValue("addRingBuy", this.paramAddRingBuy);
	}

	// Token: 0x06002F17 RID: 12055 RVA: 0x00112858 File Offset: 0x00110A58
	private void SetParameter_AddRedStarRing()
	{
		base.WriteActionParamValue("addRedstarFree", this.paramAddRedRingFree);
		base.WriteActionParamValue("addRedstarBuy", this.paramAddRedRingBuy);
	}
}
