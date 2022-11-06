using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000735 RID: 1845
public class ServerGetMenuData
{
	// Token: 0x0600312E RID: 12590 RVA: 0x00116974 File Offset: 0x00114B74
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
				NetServerGetMenuData net = new NetServerGetMenuData();
				net.Request();
				monitor.StartMonitor(new ServerGetMenuDataRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerGetMenuData.SetPlayerState(net.PlayerState);
					ServerGetMenuData.SetWheelOptions(net.WheelOptions);
					ServerGetMenuData.SetChaoWheelOptions(net.ChaoWheelOptions);
					ServerGetMenuData.SetSubCharaRingPayment(net.SubCharaRingExchange);
					ServerGetMenuData.SetDailyChallenge(net.DailyChallengeState);
					ServerGetMenuData.SetMileageMap(net.MileageMapState);
					ServerGetMenuData.SetMessage(net.MessageEntryList, net.TotalMessage);
					ServerGetMenuData.SetOperatorMessage(net.OperatorMessageEntryList, net.TotalOperatorMessage);
					ServerGetMenuData.SetRedStarExchangeList(ServerInterface.RedStarItemList, net.RedstarItemStateList, net.RedstarTotalItems);
					ServerGetMenuData.SetRedStarExchangeList(ServerInterface.RedStarExchangeRingItemList, net.RingItemStateList, net.RingTotalItems);
					ServerGetMenuData.SetRedStarExchangeList(ServerInterface.RedStarExchangeEnergyItemList, net.EnergyItemStateList, net.EnergyTotalItems);
					ServerGetMenuData.SetMonthPurchase(net.MonthPurchase);
					ServerGetMenuData.SetBirthday(net.BirthDay);
					ServerGetMenuData.SetConsumedCostList(net.ConsumedCostList);
					MsgGetMenuDataSucceed msg = new MsgGetMenuDataSucceed();
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetMenuData_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetMenuData_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetMenuData_Failed");
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

	// Token: 0x0600312F RID: 12591 RVA: 0x00116998 File Offset: 0x00114B98
	private static void SetPlayerState(ServerPlayerState resultPlayerState)
	{
		if (resultPlayerState != null)
		{
			ServerPlayerState playerState = ServerInterface.PlayerState;
			if (playerState != null)
			{
				resultPlayerState.CopyTo(playerState);
			}
		}
	}

	// Token: 0x06003130 RID: 12592 RVA: 0x001169C0 File Offset: 0x00114BC0
	private static void SetWheelOptions(ServerWheelOptions resultWheelOptions)
	{
		if (resultWheelOptions != null)
		{
			ServerWheelOptions wheelOptions = ServerInterface.WheelOptions;
			if (wheelOptions != null)
			{
				resultWheelOptions.CopyTo(wheelOptions);
			}
		}
	}

	// Token: 0x06003131 RID: 12593 RVA: 0x001169E8 File Offset: 0x00114BE8
	private static void SetChaoWheelOptions(ServerChaoWheelOptions resultChaoWheelOptions)
	{
		if (resultChaoWheelOptions != null)
		{
			ServerChaoWheelOptions chaoWheelOptions = ServerInterface.ChaoWheelOptions;
			if (chaoWheelOptions != null)
			{
				resultChaoWheelOptions.CopyTo(chaoWheelOptions);
			}
		}
	}

	// Token: 0x06003132 RID: 12594 RVA: 0x00116A10 File Offset: 0x00114C10
	private static void SetSubCharaRingPayment(int subCharaRingPayment)
	{
		ServerSettingState settingState = ServerInterface.SettingState;
		if (settingState != null)
		{
			settingState.m_subCharaRingPayment = subCharaRingPayment;
		}
	}

	// Token: 0x06003133 RID: 12595 RVA: 0x00116A30 File Offset: 0x00114C30
	private static void SetDailyChallenge(ServerDailyChallengeState dailyChallengeState)
	{
		if (dailyChallengeState != null)
		{
			int count = dailyChallengeState.m_incentiveList.Count;
			ServerDailyChallengeState dailyChallengeState2 = ServerInterface.DailyChallengeState;
			if (dailyChallengeState2 != null)
			{
				dailyChallengeState2.m_incentiveList.Clear();
				for (int i = 0; i < count; i++)
				{
					dailyChallengeState2.m_incentiveList.Add(dailyChallengeState.m_incentiveList[i]);
				}
				dailyChallengeState2.m_numIncentiveCont = dailyChallengeState.m_numIncentiveCont;
				dailyChallengeState2.m_numDailyChalDay = dailyChallengeState.m_numDailyChalDay;
				dailyChallengeState2.m_maxDailyChalDay = dailyChallengeState.m_maxDailyChalDay;
				dailyChallengeState2.m_maxIncentive = dailyChallengeState.m_maxIncentive;
				NetUtil.SyncSaveDataAndDailyMission(dailyChallengeState2);
			}
		}
	}

	// Token: 0x06003134 RID: 12596 RVA: 0x00116AC8 File Offset: 0x00114CC8
	private static void SetMileageMap(ServerMileageMapState resultMileageMapState)
	{
		if (resultMileageMapState != null)
		{
			ServerMileageMapState mileageMapState = ServerInterface.MileageMapState;
			if (mileageMapState != null)
			{
				resultMileageMapState.CopyTo(mileageMapState);
			}
		}
	}

	// Token: 0x06003135 RID: 12597 RVA: 0x00116AF0 File Offset: 0x00114CF0
	private static void SetMessage(List<ServerMessageEntry> resultMessageList, int totalMessage)
	{
		if (resultMessageList != null)
		{
			List<ServerMessageEntry> messageList = ServerInterface.MessageList;
			messageList.Clear();
			for (int i = 0; i < totalMessage; i++)
			{
				ServerMessageEntry item = resultMessageList[i];
				messageList.Add(item);
			}
		}
	}

	// Token: 0x06003136 RID: 12598 RVA: 0x00116B34 File Offset: 0x00114D34
	private static void SetOperatorMessage(List<ServerOperatorMessageEntry> resultMessageList, int totalMessage)
	{
		if (resultMessageList != null)
		{
			List<ServerOperatorMessageEntry> operatorMessageList = ServerInterface.OperatorMessageList;
			operatorMessageList.Clear();
			for (int i = 0; i < totalMessage; i++)
			{
				ServerOperatorMessageEntry item = resultMessageList[i];
				operatorMessageList.Add(item);
			}
		}
	}

	// Token: 0x06003137 RID: 12599 RVA: 0x00116B78 File Offset: 0x00114D78
	private static void SetRedStarExchangeList(List<ServerRedStarItemState> serverExachangeList, List<ServerRedStarItemState> exchangeList, int resultItems)
	{
		if (exchangeList != null && serverExachangeList != null)
		{
			serverExachangeList.Clear();
			for (int i = 0; i < resultItems; i++)
			{
				ServerRedStarItemState serverRedStarItemState = exchangeList[i];
				if (serverRedStarItemState != null)
				{
					ServerRedStarItemState serverRedStarItemState2 = new ServerRedStarItemState();
					serverRedStarItemState.CopyTo(serverRedStarItemState2);
					if (serverExachangeList != null)
					{
						serverExachangeList.Add(serverRedStarItemState2);
					}
				}
			}
		}
	}

	// Token: 0x06003138 RID: 12600 RVA: 0x00116BD4 File Offset: 0x00114DD4
	private static void SetMonthPurchase(int monthPurchase)
	{
		ServerSettingState settingState = ServerInterface.SettingState;
		if (settingState != null)
		{
			settingState.m_monthPurchase = monthPurchase;
		}
	}

	// Token: 0x06003139 RID: 12601 RVA: 0x00116BF4 File Offset: 0x00114DF4
	private static void SetBirthday(string birthday)
	{
		ServerSettingState settingState = ServerInterface.SettingState;
		if (settingState != null)
		{
			settingState.m_birthday = birthday;
		}
	}

	// Token: 0x0600313A RID: 12602 RVA: 0x00116C14 File Offset: 0x00114E14
	private static void SetConsumedCostList(List<ServerConsumedCostData> serverConsumedCostList)
	{
		List<ServerConsumedCostData> consumedCostList = ServerInterface.ConsumedCostList;
		if (consumedCostList != null)
		{
			consumedCostList.Clear();
			foreach (ServerConsumedCostData serverConsumedCostData in serverConsumedCostList)
			{
				if (serverConsumedCostData != null)
				{
					ServerConsumedCostData serverConsumedCostData2 = new ServerConsumedCostData();
					serverConsumedCostData.CopyTo(serverConsumedCostData2);
					consumedCostList.Add(serverConsumedCostData2);
				}
			}
		}
	}
}
