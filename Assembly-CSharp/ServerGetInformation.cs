using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x0200075D RID: 1885
public class ServerGetInformation
{
	// Token: 0x06003284 RID: 12932 RVA: 0x001196B4 File Offset: 0x001178B4
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
				ServerNoticeInfo instance = ServerInterface.NoticeInfo;
				NetServerGetNoticeInfo net = new NetServerGetNoticeInfo();
				net.Request();
				monitor.StartMonitor(new ServerGetInformationRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				instance.Clear();
				if (net.IsSucceeded())
				{
					instance.m_isGetNoticeInfo = true;
					instance.LastUpdateInfoTime = NetUtil.GetCurrentTime();
					int num = net.GetInfoCount();
					for (int i = 0; i < num; i++)
					{
						NetNoticeItem info = net.GetInfo(i);
						if (NetUtil.IsServerTimeWithinPeriod(info.Start, info.End))
						{
							if (info.WindowType == 14)
							{
								instance.m_rouletteItems.Add(info);
							}
							else if (info.WindowType == 15)
							{
								instance.m_eventItems.Add(info);
							}
							else
							{
								instance.m_noticeItems.Add(info);
							}
						}
					}
					if (instance.m_noticeItems.Count > 1)
					{
						instance.m_noticeItems.Sort(new Comparison<NetNoticeItem>(ServerGetInformation.PriorityComparer));
					}
					MsgGetInformationSucceed msg = new MsgGetInformationSucceed();
					msg.m_information = instance;
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetInformation_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetInformation_Failed");
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

	// Token: 0x06003285 RID: 12933 RVA: 0x001196D8 File Offset: 0x001178D8
	private static int PriorityComparer(NetNoticeItem itemA, NetNoticeItem itemB)
	{
		if (itemA == null || itemB == null)
		{
			return 0;
		}
		if (itemA.Id >= (long)NetNoticeItem.OPERATORINFO_START_ID)
		{
			if (itemB.Id >= (long)NetNoticeItem.OPERATORINFO_START_ID)
			{
				return itemA.Priority - itemB.Priority;
			}
			return 1;
		}
		else
		{
			if (itemB.Id >= (long)NetNoticeItem.OPERATORINFO_START_ID)
			{
				return -1;
			}
			return itemA.Priority - itemB.Priority;
		}
	}
}
