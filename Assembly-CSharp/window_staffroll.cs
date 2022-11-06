using System;
using DataTable;
using Text;
using UnityEngine;

// Token: 0x020004B1 RID: 1201
public class window_staffroll : WindowBase
{
	// Token: 0x170004C8 RID: 1224
	// (get) Token: 0x0600238E RID: 9102 RVA: 0x000D5A10 File Offset: 0x000D3C10
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x0600238F RID: 9103 RVA: 0x000D5A18 File Offset: 0x000D3C18
	private void Start()
	{
	}

	// Token: 0x06002390 RID: 9104 RVA: 0x000D5A1C File Offset: 0x000D3C1C
	private void Initialize()
	{
		if (this.m_init)
		{
			return;
		}
		OptionMenuUtility.TranObj(base.gameObject);
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
		if (this.m_scrollBar != null)
		{
			this.m_scrollBar.value = 0f;
		}
		if (this.m_staffrollLabel != null)
		{
			this.m_staffrollLabel.text = string.Empty;
		}
		this.m_uiAnimation = base.gameObject.AddComponent<UIPlayAnimation>();
		if (this.m_uiAnimation != null)
		{
			Animation component3 = base.gameObject.GetComponent<Animation>();
			this.m_uiAnimation.target = component3;
			this.m_uiAnimation.clipName = "ui_menu_option_window_Anim";
		}
		this.m_init = true;
	}

	// Token: 0x06002391 RID: 9105 RVA: 0x000D5B78 File Offset: 0x000D3D78
	private void Update()
	{
		if (this.m_parserObject != null)
		{
			HtmlParser component = this.m_parserObject.GetComponent<HtmlParser>();
			if (component != null && component.IsEndParse)
			{
				base.enabled = false;
				if (this.m_staffrollLabel != null)
				{
					this.m_staffrollLabel.text = component.ParsedString;
				}
				if (this.m_creditFlag)
				{
					this.m_creditText = component.ParsedString;
				}
				else
				{
					this.m_copyrightText = component.ParsedString;
				}
				UnityEngine.Object.Destroy(this.m_parserObject);
				this.m_parserObject = null;
			}
		}
	}

	// Token: 0x06002392 RID: 9106 RVA: 0x000D5C1C File Offset: 0x000D3E1C
	public void SetStaffRollText()
	{
		this.Initialize();
		this.m_creditFlag = true;
		TextUtility.SetCommonText(this.m_headerTextLabel, "Option", "staff_credit");
		if (string.IsNullOrEmpty(this.m_creditText))
		{
			if (this.m_staffrollLabel != null)
			{
				this.m_staffrollLabel.text = string.Empty;
			}
			string webPageURL = NetUtil.GetWebPageURL(InformationDataTable.Type.CREDIT);
			this.m_parserObject = HtmlParserFactory.Create(webPageURL, HtmlParser.SyncType.TYPE_ASYNC, HtmlParser.SyncType.TYPE_ASYNC);
			base.enabled = true;
		}
		else if (this.m_staffrollLabel != null)
		{
			this.m_staffrollLabel.text = this.m_creditText;
		}
	}

	// Token: 0x06002393 RID: 9107 RVA: 0x000D5CC0 File Offset: 0x000D3EC0
	public void SetCopyrightText()
	{
		this.Initialize();
		this.m_creditFlag = false;
		TextUtility.SetCommonText(this.m_headerTextLabel, "Option", "copyright");
		if (string.IsNullOrEmpty(this.m_copyrightText))
		{
			if (this.m_staffrollLabel != null)
			{
				this.m_staffrollLabel.text = string.Empty;
			}
			string webPageURL = NetUtil.GetWebPageURL(InformationDataTable.Type.COPYRIGHT);
			this.m_parserObject = HtmlParserFactory.Create(webPageURL, HtmlParser.SyncType.TYPE_ASYNC, HtmlParser.SyncType.TYPE_ASYNC);
			base.enabled = true;
		}
		else if (this.m_staffrollLabel != null)
		{
			this.m_staffrollLabel.text = this.m_copyrightText;
		}
	}

	// Token: 0x06002394 RID: 9108 RVA: 0x000D5D64 File Offset: 0x000D3F64
	private void OnClickCloseButton()
	{
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x06002395 RID: 9109 RVA: 0x000D5D78 File Offset: 0x000D3F78
	private void OnFinishedAnimationCallback()
	{
		this.m_isEnd = true;
	}

	// Token: 0x06002396 RID: 9110 RVA: 0x000D5D84 File Offset: 0x000D3F84
	public void PlayOpenWindow()
	{
		this.m_isEnd = false;
		if (this.m_scrollBar != null)
		{
			this.m_scrollBar.value = 0f;
		}
		if (this.m_uiAnimation != null)
		{
			this.m_uiAnimation.Play(true);
		}
		SoundManager.SePlay("sys_window_open", "SE");
	}

	// Token: 0x06002397 RID: 9111 RVA: 0x000D5DE8 File Offset: 0x000D3FE8
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

	// Token: 0x04002049 RID: 8265
	private const string ATTACH_ANTHOR_NAME = "UI Root (2D)/Camera/menu_Anim/OptionUI/Anchor_5_MC";

	// Token: 0x0400204A RID: 8266
	[SerializeField]
	private GameObject m_closeBtn;

	// Token: 0x0400204B RID: 8267
	[SerializeField]
	private UIScrollBar m_scrollBar;

	// Token: 0x0400204C RID: 8268
	[SerializeField]
	private UILabel m_headerTextLabel;

	// Token: 0x0400204D RID: 8269
	[SerializeField]
	private UILabel m_staffrollLabel;

	// Token: 0x0400204E RID: 8270
	private GameObject m_parserObject;

	// Token: 0x0400204F RID: 8271
	private string m_creditText = string.Empty;

	// Token: 0x04002050 RID: 8272
	private string m_copyrightText = string.Empty;

	// Token: 0x04002051 RID: 8273
	private bool m_creditFlag;

	// Token: 0x04002052 RID: 8274
	private bool m_isEnd;

	// Token: 0x04002053 RID: 8275
	private bool m_init;

	// Token: 0x04002054 RID: 8276
	private UIPlayAnimation m_uiAnimation;
}
