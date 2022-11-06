using System;
using LitJson;

// Token: 0x020006D8 RID: 1752
public class NetDebugResetUserData : NetBase
{
	// Token: 0x06002EE2 RID: 12002 RVA: 0x001123EC File Offset: 0x001105EC
	public NetDebugResetUserData()
	{
	}

	// Token: 0x06002EE3 RID: 12003 RVA: 0x001123F4 File Offset: 0x001105F4
	public NetDebugResetUserData(int userId)
	{
		this.paramUserId = userId;
	}

	// Token: 0x06002EE4 RID: 12004 RVA: 0x00112404 File Offset: 0x00110604
	protected override void DoRequest()
	{
		base.SetAction("Debug/resetUserData");
		this.SetParameter_User();
	}

	// Token: 0x06002EE5 RID: 12005 RVA: 0x00112418 File Offset: 0x00110618
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06002EE6 RID: 12006 RVA: 0x0011241C File Offset: 0x0011061C
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000626 RID: 1574
	// (get) Token: 0x06002EE7 RID: 12007 RVA: 0x00112420 File Offset: 0x00110620
	// (set) Token: 0x06002EE8 RID: 12008 RVA: 0x00112428 File Offset: 0x00110628
	public int paramUserId { get; set; }

	// Token: 0x06002EE9 RID: 12009 RVA: 0x00112434 File Offset: 0x00110634
	private void SetParameter_User()
	{
		base.WriteActionParamValue("userId", this.paramUserId);
	}
}
