using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x0200077D RID: 1917
public class ServerGetOptionUserResult
{
	// Token: 0x0600330E RID: 13070 RVA: 0x0011ADE0 File Offset: 0x00118FE0
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
				NetServerGetOptionUserResult net = new NetServerGetOptionUserResult();
				net.Request();
				monitor.StartMonitor(new ServerGetOptionUserResultRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgGetOptionUserResultSucceed msg = new MsgGetOptionUserResultSucceed();
					msg.m_serverOptionUserResult = net.UserResult;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetOptionUserResult_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetOptionUserResult_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetOptionUserResult_Failed");
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

	// Token: 0x04002BA7 RID: 11175
	private const string SuccessEvent = "ServerGetOptionUserResult_Succeeded";

	// Token: 0x04002BA8 RID: 11176
	private const string FailEvent = "ServerGetOptionUserResult_Failed";
}
