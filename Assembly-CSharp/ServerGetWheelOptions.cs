using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020007A4 RID: 1956
public class ServerGetWheelOptions
{
	// Token: 0x060033C8 RID: 13256 RVA: 0x0011C324 File Offset: 0x0011A524
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
				NetServerGetWheelOptions net = new NetServerGetWheelOptions();
				net.Request();
				monitor.StartMonitor(new ServerGetWheelOptionsRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerWheelOptions wheelOptions = ServerInterface.WheelOptions;
					ServerWheelOptions resultWheelOptions = net.resultWheelOptions;
					resultWheelOptions.CopyTo(wheelOptions);
					MsgGetWheelOptionsSucceed msg = new MsgGetWheelOptionsSucceed();
					msg.m_wheelOptions = wheelOptions;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetWheelOptions_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetWheelOptions_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetWheelOptions_Failed");
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

	// Token: 0x04002BEF RID: 11247
	private const string SuccessEvent = "ServerGetWheelOptions_Succeeded";

	// Token: 0x04002BF0 RID: 11248
	private const string FailEvent = "ServerGetWheelOptions_Failed";
}
