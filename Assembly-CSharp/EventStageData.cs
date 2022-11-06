using System;

// Token: 0x02000210 RID: 528
public class EventStageData
{
	// Token: 0x06000DEA RID: 3562 RVA: 0x00051430 File Offset: 0x0004F630
	public bool IsEndlessModeBGM()
	{
		return !string.IsNullOrEmpty(this.stageCueSheetName) && !string.IsNullOrEmpty(this.bossStagCueSheetName) && !string.IsNullOrEmpty(this.stageBGM) && !string.IsNullOrEmpty(this.bossStagBGM);
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x00051480 File Offset: 0x0004F680
	public bool IsQuickModeBGM()
	{
		return !string.IsNullOrEmpty(this.quickStageCueSheetName) && !string.IsNullOrEmpty(this.quickStageBGM);
	}

	// Token: 0x04000BDE RID: 3038
	public int bg_id;

	// Token: 0x04000BDF RID: 3039
	public string stage_key;

	// Token: 0x04000BE0 RID: 3040
	public string stageCueSheetName;

	// Token: 0x04000BE1 RID: 3041
	public string bossStagCueSheetName;

	// Token: 0x04000BE2 RID: 3042
	public string quickStageCueSheetName;

	// Token: 0x04000BE3 RID: 3043
	public string stageBGM;

	// Token: 0x04000BE4 RID: 3044
	public string bossStagBGM;

	// Token: 0x04000BE5 RID: 3045
	public string quickStageBGM;
}
