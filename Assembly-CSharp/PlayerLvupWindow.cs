using System;
using System.Collections;
using System.Collections.Generic;
using AnimationOrTween;
using Message;
using Text;
using UnityEngine;

// Token: 0x020004DB RID: 1243
public class PlayerLvupWindow : WindowBase
{
	// Token: 0x170004E4 RID: 1252
	// (get) Token: 0x060024F5 RID: 9461 RVA: 0x000DD56C File Offset: 0x000DB76C
	public static bool isActive
	{
		get
		{
			return PlayerLvupWindow.s_isActive;
		}
	}

	// Token: 0x060024F6 RID: 9462 RVA: 0x000DD574 File Offset: 0x000DB774
	private void OnDestroy()
	{
		base.Destroy();
	}

	// Token: 0x170004E5 RID: 1253
	// (get) Token: 0x060024F7 RID: 9463 RVA: 0x000DD57C File Offset: 0x000DB77C
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x060024F8 RID: 9464 RVA: 0x000DD584 File Offset: 0x000DB784
	public static bool Open(ui_player_set_scroll parent, CharaType charaType)
	{
		bool result = false;
		GameObject menuAnimUIObject = HudMenuUtility.GetMenuAnimUIObject();
		if (menuAnimUIObject != null)
		{
			PlayerLvupWindow playerLvupWindow = GameObjectUtil.FindChildGameObjectComponent<PlayerLvupWindow>(menuAnimUIObject, "PlayerLvupWindowUI");
			if (playerLvupWindow == null)
			{
				GameObject gameObject = GameObjectUtil.FindChildGameObject(menuAnimUIObject, "PlayerLvupWindowUI");
				if (gameObject != null)
				{
					playerLvupWindow = gameObject.AddComponent<PlayerLvupWindow>();
				}
			}
			if (playerLvupWindow != null)
			{
				playerLvupWindow.Setup(parent, charaType);
				result = true;
			}
		}
		return result;
	}

	// Token: 0x060024F9 RID: 9465 RVA: 0x000DD5F4 File Offset: 0x000DB7F4
	private void Setup(ui_player_set_scroll parent, CharaType charaType)
	{
		PlayerLvupWindow.s_isActive = true;
		base.gameObject.SetActive(true);
		this.m_charaType = charaType;
		this.m_parent = parent;
		this.m_lock = false;
		if (this.m_abilityList == null)
		{
			this.m_abilityList = new List<AbilityType>();
			this.m_abilityList.Add(AbilityType.INVINCIBLE);
			this.m_abilityList.Add(AbilityType.COMBO);
			this.m_abilityList.Add(AbilityType.MAGNET);
			this.m_abilityList.Add(AbilityType.TRAMPOLINE);
			this.m_abilityList.Add(AbilityType.ASTEROID);
			this.m_abilityList.Add(AbilityType.LASER);
			this.m_abilityList.Add(AbilityType.DRILL);
			this.m_abilityList.Add(AbilityType.DISTANCE_BONUS);
			this.m_abilityList.Add(AbilityType.RING_BONUS);
			this.m_abilityList.Add(AbilityType.ANIMAL);
		}
		this.SetParam();
		base.StartCoroutine(this.SetupObject(true));
	}

	// Token: 0x060024FA RID: 9466 RVA: 0x000DD6CC File Offset: 0x000DB8CC
	private void SetParam()
	{
		ServerPlayerState playerState = ServerInterface.PlayerState;
		this.m_charaState = playerState.CharacterState(this.m_charaType);
		if (this.m_lvList == null)
		{
			this.m_lvList = new Dictionary<AbilityType, int>();
		}
		else
		{
			this.m_lvList.Clear();
		}
		if (this.m_paramList == null)
		{
			this.m_paramList = new Dictionary<AbilityType, float>();
		}
		else
		{
			this.m_paramList.Clear();
		}
		ImportAbilityTable instance = ImportAbilityTable.GetInstance();
		int num = 10;
		for (int i = 0; i < num; i++)
		{
			AbilityType abilityType = (AbilityType)i;
			ServerItem.Id id = ServerItem.ConvertAbilityId(abilityType);
			int num2 = this.m_charaState.AbilityLevel[id - ServerItem.Id.INVINCIBLE];
			float abilityPotential = instance.GetAbilityPotential(abilityType, num2);
			this.m_lvList.Add(abilityType, num2);
			this.m_paramList.Add(abilityType, abilityPotential);
		}
	}

