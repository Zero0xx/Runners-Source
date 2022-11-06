using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x020004A5 RID: 1189
public class ui_option_tutorial_scroll : MonoBehaviour
{
	// Token: 0x170004BD RID: 1213
	// (get) Token: 0x06002335 RID: 9013 RVA: 0x000D3694 File Offset: 0x000D1894
	public bool OpenWindow
	{
		get
		{
			return this.m_openWindow;
		}
	}

	// Token: 0x06002336 RID: 9014 RVA: 0x000D369C File Offset: 0x000D189C
	private void Start()
	{
		base.enabled = false;
		if (this.m_btnObj != null)
		{
			UIButtonMessage component = this.m_btnObj.GetComponent<UIButtonMessage>();
			if (component != null)
			{
				component.enabled = true;
				component.trigger = UIButtonMessage.Trigger.OnClick;
				component.target = base.gameObject;
				component.functionName = "OnClickOptionTutorialScroll";
			}
		}
	}

	// Token: 0x06002337 RID: 9015 RVA: 0x000D3700 File Offset: 0x000D1900
	public void Update()
	{
		if (this.m_window != null && this.m_window.IsEnd)
		{
			this.m_openWindow = false;
			this.m_window.gameObject.SetActive(false);
			base.enabled = false;
			if (this.m_scrollInfo != null && this.m_scrollInfo.Parent != null)
			{
				this.m_scrollInfo.Parent.SetCloseBtnColliderTrigger(false);
			}
		}
	}

	// Token: 0x06002338 RID: 9016 RVA: 0x000D3780 File Offset: 0x000D1980
	public void UpdateView(window_tutorial.ScrollInfo info)
	{
		this.m_scrollInfo = info;
		this.SetText();
	}

	// Token: 0x06002339 RID: 9017 RVA: 0x000D3790 File Offset: 0x000D1990
	public void SetText()
	{
		if (this.m_scrollInfo != null)
		{
			string text = null;
			switch (this.m_scrollInfo.DispType)
			{
			case window_tutorial.DisplayType.TUTORIAL:
				text = TextUtility.GetCommonText("Option", "tutorial");
				break;
			case window_tutorial.DisplayType.QUICK:
				text = TextUtility.GetCommonText("Tutorial", "caption_quickmode_tutorial");
				break;
			case window_tutorial.DisplayType.CHARA:
				text = TextUtility.GetCommonText("CharaName", CharaName.Name[(int)this.m_scrollInfo.Chara]);
				break;
			case window_tutorial.DisplayType.BOSS_MAP_1:
				text = BossTypeUtil.GetTextCommonBossName(BossType.MAP1);
				break;
			case window_tutorial.DisplayType.BOSS_MAP_2:
				text = BossTypeUtil.GetTextCommonBossName(BossType.MAP2);
				break;
			case window_tutorial.DisplayType.BOSS_MAP_3:
				text = BossTypeUtil.GetTextCommonBossName(BossType.MAP3);
				break;
			}
			if (text != null)
			{
				if (this.m_textLabel != null)
				{
					this.m_textLabel.text = text;
				}
				if (this.m_shadowTextLabel != null)
				{
					this.m_shadowTextLabel.text = text;
				}
			}
		}
	}

	// Token: 0x0600233A RID: 9018 RVA: 0x000D3888 File Offset: 0x000D1A88
	private void OnClickOptionTutorialScroll()
	{
		if (this.m_scrollInfo != null)
		{
			if (this.m_scrollInfo.DispType == window_tutorial.DisplayType.TUTORIAL)
			{
				HudMenuUtility.SendMsgMenuSequenceToMainMenu(MsgMenuSequence.SequeneceType.STAGE);
			}
			else
			{
				if (this.m_window == null)
				{
					GameObject loadMenuChildObject = HudMenuUtility.GetLoadMenuChildObject("window_tutorial_other_character", true);
					if (loadMenuChildObject != null)
					{
						this.m_window = loadMenuChildObject.GetComponent<window_tutorial_other_character>();
					}
				}
				if (this.m_window != null)
				{
					this.m_window.SetScrollInfo(this.m_scrollInfo);
					this.m_window.PlayOpenWindow();
					this.m_openWindow = true;
					base.enabled = true;
					if (this.m_scrollInfo.Parent != null)
					{
						this.m_scrollInfo.Parent.SetCloseBtnColliderTrigger(true);
					}
				}
			}
		}
	}

	// Token: 0x04001FDD RID: 8157
	[SerializeField]
	private UILabel m_textLabel;

	// Token: 0x04001FDE RID: 8158
	[SerializeField]
	private UILabel m_shadowTextLabel;

	// Token: 0x04001FDF RID: 8159
	[SerializeField]
	private GameObject m_btnObj;

	// Token: 0x04001FE0 RID: 8160
	private window_tutorial_other_character m_window;

	// Token: 0x04001FE1 RID: 8161
	private window_tutorial.ScrollInfo m_scrollInfo;

	// Token: 0x04001FE2 RID: 8162
	private bool m_openWindow;
}
