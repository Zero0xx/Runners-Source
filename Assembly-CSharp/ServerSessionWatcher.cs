using System;
using Message;
using NoahUnity;
using SaveData;
using UnityEngine;

// Token: 0x020007D6 RID: 2006
public class ServerSessionWatcher : MonoBehaviour
{
	// Token: 0x0600356D RID: 13677 RVA: 0x0011F72C File Offset: 0x0011D92C
	private void Start()
	{
	}

	// Token: 0x0600356E RID: 13678 RVA: 0x0011F730 File Offset: 0x0011D930
	private void Update()
	{
	}

	// Token: 0x0600356F RID: 13679 RVA: 0x0011F734 File Offset: 0x0011D934
	public void ValidateSession(ServerSessionWatcher.ValidateType type, ServerSessionWatcher.ValidateSessionEndCallback callback, string loginKey)
	{
		this.m_loginKey = loginKey;
		this.ValidateSession(type, callback);
	}

	// Token: 0x06003570 RID: 13680 RVA: 0x0011F748 File Offset: 0x0011D948
	public void ValidateSession(ServerSessionWatcher.ValidateType type, ServerSessionWatcher.ValidateSessionEndCallback callback)
	{
		this.m_validateType = type;
		this.m_callback = callback;
		ServerSessionWatcher.SessionState sessionState = this.CheckSessionState();
		ServerSessionWatcher.SessionState sessionState2 = sessionState;
		if (sessionState2 != ServerSessionWatcher.SessionState.NEED_LOGIN)
		{
			if (sessionState2 != ServerSessionWatcher.SessionState.VALID)
			{
				this.Fail();
			}
			else
			{
				this.Success();
			}
		}
		else
		{
			switch (this.m_validateType)
			{
			case ServerSessionWatcher.ValidateType.PRELOGIN:
				this.Login();
				break;
			case ServerSessionWatcher.ValidateType.LOGIN:
				this.Login();
				break;
			case ServerSessionWatcher.ValidateType.LOGIN_OR_RELOGIN:
				this.Login();
				break;
			default:
				this.Fail();
				break;
			}
		}
	}

	// Token: 0x06003571 RID: 13681 RVA: 0x0011F7E4 File Offset: 0x0011D9E4
	public void InvalidateSession()
	{
		ServerLoginState loginState = ServerInterface.LoginState;
		if (loginState == null)
		{
			return;
		}
		loginState.sessionTimeLimit = 0;
		loginState.seqNum = 0UL;
	}

