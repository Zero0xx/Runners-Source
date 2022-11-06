using System;
using Text;
using UnityEngine;

// Token: 0x02000534 RID: 1332
public class SettingPartsTakeoverPassword : SettingBase
{
	// Token: 0x1700056F RID: 1391
	// (get) Token: 0x06002923 RID: 10531 RVA: 0x000FE6EC File Offset: 0x000FC8EC
	// (set) Token: 0x06002924 RID: 10532 RVA: 0x000FE71C File Offset: 0x000FC91C
	public string InputText
	{
		get
		{
			if (this.m_input == null)
			{
				return string.Empty;
			}
			return this.m_input.value;
		}
		private set
		{
		}
	}

	// Token: 0x17000570 RID: 1392
	// (get) Token: 0x06002925 RID: 10533 RVA: 0x000FE720 File Offset: 0x000FC920
	public UILabel TextLabel
	{
		get
		{
			return this.m_label;
		}
	}

	// Token: 0x17000571 RID: 1393
	// (get) Token: 0x06002926 RID: 10534 RVA: 0x000FE728 File Offset: 0x000FC928
	// (set) Token: 0x06002927 RID: 10535 RVA: 0x000FE73C File Offset: 0x000FC93C
	public bool IsDecided
	{
		get
		{
			return this.m_inputState == SettingPartsTakeoverPassword.InputState.DECIDED;
		}
		private set
		{
		}
	}

	// Token: 0x17000572 RID: 1394
	// (get) Token: 0x06002928 RID: 10536 RVA: 0x000FE740 File Offset: 0x000FC940
	// (set) Token: 0x06002929 RID: 10537 RVA: 0x000FE754 File Offset: 0x000FC954
	public bool IsCanceled
	{
		get
		{
			return this.m_inputState == SettingPartsTakeoverPassword.InputState.CANCELED;
		}
		private set
		{
		}
	}

	// Token: 0x0600292A RID: 10538 RVA: 0x000FE758 File Offset: 0x000FC958
	public void SetCancelButtonUseFlag(bool useFlag)
	{
		this.m_calcelButtonUseFlag = useFlag;
	}

	// Token: 0x0600292B RID: 10539 RVA: 0x000FE764 File Offset: 0x000FC964
	public void SetOkButtonEnabled(bool enabled)
	{
		UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(this.m_object, "Btn_ok");
		if (uiimageButton != null)
		{
			uiimageButton.isEnabled = enabled;
		}
	}

	// Token: 0x0600292C RID: 10540 RVA: 0x000FE798 File Offset: 0x000FC998
	private void OnDestroy()
	{
		if (this.m_object != null)
		{
			UnityEngine.Object.Destroy(this.m_object);
		}
	}

	// Token: 0x0600292D RID: 10541 RVA: 0x000FE7B8 File Offset: 0x000FC9B8
	protected override void OnSetup(string anchorPath)
	{
		if (!this.m_isLoaded)
		{
			this.m_anchorPath = this.ExcludePathName;
			this.m_state = SettingPartsTakeoverPassword.State.STATE_LOAD;
		}
	}

