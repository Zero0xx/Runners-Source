using System;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x02000530 RID: 1328
public class SettingTakeoverInput : SettingBase
{
	// Token: 0x1700056A RID: 1386
	// (get) Token: 0x0600290C RID: 10508 RVA: 0x000FE158 File Offset: 0x000FC358
	// (set) Token: 0x0600290D RID: 10509 RVA: 0x000FE164 File Offset: 0x000FC364
	public bool IsDicide
	{
		get
		{
			return this.m_inputState == SettingTakeoverInput.InputState.DICIDE;
		}
		private set
		{
		}
	}

	// Token: 0x1700056B RID: 1387
	// (get) Token: 0x0600290E RID: 10510 RVA: 0x000FE168 File Offset: 0x000FC368
	// (set) Token: 0x0600290F RID: 10511 RVA: 0x000FE174 File Offset: 0x000FC374
	public bool IsCanceled
	{
		get
		{
			return this.m_inputState == SettingTakeoverInput.InputState.CANCELED;
		}
		private set
		{
		}
	}

	// Token: 0x1700056C RID: 1388
	// (get) Token: 0x06002910 RID: 10512 RVA: 0x000FE178 File Offset: 0x000FC378
	public bool IsLoaded
	{
		get
		{
			return this.m_isLoaded;
		}
	}

	// Token: 0x06002911 RID: 10513 RVA: 0x000FE180 File Offset: 0x000FC380
	public void SetWindowActive(bool flag)
	{
		if (this.m_object != null)
		{
			this.m_object.SetActive(flag);
		}
	}

	// Token: 0x1700056D RID: 1389
	// (get) Token: 0x06002912 RID: 10514 RVA: 0x000FE1A0 File Offset: 0x000FC3A0
	// (set) Token: 0x06002913 RID: 10515 RVA: 0x000FE1D0 File Offset: 0x000FC3D0
	public string InputIdText
	{
		get
		{
			if (this.m_inputId == null)
			{
				return string.Empty;
			}
			return this.m_inputId.value;
		}
		private set
		{
		}
	}

	// Token: 0x1700056E RID: 1390
	// (get) Token: 0x06002914 RID: 10516 RVA: 0x000FE1D4 File Offset: 0x000FC3D4
	// (set) Token: 0x06002915 RID: 10517 RVA: 0x000FE204 File Offset: 0x000FC404
	public string InputPassText
	{
		get
		{
			if (this.m_inputPass == null)
			{
				return string.Empty;
			}
			return this.m_inputPass.value;
		}
		private set
		{
		}
	}

	// Token: 0x06002916 RID: 10518 RVA: 0x000FE208 File Offset: 0x000FC408
	protected override void OnSetup(string anthorPath)
	{
		if (!this.m_isLoaded)
		{
			this.m_anchorPath = this.ExcludePathName;
		}
	}

