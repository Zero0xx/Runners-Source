using System;
using LitJson;

// Token: 0x0200069B RID: 1691
public class NetServerGetPrizeChaoWheelSpin : NetBase
{
	// Token: 0x06002D9F RID: 11679 RVA: 0x0011007C File Offset: 0x0010E27C
	public NetServerGetPrizeChaoWheelSpin(int chaoWheelSpinType)
	{
		this.paramChaoWheelSpinType = chaoWheelSpinType;
	}

	// Token: 0x06002DA0 RID: 11680 RVA: 0x0011008C File Offset: 0x0010E28C
	protected override void DoRequest()
	{
		base.SetAction("Chao/getPrizeChaoWheelSpin");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string getPrizeChaoWheelSpinString = instance.GetGetPrizeChaoWheelSpinString(this.paramChaoWheelSpinType);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(getPrizeChaoWheelSpinString);
		}
	}

	// Token: 0x06002DA1 RID: 11681 RVA: 0x001100D4 File Offset: 0x0010E2D4
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PrizeChaoWheelSpin(jdata);
	}

	// Token: 0x06002DA2 RID: 11682 RVA: 0x001100E0 File Offset: 0x0010E2E0
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x06002DA3 RID: 11683 RVA: 0x001100E4 File Offset: 0x0010E2E4
	private void SetParameter_ChaoWheelSpinType()
	{
		base.WriteActionParamValue("chaoWheelSpinType", this.paramChaoWheelSpinType);
	}

	// Token: 0x06002DA4 RID: 11684 RVA: 0x001100FC File Offset: 0x0010E2FC
	private void GetResponse_PrizeChaoWheelSpin(JsonData jdata)
	{
		this.resultPrizeState = NetUtil.AnalyzePrizeChaoWheelSpin(jdata);
	}

	// Token: 0x040029DE RID: 10718
	public int paramChaoWheelSpinType;

	// Token: 0x040029DF RID: 10719
	public ServerPrizeState resultPrizeState;
}
