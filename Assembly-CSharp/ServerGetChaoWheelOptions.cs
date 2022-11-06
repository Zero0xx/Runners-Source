using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006A6 RID: 1702
public class ServerGetChaoWheelOptions
{
	// Token: 0x06002DC8 RID: 11720 RVA: 0x001105A0 File Offset: 0x0010E7A0
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
				NetServerGetChaoWheelOptions net = new NetServerGetChaoWheelOptions();
				net.Request();
				monitor.StartMonitor(new ServerGetChaoWheelOptionsRetry(callbackObject), 0f, HudNetworkConnect.DisplayType.NO_BG);
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerChaoWheelOptions chaoWheelOptions = ServerInterface.ChaoWheelOptions;
					ServerChaoWheelOptions resultChaoWheelOptions = net.resultChaoWheelOptions;
					net.resultChaoWheelOptions.IsConnected = true;
					resultChaoWheelOptions.CopyTo(chaoWheelOptions);
					MsgGetChaoWheelOptionsSucceed msg = new MsgGetChaoWheelOptionsSucceed();
					msg.m_options = chaoWheelOptions;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetChaoWheelOptions_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetChaoWheelOptions_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetChaoWheelOptions_Failed");
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
