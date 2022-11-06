using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000785 RID: 1925
public class ServerGetCharacterState
{
	// Token: 0x06003335 RID: 13109 RVA: 0x0011B0B8 File Offset: 0x001192B8
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
				NetServerGetCharacterState net = new NetServerGetCharacterState();
				net.Request();
				monitor.StartMonitor(new ServerGetCharacterStateRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerPlayerState playerState = ServerInterface.PlayerState;
					ServerCharacterState[] resultCharacterState = net.resultCharacterState;
					playerState.ClearCharacterState();
					foreach (ServerCharacterState characterState in resultCharacterState)
					{
						if (characterState != null)
						{
							playerState.SetCharacterState(characterState);
						}
					}
					MsgGetCharacterStateSucceed msg = new MsgGetCharacterStateSucceed();
					msg.m_playerState = playerState;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetCharacterState_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetCharacterState_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetCharacterState_Failed");
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
