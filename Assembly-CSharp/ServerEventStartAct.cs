using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020006EE RID: 1774
public class ServerEventStartAct
{
	// Token: 0x06002FA7 RID: 12199 RVA: 0x00113820 File Offset: 0x00111A20
	public static IEnumerator Process(int eventId, int energyExpend, long raidBossId, List<ItemType> modifiersItem, List<BoostItemType> modifiersBoostItem, GameObject callbackObject)
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
				NetServerEventStartAct net = new NetServerEventStartAct(eventId, energyExpend, raidBossId, modifiersItem, modifiersBoostItem);
				net.Request();
				monitor.StartMonitor(new ServerEventStartActRetry(eventId, energyExpend, raidBossId, modifiersItem, modifiersBoostItem, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerPlayerState playerState = ServerInterface.PlayerState;
					ServerPlayerState resultPlayerState = net.resultPlayerState;
					resultPlayerState.CopyTo(playerState);
					if (EventManager.Instance != null)
					{
						ServerEventUserRaidBossState userRaidBossState = net.userRaidBossState;
						EventManager.Instance.SynchServerEventUserRaidBossState(userRaidBossState);
					}
					GeneralUtil.SetItemCount(ServerItem.Id.RAIDRING, (long)net.userRaidBossState.NumRaidbossRings);
					MsgEventActStartSucceed msg = new MsgEventActStartSucceed();
					msg.m_playerState = playerState;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerEventStartAct_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerEventStartAct_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerEventStartAct_Failed");
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
