using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020006A0 RID: 1696
public class ServerCommitChaoWheelSpin
{
	// Token: 0x06002DBC RID: 11708 RVA: 0x0011041C File Offset: 0x0010E61C
	public static IEnumerator Process(int count, GameObject callbackObject)
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
				NetServerCommitChaoWheelSpin net = new NetServerCommitChaoWheelSpin(count);
				net.Request();
				monitor.StartMonitor(new ServerCommitChaoWheelSpinRetry(count, callbackObject), -1f, HudNetworkConnect.DisplayType.ONLY_ICON);
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerPlayerState playerState = ServerInterface.PlayerState;
					ServerPlayerState resultPlayerState = net.resultPlayerState;
					if (resultPlayerState != null)
					{
						resultPlayerState.CopyTo(playerState);
					}
					ServerCharacterState[] resultCharacterState = net.resultCharacterState;
					if (resultCharacterState != null)
					{
						playerState.SetCharacterState(resultCharacterState);
					}
					List<ServerChaoState> resultChaoState = net.resultChaoState;
					if (resultChaoState != null)
					{
						playerState.SetChaoState(resultChaoState);
					}
					ServerChaoWheelOptions chaoWheelOptions = ServerInterface.ChaoWheelOptions;
					ServerChaoWheelOptions resultChaoWheelOptions = net.resultChaoWheelOptions;
					resultChaoWheelOptions.CopyTo(chaoWheelOptions);
					ServerSpinResultGeneral resultSpinResultGeneral = null;
					if (net.resultSpinResultGeneral != null)
					{
						resultSpinResultGeneral = new ServerSpinResultGeneral();
						net.resultSpinResultGeneral.CopyTo(resultSpinResultGeneral);
					}
					MsgCommitChaoWheelSpicSucceed msg = new MsgCommitChaoWheelSpicSucceed();
					msg.m_playerState = playerState;
					msg.m_chaoWheelOptions = chaoWheelOptions;
					msg.m_resultSpinResultGeneral = resultSpinResultGeneral;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerCommitChaoWheelSpin_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerCommitChaoWheelSpin_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerCommitChaoWheelSpin_Failed");
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
