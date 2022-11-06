using System;
using AnimationOrTween;
using App;
using Message;
using UnityEngine;

// Token: 0x02000458 RID: 1112
public class HudPageTurner : MonoBehaviour
{
	// Token: 0x17000484 RID: 1156
	// (get) Token: 0x06002168 RID: 8552 RVA: 0x000C90D0 File Offset: 0x000C72D0
	// (set) Token: 0x06002169 RID: 8553 RVA: 0x000C90D8 File Offset: 0x000C72D8
	public bool TutorialFlag { get; set; }

	// Token: 0x0600216A RID: 8554 RVA: 0x000C90E4 File Offset: 0x000C72E4
	private void Start()
	{
	}

	// Token: 0x0600216B RID: 8555 RVA: 0x000C90E8 File Offset: 0x000C72E8
	private void OnDestroy()
	{
		if (this.m_bg_ui_tex != null)
		{
			this.m_bg_ui_tex.material.mainTexture = null;
			this.m_bg_ui_tex.material = null;
			this.m_bg_ui_tex.mainTexture = null;
		}
	}

	// Token: 0x0600216C RID: 8556 RVA: 0x000C9130 File Offset: 0x000C7330
	private void Initialize()
	{
		if (this.m_initEnd)
		{
			return;
		}
		GameObject mainMenuUIObject = HudMenuUtility.GetMainMenuUIObject();
		if (mainMenuUIObject != null)
		{
			Transform transform = mainMenuUIObject.transform.FindChild("Anchor_4_ML/mainmenu_grip_L/Btn_mainmenu_pager_L");
			if (transform != null)
			{
				this.m_left_button = transform.gameObject;
				this.SetButtonCommponent(this.m_left_button);
			}
			Transform transform2 = mainMenuUIObject.transform.FindChild("Anchor_5_MC/mainmenu_grip/Btn_pager/Btn_mainmenu_pager_L");
			if (transform2 != null)
			{
				this.m_leftPageBtn = transform2.gameObject;
				this.SetButtonCommponent(this.m_leftPageBtn);
				Transform transform3 = transform2.FindChild("img_pager_icon_l");
				if (transform3 != null)
				{
					this.m_left_icon = transform3.gameObject.GetComponent<UISprite>();
				}
			}
			Transform transform4 = mainMenuUIObject.transform.FindChild("Anchor_6_MR/mainmenu_grip_R/Btn_mainmenu_pager_R");
			if (transform4 != null)
			{
				this.m_right_button = transform4.gameObject;
				this.SetButtonCommponent(this.m_right_button);
			}
			Transform transform5 = mainMenuUIObject.transform.FindChild("Anchor_5_MC/mainmenu_grip/Btn_pager/Btn_mainmenu_pager_R");
			if (transform5 != null)
			{
				this.m_rightPageBtn = transform5.gameObject;
				this.SetButtonCommponent(this.m_rightPageBtn);
				Transform transform6 = transform5.FindChild("img_pager_icon_r");
				if (transform6 != null)
				{
					this.m_right_icon = transform6.gameObject.GetComponent<UISprite>();
				}
			}
			GameObject gameObject = mainMenuUIObject.transform.FindChild("Anchor_5_MC/mainmenu_grip/mainmenu_SB").gameObject;
			if (gameObject != null)
			{
				this.m_scroll_bar = gameObject.GetComponent<UIScrollBar>();
				if (this.m_scroll_bar != null)
				{
					EventDelegate.Add(this.m_scroll_bar.onChange, new EventDelegate.Callback(this.OnChangeScrollBarValue));
				}
			}
			GameObject gameObject2 = mainMenuUIObject.transform.FindChild("Anchor_5_MC/mainmenu_grip/mainmenu_Slider").gameObject;
			if (gameObject2 != null)
			{
				this.m_slider = gameObject2.GetComponent<UISlider>();
			}
			Transform transform7 = mainMenuUIObject.transform.FindChild("Anchor_5_MC/mainmenu_contents");
			if (transform7 != null)
			{
				GameObject gameObject3 = transform7.gameObject;
				if (gameObject3 != null)
				{
					this.m_contents_panel = gameObject3.GetComponent<UIPanel>();
				}
			}
			this.m_bg = mainMenuUIObject.transform.FindChild("Anchor_5_MC/mainmenu_grip/custom_bg").gameObject;
			if (this.m_bg != null)
			{
				this.m_bg_animation = this.m_bg.GetComponent<Animation>();
				GameObject gameObject4 = this.m_bg.transform.FindChild("img_stage_tex").gameObject;
				if (gameObject4 != null)
				{
					this.m_bg_ui_tex = gameObject4.GetComponent<UITexture>();
				}
			}
		}
		this.SetHeader(this.m_page_number);
		this.SetIcon(this.m_page_number);
		this.m_initEnd = true;
	}

