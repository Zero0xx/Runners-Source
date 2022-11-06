using System;
using System.Collections;
using System.Collections.Generic;
using DataTable;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x020003EF RID: 1007
public class ChaoSetUI : MonoBehaviour
{
	// Token: 0x1700044D RID: 1101
	// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x000AF6A4 File Offset: 0x000AD8A4
	public bool IsEndSetup
	{
		get
		{
			return this.m_endSetUp;
		}
	}

	// Token: 0x06001DE3 RID: 7651 RVA: 0x000AF6AC File Offset: 0x000AD8AC
	private void Update()
	{
		if (this.m_tutorial && GeneralWindow.IsCreated("ChaoTutorial") && GeneralWindow.IsButtonPressed)
		{
			GeneralWindow.Close();
			TutorialCursor.StartTutorialCursor(TutorialCursor.Type.BACK);
		}
		if (GeneralWindow.IsCreated("ChaoCantSet") && GeneralWindow.IsButtonPressed)
		{
			GeneralWindow.Close();
		}
		if (this.m_sortDelay > 0f)
		{
			this.m_sortDelay -= Time.deltaTime;
			if (this.m_sortDelay <= 0f)
			{
				this.m_sortDelay = 0f;
				if (this.m_sortLeveUpBC != null)
				{
					this.m_sortLeveUpBC.enabled = true;
				}
				if (this.m_sortRareUpBC != null)
				{
					this.m_sortRareUpBC.enabled = true;
				}
			}
		}
		if (this.m_initDelay > 0f)
		{
			this.m_initDelay -= Time.deltaTime;
			if (this.m_initDelay < 0.5f)
			{
				this.m_endSetUp = true;
			}
			if (this.m_initDelay <= 0f)
			{
				this.m_endSetUp = true;
				this.m_initDelay = 0f;
				HudMenuUtility.SetConnectAlertSimpleUI(false);
			}
		}
	}

