using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006F6 RID: 1782
public class ServerGetEventRaidBossUserList
{
	// Token: 0x06002FB7 RID: 12215 RVA: 0x00113A78 File Offset: 0x00111C78
	public static IEnumerator Process(int eventId, long raidBossId, GameObject callbackObject)
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
				NetServerGetEventRaidBossUserList net = new NetServerGetEventRaidBossUserList(eventId, raidBossId);
				net.Request();
				monitor.StartMonitor(new ServerGetEventRaidBossUserListRetry(eventId, raidBossId, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					if (EventManager.Instance != null)
					{
						if (net.RaidBossState != null)
						{
							EventManager.Instance.SynchServerEventRaidBossList(net.RaidBossState);
						}
						EventManager.Instance.SynchServerEventRaidBossUserList(net.RaidBossUserStateList, raidBossId, net.RaidBossBonus);
					}
					MsgGetEventRaidBossUserListSucceed msg = new MsgGetEventRaidBossUserListSucceed();
					msg.m_bonus = net.RaidBossBonus;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetEventRaidBossUserList_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetEventRaidBossUserList_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetEventRaidBossUserList_Failed");
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

	// Token: 0x04002AA3 RID: 10915
	private const string SUCCEEDED_FUNCTION_NAME = "ServerGetEventRaidBossUserList_Succeeded";

	// Token: 0x04002AA4 RID: 10916
	private const string FAILED_FUNCTION_NAME = "ServerGetEventRaidBossUserList_Failed";
}
