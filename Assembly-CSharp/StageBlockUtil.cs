using System;

// Token: 0x02000285 RID: 645
public class StageBlockUtil
{
	// Token: 0x060011B8 RID: 4536 RVA: 0x0006479C File Offset: 0x0006299C
	public static bool IsPastPosition(float pos, float basePos, float distanceOfPast)
	{
		float num = basePos - pos;
		return num > distanceOfPast;
	}
}
