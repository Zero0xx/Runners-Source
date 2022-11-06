using System;
using Text;
using UnityEngine;

// Token: 0x02000537 RID: 1335
public class SettingPartsUserName : SettingBase
{
	// Token: 0x17000573 RID: 1395
	// (get) Token: 0x06002937 RID: 10551 RVA: 0x000FEDB0 File Offset: 0x000FCFB0
	// (set) Token: 0x06002938 RID: 10552 RVA: 0x000FEDE0 File Offset: 0x000FCFE0
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

	// Token: 0x17000574 RID: 1396
	// (get) Token: 0x06002939 RID: 10553 RVA: 0x000FEDE4 File Offset: 0x000FCFE4
	public UILabel TextLabel
	{
		get
		{
			return this.m_label;
		}
	}

	// Token: 0x17000575 RID: 1397
	// (get) Token: 0x0600293A RID: 10554 RVA: 0x000FEDEC File Offset: 0x000FCFEC
	// (set) Token: 0x0600293B RID: 10555 RVA: 0x000FEE00 File Offset: 0x000FD000
	public bool IsDecided
	{
		get
		{
			return this.m_inputState == SettingPartsUserName.InputState.DECIDED;
		}
		private set
		{
		}
	}

	// Token: 0x17000576 RID: 1398
	// (get) Token: 0x0600293C RID: 10556 RVA: 0x000FEE04 File Offset: 0x000FD004
	// (set) Token: 0x0600293D RID: 10557 RVA: 0x000FEE18 File Offset: 0x000FD018
	public bool IsCanceled
	{
		get
		{
			return this.m_inputState == SettingPartsUserName.InputState.CANCELED;
		}
		private set
		{
		}
	}

	// Token: 0x0600293E RID: 10558 RVA: 0x000FEE1C File Offset: 0x000FD01C
	public void SetCancelButtonUseFlag(bool useFlag)
	{
		this.m_calcelButtonUseFlag = useFlag;
	}

	// Token: 0x0600293F RID: 10559 RVA: 0x000FEE28 File Offset: 0x000FD028
	private void OnDestroy()
	{
		if (this.m_object != null)
		{
			UnityEngine.Object.Destroy(this.m_object);
		}
	}

	// Token: 0x06002940 RID: 10560 RVA: 0x000FEE48 File Offset: 0x000FD048
	protected override void OnSetup(string anchorPath)
	{
		if (!this.m_isLoaded)
		{
			this.m_anchorPath = this.ExcludePathName;
			this.m_state = SettingPartsUserName.State.STATE_LOAD;
		}
	}

	// Token: 0x06002941 RID: 10561 RVA: 0x000FEE68 File Offset: 0x000FD068
	private void SetupWindowData()
	{
		this.m_object = base.gameObject;
		if (this.m_object != null)
		{
			this.m_input = GameObjectUtil.FindChildGameObjectComponent<UIInput>(this.m_object, "Input_name");
			EventDelegate item = new EventDelegate(new EventDelegate.Callback(this.OnFinishedInput));
			this.m_input.onSubmit.Add(item);
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_object, "Btn_ok");
			if (gameObject != null)
			{
				UIButtonMessage uibuttonMessage = gameObject.AddComponent<UIButtonMessage>();
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "OnClickOkButton";
				UIPlayAnimation component = gameObject.GetComponent<UIPlayAnimation>();
				if (component != null)
				{
					EventDelegate.Add(component.onFinished, new EventDelegate.Callback(this.OnFinishedAnimation), false);
				}
			}
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_object, "Btn_close");
			if (gameObject2 != null)
			{
				UIButtonMessage uibuttonMessage2 = gameObject2.AddComponent<UIButtonMessage>();
				uibuttonMessage2.target = base.gameObject;
				uibuttonMessage2.functionName = "OnClickCancelButton";
				window_name_setting component2 = this.m_object.GetComponent<window_name_setting>();
				if (component2 == null)
				{
					this.m_object.AddComponent<window_name_setting>();
				}
				UIPlayAnimation component3 = gameObject2.GetComponent<UIPlayAnimation>();
				if (component3 != null)
				{
					EventDelegate.Add(component3.onFinished, new EventDelegate.Callback(this.OnFinishedAnimation), false);
				}
				gameObject2.SetActive(this.m_calcelButtonUseFlag);
			}
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(this.m_object, "Lbl_name_setting");
			if (gameObject3 != null)
			{
				UILabel component4 = gameObject3.GetComponent<UILabel>();
				if (component4 != null)
				{
					TextUtility.SetCommonText(component4, "UserName", "name_setting");
				}
			}
			GameObject gameObject4 = GameObjectUtil.FindChildGameObject(this.m_object, "Lbl_name_setting_sub");
			if (gameObject4 != null)
			{
				UILabel component5 = gameObject4.GetComponent<UILabel>();
				if (component5 != null)
				{
					TextUtility.SetCommonText(component5, "UserName", "name_setting_info");
				}
			}
			GameObject gameObject5 = GameObjectUtil.FindChildGameObject(this.m_object, "Lbl_input_name");
			if (gameObject5 != null)
			{
				this.m_label = gameObject5.GetComponent<UILabel>();
				if (this.m_label != null)
				{
					string text = null;
					ServerSettingState settingState = ServerInterface.SettingState;
					if (settingState != null)
					{
						text = settingState.m_userName;
					}
					if (string.IsNullOrEmpty(text))
					{
						TextUtility.SetCommonText(this.m_label, "UserName", "input_name");
					}
					else
					{
						this.m_label.text = text;
					}
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

	// Token: 0x06002942 RID: 10562 RVA: 0x000FF138 File Offset: 0x000FD338
	protected override void OnPlayStart()
	{
		if (this.m_isLoaded)
		{
			this.m_playStartCue = false;
			if (this.m_object != null)
			{
				this.m_object.SetActive(true);
				GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_object, "Btn_close");
				if (gameObject != null)
				{
					gameObject.SetActive(this.m_calcelButtonUseFlag);
				}
			}
			if (this.m_uiAnimation != null)
			{
				this.m_uiAnimation.Play(true);
			}
			this.m_inputState = SettingPartsUserName.InputState.INPUTTING;
			this.m_state = SettingPartsUserName.State.STATE_SETTING;
			SoundManager.SePlay("sys_window_open", "SE");
		}
		else
		{
			this.m_playStartCue = true;
		}
	}

	// Token: 0x06002943 RID: 10563 RVA: 0x000FF1E4 File Offset: 0x000FD3E4
	protected override bool OnIsEndPlay()
	{
		return this.m_state == SettingPartsUserName.State.STATE_END;
	}

	// Token: 0x06002944 RID: 10564 RVA: 0x000FF1F8 File Offset: 0x000FD3F8
	protected override void OnUpdate()
	{
		switch (this.m_state)
		{
		case SettingPartsUserName.State.STATE_LOAD:
			this.m_isLoaded = true;
			this.SetupWindowData();
			if (this.m_playStartCue)
			{
				this.OnPlayStart();
			}
			break;
		case SettingPartsUserName.State.STATE_SETTING:
			if (this.m_input.selected)
			{
				string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "UserName", "input_name").text;
				UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_object, "Lbl_input_name");
				UIInput uiinput = GameObjectUtil.FindChildGameObjectComponent<UIInput>(this.m_object, "Input_name");
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
			if (this.m_inputState == SettingPartsUserName.InputState.DECIDED || this.m_inputState == SettingPartsUserName.InputState.CANCELED)
			{
				this.m_state = SettingPartsUserName.State.STATE_WAIT_END;
			}
			break;
		}
	}

