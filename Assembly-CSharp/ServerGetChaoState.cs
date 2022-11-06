using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000783 RID: 1923
public class ServerGetChaoState
{
	// Token: 0x06003331 RID: 13105 RVA: 0x0011B054 File Offset: 0x00119254
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
				NetServerGetChaoState net = new NetServerGetChaoState();
				net.Request();
				monitor.StartMonitor(new ServerGetChaoStateRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerPlayerState playerState = ServerInterface.PlayerState;
					List<ServerChaoState> resultChaoState = net.resultChaoState;
					playerState.SetChaoState(resultChaoState);
					MsgGetChaoStateSucceed msg = new MsgGetChaoStateSucceed();
					msg.m_playerState = playerState;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetChaoState_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetChaoState_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetChaoState_Failed");
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
