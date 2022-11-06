using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000789 RID: 1929
public class ServerSetUserName
{
	// Token: 0x0600333D RID: 13117 RVA: 0x0011B18C File Offset: 0x0011938C
	public static IEnumerator Process(string userName, GameObject callbackObject)
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
				NetServerSetUserName net = new NetServerSetUserName(userName);
				net.Request();
				monitor.StartMonitor(new ServerSetUserNameRetry(userName, callbackObject));
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
					MsgGetPlayerStateSucceed msg = new MsgGetPlayerStateSucceed();
					msg.m_playerState = playerState;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerSetUserName_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerSetUserName_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerSetUserName_Failed");
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
