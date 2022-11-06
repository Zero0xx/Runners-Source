using System;
using LitJson;

// Token: 0x020006AB RID: 1707
public class NetServerChangeCharacter : NetBase
{
	// Token: 0x06002DD1 RID: 11729 RVA: 0x001106A4 File Offset: 0x0010E8A4
	public NetServerChangeCharacter() : this(0, 0)
	{
	}

	// Token: 0x06002DD2 RID: 11730 RVA: 0x001106B0 File Offset: 0x0010E8B0
	public NetServerChangeCharacter(int mainCharaId, int subCharaId)
	{
		this.mainCharaId = mainCharaId;
		this.subCharaId = subCharaId;
	}

	// Token: 0x06002DD3 RID: 11731 RVA: 0x001106C8 File Offset: 0x0010E8C8
	protected override void DoRequest()
	{
		base.SetAction("Character/changeCharacter");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string changeCharacterString = instance.GetChangeCharacterString(this.mainCharaId, this.subCharaId);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(changeCharacterString);
		}
	}

	// Token: 0x06002DD4 RID: 11732 RVA: 0x00110718 File Offset: 0x0010E918
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
	}

	// Token: 0x06002DD5 RID: 11733 RVA: 0x00110724 File Offset: 0x0010E924
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170005F1 RID: 1521
	// (get) Token: 0x06002DD6 RID: 11734 RVA: 0x00110728 File Offset: 0x0010E928
	// (set) Token: 0x06002DD7 RID: 11735 RVA: 0x00110730 File Offset: 0x0010E930
	public int mainCharaId { get; set; }

	// Token: 0x170005F2 RID: 1522
	// (get) Token: 0x06002DD8 RID: 11736 RVA: 0x0011073C File Offset: 0x0010E93C
	// (set) Token: 0x06002DD9 RID: 11737 RVA: 0x00110744 File Offset: 0x0010E944
	public int subCharaId { get; set; }

	// Token: 0x170005F3 RID: 1523
	// (get) Token: 0x06002DDA RID: 11738 RVA: 0x00110750 File Offset: 0x0010E950
	// (set) Token: 0x06002DDB RID: 11739 RVA: 0x00110758 File Offset: 0x0010E958
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x06002DDC RID: 11740 RVA: 0x00110764 File Offset: 0x0010E964
	private void SetParameter_UseFlag()
	{
		base.WriteActionParamValue("mainCharacterId", this.mainCharaId);
		base.WriteActionParamValue("subCharacterId", this.subCharaId);
	}

	// Token: 0x06002DDD RID: 11741 RVA: 0x001107A0 File Offset: 0x0010E9A0
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}
}
