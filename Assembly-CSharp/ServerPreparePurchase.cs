using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x020007BA RID: 1978
public class ServerPreparePurchase
{
	// Token: 0x06003440 RID: 13376 RVA: 0x0011CD70 File Offset: 0x0011AF70
	public static IEnumerator Process(int itemId, GameObject callbackObject)
	{
		NetServerPreparePurchase net = new NetServerPreparePurchase(itemId);
		net.Request();
		while (net.IsExecuting())
		{
			yield return null;
		}
		if (net.IsSucceeded())
		{
			if (callbackObject != null)
			{
				callbackObject.SendMessage("ServerPreparePurchase_Succeeded", null, SendMessageOptions.DontRequireReceiver);
			}
		}
		else
		{
			MsgServerConnctFailed msg = new MsgServerConnctFailed(net.resultStCd);
			callbackObject.SendMessage("ServerPreparePurchase_Failed", msg, SendMessageOptions.DontRequireReceiver);
		}
		yield break;
	}
}
