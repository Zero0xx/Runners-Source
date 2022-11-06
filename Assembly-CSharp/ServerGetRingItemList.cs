using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x0200073B RID: 1851
public class ServerGetRingItemList
{
	// Token: 0x06003147 RID: 12615 RVA: 0x00116E14 File Offset: 0x00115014
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
				NetServerGetRingItemList net = new NetServerGetRingItemList();
				net.Request();
				monitor.StartMonitor(new ServerGetRingItemListRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					int resultRingItemStates = net.resultRingItemStates;
					List<ServerRingItemState> ringItemList = new List<ServerRingItemState>(resultRingItemStates);
					for (int i = 0; i < resultRingItemStates; i++)
					{
						ServerRingItemState ringItemState = net.GetResultRingItemState(i);
						ringItemList.Add(ringItemState);
					}
					MsgGetRingItemStateSucceed msg = new MsgGetRingItemStateSucceed();
					msg.m_RingStateList = ringItemList;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "GetRingItemList_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("GetRingItemList_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "GetRingItemList_Failed");
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
