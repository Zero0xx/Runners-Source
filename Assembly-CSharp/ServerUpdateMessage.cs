using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x0200077A RID: 1914
public class ServerUpdateMessage
{
	// Token: 0x06003300 RID: 13056 RVA: 0x0011A7E4 File Offset: 0x001189E4
	public static IEnumerator Process(List<int> messageIdList, List<int> operatorMessageIdList, GameObject callbackObject)
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
				NetServerUpdateMessage net = new NetServerUpdateMessage(messageIdList, operatorMessageIdList);
				net.Request();
				monitor.StartMonitor(new ServerUpdateMessageRetry(messageIdList, operatorMessageIdList, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerPlayerState playerState = ServerInterface.PlayerState;
					ServerPlayerState resultPlayerState = net.resultPlayerState;
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
					int chaoRoulette = 0;
					int itemRoulette = 0;
					int presentCount = net.resultPresentStates;
					List<ServerPresentState> resultPresentList = new List<ServerPresentState>(presentCount);
					for (int i = 0; i < presentCount; i++)
					{
						ServerPresentState presentState = net.GetResultPresentState(i);
						resultPresentList.Add(presentState);
						if (presentState.m_itemId == 230000)
						{
							chaoRoulette += presentState.m_numItem;
						}
						else if (presentState.m_itemId == 240000)
						{
							itemRoulette += presentState.m_numItem;
						}
					}
					ServerInterface.ChaoWheelOptions.NumRouletteToken += chaoRoulette;
					ServerInterface.WheelOptions.m_numRemaining += itemRoulette;
					int missingMessageCount = net.resultMissingMessages;
					List<int> resultMissingMessageIdList = new List<int>(missingMessageCount);
					for (int j = 0; j < missingMessageCount; j++)
					{
						int missingId = net.GetResultMissingMessageId(j);
						resultMissingMessageIdList.Add(missingId);
					}
					int missingOperatorMessageCount = net.resultMissingOperatorMessages;
					List<int> resultMissingOperatorMessageIdList = new List<int>(missingOperatorMessageCount);
					for (int k = 0; k < missingOperatorMessageCount; k++)
					{
						int missingId2 = net.GetResultMissingOperatorMessageId(k);
						resultMissingOperatorMessageIdList.Add(missingId2);
					}
					if (messageIdList == null)
					{
						ServerUpdateMessage.UpdateMessageList(ServerInterface.MessageList, resultMissingMessageIdList);
					}
					else
					{
						ServerUpdateMessage.UpdateMessageList(ServerInterface.MessageList, messageIdList, resultMissingMessageIdList);
					}
					if (operatorMessageIdList == null)
					{
						ServerUpdateMessage.UpdateOperatorMessageList(ServerInterface.OperatorMessageList, resultMissingOperatorMessageIdList);
					}
					else
					{
						ServerUpdateMessage.UpdateOperatorMessageList(ServerInterface.OperatorMessageList, operatorMessageIdList, resultMissingOperatorMessageIdList);
					}
					MsgUpdateMesseageSucceed msg = new MsgUpdateMesseageSucceed();
					msg.m_presentStateList = resultPresentList;
					msg.m_notRecvMessageList = resultMissingMessageIdList;
					msg.m_notRecvOperatorMessageList = resultMissingOperatorMessageIdList;
					GeneralUtil.SetPresentItemCount(msg);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerUpdateMessage_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerUpdateMessage_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerUpdateMessage_Failed");
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

	// Token: 0x06003301 RID: 13057 RVA: 0x0011A824 File Offset: 0x00118A24
	private static void UpdateMessageList(List<ServerMessageEntry> msgList, List<int> missingList)
	{
		if (msgList != null && missingList != null)
		{
			List<ServerMessageEntry> list = new List<ServerMessageEntry>();
			foreach (int num in missingList)
			{
				for (int i = 0; i < msgList.Count; i++)
				{
					if (num == msgList[i].m_messageId)
					{
						ServerMessageEntry serverMessageEntry = new ServerMessageEntry();
						msgList[i].CopyTo(serverMessageEntry);
						list.Add(serverMessageEntry);
						break;
					}
				}
			}
			msgList.Clear();
			for (int j = 0; j < list.Count; j++)
			{
				msgList.Add(list[j]);
			}
		}
	}

	// Token: 0x06003302 RID: 13058 RVA: 0x0011A90C File Offset: 0x00118B0C
	private static void UpdateOperatorMessageList(List<ServerOperatorMessageEntry> msgList, List<int> missingList)
	{
		if (msgList != null && missingList != null)
		{
			List<ServerOperatorMessageEntry> list = new List<ServerOperatorMessageEntry>();
			foreach (int num in missingList)
			{
				for (int i = 0; i < msgList.Count; i++)
				{
					if (num == msgList[i].m_messageId)
					{
						ServerOperatorMessageEntry serverOperatorMessageEntry = new ServerOperatorMessageEntry();
						msgList[i].CopyTo(serverOperatorMessageEntry);
						list.Add(serverOperatorMessageEntry);
						break;
					}
				}
			}
			msgList.Clear();
			for (int j = 0; j < list.Count; j++)
			{
				msgList.Add(list[j]);
			}
		}
	}

	// Token: 0x06003303 RID: 13059 RVA: 0x0011A9F4 File Offset: 0x00118BF4
	private static void UpdateMessageList(List<ServerMessageEntry> msgList, List<int> requestList, List<int> missingList)
	{
		if (msgList != null && requestList != null && missingList != null)
		{
			foreach (int num in missingList)
			{
				for (int i = 0; i < requestList.Count; i++)
				{
					if (num == requestList[i])
					{
						requestList.Remove(requestList[i]);
						break;
					}
				}
			}
			foreach (int num2 in requestList)
			{
				for (int j = 0; j < msgList.Count; j++)
				{
					if (num2 == msgList[j].m_messageId)
					{
						msgList.Remove(msgList[j]);
						break;
					}
				}
			}
		}
	}

	// Token: 0x06003304 RID: 13060 RVA: 0x0011AB24 File Offset: 0x00118D24
	private static void UpdateOperatorMessageList(List<ServerOperatorMessageEntry> msgList, List<int> requestList, List<int> missingList)
	{
		if (msgList != null && requestList != null && missingList != null)
		{
			foreach (int num in missingList)
			{
				for (int i = 0; i < requestList.Count; i++)
				{
					if (num == requestList[i])
					{
						requestList.Remove(requestList[i]);
						break;
					}
				}
			}
			foreach (int num2 in requestList)
			{
				for (int j = 0; j < msgList.Count; j++)
				{
					if (num2 == msgList[j].m_messageId)
					{
						msgList.Remove(msgList[j]);
						break;
					}
				}
			}
		}
	}

	// Token: 0x04002BA4 RID: 11172
	private const string SuccessEvent = "ServerUpdateMessage_Succeeded";

	// Token: 0x04002BA5 RID: 11173
	private const string FailEvent = "ServerUpdateMessage_Failed";
}
