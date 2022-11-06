using System;
using LitJson;

// Token: 0x0200074F RID: 1871
public class NetServerGetCountry : NetBase
{
	// Token: 0x060031C5 RID: 12741 RVA: 0x00117D24 File Offset: 0x00115F24
	protected override void DoRequest()
	{
		base.SetAction("Login/getCountry");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x060031C6 RID: 12742 RVA: 0x00117D5C File Offset: 0x00115F5C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_Country(jdata);
	}

	// Token: 0x060031C7 RID: 12743 RVA: 0x00117D68 File Offset: 0x00115F68
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170006B4 RID: 1716
	// (get) Token: 0x060031C8 RID: 12744 RVA: 0x00117D6C File Offset: 0x00115F6C
	// (set) Token: 0x060031C9 RID: 12745 RVA: 0x00117D74 File Offset: 0x00115F74
	public int resultCountryId { get; set; }

	// Token: 0x170006B5 RID: 1717
	// (get) Token: 0x060031CA RID: 12746 RVA: 0x00117D80 File Offset: 0x00115F80
	// (set) Token: 0x060031CB RID: 12747 RVA: 0x00117D88 File Offset: 0x00115F88
	public string resultCountryCode { get; set; }

	// Token: 0x060031CC RID: 12748 RVA: 0x00117D94 File Offset: 0x00115F94
	private void GetResponse_Country(JsonData jdata)
	{
		this.resultCountryId = NetUtil.GetJsonInt(jdata, "countryId");
		this.resultCountryCode = NetUtil.GetJsonString(jdata, "countryCode");
	}
}
