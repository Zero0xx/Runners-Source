using System;
using System.Collections.Generic;
using AnimationOrTween;
using Message;
using SaveData;
using UnityEngine;

// Token: 0x020004D9 RID: 1241
public class PlayerCharaList : MonoBehaviour
{
	// Token: 0x170004E0 RID: 1248
	// (get) Token: 0x060024D2 RID: 9426 RVA: 0x000DC168 File Offset: 0x000DA368
	private ServerPlayerState.CHARA_SORT sortType
	{
		get
		{
			return this.m_sortType;
		}
	}

	// Token: 0x170004E1 RID: 1249
	// (get) Token: 0x060024D3 RID: 9427 RVA: 0x000DC170 File Offset: 0x000DA370
	private int sortOffset
	{
		get
		{
			return this.m_sortOffset;
		}
	}

	// Token: 0x170004E2 RID: 1250
	// (get) Token: 0x060024D4 RID: 9428 RVA: 0x000DC178 File Offset: 0x000DA378
	private int currentDeck
	{
		get
		{
			return this.m_currentDeck;
		}
	}

	// Token: 0x170004E3 RID: 1251
	// (get) Token: 0x060024D5 RID: 9429 RVA: 0x000DC180 File Offset: 0x000DA380
	public bool isTutorial
	{
		get
		{
			return this.m_tutorial;
		}
	}

	// Token: 0x060024D6 RID: 9430 RVA: 0x000DC188 File Offset: 0x000DA388
	public void SetTutorialEnd()
	{
		BackKeyManager.InvalidFlag = false;
		this.m_tutorial = false;
	}

	// Token: 0x060024D7 RID: 9431 RVA: 0x000DC198 File Offset: 0x000DA398
	private void Update()
	{
		if (this.m_changeDelay > 0f)
		{
			this.m_changeDelay -= Time.deltaTime;
			if (this.m_changeDelay <= 0f)
			{
				this.m_changeDelay = 0f;
				if (this.m_changeBtn != null)
				{
					if (this.CheckSetMode(PlayerCharaList.SET_CHARA_MODE.CHANGE, CharaType.UNKNOWN))
					{
						this.m_changeBtn.isEnabled = true;
					}
					else
					{
						this.m_changeBtn.isEnabled = false;
						this.m_changeDelay = -1f;
					}
				}
			}
		}
		if (!this.m_pickup && this.m_pageMax > 0 && GeneralWindow.IsCreated("ShowNoCommunicationPicupCharaList") && GeneralWindow.IsOkButtonPressed)
		{
			if (GeneralUtil.IsNetwork())
			{
				RouletteManager.Instance.RequestPicupCharaList(false);
				this.m_pickup = true;
			}
			else
			{
				GeneralUtil.ShowNoCommunication("ShowNoCommunicationPicupCharaList");
			}
		}
	}

	// Token: 0x060024D8 RID: 9432 RVA: 0x000DC284 File Offset: 0x000DA484
	public void Setup()
	{
		base.gameObject.SetActive(true);
		this.m_pickup = false;
		this.m_tutorial = HudMenuUtility.IsTutorial_CharaLevelUp();
		if (this.m_tutorial)
		{
			BackKeyManager.InvalidFlag = true;
			TutorialCursor.StartTutorialCursor(TutorialCursor.Type.CHARASELECT_CHARA);
			HudMenuUtility.SaveSystemDataFlagStatus(SystemData.FlagStatus.CHARA_LEVEL_UP_EXPLAINED);
		}
		if (RouletteManager.Instance != null && !RouletteManager.Instance.IsRequestPicupCharaList() && this.m_pageMax <= 0)
		{
			if (GeneralUtil.IsNetwork())
			{
				RouletteManager.Instance.RequestPicupCharaList(false);
				this.m_pickup = true;
			}
			else
			{
				GeneralUtil.ShowNoCommunication("ShowNoCommunicationPicupCharaList");
				this.m_pickup = false;
			}
		}
		this.m_currentDeck = DeckUtil.GetDeckCurrentStockIndex();
		this.m_deckList = DeckUtil.GetDeckList();
		global::Debug.Log(string.Concat(new object[]
		{
			"GetCurrentDeck ",
			this.m_deckList.Count,
			"   ",
			this.m_currentDeck
		}));
		this.m_isEnd = false;
		this.m_page = 0;
		this.m_pageMax = 0;
		this.m_changeDelay = 0f;
		this.SetParam(true);
		this.SetObject(true);
		if (this.m_animation != null)
		{
			ActiveAnimation.Play(this.m_animation, "ui_mm_player_set_2_intro_Anim", Direction.Forward);
		}
		if (this.m_gameObjectList != null && this.m_gameObjectList.Count > 0)
		{
			foreach (GameObject gameObject in this.m_gameObjectList)
			{
				if (gameObject != null)
				{
					gameObject.SetActive(true);
				}
			}
		}
		this.SetSort(ServerPlayerState.CHARA_SORT.TEAM_ATTR);
	}

