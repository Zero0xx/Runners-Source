using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020007B3 RID: 1971
public class ServerBuyAndroid
{
	// Token: 0x06003432 RID: 13362 RVA: 0x0011CBCC File Offset: 0x0011ADCC
	public static IEnumerator Process(string receiptData, string signature, GameObject callbackObject)
	{
		NetMonitor monitor = NetMonitor.Instance;
		if (monitor != null)
		{
			monitor.PrepareConnect(ServerSessionWatcher.ValidateType.LOGIN_OR_RELOGIN);
			while (!monitor.IsEndPrepare())
			{
				yield return null;
			}
			if (monitor.IsSuccessPrepare())
			{
				NetServerBuyAndroid net = new NetServerBuyAndroid(receiptData, signature);
				net.Request();
				monitor.StartMonitor(new ServerBuyAndroidRetry(receiptData, signature, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerPlayerState playerState = ServerInterface.PlayerState;
					ServerPlayerState resultPlayerState = net.resultPlayerState;
					resultPlayerState.CopyTo(playerState);
					ServerInterface.SettingState.m_isPurchased = true;
					MsgGetPlayerStateSucceed msg = new MsgGetPlayerStateSucceed();
					msg.m_playerState = resultPlayerState;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerBuyAndroid_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerBuyAndroid_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (net.resultStCd == ServerInterface.StatusCode.AlreadyProcessedReceipt)
					{
						if (callbackObject != null)
						{
							callbackObject.SendMessage("ServerBuyAndroid_Failed", msg2, SendMessageOptions.DontRequireReceiver);
						}
					}
					else if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerBuyAndroid_Failed");
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
