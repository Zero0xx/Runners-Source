using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020007A8 RID: 1960
public class ServerGetWheelSpinInfo
{
	// Token: 0x060033D0 RID: 13264 RVA: 0x0011C420 File Offset: 0x0011A620
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
				NetServerGetWheelSpinInfo net = new NetServerGetWheelSpinInfo();
				net.Request();
				monitor.StartMonitor(new ServerGetWheelSpinInfoRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgGetChaoWheelSpinInfoSucceed msg = new MsgGetChaoWheelSpinInfoSucceed();
					msg.m_wheelSpinInfos = net.resultWheelSpinInfos;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetWheelSpinInfo_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetWheelSpinInfo_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetWheelSpinInfo_Failed");
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
