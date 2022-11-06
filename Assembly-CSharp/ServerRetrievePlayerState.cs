using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000787 RID: 1927
public class ServerRetrievePlayerState
{
	// Token: 0x06003339 RID: 13113 RVA: 0x0011B11C File Offset: 0x0011931C
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
				NetServerRetrievePlayerState net = new NetServerRetrievePlayerState();
				net.Request();
				monitor.StartMonitor(new ServerRetrievePlayerStateRetry(callbackObject));
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
						monitor.EndMonitorForward(msg, callbackObject, "ServerRetrievePlayerState_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerRetrievePlayerState_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerRetrievePlayerState_Failed");
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
