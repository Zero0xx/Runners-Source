using System;
using UnityEngine;

// Token: 0x0200070E RID: 1806
public class ServerSetInviteCodeRetry : ServerRetryProcess
{
	// Token: 0x0600301B RID: 12315 RVA: 0x00114458 File Offset: 0x00112658
	public ServerSetInviteCodeRetry(string friendId, GameObject callbackObject) : base(callbackObject)
	{
		this.m_friendId = friendId;
	}

	// Token: 0x0600301C RID: 12316 RVA: 0x00114468 File Offset: 0x00112668
	public override void Retry()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerSetInviteCode(this.m_friendId, this.m_callbackObject);
		}
	}

	// Token: 0x04002AC6 RID: 10950
	public string m_friendId;
}
