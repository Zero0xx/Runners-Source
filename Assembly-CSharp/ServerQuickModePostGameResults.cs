﻿using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000723 RID: 1827
public class ServerQuickModePostGameResults
{
	// Token: 0x0600310A RID: 12554 RVA: 0x00116560 File Offset: 0x00114760
	public static IEnumerator Process(ServerQuickModeGameResults results, GameObject callbackObject)
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
				NetServerQuickModePostGameResults net = new NetServerQuickModePostGameResults(results);
				net.Request();
				monitor.StartMonitor(new ServerQuickModePostGameResultsRetry(results, callbackObject));
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
					ServerCharacterState[] characterState = net.m_resultCharacterState;
					if (characterState != null)
					{
						playerState.SetCharacterState(characterState);
					}
					List<ServerChaoState> chaoState = net.m_resultChaoState;
					if (chaoState != null)
					{
						playerState.SetChaoState(chaoState);
					}
					ServerPlayCharacterState[] playCharacterState = net.m_resultPlayCharacterState;
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
					if (net.m_messageEntryList != null)
					{
						List<ServerMessageEntry> messageEntries = ServerInterface.MessageList;
						messageEntries.Clear();
						int resultMessageEntries = net.totalMessage;
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
						int resultOperatorMessageEntries = net.totalOperatorMessage;
						for (int index2 = 0; index2 < resultOperatorMessageEntries; index2++)
						{
							ServerOperatorMessageEntry messageEntry2 = net.m_operatorMessageEntryList[index2];
							operatorMessageEntries.Add(messageEntry2);
						}
					}
					List<ServerItemState> dailyIncentiveList = net.m_dailyMissionIncentiveList;
					MsgQuickModePostGameResultsSucceed msg = new MsgQuickModePostGameResultsSucceed();
					msg.m_playerState = playerState;
					msg.m_dailyIncentive = dailyIncentiveList;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerQuickModePostGameResults_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerQuickModePostGameResults_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerQuickModePostGameResults_Failed");
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
