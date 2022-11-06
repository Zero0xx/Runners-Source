using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006C9 RID: 1737
public class ServerGetPrizeDailyBattle
{
	// Token: 0x06002E8B RID: 11915 RVA: 0x00111C74 File Offset: 0x0010FE74
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
				NetServerGetPrizeDailyBattle net = new NetServerGetPrizeDailyBattle();
				net.Request();
				monitor.StartMonitor(new ServerGetPrizeDailyBattleRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgGetPrizeDailyBattleSucceed msg = new MsgGetPrizeDailyBattleSucceed();
					if (net.battleDataPrizeList != null && net.battleDataPrizeList.Count > 0)
					{
						foreach (ServerDailyBattlePrizeData prize in net.battleDataPrizeList)
						{
							ServerDailyBattlePrizeData setPrize = new ServerDailyBattlePrizeData();
							prize.CopyTo(setPrize);
							msg.battlePrizeDataList.Add(setPrize);
						}
					}
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetPrizeDailyBattle_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetPrizeDailyBattle_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetPrizeDailyBattle_Failed", msg2, SendMessageOptions.DontRequireReceiver);
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

	// Token: 0x04002A37 RID: 10807
	private const string SuccessEvent = "ServerGetPrizeDailyBattle_Succeeded";

	// Token: 0x04002A38 RID: 10808
	private const string FailEvent = "ServerGetPrizeDailyBattle_Failed";
}
