using System;

// Token: 0x020007E7 RID: 2023
public class ServerMileageFriendEntry : ServerFriendEntry
{
	// Token: 0x06003624 RID: 13860 RVA: 0x00120930 File Offset: 0x0011EB30
	public ServerMileageFriendEntry()
	{
		this.m_friendId = "0123456789abcdef";
		this.m_mapState = new ServerMileageMapState();
	}

	// Token: 0x06003625 RID: 13861 RVA: 0x00120950 File Offset: 0x0011EB50
	public void CopyTo(ServerMileageFriendEntry to)
	{
		if (to != null)
		{
			base.CopyTo(to);
			to.m_friendId = this.m_friendId;
			this.m_mapState.CopyTo(to.m_mapState);
		}
	}

	// Token: 0x04002D97 RID: 11671
	public string m_friendId;

	// Token: 0x04002D98 RID: 11672
	public ServerMileageMapState m_mapState;
}
