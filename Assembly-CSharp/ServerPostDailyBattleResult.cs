using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006CB RID: 1739
public class ServerPostDailyBattleResult
{
	// Token: 0x06002E8F RID: 11919 RVA: 0x00111CD8 File Offset: 0x0010FED8
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
				NetServerPostDailyBattleResult net = new NetServerPostDailyBattleResult();
				net.Request();
				monitor.StartMonitor(new ServerPostDailyBattleResultRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgPostDailyBattleResultSucceed msg = new MsgPostDailyBattleResultSucceed();
					net.battleStatus.CopyTo(msg.battleStatus);
					net.battleDataPair.CopyTo(msg.battleDataPair);
					msg.rewardFlag = net.rewardFlag;
					if (msg.rewardFlag && net.rewardBattleDataPair != null)
					{
						msg.rewardBattleDataPair = new ServerDailyBattleDataPair();
						net.rewardBattleDataPair.CopyTo(msg.rewardBattleDataPair);
					}
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerPostDailyBattleResult_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerPostDailyBattleResult_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerPostDailyBattleResult_Failed", msg2, SendMessageOptions.DontRequireReceiver);
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

	// Token: 0x04002A39 RID: 10809
	private const string SuccessEvent = "ServerPostDailyBattleResult_Succeeded";

	// Token: 0x04002A3A RID: 10810
	private const string FailEvent = "ServerPostDailyBattleResult_Failed";
}
