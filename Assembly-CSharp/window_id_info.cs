using System;
using AnimationOrTween;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x020004AD RID: 1197
public class window_id_info : WindowBase
{
	// Token: 0x170004C2 RID: 1218
	// (get) Token: 0x06002362 RID: 9058 RVA: 0x000D4C2C File Offset: 0x000D2E2C
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x170004C3 RID: 1219
	// (get) Token: 0x06002363 RID: 9059 RVA: 0x000D4C34 File Offset: 0x000D2E34
	public bool IsPassResetEnd
	{
		get
		{
			return this.m_isResetPassEnd;
		}
	}

	// Token: 0x06002364 RID: 9060 RVA: 0x000D4C3C File Offset: 0x000D2E3C
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
			UIButtonMessage component2 = this.m_closeBtn.GetComponent<UIButtonMessage>();
			if (component2 == null)
			{
				this.m_closeBtn.AddComponent<UIButtonMessage>();
				component2 = this.m_closeBtn.GetComponent<UIButtonMessage>();
			}
			if (component2 != null)
			{
				component2.enabled = true;
				component2.trigger = UIButtonMessage.Trigger.OnClick;
				component2.target = base.gameObject;
				component2.functionName = "OnClickCloseButton";
			}
		}
		UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "Btn_copy");
		if (uibuttonMessage != null)
		{
			uibuttonMessage.enabled = true;
			uibuttonMessage.trigger = UIButtonMessage.Trigger.OnClick;
			uibuttonMessage.target = base.gameObject;
			uibuttonMessage.functionName = "OnClickClipboard";
		}
		UIButtonMessage uibuttonMessage2 = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "Btn_passset");
		if (uibuttonMessage2 != null)
		{
			uibuttonMessage2.enabled = true;
			uibuttonMessage2.trigger = UIButtonMessage.Trigger.OnClick;
			uibuttonMessage2.target = base.gameObject;
			uibuttonMessage2.functionName = "OnClickPassSet";
		}
		this.m_uiAnimation = base.gameObject.AddComponent<UIPlayAnimation>();
		if (this.m_uiAnimation != null)
		{
			Animation component3 = base.gameObject.GetComponent<Animation>();
			this.m_uiAnimation.target = component3;
			this.m_uiAnimation.clipName = "ui_menu_option_window_Anim";
		}
		TextUtility.SetCommonText(this.m_headerTextLabel, "Option", "id_check");
		TextUtility.SetCommonText(this.m_yourIDLabel, "Option", "your_id");
		if (this.m_idLabel != null)
		{
			string viewUserID = this.GetViewUserID();
			this.m_idLabel.text = viewUserID;
		}
		SoundManager.SePlay("sys_window_open", "SE");
	}

	// Token: 0x06002365 RID: 9061 RVA: 0x000D4E30 File Offset: 0x000D3030
	private void OnClickCloseButton()
	{
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x06002366 RID: 9062 RVA: 0x000D4E44 File Offset: 0x000D3044
	private void OnFinishedAnimationCallback()
	{
		this.m_isEnd = true;
	}

	// Token: 0x06002367 RID: 9063 RVA: 0x000D4E50 File Offset: 0x000D3050
	public void PlayOpenWindow()
	{
		this.m_isEnd = false;
		this.m_isResetPassEnd = false;
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_num_takeover");
		if (uilabel != null)
		{
			uilabel.text = SystemSaveManager.GetTakeoverID();
		}
		UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_num_pass");
		if (uilabel2 != null)
		{
			uilabel2.text = SystemSaveManager.GetTakeoverPassword();
		}
		if (this.m_uiAnimation != null)
		{
			this.m_uiAnimation.Play(true);
		}
	}

	// Token: 0x06002368 RID: 9064 RVA: 0x000D4ED8 File Offset: 0x000D30D8
	private void PlayCloseAnimation()
	{
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(component, Direction.Reverse);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedAnimationCallback), true);
			}
		}
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x06002369 RID: 9065 RVA: 0x000D4F3C File Offset: 0x000D313C
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

	// Token: 0x0600236A RID: 9066 RVA: 0x000D4F78 File Offset: 0x000D3178
	private void OnClickClipboard()
	{
		string gameID = SystemSaveManager.GetGameID();
		string takeoverID = SystemSaveManager.GetTakeoverID();
		string text = gameID + "\n" + takeoverID;
		Clipboard.text = text;
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x0600236B RID: 9067 RVA: 0x000D4FB4 File Offset: 0x000D31B4
	private void OnClickPassSet()
	{
		this.PlayCloseAnimation();
		this.m_isResetPassEnd = true;
	}

	// Token: 0x0600236C RID: 9068 RVA: 0x000D4FC4 File Offset: 0x000D31C4
	private string GetViewUserID()
	{
		string text = SystemSaveManager.GetGameID();
		if (text.Length > 7)
		{
			text = text.Insert(6, " ");
			text = text.Insert(3, " ");
		}
		return text;
	}

	// Token: 0x04002027 RID: 8231
	[SerializeField]
	private GameObject m_closeBtn;

	// Token: 0x04002028 RID: 8232
	[SerializeField]
	private UILabel m_headerTextLabel;

	// Token: 0x04002029 RID: 8233
	[SerializeField]
	private UILabel m_yourIDLabel;

	// Token: 0x0400202A RID: 8234
	[SerializeField]
	private UILabel m_idLabel;

	// Token: 0x0400202B RID: 8235
	private bool m_isEnd;

	// Token: 0x0400202C RID: 8236
	private bool m_isResetPassEnd;

	// Token: 0x0400202D RID: 8237
	private UIPlayAnimation m_uiAnimation;
}
