using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020006F2 RID: 1778
public class ServerGetEventList
{
	// Token: 0x06002FAF RID: 12207 RVA: 0x00113948 File Offset: 0x00111B48
	public static IEnumerator Process(GameObject callbackObject)
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
				NetServerGetEventList net = new NetServerGetEventList();
				net.Request();
				monitor.StartMonitor(new ServerGetEventListRetry(callbackObject), -1f, HudNetworkConnect.DisplayType.ONLY_ICON);
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					List<ServerEventEntry> entries = ServerInterface.EventEntryList;
					entries.Clear();
					if (net.resultEventList != null)
					{
						int listCount = net.resultEventList.Count;
						for (int index = 0; index < listCount; index++)
						{
							entries.Add(net.resultEventList[index]);
						}
					}
					MsgGetEventListSucceed msg = new MsgGetEventListSucceed();
					msg.m_eventList = net.resultEventList;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetEventList_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetEventList_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
					if (EventManager.Instance != null)
					{
						EventManager.Instance.SynchServerEntryList();
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetEventList_Failed");
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
