using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x020004B7 RID: 1207
public class window_user_date : WindowBase
{
	// Token: 0x170004D0 RID: 1232
	// (get) Token: 0x060023C5 RID: 9157 RVA: 0x000D6E50 File Offset: 0x000D5050
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x170004D1 RID: 1233
	// (get) Token: 0x060023C6 RID: 9158 RVA: 0x000D6E58 File Offset: 0x000D5058
	public ServerOptionUserResult OptionUserResult
	{
		get
		{
			return this.m_serverOptionUserResult;
		}
	}

	// Token: 0x060023C7 RID: 9159 RVA: 0x000D6E60 File Offset: 0x000D5060
	private void Start()
	{
		OptionMenuUtility.TranObj(base.gameObject);
		if (this.m_closeBtn != null)
		{
			UIButtonMessage component = this.m_closeBtn.GetComponent<UIButtonMessage>();
			if (component == null)
			{
				this.m_closeBtn.AddComponent<UIButtonMessage>();
				component = this.m_closeBtn.GetComponent<UIButtonMessage>();
			}
			if (component != null)
			{
				component.enabled = true;
				component.trigger = UIButtonMessage.Trigger.OnClick;
				component.target = base.gameObject;
				component.functionName = "OnClickCloseButton";
			}
			UIPlayAnimation component2 = this.m_closeBtn.GetComponent<UIPlayAnimation>();
			if (component2 != null)
			{
				EventDelegate.Add(component2.onFinished, new EventDelegate.Callback(this.OnFinishedAnimationCallback), false);
			}
		}
		if (this.m_scrollBar != null)
		{
			this.m_scrollBar.value = 0f;
		}
		if (this.m_headerTextLabel != null)
		{
			TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Option", "users_results");
			if (text != null)
			{
				this.m_headerTextLabel.text = text.text;
			}
		}
		this.m_uiAnimation = base.gameObject.AddComponent<UIPlayAnimation>();
		if (this.m_uiAnimation != null)
		{
			Animation component3 = base.gameObject.GetComponent<Animation>();
			this.m_uiAnimation.target = component3;
			this.m_uiAnimation.clipName = "ui_menu_option_window_Anim";
		}
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerOptionUserResult(base.gameObject);
		}
		SoundManager.SePlay("sys_window_open", "SE");
	}

	// Token: 0x060023C8 RID: 9160 RVA: 0x000D6FF0 File Offset: 0x000D51F0
	private void Update()
	{
		if (!this.m_init)
		{
			base.enabled = false;
			this.m_init = true;
			this.SetItemStorage();
		}
	}

	// Token: 0x060023C9 RID: 9161 RVA: 0x000D7014 File Offset: 0x000D5214
	private void SetItemStorage()
	{
		if (this.m_itemStorage != null)
		{
			this.m_itemStorage.maxItemCount = 14;
			this.m_itemStorage.maxRows = 14;
			this.m_itemStorage.Restart();
			this.UpdateViewItemStorage();
		}
	}

	// Token: 0x060023CA RID: 9162 RVA: 0x000D7060 File Offset: 0x000D5260
	private void UpdateViewItemStorage()
	{
		if (this.m_itemStorage != null)
		{
			ui_option_window_user_date_scroll[] componentsInChildren = this.m_itemStorage.GetComponentsInChildren<ui_option_window_user_date_scroll>(true);
			int num = componentsInChildren.Length;
			for (int i = 0; i < num; i++)
			{
				if (i < 14)
				{
					ui_option_window_user_date_scroll.ResultType type = (ui_option_window_user_date_scroll.ResultType)i;
					componentsInChildren[i].UpdateView(type, this.m_serverOptionUserResult);
				}
			}
		}
	}

	// Token: 0x060023CB RID: 9163 RVA: 0x000D70BC File Offset: 0x000D52BC
	private void OnClickCloseButton()
	{
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x060023CC RID: 9164 RVA: 0x000D70D0 File Offset: 0x000D52D0
	private void OnFinishedAnimationCallback()
	{
		this.m_isEnd = true;
	}

	// Token: 0x060023CD RID: 9165 RVA: 0x000D70DC File Offset: 0x000D52DC
	private void ServerGetOptionUserResult_Succeeded(MsgGetOptionUserResultSucceed msg)
	{
		if (msg != null && msg.m_serverOptionUserResult != null)
		{
			msg.m_serverOptionUserResult.CopyTo(this.m_serverOptionUserResult);
		}
		this.UpdateViewItemStorage();
	}

	// Token: 0x060023CE RID: 9166 RVA: 0x000D7114 File Offset: 0x000D5314
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

	// Token: 0x060023CF RID: 9167 RVA: 0x000D7150 File Offset: 0x000D5350
	private void ServerGetOptionUserResult_Failed()
	{
	}

	// Token: 0x060023D0 RID: 9168 RVA: 0x000D7154 File Offset: 0x000D5354
	public void PlayOpenWindow()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerOptionUserResult(base.gameObject);
		}
		this.m_isEnd = false;
		if (this.m_uiAnimation != null)
		{
			this.m_uiAnimation.Play(true);
		}
	}

	// Token: 0x04002080 RID: 8320
	[SerializeField]
	private GameObject m_closeBtn;

	// Token: 0x04002081 RID: 8321
	[SerializeField]
	private UIScrollBar m_scrollBar;

	// Token: 0x04002082 RID: 8322
	[SerializeField]
	private UIRectItemStorage m_itemStorage;

	// Token: 0x04002083 RID: 8323
	[SerializeField]
	private UILabel m_headerTextLabel;

	// Token: 0x04002084 RID: 8324
	private ServerOptionUserResult m_serverOptionUserResult = new ServerOptionUserResult();

	// Token: 0x04002085 RID: 8325
	private UIPlayAnimation m_uiAnimation;

	// Token: 0x04002086 RID: 8326
	private bool m_isEnd;

	// Token: 0x04002087 RID: 8327
	private bool m_init;
}
