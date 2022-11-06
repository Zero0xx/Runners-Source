using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x0200074E RID: 1870
public class ServerGetWeeklyLeaderboardOptions
{
	// Token: 0x060031C3 RID: 12739 RVA: 0x00117CEC File Offset: 0x00115EEC
	public static IEnumerator Process(int mode, GameObject callbackObject)
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
				NetServerGetWeeklyLeaderboardOptions net = new NetServerGetWeeklyLeaderboardOptions(mode);
				net.Request();
				monitor.StartMonitor(new ServerGetWeeklyLeaderboardOptionsRetry(mode, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgGetWeeklyLeaderboardOptions msg = new MsgGetWeeklyLeaderboardOptions();
					msg.m_weeklyLeaderboardOptions = net.weeklyLeaderboardOptions;
					if (SingletonGameObject<RankingManager>.Instance != null)
					{
						SingletonGameObject<RankingManager>.Instance.SetRankingDataSet(net.weeklyLeaderboardOptions);
					}
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetWeeklyLeaderboardOptions_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetWeeklyLeaderboardOptions_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerWeeklyLeaderboardOptions_Failed");
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
