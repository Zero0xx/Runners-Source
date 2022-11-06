using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020007BD RID: 1981
public class ServerRedStarExchange
{
	// Token: 0x06003446 RID: 13382 RVA: 0x0011CE24 File Offset: 0x0011B024
	public static IEnumerator Process(int storeItemId, GameObject callbackObject)
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
				NetServerRedStarExchange net = new NetServerRedStarExchange(storeItemId);
				net.Request();
				monitor.StartMonitor(new ServerRedStarExchangeRetry(storeItemId, callbackObject));
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
					msg.m_playerState = playerState;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerRedStarExchange_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerRedStarExchange_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerRedStarExchange_Failed");
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