	// Token: 0x06002917 RID: 10519 RVA: 0x000FE224 File Offset: 0x000FC424
	protected override void OnPlayStart()
	{
		this.m_isEnd = false;
		this.m_playStartCue = false;
		this.m_isWindowOpen = false;
		if (this.m_isLoaded)
		{
			this.SetWindowActive(true);
			base.enabled = true;
			if (this.m_uiAnimation != null)
			{
				EventDelegate.Add(this.m_uiAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedOpenAnimationCallback), true);
				this.m_uiAnimation.Play(true);
			}
			this.m_inputState = SettingTakeoverInput.InputState.INPUTTING;
			SoundManager.SePlay("sys_window_open", "SE");
			BackKeyManager.AddWindowCallBack(base.gameObject);
		}
		else
		{
			this.m_playStartCue = true;
		}
	}

	// Token: 0x06002918 RID: 10520 RVA: 0x000FE2C8 File Offset: 0x000FC4C8
	protected override bool OnIsEndPlay()
	{
		return this.m_isEnd;
	}

	// Token: 0x06002919 RID: 10521 RVA: 0x000FE2D0 File Offset: 0x000FC4D0
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
	}

	// Token: 0x0600291A RID: 10522 RVA: 0x000FE310 File Offset: 0x000FC510
	private void SetupWindowData()
	{
		if (SettingTakeoverInput.m_prefab == null)
		{
			SettingTakeoverInput.m_prefab = (Resources.Load("Prefabs/UI/window_takeover_id_input") as GameObject);
		}
		this.m_object = (UnityEngine.Object.Instantiate(SettingTakeoverInput.m_prefab, Vector3.zero, Quaternion.identity) as GameObject);
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
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_object, "Btn_ok");
			if (gameObject2 != null)
			{
				UIButtonMessage uibuttonMessage = gameObject2.GetComponent<UIButtonMessage>();
				if (uibuttonMessage == null)
				{
					uibuttonMessage = gameObject2.AddComponent<UIButtonMessage>();
				}
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "OnClickOkButton";
			}
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(this.m_object, "Btn_close");
			if (gameObject3 != null)
			{
				UIButtonMessage uibuttonMessage2 = gameObject3.GetComponent<UIButtonMessage>();
				if (uibuttonMessage2 == null)
				{
					uibuttonMessage2 = gameObject3.AddComponent<UIButtonMessage>();
				}
				uibuttonMessage2.target = base.gameObject;
				uibuttonMessage2.functionName = "OnClickCancelButton";
			}
			TextManager.TextType type = TextManager.TextType.TEXTTYPE_FIXATION_TEXT;
			for (int i = 0; i < 5; i++)
			{
				GameObject gameObject4 = GameObjectUtil.FindChildGameObject(this.m_object, this.textParamTable[i].label);
				if (gameObject4 != null)
				{
					UILabel component = gameObject4.GetComponent<UILabel>();
					if (component != null)
					{
						TextUtility.SetText(component, type, "Title", this.textParamTable[i].text_label);
					}
				}
			}
			this.m_inputId = GameObjectUtil.FindChildGameObjectComponent<UIInput>(this.m_object, "Input_id");
			this.m_inputPass = GameObjectUtil.FindChildGameObjectComponent<UIInput>(this.m_object, "Input_pass");
			this.m_uiAnimation = base.gameObject.AddComponent<UIPlayAnimation>();
			if (this.m_uiAnimation != null)
			{
				Animation component2 = this.m_object.GetComponent<Animation>();
				this.m_uiAnimation.target = component2;
				this.m_uiAnimation.clipName = "ui_menu_option_window_Anim";
			}
			this.m_object.SetActive(false);
			global::Debug.Log("SettingTakeoverInput:SetupWindowData End");
		}
	}

	// Token: 0x0600291B RID: 10523 RVA: 0x000FE594 File Offset: 0x000FC794
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

	// Token: 0x0600291C RID: 10524 RVA: 0x000FE610 File Offset: 0x000FC810
	private void OnClickCancelButton()
	{
		this.PlayCloseAnimation();
		this.m_inputState = SettingTakeoverInput.InputState.CANCELED;
	}

	// Token: 0x0600291D RID: 10525 RVA: 0x000FE620 File Offset: 0x000FC820
	private void OnClickOkButton()
	{
		this.PlayCloseAnimation();
		SoundManager.SePlay("sys_menu_decide", "SE");
		this.m_inputState = SettingTakeoverInput.InputState.DICIDE;
	}

	// Token: 0x0600291E RID: 10526 RVA: 0x000FE640 File Offset: 0x000FC840
	private void OnFinishedOpenAnimationCallback()
	{
		if (!this.m_isWindowOpen)
		{
			this.m_isWindowOpen = true;
		}
	}

	// Token: 0x0600291F RID: 10527 RVA: 0x000FE654 File Offset: 0x000FC854
	private void OnFinishedAnimationCallback()
	{
		this.m_isEnd = true;
		if (this.m_object != null)
		{
			this.m_object.SetActive(false);
			BackKeyManager.RemoveWindowCallBack(base.gameObject);
		}
	}

	// Token: 0x06002920 RID: 10528 RVA: 0x000FE688 File Offset: 0x000FC888
	public void OnClickPlatformBackButton()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (this.m_isWindowOpen)
		{
			this.OnClickCancelButton();
		}
	}

	// Token: 0x04002469 RID: 9321
	private static GameObject m_prefab;

	// Token: 0x0400246A RID: 9322
	private GameObject m_object;

	// Token: 0x0400246B RID: 9323
	private UIPlayAnimation m_uiAnimation;

	// Token: 0x0400246C RID: 9324
	private SettingTakeoverInput.InputState m_inputState;

	// Token: 0x0400246D RID: 9325
	private string m_anchorPath;

	// Token: 0x0400246E RID: 9326
	private readonly string ExcludePathName = "UI Root (2D)/Camera/Anchor_5_MC";

	// Token: 0x0400246F RID: 9327
	private bool m_isEnd;

	// Token: 0x04002470 RID: 9328
	private bool m_isLoaded;

	// Token: 0x04002471 RID: 9329
	private bool m_playStartCue;

	// Token: 0x04002472 RID: 9330
	private bool m_isWindowOpen;

	// Token: 0x04002473 RID: 9331
	private UIInput m_inputId;

	// Token: 0x04002474 RID: 9332
	private UIInput m_inputPass;

	// Token: 0x04002475 RID: 9333
	private SettingTakeoverInput.textLabelData[] textParamTable = new SettingTakeoverInput.textLabelData[]
	{
		new SettingTakeoverInput.textLabelData("Lbl_id_info", "takeover_input_header"),
		new SettingTakeoverInput.textLabelData("Lbl_word_id", "takeover_input_id_head"),
		new SettingTakeoverInput.textLabelData("Lbl_input_id", "takeover_input_id_space"),
		new SettingTakeoverInput.textLabelData("Lbl_word_pass", "takeover_input_pass_head"),
		new SettingTakeoverInput.textLabelData("Lbl_input_pass", "takeover_input_pass_space")
	};

	// Token: 0x02000531 RID: 1329
	private enum InputState
	{
		// Token: 0x04002477 RID: 9335
		INPUTTING,
		// Token: 0x04002478 RID: 9336
		DICIDE,
		// Token: 0x04002479 RID: 9337
		CANCELED
	}

	// Token: 0x02000532 RID: 1330
	private class textLabelData
	{
		// Token: 0x06002921 RID: 10529 RVA: 0x000FE6B8 File Offset: 0x000FC8B8
		public textLabelData(string s1, string s2)
		{
			this.label = s1;
			this.text_label = s2;
		}

		// Token: 0x0400247A RID: 9338
		public string label;

		// Token: 0x0400247B RID: 9339
		public string text_label;
	}

	// Token: 0x02000533 RID: 1331
	private enum textKind
	{
		// Token: 0x0400247D RID: 9341
		HEADER,
		// Token: 0x0400247E RID: 9342
		INPUT_ID,
		// Token: 0x0400247F RID: 9343
		INPUT_ID_SPACE,
		// Token: 0x04002480 RID: 9344
		INPUT_PASS,
		// Token: 0x04002481 RID: 9345
		INPUT_PASS_SPACE,
		// Token: 0x04002482 RID: 9346
		END
	}
}
