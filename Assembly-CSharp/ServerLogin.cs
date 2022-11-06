using System;
using System.Collections;
using Message;
using UnityEngine;

// Token: 0x02000767 RID: 1895
public class ServerLogin
{
	// Token: 0x0600329A RID: 12954 RVA: 0x0011996C File Offset: 0x00117B6C
	public static IEnumerator Process(string userId, string password, GameObject callbackObject)
	{
		NetServerLogin net = new NetServerLogin(userId, password);
		net.Request();
		NetMonitor monitor = NetMonitor.Instance;
		if (monitor != null)
		{
			monitor.StartMonitor(new ServerLoginRetry(userId, password, callbackObject));
		}
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
			loginState.sessionTimeLimit = net.sessionTimeLimit;
			ServerSettingState settingState = ServerInterface.SettingState;
			settingState.m_energyRefreshTime = net.resultEnergyRefreshTime;
			settingState.m_energyRecoveryMax = net.energyRecoveryMax;
			settingState.m_invitBaseIncentive = net.resultInviteBaseIncentive;
			settingState.m_rentalBaseIncentive = net.resultRentalBaseIncentive;
			settingState.m_userName = net.userName;
			settingState.m_userId = net.userId;
			MsgLoginSucceed msg = new MsgLoginSucceed();
			msg.m_userId = net.userId;
			msg.m_password = net.password;
			if (monitor != null)
			{
				monitor.EndMonitorForward(msg, callbackObject, "ServerLogin_Succeeded");
			}
			ServerLogin.m_passwordErrorCount = 0;
			if (callbackObject != null)
			{
				callbackObject.SendMessage("ServerLogin_Succeeded", msg, SendMessageOptions.DontRequireReceiver);
			}
		}
		else if (net.resultStCd == ServerInterface.StatusCode.PassWordError && ServerLogin.m_passwordErrorCount < ServerLogin.PasswordErrorCountMax)
		{
			ServerLogin.m_passwordErrorCount++;
			MsgServerPasswordError msg2 = new MsgServerPasswordError();
			msg2.m_key = net.key;
			msg2.m_userId = net.userId;
			msg2.m_password = net.password;
			if (callbackObject != null)
			{
				callbackObject.SendMessage("ServerLogin_Failed", msg2, SendMessageOptions.DontRequireReceiver);
			}
		}
		else
		{
			ServerLogin.m_passwordErrorCount = 0;
			MsgServerConnctFailed msg3 = new MsgServerConnctFailed(net.resultStCd);
			if (monitor != null)
			{
				monitor.EndMonitorForward(msg3, callbackObject, "ServerLogin_Failed");
			}
		}
		if (monitor != null)
		{
			monitor.EndMonitorBackward();
		}
		yield break;
	}

	// Token: 0x04002B82 RID: 11138
	private static int m_passwordErrorCount;

	// Token: 0x04002B83 RID: 11139
	private static readonly int PasswordErrorCountMax = 3;
}
