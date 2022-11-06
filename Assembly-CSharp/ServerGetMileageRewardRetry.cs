using System;
using UnityEngine;

// Token: 0x02000738 RID: 1848
public class ServerGetMileageRewardRetry : ServerRetryProcess
{
	// Token: 0x06003140 RID: 12608 RVA: 0x00116D3C File Offset: 0x00114F3C
	public ServerGetMileageRewardRetry(int episode, int chapter, GameObject callbackObject) : base(callbackObject)
	{
		this.m_episode = episode;
		this.m_chapter = chapter;
	}

	// Token: 0x06003141 RID: 12609 RVA: 0x00116D54 File Offset: 0x00114F54
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetMileageReward(this.m_episode, this.m_chapter, this.m_callbackObject);
		}
	}

	// Token: 0x04002B1D RID: 11037
	private int m_episode;

	// Token: 0x04002B1E RID: 11038
	private int m_chapter;
}
