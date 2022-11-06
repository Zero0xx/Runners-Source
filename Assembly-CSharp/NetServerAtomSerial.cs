using System;
using LitJson;

// Token: 0x0200078A RID: 1930
public class NetServerAtomSerial : NetBase
{
	// Token: 0x0600333E RID: 13118 RVA: 0x0011B1BC File Offset: 0x001193BC
	public NetServerAtomSerial() : this(null, null, false)
	{
	}

	// Token: 0x0600333F RID: 13119 RVA: 0x0011B1C8 File Offset: 0x001193C8
	public NetServerAtomSerial(string campaign, string serial, bool new_user)
	{
		this.campaign = campaign;
		this.serial = serial;
		this.new_user = new_user;
	}

	// Token: 0x06003340 RID: 13120 RVA: 0x0011B1F0 File Offset: 0x001193F0
	protected override void DoRequest()
	{
		base.SetAction("Sgn/setSerialCode");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string setSerialCodeString = instance.GetSetSerialCodeString(this.campaign, this.serial, this.new_user);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(setSerialCodeString);
		}
	}

	// Token: 0x06003341 RID: 13121 RVA: 0x0011B244 File Offset: 0x00119444
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06003342 RID: 13122 RVA: 0x0011B248 File Offset: 0x00119448
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170006FE RID: 1790
	// (get) Token: 0x06003343 RID: 13123 RVA: 0x0011B24C File Offset: 0x0011944C
	// (set) Token: 0x06003344 RID: 13124 RVA: 0x0011B254 File Offset: 0x00119454
	public string campaign { get; set; }

	// Token: 0x170006FF RID: 1791
	// (get) Token: 0x06003345 RID: 13125 RVA: 0x0011B260 File Offset: 0x00119460
	// (set) Token: 0x06003346 RID: 13126 RVA: 0x0011B268 File Offset: 0x00119468
	public string serial { get; set; }

	// Token: 0x17000700 RID: 1792
	// (get) Token: 0x06003347 RID: 13127 RVA: 0x0011B274 File Offset: 0x00119474
	// (set) Token: 0x06003348 RID: 13128 RVA: 0x0011B27C File Offset: 0x0011947C
	public bool new_user { get; set; }

	// Token: 0x06003349 RID: 13129 RVA: 0x0011B288 File Offset: 0x00119488
	private void SetParameter_Data()
	{
		base.WriteActionParamValue("campaignId", this.campaign);
		base.WriteActionParamValue("serialCode", this.serial);
		ushort num = 0;
		if (this.new_user)
		{
			num = 1;
		}
		base.WriteActionParamValue("newUser", num.ToString());
	}
}
