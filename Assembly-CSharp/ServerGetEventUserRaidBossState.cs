using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020006FE RID: 1790
public class ServerGetEventUserRaidBossState
{
	// Token: 0x06002FC7 RID: 12231 RVA: 0x00113C78 File Offset: 0x00111E78
	public static IEnumerator Process(int eventId, GameObject callbackObject)
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
				NetServerGetEventUserRaidBossState net = new NetServerGetEventUserRaidBossState(eventId);
				net.Request();
				monitor.StartMonitor(new ServerGetEventUserRaidBossStateRetry(eventId, callbackObject), -1f, HudNetworkConnect.DisplayType.ONLY_ICON);
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgGetEventUserRaidBossStateSucceed msg = new MsgGetEventUserRaidBossStateSucceed();
					if (EventManager.Instance != null)
					{
						EventManager.Instance.SynchServerEventUserRaidBossState(net.UserRaidBossState);
					}
					GeneralUtil.SetItemCount(ServerItem.Id.RAIDRING, (long)net.UserRaidBossState.NumRaidbossRings);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetEventUserRaidBossState_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetEventUserRaidBossState_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetEventUserRaidBossState_Failed");
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

	// Token: 0x04002AAB RID: 10923
	private const string SUCCEEDED_FUNCTION_NAME = "ServerGetEventUserRaidBossState_Succeeded";

	// Token: 0x04002AAC RID: 10924
	private const string FAILED_FUNCTION_NAME = "ServerGetEventUserRaidBossState_Failed";
}
