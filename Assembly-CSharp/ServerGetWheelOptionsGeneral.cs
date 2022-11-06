using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020007A6 RID: 1958
public class ServerGetWheelOptionsGeneral
{
	// Token: 0x060033CC RID: 13260 RVA: 0x0011C3A0 File Offset: 0x0011A5A0
	public static IEnumerator Process(int eventId, int spinId, GameObject callbackObject)
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
				NetServerGetWheelOptionsGeneral net = new NetServerGetWheelOptionsGeneral(eventId, spinId);
				net.Request();
				monitor.StartMonitor(new ServerGetWheelOptionsGeneralRetry(spinId, eventId, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerWheelOptionsGeneral resultWheelOptions = net.resultWheelOptionsGeneral;
					MsgGetWheelOptionsGeneralSucceed msg = new MsgGetWheelOptionsGeneralSucceed();
					msg.m_wheelOptionsGeneral = resultWheelOptions;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetWheelOptionsGeneral_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetWheelOptionsGeneral_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetWheelOptionsGeneral_Failed");
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

	// Token: 0x04002BF3 RID: 11251
	private const string SuccessEvent = "ServerGetWheelOptionsGeneral_Succeeded";

	// Token: 0x04002BF4 RID: 11252
	private const string FailEvent = "ServerGetWheelOptionsGeneral_Failed";
}