	// Token: 0x060024FB RID: 9467 RVA: 0x000DD7A8 File Offset: 0x000DB9A8
	private IEnumerator SetupObject(bool init)
	{
		yield return null;
		if (this.m_mainPanel != null)
		{
			this.m_mainPanel.alpha = 1f;
		}
		if (init && this.m_animation != null)
		{
			ActiveAnimation anim = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim2", Direction.Forward);
			if (anim != null)
			{
				EventDelegate.Add(anim.onFinished, new EventDelegate.Callback(this.OnFinishedInAnim), true);
			}
		}
		GeneralUtil.SetButtonFunc(base.gameObject, "Btn_close", base.gameObject, "OnClickClose");
		GeneralUtil.SetButtonFunc(base.gameObject, "Btn_lv_up", base.gameObject, "OnClickLvUp");
		UIPlayAnimation btnClose = GameObjectUtil.FindChildGameObjectComponent<UIPlayAnimation>(base.gameObject, "Btn_close");
		if (btnClose != null && !EventDelegate.IsValid(btnClose.onFinished))
		{
			EventDelegate.Add(btnClose.onFinished, new EventDelegate.Callback(this.OnFinished), true);
		}
		this.m_isEnd = false;
		this.m_isClickClose = false;
		GameObject saleObj = GameObjectUtil.FindChildGameObject(base.gameObject, "img_icon_sale");
		UILabel labCaption = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_caption");
		UILabel labLv = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_player_lv");
		UILabel labCost = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_price_number");
		UITexture texChara = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, "img_player_tex");
		UISlider sliExp = GameObjectUtil.FindChildGameObjectComponent<UISlider>(base.gameObject, "Pgb_exp");
		UIImageButton imgBtn = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(base.gameObject, "Btn_lv_up");
		this.m_storage = GameObjectUtil.FindChildGameObjectComponent<UIRectItemStorage>(base.gameObject, "slot");
		if (this.m_storage != null && init)
		{
			if (this.m_btnObjList == null)
			{
				this.m_btnObjList = new Dictionary<AbilityType, MenuPlayerSetAbilityButton>();
			}
			else
			{
				this.m_btnObjList.Clear();
			}
			this.m_btnObjList = new Dictionary<AbilityType, MenuPlayerSetAbilityButton>();
			int cnt = 0;
			List<GameObject> buttonList = GameObjectUtil.FindChildGameObjects(this.m_storage.gameObject, "ui_player_set_item_2_cell(Clone)");
			foreach (AbilityType key in this.m_abilityList)
			{
				if (buttonList.Count > cnt)
				{
					MenuPlayerSetAbilityButton button = buttonList[cnt].GetComponent<MenuPlayerSetAbilityButton>();
					if (button == null)
					{
						button = buttonList[cnt].AddComponent<MenuPlayerSetAbilityButton>();
					}
					if (button != null)
					{
						button.Setup(this.m_charaType, key);
						this.m_btnObjList.Add(key, button);
					}
				}
				cnt++;
			}
		}
		if (texChara != null)
		{
			TextureRequestChara textureRequest = new TextureRequestChara(this.m_charaType, texChara);
			TextureAsyncLoadManager.Instance.Request(textureRequest);
			if (this.m_charaState.IsUnlocked)
			{
				texChara.color = new Color(1f, 1f, 1f);
			}
			else
			{
				texChara.color = new Color(0f, 0f, 0f);
			}
		}
		ServerItem serverItem = new ServerItem(this.m_charaType);
		int charaId = (int)serverItem.id;
		ServerCampaignData campaignData = ServerInterface.CampaignState.GetCampaignInSession(Constants.Campaign.emType.CharacterUpgradeCost, charaId);
		if (campaignData != null)
		{
			if (saleObj != null)
			{
				saleObj.SetActive(true);
			}
		}
		else if (saleObj != null)
		{
			saleObj.SetActive(false);
		}
		if (labCaption != null)
		{
			labCaption.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "chara_level_up_caption").text;
		}
		if (labLv != null)
		{
			int totalLevel = MenuPlayerSetUtil.GetTotalLevel(this.m_charaType);
			labLv.text = TextUtility.GetTextLevel(string.Format("{0:000}", totalLevel));
		}
		if (labCost != null)
		{
			int levelUpCost = MenuPlayerSetUtil.GetAbilityCost(this.m_charaType);
			int remainCost = levelUpCost - MenuPlayerSetUtil.GetCurrentExp(this.m_charaType);
			remainCost = Mathf.Max(0, remainCost);
			labCost.text = HudUtility.GetFormatNumString<int>(remainCost);
		}
		if (imgBtn != null)
		{
			imgBtn.isEnabled = !MenuPlayerSetUtil.IsCharacterLevelMax(this.m_charaType);
		}
		if (init)
		{
			SoundManager.SePlay("sys_window_open", "SE");
		}
		if (sliExp != null)
		{
			float expRatio = MenuPlayerSetUtil.GetCurrentExpRatio(this.m_charaType);
			sliExp.value = expRatio;
			GameObject barObj = GameObjectUtil.FindChildGameObject(base.gameObject, "img_bar_body");
			if (barObj != null)
			{
				barObj.SetActive(sliExp.value > 0f);
			}
		}
		yield break;
	}

	// Token: 0x060024FC RID: 9468 RVA: 0x000DD7D4 File Offset: 0x000DB9D4
	private void OnClickLvUp()
	{
		if (this.m_parent != null && this.m_parent.parent != null && this.m_parent.parent.isTutorial)
		{
			TutorialCursor.EndTutorialCursor(TutorialCursor.Type.CHARASELECT_LEVEL_UP);
			this.m_parent.parent.SetTutorialEnd();
		}
		if (GeneralUtil.IsNetwork())
		{
			this.SendLevelUp();
		}
		else
		{
			GeneralUtil.ShowNoCommunication("ShowNoCommunication");
		}
		global::Debug.Log("OnClickLvUp");
	}

	// Token: 0x060024FD RID: 9469 RVA: 0x000DD860 File Offset: 0x000DBA60
	private bool SendLevelUp()
	{
		bool result = false;
		if (!this.m_lock && this.m_charaState != null && this.m_charaType != CharaType.UNKNOWN)
		{
			SaveDataManager instance = SaveDataManager.Instance;
			if (instance != null)
			{
				int abilityCost = MenuPlayerSetUtil.GetAbilityCost(this.m_charaType);
				int num = abilityCost - MenuPlayerSetUtil.GetCurrentExp(this.m_charaType);
				num = Mathf.Max(0, num);
				int num2 = num;
				bool flag = true;
				if (instance.ItemData.RingCount - (uint)num2 < 0U)
				{
					flag = false;
				}
				if (flag)
				{
					BackKeyManager.InvalidFlag = true;
					this.m_currentLevelUpAbility = MenuPlayerSetUtil.GetNextLevelUpAbility(this.m_charaType);
					if (ServerInterface.LoggedInServerInterface != null && this.m_currentLevelUpAbility != AbilityType.NONE)
					{
						ServerInterface component = GameObject.Find("ServerInterface").GetComponent<ServerInterface>();
						int abilityId = MenuPlayerSetUtil.TransformServerAbilityID(this.m_currentLevelUpAbility);
						component.RequestServerUpgradeCharacter(this.m_charaState.Id, abilityId, base.gameObject);
						this.m_lock = true;
						result = true;
					}
				}
				else
				{
					GeneralWindow.CInfo info = default(GeneralWindow.CInfo);
					string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "insufficient_ring").text;
					string text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemSet", "gw_buy_ring__error_text").text;
					info.caption = text;
					info.message = text2;
					info.buttonType = GeneralWindow.ButtonType.ShopCancel;
					info.isPlayErrorSe = true;
					info.finishedCloseDelegate = new GeneralWindow.CInfo.FinishedCloseDelegate(this.GeneralWindowCloseCallback);
					GeneralWindow.Create(info);
				}
			}
		}
		return result;
	}

	// Token: 0x060024FE RID: 9470 RVA: 0x000DD9DC File Offset: 0x000DBBDC
	private void OnClickClose()
	{
		if (!this.m_isClickClose)
		{
			if (this.m_parent != null && this.m_parent.parent != null && this.m_parent.parent.isTutorial)
			{
				TutorialCursor.EndTutorialCursor(TutorialCursor.Type.CHARASELECT_LEVEL_UP);
				this.m_parent.parent.SetTutorialEnd();
			}
			this.m_isClickClose = true;
			PlayerLvupWindow.s_isActive = false;
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim2", Direction.Reverse);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinished), true);
			}
			SoundManager.SePlay("sys_window_close", "SE");
		}
	}

	// Token: 0x060024FF RID: 9471 RVA: 0x000DDA9C File Offset: 0x000DBC9C
	public void OnFinished()
	{
		PlayerLvupWindow.s_isActive = false;
		this.m_isEnd = true;
		if (this.m_mainPanel != null)
		{
			this.m_mainPanel.alpha = 0f;
		}
		base.gameObject.SetActive(false);
		base.enabled = false;
	}

	// Token: 0x06002500 RID: 9472 RVA: 0x000DDAEC File Offset: 0x000DBCEC
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (this.m_isEnd)
		{
			return;
		}
		if (msg != null)
		{
			msg.StaySequence();
		}
		if (this.m_parent != null && this.m_parent.parent != null && this.m_parent.parent.isTutorial)
		{
			TutorialCursor.EndTutorialCursor(TutorialCursor.Type.CHARASELECT_LEVEL_UP);
			this.m_parent.parent.SetTutorialEnd();
		}
		if (!this.m_isClickClose && !GeneralWindow.Created && !NetworkErrorWindow.Created)
		{
			PlayerLvupWindow.s_isActive = false;
			this.m_isClickClose = true;
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim2", Direction.Reverse);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinished), true);
			}
			SoundManager.SePlay("sys_window_close", "SE");
		}
	}

	// Token: 0x06002501 RID: 9473 RVA: 0x000DDBD8 File Offset: 0x000DBDD8
	private void OnFinishedInAnim()
	{
		if (this.m_parent == null)
		{
			return;
		}
		PlayerCharaList parent = this.m_parent.parent;
		if (parent == null)
		{
			return;
		}
		if (!parent.isTutorial)
		{
			BackKeyManager.InvalidFlag = false;
		}
	}

	// Token: 0x06002502 RID: 9474 RVA: 0x000DDC24 File Offset: 0x000DBE24
	private void ServerUpgradeCharacter_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		this.m_lock = false;
		BackKeyManager.InvalidFlag = false;
		SoundManager.SePlay("sys_buy", "SE");
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
		StageAbilityManager instance = StageAbilityManager.Instance;
		if (instance != null)
		{
			instance.RecalcAbilityVaue();
		}
		ServerPlayerState playerState = ServerInterface.PlayerState;
		if (playerState != null)
		{
			this.m_charaState = playerState.CharacterState(this.m_charaType);
		}
		if (this.m_currentLevelUpAbility != AbilityType.NONE)
		{
			MenuPlayerSetAbilityButton menuPlayerSetAbilityButton = this.m_btnObjList[this.m_currentLevelUpAbility];
			if (menuPlayerSetAbilityButton != null)
			{
				menuPlayerSetAbilityButton.LevelUp(new MenuPlayerSetAbilityButton.AnimEndCallback(this.LevelUpAnimationEndCallback));
			}
		}
		this.m_currentLevelUpAbility = AbilityType.NONE;
		this.SetParam();
		base.StartCoroutine(this.SetupObject(false));
		if (this.m_parent != null)
		{
			this.m_parent.UpdateView();
		}
	}

	// Token: 0x06002503 RID: 9475 RVA: 0x000DDD00 File Offset: 0x000DBF00
	public void GeneralWindowCloseCallback()
	{
		global::Debug.Log("GeneralWindowCloseCallback IsOkButtonPressed:" + GeneralWindow.IsOkButtonPressed);
		if (GeneralWindow.IsYesButtonPressed)
		{
			PlayerLvupWindow.s_isActive = false;
			this.m_isClickClose = true;
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim2", Direction.Reverse);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinished), true);
			}
			HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.RING_TO_SHOP, false);
			this.m_lock = false;
		}
		BackKeyManager.InvalidFlag = false;
	}

	// Token: 0x06002504 RID: 9476 RVA: 0x000DDD88 File Offset: 0x000DBF88
	private void LevelUpAnimationEndCallback()
	{
	}

	// Token: 0x0400211F RID: 8479
	private static bool s_isActive;

	// Token: 0x04002120 RID: 8480
	[SerializeField]
	private Animation m_animation;

	// Token: 0x04002121 RID: 8481
	[SerializeField]
	private UIPanel m_mainPanel;

	// Token: 0x04002122 RID: 8482
	private bool m_isClickClose;

	// Token: 0x04002123 RID: 8483
	private bool m_isEnd;

	// Token: 0x04002124 RID: 8484
	private ui_player_set_scroll m_parent;

	// Token: 0x04002125 RID: 8485
	private ServerCharacterState m_charaState;

	// Token: 0x04002126 RID: 8486
	private CharaType m_charaType = CharaType.UNKNOWN;

	// Token: 0x04002127 RID: 8487
	private AbilityType m_currentLevelUpAbility = AbilityType.NONE;

	// Token: 0x04002128 RID: 8488
	private Dictionary<AbilityType, int> m_lvList;

	// Token: 0x04002129 RID: 8489
	private Dictionary<AbilityType, float> m_paramList;

	// Token: 0x0400212A RID: 8490
	private List<AbilityType> m_abilityList;

	// Token: 0x0400212B RID: 8491
	private bool m_lock;

	// Token: 0x0400212C RID: 8492
	private UIRectItemStorage m_storage;

	// Token: 0x0400212D RID: 8493
	private Dictionary<AbilityType, MenuPlayerSetAbilityButton> m_btnObjList;
}
