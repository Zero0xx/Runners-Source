using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000725 RID: 1829
public class ServerQuickModeStartAct
{
	// Token: 0x0600310E RID: 12558 RVA: 0x001165F8 File Offset: 0x001147F8
	public static IEnumerator Process(List<ItemType> modifiersItem, List<BoostItemType> modifiersBoostItem, bool tutorial, GameObject callbackObject)
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
				float dummyTime = Time.realtimeSinceStartup;
				do
				{
					yield return null;
				}
				while (Time.realtimeSinceStartup - dummyTime < 1f);
				NetServerQuickModeStartAct net = new NetServerQuickModeStartAct(modifiersItem, modifiersBoostItem, tutorial);
				net.Request();
				monitor.StartMonitor(new ServerQuickModeStartActRetry(modifiersItem, modifiersBoostItem, tutorial, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					SaveDataUtil.SetBeforeDailyMissionSaveData(net.resultPlayerState);
					ServerPlayerState playerState = ServerInterface.PlayerState;
					ServerPlayerState resultPlayerState = net.resultPlayerState;
					resultPlayerState.CopyTo(playerState);
					MsgQuickModeActStartSucceed msg = new MsgQuickModeActStartSucceed();
					msg.m_playerState = playerState;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerQuickModeStartAct_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerQuickModeStartAct_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerQuickModeStartAct_Failed");
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
