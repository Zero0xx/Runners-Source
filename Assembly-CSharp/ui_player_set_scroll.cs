using System;
using System.Collections.Generic;
using Text;
using UnityEngine;

// Token: 0x020004DC RID: 1244
public class ui_player_set_scroll : MonoBehaviour
{
	// Token: 0x170004E6 RID: 1254
	// (get) Token: 0x06002507 RID: 9479 RVA: 0x000DDDA0 File Offset: 0x000DBFA0
	public PlayerCharaList parent
	{
		get
		{
			return this.m_parent;
		}
	}

	// Token: 0x170004E7 RID: 1255
	// (get) Token: 0x06002508 RID: 9480 RVA: 0x000DDDA8 File Offset: 0x000DBFA8
	public CharaType charaType
	{
		get
		{
			return this.m_charaType;
		}
	}

	// Token: 0x06002509 RID: 9481 RVA: 0x000DDDB0 File Offset: 0x000DBFB0
	public void Setup(PlayerCharaList parent, ServerCharacterState characterState)
	{
		this.m_parent = parent;
		this.m_charaType = characterState.charaType;
		this.m_charaState = characterState;
		this.m_currentDeck = DeckUtil.GetDeckCurrentStockIndex();
		if (this.m_charaType != CharaType.UNKNOWN && this.m_charaState != null)
		{
			this.SetParam();
			this.SetObject();
		}
	}

	// Token: 0x0600250A RID: 9482 RVA: 0x000DDE08 File Offset: 0x000DC008
	public bool UpdateView()
	{
		this.m_currentDeck = DeckUtil.GetDeckCurrentStockIndex();
		if (this.m_charaType != CharaType.UNKNOWN)
		{
			ServerPlayerState playerState = ServerInterface.PlayerState;
			this.m_charaState = playerState.CharacterState(this.m_charaType);
			if (this.m_charaType != CharaType.UNKNOWN && this.m_charaState != null)
			{
				this.SetParam();
				this.SetObject();
			}
			return true;
		}
		return false;
	}

	// Token: 0x0600250B RID: 9483 RVA: 0x000DDE6C File Offset: 0x000DC06C
	private int GetSelect()
	{
		int result = 0;
		if (this.m_selectList != null && this.m_selectList.Count > 0 && this.m_selectList.Count > this.m_currentDeck)
		{
			result = this.m_selectList[this.m_currentDeck];
		}
		return result;
	}

	// Token: 0x0600250C RID: 9484 RVA: 0x000DDEC0 File Offset: 0x000DC0C0
	private void SetParam()
	{
		this.m_btnMode = ui_player_set_scroll.BTN_MODE.LOCK;
		if (this.m_btnCost != null)
		{
			this.m_btnCost.Clear();
		}
		else
		{
			this.m_btnCost = new Dictionary<int, int>();
		}
		if (this.m_charaState.IsUnlocked)
		{
			this.m_btnMode = ui_player_set_scroll.BTN_MODE.ADD;
			if (this.m_charaState.star >= this.m_charaState.starMax && this.m_charaState.starMax > 0)
			{
				this.m_btnMode = ui_player_set_scroll.BTN_MODE.MAX;
			}
		}
		else
		{
			this.m_btnMode = ui_player_set_scroll.BTN_MODE.GET;
			if (this.m_charaState.Condition == ServerCharacterState.LockCondition.MILEAGE_EPISODE)
			{
				this.m_btnMode = ui_player_set_scroll.BTN_MODE.LOCK_EPISODE;
			}
		}
		if (this.m_btnMode != ui_player_set_scroll.BTN_MODE.ADD || this.m_btnMode != ui_player_set_scroll.BTN_MODE.GET)
		{
			if (this.m_charaState.priceNumRings > 0)
			{
				this.m_btnCost.Add(910000, this.m_charaState.priceNumRings);
			}
			if (this.m_charaState.priceNumRedRings > 0)
			{
				this.m_btnCost.Add(900000, this.m_charaState.priceNumRedRings);
			}
			if (this.m_charaState.IsRoulette)
			{
				this.m_btnCost.Add(0, 0);
			}
		}
		if (this.m_selectList != null)
		{
			this.m_selectList.Clear();
		}
		else
		{
			this.m_selectList = new List<int>();
		}
		List<DeckUtil.DeckSet> deckList = DeckUtil.GetDeckList();
		if (deckList != null && deckList.Count > 0)
		{
			for (int i = 0; i < deckList.Count; i++)
			{
				int item = 0;
				if (deckList[i].charaMain == this.m_charaType)
				{
					item = 1;
				}
				else if (deckList[i].charaSub == this.m_charaType)
				{
					item = 2;
				}
				this.m_selectList.Add(item);
			}
		}
	}

