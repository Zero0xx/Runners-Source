using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006C7 RID: 1735
public class ServerGetDailyBattleStatus
{
	// Token: 0x06002E87 RID: 11911 RVA: 0x00111C10 File Offset: 0x0010FE10
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
				NetServerGetDailyBattleStatus net = new NetServerGetDailyBattleStatus();
				net.Request();
				monitor.StartMonitor(new ServerGetDailyBattleStatusRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgGetDailyBattleStatusSucceed msg = new MsgGetDailyBattleStatusSucceed();
					net.battleStatus.CopyTo(msg.battleStatus);
					msg.endTime = net.endTime;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetDailyBattleStatus_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetDailyBattleStatus_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetDailyBattleStatus_Failed", msg2, SendMessageOptions.DontRequireReceiver);
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

	// Token: 0x04002A35 RID: 10805
	private const string SuccessEvent = "ServerGetDailyBattleStatus_Succeeded";

	// Token: 0x04002A36 RID: 10806
	private const string FailEvent = "ServerGetDailyBattleStatus_Failed";
}