	// Token: 0x0600216D RID: 8557 RVA: 0x000C93F0 File Offset: 0x000C75F0
	private void SetButtonCommponent(GameObject obj)
	{
		if (obj != null)
		{
			UIButtonMessage uibuttonMessage = obj.GetComponent<UIButtonMessage>();
			if (uibuttonMessage == null)
			{
				uibuttonMessage = obj.AddComponent<UIButtonMessage>();
			}
			if (uibuttonMessage != null)
			{
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "OnClickButtonCallBack";
			}
		}
	}

	// Token: 0x0600216E RID: 8558 RVA: 0x000C9448 File Offset: 0x000C7648
	private void ChangeNextPage(bool left_page_flag)
	{
		if (this.m_scroll_bar != null)
		{
			if (left_page_flag)
			{
				if (this.m_page_number == 2U)
				{
					this.m_scroll_bar.value = 0.5f;
				}
				else if (this.m_page_number == 1U)
				{
					this.m_scroll_bar.value = 0f;
					this.SetVisible(this.m_left_button, false);
					this.SetVisible(this.m_leftPageBtn, false);
				}
				this.SetVisible(this.m_right_button, true);
				this.SetVisible(this.m_rightPageBtn, true);
			}
			else
			{
				if (this.m_page_number == 0U)
				{
					this.m_scroll_bar.value = 0.5f;
				}
				else if (this.m_page_number == 1U)
				{
					this.m_scroll_bar.value = 1f;
					this.SetVisible(this.m_right_button, false);
					this.SetVisible(this.m_rightPageBtn, false);
				}
				this.SetVisible(this.m_left_button, true);
				this.SetVisible(this.m_leftPageBtn, true);
			}
			if (this.m_scroll_bar.onDragFinished != null)
			{
				this.m_scroll_bar.onDragFinished();
			}
		}
	}

