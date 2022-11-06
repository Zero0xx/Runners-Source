using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x0200070F RID: 1807
public class ServerSetInviteCode
{
	// Token: 0x0600301E RID: 12318 RVA: 0x001144A4 File Offset: 0x001126A4
	public static IEnumerator Process(string friendId, GameObject callbackObject)
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
				NetServerSetInviteCode net = new NetServerSetInviteCode(friendId);
				net.Request();
				monitor.StartMonitor(new ServerSetInviteCodeRetry(friendId, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					List<ServerPresentState> incentive = net.incentive;
					ServerPlayerState playerState = ServerInterface.PlayerState;
					ServerPlayerState resultPlayerState = net.playerState;
					resultPlayerState.CopyTo(playerState);
					MsgGetNormalIncentiveSucceed msg = new MsgGetNormalIncentiveSucceed();
					msg.m_playerState = playerState;
					msg.m_incentive = incentive;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerSetInviteCode_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerSetInviteCode_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerSetInviteCode_Failed");
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
