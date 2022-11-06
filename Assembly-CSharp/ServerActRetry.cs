using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000727 RID: 1831
public class ServerActRetry
{
	// Token: 0x06003112 RID: 12562 RVA: 0x00116684 File Offset: 0x00114884
	public static IEnumerator Process(GameObject callbackObject)
	{
		NetMonitor monitor = NetMonitor.Instance;
		if (monitor != null)
		{
			monitor.PrepareConnect(ServerSessionWatcher.ValidateType.LOGIN_OR_RELOGIN);
			while (!monitor.IsEndPrepare())
			{
				yield return null;
			}
			if (monitor.IsSuccessPrepare())
			{
				NetServerActRetry net = new NetServerActRetry();
				net.Request();
				monitor.StartMonitor(new ServerActRetryRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgActRetrySucceed msg = new MsgActRetrySucceed();
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerActRetry_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerActRetry_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerActRetry_Failed");
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
