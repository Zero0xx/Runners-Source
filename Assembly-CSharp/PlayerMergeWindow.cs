using System;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x02000358 RID: 856
public class PlayerMergeWindow : WindowBase
{
	// Token: 0x170003CB RID: 971
	// (get) Token: 0x06001966 RID: 6502 RVA: 0x00093A0C File Offset: 0x00091C0C
	// (set) Token: 0x06001967 RID: 6503 RVA: 0x00093A14 File Offset: 0x00091C14
	public bool isSetuped
	{
		get
		{
			return this.m_isSetup;
		}
		set
		{
			this.m_isSetup = false;
		}
	}

	// Token: 0x06001968 RID: 6504 RVA: 0x00093A20 File Offset: 0x00091C20
	public void PlayStart(int playerId, RouletteUtility.AchievementType achievement = RouletteUtility.AchievementType.NONE)
	{
		this.m_charaInfo = CharacterDataNameInfo.Instance.GetDataByServerID(playerId);
		RouletteManager.OpenRouletteWindow();
		ServerPlayerState playerState = ServerInterface.PlayerState;
		ServerCharacterState serverCharacterState = playerState.CharacterState(this.m_charaInfo.m_ID);
		int totalLevel = MenuPlayerSetUtil.GetTotalLevel(this.m_charaInfo.m_ID);
		int star = serverCharacterState.star;
		int starMax = serverCharacterState.starMax;
		int num = star - 1;
		this.m_backKeyVaild = false;
		this.m_isEnd = false;
		this.m_achievementType = achievement;
		if (!this.m_isSetup)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "pattern_btn_use");
			if (gameObject != null)
			{
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "pattern_0");
				if (gameObject2 != null)
				{
					UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(gameObject2, "Btn_ok");
					if (uibuttonMessage != null)
					{
						uibuttonMessage.target = base.gameObject;
						uibuttonMessage.functionName = "OkButtonClickedCallback";
					}
				}
			}
			UIButtonMessage uibuttonMessage2 = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "skip_collider");
			if (uibuttonMessage2 != null)
			{
				uibuttonMessage2.target = base.gameObject;
				uibuttonMessage2.functionName = "SkipButtonClickedCallback";
			}
			this.m_SeFlagFog = new HudFlagWatcher();
			GameObject watchObject = GameObjectUtil.FindChildGameObject(base.gameObject, "SE_flag_fog");
			this.m_SeFlagFog.Setup(watchObject, new HudFlagWatcher.ValueChangeCallback(this.SeFlagFogCallback));
			this.m_isSetup = true;
		}
		base.gameObject.SetActive(true);
		GameObject gameObject3 = GameObjectUtil.FindChildGameObject(base.gameObject, "skip_collider");
		if (gameObject3 != null)
		{
			gameObject3.SetActive(true);
		}
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component != null)
		{
			component.Stop();
			ActiveAnimation activeAnimation = ActiveAnimation.Play(component, "ui_menu_player_merge_Window_Anim", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.InAnimationEndCallback), true);
			UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(base.gameObject, "Btn_ok");
			if (uiimageButton != null)
			{
				uiimageButton.isEnabled = false;
			}
		}
		foreach (string name in PlayerMergeWindow.PlayerIconNameList)
		{
			UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, name);
			if (uitexture != null)
			{
				TextureRequestChara request = new TextureRequestChara(this.m_charaInfo.m_ID, uitexture);
				TextureAsyncLoadManager.Instance.Request(request);
			}
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_player_name");
		if (uilabel != null && this.m_charaInfo != null)
		{
			uilabel.gameObject.SetActive(true);
			uilabel.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaName", this.m_charaInfo.m_name.ToLower()).text;
		}
		UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_player_lv");
		if (uilabel2 != null && this.m_charaInfo != null)
		{
			uilabel2.gameObject.SetActive(true);
			uilabel2.text = string.Format("Lv.{0:D3}", totalLevel);
		}
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_player_speacies");
		if (uisprite != null && this.m_charaInfo != null)
		{
			uisprite.gameObject.SetActive(true);
			switch (this.m_charaInfo.m_attribute)
			{
			case CharacterAttribute.SPEED:
				uisprite.spriteName = "ui_mm_player_species_0";
				break;
			case CharacterAttribute.FLY:
				uisprite.spriteName = "ui_mm_player_species_1";
				break;
			case CharacterAttribute.POWER:
				uisprite.spriteName = "ui_mm_player_species_2";
				break;
			}
		}
		UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_player_genus");
		if (uisprite2 != null && this.m_charaInfo != null)
		{
			uisprite2.gameObject.SetActive(true);
			uisprite2.spriteName = HudUtility.GetTeamAttributeSpriteName(this.m_charaInfo.m_ID);
		}
		GameObject gameObject4 = GameObjectUtil.FindChildGameObject(base.gameObject, "star_lv_before");
		if (gameObject4 != null)
		{
			UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject4, "Lbl_player_star_lv");
			if (uilabel3 != null && this.m_charaInfo != null)
			{
				uilabel3.gameObject.SetActive(true);
				uilabel3.text = string.Format("{0:D2}", num);
				uilabel3.color = this.m_starLabelColor;
			}
		}
		GameObject gameObject5 = GameObjectUtil.FindChildGameObject(base.gameObject, "star_lv_after");
		if (gameObject5 != null)
		{
			UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject5, "Lbl_player_star_lv");
			if (uilabel4 != null && this.m_charaInfo != null)
			{
				uilabel4.gameObject.SetActive(true);
				uilabel4.text = string.Format("{0:D2}", star);
				if (star >= starMax)
				{
					uilabel4.color = this.m_maxStarLabelColor;
				}
				else
				{
					uilabel4.color = this.m_starLabelColor;
				}
			}
		}
		UILabel uilabel5 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_player_attribute");
		if (uilabel5 != null && this.m_charaInfo != null)
		{
			GameObject gameObject6 = ResourceManager.Instance.GetGameObject(ResourceCategory.QUICK_MODE, "StageTimeTable");
			GameObject gameObject7 = ResourceManager.Instance.GetGameObject(ResourceCategory.ETC, "OverlapBonusTable");
			if (gameObject6 != null && gameObject7 != null)
			{
				StageTimeTable component2 = gameObject6.GetComponent<StageTimeTable>();
				OverlapBonusTable component3 = gameObject7.GetComponent<OverlapBonusTable>();
				if (component2 != null && component3 != null)
				{
					int tableItemData = component2.GetTableItemData(StageTimeTableItem.OverlapBonus);
					uilabel5.gameObject.SetActive(true);
					TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "chara_overlap_text");
					text.ReplaceTag("{BEFORE_TIME}", (num * tableItemData).ToString());
					text.ReplaceTag("{AFTER_TIME}", (star * tableItemData).ToString());
					if (component3.GetStarBonusList(this.m_charaInfo.m_ID, star).ContainsKey(BonusParam.BonusType.SCORE))
					{
						text.ReplaceTag("{BEFORE_PARAM}", component3.GetStarBonusList(this.m_charaInfo.m_ID, star - 1)[BonusParam.BonusType.SCORE].ToString());
						text.ReplaceTag("{AFTER_PARAM}", component3.GetStarBonusList(this.m_charaInfo.m_ID, star)[BonusParam.BonusType.SCORE].ToString());
					}
					uilabel5.text = text.text;
				}
			}
		}
		UIDraggablePanel uidraggablePanel = GameObjectUtil.FindChildGameObjectComponent<UIDraggablePanel>(base.gameObject, "window_chaoset_alpha_clip");
		if (uidraggablePanel != null)
		{
			uidraggablePanel.ResetPosition();
		}
		GameObject gameObject8 = GameObjectUtil.FindChildGameObject(base.gameObject, "eff_4");
		GameObject gameObject9 = GameObjectUtil.FindChildGameObject(base.gameObject, "eff_6");
		GameObject gameObject10 = GameObjectUtil.FindChildGameObject(base.gameObject, "eff_7");
		if (gameObject8 != null)
		{
			gameObject8.SetActive(true);
		}
		if (gameObject9 != null)
		{
			gameObject9.SetActive(true);
		}
		if (gameObject10 != null)
		{
			gameObject10.SetActive(true);
		}
	}

	// Token: 0x170003CC RID: 972
	// (get) Token: 0x06001969 RID: 6505 RVA: 0x00094134 File Offset: 0x00092334
	public bool IsPlayEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x0600196A RID: 6506 RVA: 0x0009413C File Offset: 0x0009233C
	private void Update()
	{
		if (this.m_SeFlagFog != null)
		{
			this.m_SeFlagFog.Update();
		}
	}

	// Token: 0x0600196B RID: 6507 RVA: 0x00094154 File Offset: 0x00092354
	private void SeFlagFogCallback(float newValue, float prevValue)
	{
		if (newValue == 1f)
		{
			ChaoWindowUtility.PlaySEChaoUnite(this.m_playerId);
		}
	}

	// Token: 0x0600196C RID: 6508 RVA: 0x0009416C File Offset: 0x0009236C
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
			ActiveAnimation activeAnimation = ActiveAnimation.Play(component, "ui_menu_player_merge_Window_outro_Anim", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OutAnimationEndCallback), true);
		}
		this.m_backKeyVaild = false;
	}

	// Token: 0x0600196D RID: 6509 RVA: 0x000941F4 File Offset: 0x000923F4
	private void InAnimationEndCallback()
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
		this.m_backKeyVaild = true;
	}

	// Token: 0x0600196E RID: 6510 RVA: 0x00094250 File Offset: 0x00092450
	private void OutAnimationEndCallback()
	{
		base.gameObject.SetActive(false);
		this.m_isEnd = true;
	}

	// Token: 0x0600196F RID: 6511 RVA: 0x00094268 File Offset: 0x00092468
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

	// Token: 0x06001970 RID: 6512 RVA: 0x00094308 File Offset: 0x00092508
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (msg != null)
		{
			msg.StaySequence();
		}
		if (this.m_backKeyVaild)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "pattern_0");
			if (gameObject != null)
			{
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "Btn_ok");
				if (gameObject2 != null)
				{
					UIButtonMessage component = gameObject2.GetComponent<UIButtonMessage>();
					if (component != null)
					{
						component.SendMessage("OnClick");
					}
				}
			}
		}
	}

	// Token: 0x040016CB RID: 5835
	private bool m_isSetup;

	// Token: 0x040016CC RID: 5836
	private int m_playerId;

	// Token: 0x040016CD RID: 5837
	private int m_level;

	// Token: 0x040016CE RID: 5838
	private int m_rarity;

	// Token: 0x040016CF RID: 5839
	private int m_star;

	// Token: 0x040016D0 RID: 5840
	private bool m_backKeyVaild;

	// Token: 0x040016D1 RID: 5841
	private bool m_isEnd;

	// Token: 0x040016D2 RID: 5842
	private RouletteUtility.AchievementType m_achievementType;

	// Token: 0x040016D3 RID: 5843
	private static readonly string[] PlayerIconNameList = new string[]
	{
		"img_player_1",
		"img_player_2",
		"img_player_3"
	};

	// Token: 0x040016D4 RID: 5844
	private HudFlagWatcher m_SeFlagFog;

	// Token: 0x040016D5 RID: 5845
	private CharacterDataNameInfo.Info m_charaInfo;

	// Token: 0x040016D6 RID: 5846
	[SerializeField]
	private Color m_starLabelColor = new Color32(239, 236, 0, byte.MaxValue);

	// Token: 0x040016D7 RID: 5847
	[SerializeField]
	private Color m_maxStarLabelColor = new Color32(246, 116, 0, byte.MaxValue);
}
