using System;
using AnimationOrTween;
using App.Utility;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x02000528 RID: 1320
public class SettingPartsPushNotice : SettingBase
{
	// Token: 0x17000562 RID: 1378
	// (get) Token: 0x060028C6 RID: 10438 RVA: 0x000FC36C File Offset: 0x000FA56C
	// (set) Token: 0x060028C7 RID: 10439 RVA: 0x000FC378 File Offset: 0x000FA578
	public bool IsPressedOn
	{
		get
		{
			return this.m_inputState == SettingPartsPushNotice.InputState.PRESSED_ON;
		}
		private set
		{
		}
	}

	// Token: 0x17000563 RID: 1379
	// (get) Token: 0x060028C8 RID: 10440 RVA: 0x000FC37C File Offset: 0x000FA57C
	// (set) Token: 0x060028C9 RID: 10441 RVA: 0x000FC388 File Offset: 0x000FA588
	public bool IsPressedOff
	{
		get
		{
			return this.m_inputState == SettingPartsPushNotice.InputState.PRESSED_OFF;
		}
		private set
		{
		}
	}

	// Token: 0x17000564 RID: 1380
	// (get) Token: 0x060028CA RID: 10442 RVA: 0x000FC38C File Offset: 0x000FA58C
	// (set) Token: 0x060028CB RID: 10443 RVA: 0x000FC398 File Offset: 0x000FA598
	public bool IsCanceled
	{
		get
		{
			return this.m_inputState == SettingPartsPushNotice.InputState.CANCELED;
		}
		private set
		{
		}
	}

	// Token: 0x17000565 RID: 1381
	// (get) Token: 0x060028CC RID: 10444 RVA: 0x000FC39C File Offset: 0x000FA59C
	// (set) Token: 0x060028CD RID: 10445 RVA: 0x000FC3A4 File Offset: 0x000FA5A4
	public bool IsOverwrite
	{
		get
		{
			return this.m_isOverwrite;
		}
		private set
		{
		}
	}

	// Token: 0x17000566 RID: 1382
	// (get) Token: 0x060028CE RID: 10446 RVA: 0x000FC3A8 File Offset: 0x000FA5A8
	public bool IsLoaded
	{
		get
		{
			return this.m_isLoaded;
		}
	}

	// Token: 0x060028CF RID: 10447 RVA: 0x000FC3B0 File Offset: 0x000FA5B0
	public void SetWindowActive(bool flag)
	{
		if (this.m_object != null)
		{
			this.m_object.SetActive(flag);
		}
	}

	// Token: 0x060028D0 RID: 10448 RVA: 0x000FC3D0 File Offset: 0x000FA5D0
	protected override void OnSetup(string anthorPath)
	{
		if (!this.m_isLoaded)
		{
			this.m_anchorPath = this.ExcludePathName;
		}
	}

