using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x0200074A RID: 1866
public class ServerGetLeagueData
{
	// Token: 0x060031BB RID: 12731 RVA: 0x00117BF4 File Offset: 0x00115DF4
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
				NetServerGetLeagueData net = new NetServerGetLeagueData(mode);
				net.Request();
				monitor.StartMonitor(new ServerGetLeagueDataRetry(mode, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerLeagueData resultLeagueData = net.leagueData;
					if (resultLeagueData == null)
					{
						resultLeagueData = new ServerLeagueData();
					}
					if (SingletonGameObject<RankingManager>.Instance != null)
					{
						SingletonGameObject<RankingManager>.Instance.SetLeagueData(resultLeagueData);
					}
					MsgGetLeagueDataSucceed msg = new MsgGetLeagueDataSucceed();
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetLeagueData_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetLeagueData_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetLeagueData_Failed");
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
