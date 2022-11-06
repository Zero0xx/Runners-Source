using System;

// Token: 0x02000242 RID: 578
public class AchievementTempData
{
	// Token: 0x06000FFA RID: 4090 RVA: 0x0005DD80 File Offset: 0x0005BF80
	public AchievementTempData(string id)
	{
		this.m_id = id;
		this.m_reportEnd = false;
	}

	// Token: 0x04000DAC RID: 3500
	public string m_id;

	// Token: 0x04000DAD RID: 3501
	public bool m_reportEnd;
}
