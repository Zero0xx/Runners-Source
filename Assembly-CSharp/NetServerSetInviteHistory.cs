using System;
using LitJson;

// Token: 0x02000704 RID: 1796
public class NetServerSetInviteHistory : NetBase
{
	// Token: 0x06003005 RID: 12293 RVA: 0x001141FC File Offset: 0x001123FC
	public NetServerSetInviteHistory() : this(string.Empty)
	{
	}

	// Token: 0x06003006 RID: 12294 RVA: 0x0011420C File Offset: 0x0011240C
	public NetServerSetInviteHistory(string facebookIdHash)
	{
		this.facebookIdHash = facebookIdHash;
	}

	// Token: 0x06003007 RID: 12295 RVA: 0x0011421C File Offset: 0x0011241C
	protected override void DoRequest()
	{
		base.SetAction("Friend/setInviteHistory");
		this.SetParameter_Data();
	}

	// Token: 0x06003008 RID: 12296 RVA: 0x00114230 File Offset: 0x00112430
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06003009 RID: 12297 RVA: 0x00114234 File Offset: 0x00112434
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x0600300A RID: 12298 RVA: 0x00114238 File Offset: 0x00112438
	private void SetParameter_Data()
	{
		base.WriteActionParamValue("facebookIdHash", this.facebookIdHash);
	}

	// Token: 0x04002ABA RID: 10938
	public string facebookIdHash;
}
