using System;
using AnimationOrTween;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x020004AA RID: 1194
public class window_event_setting : WindowBase
{
	// Token: 0x170004BF RID: 1215
	// (get) Token: 0x0600234B RID: 9035 RVA: 0x000D41AC File Offset: 0x000D23AC
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x170004C0 RID: 1216
	// (get) Token: 0x0600234C RID: 9036 RVA: 0x000D41B4 File Offset: 0x000D23B4
	public bool IsOverwrite
	{
		get
		{
			return this.m_isOverwrite;
		}
	}

	// Token: 0x170004C1 RID: 1217
	// (get) Token: 0x0600234D RID: 9037 RVA: 0x000D41BC File Offset: 0x000D23BC
	public window_event_setting.State EndState
	{
		get
		{
			return this.m_State;
		}
	}

	// Token: 0x0600234E RID: 9038 RVA: 0x000D41C4 File Offset: 0x000D23C4
	private void Start()
	{
		OptionMenuUtility.TranObj(base.gameObject);
		base.enabled = false;
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
		}
		this.m_uiAnimation = base.gameObject.AddComponent<UIPlayAnimation>();
		if (this.m_uiAnimation != null)
		{
			Animation component2 = base.gameObject.GetComponent<Animation>();
			this.m_uiAnimation.target = component2;
			this.m_uiAnimation.clipName = "ui_menu_option_window_Anim";
		}
		SoundManager.SePlay("sys_window_open", "SE");
	}

	// Token: 0x0600234F RID: 9039 RVA: 0x000D42B0 File Offset: 0x000D24B0
	public void Setup(window_event_setting.TextType textType)
	{
		this.m_State = window_event_setting.State.EXEC;
		this.m_textType = textType;
		window_event_setting.TextType textType2 = this.m_textType;
		if (textType2 != window_event_setting.TextType.WEIGHT_SAVING)
		{
			if (textType2 == window_event_setting.TextType.FACEBOOK_ACCESS)
			{
				bool lightValue = this.IsLogin();
				this.UpdateButtonImage(lightValue, false);
				TextUtility.SetCommonText(this.m_headerTextLabel, "Option", "facebook_access");
				TextUtility.SetCommonText(this.m_headerSubTextLabel, "Option", "facebook_access_info");
				TextUtility.SetCommonText(this.m_ButtonOnTextLabel, "Option", "login");
				TextUtility.SetCommonText(this.m_ButtonOnSubTextLabel, "Option", "login");
				TextUtility.SetCommonText(this.m_ButtonOffTextLabel, "Option", "logout");
				TextUtility.SetCommonText(this.m_ButtonOffSubTextLabel, "Option", "logout");
			}
		}
		else
		{
			bool lightValue2 = this.IsLightMode();
			bool texValue = this.IsHighTexture();
			this.UpdateButtonImage(lightValue2, texValue);
			TextUtility.SetCommonText(this.m_headerTextLabel, "Option", "weight_saving");
			TextUtility.SetCommonText(this.m_headerSubTextLabel, "Option", "weight_saving_info");
			TextUtility.SetCommonText(this.m_ButtonOnTextLabel, "Option", "button_on");
			TextUtility.SetCommonText(this.m_ButtonOnSubTextLabel, "Option", "button_on");
			TextUtility.SetCommonText(this.m_ButtonOffTextLabel, "Option", "button_off");
			TextUtility.SetCommonText(this.m_ButtonOffSubTextLabel, "Option", "button_off");
		}
	}

	// Token: 0x06002350 RID: 9040 RVA: 0x000D4410 File Offset: 0x000D2610
	private void Update()
	{
		if (GeneralWindow.IsCreated("BackTitleSelect") && GeneralWindow.IsButtonPressed)
		{
			HudMenuUtility.SendMsgMenuSequenceToMainMenu(MsgMenuSequence.SequeneceType.TITLE);
			GeneralWindow.Close();
			base.enabled = false;
			this.PlayCloseAnimation();
		}
	}

	// Token: 0x06002351 RID: 9041 RVA: 0x000D4450 File Offset: 0x000D2650
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

	// Token: 0x06002352 RID: 9042 RVA: 0x000D44B4 File Offset: 0x000D26B4
	private bool IsLightMode()
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				return systemdata.lightMode;
			}
		}
		return false;
	}

	// Token: 0x06002353 RID: 9043 RVA: 0x000D44E8 File Offset: 0x000D26E8
	private bool IsHighTexture()
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				return systemdata.highTexture;
			}
		}
		return false;
	}

	// Token: 0x06002354 RID: 9044 RVA: 0x000D451C File Offset: 0x000D271C
	private bool IsLogin()
	{
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		return socialInterface != null && socialInterface.IsLoggedIn;
	}

	// Token: 0x06002355 RID: 9045 RVA: 0x000D4548 File Offset: 0x000D2748
	private void UpdatePosition()
	{
		if (this.m_liteButton == null || this.m_textureButton == null)
		{
			return;
		}
		if (this.m_textType == window_event_setting.TextType.WEIGHT_SAVING)
		{
			if (this.m_initY == -3000f)
			{
				this.m_initY = this.m_liteButton.transform.localPosition.y;
			}
			this.m_liteButton.transform.localPosition = new Vector3(this.m_liteButton.transform.localPosition.x, this.m_initY, this.m_liteButton.transform.localPosition.x);
			this.m_textureButton.SetActive(true);
		}
		else
		{
			if (this.m_initY == -3000f)
			{
				this.m_initY = this.m_liteButton.transform.localPosition.y;
			}
			float y = this.m_initY + (this.m_textureButton.transform.localPosition.y - this.m_initY) * 0.5f;
			this.m_liteButton.transform.localPosition = new Vector3(this.m_liteButton.transform.localPosition.x, y, this.m_liteButton.transform.localPosition.x);
			this.m_textureButton.SetActive(false);
		}
	}

	// Token: 0x06002356 RID: 9046 RVA: 0x000D46C0 File Offset: 0x000D28C0
	private void UpdateButtonImage(bool lightValue, bool texValue)
	{
		this.UpdatePosition();
		window_event_setting.TextType textType = this.m_textType;
		if (textType != window_event_setting.TextType.WEIGHT_SAVING)
		{
			if (textType == window_event_setting.TextType.FACEBOOK_ACCESS)
			{
				if (this.m_onButtonLite != null)
				{
					if (lightValue)
					{
						this.m_onButtonLite.gameObject.SetActive(false);
					}
					else
					{
						this.m_onButtonLite.gameObject.SetActive(true);
					}
				}
				if (this.m_offButtonLite != null)
				{
					if (!lightValue)
					{
						this.m_offButtonLite.gameObject.SetActive(false);
					}
					else
					{
						this.m_offButtonLite.gameObject.SetActive(true);
					}
				}
			}
		}
		else
		{
			if (this.m_onButtonTex != null)
			{
				if (texValue)
				{
					this.m_onButtonTex.gameObject.SetActive(false);
				}
				else
				{
					this.m_onButtonTex.gameObject.SetActive(true);
				}
			}
			if (this.m_offButtonTex != null)
			{
				if (!texValue)
				{
					this.m_offButtonTex.gameObject.SetActive(false);
				}
				else
				{
					this.m_offButtonTex.gameObject.SetActive(true);
				}
			}
			if (this.m_onButtonLite != null)
			{
				if (lightValue)
				{
					this.m_onButtonLite.gameObject.SetActive(false);
				}
				else
				{
					this.m_onButtonLite.gameObject.SetActive(true);
				}
			}
			if (this.m_offButtonLite != null)
			{
				if (!lightValue)
				{
					this.m_offButtonLite.gameObject.SetActive(false);
				}
				else
				{
					this.m_offButtonLite.gameObject.SetActive(true);
				}
			}
		}
	}

	// Token: 0x06002357 RID: 9047 RVA: 0x000D486C File Offset: 0x000D2A6C
	private void SaveSystemData(bool lightModeFlag)
	{
		bool flag = this.IsLightMode();
		if (flag != lightModeFlag)
		{
			this.m_isOverwrite = true;
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null)
				{
					systemdata.lightMode = lightModeFlag;
				}
			}
		}
	}

	// Token: 0x06002358 RID: 9048 RVA: 0x000D48B4 File Offset: 0x000D2AB4
	private void SaveSystemDataTex(bool texModeFlag)
	{
		bool flag = this.IsHighTexture();
		if (flag != texModeFlag)
		{
			this.m_isOverwrite = true;
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null)
				{
					systemdata.highTexture = texModeFlag;
				}
			}
		}
	}

	// Token: 0x06002359 RID: 9049 RVA: 0x000D48FC File Offset: 0x000D2AFC
	private void OnClickOnButton()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		window_event_setting.TextType textType = this.m_textType;
		if (textType != window_event_setting.TextType.WEIGHT_SAVING)
		{
			if (textType == window_event_setting.TextType.FACEBOOK_ACCESS)
			{
				this.m_State = window_event_setting.State.PRESS_LOGIN;
				this.PlayCloseAnimation();
			}
		}
		else
		{
			this.SaveSystemData(true);
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "BackTitleSelect",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextUtility.GetCommonText("MainMenu", "back_title_caption"),
				message = TextUtility.GetCommonText("Option", "weight_saving_back_title_text")
			});
			base.enabled = true;
		}
	}

	// Token: 0x0600235A RID: 9050 RVA: 0x000D49A8 File Offset: 0x000D2BA8
	private void OnClickOffButton()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		window_event_setting.TextType textType = this.m_textType;
		if (textType != window_event_setting.TextType.WEIGHT_SAVING)
		{
			if (textType == window_event_setting.TextType.FACEBOOK_ACCESS)
			{
				this.m_State = window_event_setting.State.PRESS_LOGOUT;
				this.PlayCloseAnimation();
			}
		}
		else
		{
			this.SaveSystemData(false);
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "BackTitleSelect",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextUtility.GetCommonText("MainMenu", "back_title_caption"),
				message = TextUtility.GetCommonText("Option", "weight_saving_back_title_text")
			});
			base.enabled = true;
		}
	}

	// Token: 0x0600235B RID: 9051 RVA: 0x000D4A54 File Offset: 0x000D2C54
	private void OnClickTexOnButton()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		window_event_setting.TextType textType = this.m_textType;
		if (textType != window_event_setting.TextType.WEIGHT_SAVING)
		{
			if (textType == window_event_setting.TextType.FACEBOOK_ACCESS)
			{
				this.PlayCloseAnimation();
			}
		}
		else
		{
			this.SaveSystemDataTex(true);
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "BackTitleSelect",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextUtility.GetCommonText("MainMenu", "back_title_caption"),
				message = TextUtility.GetCommonText("Option", "weight_saving_back_title_text")
			});
			base.enabled = true;
		}
	}

	// Token: 0x0600235C RID: 9052 RVA: 0x000D4AF8 File Offset: 0x000D2CF8
	private void OnClickTexOffButton()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		window_event_setting.TextType textType = this.m_textType;
		if (textType != window_event_setting.TextType.WEIGHT_SAVING)
		{
			if (textType == window_event_setting.TextType.FACEBOOK_ACCESS)
			{
				this.PlayCloseAnimation();
			}
		}
		else
		{
			this.SaveSystemDataTex(false);
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "BackTitleSelect",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextUtility.GetCommonText("MainMenu", "back_title_caption"),
				message = TextUtility.GetCommonText("Option", "weight_saving_back_title_text")
			});
			base.enabled = true;
		}
	}

	// Token: 0x0600235D RID: 9053 RVA: 0x000D4B9C File Offset: 0x000D2D9C
	private void OnClickCloseButton()
	{
		this.m_State = window_event_setting.State.CLOSE;
		this.PlayCloseAnimation();
	}

	// Token: 0x0600235E RID: 9054 RVA: 0x000D4BAC File Offset: 0x000D2DAC
	private void OnFinishedAnimationCallback()
	{
		this.m_isEnd = true;
	}

	// Token: 0x0600235F RID: 9055 RVA: 0x000D4BB8 File Offset: 0x000D2DB8
	public void PlayOpenWindow()
	{
		this.m_isEnd = false;
		if (this.m_uiAnimation != null)
		{
			this.m_uiAnimation.Play(true);
			base.enabled = false;
		}
	}

	// Token: 0x06002360 RID: 9056 RVA: 0x000D4BE8 File Offset: 0x000D2DE8
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

	// Token: 0x0400200C RID: 8204
	[SerializeField]
	private GameObject m_closeBtn;

	// Token: 0x0400200D RID: 8205
	[SerializeField]
	private GameObject m_liteButton;

	// Token: 0x0400200E RID: 8206
	[SerializeField]
	private GameObject m_textureButton;

	// Token: 0x0400200F RID: 8207
	[SerializeField]
	private UIImageButton m_onButtonLite;

	// Token: 0x04002010 RID: 8208
	[SerializeField]
	private UIImageButton m_offButtonLite;

	// Token: 0x04002011 RID: 8209
	[SerializeField]
	private UIImageButton m_onButtonTex;

	// Token: 0x04002012 RID: 8210
	[SerializeField]
	private UIImageButton m_offButtonTex;

	// Token: 0x04002013 RID: 8211
	private window_event_setting.TextType m_textType;

	// Token: 0x04002014 RID: 8212
	private window_event_setting.State m_State;

	// Token: 0x04002015 RID: 8213
	[SerializeField]
	private UILabel m_headerTextLabel;

	// Token: 0x04002016 RID: 8214
	[SerializeField]
	private UILabel m_headerSubTextLabel;

	// Token: 0x04002017 RID: 8215
	[SerializeField]
	private UILabel m_ButtonOnTextLabel;

	// Token: 0x04002018 RID: 8216
	[SerializeField]
	private UILabel m_ButtonOnSubTextLabel;

	// Token: 0x04002019 RID: 8217
	[SerializeField]
	private UILabel m_ButtonOffTextLabel;

	// Token: 0x0400201A RID: 8218
	[SerializeField]
	private UILabel m_ButtonOffSubTextLabel;

	// Token: 0x0400201B RID: 8219
	private UIPlayAnimation m_uiAnimation;

	// Token: 0x0400201C RID: 8220
	private bool m_isEnd;

	// Token: 0x0400201D RID: 8221
	private bool m_isOverwrite;

	// Token: 0x0400201E RID: 8222
	private float m_initY = -3000f;

	// Token: 0x020004AB RID: 1195
	public enum TextType
	{
		// Token: 0x04002020 RID: 8224
		WEIGHT_SAVING,
		// Token: 0x04002021 RID: 8225
		FACEBOOK_ACCESS
	}

	// Token: 0x020004AC RID: 1196
	public enum State
	{
		// Token: 0x04002023 RID: 8227
		EXEC,
		// Token: 0x04002024 RID: 8228
		PRESS_LOGIN,
		// Token: 0x04002025 RID: 8229
		PRESS_LOGOUT,
		// Token: 0x04002026 RID: 8230
		CLOSE
	}
}