	// Token: 0x060024D9 RID: 9433 RVA: 0x000DC460 File Offset: 0x000DA660
	public bool UpdateView(bool rest = false)
	{
		this.m_currentDeck = DeckUtil.GetDeckCurrentStockIndex();
		this.m_deckList = DeckUtil.GetDeckList();
		this.SetParam(rest);
		this.SetObject(rest);
		return true;
	}

	// Token: 0x060024DA RID: 9434 RVA: 0x000DC488 File Offset: 0x000DA688
	public bool CheckSetMode(PlayerCharaList.SET_CHARA_MODE setMode, CharaType setCharaType = CharaType.UNKNOWN)
	{
		bool result = false;
		int deckCurrentStockIndex = DeckUtil.GetDeckCurrentStockIndex();
		List<DeckUtil.DeckSet> deckList = DeckUtil.GetDeckList();
		DeckUtil.DeckSet deckSet = deckList[deckCurrentStockIndex];
		switch (setMode)
		{
		case PlayerCharaList.SET_CHARA_MODE.MAIN:
			if (deckSet.charaMain != setCharaType)
			{
				result = true;
			}
			break;
		case PlayerCharaList.SET_CHARA_MODE.SUB:
			if (deckSet.charaSub != setCharaType)
			{
				result = (deckSet.charaMain != setCharaType || deckSet.charaSub != CharaType.UNKNOWN);
			}
			break;
		case PlayerCharaList.SET_CHARA_MODE.CHANGE:
			if (deckSet.charaSub != CharaType.UNKNOWN && deckSet.charaMain != CharaType.UNKNOWN)
			{
				result = true;
			}
			break;
		}
		return result;
	}

