using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006FA RID: 1786
public class ServerGetEventState
{
	// Token: 0x06002FBF RID: 12223 RVA: 0x00113B80 File Offset: 0x00111D80
	public static IEnumerator Process(int eventId, GameObject callbackObject)
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
				NetServerGetEventState net = new NetServerGetEventState(eventId);
				net.Request();
				monitor.StartMonitor(new ServerGetEventStateRetry(eventId, callbackObject), -1f, HudNetworkConnect.DisplayType.ONLY_ICON);
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerEventState eventState = ServerInterface.EventState;
					net.resultEventState.CopyTo(eventState);
					MsgGetEventStateSucceed msg = new MsgGetEventStateSucceed();
					msg.m_eventState = net.resultEventState;
					if (EventManager.Instance != null)
					{
						EventManager.Instance.SynchServerEventState();
					}
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetEventState_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetEventState_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetEventState_Failed");
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
