using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000765 RID: 1893
public class ServerGetVersion
{
	// Token: 0x06003295 RID: 12949 RVA: 0x001198E4 File Offset: 0x00117AE4
	public static IEnumerator Process(GameObject callbackObject)
	{
		NetMonitor monitor = NetMonitor.Instance;
		if (monitor != null)
		{
			NetServerGetVersion net = new NetServerGetVersion();
			net.Request();
			monitor.StartMonitor(new ServerGetVersionRetry(callbackObject));
			while (net.IsExecuting())
			{
				yield return null;
			}
			if (net.IsSucceeded())
			{
				MsgGetVersionSucceed msg = new MsgGetVersionSucceed();
				if (monitor != null)
				{
					monitor.EndMonitorForward(msg, callbackObject, "ServerGetVersion_Succeeded");
				}
				if (callbackObject != null)
				{
					callbackObject.SendMessage("ServerGetVersion_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
				}
			}
			else
			{
				MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
				if (monitor != null)
				{
					monitor.EndMonitorForward(msg2, callbackObject, "ServerGetVersion_Failed");
				}
			}
			if (monitor != null)
			{
				monitor.EndMonitorBackward();
			}
		}
		yield break;
	}
}
