using System;
using LitJson;

// Token: 0x020006DD RID: 1757
public class NetDebugUpdRouletteData : NetBase
{
	// Token: 0x06002F18 RID: 12056 RVA: 0x00112894 File Offset: 0x00110A94
	public NetDebugUpdRouletteData() : this(0, 0, 0)
	{
	}

	// Token: 0x06002F19 RID: 12057 RVA: 0x001128A0 File Offset: 0x00110AA0
	public NetDebugUpdRouletteData(int rank, int numRemaining, int itemWon)
	{
		this.paramRank = rank;
		this.paramNumRemaining = numRemaining;
		this.paramItemWon = itemWon;
	}

	// Token: 0x06002F1A RID: 12058 RVA: 0x001128C8 File Offset: 0x00110AC8
	protected override void DoRequest()
	{
		base.SetAction("Debug/updRouletteData");
		this.SetParameter_Roulette();
	}

	// Token: 0x06002F1B RID: 12059 RVA: 0x001128DC File Offset: 0x00110ADC
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06002F1C RID: 12060 RVA: 0x001128E0 File Offset: 0x00110AE0
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000631 RID: 1585
	// (get) Token: 0x06002F1D RID: 12061 RVA: 0x001128E4 File Offset: 0x00110AE4
	// (set) Token: 0x06002F1E RID: 12062 RVA: 0x001128EC File Offset: 0x00110AEC
	public int paramRank { get; set; }

	// Token: 0x17000632 RID: 1586
	// (get) Token: 0x06002F1F RID: 12063 RVA: 0x001128F8 File Offset: 0x00110AF8
	// (set) Token: 0x06002F20 RID: 12064 RVA: 0x00112900 File Offset: 0x00110B00
	public int paramNumRemaining { get; set; }

	// Token: 0x17000633 RID: 1587
	// (get) Token: 0x06002F21 RID: 12065 RVA: 0x0011290C File Offset: 0x00110B0C
	// (set) Token: 0x06002F22 RID: 12066 RVA: 0x00112914 File Offset: 0x00110B14
	public int paramItemWon { get; set; }

	// Token: 0x06002F23 RID: 12067 RVA: 0x00112920 File Offset: 0x00110B20
	private void SetParameter_Roulette()
	{
		base.WriteActionParamValue("rouletteRank", this.paramRank);
		base.WriteActionParamValue("numRemainingRoulette", this.paramNumRemaining);
		base.WriteActionParamValue("itemWon", this.paramItemWon);
	}
}
