using System;
using System.Collections.Generic;
using AnimationOrTween;
using DataTable;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x02000424 RID: 1060
public class DeckViewWindow : WindowBase
{
	// Token: 0x06001FFC RID: 8188 RVA: 0x000BEAA4 File Offset: 0x000BCCA4
	private void OnDestroy()
	{
		base.Destroy();
	}

	// Token: 0x17000472 RID: 1138
	// (get) Token: 0x06001FFD RID: 8189 RVA: 0x000BEAAC File Offset: 0x000BCCAC
	public static DeckViewWindow instance
	{
		get
		{
			return DeckViewWindow.s_instance;
		}
	}

	// Token: 0x17000473 RID: 1139
	// (get) Token: 0x06001FFE RID: 8190 RVA: 0x000BEAB4 File Offset: 0x000BCCB4
	public static bool isActive
	{
		get
		{
			return DeckViewWindow.s_instance != null && DeckViewWindow.s_instance.gameObject.activeSelf;
		}
	}

	// Token: 0x06001FFF RID: 8191 RVA: 0x000BEAD8 File Offset: 0x000BCCD8
	public void Init()
	{
		this.m_btnLock = false;
		this.m_parent = null;
		UIPanel uipanel = GameObjectUtil.FindChildGameObjectComponent<UIPanel>(base.gameObject, "DeckViewWindow");
		if (uipanel != null)
		{
			uipanel.alpha = 0f;
		}
		if (this.m_bgCollider != null)
		{
			this.m_bgCollider.enabled = false;
		}
		if (this.m_windowRoot == null)
		{
			this.m_windowRoot = GameObjectUtil.FindChildGameObject(base.gameObject, "window");
		}
		if (this.m_windowRoot != null)
		{
			this.m_windowRoot.SetActive(false);
		}
		this.m_change = false;
		this.m_chaoMainId = -1;
		this.m_chaoSubId = -1;
		this.m_playerMain = CharaType.UNKNOWN;
		this.m_playerSub = CharaType.UNKNOWN;
		this.m_init = false;
		this.m_close = false;
		this.m_chaoSpIconTime = 0f;
		this.m_initChaoSetStock = 0;
		this.m_initPlayerMain = CharaType.UNKNOWN;
		this.m_initPlayerSub = CharaType.UNKNOWN;
		this.m_initChaoMain = -1;
		this.m_initChaoSub = -1;
		this.SetChangeBtn();
		this.ResetBtnDelayTime();
		this.ResetLastInputTime(DeckViewWindow.SELECT_TYPE.NUM);
	}

	// Token: 0x06002000 RID: 8192 RVA: 0x000BEBF0 File Offset: 0x000BCDF0
	public void SetChangeBtn()
	{
		if (this.m_changeBtnList == null)
		{
			this.m_changeBtnList = new Dictionary<DeckViewWindow.SELECT_TYPE, List<UIImageButton>>();
		}
		else if (this.m_changeBtnList.Count > 0)
		{
			this.m_changeBtnList.Clear();
		}
		if (this.m_changeBtnList != null)
		{
			GameObject parent = GameObjectUtil.FindChildGameObject(base.gameObject, "info_player");
			GameObject parent2 = GameObjectUtil.FindChildGameObject(base.gameObject, "info_chao");
			int num = 4;
			for (int i = 0; i < num; i++)
			{
				DeckViewWindow.SELECT_TYPE key = (DeckViewWindow.SELECT_TYPE)i;
				List<UIImageButton> list = new List<UIImageButton>();
				switch (key)
				{
				case DeckViewWindow.SELECT_TYPE.CHARA_MAIN:
				{
					UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(parent, "Btn_main_up");
					UIImageButton uiimageButton2 = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(parent, "Btn_main_down");
					if (uiimageButton != null)
					{
						uiimageButton.isEnabled = true;
					}
					if (uiimageButton2 != null)
					{
						uiimageButton2.isEnabled = true;
					}
					list.Add(uiimageButton);
					list.Add(uiimageButton2);
					break;
				}
				case DeckViewWindow.SELECT_TYPE.CHARA_SUB:
				{
					UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(parent, "Btn_sub_up");
					UIImageButton uiimageButton2 = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(parent, "Btn_sub_down");
					if (uiimageButton != null)
					{
						uiimageButton.isEnabled = true;
					}
					if (uiimageButton2 != null)
					{
						uiimageButton2.isEnabled = true;
					}
					list.Add(uiimageButton);
					list.Add(uiimageButton2);
					break;
				}
				case DeckViewWindow.SELECT_TYPE.CHAO_MAIN:
				{
					UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(parent2, "Btn_main_up");
					UIImageButton uiimageButton2 = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(parent2, "Btn_main_down");
					if (uiimageButton != null)
					{
						uiimageButton.isEnabled = true;
					}
					if (uiimageButton2 != null)
					{
						uiimageButton2.isEnabled = true;
					}
					list.Add(uiimageButton);
					list.Add(uiimageButton2);
					break;
				}
				case DeckViewWindow.SELECT_TYPE.CHAO_SUB:
				{
					UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(parent2, "Btn_sub_up");
					UIImageButton uiimageButton2 = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(parent2, "Btn_sub_down");
					if (uiimageButton != null)
					{
						uiimageButton.isEnabled = true;
					}
					if (uiimageButton2 != null)
					{
						uiimageButton2.isEnabled = true;
					}
					list.Add(uiimageButton);
					list.Add(uiimageButton2);
					break;
				}
				}
				if (list.Count > 0)
				{
					this.m_changeBtnList.Add(key, list);
				}
			}
		}
	}

	// Token: 0x06002001 RID: 8193 RVA: 0x000BEE18 File Offset: 0x000BD018
	public void ResetBtnDelayTime()
	{
		this.SetAllChangeBtnEnabled(true);
		this.m_changeDelayCheckTime = 0f;
	}