	// Token: 0x0600292E RID: 10542 RVA: 0x000FE7D8 File Offset: 0x000FC9D8
	private void SetupWindowData()
	{
		this.m_object = HudMenuUtility.GetLoadMenuChildObject("window_password_setting", true);
		if (this.m_object != null)
		{
			GameObject gameObject = GameObject.Find(this.m_anchorPath);
			if (gameObject != null)
			{
				Vector3 localPosition = new Vector3(0f, 0f, 0f);
				Vector3 localScale = this.m_object.transform.localScale;
				this.m_object.transform.parent = gameObject.transform;
				this.m_object.transform.localPosition = localPosition;
				this.m_object.transform.localScale = localScale;
			}
			this.m_input = GameObjectUtil.FindChildGameObjectComponent<UIInput>(this.m_object, "Input_password");
			EventDelegate item = new EventDelegate(new EventDelegate.Callback(this.OnFinishedInput));
			this.m_input.onSubmit.Add(item);
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_object, "Btn_ok");
			if (gameObject2 != null)
			{
				UIButtonMessage uibuttonMessage = gameObject2.AddComponent<UIButtonMessage>();
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "OnClickOkButton";
				UIPlayAnimation component = gameObject2.GetComponent<UIPlayAnimation>();
				if (component != null)
				{
					EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.OnFinishedAnimation), false);
				}
			}
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(this.m_object, "Btn_close");
			if (gameObject3 != null)
			{
				if (!this.m_calcelButtonUseFlag)
				{
					gameObject3.SetActive(false);
				}
				else
				{
					gameObject3.SetActive(true);
					UIButtonMessage uibuttonMessage2 = gameObject3.AddComponent<UIButtonMessage>();
					uibuttonMessage2.target = base.gameObject;
					uibuttonMessage2.functionName = "OnClickCancelButton";
					window_name_setting component2 = this.m_object.GetComponent<window_name_setting>();
					if (component2 == null)
					{
						this.m_object.AddComponent<window_name_setting>();
					}
				}
				UIPlayAnimation component3 = gameObject3.GetComponent<UIPlayAnimation>();
				if (component3 != null)
				{
					EventDelegate.Add(component3.onFinished, new EventDelegate.Callback(this.OnFinishedAnimation), false);
				}
			}
			GameObject gameObject4 = GameObjectUtil.FindChildGameObject(this.m_object, "Lbl_name_setting");
			if (gameObject4 != null)
			{
				UILabel component4 = gameObject4.GetComponent<UILabel>();
				if (component4 != null)
				{
					TextUtility.SetCommonText(component4, "Option", "take_over_password_setting");
				}
			}
			GameObject gameObject5 = GameObjectUtil.FindChildGameObject(this.m_object, "Lbl_name_setting_sub");
			if (gameObject5 != null)
			{
				UILabel component5 = gameObject5.GetComponent<UILabel>();
				if (component5 != null)
				{
					TextUtility.SetCommonText(component5, "Option", "take_over_password_setting_info");
				}
			}
			GameObject gameObject6 = GameObjectUtil.FindChildGameObject(this.m_object, "Lbl_input_password");
			if (gameObject6 != null)
			{
				this.m_label = gameObject6.GetComponent<UILabel>();
				if (this.m_label != null)
				{
					TextUtility.SetCommonText(this.m_label, "Option", "take_over_password_input");
				}
			}
			this.m_uiAnimation = base.gameObject.AddComponent<UIPlayAnimation>();
			if (this.m_uiAnimation != null)
			{
				Animation component6 = this.m_object.GetComponent<Animation>();
				this.m_uiAnimation.target = component6;
				this.m_uiAnimation.clipName = "ui_menu_option_window_Anim";
			}
		}
	}

	// Token: 0x0600292F RID: 10543 RVA: 0x000FEB08 File Offset: 0x000FCD08
	protected override void OnPlayStart()
	{
		if (this.m_isLoaded)
		{
			this.m_playStartCue = false;
			if (this.m_object != null)
			{
				this.m_object.SetActive(true);
			}
			if (this.m_uiAnimation != null)
			{
				this.m_uiAnimation.Play(true);
			}
			this.m_inputState = SettingPartsTakeoverPassword.InputState.INPUTTING;
			this.m_state = SettingPartsTakeoverPassword.State.STATE_SETTING;
			UIInput uiinput = GameObjectUtil.FindChildGameObjectComponent<UIInput>(this.m_object, "Input_password");
			if (uiinput != null)
			{
				uiinput.value = string.Empty;
			}
			if (this.m_label != null)
			{
				TextUtility.SetCommonText(this.m_label, "Option", "take_over_password_input");
			}
			SoundManager.SePlay("sys_window_open", "SE");
		}
		else
		{
			this.m_playStartCue = true;
		}
	}

	// Token: 0x06002930 RID: 10544 RVA: 0x000FEBDC File Offset: 0x000FCDDC
	protected override bool OnIsEndPlay()
	{
		return this.m_state == SettingPartsTakeoverPassword.State.STATE_END;
	}

	// Token: 0x06002931 RID: 10545 RVA: 0x000FEBF0 File Offset: 0x000FCDF0
	protected override void OnUpdate()
	{
		switch (this.m_state)
		{
		case SettingPartsTakeoverPassword.State.STATE_LOAD:
			this.m_isLoaded = true;
			this.SetupWindowData();
			if (this.m_playStartCue)
			{
				this.OnPlayStart();
			}
			break;
		case SettingPartsTakeoverPassword.State.STATE_SETTING:
			if (this.m_input.selected)
			{
				string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Option", "take_over_password_input").text;
				UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_object, "Lbl_input_password");
				UIInput uiinput = GameObjectUtil.FindChildGameObjectComponent<UIInput>(this.m_object, "Input_password");
				if (text != null && uiinput != null && uilabel != null)
				{
					string value = uiinput.value;
					if (value.IndexOf(text) >= 0)
					{
						uiinput.value = string.Empty;
						uilabel.text = string.Empty;
					}
				}
			}
			if (this.m_inputState == SettingPartsTakeoverPassword.InputState.DECIDED || this.m_inputState == SettingPartsTakeoverPassword.InputState.CANCELED)
			{
				this.m_state = SettingPartsTakeoverPassword.State.STATE_WAIT_END;
			}
			break;
		}
	}

	// Token: 0x06002932 RID: 10546 RVA: 0x000FED0C File Offset: 0x000FCF0C
	private void OnFinishedAnimation()
	{
		if (this.m_object != null)
		{
			this.m_object.SetActive(false);
		}
		this.m_state = SettingPartsTakeoverPassword.State.STATE_END;
	}

	// Token: 0x06002933 RID: 10547 RVA: 0x000FED40 File Offset: 0x000FCF40
	private void OnClickOkButton()
	{
		this.m_inputState = SettingPartsTakeoverPassword.InputState.DECIDED;
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x06002934 RID: 10548 RVA: 0x000FED5C File Offset: 0x000FCF5C
	private void OnClickCancelButton()
	{
		this.m_inputState = SettingPartsTakeoverPassword.InputState.CANCELED;
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x06002935 RID: 10549 RVA: 0x000FED78 File Offset: 0x000FCF78
	private void OnFinishedInput()
	{
		global::Debug.Log("Input Finished! Input Text is" + this.m_input.value);
	}

	// Token: 0x04002483 RID: 9347
	private SettingPartsTakeoverPassword.State m_state;

	// Token: 0x04002484 RID: 9348
	private GameObject m_object;

	// Token: 0x04002485 RID: 9349
	private UIPlayAnimation m_uiAnimation;

	// Token: 0x04002486 RID: 9350
	private UIInput m_input;

	// Token: 0x04002487 RID: 9351
	private UILabel m_label;

	// Token: 0x04002488 RID: 9352
	private string m_anchorPath;

	// Token: 0x04002489 RID: 9353
	private readonly string ExcludePathName = "UI Root (2D)/Camera/Anchor_5_MC";

	// Token: 0x0400248A RID: 9354
	private bool m_calcelButtonUseFlag = true;

	// Token: 0x0400248B RID: 9355
	private bool m_isLoaded;

	// Token: 0x0400248C RID: 9356
	private bool m_playStartCue;

	// Token: 0x0400248D RID: 9357
	private SettingPartsTakeoverPassword.InputState m_inputState;

	// Token: 0x02000535 RID: 1333
	private enum State
	{
		// Token: 0x0400248F RID: 9359
		STATE_NONE = -1,
		// Token: 0x04002490 RID: 9360
		STATE_IDLE,
		// Token: 0x04002491 RID: 9361
		STATE_LOAD,
		// Token: 0x04002492 RID: 9362
		STATE_SETTING,
		// Token: 0x04002493 RID: 9363
		STATE_WAIT_END,
		// Token: 0x04002494 RID: 9364
		STATE_END
	}

	// Token: 0x02000536 RID: 1334
	private enum InputState
	{
		// Token: 0x04002496 RID: 9366
		INPUTTING,
		// Token: 0x04002497 RID: 9367
		DECIDED,
		// Token: 0x04002498 RID: 9368
		CANCELED
	}
}
