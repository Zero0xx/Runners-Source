using System;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x020004CD RID: 1229
public class MenuPlayerSetLevelUpButton : MonoBehaviour
{
	// Token: 0x06002450 RID: 9296 RVA: 0x000DA1D4 File Offset: 0x000D83D4
	private void OnEnable()
	{
	}

	// Token: 0x06002451 RID: 9297 RVA: 0x000DA1D8 File Offset: 0x000D83D8
	public void Setup(CharaType charaType, GameObject pageRootObject)
	{
		this.m_charaType = charaType;
		this.m_pageRootObject = pageRootObject;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_pageRootObject, "Btn_lv_up");
		if (gameObject != null)
		{
			UIButtonMessage uibuttonMessage = gameObject.GetComponent<UIButtonMessage>();
			if (uibuttonMessage == null)
			{
				uibuttonMessage = gameObject.AddComponent<UIButtonMessage>();
			}
			uibuttonMessage.target = base.gameObject;
			uibuttonMessage.functionName = "LevelUpButtonClickedCallback";
			this.m_saleIcon = GameObjectUtil.FindChildGameObject(gameObject, "img_icon_sale");
		}
		this.InitCostLabel();
	}

	// Token: 0x06002452 RID: 9298 RVA: 0x000DA258 File Offset: 0x000D8458
	public void InitCostLabel()
	{
		ServerItem serverItem = new ServerItem(this.m_charaType);
		int id = (int)serverItem.id;
		ServerCampaignData campaignInSession = ServerInterface.CampaignState.GetCampaignInSession(Constants.Campaign.emType.CharacterUpgradeCost, id);
		if (campaignInSession != null)
		{
			if (this.m_saleIcon != null)
			{
				this.m_saleIcon.SetActive(true);
			}
		}
		else if (this.m_saleIcon != null)
		{
			this.m_saleIcon.SetActive(false);
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_price_number");
		if (uilabel != null)
		{
			int abilityCost = MenuPlayerSetUtil.GetAbilityCost(this.m_charaType);
			int num = abilityCost - MenuPlayerSetUtil.GetCurrentExp(this.m_charaType);
			num = Mathf.Max(0, num);
			uilabel.text = HudUtility.GetFormatNumString<int>(num);
		}
		UISlider uislider = GameObjectUtil.FindChildGameObjectComponent<UISlider>(base.gameObject, "Pgb_exp");
		if (uislider != null)
		{
			float currentExpRatio = MenuPlayerSetUtil.GetCurrentExpRatio(this.m_charaType);
			uislider.value = currentExpRatio;
		}
	}

	// Token: 0x06002453 RID: 9299 RVA: 0x000DA350 File Offset: 0x000D8550
	public void SetCallback(MenuPlayerSetLevelUpButton.LevelUpCallback callback)
	{
		this.m_callback = callback;
	}

	// Token: 0x06002454 RID: 9300 RVA: 0x000DA35C File Offset: 0x000D855C
	public void OnLevelUpEnd()
	{
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(101);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06002455 RID: 9301 RVA: 0x000DA390 File Offset: 0x000D8590
	private void Start()
	{
		this.m_fsm = base.gameObject.AddComponent<TinyFsmBehavior>();
		TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
		description.initState = new TinyFsmState(new EventFunction(this.StateWaitLevelUpButtonPressed));
		description.onFixedUpdate = true;
		this.m_fsm.SetUp(description);
	}

	// Token: 0x06002456 RID: 9302 RVA: 0x000DA3E0 File Offset: 0x000D85E0
	private void Update()
	{
		this.InitCostLabel();
	}

	// Token: 0x06002457 RID: 9303 RVA: 0x000DA3E8 File Offset: 0x000D85E8
	private TinyFsmState StateWaitLevelUpButtonPressed(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_is_end_connect = false;
			if (MenuPlayerSetUtil.IsCharacterLevelMax(this.m_charaType))
			{
				UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(this.m_pageRootObject, "Btn_lv_up");
				if (uiimageButton != null)
				{
					uiimageButton.isEnabled = false;
				}
			}
			return TinyFsmState.End();
		default:
			if (signal != 100)
			{
				return TinyFsmState.End();
			}
			this.m_currentLevelUpAbility = MenuPlayerSetUtil.GetNextLevelUpAbility(this.m_charaType);
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateAskLevelUp)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06002458 RID: 9304 RVA: 0x000DA4AC File Offset: 0x000D86AC
	private TinyFsmState StateAskLevelUp(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
		{
			GeneralWindow.Close();
			SaveDataManager instance = SaveDataManager.Instance;
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
				if (ServerInterface.LoggedInServerInterface != null)
				{
					ServerInterface component = GameObject.Find("ServerInterface").GetComponent<ServerInterface>();
					ServerPlayerState playerState = ServerInterface.PlayerState;
					if (playerState != null)
					{
						ServerCharacterState serverCharacterState = playerState.CharacterState(this.m_charaType);
						if (serverCharacterState != null)
						{
							int abilityId = MenuPlayerSetUtil.TransformServerAbilityID(this.m_currentLevelUpAbility);
							component.RequestServerUpgradeCharacter(serverCharacterState.Id, abilityId, base.gameObject);
						}
					}
				}
				else
				{
					CharaData charaData = instance.CharaData;
					CharaAbility charaAbility = charaData.AbilityArray[(int)this.m_charaType];
					charaAbility.Ability[(int)this.m_currentLevelUpAbility] += 1U;
					this.ServerUpgradeCharacter_Succeeded(null);
				}
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitServerConnectEnd)));
			}
			else
			{
				GeneralWindow.CInfo info = default(GeneralWindow.CInfo);
				string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "insufficient_ring").text;
				string text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemSet", "gw_buy_ring__error_text").text;
				info.caption = text;
				info.message = text2;
				info.anchor_path = "Camera/menu_Anim/PlayerSet_2_UI/Anchor_5_MC";
				info.buttonType = GeneralWindow.ButtonType.Ok;
				info.isPlayErrorSe = true;
				info.finishedCloseDelegate = new GeneralWindow.CInfo.FinishedCloseDelegate(this.GeneralWindowCloseCallback);
				GeneralWindow.Create(info);
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateFailedLevelUp)));
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002459 RID: 9305 RVA: 0x000DA6B4 File Offset: 0x000D88B4
	private TinyFsmState StateWaitServerConnectEnd(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			if (this.m_is_end_connect)
			{
				this.m_callback(this.m_currentLevelUpAbility);
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitLevelUpAnimation)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600245A RID: 9306 RVA: 0x000DA73C File Offset: 0x000D893C
	private TinyFsmState StateFailedLevelUp(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600245B RID: 9307 RVA: 0x000DA788 File Offset: 0x000D8988
	private TinyFsmState StateWaitLevelUpAnimation(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		default:
			if (signal != 101)
			{
				return TinyFsmState.End();
			}
			BackKeyManager.InvalidFlag = false;
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitLevelUpButtonPressed)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x0600245C RID: 9308 RVA: 0x000DA808 File Offset: 0x000D8A08
	private TinyFsmState StateLevelUpAbilityExplain(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			int level = MenuPlayerSetUtil.GetLevel(this.m_charaType, this.m_currentLevelUpAbility);
			int abilityCost = MenuPlayerSetUtil.GetAbilityCost(this.m_charaType);
			GeneralWindow.CInfo info = default(GeneralWindow.CInfo);
			info.caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "level_up_caption").text;
			TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "level_up_text");
			text.ReplaceTag("{RING_COST}", abilityCost.ToString());
			string text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaStatus", "abilityname" + ((int)(this.m_currentLevelUpAbility + 1)).ToString()).text;
			text.ReplaceTag("{ABILITY_NAME}", text2);
			string text3 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaStatus", "abilitycaption" + ((int)(this.m_currentLevelUpAbility + 1)).ToString()).text;
			text.ReplaceTag("{ABILITY_CAPTION}", text3);
			text.ReplaceTag("{ABILITY_CAPTION2}", text3);
			TextObject text4 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaStatus", "abilitypotential" + ((int)(this.m_currentLevelUpAbility + 1)).ToString());
			TextObject text5 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaStatus", "abilitypotential" + ((int)(this.m_currentLevelUpAbility + 1)).ToString());
			float levelAbility = MenuPlayerSetUtil.GetLevelAbility(this.m_charaType, this.m_currentLevelUpAbility, level - 1);
			float levelAbility2 = MenuPlayerSetUtil.GetLevelAbility(this.m_charaType, this.m_currentLevelUpAbility, level);
			text4.ReplaceTag("{ABILITY_POTENTIAL}", levelAbility.ToString());
			text5.ReplaceTag("{ABILITY_POTENTIAL}", levelAbility2.ToString());
			text.ReplaceTag("{ABILITY_POTENTIAL}", text4.text);
			text.ReplaceTag("{ABILITY_POTENTIAL2}", text5.text);
			info.message = text.text;
			info.anchor_path = "Camera/menu_Anim/PlayerSet_2_UI/Anchor_5_MC";
			info.buttonType = GeneralWindow.ButtonType.Ok;
			info.finishedCloseDelegate = new GeneralWindow.CInfo.FinishedCloseDelegate(this.GeneralWindowCloseCallback);
			GeneralWindow.Create(info);
			return TinyFsmState.End();
		}
		case 4:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600245D RID: 9309 RVA: 0x000DAA3C File Offset: 0x000D8C3C
	private void LevelUpButtonClickedCallback()
	{
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(100);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
		if (HudMenuUtility.IsTutorial_CharaLevelUp())
		{
			GameObjectUtil.SendMessageFindGameObject("PlayerSet_2_UI", "OnClickedLevelUpButton", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0600245E RID: 9310 RVA: 0x000DAA8C File Offset: 0x000D8C8C
	private void ServerUpgradeCharacter_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		this.m_is_end_connect = true;
		SoundManager.SePlay("sys_buy", "SE");
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
		StageAbilityManager instance = StageAbilityManager.Instance;
		if (instance != null)
		{
			instance.RecalcAbilityVaue();
		}
	}

	// Token: 0x0600245F RID: 9311 RVA: 0x000DAAD0 File Offset: 0x000D8CD0
	public void GeneralWindowCloseCallback()
	{
		global::Debug.Log("GeneralWindowCloseCallback IsOkButtonPressed:" + GeneralWindow.IsOkButtonPressed);
		if (this.m_fsm != null && GeneralWindow.IsOkButtonPressed)
		{
			BackKeyManager.InvalidFlag = false;
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitLevelUpButtonPressed)));
		}
	}

	// Token: 0x040020D4 RID: 8404
	private TinyFsmBehavior m_fsm;

	// Token: 0x040020D5 RID: 8405
	private CharaType m_charaType;

	// Token: 0x040020D6 RID: 8406
	private GameObject m_pageRootObject;

	// Token: 0x040020D7 RID: 8407
	private bool m_is_end_connect;

	// Token: 0x040020D8 RID: 8408
	private MenuPlayerSetLevelUpButton.LevelUpCallback m_callback;

	// Token: 0x040020D9 RID: 8409
	private GameObject m_saleIcon;

	// Token: 0x040020DA RID: 8410
	private AbilityType m_currentLevelUpAbility;

	// Token: 0x020004CE RID: 1230
	private enum EventSignal
	{
		// Token: 0x040020DC RID: 8412
		BUTTON_PRESSED = 100,
		// Token: 0x040020DD RID: 8413
		LEVEL_UP_END
	}

	// Token: 0x02000A95 RID: 2709
	// (Invoke) Token: 0x0600488E RID: 18574
	public delegate void LevelUpCallback(AbilityType abilityType);
}
