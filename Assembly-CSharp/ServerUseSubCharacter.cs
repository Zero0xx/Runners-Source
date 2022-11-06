using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006B6 RID: 1718
public class ServerUseSubCharacter
{
	// Token: 0x06002E18 RID: 11800 RVA: 0x00110D40 File Offset: 0x0010EF40
	public static IEnumerator Process(bool useFlag, GameObject callbackObject)
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
				NetServerUseSubCharacter net = new NetServerUseSubCharacter(useFlag);
				net.Request();
				monitor.StartMonitor(new ServerUseSubCharacterRetry(useFlag, callbackObject));
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
						monitor.EndMonitorForward(msg, callbackObject, "ServerUseSubCharacter_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerUseSubCharacter_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerUseSubCharacter_Failed");
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
