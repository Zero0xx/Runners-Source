using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000791 RID: 1937
public class ServerSendApollo
{
	// Token: 0x06003364 RID: 13156 RVA: 0x0011B688 File Offset: 0x00119888
	public static IEnumerator Process(int type, string[] value, GameObject callbackObject)
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
				NetServerSendApollo net = new NetServerSendApollo(type, value);
				net.Request();
				monitor.StartMonitor(new ServerSendApolloRetry(type, value, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgSendApolloSucceed msg = new MsgSendApolloSucceed();
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerSendApollo_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerSendApollo_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					global::Debug.Log("ServerSendApollo: connectIsFailded");
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerSendApollo_Failed");
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