	// Token: 0x060028D1 RID: 10449 RVA: 0x000FC3EC File Offset: 0x000FA5EC
	protected override void OnPlayStart()
	{
		this.m_isEnd = false;
		this.m_playStartCue = false;
		this.m_isWindowOpen = false;
		if (this.m_isLoaded)
		{
			this.SetWindowActive(true);
			if (this.m_uiAnimation != null)
			{
				EventDelegate.Add(this.m_uiAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedOpenAnimationCallback), true);
				this.m_uiAnimation.Play(true);
			}
			this.m_inputState = SettingPartsPushNotice.InputState.INPUTTING;
			this.m_isOverwrite = false;
			SoundManager.SePlay("sys_window_open", "SE");
			BackKeyManager.AddWindowCallBack(base.gameObject);
		}
		else
		{
			this.m_playStartCue = true;
		}
	}

	// Token: 0x060028D2 RID: 10450 RVA: 0x000FC490 File Offset: 0x000FA690
	protected override bool OnIsEndPlay()
	{
		return this.m_isEnd;
	}

	// Token: 0x060028D3 RID: 10451 RVA: 0x000FC498 File Offset: 0x000FA698
	protected override void OnUpdate()
	{
		if (!this.m_isLoaded)
		{
			this.m_isLoaded = true;
			base.enabled = false;
			this.SetupWindowData();
			if (this.m_playStartCue)
			{
				this.OnPlayStart();
			}
		}
		if (this.m_closeBtnEnabled != -1 && this.m_closeBtn != null)
		{
			if (this.m_closeBtnEnabled == 1)
			{
				this.m_closeBtn.SetActive(true);
			}
			else
			{
				this.m_closeBtn.SetActive(false);
			}
			this.m_closeBtnEnabled = -1;
		}
	}

	// Token: 0x060028D4 RID: 10452 RVA: 0x000FC524 File Offset: 0x000FA724
	private void SetupWindowData()
	{
		if (SettingPartsPushNotice.m_prefab == null)
		{
			SettingPartsPushNotice.m_prefab = (Resources.Load("Prefabs/UI/window_pushinfo_setting2") as GameObject);
		}
		this.m_object = (UnityEngine.Object.Instantiate(SettingPartsPushNotice.m_prefab, Vector3.zero, Quaternion.identity) as GameObject);
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
			window_pushinfo_setting component = this.m_object.GetComponent<window_pushinfo_setting>();
			if (component == null)
			{
				this.m_object.AddComponent<window_pushinfo_setting>();
			}
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null)
				{
					this.m_infoStateBackup = new Bitset32(systemdata.pushNoticeFlags);
				}
				this.m_LocalPushNoticeFlag = this.IsPushNotice();
			}
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_object, "Btn_ok");
			if (gameObject2 != null)
			{
				this.m_closeBtn = gameObject2;
				UIButtonMessage uibuttonMessage = gameObject2.AddComponent<UIButtonMessage>();
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "OnClickOkButton";
			}
			for (int i = 0; i < 3; i++)
			{
				if (i != 2)
				{
					GameObject gameObject3 = GameObjectUtil.FindChildGameObject(this.m_object, this.InfoCheckToggleButton[i].Name);
					if (gameObject3 != null)
					{
						UIButtonMessage uibuttonMessage2 = gameObject3.AddComponent<UIButtonMessage>();
						uibuttonMessage2.target = base.gameObject;
						uibuttonMessage2.functionName = this.InfoCheckToggleButton[i].FunctionName;
						UIToggle component2 = gameObject3.GetComponent<UIToggle>();
						if (component2 != null)
						{
							component2.value = this.IsPushNoticeFlagStatus((SystemData.PushNoticeFlagStatus)i);
							this.m_InfoStatusToggle[i] = component2;
						}
					}
				}
			}
			GameObject gameObject4 = GameObjectUtil.FindChildGameObject(this.m_object, "Btn_on");
			if (gameObject4 != null)
			{
				UIButtonMessage uibuttonMessage3 = gameObject4.AddComponent<UIButtonMessage>();
				uibuttonMessage3.target = base.gameObject;
				uibuttonMessage3.functionName = "OnClickOnButton";
				UIToggle component3 = gameObject4.GetComponent<UIToggle>();
				if (component3 != null)
				{
					component3.value = this.m_LocalPushNoticeFlag;
				}
			}
			GameObject gameObject5 = GameObjectUtil.FindChildGameObject(this.m_object, "Btn_off");
			if (gameObject5 != null)
			{
				UIButtonMessage uibuttonMessage4 = gameObject5.AddComponent<UIButtonMessage>();
				uibuttonMessage4.target = base.gameObject;
				uibuttonMessage4.functionName = "OnClickOffButton";
				UIToggle component4 = gameObject5.GetComponent<UIToggle>();
				if (component4 != null)
				{
					component4.value = !this.m_LocalPushNoticeFlag;
				}
			}
			TextManager.TextType type = TextManager.TextType.TEXTTYPE_FIXATION_TEXT;
			GameObject gameObject6 = GameObjectUtil.FindChildGameObject(this.m_object, "Lbl_pushinfo_setting");
			if (gameObject6 != null)
			{
				UILabel component5 = gameObject6.GetComponent<UILabel>();
				if (component5 != null)
				{
					TextUtility.SetText(component5, type, "Option", "push_notification");
				}
			}
			GameObject gameObject7 = GameObjectUtil.FindChildGameObject(this.m_object, "Lbl_pushinfo_setting_sub");
			if (gameObject7 != null)
			{
				UILabel component6 = gameObject7.GetComponent<UILabel>();
				if (component6 != null)
				{
					TextUtility.SetText(component6, type, "Option", "push_notification_info");
				}
			}
			this.m_uiAnimation = base.gameObject.AddComponent<UIPlayAnimation>();
			if (this.m_uiAnimation != null)
			{
				Animation component7 = this.m_object.GetComponent<Animation>();
				this.m_uiAnimation.target = component7;
				this.m_uiAnimation.clipName = "ui_menu_option_window_Anim";
			}
			this.m_object.SetActive(false);
			global::Debug.Log("SettingPartsPushNotice:SetupWindowData End");
		}
	}

	// Token: 0x060028D5 RID: 10453 RVA: 0x000FC914 File Offset: 0x000FAB14
	private void PlayCloseAnimation()
	{
		if (this.m_object != null)
		{
			Animation component = this.m_object.GetComponent<Animation>();
			if (component != null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(component, Direction.Reverse);
				if (activeAnimation != null)
				{
					EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedAnimationCallback), true);
					this.m_isWindowOpen = false;
				}
			}
		}
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x060028D6 RID: 10454 RVA: 0x000FC990 File Offset: 0x000FAB90
	private void OverwriteSystemData()
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				if (systemdata.pushNotice != this.m_LocalPushNoticeFlag)
				{
					systemdata.pushNotice = this.m_LocalPushNoticeFlag;
					LocalNotification.EnableNotification(this.m_LocalPushNoticeFlag);
					this.m_isOverwrite = true;
				}
				PnoteNotification.RegistTagsPnote(systemdata.pushNoticeFlags);
				if (systemdata.pushNoticeFlags != this.m_infoStateBackup)
				{
					this.m_isOverwrite = true;
				}
			}
		}
	}

	// Token: 0x060028D7 RID: 10455 RVA: 0x000FCA14 File Offset: 0x000FAC14
	private bool IsPushNotice()
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				return systemdata.pushNotice;
			}
		}
		return false;
	}

	// Token: 0x060028D8 RID: 10456 RVA: 0x000FCA48 File Offset: 0x000FAC48
	private bool IsPushNoticeFlagStatus(SystemData.PushNoticeFlagStatus state)
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				return systemdata.IsFlagStatus(state);
			}
		}
		return false;
	}

	// Token: 0x060028D9 RID: 10457 RVA: 0x000FCA80 File Offset: 0x000FAC80
	private void SetPushNoticeFlagStatus(SystemData.PushNoticeFlagStatus state, bool flag)
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				systemdata.SetFlagStatus(state, flag);
			}
		}
	}

	// Token: 0x060028DA RID: 10458 RVA: 0x000FCAB4 File Offset: 0x000FACB4
	public void SetCloseButtonEnabled(bool enabled)
	{
		if (enabled)
		{
			this.m_closeBtnEnabled = 1;
		}
		else
		{
			this.m_closeBtnEnabled = 1;
			enabled = true;
		}
		if (this.m_closeBtn != null)
		{
			this.m_closeBtn.SetActive(enabled);
			this.m_closeBtnEnabled = -1;
		}
	}

	// Token: 0x060028DB RID: 10459 RVA: 0x000FCB04 File Offset: 0x000FAD04
	private void OnClickCancelButton()
	{
		this.PlayCloseAnimation();
		this.m_inputState = SettingPartsPushNotice.InputState.CANCELED;
	}

	// Token: 0x060028DC RID: 10460 RVA: 0x000FCB14 File Offset: 0x000FAD14
	private void OnClickOnButton()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		this.m_inputState = SettingPartsPushNotice.InputState.PRESSED_ON;
		this.m_LocalPushNoticeFlag = true;
	}

	// Token: 0x060028DD RID: 10461 RVA: 0x000FCB34 File Offset: 0x000FAD34
	private void OnClickOffButton()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		this.m_inputState = SettingPartsPushNotice.InputState.PRESSED_OFF;
		this.m_LocalPushNoticeFlag = false;
	}

	// Token: 0x060028DE RID: 10462 RVA: 0x000FCB54 File Offset: 0x000FAD54
	private void OnClickOkButton()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		this.PlayCloseAnimation();
		this.OverwriteSystemData();
	}

	// Token: 0x060028DF RID: 10463 RVA: 0x000FCB74 File Offset: 0x000FAD74
	private void OnClickEventInfoButton()
	{
		this.InfoCheckBoxCommon(SystemData.PushNoticeFlagStatus.EVENT_INFO);
	}

	// Token: 0x060028E0 RID: 10464 RVA: 0x000FCB80 File Offset: 0x000FAD80
	private void OnClickChallengeInfoButton()
	{
		this.InfoCheckBoxCommon(SystemData.PushNoticeFlagStatus.CHALLENGE_INFO);
	}

	// Token: 0x060028E1 RID: 10465 RVA: 0x000FCB8C File Offset: 0x000FAD8C
	private void OnClickFriendInfoButton()
	{
		this.InfoCheckBoxCommon(SystemData.PushNoticeFlagStatus.FRIEND_INFO);
	}

	// Token: 0x060028E2 RID: 10466 RVA: 0x000FCB98 File Offset: 0x000FAD98
	private void InfoCheckBoxCommon(SystemData.PushNoticeFlagStatus status)
	{
		UIToggle uitoggle = this.m_InfoStatusToggle[(int)status];
		bool flag = false;
		if (uitoggle != null)
		{
			flag = uitoggle.value;
			this.SetPushNoticeFlagStatus(status, uitoggle.value);
		}
		if (flag)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
		}
		else
		{
			SoundManager.SePlay("sys_window_close", "SE");
		}
	}

	// Token: 0x060028E3 RID: 10467 RVA: 0x000FCBFC File Offset: 0x000FADFC
	private void OnFinishedOpenAnimationCallback()
	{
		if (!this.m_isWindowOpen)
		{
			this.m_isWindowOpen = true;
		}
	}

	// Token: 0x060028E4 RID: 10468 RVA: 0x000FCC10 File Offset: 0x000FAE10
	private void OnFinishedAnimationCallback()
	{
		this.m_isEnd = true;
		if (this.m_object != null)
		{
			this.m_object.SetActive(false);
			BackKeyManager.RemoveWindowCallBack(base.gameObject);
		}
	}

	// Token: 0x060028E5 RID: 10469 RVA: 0x000FCC44 File Offset: 0x000FAE44
	public void OnClickPlatformBackButton()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (this.m_isWindowOpen)
		{
			this.OnClickOkButton();
		}
	}

	// Token: 0x04002438 RID: 9272
	private static GameObject m_prefab;

	// Token: 0x04002439 RID: 9273
	private GameObject m_object;

	// Token: 0x0400243A RID: 9274
	private UIPlayAnimation m_uiAnimation;

	// Token: 0x0400243B RID: 9275
	private SettingPartsPushNotice.InputState m_inputState;

	// Token: 0x0400243C RID: 9276
	private string m_anchorPath;

	// Token: 0x0400243D RID: 9277
	private readonly string ExcludePathName = "UI Root (2D)/Camera/Anchor_5_MC";

	// Token: 0x0400243E RID: 9278
	private bool m_isEnd;

	// Token: 0x0400243F RID: 9279
	private bool m_isOverwrite;

	// Token: 0x04002440 RID: 9280
	private bool m_isLoaded;

	// Token: 0x04002441 RID: 9281
	private bool m_playStartCue;

	// Token: 0x04002442 RID: 9282
	private GameObject m_closeBtn;

	// Token: 0x04002443 RID: 9283
	private int m_closeBtnEnabled = -1;

	// Token: 0x04002444 RID: 9284
	private bool m_LocalPushNoticeFlag;

	// Token: 0x04002445 RID: 9285
	private Bitset32 m_infoStateBackup = new Bitset32(0U);

	// Token: 0x04002446 RID: 9286
	private UIToggle[] m_InfoStatusToggle = new UIToggle[3];

	// Token: 0x04002447 RID: 9287
	private bool m_isWindowOpen;

	// Token: 0x04002448 RID: 9288
	private SettingPartsPushNotice.InfoButton[] InfoCheckToggleButton = new SettingPartsPushNotice.InfoButton[]
	{
		new SettingPartsPushNotice.InfoButton("img_check_box_0", "OnClickEventInfoButton"),
		new SettingPartsPushNotice.InfoButton("img_check_box_1", "OnClickChallengeInfoButton"),
		new SettingPartsPushNotice.InfoButton("img_check_box_2", "OnClickFriendInfoButton")
	};

	// Token: 0x02000529 RID: 1321
	private enum InputState
	{
		// Token: 0x0400244A RID: 9290
		INPUTTING,
		// Token: 0x0400244B RID: 9291
		PRESSED_ON,
		// Token: 0x0400244C RID: 9292
		PRESSED_OFF,
		// Token: 0x0400244D RID: 9293
		CANCELED
	}

	// Token: 0x0200052A RID: 1322
	private class InfoButton
	{
		// Token: 0x060028E6 RID: 10470 RVA: 0x000FCC74 File Offset: 0x000FAE74
		public InfoButton(string s1, string s2)
		{
			this.Name = s1;
			this.FunctionName = s2;
		}

		// Token: 0x0400244E RID: 9294
		public string Name;

		// Token: 0x0400244F RID: 9295
		public string FunctionName;
	}
}
