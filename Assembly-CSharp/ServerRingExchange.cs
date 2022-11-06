using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020007BF RID: 1983
public class ServerRingExchange
{
	// Token: 0x0600344A RID: 13386 RVA: 0x0011CEAC File Offset: 0x0011B0AC
	public static IEnumerator Process(int itemId, int itemNum, GameObject callbackObject)
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
				NetServerRingExchange net = new NetServerRingExchange(itemId, itemNum);
				net.Request();
				monitor.StartMonitor(new ServerRingExchangeRetry(itemId, itemNum, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerPlayerState playerState = ServerInterface.PlayerState;
					ServerPlayerState resultPlayerState = net.resultPlayerState;
					resultPlayerState.CopyTo(playerState);
					MsgGetPlayerStateSucceed msg = new MsgGetPlayerStateSucceed();
					msg.m_playerState = resultPlayerState;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerRingExchange_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerRingExchange_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerRingExchange_Failed");
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
