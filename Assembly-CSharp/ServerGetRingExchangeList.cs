using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020007B9 RID: 1977
public class ServerGetRingExchangeList
{
	// Token: 0x0600343E RID: 13374 RVA: 0x0011CD44 File Offset: 0x0011AF44
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
				NetServerGetRingExchangeList net = new NetServerGetRingExchangeList();
				net.Request();
				monitor.StartMonitor(new ServerGetRingExchangeListRetry(callbackObject), 0f, HudNetworkConnect.DisplayType.NO_BG);
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					List<ServerRingExchangeList> exchangeList = ServerInterface.RingExchangeList;
					List<ServerRingExchangeList> resultExchangeList = net.m_ringExchangeList;
					exchangeList.Clear();
					for (int index = 0; index < resultExchangeList.Count; index++)
					{
						ServerRingExchangeList e = new ServerRingExchangeList();
						resultExchangeList[index].CopyTo(e);
						exchangeList.Add(e);
					}
					MsgGetRingExchangeListSucceed msg = new MsgGetRingExchangeListSucceed();
					msg.m_exchangeList = resultExchangeList;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetRingExchangeList_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetRingExchangeList_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetRingExchangeList_Failed");
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
