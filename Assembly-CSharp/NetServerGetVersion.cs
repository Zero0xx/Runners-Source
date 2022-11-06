using System;
using LitJson;

// Token: 0x02000754 RID: 1876
public class NetServerGetVersion : NetBase
{
	// Token: 0x060031F9 RID: 12793 RVA: 0x00118644 File Offset: 0x00116844
	protected override void DoRequest()
	{
		base.SetAction("Login/getVersionData");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x060031FA RID: 12794 RVA: 0x0011867C File Offset: 0x0011687C
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x060031FB RID: 12795 RVA: 0x00118680 File Offset: 0x00116880
	protected override void DoDidSuccessEmulation()
	{
	}
}
