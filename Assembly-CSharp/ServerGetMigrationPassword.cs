using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x0200075F RID: 1887
public class ServerGetMigrationPassword
{
	// Token: 0x06003289 RID: 12937 RVA: 0x00119794 File Offset: 0x00117994
	public static IEnumerator Process(string userPassword, GameObject callbackObject)
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
				NetServerGetMigrationPassword net = new NetServerGetMigrationPassword(userPassword);
				net.Request();
				monitor.StartMonitor(new ServerGetMigrationPasswordRetry(userPassword, callbackObject));
				while (net.IsExecuting())
				{
					yield return null;
				}
				if (net.IsSucceeded())
				{
					ServerInterface.MigrationPassword = net.paramMigrationPassword;
					MsgGetMigrationPasswordSucceed msg = new MsgGetMigrationPasswordSucceed();
					msg.m_migrationPassword = net.paramMigrationPassword;
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg, callbackObject, "ServerGetMigrationPassword_Succeeded");
					}
					if (callbackObject != null)
					{
						callbackObject.SendMessage("ServerGetMigrationPassword_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
					if (monitor != null)
					{
						monitor.EndMonitorForward(msg2, callbackObject, "ServerGetMigrationPassword_Failed");
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
