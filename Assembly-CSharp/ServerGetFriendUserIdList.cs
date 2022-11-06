using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000709 RID: 1801
public class ServerGetFriendUserIdList
{
	// Token: 0x06003012 RID: 12306 RVA: 0x00114330 File Offset: 0x00112530
	public static IEnumerator Process(List<string> friendFBIdList, GameObject callbackObject)
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
				NetServerGetFriendUserIdList net = new NetServerGetFriendUserIdList(friendFBIdList);
				net.Request();
				monitor.StartMonitor(new ServerGetFriendUserIdListRetry(friendFBIdList, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					List<ServerUserTransformData> transformDataList = ServerInterface.UserTransformDataList;
					transformDataList.Clear();
					if (net.resultTransformDataList != null)
					{
						foreach (ServerUserTransformData data in net.resultTransformDataList)
						{
							transformDataList.Add(data);
						}
					}
					MsgGetFriendUserIdListSucceed msg = new MsgGetFriendUserIdListSucceed();
					msg.m_transformDataList = net.resultTransformDataList;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetFriendUserIdList_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetFriendUserIdList_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetFriendUserIdList_Failed");
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