	// Token: 0x06002002 RID: 8194 RVA: 0x000BEE2C File Offset: 0x000BD02C
	public void Setup(int mainChaoId, int subChaoId, GameObject parent)
	{
		this.m_btnLock = false;
		DeckViewWindow.s_instance = this;
		base.gameObject.SetActive(true);
		this.m_parent = parent;
		this.m_change = false;
		this.m_chaoMainId = mainChaoId;
		this.m_chaoSubId = subChaoId;
		this.m_init = true;
		this.m_close = false;
		this.m_chaoSpIconTime = 0f;
		this.m_windowAnimation = base.gameObject.GetComponentInChildren<Animation>();
		ActiveAnimation.Play(this.m_windowAnimation, Direction.Forward);
		this.SetChangeBtn();
		this.ResetBtnDelayTime();
		this.m_currentChaoSetStock = DeckUtil.GetDeckCurrentStockIndex();
		this.m_initChaoSetStock = this.m_currentChaoSetStock;
		this.m_isSaveData = new List<bool>();
		this.m_isSaveData.Add(true);
		this.m_isSaveData.Add(DeckUtil.IsChaoSetSave(1));
		this.m_isSaveData.Add(DeckUtil.IsChaoSetSave(2));
		this.ResetLastInputTime(DeckViewWindow.SELECT_TYPE.NUM);
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "info_notice");
		if (gameObject != null)
		{
			this.m_detailTextBg = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_base_bg");
			this.m_detailTextLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_bonusnotice");
		}
		if (this.m_detailTextBg != null && this.m_detailTextLabel != null)
		{
			this.m_detailTextBg.alpha = 0f;
			this.m_detailTextLabel.alpha = 0f;
			TweenAlpha component = this.m_detailTextBg.GetComponent<TweenAlpha>();
			TweenAlpha component2 = this.m_detailTextLabel.GetComponent<TweenAlpha>();
			if (component != null && component2 != null)
			{
				component.enabled = false;
				component2.enabled = false;
			}
		}
		if (this.m_windowRoot == null)
		{
			this.m_windowRoot = GameObjectUtil.FindChildGameObject(base.gameObject, "window");
		}
		if (this.m_windowRoot != null)
		{
			this.m_windowRoot.SetActive(true);
		}
		this.m_bgCollider = GameObjectUtil.FindChildGameObjectComponent<BoxCollider>(base.gameObject, "blinder");
		if (this.m_bgCollider != null)
		{
			this.m_bgCollider.enabled = true;
		}
		UIPlayAnimation[] componentsInChildren = base.gameObject.GetComponentsInChildren<UIPlayAnimation>();
		if (componentsInChildren != null && componentsInChildren.Length > 0)
		{
			foreach (UIPlayAnimation uiplayAnimation in componentsInChildren)
			{
				uiplayAnimation.enabled = true;
				if (uiplayAnimation.onFinished == null || uiplayAnimation.onFinished.Count == 0)
				{
					EventDelegate.Add(uiplayAnimation.onFinished, new EventDelegate.Callback(this.OnFinished), false);
				}
			}
		}
		SaveDataManager instance = SaveDataManager.Instance;
		this.m_initPlayerMain = instance.PlayerData.MainChara;
		this.m_initPlayerSub = instance.PlayerData.SubChara;
		this.m_initChaoMain = mainChaoId;
		this.m_initChaoSub = subChaoId;
		this.SetupChaoView();
		this.SetupPlayerView(instance);
		this.SetupBonusView();
		this.SetupTabView();
		this.m_charaList = null;
		if (instance != null)
		{
			ServerPlayerState playerState = ServerInterface.PlayerState;
			if (playerState != null)
			{
				int num = 29;
				for (int j = 0; j < num; j++)
				{
					ServerCharacterState serverCharacterState = playerState.CharacterState((CharaType)j);
					if (serverCharacterState != null && serverCharacterState.IsUnlocked)
					{
						CharaType charaType = (CharaType)j;
						if (this.m_charaList == null)
						{
							this.m_charaList = new List<CharaType>();
							this.m_charaList.Add(charaType);
						}
						else
						{
							this.m_charaList.Add(charaType);
						}
						global::Debug.Log("use chara:" + charaType);
					}
				}
			}
		}
		this.m_currentChaoSort = ChaoSort.NONE;
		this.m_chaoList = ChaoTable.GetPossessionChaoData();
	}

	// Token: 0x06002003 RID: 8195 RVA: 0x000BF1CC File Offset: 0x000BD3CC
	private void SetupBonusView()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "info_bonus");
		if (gameObject != null)
		{
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_bonus_0");
			UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_bonus_1");
			UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_bonus_2");
			UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_bonus_3");
			UILabel uilabel5 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_bonus_4");
			this.SetBonus(ref uilabel, ref uilabel2, ref uilabel3, ref uilabel4, ref uilabel5);
		}
	}

	// Token: 0x06002004 RID: 8196 RVA: 0x000BF244 File Offset: 0x000BD444
	private void SetBonus(ref UILabel scoreBonus, ref UILabel ringBonus, ref UILabel animalBonus, ref UILabel distanceBonus, ref UILabel enemyBonus)
	{
		BonusParamContainer currentBonusData = BonusUtil.GetCurrentBonusData(this.m_playerMain, this.m_playerSub, this.m_chaoMainId, this.m_chaoSubId);
		if (currentBonusData != null)
		{
			int index = -1;
			Dictionary<BonusParam.BonusType, float> bonusInfo = currentBonusData.GetBonusInfo(index);
			this.SetupAbilityIcon(currentBonusData);
			this.SetupNoticeView(currentBonusData);
			if (bonusInfo != null)
			{
				if (bonusInfo.ContainsKey(BonusParam.BonusType.SCORE))
				{
					this.SetBonusParam(ref scoreBonus, bonusInfo[BonusParam.BonusType.SCORE]);
				}
				else
				{
					this.SetBonusParam(ref scoreBonus, 0f);
				}
				if (bonusInfo.ContainsKey(BonusParam.BonusType.RING))
				{
					this.SetBonusParam(ref ringBonus, bonusInfo[BonusParam.BonusType.RING]);
				}
				else
				{
					this.SetBonusParam(ref ringBonus, 0f);
				}
				if (bonusInfo.ContainsKey(BonusParam.BonusType.ANIMAL))
				{
					this.SetBonusParam(ref animalBonus, bonusInfo[BonusParam.BonusType.ANIMAL]);
				}
				else
				{
					this.SetBonusParam(ref animalBonus, 0f);
				}
				if (bonusInfo.ContainsKey(BonusParam.BonusType.DISTANCE))
				{
					this.SetBonusParam(ref distanceBonus, bonusInfo[BonusParam.BonusType.DISTANCE]);
				}
				else
				{
					this.SetBonusParam(ref distanceBonus, 0f);
				}
				if (bonusInfo.ContainsKey(BonusParam.BonusType.ENEMY_OBJBREAK))
				{
					this.SetBonusParam(ref enemyBonus, bonusInfo[BonusParam.BonusType.ENEMY_OBJBREAK]);
				}
				else
				{
					this.SetBonusParam(ref enemyBonus, 0f);
				}
			}
		}
	}

	// Token: 0x06002005 RID: 8197 RVA: 0x000BF370 File Offset: 0x000BD570
	private void SetBonusParam(ref UILabel bonusLabel, float param)
	{
		if (bonusLabel != null)
		{
			bonusLabel.text = TextUtility.Replaces(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "DailyMission", "clear_percent").text, new Dictionary<string, string>
			{
				{
					"{PARAM}",
					param.ToString()
				}
			});
		}
	}

	// Token: 0x06002006 RID: 8198 RVA: 0x000BF3C4 File Offset: 0x000BD5C4
	private void SetupTabView()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Deck_tab");
		if (gameObject != null)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			list.Add("tab_1");
			list.Add("tab_2");
			list.Add("tab_3");
			list.Add("tab_4");
			list.Add("tab_5");
			list2.Add("OnClickTab1");
			list2.Add("OnClickTab2");
			list2.Add("OnClickTab3");
			list2.Add("OnClickTab4");
			list2.Add("OnClickTab5");
			GeneralUtil.SetToggleObject(base.gameObject, gameObject, list2, list, this.m_currentChaoSetStock, true);
		}
	}

	// Token: 0x06002007 RID: 8199 RVA: 0x000BF480 File Offset: 0x000BD680
	private void SetupChaoView()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "info_chao");
		if (gameObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "chao_main");
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject, "chao_sub");
			if (gameObject2 != null)
			{
				UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject2, "Lbl_chao_main_lv");
				UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject2, "Lbl_chao_main_name");
				UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject2, "img_chao_rank_bg_main");
				UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject2, "img_chao_type_main");
				UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject2, "img_chao_text_main");
				UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject2, "img_chao_main");
				this.SetChao(this.m_chaoMainId, ref uilabel2, ref uilabel, ref uisprite, ref uisprite2, ref uitexture, ref uisprite3);
			}
			if (gameObject3 != null)
			{
				UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject3, "Lbl_chao_sub_lv");
				UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject3, "Lbl_chao_sub_name");
				UISprite uisprite4 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject3, "img_chao_rank_bg_sub");
				UISprite uisprite5 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject3, "img_chao_type_sub");
				UITexture uitexture2 = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject3, "img_chao_text_sub");
				UISprite uisprite6 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject3, "img_chao_sub");
				this.SetChao(this.m_chaoSubId, ref uilabel4, ref uilabel3, ref uisprite4, ref uisprite5, ref uitexture2, ref uisprite6);
			}
		}
	}

	// Token: 0x06002008 RID: 8200 RVA: 0x000BF5A8 File Offset: 0x000BD7A8
	private void ResetupChaoView()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "info_chao");
		if (gameObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "chao_main");
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject, "chao_sub");
			if (gameObject2 != null)
			{
				UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject2, "img_chao_text_main");
				UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject2, "img_chao_main");
				this.ResetupChao(ref uitexture, ref uisprite);
			}
			if (gameObject3 != null)
			{
				UITexture uitexture2 = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject3, "img_chao_text_sub");
				UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject3, "img_chao_sub");
				this.ResetupChao(ref uitexture2, ref uisprite2);
			}
		}
	}

	// Token: 0x06002009 RID: 8201 RVA: 0x000BF64C File Offset: 0x000BD84C
	private void SetupPlayerView(SaveDataManager saveMrg = null)
	{
		if (saveMrg == null)
		{
			saveMrg = SaveDataManager.Instance;
		}
		else
		{
			PlayerData playerData = saveMrg.PlayerData;
			this.m_playerMain = CharaType.UNKNOWN;
			this.m_playerSub = CharaType.UNKNOWN;
			if (playerData != null)
			{
				this.m_playerMain = playerData.MainChara;
				this.m_playerSub = playerData.SubChara;
			}
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "info_player");
		if (gameObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "player_main");
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject, "player_sub");
			GameObject parent = GameObjectUtil.FindChildGameObject(gameObject, "base_main");
			GameObject parent2 = GameObjectUtil.FindChildGameObject(gameObject, "base_sub");
			if (gameObject2 != null)
			{
				UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject2, "Lbl_player_main_lv");
				UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject2, "Lbl_player_main_name");
				UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject2, "img_player_tex_main");
				UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject2, "img_player_main_genus");
				UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject2, "img_player_main_speacies");
				UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(parent, "base_star");
				UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject2, "Lbl_player_main_star_lv");
				this.SetPlayer(this.m_playerMain, ref uilabel2, ref uilabel, ref uitexture, ref uisprite2, ref uisprite, ref uisprite3, ref uilabel3);
			}
			if (gameObject3 != null)
			{
				UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject3, "Lbl_player_sub_lv");
				UILabel uilabel5 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject3, "Lbl_player_sub_name");
				UITexture uitexture2 = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject3, "img_player_tex_sub");
				UISprite uisprite4 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject3, "img_player_sub_genus");
				UISprite uisprite5 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject3, "img_player_sub_speacies");
				UISprite uisprite6 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(parent2, "base_star");
				UILabel uilabel6 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject3, "Lbl_player_sub_star_lv");
				this.SetPlayer(this.m_playerSub, ref uilabel5, ref uilabel4, ref uitexture2, ref uisprite5, ref uisprite4, ref uisprite6, ref uilabel6);
			}
			UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(gameObject, "Btn_change");
			if (uiimageButton != null)
			{
				if (this.m_playerMain == CharaType.UNKNOWN || this.m_playerSub == CharaType.UNKNOWN)
				{
					uiimageButton.gameObject.SetActive(false);
				}
				else
				{
					uiimageButton.gameObject.SetActive(true);
				}
			}
		}
	}

	// Token: 0x0600200A RID: 8202 RVA: 0x000BF848 File Offset: 0x000BDA48
	private void SetupNoticeView(BonusParamContainer bonus)
	{
		string a = string.Empty;
		string text;
		if (bonus.IsDetailInfo(out text) && this.m_detailTextLabel != null)
		{
			a = this.m_detailTextLabel.text;
			this.m_detailTextLabel.text = text;
		}
		if (this.m_detailTextBg != null && this.m_detailTextLabel != null)
		{
			TweenAlpha component = this.m_detailTextBg.GetComponent<TweenAlpha>();
			TweenAlpha component2 = this.m_detailTextLabel.GetComponent<TweenAlpha>();
			if (!string.IsNullOrEmpty(text))
			{
				if (component != null && component2 != null)
				{
					if (a != text)
					{
						component.Reset();
						component2.Reset();
						this.m_detailTextBg.alpha = 0f;
						this.m_detailTextLabel.alpha = 0f;
						component.enabled = true;
						component2.enabled = true;
						component.Play();
						component2.Play();
					}
					else if (!component.enabled)
					{
						this.m_detailTextBg.alpha = 0f;
						this.m_detailTextLabel.alpha = 0f;
						component.enabled = true;
						component2.enabled = true;
						component.Play();
						component2.Play();
					}
				}
			}
			else
			{
				this.m_detailTextBg.alpha = 0f;
				this.m_detailTextLabel.alpha = 0f;
				if (component != null && component2 != null)
				{
					component.Reset();
					component2.Reset();
					component.enabled = false;
					component2.enabled = false;
				}
			}
		}
	}

	// Token: 0x0600200B RID: 8203 RVA: 0x000BF9E4 File Offset: 0x000BDBE4
	private void SetupAbilityIcon(BonusParamContainer bonus)
	{
		List<UISprite> list = new List<UISprite>();
		List<UISprite> list2 = new List<UISprite>();
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "info_player");
		if (gameObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "player_main");
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject, "player_sub");
			if (gameObject2 != null)
			{
				GameObject gameObject4 = GameObjectUtil.FindChildGameObject(gameObject2, "ability");
				if (gameObject4 != null)
				{
					for (int i = 0; i < 8; i++)
					{
						UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject4, "img_ability_icon_" + i);
						if (!(uisprite != null))
						{
							break;
						}
						list.Add(uisprite);
					}
				}
			}
			if (gameObject3 != null)
			{
				GameObject gameObject5 = GameObjectUtil.FindChildGameObject(gameObject3, "ability");
				if (gameObject5 != null)
				{
					for (int j = 0; j < 8; j++)
					{
						UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject5, "img_ability_icon_" + j);
						if (!(uisprite2 != null))
						{
							break;
						}
						list2.Add(uisprite2);
					}
				}
			}
		}
		if (bonus != null)
		{
			if (list.Count > 0)
			{
				for (int k = 0; k < list.Count; k++)
				{
					list[k].enabled = false;
					list[k].gameObject.SetActive(false);
				}
			}
			if (list2.Count > 0)
			{
				for (int l = 0; l < list2.Count; l++)
				{
					list2[l].enabled = false;
					list2[l].gameObject.SetActive(false);
				}
			}
			BonusParam bonusParam = bonus.GetBonusParam(0);
			BonusParam bonusParam2 = bonus.GetBonusParam(1);
			if (bonusParam != null && bonusParam.GetBonusInfo(BonusParam.BonusTarget.CHARA, true) != null)
			{
				Dictionary<BonusParam.BonusType, float> bonusInfo = bonusParam.GetBonusInfo(BonusParam.BonusTarget.CHARA, true);
				this.SetAbilityIconSprite(ref list, bonusInfo, this.m_playerMain);
			}
			if (bonusParam2 != null && bonusParam2.GetBonusInfo(BonusParam.BonusTarget.CHARA, true) != null)
			{
				Dictionary<BonusParam.BonusType, float> bonusInfo2 = bonusParam2.GetBonusInfo(BonusParam.BonusTarget.CHARA, true);
				this.SetAbilityIconSprite(ref list2, bonusInfo2, this.m_playerSub);
			}
		}
		else
		{
			if (list.Count > 0)
			{
				for (int m = 0; m < list.Count; m++)
				{
					list[m].enabled = false;
				}
			}
			if (list2.Count > 0)
			{
				for (int n = 0; n < list2.Count; n++)
				{
					list2[n].enabled = false;
				}
			}
		}
	}

	// Token: 0x0600200C RID: 8204 RVA: 0x000BFC98 File Offset: 0x000BDE98
	private void SetAbilityIconSprite(ref List<UISprite> icons, Dictionary<BonusParam.BonusType, float> info, CharaType charaType)
	{
		if (info == null || icons == null)
		{
			return;
		}
		if (info.Count > 0)
		{
			int num = 0;
			Dictionary<BonusParam.BonusType, float>.KeyCollection keys = info.Keys;
			List<BonusParam.BonusType> list = new List<BonusParam.BonusType>();
			foreach (BonusParam.BonusType item in keys)
			{
				list.Add(item);
			}
			Dictionary<BonusParam.BonusType, bool> dictionary = BonusUtil.IsTeamBonus(charaType, list);
			foreach (BonusParam.BonusType bonusType in keys)
			{
				if (info[bonusType] != 0f && dictionary != null && dictionary.ContainsKey(bonusType) && dictionary[bonusType])
				{
					string abilityIconSpriteName = BonusUtil.GetAbilityIconSpriteName(bonusType, info[bonusType]);
					if (!string.IsNullOrEmpty(abilityIconSpriteName))
					{
						icons[num].spriteName = BonusUtil.GetAbilityIconSpriteName(bonusType, info[bonusType]);
						icons[num].enabled = true;
						icons[num].gameObject.SetActive(true);
						num++;
					}
				}
			}
		}
	}

	// Token: 0x0600200D RID: 8205 RVA: 0x000BFE0C File Offset: 0x000BE00C
	private void SetPlayer(CharaType charaType, ref UILabel name, ref UILabel lv, ref UITexture chara, ref UISprite type, ref UISprite genus, ref UISprite star, ref UILabel starLv)
	{
		bool flag = false;
		if (charaType != CharaType.NUM && charaType != CharaType.UNKNOWN && HudCharacterPanelUtil.CheckValidChara(charaType))
		{
			chara.gameObject.SetActive(true);
			TextureRequestChara request = new TextureRequestChara(charaType, chara);
			TextureAsyncLoadManager.Instance.Request(request);
			HudCharacterPanelUtil.SetName(charaType, name.gameObject);
			HudCharacterPanelUtil.SetLevel(charaType, lv.gameObject);
			HudCharacterPanelUtil.SetCharaType(charaType, type.gameObject);
			HudCharacterPanelUtil.SetTeamType(charaType, genus.gameObject);
			if (star != null && starLv != null && ServerInterface.PlayerState != null)
			{
				ServerCharacterState serverCharacterState = ServerInterface.PlayerState.CharacterState(charaType);
				if (serverCharacterState != null)
				{
					star.gameObject.SetActive(true);
					starLv.text = string.Empty + serverCharacterState.star;
					starLv.gameObject.SetActive(true);
				}
				else
				{
					star.gameObject.SetActive(false);
					starLv.gameObject.SetActive(false);
				}
			}
			flag = true;
		}
		if (!flag)
		{
			if (lv != null)
			{
				lv.text = string.Empty;
			}
			if (name != null)
			{
				name.text = string.Empty;
			}
			if (chara != null)
			{
				chara.gameObject.SetActive(false);
			}
			if (type != null)
			{
				type.spriteName = string.Empty;
			}
			if (genus != null)
			{
				genus.spriteName = string.Empty;
			}
			if (star != null && starLv != null)
			{
				star.gameObject.SetActive(false);
				starLv.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600200E RID: 8206 RVA: 0x000BFFE4 File Offset: 0x000BE1E4
	private void SetChao(int id, ref UILabel name, ref UILabel lv, ref UISprite bg, ref UISprite type, ref UITexture tex, ref UISprite chao)
	{
		DataTable.ChaoData chaoData = ChaoTable.GetChaoData(id);
		if (chaoData != null)
		{
			if (lv != null)
			{
				string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "ui_LevelNumber").text;
				lv.text = TextUtility.Replace(text, "{PARAM}", chaoData.level.ToString());
			}
			if (name != null)
			{
				name.text = chaoData.nameTwolines;
			}
			if (bg != null)
			{
				bg.spriteName = "ui_chao_set_bg_ll_" + (int)chaoData.rarity;
			}
			if (type != null)
			{
				switch (chaoData.charaAtribute)
				{
				case CharacterAttribute.SPEED:
					type.spriteName = "ui_chao_set_type_icon_speed";
					break;
				case CharacterAttribute.FLY:
					type.spriteName = "ui_chao_set_type_icon_fly";
					break;
				case CharacterAttribute.POWER:
					type.spriteName = "ui_chao_set_type_icon_power";
					break;
				default:
					type.spriteName = string.Empty;
					break;
				}
			}
			if (tex != null)
			{
				ChaoTextureManager instance = ChaoTextureManager.Instance;
				if (instance != null)
				{
					Texture loadedTexture = ChaoTextureManager.Instance.GetLoadedTexture(id);
					if (loadedTexture != null)
					{
						tex.mainTexture = loadedTexture;
					}
					else
					{
						ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(tex, null, true);
						ChaoTextureManager.Instance.GetTexture(id, info);
					}
					tex.alpha = 1f;
					chao.alpha = 0f;
					tex.enabled = true;
				}
			}
			else if (chao != null)
			{
				chao.enabled = true;
				chao.alpha = 1f;
			}
		}
		else
		{
			if (lv != null)
			{
				lv.text = string.Empty;
			}
			if (name != null)
			{
				name.text = string.Empty;
			}
			if (bg != null)
			{
				bg.spriteName = "ui_chao_set_ll_3";
			}
			if (type != null)
			{
				type.spriteName = string.Empty;
			}
			if (tex != null)
			{
				tex.alpha = 0f;
				tex.mainTexture = null;
			}
			if (chao != null)
			{
				chao.spriteName = string.Empty;
				chao.alpha = 0f;
			}
		}
	}

	// Token: 0x0600200F RID: 8207 RVA: 0x000C0264 File Offset: 0x000BE464
	private void ResetupChao(ref UITexture tex, ref UISprite chao)
	{
		if (chao != null && tex != null && chao.enabled && chao.alpha >= 0.1f && tex.alpha >= 0f && !string.IsNullOrEmpty(chao.spriteName))
		{
			string s = chao.spriteName.Replace("ui_tex_chao_", string.Empty);
			int num = int.Parse(s);
			if (num >= 0)
			{
				ChaoTextureManager instance = ChaoTextureManager.Instance;
				if (instance != null)
				{
					Texture loadedTexture = ChaoTextureManager.Instance.GetLoadedTexture(num);
					if (loadedTexture != null)
					{
						tex.mainTexture = loadedTexture;
					}
					else
					{
						ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(tex, null, true);
						ChaoTextureManager.Instance.GetTexture(num, info);
					}
					tex.alpha = 1f;
					chao.alpha = 0f;
					tex.enabled = true;
				}
			}
		}
	}

	// Token: 0x06002010 RID: 8208 RVA: 0x000C0360 File Offset: 0x000BE560
	private void Update()
	{
		if (this.m_pressBtnType != DeckViewWindow.SELECT_TYPE.UNKNOWN)
		{
			this.m_pressTime += Time.deltaTime;
			if (this.m_pressTime > 0.5f)
			{
				switch (this.m_pressBtnType)
				{
				case DeckViewWindow.SELECT_TYPE.CHARA_MAIN:
					this.OnReleasePlayerMain();
					break;
				case DeckViewWindow.SELECT_TYPE.CHARA_SUB:
					this.OnReleasePlayerSub();
					break;
				case DeckViewWindow.SELECT_TYPE.CHAO_MAIN:
					this.OnReleaseChaoMain();
					break;
				case DeckViewWindow.SELECT_TYPE.CHAO_SUB:
					this.OnReleaseChaoSub();
					break;
				}
				this.m_pressTime = 0f;
				this.m_pressBtnType = DeckViewWindow.SELECT_TYPE.UNKNOWN;
			}
		}
		if (this.m_chaoSpIconTime > 0f)
		{
			this.m_chaoSpIconTime += Time.deltaTime;
			if (this.m_chaoSpIconTime > 10f)
			{
				this.m_chaoSpIconTime = 0f;
				this.ResetupChaoView();
			}
		}
		float deltaTime = Time.deltaTime;
		this.UpdateChangeBtnDelay(deltaTime);
		this.UpdateLastInputTime(deltaTime);
	}

	// Token: 0x06002011 RID: 8209 RVA: 0x000C0454 File Offset: 0x000BE654
	private void ChangePlayer(DeckViewWindow.SELECT_TYPE select, CharaType type)
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null && this.m_charaList != null && this.m_charaList.Count > 1 && this.m_charaList.Contains(type))
		{
			bool flag = false;
			if (select != DeckViewWindow.SELECT_TYPE.CHARA_MAIN)
			{
				if (select != DeckViewWindow.SELECT_TYPE.CHARA_SUB)
				{
					global::Debug.Log("ChangePlayer error select:" + select + " !!!!!!");
				}
				else if (type != this.m_playerSub)
				{
					if (type == this.m_playerMain)
					{
						this.m_playerMain = this.m_playerSub;
					}
					this.m_playerSub = type;
					flag = true;
				}
			}
			else if (type != this.m_playerMain)
			{
				if (type == this.m_playerSub)
				{
					this.m_playerSub = this.m_playerMain;
				}
				this.m_playerMain = type;
				flag = true;
			}
			if (flag)
			{
				this.m_change = true;
				this.SetupPlayerView(null);
				this.SetupBonusView();
				SoundManager.SePlay("sys_menu_decide", "SE");
			}
			else
			{
				SoundManager.SePlay("sys_window_close", "SE");
			}
		}
		else
		{
			SoundManager.SePlay("sys_window_close", "SE");
		}
	}

	// Token: 0x06002012 RID: 8210 RVA: 0x000C0590 File Offset: 0x000BE790
	private void ChangePlayer(ref CharaType target, ref CharaType other, int param = 1)
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null && this.m_charaList != null && this.m_charaList.Count > 1)
		{
			CharaType charaType = target;
			ServerPlayerState playerState = ServerInterface.PlayerState;
			if (playerState != null)
			{
				int count = this.m_charaList.Count;
				int num = 0;
				if (target != CharaType.UNKNOWN)
				{
					for (int i = 0; i < count; i++)
					{
						if (this.m_charaList[i] == target)
						{
							num = i;
							break;
						}
					}
					if (this.m_charaList[(num + param + count) % count] == other)
					{
						num = (num + param + count) % count;
					}
				}
				else
				{
					num = 0;
					if (this.m_charaList[(num + param + count) % count] == other)
					{
						num = 1;
					}
				}
				int index = (num + param + count) % count;
				ServerCharacterState serverCharacterState = playerState.CharacterState(this.m_charaList[index]);
				if (serverCharacterState != null && serverCharacterState.IsUnlocked)
				{
					target = this.m_charaList[index];
				}
			}
			if (target != charaType)
			{
				SoundManager.SePlay("sys_menu_decide", "SE");
				if (target == other)
				{
					other = charaType;
				}
				this.m_change = true;
				this.SetupPlayerView(null);
				this.SetupBonusView();
			}
			else
			{
				SoundManager.SePlay("sys_window_close", "SE");
			}
		}
		else
		{
			SoundManager.SePlay("sys_window_close", "SE");
		}
	}

	// Token: 0x06002013 RID: 8211 RVA: 0x000C0710 File Offset: 0x000BE910
	private void OnPressPlayerMainUp()
	{
		if (this.m_btnLock)
		{
			return;
		}
		this.m_direction = -1;
		this.m_pressBtnType = DeckViewWindow.SELECT_TYPE.CHARA_MAIN;
		this.m_pressTime = 0f;
	}

	// Token: 0x06002014 RID: 8212 RVA: 0x000C0738 File Offset: 0x000BE938
	private void OnPressPlayerMainDown()
	{
		if (this.m_btnLock)
		{
			return;
		}
		this.m_direction = 1;
		this.m_pressBtnType = DeckViewWindow.SELECT_TYPE.CHARA_MAIN;
		this.m_pressTime = 0f;
	}

	// Token: 0x06002015 RID: 8213 RVA: 0x000C0760 File Offset: 0x000BE960
	private void OnReleasePlayerMain()
	{
		if (this.m_btnLock)
		{
			return;
		}
		if (this.m_init && this.m_pressBtnType == DeckViewWindow.SELECT_TYPE.CHARA_MAIN)
		{
			if (this.m_pressTime > 0.5f)
			{
				PlayerSetWindowUI.Create(this.m_playerMain, null, PlayerSetWindowUI.WINDOW_MODE.DEFAULT);
			}
			else
			{
				this.ResetLastInputTime(DeckViewWindow.SELECT_TYPE.CHARA_MAIN);
				this.SetChangeBtnDelay(DeckViewWindow.SELECT_TYPE.CHARA_MAIN);
				this.ChangePlayer(ref this.m_playerMain, ref this.m_playerSub, this.m_direction);
			}
		}
		this.m_pressBtnType = DeckViewWindow.SELECT_TYPE.UNKNOWN;
	}

	// Token: 0x06002016 RID: 8214 RVA: 0x000C07E0 File Offset: 0x000BE9E0
	private void OnPressPlayerSubUp()
	{
		if (this.m_btnLock)
		{
			return;
		}
		this.m_direction = -1;
		this.m_pressBtnType = DeckViewWindow.SELECT_TYPE.CHARA_SUB;
		this.m_pressTime = 0f;
	}

	// Token: 0x06002017 RID: 8215 RVA: 0x000C0808 File Offset: 0x000BEA08
	private void OnPressPlayerSubDown()
	{
		if (this.m_btnLock)
		{
			return;
		}
		this.m_direction = 1;
		this.m_pressBtnType = DeckViewWindow.SELECT_TYPE.CHARA_SUB;
		this.m_pressTime = 0f;
	}

	// Token: 0x06002018 RID: 8216 RVA: 0x000C0830 File Offset: 0x000BEA30
	private void OnReleasePlayerSub()
	{
		if (this.m_btnLock)
		{
			return;
		}
		if (this.m_init && this.m_pressBtnType == DeckViewWindow.SELECT_TYPE.CHARA_SUB)
		{
			if (this.m_pressTime > 0.5f)
			{
				PlayerSetWindowUI.Create(this.m_playerSub, null, PlayerSetWindowUI.WINDOW_MODE.DEFAULT);
			}
			else
			{
				this.ResetLastInputTime(DeckViewWindow.SELECT_TYPE.CHARA_SUB);
				this.SetChangeBtnDelay(DeckViewWindow.SELECT_TYPE.CHARA_SUB);
				this.ChangePlayer(ref this.m_playerSub, ref this.m_playerMain, this.m_direction);
			}
		}
		this.m_pressBtnType = DeckViewWindow.SELECT_TYPE.UNKNOWN;
	}

	// Token: 0x06002019 RID: 8217 RVA: 0x000C08B0 File Offset: 0x000BEAB0
	private void ChangeChaoSort(ChaoSort sort)
	{
		if (sort != ChaoSort.NUM && sort != ChaoSort.NONE)
		{
			this.m_currentChaoSort = sort;
			DataTable.ChaoData[] dataTable = ChaoTable.GetDataTable();
			ChaoDataSorting chaoDataSorting = new ChaoDataSorting(this.m_currentChaoSort);
			if (chaoDataSorting != null)
			{
				ChaoDataVisitorBase visitor = chaoDataSorting.visitor;
				if (visitor != null)
				{
					foreach (DataTable.ChaoData chaoData in dataTable)
					{
						chaoData.accept(ref visitor);
					}
					this.m_chaoList = chaoDataSorting.GetChaoList(false, DataTable.ChaoData.Rarity.NONE);
				}
			}
		}
	}

	// Token: 0x0600201A RID: 8218 RVA: 0x000C0930 File Offset: 0x000BEB30
	private bool ChangeChao(DeckViewWindow.SELECT_TYPE select, int type)
	{
		bool flag = false;
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null && this.m_chaoList != null && this.m_chaoList.Count > 1)
		{
			if (select != DeckViewWindow.SELECT_TYPE.CHAO_MAIN)
			{
				if (select != DeckViewWindow.SELECT_TYPE.CHAO_SUB)
				{
					global::Debug.Log("ChangePlayer error select:" + select + " !!!!!!");
				}
				else if (type != this.m_chaoSubId)
				{
					if (type == this.m_chaoMainId)
					{
						this.m_chaoMainId = this.m_chaoSubId;
					}
					this.m_chaoSubId = type;
					flag = true;
				}
			}
			else if (type != this.m_chaoMainId)
			{
				if (type == this.m_chaoSubId)
				{
					this.m_chaoSubId = this.m_chaoMainId;
				}
				this.m_chaoMainId = type;
				flag = true;
			}
			if (flag)
			{
				this.m_change = true;
				this.SetupChaoView();
				this.SetupBonusView();
				SoundManager.SePlay("sys_menu_decide", "SE");
			}
			else
			{
				SoundManager.SePlay("sys_window_close", "SE");
			}
		}
		else
		{
			SoundManager.SePlay("sys_window_close", "SE");
		}
		return flag;
	}

	// Token: 0x0600201B RID: 8219 RVA: 0x000C0A5C File Offset: 0x000BEC5C
	private bool ChangeChao(ref int target, ref int other, int param = 1)
	{
		bool result = false;
		bool flag = false;
		if (target >= 0 && this.m_chaoList != null && this.m_chaoList.Count > 1)
		{
			flag = true;
		}
		else if (target == -1 && this.m_chaoList != null && this.m_chaoList.Count > 0 && ((other >= 0 && this.m_chaoList.Count > 1) || (other < 0 && this.m_chaoList.Count > 0)))
		{
			flag = true;
		}
		if (flag)
		{
			int count = this.m_chaoList.Count;
			int num = -1;
			if (target >= 0)
			{
				for (int i = 0; i < count; i++)
				{
					if (target == this.m_chaoList[i].id)
					{
						num = i;
						break;
					}
				}
			}
			else
			{
				num = 0;
				int num2 = (num + param + count) % count;
				if (this.m_chaoList.Count > num2 && this.m_chaoList[num2] != null)
				{
					if (other >= 0)
					{
						int id = this.m_chaoList[num2].id;
						if (id == other)
						{
							num2 = (num + param + param + count) % count;
							if (this.m_chaoList.Count > num2 && this.m_chaoList[num2] != null)
							{
								num = 1;
							}
							else
							{
								num = -1;
							}
						}
					}
				}
				else
				{
					num = -1;
				}
			}
			if (num >= 0)
			{
				if (param != 0)
				{
					int num3 = target;
					int num4 = (num + param + count) % count;
					if (num4 >= 0 && num4 < this.m_chaoList.Count && this.m_chaoList[num4] != null)
					{
						if (this.m_chaoList[num4].id == other)
						{
							num4 = (num + param + param + count) % count;
							if (num4 < 0 || num4 >= this.m_chaoList.Count || this.m_chaoList[num4] == null)
							{
								return false;
							}
						}
						target = this.m_chaoList[num4].id;
						if (target != num3)
						{
							result = true;
						}
					}
				}
				else
				{
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x0600201C RID: 8220 RVA: 0x000C0C9C File Offset: 0x000BEE9C
	private void OnPressChaoMainUp()
	{
		if (this.m_btnLock)
		{
			return;
		}
		this.m_direction = -1;
		this.m_pressBtnType = DeckViewWindow.SELECT_TYPE.CHAO_MAIN;
		this.m_pressTime = 0f;
	}

	// Token: 0x0600201D RID: 8221 RVA: 0x000C0CC4 File Offset: 0x000BEEC4
	private void OnPressChaoMainDown()
	{
		if (this.m_btnLock)
		{
			return;
		}
		this.m_direction = 1;
		this.m_pressBtnType = DeckViewWindow.SELECT_TYPE.CHAO_MAIN;
		this.m_pressTime = 0f;
	}

	// Token: 0x0600201E RID: 8222 RVA: 0x000C0CEC File Offset: 0x000BEEEC
	private void OnReleaseChaoMain()
	{
		if (this.m_btnLock)
		{
			return;
		}
		if (this.m_init && this.m_pressBtnType == DeckViewWindow.SELECT_TYPE.CHAO_MAIN)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			this.ResetLastInputTime(DeckViewWindow.SELECT_TYPE.CHAO_MAIN);
			if (this.m_pressTime > 0.5f)
			{
				ChaoSetWindowUI window = ChaoSetWindowUI.GetWindow();
				if (window != null)
				{
					DataTable.ChaoData chaoData = ChaoTable.GetChaoData(this.m_chaoMainId);
					if (chaoData != null)
					{
						ChaoSetWindowUI.ChaoInfo chaoInfo = new ChaoSetWindowUI.ChaoInfo(chaoData);
						chaoInfo.level = chaoData.level;
						chaoInfo.detail = chaoData.GetDetailLevelPlusSP(chaoInfo.level);
						window.OpenWindow(chaoInfo, ChaoSetWindowUI.WindowType.WINDOW_ONLY);
					}
				}
			}
			else if (this.ChangeChao(ref this.m_chaoMainId, ref this.m_chaoSubId, this.m_direction))
			{
				this.m_change = true;
				this.SetupChaoView();
				this.SetupBonusView();
				this.SetChangeBtnDelay(DeckViewWindow.SELECT_TYPE.CHAO_MAIN);
			}
		}
		this.m_pressBtnType = DeckViewWindow.SELECT_TYPE.UNKNOWN;
	}

	// Token: 0x0600201F RID: 8223 RVA: 0x000C0DDC File Offset: 0x000BEFDC
	private void OnPressChaoSubUp()
	{
		if (this.m_btnLock)
		{
			return;
		}
		this.m_direction = -1;
		this.m_pressBtnType = DeckViewWindow.SELECT_TYPE.CHAO_SUB;
		this.m_pressTime = 0f;
	}

	// Token: 0x06002020 RID: 8224 RVA: 0x000C0E04 File Offset: 0x000BF004
	private void OnPressChaoSubDown()
	{
		if (this.m_btnLock)
		{
			return;
		}
		this.m_direction = 1;
		this.m_pressBtnType = DeckViewWindow.SELECT_TYPE.CHAO_SUB;
		this.m_pressTime = 0f;
	}

	// Token: 0x06002021 RID: 8225 RVA: 0x000C0E2C File Offset: 0x000BF02C
	private void OnReleaseChaoSub()
	{
		if (this.m_btnLock)
		{
			return;
		}
		if (this.m_init && this.m_pressBtnType == DeckViewWindow.SELECT_TYPE.CHAO_SUB)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			this.ResetLastInputTime(DeckViewWindow.SELECT_TYPE.CHAO_SUB);
			if (this.m_pressTime > 0.5f)
			{
				ChaoSetWindowUI window = ChaoSetWindowUI.GetWindow();
				if (window != null)
				{
					DataTable.ChaoData chaoData = ChaoTable.GetChaoData(this.m_chaoSubId);
					if (chaoData != null)
					{
						ChaoSetWindowUI.ChaoInfo chaoInfo = new ChaoSetWindowUI.ChaoInfo(chaoData);
						chaoInfo.level = chaoData.level;
						chaoInfo.detail = chaoData.GetDetailLevelPlusSP(chaoInfo.level);
						window.OpenWindow(chaoInfo, ChaoSetWindowUI.WindowType.WINDOW_ONLY);
					}
				}
			}
			else if (this.ChangeChao(ref this.m_chaoSubId, ref this.m_chaoMainId, this.m_direction))
			{
				this.m_change = true;
				this.SetupChaoView();
				this.SetupBonusView();
				this.SetChangeBtnDelay(DeckViewWindow.SELECT_TYPE.CHAO_SUB);
			}
		}
		this.m_pressBtnType = DeckViewWindow.SELECT_TYPE.UNKNOWN;
	}

	// Token: 0x06002022 RID: 8226 RVA: 0x000C0F1C File Offset: 0x000BF11C
	private void OnClickChange()
	{
		if (this.m_btnLock)
		{
			return;
		}
		this.ResetLastInputTime(DeckViewWindow.SELECT_TYPE.NUM);
		if (this.m_playerMain != CharaType.UNKNOWN && this.m_playerSub != CharaType.UNKNOWN)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			CharaType playerMain = this.m_playerMain;
			CharaType playerSub = this.m_playerSub;
			this.m_playerMain = playerSub;
			this.m_playerSub = playerMain;
			this.m_change = true;
			this.SetupPlayerView(null);
			this.SetupBonusView();
		}
		else
		{
			SoundManager.SePlay("sys_window_close", "SE");
		}
	}

	// Token: 0x06002023 RID: 8227 RVA: 0x000C0FAC File Offset: 0x000BF1AC
	private void OnClickTab1()
	{
		if (this.m_currentChaoSetStock != 0 && !this.m_close)
		{
			this.DeckSetLoad(0);
			SoundManager.SePlay("sys_menu_decide", "SE");
		}
	}

	// Token: 0x06002024 RID: 8228 RVA: 0x000C0FDC File Offset: 0x000BF1DC
	private void OnClickTab2()
	{
		if (this.m_currentChaoSetStock != 1 && !this.m_close)
		{
			this.DeckSetLoad(1);
			SoundManager.SePlay("sys_menu_decide", "SE");
		}
	}

	// Token: 0x06002025 RID: 8229 RVA: 0x000C1018 File Offset: 0x000BF218
	private void OnClickTab3()
	{
		if (this.m_currentChaoSetStock != 2 && !this.m_close)
		{
			this.DeckSetLoad(2);
			SoundManager.SePlay("sys_menu_decide", "SE");
		}
	}

	// Token: 0x06002026 RID: 8230 RVA: 0x000C1054 File Offset: 0x000BF254
	private void OnClickTab4()
	{
		if (this.m_currentChaoSetStock != 3 && !this.m_close)
		{
			this.DeckSetLoad(3);
			SoundManager.SePlay("sys_menu_decide", "SE");
		}
	}

	// Token: 0x06002027 RID: 8231 RVA: 0x000C1090 File Offset: 0x000BF290
	private void OnClickTab5()
	{
		if (this.m_currentChaoSetStock != 4 && !this.m_close)
		{
			this.DeckSetLoad(4);
			SoundManager.SePlay("sys_menu_decide", "SE");
		}
	}

	// Token: 0x06002028 RID: 8232 RVA: 0x000C10CC File Offset: 0x000BF2CC
	private void OnClickClose()
	{
		if (this.m_btnLock)
		{
			return;
		}
		if (this.m_init && !this.m_close)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			this.m_init = false;
			this.m_close = true;
			UIPlayAnimation[] componentsInChildren = base.gameObject.GetComponentsInChildren<UIPlayAnimation>();
			if (componentsInChildren != null && componentsInChildren.Length > 0)
			{
				foreach (UIPlayAnimation uiplayAnimation in componentsInChildren)
				{
					uiplayAnimation.enabled = false;
				}
			}
			this.ResetRequestData(false);
			if (this.m_change && this.m_initPlayerMain == this.m_playerMain && this.m_initPlayerSub == this.m_playerSub && this.m_initChaoMain == this.m_chaoMainId && this.m_initChaoSub == this.m_chaoSubId)
			{
				this.m_change = false;
				if (this.m_initChaoSetStock != this.m_currentChaoSetStock)
				{
					DeckUtil.SetDeckCurrentStockIndex(this.m_currentChaoSetStock);
					this.m_parent.SendMessage("OnMsgReset", SendMessageOptions.DontRequireReceiver);
				}
			}
			if (this.m_change)
			{
				this.m_change = false;
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (!GeneralUtil.IsNetwork() || loggedInServerInterface == null)
				{
					DeckUtil.SetDeckCurrentStockIndex(this.m_initChaoSetStock);
					DeckUtil.DeckSetSave(this.m_initChaoSetStock, this.m_initPlayerMain, this.m_initPlayerSub, this.m_initChaoMain, this.m_initChaoSub);
					GeneralUtil.SetCharasetBtnIcon(this.m_initPlayerMain, this.m_initPlayerSub, this.m_initChaoMain, this.m_initChaoSub, this.m_parent, "Btn_charaset");
					GeneralUtil.ShowNoCommunication("ShowNoCommunication");
					ChaoTextureManager.Instance.RemoveChaoTextureForMainMenuEnd();
					if (this.m_parent != null)
					{
						this.m_parent.SendMessage("OnMsgDeckViewWindowNetworkError", SendMessageOptions.DontRequireReceiver);
					}
					this.CloseWindowAnim();
					return;
				}
				GeneralUtil.SetCharasetBtnIcon(this.m_playerMain, this.m_playerSub, this.m_chaoMainId, this.m_chaoSubId, this.m_parent, "Btn_charaset");
				this.SetRequestData();
				DeckUtil.DeckSetSave(this.m_currentChaoSetStock, this.m_playerMain, this.m_playerSub, this.m_chaoMainId, this.m_chaoSubId);
				DeckUtil.SetDeckCurrentStockIndex(this.m_currentChaoSetStock);
				this.PlayerDataUpdate();
				if (loggedInServerInterface != null && this.m_playerMain != this.m_playerSub)
				{
					if (this.m_initChaoMain != this.m_chaoMainId || this.m_initChaoSub != this.m_chaoSubId)
					{
						int id = (int)ServerItem.CreateFromChaoId(this.m_chaoMainId).id;
						int id2 = (int)ServerItem.CreateFromChaoId(this.m_chaoSubId).id;
						loggedInServerInterface.RequestServerEquipChao(id, id2, base.gameObject);
					}
					else
					{
						this.ServerEquipChao_Dummy();
					}
				}
				this.m_parent.SendMessage("OnMsgReset", SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				if (this.m_parent != null)
				{
					this.m_parent.SendMessage("OnMsgDeckViewWindowNotChange", SendMessageOptions.DontRequireReceiver);
				}
				this.CloseWindowAnim();
			}
			if (this.m_parent != null)
			{
				this.m_parent.SendMessage("OnMsgDeckViewWindowEnd", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x06002029 RID: 8233 RVA: 0x000C13F0 File Offset: 0x000BF5F0
	private void OnClickSelect()
	{
		if (this.m_btnLock)
		{
			return;
		}
		if (this.m_init && !this.m_close)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			this.m_init = false;
			this.m_close = true;
			UIPlayAnimation[] componentsInChildren = base.gameObject.GetComponentsInChildren<UIPlayAnimation>();
			if (componentsInChildren != null && componentsInChildren.Length > 0)
			{
				foreach (UIPlayAnimation uiplayAnimation in componentsInChildren)
				{
					uiplayAnimation.enabled = false;
				}
			}
			this.ResetRequestData(false);
			if (this.m_change && this.m_initPlayerMain == this.m_playerMain && this.m_initPlayerSub == this.m_playerSub && this.m_initChaoMain == this.m_chaoMainId && this.m_initChaoSub == this.m_chaoSubId)
			{
				this.m_change = false;
				if (this.m_initChaoSetStock != this.m_currentChaoSetStock)
				{
					DeckUtil.SetDeckCurrentStockIndex(this.m_currentChaoSetStock);
				}
			}
			if (this.m_change)
			{
				this.m_change = false;
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (!GeneralUtil.IsNetwork() || loggedInServerInterface == null)
				{
					DeckUtil.SetDeckCurrentStockIndex(this.m_initChaoSetStock);
					DeckUtil.DeckSetSave(this.m_initChaoSetStock, this.m_initPlayerMain, this.m_initPlayerSub, this.m_initChaoMain, this.m_initChaoSub);
					GeneralUtil.ShowNoCommunication("ShowNoCommunication");
					ChaoTextureManager.Instance.RemoveChaoTextureForMainMenuEnd();
					HudMenuUtility.SendStartPlayerChaoPage();
					if (this.m_parent != null)
					{
						this.m_parent.SendMessage("OnMsgDeckViewWindowNetworkError", SendMessageOptions.DontRequireReceiver);
					}
					this.CloseWindowAnim();
					return;
				}
				this.SetRequestData();
				DeckUtil.DeckSetSave(this.m_currentChaoSetStock, this.m_playerMain, this.m_playerSub, this.m_chaoMainId, this.m_chaoSubId);
				DeckUtil.SetDeckCurrentStockIndex(this.m_currentChaoSetStock);
				this.PlayerDataUpdate();
				if (loggedInServerInterface != null && this.m_playerMain != this.m_playerSub)
				{
					if (this.m_initChaoMain != this.m_chaoMainId || this.m_initChaoSub != this.m_chaoSubId)
					{
						int id = (int)ServerItem.CreateFromChaoId(this.m_chaoMainId).id;
						int id2 = (int)ServerItem.CreateFromChaoId(this.m_chaoSubId).id;
						loggedInServerInterface.RequestServerEquipChao(id, id2, base.gameObject);
					}
					else
					{
						this.ServerEquipChao_Dummy();
					}
				}
			}
			else
			{
				if (this.m_parent != null)
				{
					this.m_parent.SendMessage("OnMsgDeckViewWindowNotChange", SendMessageOptions.DontRequireReceiver);
				}
				this.CloseWindowAnim();
			}
			if (this.m_parent != null)
			{
				this.m_parent.SendMessage("OnMsgDeckViewWindowEnd", SendMessageOptions.DontRequireReceiver);
			}
			HudMenuUtility.SendStartPlayerChaoPage();
		}
	}

	// Token: 0x0600202A RID: 8234 RVA: 0x000C16AC File Offset: 0x000BF8AC
	private void UpdateChangeBtnDelay(float delteTime)
	{
		if (this.m_changeDelayCheckTime > 0f)
		{
			this.m_changeDelayCheckTime -= delteTime;
			if (this.m_changeDelayCheckTime <= 0f)
			{
				this.m_changeDelayCheckTime = 0f;
				this.SetAllChangeBtnEnabled(true);
			}
		}
	}

	// Token: 0x0600202B RID: 8235 RVA: 0x000C16FC File Offset: 0x000BF8FC
	private void SetAllChangeBtnEnabled(bool enabled)
	{
		if (this.m_changeBtnList != null && this.m_changeBtnList.Count > 0)
		{
			Dictionary<DeckViewWindow.SELECT_TYPE, List<UIImageButton>>.KeyCollection keys = this.m_changeBtnList.Keys;
			foreach (DeckViewWindow.SELECT_TYPE key in keys)
			{
				if (this.m_changeBtnList[key] != null && this.m_changeBtnList[key].Count > 0)
				{
					foreach (UIImageButton uiimageButton in this.m_changeBtnList[key])
					{
						if (uiimageButton != null)
						{
							uiimageButton.isEnabled = enabled;
						}
					}
				}
			}
		}
	}

	// Token: 0x0600202C RID: 8236 RVA: 0x000C1810 File Offset: 0x000BFA10
	private void UpdateLastInputTime(float delteTime)
	{
		if (!this.m_isLastInputTime)
		{
			return;
		}
		int num = 4;
		if (this.m_lastInputTime == null)
		{
			this.m_lastInputTime = new Dictionary<DeckViewWindow.SELECT_TYPE, float>();
			for (int i = 0; i < num; i++)
			{
				DeckViewWindow.SELECT_TYPE key = (DeckViewWindow.SELECT_TYPE)i;
				this.m_lastInputTime.Add(key, delteTime);
			}
		}
		else
		{
			int num2 = 0;
			for (int j = 0; j < num; j++)
			{
				DeckViewWindow.SELECT_TYPE select_TYPE = (DeckViewWindow.SELECT_TYPE)j;
				if (this.m_lastInputTime.ContainsKey(select_TYPE))
				{
					if (this.m_lastInputTime[select_TYPE] >= 0f)
					{
						Dictionary<DeckViewWindow.SELECT_TYPE, float> lastInputTime;
						Dictionary<DeckViewWindow.SELECT_TYPE, float> dictionary = lastInputTime = this.m_lastInputTime;
						DeckViewWindow.SELECT_TYPE select_TYPE2;
						DeckViewWindow.SELECT_TYPE key2 = select_TYPE2 = select_TYPE;
						float num3 = lastInputTime[select_TYPE2];
						dictionary[key2] = num3 + delteTime;
						if (this.m_lastInputTime[select_TYPE] > 0.75f)
						{
							select_TYPE2 = select_TYPE;
							if (select_TYPE2 != DeckViewWindow.SELECT_TYPE.CHAO_MAIN)
							{
								if (select_TYPE2 != DeckViewWindow.SELECT_TYPE.CHAO_SUB)
								{
									this.m_lastInputTime[select_TYPE] = -1f;
								}
								else
								{
									GameObject parent = GameObjectUtil.FindChildGameObject(base.gameObject, "info_chao");
									GameObject gameObject = GameObjectUtil.FindChildGameObject(parent, "chao_sub");
									if (gameObject != null && this.m_chaoSubId >= 0)
									{
										UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject, "img_chao_text_sub");
										if (uitexture != null)
										{
											if (GeneralUtil.CheckChaoTexture(uitexture, this.m_chaoSubId))
											{
												this.m_lastInputTime[select_TYPE] = -1f;
											}
											else
											{
												Texture loadedTexture = ChaoTextureManager.Instance.GetLoadedTexture(this.m_chaoSubId);
												if (loadedTexture != null)
												{
													uitexture.mainTexture = loadedTexture;
												}
												else
												{
													ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(uitexture, null, true);
													ChaoTextureManager.Instance.GetTexture(this.m_chaoSubId, info);
												}
												uitexture.alpha = 1f;
												uitexture.enabled = true;
											}
										}
									}
									else
									{
										this.m_lastInputTime[select_TYPE] = -1f;
									}
								}
							}
							else
							{
								GameObject parent = GameObjectUtil.FindChildGameObject(base.gameObject, "info_chao");
								GameObject gameObject = GameObjectUtil.FindChildGameObject(parent, "chao_main");
								if (gameObject != null && this.m_chaoMainId >= 0)
								{
									UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject, "img_chao_text_main");
									if (uitexture != null)
									{
										if (GeneralUtil.CheckChaoTexture(uitexture, this.m_chaoMainId))
										{
											this.m_lastInputTime[select_TYPE] = -1f;
										}
										else
										{
											Texture loadedTexture2 = ChaoTextureManager.Instance.GetLoadedTexture(this.m_chaoMainId);
											if (loadedTexture2 != null)
											{
												uitexture.mainTexture = loadedTexture2;
											}
											else
											{
												ChaoTextureManager.CallbackInfo info2 = new ChaoTextureManager.CallbackInfo(uitexture, null, true);
												ChaoTextureManager.Instance.GetTexture(this.m_chaoMainId, info2);
											}
											uitexture.alpha = 1f;
											uitexture.enabled = true;
										}
									}
								}
								else
								{
									this.m_lastInputTime[select_TYPE] = -1f;
								}
							}
						}
					}
					else
					{
						num2++;
					}
				}
			}
			if (num2 >= num)
			{
				this.m_isLastInputTime = false;
			}
		}
	}

	// Token: 0x0600202D RID: 8237 RVA: 0x000C1B24 File Offset: 0x000BFD24
	private void ResetLastInputTime(DeckViewWindow.SELECT_TYPE targetType = DeckViewWindow.SELECT_TYPE.NUM)
	{
		this.m_isLastInputTime = true;
		if (this.m_lastInputTime == null)
		{
			this.m_lastInputTime = new Dictionary<DeckViewWindow.SELECT_TYPE, float>();
			int num = 4;
			for (int i = 0; i < num; i++)
			{
				DeckViewWindow.SELECT_TYPE key = (DeckViewWindow.SELECT_TYPE)i;
				this.m_lastInputTime.Add(key, 0f);
			}
		}
		else if (targetType != DeckViewWindow.SELECT_TYPE.NUM)
		{
			if (this.m_lastInputTime.ContainsKey(targetType))
			{
				this.m_lastInputTime[targetType] = 0f;
			}
		}
		else
		{
			int num2 = 4;
			for (int j = 0; j < num2; j++)
			{
				DeckViewWindow.SELECT_TYPE key2 = (DeckViewWindow.SELECT_TYPE)j;
				if (this.m_lastInputTime.ContainsKey(key2))
				{
					this.m_lastInputTime[key2] = 0f;
				}
			}
		}
	}

	// Token: 0x0600202E RID: 8238 RVA: 0x000C1BE8 File Offset: 0x000BFDE8
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (!this.m_close)
		{
			if (msg != null)
			{
				msg.StaySequence();
			}
			if (PlayerSetWindowUI.isActive)
			{
				return;
			}
			if (ChaoSetWindowUI.isActive)
			{
				return;
			}
			if (this.m_btnLock)
			{
				return;
			}
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
	}

	// Token: 0x0600202F RID: 8239 RVA: 0x000C1C6C File Offset: 0x000BFE6C
	private void SetRequestData()
	{
		this.m_reqChaoSetStock = this.m_currentChaoSetStock;
		this.m_reqPlayerMain = this.m_playerMain;
		this.m_reqPlayerSub = this.m_playerSub;
		this.m_reqChaoMain = this.m_chaoMainId;
		this.m_reqChaoSub = this.m_chaoSubId;
	}

	// Token: 0x06002030 RID: 8240 RVA: 0x000C1CB8 File Offset: 0x000BFEB8
	private void PlayerDataUpdate()
	{
		if (this.m_reqChaoSetStock >= 0)
		{
			DeckUtil.SetDeckCurrentStockIndex(this.m_reqChaoSetStock);
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null)
				{
					SaveDataManager instance2 = SaveDataManager.Instance;
					instance2.PlayerData.MainChara = this.m_reqPlayerMain;
					instance2.PlayerData.SubChara = this.m_reqPlayerSub;
					instance2.PlayerData.MainChaoID = this.m_reqChaoMain;
					instance2.PlayerData.SubChaoID = this.m_reqChaoSub;
				}
			}
		}
	}

	// Token: 0x06002031 RID: 8241 RVA: 0x000C1D48 File Offset: 0x000BFF48
	private void ResetRequestData(bool isSavaDataUpdate)
	{
		if (isSavaDataUpdate && this.m_reqChaoSetStock >= 0)
		{
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null)
				{
					SaveDataManager instance2 = SaveDataManager.Instance;
					instance2.PlayerData.MainChara = this.m_reqPlayerMain;
					instance2.PlayerData.SubChara = this.m_reqPlayerSub;
					instance2.PlayerData.MainChaoID = this.m_reqChaoMain;
					instance2.PlayerData.SubChaoID = this.m_reqChaoSub;
					HudMenuUtility.SendMsgUpdateSaveDataDisplay();
				}
			}
		}
		this.m_reqChaoSetStock = -1;
		this.m_reqPlayerMain = CharaType.UNKNOWN;
		this.m_reqPlayerSub = CharaType.UNKNOWN;
		this.m_reqChaoMain = -1;
		this.m_reqChaoSub = -1;
	}

	// Token: 0x06002032 RID: 8242 RVA: 0x000C1DF8 File Offset: 0x000BFFF8
	private void CloseWindowAnim()
	{
		if (this.m_windowAnimation != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_windowAnimation, Direction.Reverse);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
		}
	}

	// Token: 0x06002033 RID: 8243 RVA: 0x000C1E3C File Offset: 0x000C003C
	private void WindowAnimationFinishCallback()
	{
		if (this.m_close)
		{
			this.OnFinished();
		}
	}

	// Token: 0x06002034 RID: 8244 RVA: 0x000C1E50 File Offset: 0x000C0050
	private void ServerChangeCharacter_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		this.ResetRequestData(true);
		ItemSetMenu.UpdateBoostItemForCharacterDeck();
		if (this.m_parent != null)
		{
			this.m_parent.SendMessage("OnMsgDeckViewWindowChange", SendMessageOptions.DontRequireReceiver);
		}
		this.CloseWindowAnim();
	}

	// Token: 0x06002035 RID: 8245 RVA: 0x000C1E94 File Offset: 0x000C0094
	private void ServerChangeCharacter_Dummy()
	{
		this.ResetRequestData(true);
		ItemSetMenu.UpdateBoostItemForCharacterDeck();
		if (this.m_parent != null)
		{
			this.m_parent.SendMessage("OnMsgDeckViewWindowChange", SendMessageOptions.DontRequireReceiver);
		}
		this.CloseWindowAnim();
	}

	// Token: 0x06002036 RID: 8246 RVA: 0x000C1ED8 File Offset: 0x000C00D8
	private void ServerEquipChao_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		bool flag = false;
		if (loggedInServerInterface != null)
		{
			if (this.m_initPlayerMain != this.m_reqPlayerMain)
			{
				flag = true;
			}
			if (this.m_initPlayerSub != this.m_reqPlayerSub)
			{
				flag = true;
			}
			if (flag)
			{
				int mainCharaId = -1;
				int subCharaId = -1;
				ServerCharacterState serverCharacterState = ServerInterface.PlayerState.CharacterState(this.m_reqPlayerMain);
				ServerCharacterState serverCharacterState2 = ServerInterface.PlayerState.CharacterState(this.m_reqPlayerSub);
				if (serverCharacterState != null)
				{
					mainCharaId = serverCharacterState.Id;
				}
				if (serverCharacterState2 != null)
				{
					subCharaId = serverCharacterState2.Id;
				}
				loggedInServerInterface.RequestServerChangeCharacter(mainCharaId, subCharaId, base.gameObject);
			}
			else
			{
				this.ServerChangeCharacter_Dummy();
			}
		}
	}

	// Token: 0x06002037 RID: 8247 RVA: 0x000C1F84 File Offset: 0x000C0184
	private void ServerEquipChao_Dummy()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		bool flag = false;
		if (loggedInServerInterface != null)
		{
			if (this.m_initPlayerMain != this.m_reqPlayerMain)
			{
				flag = true;
			}
			if (this.m_initPlayerSub != this.m_reqPlayerSub)
			{
				flag = true;
			}
			if (flag)
			{
				int mainCharaId = -1;
				int subCharaId = -1;
				ServerCharacterState serverCharacterState = ServerInterface.PlayerState.CharacterState(this.m_reqPlayerMain);
				ServerCharacterState serverCharacterState2 = ServerInterface.PlayerState.CharacterState(this.m_reqPlayerSub);
				if (serverCharacterState != null)
				{
					mainCharaId = serverCharacterState.Id;
				}
				if (serverCharacterState2 != null)
				{
					subCharaId = serverCharacterState2.Id;
				}
				loggedInServerInterface.RequestServerChangeCharacter(mainCharaId, subCharaId, base.gameObject);
			}
			else
			{
				this.ServerChangeCharacter_Dummy();
			}
		}
	}

	// Token: 0x06002038 RID: 8248 RVA: 0x000C2030 File Offset: 0x000C0230
	private void OnFinished()
	{
		if (this.m_bgCollider != null)
		{
			this.m_bgCollider.enabled = false;
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "anime_blinder");
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
		if (this.m_windowRoot == null)
		{
			this.m_windowRoot = GameObjectUtil.FindChildGameObject(base.gameObject, "window");
		}
		if (this.m_windowRoot != null)
		{
			this.m_windowRoot.SetActive(false);
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002039 RID: 8249 RVA: 0x000C20D0 File Offset: 0x000C02D0
	public void DeckSetLoad(int stock)
	{
		DeckUtil.DeckSetSave(this.m_currentChaoSetStock, this.m_playerMain, this.m_playerSub, this.m_chaoMainId, this.m_chaoSubId);
		if (DeckUtil.DeckSetLoad(stock, ref this.m_playerMain, ref this.m_playerSub, ref this.m_chaoMainId, ref this.m_chaoSubId, null))
		{
			this.m_change = true;
			this.SetupChaoView();
			this.SetupBonusView();
			this.SetupPlayerView(null);
			this.m_currentChaoSetStock = stock;
			this.SetupTabView();
			DeckUtil.SetDeckCurrentStockIndex(this.m_currentChaoSetStock);
		}
	}

	// Token: 0x0600203A RID: 8250 RVA: 0x000C2158 File Offset: 0x000C0358
	public bool ChaoSetName(int stock, out string main, out string sub)
	{
		main = string.Empty;
		sub = string.Empty;
		if (stock < 0 || stock >= 3)
		{
			return false;
		}
		bool result = false;
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				int num = -1;
				int num2 = -1;
				systemdata.GetDeckData(stock, out num, out num2);
				if (num >= 0 || num2 >= 0)
				{
					if (num >= 0)
					{
						DataTable.ChaoData chaoData = ChaoTable.GetChaoData(num);
						main = chaoData.name;
					}
					if (num2 >= 0)
					{
						DataTable.ChaoData chaoData = ChaoTable.GetChaoData(num2);
						sub = chaoData.name;
					}
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x0600203B RID: 8251 RVA: 0x000C21F8 File Offset: 0x000C03F8
	private void SetChangeBtnDelay(DeckViewWindow.SELECT_TYPE type)
	{
		this.SetAllChangeBtnEnabled(false);
		this.m_changeDelayCheckTime = 0.5f;
	}

	// Token: 0x0600203C RID: 8252 RVA: 0x000C220C File Offset: 0x000C040C
	public static void Reset()
	{
		GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
		if (cameraUIObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(cameraUIObject, "DeckViewWindow");
			if (gameObject != null)
			{
				DeckViewWindow deckViewWindow = GameObjectUtil.FindChildGameObjectComponent<DeckViewWindow>(gameObject.gameObject, "DeckViewWindow");
				if (deckViewWindow != null)
				{
					deckViewWindow.Init();
				}
			}
		}
	}

	// Token: 0x0600203D RID: 8253 RVA: 0x000C2268 File Offset: 0x000C0468
	public static DeckViewWindow Create(GameObject parent = null)
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			PlayerData playerData = instance.PlayerData;
			if (playerData != null)
			{
				DeckViewWindow.Create(playerData.MainChaoID, playerData.SubChaoID, parent);
			}
		}
		return null;
	}

	// Token: 0x0600203E RID: 8254 RVA: 0x000C22A8 File Offset: 0x000C04A8
	public static DeckViewWindow Create(int mainChaoId, int subChaoId, GameObject parent = null)
	{
		GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
		if (cameraUIObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(cameraUIObject, "DeckViewWindow");
			DeckViewWindow deckViewWindow = null;
			if (gameObject != null)
			{
				deckViewWindow = gameObject.GetComponent<DeckViewWindow>();
				if (deckViewWindow == null)
				{
					deckViewWindow = gameObject.AddComponent<DeckViewWindow>();
				}
				if (deckViewWindow != null)
				{
					deckViewWindow.Setup(mainChaoId, subChaoId, parent);
				}
			}
			return deckViewWindow;
		}
		return null;
	}

	// Token: 0x04001CD5 RID: 7381
	private const float BTN_TIME_LIMIT = 0.5f;

	// Token: 0x04001CD6 RID: 7382
	private const float BTN_DELAY_TIME = 0.5f;

	// Token: 0x04001CD7 RID: 7383
	private GameObject m_parent;

	// Token: 0x04001CD8 RID: 7384
	private GameObject m_windowRoot;

	// Token: 0x04001CD9 RID: 7385
	private Animation m_windowAnimation;

	// Token: 0x04001CDA RID: 7386
	private BoxCollider m_bgCollider;

	// Token: 0x04001CDB RID: 7387
	private bool m_init;

	// Token: 0x04001CDC RID: 7388
	private bool m_change;

	// Token: 0x04001CDD RID: 7389
	private bool m_close;

	// Token: 0x04001CDE RID: 7390
	private int m_chaoMainId = -1;

	// Token: 0x04001CDF RID: 7391
	private int m_chaoSubId = -1;

	// Token: 0x04001CE0 RID: 7392
	private float m_chaoSpIconTime;

	// Token: 0x04001CE1 RID: 7393
	private int m_direction = 1;

	// Token: 0x04001CE2 RID: 7394
	private float m_pressTime;

	// Token: 0x04001CE3 RID: 7395
	private DeckViewWindow.SELECT_TYPE m_pressBtnType = DeckViewWindow.SELECT_TYPE.UNKNOWN;

	// Token: 0x04001CE4 RID: 7396
	private float m_changeDelayCheckTime;

	// Token: 0x04001CE5 RID: 7397
	private Dictionary<DeckViewWindow.SELECT_TYPE, List<UIImageButton>> m_changeBtnList;

	// Token: 0x04001CE6 RID: 7398
	private int m_currentChaoSetStock;

	// Token: 0x04001CE7 RID: 7399
	private List<bool> m_isSaveData;

	// Token: 0x04001CE8 RID: 7400
	private CharaType m_playerMain = CharaType.UNKNOWN;

	// Token: 0x04001CE9 RID: 7401
	private CharaType m_playerSub = CharaType.UNKNOWN;

	// Token: 0x04001CEA RID: 7402
	private List<CharaType> m_charaList;

	// Token: 0x04001CEB RID: 7403
	private List<DataTable.ChaoData> m_chaoList;

	// Token: 0x04001CEC RID: 7404
	private bool m_isLastInputTime;

	// Token: 0x04001CED RID: 7405
	private Dictionary<DeckViewWindow.SELECT_TYPE, float> m_lastInputTime;

	// Token: 0x04001CEE RID: 7406
	private UISprite m_detailTextBg;

	// Token: 0x04001CEF RID: 7407
	private UILabel m_detailTextLabel;

	// Token: 0x04001CF0 RID: 7408
	private ChaoSort m_currentChaoSort = ChaoSort.NONE;

	// Token: 0x04001CF1 RID: 7409
	private int m_initChaoSetStock;

	// Token: 0x04001CF2 RID: 7410
	private CharaType m_initPlayerMain = CharaType.UNKNOWN;

	// Token: 0x04001CF3 RID: 7411
	private CharaType m_initPlayerSub = CharaType.UNKNOWN;

	// Token: 0x04001CF4 RID: 7412
	private int m_initChaoMain = -1;

	// Token: 0x04001CF5 RID: 7413
	private int m_initChaoSub = -1;

	// Token: 0x04001CF6 RID: 7414
	private int m_reqChaoSetStock;

	// Token: 0x04001CF7 RID: 7415
	private CharaType m_reqPlayerMain = CharaType.UNKNOWN;

	// Token: 0x04001CF8 RID: 7416
	private CharaType m_reqPlayerSub = CharaType.UNKNOWN;

	// Token: 0x04001CF9 RID: 7417
	private int m_reqChaoMain = -1;

	// Token: 0x04001CFA RID: 7418
	private int m_reqChaoSub = -1;

	// Token: 0x04001CFB RID: 7419
	private bool m_btnLock;

	// Token: 0x04001CFC RID: 7420
	private static DeckViewWindow s_instance;

	// Token: 0x02000425 RID: 1061
	private enum BTN_TYPE
	{
		// Token: 0x04001CFE RID: 7422
		UP,
		// Token: 0x04001CFF RID: 7423
		DOWN
	}

	// Token: 0x02000426 RID: 1062
	private enum SELECT_TYPE
	{
		// Token: 0x04001D01 RID: 7425
		CHARA_MAIN,
		// Token: 0x04001D02 RID: 7426
		CHARA_SUB,
		// Token: 0x04001D03 RID: 7427
		CHAO_MAIN,
		// Token: 0x04001D04 RID: 7428
		CHAO_SUB,
		// Token: 0x04001D05 RID: 7429
		NUM,
		// Token: 0x04001D06 RID: 7430
		UNKNOWN = -1
	}
}
