using System;
using System.Collections;
using System.Collections.Generic;
using AnimationOrTween;
using Message;
using Text;
using UnityEngine;

// Token: 0x020004DE RID: 1246
public class PlayerSetWindowUI : WindowBase
{
	// Token: 0x170004E8 RID: 1256
	// (get) Token: 0x06002515 RID: 9493 RVA: 0x000DEA8C File Offset: 0x000DCC8C
	public static bool isActive
	{
		get
		{
			return PlayerSetWindowUI.s_isActive;
		}
	}

	// Token: 0x06002516 RID: 9494 RVA: 0x000DEA94 File Offset: 0x000DCC94
	private void OnDestroy()
	{
		base.Destroy();
	}

	// Token: 0x06002517 RID: 9495 RVA: 0x000DEA9C File Offset: 0x000DCC9C
	private void Update()
	{
		if (this.m_buyId != ServerItem.Id.NONE && GeneralWindow.IsCreated("BuyCharacter") && GeneralWindow.IsButtonPressed)
		{
			if (GeneralWindow.IsYesButtonPressed)
			{
				if (GeneralUtil.IsNetwork())
				{
					if (this.m_buyCostList != null && this.m_buyCostList.ContainsKey(this.m_buyId))
					{
						ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
						if (loggedInServerInterface != null)
						{
							this.SetBtnObjectCollider(false);
							this.m_oldUnlockedCharacterNum = 1;
							ServerPlayerState playerState = ServerInterface.PlayerState;
							if (playerState != null)
							{
								this.m_oldUnlockedCharacterNum = playerState.unlockedCharacterNum;
							}
							ServerItem item = new ServerItem(this.m_buyId);
							loggedInServerInterface.RequestServerUnlockedCharacter(this.m_charaType, item, base.gameObject);
						}
					}
				}
				else
				{
					GeneralUtil.ShowNoCommunication("ShowNoCommunication");
				}
			}
			this.m_buyId = ServerItem.Id.NONE;
		}
		if (this.m_btnDelay > 0f)
		{
			this.m_btnDelay -= Time.deltaTime;
			if (this.m_btnDelay <= 0f)
			{
				this.SetBtnObjectEnabeld(true, 2f);
				this.m_btnDelay = 0f;
			}
		}
	}

