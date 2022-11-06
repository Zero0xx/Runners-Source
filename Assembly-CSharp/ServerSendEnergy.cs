using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000778 RID: 1912
public class ServerSendEnergy
{
	// Token: 0x060032FC RID: 13052 RVA: 0x0011A75C File Offset: 0x0011895C
	public static IEnumerator Process(string friendId, GameObject callbackObject)
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
				NetServerSendEnergy net = new NetServerSendEnergy(friendId);
				net.Request();
				monitor.StartMonitor(new ServerSendEnergyRetry(friendId, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgSendEnergySucceed msg = new MsgSendEnergySucceed();
					msg.m_expireTime = net.resultExpireTime;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerSendEnergy_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerSendEnergy_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerSendEnergy_Failed");
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

	// Token: 0x04002BA0 RID: 11168
	private const string SuccessEvent = "ServerSendEnergy_Succeeded";

	// Token: 0x04002BA1 RID: 11169
	private const string FailEvent = "ServerSendEnergy_Failed";
}