	// Token: 0x060024DB RID: 9435 RVA: 0x000DC52C File Offset: 0x000DA72C
	public bool SetChara(PlayerCharaList.SET_CHARA_MODE setMode, CharaType setCharaType = CharaType.UNKNOWN)
	{
		bool flag = false;
		if (GeneralUtil.IsNetwork())
		{
			if (setCharaType != CharaType.UNKNOWN || setMode == PlayerCharaList.SET_CHARA_MODE.CHANGE)
			{
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null)
				{
					int deckCurrentStockIndex = DeckUtil.GetDeckCurrentStockIndex();
					List<DeckUtil.DeckSet> deckList = DeckUtil.GetDeckList();
					DeckUtil.DeckSet deckSet = deckList[deckCurrentStockIndex];
					int num = -1;
					int num2 = -1;
					int num3 = -1;
					int num4 = -1;
					int num5 = -1;
					ServerCharacterState serverCharacterState = null;
					ServerCharacterState serverCharacterState2 = ServerInterface.PlayerState.CharacterState(deckSet.charaMain);
					ServerCharacterState serverCharacterState3 = ServerInterface.PlayerState.CharacterState(deckSet.charaSub);
					if (setCharaType != CharaType.UNKNOWN)
					{
						serverCharacterState = ServerInterface.PlayerState.CharacterState(setCharaType);
					}
					if (serverCharacterState != null)
					{
						num = serverCharacterState.Id;
					}
					if (serverCharacterState2 != null)
					{
						num2 = serverCharacterState2.Id;
						num4 = num2;
					}
					if (serverCharacterState3 != null)
					{
						num3 = serverCharacterState3.Id;
						num5 = num3;
					}
					switch (setMode)
					{
					case PlayerCharaList.SET_CHARA_MODE.MAIN:
						if (num >= 0)
						{
							if (num3 == num)
							{
								num5 = num4;
							}
							num4 = num;
							flag = true;
						}
						break;
					case PlayerCharaList.SET_CHARA_MODE.SUB:
						if (num >= 0)
						{
							if (num2 == num)
							{
								num4 = num5;
							}
							num5 = num;
							flag = true;
						}
						break;
					case PlayerCharaList.SET_CHARA_MODE.CHANGE:
						if (num3 >= 0)
						{
							num4 = num3;
							num5 = num2;
							flag = true;
						}
						break;
					}
					if (flag)
					{
						loggedInServerInterface.RequestServerChangeCharacter(num4, num5, base.gameObject);
					}
				}
			}
		}
		else
		{
			GeneralUtil.ShowNoCommunication("ShowNoCommunication");
		}
		return flag;
	}

	// Token: 0x060024DC RID: 9436 RVA: 0x000DC698 File Offset: 0x000DA898
	public bool SetSort(ServerPlayerState.CHARA_SORT sort)
	{
		bool result = false;
		if (this.m_sortType != sort)
		{
			this.m_sortType = sort;
			this.m_sortOffset = 0;
			result = true;
		}
		this.SetParam(false);
		this.SetObject(false);
		return result;
	}

	// Token: 0x060024DD RID: 9437 RVA: 0x000DC6D4 File Offset: 0x000DA8D4
	public bool SetSortDebug(ServerPlayerState.CHARA_SORT sort)
	{
		bool result = false;
		if (this.m_sortType == sort)
		{
			this.m_sortOffset++;
		}
		else
		{
			this.m_sortType = sort;
			this.m_sortOffset = 0;
			result = true;
		}
		this.SetParam(false);
		this.SetObject(false);
		return result;
	}

	// Token: 0x060024DE RID: 9438 RVA: 0x000DC724 File Offset: 0x000DA924
	public bool SetDeck(int stock)
	{
		bool result = false;
		if (GeneralUtil.IsNetwork())
		{
			if (stock >= 0 && this.m_currentDeck != stock && this.m_deckList != null && this.m_deckList.Count > stock)
			{
				this.DeckSetLoad(stock);
			}
			else
			{
				global::Debug.Log(string.Concat(new object[]
				{
					"SetDeck error   currentDeck:",
					this.m_currentDeck,
					"  stock:",
					stock
				}));
			}
		}
		else
		{
			GeneralUtil.ShowNoCommunication("ShowNoCommunication");
		}
		return result;
	}

	// Token: 0x060024DF RID: 9439 RVA: 0x000DC7C0 File Offset: 0x000DA9C0
	private void DeckSetLoad(int stock)
	{
		this.m_oldDeckSet = this.GetCurrentDeck();
		this.m_reqCharaMain = this.m_deckList[stock].charaMain;
		this.m_reqCharaSub = this.m_deckList[stock].charaSub;
		this.m_reqChaoMain = this.m_deckList[stock].chaoMain;
		this.m_reqChaoSub = this.m_deckList[stock].chaoSub;
		this.m_reqDeck = stock;
		DeckUtil.SetDeckCurrentStockIndex(this.m_reqDeck);
		if (this.m_oldDeckSet.charaMain == this.m_reqCharaMain && this.m_oldDeckSet.charaSub == this.m_reqCharaSub && this.m_oldDeckSet.chaoMain == this.m_reqChaoMain && this.m_oldDeckSet.chaoSub == this.m_reqChaoSub)
		{
			this.m_oldDeckSet = null;
			this.m_reqDeck = -1;
			this.m_reqCharaMain = CharaType.UNKNOWN;
			this.m_reqCharaSub = CharaType.UNKNOWN;
			this.m_reqChaoMain = -1;
			this.m_reqChaoSub = -1;
			this.UpdateView(true);
		}
		else
		{
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (this.m_oldDeckSet.chaoMain != this.m_reqChaoMain || this.m_oldDeckSet.chaoSub != this.m_reqChaoSub)
			{
				int id = (int)ServerItem.CreateFromChaoId(this.m_reqChaoMain).id;
				int id2 = (int)ServerItem.CreateFromChaoId(this.m_reqChaoSub).id;
				this.m_changeDelay = 0f;
				loggedInServerInterface.RequestServerEquipChao(id, id2, base.gameObject);
			}
			else
			{
				this.ServerEquipChao_Dummy();
			}
		}
	}

	// Token: 0x060024E0 RID: 9440 RVA: 0x000DC958 File Offset: 0x000DAB58
	private DeckUtil.DeckSet GetCurrentDeck()
	{
		DeckUtil.DeckSet result = null;
		if (this.m_deckList != null && this.m_deckList.Count > 0 && this.m_deckList.Count > this.m_currentDeck)
		{
			result = this.m_deckList[this.m_currentDeck];
		}
		return result;
	}

	// Token: 0x060024E1 RID: 9441 RVA: 0x000DC9AC File Offset: 0x000DABAC
	private void SetParam(bool reset)
	{
		if (reset && this.m_charaObjectList != null)
		{
			this.m_charaObjectList.Clear();
		}
		if (this.m_charaStateList != null)
		{
			this.m_charaStateList.Clear();
		}
		ServerPlayerState playerState = ServerInterface.PlayerState;
		this.m_charaStateList = playerState.GetCharacterStateList(this.m_sortType, false, this.m_sortOffset);
		this.m_pageMax = Mathf.CeilToInt((float)this.m_charaStateList.Count / 4f);
		GeneralUtil.SetButtonFunc(base.gameObject, "player_set_grip_R", base.gameObject, "OnClickPageNext");
		GeneralUtil.SetButtonFunc(base.gameObject, "Btn_player_set_grip_R", base.gameObject, "OnClickPageNext");
		GeneralUtil.SetButtonFunc(base.gameObject, "player_set_grip_L", base.gameObject, "OnClickPagePrev");
		GeneralUtil.SetButtonFunc(base.gameObject, "Btn_player_set_grip_L", base.gameObject, "OnClickPagePrev");
	}

	// Token: 0x060024E2 RID: 9442 RVA: 0x000DCA98 File Offset: 0x000DAC98
	private void SetObject(bool reset)
	{
		if (this.m_storage != null)
		{
			if (reset)
			{
				this.m_storage.maxItemCount = (this.m_storage.maxColumns = 0);
				this.m_storage.maxRows = 1;
				this.m_storage.Restart();
			}
			List<CharaType> list = new List<CharaType>();
			int num = 4 * this.m_page;
			int num2 = 0;
			if (this.m_charaStateList.Count > num)
			{
				num2 = this.m_charaStateList.Count - num;
				if (num2 > 4)
				{
					num2 = 4;
				}
			}
			this.m_storage.maxItemCount = (this.m_storage.maxColumns = num2);
			this.m_storage.Restart();
			this.m_charaObjectList = GameObjectUtil.FindChildGameObjectsComponents<ui_player_set_scroll>(this.m_storage.gameObject, "ui_player_set_scroll(Clone)");
			if (this.m_charaObjectList != null && this.m_charaObjectList.Count > 0)
			{
				ServerPlayerState playerState = ServerInterface.PlayerState;
				this.m_charaStateList = playerState.GetCharacterStateList(this.m_sortType, false, this.m_sortOffset);
				int num3 = 0;
				Dictionary<CharaType, ServerCharacterState>.KeyCollection keys = this.m_charaStateList.Keys;
				foreach (CharaType charaType in keys)
				{
					ServerCharacterState characterState = this.m_charaStateList[charaType];
					if (num3 >= num && num3 < num + num2 && this.m_charaObjectList.Count > num3 - num)
					{
						list.Add(charaType);
						this.m_charaObjectList[num3 - num].Setup(this, characterState);
					}
					num3++;
				}
			}
			if (list.Count > 0)
			{
				GeneralUtil.RemoveCharaTexture(list);
			}
		}
		DeckUtil.DeckSet currentDeck = this.GetCurrentDeck();
		if (this.m_charaDeckObject != null)
		{
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_charaDeckObject, "img_player_main");
			UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_charaDeckObject, "img_player_sub");
			UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_charaDeckObject, "img_decknum");
			if (uisprite != null)
			{
				uisprite.spriteName = "ui_tex_player_set_" + CharaTypeUtil.GetCharaSpriteNameSuffix(currentDeck.charaMain);
				uisprite.gameObject.SetActive(currentDeck.charaMain != CharaType.UNKNOWN);
			}
			if (uisprite2 != null)
			{
				uisprite2.spriteName = "ui_tex_player_set_" + CharaTypeUtil.GetCharaSpriteNameSuffix(currentDeck.charaSub);
				uisprite2.gameObject.SetActive(currentDeck.charaMain != CharaType.UNKNOWN);
			}
			if (uisprite3 != null)
			{
				uisprite3.spriteName = "ui_chao_set_deck_tab_" + (this.m_currentDeck + 1);
			}
			GeneralUtil.SetButtonFunc(this.m_charaDeckObject, "Btn_player_change", base.gameObject, "OnClickChange");
		}
		if (this.m_chaoDeckObject != null && reset)
		{
			UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(this.m_chaoDeckObject, "img_chao_main");
			UITexture uitexture2 = GameObjectUtil.FindChildGameObjectComponent<UITexture>(this.m_chaoDeckObject, "img_chao_sub");
			if (ChaoTextureManager.Instance != null)
			{
				if (uitexture != null)
				{
					uitexture.gameObject.SetActive(currentDeck.chaoMain >= 0);
					if (currentDeck.chaoMain >= 0)
					{
						ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(uitexture, null, true);
						ChaoTextureManager.Instance.GetTexture(currentDeck.chaoMain, info);
					}
				}
				if (uitexture2 != null)
				{
					uitexture2.gameObject.SetActive(currentDeck.chaoSub >= 0);
					if (currentDeck.chaoSub >= 0)
					{
						ChaoTextureManager.CallbackInfo info2 = new ChaoTextureManager.CallbackInfo(uitexture2, null, true);
						ChaoTextureManager.Instance.GetTexture(currentDeck.chaoSub, info2);
					}
				}
			}
			else
			{
				if (uitexture != null)
				{
					uitexture.gameObject.SetActive(false);
				}
				if (uitexture2 != null)
				{
					uitexture2.gameObject.SetActive(false);
				}
			}
			GeneralUtil.SetButtonFunc(base.gameObject, this.m_chaoDeckObject.name, base.gameObject, "OnClickChao");
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_page");
		if (uilabel != null)
		{
			uilabel.text = this.m_page + 1 + "/" + this.m_pageMax;
		}
		if (this.m_changeDelay <= 0f && reset)
		{
			this.m_changeBtn = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(base.gameObject, "Btn_player_change");
			if (this.m_changeBtn != null)
			{
				if (this.CheckSetMode(PlayerCharaList.SET_CHARA_MODE.CHANGE, CharaType.UNKNOWN))
				{
					this.m_changeBtn.isEnabled = true;
					this.m_changeDelay = 1.5f;
				}
				else
				{
					this.m_changeBtn.isEnabled = false;
					this.m_changeDelay = -1f;
				}
			}
		}
		GeneralUtil.SetCharasetBtnIcon(base.gameObject, "Btn_charaset");
		GeneralUtil.SetButtonFunc(base.gameObject, "Btn_charaset", base.gameObject, "OnClickDeck");
	}

	// Token: 0x060024E3 RID: 9443 RVA: 0x000DCFC8 File Offset: 0x000DB1C8
	private void OnMsgDeckViewWindowChange()
	{
		this.UpdateView(true);
	}

	// Token: 0x060024E4 RID: 9444 RVA: 0x000DCFD4 File Offset: 0x000DB1D4
	private void OnMsgDeckViewWindowNotChange()
	{
		this.UpdateView(true);
	}

	// Token: 0x060024E5 RID: 9445 RVA: 0x000DCFE0 File Offset: 0x000DB1E0
	private void OnMsgDeckViewWindowNetworkError()
	{
		this.UpdateView(false);
	}

	// Token: 0x060024E6 RID: 9446 RVA: 0x000DCFEC File Offset: 0x000DB1EC
	private void OnClickBack()
	{
		if (base.gameObject.activeSelf && !this.m_isEnd)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			this.m_isEnd = true;
		}
	}

	// Token: 0x060024E7 RID: 9447 RVA: 0x000DD02C File Offset: 0x000DB22C
	private void OnClickDeck()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		DeckViewWindow.Create(base.gameObject);
	}

	// Token: 0x060024E8 RID: 9448 RVA: 0x000DD04C File Offset: 0x000DB24C
	private void OnClickChao()
	{
		HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.CHAO, true);
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x060024E9 RID: 9449 RVA: 0x000DD068 File Offset: 0x000DB268
	private void OnClickChange()
	{
		this.SetChara(PlayerCharaList.SET_CHARA_MODE.CHANGE, CharaType.UNKNOWN);
		if (this.m_changeBtn != null)
		{
			this.m_changeBtn.isEnabled = false;
			this.m_changeDelay = 1.5f;
		}
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x060024EA RID: 9450 RVA: 0x000DD0B8 File Offset: 0x000DB2B8
	private void OnClickPageNext()
	{
		if (this.m_pageMax > 0)
		{
			this.m_page = (this.m_page + 1 + this.m_pageMax) % this.m_pageMax;
			this.SetObject(false);
			SoundManager.SePlay("sys_menu_decide", "SE");
		}
	}

	// Token: 0x060024EB RID: 9451 RVA: 0x000DD104 File Offset: 0x000DB304
	private void OnClickPagePrev()
	{
		if (this.m_pageMax > 0)
		{
			this.m_page = (this.m_page - 1 + this.m_pageMax) % this.m_pageMax;
			this.SetObject(false);
			SoundManager.SePlay("sys_menu_decide", "SE");
		}
	}

	// Token: 0x060024EC RID: 9452 RVA: 0x000DD150 File Offset: 0x000DB350
	public void OnClosedWindowAnim()
	{
		if (this.m_isEnd)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060024ED RID: 9453 RVA: 0x000DD16C File Offset: 0x000DB36C
	public void OnClickBackButton()
	{
		if (base.gameObject.activeSelf && !this.m_isEnd)
		{
			this.m_isEnd = true;
			SoundManager.SePlay("sys_menu_decide", "SE");
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_mm_player_set_2_intro_Anim", Direction.Reverse);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnClosedWindowAnim), true);
			}
		}
	}

	// Token: 0x060024EE RID: 9454 RVA: 0x000DD1E4 File Offset: 0x000DB3E4
	private void ServerChangeCharacter_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		this.m_oldDeckSet = null;
		this.m_reqDeck = -1;
		this.m_reqCharaMain = CharaType.UNKNOWN;
		this.m_reqCharaSub = CharaType.UNKNOWN;
		this.m_reqChaoMain = -1;
		this.m_reqChaoSub = -1;
		this.UpdateView(true);
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
	}

	// Token: 0x060024EF RID: 9455 RVA: 0x000DD220 File Offset: 0x000DB420
	private void ServerChangeCharacter_Dummy()
	{
		this.m_oldDeckSet = null;
		this.m_reqDeck = -1;
		this.m_reqCharaMain = CharaType.UNKNOWN;
		this.m_reqCharaSub = CharaType.UNKNOWN;
		this.m_reqChaoMain = -1;
		this.m_reqChaoSub = -1;
		this.UpdateView(true);
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
	}

	// Token: 0x060024F0 RID: 9456 RVA: 0x000DD25C File Offset: 0x000DB45C
	private void ServerEquipChao_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		bool flag = false;
		if (loggedInServerInterface != null)
		{
			if (this.m_oldDeckSet.charaMain != CharaType.UNKNOWN && this.m_oldDeckSet.charaMain != this.m_reqCharaMain)
			{
				flag = true;
			}
			if (this.m_oldDeckSet.charaSub != this.m_reqCharaSub)
			{
				flag = true;
			}
			if (flag)
			{
				int mainCharaId = -1;
				int subCharaId = -1;
				ServerCharacterState serverCharacterState = ServerInterface.PlayerState.CharacterState(this.m_reqCharaMain);
				ServerCharacterState serverCharacterState2 = ServerInterface.PlayerState.CharacterState(this.m_reqCharaSub);
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

	// Token: 0x060024F1 RID: 9457 RVA: 0x000DD324 File Offset: 0x000DB524
	private void ServerEquipChao_Dummy()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		bool flag = false;
		if (loggedInServerInterface != null)
		{
			if (this.m_oldDeckSet.charaMain != CharaType.UNKNOWN && this.m_oldDeckSet.charaMain != this.m_reqCharaMain)
			{
				flag = true;
			}
			if (this.m_oldDeckSet.charaSub != this.m_reqCharaSub)
			{
				flag = true;
			}
			if (flag)
			{
				int mainCharaId = -1;
				int subCharaId = -1;
				ServerCharacterState serverCharacterState = ServerInterface.PlayerState.CharacterState(this.m_reqCharaMain);
				ServerCharacterState serverCharacterState2 = ServerInterface.PlayerState.CharacterState(this.m_reqCharaSub);
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

	// Token: 0x060024F2 RID: 9458 RVA: 0x000DD3EC File Offset: 0x000DB5EC
	public static void DebugShowPlayerCharaListGui()
	{
		if (PlayerCharaList.s_isActive && PlayerCharaList.s_instance != null && PlayerCharaList.s_instance.gameObject.activeSelf && !PlayerLvupWindow.isActive && !PlayerSetWindowUI.isActive && !DeckViewWindow.isActive && !GeneralWindow.Created)
		{
			Rect rect = new Rect(170f, -5f, 150f, 90f);
			Rect rect2 = SingletonGameObject<DebugGameObject>.Instance.CreateGuiRect(rect, DebugGameObject.GUI_RECT_ANCHOR.CENTER_BOTTOM);
			GUI.Box(rect2, string.Concat(new object[]
			{
				"sort:",
				PlayerCharaList.s_instance.sortType,
				"  [",
				PlayerCharaList.s_instance.sortOffset,
				"]"
			}));
			int num = 3;
			float num2 = 0.25f;
			float num3 = (1f - num2) / (float)num;
			for (int i = 0; i < num; i++)
			{
				Rect rect3 = new Rect(0f, num2 + num3 * (float)i, 0.95f, num3 * 0.95f);
				Rect position = SingletonGameObject<DebugGameObject>.Instance.CreateGuiRectInRate(rect2, rect3, DebugGameObject.GUI_RECT_ANCHOR.CENTER_TOP);
				ServerPlayerState.CHARA_SORT chara_SORT = (ServerPlayerState.CHARA_SORT)i;
				if (GUI.Button(position, string.Empty + chara_SORT))
				{
					PlayerCharaList.s_instance.SetSortDebug(chara_SORT);
				}
			}
		}
	}

	// Token: 0x040020FF RID: 8447
	private const int CHARA_DRAW_MAX = 4;

	// Token: 0x04002100 RID: 8448
	private const float CHANGE_BTN_DELAY = 1.5f;

	// Token: 0x04002101 RID: 8449
	[SerializeField]
	private Animation m_animation;

	// Token: 0x04002102 RID: 8450
	[SerializeField]
	private UIRectItemStorage m_storage;

	// Token: 0x04002103 RID: 8451
	[SerializeField]
	private GameObject m_charaDeckObject;

	// Token: 0x04002104 RID: 8452
	[SerializeField]
	private GameObject m_chaoDeckObject;

	// Token: 0x04002105 RID: 8453
	[SerializeField]
	private List<GameObject> m_gameObjectList;

	// Token: 0x04002106 RID: 8454
	private static bool s_isActive;

	// Token: 0x04002107 RID: 8455
	private static PlayerCharaList s_instance;

	// Token: 0x04002108 RID: 8456
	private ServerPlayerState.CHARA_SORT m_sortType;

	// Token: 0x04002109 RID: 8457
	private int m_sortOffset;

	// Token: 0x0400210A RID: 8458
	private int m_page;

	// Token: 0x0400210B RID: 8459
	private int m_pageMax;

	// Token: 0x0400210C RID: 8460
	private float m_changeDelay;

	// Token: 0x0400210D RID: 8461
	private UIImageButton m_changeBtn;

	// Token: 0x0400210E RID: 8462
	private List<ui_player_set_scroll> m_charaObjectList;

	// Token: 0x0400210F RID: 8463
	private Dictionary<CharaType, ServerCharacterState> m_charaStateList;

	// Token: 0x04002110 RID: 8464
	private int m_currentDeck;

	// Token: 0x04002111 RID: 8465
	private List<DeckUtil.DeckSet> m_deckList;

	// Token: 0x04002112 RID: 8466
	private bool m_isEnd;

	// Token: 0x04002113 RID: 8467
	private DeckUtil.DeckSet m_oldDeckSet;

	// Token: 0x04002114 RID: 8468
	private bool m_tutorial;

	// Token: 0x04002115 RID: 8469
	private bool m_pickup;

	// Token: 0x04002116 RID: 8470
	private int m_reqDeck;

	// Token: 0x04002117 RID: 8471
	private CharaType m_reqCharaMain = CharaType.UNKNOWN;

	// Token: 0x04002118 RID: 8472
	private CharaType m_reqCharaSub = CharaType.UNKNOWN;

	// Token: 0x04002119 RID: 8473
	private int m_reqChaoMain = -1;

	// Token: 0x0400211A RID: 8474
	private int m_reqChaoSub = -1;

	// Token: 0x020004DA RID: 1242
	public enum SET_CHARA_MODE
	{
		// Token: 0x0400211C RID: 8476
		MAIN,
		// Token: 0x0400211D RID: 8477
		SUB,
		// Token: 0x0400211E RID: 8478
		CHANGE
	}
}
