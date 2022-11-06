using System;
using LitJson;

// Token: 0x020006D3 RID: 1747
public class NetDebugDeleteUserData : NetBase
{
	// Token: 0x06002EAA RID: 11946 RVA: 0x00112000 File Offset: 0x00110200
	public NetDebugDeleteUserData() : this(0)
	{
	}

	// Token: 0x06002EAB RID: 11947 RVA: 0x0011200C File Offset: 0x0011020C
	public NetDebugDeleteUserData(int userId)
	{
		this.paramUserId = userId;
	}

	// Token: 0x06002EAC RID: 11948 RVA: 0x0011201C File Offset: 0x0011021C
	protected override void DoRequest()
	{
		base.SetAction("Debug/deleteUserData");
		this.SetParameter_User();
	}

	// Token: 0x06002EAD RID: 11949 RVA: 0x00112030 File Offset: 0x00110230
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06002EAE RID: 11950 RVA: 0x00112034 File Offset: 0x00110234
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000619 RID: 1561
	// (get) Token: 0x06002EAF RID: 11951 RVA: 0x00112038 File Offset: 0x00110238
	// (set) Token: 0x06002EB0 RID: 11952 RVA: 0x00112040 File Offset: 0x00110240
	public int paramUserId { get; set; }

	// Token: 0x06002EB1 RID: 11953 RVA: 0x0011204C File Offset: 0x0011024C
	private void SetParameter_User()
	{
		base.WriteActionParamValue("userId", this.paramUserId);
	}
}
