using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020007A0 RID: 1952
public class ServerGetItemStockNum
{
	// Token: 0x060033C0 RID: 13248 RVA: 0x0011C20C File Offset: 0x0011A40C
	public static IEnumerator Process(int eventId, List<int> itemId, GameObject callbackObject)
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
				NetServerGetItemStockNum net = new NetServerGetItemStockNum(eventId, itemId);
				net.Request();
				monitor.StartMonitor(new ServerGetItemStockNumRetry(eventId, itemId, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					List<ServerItemState> itemStockList = net.m_itemStockList;
					if (itemStockList != null && itemStockList.Count > 0)
					{
						foreach (ServerItemState item in itemStockList)
						{
							GeneralUtil.SetItemCount((ServerItem.Id)item.m_itemId, (long)item.m_num);
						}
					}
					MsgGetItemStockNumSucceed msg = new MsgGetItemStockNumSucceed();
					msg.m_itemStockList = itemStockList;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetItemStockNum_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetItemStockNum_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetItemStockNum_Failed");
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
