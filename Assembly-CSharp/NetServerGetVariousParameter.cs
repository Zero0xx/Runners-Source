using System;
using LitJson;

// Token: 0x02000753 RID: 1875
public class NetServerGetVariousParameter : NetBase
{
	// Token: 0x060031E8 RID: 12776 RVA: 0x00118508 File Offset: 0x00116708
	protected override void DoRequest()
	{
		base.SetAction("Login/getVariousParameter");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x060031E9 RID: 12777 RVA: 0x00118540 File Offset: 0x00116740
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponseVariousParameter(jdata);
	}

	// Token: 0x060031EA RID: 12778 RVA: 0x0011854C File Offset: 0x0011674C
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170006B8 RID: 1720
	// (get) Token: 0x060031EB RID: 12779 RVA: 0x00118550 File Offset: 0x00116750
	// (set) Token: 0x060031EC RID: 12780 RVA: 0x00118558 File Offset: 0x00116758
	public int resultEnergyRecveryTime { get; set; }

	// Token: 0x170006B9 RID: 1721
	// (get) Token: 0x060031ED RID: 12781 RVA: 0x00118564 File Offset: 0x00116764
	// (set) Token: 0x060031EE RID: 12782 RVA: 0x0011856C File Offset: 0x0011676C
	public int resultEnergyRecoveryMax { get; set; }

	// Token: 0x170006BA RID: 1722
	// (get) Token: 0x060031EF RID: 12783 RVA: 0x00118578 File Offset: 0x00116778
	// (set) Token: 0x060031F0 RID: 12784 RVA: 0x00118580 File Offset: 0x00116780
	public int resultOnePlayCmCount { get; set; }

	// Token: 0x170006BB RID: 1723
	// (get) Token: 0x060031F1 RID: 12785 RVA: 0x0011858C File Offset: 0x0011678C
	// (set) Token: 0x060031F2 RID: 12786 RVA: 0x00118594 File Offset: 0x00116794
	public int resultOnePlayContinueCount { get; set; }

	// Token: 0x170006BC RID: 1724
	// (get) Token: 0x060031F3 RID: 12787 RVA: 0x001185A0 File Offset: 0x001167A0
	// (set) Token: 0x060031F4 RID: 12788 RVA: 0x001185A8 File Offset: 0x001167A8
	public int resultCmSkipCount { get; set; }

	// Token: 0x170006BD RID: 1725
	// (get) Token: 0x060031F5 RID: 12789 RVA: 0x001185B4 File Offset: 0x001167B4
	// (set) Token: 0x060031F6 RID: 12790 RVA: 0x001185BC File Offset: 0x001167BC
	public bool resultIsPurchased { get; set; }

	// Token: 0x060031F7 RID: 12791 RVA: 0x001185C8 File Offset: 0x001167C8
	private void GetResponseVariousParameter(JsonData jdata)
	{
		this.resultEnergyRecveryTime = NetUtil.GetJsonInt(jdata, "energyRecveryTime");
		this.resultEnergyRecoveryMax = NetUtil.GetJsonInt(jdata, "energyRecoveryMax");
		this.resultOnePlayCmCount = NetUtil.GetJsonInt(jdata, "onePlayCmCount");
		this.resultOnePlayContinueCount = NetUtil.GetJsonInt(jdata, "onePlayContinueCount");
		this.resultCmSkipCount = NetUtil.GetJsonInt(jdata, "cmSkipCount");
		this.resultIsPurchased = NetUtil.GetJsonFlag(jdata, "isPurchased");
	}
}
