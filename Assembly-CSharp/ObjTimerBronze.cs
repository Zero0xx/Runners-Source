using System;

// Token: 0x0200095A RID: 2394
public class ObjTimerBronze : ObjTimerBase
{
	// Token: 0x06003E5A RID: 15962 RVA: 0x00144510 File Offset: 0x00142710
	protected override string GetModelName()
	{
		return ObjTimerUtil.GetModelName(TimerType.BRONZE);
	}

	// Token: 0x06003E5B RID: 15963 RVA: 0x00144518 File Offset: 0x00142718
	protected override TimerType GetTimerType()
	{
		return TimerType.BRONZE;
	}
}
