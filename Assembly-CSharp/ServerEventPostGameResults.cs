using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006EC RID: 1772
public class ServerEventPostGameResults
{
	// Token: 0x06002FA3 RID: 12195 RVA: 0x0011375C File Offset: 0x0011195C
	public static IEnumerator Process(int eventId, int numRaidBossRings, GameObject callbackObject)
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
				NetServerEventPostGameResults net = new NetServerEventPostGameResults(eventId, numRaidBossRings);
				net.Request();
				monitor.StartMonitor(new ServerEventPostGameResultsRetry(eventId, numRaidBossRings, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					if (EventManager.Instance != null)
					{
						ServerEventUserRaidBossState userRaidBossState = net.UserRaidBossState;
						EventManager.Instance.SynchServerEventUserRaidBossState(userRaidBossState);
					}
					GeneralUtil.SetItemCount(ServerItem.Id.RAIDRING, (long)net.UserRaidBossState.NumRaidbossRings);
					MsgEventPostGameResultsSucceed msg = new MsgEventPostGameResultsSucceed();
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerEventPostGameResults_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerEventPostGameResults_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerEventPostGameResults_Failed");
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
