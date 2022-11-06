using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020007C1 RID: 1985
public class ServerSetBirthday
{
	// Token: 0x0600344E RID: 13390 RVA: 0x0011CF38 File Offset: 0x0011B138
	public static IEnumerator Process(string birthday, GameObject callbackObject)
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
				NetServerSetBirthday net = new NetServerSetBirthday(birthday);
				net.Request();
				monitor.StartMonitor(new ServerSetBirthdayRetry(birthday, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerSettingState settingState = ServerInterface.SettingState;
					if (settingState != null && string.IsNullOrEmpty(settingState.m_birthday))
					{
						settingState.m_birthday = birthday;
					}
					MsgSetBirthday msg = new MsgSetBirthday();
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerSetBirthday_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerSetBirthday_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerSetBirthday_Failed");
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
