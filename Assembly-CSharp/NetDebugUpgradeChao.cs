using System;
using LitJson;

// Token: 0x020006DF RID: 1759
public class NetDebugUpgradeChao : NetBase
{
	// Token: 0x06002F2C RID: 12076 RVA: 0x001129D4 File Offset: 0x00110BD4
	public NetDebugUpgradeChao() : this(0, 0)
	{
	}

	// Token: 0x06002F2D RID: 12077 RVA: 0x001129E0 File Offset: 0x00110BE0
	public NetDebugUpgradeChao(int chaoId, int level)
	{
		this.paramChaoId = chaoId;
		this.paramLevel = level;
	}

	// Token: 0x06002F2E RID: 12078 RVA: 0x001129F8 File Offset: 0x00110BF8
	protected override void DoRequest()
	{
		base.SetAction("Debug/upgradeChao");
		this.SetParameter_Chao();
	}

	// Token: 0x06002F2F RID: 12079 RVA: 0x00112A0C File Offset: 0x00110C0C
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06002F30 RID: 12080 RVA: 0x00112A10 File Offset: 0x00110C10
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000635 RID: 1589
	// (get) Token: 0x06002F31 RID: 12081 RVA: 0x00112A14 File Offset: 0x00110C14
	// (set) Token: 0x06002F32 RID: 12082 RVA: 0x00112A1C File Offset: 0x00110C1C
	public int paramChaoId { get; set; }

	// Token: 0x17000636 RID: 1590
	// (get) Token: 0x06002F33 RID: 12083 RVA: 0x00112A28 File Offset: 0x00110C28
	// (set) Token: 0x06002F34 RID: 12084 RVA: 0x00112A30 File Offset: 0x00110C30
	public int paramLevel { get; set; }

	// Token: 0x06002F35 RID: 12085 RVA: 0x00112A3C File Offset: 0x00110C3C
	private void SetParameter_Chao()
	{
		base.WriteActionParamValue("chaoId", this.paramChaoId);
		base.WriteActionParamValue("lv", this.paramLevel);
	}
}
