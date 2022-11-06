using System;
using UnityEngine;

// Token: 0x02000747 RID: 1863
public class ServerGetLeaderboardEntriesRetry : ServerRetryProcess
{
	// Token: 0x060031B4 RID: 12724 RVA: 0x00117A7C File Offset: 0x00115C7C
	public ServerGetLeaderboardEntriesRetry(int mode, int first, int count, int index, int rankingType, int eventId, string[] friendIdList, GameObject callbackObject) : base(callbackObject)
	{
		this.m_mode = mode;
		this.m_first = first;
		this.m_count = count;
		this.m_index = index;
		this.m_rankingType = rankingType;
		this.m_eventId = eventId;
		this.m_friendIdList = friendIdList;
	}

	// Token: 0x060031B5 RID: 12725 RVA: 0x00117ABC File Offset: 0x00115CBC
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetLeaderboardEntries(this.m_mode, this.m_first, this.m_count, this.m_index, this.m_rankingType, this.m_eventId, this.m_friendIdList, this.m_callbackObject);
		}
	}

	// Token: 0x04002B3D RID: 11069
	public int m_mode;

	// Token: 0x04002B3E RID: 11070
	public int m_first;

	// Token: 0x04002B3F RID: 11071
	public int m_count;

	// Token: 0x04002B40 RID: 11072
	public int m_index;

	// Token: 0x04002B41 RID: 11073
	public int m_rankingType;

	// Token: 0x04002B42 RID: 11074
	public int m_eventId;

	// Token: 0x04002B43 RID: 11075
	public string[] m_friendIdList;
}