	// Token: 0x06001DE4 RID: 7652 RVA: 0x000AF7E4 File Offset: 0x000AD9E4
	private void OnStartChaoSet()
	{
		HudMenuUtility.SetConnectAlertSimpleUI(true);
		this.m_initDelay = 0f;
		this.m_currentDeckSetStock = DeckUtil.GetDeckCurrentStockIndex();
		this.m_endSetUp = false;
		this.m_tutorial = ChaoSetUI.IsChaoTutorial();
		if (this.m_tutorial)
		{
			ChaoSetUI.SaveDataInterface.MainChaoId = -1;
			ChaoSetUI.SaveDataInterface.SubChaoId = -1;
		}
		if (this.isDebugRondomSetChao)
		{
			SaveDataManager instance = SaveDataManager.Instance;
			if (instance != null)
			{
				DataTable.ChaoData[] dataTable = ChaoTable.GetDataTable();
				if (dataTable != null)
				{
					instance.ChaoData.Info = new SaveData.ChaoData.ChaoDataInfo[dataTable.Length];
					instance.PlayerData.MainChaoID = -1;
					instance.PlayerData.SubChaoID = -1;
					for (int i = 0; i < dataTable.Length; i++)
					{
						int id = dataTable[i].id;
						int num = UnityEngine.Random.Range(-1, 11);
						instance.ChaoData.Info[i].chao_id = id;
						instance.ChaoData.Info[i].level = num;
						if (instance.PlayerData.MainChaoID == -1 && num != -1)
						{
							instance.PlayerData.MainChaoID = id;
						}
					}
				}
			}
		}
		this.m_rouletteButtonUI.Setup();
		this.m_stageAbilityManager = GameObjectUtil.FindGameObjectComponent<StageAbilityManager>("StageAbilityManager");
		if (this.m_stageAbilityManager != null)
		{
			this.m_stageAbilityManager.RecalcAbilityVaue();
		}
		this.m_slot = GameObjectUtil.FindChildGameObject(base.gameObject, "slot");
		this.m_uiRectItemStorage = this.m_slot.GetComponent<UIRectItemStorage>();
		this.m_uiDraggablePanel = GameObjectUtil.FindChildGameObjectComponent<UIDraggablePanel>(base.gameObject, "chao_set_contents");
		this.SetRouletteButton();
		GeneralUtil.SetCharasetBtnIcon(base.gameObject, "Btn_charaset");
		GeneralUtil.SetButtonFunc(base.gameObject, "Btn_charaset", base.gameObject, "OnClickDeck");
		UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "Btn_player");
		if (uibuttonMessage != null)
		{
			uibuttonMessage.target = base.gameObject;
			uibuttonMessage.functionName = "GoToCharacterButtonClicked";
		}
		base.StartCoroutine(this.InitView());
	}

	// Token: 0x06001DE5 RID: 7653 RVA: 0x000AF9F4 File Offset: 0x000ADBF4
	private IEnumerator InitView()
	{
		while (GameObjectUtil.FindChildGameObjects(this.m_slot, "ui_chao_set_cell(Clone)").Count == 0)
		{
			yield return null;
		}
		this.m_currentDeckSetStock = DeckUtil.GetDeckCurrentStockIndex();
		this.SetupTabView();
		this.m_uiRectItemStorage.maxColumns = 4;
		int maxChaoCount = 0;
		if (ServerInterface.PlayerState != null && ServerInterface.PlayerState.ChaoStates != null)
		{
			maxChaoCount = ServerInterface.PlayerState.ChaoStates.Count;
		}
		if (maxChaoCount == 0)
		{
			maxChaoCount = ChaoTable.GetDataTable().Length;
		}
		this.m_uiRectItemStorage.maxRows = (maxChaoCount + this.m_uiRectItemStorage.maxColumns - 1) / this.m_uiRectItemStorage.maxColumns;
		this.m_uiRectItemStorage.maxItemCount = maxChaoCount;
		this.m_uiRectItemStorage.Restart();
		this.m_uiRectItemStorage.Strip();
		if (this.m_initEnd)
		{
			this.ChaoSortUpadate(this.m_chaoSort, this.m_descendingOrder, DataTable.ChaoData.Rarity.NONE);
		}
		else
		{
			this.m_mask0 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_mask_0_bg");
			this.m_mask1 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_mask_1_bg");
			this.m_sortLeveUpBC = GameObjectUtil.FindChildGameObjectComponent<BoxCollider>(base.gameObject, "sort_1");
			this.m_sortRareUpBC = GameObjectUtil.FindChildGameObjectComponent<BoxCollider>(base.gameObject, "sort_0");
			this.m_mask0.alpha = 0f;
			this.m_mask1.alpha = 1f;
			this.m_sortLeveUp.alpha = 0f;
			this.m_sortRareUp.alpha = 0f;
			this.m_chaoSort = ChaoSort.RARE;
			this.m_descendingOrder = false;
			if (this.m_tutorial)
			{
				this.m_mask0.alpha = 1f;
				this.m_mask1.alpha = 0f;
				this.m_sortLeveUp.alpha = 1f;
				this.m_sortRareUp.alpha = 1f;
				this.m_chaoSort = ChaoSort.LEVEL;
				this.m_descendingOrder = true;
				this.ChaoSortUpadate(this.m_chaoSort, this.m_descendingOrder, DataTable.ChaoData.Rarity.NONE);
			}
			else
			{
				SystemSaveManager systemSaveManager = SystemSaveManager.Instance;
				if (systemSaveManager != null)
				{
					SystemData saveData = systemSaveManager.GetSystemdata();
					if (saveData != null)
					{
						this.m_chaoSort = (ChaoSort)saveData.chaoSortType01;
						this.m_descendingOrder = (saveData.chaoSortType02 > 0);
						if (this.m_descendingOrder)
						{
							this.m_sortLeveUp.alpha = 1f;
							this.m_sortRareUp.alpha = 1f;
						}
						if (this.m_chaoSort == ChaoSort.LEVEL)
						{
							this.m_mask0.alpha = 1f;
							this.m_mask1.alpha = 0f;
						}
					}
				}
				this.ChaoSortUpadate(this.m_chaoSort, this.m_descendingOrder, DataTable.ChaoData.Rarity.NONE);
			}
			this.m_initEnd = true;
		}
		if (this.m_sortLeveUpBC != null)
		{
			this.m_sortLeveUpBC.enabled = true;
		}
		if (this.m_sortRareUpBC != null)
		{
			this.m_sortRareUpBC.enabled = true;
		}
		this.m_sortDelay = 0f;
		this.m_initDelay = 0.2f;
		this.UpdateView();
		yield break;
	}

	// Token: 0x06001DE6 RID: 7654 RVA: 0x000AFA10 File Offset: 0x000ADC10
	private void SetRouletteButton()
	{
		HudRouletteButtonUtil.SetSpecialEggIcon(this.m_specialEggIconObj);
		HudRouletteButtonUtil.SetFreeSpin(this.m_freeSpinIconObj, this.m_freeSpinCountLabel, false);
		HudRouletteButtonUtil.SetSaleIcon(this.m_saleIconObj);
		HudRouletteButtonUtil.SetEventIcon(this.m_eventIconObj);
	}

	// Token: 0x06001DE7 RID: 7655 RVA: 0x000AFA48 File Offset: 0x000ADC48
	private void SetupTabView()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "window_chao_tab");
		if (gameObject != null)
		{
			if (this.m_deckObjects != null)
			{
				this.m_deckObjects.Clear();
			}
			this.m_deckObjects = new List<GameObject>();
			for (int i = 0; i < 10; i++)
			{
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "tab_" + (i + 1));
				if (!(gameObject2 != null))
				{
					break;
				}
				this.m_deckObjects.Add(gameObject2);
			}
			if (this.m_deckObjects.Count > 0 && this.m_deckObjects.Count > this.m_currentDeckSetStock)
			{
				for (int j = 0; j < this.m_deckObjects.Count; j++)
				{
					this.m_deckObjects[j].SetActive(this.m_currentDeckSetStock == j);
				}
			}
		}
	}

	// Token: 0x06001DE8 RID: 7656 RVA: 0x000AFB40 File Offset: 0x000ADD40
	private void UpdateView()
	{
		base.StartCoroutine(this.UpdateViewCoroutine());
	}

	// Token: 0x06001DE9 RID: 7657 RVA: 0x000AFB50 File Offset: 0x000ADD50
	private IEnumerator UpdateViewCoroutine()
	{
		this.m_cells = GameObjectUtil.FindChildGameObjects(this.m_slot, "ui_chao_set_cell(Clone)");
		if (this.m_cells.Count != this.m_uiRectItemStorage.maxItemCount)
		{
			yield return null;
		}
		if (this.m_stageAbilityManager != null)
		{
			this.m_getChaoCountLabel.text = this.m_stageAbilityManager.GetChaoCount().ToString();
			this.m_getChaoCountShadowLabel.text = this.m_stageAbilityManager.GetChaoCount().ToString();
			int chaoCountIndex;
			for (chaoCountIndex = 0; chaoCountIndex < this.m_chaoCountNumber.Length; chaoCountIndex++)
			{
				if (this.m_stageAbilityManager.GetChaoCount() < this.m_chaoCountNumber[chaoCountIndex])
				{
					break;
				}
			}
			this.m_getChaoSprite.spriteName = "ui_chao_set_dec_" + chaoCountIndex;
			this.m_getChaoBonusLabel.text = HudUtility.GetChaoCountBonusText(this.m_stageAbilityManager.GetChaoCountBonusValue());
		}
		this.RegistChao(0, ChaoSetUI.SaveDataInterface.MainChaoId);
		this.RegistChao(1, ChaoSetUI.SaveDataInterface.SubChaoId);
		this.UpdateRegistedChaoView();
		this.UpdateGotChaoView();
		if (this.m_tutorial)
		{
			TutorialCursor.StartTutorialCursor(TutorialCursor.Type.CHAOSELECT_CHAO);
		}
		yield break;
	}

	// Token: 0x06001DEA RID: 7658 RVA: 0x000AFB6C File Offset: 0x000ADD6C
	private void SetCellChao(int cellIndex, DataTable.ChaoData chaoData, int mainChaoId, int subChaoId)
	{
		if (this.m_chaoDatas != null)
		{
			ui_chao_set_cell[] componentsInChildren = base.gameObject.GetComponentsInChildren<ui_chao_set_cell>(true);
			if (cellIndex >= 0 && cellIndex < componentsInChildren.Length)
			{
				if (this.m_uiDraggablePanel == null)
				{
					this.m_uiDraggablePanel = GameObjectUtil.FindChildGameObjectComponent<UIDraggablePanel>(base.gameObject, "chao_set_contents");
				}
				if (this.m_uiDraggablePanel != null)
				{
					componentsInChildren[cellIndex].UpdateView(chaoData.id, mainChaoId, subChaoId, this.m_uiDraggablePanel.panel);
				}
			}
		}
	}

	// Token: 0x06001DEB RID: 7659 RVA: 0x000AFBF8 File Offset: 0x000ADDF8
	public void RegistChao(int chaoMainSubIndex, int chaoId)
	{
		base.enabled = true;
		int num = -1;
		int num2 = -1;
		bool flag = false;
		bool flag2 = false;
		if (chaoMainSubIndex == 0)
		{
			if (ChaoSetUI.SaveDataInterface.SubChaoId == chaoId)
			{
				num2 = ChaoSetUI.SaveDataInterface.MainChaoId;
				flag2 = true;
			}
			if (ChaoSetUI.SaveDataInterface.MainChaoId != chaoId)
			{
				num = chaoId;
				flag = true;
			}
		}
		else
		{
			if (ChaoSetUI.SaveDataInterface.MainChaoId == chaoId)
			{
				num = ChaoSetUI.SaveDataInterface.SubChaoId;
				flag = true;
			}
			if (ChaoSetUI.SaveDataInterface.SubChaoId != chaoId)
			{
				num2 = chaoId;
				flag2 = true;
			}
		}
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null && this.m_initDelay <= 0f)
		{
			if (flag || flag2)
			{
				loggedInServerInterface.RequestServerEquipChao((int)ServerItem.CreateFromChaoId((!flag) ? ChaoSetUI.SaveDataInterface.MainChaoId : num).id, (int)ServerItem.CreateFromChaoId((!flag2) ? ChaoSetUI.SaveDataInterface.SubChaoId : num2).id, base.gameObject);
			}
		}
		else
		{
			if (flag)
			{
				ChaoSetUI.SaveDataInterface.MainChaoId = num;
			}
			if (flag2)
			{
				ChaoSetUI.SaveDataInterface.SubChaoId = num2;
			}
			if (flag || flag2)
			{
				this.UpdateRegistedChaoView();
				this.UpdateGotChaoView();
			}
		}
	}

	// Token: 0x06001DEC RID: 7660 RVA: 0x000AFD14 File Offset: 0x000ADF14
	private void ServerEquipChao_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		global::Debug.Log(string.Concat(new object[]
		{
			"ServerEquipChao_Succeeded  mainCharaId:",
			msg.m_playerState.m_mainCharaId,
			"  subCharaId:",
			msg.m_playerState.m_subCharaId
		}));
		NetUtil.SyncSaveDataAndDataBase(msg.m_playerState);
		this.UpdateRegistedChaoView();
		this.UpdateGotChaoView();
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
	}

	// Token: 0x06001DED RID: 7661 RVA: 0x000AFD84 File Offset: 0x000ADF84
	private void ServerEquipChao_Failed(MsgServerConnctFailed msg)
	{
	}

	// Token: 0x06001DEE RID: 7662 RVA: 0x000AFD88 File Offset: 0x000ADF88
	private void ServerEquipChao_Dummy()
	{
		this.UpdateRegistedChaoView();
		this.UpdateGotChaoView();
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
	}

	// Token: 0x06001DEF RID: 7663 RVA: 0x000AFD9C File Offset: 0x000ADF9C
	private void UpdateRegistedChaoView()
	{
		GeneralUtil.SetCharasetBtnIcon(base.gameObject, "Btn_charaset");
		this.UpdateRegistedChaoView(0, ChaoTable.GetChaoData(ChaoSetUI.SaveDataInterface.MainChaoId));
		this.UpdateRegistedChaoView(1, ChaoTable.GetChaoData(ChaoSetUI.SaveDataInterface.SubChaoId));
	}

	// Token: 0x06001DF0 RID: 7664 RVA: 0x000AFDDC File Offset: 0x000ADFDC
	public void UpdateRegistedChaoView(int chaoMainSubIndex, DataTable.ChaoData chaoData)
	{
		ChaoSetUI.ChaoSerializeFields chaoSerializeFields = this.m_chaosSerializeFields[chaoMainSubIndex];
		if (chaoData != null && chaoData.IsValidate)
		{
			ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(chaoSerializeFields.m_chaoTexture, null, true);
			ChaoTextureManager.Instance.GetTexture(chaoData.id, info);
			chaoSerializeFields.m_chaoTexture.enabled = true;
			chaoSerializeFields.m_chaoSprite.enabled = false;
			chaoSerializeFields.m_chaoRankSprite.spriteName = "ui_chao_set_bg_l_" + (int)chaoData.rarity;
			chaoSerializeFields.m_chaoNameLabel.text = chaoData.nameTwolines;
			chaoSerializeFields.m_chaoLevelLabel.text = TextUtility.GetTextLevel(chaoData.level.ToString());
			string str = chaoData.charaAtribute.ToString().ToLower();
			chaoSerializeFields.m_chaoTypeSprite.spriteName = "ui_chao_set_type_icon_" + str;
			chaoSerializeFields.m_bonusLabel.gameObject.SetActive(false);
			chaoSerializeFields.m_bonusTypeSprite.gameObject.SetActive(false);
		}
		else
		{
			chaoSerializeFields.m_chaoTexture.enabled = false;
			chaoSerializeFields.m_chaoSprite.enabled = true;
			chaoSerializeFields.m_chaoRankSprite.spriteName = "ui_chao_set_bg_l_3";
			chaoSerializeFields.m_chaoNameLabel.text = string.Empty;
			chaoSerializeFields.m_chaoLevelLabel.text = string.Empty;
			chaoSerializeFields.m_chaoTypeSprite.spriteName = null;
			chaoSerializeFields.m_bonusTypeSprite.spriteName = null;
			chaoSerializeFields.m_bonusLabel.text = string.Empty;
		}
	}

	// Token: 0x06001DF1 RID: 7665 RVA: 0x000AFF4C File Offset: 0x000AE14C
	private void UpdateGotChaoView()
	{
		int num = 0;
		if (this.m_chaoDatas != null && this.m_chaoDatas.Count > 0 && ServerInterface.PlayerState != null && ServerInterface.PlayerState.ChaoStates != null && ServerInterface.PlayerState.ChaoStates.Count > 0)
		{
			foreach (DataTable.ChaoData chaoData in this.m_chaoDatas)
			{
				int num2 = chaoData.id + 400000;
				foreach (ServerChaoState serverChaoState in ServerInterface.PlayerState.ChaoStates)
				{
					if (num2 == serverChaoState.Id)
					{
						this.SetCellChao(num, chaoData, ChaoSetUI.SaveDataInterface.MainChaoId, ChaoSetUI.SaveDataInterface.SubChaoId);
						num++;
						break;
					}
				}
			}
		}
		else
		{
			this.ChaoSortUpadate(ChaoSort.RARE, false, DataTable.ChaoData.Rarity.NONE);
			if (this.m_chaoDatas != null)
			{
				foreach (DataTable.ChaoData chaoData2 in this.m_chaoDatas)
				{
					this.SetCellChao(num, chaoData2, ChaoSetUI.SaveDataInterface.MainChaoId, ChaoSetUI.SaveDataInterface.SubChaoId);
					num++;
				}
			}
		}
	}

	// Token: 0x06001DF2 RID: 7666 RVA: 0x000B0104 File Offset: 0x000AE304
	private void ChaoSortUpadate(ChaoSort sort, bool down = false, DataTable.ChaoData.Rarity exclusion = DataTable.ChaoData.Rarity.NONE)
	{
		DataTable.ChaoData[] dataTable = ChaoTable.GetDataTable();
		ChaoDataSorting chaoDataSorting = new ChaoDataSorting(sort);
		if (chaoDataSorting != null)
		{
			ChaoDataVisitorBase visitor = chaoDataSorting.visitor;
			if (visitor != null)
			{
				if (this.m_lastSort != sort)
				{
					this.m_lastSort = sort;
					this.m_sortCount = 0;
				}
				else
				{
					this.m_sortCount++;
				}
				foreach (DataTable.ChaoData chaoData in dataTable)
				{
					chaoData.accept(ref visitor);
				}
				switch (sort)
				{
				case ChaoSort.RARE:
				case ChaoSort.LEVEL:
					this.m_chaoDatas = chaoDataSorting.GetChaoListAll(down, exclusion);
					break;
				case ChaoSort.ATTRIBUTE:
				case ChaoSort.ABILITY:
				case ChaoSort.EVENT:
					this.m_chaoDatas = chaoDataSorting.GetChaoListAllOffset(this.m_sortCount, false, DataTable.ChaoData.Rarity.NONE);
					break;
				}
			}
		}
	}

	// Token: 0x06001DF3 RID: 7667 RVA: 0x000B01D8 File Offset: 0x000AE3D8
	private void OnClickDeck()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		DeckViewWindow.Create(base.gameObject);
	}

	// Token: 0x06001DF4 RID: 7668 RVA: 0x000B01F8 File Offset: 0x000AE3F8
	private void OnMsgDeckViewWindowChange()
	{
		this.OnStartChaoSet();
		global::Debug.Log("ChaoSetUI OnMsgDeckViewWindowChange!");
	}

	// Token: 0x06001DF5 RID: 7669 RVA: 0x000B020C File Offset: 0x000AE40C
	private void OnMsgDeckViewWindowNotChange()
	{
		this.OnStartChaoSet();
		global::Debug.Log("ChaoSetUI OnMsgDeckViewWindowNotChange!");
	}

	// Token: 0x06001DF6 RID: 7670 RVA: 0x000B0220 File Offset: 0x000AE420
	private void OnMsgDeckViewWindowNetworkError()
	{
		this.OnStartChaoSet();
		global::Debug.Log("ChaoSetUI OnMsgDeckViewWindowNetworkError!");
	}

	// Token: 0x06001DF7 RID: 7671 RVA: 0x000B0234 File Offset: 0x000AE434
	public void OnPressSortLevel()
	{
		if (this.m_sortLeveUpBC != null)
		{
			this.m_sortLeveUpBC.enabled = false;
		}
		if (this.m_sortRareUpBC != null)
		{
			this.m_sortRareUpBC.enabled = false;
		}
		this.m_sortDelay = 0.4f;
		if (this.m_chaoSort != ChaoSort.LEVEL)
		{
			this.m_descendingOrder = (this.m_sortLeveUp.alpha >= 0.9f);
			this.m_chaoSort = ChaoSort.LEVEL;
		}
		else
		{
			if (this.m_sortLeveUp.alpha >= 0.9f)
			{
				this.m_sortLeveUp.alpha = 0f;
			}
			else
			{
				this.m_sortLeveUp.alpha = 1f;
			}
			this.m_descendingOrder = !this.m_descendingOrder;
		}
		this.m_mask0.alpha = 1f;
		this.m_mask1.alpha = 0f;
		this.ChaoSortUpadate(this.m_chaoSort, this.m_descendingOrder, DataTable.ChaoData.Rarity.NONE);
		this.UpdateGotChaoView();
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x06001DF8 RID: 7672 RVA: 0x000B034C File Offset: 0x000AE54C
	public void OnPressSortRare()
	{
		if (this.m_sortLeveUpBC != null)
		{
			this.m_sortLeveUpBC.enabled = false;
		}
		if (this.m_sortRareUpBC != null)
		{
			this.m_sortRareUpBC.enabled = false;
		}
		this.m_sortDelay = 0.4f;
		if (this.m_chaoSort != ChaoSort.RARE)
		{
			this.m_descendingOrder = (this.m_sortRareUp.alpha >= 0.9f);
			this.m_chaoSort = ChaoSort.RARE;
		}
		else
		{
			if (this.m_sortRareUp.alpha >= 0.9f)
			{
				this.m_sortRareUp.alpha = 0f;
			}
			else
			{
				this.m_sortRareUp.alpha = 1f;
			}
			this.m_descendingOrder = !this.m_descendingOrder;
		}
		this.m_mask0.alpha = 0f;
		this.m_mask1.alpha = 1f;
		this.ChaoSortUpadate(this.m_chaoSort, this.m_descendingOrder, DataTable.ChaoData.Rarity.NONE);
		this.UpdateGotChaoView();
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x06001DF9 RID: 7673 RVA: 0x000B0464 File Offset: 0x000AE664
	private void OnSetChaoTexture(ChaoTextureManager.TextureData data)
	{
		if (ChaoSetUI.SaveDataInterface.MainChaoId == data.chao_id)
		{
			this.m_chaosSerializeFields[0].m_chaoTexture.enabled = true;
			this.m_chaosSerializeFields[0].m_chaoTexture.mainTexture = data.tex;
		}
		else if (ChaoSetUI.SaveDataInterface.SubChaoId == data.chao_id)
		{
			this.m_chaosSerializeFields[1].m_chaoTexture.enabled = true;
			this.m_chaosSerializeFields[1].m_chaoTexture.mainTexture = data.tex;
		}
	}

	// Token: 0x06001DFA RID: 7674 RVA: 0x000B04EC File Offset: 0x000AE6EC
	private void OnClickChaoMain()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		ChaoSetWindowUI window = ChaoSetWindowUI.GetWindow();
		if (window != null)
		{
			DataTable.ChaoData chaoData = ChaoTable.GetChaoData(ChaoSetUI.SaveDataInterface.MainChaoId);
			if (chaoData != null)
			{
				ChaoSetWindowUI.ChaoInfo chaoInfo = new ChaoSetWindowUI.ChaoInfo(chaoData);
				chaoInfo.level = chaoData.level;
				chaoInfo.detail = chaoData.GetDetailLevelPlusSP(chaoInfo.level);
				window.OpenWindow(chaoInfo, ChaoSetWindowUI.WindowType.WINDOW_ONLY);
			}
		}
	}

	// Token: 0x06001DFB RID: 7675 RVA: 0x000B0560 File Offset: 0x000AE760
	private void OnClickChaoSub()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		ChaoSetWindowUI window = ChaoSetWindowUI.GetWindow();
		if (window != null)
		{
			DataTable.ChaoData chaoData = ChaoTable.GetChaoData(ChaoSetUI.SaveDataInterface.SubChaoId);
			if (chaoData != null)
			{
				ChaoSetWindowUI.ChaoInfo chaoInfo = new ChaoSetWindowUI.ChaoInfo(chaoData);
				chaoInfo.level = chaoData.level;
				chaoInfo.detail = chaoData.GetDetailLevelPlusSP(chaoInfo.level);
				window.OpenWindow(chaoInfo, ChaoSetWindowUI.WindowType.WINDOW_ONLY);
			}
		}
	}

	// Token: 0x06001DFC RID: 7676 RVA: 0x000B05D4 File Offset: 0x000AE7D4
	private void GoToCharacterButtonClicked()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.CHARA_MAIN, true);
	}

	// Token: 0x06001DFD RID: 7677 RVA: 0x000B05F0 File Offset: 0x000AE7F0
	private void OnMsgMenuBack()
	{
		ui_chao_set_cell.ResetLastLoadTime();
		if (this.m_tutorial)
		{
			TutorialCursor.EndTutorialCursor(TutorialCursor.Type.BACK);
			HudMenuUtility.SendMsgUpdateSaveDataDisplay();
			this.m_tutorial = false;
		}
		ChaoTextureManager.Instance.RemoveChaoTexture();
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				int num = (!this.m_descendingOrder) ? 0 : 1;
				if (this.m_chaoSort != (ChaoSort)systemdata.chaoSortType01 || num != systemdata.chaoSortType02)
				{
					systemdata.chaoSortType01 = (int)this.m_chaoSort;
					systemdata.chaoSortType02 = num;
					instance.SaveSystemData();
				}
			}
		}
		this.m_endSetUp = false;
	}

	// Token: 0x06001DFE RID: 7678 RVA: 0x000B069C File Offset: 0x000AE89C
	public void ChaoSetLoad(int stock)
	{
	}

	// Token: 0x06001DFF RID: 7679 RVA: 0x000B06A0 File Offset: 0x000AE8A0
	public static bool IsChaoTutorial()
	{
		return false;
	}

	// Token: 0x04001B25 RID: 6949
	private const int MAX_COLUMNS = 4;

	// Token: 0x04001B26 RID: 6950
	private const float SORT_DELAY = 0.4f;

	// Token: 0x04001B27 RID: 6951
	private const float INIT_DELAY = 0.2f;

	// Token: 0x04001B28 RID: 6952
	[SerializeField]
	private bool isDebugRondomSetChao;

	// Token: 0x04001B29 RID: 6953
	[SerializeField]
	private int[] m_chaoCountNumber = new int[3];

	// Token: 0x04001B2A RID: 6954
	[SerializeField]
	private UILabel m_getChaoCountLabel;

	// Token: 0x04001B2B RID: 6955
	[SerializeField]
	private UILabel m_getChaoCountShadowLabel;

	// Token: 0x04001B2C RID: 6956
	[SerializeField]
	private UISprite m_getChaoSprite;

	// Token: 0x04001B2D RID: 6957
	[SerializeField]
	private UILabel m_getChaoBonusLabel;

	// Token: 0x04001B2E RID: 6958
	[SerializeField]
	private ChaoSetUI.ChaoSerializeFields[] m_chaosSerializeFields = new ChaoSetUI.ChaoSerializeFields[2];

	// Token: 0x04001B2F RID: 6959
	[SerializeField]
	private ChaoSetUI.RouletteButtonUI m_rouletteButtonUI;

	// Token: 0x04001B30 RID: 6960
	[SerializeField]
	private UISprite m_sortLeveUp;

	// Token: 0x04001B31 RID: 6961
	[SerializeField]
	private UISprite m_sortRareUp;

	// Token: 0x04001B32 RID: 6962
	[SerializeField]
	private GameObject m_specialEggIconObj;

	// Token: 0x04001B33 RID: 6963
	[SerializeField]
	private GameObject m_freeSpinIconObj;

	// Token: 0x04001B34 RID: 6964
	[SerializeField]
	private UILabel m_freeSpinCountLabel;

	// Token: 0x04001B35 RID: 6965
	[SerializeField]
	private GameObject m_saleIconObj;

	// Token: 0x04001B36 RID: 6966
	[SerializeField]
	private GameObject m_eventIconObj;

	// Token: 0x04001B37 RID: 6967
	public static readonly string[] s_chaoBonusTypeSpriteNameSuffixs = new string[]
	{
		"score",
		"ring",
		"rsr",
		"animal",
		"range"
	};

	// Token: 0x04001B38 RID: 6968
	private GameObject m_slot;

	// Token: 0x04001B39 RID: 6969
	private UIRectItemStorage m_uiRectItemStorage;

	// Token: 0x04001B3A RID: 6970
	private UIDraggablePanel m_uiDraggablePanel;

	// Token: 0x04001B3B RID: 6971
	private List<GameObject> m_cells;

	// Token: 0x04001B3C RID: 6972
	private StageAbilityManager m_stageAbilityManager;

	// Token: 0x04001B3D RID: 6973
	private ChaoSort m_lastSort;

	// Token: 0x04001B3E RID: 6974
	private int m_currentDeckSetStock;

	// Token: 0x04001B3F RID: 6975
	private int m_sortCount;

	// Token: 0x04001B40 RID: 6976
	private List<GameObject> m_deckObjects;

	// Token: 0x04001B41 RID: 6977
	private List<DataTable.ChaoData> m_chaoDatas;

	// Token: 0x04001B42 RID: 6978
	private float m_sortDelay;

	// Token: 0x04001B43 RID: 6979
	private BoxCollider m_sortLeveUpBC;

	// Token: 0x04001B44 RID: 6980
	private BoxCollider m_sortRareUpBC;

	// Token: 0x04001B45 RID: 6981
	private UISprite m_mask0;

	// Token: 0x04001B46 RID: 6982
	private UISprite m_mask1;

	// Token: 0x04001B47 RID: 6983
	private ChaoSort m_chaoSort;

	// Token: 0x04001B48 RID: 6984
	private bool m_tutorial;

	// Token: 0x04001B49 RID: 6985
	private bool m_descendingOrder;

	// Token: 0x04001B4A RID: 6986
	private bool m_initEnd;

	// Token: 0x04001B4B RID: 6987
	private bool m_endSetUp;

	// Token: 0x04001B4C RID: 6988
	private float m_initDelay;

	// Token: 0x020003F0 RID: 1008
	public class SaveDataInterface
	{
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x000B06AC File Offset: 0x000AE8AC
		// (set) Token: 0x06001E02 RID: 7682 RVA: 0x000B06D8 File Offset: 0x000AE8D8
		public static int MainChaoId
		{
			get
			{
				SaveDataManager instance = SaveDataManager.Instance;
				if (instance != null)
				{
					return instance.PlayerData.MainChaoID;
				}
				return -1;
			}
			set
			{
				SaveDataManager instance = SaveDataManager.Instance;
				if (instance != null)
				{
					instance.PlayerData.MainChaoID = value;
				}
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x000B0704 File Offset: 0x000AE904
		// (set) Token: 0x06001E04 RID: 7684 RVA: 0x000B0730 File Offset: 0x000AE930
		public static int SubChaoId
		{
			get
			{
				SaveDataManager instance = SaveDataManager.Instance;
				if (instance != null)
				{
					return instance.PlayerData.SubChaoID;
				}
				return -1;
			}
			set
			{
				SaveDataManager instance = SaveDataManager.Instance;
				if (instance != null)
				{
					instance.PlayerData.SubChaoID = value;
				}
			}
		}
	}

	// Token: 0x020003F1 RID: 1009
	[Serializable]
	private class ChaoSerializeFields
	{
		// Token: 0x04001B4D RID: 6989
		[SerializeField]
		public UISprite m_chaoSprite;

		// Token: 0x04001B4E RID: 6990
		[SerializeField]
		public UITexture m_chaoTexture;

		// Token: 0x04001B4F RID: 6991
		[SerializeField]
		public UISprite m_chaoRankSprite;

		// Token: 0x04001B50 RID: 6992
		[SerializeField]
		public UILabel m_chaoNameLabel;

		// Token: 0x04001B51 RID: 6993
		[SerializeField]
		public UILabel m_chaoLevelLabel;

		// Token: 0x04001B52 RID: 6994
		[SerializeField]
		public UISprite m_chaoTypeSprite;

		// Token: 0x04001B53 RID: 6995
		[SerializeField]
		public UISprite m_bonusTypeSprite;

		// Token: 0x04001B54 RID: 6996
		[SerializeField]
		public UILabel m_bonusLabel;
	}

	// Token: 0x020003F2 RID: 1010
	[Serializable]
	private class RouletteButtonUI
	{
		// Token: 0x06001E07 RID: 7687 RVA: 0x000B076C File Offset: 0x000AE96C
		public void Setup()
		{
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				this.m_alertBadgeGameObject.SetActive(ServerInterface.CampaignState.InAnyIdSession(Constants.Campaign.emType.ChaoRouletteCost));
				int num = 0;
				if (RouletteManager.Instance != null)
				{
					num = RouletteManager.Instance.specialEgg;
				}
				this.m_eqqBadgeGameObject.SetActive(num >= 10);
				this.m_spinCountBadgeGameObject.SetActive(ServerInterface.WheelOptions.m_numRemaining > 0);
				this.m_spinCountLabel.text = ServerInterface.WheelOptions.m_numRemaining.ToString();
				this.m_spinCountBadgeGameObject.SetActive(false);
			}
		}

		// Token: 0x04001B55 RID: 6997
		[SerializeField]
		public GameObject m_alertBadgeGameObject;

		// Token: 0x04001B56 RID: 6998
		[SerializeField]
		public GameObject m_eqqBadgeGameObject;

		// Token: 0x04001B57 RID: 6999
		[SerializeField]
		public GameObject m_spinCountBadgeGameObject;

		// Token: 0x04001B58 RID: 7000
		[SerializeField]
		public UILabel m_spinCountLabel;
	}
}