	// Token: 0x06002518 RID: 9496 RVA: 0x000DEBBC File Offset: 0x000DCDBC
	public void Init()
	{
		base.gameObject.SetActive(true);
		this.m_panel = GameObjectUtil.FindChildGameObjectComponent<UIPanel>(base.gameObject, "player_set_window");
		this.m_panel.alpha = 0f;
		this.m_animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(base.gameObject, "player_set_window");
		if (this.m_animation != null)
		{
			GameObject gameObject = this.m_animation.gameObject;
			gameObject.SetActive(false);
			this.m_animation.Stop();
		}
		this.m_playAnimation = base.gameObject.AddComponent<UIPlayAnimation>();
		this.m_playAnimation.target = this.m_animation;
		this.m_btnClose = GameObjectUtil.FindChildGameObjectComponent<UIButton>(base.gameObject, "Btn_window_close");
		this.m_btnObjList = new List<GameObject>();
		for (int i = 0; i < 7; i++)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_" + i);
			if (gameObject2 != null)
			{
				this.m_btnObjList.Add(gameObject2);
			}
			else if (i > 0)
			{
				break;
			}
		}
		UIButtonMessage uibuttonMessage = this.m_btnClose.gameObject.GetComponent<UIButtonMessage>();
		if (uibuttonMessage == null)
		{
			uibuttonMessage = this.m_btnClose.gameObject.AddComponent<UIButtonMessage>();
		}
		if (uibuttonMessage != null)
		{
			uibuttonMessage.trigger = UIButtonMessage.Trigger.OnClick;
			uibuttonMessage.target = base.gameObject;
			uibuttonMessage.functionName = "OnClickBtn";
		}
		base.gameObject.SetActive(false);
		this.m_init = true;
	}

	// Token: 0x06002519 RID: 9497 RVA: 0x000DED48 File Offset: 0x000DCF48
	private void OnClickBtn()
	{
		if (this.m_setup)
		{
			PlayerSetWindowUI.s_isActive = false;
			this.m_setup = false;
			this.m_costErrorType = ButtonInfoTable.ButtonType.UNKNOWN;
			this.m_buyId = ServerItem.Id.NONE;
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim", Direction.Reverse);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnAnimFinished), true);
			}
			SoundManager.SePlay("sys_window_close", "SE");
		}
	}

	// Token: 0x0600251A RID: 9498 RVA: 0x000DEDC4 File Offset: 0x000DCFC4
	private void OnAnimFinished()
	{
		base.gameObject.SetActive(false);
		this.m_windMode = PlayerSetWindowUI.WINDOW_MODE.DEFAULT;
		PlayerSetWindowUI.s_isActive = false;
	}

	// Token: 0x0600251B RID: 9499 RVA: 0x000DEDE0 File Offset: 0x000DCFE0
	private IEnumerator OnFinished()
	{
		yield return new WaitForSeconds(0.5f);
		this.m_windMode = PlayerSetWindowUI.WINDOW_MODE.DEFAULT;
		PlayerSetWindowUI.s_isActive = false;
		base.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x0600251C RID: 9500 RVA: 0x000DEDFC File Offset: 0x000DCFFC
	private void ResetBtnObjectList()
	{
		if (this.m_btnObjectList != null)
		{
			this.m_btnObjectList.Clear();
		}
		else
		{
			this.m_btnObjectList = new List<UIImageButton>();
		}
	}

	// Token: 0x0600251D RID: 9501 RVA: 0x000DEE30 File Offset: 0x000DD030
	private void AddBtnObjectList(UIImageButton btnObject)
	{
		if (this.m_btnObjectList == null)
		{
			this.m_btnObjectList = new List<UIImageButton>();
		}
		btnObject.isEnabled = (this.m_btnDelay <= 0f);
		this.m_btnObjectList.Add(btnObject);
	}

	// Token: 0x0600251E RID: 9502 RVA: 0x000DEE78 File Offset: 0x000DD078
	private void SetBtnObjectCollider(bool enabeld)
	{
		if (this.m_btnObjectList != null && this.m_btnObjectList.Count > 0)
		{
			foreach (UIImageButton uiimageButton in this.m_btnObjectList)
			{
				if (uiimageButton != null)
				{
					BoxCollider component = uiimageButton.gameObject.GetComponent<BoxCollider>();
					if (component != null)
					{
						component.isTrigger = !enabeld;
					}
				}
			}
		}
	}

	// Token: 0x0600251F RID: 9503 RVA: 0x000DEF24 File Offset: 0x000DD124
	private void SetBtnObjectEnabeld(bool enabeld, float delay = 2f)
	{
		if (this.m_btnObjectList != null && this.m_btnObjectList.Count > 0)
		{
			foreach (UIImageButton uiimageButton in this.m_btnObjectList)
			{
				if (uiimageButton != null)
				{
					uiimageButton.isEnabled = enabeld;
				}
			}
		}
		if (enabeld)
		{
			this.m_btnDelay = 0f;
			this.SetBtnObjectCollider(true);
		}
		else
		{
			this.m_btnDelay = delay;
		}
	}

	// Token: 0x06002520 RID: 9504 RVA: 0x000DEFD8 File Offset: 0x000DD1D8
	private void SetupCharaSetBtnView(GameObject parent, string mainObjName, string subObjName)
	{
		GeneralUtil.SetButtonFunc(parent, mainObjName, base.gameObject, "OnClickBtnMain");
		GeneralUtil.SetButtonFunc(parent, subObjName, base.gameObject, "OnClickBtnSub");
		UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(parent, mainObjName);
		UIImageButton uiimageButton2 = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(parent, subObjName);
		if (uiimageButton != null && uiimageButton2 != null)
		{
			if (this.m_parent != null && this.m_parent.parent != null)
			{
				uiimageButton.isEnabled = this.m_parent.parent.CheckSetMode(PlayerCharaList.SET_CHARA_MODE.MAIN, this.m_charaType);
				uiimageButton2.isEnabled = this.m_parent.parent.CheckSetMode(PlayerCharaList.SET_CHARA_MODE.SUB, this.m_charaType);
			}
			else
			{
				uiimageButton.isEnabled = true;
				uiimageButton2.isEnabled = true;
			}
		}
	}

	// Token: 0x06002521 RID: 9505 RVA: 0x000DF0A8 File Offset: 0x000DD2A8
	private void SetupRouletteBtnView(GameObject parent, string objName)
	{
		GeneralUtil.SetButtonFunc(parent, objName, base.gameObject, "OnClickBtnRoulette");
		UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(parent, objName);
		GameObject gameObject = GameObjectUtil.FindChildGameObject(parent, objName + "_1");
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(parent, objName + "_2");
		GameObject gameObject3 = GameObjectUtil.FindChildGameObject(parent, "img_sale_icon");
		if (uiimageButton != null)
		{
			this.AddBtnObjectList(uiimageButton);
		}
		if (gameObject != null && gameObject2 != null)
		{
			gameObject.SetActive(false);
			gameObject2.SetActive(true);
		}
		if (gameObject3 != null)
		{
			if (HudMenuUtility.IsSale(Constants.Campaign.emType.ChaoRouletteCost))
			{
				gameObject3.SetActive(true);
			}
			else if (HudMenuUtility.IsSale(Constants.Campaign.emType.PremiumRouletteOdds))
			{
				gameObject3.SetActive(true);
			}
			else
			{
				gameObject3.SetActive(false);
			}
		}
	}

	// Token: 0x06002522 RID: 9506 RVA: 0x000DF17C File Offset: 0x000DD37C
	private void SetupBuyBtnView(GameObject parent, string objName, ServerItem.Id costItem, int costValue)
	{
		GeneralUtil.SetButtonFunc(parent, objName, base.gameObject, "OnClickBtn_" + costItem);
		UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(parent, objName);
		GameObject gameObject = GameObjectUtil.FindChildGameObject(parent, objName + "_1");
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(parent, objName + "_2");
		GameObject gameObject3 = GameObjectUtil.FindChildGameObject(parent, "img_sale_icon");
		if (uiimageButton != null)
		{
			this.AddBtnObjectList(uiimageButton);
		}
		if (gameObject != null && gameObject2 != null)
		{
			gameObject.SetActive(true);
			gameObject2.SetActive(false);
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_icon_ring");
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_cost");
			if (uisprite != null && uilabel != null)
			{
				if (costItem != ServerItem.Id.SPECIAL_EGG)
				{
					if (costItem != ServerItem.Id.RSRING)
					{
						if (costItem != ServerItem.Id.RING)
						{
							if (costItem != ServerItem.Id.RAIDRING)
							{
								uisprite.spriteName = string.Empty;
							}
							else
							{
								uisprite.spriteName = "ui_event_ring_icon";
							}
						}
						else
						{
							uisprite.spriteName = "ui_test_icon_ring00";
						}
					}
					else
					{
						uisprite.spriteName = "ui_test_icon_rsring";
					}
				}
				else
				{
					uisprite.spriteName = "ui_roulette_pager_icon_8";
				}
				uilabel.text = HudUtility.GetFormatNumString<int>(costValue);
			}
		}
		else
		{
			UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(parent, "img_icon_ring");
			UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parent, "Lbl_rs_cost_1");
			if (uilabel2 == null)
			{
				uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parent, "Lbl_rs_cost_2");
			}
			if (uisprite2 != null && uilabel2 != null)
			{
				if (costItem != ServerItem.Id.SPECIAL_EGG)
				{
					if (costItem != ServerItem.Id.RSRING)
					{
						if (costItem != ServerItem.Id.RING)
						{
							if (costItem != ServerItem.Id.RAIDRING)
							{
								uisprite2.spriteName = string.Empty;
							}
							else
							{
								uisprite2.spriteName = "ui_event_ring_icon";
							}
						}
						else
						{
							uisprite2.spriteName = "ui_test_icon_ring00";
						}
					}
					else
					{
						uisprite2.spriteName = "ui_test_icon_rsring";
					}
				}
				else
				{
					uisprite2.spriteName = "ui_roulette_pager_icon_8";
				}
				uilabel2.text = HudUtility.GetFormatNumString<int>(costValue);
			}
		}
		if (gameObject3 != null)
		{
			ServerItem serverItem = new ServerItem(this.m_charaType);
			int id = (int)serverItem.id;
			ServerCampaignData campaignInSession = ServerInterface.CampaignState.GetCampaignInSession(Constants.Campaign.emType.CharacterUpgradeCost, id);
			gameObject3.SetActive(campaignInSession != null);
		}
	}

	// Token: 0x06002523 RID: 9507 RVA: 0x000DF414 File Offset: 0x000DD614
	private void SetupBtn()
	{
		this.ResetBtnObjectList();
		ServerPlayerState playerState = ServerInterface.PlayerState;
		if (playerState != null)
		{
			this.m_charaState = playerState.CharacterState(this.m_charaType);
		}
		else
		{
			this.m_charaState = null;
		}
		this.m_buyCostList = null;
		PlayerSetWindowUI.WINDOW_MODE windMode = this.m_windMode;
		if (windMode != PlayerSetWindowUI.WINDOW_MODE.BUY)
		{
			if (windMode != PlayerSetWindowUI.WINDOW_MODE.SET)
			{
				this.m_btnType = PlayerSetWindowUI.BTN_TYPE.NONE;
			}
			else
			{
				this.m_btnType = PlayerSetWindowUI.BTN_TYPE.SET;
			}
		}
		else
		{
			this.m_btnType = PlayerSetWindowUI.BTN_TYPE.BUY_1;
			if (this.m_charaState != null)
			{
				int num = 0;
				if (this.m_charaState.IsBuy)
				{
					this.m_buyCostList = this.m_charaState.GetBuyCostItemList();
					if (this.m_buyCostList != null)
					{
						num = this.m_buyCostList.Count;
					}
				}
				if (this.m_charaState.IsRoulette)
				{
					num++;
				}
				if (num == 1)
				{
					this.m_btnType = PlayerSetWindowUI.BTN_TYPE.BUY_1;
				}
				else if (num == 2)
				{
					this.m_btnType = PlayerSetWindowUI.BTN_TYPE.BUY_2;
				}
				else if (num == 3)
				{
					this.m_btnType = PlayerSetWindowUI.BTN_TYPE.BUY_3;
				}
				else
				{
					this.m_btnType = PlayerSetWindowUI.BTN_TYPE.NONE;
				}
			}
		}
		List<ServerItem.Id> list = null;
		if (this.m_buyCostList != null && this.m_buyCostList.Count > 0)
		{
			list = new List<ServerItem.Id>();
			Dictionary<ServerItem.Id, int>.KeyCollection keys = this.m_buyCostList.Keys;
			foreach (ServerItem.Id item in keys)
			{
				list.Add(item);
			}
		}
		int num2;
		switch (this.m_btnType)
		{
		case PlayerSetWindowUI.BTN_TYPE.BUY_1:
			num2 = 0;
			break;
		case PlayerSetWindowUI.BTN_TYPE.BUY_2:
			num2 = 1;
			break;
		case PlayerSetWindowUI.BTN_TYPE.BUY_3:
			num2 = 2;
			break;
		case PlayerSetWindowUI.BTN_TYPE.SET:
			num2 = 3;
			break;
		default:
			num2 = -1;
			break;
		}
		for (int i = 0; i < this.m_btnObjList.Count; i++)
		{
			if (i == num2)
			{
				this.m_btnObjList[i].SetActive(true);
				switch (this.m_btnType)
				{
				case PlayerSetWindowUI.BTN_TYPE.BUY_1:
					if (this.m_charaState.IsRoulette)
					{
						this.SetupRouletteBtnView(this.m_btnObjList[i], "Btn_c");
					}
					else
					{
						this.SetupBuyBtnView(this.m_btnObjList[i], "Btn_c", list[0], this.m_buyCostList[list[0]]);
					}
					break;
				case PlayerSetWindowUI.BTN_TYPE.BUY_2:
					if (this.m_charaState.IsRoulette)
					{
						this.SetupRouletteBtnView(this.m_btnObjList[i], "Btn_l");
						this.SetupBuyBtnView(this.m_btnObjList[i], "Btn_r", list[0], this.m_buyCostList[list[0]]);
					}
					else
					{
						this.SetupBuyBtnView(this.m_btnObjList[i], "Btn_l", list[0], this.m_buyCostList[list[0]]);
						this.SetupBuyBtnView(this.m_btnObjList[i], "Btn_r", list[1], this.m_buyCostList[list[1]]);
					}
					break;
				case PlayerSetWindowUI.BTN_TYPE.BUY_3:
					if (this.m_charaState.IsRoulette)
					{
						this.SetupRouletteBtnView(this.m_btnObjList[i], "Btn_l");
						this.SetupBuyBtnView(this.m_btnObjList[i], "Btn_c", list[0], this.m_buyCostList[list[0]]);
						this.SetupBuyBtnView(this.m_btnObjList[i], "Btn_r", list[1], this.m_buyCostList[list[1]]);
					}
					else
					{
						this.SetupBuyBtnView(this.m_btnObjList[i], "Btn_l", list[0], this.m_buyCostList[list[0]]);
						this.SetupBuyBtnView(this.m_btnObjList[i], "Btn_c", list[1], this.m_buyCostList[list[1]]);
						this.SetupBuyBtnView(this.m_btnObjList[i], "Btn_r", list[2], this.m_buyCostList[list[2]]);
					}
					break;
				case PlayerSetWindowUI.BTN_TYPE.SET:
					this.SetupCharaSetBtnView(this.m_btnObjList[i], "Btn_main", "Btn_sub");
					break;
				}
			}
			else
			{
				this.m_btnObjList[i].SetActive(false);
			}
		}
	}

	// Token: 0x06002524 RID: 9508 RVA: 0x000DF8F4 File Offset: 0x000DDAF4
	private void SetupObject()
	{
		if (this.m_panel != null)
		{
			this.m_panel.depth = 54;
			this.m_panel.alpha = 1f;
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "img_icon_key");
		if (gameObject != null)
		{
			gameObject.SetActive(!this.m_charaState.IsUnlocked);
		}
		string commonText = TextUtility.GetCommonText("CharaName", CharaName.Name[(int)this.m_charaType]);
		string charaAttributeSpriteName = HudUtility.GetCharaAttributeSpriteName(this.m_charaType);
		string teamAttributeSpriteName = HudUtility.GetTeamAttributeSpriteName(this.m_charaType);
		string charaAttributeText = this.m_charaState.GetCharaAttributeText();
		int totalLevel = MenuPlayerSetUtil.GetTotalLevel(this.m_charaType);
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_player_speacies");
		UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_player_genus");
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_player_lv");
		UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_player_name");
		UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_player_attribute");
		UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_player_star_lv");
		UILabel uilabel5 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbt_caption");
		if (uilabel5 != null)
		{
			switch (this.m_btnType)
			{
			case PlayerSetWindowUI.BTN_TYPE.BUY_1:
			case PlayerSetWindowUI.BTN_TYPE.BUY_2:
			case PlayerSetWindowUI.BTN_TYPE.BUY_3:
				uilabel5.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "PlayerSet", "ui_Lbt_buy_caption").text;
				break;
			case PlayerSetWindowUI.BTN_TYPE.SET:
				uilabel5.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "PlayerSet", "ui_Lbt_caption").text;
				break;
			case PlayerSetWindowUI.BTN_TYPE.NONE:
				uilabel5.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "PlayerSet", "ui_Lbt_info_caption").text;
				break;
			}
		}
		if (uilabel4 != null)
		{
			uilabel4.text = this.m_charaState.star.ToString();
			if (!PlayerSetWindowUI.s_starTextDefaultInit)
			{
				PlayerSetWindowUI.s_starTextDefault = new Color(uilabel4.color.r, uilabel4.color.g, uilabel4.color.b, uilabel4.color.a);
				PlayerSetWindowUI.s_starTextDefaultInit = true;
			}
			if (this.m_charaState.star >= this.m_charaState.starMax)
			{
				uilabel4.color = new Color(0.9647059f, 0.45490196f, 0f);
			}
			else
			{
				uilabel4.color = PlayerSetWindowUI.s_starTextDefault;
			}
		}
		UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, "img_player_tex");
		if (uitexture != null)
		{
			TextureRequestChara request = new TextureRequestChara(this.m_charaType, uitexture);
			TextureAsyncLoadManager.Instance.Request(request);
			if (this.m_charaState.IsUnlocked)
			{
				uitexture.color = new Color(1f, 1f, 1f);
			}
			else
			{
				uitexture.color = new Color(0f, 0f, 0f);
			}
		}
		uisprite.spriteName = charaAttributeSpriteName;
		uisprite2.spriteName = teamAttributeSpriteName;
		uilabel.text = TextUtility.GetTextLevel(string.Format("{0:000}", totalLevel));
		uilabel2.text = commonText;
		uilabel3.text = charaAttributeText;
	}

	// Token: 0x06002525 RID: 9509 RVA: 0x000DFC4C File Offset: 0x000DDE4C
	public void Setup(CharaType charaType, ui_player_set_scroll parent = null, PlayerSetWindowUI.WINDOW_MODE mode = PlayerSetWindowUI.WINDOW_MODE.DEFAULT)
	{
		PlayerSetWindowUI.s_isActive = true;
		this.m_setup = true;
		this.m_costError = false;
		this.m_costErrorType = ButtonInfoTable.ButtonType.UNKNOWN;
		this.m_buyId = ServerItem.Id.NONE;
		if (!this.m_init)
		{
			this.Init();
		}
		this.m_parent = parent;
		this.m_charaType = charaType;
		this.m_windMode = mode;
		this.m_btnDelay = 0f;
		this.SetupBtn();
		if (this.m_charaState != null)
		{
			base.gameObject.SetActive(true);
			this.SetupObject();
			if (this.m_animation != null)
			{
				GameObject gameObject = this.m_animation.gameObject;
				gameObject.SetActive(true);
				this.m_playAnimation.Play(true);
			}
			UIDraggablePanel uidraggablePanel = GameObjectUtil.FindChildGameObjectComponent<UIDraggablePanel>(base.gameObject, "ScrollView");
			if (uidraggablePanel != null)
			{
				uidraggablePanel.ResetPosition();
			}
			SoundManager.SePlay("sys_window_open", "SE");
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "eff_set1");
		GameObject gameObject3 = GameObjectUtil.FindChildGameObject(base.gameObject, "eff_set2");
		if (gameObject2 != null && gameObject3 != null)
		{
			gameObject2.SetActive(false);
			gameObject3.SetActive(false);
		}
		this.SetBtnObjectCollider(true);
	}

	// Token: 0x06002526 RID: 9510 RVA: 0x000DFD84 File Offset: 0x000DDF84
	public static PlayerSetWindowUI Create(CharaType charaType, ui_player_set_scroll parent = null, PlayerSetWindowUI.WINDOW_MODE mode = PlayerSetWindowUI.WINDOW_MODE.DEFAULT)
	{
		GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
		if (cameraUIObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(cameraUIObject, "PlayerSetWindowUI");
			PlayerSetWindowUI playerSetWindowUI = null;
			if (gameObject != null)
			{
				playerSetWindowUI = gameObject.GetComponent<PlayerSetWindowUI>();
				if (playerSetWindowUI == null)
				{
					playerSetWindowUI = gameObject.AddComponent<PlayerSetWindowUI>();
				}
				if (playerSetWindowUI != null)
				{
					playerSetWindowUI.Setup(charaType, parent, mode);
				}
			}
			return playerSetWindowUI;
		}
		return null;
	}

	// Token: 0x06002527 RID: 9511 RVA: 0x000DFDF0 File Offset: 0x000DDFF0
	private void OnClickBtnRoulette()
	{
		PlayerSetWindowUI.s_isActive = false;
		this.m_setup = false;
		this.m_buyId = ServerItem.Id.NONE;
		ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim", Direction.Reverse);
		if (activeAnimation != null)
		{
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnAnimFinished), true);
		}
		HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.ROULETTE, false);
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x06002528 RID: 9512 RVA: 0x000DFE60 File Offset: 0x000DE060
	private void OnClickBtn_RING()
	{
		if (this.m_buyCostList != null && this.m_buyCostList.Count > 0 && this.m_buyCostList.ContainsKey(ServerItem.Id.RING))
		{
			int num = this.m_buyCostList[ServerItem.Id.RING];
			if (this.SendBuyChara(ServerItem.Id.RING, num))
			{
				global::Debug.Log("OnClickBtn_RING value:" + num);
			}
		}
	}

	// Token: 0x06002529 RID: 9513 RVA: 0x000DFED8 File Offset: 0x000DE0D8
	private void OnClickBtn_RSRING()
	{
		if (this.m_buyCostList != null && this.m_buyCostList.Count > 0 && this.m_buyCostList.ContainsKey(ServerItem.Id.RSRING))
		{
			int num = this.m_buyCostList[ServerItem.Id.RSRING];
			if (this.SendBuyChara(ServerItem.Id.RSRING, num))
			{
				global::Debug.Log("OnClickBtn_RSRING value:" + num);
			}
		}
	}

	// Token: 0x0600252A RID: 9514 RVA: 0x000DFF50 File Offset: 0x000DE150
	private void OnClickBtnMain()
	{
		if (this.m_parent != null && this.m_parent.parent != null)
		{
			this.m_parent.parent.SetChara(PlayerCharaList.SET_CHARA_MODE.MAIN, this.m_charaType);
		}
		PlayerSetWindowUI.s_isActive = false;
		ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim", Direction.Reverse);
		if (activeAnimation != null)
		{
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnAnimFinished), true);
		}
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x0600252B RID: 9515 RVA: 0x000DFFE8 File Offset: 0x000DE1E8
	private void OnClickBtnSub()
	{
		if (this.m_parent != null && this.m_parent.parent != null)
		{
			this.m_parent.parent.SetChara(PlayerCharaList.SET_CHARA_MODE.SUB, this.m_charaType);
		}
		PlayerSetWindowUI.s_isActive = false;
		ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim", Direction.Reverse);
		if (activeAnimation != null)
		{
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnAnimFinished), true);
		}
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x0600252C RID: 9516 RVA: 0x000E0080 File Offset: 0x000DE280
	private bool SendBuyChara(ServerItem.Id itemId, int cost)
	{
		bool result = false;
		if (GeneralUtil.IsNetwork())
		{
			if (this.m_charaState != null)
			{
				this.SetBtnObjectCollider(false);
				long itemCount = GeneralUtil.GetItemCount(itemId);
				if (itemCount >= (long)cost)
				{
					this.m_buyId = itemId;
					string text = string.Empty;
					string caption = string.Empty;
					if (itemId != ServerItem.Id.RSRING)
					{
						if (itemId == ServerItem.Id.RING)
						{
							if (this.m_charaState.IsUnlocked)
							{
								text = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "chara_unlock_ring_text_2");
							}
							else
							{
								text = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "chara_unlock_ring_text");
							}
						}
					}
					else if (this.m_charaState.IsUnlocked)
					{
						text = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "chara_unlock_rsring_text_2");
					}
					else
					{
						text = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "chara_unlock_rsring_text");
					}
					text = text.Replace("{RING_COST}", HudUtility.GetFormatNumString<int>(cost));
					if (this.m_charaState.IsUnlocked)
					{
						caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "chara_unlock_caption_2");
					}
					else
					{
						caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "chara_unlock_caption");
					}
					GeneralWindow.Create(new GeneralWindow.CInfo
					{
						name = "BuyCharacter",
						buttonType = GeneralWindow.ButtonType.YesNo,
						finishedCloseDelegate = new GeneralWindow.CInfo.FinishedCloseDelegate(this.GeneralWindowBuyCharacterClosedCallback),
						caption = caption,
						message = text
					});
					result = true;
				}
				else
				{
					string message = string.Empty;
					string caption2 = string.Empty;
					string name = string.Empty;
					this.m_costError = false;
					GeneralWindow.ButtonType buttonType = GeneralWindow.ButtonType.ShopCancel;
					bool flag = ServerInterface.IsRSREnable();
					if (itemId != ServerItem.Id.RSRING)
					{
						if (itemId == ServerItem.Id.RING)
						{
							name = "SpinCostErrorRing";
							caption2 = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "gw_cost_caption");
							message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", "gw_cost_text");
							this.m_costErrorType = ButtonInfoTable.ButtonType.RING_TO_SHOP;
						}
					}
					else
					{
						name = "SpinCostErrorRSRing";
						caption2 = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "gw_cost_caption");
						message = ((!flag) ? TextUtility.GetCommonText("ChaoRoulette", "gw_cost_caption_text_2") : TextUtility.GetCommonText("ChaoRoulette", "gw_cost_caption_text"));
						buttonType = ((!flag) ? GeneralWindow.ButtonType.Ok : GeneralWindow.ButtonType.ShopCancel);
						this.m_costErrorType = ButtonInfoTable.ButtonType.REDSTAR_TO_SHOP;
					}
					GeneralWindow.Create(new GeneralWindow.CInfo
					{
						name = name,
						buttonType = buttonType,
						finishedCloseDelegate = new GeneralWindow.CInfo.FinishedCloseDelegate(this.GeneralWindowClosedCallback),
						caption = caption2,
						message = message,
						isPlayErrorSe = true
					});
				}
			}
		}
		else
		{
			GeneralUtil.ShowNoCommunication("ShowNoCommunication");
		}
		return result;
	}

	// Token: 0x0600252D RID: 9517 RVA: 0x000E033C File Offset: 0x000DE53C
	private void GeneralWindowBuyCharacterClosedCallback()
	{
		if (!GeneralWindow.IsYesButtonPressed)
		{
			GeneralWindow.Close();
			this.SetBtnObjectEnabeld(true, 2f);
		}
	}

	// Token: 0x0600252E RID: 9518 RVA: 0x000E035C File Offset: 0x000DE55C
	private void GeneralWindowClosedCallback()
	{
		HudMenuUtility.SetConnectAlertSimpleUI(false);
		if (this.m_costErrorType != ButtonInfoTable.ButtonType.UNKNOWN)
		{
			if (GeneralWindow.IsYesButtonPressed)
			{
				PlayerSetWindowUI.s_isActive = false;
				this.m_setup = false;
				this.m_buyId = ServerItem.Id.NONE;
				ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim", Direction.Reverse);
				if (activeAnimation != null)
				{
					EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnAnimFinished), true);
				}
				HudMenuUtility.SendMenuButtonClicked(this.m_costErrorType, false);
				this.m_costErrorType = ButtonInfoTable.ButtonType.UNKNOWN;
			}
			else
			{
				this.SetBtnObjectEnabeld(true, 2f);
				this.m_costErrorType = ButtonInfoTable.ButtonType.UNKNOWN;
			}
		}
		GeneralWindow.Close();
	}

	// Token: 0x0600252F RID: 9519 RVA: 0x000E0400 File Offset: 0x000DE600
	private void ServerUnlockedCharacter_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(base.gameObject, "window");
		if (animation != null)
		{
			ActiveAnimation.Play(animation, "ui_menu_player_open_Anim", Direction.Forward);
		}
		if (this.m_parent != null)
		{
			this.m_parent.UpdateView();
		}
		this.SetupBtn();
		this.SetupObject();
		ServerPlayerState playerState = ServerInterface.PlayerState;
		if (playerState != null && playerState.unlockedCharacterNum == 2 && this.m_oldUnlockedCharacterNum == 1 && this.m_parent != null && this.m_parent.parent != null)
		{
			this.m_parent.parent.SetChara(PlayerCharaList.SET_CHARA_MODE.SUB, this.m_charaType);
		}
		global::Debug.Log(string.Concat(new object[]
		{
			"ServerUnlockedCharacter_Succeeded oldUnlockedCharacterNum:",
			this.m_oldUnlockedCharacterNum,
			"  currentUnlockedCharacterNum:",
			playerState.unlockedCharacterNum
		}));
		SoundManager.SePlay("sys_buy", "SE");
		this.SetBtnObjectEnabeld(false, 2f);
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
	}

	// Token: 0x06002530 RID: 9520 RVA: 0x000E0520 File Offset: 0x000DE720
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (msg != null)
		{
			msg.StaySequence();
		}
		if (!GeneralWindow.Created && !NetworkErrorWindow.Created)
		{
			PlayerSetWindowUI.s_isActive = false;
			this.m_setup = false;
			this.m_costErrorType = ButtonInfoTable.ButtonType.UNKNOWN;
			this.m_buyId = ServerItem.Id.NONE;
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim", Direction.Reverse);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnAnimFinished), true);
			}
			SoundManager.SePlay("sys_window_close", "SE");
		}
	}

	// Token: 0x0400213D RID: 8509
	private const float BTN_DELAY_TIME = 2f;

	// Token: 0x0400213E RID: 8510
	private static bool s_isActive;

	// Token: 0x0400213F RID: 8511
	private ui_player_set_scroll m_parent;

	// Token: 0x04002140 RID: 8512
	private bool m_init;

	// Token: 0x04002141 RID: 8513
	private bool m_setup;

	// Token: 0x04002142 RID: 8514
	private bool m_costError;

	// Token: 0x04002143 RID: 8515
	private ButtonInfoTable.ButtonType m_costErrorType = ButtonInfoTable.ButtonType.UNKNOWN;

	// Token: 0x04002144 RID: 8516
	private ServerItem.Id m_buyId = ServerItem.Id.NONE;

	// Token: 0x04002145 RID: 8517
	private PlayerSetWindowUI.WINDOW_MODE m_windMode;

	// Token: 0x04002146 RID: 8518
	private PlayerSetWindowUI.BTN_TYPE m_btnType = PlayerSetWindowUI.BTN_TYPE.NONE;

	// Token: 0x04002147 RID: 8519
	private CharaType m_charaType = CharaType.UNKNOWN;

	// Token: 0x04002148 RID: 8520
	private ServerCharacterState m_charaState;

	// Token: 0x04002149 RID: 8521
	private Dictionary<ServerItem.Id, int> m_buyCostList;

	// Token: 0x0400214A RID: 8522
	private List<UIImageButton> m_btnObjectList;

	// Token: 0x0400214B RID: 8523
	private float m_btnDelay;

	// Token: 0x0400214C RID: 8524
	private Animation m_animation;

	// Token: 0x0400214D RID: 8525
	private UIPlayAnimation m_playAnimation;

	// Token: 0x0400214E RID: 8526
	private UIPanel m_panel;

	// Token: 0x0400214F RID: 8527
	private int m_oldUnlockedCharacterNum;

	// Token: 0x04002150 RID: 8528
	private UIButton m_btnClose;

	// Token: 0x04002151 RID: 8529
	private List<GameObject> m_btnObjList;

	// Token: 0x04002152 RID: 8530
	private static bool s_starTextDefaultInit;

	// Token: 0x04002153 RID: 8531
	private static Color s_starTextDefault;

	// Token: 0x020004DF RID: 1247
	public enum WINDOW_MODE
	{
		// Token: 0x04002155 RID: 8533
		DEFAULT,
		// Token: 0x04002156 RID: 8534
		BUY,
		// Token: 0x04002157 RID: 8535
		SET
	}

	// Token: 0x020004E0 RID: 1248
	private enum BTN_TYPE
	{
		// Token: 0x04002159 RID: 8537
		BUY_1,
		// Token: 0x0400215A RID: 8538
		BUY_2,
		// Token: 0x0400215B RID: 8539
		BUY_3,
		// Token: 0x0400215C RID: 8540
		SET,
		// Token: 0x0400215D RID: 8541
		NONE
	}
}
