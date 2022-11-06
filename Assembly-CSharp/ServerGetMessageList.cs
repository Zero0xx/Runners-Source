using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000776 RID: 1910
public class ServerGetMessageList
{
	// Token: 0x060032F8 RID: 13048 RVA: 0x0011A6EC File Offset: 0x001188EC
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
				NetServerGetMessageList net = new NetServerGetMessageList();
				net.Request();
				monitor.StartMonitor(new ServerGetMessageListRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					List<ServerMessageEntry> messageEntries = ServerInterface.MessageList;
					messageEntries.Clear();
					List<ServerOperatorMessageEntry> operatorMessageEntries = ServerInterface.OperatorMessageList;
					operatorMessageEntries.Clear();
					int resultMessageEntries = net.resultMessages;
					for (int index = 0; index < resultMessageEntries; index++)
					{
						ServerMessageEntry messageEntry = net.GetResultMessageEntry(index);
						messageEntries.Add(messageEntry);
					}
					int resultOperatorMessageEntries = net.resultOperatorMessages;
					for (int index2 = 0; index2 < resultOperatorMessageEntries; index2++)
					{
						ServerOperatorMessageEntry messageEntry2 = net.GetResultOperatorMessageEntry(index2);
						operatorMessageEntries.Add(messageEntry2);
					}
					MsgGetMessageListSucceed msg = new MsgGetMessageListSucceed();
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetMessageList_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetMessageList_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetMessageList_Failed");
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

	// Token: 0x04002B9D RID: 11165
	private const string SuccessEvent = "ServerGetMessageList_Succeeded";

	// Token: 0x04002B9E RID: 11166
	private const string FailEvent = "ServerGetMessageList_Failed";
}
