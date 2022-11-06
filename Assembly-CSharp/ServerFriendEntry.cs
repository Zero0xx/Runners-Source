using System;

// Token: 0x020007FF RID: 2047
public class ServerFriendEntry
{
	// Token: 0x060036D8 RID: 14040 RVA: 0x00122418 File Offset: 0x00120618
	public ServerFriendEntry()
	{
		this.m_mid = "0123456789abcdef";
		this.m_name = "0123456789abcdef";
		this.m_url = "0123456789abcdef";
		this.m_stateFlags = ServerFriendEntry.FriendStateFlags.None;
	}

	// Token: 0x060036D9 RID: 14041 RVA: 0x00122454 File Offset: 0x00120654
	public void CopyTo(ServerFriendEntry to)
	{
		to.m_mid = this.m_mid;
		to.m_name = this.m_name;
		to.m_url = this.m_url;
		to.m_stateFlags = this.m_stateFlags;
	}

	// Token: 0x060036DA RID: 14042 RVA: 0x00122494 File Offset: 0x00120694
	public bool IsInvited()
	{
		return ServerFriendEntry.FriendStateFlags.Invited == this.m_stateFlags;
	}

	// Token: 0x04002E18 RID: 11800
	public string m_mid;

	// Token: 0x04002E19 RID: 11801
	public string m_name;

	// Token: 0x04002E1A RID: 11802
	public string m_url;

	// Token: 0x04002E1B RID: 11803
	public ServerFriendEntry.FriendStateFlags m_stateFlags;

	// Token: 0x02000800 RID: 2048
	[Flags]
	public enum FriendStateFlags
	{
		// Token: 0x04002E1D RID: 11805
		None = 0,
		// Token: 0x04002E1E RID: 11806
		SentEnergy = 1,
		// Token: 0x04002E1F RID: 11807
		RequestedEnergy = 2,
		// Token: 0x04002E20 RID: 11808
		Invited = 4
	}
}
