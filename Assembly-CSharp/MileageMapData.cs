using System;

// Token: 0x02000660 RID: 1632
public class MileageMapData : IComparable
{
	// Token: 0x06002BE2 RID: 11234 RVA: 0x0010AD64 File Offset: 0x00108F64
	public MileageMapData()
	{
	}

	// Token: 0x06002BE3 RID: 11235 RVA: 0x0010AD6C File Offset: 0x00108F6C
	public MileageMapData(ScenarioData scenario, LoadingData loading, MapData map_data, EventData event_data, WindowEventData[] window_data)
	{
		this.scenario = scenario;
		this.loading = loading;
		this.map_data = map_data;
		this.event_data = event_data;
		this.window_data = window_data;
	}

	// Token: 0x170005A3 RID: 1443
	// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x0010ADA4 File Offset: 0x00108FA4
	// (set) Token: 0x06002BE5 RID: 11237 RVA: 0x0010ADAC File Offset: 0x00108FAC
	public ScenarioData scenario { get; set; }

	// Token: 0x170005A4 RID: 1444
	// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x0010ADB8 File Offset: 0x00108FB8
	// (set) Token: 0x06002BE7 RID: 11239 RVA: 0x0010ADC0 File Offset: 0x00108FC0
	public LoadingData loading { get; set; }

	// Token: 0x170005A5 RID: 1445
	// (get) Token: 0x06002BE8 RID: 11240 RVA: 0x0010ADCC File Offset: 0x00108FCC
	// (set) Token: 0x06002BE9 RID: 11241 RVA: 0x0010ADD4 File Offset: 0x00108FD4
	public MapData map_data { get; set; }

	// Token: 0x170005A6 RID: 1446
	// (get) Token: 0x06002BEA RID: 11242 RVA: 0x0010ADE0 File Offset: 0x00108FE0
	// (set) Token: 0x06002BEB RID: 11243 RVA: 0x0010ADE8 File Offset: 0x00108FE8
	public EventData event_data { get; set; }

	// Token: 0x170005A7 RID: 1447
	// (get) Token: 0x06002BEC RID: 11244 RVA: 0x0010ADF4 File Offset: 0x00108FF4
	// (set) Token: 0x06002BED RID: 11245 RVA: 0x0010ADFC File Offset: 0x00108FFC
	public WindowEventData[] window_data { get; set; }

	// Token: 0x06002BEE RID: 11246 RVA: 0x0010AE08 File Offset: 0x00109008
	public int CompareTo(object obj)
	{
		if (this == (MileageMapData)obj)
		{
			return 0;
		}
		return -1;
	}
}
