using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x0200074C RID: 1868
public class ServerGetLeagueOperatorData
{
	// Token: 0x060031BF RID: 12735 RVA: 0x00117C70 File Offset: 0x00115E70
	public static IEnumerator Process(int mode, GameObject callbackObject)
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
				NetServerGetLeagueOperatorData net = new NetServerGetLeagueOperatorData(mode);
				net.Request();
				monitor.StartMonitor(new ServerGetLeagueOperatorDataRetry(mode, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgGetLeagueOperatorDataSucceed msg = new MsgGetLeagueOperatorDataSucceed();
					msg.m_leagueOperatorData = net.leagueOperatorData;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetLeagueOperatorData_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetLeagueOperatorData_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetLeagueOperatorData_Failed");
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
