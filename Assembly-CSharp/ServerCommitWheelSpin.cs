using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x0200079C RID: 1948
public class ServerCommitWheelSpin
{
	// Token: 0x060033B8 RID: 13240 RVA: 0x0011C0B4 File Offset: 0x0011A2B4
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
				NetServerCommitWheelSpin net = new NetServerCommitWheelSpin(count);
				net.Request();
				monitor.StartMonitor(new ServerCommitWheelSpinRetry(count, callbackObject), -1f, HudNetworkConnect.DisplayType.ONLY_ICON);
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerPlayerState playerState = ServerInterface.PlayerState;
					ServerPlayerState resultPlayerState = net.resultPlayerState;
					ServerSpinResultGeneral resultSpinResultGeneral = null;
					if (net.resultSpinResultGeneral != null)
					{
						resultSpinResultGeneral = new ServerSpinResultGeneral();
						net.resultSpinResultGeneral.CopyTo(resultSpinResultGeneral);
					}
					if (resultPlayerState != null)
					{
						resultPlayerState.CopyTo(playerState);
					}
					ServerCharacterState[] characterState = net.resultCharacterState;
					if (characterState != null)
					{
						playerState.SetCharacterState(characterState);
					}
					List<ServerChaoState> chaoState = net.resultChaoState;
					if (chaoState != null)
					{
						playerState.SetChaoState(chaoState);
					}
					ServerWheelOptions wheelOptions = ServerInterface.WheelOptions;
					ServerWheelOptions resultWheelOptions = net.resultWheelOptions;
					resultWheelOptions.CopyTo(wheelOptions);
					MsgCommitWheelSpinSucceed msg = new MsgCommitWheelSpinSucceed();
					msg.m_playerState = playerState;
					msg.m_wheelOptions = wheelOptions;
					msg.m_resultSpinResultGeneral = resultSpinResultGeneral;
					if (msg.m_resultSpinResultGeneral == null && RouletteManager.Instance != null)
					{
						ServerWheelOptionsData currentWheelData = RouletteManager.Instance.GetRouletteDataOrg(RouletteCategory.ITEM);
						if (currentWheelData != null && currentWheelData.GetOrgRankupData() != null)
						{
							ServerSpinResultGeneral res = new ServerSpinResultGeneral(wheelOptions, currentWheelData.GetOrgRankupData());
							msg.m_resultSpinResultGeneral = res;
						}
					}
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerCommitWheelSpin_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerCommitWheelSpin_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (net.resultStCd == ServerInterface.StatusCode.RouletteBoardReset)
					{
						if (callbackObject != null)
						{
							callbackObject.SendMessage("ServerCommitWheelSpin_Failed", msg2, SendMessageOptions.DontRequireReceiver);
						}
					}
					else if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerCommitWheelSpin_Failed");
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

	// Token: 0x04002BE3 RID: 11235
	private const string SuccessEvent = "ServerCommitWheelSpin_Succeeded";

	// Token: 0x04002BE4 RID: 11236
	private const string FailEvent = "ServerCommitWheelSpin_Failed";
}
