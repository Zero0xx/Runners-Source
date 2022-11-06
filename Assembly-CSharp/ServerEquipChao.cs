using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006A2 RID: 1698
public class ServerEquipChao
{
	// Token: 0x06002DC0 RID: 11712 RVA: 0x001104A4 File Offset: 0x0010E6A4
	public static IEnumerator Process(int mainChaoId, int subChaoId, GameObject callbackObject)
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
				NetServerEquipChao net = new NetServerEquipChao(mainChaoId, subChaoId);
				net.Request();
				monitor.StartMonitor(new ServerEquipChaoRetry(mainChaoId, subChaoId, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					DeckUtil.ChaoSetSaveAuto(mainChaoId, subChaoId);
					ServerPlayerState playerState = ServerInterface.PlayerState;
					ServerPlayerState resultPlayerState = net.resultPlayerState;
					resultPlayerState.CopyTo(playerState);
					MsgGetPlayerStateSucceed msg = new MsgGetPlayerStateSucceed();
					msg.m_playerState = playerState;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerEquipChao_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerEquipChao_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerEquipChao_Failed");
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
