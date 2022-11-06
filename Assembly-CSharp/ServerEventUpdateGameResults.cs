using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020006F0 RID: 1776
public class ServerEventUpdateGameResults
{
	// Token: 0x06002FAB RID: 12203 RVA: 0x001138D8 File Offset: 0x00111AD8
	public static IEnumerator Process(ServerEventGameResults results, GameObject callbackObject)
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
				NetServerEventUpdateGameResults net = new NetServerEventUpdateGameResults(results);
				net.Request();
				monitor.StartMonitor(new ServerEventUpdateGameResultsRetry(results, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerPlayerState playerState = ServerInterface.PlayerState;
					ServerPlayerState resultPlayerState = net.PlayerState;
					if (resultPlayerState != null)
					{
						resultPlayerState.CopyTo(playerState);
					}
					ServerPlayCharacterState[] playCharacterState = net.PlayerCharacterState;
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
					ServerWheelOptions wheelOptions = ServerInterface.WheelOptions;
					ServerWheelOptions resultWheelOptions = net.WheelOptions;
					if (resultWheelOptions != null)
					{
						resultWheelOptions.CopyTo(wheelOptions);
					}
					List<ServerMessageEntry> messageEntries = net.MessageEntryList;
					if (messageEntries != null)
					{
						List<ServerMessageEntry> serverMessageEntries = ServerInterface.MessageList;
						serverMessageEntries.Clear();
						for (int index = 0; index < messageEntries.Count; index++)
						{
							ServerMessageEntry messageEntry = messageEntries[index];
							serverMessageEntries.Add(messageEntry);
						}
					}
					List<ServerOperatorMessageEntry> operatorMessageEntries = net.OperatorMessageEntryList;
					if (operatorMessageEntries != null)
					{
						List<ServerOperatorMessageEntry> serverOperatorMessageEntries = ServerInterface.OperatorMessageList;
						serverOperatorMessageEntries.Clear();
						for (int index2 = 0; index2 < operatorMessageEntries.Count; index2++)
						{
							ServerOperatorMessageEntry messageEntry2 = operatorMessageEntries[index2];
							serverOperatorMessageEntries.Add(messageEntry2);
						}
					}
					EventUtility.SetEventIncentiveListToEventManager(net.EventIncentiveList);
					EventUtility.SetEventStateToEventManager(net.EventState);
					EventManager eventManager = EventManager.Instance;
					if (eventManager != null)
					{
						eventManager.RaidBossBonus = net.RaidBossBonus;
					}
					MsgEventUpdateGameResultsSucceed msg = new MsgEventUpdateGameResultsSucceed();
					msg.m_bonus = net.RaidBossBonus;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerUpdateGameResults_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerUpdateGameResults_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
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
