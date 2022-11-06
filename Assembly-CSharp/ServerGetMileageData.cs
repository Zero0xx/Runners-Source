using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000737 RID: 1847
public class ServerGetMileageData
{
	// Token: 0x0600313E RID: 12606 RVA: 0x00116CEC File Offset: 0x00114EEC
	public static IEnumerator Process(string[] distanceFriendList, GameObject callbackObject)
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
				NetServerGetMileageData net = new NetServerGetMileageData(distanceFriendList);
				net.Request();
				monitor.StartMonitor(new ServerGetMileageDataRetry(distanceFriendList, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerMileageMapState mileageMapState = ServerInterface.MileageMapState;
					ServerMileageMapState resultMileageMapState = ServerGetMileageData.MakeResultMileageState(net);
					if (resultMileageMapState != null && mileageMapState != null)
					{
						resultMileageMapState.CopyTo(mileageMapState);
					}
					List<ServerMileageFriendEntry> mileageFriendList = ServerInterface.MileageFriendList;
					List<ServerMileageFriendEntry> resultMileageFriendList = net.m_resultMileageFriendList;
					mileageFriendList.Clear();
					for (int index = 0; index < resultMileageFriendList.Count; index++)
					{
						ServerMileageFriendEntry e = new ServerMileageFriendEntry();
						resultMileageFriendList[index].CopyTo(e);
						mileageFriendList.Add(e);
					}
					MsgGetPlayerStateSucceed msg = new MsgGetPlayerStateSucceed();
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetMileageData_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetMileageData_Succeeded", null, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetMileageData_Failed");
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

	// Token: 0x0600313F RID: 12607 RVA: 0x00116D1C File Offset: 0x00114F1C
	private static ServerMileageMapState MakeResultMileageState(NetServerGetMileageData net)
	{
		ServerMileageMapState serverMileageMapState = new ServerMileageMapState();
		net.resultMileageMapState.CopyTo(serverMileageMapState);
		return serverMileageMapState;
	}
}
