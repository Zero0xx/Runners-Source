using System;
using LitJson;

// Token: 0x02000713 RID: 1811
public class NetServerActRetry : NetBase
{
	// Token: 0x06003024 RID: 12324 RVA: 0x00114558 File Offset: 0x00112758
	protected override void DoRequest()
	{
		base.SetAction("Game/actRetry");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string actRetryString = instance.GetActRetryString();
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(actRetryString);
		}
	}

	// Token: 0x06003025 RID: 12325 RVA: 0x0011459C File Offset: 0x0011279C
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06003026 RID: 12326 RVA: 0x001145A0 File Offset: 0x001127A0
	protected override void DoDidSuccessEmulation()
	{
	}
}
