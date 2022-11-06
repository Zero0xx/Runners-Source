using System;
using LitJson;

// Token: 0x020006D9 RID: 1753
public class NetDebugUpdDailyMission : NetBase
{
	// Token: 0x06002EEA RID: 12010 RVA: 0x0011244C File Offset: 0x0011064C
	public NetDebugUpdDailyMission() : this(0)
	{
	}

	// Token: 0x06002EEB RID: 12011 RVA: 0x00112458 File Offset: 0x00110658
	public NetDebugUpdDailyMission(int missionId)
	{
		this.paramMissionId = missionId;
	}

	// Token: 0x06002EEC RID: 12012 RVA: 0x00112468 File Offset: 0x00110668
	protected override void DoRequest()
	{
		base.SetAction("Debug/updDailyMission");
		this.SetParameter_DailyMission();
	}

	// Token: 0x06002EED RID: 12013 RVA: 0x0011247C File Offset: 0x0011067C
	protected override void DoDidSuccess(JsonData jdata)
	{
	}

	// Token: 0x06002EEE RID: 12014 RVA: 0x00112480 File Offset: 0x00110680
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000627 RID: 1575
	// (get) Token: 0x06002EEF RID: 12015 RVA: 0x00112484 File Offset: 0x00110684
	// (set) Token: 0x06002EF0 RID: 12016 RVA: 0x0011248C File Offset: 0x0011068C
	public int paramMissionId { get; set; }

	// Token: 0x06002EF1 RID: 12017 RVA: 0x00112498 File Offset: 0x00110698
	private void SetParameter_DailyMission()
	{
		base.WriteActionParamValue("missionId", this.paramMissionId);
	}
}
