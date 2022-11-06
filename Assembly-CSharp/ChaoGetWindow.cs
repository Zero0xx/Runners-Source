using System;
using UnityEngine;

// Token: 0x02000352 RID: 850
public class ChaoGetWindow : WindowBase
{
	// Token: 0x170003C6 RID: 966
	// (get) Token: 0x0600192A RID: 6442 RVA: 0x00091764 File Offset: 0x0008F964
	// (set) Token: 0x0600192B RID: 6443 RVA: 0x0009176C File Offset: 0x0008F96C
	public bool isSetuped
	{
		get
		{
			return this.m_isSetuped;
		}
		set
		{
			this.m_isSetuped = false;
		}
	}

	// Token: 0x0600192C RID: 6444 RVA: 0x00091778 File Offset: 0x0008F978
	private void Start()
	{
	}

	// Token: 0x0600192D RID: 6445 RVA: 0x0009177C File Offset: 0x0008F97C
	private void Update()
	{
		if (this.m_state == ChaoGetWindow.State.CLICKED_NEXT_BUTTON)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "skip_collider");
			if (gameObject != null)
			{
				gameObject.SetActive(true);
			}
			foreach (GameObject gameObject2 in this.m_buttons)
			{
				if (!(gameObject2 == null))
				{
					gameObject2.SetActive(false);
				}
			}
			if (this.m_chaoGetParts != null)
			{
				this.m_chaoGetParts.PlayGetAnimation(base.gameObject.GetComponent<Animation>());
				this.m_btnType = this.m_chaoGetParts.GetButtonType();
				this.m_buttons[(int)this.m_btnType].SetActive(true);
			}
			this.SetEnableButton(this.m_btnType, false);
			this.m_state = ChaoGetWindow.State.PLAYING;
		}
		if (this.m_SeFlagHatch != null)
		{
			this.m_SeFlagHatch.Update();
		}
		if (this.m_SeFlagBreak != null)
		{
			this.m_SeFlagBreak.Update();
		}
	}

	// Token: 0x0600192E RID: 6446 RVA: 0x0009187C File Offset: 0x0008FA7C
	public void PlayStart(ChaoGetPartsBase chaoGetParts, bool isTutorial, bool disabledEqip = false, RouletteUtility.AchievementType achievement = RouletteUtility.AchievementType.NONE)
	{
		this.m_achievementType = achievement;
		RouletteManager.OpenRouletteWindow();
		this.m_chaoGetParts = chaoGetParts;
		this.m_isTutorial = isTutorial;
		this.m_backKyeVaildOKBtn = false;
		this.m_backKeyVaildNextBtn = false;
		this.m_isClickedEquip = false;
		this.m_disabledEqip = disabledEqip;
		this.m_state = ChaoGetWindow.State.PLAYING;
		if (this.m_isTutorial)
		{
			if (RouletteTop.Instance != null && RouletteTop.Instance.category == RouletteCategory.PREMIUM)
			{
				this.m_isTutorial = true;
			}
			else
			{
				this.m_isTutorial = false;
			}
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "pattern_btn_use");
		if (gameObject != null)
		{
			this.m_buttons[0] = GameObjectUtil.FindChildGameObject(gameObject, "pattern_0");
			this.m_buttons[2] = GameObjectUtil.FindChildGameObject(gameObject, "pattern_5");
			this.m_buttons[1] = GameObjectUtil.FindChildGameObject(gameObject, "pattern_6");
		}
		if (!this.m_isSetuped)
		{
			if (this.m_buttons[0] != null)
			{
				UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(this.m_buttons[0], "Btn_ok");
				if (uibuttonMessage != null)
				{
					uibuttonMessage.target = base.gameObject;
					uibuttonMessage.functionName = "OkButtonClickedCallback";
				}
			}
			if (this.m_buttons[1] != null)
			{
				UIButtonMessage uibuttonMessage2 = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(this.m_buttons[1], "Btn_next");
				if (uibuttonMessage2 != null)
				{
					uibuttonMessage2.target = base.gameObject;
					uibuttonMessage2.functionName = "NextButtonClickedCallback";
				}
			}
			if (this.m_buttons[2] != null)
			{
				UIButtonMessage uibuttonMessage3 = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(this.m_buttons[2], "Btn_ok");
				if (uibuttonMessage3 != null)
				{
					uibuttonMessage3.target = base.gameObject;
					uibuttonMessage3.functionName = "OkButtonClickedCallback";
				}
				UIButtonMessage uibuttonMessage4 = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(this.m_buttons[2], "Btn_post");
				if (uibuttonMessage4 != null)
				{
					uibuttonMessage4.target = base.gameObject;
					uibuttonMessage4.functionName = "EquipButtonClickedCallback";
				}
			}
			UIButtonMessage uibuttonMessage5 = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "skip_collider");
			if (uibuttonMessage5 != null)
			{
				uibuttonMessage5.target = base.gameObject;
				uibuttonMessage5.functionName = "SkipButtonClickedCallback";
			}
			this.m_SeFlagHatch = new HudFlagWatcher();
			GameObject watchObject = GameObjectUtil.FindChildGameObject(base.gameObject, "SE_flag");
			this.m_SeFlagHatch.Setup(watchObject, new HudFlagWatcher.ValueChangeCallback(this.SeFlagHatchCallback));
			this.m_SeFlagBreak = new HudFlagWatcher();
			GameObject watchObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "SE_flag_break");
			this.m_SeFlagBreak.Setup(watchObject2, new HudFlagWatcher.ValueChangeCallback(this.SeFlagBreakCallback));
			this.m_isSetuped = true;
		}
		base.gameObject.SetActive(true);
		foreach (GameObject gameObject2 in this.m_buttons)
		{
			if (!(gameObject2 == null))
			{
				gameObject2.SetActive(false);
			}
		}
		if (this.m_chaoGetParts != null)
		{
			this.m_chaoGetParts.SetCallback(new ChaoGetPartsBase.AnimationEndCallback(this.AnimationEndCallback));
			this.m_chaoGetParts.Setup(base.gameObject);
			Animation component = base.gameObject.GetComponent<Animation>();
			component.Stop();
			this.m_chaoGetParts.PlayGetAnimation(component);
			this.m_btnType = this.m_chaoGetParts.GetButtonType();
			if (this.m_achievementType == RouletteUtility.AchievementType.Multi || RouletteUtility.loginRoulette)
			{
				this.m_btnType = ChaoGetPartsBase.BtnType.OK;
			}
			if (this.m_btnType >= ChaoGetPartsBase.BtnType.OK && this.m_btnType < ChaoGetPartsBase.BtnType.NUM)
			{
				if (this.m_buttons[(int)this.m_btnType] != null)
				{
					this.m_buttons[(int)this.m_btnType].SetActive(true);
				}
				else if (this.m_buttons[2] != null)
				{
					GameObject gameObject3 = this.m_buttons[2];
					if (gameObject3 != null)
					{
						gameObject3.SetActive(true);
						UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(gameObject3, "Btn_post");
						if (uiimageButton != null)
						{
							uiimageButton.isEnabled = false;
						}
					}
				}
			}
		}
		GameObject gameObject4 = GameObjectUtil.FindChildGameObject(base.gameObject, "skip_collider");
		if (gameObject4 != null)
		{
			gameObject4.SetActive(true);
		}
		this.SetEnableButton(this.m_btnType, false);
		UIDraggablePanel uidraggablePanel = GameObjectUtil.FindChildGameObjectComponent<UIDraggablePanel>(base.gameObject, "window_chaoset_alpha_clip");
		if (uidraggablePanel != null)
		{
			uidraggablePanel.ResetPosition();
		}
		SoundManager.SePlay("sys_window_open", "SE");
	}

	// Token: 0x170003C7 RID: 967
	// (get) Token: 0x0600192F RID: 6447 RVA: 0x00091D04 File Offset: 0x0008FF04
	public bool IsPlayEnd
	{
		get
		{
			return this.m_state == ChaoGetWindow.State.END;
		}
	}

	// Token: 0x170003C8 RID: 968
	// (get) Token: 0x06001930 RID: 6448 RVA: 0x00091D10 File Offset: 0x0008FF10
	public bool IsClickedEquip
	{
		get
		{
			return this.m_isClickedEquip;
		}
	}

	// Token: 0x06001931 RID: 6449 RVA: 0x00091D18 File Offset: 0x0008FF18
	private void SetEnableButton(ChaoGetPartsBase.BtnType buttonType, bool isEnable)
	{
		UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(this.m_buttons[(int)buttonType], "Btn_ok");
		if (uiimageButton != null)
		{
			uiimageButton.isEnabled = isEnable;
		}
		UIImageButton uiimageButton2 = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(this.m_buttons[(int)buttonType], "Btn_next");
		if (uiimageButton2 != null)
		{
			uiimageButton2.isEnabled = isEnable;
		}
		UIImageButton uiimageButton3 = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(this.m_buttons[(int)buttonType], "Btn_post");
		if (uiimageButton3 != null)
		{
			uiimageButton3.isEnabled = isEnable;
		}
	}

	// Token: 0x06001932 RID: 6450 RVA: 0x00091D98 File Offset: 0x0008FF98
	private void AnimationEndCallback(ChaoGetPartsBase.AnimType animType)
	{
		switch (animType)
		{
		case ChaoGetPartsBase.AnimType.GET_ANIM_CONTINUE:
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "skip_collider");
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
			this.SetEnableButton(this.m_btnType, true);
			this.SetEqipBtnDisabled();
			this.m_backKeyVaildNextBtn = true;
			this.m_state = ChaoGetWindow.State.WAIT_CLICK_NEXT_BUTTON;
			break;
		}
		case ChaoGetPartsBase.AnimType.GET_ANIM_FINISH:
		{
			this.SetEnableButton(this.m_btnType, true);
			this.SetEqipBtnDisabled();
			if (this.m_isTutorial)
			{
				TutorialCursor.StartTutorialCursor(TutorialCursor.Type.ROULETTE_OK);
			}
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "skip_collider");
			if (gameObject2 != null)
			{
				gameObject2.SetActive(false);
			}
			this.m_backKyeVaildOKBtn = true;
			break;
		}
		case ChaoGetPartsBase.AnimType.OUT_ANIM:
			this.DeleteChaoTexture();
			base.gameObject.SetActive(false);
			this.m_state = ChaoGetWindow.State.END;
			break;
		}
	}

	// Token: 0x06001933 RID: 6451 RVA: 0x00091E7C File Offset: 0x0009007C
	private void SetEqipBtnDisabled()
	{
		if (this.m_disabledEqip)
		{
			GameObject gameObject = this.m_buttons[2];
			if (gameObject != null)
			{
				UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(gameObject, "Btn_post");
				if (uiimageButton != null)
				{
					uiimageButton.isEnabled = false;
				}
			}
		}
	}

	// Token: 0x06001934 RID: 6452 RVA: 0x00091EC8 File Offset: 0x000900C8
	private void OkButtonClickedCallback()
	{
		RouletteManager.CloseRouletteWindow();
		this.m_isClickedEquip = false;
		SoundManager.SePlay("sys_menu_decide", "SE");
		if (this.m_achievementType != RouletteUtility.AchievementType.NONE)
		{
			RouletteManager.RouletteGetWindowClose(this.m_achievementType, RouletteUtility.NextType.NONE);
			this.m_achievementType = RouletteUtility.AchievementType.NONE;
		}
		if (this.m_chaoGetParts != null)
		{
			this.m_chaoGetParts.PlayEndAnimation(base.gameObject.GetComponent<Animation>());
		}
		this.m_backKyeVaildOKBtn = false;
		this.m_backKeyVaildNextBtn = false;
	}

	// Token: 0x06001935 RID: 6453 RVA: 0x00091F48 File Offset: 0x00090148
	private void NextButtonClickedCallback()
	{
		RouletteManager.CloseRouletteWindow();
		this.m_backKyeVaildOKBtn = false;
		this.m_backKeyVaildNextBtn = false;
		this.m_state = ChaoGetWindow.State.CLICKED_NEXT_BUTTON;
	}

	// Token: 0x06001936 RID: 6454 RVA: 0x00091F68 File Offset: 0x00090168
	private void EquipButtonClickedCallback()
	{
		RouletteManager.CloseRouletteWindow();
		if (this.m_achievementType != RouletteUtility.AchievementType.NONE)
		{
			if (this.m_achievementType == RouletteUtility.AchievementType.PlayerGet)
			{
				RouletteManager.RouletteGetWindowClose(this.m_achievementType, RouletteUtility.NextType.CHARA_EQUIP);
			}
			else
			{
				RouletteManager.RouletteGetWindowClose(this.m_achievementType, RouletteUtility.NextType.EQUIP);
			}
			this.m_achievementType = RouletteUtility.AchievementType.NONE;
		}
		this.m_isClickedEquip = true;
		SoundManager.SePlay("sys_menu_decide", "SE");
		if (this.m_chaoGetParts != null)
		{
			this.m_chaoGetParts.PlayEndAnimation(base.gameObject.GetComponent<Animation>());
		}
		this.m_backKyeVaildOKBtn = false;
		this.m_backKeyVaildNextBtn = false;
	}

	// Token: 0x06001937 RID: 6455 RVA: 0x00092004 File Offset: 0x00090204
	private void SeFlagHatchCallback(float newValue, float prevValue)
	{
		if (newValue == 1f && this.m_chaoGetParts != null)
		{
			this.m_chaoGetParts.PlaySE(ChaoWindowUtility.SeHatch);
		}
	}

	// Token: 0x06001938 RID: 6456 RVA: 0x00092040 File Offset: 0x00090240
	private void SeFlagBreakCallback(float newValue, float prevValue)
	{
		if (newValue == 1f && this.m_chaoGetParts != null)
		{
			this.m_chaoGetParts.PlaySE(ChaoWindowUtility.SeBreak);
		}
	}

	// Token: 0x06001939 RID: 6457 RVA: 0x0009207C File Offset: 0x0009027C
	private void SkipButtonClickedCallback()
	{
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component != null)
		{
			foreach (object obj in component)
			{
				AnimationState animationState = (AnimationState)obj;
				if (!(animationState == null))
				{
					animationState.time = animationState.length * 0.99f;
				}
			}
		}
	}

	// Token: 0x0600193A RID: 6458 RVA: 0x0009211C File Offset: 0x0009031C
	private void OnSetChaoTexture(ChaoTextureManager.TextureData data)
	{
		UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, "img_chao_1");
		if (uitexture != null)
		{
			uitexture.mainTexture = data.tex;
			uitexture.enabled = true;
		}
	}

	// Token: 0x0600193B RID: 6459 RVA: 0x0009215C File Offset: 0x0009035C
	private void DeleteChaoTexture()
	{
		UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, "img_chao_1");
		if (uitexture != null)
		{
			uitexture.mainTexture = null;
			uitexture.enabled = false;
			ChaoTextureManager.Instance.RemoveChaoTexture(this.m_chaoGetParts.ChaoId);
		}
	}

	// Token: 0x0600193C RID: 6460 RVA: 0x000921AC File Offset: 0x000903AC
	private void SendMessageOnClick(string btnName)
	{
		if (this.m_btnType == ChaoGetPartsBase.BtnType.NONE || this.m_btnType == ChaoGetPartsBase.BtnType.NUM)
		{
			return;
		}
		if (this.m_buttons[(int)this.m_btnType] != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_buttons[(int)this.m_btnType], btnName);
			if (gameObject != null && gameObject.activeSelf)
			{
				UIButtonMessage component = gameObject.GetComponent<UIButtonMessage>();
				if (component != null)
				{
					component.SendMessage("OnClick");
				}
			}
		}
	}

	// Token: 0x0600193D RID: 6461 RVA: 0x00092234 File Offset: 0x00090434
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (this.m_backKyeVaildOKBtn)
		{
			this.SendMessageOnClick("Btn_ok");
		}
		else if (this.m_backKeyVaildNextBtn)
		{
			this.SendMessageOnClick("Btn_next");
		}
		if (msg != null)
		{
			msg.StaySequence();
		}
	}

	// Token: 0x0400169A RID: 5786
	private bool m_isSetuped;

	// Token: 0x0400169B RID: 5787
	private bool m_isClickedEquip;

	// Token: 0x0400169C RID: 5788
	private bool m_isTutorial;

	// Token: 0x0400169D RID: 5789
	private bool m_disabledEqip;

	// Token: 0x0400169E RID: 5790
	private bool m_backKeyVaildNextBtn;

	// Token: 0x0400169F RID: 5791
	private bool m_backKyeVaildOKBtn;

	// Token: 0x040016A0 RID: 5792
	private ChaoGetPartsBase m_chaoGetParts;

	// Token: 0x040016A1 RID: 5793
	private HudFlagWatcher m_SeFlagHatch;

	// Token: 0x040016A2 RID: 5794
	private HudFlagWatcher m_SeFlagBreak;

	// Token: 0x040016A3 RID: 5795
	private ChaoGetPartsBase.BtnType m_btnType;

	// Token: 0x040016A4 RID: 5796
	private RouletteUtility.AchievementType m_achievementType;

	// Token: 0x040016A5 RID: 5797
	private GameObject[] m_buttons = new GameObject[3];

	// Token: 0x040016A6 RID: 5798
	private ChaoGetWindow.State m_state = ChaoGetWindow.State.END;

	// Token: 0x02000353 RID: 851
	private enum State
	{
		// Token: 0x040016A8 RID: 5800
		NONE = -1,
		// Token: 0x040016A9 RID: 5801
		PLAYING,
		// Token: 0x040016AA RID: 5802
		WAIT_CLICK_NEXT_BUTTON,
		// Token: 0x040016AB RID: 5803
		CLICKED_NEXT_BUTTON,
		// Token: 0x040016AC RID: 5804
		END,
		// Token: 0x040016AD RID: 5805
		NUM
	}
}
