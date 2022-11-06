using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x0200070B RID: 1803
public class ServerRequestEnergy
{
	// Token: 0x06003016 RID: 12310 RVA: 0x001143AC File Offset: 0x001125AC
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
				NetServerRequestEnergy net = new NetServerRequestEnergy(friendId);
				net.Request();
				monitor.StartMonitor(new ServerRequestEnergyRetry(friendId, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					long resultExpireTime = net.resultExpireTime;
					ServerPlayerState playerState = ServerInterface.PlayerState;
					ServerPlayerState resultPlayerState = net.resultPlayerState;
					resultPlayerState.CopyTo(playerState);
					MsgRequestEnergySucceed msg = new MsgRequestEnergySucceed();
					msg.m_playerState = playerState;
					msg.m_resultExpireTime = resultExpireTime;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerRequestEnergy_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerRequestEnergy_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerRequestEnergy_Failed");
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
