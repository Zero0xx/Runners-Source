using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x0200075B RID: 1883
public class ServerGetCountry
{
	// Token: 0x06003280 RID: 12928 RVA: 0x00119650 File Offset: 0x00117850
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
				NetServerGetCountry net = new NetServerGetCountry();
				net.Request();
				monitor.StartMonitor(new ServerGetCountryRetry(callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerInterface.SettingState.m_countryId = net.resultCountryId;
					ServerInterface.SettingState.m_countryCode = net.resultCountryCode;
					MsgGetCountrySucceed msg = new MsgGetCountrySucceed();
					msg.m_countryId = net.resultCountryId;
					msg.m_countryCode = net.resultCountryCode;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetCountry_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetCountry_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetCountry_Failed");
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
