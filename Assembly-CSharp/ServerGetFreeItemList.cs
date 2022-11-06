using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000733 RID: 1843
public class ServerGetFreeItemList
{
	// Token: 0x0600312A RID: 12586 RVA: 0x00116910 File Offset: 0x00114B10
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
				NetServerGetFreeItemList net = new NetServerGetFreeItemList();
				net.Request();
				monitor.StartMonitor(new ServerGetFreeItemListRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerFreeItemState freeItemState = ServerInterface.FreeItemState;
					if (freeItemState != null)
					{
						ServerFreeItemState resultFreeItemState = net.resultFreeItemState;
						if (resultFreeItemState != null)
						{
							freeItemState.ClearList();
							resultFreeItemState.CopyTo(freeItemState);
						}
					}
					MsgGetFreeItemListSucceed msg = new MsgGetFreeItemListSucceed();
					msg.m_freeItemState = freeItemState;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetFreeItemList_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetFreeItemList_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetFreeItemList_Failed");
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
