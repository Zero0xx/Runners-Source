using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006CD RID: 1741
public class ServerResetDailyBattleMatching
{
	// Token: 0x06002E93 RID: 11923 RVA: 0x00111D48 File Offset: 0x0010FF48
	public static IEnumerator Process(int type, GameObject callbackObject)
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
				NetServerResetDailyBattleMatching net = new NetServerResetDailyBattleMatching(type);
				net.Request();
				monitor.StartMonitor(new ServerResetDailyBattleMatchingRetry(type, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgResetDailyBattleMatchingSucceed msg = new MsgResetDailyBattleMatchingSucceed();
					net.playerState.CopyTo(msg.playerState);
					net.playerState.CopyTo(ServerInterface.PlayerState);
					net.battleDataPair.CopyTo(msg.battleDataPair);
					msg.endTime = net.endTime;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerResetDailyBattleMatching_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerResetDailyBattleMatching_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerResetDailyBattleMatching_Failed", msg2, SendMessageOptions.DontRequireReceiver);
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

	// Token: 0x04002A3C RID: 10812
	private const string SuccessEvent = "ServerResetDailyBattleMatching_Succeeded";

	// Token: 0x04002A3D RID: 10813
	private const string FailEvent = "ServerResetDailyBattleMatching_Failed";
}
