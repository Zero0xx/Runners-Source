using System;
using LitJson;

// Token: 0x02000716 RID: 1814
public class NetServerGetCampaignList : NetBase
{
	// Token: 0x06003031 RID: 12337 RVA: 0x00114664 File Offset: 0x00112864
	protected override void DoRequest()
	{
		base.SetAction("Game/getCampaignList");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x06003032 RID: 12338 RVA: 0x0011469C File Offset: 0x0011289C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_CampaignList(jdata);
	}

	// Token: 0x06003033 RID: 12339 RVA: 0x001146A8 File Offset: 0x001128A8
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x06003034 RID: 12340 RVA: 0x001146AC File Offset: 0x001128AC
	private void GetResponse_CampaignList(JsonData jdata)
	{
		NetUtil.GetResponse_CampaignList(jdata);
	}
}
