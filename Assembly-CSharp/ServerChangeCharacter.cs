using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006B0 RID: 1712
public class ServerChangeCharacter
{
	// Token: 0x06002E0C RID: 11788 RVA: 0x00110B84 File Offset: 0x0010ED84
	public static IEnumerator Process(int mainCharaId, int subCharaId, GameObject callbackObject)
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
				NetServerChangeCharacter net = new NetServerChangeCharacter(mainCharaId, subCharaId);
				net.Request();
				monitor.StartMonitor(new ServerChangeCharacterRetry(mainCharaId, subCharaId, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerPlayerState playerState = ServerInterface.PlayerState;
					ServerPlayerState resultPlayerState = net.resultPlayerState;
					resultPlayerState.CopyTo(playerState);
					DeckUtil.CharaSetSaveAuto(playerState.m_mainCharaId, playerState.m_subCharaId);
					MsgGetPlayerStateSucceed msg = new MsgGetPlayerStateSucceed();
					msg.m_playerState = resultPlayerState;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerChangeCharacter_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerChangeCharacter_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerChangeCharacter_Failed");
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
