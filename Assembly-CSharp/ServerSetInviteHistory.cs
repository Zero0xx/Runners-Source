using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000711 RID: 1809
public class ServerSetInviteHistory
{
	// Token: 0x06003022 RID: 12322 RVA: 0x00114520 File Offset: 0x00112720
	public static IEnumerator Process(string facebookIdHash, GameObject callbackObject)
	{
		NetMonitor monitor = NetMonitor.Instance;
		if (monitor != null)
		{
			monitor.PrepareConnect();
			while (!monitor.IsEndPrepare())
			{
				yield return null;
			}
			if (monitor.IsSuccessPrepare())
			{
				NetServerSetInviteHistory net = new NetServerSetInviteHistory(facebookIdHash);
				net.Request();
				monitor.StartMonitor(new ServerSetInviteHistoryRetry(facebookIdHash, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgSetInviteHistorySucceed msg = new MsgSetInviteHistorySucceed();
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerSetInviteHistory_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerSetInviteHistory_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerSetInviteHistory_Failed");
					}
				}
				if (monitor != null)
				{
					monitor.EndMonitorBackward();
				}
			}
		}
		yield break;
	}

	// Token: 0x02000712 RID: 1810
	public enum IncentiveType
	{
		// Token: 0x04002AC9 RID: 10953
		LOGIN,
		// Token: 0x04002ACA RID: 10954
		REVIEW,
		// Token: 0x04002ACB RID: 10955
		FEED,
		// Token: 0x04002ACC RID: 10956
		ACHIEVEMENT,
		// Token: 0x04002ACD RID: 10957
		PUSH_NOLOGIN
	}
}
