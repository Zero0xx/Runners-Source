using System;

// Token: 0x0200095D RID: 2397
public class ObjTimerSilver : ObjTimerBase
{
	// Token: 0x06003E60 RID: 15968 RVA: 0x00144538 File Offset: 0x00142738
	protected override string GetModelName()
	{
		return ObjTimerUtil.GetModelName(TimerType.SILVER);
	}

	// Token: 0x06003E61 RID: 15969 RVA: 0x00144540 File Offset: 0x00142740
	protected override TimerType GetTimerType()
	{
		return TimerType.SILVER;
	}
}
