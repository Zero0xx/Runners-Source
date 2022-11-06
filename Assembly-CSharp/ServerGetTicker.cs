using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000761 RID: 1889
public class ServerGetTicker
{
	// Token: 0x0600328D RID: 12941 RVA: 0x00119804 File Offset: 0x00117A04
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
				List<ServerTickerData> instance = ServerInterface.TickerInfo.Data;
				NetServerGetTicker net = new NetServerGetTicker();
				net.Request();
				monitor.StartMonitor(new ServerGetTickerRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				instance.Clear();
				if (net.IsSucceeded())
				{
					int num = net.GetInfoCount();
					for (int i = 0; i < num; i++)
					{
						ServerTickerData info = net.GetInfo(i);
						if (NetUtil.IsServerTimeWithinPeriod(info.Start, info.End))
						{
							instance.Add(info);
						}
					}
					MsgGetTickerDataSucceed msg = new MsgGetTickerDataSucceed();
					msg.m_tickerData = instance;
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetTicker_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetTicker_Failed");
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
