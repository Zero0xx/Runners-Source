using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020007A2 RID: 1954
public class ServerGetPrizeWheelSpinGeneral
{
	// Token: 0x060033C4 RID: 13252 RVA: 0x0011C2A4 File Offset: 0x0011A4A4
	public static IEnumerator Process(int eventId, int spinType, GameObject callbackObject)
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
				NetServerGetPrizeWheelSpinGeneral net = new NetServerGetPrizeWheelSpinGeneral(eventId, spinType);
				net.Request();
				monitor.StartMonitor(new ServerGetPrizeWheelSpinGeneralRetry(eventId, spinType, callbackObject), 0f, HudNetworkConnect.DisplayType.NO_BG);
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					if (net.resultPrizeState != null)
					{
						net.resultPrizeState.CopyTo(ServerInterface.RaidRoulettePrizeList);
					}
					MsgGetPrizeWheelSpinGeneralSucceed msg = new MsgGetPrizeWheelSpinGeneralSucceed();
					msg.prizeState = ServerInterface.RaidRoulettePrizeList;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetPrizeWheelSpinGeneral_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetPrizeWheelSpinGeneral_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetPrizeWheelSpinGeneral_Failed");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetPrizeWheelSpinGeneral_Failed", SendMessageOptions.DontRequireReceiver);
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
