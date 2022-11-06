using System;
using LitJson;

// Token: 0x02000698 RID: 1688
public class NetServerEquipChao : NetBase
{
	// Token: 0x06002D80 RID: 11648 RVA: 0x0010FE38 File Offset: 0x0010E038
	public NetServerEquipChao() : this(0, 0)
	{
	}

	// Token: 0x06002D81 RID: 11649 RVA: 0x0010FE44 File Offset: 0x0010E044
	public NetServerEquipChao(int mainChaoId, int subChaoId)
	{
		this.paramMainChaoId = mainChaoId;
		this.paramSubChaoId = subChaoId;
	}

	// Token: 0x06002D82 RID: 11650 RVA: 0x0010FE5C File Offset: 0x0010E05C
	protected override void DoRequest()
	{
		base.SetAction("Chao/equipChao");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string equipChaoString = instance.GetEquipChaoString(this.paramMainChaoId, this.paramSubChaoId);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(equipChaoString);
		}
	}

	// Token: 0x06002D83 RID: 11651 RVA: 0x0010FEAC File Offset: 0x0010E0AC
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
	}

	// Token: 0x06002D84 RID: 11652 RVA: 0x0010FEB8 File Offset: 0x0010E0B8
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170005E7 RID: 1511
	// (get) Token: 0x06002D85 RID: 11653 RVA: 0x0010FEBC File Offset: 0x0010E0BC
	// (set) Token: 0x06002D86 RID: 11654 RVA: 0x0010FEC4 File Offset: 0x0010E0C4
	public int paramMainChaoId { get; set; }

	// Token: 0x170005E8 RID: 1512
	// (get) Token: 0x06002D87 RID: 11655 RVA: 0x0010FED0 File Offset: 0x0010E0D0
	// (set) Token: 0x06002D88 RID: 11656 RVA: 0x0010FED8 File Offset: 0x0010E0D8
	public int paramSubChaoId { get; set; }

	// Token: 0x06002D89 RID: 11657 RVA: 0x0010FEE4 File Offset: 0x0010E0E4
	private void SetParameter_MainChaoId()
	{
		base.WriteActionParamValue("mainChaoId", this.paramMainChaoId);
	}

	// Token: 0x06002D8A RID: 11658 RVA: 0x0010FEFC File Offset: 0x0010E0FC
	private void SetParameter_SubChaoId()
	{
		base.WriteActionParamValue("subChaoId", this.paramSubChaoId);
	}

	// Token: 0x170005E9 RID: 1513
	// (get) Token: 0x06002D8B RID: 11659 RVA: 0x0010FF14 File Offset: 0x0010E114
	// (set) Token: 0x06002D8C RID: 11660 RVA: 0x0010FF1C File Offset: 0x0010E11C
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x06002D8D RID: 11661 RVA: 0x0010FF28 File Offset: 0x0010E128
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}
}
