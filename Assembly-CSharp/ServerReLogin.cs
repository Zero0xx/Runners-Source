using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x0200076F RID: 1903
public class ServerReLogin
{
	// Token: 0x060032AA RID: 12970 RVA: 0x00119BDC File Offset: 0x00117DDC
	public static IEnumerator Process(GameObject callbackObject)
	{
		NetMonitor monitor = NetMonitor.Instance;
		if (monitor != null)
		{
			NetServerReLogin net = new NetServerReLogin();
			net.Request();
			monitor.StartMonitor(new ServerReLoginRetry(callbackObject));
			while (net.IsExecuting())
			{
				yield return null;
			}
			if (net.IsSucceeded())
			{
				ServerLoginState loginState = ServerInterface.LoginState;
				loginState.sessionId = net.resultSessionId;
				loginState.sessionTimeLimit = net.sessionTimeLimit;
				if (monitor != null)
				{
					monitor.EndMonitorForward(null, callbackObject, "ServerReLogin_Succeeded");
				}
				if (callbackObject != null)
				{
					callbackObject.SendMessage("ServerReLogin_Succeeded", null, SendMessageOptions.DontRequireReceiver);
				}
			}
			else
			{
				MsgServerConnctFailed msg = new MsgServerConnctFailed(net.resultStCd);
				if (monitor != null)
				{
					monitor.EndMonitorForward(msg, callbackObject, "ServerReLogin_Failed");
				}
			}
			if (monitor != null)
			{
				monitor.EndMonitorBackward();
			}
		}
		yield break;
	}

	// Token: 0x04002B8B RID: 11147
	private const string SuccessEvent = "ServerReLogin_Succeeded";

	// Token: 0x04002B8C RID: 11148
	private const string FailEvent = "ServerReLogin_Failed";
}