	// Token: 0x06003572 RID: 13682 RVA: 0x0011F810 File Offset: 0x0011DA10
	private ServerSessionWatcher.SessionState CheckSessionState()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface == null)
		{
			return ServerSessionWatcher.SessionState.NEED_LOGIN;
		}
		ServerLoginState loginState = ServerInterface.LoginState;
		DateTime localDateTime = NetUtil.GetLocalDateTime((long)loginState.sessionTimeLimit);
		DateTime currentTime = NetUtil.GetCurrentTime();
		if (NetUtil.IsAlreadySessionTimeOut(localDateTime, currentTime))
		{
			return ServerSessionWatcher.SessionState.NEED_LOGIN;
		}
		return ServerSessionWatcher.SessionState.VALID;
	}

	// Token: 0x06003573 RID: 13683 RVA: 0x0011F85C File Offset: 0x0011DA5C
	private void Success()
	{
		global::Debug.Log("ServerSessionWatcher:Succeeded");
		this.m_callback(true);
	}

	// Token: 0x06003574 RID: 13684 RVA: 0x0011F874 File Offset: 0x0011DA74
	private void Fail()
	{
		global::Debug.Log("ServerSessionWatcher:Failed");
		this.m_callback(false);
	}

	// Token: 0x06003575 RID: 13685 RVA: 0x0011F88C File Offset: 0x0011DA8C
	private void Login()
	{
		global::Debug.Log("ServerSessionWatcher:Login");
		if (!string.IsNullOrEmpty(this.m_loginKey))
		{
			string systemSaveDataGameId = TitleUtil.GetSystemSaveDataGameId();
			string systemSaveDataPassword = TitleUtil.GetSystemSaveDataPassword();
			string password = NetUtil.CalcMD5String(string.Concat(new string[]
			{
				this.m_loginKey,
				":dho5v5yy7n2uswa5iblb:",
				systemSaveDataGameId,
				":",
				systemSaveDataPassword
			}));
			ServerInterface serverInterface = GameObjectUtil.FindGameObjectComponent<ServerInterface>("ServerInterface");
			if (serverInterface != null)
			{
				serverInterface.RequestServerLogin(systemSaveDataGameId, password, base.gameObject);
			}
		}
		else
		{
			string systemSaveDataGameId2 = TitleUtil.GetSystemSaveDataGameId();
			ServerInterface serverInterface2 = GameObjectUtil.FindGameObjectComponent<ServerInterface>("ServerInterface");
			if (serverInterface2 != null)
			{
				serverInterface2.RequestServerLogin(systemSaveDataGameId2, string.Empty, base.gameObject);
			}
		}
	}

	// Token: 0x06003576 RID: 13686 RVA: 0x0011F954 File Offset: 0x0011DB54
	private void Relogin()
	{
		global::Debug.Log("ServerSessionWatcher:ReLogin");
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerReLogin(base.gameObject);
		}
	}

	// Token: 0x06003577 RID: 13687 RVA: 0x0011F98C File Offset: 0x0011DB8C
	private void ServerLogin_Succeeded(MsgLoginSucceed msg)
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		bool flag = TitleUtil.SetSystemSaveDataGameId(msg.m_userId);
		bool flag2 = TitleUtil.SetSystemSaveDataPassword(msg.m_password);
		if ((flag || flag2) && instance != null)
		{
			instance.SaveSystemData();
		}
		if (flag)
		{
			FoxManager.SendLtvPoint(FoxLtvType.RegisterAccount);
		}
		this.m_loginKey = string.Empty;
		this.Success();
		bool flag3 = false;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				string noahID = Noah.Instance.GetNoahID();
				string noahId = systemdata.noahId;
				global::Debug.Log("CurrentNoahID=" + noahID);
				global::Debug.Log("PreviousNoahID=" + noahId);
				if (noahID != noahId)
				{
					flag3 = true;
				}
			}
		}
		if (!this.m_isSendNoahId && flag3)
		{
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				string noahID2 = Noah.Instance.GetNoahID();
				if (!string.IsNullOrEmpty(noahID2))
				{
					loggedInServerInterface.RequestServerSetNoahId(noahID2, base.gameObject);
				}
			}
			this.m_isSendNoahId = true;
			global::Debug.Log("NoahId sended");
		}
		else
		{
			global::Debug.Log("NoahId don't sended");
		}
	}

	// Token: 0x06003578 RID: 13688 RVA: 0x0011FAC4 File Offset: 0x0011DCC4
	private void ServerLogin_Failed(MessageBase msg)
	{
		if (msg == null)
		{
			return;
		}
		MsgServerPasswordError msgServerPasswordError = msg as MsgServerPasswordError;
		if (msgServerPasswordError == null)
		{
			return;
		}
		this.m_loginKey = msgServerPasswordError.m_key;
		bool flag = TitleUtil.SetSystemSaveDataGameId(msgServerPasswordError.m_userId);
		bool flag2 = TitleUtil.SetSystemSaveDataPassword(msgServerPasswordError.m_password);
		if (flag || flag2)
		{
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				instance.SaveSystemData();
			}
		}
		if (flag)
		{
			FoxManager.SendLtvPoint(FoxLtvType.RegisterAccount);
		}
		if (this.m_validateType == ServerSessionWatcher.ValidateType.PRELOGIN)
		{
			this.m_callback(true);
			return;
		}
		global::Debug.Log("GameModeTitle.UserId: " + msgServerPasswordError.m_userId);
		global::Debug.Log("GameModeTitle.Password: " + msgServerPasswordError.m_password);
		this.Login();
	}

	// Token: 0x06003579 RID: 13689 RVA: 0x0011FB84 File Offset: 0x0011DD84
	public void ServerReLogin_Succeeded()
	{
		this.Success();
	}

	// Token: 0x0600357A RID: 13690 RVA: 0x0011FB8C File Offset: 0x0011DD8C
	private void ServerSetNoahId_Succeeded(MsgSetNoahIdSucceed msg)
	{
		global::Debug.Log("GameModeTitle.ServerSetNoahId_Succeeded");
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				string noahID = Noah.Instance.GetNoahID();
				systemdata.noahId = noahID;
				instance.SaveSystemData();
			}
		}
	}

	// Token: 0x04002D25 RID: 11557
	private ServerSessionWatcher.ValidateType m_validateType;

	// Token: 0x04002D26 RID: 11558
	private ServerSessionWatcher.ValidateSessionEndCallback m_callback;

	// Token: 0x04002D27 RID: 11559
	private string m_loginKey = string.Empty;

	// Token: 0x04002D28 RID: 11560
	private bool m_isSendNoahId;

	// Token: 0x020007D7 RID: 2007
	public enum ValidateType
	{
		// Token: 0x04002D2A RID: 11562
		NONE = -1,
		// Token: 0x04002D2B RID: 11563
		PRELOGIN,
		// Token: 0x04002D2C RID: 11564
		LOGIN,
		// Token: 0x04002D2D RID: 11565
		LOGIN_OR_RELOGIN,
		// Token: 0x04002D2E RID: 11566
		COUNT
	}

	// Token: 0x020007D8 RID: 2008
	private enum SessionState
	{
		// Token: 0x04002D30 RID: 11568
		NONE = -1,
		// Token: 0x04002D31 RID: 11569
		NEED_LOGIN,
		// Token: 0x04002D32 RID: 11570
		VALID,
		// Token: 0x04002D33 RID: 11571
		COUNT
	}

	// Token: 0x02000AA3 RID: 2723
	// (Invoke) Token: 0x060048C6 RID: 18630
	public delegate void ValidateSessionEndCallback(bool isSuccess);
}
