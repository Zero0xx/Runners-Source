using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000706 RID: 1798
public class ServerGetFacebookIncentive
{
	// Token: 0x0600300E RID: 12302 RVA: 0x001142A4 File Offset: 0x001124A4
	public static IEnumerator Process(int incentiveType, int achievementCount, GameObject callbackObject)
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
				NetServerGetFacebookIncentive net = new NetServerGetFacebookIncentive(incentiveType, achievementCount);
				net.Request();
				monitor.StartMonitor(new ServerGetFacebookIncentiveRetry(incentiveType, achievementCount, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					List<ServerPresentState> incentive = net.incentive;
					ServerPlayerState playerState = ServerInterface.PlayerState;
					ServerPlayerState resultPlayerState = net.playerState;
					resultPlayerState.CopyTo(playerState);
					MsgGetNormalIncentiveSucceed msg = new MsgGetNormalIncentiveSucceed();
					msg.m_playerState = playerState;
					msg.m_incentive = incentive;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetFacebookIncentive_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetFacebookIncentive_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetFacebookIncentive_Failed");
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

	// Token: 0x02000707 RID: 1799
	public enum IncentiveType
	{
		// Token: 0x04002ABE RID: 10942
		LOGIN,
		// Token: 0x04002ABF RID: 10943
		REVIEW,
		// Token: 0x04002AC0 RID: 10944
		FEED,
		// Token: 0x04002AC1 RID: 10945
		ACHIEVEMENT,
		// Token: 0x04002AC2 RID: 10946
		PUSH_NOLOGIN
	}
}
