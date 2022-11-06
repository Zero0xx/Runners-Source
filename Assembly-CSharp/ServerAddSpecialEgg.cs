using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x0200069E RID: 1694
public class ServerAddSpecialEgg
{
	// Token: 0x06002DB8 RID: 11704 RVA: 0x00110398 File Offset: 0x0010E598
	public static IEnumerator Process(int numSpecialEgg, GameObject callbackObject)
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
				NetServerAddSpecialEgg net = new NetServerAddSpecialEgg(numSpecialEgg);
				net.Request();
				monitor.StartMonitor(new ServerAddSpecialEggRetry(numSpecialEgg, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerChaoWheelOptions serverChaoWheelOptions = ServerInterface.ChaoWheelOptions;
					if (serverChaoWheelOptions != null)
					{
						serverChaoWheelOptions.NumSpecialEggs = net.resultSpecialEgg;
					}
					if (RouletteManager.Instance != null)
					{
						RouletteManager.Instance.specialEgg = net.resultSpecialEgg;
						GeneralUtil.SetRouletteBtnIcon(null, "Btn_roulette");
					}
					MsgAddSpecialEggSucceed msg = new MsgAddSpecialEggSucceed();
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerAddSpecialEgg_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerAddSpecialEgg_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerAddSpecialEgg_Failed");
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
