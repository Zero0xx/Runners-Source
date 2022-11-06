using System;
using AnimationOrTween;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x020004AE RID: 1198
public class window_performance_setting : WindowBase
{
	// Token: 0x170004C4 RID: 1220
	// (get) Token: 0x0600236E RID: 9070 RVA: 0x000D5008 File Offset: 0x000D3208
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x170004C5 RID: 1221
	// (get) Token: 0x0600236F RID: 9071 RVA: 0x000D5010 File Offset: 0x000D3210
	public window_performance_setting.State EndState
	{
		get
		{
			return this.m_State;
		}
	}

	// Token: 0x06002370 RID: 9072 RVA: 0x000D5018 File Offset: 0x000D3218
	private void Start()
	{
		OptionMenuUtility.TranObj(base.gameObject);
		base.enabled = false;
		this.m_selected = false;
		this.m_uiAnimation = base.gameObject.AddComponent<UIPlayAnimation>();
		if (this.m_uiAnimation != null)
		{
			Animation component = base.gameObject.GetComponent<Animation>();
			this.m_uiAnimation.target = component;
			this.m_uiAnimation.clipName = "ui_cmn_window_Anim2";
		}
		SoundManager.SePlay("sys_window_open", "SE");
	}

	// Token: 0x06002371 RID: 9073 RVA: 0x000D5098 File Offset: 0x000D3298
	public void Setup()
	{
		this.m_selected = false;
		this.m_State = window_performance_setting.State.EXEC;
		this.UpdateButtonImage();
	}

