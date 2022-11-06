using System;
using LitJson;

// Token: 0x020006DE RID: 1758
public class NetDebugUpdUserData : NetBase
{
	// Token: 0x06002F24 RID: 12068 RVA: 0x00112970 File Offset: 0x00110B70
	public NetDebugUpdUserData() : this(0)
	{
	}

	// Token: 0x06002F25 RID: 12069 RVA: 0x0011297C File Offset: 0x00110B7C
	public NetDebugUpdUserData(int addRank)
	{
		this.paramAddRank = addRank;
	}

	// Token: 0x06002F26 RID: 12070 RVA: 0x0011298C File Offset: 0x00110B8C
	protected override void DoRequest()
	{
		base.SetAction("Debug/updUserData");
		this.SetParameter_AddRank();
	}

	// Token: 0x06002F27 RID: 12071 RVA: 0x001129A0 File Offset: 0x00110BA0
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06002F28 RID: 12072 RVA: 0x001129A4 File Offset: 0x00110BA4
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000634 RID: 1588
	// (get) Token: 0x06002F29 RID: 12073 RVA: 0x001129A8 File Offset: 0x00110BA8
	// (set) Token: 0x06002F2A RID: 12074 RVA: 0x001129B0 File Offset: 0x00110BB0
	public int paramAddRank { get; set; }

	// Token: 0x06002F2B RID: 12075 RVA: 0x001129BC File Offset: 0x00110BBC
	private void SetParameter_AddRank()
	{
		base.WriteActionParamValue("addRank", this.paramAddRank);
	}
}
