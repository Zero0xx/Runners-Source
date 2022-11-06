using System;
using LitJson;

// Token: 0x020006E0 RID: 1760
public class NetDebugUpgradeCharacter : NetBase
{
	// Token: 0x06002F36 RID: 12086 RVA: 0x00112A78 File Offset: 0x00110C78
	public NetDebugUpgradeCharacter() : this(0, 0)
	{
	}

	// Token: 0x06002F37 RID: 12087 RVA: 0x00112A84 File Offset: 0x00110C84
	public NetDebugUpgradeCharacter(int characterId, int level)
	{
		this.paramCharacterId = characterId;
		this.paramLevel = level;
	}

	// Token: 0x06002F38 RID: 12088 RVA: 0x00112A9C File Offset: 0x00110C9C
	protected override void DoRequest()
	{
		base.SetAction("Debug/upgradeCharacter");
		this.SetParameter_Character();
	}

	// Token: 0x06002F39 RID: 12089 RVA: 0x00112AB0 File Offset: 0x00110CB0
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06002F3A RID: 12090 RVA: 0x00112AB4 File Offset: 0x00110CB4
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000637 RID: 1591
	// (get) Token: 0x06002F3B RID: 12091 RVA: 0x00112AB8 File Offset: 0x00110CB8
	// (set) Token: 0x06002F3C RID: 12092 RVA: 0x00112AC0 File Offset: 0x00110CC0
	public int paramCharacterId { get; set; }

	// Token: 0x17000638 RID: 1592
	// (get) Token: 0x06002F3D RID: 12093 RVA: 0x00112ACC File Offset: 0x00110CCC
	// (set) Token: 0x06002F3E RID: 12094 RVA: 0x00112AD4 File Offset: 0x00110CD4
	public int paramLevel { get; set; }

	// Token: 0x06002F3F RID: 12095 RVA: 0x00112AE0 File Offset: 0x00110CE0
	private void SetParameter_Character()
	{
		base.WriteActionParamValue("characterId", this.paramCharacterId);
		base.WriteActionParamValue("lv", this.paramLevel);
	}
}
