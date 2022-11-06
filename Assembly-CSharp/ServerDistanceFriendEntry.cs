using System;

// Token: 0x020007FD RID: 2045
public class ServerDistanceFriendEntry
{
	// Token: 0x060036CF RID: 14031 RVA: 0x0012231C File Offset: 0x0012051C
	public ServerDistanceFriendEntry()
	{
		this.m_friendId = string.Empty;
		this.m_distance = 0;
	}

	// Token: 0x060036D0 RID: 14032 RVA: 0x00122338 File Offset: 0x00120538
	public void CopyTo(ServerDistanceFriendEntry to)
	{
		to.m_friendId = this.m_friendId;
		to.m_distance = this.m_distance;
	}

	// Token: 0x04002E14 RID: 11796
	public string m_friendId;

	// Token: 0x04002E15 RID: 11797
	public int m_distance;
}
