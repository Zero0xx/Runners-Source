using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006B4 RID: 1716
public class ServerUpgradeCharacter
{
	// Token: 0x06002E14 RID: 11796 RVA: 0x00110CB4 File Offset: 0x0010EEB4
	public static IEnumerator Process(int characterId, int abilityId, GameObject callbackObject)
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
				NetServerUpgradeCharacter net = new NetServerUpgradeCharacter(characterId, abilityId);
				net.Request();
				monitor.StartMonitor(new ServerUpgradeCharacterRetry(characterId, abilityId, callbackObject));
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
					ServerCharacterState[] resultCharacterState = net.resultCharacterState;
					if (resultCharacterState != null)
					{
						playerState.SetCharacterState(resultCharacterState);
					}
					MsgGetPlayerStateSucceed msg = new MsgGetPlayerStateSucceed();
					msg.m_playerState = playerState;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerUpgradeCharacter_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerUpgradeCharacter_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					msg2.m_status = net.resultStCd;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerUpgradeCharacter_Failed");
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