	// Token: 0x06002372 RID: 9074 RVA: 0x000D50B0 File Offset: 0x000D32B0
	private void Update()
	{
		if (GeneralWindow.IsCreated("BackTitleSelect") && this.m_selected && GeneralWindow.IsButtonPressed)
		{
			bool flag = this.IsLightMode();
			bool flag2 = this.IsHighTexture();
			if (this.m_isChangeCheckBox0)
			{
				this.SaveSystemData(!flag);
			}
			if (this.m_isChangeCheckBox1)
			{
				this.SaveSystemDataTex(!flag2);
			}
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				instance.SaveSystemData();
			}
			HudMenuUtility.SendMsgMenuSequenceToMainMenu(MsgMenuSequence.SequeneceType.TITLE);
			GeneralWindow.Close();
			base.enabled = false;
			this.PlayCloseAnimation();
		}
	}

	// Token: 0x06002373 RID: 9075 RVA: 0x000D514C File Offset: 0x000D334C
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

	// Token: 0x06002374 RID: 9076 RVA: 0x000D51B0 File Offset: 0x000D33B0
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

	// Token: 0x06002375 RID: 9077 RVA: 0x000D51E4 File Offset: 0x000D33E4
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

	// Token: 0x06002376 RID: 9078 RVA: 0x000D5218 File Offset: 0x000D3418
	private void UpdateButtonImage()
	{
		bool startsActive = this.IsLightMode();
		bool flag = this.IsHighTexture();
		this.m_isChangeCheckBox0 = false;
		this.m_isChangeCheckBox1 = false;
		this.m_isToggleLock = true;
		if (this.m_checkBox0 != null)
		{
			this.m_checkBox0.startsActive = startsActive;
			this.m_checkBox0.SendMessage("Start", SendMessageOptions.DontRequireReceiver);
		}
		if (this.m_checkBox1 != null)
		{
			this.m_checkBox1.startsActive = !flag;
			this.m_checkBox1.SendMessage("Start", SendMessageOptions.DontRequireReceiver);
		}
		this.m_isToggleLock = false;
	}

	// Token: 0x06002377 RID: 9079 RVA: 0x000D52B0 File Offset: 0x000D34B0
	private void SaveSystemData(bool lightModeFlag)
	{
		bool flag = this.IsLightMode();
		if (flag != lightModeFlag)
		{
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

	// Token: 0x06002378 RID: 9080 RVA: 0x000D52F4 File Offset: 0x000D34F4
	private void SaveSystemDataTex(bool texModeFlag)
	{
		bool flag = this.IsHighTexture();
		if (flag != texModeFlag)
		{
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

	// Token: 0x06002379 RID: 9081 RVA: 0x000D5338 File Offset: 0x000D3538
	private void ShowBackTileMessage()
	{
		if (this.m_isChangeCheckBox0 || this.m_isChangeCheckBox1)
		{
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "BackTitleSelect",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextUtility.GetCommonText("MainMenu", "back_title_caption"),
				message = TextUtility.GetCommonText("Option", "weight_saving_back_title_text")
			});
			this.m_selected = true;
			base.enabled = true;
		}
		else
		{
			this.PlayCloseAnimation();
		}
	}

	// Token: 0x0600237A RID: 9082 RVA: 0x000D53C4 File Offset: 0x000D35C4
	public void OnChangeCheckBox0()
	{
		if (!this.m_isToggleLock)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			this.m_isChangeCheckBox0 = !this.m_isChangeCheckBox0;
		}
	}

	// Token: 0x0600237B RID: 9083 RVA: 0x000D53FC File Offset: 0x000D35FC
	public void OnChangeCheckBox1()
	{
		if (!this.m_isToggleLock)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			this.m_isChangeCheckBox1 = !this.m_isChangeCheckBox1;
		}
	}

	// Token: 0x0600237C RID: 9084 RVA: 0x000D5434 File Offset: 0x000D3634
	private void OnClickOkButton()
	{
		this.m_State = window_performance_setting.State.CLOSE;
		this.m_isToggleLock = false;
		this.ShowBackTileMessage();
	}

	// Token: 0x0600237D RID: 9085 RVA: 0x000D544C File Offset: 0x000D364C
	private void OnClickCloseButton()
	{
		this.m_State = window_performance_setting.State.CLOSE;
		this.m_isToggleLock = false;
		this.PlayCloseAnimation();
	}

	// Token: 0x0600237E RID: 9086 RVA: 0x000D5464 File Offset: 0x000D3664
	private void OnFinishedAnimationCallback()
	{
		this.m_isEnd = true;
		this.m_selected = false;
	}

	// Token: 0x0600237F RID: 9087 RVA: 0x000D5474 File Offset: 0x000D3674
	public void PlayOpenWindow()
	{
		this.m_isEnd = false;
		if (this.m_uiAnimation != null)
		{
			this.m_uiAnimation.Play(true);
			base.enabled = true;
		}
	}

	// Token: 0x06002380 RID: 9088 RVA: 0x000D54A4 File Offset: 0x000D36A4
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (msg != null)
		{
			msg.StaySequence();
		}
		UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "Btn_close");
		if (uibuttonMessage != null)
		{
			uibuttonMessage.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0400202E RID: 8238
	[SerializeField]
	private UIToggle m_checkBox0;

	// Token: 0x0400202F RID: 8239
	[SerializeField]
	private UIToggle m_checkBox1;

	// Token: 0x04002030 RID: 8240
	private window_performance_setting.State m_State;

	// Token: 0x04002031 RID: 8241
	private bool m_selected;

	// Token: 0x04002032 RID: 8242
	private UIPlayAnimation m_uiAnimation;

	// Token: 0x04002033 RID: 8243
	private bool m_isToggleLock;

	// Token: 0x04002034 RID: 8244
	private bool m_isEnd;

	// Token: 0x04002035 RID: 8245
	private bool m_isChangeCheckBox0;

	// Token: 0x04002036 RID: 8246
	private bool m_isChangeCheckBox1;

	// Token: 0x020004AF RID: 1199
	public enum State
	{
		// Token: 0x04002038 RID: 8248
		EXEC,
		// Token: 0x04002039 RID: 8249
		CLOSE
	}
}
