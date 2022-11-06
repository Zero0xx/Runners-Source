using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020007B5 RID: 1973
public class ServerBuyIos
{
	// Token: 0x06003436 RID: 13366 RVA: 0x0011CC58 File Offset: 0x0011AE58
	public static IEnumerator Process(string receiptData, GameObject callbackObject)
	{
		NetMonitor monitor = NetMonitor.Instance;
		if (monitor != null)
		{
			monitor.PrepareConnect(ServerSessionWatcher.ValidateType.LOGIN_OR_RELOGIN);
			while (!monitor.IsEndPrepare())
			{
				yield return null;
			}
			if (monitor.IsSuccessPrepare())
			{
				NetServerBuyIos net = new NetServerBuyIos(receiptData);
				net.Request();
				monitor.StartMonitor(new ServerBuyIosRetry(receiptData, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					global::Debug.Log("ServerBuyIos: connectIsSucceed");
					ServerPlayerState playerState = ServerInterface.PlayerState;
					ServerPlayerState resultPlayerState = net.resultPlayerState;
					resultPlayerState.CopyTo(playerState);
					ServerInterface.SettingState.m_isPurchased = true;
					MsgGetPlayerStateSucceed msg = new MsgGetPlayerStateSucceed();
					msg.m_playerState = resultPlayerState;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerBuyIos_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerBuyIos_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					global::Debug.Log("ServerBuyIos: connectIsFailded");
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (net.resultStCd == ServerInterface.StatusCode.AlreadyProcessedReceipt)
					{
						if (callbackObject != null)
						{
							callbackObject.SendMessage("ServerBuyIos_Failed", msg2, SendMessageOptions.DontRequireReceiver);
						}
					}
					else if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerBuyIos_Failed");
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
