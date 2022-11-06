using System;

// Token: 0x02000675 RID: 1653
public class EventData
{
	// Token: 0x06002C14 RID: 11284 RVA: 0x0010B6F0 File Offset: 0x001098F0
	public bool IsBossEvent()
	{
		int num = this.point.Length;
		return num == 6 && this.point[5].boss.boss_flag == 1;
	}

	// Token: 0x06002C15 RID: 11285 RVA: 0x0010B724 File Offset: 0x00109924
	public BossEvent GetBossEvent()
	{
		if (this.IsBossEvent())
		{
			return this.point[5].boss;
		}
		return new BossEvent();
	}

	// Token: 0x0400292E RID: 10542
	public PointEventData[] point;
}
