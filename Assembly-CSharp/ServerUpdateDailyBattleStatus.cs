using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006CF RID: 1743
public class ServerUpdateDailyBattleStatus
{
	// Token: 0x06002E97 RID: 11927 RVA: 0x00111DB8 File Offset: 0x0010FFB8
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
				NetServerUpdateDailyBattleStatus net = new NetServerUpdateDailyBattleStatus();
				net.Request();
				monitor.StartMonitor(new ServerUpdateDailyBattleStatusRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgUpdateDailyBattleStatusSucceed msg = new MsgUpdateDailyBattleStatusSucceed();
					net.battleDataStatus.CopyTo(msg.battleStatus);
					msg.endTime = net.endTime;
					msg.rewardFlag = net.rewardFlag;
					if (msg.rewardFlag && net.rewardBattleDataPair != null)
					{
						msg.rewardBattleDataPair = new ServerDailyBattleDataPair();
						net.rewardBattleDataPair.CopyTo(msg.rewardBattleDataPair);
					}
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerUpdateDailyBattleStatus_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerUpdateDailyBattleStatus_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerUpdateDailyBattleStatus_Failed", msg2, SendMessageOptions.DontRequireReceiver);
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

	// Token: 0x04002A3E RID: 10814
	private const string SuccessEvent = "ServerUpdateDailyBattleStatus_Succeeded";

	// Token: 0x04002A3F RID: 10815
	private const string FailEvent = "ServerUpdateDailyBattleStatus_Failed";
}
