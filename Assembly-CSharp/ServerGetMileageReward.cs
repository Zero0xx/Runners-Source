using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000739 RID: 1849
public class ServerGetMileageReward
{
	// Token: 0x06003143 RID: 12611 RVA: 0x00116D94 File Offset: 0x00114F94
	public static IEnumerator Process(int episode, int chapter, GameObject callbackObject)
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
				NetServerGetMileageReward net = new NetServerGetMileageReward(episode, chapter);
				net.Request();
				monitor.StartMonitor(new ServerGetMileageRewardRetry(episode, chapter, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					List<ServerMileageReward> mileageRewardList = ServerInterface.MileageRewardList;
					List<ServerMileageReward> allList = new List<ServerMileageReward>();
					foreach (ServerMileageReward reward in mileageRewardList)
					{
						allList.Add(reward);
					}
					if (net.m_rewardList != null)
					{
						mileageRewardList.Clear();
						foreach (ServerMileageReward reward2 in net.m_rewardList)
						{
							if (!mileageRewardList.Contains(reward2))
							{
								mileageRewardList.Add(reward2);
							}
							if (!allList.Contains(reward2))
							{
								allList.Add(reward2);
							}
						}
					}
					MsgGetMileageRewardSucceed msg = new MsgGetMileageRewardSucceed();
					msg.m_mileageRewardList = allList;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetMileageReward_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetMileageReward_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetMileageReward_Failed");
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