	// Token: 0x0600216F RID: 8559 RVA: 0x000C9574 File Offset: 0x000C7774
	private void OnChangeScrollBarValue()
	{
		if (this.m_slider != null)
		{
			float value = this.m_scroll_bar.value;
			if (App.Math.NearZero(value, 1E-06f))
			{
				if (this.m_page_number != 0U)
				{
					this.m_page_number = 0U;
					this.SetHeader(this.m_page_number);
					this.SetIcon(this.m_page_number);
					this.SetBG(this.m_page_number);
					this.m_slider.value = 0f;
					SoundManager.SePlay("sys_page_skip", "SE");
				}
				this.SetVisible(this.m_left_button, false);
				this.SetVisible(this.m_leftPageBtn, false);
				this.SetVisible(this.m_right_button, true);
				this.SetVisible(this.m_rightPageBtn, true);
			}
			else if (Mathf.Abs(value - 0.5f) < 0.02f)
			{
				if (this.m_page_number != 1U)
				{
					this.m_page_number = 1U;
					this.SetHeader(this.m_page_number);
					this.SetIcon(this.m_page_number);
					this.SetBG(this.m_page_number);
					this.m_slider.value = 0.5f;
					this.m_scroll_bar.value = 0.5f;
					SoundManager.SePlay("sys_page_skip", "SE");
				}
				this.SetVisible(this.m_left_button, true);
				this.SetVisible(this.m_leftPageBtn, true);
				this.SetVisible(this.m_right_button, true);
				this.SetVisible(this.m_rightPageBtn, true);
			}
			else if (App.Math.NearZero(value - 1f, 1E-06f))
			{
				if (this.m_page_number != 2U)
				{
					this.m_page_number = 2U;
					this.SetHeader(this.m_page_number);
					this.SetIcon(this.m_page_number);
					this.SetBG(this.m_page_number);
					this.m_slider.value = 1f;
					SoundManager.SePlay("sys_page_skip", "SE");
				}
				this.SetVisible(this.m_left_button, true);
				this.SetVisible(this.m_leftPageBtn, true);
				this.SetVisible(this.m_right_button, false);
				this.SetVisible(this.m_rightPageBtn, false);
			}
			if (this.m_contents_panel != null)
			{
				Vector4 clipRange = this.m_contents_panel.clipRange;
				clipRange.y = 0f;
				this.m_contents_panel.clipRange = clipRange;
			}
		}
	}

	// Token: 0x06002170 RID: 8560 RVA: 0x000C97CC File Offset: 0x000C79CC
	private void OnClickButtonCallBack(GameObject obj)
	{
		if (obj.name == "Btn_mainmenu_pager_L" || obj.name == "Btn_mainmenu_pager_L")
		{
			this.ChangeNextPage(true);
		}
		else if (obj.name == "Btn_mainmenu_pager_R" || obj.name == "Btn_mainmenu_pager_R")
		{
			this.ChangeNextPage(false);
			if (this.TutorialFlag)
			{
				HudMenuUtility.SendMsgMenuSequenceToMainMenu(MsgMenuSequence.SequeneceType.TUTORIAL_PAGE_MOVE);
			}
		}
	}

	// Token: 0x06002171 RID: 8561 RVA: 0x000C9854 File Offset: 0x000C7A54
	private void SetVisible(GameObject obj, bool flag)
	{
		if (obj != null)
		{
			obj.SetActive(flag);
		}
	}

	// Token: 0x06002172 RID: 8562 RVA: 0x000C986C File Offset: 0x000C7A6C
	private void SetPage(uint page)
	{
		this.SetHeader(page);
		this.SetBG(page);
		this.SetIcon(page);
	}

	// Token: 0x06002173 RID: 8563 RVA: 0x000C9884 File Offset: 0x000C7A84
	private void SetHeader(uint page)
	{
		if (page >= 0U && page <= 2U)
		{
			uint num = page + 1U;
			HudMenuUtility.SendChangeHeaderText("ui_Header_MainPage" + num);
		}
	}

	// Token: 0x06002174 RID: 8564 RVA: 0x000C98B8 File Offset: 0x000C7AB8
	private void SetBG(uint page)
	{
		if (this.m_bg != null && this.m_bg_animation != null && this.m_bg_ui_tex != null)
		{
			if (page == 1U)
			{
				if (!this.m_bg_anim)
				{
					this.m_bg.SetActive(true);
					this.m_bg_ui_tex.material.mainTexture = MileageMapUtility.GetBGTexture();
					ActiveAnimation.Play(this.m_bg_animation, "ui_mm_mileage_bg_Anim", Direction.Forward);
					this.m_bg_anim = true;
				}
			}
			else if (this.m_bg_anim)
			{
				ActiveAnimation.Play(this.m_bg_animation, "ui_mm_mileage_bg_Anim", Direction.Reverse);
				this.m_bg_anim = false;
			}
		}
	}

