using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020007BB RID: 1979
public class ServerPurchase
{
	// Token: 0x06003442 RID: 13378 RVA: 0x0011CDA8 File Offset: 0x0011AFA8
	public static IEnumerator Process(bool purchaseResult, GameObject callbackObject)
	{
		NetServerPurchase net = new NetServerPurchase(purchaseResult);
		net.Request();
		while (net.IsExecuting())
		{
			yield return null;
		}
		if (net.IsSucceeded())
		{
			ServerPlayerState playerState = ServerInterface.PlayerState;
			ServerPlayerState resultPlayerState = net.resultPlayerState;
			resultPlayerState.CopyTo(playerState);
			if (callbackObject != null)
			{
				callbackObject.SendMessage("ServerPurchase_Succeeded", new MsgGetPlayerStateSucceed
				{
					m_playerState = playerState
				}, SendMessageOptions.DontRequireReceiver);
			}
		}
		else if (callbackObject != null)
		{
			callbackObject.SendMessage("ServerPurchase_Failed", new MsgServerConnctFailed(net.resultStCd)
			{
				m_status = net.resultStCd
			}, SendMessageOptions.DontRequireReceiver);
		}
		yield break;
	}
}
