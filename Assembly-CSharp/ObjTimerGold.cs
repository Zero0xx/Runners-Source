using System;

// Token: 0x0200095C RID: 2396
public class ObjTimerGold : ObjTimerBase
{
	// Token: 0x06003E5D RID: 15965 RVA: 0x00144524 File Offset: 0x00142724
	protected override string GetModelName()
	{
		return ObjTimerUtil.GetModelName(TimerType.GOLD);
	}

	// Token: 0x06003E5E RID: 15966 RVA: 0x0014452C File Offset: 0x0014272C
	protected override TimerType GetTimerType()
	{
		return TimerType.GOLD;
	}
}