	// Token: 0x0600250D RID: 9485 RVA: 0x000DE090 File Offset: 0x000DC290
	private void SetObject()
	{
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_player_lv");
		UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_player_name");
		UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_player_star");
		UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, "img_player_tex");
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_player_genus");
		UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_player_speacies");
		UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_word_icon");
		UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(base.gameObject, "Btn_2_get");
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "ability");
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "pattern_lock");
		GameObject gameObject3 = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_1_lvUP");
		GameObject gameObject4 = GameObjectUtil.FindChildGameObject(base.gameObject, "pattern_0");
		GameObject gameObject5 = GameObjectUtil.FindChildGameObject(base.gameObject, "pattern_1");
		if (gameObject != null && gameObject2 != null && gameObject3 != null && gameObject4 != null && gameObject5 != null)
		{
			if (this.m_charaState.IsUnlocked)
			{
				gameObject2.SetActive(false);
				gameObject3.SetActive(true);
				gameObject4.SetActive(false);
				gameObject5.SetActive(true);
				if (!ui_player_set_scroll.s_starTextDefaultInit)
				{
					ui_player_set_scroll.s_starTextDefault = new Color(uilabel3.color.r, uilabel3.color.g, uilabel3.color.b, uilabel3.color.a);
					ui_player_set_scroll.s_starTextDefaultInit = true;
				}
				if (this.m_charaState.starMax > 0 && this.m_charaState.star >= this.m_charaState.starMax)
				{
					uilabel3.color = new Color(0.9647059f, 0.45490196f, 0f);
					if (uiimageButton != null)
					{
						uiimageButton.isEnabled = false;
					}
				}
				else
				{
					uilabel3.color = ui_player_set_scroll.s_starTextDefault;
					if (uiimageButton != null)
					{
						uiimageButton.isEnabled = true;
					}
				}
			}
			else
			{
				if (uiimageButton != null)
				{
					uiimageButton.isEnabled = true;
				}
				gameObject2.SetActive(true);
				gameObject3.SetActive(false);
				gameObject4.SetActive(true);
				gameObject5.SetActive(false);
			}
			Dictionary<BonusParam.BonusType, float> teamBonusList = this.m_charaState.GetTeamBonusList();
			if (teamBonusList != null && teamBonusList.Count > 0)
			{
				gameObject.SetActive(true);
				int count = teamBonusList.Count;
				GameObject gameObject6 = null;
				switch (count)
				{
				case 1:
					gameObject6 = GameObjectUtil.FindChildGameObject(gameObject, "1_item");
					break;
				case 2:
					gameObject6 = GameObjectUtil.FindChildGameObject(gameObject, "2_item");
					break;
				case 3:
					gameObject6 = GameObjectUtil.FindChildGameObject(gameObject, "4_item");
					if (gameObject6 != null)
					{
						GameObject gameObject7 = GameObjectUtil.FindChildGameObject(gameObject6, "cell_4");
						if (gameObject7 != null)
						{
							gameObject7.SetActive(false);
						}
					}
					break;
				case 4:
					gameObject6 = GameObjectUtil.FindChildGameObject(gameObject, "4_item");
					break;
				default:
					global::Debug.Log("ui_player_set_scroll SetObject error  abilityNum:" + count + " !!!!!!!");
					break;
				}
				if (gameObject6 != null)
				{
					for (int i = 1; i <= 5; i++)
					{
						if (i != count)
						{
							GameObject gameObject8 = GameObjectUtil.FindChildGameObject(gameObject, i + "_item");
							if (gameObject8 != null)
							{
								gameObject8.SetActive(false);
							}
						}
					}
					gameObject6.SetActive(true);
					int num = 1;
					Dictionary<BonusParam.BonusType, float>.KeyCollection keys = teamBonusList.Keys;
					foreach (BonusParam.BonusType bonusType in keys)
					{
						GameObject gameObject7 = GameObjectUtil.FindChildGameObject(gameObject6, "cell_" + num);
						if (!(gameObject7 != null))
						{
							break;
						}
						gameObject7.SetActive(true);
						UISprite uisprite4 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject7, "img_ability_icon_" + num);
						UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject7, "Lbl_ability_name_" + num);
						if (uisprite4 != null && uilabel4 != null)
						{
							float num2 = teamBonusList[bonusType];
							uisprite4.spriteName = BonusUtil.GetAbilityIconSpriteName(bonusType, num2);
							string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoSet", "bonus_percent").text;
							if (!string.IsNullOrEmpty(text))
							{
								if (bonusType == BonusParam.BonusType.SPEED)
								{
									uilabel4.text = text.Replace("{BONUS}", (100f - num2).ToString());
								}
								else
								{
									if (bonusType == BonusParam.BonusType.TOTAL_SCORE && Mathf.Abs(num2) <= 1f)
									{
										num2 *= 100f;
									}
									if (num2 >= 0f)
									{
										uilabel4.text = "+" + text.Replace("{BONUS}", num2.ToString());
									}
									else
									{
										uilabel4.text = text.Replace("{BONUS}", num2.ToString());
									}
								}
							}
						}
						num++;
					}
				}
				else
				{
					gameObject.SetActive(false);
				}
			}
			else
			{
				gameObject.SetActive(false);
			}
		}
		this.SetCharacter(this.m_charaType, ref uilabel2, ref uilabel, ref uilabel3, ref uitexture, ref uisprite2, ref uisprite, ref uisprite3);
		GeneralUtil.SetButtonFunc(base.gameObject, "Btn_0_info", base.gameObject, "OnClickChara");
		GeneralUtil.SetButtonFunc(base.gameObject, "Btn_2_get", base.gameObject, "OnClickGet");
		GeneralUtil.SetButtonFunc(base.gameObject, "Btn_1_lvUP", base.gameObject, "OnClickLvUp");
	}

	// Token: 0x0600250E RID: 9486 RVA: 0x000DE6B0 File Offset: 0x000DC8B0
	private void SetCharacter(CharaType charaType, ref UILabel name, ref UILabel lv, ref UILabel star, ref UITexture chara, ref UISprite type, ref UISprite genus, ref UISprite select)
	{
		bool flag = false;
		if (charaType != CharaType.NUM && charaType != CharaType.UNKNOWN && this.m_charaState != null && HudCharacterPanelUtil.CheckValidChara(charaType))
		{
			chara.gameObject.SetActive(true);
			if (this.m_charaState.IsUnlocked)
			{
				chara.color = new Color(1f, 1f, 1f);
				TextureRequestChara request = new TextureRequestChara(charaType, chara);
				TextureAsyncLoadManager.Instance.Request(request);
			}
			else
			{
				chara.color = new Color(0f, 0f, 0f);
				TextureRequestChara request2 = new TextureRequestChara(charaType, chara);
				TextureAsyncLoadManager.Instance.Request(request2);
			}
			HudCharacterPanelUtil.SetName(charaType, name.gameObject);
			HudCharacterPanelUtil.SetLevel(charaType, lv.gameObject);
			HudCharacterPanelUtil.SetCharaType(charaType, type.gameObject);
			HudCharacterPanelUtil.SetTeamType(charaType, genus.gameObject);
			if (select != null)
			{
				int select2 = this.GetSelect();
				if (select2 == 1)
				{
					select.spriteName = "ui_player_set_main";
				}
				else if (select2 == 2)
				{
					select.spriteName = "ui_player_set_sub";
				}
				else
				{
					select.spriteName = string.Empty;
				}
			}
			if (star != null)
			{
				star.text = this.m_charaState.star.ToString();
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
			if (star != null)
			{
				star.text = string.Empty;
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
			if (select != null)
			{
				select.spriteName = string.Empty;
			}
		}
	}

	// Token: 0x0600250F RID: 9487 RVA: 0x000DE8E0 File Offset: 0x000DCAE0
	private void OnClickChara()
	{
		if (this.m_parent != null && this.m_parent.isTutorial)
		{
			TutorialCursor.EndTutorialCursor(TutorialCursor.Type.CHARASELECT_MAIN);
			this.m_parent.SetTutorialEnd();
		}
		if (this.m_charaState != null && this.m_charaState.IsUnlocked)
		{
			PlayerSetWindowUI.Create(this.m_charaType, this, PlayerSetWindowUI.WINDOW_MODE.SET);
		}
	}

	// Token: 0x06002510 RID: 9488 RVA: 0x000DE94C File Offset: 0x000DCB4C
	private void OnClickGet()
	{
		if (this.m_parent != null && this.m_parent.isTutorial)
		{
			TutorialCursor.EndTutorialCursor(TutorialCursor.Type.CHARASELECT_MAIN);
			this.m_parent.SetTutorialEnd();
		}
		PlayerSetWindowUI.Create(this.m_charaType, this, PlayerSetWindowUI.WINDOW_MODE.BUY);
	}

	// Token: 0x06002511 RID: 9489 RVA: 0x000DE99C File Offset: 0x000DCB9C
	private void OnClickLvUp()
	{
		BackKeyManager.InvalidFlag = true;
		if (this.m_parent != null && this.m_parent.isTutorial)
		{
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "chara_level_up_explan",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextUtility.GetCommonText("MainMenu", "chara_level_up_explan_caption"),
				message = TextUtility.GetCommonText("MainMenu", "chara_level_up_explan"),
				finishedCloseDelegate = new GeneralWindow.CInfo.FinishedCloseDelegate(this.GeneralWindowCharaLevelUpCloseCallback)
			});
		}
		PlayerLvupWindow.Open(this, this.m_charaType);
		global::Debug.Log("OnClickLvUp");
	}

	// Token: 0x06002512 RID: 9490 RVA: 0x000DEA4C File Offset: 0x000DCC4C
	private void GeneralWindowCharaLevelUpCloseCallback()
	{
		TutorialCursor.StartTutorialCursor(TutorialCursor.Type.CHARASELECT_LEVEL_UP);
	}

	// Token: 0x0400212E RID: 8494
	private PlayerCharaList m_parent;

	// Token: 0x0400212F RID: 8495
	private int m_currentDeck;

	// Token: 0x04002130 RID: 8496
	private List<int> m_selectList;

	// Token: 0x04002131 RID: 8497
	private ui_player_set_scroll.BTN_MODE m_btnMode;

	// Token: 0x04002132 RID: 8498
	private Dictionary<int, int> m_btnCost;

	// Token: 0x04002133 RID: 8499
	private CharaType m_charaType = CharaType.UNKNOWN;

	// Token: 0x04002134 RID: 8500
	private ServerCharacterState m_charaState;

	// Token: 0x04002135 RID: 8501
	private static bool s_starTextDefaultInit;

	// Token: 0x04002136 RID: 8502
	private static Color s_starTextDefault;

	// Token: 0x020004DD RID: 1245
	private enum BTN_MODE
	{
		// Token: 0x04002138 RID: 8504
		GET,
		// Token: 0x04002139 RID: 8505
		ADD,
		// Token: 0x0400213A RID: 8506
		MAX,
		// Token: 0x0400213B RID: 8507
		LOCK_EPISODE,
		// Token: 0x0400213C RID: 8508
		LOCK
	}
}
