using System;
using LitJson;

// Token: 0x020006AC RID: 1708
public class NetServerUnlockedCharacter : NetBase
{
	// Token: 0x06002DDE RID: 11742 RVA: 0x001107B4 File Offset: 0x0010E9B4
	public NetServerUnlockedCharacter(CharaType charaType, ServerItem serverItem)
	{
		this.charaType = charaType;
		this.serverItem = serverItem;
	}

	// Token: 0x06002DDF RID: 11743 RVA: 0x001107CC File Offset: 0x0010E9CC
	protected override void DoRequest()
	{
		base.SetAction("Character/unlockedCharacter");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			int characterId = 0;
			ServerCharacterState serverCharacterState = ServerInterface.PlayerState.CharacterState(this.charaType);
			if (serverCharacterState != null)
			{
				characterId = serverCharacterState.Id;
			}
			string unlockedCharacterString = instance.GetUnlockedCharacterString(characterId, (int)this.serverItem.id);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(unlockedCharacterString);
		}
	}

	// Token: 0x06002DE0 RID: 11744 RVA: 0x00110840 File Offset: 0x0010EA40
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
		this.GetResponse_CharacterState(jdata);
	}

	// Token: 0x06002DE1 RID: 11745 RVA: 0x00110850 File Offset: 0x0010EA50
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170005F4 RID: 1524
	// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x00110854 File Offset: 0x0010EA54
	// (set) Token: 0x06002DE3 RID: 11747 RVA: 0x0011085C File Offset: 0x0010EA5C
	public CharaType charaType { get; set; }

	// Token: 0x170005F5 RID: 1525
	// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x00110868 File Offset: 0x0010EA68
	// (set) Token: 0x06002DE5 RID: 11749 RVA: 0x00110870 File Offset: 0x0010EA70
	public ServerItem serverItem { get; set; }

	// Token: 0x170005F6 RID: 1526
	// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x0011087C File Offset: 0x0010EA7C
	// (set) Token: 0x06002DE7 RID: 11751 RVA: 0x00110884 File Offset: 0x0010EA84
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x170005F7 RID: 1527
	// (get) Token: 0x06002DE8 RID: 11752 RVA: 0x00110890 File Offset: 0x0010EA90
	// (set) Token: 0x06002DE9 RID: 11753 RVA: 0x00110898 File Offset: 0x0010EA98
	public ServerCharacterState[] resultCharacterState { get; private set; }

	// Token: 0x06002DEA RID: 11754 RVA: 0x001108A4 File Offset: 0x0010EAA4
	private void SetParameter_CharaType()
	{
		ServerCharacterState serverCharacterState = ServerInterface.PlayerState.CharacterState(this.charaType);
		if (serverCharacterState != null)
		{
			int id = serverCharacterState.Id;
			base.WriteActionParamValue("characterId", id);
		}
	}

	// Token: 0x06002DEB RID: 11755 RVA: 0x001108E0 File Offset: 0x0010EAE0
	private void SetParameter_Item()
	{
		int id = (int)this.serverItem.id;
		base.WriteActionParamValue("itemId", id);
	}

	// Token: 0x06002DEC RID: 11756 RVA: 0x00110910 File Offset: 0x0010EB10
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}

	// Token: 0x06002DED RID: 11757 RVA: 0x00110924 File Offset: 0x0010EB24
	private void GetResponse_CharacterState(JsonData jdata)
	{
		this.resultCharacterState = NetUtil.AnalyzePlayerState_CharactersStates(jdata);
	}
}
