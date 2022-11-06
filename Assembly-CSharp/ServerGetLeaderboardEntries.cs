using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000748 RID: 1864
public class ServerGetLeaderboardEntries
{
	// Token: 0x060031B7 RID: 12727 RVA: 0x00117B1C File Offset: 0x00115D1C
	public static IEnumerator Process(int mode, int first, int count, int index, int rankingType, int eventId, string[] friendIdList, GameObject callbackObject)
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
				NetServerGetLeaderboardEntries net = new NetServerGetLeaderboardEntries(mode, first, count, index, rankingType, eventId, friendIdList);
				net.Request();
				monitor.StartMonitor(new ServerGetLeaderboardEntriesRetry(mode, first, count, index, rankingType, eventId, friendIdList, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgGetLeaderboardEntriesSucceed msg = new MsgGetLeaderboardEntriesSucceed();
					msg.m_leaderboardEntries = ServerInterface.LeaderboardEntries;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetLeaderboardEntries_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetLeaderboardEntries_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetLeaderboardEntries_Failed");
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
