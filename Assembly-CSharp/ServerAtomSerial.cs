using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x0200078F RID: 1935
public class ServerAtomSerial
{
	// Token: 0x0600335F RID: 13151 RVA: 0x0011B508 File Offset: 0x00119708
	public static IEnumerator Process(string campaignId, string serial, bool new_user, GameObject callbackObject)
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
				NetServerAtomSerial net = new NetServerAtomSerial(campaignId, serial, new_user);
				net.Request();
				monitor.StartMonitor(new ServerAtomSerialRetry(campaignId, serial, new_user, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					MsgSendAtomSerialSucceed msg = new MsgSendAtomSerialSucceed();
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerAtomSerial_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerAtomSerial_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (msg2.m_status == ServerInterface.StatusCode.InvalidSerialCode || msg2.m_status == ServerInterface.StatusCode.UsedSerialCode)
					{
						if (callbackObject != null)
						{
							callbackObject.SendMessage("ServerAtomSerial_Failed", msg2, SendMessageOptions.DontRequireReceiver);
						}
					}
					else if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerAtomSerial_Failed");
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

	// Token: 0x06003360 RID: 13152 RVA: 0x0011B554 File Offset: 0x00119754
	public static bool GetSerialFromScheme(string scheme, ref string campaign, ref string serial)
	{
		if (string.IsNullOrEmpty(scheme))
		{
			return false;
		}
		int num = scheme.IndexOf("cid=");
		if (num > 0)
		{
			campaign = scheme.Substring(num + "cid=".Length);
			int num2 = campaign.IndexOf("&");
			if (num2 > 0)
			{
				campaign = campaign.Remove(num2);
			}
		}
		int num3 = scheme.IndexOf("serial=");
		if (num3 > 0)
		{
			serial = scheme.Substring(num3 + "serial=".Length);
		}
		if (string.IsNullOrEmpty(serial))
		{
			int num4 = scheme.IndexOf("start_code");
			if (num4 > 0)
			{
				serial = scheme.Substring(num4 + "start_code".Length);
			}
		}
		return !string.IsNullOrEmpty(campaign) && !string.IsNullOrEmpty(serial);
	}
}
