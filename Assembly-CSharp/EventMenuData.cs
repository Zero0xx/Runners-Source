using System;

// Token: 0x02000218 RID: 536
public class EventMenuData : IComparable
{
	// Token: 0x06000DF3 RID: 3571 RVA: 0x000514DC File Offset: 0x0004F6DC
	public EventMenuData()
	{
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x000514E4 File Offset: 0x0004F6E4
	public EventMenuData(WindowEventData[] window_data, EventStageData stage_data, EventChaoData chao_data, EventProductionData puduction_data, EventRaidProductionData raid_data, EventAvertData advert_data)
	{
		this.stage_data = stage_data;
		this.window_data = window_data;
		this.chao_data = chao_data;
		this.puduction_data = puduction_data;
		this.raid_data = raid_data;
		this.advert_data = advert_data;
	}

	// Token: 0x1700020A RID: 522
	// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x00051524 File Offset: 0x0004F724
	// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x0005152C File Offset: 0x0004F72C
	public WindowEventData[] window_data { get; set; }

	// Token: 0x1700020B RID: 523
	// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x00051538 File Offset: 0x0004F738
	// (set) Token: 0x06000DF8 RID: 3576 RVA: 0x00051540 File Offset: 0x0004F740
	public EventStageData stage_data { get; set; }

	// Token: 0x1700020C RID: 524
	// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0005154C File Offset: 0x0004F74C
	// (set) Token: 0x06000DFA RID: 3578 RVA: 0x00051554 File Offset: 0x0004F754
	public EventChaoData chao_data { get; set; }

	// Token: 0x1700020D RID: 525
	// (get) Token: 0x06000DFB RID: 3579 RVA: 0x00051560 File Offset: 0x0004F760
	// (set) Token: 0x06000DFC RID: 3580 RVA: 0x00051568 File Offset: 0x0004F768
	public EventProductionData puduction_data { get; set; }

	// Token: 0x1700020E RID: 526
	// (get) Token: 0x06000DFD RID: 3581 RVA: 0x00051574 File Offset: 0x0004F774
	// (set) Token: 0x06000DFE RID: 3582 RVA: 0x0005157C File Offset: 0x0004F77C
	public EventRaidProductionData raid_data { get; set; }

	// Token: 0x1700020F RID: 527
	// (get) Token: 0x06000DFF RID: 3583 RVA: 0x00051588 File Offset: 0x0004F788
	// (set) Token: 0x06000E00 RID: 3584 RVA: 0x00051590 File Offset: 0x0004F790
	public EventAvertData advert_data { get; set; }

	// Token: 0x06000E01 RID: 3585 RVA: 0x0005159C File Offset: 0x0004F79C
	public int CompareTo(object obj)
	{
		if (this == (EventMenuData)obj)
		{
			return 0;
		}
		return -1;
	}
}
