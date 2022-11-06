using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000729 RID: 1833
public class ServerActRetryFree
{
	// Token: 0x06003116 RID: 12566 RVA: 0x001166E8 File Offset: 0x001148E8
	public static IEnumerator Process(GameObject callbackObject)
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
				NetServerActRetryFree net = new NetServerActRetryFree();
				net.Request();
				monitor.StartMonitor(new ServerActRetryFreeRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgActRetryFreeSucceed msg = new MsgActRetryFreeSucceed();
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerActRetryFree_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerActRetryFree_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerActRetryFree_Failed");
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
