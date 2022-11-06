using System;

// Token: 0x020007EB RID: 2027
public class ServerMileageReward
{
	// Token: 0x0600362A RID: 13866 RVA: 0x00120AAC File Offset: 0x0011ECAC
	public ServerMileageReward()
	{
		this.m_episode = 1;
		this.m_chapter = 1;
		this.m_type = 1;
		this.m_point = 0;
		this.m_itemId = 0;
		this.m_numItem = 0;
		this.m_limitTime = 0;
	}

	// Token: 0x0600362B RID: 13867 RVA: 0x00120AE8 File Offset: 0x0011ECE8
	public void CopyTo(ServerMileageReward to)
	{
		if (to != null)
		{
			to.m_episode = this.m_episode;
			to.m_chapter = this.m_chapter;
			to.m_type = this.m_type;
			to.m_point = this.m_point;
			to.m_itemId = this.m_itemId;
			to.m_numItem = this.m_numItem;
			to.m_limitTime = this.m_limitTime;
			to.m_startTime = this.m_startTime;
		}
	}

	// Token: 0x04002DAB RID: 11691
	public int m_episode;

	// Token: 0x04002DAC RID: 11692
	public int m_chapter;

	// Token: 0x04002DAD RID: 11693
	public int m_type;

	// Token: 0x04002DAE RID: 11694
	public int m_point;

	// Token: 0x04002DAF RID: 11695
	public int m_itemId;

	// Token: 0x04002DB0 RID: 11696
	public int m_numItem;

	// Token: 0x04002DB1 RID: 11697
	public int m_limitTime;

	// Token: 0x04002DB2 RID: 11698
	public DateTime m_startTime;
}