	// Token: 0x06002945 RID: 10565 RVA: 0x000FF314 File Offset: 0x000FD514
	private void OnFinishedAnimation()
	{
		this.m_state = SettingPartsUserName.State.STATE_END;
	}

	// Token: 0x06002946 RID: 10566 RVA: 0x000FF320 File Offset: 0x000FD520
	private void OnClickOkButton()
	{
		if (this.m_inputState == SettingPartsUserName.InputState.DECIDED)
		{
			return;
		}
		this.m_inputState = SettingPartsUserName.InputState.DECIDED;
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x06002947 RID: 10567 RVA: 0x000FF354 File Offset: 0x000FD554
	private void OnClickCancelButton()
	{
		if (this.m_inputState == SettingPartsUserName.InputState.CANCELED)
		{
			return;
		}
		this.m_input.value = ServerInterface.SettingState.m_userName;
		this.m_inputState = SettingPartsUserName.InputState.CANCELED;
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x06002948 RID: 10568 RVA: 0x000FF390 File Offset: 0x000FD590
	private void OnFinishedInput()
	{
		global::Debug.Log("Input Finished! Input Text is" + this.m_input.value);
	}

	// Token: 0x04002499 RID: 9369
	private SettingPartsUserName.State m_state;

	// Token: 0x0400249A RID: 9370
	private GameObject m_object;

	// Token: 0x0400249B RID: 9371
	private UIPlayAnimation m_uiAnimation;

	// Token: 0x0400249C RID: 9372
	private UIInput m_input;

	// Token: 0x0400249D RID: 9373
	private UILabel m_label;

	// Token: 0x0400249E RID: 9374
	private string m_anchorPath;

	// Token: 0x0400249F RID: 9375
	private readonly string ExcludePathName = "UI Root (2D)/Camera/Anchor_5_MC";

	// Token: 0x040024A0 RID: 9376
	private bool m_calcelButtonUseFlag = true;

	// Token: 0x040024A1 RID: 9377
	private bool m_isLoaded;

	// Token: 0x040024A2 RID: 9378
	private bool m_playStartCue;

	// Token: 0x040024A3 RID: 9379
	private SettingPartsUserName.InputState m_inputState;

	// Token: 0x02000538 RID: 1336
	private enum State
	{
		// Token: 0x040024A5 RID: 9381
		STATE_NONE = -1,
		// Token: 0x040024A6 RID: 9382
		STATE_IDLE,
		// Token: 0x040024A7 RID: 9383
		STATE_LOAD,
		// Token: 0x040024A8 RID: 9384
		STATE_SETTING,
		// Token: 0x040024A9 RID: 9385
		STATE_WAIT_END,
		// Token: 0x040024AA RID: 9386
		STATE_END
	}

	// Token: 0x02000539 RID: 1337
	private enum InputState
	{
		// Token: 0x040024AC RID: 9388
		INPUTTING,
		// Token: 0x040024AD RID: 9389
		DECIDED,
		// Token: 0x040024AE RID: 9390
		CANCELED
	}
}
