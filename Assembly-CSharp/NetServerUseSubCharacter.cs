using System;
using LitJson;

// Token: 0x020006AE RID: 1710
public class NetServerUseSubCharacter : NetBase
{
	// Token: 0x06002DFE RID: 11774 RVA: 0x00110A80 File Offset: 0x0010EC80
	public NetServerUseSubCharacter() : this(false)
	{
	}

	// Token: 0x06002DFF RID: 11775 RVA: 0x00110A8C File Offset: 0x0010EC8C
	public NetServerUseSubCharacter(bool useFlag)
	{
		this.useFlag = useFlag;
	}

	// Token: 0x06002E00 RID: 11776 RVA: 0x00110A9C File Offset: 0x0010EC9C
	protected override void DoRequest()
	{
		base.SetAction("Character/useSubCharacter");
		this.SetParameter_UseFlag();
	}

	// Token: 0x06002E01 RID: 11777 RVA: 0x00110AB0 File Offset: 0x0010ECB0
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
	}

	// Token: 0x06002E02 RID: 11778 RVA: 0x00110ABC File Offset: 0x0010ECBC
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170005FC RID: 1532
	// (get) Token: 0x06002E03 RID: 11779 RVA: 0x00110AC0 File Offset: 0x0010ECC0
	// (set) Token: 0x06002E04 RID: 11780 RVA: 0x00110AC8 File Offset: 0x0010ECC8
	public bool useFlag { get; set; }

	// Token: 0x170005FD RID: 1533
	// (get) Token: 0x06002E05 RID: 11781 RVA: 0x00110AD4 File Offset: 0x0010ECD4
	// (set) Token: 0x06002E06 RID: 11782 RVA: 0x00110ADC File Offset: 0x0010ECDC
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x06002E07 RID: 11783 RVA: 0x00110AE8 File Offset: 0x0010ECE8
	private void SetParameter_UseFlag()
	{
		base.WriteActionParamValue("use_flag", (!this.useFlag) ? 0 : 1);
	}

	// Token: 0x06002E08 RID: 11784 RVA: 0x00110B18 File Offset: 0x0010ED18
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}
}
