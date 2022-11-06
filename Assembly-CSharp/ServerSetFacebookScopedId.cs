using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x0200070D RID: 1805
public class ServerSetFacebookScopedId
{
	// Token: 0x0600301A RID: 12314 RVA: 0x00114428 File Offset: 0x00112628
	public static IEnumerator Process(string userId, GameObject callbackObject)
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
				NetServerSetFacebookScopedId net = new NetServerSetFacebookScopedId(userId);
				net.Request();
				monitor.StartMonitor(new ServerSetFacebookScopedIdRetry(userId, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgSetFacebookScopedIdSucceed msg = new MsgSetFacebookScopedIdSucceed();
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerSetFacebookScopedId_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerSetFacebookScopedId_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerSetFacebookScopedId_Failed");
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
}
