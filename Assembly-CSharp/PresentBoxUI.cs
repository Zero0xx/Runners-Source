using System;
using System.Collections;
using System.Collections.Generic;
using DataTable;
using Message;
using Text;
using UnityEngine;

// Token: 0x020004E3 RID: 1251
public class PresentBoxUI : MonoBehaviour
{
	// Token: 0x0600253F RID: 9535 RVA: 0x000E07B0 File Offset: 0x000DE9B0
	private void Start()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface == null)
		{
			ServerInterface.DebugInit();
		}
		this.Initialize();
		base.enabled = false;
	}

	// Token: 0x06002540 RID: 9536 RVA: 0x000E07E4 File Offset: 0x000DE9E4
	private void Update()
	{
	}

	// Token: 0x170004EC RID: 1260
	// (get) Token: 0x06002541 RID: 9537 RVA: 0x000E07E8 File Offset: 0x000DE9E8
	public bool IsEndSetup
	{
		get
		{
			return this.m_setUp;
		}
	}

	// Token: 0x06002542 RID: 9538 RVA: 0x000E07F0 File Offset: 0x000DE9F0
	private void Initialize()
	{
		if (!this.m_init_flag)
		{
			this.SetUIButtonMessage(this.m_recieveAllBtnObj, "OnClickedRecieveAllBtn");
			this.SetUIButtonMessage(this.m_recieveSelectBtnObj, "OnClickedRecieveSelectBtn");
			this.SetUIButtonMessage(this.m_nextPageBtnObj, "OnClickedNextPageBtn");
			this.SetUIButtonMessage(this.m_prevPageBtnObj, "OnClickedPrevPageBtnBtn");
			this.SetTextLabel(this.m_infoLabel, "information", null, null);
			this.SetTextLabel(this.m_recieveAllLabel, "recieve_all_item", null, null);
			this.SetTextLabel(this.m_recieveSelectLabel, "recieve_select_item", null, null);
			this.SetTextLabel(this.m_prevPageLabel, "prev_page", "{MAILE_COUNT}", 10.ToString());
			this.SetTextLabel(this.m_nextPageLabel, "next_page", "{MAILE_COUNT}", 10.ToString());
			this.m_init_flag = true;
		}
	}

	// Token: 0x06002543 RID: 9539 RVA: 0x000E08CC File Offset: 0x000DEACC
	private void SetTextLabel(UILabel uiLabel, string cellId, string tagString, string replaceString)
	{
		if (uiLabel != null)
		{
			TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "PresentBox", cellId);
			if (text != null)
			{
				text.ReplaceTag(tagString, replaceString);
				uiLabel.text = text.text;
			}
		}
	}

	// Token: 0x06002544 RID: 9540 RVA: 0x000E0910 File Offset: 0x000DEB10
	private void SetUIButtonMessage(GameObject obj, string callBackName)
	{
		if (obj != null)
		{
			UIButtonMessage component = obj.GetComponent<UIButtonMessage>();
			if (component == null)
			{
				obj.AddComponent<UIButtonMessage>();
				component = obj.GetComponent<UIButtonMessage>();
			}
			if (component != null)
			{
				component.enabled = true;
				component.trigger = UIButtonMessage.Trigger.OnClick;
				component.target = base.gameObject;
				component.functionName = callBackName;
			}
		}
	}

	// Token: 0x06002545 RID: 9541 RVA: 0x000E0978 File Offset: 0x000DEB78
	private void UpdatePage()
	{
		this.UpdateRectItemStorage();
		this.UpdateText();
		bool flag = this.m_totalPageCount > 1;
		this.SetEnableImageButton(this.m_nextPageBtnObj, flag);
		this.SetEnableImageButton(this.m_prevPageBtnObj, flag);
		bool flag2 = this.m_totalPageCount > 0;
		this.SetEnableImageButton(this.m_recieveAllBtnObj, flag2);
		this.SetEnableImageButton(this.m_recieveSelectBtnObj, flag2);
		if (this.m_scrollBar != null)
		{
			this.m_scrollBar.value = 0f;
		}
	}

	// Token: 0x06002546 RID: 9542 RVA: 0x000E09FC File Offset: 0x000DEBFC
	private void SetEnableImageButton(GameObject obj, bool flag)
	{
		if (obj != null)
		{
			UIImageButton component = obj.GetComponent<UIImageButton>();
			if (component != null)
			{
				component.isEnabled = flag;
			}
		}
	}

	// Token: 0x06002547 RID: 9543 RVA: 0x000E0A30 File Offset: 0x000DEC30
	private void UpdateText()
	{
		if (this.m_numPageLabel != null)
		{
			if (this.m_totalPageCount == 0)
			{
				this.m_numPageLabel.text = "0/0";
			}
			else
			{
				this.m_numPageLabel.text = (1 + this.m_currentPageNum).ToString() + "/" + this.m_totalPageCount.ToString();
			}
		}
	}

	// Token: 0x06002548 RID: 9544 RVA: 0x000E0AA0 File Offset: 0x000DECA0
	private void UpdateRectItemStorage()
	{
		if (this.m_itemStorage != null && this.m_presentInfoList != null)
		{
			if (this.m_totalPageCount == 0)
			{
				this.m_itemStorage.maxItemCount = 0;
				this.m_itemStorage.maxRows = 0;
				this.m_currentPageNum = 0;
				this.m_itemStorage.Restart();
			}
			else
			{
				this.m_currentPageNum = Mathf.Min(this.m_currentPageNum, this.m_totalPageCount - 1);
				this.m_currentPageNum = Mathf.Max(this.m_currentPageNum, 0);
				int num = Mathf.Clamp(this.m_pageItemCountDic[this.m_currentPageNum], 0, 10);
				this.m_itemStorage.maxItemCount = num;
				this.m_itemStorage.maxRows = num;
				this.m_itemStorage.Restart();
				ui_presentbox_scroll[] componentsInChildren = this.m_itemStorage.GetComponentsInChildren<ui_presentbox_scroll>(true);
				int num2 = componentsInChildren.Length;
				for (int i = 0; i < num; i++)
				{
					if (i < num2)
					{
						int index = i + this.m_currentPageNum * 10;
						componentsInChildren[i].UpdateView(this.m_presentInfoList[index]);
					}
				}
			}
		}
	}

	// Token: 0x06002549 RID: 9545 RVA: 0x000E0BB8 File Offset: 0x000DEDB8
	private void SetPresentBoxInfo()
	{
		if (this.m_presentInfoList != null)
		{
			this.m_presentInfoList.Clear();
			this.m_pageItemCountDic.Clear();
			if (ServerInterface.MessageList != null)
			{
				List<ServerMessageEntry> messageList = ServerInterface.MessageList;
				foreach (ServerMessageEntry serverMessageEntry in messageList)
				{
					if (PresentBoxUtility.IsWithinTimeLimit(serverMessageEntry.m_expireTiem))
					{
						PresentBoxUI.PresentInfo item = new PresentBoxUI.PresentInfo(serverMessageEntry);
						this.m_presentInfoList.Add(item);
					}
				}
			}
			if (ServerInterface.OperatorMessageList != null)
			{
				List<ServerOperatorMessageEntry> operatorMessageList = ServerInterface.OperatorMessageList;
				foreach (ServerOperatorMessageEntry serverOperatorMessageEntry in operatorMessageList)
				{
					if (PresentBoxUtility.IsWithinTimeLimit(serverOperatorMessageEntry.m_expireTiem))
					{
						PresentBoxUI.PresentInfo item2 = new PresentBoxUI.PresentInfo(serverOperatorMessageEntry);
						this.m_presentInfoList.Add(item2);
					}
				}
			}
			this.m_totalPageCount = this.m_presentInfoList.Count / 10;
			int num = this.m_presentInfoList.Count % 10;
			if (num > 0)
			{
				this.m_totalPageCount++;
			}
			for (int i = 0; i < this.m_totalPageCount; i++)
			{
				if (i == this.m_totalPageCount - 1)
				{
					if (num == 0)
					{
						this.m_pageItemCountDic.Add(i, 10);
					}
					else
					{
						this.m_pageItemCountDic.Add(i, num);
					}
				}
				else
				{
					this.m_pageItemCountDic.Add(i, 10);
				}
			}
		}
	}

	// Token: 0x0600254A RID: 9546 RVA: 0x000E0D90 File Offset: 0x000DEF90
	private void OnStartPresentBox()
	{
		this.m_outOfDataFlag = false;
		this.Initialize();
		this.SetPresentBoxInfo();
		this.UpdatePage();
		this.m_setUp = true;
	}

	// Token: 0x0600254B RID: 9547 RVA: 0x000E0DC0 File Offset: 0x000DEFC0
	private void OnEndPresentBox()
	{
		if (this.m_itemStorage != null)
		{
			this.m_itemStorage.maxItemCount = 0;
			this.m_itemStorage.maxRows = 0;
			this.m_itemStorage.Restart();
			this.m_presentInfoList.Clear();
			this.m_pageItemCountDic.Clear();
			GameObject gameObject = GameObject.Find("PresentBoxTextures");
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		this.m_setUp = false;
	}

	// Token: 0x0600254C RID: 9548 RVA: 0x000E0E3C File Offset: 0x000DF03C
	private void OnClickedRecieveAllBtn()
	{
		if (this.m_totalPageCount > 0)
		{
			int num = 0;
			if (ServerInterface.MessageList != null)
			{
				foreach (PresentBoxUI.PresentInfo presentInfo in this.m_presentInfoList)
				{
					if (PresentBoxUtility.IsWithinTimeLimit(presentInfo.expireTime))
					{
						num++;
					}
					else
					{
						this.m_outOfDataFlag = true;
					}
				}
			}
			if (num > 0)
			{
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null)
				{
					loggedInServerInterface.RequestServerUpdateMessage(null, null, base.gameObject);
				}
				BackKeyManager.InvalidFlag = true;
			}
			else if (this.m_outOfDataFlag)
			{
				base.StartCoroutine(this.ShowOnlyOutOfDataResult());
			}
			SoundManager.SePlay("sys_roulette_itemget", "SE");
		}
	}

	// Token: 0x0600254D RID: 9549 RVA: 0x000E0F30 File Offset: 0x000DF130
	private void OnClickedRecieveSelectBtn()
	{
		if (this.m_totalPageCount > 0)
		{
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			if (this.m_itemStorage != null && list != null && list2 != null)
			{
				ui_presentbox_scroll[] componentsInChildren = this.m_itemStorage.GetComponentsInChildren<ui_presentbox_scroll>(true);
				int num = componentsInChildren.Length;
				for (int i = 0; i < num; i++)
				{
					if (componentsInChildren[i].IsCheck())
					{
						int index = i + this.m_currentPageNum * 10;
						int messageId = this.m_presentInfoList[index].messageId;
						if (PresentBoxUtility.IsWithinTimeLimit(this.m_presentInfoList[index].expireTime))
						{
							if (this.m_presentInfoList[index].operatorFlag)
							{
								list2.Add(messageId);
							}
							else
							{
								list.Add(messageId);
							}
						}
						else
						{
							this.m_outOfDataFlag = true;
						}
					}
				}
				int num2 = list.Count + list2.Count;
				if (num2 > 0)
				{
					ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
					if (loggedInServerInterface != null)
					{
						loggedInServerInterface.RequestServerUpdateMessage(list, list2, base.gameObject);
					}
					BackKeyManager.InvalidFlag = true;
				}
				else if (this.m_outOfDataFlag)
				{
					base.StartCoroutine(this.ShowOnlyOutOfDataResult());
				}
			}
			SoundManager.SePlay("sys_roulette_itemget", "SE");
		}
	}

	// Token: 0x0600254E RID: 9550 RVA: 0x000E108C File Offset: 0x000DF28C
	private void OnClickedNextPageBtn()
	{
		SoundManager.SePlay("sys_page_skip", "SE");
		if (this.m_totalPageCount > 1)
		{
			this.m_currentPageNum++;
			if (this.m_totalPageCount == this.m_currentPageNum)
			{
				this.m_currentPageNum = 0;
			}
			this.UpdatePage();
		}
	}

	// Token: 0x0600254F RID: 9551 RVA: 0x000E10E4 File Offset: 0x000DF2E4
	private void OnClickedPrevPageBtnBtn()
	{
		SoundManager.SePlay("sys_page_skip", "SE");
		if (this.m_totalPageCount > 1)
		{
			if (this.m_currentPageNum > 0)
			{
				this.m_currentPageNum--;
			}
			else
			{
				this.m_currentPageNum = this.m_totalPageCount - 1;
			}
			this.UpdatePage();
		}
	}

	// Token: 0x06002550 RID: 9552 RVA: 0x000E1140 File Offset: 0x000DF340
	private void ServerUpdateMessage_Succeeded(MsgUpdateMesseageSucceed msg)
	{
		BackKeyManager.InvalidFlag = false;
		base.StartCoroutine(this.ShowResult(msg));
		this.SetPresentBoxInfo();
		this.UpdatePage();
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
	}

	// Token: 0x06002551 RID: 9553 RVA: 0x000E1174 File Offset: 0x000DF374
	private void ServerUpdateMessage_Failed()
	{
		BackKeyManager.InvalidFlag = false;
	}

	// Token: 0x06002552 RID: 9554 RVA: 0x000E117C File Offset: 0x000DF37C
	public IEnumerator ShowOnlyOutOfDataResult()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "presentbox",
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = TextUtility.GetCommonText("PresentBox", "out_of_date_caption"),
			message = TextUtility.GetCommonText("PresentBox", "out_of_date_text"),
			parentGameObject = base.gameObject
		});
		while (!GeneralWindow.IsButtonPressed)
		{
			yield return null;
		}
		GeneralWindow.Close();
		this.m_outOfDataFlag = false;
		yield break;
	}

	// Token: 0x06002553 RID: 9555 RVA: 0x000E1198 File Offset: 0x000DF398
	public IEnumerator ShowResult(MsgUpdateMesseageSucceed msg)
	{
		CharaType charaType = CharaType.UNKNOWN;
		bool charaFlag = false;
		bool chaoFlag = false;
		bool missFlag = false;
		if (msg != null)
		{
			missFlag = (msg.m_notRecvMessageList.Count + msg.m_notRecvOperatorMessageList.Count > 0);
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "presentbox",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextUtility.GetCommonText("PresentBox", "present_box"),
				message = PresentBoxUtility.GetPresetTextList(msg.m_presentStateList),
				parentGameObject = base.gameObject
			});
		}
		else
		{
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "presentbox",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextUtility.GetCommonText("PresentBox", "present_box"),
				message = TextUtility.GetCommonText("PresentBox", "not_receive_message"),
				parentGameObject = base.gameObject
			});
		}
		while (!GeneralWindow.IsButtonPressed)
		{
			yield return null;
		}
		GeneralWindow.Close();
		if (missFlag)
		{
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "presentbox",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextUtility.GetCommonText("PresentBox", "present_box"),
				message = TextUtility.GetCommonText("PresentBox", "miss_message"),
				parentGameObject = base.gameObject
			});
			while (!GeneralWindow.IsButtonPressed)
			{
				yield return null;
			}
			GeneralWindow.Close();
		}
		if (this.m_outOfDataFlag)
		{
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "presentbox",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextUtility.GetCommonText("PresentBox", "out_of_date_caption"),
				message = TextUtility.GetCommonText("PresentBox", "out_of_date_text"),
				parentGameObject = base.gameObject
			});
			while (!GeneralWindow.IsButtonPressed)
			{
				yield return null;
			}
			GeneralWindow.Close();
		}
		if (msg != null)
		{
			foreach (ServerPresentState state in msg.m_presentStateList)
			{
				ServerItem serverItem = new ServerItem((ServerItem.Id)state.m_itemId);
				if (serverItem.idType == ServerItem.IdType.CHARA && serverItem.charaType != CharaType.UNKNOWN)
				{
					charaFlag = true;
					charaType = serverItem.charaType;
					GameObject uiRoot = GameObject.Find("UI Root (2D)");
					ServerPlayerState playerState = ServerInterface.PlayerState;
					if (playerState != null)
					{
						ServerCharacterState charaState = playerState.CharacterState(charaType);
						if (charaState != null && charaState.star > 0)
						{
							if (this.m_playerMergeWindow == null)
							{
								this.m_playerMergeWindow = GameObjectUtil.FindChildGameObjectComponent<PlayerMergeWindow>(uiRoot, "player_merge_Window");
							}
							if (this.m_playerMergeWindow != null)
							{
								this.m_playerMergeWindow.PlayStart(state.m_itemId, RouletteUtility.AchievementType.PlayerGet);
								while (!this.m_playerMergeWindow.IsPlayEnd)
								{
									yield return null;
								}
								this.m_playerMergeWindow = null;
							}
						}
						else
						{
							if (this.m_charaGetWindow == null)
							{
								this.m_charaGetWindow = GameObjectUtil.FindChildGameObjectComponent<ChaoGetWindow>(uiRoot, "ro_PlayerGetWindowUI");
							}
							if (this.m_charaGetWindow != null)
							{
								PlayerGetPartsOverlap playerGet = this.m_charaGetWindow.gameObject.GetComponent<PlayerGetPartsOverlap>();
								if (playerGet == null)
								{
									playerGet = this.m_charaGetWindow.gameObject.AddComponent<PlayerGetPartsOverlap>();
								}
								playerGet.Init(state.m_itemId, 100, 0, null, PlayerGetPartsOverlap.IntroType.NO_EGG);
								ChaoGetPartsBase partsBase = playerGet;
								bool tutorial = false;
								bool disabledEqip = true;
								this.m_charaGetWindow.PlayStart(partsBase, tutorial, disabledEqip, RouletteUtility.AchievementType.NONE);
								while (!this.m_charaGetWindow.IsPlayEnd)
								{
									yield return null;
								}
								this.m_charaGetWindow = null;
							}
						}
					}
				}
			}
			List<int> chaoIdList = new List<int>();
			foreach (ServerPresentState state2 in msg.m_presentStateList)
			{
				ServerItem serverItem2 = new ServerItem((ServerItem.Id)state2.m_itemId);
				if (serverItem2.idType == ServerItem.IdType.CHAO)
				{
					if (!chaoIdList.Contains(serverItem2.chaoId))
					{
						chaoIdList.Add(serverItem2.chaoId);
						GameObject uiRoot2 = GameObject.Find("UI Root (2D)");
						ChaoData data = ChaoTable.GetChaoData(serverItem2.chaoId);
						if (data != null)
						{
							chaoFlag = true;
							ChaoGetPartsBase partsBase2 = null;
							if (data.level > 0)
							{
								if (this.m_chaoMergeWindow == null)
								{
									this.m_chaoMergeWindow = GameObjectUtil.FindChildGameObjectComponent<ChaoMergeWindow>(uiRoot2, "chao_merge_Window");
								}
								this.m_chaoMergeWindow.PlayStart(state2.m_itemId, data.level, (int)data.rarity, RouletteUtility.AchievementType.NONE);
								while (!this.m_chaoMergeWindow.IsPlayEnd)
								{
									yield return null;
								}
								this.m_chaoMergeWindow = null;
							}
							else
							{
								if (data.rarity == ChaoData.Rarity.NORMAL || data.rarity == ChaoData.Rarity.SRARE)
								{
									if (this.m_chaoGetWindow == null)
									{
										this.m_chaoGetWindow = GameObjectUtil.FindChildGameObjectComponent<ChaoGetWindow>(uiRoot2, "chao_get_Window");
									}
									ChaoGetPartsNormal normal = this.m_chaoGetWindow.gameObject.GetComponent<ChaoGetPartsNormal>();
									if (normal == null)
									{
										normal = this.m_chaoGetWindow.gameObject.AddComponent<ChaoGetPartsNormal>();
									}
									normal.Init(state2.m_itemId, (int)data.rarity);
									partsBase2 = normal;
								}
								else
								{
									if (this.m_chaoGetWindow == null)
									{
										this.m_chaoGetWindow = GameObjectUtil.FindChildGameObjectComponent<ChaoGetWindow>(uiRoot2, "chao_rare_get_Window");
									}
									ChaoGetPartsRare rare = this.m_chaoGetWindow.gameObject.GetComponent<ChaoGetPartsRare>();
									if (rare == null)
									{
										rare = this.m_chaoGetWindow.gameObject.AddComponent<ChaoGetPartsRare>();
									}
									rare.Init(state2.m_itemId, (int)data.rarity);
									partsBase2 = rare;
								}
								bool tutorial2 = false;
								bool disabledEqip2 = true;
								this.m_chaoGetWindow.PlayStart(partsBase2, tutorial2, disabledEqip2, RouletteUtility.AchievementType.NONE);
								while (!this.m_chaoGetWindow.IsPlayEnd)
								{
									yield return null;
								}
								this.m_chaoGetWindow = null;
							}
						}
					}
				}
			}
		}
		if (charaFlag || chaoFlag)
		{
			AchievementManager.RequestUpdate();
			while (!AchievementManager.IsRequestEnd())
			{
				yield return null;
			}
		}
		if (charaFlag)
		{
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "presentbox",
				buttonType = GeneralWindow.ButtonType.YesNo,
				caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "FaceBook", "ui_Lbl_verification"),
				message = TextUtility.GetCommonText("PresentBox", "player_set_text"),
				parentGameObject = base.gameObject
			});
			while (!GeneralWindow.IsButtonPressed)
			{
				yield return null;
			}
			if (GeneralWindow.IsYesButtonPressed)
			{
				chaoFlag = false;
				MenuPlayerSetUtil.SetMarkCharaPage(charaType);
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.CHARA_MAIN, true);
			}
			GeneralWindow.Close();
		}
		if (chaoFlag)
		{
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "presentbox",
				buttonType = GeneralWindow.ButtonType.YesNo,
				caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "FaceBook", "ui_Lbl_verification"),
				message = TextUtility.GetCommonText("PresentBox", "chao_set_text"),
				parentGameObject = base.gameObject
			});
			while (!GeneralWindow.IsButtonPressed)
			{
				yield return null;
			}
			if (GeneralWindow.IsYesButtonPressed)
			{
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.CHAO, true);
			}
			GeneralWindow.Close();
		}
		this.m_outOfDataFlag = false;
		yield break;
	}

	// Token: 0x04002163 RID: 8547
	private const int DISPLAY_MAX_ITEM_COUNT = 10;

	// Token: 0x04002164 RID: 8548
	[SerializeField]
	private UIRectItemStorage m_itemStorage;

	// Token: 0x04002165 RID: 8549
	[SerializeField]
	private UIScrollBar m_scrollBar;

	// Token: 0x04002166 RID: 8550
	[SerializeField]
	private UILabel m_recieveAllLabel;

	// Token: 0x04002167 RID: 8551
	[SerializeField]
	private UILabel m_recieveSelectLabel;

	// Token: 0x04002168 RID: 8552
	[SerializeField]
	private UILabel m_infoLabel;

	// Token: 0x04002169 RID: 8553
	[SerializeField]
	private UILabel m_nextPageLabel;

	// Token: 0x0400216A RID: 8554
	[SerializeField]
	private UILabel m_prevPageLabel;

	// Token: 0x0400216B RID: 8555
	[SerializeField]
	private UILabel m_numPageLabel;

	// Token: 0x0400216C RID: 8556
	[SerializeField]
	private GameObject m_recieveAllBtnObj;

	// Token: 0x0400216D RID: 8557
	[SerializeField]
	private GameObject m_recieveSelectBtnObj;

	// Token: 0x0400216E RID: 8558
	[SerializeField]
	private GameObject m_nextPageBtnObj;

	// Token: 0x0400216F RID: 8559
	[SerializeField]
	private GameObject m_prevPageBtnObj;

	// Token: 0x04002170 RID: 8560
	private int m_currentPageNum;

	// Token: 0x04002171 RID: 8561
	private int m_totalPageCount;

	// Token: 0x04002172 RID: 8562
	private bool m_init_flag;

	// Token: 0x04002173 RID: 8563
	private bool m_outOfDataFlag;

	// Token: 0x04002174 RID: 8564
	private bool m_setUp;

	// Token: 0x04002175 RID: 8565
	private List<PresentBoxUI.PresentInfo> m_presentInfoList = new List<PresentBoxUI.PresentInfo>();

	// Token: 0x04002176 RID: 8566
	private Dictionary<int, int> m_pageItemCountDic = new Dictionary<int, int>();

	// Token: 0x04002177 RID: 8567
	private ChaoGetWindow m_charaGetWindow;

	// Token: 0x04002178 RID: 8568
	private ChaoGetWindow m_chaoGetWindow;

	// Token: 0x04002179 RID: 8569
	private ChaoMergeWindow m_chaoMergeWindow;

	// Token: 0x0400217A RID: 8570
	private PlayerMergeWindow m_playerMergeWindow;

	// Token: 0x020004E4 RID: 1252
	public class PresentInfo
	{
		// Token: 0x06002554 RID: 9556 RVA: 0x000E11C4 File Offset: 0x000DF3C4
		public PresentInfo(ServerMessageEntry msg)
		{
			if (msg != null)
			{
				this.messageId = msg.m_messageId;
				this.messageType = msg.m_messageType;
				this.itemId = msg.m_presentState.m_itemId;
				this.itemNum = msg.m_presentState.m_numItem;
				this.expireTime = msg.m_expireTiem;
				this.name = msg.m_name;
				this.fromId = msg.m_fromId;
				this.serverItem = new ServerItem((ServerItem.Id)this.itemId);
			}
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x000E128C File Offset: 0x000DF48C
		public PresentInfo(ServerOperatorMessageEntry msg)
		{
			if (msg != null)
			{
				this.messageId = msg.m_messageId;
				this.itemId = msg.m_presentState.m_itemId;
				this.itemNum = msg.m_presentState.m_numItem;
				this.expireTime = msg.m_expireTiem;
				this.infoText = msg.m_content;
				this.serverItem = new ServerItem((ServerItem.Id)this.itemId);
				this.operatorFlag = true;
			}
		}

		// Token: 0x0400217B RID: 8571
		public int messageId = -1;

		// Token: 0x0400217C RID: 8572
		public int itemId = -1;

		// Token: 0x0400217D RID: 8573
		public int itemNum;

		// Token: 0x0400217E RID: 8574
		public int expireTime;

		// Token: 0x0400217F RID: 8575
		public ServerMessageEntry.MessageType messageType = ServerMessageEntry.MessageType.Unknown;

		// Token: 0x04002180 RID: 8576
		public string name = string.Empty;

		// Token: 0x04002181 RID: 8577
		public string infoText = string.Empty;

		// Token: 0x04002182 RID: 8578
		public string fromId = string.Empty;

		// Token: 0x04002183 RID: 8579
		public ServerItem serverItem;

		// Token: 0x04002184 RID: 8580
		public Texture charaTex;

		// Token: 0x04002185 RID: 8581
		public bool operatorFlag;

		// Token: 0x04002186 RID: 8582
		public bool checkFlag = true;
	}
}
