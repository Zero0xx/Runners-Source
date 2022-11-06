using System;
using LitJson;

// Token: 0x02000696 RID: 1686
public class NetServerAddSpecialEgg : NetBase
{
	// Token: 0x06002D61 RID: 11617 RVA: 0x0010FC04 File Offset: 0x0010DE04
	public NetServerAddSpecialEgg() : this(0)
	{
	}

	// Token: 0x06002D62 RID: 11618 RVA: 0x0010FC10 File Offset: 0x0010DE10
	public NetServerAddSpecialEgg(int numSpecialEgg)
	{
		this.numSpecialEgg = numSpecialEgg;
		this.resultSpecialEgg = 0;
	}

	// Token: 0x06002D63 RID: 11619 RVA: 0x0010FC28 File Offset: 0x0010DE28
	protected override void DoRequest()
	{
		base.SetAction("Chao/addSpecialEgg");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string addSpecialEggString = instance.GetAddSpecialEggString(this.numSpecialEgg);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(addSpecialEggString);
		}
	}

	// Token: 0x06002D64 RID: 11620 RVA: 0x0010FC70 File Offset: 0x0010DE70
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_Data(jdata);
	}

	// Token: 0x06002D65 RID: 11621 RVA: 0x0010FC7C File Offset: 0x0010DE7C
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170005E0 RID: 1504
	// (get) Token: 0x06002D66 RID: 11622 RVA: 0x0010FC80 File Offset: 0x0010DE80
	// (set) Token: 0x06002D67 RID: 11623 RVA: 0x0010FC88 File Offset: 0x0010DE88
	public int numSpecialEgg { get; set; }

	// Token: 0x06002D68 RID: 11624 RVA: 0x0010FC94 File Offset: 0x0010DE94
	private void SetParameter_Data()
	{
		base.WriteActionParamValue("numSpecialEgg", this.numSpecialEgg);
	}

	// Token: 0x170005E1 RID: 1505
	// (get) Token: 0x06002D69 RID: 11625 RVA: 0x0010FCAC File Offset: 0x0010DEAC
	// (set) Token: 0x06002D6A RID: 11626 RVA: 0x0010FCB4 File Offset: 0x0010DEB4
	public int resultSpecialEgg { get; set; }

	// Token: 0x06002D6B RID: 11627 RVA: 0x0010FCC0 File Offset: 0x0010DEC0
	private void GetResponse_Data(JsonData jdata)
	{
		this.resultSpecialEgg = NetUtil.GetJsonInt(jdata, "numSpecialEgg");
	}
}
