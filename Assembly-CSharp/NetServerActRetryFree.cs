using System;
using LitJson;

// Token: 0x02000714 RID: 1812
public class NetServerActRetryFree : NetBase
{
	// Token: 0x06003028 RID: 12328 RVA: 0x001145AC File Offset: 0x001127AC
	protected override void DoRequest()
	{
		base.SetAction("Game/actRetryFree");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x06003029 RID: 12329 RVA: 0x001145E4 File Offset: 0x001127E4
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x0600302A RID: 12330 RVA: 0x001145E8 File Offset: 0x001127E8
	protected override void DoDidSuccessEmulation()
	{
	}
}
