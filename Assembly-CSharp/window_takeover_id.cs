using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x020004B2 RID: 1202
public class window_takeover_id : WindowBase
{
	// Token: 0x170004C9 RID: 1225
	// (get) Token: 0x06002399 RID: 9113 RVA: 0x000D5E2C File Offset: 0x000D402C
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x0600239A RID: 9114 RVA: 0x000D5E34 File Offset: 0x000D4034
	private void Start()
	{
		OptionMenuUtility.TranObj(base.gameObject);
		base.enabled = false;
		if (this.m_closeBtn != null)
		{
			UIPlayAnimation component = this.m_closeBtn.GetComponent<UIPlayAnimation>();
			if (component != null)
			{
				EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.OnFinishedAnimationCallback), false);
			}
			UIButtonMessage uibuttonMessage = this.m_closeBtn.GetComponent<UIButtonMessage>();
			if (uibuttonMessage == null)
			{
				uibuttonMessage = this.m_closeBtn.AddComponent<UIButtonMessage>();
			}
			if (uibuttonMessage != null)
			{
				uibuttonMessage.enabled = true;
				uibuttonMessage.trigger = UIButtonMessage.Trigger.OnClick;
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "OnClickCloseButton";
			}
		}
		TextUtility.SetCommonText(this.m_headerTextLabel, "Option", "take_over");
		TextUtility.SetCommonText(this.m_passwordTextLabel, "Option", "password");
		this.m_uiAnimation = base.gameObject.AddComponent<UIPlayAnimation>();
		if (this.m_uiAnimation != null)
		{
			Animation component2 = base.gameObject.GetComponent<Animation>();
			this.m_uiAnimation.target = component2;
			this.m_uiAnimation.clipName = "ui_menu_option_window_Anim";
		}
		if (this.m_passwordLabel != null)
		{
			this.m_passwordLabel.text = string.Empty;
		}
		SoundManager.SePlay("sys_window_open", "SE");
	}

	// Token: 0x0600239B RID: 9115 RVA: 0x000D5F90 File Offset: 0x000D4190
	private void OnClickCloseButton()
	{
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x0600239C RID: 9116 RVA: 0x000D5FA4 File Offset: 0x000D41A4
	private void OnFinishedAnimationCallback()
	{
		this.m_isEnd = true;
	}

	// Token: 0x0600239D RID: 9117 RVA: 0x000D5FB0 File Offset: 0x000D41B0
	private void RequestServerGetMigrationPassword()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			if (string.IsNullOrEmpty(ServerInterface.MigrationPassword))
			{
				loggedInServerInterface.RequestServerGetMigrationPassword(null, base.gameObject);
			}
			else if (this.m_passwordLabel != null)
			{
				this.m_passwordLabel.text = ServerInterface.MigrationPassword;
			}
		}
	}

	// Token: 0x0600239E RID: 9118 RVA: 0x000D6014 File Offset: 0x000D4214
	public void PlayOpenWindow()
	{
		this.RequestServerGetMigrationPassword();
		this.m_isEnd = false;
		if (this.m_uiAnimation != null)
		{
			this.m_uiAnimation.Play(true);
		}
	}

	// Token: 0x0600239F RID: 9119 RVA: 0x000D604C File Offset: 0x000D424C
	private void ServerGetMigrationPassword_Succeeded(MsgGetMigrationPasswordSucceed msg)
	{
		if (msg != null && this.m_passwordLabel != null)
		{
			this.m_passwordLabel.text = msg.m_migrationPassword;
		}
	}

	// Token: 0x060023A0 RID: 9120 RVA: 0x000D6084 File Offset: 0x000D4284
	private void ServerGetMigrationPassword_Failed()
	{
	}

	// Token: 0x060023A1 RID: 9121 RVA: 0x000D6088 File Offset: 0x000D4288
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (msg != null)
		{
			msg.StaySequence();
		}
		UIButtonMessage component = this.m_closeBtn.GetComponent<UIButtonMessage>();
		if (component != null)
		{
			component.SendMessage("OnClick");
		}
	}

	// Token: 0x04002055 RID: 8277
	[SerializeField]
	private GameObject m_closeBtn;

	// Token: 0x04002056 RID: 8278
	[SerializeField]
	private UILabel m_headerTextLabel;

	// Token: 0x04002057 RID: 8279
	[SerializeField]
	private UILabel m_passwordTextLabel;

	// Token: 0x04002058 RID: 8280
	[SerializeField]
	private UILabel m_passwordLabel;

	// Token: 0x04002059 RID: 8281
	private bool m_isEnd;

	// Token: 0x0400205A RID: 8282
	private UIPlayAnimation m_uiAnimation;
}
