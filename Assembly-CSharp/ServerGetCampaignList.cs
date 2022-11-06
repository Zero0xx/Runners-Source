using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x0200072D RID: 1837
public class ServerGetCampaignList
{
	// Token: 0x0600311E RID: 12574 RVA: 0x001167E4 File Offset: 0x001149E4
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
				NetServerGetCampaignList net = new NetServerGetCampaignList();
				net.Request();
				monitor.StartMonitor(new ServerGetCampaignListRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgGetCampaignListSucceed msg = new MsgGetCampaignListSucceed();
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "GetCampaignList_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("GetCampaignList_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "GetCampaignList_Failed");
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
