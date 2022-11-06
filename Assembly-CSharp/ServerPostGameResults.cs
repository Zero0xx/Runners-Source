using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x0200073D RID: 1853
public class ServerPostGameResults
{
	// Token: 0x0600314B RID: 12619 RVA: 0x00116E84 File Offset: 0x00115084
	public static IEnumerator Process(ServerGameResults results, GameObject callbackObject)
	{
		NetMonitor monitor = NetMonitor.Instance;
		if (monitor != null)
		{
			monitor.PrepareConnect(ServerSessionWatcher.ValidateType.LOGIN_OR_RELOGIN);
			while (!monitor.IsEndPrepare())
			{
				yield return null;
			}
			if (monitor.IsSuccessPrepare())
			{
				NetServerPostGameResults net = new NetServerPostGameResults(results);
				net.Request();
				monitor.StartMonitor(new ServerPostGameResultsRetry(results, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerPlayerState playerState = ServerInterface.PlayerState;
					ServerPlayerState resultPlayerState = net.m_resultPlayerState;
					if (resultPlayerState != null)
					{
						resultPlayerState.CopyTo(playerState);
					}
					ServerCharacterState[] characterState = net.resultCharacterState;
					if (characterState != null)
					{
						playerState.SetCharacterState(characterState);
					}
					List<ServerChaoState> chaoState = net.resultChaoState;
					if (chaoState != null)
					{
						playerState.SetChaoState(chaoState);
					}
					ServerPlayCharacterState[] playCharacterState = net.resultPlayCharacterState;
					if (playCharacterState != null)
					{
						playerState.ClearPlayCharacterState();
						foreach (ServerPlayCharacterState playCharaState in playCharacterState)
						{
							if (playCharaState != null)
							{
								playerState.SetPlayCharacterState(playCharaState);
							}
						}
					}
					ServerMileageMapState interfaceMileageMapState = ServerInterface.MileageMapState;
					net.m_resultMileageMapState.CopyTo(interfaceMileageMapState);
					if (results.m_chaoEggPresent && RouletteManager.Instance != null)
					{
						RouletteManager.Instance.specialEgg++;
					}
					if (net.m_messageEntryList != null)
					{
						List<ServerMessageEntry> messageEntries = ServerInterface.MessageList;
						messageEntries.Clear();
						int resultMessageEntries = net.m_totalMessage;
						for (int index = 0; index < resultMessageEntries; index++)
						{
							ServerMessageEntry messageEntry = net.m_messageEntryList[index];
							messageEntries.Add(messageEntry);
						}
					}
					if (net.m_operatorMessageEntryList != null)
					{
						List<ServerOperatorMessageEntry> operatorMessageEntries = ServerInterface.OperatorMessageList;
						operatorMessageEntries.Clear();
						int resultOperatorMessageEntries = net.m_totalOperatorMessage;
						for (int index2 = 0; index2 < resultOperatorMessageEntries; index2++)
						{
							ServerOperatorMessageEntry messageEntry2 = net.m_operatorMessageEntryList[index2];
							operatorMessageEntries.Add(messageEntry2);
						}
					}
					if (net.m_resultEventIncentiveList != null)
					{
						EventUtility.SetEventIncentiveListToEventManager(net.m_resultEventIncentiveList);
					}
					if (net.m_resultEventState != null)
					{
						EventUtility.SetEventStateToEventManager(net.m_resultEventState);
					}
					List<ServerMileageIncentive> mileageIncentiveList = net.m_resultMileageIncentive;
					List<ServerItemState> dailyIncentiveList = net.m_resultDailyMissionIncentiveList;
					MsgPostGameResultsSucceed msg = new MsgPostGameResultsSucceed();
					msg.m_playerState = playerState;
					msg.m_mileageMapState = interfaceMileageMapState;
					msg.m_mileageIncentive = mileageIncentiveList;
					msg.m_dailyIncentive = dailyIncentiveList;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerPostGameResults_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerPostGameResults_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerPostGameResults_Failed");
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
