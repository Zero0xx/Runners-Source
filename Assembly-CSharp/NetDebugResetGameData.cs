using System;
using LitJson;

// Token: 0x020006D7 RID: 1751
public class NetDebugResetGameData : NetBase
{
	// Token: 0x06002EDF RID: 11999 RVA: 0x001123D4 File Offset: 0x001105D4
	protected override void DoRequest()
	{
		base.SetAction("Debug/resetGameData");
	}

	// Token: 0x06002EE0 RID: 12000 RVA: 0x001123E4 File Offset: 0x001105E4
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06002EE1 RID: 12001 RVA: 0x001123E8 File Offset: 0x001105E8
	protected override void DoDidSuccessEmulation()
	{
	}
}
