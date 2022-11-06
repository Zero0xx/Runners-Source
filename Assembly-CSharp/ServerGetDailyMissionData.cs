using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000731 RID: 1841
public class ServerGetDailyMissionData
{
	// Token: 0x06003126 RID: 12582 RVA: 0x001168AC File Offset: 0x00114AAC
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
				NetServerGetDailyMissionData net = new NetServerGetDailyMissionData();
				net.Request();
				monitor.StartMonitor(new ServerGetDailyMissionDataRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					int numContinue = net.resultNumContinue;
					int numIncentive = net.resultIncentives;
					ServerDailyChallengeState dailyIncentive = ServerInterface.DailyChallengeState;
					dailyIncentive.m_incentiveList.Clear();
					for (int i = 0; i < numIncentive; i++)
					{
						ServerDailyChallengeIncentive incentive = net.GetResultDailyMissionIncentive(i);
						dailyIncentive.m_incentiveList.Add(incentive);
					}
					dailyIncentive.m_numIncentiveCont = numContinue;
					dailyIncentive.m_numDailyChalDay = net.resultNumDailyChalDay;
					dailyIncentive.m_maxDailyChalDay = net.resultMaxDailyChalDay;
					dailyIncentive.m_maxIncentive = net.resultMaxIncentive;
					dailyIncentive.m_chalEndTime = net.resultChalEndTime;
					NetUtil.SyncSaveDataAndDailyMission(dailyIncentive);
					MsgGetDailyMissionDataSucceed msg = new MsgGetDailyMissionDataSucceed();
					msg.m_dailyChallengeState = dailyIncentive;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetDailyMissionData_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetDailyMissionData_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetDailyMissionData_Failed");
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
