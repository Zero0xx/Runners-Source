using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000763 RID: 1891
public class ServerGetVariousParameter
{
	// Token: 0x06003291 RID: 12945 RVA: 0x00119868 File Offset: 0x00117A68
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
				NetServerGetVariousParameter net = new NetServerGetVariousParameter();
				net.Request();
				monitor.StartMonitor(new ServerGetVariousParameterRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerInterface.SettingState.m_energyRefreshTime = (long)net.resultEnergyRecveryTime;
					ServerInterface.SettingState.m_energyRecoveryMax = net.resultEnergyRecoveryMax;
					ServerInterface.SettingState.m_onePlayCmCount = net.resultOnePlayCmCount;
					ServerInterface.SettingState.m_onePlayContinueCount = net.resultOnePlayContinueCount;
					ServerInterface.SettingState.m_cmSkipCount = net.resultCmSkipCount;
					ServerInterface.SettingState.m_isPurchased = net.resultIsPurchased;
					MsgGetVariousParameterSucceed msg = new MsgGetVariousParameterSucceed();
					msg.m_energyRefreshTime = net.resultEnergyRecveryTime;
					msg.m_energyRecoveryMax = net.resultEnergyRecoveryMax;
					msg.m_onePlayCmCount = net.resultOnePlayCmCount;
					msg.m_onePlayContinueCount = net.resultOnePlayContinueCount;
					msg.m_cmSkipCount = net.resultCmSkipCount;
					msg.m_isPurchased = net.resultIsPurchased;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetVariousParameter_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetVariousParameter_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetVariousParameter_Failed");
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
