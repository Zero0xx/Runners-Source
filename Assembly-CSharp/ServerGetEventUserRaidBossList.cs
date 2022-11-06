using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006FC RID: 1788
public class ServerGetEventUserRaidBossList
{
	// Token: 0x06002FC3 RID: 12227 RVA: 0x00113BFC File Offset: 0x00111DFC
	public static IEnumerator Process(int eventId, GameObject callbackObject)
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
				NetServerGetEventUserRaidBossList net = new NetServerGetEventUserRaidBossList(eventId);
				net.Request();
				monitor.StartMonitor(new ServerGetEventUserRaidBossListRetry(eventId, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgGetEventUserRaidBossListSucceed msg = new MsgGetEventUserRaidBossListSucceed();
					if (EventManager.Instance != null)
					{
						EventManager.Instance.SynchServerEventRaidBossList(net.UserRaidBossList);
						if (net.UserRaidBossState != null)
						{
							EventManager.Instance.SynchServerEventUserRaidBossState(net.UserRaidBossState);
							GeneralUtil.SetItemCount(ServerItem.Id.RAIDRING, (long)net.UserRaidBossState.NumRaidbossRings);
						}
					}
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetEventUserRaidBossList_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetEventUserRaidBossList_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetEventUserRaidBossList_Failed");
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

	// Token: 0x04002AA8 RID: 10920
	private const string SUCCEEDED_FUNCTION_NAME = "ServerGetEventUserRaidBossList_Succeeded";

	// Token: 0x04002AA9 RID: 10921
	private const string FAILED_FUNCTION_NAME = "ServerGetEventUserRaidBossList_Failed";
}
