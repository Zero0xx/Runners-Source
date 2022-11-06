using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020006F4 RID: 1780
public class ServerGetEventRaidBossDesiredList
{
	// Token: 0x06002FB3 RID: 12211 RVA: 0x001139D4 File Offset: 0x00111BD4
	public static IEnumerator Process(int eventId, long raidBossId, List<string> friendIdList, GameObject callbackObject)
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
				NetServerGetEventRaidBossDesiredList net = new NetServerGetEventRaidBossDesiredList(eventId, raidBossId, friendIdList);
				net.Request();
				monitor.StartMonitor(new ServerGetEventRaidBossDesiredListRetry(eventId, raidBossId, friendIdList, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgEventRaidBossDesiredListSucceed msg = new MsgEventRaidBossDesiredListSucceed();
					msg.m_desiredList = net.DesiredList;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetEventRaidBossDesiredList_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetEventRaidBossDesiredList_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetEventRaidBossDesiredList_Failed");
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
