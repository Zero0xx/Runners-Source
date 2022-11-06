using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000769 RID: 1897
public class ServerLoginBonus
{
	// Token: 0x0600329E RID: 12958 RVA: 0x001199EC File Offset: 0x00117BEC
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
				NetServerLoginBonus net = new NetServerLoginBonus();
				net.Request();
				monitor.StartMonitor(new ServerLoginBonusRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerLoginBonusData loginBonusData = ServerInterface.LoginBonusData;
					net.loginBonusState.CopyTo(loginBonusData.m_loginBonusState);
					loginBonusData.m_startTime = net.startTime;
					loginBonusData.m_endTime = net.endTime;
					foreach (ServerLoginBonusReward data in net.loginBonusRewardList)
					{
						loginBonusData.m_loginBonusRewardList.Add(data);
					}
					foreach (ServerLoginBonusReward data2 in net.firstLoginBonusRewardList)
					{
						loginBonusData.m_firstLoginBonusRewardList.Add(data2);
					}
					loginBonusData.m_rewardId = net.rewardId;
					loginBonusData.m_rewardDays = net.rewardDays;
					loginBonusData.m_firstRewardDays = net.firstRewardDays;
					MsgLoginBonusSucceed msg = new MsgLoginBonusSucceed();
					msg.m_loginBonusData = loginBonusData;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerLoginBonus_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerLoginBonus_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerLoginBonus_Failed");
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
