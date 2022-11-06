using System;
using UnityEngine;

// Token: 0x020006F7 RID: 1783
public class ServerGetEventRewardRetry : ServerRetryProcess
{
	// Token: 0x06002FB8 RID: 12216 RVA: 0x00113AB8 File Offset: 0x00111CB8
	public ServerGetEventRewardRetry(int eventId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_eventId = eventId;
	}

	// Token: 0x06002FB9 RID: 12217 RVA: 0x00113AC8 File Offset: 0x00111CC8
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetEventReward(this.m_eventId, this.m_callbackObject);
		}
	}

	// Token: 0x04002AA5 RID: 10917
	private int m_eventId;
}
