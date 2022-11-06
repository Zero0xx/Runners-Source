using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006B2 RID: 1714
public class ServerUnlockedCharacter
{
	// Token: 0x06002E10 RID: 11792 RVA: 0x00110C1C File Offset: 0x0010EE1C
	public static IEnumerator Process(CharaType charaType, ServerItem serverItem, GameObject callbackObject)
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
				NetServerUnlockedCharacter net = new NetServerUnlockedCharacter(charaType, serverItem);
				net.Request();
				monitor.StartMonitor(new ServerUnlockedCharacterRetry(charaType, serverItem, callbackObject));
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
					ServerCharacterState[] characterState = net.resultCharacterState;
					if (characterState != null)
					{
						playerState.SetCharacterState(characterState);
					}
					MsgGetPlayerStateSucceed msg = new MsgGetPlayerStateSucceed();
					msg.m_playerState = resultPlayerState;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerUnlockedCharacter_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerUnlockedCharacter_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerUnlockedCharacter_Failed");
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
