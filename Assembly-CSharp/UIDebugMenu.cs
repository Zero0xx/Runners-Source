using System;
using System.Collections;
using System.Collections.Generic;
using App;
using SaveData;
using UnityEngine;

// Token: 0x020001F3 RID: 499
public class UIDebugMenu : UIDebugMenuTask
{
	// Token: 0x06000D55 RID: 3413 RVA: 0x0004E9AC File Offset: 0x0004CBAC
	protected override void OnStartFromTask()
	{
		this.m_buttonList = base.gameObject.AddComponent<UIDebugMenuButtonList>();
		for (int i = 0; i < 15; i++)
		{
			string name = this.MenuObjName[i];
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, name);
			if (!(gameObject == null))
			{
				string childName = this.MenuObjName[i];
				this.m_buttonList.Add(this.RectList, this.MenuObjName, base.gameObject);
				base.AddChild(childName, gameObject);
			}
		}
		this.m_LoginIDField = base.gameObject.AddComponent<UIDebugMenuTextField>();
		string fieldText = string.Empty;
		string gameID = SystemSaveManager.GetGameID();
		fieldText = gameID;
		this.m_LoginIDField.Setup(new Rect(200f, 350f, 375f, 30f), "Enter Login ID.", fieldText);
		GameObject gameObject2 = GameObject.Find("DebugGameObject");
		if (gameObject2 != null)
		{
			gameObject2.AddComponent<LoadURLComponent>();
		}
		this.m_debugServerUrlField = base.gameObject.AddComponent<UIDebugMenuTextField>();
		this.m_debugServerUrlField.Setup(new Rect(200f, 470f, 375f, 30f), "forDev:デバッグサーバーURL入力(例：http://157.109.83.27/sdl/)");
		base.StartCoroutine(this.InitCoroutine());
		base.TransitionFrom();
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x0004EAF8 File Offset: 0x0004CCF8
	protected override void OnTransitionTo()
	{
		if (this.m_buttonList != null)
		{
			this.m_buttonList.SetActive(false);
		}
		if (this.m_LoginIDField != null)
		{
			this.m_LoginIDField.SetActive(false);
		}
		if (this.m_debugServerUrlField != null)
		{
			this.m_debugServerUrlField.SetActive(false);
		}
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x0004EB5C File Offset: 0x0004CD5C
	protected override void OnTransitionFrom()
	{
		if (this.m_buttonList != null)
		{
			this.m_buttonList.SetActive(true);
		}
		if (this.m_LoginIDField != null)
		{
			this.m_LoginIDField.SetActive(true);
		}
		if (this.m_debugServerUrlField != null)
		{
			this.m_debugServerUrlField.SetActive(true);
		}
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x0004EBC0 File Offset: 0x0004CDC0
	private void OnClicked(string name)
	{
		this.m_clickedButtonName = name;
		bool flag = true;
		if (this.m_clickedButtonName == this.MenuObjName[11] || this.m_clickedButtonName == this.MenuObjName[12] || this.m_clickedButtonName == this.MenuObjName[13] || this.m_clickedButtonName == this.MenuObjName[14] || this.m_clickedButtonName == this.MenuObjName[5] || this.m_clickedButtonName == this.MenuObjName[1] || this.m_clickedButtonName == this.MenuObjName[8] || this.m_clickedButtonName == this.MenuObjName[7])
		{
			flag = false;
		}
		if (flag)
		{
			ServerSessionWatcher serverSessionWatcher = GameObjectUtil.FindGameObjectComponent<ServerSessionWatcher>("NetMonitor");
			if (serverSessionWatcher != null)
			{
				serverSessionWatcher.ValidateSession(ServerSessionWatcher.ValidateType.LOGIN_OR_RELOGIN, new ServerSessionWatcher.ValidateSessionEndCallback(this.ValidateSessionCallback));
			}
		}
		else
		{
			this.ValidateSessionCallback(true);
		}
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x0004ECF8 File Offset: 0x0004CEF8
	private void ValidateSessionCallback(bool isSuccess)
	{
		if (this.m_clickedButtonName == this.MenuObjName[11])
		{
			Env.actionServerType = Env.ActionServerType.LOCAL1;
		}
		else if (this.m_clickedButtonName == this.MenuObjName[12])
		{
			Env.actionServerType = Env.ActionServerType.LOCAL2;
		}
		else if (this.m_clickedButtonName == this.MenuObjName[13])
		{
			Env.actionServerType = Env.ActionServerType.LOCAL3;
		}
		else if (this.m_clickedButtonName == this.MenuObjName[14])
		{
			Env.actionServerType = Env.ActionServerType.DEVELOP;
		}
		if (this.m_clickedButtonName == this.MenuObjName[11] || this.m_clickedButtonName == this.MenuObjName[12] || this.m_clickedButtonName == this.MenuObjName[13] || this.m_clickedButtonName == this.MenuObjName[14])
		{
			NetBaseUtil.DebugServerUrl = null;
			string text = NetBaseUtil.ActionServerURL;
		}
		else if (this.m_clickedButtonName == this.MenuObjName[5])
		{
			string text = this.m_debugServerUrlField.text;
			NetBaseUtil.DebugServerUrl = text;
			DebugSaveServerUrl.SaveURL(text);
		}
		else if (this.m_clickedButtonName == this.MenuObjName[1])
		{
			Application.LoadLevel(TitleDefine.TitleSceneName);
		}
		else
		{
			base.TransitionToChild(this.m_clickedButtonName);
		}
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x0004EE98 File Offset: 0x0004D098
	protected override void OnGuiFromTask()
	{
		GUI.Label(new Rect(400f, 510f, 300f, 60f), "現在のURL\n" + NetBaseUtil.ActionServerURL);
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x0004EEC8 File Offset: 0x0004D0C8
	private IEnumerator InitCoroutine()
	{
		yield return null;
		yield return null;
		if (DebugSaveServerUrl.Url != null)
		{
			this.m_debugServerUrlField.text = DebugSaveServerUrl.Url;
		}
		else
		{
			this.m_debugServerUrlField.text = NetBaseUtil.ActionServerURL;
		}
		yield break;
	}

	// Token: 0x04000B20 RID: 2848
	private List<string> MenuObjName = new List<string>
	{
		"Game",
		"Title",
		"Achievement",
		"Event",
		"Server",
		"for Dev./ActiveDebugServer",
		"for Dev./migration",
		"for Dev./user_move",
		"Facebook",
		"Notification",
		"Campaign",
		"LOCAL1",
		"LOCAL2",
		"LOCAL3",
		"DEVELOP"
	};

	// Token: 0x04000B21 RID: 2849
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 150f, 50f),
		new Rect(400f, 200f, 150f, 50f),
		new Rect(600f, 200f, 150f, 50f),
		new Rect(200f, 260f, 150f, 50f),
		new Rect(200f, 380f, 150f, 50f),
		new Rect(200f, 500f, 175f, 50f),
		new Rect(600f, 330f, 150f, 50f),
		new Rect(600f, 390f, 150f, 50f),
		new Rect(600f, 470f, 150f, 50f),
		new Rect(800f, 200f, 150f, 50f),
		new Rect(400f, 260f, 150f, 50f),
		new Rect(200f, 570f, 150f, 50f),
		new Rect(400f, 570f, 150f, 50f),
		new Rect(600f, 570f, 150f, 50f),
		new Rect(800f, 570f, 150f, 50f)
	};

	// Token: 0x04000B22 RID: 2850
	private UIDebugMenuButtonList m_buttonList;

	// Token: 0x04000B23 RID: 2851
	private UIDebugMenuTextField m_LoginIDField;

	// Token: 0x04000B24 RID: 2852
	private NetDebugLogin m_debugLogin;

	// Token: 0x04000B25 RID: 2853
	private UIDebugMenuTextField m_debugServerUrlField;

	// Token: 0x04000B26 RID: 2854
	private string m_clickedButtonName;

	// Token: 0x020001F4 RID: 500
	private enum MenuType
	{
		// Token: 0x04000B28 RID: 2856
		GAME,
		// Token: 0x04000B29 RID: 2857
		TITLE,
		// Token: 0x04000B2A RID: 2858
		AHIEVEMENT,
		// Token: 0x04000B2B RID: 2859
		EVENT,
		// Token: 0x04000B2C RID: 2860
		SERVER,
		// Token: 0x04000B2D RID: 2861
		ACTIVE_DEBUG_SERVER,
		// Token: 0x04000B2E RID: 2862
		MIGRATION,
		// Token: 0x04000B2F RID: 2863
		USER_MOVE,
		// Token: 0x04000B30 RID: 2864
		FACEBOOK,
		// Token: 0x04000B31 RID: 2865
		NOTIFICATION,
		// Token: 0x04000B32 RID: 2866
		CAMPAIGN,
		// Token: 0x04000B33 RID: 2867
		LOCAL1,
		// Token: 0x04000B34 RID: 2868
		LOCAL2,
		// Token: 0x04000B35 RID: 2869
		LOCAL3,
		// Token: 0x04000B36 RID: 2870
		DEVELOP,
		// Token: 0x04000B37 RID: 2871
		NUM
	}
}
