using System;
using System.Runtime.InteropServices;

// Token: 0x02000110 RID: 272
public struct QuickModeTimerNativeParam
{
	// Token: 0x060007F5 RID: 2037 RVA: 0x0002F27C File Offset: 0x0002D47C
	public void Init(int gold, int silver, int bronze, int continueCount, int main, int sub, int total, long playTime)
	{
		this.goldCount = gold;
		this.silverCount = silver;
		this.bronzeCount = bronze;
		this.continuCount = continueCount;
		this.mainCharaExtendTime = main;
		this.subCharaExtendTime = sub;
		this.totalTime = total;
		BindingLinkUtility.LongToIntArray(out this.playTime, playTime);
	}

	// Token: 0x04000629 RID: 1577
	public int goldCount;

	// Token: 0x0400062A RID: 1578
	public int silverCount;

	// Token: 0x0400062B RID: 1579
	public int bronzeCount;

	// Token: 0x0400062C RID: 1580
	public int continuCount;

	// Token: 0x0400062D RID: 1581
	public int mainCharaExtendTime;

	// Token: 0x0400062E RID: 1582
	public int subCharaExtendTime;

	// Token: 0x0400062F RID: 1583
	public int totalTime;

	// Token: 0x04000630 RID: 1584
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] playTime;
}
