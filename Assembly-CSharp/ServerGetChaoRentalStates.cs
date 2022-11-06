using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020006A4 RID: 1700
public class ServerGetChaoRentalStates
{
	// Token: 0x06002DC4 RID: 11716 RVA: 0x00110530 File Offset: 0x0010E730
	public static IEnumerator Process(string[] friendId, GameObject callbackObject)
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
				NetServerGetRentalState net = new NetServerGetRentalState(friendId);
				net.Request();
				monitor.StartMonitor(new ServerGetChaoRentalStatesRetry(friendId, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					int resultChaoRentalStates = net.resultStates;
					List<ServerChaoRentalState> resultChaoRentalStateList = new List<ServerChaoRentalState>(resultChaoRentalStates);
					for (int i = 0; i < resultChaoRentalStates; i++)
					{
						ServerChaoRentalState chaoRentalState = net.GetResultChaoRentalState(i);
						resultChaoRentalStateList.Add(chaoRentalState);
					}
					MsgGetFriendChaoStateSucceed msg = new MsgGetFriendChaoStateSucceed();
					msg.m_chaoRentalStates = resultChaoRentalStateList;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetChaoRentalStates_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetChaoRentalStates_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetChaoRentalStates_Failed");
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

	// Token: 0x040029E8 RID: 10728
	private const string SuccessEvent = "ServerGetChaoRentalStates_Succeeded";

	// Token: 0x040029E9 RID: 10729
	private const string FailEvent = "ServerGetChaoRentalStates_Failed";
}
