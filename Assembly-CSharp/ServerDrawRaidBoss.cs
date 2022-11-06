using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x0200072B RID: 1835
public class ServerDrawRaidBoss
{
	// Token: 0x0600311A RID: 12570 RVA: 0x00116764 File Offset: 0x00114964
	public static IEnumerator Process(int eventId, long score, GameObject callbackObject)
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
				NetServerDrawRaidBoss net = new NetServerDrawRaidBoss(eventId, score);
				net.Request();
				monitor.StartMonitor(new ServerDrawRaidBossRetry(eventId, score, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					if (EventManager.Instance != null)
					{
						EventManager.Instance.SynchServerEventRaidBossList(net.RaidBossState);
					}
					MsgDrawRaidBossSucceed msg = new MsgDrawRaidBossSucceed();
					msg.m_raidBossState = net.RaidBossState;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerDrawRaidBoss_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerDrawRaidBoss_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerDrawRaidBoss_Failed");
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
