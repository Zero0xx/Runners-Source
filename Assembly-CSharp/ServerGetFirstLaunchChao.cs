using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020006A8 RID: 1704
public class ServerGetFirstLaunchChao
{
	// Token: 0x06002DCC RID: 11724 RVA: 0x00110604 File Offset: 0x0010E804
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
				NetServerGetFirstLaunchChao net = new NetServerGetFirstLaunchChao();
				net.Request();
				monitor.StartMonitor(new ServerGetFirstLaunchChaoRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerPlayerState playerState = ServerInterface.PlayerState;
					ServerPlayerState resultPlayerState = net.resultPlayerState;
					if (resultPlayerState != null)
					{
						resultPlayerState.CopyTo(playerState);
					}
					List<ServerChaoState> resultChaoState = net.resultChaoState;
					if (resultChaoState != null && resultChaoState.Count > 0)
					{
						playerState.SetChaoState(resultChaoState);
					}
					MsgGetPlayerStateSucceed msg = new MsgGetPlayerStateSucceed();
					msg.m_playerState = playerState;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetFirstLaunchChao_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetFirstLaunchChao_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetFirstLaunchChao_Failed");
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
