using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020007B7 RID: 1975
public class ServerGetRedStarExchangeList
{
	// Token: 0x0600343A RID: 13370 RVA: 0x0011CCD4 File Offset: 0x0011AED4
	public static IEnumerator Process(int itemType, GameObject callbackObject)
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
				NetServerGetRedStarExchangeList net = new NetServerGetRedStarExchangeList(itemType);
				net.Request();
				monitor.StartMonitor(new ServerGetRedStarExchangeListRetry(itemType, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					int resultItems = net.resultItems;
					List<ServerRedStarItemState> exchangeList = new List<ServerRedStarItemState>(net.resultItems);
					List<ServerRedStarItemState> serverExachangeList = null;
					switch (itemType)
					{
					case 0:
					{
						serverExachangeList = ServerInterface.RedStarItemList;
						ServerSettingState settingState = ServerInterface.SettingState;
						if (settingState != null)
						{
							settingState.m_birthday = net.resultBirthDay;
							settingState.m_monthPurchase = net.resultMonthPurchase;
						}
						break;
					}
					case 1:
						serverExachangeList = ServerInterface.RedStarExchangeRingItemList;
						break;
					case 2:
						serverExachangeList = ServerInterface.RedStarExchangeEnergyItemList;
						break;
					case 4:
						serverExachangeList = ServerInterface.RedStarExchangeRaidbossEnergyItemList;
						break;
					}
					if (serverExachangeList != null)
					{
						serverExachangeList.Clear();
					}
					for (int i = 0; i < resultItems; i++)
					{
						ServerRedStarItemState result = net.GetResultRedStarItemState(i);
						if (result != null)
						{
							ServerRedStarItemState item = new ServerRedStarItemState();
							result.CopyTo(item);
							exchangeList.Add(item);
							if (serverExachangeList != null)
							{
								serverExachangeList.Add(item);
							}
						}
					}
					MsgGetRedStarExchangeListSucceed msg = new MsgGetRedStarExchangeListSucceed();
					msg.m_itemList = exchangeList;
					msg.m_totalItems = resultItems;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetRedStarExchangeList_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetRedStarExchangeList_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetRedStarExchangeList_Failed");
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
