using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006AA RID: 1706
public class ServerGetPrizeChaoWheelSpin
{
	// Token: 0x06002DD0 RID: 11728 RVA: 0x00110674 File Offset: 0x0010E874
	public static IEnumerator Process(int chaoWheelSpinType, GameObject callbackObject)
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
				NetServerGetPrizeChaoWheelSpin net = new NetServerGetPrizeChaoWheelSpin(chaoWheelSpinType);
				net.Request();
				monitor.StartMonitor(new ServerGetPrizeChaoWheelSpinRetry(chaoWheelSpinType, callbackObject), 0f, HudNetworkConnect.DisplayType.NO_BG);
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerPrizeState prizeState = null;
					switch (chaoWheelSpinType)
					{
					case 0:
						prizeState = ServerInterface.PremiumRoulettePrizeList;
						break;
					case 1:
						prizeState = ServerInterface.SpecialRoulettePrizeList;
						break;
					}
					if (net.resultPrizeState != null)
					{
						net.resultPrizeState.CopyTo(prizeState);
					}
					MsgGetPrizeChaoWheelSpinSucceed msg = new MsgGetPrizeChaoWheelSpinSucceed();
					msg.m_prizeState = prizeState;
					msg.m_type = chaoWheelSpinType;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetPrizeChaoWheelSpin_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetPrizeChaoWheelSpin_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetPrizeChaoWheelSpin_Failed");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetPrizeChaoWheelSpin_Failed", SendMessageOptions.DontRequireReceiver);
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
