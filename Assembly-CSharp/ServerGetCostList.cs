using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x0200072F RID: 1839
public class ServerGetCostList
{
	// Token: 0x06003122 RID: 12578 RVA: 0x00116848 File Offset: 0x00114A48
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
				NetServerGetCostList net = new NetServerGetCostList();
				net.Request();
				monitor.StartMonitor(new ServerGetCostListRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					List<ServerConsumedCostData> costData = ServerInterface.CostList;
					if (costData != null)
					{
						costData.Clear();
						List<ServerConsumedCostData> resultCostData = net.resultCostList;
						if (resultCostData != null)
						{
							foreach (ServerConsumedCostData data in resultCostData)
							{
								if (data != null)
								{
									costData.Add(data);
								}
							}
						}
					}
					MsgGetCostListSucceed msg = new MsgGetCostListSucceed();
					msg.m_costList = costData;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "GetCostList_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("GetCostList_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "GetCostList_Failed");
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
