using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000793 RID: 1939
public class ServerSetNoahId
{
	// Token: 0x06003368 RID: 13160 RVA: 0x0011B714 File Offset: 0x00119914
	public static IEnumerator Process(string noahId, GameObject callbackObject)
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
				NetServerSetNoahId net = new NetServerSetNoahId(noahId);
				net.Request();
				monitor.StartMonitor(new ServerSetNoahIdRetry(noahId, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgSetNoahIdSucceed msg = new MsgSetNoahIdSucceed();
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerSetNoahId_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerSetNoahId_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					global::Debug.Log("ServerSetNoahId: connectIsFailded");
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerSetNoahId_Failed");
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
