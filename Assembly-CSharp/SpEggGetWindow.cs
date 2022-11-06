using System;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x02000365 RID: 869
public class SpEggGetWindow : WindowBase
{
	// Token: 0x060019AD RID: 6573 RVA: 0x000958DC File Offset: 0x00093ADC
	private void Start()
	{
	}

	// Token: 0x060019AE RID: 6574 RVA: 0x000958E0 File Offset: 0x00093AE0
	private void Update()
	{
		if (this.m_SeFlagHatch != null)
		{
			this.m_SeFlagHatch.Update();
		}
		if (this.m_SeFlagBreak != null)
		{
			this.m_SeFlagBreak.Update();
		}
		if (this.m_SeFlagSpEgg != null)
		{
			this.m_SeFlagSpEgg.Update();
		}
	}

	// Token: 0x060019AF RID: 6575 RVA: 0x00095930 File Offset: 0x00093B30
	public void PlayStart(SpEggGetPartsBase spEggGetParts, RouletteUtility.AchievementType achievement = RouletteUtility.AchievementType.NONE)
	{
		RouletteManager.OpenRouletteWindow();
		this.m_achievementType = achievement;
		this.m_spEggGetParts = spEggGetParts;
		this.m_state = SpEggGetWindow.State.PLAYING;
		this.m_isOpened = false;
		if (this.m_caption != null)
		{
			this.m_caption.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "gw_get_chao_caption").text;
		}
		if (!this.m_isSetuped)
		{
			UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "Btn_ok");
			if (uibuttonMessage != null)
			{
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "OkButtonClickedCallback";
			}
			UIButtonMessage uibuttonMessage2 = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "skip_collider");
			if (uibuttonMessage2 != null)
			{
				uibuttonMessage2.target = base.gameObject;
				uibuttonMessage2.functionName = "SkipButtonClickedCallback";
			}
			this.m_SeFlagHatch = new HudFlagWatcher();
			GameObject watchObject = GameObjectUtil.FindChildGameObject(base.gameObject, "SE_flag");
			this.m_SeFlagHatch.Setup(watchObject, new HudFlagWatcher.ValueChangeCallback(this.SeFlagHatchCallback));
			this.m_SeFlagBreak = new HudFlagWatcher();
			GameObject watchObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "SE_flag_break");
			this.m_SeFlagBreak.Setup(watchObject2, new HudFlagWatcher.ValueChangeCallback(this.SeFlagBreakCallback));
			this.m_SeFlagSpEgg = new HudFlagWatcher();
			GameObject watchObject3 = GameObjectUtil.FindChildGameObject(base.gameObject, "SE_flag_spegg");
			this.m_SeFlagSpEgg.Setup(watchObject3, new HudFlagWatcher.ValueChangeCallback(this.SeFlagSpEggCallback));
			this.m_isSetuped = true;
		}
		base.gameObject.SetActive(true);
		if (this.m_spEggGetParts != null)
		{
			this.m_spEggGetParts.Setup(base.gameObject);
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "skip_collider");
		if (gameObject != null)
		{
			gameObject.SetActive(true);
		}
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(component, Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.InAnimationFinishCallback), true);
			UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(base.gameObject, "Btn_ok");
			if (uiimageButton != null)
			{
				uiimageButton.isEnabled = false;
			}
		}
		UIDraggablePanel uidraggablePanel = GameObjectUtil.FindChildGameObjectComponent<UIDraggablePanel>(base.gameObject, "window_chaoset_alpha_clip");
		if (uidraggablePanel != null)
		{
			uidraggablePanel.ResetPosition();
		}
		SoundManager.SePlay("sys_window_open", "SE");
	}

	// Token: 0x170003CF RID: 975
	// (get) Token: 0x060019B0 RID: 6576 RVA: 0x00095B94 File Offset: 0x00093D94
	public bool IsPlayEnd
	{
		get
		{
			return this.m_state == SpEggGetWindow.State.END;
		}
	}

	// Token: 0x060019B1 RID: 6577 RVA: 0x00095BA0 File Offset: 0x00093DA0
	private void OkButtonClickedCallback()
	{
		RouletteManager.CloseRouletteWindow();
		SoundManager.SePlay("sys_menu_decide", "SE");
		if (this.m_achievementType != RouletteUtility.AchievementType.NONE)
		{
			RouletteManager.RouletteGetWindowClose(this.m_achievementType, RouletteUtility.NextType.NONE);
			this.m_achievementType = RouletteUtility.AchievementType.NONE;
		}
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(component, "ui_menu_chao_egg_transform_Window_outro_Anim", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OutAnimationFinishedCallback), true);
		}
	}

	// Token: 0x060019B2 RID: 6578 RVA: 0x00095C20 File Offset: 0x00093E20
	private void SeFlagHatchCallback(float newValue, float prevValue)
	{
		if (newValue == 1f && this.m_spEggGetParts != null)
		{
			this.m_spEggGetParts.PlaySE(ChaoWindowUtility.SeHatch);
		}
	}

	// Token: 0x060019B3 RID: 6579 RVA: 0x00095C54 File Offset: 0x00093E54
	private void SeFlagBreakCallback(float newValue, float prevValue)
	{
		if (newValue == 1f && this.m_spEggGetParts != null)
		{
			this.m_spEggGetParts.PlaySE(ChaoWindowUtility.SeBreak);
		}
	}

	// Token: 0x060019B4 RID: 6580 RVA: 0x00095C88 File Offset: 0x00093E88
	private void SeFlagSpEggCallback(float newValue, float prevValue)
	{
		if (newValue == 1f)
		{
			if (this.m_spEggGetParts != null)
			{
				this.m_spEggGetParts.PlaySE(ChaoWindowUtility.SeSpEgg);
			}
			if (this.m_caption != null)
			{
				this.m_caption.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "gw_got_special_egg_caption").text;
			}
		}
	}

	// Token: 0x060019B5 RID: 6581 RVA: 0x00095CEC File Offset: 0x00093EEC
	private void InAnimationFinishCallback()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "skip_collider");
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
		UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(base.gameObject, "Btn_ok");
		if (uiimageButton != null)
		{
			uiimageButton.isEnabled = true;
		}
		this.m_isOpened = true;
	}

	// Token: 0x060019B6 RID: 6582 RVA: 0x00095D48 File Offset: 0x00093F48
	private void OutAnimationFinishedCallback()
	{
		this.DeleteChaoTexture();
		base.gameObject.SetActive(false);
		this.m_isOpened = false;
		this.m_state = SpEggGetWindow.State.END;
	}

	// Token: 0x060019B7 RID: 6583 RVA: 0x00095D78 File Offset: 0x00093F78
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
		if (this.m_caption != null)
		{
			this.m_caption.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "gw_got_special_egg_caption").text;
		}
	}

	// Token: 0x060019B8 RID: 6584 RVA: 0x00095E48 File Offset: 0x00094048
	private void OnSetChaoTexture(ChaoTextureManager.TextureData data)
	{
		UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, "img_chao_1");
		if (uitexture != null)
		{
			uitexture.mainTexture = data.tex;
			uitexture.enabled = true;
		}
	}

	// Token: 0x060019B9 RID: 6585 RVA: 0x00095E88 File Offset: 0x00094088
	private void DeleteChaoTexture()
	{
		UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, "img_chao_1");
		if (uitexture != null)
		{
			uitexture.mainTexture = null;
			uitexture.enabled = false;
			ChaoTextureManager.Instance.RemoveChaoTexture(this.m_spEggGetParts.ChaoId);
		}
	}

	// Token: 0x060019BA RID: 6586 RVA: 0x00095ED8 File Offset: 0x000940D8
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (this.m_isOpened)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_ok");
			if (gameObject != null)
			{
				UIButtonMessage component = gameObject.GetComponent<UIButtonMessage>();
				if (component != null)
				{
					component.SendMessage("OnClick");
				}
			}
		}
		if (msg != null)
		{
			msg.StaySequence();
		}
	}

	// Token: 0x040016F3 RID: 5875
	private bool m_isSetuped;

	// Token: 0x040016F4 RID: 5876
	private bool m_isOpened;

	// Token: 0x040016F5 RID: 5877
	[SerializeField]
	private UILabel m_caption;

	// Token: 0x040016F6 RID: 5878
	private SpEggGetPartsBase m_spEggGetParts;

	// Token: 0x040016F7 RID: 5879
	private HudFlagWatcher m_SeFlagHatch;

	// Token: 0x040016F8 RID: 5880
	private HudFlagWatcher m_SeFlagBreak;

	// Token: 0x040016F9 RID: 5881
	private HudFlagWatcher m_SeFlagSpEgg;

	// Token: 0x040016FA RID: 5882
	private RouletteUtility.AchievementType m_achievementType;

	// Token: 0x040016FB RID: 5883
	private SpEggGetWindow.State m_state = SpEggGetWindow.State.END;

	// Token: 0x02000366 RID: 870
	private enum State
	{
		// Token: 0x040016FD RID: 5885
		NONE = -1,
		// Token: 0x040016FE RID: 5886
		PLAYING,
		// Token: 0x040016FF RID: 5887
		END,
		// Token: 0x04001700 RID: 5888
		NUM
	}
}
