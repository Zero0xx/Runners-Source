using System;

// Token: 0x02000807 RID: 2055
public class ServerLoginBonusState
{
	// Token: 0x0600371C RID: 14108 RVA: 0x001233A4 File Offset: 0x001215A4
	public ServerLoginBonusState()
	{
		this.m_numLogin = 0;
		this.m_numBonus = 0;
		this.m_lastBonusTime = DateTime.Now;
	}

	// Token: 0x0600371D RID: 14109 RVA: 0x001233C8 File Offset: 0x001215C8
	public void CopyTo(ServerLoginBonusState dest)
	{
		dest.m_numLogin = this.m_numLogin;
		dest.m_numBonus = this.m_numBonus;
		dest.m_lastBonusTime = this.m_lastBonusTime;
	}

	// Token: 0x04002E66 RID: 11878
	public int m_numLogin;

	// Token: 0x04002E67 RID: 11879
	public int m_numBonus;

	// Token: 0x04002E68 RID: 11880
	public DateTime m_lastBonusTime;
}
