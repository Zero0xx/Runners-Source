using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006C5 RID: 1733
public class ServerGetDailyBattleDataHistory
{
	// Token: 0x06002E83 RID: 11907 RVA: 0x00111BA0 File Offset: 0x0010FDA0
	public static IEnumerator Process(int count, GameObject callbackObject)
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
				NetServerGetDailyBattleDataHistory net = new NetServerGetDailyBattleDataHistory(count);
				net.Request();
				monitor.StartMonitor(new ServerGetDailyBattleDataHistoryRetry(count, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgGetDailyBattleDataHistorySucceed msg = new MsgGetDailyBattleDataHistorySucceed();
					if (net.battleDataPairList != null && net.battleDataPairList.Count > 0)
					{
						foreach (ServerDailyBattleDataPair pair in net.battleDataPairList)
						{
							ServerDailyBattleDataPair setPair = new ServerDailyBattleDataPair();
							pair.CopyTo(setPair);
							msg.battleDataPairList.Add(setPair);
						}
					}
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetDailyBattleDataHistory_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetDailyBattleDataHistory_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetDailyBattleDataHistory_Failed", msg2, SendMessageOptions.DontRequireReceiver);
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

	// Token: 0x04002A33 RID: 10803
	private const string SuccessEvent = "ServerGetDailyBattleDataHistory_Succeeded";

	// Token: 0x04002A34 RID: 10804
	private const string FailEvent = "ServerGetDailyBattleDataHistory_Failed";
}
