using System;
using System.Collections.Generic;

// Token: 0x02000806 RID: 2054
public class ServerLeagueOperatorData
{
	// Token: 0x0600370C RID: 14092 RVA: 0x00123234 File Offset: 0x00121434
	public ServerLeagueOperatorData()
	{
		this.leagueId = 1;
		this.numUp = 10;
		this.numDown = 10;
		this.highScoreOpe = new List<ServerRemainOperator>();
		this.totalScoreOpe = new List<ServerRemainOperator>();
	}

	// Token: 0x1700080A RID: 2058
	// (get) Token: 0x0600370D RID: 14093 RVA: 0x00123274 File Offset: 0x00121474
	// (set) Token: 0x0600370E RID: 14094 RVA: 0x0012327C File Offset: 0x0012147C
	public int leagueId { get; set; }

	// Token: 0x1700080B RID: 2059
	// (get) Token: 0x0600370F RID: 14095 RVA: 0x00123288 File Offset: 0x00121488
	// (set) Token: 0x06003710 RID: 14096 RVA: 0x00123290 File Offset: 0x00121490
	public int numUp { get; set; }

	// Token: 0x1700080C RID: 2060
	// (get) Token: 0x06003711 RID: 14097 RVA: 0x0012329C File Offset: 0x0012149C
	// (set) Token: 0x06003712 RID: 14098 RVA: 0x001232A4 File Offset: 0x001214A4
	public int numDown { get; set; }

	// Token: 0x1700080D RID: 2061
	// (get) Token: 0x06003713 RID: 14099 RVA: 0x001232B0 File Offset: 0x001214B0
	// (set) Token: 0x06003714 RID: 14100 RVA: 0x001232B8 File Offset: 0x001214B8
	public List<ServerRemainOperator> highScoreOpe { get; set; }

	// Token: 0x1700080E RID: 2062
	// (get) Token: 0x06003715 RID: 14101 RVA: 0x001232C4 File Offset: 0x001214C4
	// (set) Token: 0x06003716 RID: 14102 RVA: 0x001232CC File Offset: 0x001214CC
	public List<ServerRemainOperator> totalScoreOpe { get; set; }

	// Token: 0x06003717 RID: 14103 RVA: 0x001232D8 File Offset: 0x001214D8
	public void Dump()
	{
	}

	// Token: 0x06003718 RID: 14104 RVA: 0x001232DC File Offset: 0x001214DC
	public void CopyTo(ServerLeagueOperatorData to)
	{
		to.leagueId = this.leagueId;
		to.numUp = this.numUp;
		to.numDown = this.numDown;
		this.SetServerRemainOperator(this.highScoreOpe, to.highScoreOpe);
		this.SetServerRemainOperator(this.totalScoreOpe, to.totalScoreOpe);
	}

	// Token: 0x06003719 RID: 14105 RVA: 0x00123334 File Offset: 0x00121534
	public void AddHighScoreRemainOperator(ServerRemainOperator remainOperator)
	{
		this.highScoreOpe.Add(remainOperator);
	}

	// Token: 0x0600371A RID: 14106 RVA: 0x00123344 File Offset: 0x00121544
	public void AddTotalScoreRemainOperator(ServerRemainOperator remainOperator)
	{
		this.totalScoreOpe.Add(remainOperator);
	}

	// Token: 0x0600371B RID: 14107 RVA: 0x00123354 File Offset: 0x00121554
	private void SetServerRemainOperator(List<ServerRemainOperator> setData, List<ServerRemainOperator> getData)
	{
		if (setData != null && getData != null && setData.Count > 0)
		{
			getData.Clear();
			for (int i = 0; i < setData.Count; i++)
			{
				getData.Add(setData[i]);
			}
		}
	}
}