	// Token: 0x06002175 RID: 8565 RVA: 0x000C9970 File Offset: 0x000C7B70
	private void SetIcon(uint page)
	{
		switch (page)
		{
		case 0U:
			this.SetIcon(this.m_right_icon, "ui_mm_pager_icon_1");
			break;
		case 1U:
			this.SetIcon(this.m_left_icon, "ui_mm_pager_icon_0");
			this.SetIcon(this.m_right_icon, "ui_mm_pager_icon_2");
			break;
		case 2U:
			this.SetIcon(this.m_left_icon, "ui_mm_pager_icon_1");
			break;
		}
	}

	// Token: 0x06002176 RID: 8566 RVA: 0x000C99EC File Offset: 0x000C7BEC
	private void SetIcon(UISprite uiSprite, string name)
	{
		if (uiSprite != null)
		{
			uiSprite.spriteName = name;
		}
	}

	// Token: 0x06002177 RID: 8567 RVA: 0x000C9A04 File Offset: 0x000C7C04
	private void OnSendChangeMainPageHeaderText()
	{
		this.SetPage(this.m_page_number);
	}

	// Token: 0x06002178 RID: 8568 RVA: 0x000C9A14 File Offset: 0x000C7C14
	private void SetMileageMapProduction()
	{
		this.m_page_number = 1U;
		this.SetPage(this.m_page_number);
		this.m_scroll_bar.value = 0.5f;
		this.m_slider.value = 0.5f;
		if (this.m_scroll_bar.onDragFinished != null)
		{
			this.m_scroll_bar.onDragFinished();
		}
	}

	// Token: 0x06002179 RID: 8569 RVA: 0x000C9A74 File Offset: 0x000C7C74
	public void OnSetBGTexture()
	{
		if (this.m_bg_ui_tex != null)
		{
			this.m_bg_ui_tex.material.mainTexture = MileageMapUtility.GetBGTexture();
		}
	}

	// Token: 0x0600217A RID: 8570 RVA: 0x000C9AA8 File Offset: 0x000C7CA8
	public void OnNormalDisplay()
	{
		this.Initialize();
		this.SetMileageMapProduction();
	}

	// Token: 0x0600217B RID: 8571 RVA: 0x000C9AB8 File Offset: 0x000C7CB8
	private void OnStartMileageMapProduction()
	{
		this.Initialize();
		this.SetMileageMapProduction();
		this.SetColliderTrigger(this.m_left_button, true);
		this.SetColliderTrigger(this.m_right_button, true);
	}

	// Token: 0x0600217C RID: 8572 RVA: 0x000C9AEC File Offset: 0x000C7CEC
	private void OnStartRankingProduction()
	{
		this.Initialize();
		this.m_page_number = 0U;
		this.SetPage(this.m_page_number);
		this.m_scroll_bar.value = 0f;
		this.m_slider.value = 0f;
		this.SetVisible(this.m_left_button, false);
		this.SetVisible(this.m_leftPageBtn, false);
		this.SetVisible(this.m_right_button, false);
		this.SetVisible(this.m_rightPageBtn, false);
		if (this.m_scroll_bar.onDragFinished != null)
		{
			this.m_scroll_bar.onDragFinished();
		}
		this.SetColliderTrigger(this.m_left_button, true);
		this.SetColliderTrigger(this.m_right_button, true);
	}

	// Token: 0x0600217D RID: 8573 RVA: 0x000C9BA0 File Offset: 0x000C7DA0
	private void OnSetPlayerChaoSetPage()
	{
		this.Initialize();
		this.m_scroll_bar.value = 1f;
		if (this.m_bg)
		{
			this.m_bg.SetActive(false);
		}
		if (this.m_scroll_bar.onDragFinished != null)
		{
			this.m_scroll_bar.onDragFinished();
		}
	}

	// Token: 0x0600217E RID: 8574 RVA: 0x000C9C00 File Offset: 0x000C7E00
	private void OnEndMileageMapProduction()
	{
		this.SetColliderTrigger(this.m_left_button, false);
		this.SetColliderTrigger(this.m_right_button, false);
	}

