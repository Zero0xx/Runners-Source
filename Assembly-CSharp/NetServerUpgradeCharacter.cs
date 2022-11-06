using System;
using LitJson;

// Token: 0x020006AD RID: 1709
public class NetServerUpgradeCharacter : NetBase
{
	// Token: 0x06002DEE RID: 11758 RVA: 0x00110934 File Offset: 0x0010EB34
	public NetServerUpgradeCharacter() : this(0, 0)
	{
	}

	// Token: 0x06002DEF RID: 11759 RVA: 0x00110940 File Offset: 0x0010EB40
	public NetServerUpgradeCharacter(int characterId, int abilityId)
	{
		this.paramCharacterId = characterId;
		this.paramAbilityId = abilityId;
	}

	// Token: 0x06002DF0 RID: 11760 RVA: 0x00110958 File Offset: 0x0010EB58
	protected override void DoRequest()
	{
		base.SetAction("Character/upgradeCharacter");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string upgradeCharacterString = instance.GetUpgradeCharacterString(this.paramCharacterId, this.paramAbilityId);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(upgradeCharacterString);
		}
	}

	// Token: 0x06002DF1 RID: 11761 RVA: 0x001109A8 File Offset: 0x0010EBA8
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
		this.GetResponse_CharacterState(jdata);
	}

	// Token: 0x06002DF2 RID: 11762 RVA: 0x001109B8 File Offset: 0x0010EBB8
	protected override void DoDidSuccessEmulation()
	{
		this.resultPlayerState = ServerInterface.PlayerState;
		this.resultPlayerState.RefreshFakeState();
	}

	// Token: 0x170005F8 RID: 1528
	// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x001109D0 File Offset: 0x0010EBD0
	// (set) Token: 0x06002DF4 RID: 11764 RVA: 0x001109D8 File Offset: 0x0010EBD8
	public int paramCharacterId { get; set; }

	// Token: 0x170005F9 RID: 1529
	// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x001109E4 File Offset: 0x0010EBE4
	// (set) Token: 0x06002DF6 RID: 11766 RVA: 0x001109EC File Offset: 0x0010EBEC
	public int paramAbilityId { get; set; }

	// Token: 0x06002DF7 RID: 11767 RVA: 0x001109F8 File Offset: 0x0010EBF8
	private void SetParameter()
	{
		base.WriteActionParamValue("characterId", this.paramCharacterId);
		base.WriteActionParamValue("abilityId", this.paramAbilityId);
	}

	// Token: 0x170005FA RID: 1530
	// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x00110A34 File Offset: 0x0010EC34
	// (set) Token: 0x06002DF9 RID: 11769 RVA: 0x00110A3C File Offset: 0x0010EC3C
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x170005FB RID: 1531
	// (get) Token: 0x06002DFA RID: 11770 RVA: 0x00110A48 File Offset: 0x0010EC48
	// (set) Token: 0x06002DFB RID: 11771 RVA: 0x00110A50 File Offset: 0x0010EC50
	public ServerCharacterState[] resultCharacterState { get; private set; }

	// Token: 0x06002DFC RID: 11772 RVA: 0x00110A5C File Offset: 0x0010EC5C
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}

	// Token: 0x06002DFD RID: 11773 RVA: 0x00110A70 File Offset: 0x0010EC70
	private void GetResponse_CharacterState(JsonData jdata)
	{
		this.resultCharacterState = NetUtil.AnalyzePlayerState_CharactersStates(jdata);
	}
}
