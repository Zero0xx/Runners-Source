using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020006F8 RID: 1784
public class ServerGetEventReward
{
	// Token: 0x06002FBB RID: 12219 RVA: 0x00113B04 File Offset: 0x00111D04
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
				NetServerGetEventReward net = new NetServerGetEventReward(eventId);
				net.Request();
				monitor.StartMonitor(new ServerGetEventRewardRetry(eventId, callbackObject), -1f, HudNetworkConnect.DisplayType.ONLY_ICON);
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					List<ServerEventReward> rewards = ServerInterface.EventRewardList;
					rewards.Clear();
					if (net.resultEventRewardList != null)
					{
						int listCount = net.resultEventRewardList.Count;
						for (int index = 0; index < listCount; index++)
						{
							rewards.Add(net.resultEventRewardList[index]);
						}
					}
					MsgGetEventRewardSucceed msg = new MsgGetEventRewardSucceed();
					msg.m_eventRewardList = net.resultEventRewardList;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetEventReward_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetEventReward_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
					if (EventManager.Instance != null)
					{
						EventManager.Instance.SynchServerRewardList();
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetEventReward_Failed");
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
