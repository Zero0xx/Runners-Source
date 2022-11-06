using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x0200076D RID: 1901
public class ServerMigration
{
	// Token: 0x060032A6 RID: 12966 RVA: 0x00119B5C File Offset: 0x00117D5C
	public static IEnumerator Process(string migrationId, string migrationPassword, GameObject callbackObject)
	{
		NetMonitor monitor = NetMonitor.Instance;
		if (monitor != null)
		{
			NetServerMigration net = new NetServerMigration(migrationId, migrationPassword);
			net.Request();
			monitor.StartMonitor(new ServerMigrationRetry(migrationId, migrationPassword, callbackObject));
			while (net.IsExecuting())
			{
				yield return null;
			}
			if (net.IsSucceeded())
			{
				ServerLoginState loginState = ServerInterface.LoginState;
				loginState.m_lineId = net.paramUserId;
				loginState.m_altLineId = string.Empty;
				loginState.m_lineAuthToken = string.Empty;
				loginState.sessionId = net.resultSessionId;
				ServerSettingState settingState = ServerInterface.SettingState;
				settingState.m_energyRefreshTime = net.resultEnergyRefreshTime;
				settingState.m_invitBaseIncentive = net.resultInviteBaseIncentive;
				settingState.m_rentalBaseIncentive = net.resultRentalBaseIncentive;
				settingState.m_userName = net.userName;
				settingState.m_userId = net.userId;
				MsgLoginSucceed msg = new MsgLoginSucceed();
				msg.m_userId = net.userId;
				msg.m_password = net.password;
				msg.m_countryCode = net.countryCode;
				if (monitor != null)
				{
					monitor.EndMonitorForward(msg, callbackObject, "ServerMigration_Succeeded");
				}
				if (callbackObject != null)
				{
					callbackObject.SendMessage("ServerMigration_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
				}
			}
			else
			{
				MsgServerConnctFailed msg2 = new MsgServerConnctFailed(net.resultStCd);
				if (net.resultStCd == ServerInterface.StatusCode.PassWordError)
				{
					callbackObject.SendMessage("ServerMigration_Failed", msg2, SendMessageOptions.DontRequireReceiver);
				}
				else if (monitor != null)
				{
					monitor.EndMonitorForward(msg2, callbackObject, "ServerMigration_Failed");
				}
			}
			if (monitor != null)
			{
				monitor.EndMonitorBackward();
			}
		}
		yield break;
	}
}
