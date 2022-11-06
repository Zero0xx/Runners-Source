using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006C3 RID: 1731
public class ServerGetDailyBattleData
{
	// Token: 0x06002E7F RID: 11903 RVA: 0x00111B30 File Offset: 0x0010FD30
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
				NetServerGetDailyBattleData net = new NetServerGetDailyBattleData();
				net.Request();
				monitor.StartMonitor(new ServerGetDailyBattleDataRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgGetDailyBattleDataSucceed msg = new MsgGetDailyBattleDataSucceed();
					net.battleDataPair.CopyTo(msg.battleDataPair);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetDailyBattleData_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetDailyBattleData_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetDailyBattleData_Failed", msg2, SendMessageOptions.DontRequireReceiver);
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

	// Token: 0x04002A30 RID: 10800
	private const string SuccessEvent = "ServerGetDailyBattleData_Succeeded";

	// Token: 0x04002A31 RID: 10801
	private const string FailEvent = "ServerGetDailyBattleData_Failed";
}
