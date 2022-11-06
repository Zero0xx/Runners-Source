using System;
using System.Collections.Generic;

// Token: 0x02000805 RID: 2053
public class ServerLeagueData
{
	// Token: 0x060036F3 RID: 14067 RVA: 0x00122FF8 File Offset: 0x001211F8
	public ServerLeagueData()
	{
		this.mode = 0;
		this.leagueId = 1;
		this.groupId = 0;
		this.numGroupMember = 0;
		this.numLeagueMember = 0;
		this.numUp = 10;
		this.numDown = 10;
		this.highScoreOpe = new List<ServerRemainOperator>();
		this.totalScoreOpe = new List<ServerRemainOperator>();
	}

	// Token: 0x17000800 RID: 2048
	// (get) Token: 0x060036F4 RID: 14068 RVA: 0x00123054 File Offset: 0x00121254
	// (set) Token: 0x060036F5 RID: 14069 RVA: 0x0012305C File Offset: 0x0012125C
	public int mode { get; set; }

	// Token: 0x17000801 RID: 2049
	// (get) Token: 0x060036F6 RID: 14070 RVA: 0x00123068 File Offset: 0x00121268
	public RankingUtil.RankingMode rankinMode
	{
		get
		{
			RankingUtil.RankingMode result = RankingUtil.RankingMode.ENDLESS;
			if (this.mode >= 0 && this.mode < 2)
			{
				result = (RankingUtil.RankingMode)this.mode;
			}
			return result;
		}
	}

	// Token: 0x17000802 RID: 2050
	// (get) Token: 0x060036F7 RID: 14071 RVA: 0x00123098 File Offset: 0x00121298
	// (set) Token: 0x060036F8 RID: 14072 RVA: 0x001230A0 File Offset: 0x001212A0
	public int leagueId { get; set; }

	// Token: 0x17000803 RID: 2051
	// (get) Token: 0x060036F9 RID: 14073 RVA: 0x001230AC File Offset: 0x001212AC
	// (set) Token: 0x060036FA RID: 14074 RVA: 0x001230B4 File Offset: 0x001212B4
	public int groupId { get; set; }

	// Token: 0x17000804 RID: 2052
	// (get) Token: 0x060036FB RID: 14075 RVA: 0x001230C0 File Offset: 0x001212C0
	// (set) Token: 0x060036FC RID: 14076 RVA: 0x001230C8 File Offset: 0x001212C8
	public int numGroupMember { get; set; }

	// Token: 0x17000805 RID: 2053
	// (get) Token: 0x060036FD RID: 14077 RVA: 0x001230D4 File Offset: 0x001212D4
	// (set) Token: 0x060036FE RID: 14078 RVA: 0x001230DC File Offset: 0x001212DC
	public int numLeagueMember { get; set; }

	// Token: 0x17000806 RID: 2054
	// (get) Token: 0x060036FF RID: 14079 RVA: 0x001230E8 File Offset: 0x001212E8
	// (set) Token: 0x06003700 RID: 14080 RVA: 0x001230F0 File Offset: 0x001212F0
	public int numUp { get; set; }

	// Token: 0x17000807 RID: 2055
	// (get) Token: 0x06003701 RID: 14081 RVA: 0x001230FC File Offset: 0x001212FC
	// (set) Token: 0x06003702 RID: 14082 RVA: 0x00123104 File Offset: 0x00121304
	public int numDown { get; set; }

	// Token: 0x17000808 RID: 2056
	// (get) Token: 0x06003703 RID: 14083 RVA: 0x00123110 File Offset: 0x00121310
	// (set) Token: 0x06003704 RID: 14084 RVA: 0x00123118 File Offset: 0x00121318
	public List<ServerRemainOperator> highScoreOpe { get; set; }

	// Token: 0x17000809 RID: 2057
	// (get) Token: 0x06003705 RID: 14085 RVA: 0x00123124 File Offset: 0x00121324
	// (set) Token: 0x06003706 RID: 14086 RVA: 0x0012312C File Offset: 0x0012132C
	public List<ServerRemainOperator> totalScoreOpe { get; set; }

	// Token: 0x06003707 RID: 14087 RVA: 0x00123138 File Offset: 0x00121338
	public void Dump()
	{
	}

	// Token: 0x06003708 RID: 14088 RVA: 0x0012313C File Offset: 0x0012133C
	public void CopyTo(ServerLeagueData to)
	{
		to.mode = this.mode;
		to.leagueId = this.leagueId;
		to.groupId = this.groupId;
		to.numGroupMember = this.numGroupMember;
		to.numLeagueMember = this.numLeagueMember;
		to.numUp = this.numUp;
		to.numDown = this.numDown;
		this.SetServerRemainOperator(this.highScoreOpe, to.highScoreOpe);
		this.SetServerRemainOperator(this.totalScoreOpe, to.totalScoreOpe);
	}

	// Token: 0x06003709 RID: 14089 RVA: 0x001231C4 File Offset: 0x001213C4
	public void AddHighScoreRemainOperator(ServerRemainOperator remainOperator)
	{
		this.highScoreOpe.Add(remainOperator);
	}

	// Token: 0x0600370A RID: 14090 RVA: 0x001231D4 File Offset: 0x001213D4
	public void AddTotalScoreRemainOperator(ServerRemainOperator remainOperator)
	{
		this.totalScoreOpe.Add(remainOperator);
	}

	// Token: 0x0600370B RID: 14091 RVA: 0x001231E4 File Offset: 0x001213E4
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
