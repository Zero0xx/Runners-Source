using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x0200079E RID: 1950
public class ServerCommitWheelSpinGeneral
{
	// Token: 0x060033BC RID: 13244 RVA: 0x0011C158 File Offset: 0x0011A358
	public static IEnumerator Process(int eventId, int spinId, int spinCostItemId, int spinNum, GameObject callbackObject)
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
				NetServerCommitWheelSpinGeneral net = new NetServerCommitWheelSpinGeneral(eventId, spinId, spinCostItemId, spinNum);
				net.Request();
				monitor.StartMonitor(new ServerCommitWheelSpinGeneralRetry(eventId, spinId, spinCostItemId, spinNum, callbackObject), -1f, HudNetworkConnect.DisplayType.ONLY_ICON);
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
					MsgCommitWheelSpinGeneralSucceed msg = new MsgCommitWheelSpinGeneralSucceed();
					msg.m_playerState = playerState;
					msg.m_wheelOptionsGeneral = net.resultWheelOptionsGen;
					msg.m_resultSpinResultGeneral = net.resultWheelResultGen;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerCommitWheelSpinGeneral_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerCommitWheelSpinGeneral_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (net.resultStCd == ServerInterface.StatusCode.RouletteBoardReset)
					{
						if (callbackObject != null)
						{
							callbackObject.SendMessage("ServerCommitWheelSpinGeneral_Failed", msg2, SendMessageOptions.DontRequireReceiver);
						}
					}
					else if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerCommitWheelSpinGeneral_Failed");
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

	// Token: 0x04002BE9 RID: 11241
	private const string SuccessEvent = "ServerCommitWheelSpinGeneral_Succeeded";

	// Token: 0x04002BEA RID: 11242
	private const string FailEvent = "ServerCommitWheelSpinGeneral_Failed";
}