	// Token: 0x0600217F RID: 8575 RVA: 0x000C9C1C File Offset: 0x000C7E1C
	private void SetColliderTrigger(GameObject obj, bool trigger)
	{
		if (obj != null)
		{
			BoxCollider component = obj.GetComponent<BoxCollider>();
			if (component != null)
			{
				component.isTrigger = trigger;
			}
		}
	}

	// Token: 0x04001E36 RID: 7734
	private const string LEFT_BUTTON_PATH = "Anchor_4_ML/mainmenu_grip_L/";

	// Token: 0x04001E37 RID: 7735
	private const string RIGHT_BUTTON_PATH = "Anchor_6_MR/mainmenu_grip_R/";

	// Token: 0x04001E38 RID: 7736
	private const string SCROLL_BAR_PATH = "Anchor_5_MC/mainmenu_grip/mainmenu_SB";

	// Token: 0x04001E39 RID: 7737
	private const string SLIDER_PATH = "Anchor_5_MC/mainmenu_grip/mainmenu_Slider";

	// Token: 0x04001E3A RID: 7738
	private const string CONTENTS_PATH = "Anchor_5_MC/mainmenu_contents";

	// Token: 0x04001E3B RID: 7739
	private const string LEFT_BUTTON_NAME = "Btn_mainmenu_pager_L";

	// Token: 0x04001E3C RID: 7740
	private const string RIGHT_BUTTON_NAME = "Btn_mainmenu_pager_R";

	// Token: 0x04001E3D RID: 7741
	private const string LEFT_ICON_NAME = "img_pager_icon_l";

	// Token: 0x04001E3E RID: 7742
	private const string RIGHT_ICON_NAME = "img_pager_icon_r";

	// Token: 0x04001E3F RID: 7743
	private const string PAGE_BTN_PATH = "Anchor_5_MC/mainmenu_grip/Btn_pager/";

	// Token: 0x04001E40 RID: 7744
	private const string LEFT_PAGE_BTN_NAME = "Btn_mainmenu_pager_L";

	// Token: 0x04001E41 RID: 7745
	private const string RIGHT_PAGE_BTN_NAME = "Btn_mainmenu_pager_R";

	// Token: 0x04001E42 RID: 7746
	private const string SE_NAME = "sys_page_skip";

	// Token: 0x04001E43 RID: 7747
	private const string BG_PATH = "Anchor_5_MC/mainmenu_grip/custom_bg";

	// Token: 0x04001E44 RID: 7748
	private const string BG_ANIM = "ui_mm_mileage_bg_Anim";

	// Token: 0x04001E45 RID: 7749
	private const float PAGE_STEP_VALUE = 0.5f;

	// Token: 0x04001E46 RID: 7750
	private GameObject m_left_button;

	// Token: 0x04001E47 RID: 7751
	private GameObject m_right_button;

	// Token: 0x04001E48 RID: 7752
	private GameObject m_leftPageBtn;

	// Token: 0x04001E49 RID: 7753
	private GameObject m_rightPageBtn;

	// Token: 0x04001E4A RID: 7754
	private GameObject m_bg;

	// Token: 0x04001E4B RID: 7755
	private UITexture m_bg_ui_tex;

	// Token: 0x04001E4C RID: 7756
	private Animation m_bg_animation;

	// Token: 0x04001E4D RID: 7757
	private UIScrollBar m_scroll_bar;

	// Token: 0x04001E4E RID: 7758
	private UISlider m_slider;

	// Token: 0x04001E4F RID: 7759
	private UIPanel m_contents_panel;

	// Token: 0x04001E50 RID: 7760
	private UISprite m_left_icon;

	// Token: 0x04001E51 RID: 7761
	private UISprite m_right_icon;

	// Token: 0x04001E52 RID: 7762
	private uint m_page_number;

	// Token: 0x04001E53 RID: 7763
	private bool m_bg_anim;

	// Token: 0x04001E54 RID: 7764
	private bool m_initEnd;
}
