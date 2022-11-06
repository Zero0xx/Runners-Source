using System;
using UnityEngine;

// Token: 0x02000705 RID: 1797
public class ServerGetFacebookIncentiveRetry : ServerRetryProcess
{
	// Token: 0x0600300B RID: 12299 RVA: 0x0011424C File Offset: 0x0011244C
	public ServerGetFacebookIncentiveRetry(int incentiveType, int achievementCount, GameObject callbackObject) : base(callbackObject)
	{
		this.m_incentiveType = incentiveType;
		this.m_achievementCount = achievementCount;
	}

	// Token: 0x0600300C RID: 12300 RVA: 0x00114264 File Offset: 0x00112464
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetFacebookIncentive(this.m_incentiveType, this.m_achievementCount, this.m_callbackObject);
		}
	}

	// Token: 0x04002ABB RID: 10939
	public int m_incentiveType;

	// Token: 0x04002ABC RID: 10940
	public int m_achievementCount;
}
