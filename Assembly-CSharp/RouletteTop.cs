using System;
using System.Collections.Generic;
using AnimationOrTween;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000516 RID: 1302
public class RouletteTop : CustomGameObject
{
	// Token: 0x17000530 RID: 1328
	// (get) Token: 0x06002765 RID: 10085 RVA: 0x000F3DA8 File Offset: 0x000F1FA8
	public bool addSpecialEgg
	{
		get
		{
			return this.m_addSpecialEgg;
		}
	}

	// Token: 0x06002766 RID: 10086 RVA: 0x000F3DB0 File Offset: 0x000F1FB0
	public bool IsClose()
	{
		return !this.m_opened;
	}

	// Token: 0x06002767 RID: 10087 RVA: 0x000F3DBC File Offset: 0x000F1FBC
	public Color GetBtnColor(RouletteCategory category)
	{
		Color result = this.m_defaultColor;
		if (category != RouletteCategory.PREMIUM)
		{
			if (category == RouletteCategory.SPECIAL)
			{
				result = this.m_specialColor;
			}
		}
		else
		{
			result = this.m_premiumColor;
		}
		return result;
	}

	// Token: 0x06002768 RID: 10088 RVA: 0x000F3E00 File Offset: 0x000F2000
	public bool IsEffect(RouletteTop.ROULETTE_EFFECT_TYPE type)
	{
		bool result = false;
		if (this.m_notEffectList != null && this.m_notEffectList.Count > 0)
		{
			if (!this.m_notEffectList.Contains(type))
			{
				result = true;
			}
		}
		else
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06002769 RID: 10089 RVA: 0x000F3E48 File Offset: 0x000F2048
	public void OnRouletteOpenItem()
	{
		if (this.m_removeTime == 0f || Mathf.Abs(Time.realtimeSinceStartup - this.m_removeTime) > 0.5f)
		{
			if (this.m_rouletteCostItemLoadedList != null)
			{
				this.m_rouletteCostItemLoadedList.Clear();
			}
			else
			{
				this.m_rouletteCostItemLoadedList = new List<RouletteCategory>();
			}
			this.m_isTopPage = false;
			this.m_tutorialSpin = false;
			this.m_opened = true;
			global::Debug.Log("RouletteTop:OnRouletteOpenItem!");
			RouletteManager.RouletteBgmReset();
			RouletteManager.RouletteOpen(RouletteCategory.ITEM);
		}
	}

	// Token: 0x0600276A RID: 10090 RVA: 0x000F3ED4 File Offset: 0x000F20D4
	public void OnRouletteOpenPremium()
	{
		if (this.m_removeTime == 0f || Mathf.Abs(Time.realtimeSinceStartup - this.m_removeTime) > 0.5f)
		{
			if (this.m_rouletteCostItemLoadedList != null)
			{
				this.m_rouletteCostItemLoadedList.Clear();
			}
			else
			{
				this.m_rouletteCostItemLoadedList = new List<RouletteCategory>();
			}
			this.m_isTopPage = false;
			this.m_tutorialSpin = false;
			this.m_opened = true;
			global::Debug.Log("RouletteTop:OnRouletteOpenPremium!");
			RouletteManager.RouletteBgmReset();
			RouletteManager.RouletteOpen(RouletteCategory.PREMIUM);
		}
	}

	// Token: 0x0600276B RID: 10091 RVA: 0x000F3F60 File Offset: 0x000F2160
	public void OnRouletteOpenRaid()
	{
		if (this.m_removeTime == 0f || Mathf.Abs(Time.realtimeSinceStartup - this.m_removeTime) > 0.5f)
		{
			if (this.m_rouletteCostItemLoadedList != null)
			{
				this.m_rouletteCostItemLoadedList.Clear();
			}
			else
			{
				this.m_rouletteCostItemLoadedList = new List<RouletteCategory>();
			}
			this.m_isTopPage = false;
			this.m_tutorialSpin = false;
			this.m_opened = true;
			global::Debug.Log("RouletteTop:OnRouletteOpenRaid!");
			RouletteManager.RouletteBgmReset();
			RouletteManager.RouletteOpen(RouletteCategory.RAID);
		}
	}

	// Token: 0x0600276C RID: 10092 RVA: 0x000F3FEC File Offset: 0x000F21EC
	public void OnRouletteOpenDefault()
	{
		if (this.m_removeTime == 0f || Mathf.Abs(Time.realtimeSinceStartup - this.m_removeTime) > 0.5f)
		{
			if (this.m_rouletteCostItemLoadedList != null)
			{
				this.m_rouletteCostItemLoadedList.Clear();
			}
			else
			{
				this.m_rouletteCostItemLoadedList = new List<RouletteCategory>();
			}
			this.m_isTopPage = false;
			this.m_tutorialSpin = false;
			this.m_opened = true;
			global::Debug.Log("RouletteTop:OnRouletteOpenDefault!  rouletteDefault:" + RouletteUtility.rouletteDefault);
			RouletteManager.RouletteBgmReset();
			RouletteManager.RouletteOpen(RouletteCategory.NONE);
		}
	}

	// Token: 0x0600276D RID: 10093 RVA: 0x000F4084 File Offset: 0x000F2284
	public void OnRouletteEnd()
	{
		RouletteManager.RouletteClose();
	}

	// Token: 0x0600276E RID: 10094 RVA: 0x000F408C File Offset: 0x000F228C
	public void UpdateCostItemList(List<ServerItem.Id> costItemList)
	{
		this.SetTopPageHeaderObject();
	}

	// Token: 0x0600276F RID: 10095 RVA: 0x000F4094 File Offset: 0x000F2294
	protected override void UpdateStd(float deltaTime, float timeRate)
	{
		if (this.m_setupNoCommunicationCategory != RouletteCategory.NONE)
		{
			if (GeneralWindow.IsCreated("SetupNoCommunication") && GeneralWindow.IsButtonPressed)
			{
				if (!GeneralUtil.IsNetwork())
				{
					GeneralUtil.ShowNoCommunication("SetupNoCommunication");
				}
				else
				{
					this.Setup(this.m_setupNoCommunicationCategory);
				}
			}
			return;
		}
		if (this.m_tutorial)
		{
			if (!GeneralWindow.Created)
			{
				if (GeneralWindow.IsCreated("RouletteTutorial") && GeneralWindow.IsOkButtonPressed && !this.m_tutorialSpin)
				{
					TutorialCursor.StartTutorialCursor(TutorialCursor.Type.ROULETTE_SPIN);
					this.m_tutorialSpin = true;
				}
				else if (GeneralWindow.IsCreated("RouletteTutorialEnd") && GeneralWindow.IsButtonPressed)
				{
					RouletteUtility.rouletteTurtorialEnd = true;
					this.m_tutorial = false;
					ItemGetWindow itemGetWindow = ItemGetWindowUtil.GetItemGetWindow();
					if (itemGetWindow != null)
					{
						itemGetWindow.Create(new ItemGetWindow.CInfo
						{
							name = "TutorialEndAddSp",
							caption = TextUtility.GetCommonText("MainMenu", "tutorial_sp_egg1_caption"),
							serverItemId = 220000,
							imageCount = TextUtility.GetCommonText("MainMenu", "tutorial_sp_egg1_text", "{COUNT}", 10.ToString())
						});
						SoundManager.SePlay("sys_specialegg", "SE");
					}
					global::Debug.Log("TurtorialEnd:" + RouletteUtility.rouletteTurtorialEnd + " !!!!!!!!!!!!!!!!!!!! ");
				}
				else if (GeneralWindow.IsCreated("RouletteTutorialError") && GeneralWindow.IsButtonPressed)
				{
					if (GeneralUtil.IsNetwork())
					{
						GeneralWindow.Create(new GeneralWindow.CInfo
						{
							name = "RouletteTutorialEnd",
							buttonType = GeneralWindow.ButtonType.Ok,
							caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "end_of_tutorial_caption").text,
							message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "end_of_tutorial_text").text
						});
						string[] value = new string[1];
						SendApollo.GetTutorialValue(ApolloTutorialIndex.START_STEP6, ref value);
						this.m_sendApollo = SendApollo.CreateRequest(ApolloType.TUTORIAL_END, value);
					}
					else
					{
						GeneralUtil.ShowNoCommunication("RouletteTutorialError");
					}
				}
			}
		}
		else if (RouletteUtility.rouletteTurtorialEnd)
		{
			ItemGetWindow itemGetWindow2 = ItemGetWindowUtil.GetItemGetWindow();
			if (itemGetWindow2 != null && itemGetWindow2.IsCreated("TutorialEndAddSp") && itemGetWindow2.IsEnd)
			{
				itemGetWindow2.Reset();
				if (RouletteManager.Instance != null && RouletteManager.Instance.specialEgg >= 10)
				{
					GeneralWindow.Create(new GeneralWindow.CInfo
					{
						name = "SpEggMax",
						buttonType = GeneralWindow.ButtonType.Ok,
						caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "sp_egg_max_caption").text,
						message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "sp_egg_max_text").text
					});
				}
				RouletteManager.RequestRoulette(this.category, base.gameObject);
				RouletteUtility.rouletteTurtorialEnd = false;
				this.m_addSpecialEgg = false;
				global::Debug.Log("TurtorialEnd:" + RouletteUtility.rouletteTurtorialEnd + " !!!!!!!!!!!!!!!!!!!! ");
			}
		}
		if (this.m_spin)
		{
			this.m_spinTime += deltaTime;
			if (this.m_multiGetDelayTime > 0f && this.m_spinDecision)
			{
				this.m_multiGetDelayTime -= deltaTime;
				if (this.m_multiGetDelayTime <= 0f)
				{
					this.OnRouletteSpinEnd();
					this.m_multiGetDelayTime = 0f;
				}
			}
			this.m_spinCount += 1L;
		}
		if (this.m_closeTime > 0f)
		{
			this.m_closeTime -= deltaTime;
			if (this.m_closeTime <= 0f)
			{
				this.m_clickBack = true;
				this.Close(RouletteUtility.NextType.NONE);
				this.m_closeTime = 0f;
			}
		}
		if (this.m_sendApollo != null && this.m_sendApollo.IsEnd())
		{
			UnityEngine.Object.Destroy(this.m_sendApollo.gameObject);
			this.m_sendApollo = null;
		}
		if (this.m_inputLimitTime > 0f)
		{
			this.m_inputLimitTime -= Time.deltaTime;
			if (this.m_inputLimitTime <= 0f)
			{
				this.m_inputLimitTime = 0f;
				HudMenuUtility.SetConnectAlertSimpleUI(false);
			}
		}
		if (this.m_topPageWheelData && this.m_topPageOddsSelect != RouletteCategory.NONE && RouletteManager.Instance != null && !RouletteManager.Instance.isCurrentPrizeLoading)
		{
			ServerPrizeState prizeList = RouletteManager.Instance.GetPrizeList(this.m_topPageOddsSelect);
			ServerWheelOptionsData rouletteDataOrg = RouletteManager.Instance.GetRouletteDataOrg(this.m_topPageOddsSelect);
			if (prizeList != null && rouletteDataOrg != null)
			{
				this.OpenOdds(prizeList, rouletteDataOrg);
				this.m_topPageWheelData = false;
				this.m_topPageOddsSelect = RouletteCategory.NONE;
			}
		}
	}

	// Token: 0x17000531 RID: 1329
	// (get) Token: 0x06002770 RID: 10096 RVA: 0x000F4560 File Offset: 0x000F2760
	public bool isWindow
	{
		get
		{
			return this.m_isWindow;
		}
	}

	// Token: 0x17000532 RID: 1330
	// (get) Token: 0x06002771 RID: 10097 RVA: 0x000F4568 File Offset: 0x000F2768
	public bool isEnabled
	{
		get
		{
			bool result = false;
			if (base.gameObject.activeSelf && this.m_parts != null && this.m_parts.Count > 0)
			{
				result = true;
			}
			return result;
		}
	}

	// Token: 0x17000533 RID: 1331
	// (get) Token: 0x06002772 RID: 10098 RVA: 0x000F45A8 File Offset: 0x000F27A8
	public ServerWheelOptionsData wheelData
	{
		get
		{
			return this.m_wheelData;
		}
	}

	// Token: 0x17000534 RID: 1332
	// (get) Token: 0x06002773 RID: 10099 RVA: 0x000F45B0 File Offset: 0x000F27B0
	public RouletteCategory category
	{
		get
		{
			if (this.m_wheelData == null)
			{
				return RouletteCategory.NONE;
			}
			return this.m_wheelData.category;
		}
	}

	// Token: 0x17000535 RID: 1333
	// (get) Token: 0x06002774 RID: 10100 RVA: 0x000F45CC File Offset: 0x000F27CC
	public float spinTime
	{
		get
		{
			if (!this.m_spin)
			{
				return 0f;
			}
			return this.m_spinTime;
		}
	}

	// Token: 0x17000536 RID: 1334
	// (get) Token: 0x06002775 RID: 10101 RVA: 0x000F45E8 File Offset: 0x000F27E8
	public bool isSpin
	{
		get
		{
			return this.m_spin;
		}
	}

	// Token: 0x17000537 RID: 1335
	// (get) Token: 0x06002776 RID: 10102 RVA: 0x000F45F0 File Offset: 0x000F27F0
	public bool isSpinSkip
	{
		get
		{
			return this.m_spinSkip;
		}
	}

	// Token: 0x17000538 RID: 1336
	// (get) Token: 0x06002777 RID: 10103 RVA: 0x000F45F8 File Offset: 0x000F27F8
	public bool isSpinDecision
	{
		get
		{
			return this.m_spinDecision;
		}
	}

	// Token: 0x17000539 RID: 1337
	// (get) Token: 0x06002778 RID: 10104 RVA: 0x000F4600 File Offset: 0x000F2800
	public int spinDecisionIndex
	{
		get
		{
			return this.m_spinDecisionIndex;
		}
	}

	// Token: 0x06002779 RID: 10105 RVA: 0x000F4608 File Offset: 0x000F2808
	public void SetPanelsAlpha(float alpha)
	{
		if (this.m_panels != null && this.m_panels.Count > 0)
		{
			foreach (UIPanel uipanel in this.m_panels)
			{
				uipanel.alpha = alpha;
			}
		}
	}

	// Token: 0x0600277A RID: 10106 RVA: 0x000F468C File Offset: 0x000F288C
	public float GetPanelsAlpha()
	{
		float num = -1f;
		if (this.m_parts != null && this.m_parts.Count > 0)
		{
			num = 0f;
			foreach (UIPanel uipanel in this.m_panels)
			{
				float num2 = uipanel.alpha;
				if (num2 > 1f)
				{
					num2 = 1f;
				}
				num += num2;
			}
			num /= (float)this.m_panels.Count;
			if (num > 1f)
			{
				num = 1f;
			}
			else if (num < 0.01f)
			{
				num = 0f;
			}
		}
		return num;
	}

	// Token: 0x1700053A RID: 1338
	// (get) Token: 0x0600277B RID: 10107 RVA: 0x000F4768 File Offset: 0x000F2968
	public bool isSpinGetWindow
	{
		get
		{
			bool result = false;
			if (this.m_wheelDataAfter != null)
			{
				result = true;
			}
			return result;
		}
	}

	// Token: 0x1700053B RID: 1339
	// (get) Token: 0x0600277C RID: 10108 RVA: 0x000F4788 File Offset: 0x000F2988
	public bool isWordAnime
	{
		get
		{
			return this.m_word;
		}
	}

	// Token: 0x0600277D RID: 10109 RVA: 0x000F4790 File Offset: 0x000F2990
	public bool OpenOdds(ServerPrizeState prize, ServerWheelOptionsData wheelOptionsData = null)
	{
		bool result = false;
		if (this.m_odds != null)
		{
			if (wheelOptionsData != null)
			{
				this.m_odds.Open(prize, wheelOptionsData);
			}
			else
			{
				this.m_odds.Open(prize, this.wheelData);
			}
			result = true;
		}
		return result;
	}

	// Token: 0x0600277E RID: 10110 RVA: 0x000F47E0 File Offset: 0x000F29E0
	public bool OnRouletteSpinStart(ServerWheelOptionsData data, int num)
	{
		if (data != null && data.category == RouletteCategory.RAID && EventManager.Instance != null && EventManager.Instance.TypeInTime != EventManager.EventType.RAID_BOSS)
		{
			GeneralUtil.ShowEventEnd("ShowEventEnd");
			this.Setup(this.m_rouletteList[0]);
			return false;
		}
		bool result = false;
		this.m_nextType = RouletteUtility.NextType.NONE;
		this.m_closeTime = 0f;
		if (!this.isSpin)
		{
			this.m_spinCount = 0L;
			GC.Collect();
			if (this.m_tutorial)
			{
				TutorialCursor.EndTutorialCursor(TutorialCursor.Type.ROULETTE_SPIN);
			}
			if (this.m_backButtonImg != null)
			{
				this.m_backButtonImg.isEnabled = false;
			}
			this.m_spinDecisionIndex = -1;
			if (this.m_parts != null && this.m_parts.Count > 0)
			{
				foreach (RoulettePartsBase roulettePartsBase in this.m_parts)
				{
					if (roulettePartsBase != null)
					{
						roulettePartsBase.OnSpinStart();
					}
				}
			}
			this.m_wheelSetup = false;
			this.m_spinTime = 0f;
			this.m_word = false;
			this.m_spin = true;
			this.m_spinSkip = false;
			this.m_spinDecision = false;
			result = RouletteManager.RequestCommitRoulette(data, num, base.gameObject);
		}
		return result;
	}

	// Token: 0x0600277F RID: 10111 RVA: 0x000F495C File Offset: 0x000F2B5C
	public bool OnRouletteSpinSkip()
	{
		bool result = false;
		this.m_closeTime = 0f;
		if (this.isSpin && !this.isSpinSkip && this.m_spinDecisionIndex >= 0 && this.m_spinTime > 0.1f)
		{
			if (this.m_parts != null && this.m_parts.Count > 0)
			{
				foreach (RoulettePartsBase roulettePartsBase in this.m_parts)
				{
					if (roulettePartsBase != null)
					{
						roulettePartsBase.OnSpinSkip();
					}
				}
			}
			this.m_spinSkip = true;
			this.m_spinDecision = true;
		}
		return result;
	}

	// Token: 0x06002780 RID: 10112 RVA: 0x000F4A38 File Offset: 0x000F2C38
	public bool OnRouletteSpinDecision(int decIndex)
	{
		bool result = false;
		this.m_closeTime = 0f;
		if (this.isSpin && !this.isSpinDecision)
		{
			if (decIndex >= 0)
			{
				if (this.m_backButtonImg != null)
				{
					this.m_backButtonImg.isEnabled = true;
				}
				this.m_spinDecisionIndex = decIndex;
				if (this.m_parts != null && this.m_parts.Count > 0)
				{
					foreach (RoulettePartsBase roulettePartsBase in this.m_parts)
					{
						if (roulettePartsBase != null)
						{
							roulettePartsBase.OnSpinDecision();
						}
					}
				}
				this.m_spinSkip = false;
			}
			else
			{
				if (this.m_backButtonImg != null)
				{
					this.m_backButtonImg.isEnabled = false;
				}
				this.m_multiGetDelayTime = 5f;
				if (this.m_parts != null && this.m_parts.Count > 0)
				{
					foreach (RoulettePartsBase roulettePartsBase2 in this.m_parts)
					{
						if (roulettePartsBase2 != null)
						{
							roulettePartsBase2.OnSpinDecisionMulti();
						}
					}
				}
				this.m_spinSkip = true;
			}
			this.m_spinDecision = true;
			result = true;
		}
		return result;
	}

	// Token: 0x06002781 RID: 10113 RVA: 0x000F4BD8 File Offset: 0x000F2DD8
	public bool OnRouletteSpinEnd()
	{
		HudMenuUtility.SetConnectAlertSimpleUI(true);
		this.m_inputLimitTime = 5f;
		bool result = false;
		this.m_closeTime = 0f;
		this.m_multiGetDelayTime = 0f;
		if (this.isSpin && this.isSpinDecision)
		{
			GC.Collect();
			this.SetDelayTime(0.25f);
			if (this.m_backButtonImg != null)
			{
				this.m_backButtonImg.isEnabled = true;
			}
			this.m_word = true;
			if (this.m_parts != null && this.m_parts.Count > 0)
			{
				foreach (RoulettePartsBase roulettePartsBase in this.m_parts)
				{
					if (roulettePartsBase != null)
					{
						roulettePartsBase.OnSpinEnd();
					}
				}
			}
			this.m_spin = false;
			this.m_spinSkip = false;
			this.m_spinDecision = false;
			result = true;
		}
		return result;
	}

	// Token: 0x06002782 RID: 10114 RVA: 0x000F4CF0 File Offset: 0x000F2EF0
	public bool OnRouletteWordAnimeEnd()
	{
		this.m_inputLimitTime = 0.25f;
		bool result = false;
		float delay = 0f;
		this.m_closeTime = 0f;
		this.m_multiGetDelayTime = 0f;
		if (this.m_word)
		{
			this.SetDelayTime(0.25f);
			this.m_word = false;
			HudMenuUtility.SendMsgUpdateSaveDataDisplay();
			if (this.m_spinResultGeneral != null)
			{
				if (this.m_spinResultGeneral.ItemWon >= 0 && this.m_wheelData.GetRouletteRank() != RouletteUtility.WheelRank.Super)
				{
					int num = 0;
					if (this.m_wheelData.GetCellItem(this.m_spinResultGeneral.ItemWon, out num).idType == ServerItem.IdType.ITEM_ROULLETE_WIN)
					{
						this.CloseGetWindow(RouletteUtility.AchievementType.NONE, RouletteUtility.NextType.NONE);
					}
					else
					{
						RouletteUtility.ShowGetWindow(this.m_spinResultGeneral);
						delay = 0.5f;
					}
				}
				else
				{
					RouletteUtility.ShowGetWindow(this.m_spinResultGeneral);
					delay = 0.5f;
				}
				this.m_spinResultGeneral = null;
			}
			else if (this.m_spinResult != null)
			{
				RouletteUtility.ShowGetWindow(this.m_spinResult);
				this.m_spinResult = null;
				delay = 0.5f;
			}
			else
			{
				global::Debug.Log("OnRouletteWordAnimeEnd error?");
				if (this.m_wheelData.itemWonData.idType == ServerItem.IdType.ITEM_ROULLETE_WIN && this.m_wheelData.GetRouletteRank() != RouletteUtility.WheelRank.Super)
				{
					this.CloseGetWindow(RouletteUtility.AchievementType.NONE, RouletteUtility.NextType.NONE);
				}
				else
				{
					RouletteUtility.ShowGetWindow(this.m_wheelData.GetOrgRankupData());
					delay = 0.5f;
				}
			}
			result = true;
		}
		ServerWheelOptionsData serverWheelOptionsData = RouletteManager.UpdateRoulette(this.m_wheelData.category, delay);
		if (serverWheelOptionsData != null)
		{
			this.UpdateWheelData(serverWheelOptionsData, false);
		}
		return result;
	}

	// Token: 0x06002783 RID: 10115 RVA: 0x000F4E88 File Offset: 0x000F3088
	public void OnRouletteGetError(MsgServerConnctFailed msg)
	{
		this.m_spin = false;
		this.m_spinSkip = false;
		this.m_spinDecision = false;
		this.SetDelayTime(0.5f);
		this.m_closeTime = 0.1f;
		this.m_multiGetDelayTime = 0f;
	}

	// Token: 0x06002784 RID: 10116 RVA: 0x000F4ECC File Offset: 0x000F30CC
	public bool OnRouletteSpinError(MsgServerConnctFailed msg)
	{
		global::Debug.Log("RouletteTop  OnRouletteSpinError !!!!!!!");
		bool result = false;
		this.SetDelayTime(0.5f);
		this.m_closeTime = 0.1f;
		this.m_multiGetDelayTime = 0f;
		if (this.isSpin)
		{
			if (this.m_backButtonImg != null)
			{
				this.m_backButtonImg.isEnabled = true;
			}
			if (this.m_parts != null && this.m_parts.Count > 0)
			{
				foreach (RoulettePartsBase roulettePartsBase in this.m_parts)
				{
					if (roulettePartsBase != null)
					{
						roulettePartsBase.OnSpinError();
					}
				}
			}
			this.m_spin = false;
			this.m_spinSkip = false;
			this.m_spinDecision = false;
			result = true;
		}
		return result;
	}

	// Token: 0x06002785 RID: 10117 RVA: 0x000F4FC8 File Offset: 0x000F31C8
	public void UpdateEffectSetting()
	{
		if (this.m_parts != null && this.m_parts.Count > 0)
		{
			foreach (RoulettePartsBase roulettePartsBase in this.m_parts)
			{
				if (roulettePartsBase != null)
				{
					roulettePartsBase.UpdateEffectSetting();
				}
			}
		}
	}

	// Token: 0x06002786 RID: 10118 RVA: 0x000F5058 File Offset: 0x000F3258
	public void OpenRouletteWindow()
	{
		if (base.gameObject.activeSelf && this.m_parts != null && this.m_parts.Count > 0)
		{
			this.m_isWindow = true;
			foreach (RoulettePartsBase roulettePartsBase in this.m_parts)
			{
				if (roulettePartsBase != null)
				{
					roulettePartsBase.windowOpen();
				}
			}
		}
	}

	// Token: 0x06002787 RID: 10119 RVA: 0x000F50FC File Offset: 0x000F32FC
	public void CloseRouletteWindow()
	{
		if (base.gameObject.activeSelf)
		{
			if (this.m_parts != null && this.m_parts.Count > 0)
			{
				this.m_isWindow = false;
				foreach (RoulettePartsBase roulettePartsBase in this.m_parts)
				{
					if (roulettePartsBase != null)
					{
						roulettePartsBase.windowClose();
					}
				}
			}
			if (this.m_tutorial)
			{
				if (GeneralUtil.IsNetwork())
				{
					GeneralWindow.Create(new GeneralWindow.CInfo
					{
						name = "RouletteTutorialEnd",
						buttonType = GeneralWindow.ButtonType.Ok,
						caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "end_of_tutorial_caption").text,
						message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "end_of_tutorial_text").text
					});
					string[] value = new string[1];
					SendApollo.GetTutorialValue(ApolloTutorialIndex.START_STEP6, ref value);
					this.m_sendApollo = SendApollo.CreateRequest(ApolloType.TUTORIAL_END, value);
				}
				else
				{
					GeneralUtil.ShowNoCommunication("RouletteTutorialError");
				}
				TutorialCursor.EndTutorialCursor(TutorialCursor.Type.ROULETTE_OK);
			}
		}
	}

	// Token: 0x06002788 RID: 10120 RVA: 0x000F5244 File Offset: 0x000F3444
	public void CloseGetWindow(RouletteUtility.AchievementType achievement, RouletteUtility.NextType nextType = RouletteUtility.NextType.NONE)
	{
		this.CloseRouletteWindow();
		if (nextType == RouletteUtility.NextType.NONE)
		{
			if (this.m_wheelDataAfter != null && !this.m_change)
			{
				this.UpdateWheelData(this.m_wheelDataAfter, true);
				if (!this.m_change)
				{
					this.m_wheelDataAfter = null;
				}
			}
		}
		else
		{
			this.Close(nextType);
		}
	}

	// Token: 0x06002789 RID: 10121 RVA: 0x000F52A0 File Offset: 0x000F34A0
	public void SetDelayTime(float delay = 0.2f)
	{
		if (this.m_parts != null && this.m_parts.Count > 0)
		{
			foreach (RoulettePartsBase roulettePartsBase in this.m_parts)
			{
				if (roulettePartsBase != null)
				{
					roulettePartsBase.SetDelayTime(delay);
				}
			}
		}
	}

	// Token: 0x0600278A RID: 10122 RVA: 0x000F5330 File Offset: 0x000F3530
	public void BtnInit()
	{
		if (this.m_buttonsBase != null)
		{
			if (this.m_buttons == null)
			{
				bool activeSelf = this.m_buttonsBase.activeSelf;
				this.m_buttonsBase.SetActive(true);
				for (int i = 0; i < 10; i++)
				{
					UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(this.m_buttonsBase, "Btn_" + i);
					if (!(uibuttonMessage != null))
					{
						break;
					}
					if (this.m_buttons == null)
					{
						this.m_buttons = new List<UIButtonMessage>();
					}
					uibuttonMessage.gameObject.SetActive(false);
					this.m_buttons.Add(uibuttonMessage);
				}
				this.m_buttonsBase.SetActive(activeSelf);
			}
			else if (this.m_buttons.Count > 0)
			{
				foreach (UIButtonMessage uibuttonMessage2 in this.m_buttons)
				{
					uibuttonMessage2.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x0600278B RID: 10123 RVA: 0x000F5468 File Offset: 0x000F3668
	public bool SetupTopPage(bool init = true)
	{
		if (this.m_close)
		{
			return false;
		}
		if (RouletteManager.Instance == null)
		{
			return false;
		}
		this.ResetParts();
		if (this.m_topPageObject != null)
		{
			this.m_topPageObject.SetActive(true);
		}
		if (HudMenuUtility.IsNumPlayingRouletteTutorial() && RouletteUtility.isTutorial)
		{
			TutorialCursor.StartTutorialCursor(TutorialCursor.Type.ROULETTE_TOP_PAGE);
		}
		if (this.m_topPageStorage == null && this.m_topPageObject != null)
		{
			this.m_topPageStorage = GameObjectUtil.FindChildGameObjectComponent<UIRectItemStorage>(this.m_topPageObject, "list");
		}
		if (this.m_topPageHeaderList == null && this.m_topPageObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_topPageObject, "window_header");
			if (gameObject != null)
			{
				string text = "img_{PARAM}_bg";
				for (int i = 0; i < 10; i++)
				{
					string name = text.Replace("{PARAM}", i.ToString());
					GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, name);
					if (gameObject2 != null)
					{
						if (this.m_topPageHeaderList == null)
						{
							this.m_topPageHeaderList = new List<GameObject>();
						}
						gameObject2.SetActive(false);
						this.m_topPageHeaderList.Add(gameObject2);
					}
				}
			}
		}
		else
		{
			this.SetTopPageHeaderObject();
		}
		if (this.m_topPageRouletteList != null)
		{
			this.m_topPageRouletteList.Clear();
		}
		if (this.m_topPageStorage != null)
		{
			this.m_topPageStorage.maxItemCount = (this.m_topPageStorage.maxRows = 0);
			this.m_topPageStorage.Restart();
		}
		this.m_isTopPage = true;
		RouletteUtility.ChangeRouletteHeader(RouletteCategory.ALL);
		if (this.m_backButtonImg == null)
		{
			this.m_backButtonImg = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(base.gameObject, "Btn_cmn_back");
		}
		if (this.m_buttonsBase != null)
		{
			if (this.m_buttons == null)
			{
				for (int j = 0; j < 10; j++)
				{
					UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(this.m_buttonsBase, "Btn_" + j);
					if (!(uibuttonMessage != null))
					{
						break;
					}
					if (this.m_buttons == null)
					{
						this.m_buttons = new List<UIButtonMessage>();
					}
					uibuttonMessage.gameObject.SetActive(false);
					this.m_buttons.Add(uibuttonMessage);
				}
			}
			else if (this.m_buttons.Count > 0)
			{
				foreach (UIButtonMessage uibuttonMessage2 in this.m_buttons)
				{
					uibuttonMessage2.gameObject.SetActive(false);
				}
			}
		}
		this.m_isWindow = false;
		this.m_requestCategory = RouletteCategory.NONE;
		RouletteUtility.rouletteDefault = RouletteCategory.NONE;
		if (RouletteUtility.rouletteDefault != RouletteCategory.ITEM && RouletteUtility.rouletteDefault != RouletteCategory.PREMIUM)
		{
			RouletteUtility.rouletteDefault = RouletteCategory.PREMIUM;
		}
		base.gameObject.SetActive(true);
		if (init)
		{
			if (this.m_buttonsBase != null)
			{
				this.m_buttonsBase.SetActive(false);
			}
			if (this.m_buttonsBaseBg != null)
			{
				this.m_buttonsBaseBg.SetActive(false);
			}
			RouletteManager.Instance.RequestRouletteBasicInformation(base.gameObject);
		}
		else
		{
			if (this.m_buttonsBase != null)
			{
				this.m_buttonsBase.SetActive(false);
			}
			if (this.m_buttonsBaseBg != null)
			{
				this.m_buttonsBaseBg.SetActive(false);
			}
			this.SetTopPageHeaderObject();
			this.UpdateChangeBotton(RouletteCategory.ALL);
		}
		EventManager.EventType type = EventManager.Instance.Type;
		string text2 = null;
		string cueSheetName = "BGM";
		if (type != EventManager.EventType.NUM && type != EventManager.EventType.UNKNOWN && type != EventManager.EventType.ADVERT && EventManager.Instance.IsInEvent() && EventCommonDataTable.Instance != null)
		{
			string data = EventCommonDataTable.Instance.GetData(EventCommonDataItem.Roulette_BgmName);
			if (!string.IsNullOrEmpty(data))
			{
				cueSheetName = "BGM_" + EventManager.GetEventTypeName(EventManager.Instance.Type);
				text2 = data;
			}
		}
		if (string.IsNullOrEmpty(text2))
		{
			text2 = "bgm_sys_roulette";
		}
		if (!string.IsNullOrEmpty(text2))
		{
			SoundManager.BgmChange(text2, cueSheetName);
		}
		return true;
	}

	// Token: 0x0600278C RID: 10124 RVA: 0x000F58D8 File Offset: 0x000F3AD8
	public bool Setup(RouletteCategory category)
	{
		if (this.m_close)
		{
			return false;
		}
		if (RouletteManager.Instance == null)
		{
			return false;
		}
		if (this.m_topPageObject != null)
		{
			this.m_topPageObject.SetActive(false);
		}
		if (HudMenuUtility.IsNumPlayingRouletteTutorial() && RouletteUtility.isTutorial)
		{
			TutorialCursor.EndTutorialCursor(TutorialCursor.Type.ROULETTE_TOP_PAGE);
		}
		this.m_setupNoCommunicationCategory = RouletteCategory.NONE;
		if (!GeneralUtil.IsNetwork())
		{
			this.m_setupNoCommunicationCategory = category;
			GeneralUtil.ShowNoCommunication("SetupNoCommunication");
			return false;
		}
		bool isTopPage = this.m_isTopPage;
		this.m_isTopPage = false;
		if (this.m_backButtonImg == null)
		{
			this.m_backButtonImg = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(base.gameObject, "Btn_cmn_back");
		}
		if (this.m_buttonsBase != null)
		{
			if (this.m_buttons == null)
			{
				for (int i = 0; i < 10; i++)
				{
					UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(this.m_buttonsBase, "Btn_" + i);
					if (!(uibuttonMessage != null))
					{
						break;
					}
					if (this.m_buttons == null)
					{
						this.m_buttons = new List<UIButtonMessage>();
					}
					uibuttonMessage.gameObject.SetActive(false);
					this.m_buttons.Add(uibuttonMessage);
				}
			}
			else if (this.m_buttons.Count > 0)
			{
				foreach (UIButtonMessage uibuttonMessage2 in this.m_buttons)
				{
					uibuttonMessage2.gameObject.SetActive(false);
				}
			}
		}
		if (category == RouletteCategory.SPECIAL)
		{
			category = RouletteCategory.PREMIUM;
		}
		this.m_isWindow = false;
		this.m_requestCategory = category;
		RouletteUtility.rouletteDefault = category;
		if (RouletteUtility.rouletteDefault != RouletteCategory.ITEM && RouletteUtility.rouletteDefault != RouletteCategory.PREMIUM)
		{
			RouletteUtility.rouletteDefault = RouletteCategory.PREMIUM;
		}
		if (this.isEnabled && !isTopPage)
		{
			if (this.m_buttonsBase != null)
			{
				this.m_buttonsBase.SetActive(false);
			}
			if (this.m_buttonsBaseBg != null)
			{
				this.m_buttonsBaseBg.SetActive(false);
			}
			this.SetDelayTime(1f);
			ServerWheelOptionsData rouletteData = RouletteManager.GetRouletteData(category);
			if (rouletteData != null)
			{
				this.UpdateWheelData(rouletteData, true);
				if (this.m_buttons.Count > 0)
				{
					int num = 0;
					foreach (RouletteCategory rouletteCategory in this.m_rouletteList)
					{
						if (this.m_buttons.Count <= num)
						{
							break;
						}
						this.UpdateChangeBottonIcon(rouletteCategory, this.m_buttons[num], num, rouletteCategory != category);
						num++;
					}
				}
			}
			else
			{
				this.m_updateRequest = true;
				RouletteManager.RequestRoulette(category, base.gameObject);
			}
		}
		else
		{
			if (this.m_buttonsBase != null)
			{
				this.m_buttonsBase.SetActive(false);
			}
			if (this.m_buttonsBaseBg != null)
			{
				this.m_buttonsBaseBg.SetActive(false);
			}
			if (category != RouletteCategory.ITEM)
			{
				RouletteManager.RouletteBgmReset();
			}
			RouletteManager.ResetRoulette(RouletteCategory.ALL);
			if (!isTopPage)
			{
				this.SetPanelsAlpha(0f);
			}
			base.gameObject.SetActive(true);
			this.m_updateRequest = false;
			RouletteManager.RequestRoulette(category, base.gameObject);
		}
		if (!isTopPage)
		{
			RouletteManager.Instance.RequestRouletteBasicInformation(base.gameObject);
		}
		else
		{
			this.SetTopPageHeaderObject();
			this.UpdateChangeBotton(this.m_requestCategory);
		}
		return true;
	}

	// Token: 0x0600278D RID: 10125 RVA: 0x000F5CB4 File Offset: 0x000F3EB4
	private void SetupWheelData(ServerWheelOptionsData wheelData)
	{
		this.m_setupNoCommunicationCategory = RouletteCategory.NONE;
		if (wheelData != null)
		{
			this.m_isTopPage = false;
			this.m_wheelSetup = true;
			this.m_wheelDataAfter = null;
			this.m_closeTime = 0f;
			this.m_nextType = RouletteUtility.NextType.NONE;
			this.m_spinTime = 0f;
			this.m_multiGetDelayTime = 0f;
			this.m_word = false;
			this.m_spin = false;
			this.m_spinSkip = false;
			this.m_spinDecision = false;
			this.m_spinDecisionIndex = -1;
			this.m_wheelData = wheelData;
			this.m_close = false;
			this.m_clickBack = false;
			this.SetPanelsAlpha(1f);
			base.gameObject.SetActive(true);
			if (this.m_animation != null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_mm_roulette_intro_Anim", Direction.Forward);
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.AnimationFinishCallback), true);
				if (this.m_wheelData.category == RouletteCategory.SPECIAL)
				{
					SoundManager.BgmStop();
					this.m_wheelData.PlaySe(ServerWheelOptionsData.SE_TYPE.Change, 0f);
					this.m_wheelData.PlayBgm(2.2f);
				}
				else
				{
					this.m_wheelData.PlaySe(ServerWheelOptionsData.SE_TYPE.Open, 0f);
					this.m_wheelData.PlayBgm(0.3f);
				}
			}
			this.ResetParts();
			if (this.m_rouletteBase != null && this.m_orgRouletteBoard != null)
			{
				this.CreateParts(this.m_orgRouletteBoard.gameObject, this.m_rouletteBase);
			}
			if (this.m_stdPartsBase != null && this.m_orgStdPartsBoard != null)
			{
				this.CreateParts(this.m_orgStdPartsBoard.gameObject, this.m_stdPartsBase);
			}
			RouletteUtility.ChangeRouletteHeader(this.m_wheelData.category);
			if (this.m_buttonsBase != null)
			{
				this.m_buttonsBase.SetActive(false);
			}
			if (this.m_buttonsBaseBg != null)
			{
				this.m_buttonsBaseBg.SetActive(false);
			}
			this.UpdateChangeBotton(this.m_wheelData.category);
		}
	}

	// Token: 0x0600278E RID: 10126 RVA: 0x000F5EC8 File Offset: 0x000F40C8
	public void UpdateWheel(ServerWheelOptionsData wheelData, bool changeEffect)
	{
		this.m_isTopPage = false;
		if (base.gameObject.activeSelf)
		{
			this.UpdateWheelData(wheelData, changeEffect);
		}
	}

	// Token: 0x0600278F RID: 10127 RVA: 0x000F5EEC File Offset: 0x000F40EC
	private void UpdateWheelData(ServerWheelOptionsData wheelData, bool changeEffect = true)
	{
		this.m_setupNoCommunicationCategory = RouletteCategory.NONE;
		if (wheelData != null)
		{
			this.m_isTopPage = false;
			if (wheelData.isGeneral && (this.m_rouletteCostItemLoadedList == null || !this.m_rouletteCostItemLoadedList.Contains(wheelData.category)) && ServerInterface.LoggedInServerInterface != null)
			{
				List<int> spinCostItemIdList = wheelData.GetSpinCostItemIdList();
				if (spinCostItemIdList != null && spinCostItemIdList.Count > 0)
				{
					List<int> list = new List<int>();
					foreach (int num in spinCostItemIdList)
					{
						if (num != 910000 && num != 900000)
						{
							list.Add(num);
						}
					}
					if (list.Count > 0)
					{
						this.m_requestCostItemCategory = wheelData.category;
						ServerInterface.LoggedInServerInterface.RequestServerGetItemStockNum(EventManager.Instance.Id, list, base.gameObject);
					}
				}
			}
			this.m_closeTime = 0f;
			this.m_nextType = RouletteUtility.NextType.NONE;
			this.m_spinTime = 0f;
			this.m_multiGetDelayTime = 0f;
			this.m_spin = false;
			this.m_spinSkip = false;
			this.m_spinDecision = false;
			this.m_spinDecisionIndex = -1;
			if (this.m_wheelData.category == RouletteCategory.PREMIUM && wheelData.category == RouletteCategory.SPECIAL)
			{
				SoundManager.BgmStop();
				wheelData.PlaySe(ServerWheelOptionsData.SE_TYPE.Change, 0f);
				wheelData.PlayBgm(2.2f);
				changeEffect = false;
			}
			else if (this.m_wheelData.category != RouletteCategory.SPECIAL && wheelData.category == RouletteCategory.SPECIAL)
			{
				SoundManager.BgmStop();
				wheelData.PlaySe(ServerWheelOptionsData.SE_TYPE.Change, 0f);
				wheelData.PlayBgm(2.2f);
				changeEffect = false;
			}
			else if (this.m_wheelData.category == RouletteCategory.SPECIAL && wheelData.category == RouletteCategory.PREMIUM)
			{
				wheelData.PlayBgm(0.3f);
				changeEffect = false;
			}
			else
			{
				bool flag = false;
				if (this.m_wheelData.category == RouletteCategory.SPECIAL && wheelData.category == RouletteCategory.SPECIAL && RouletteManager.Instance != null && string.IsNullOrEmpty(RouletteManager.Instance.oldBgmName))
				{
					flag = true;
				}
				if (flag)
				{
					wheelData.PlaySe(ServerWheelOptionsData.SE_TYPE.Change, 0f);
					wheelData.PlayBgm(2.2f);
					changeEffect = false;
				}
				else
				{
					wheelData.PlayBgm(0.3f);
					if (changeEffect)
					{
						wheelData.PlaySe(ServerWheelOptionsData.SE_TYPE.Open, 0f);
					}
					changeEffect = false;
				}
			}
			if (changeEffect && this.m_wheelData.category != wheelData.category)
			{
				this.m_wheelDataAfter = wheelData;
				this.m_close = false;
				this.m_clickBack = false;
				this.m_change = true;
				if (this.m_animation != null)
				{
					ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_mm_roulette_intro_Anim", Direction.Reverse);
					EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.AnimationFinishCallback), true);
				}
			}
			else
			{
				int multi = this.m_wheelData.multi;
				this.m_wheelData = new ServerWheelOptionsData(wheelData);
				this.m_wheelData.ChangeMulti(multi);
				this.UpdateChangeBotton(this.m_wheelData.category);
				this.m_wheelDataAfter = null;
				this.m_close = false;
				this.m_clickBack = false;
				this.m_change = false;
				if (this.m_parts != null && this.m_parts.Count > 0)
				{
					foreach (RoulettePartsBase roulettePartsBase in this.m_parts)
					{
						if (roulettePartsBase != null)
						{
							roulettePartsBase.OnUpdateWheelData(this.m_wheelData);
						}
					}
				}
				RouletteUtility.ChangeRouletteHeader(this.m_wheelData.category);
				this.m_wheelSetup = true;
			}
		}
	}

	// Token: 0x06002790 RID: 10128 RVA: 0x000F62F8 File Offset: 0x000F44F8
	private void ServerAddSpecialEgg_Succeeded(MsgAddSpecialEggSucceed msg)
	{
	}

	// Token: 0x06002791 RID: 10129 RVA: 0x000F62FC File Offset: 0x000F44FC
	private void ServerAddSpecialEgg_Failed(MsgServerConnctFailed msg)
	{
	}

	// Token: 0x06002792 RID: 10130 RVA: 0x000F6300 File Offset: 0x000F4500
	private void ServerGetItemStockNum_Succeeded(MsgGetItemStockNumSucceed msg)
	{
		if (this.m_rouletteCostItemLoadedList == null)
		{
			this.m_rouletteCostItemLoadedList = new List<RouletteCategory>();
		}
		if (!this.m_rouletteCostItemLoadedList.Contains(this.m_requestCostItemCategory))
		{
			this.m_rouletteCostItemLoadedList.Add(this.m_requestCostItemCategory);
		}
		if (this.m_parts != null && this.m_parts.Count > 0)
		{
			foreach (RoulettePartsBase roulettePartsBase in this.m_parts)
			{
				if (roulettePartsBase != null)
				{
					roulettePartsBase.PartsSendMessage("CostItemUpdate");
				}
			}
		}
		this.m_requestCostItemCategory = RouletteCategory.NONE;
	}

	// Token: 0x06002793 RID: 10131 RVA: 0x000F63D8 File Offset: 0x000F45D8
	public bool Close(RouletteUtility.NextType nextType = RouletteUtility.NextType.NONE)
	{
		if (this.m_close)
		{
			return false;
		}
		RouletteUtility.loginRoulette = false;
		if (this.m_rouletteCostItemLoadedList != null)
		{
			this.m_rouletteCostItemLoadedList.Clear();
		}
		this.SetDelayTime(0.25f);
		this.m_nextType = nextType;
		this.m_spinTime = 0f;
		this.m_multiGetDelayTime = 0f;
		this.m_spin = false;
		this.m_spinSkip = false;
		this.m_spinDecision = false;
		this.m_spinDecisionIndex = -1;
		if (this.m_nextType != RouletteUtility.NextType.NONE)
		{
			RouletteUtility.NextType nextType2 = this.m_nextType;
			if (nextType2 != RouletteUtility.NextType.EQUIP)
			{
				if (nextType2 == RouletteUtility.NextType.CHARA_EQUIP)
				{
					HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.CHARA_MAIN, true);
				}
			}
			else
			{
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.CHAO, true);
			}
		}
		else
		{
			this.m_close = true;
			if (this.m_animation != null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_mm_roulette_intro_Anim", Direction.Reverse);
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.AnimationFinishCallback), true);
				if (this.m_isTopPage)
				{
					SoundManager.SePlay("sys_window_close", "SE");
				}
				else if (this.m_wheelData != null)
				{
					this.m_wheelData.PlaySe(ServerWheelOptionsData.SE_TYPE.Close, 0f);
				}
			}
		}
		return true;
	}

	// Token: 0x06002794 RID: 10132 RVA: 0x000F6514 File Offset: 0x000F4714
	public void Remove()
	{
		global::Debug.Log("RouletteTop Remove!");
		this.m_closeTime = 0f;
		this.m_spinTime = 0f;
		this.m_multiGetDelayTime = 0f;
		this.m_spin = false;
		this.m_spinSkip = false;
		this.m_spinDecision = false;
		this.m_spinDecisionIndex = -1;
		this.m_opened = false;
		this.m_close = false;
		this.SetPanelsAlpha(0f);
		HudMenuUtility.SendEnableShopButton(true);
		ChaoTextureManager.Instance.RemoveChaoTexture();
		this.ResetParts();
		RouletteManager.RouletteBgmReset();
		HudMenuUtility.ChangeMainMenuBGM();
		this.m_removeTime = Time.realtimeSinceStartup;
		if (this.m_clickBack)
		{
			HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.ROULETTE_BACK, false);
		}
		this.m_clickBack = false;
	}

	// Token: 0x06002795 RID: 10133 RVA: 0x000F65C8 File Offset: 0x000F47C8
	private void CreateParts(GameObject org, GameObject parent)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(org, Vector3.zero, Quaternion.identity) as GameObject;
		gameObject.gameObject.transform.parent = parent.transform;
		gameObject.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		gameObject.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		RoulettePartsBase component = gameObject.gameObject.GetComponent<RoulettePartsBase>();
		if (component != null)
		{
			component.Setup(this);
			component.SetDelayTime(0f);
			if (this.m_parts == null)
			{
				this.m_parts = new List<RoulettePartsBase>();
			}
			this.m_parts.Add(component);
		}
	}

	// Token: 0x06002796 RID: 10134 RVA: 0x000F6698 File Offset: 0x000F4898
	private void ResetParts()
	{
		if (this.m_parts != null && this.m_parts.Count > 0)
		{
			for (int i = 0; i < this.m_parts.Count; i++)
			{
				if (this.m_parts[i] != null)
				{
					this.m_parts[i].DestroyParts();
				}
			}
			this.m_parts.Clear();
			this.m_parts = null;
		}
	}

	// Token: 0x06002797 RID: 10135 RVA: 0x000F6718 File Offset: 0x000F4918
	public static RouletteTop RouletteTopPageCreate()
	{
		if (RouletteTop.s_instance != null)
		{
			RouletteTop.s_instance.SetupTopPage(true);
			return RouletteTop.s_instance;
		}
		return null;
	}

	// Token: 0x06002798 RID: 10136 RVA: 0x000F6740 File Offset: 0x000F4940
	public static RouletteTop RouletteCreate(RouletteCategory category)
	{
		if (RouletteTop.s_instance != null && category != RouletteCategory.NONE && category != RouletteCategory.ALL)
		{
			RouletteTop.s_instance.Setup(category);
			return RouletteTop.s_instance;
		}
		return null;
	}

	// Token: 0x06002799 RID: 10137 RVA: 0x000F6774 File Offset: 0x000F4974
	public void UpdateChangeBotton(RouletteCategory current)
	{
		if (current == RouletteCategory.SPECIAL)
		{
			current = RouletteCategory.PREMIUM;
		}
		if (this.m_rouletteList != null && this.m_rouletteList.Count > 0)
		{
			if (this.m_rouletteList.Contains(RouletteCategory.RAID) && EventManager.Instance != null && EventManager.Instance.TypeInTime != EventManager.EventType.RAID_BOSS)
			{
				this.m_rouletteList.Remove(RouletteCategory.RAID);
			}
			this.SetTopPageObject();
			if (this.m_buttons != null && current != RouletteCategory.NONE && current != RouletteCategory.GENERAL)
			{
				if (current != RouletteCategory.ALL)
				{
					for (int i = 0; i < this.m_buttons.Count; i++)
					{
						this.m_buttons[i].gameObject.SetActive(false);
					}
				}
				else
				{
					for (int j = 0; j < this.m_buttons.Count; j++)
					{
						this.m_buttons[j].gameObject.SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x0600279A RID: 10138 RVA: 0x000F6878 File Offset: 0x000F4A78
	private void ResetChangeBotton()
	{
		if (this.m_rouletteList != null && this.m_rouletteList.Count > 0 && this.m_buttons != null)
		{
			for (int i = 0; i < this.m_buttons.Count; i++)
			{
				this.m_buttons[i].gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600279B RID: 10139 RVA: 0x000F68E0 File Offset: 0x000F4AE0
	private void UpdateChangeBottonIcon(RouletteCategory category, UIButtonMessage button, int idx, bool enabled)
	{
		UIImageButton component = button.GetComponent<UIImageButton>();
		if (component != null)
		{
			component.isEnabled = enabled;
		}
		if (category != RouletteCategory.PREMIUM && category != RouletteCategory.SPECIAL)
		{
			button.functionName = "OnClickChangeBtn_" + idx;
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(button.gameObject, "img_btn_icon");
			UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(button.gameObject, "img_move_icon");
			if (uisprite != null)
			{
				uisprite.gameObject.SetActive(true);
				uisprite.spriteName = RouletteUtility.GetRouletteChangeIconSpriteName(category);
			}
			if (uisprite2 != null)
			{
				uisprite2.gameObject.SetActive(false);
			}
		}
		else
		{
			button.functionName = "OnClickChangeBtn_" + idx;
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(button.gameObject, "img_btn_icon");
			UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(button.gameObject, "img_move_icon");
			if (RouletteManager.Instance.specialEgg >= 10 && !this.m_tutorial && !RouletteUtility.rouletteTurtorialEnd)
			{
				if (uisprite != null)
				{
					uisprite.gameObject.SetActive(false);
				}
				if (uisprite2 != null)
				{
					uisprite2.spriteName = RouletteUtility.GetRouletteChangeIconSpriteName(RouletteCategory.SPECIAL);
					uisprite2.gameObject.SetActive(true);
				}
			}
			else
			{
				if (uisprite != null)
				{
					uisprite.gameObject.SetActive(true);
					uisprite.spriteName = RouletteUtility.GetRouletteChangeIconSpriteName(category);
				}
				if (uisprite2 != null)
				{
					uisprite2.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x0600279C RID: 10140 RVA: 0x000F6A7C File Offset: 0x000F4C7C
	private void UpdateChangeBottonIcon(RouletteCategory category, UIButtonMessage button, int idx)
	{
		if (category != RouletteCategory.PREMIUM && category != RouletteCategory.SPECIAL)
		{
			button.functionName = "OnClickChangeBtn_" + idx;
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(button.gameObject, "img_btn_icon");
			UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(button.gameObject, "img_move_icon");
			if (uisprite != null)
			{
				uisprite.gameObject.SetActive(true);
				uisprite.spriteName = RouletteUtility.GetRouletteChangeIconSpriteName(category);
			}
			if (uisprite2 != null)
			{
				uisprite2.gameObject.SetActive(false);
			}
		}
		else
		{
			button.functionName = "OnClickChangeBtn_" + idx;
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(button.gameObject, "img_btn_icon");
			UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(button.gameObject, "img_move_icon");
			if (RouletteManager.Instance.specialEgg >= 10 && !this.m_tutorial && !RouletteUtility.rouletteTurtorialEnd)
			{
				if (uisprite != null)
				{
					uisprite.gameObject.SetActive(false);
				}
				if (uisprite2 != null)
				{
					uisprite2.spriteName = RouletteUtility.GetRouletteChangeIconSpriteName(RouletteCategory.SPECIAL);
					uisprite2.gameObject.SetActive(true);
				}
			}
			else
			{
				if (uisprite != null)
				{
					uisprite.gameObject.SetActive(true);
					uisprite.spriteName = RouletteUtility.GetRouletteChangeIconSpriteName(category);
				}
				if (uisprite2 != null)
				{
					uisprite2.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x0600279D RID: 10141 RVA: 0x000F6BFC File Offset: 0x000F4DFC
	public void RequestBasicInfo_Succeeded(List<RouletteCategory> rouletteList)
	{
		if (rouletteList != null && rouletteList.Count > 0)
		{
			this.m_rouletteList = rouletteList;
			if (this.m_buttons != null && this.m_buttons.Count > 0)
			{
				for (int i = 0; i < this.m_buttons.Count; i++)
				{
					if (rouletteList.Count > i)
					{
						this.m_buttons[i].gameObject.SetActive(true);
						bool enabled = this.m_requestCategory != this.m_rouletteList[i];
						if (this.m_isTopPage)
						{
							enabled = true;
						}
						this.UpdateChangeBottonIcon(this.m_rouletteList[i], this.m_buttons[i], i, enabled);
					}
					else
					{
						this.m_buttons[i].gameObject.SetActive(false);
					}
				}
			}
			this.SetTopPageObject();
			if (this.m_buttonsBase != null)
			{
				this.m_buttonsBase.SetActive(false);
			}
			if (this.m_buttonsBaseBg != null)
			{
				this.m_buttonsBaseBg.SetActive(false);
			}
		}
	}

	// Token: 0x0600279E RID: 10142 RVA: 0x000F6D20 File Offset: 0x000F4F20
	public void RequestBasicInfo_Failed()
	{
	}

	// Token: 0x0600279F RID: 10143 RVA: 0x000F6D24 File Offset: 0x000F4F24
	private void SetTopPageObject()
	{
		if (this.m_topPageStorage != null)
		{
			this.m_topPageStorage.maxItemCount = (this.m_topPageStorage.maxRows = this.m_rouletteList.Count);
			this.m_topPageStorage.Restart();
			this.m_topPageRouletteList = GameObjectUtil.FindChildGameObjects(this.m_topPageStorage.gameObject, "ui_rouletteTop_scroll(Clone)");
			int specialEgg = RouletteManager.Instance.specialEgg;
			if (this.m_rouletteInfoList != null)
			{
				this.m_rouletteInfoList.Clear();
			}
			if (RouletteInformationManager.Instance != null)
			{
				RouletteInformationManager.Instance.GetCurrentInfoParam(out this.m_rouletteInfoList);
			}
			if (this.m_topPageRouletteList != null)
			{
				UIDraggablePanel uidraggablePanel = GameObjectUtil.FindChildGameObjectComponent<UIDraggablePanel>(this.m_topPageObject, "ScrollView");
				if (uidraggablePanel != null)
				{
					uidraggablePanel.enabled = (!HudMenuUtility.IsNumPlayingRouletteTutorial() || !RouletteUtility.isTutorial);
				}
				for (int i = 0; i < this.m_topPageRouletteList.Count; i++)
				{
					UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_topPageRouletteList[i], "base");
					UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_topPageRouletteList[i], "Lbl_btn_roulette");
					UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_topPageRouletteList[i], "Lbl_btn_roulette_sh");
					UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(this.m_topPageRouletteList[i], "img_ad_tex");
					UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(this.m_topPageRouletteList[i], "Btn_information");
					RouletteCategory rouletteCategory = this.m_rouletteList[i];
					if (this.m_rouletteList[i] == RouletteCategory.PREMIUM && specialEgg >= 10 && !RouletteUtility.isTutorial)
					{
						rouletteCategory = RouletteCategory.SPECIAL;
					}
					ServerWheelOptionsData rouletteDataOrg = RouletteManager.Instance.GetRouletteDataOrg(rouletteCategory);
					if (rouletteDataOrg != null && this.m_isTopPage)
					{
						rouletteDataOrg.ChangeMulti(1);
					}
					if (uisprite != null)
					{
						uisprite.color = this.GetBtnColor(rouletteCategory);
					}
					if (uilabel != null && uilabel2 != null)
					{
						UILabel uilabel3 = uilabel;
						string rouletteCategoryHeaderText = RouletteUtility.GetRouletteCategoryHeaderText(rouletteCategory);
						uilabel2.text = rouletteCategoryHeaderText;
						uilabel3.text = rouletteCategoryHeaderText;
					}
					if (rouletteCategory == RouletteCategory.PREMIUM || rouletteCategory == RouletteCategory.SPECIAL)
					{
						this.m_premiumRouletteLabel = uilabel;
						this.m_premiumRouletteShLabel = uilabel2;
					}
					if (rouletteCategory != RouletteCategory.ITEM)
					{
						RouletteCategory rouletteCategory2 = RouletteCategory.NONE;
						RouletteCategory rouletteCategory3 = rouletteCategory;
						switch (rouletteCategory3)
						{
						case RouletteCategory.PREMIUM:
							goto IL_25E;
						default:
							if (rouletteCategory3 == RouletteCategory.SPECIAL)
							{
								goto IL_25E;
							}
							break;
						case RouletteCategory.RAID:
							if (this.m_rouletteInfoList.ContainsKey(RouletteCategory.RAID))
							{
								rouletteCategory2 = RouletteCategory.RAID;
							}
							break;
						}
						IL_290:
						if (rouletteCategory2 != RouletteCategory.NONE)
						{
							if (uitexture != null && RouletteInformationManager.Instance != null)
							{
								RouletteInformationManager.InfoBannerRequest bannerRequest = new RouletteInformationManager.InfoBannerRequest(uitexture);
								RouletteInformationManager.Instance.LoadInfoBaner(bannerRequest, rouletteCategory2);
							}
							GeneralUtil.SetButtonFunc(this.m_topPageRouletteList[i], "Btn_information", base.gameObject, "OnClickInfoBtn_" + rouletteCategory2);
							if (uiimageButton != null)
							{
								uiimageButton.isEnabled = true;
							}
						}
						else
						{
							GeneralUtil.SetButtonFunc(this.m_topPageRouletteList[i], "Btn_information", base.gameObject, "OnClickInfoBtn_" + RouletteCategory.ITEM);
							if (uiimageButton != null)
							{
								uiimageButton.isEnabled = false;
							}
						}
						goto IL_3BC;
						IL_25E:
						if (this.m_rouletteInfoList.ContainsKey(RouletteCategory.PREMIUM))
						{
							rouletteCategory2 = RouletteCategory.PREMIUM;
						}
						goto IL_290;
					}
					if (uitexture != null && this.m_itemRouletteDefaultTexture != null)
					{
						uitexture.mainTexture = this.m_itemRouletteDefaultTexture;
					}
					GeneralUtil.SetButtonFunc(this.m_topPageRouletteList[i], "Btn_information", base.gameObject, "OnClickDummy");
					if (uiimageButton != null)
					{
						uiimageButton.isEnabled = false;
					}
					IL_3BC:
					GeneralUtil.SetButtonFunc(this.m_topPageRouletteList[i], "Btn_all_item", base.gameObject, "OnClickOddsBtn_" + i);
					GeneralUtil.SetButtonFunc(this.m_topPageRouletteList[i], "Btn_roulette", base.gameObject, "OnClickChangeBtn_" + i);
				}
			}
		}
	}

	// Token: 0x060027A0 RID: 10144 RVA: 0x000F715C File Offset: 0x000F535C
	private void SetTopPageHeaderObject()
	{
		if (this.m_topPageHeaderList != null && this.m_topPageHeaderList.Count > 0 && RouletteManager.Instance != null)
		{
			List<ServerItem.Id> rouletteCostItemIdList = RouletteManager.Instance.rouletteCostItemIdList;
			Dictionary<ServerItem.Id, string> dictionary = new Dictionary<ServerItem.Id, string>();
			dictionary.Add(ServerItem.Id.ROULLETE_TICKET_ITEM, "ui_roulette_ticket_2");
			dictionary.Add(ServerItem.Id.ROULLETE_TICKET_PREMIAM, "ui_roulette_ticket_1");
			dictionary.Add(ServerItem.Id.SPECIAL_EGG, "ui_result_special_egg");
			dictionary.Add(ServerItem.Id.RAIDRING, "ui_event_ring_icon");
			int num = 88;
			int num2 = 108;
			float num3 = Mathf.Sqrt((float)(num * num2));
			for (int i = 0; i < this.m_topPageHeaderList.Count; i++)
			{
				GameObject gameObject = this.m_topPageHeaderList[i];
				if (gameObject != null && rouletteCostItemIdList.Count > i)
				{
					ServerItem.Id id = rouletteCostItemIdList[i];
					long itemCount = GeneralUtil.GetItemCount(id);
					if (id == ServerItem.Id.SPECIAL_EGG && this.m_premiumRouletteLabel != null && this.m_premiumRouletteShLabel != null)
					{
						if (itemCount >= 10L && !RouletteUtility.isTutorial)
						{
							UILabel premiumRouletteLabel = this.m_premiumRouletteLabel;
							string rouletteCategoryHeaderText = RouletteUtility.GetRouletteCategoryHeaderText(RouletteCategory.SPECIAL);
							this.m_premiumRouletteShLabel.text = rouletteCategoryHeaderText;
							premiumRouletteLabel.text = rouletteCategoryHeaderText;
						}
						else
						{
							UILabel premiumRouletteLabel2 = this.m_premiumRouletteLabel;
							string rouletteCategoryHeaderText = RouletteUtility.GetRouletteCategoryHeaderText(RouletteCategory.PREMIUM);
							this.m_premiumRouletteShLabel.text = rouletteCategoryHeaderText;
							premiumRouletteLabel2.text = rouletteCategoryHeaderText;
						}
					}
					UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_icon");
					UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_num");
					if (uisprite != null)
					{
						if (dictionary.ContainsKey(id))
						{
							uisprite.spriteName = dictionary[id];
							UISpriteData atlasSprite = uisprite.GetAtlasSprite();
							if (atlasSprite != null)
							{
								int width = atlasSprite.width;
								int height = atlasSprite.height;
								float num4 = Mathf.Sqrt((float)(width * height));
								float num5 = num3 / num4;
								uisprite.width = (int)((float)width * num5);
								uisprite.height = (int)((float)height * num5);
							}
						}
						else
						{
							uisprite.spriteName = string.Empty;
						}
					}
					if (uilabel != null)
					{
						uilabel.text = HudUtility.GetFormatNumString<long>(itemCount);
					}
					gameObject.SetActive(true);
				}
				else
				{
					gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x060027A1 RID: 10145 RVA: 0x000F73B8 File Offset: 0x000F55B8
	public void RequestGetRoulette_Succeeded(ServerWheelOptionsData wheelData)
	{
		this.m_tutorial = false;
		if (this.m_topPageOddsSelect == RouletteCategory.NONE)
		{
			if (wheelData != null)
			{
				if (RouletteUtility.isTutorial && wheelData.category == RouletteCategory.PREMIUM)
				{
					this.m_tutorial = true;
					GeneralWindow.Create(new GeneralWindow.CInfo
					{
						name = "RouletteTutorial",
						buttonType = GeneralWindow.ButtonType.Ok,
						caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "roulette_move_explan_caption").text,
						message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "roulette_move_explan").text
					});
					string[] value = new string[1];
					SendApollo.GetTutorialValue(ApolloTutorialIndex.START_STEP6, ref value);
					this.m_sendApollo = SendApollo.CreateRequest(ApolloType.TUTORIAL_START, value);
				}
				if (this.m_updateRequest)
				{
					this.SetDelayTime(0.25f);
					this.UpdateWheelData(wheelData, true);
					this.m_updateRequest = false;
				}
				else
				{
					this.SetupWheelData(wheelData);
				}
			}
		}
		else if (wheelData.category == RouletteCategory.ITEM)
		{
			RouletteManager.Instance.RequestRoulettePrizeOrg(this.m_topPageOddsSelect, base.gameObject);
		}
		else
		{
			this.m_topPageOddsSelect = wheelData.category;
			this.m_topPageWheelData = true;
		}
	}

	// Token: 0x060027A2 RID: 10146 RVA: 0x000F74E4 File Offset: 0x000F56E4
	public void RequestCommitRoulette_Succeeded(ServerWheelOptionsData wheelData)
	{
		if (wheelData != null)
		{
			if (this.m_tutorial)
			{
				this.m_addSpecialEgg = true;
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null)
				{
					loggedInServerInterface.RequestServerAddSpecialEgg(10, base.gameObject);
				}
			}
			RouletteManager instance = RouletteManager.Instance;
			this.m_wheelDataAfter = wheelData;
			if (instance != null)
			{
				this.m_spinResultGeneral = instance.GetResult();
				if (this.m_spinResultGeneral != null)
				{
					this.m_spinResult = null;
					this.OnRouletteSpinDecision(this.m_spinResultGeneral.ItemWon);
				}
				else
				{
					this.m_spinResult = instance.GetResultChao();
					this.OnRouletteSpinDecision(this.m_spinResult.ItemWon);
				}
			}
		}
	}

	// Token: 0x060027A3 RID: 10147 RVA: 0x000F7598 File Offset: 0x000F5798
	private void OnClickBack()
	{
		if (this.m_isTopPage)
		{
			if (!this.m_spin && this.m_closeTime <= 0f)
			{
				HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.ROULETTE_BACK, false);
			}
		}
		else
		{
			SoundManager.SePlay("sys_window_close", "SE");
			this.SetupTopPage(false);
		}
	}

	// Token: 0x060027A4 RID: 10148 RVA: 0x000F75F0 File Offset: 0x000F57F0
	public void OnClickOddsBtn_0()
	{
		this.OnClickOddsBtn(0);
	}

	// Token: 0x060027A5 RID: 10149 RVA: 0x000F75FC File Offset: 0x000F57FC
	public void OnClickOddsBtn_1()
	{
		this.OnClickOddsBtn(1);
	}

	// Token: 0x060027A6 RID: 10150 RVA: 0x000F7608 File Offset: 0x000F5808
	public void OnClickOddsBtn_2()
	{
		this.OnClickOddsBtn(2);
	}

	// Token: 0x060027A7 RID: 10151 RVA: 0x000F7614 File Offset: 0x000F5814
	public void OnClickOddsBtn_3()
	{
		this.OnClickOddsBtn(3);
	}

	// Token: 0x060027A8 RID: 10152 RVA: 0x000F7620 File Offset: 0x000F5820
	public void OnClickOddsBtn_4()
	{
		this.OnClickOddsBtn(4);
	}

	// Token: 0x060027A9 RID: 10153 RVA: 0x000F762C File Offset: 0x000F582C
	public void OnClickOddsBtn_5()
	{
		this.OnClickOddsBtn(5);
	}

	// Token: 0x060027AA RID: 10154 RVA: 0x000F7638 File Offset: 0x000F5838
	public void OnClickOddsBtn_6()
	{
		this.OnClickOddsBtn(6);
	}

	// Token: 0x060027AB RID: 10155 RVA: 0x000F7644 File Offset: 0x000F5844
	public void OnClickOddsBtn_7()
	{
		this.OnClickOddsBtn(7);
	}

	// Token: 0x060027AC RID: 10156 RVA: 0x000F7650 File Offset: 0x000F5850
	public void OnClickOddsBtn_8()
	{
		this.OnClickOddsBtn(8);
	}

	// Token: 0x060027AD RID: 10157 RVA: 0x000F765C File Offset: 0x000F585C
	public void OnClickOddsBtn_9()
	{
		this.OnClickOddsBtn(9);
	}

	// Token: 0x060027AE RID: 10158 RVA: 0x000F7668 File Offset: 0x000F5868
	private void OnClickOddsBtn(int index)
	{
		global::Debug.Log("OnClickOddsBtn  " + index);
		if (RouletteManager.Instance != null && this.m_rouletteList != null && this.m_rouletteList.Count > index)
		{
			RouletteCategory rouletteCategory = this.m_rouletteList[index];
			if (rouletteCategory == RouletteCategory.SPECIAL && RouletteUtility.isTutorial)
			{
				rouletteCategory = RouletteCategory.PREMIUM;
			}
			this.m_topPageOddsSelect = rouletteCategory;
			this.m_topPageWheelData = false;
			ServerWheelOptionsData rouletteDataOrg = RouletteManager.Instance.GetRouletteDataOrg(rouletteCategory);
			global::Debug.Log(string.Concat(new object[]
			{
				"OnClickOddsBtn data:",
				rouletteDataOrg != null,
				" categ:",
				rouletteCategory,
				" !!!!!!"
			}));
			if (rouletteDataOrg != null)
			{
				RouletteManager.Instance.RequestRoulettePrizeOrg(this.m_topPageOddsSelect, base.gameObject);
			}
			else if (GeneralUtil.IsNetwork())
			{
				RouletteManager.Instance.RequestRouletteOrg(this.m_topPageOddsSelect, base.gameObject);
			}
			else
			{
				this.m_topPageOddsSelect = RouletteCategory.NONE;
				this.m_topPageWheelData = false;
				GeneralUtil.ShowNoCommunication("ShowNoCommunication");
			}
		}
	}

	// Token: 0x060027AF RID: 10159 RVA: 0x000F7790 File Offset: 0x000F5990
	private void RequestRoulettePrize_Succeeded(ServerPrizeState prize)
	{
		ServerWheelOptionsData rouletteDataOrg = RouletteManager.Instance.GetRouletteDataOrg(this.m_topPageOddsSelect);
		this.OpenOdds(prize, rouletteDataOrg);
		this.m_topPageOddsSelect = RouletteCategory.NONE;
	}

	// Token: 0x060027B0 RID: 10160 RVA: 0x000F77C0 File Offset: 0x000F59C0
	public void OnClickCurrentRouletteBanner()
	{
		RouletteCategory rouletteCategory = this.m_wheelData.category;
		if (rouletteCategory == RouletteCategory.SPECIAL)
		{
			rouletteCategory = RouletteCategory.PREMIUM;
		}
		this.OnClickInfoBtn(rouletteCategory);
	}

	// Token: 0x060027B1 RID: 10161 RVA: 0x000F77EC File Offset: 0x000F59EC
	public void OnClickInfoBtn_ITEM()
	{
		this.OnClickInfoBtn(RouletteCategory.ITEM);
		global::Debug.Log("OnClickInfoBtn_ITEM  !!!!!!!!!");
	}

	// Token: 0x060027B2 RID: 10162 RVA: 0x000F7800 File Offset: 0x000F5A00
	public void OnClickInfoBtn_PREMIUM()
	{
		this.OnClickInfoBtn(RouletteCategory.PREMIUM);
	}

	// Token: 0x060027B3 RID: 10163 RVA: 0x000F780C File Offset: 0x000F5A0C
	public void OnClickInfoBtn_SPECIAL()
	{
		this.OnClickInfoBtn(RouletteCategory.PREMIUM);
	}

	// Token: 0x060027B4 RID: 10164 RVA: 0x000F7818 File Offset: 0x000F5A18
	public void OnClickInfoBtn_RAID()
	{
		this.OnClickInfoBtn(RouletteCategory.RAID);
	}

	// Token: 0x060027B5 RID: 10165 RVA: 0x000F7824 File Offset: 0x000F5A24
	public void OnClickInfoBtn_EVENT()
	{
		this.OnClickInfoBtn(RouletteCategory.EVENT);
	}

	// Token: 0x060027B6 RID: 10166 RVA: 0x000F7830 File Offset: 0x000F5A30
	private void OnClickInfoBtn(RouletteCategory category)
	{
		if (this.m_rouletteInfoList != null && this.m_rouletteInfoList.ContainsKey(category))
		{
			RouletteInformationUtility.ShowNewsWindow(this.m_rouletteInfoList[category]);
		}
		global::Debug.Log("OnClickInfoBtn  " + category);
	}

	// Token: 0x060027B7 RID: 10167 RVA: 0x000F7880 File Offset: 0x000F5A80
	public void OnClickChangeBtn_0()
	{
		this.OnClickChangeBtn(0);
	}

	// Token: 0x060027B8 RID: 10168 RVA: 0x000F788C File Offset: 0x000F5A8C
	public void OnClickChangeBtn_1()
	{
		this.OnClickChangeBtn(1);
	}

	// Token: 0x060027B9 RID: 10169 RVA: 0x000F7898 File Offset: 0x000F5A98
	public void OnClickChangeBtn_2()
	{
		this.OnClickChangeBtn(2);
	}

	// Token: 0x060027BA RID: 10170 RVA: 0x000F78A4 File Offset: 0x000F5AA4
	public void OnClickChangeBtn_3()
	{
		this.OnClickChangeBtn(3);
	}

	// Token: 0x060027BB RID: 10171 RVA: 0x000F78B0 File Offset: 0x000F5AB0
	public void OnClickChangeBtn_4()
	{
		this.OnClickChangeBtn(4);
	}

	// Token: 0x060027BC RID: 10172 RVA: 0x000F78BC File Offset: 0x000F5ABC
	public void OnClickChangeBtn_5()
	{
		this.OnClickChangeBtn(5);
	}

	// Token: 0x060027BD RID: 10173 RVA: 0x000F78C8 File Offset: 0x000F5AC8
	public void OnClickChangeBtn_6()
	{
		this.OnClickChangeBtn(6);
	}

	// Token: 0x060027BE RID: 10174 RVA: 0x000F78D4 File Offset: 0x000F5AD4
	public void OnClickChangeBtn_7()
	{
		this.OnClickChangeBtn(7);
	}

	// Token: 0x060027BF RID: 10175 RVA: 0x000F78E0 File Offset: 0x000F5AE0
	public void OnClickChangeBtn_8()
	{
		this.OnClickChangeBtn(8);
	}

	// Token: 0x060027C0 RID: 10176 RVA: 0x000F78EC File Offset: 0x000F5AEC
	public void OnClickChangeBtn_9()
	{
		this.OnClickChangeBtn(9);
	}

	// Token: 0x060027C1 RID: 10177 RVA: 0x000F78F8 File Offset: 0x000F5AF8
	private void OnClickChangeBtn(int index)
	{
		if (GeneralUtil.IsNetwork())
		{
			if (this.m_rouletteList != null && this.m_rouletteList.Count > index)
			{
				if (this.m_rouletteList[index] == RouletteCategory.RAID)
				{
					if (EventManager.Instance != null)
					{
						EventManager.EventType typeInTime = EventManager.Instance.TypeInTime;
						if (typeInTime != EventManager.EventType.RAID_BOSS)
						{
							this.m_rouletteList.Remove(RouletteCategory.RAID);
							GeneralUtil.ShowEventEnd("ShowEventEnd");
							if (this.m_requestCategory == RouletteCategory.RAID)
							{
								this.Setup(this.m_rouletteList[0]);
							}
							else
							{
								this.ResetChangeBotton();
							}
						}
						else
						{
							this.Setup(this.m_rouletteList[index]);
						}
					}
					else
					{
						this.Setup(this.m_rouletteList[index]);
					}
				}
				else
				{
					this.Setup(this.m_rouletteList[index]);
				}
			}
		}
		else
		{
			GeneralUtil.ShowNoCommunication("ShowNoCommunication");
		}
	}

	// Token: 0x060027C2 RID: 10178 RVA: 0x000F79F8 File Offset: 0x000F5BF8
	private void AnimationFinishCallback()
	{
		if (this.m_close)
		{
			this.Remove();
		}
		else if (this.m_change && this.m_wheelDataAfter != null)
		{
			this.UpdateWheelData(this.m_wheelDataAfter, false);
			this.m_wheelDataAfter = null;
			this.m_change = false;
			if (this.m_animation != null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_mm_roulette_intro_Anim", Direction.Forward);
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.AnimationFinishCallback), true);
				this.m_wheelData.PlaySe(ServerWheelOptionsData.SE_TYPE.Open, 0f);
			}
		}
	}

	// Token: 0x1700053C RID: 1340
	// (get) Token: 0x060027C3 RID: 10179 RVA: 0x000F7A98 File Offset: 0x000F5C98
	public static RouletteTop Instance
	{
		get
		{
			return RouletteTop.s_instance;
		}
	}

	// Token: 0x060027C4 RID: 10180 RVA: 0x000F7AA0 File Offset: 0x000F5CA0
	private void Awake()
	{
		this.SetInstance();
		if (RouletteTop.s_instance != null)
		{
			this.SetPanelsAlpha(0f);
		}
	}

	// Token: 0x060027C5 RID: 10181 RVA: 0x000F7AC4 File Offset: 0x000F5CC4
	private void OnDestroy()
	{
		if (RouletteTop.s_instance == this)
		{
			this.RemoveBackKeyCallBack();
			RouletteTop.s_instance = null;
		}
	}

	// Token: 0x060027C6 RID: 10182 RVA: 0x000F7AE4 File Offset: 0x000F5CE4
	private void SetInstance()
	{
		if (RouletteTop.s_instance == null)
		{
			this.EntryBackKeyCallBack();
			RouletteTop.s_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060027C7 RID: 10183 RVA: 0x000F7B20 File Offset: 0x000F5D20
	private void EntryBackKeyCallBack()
	{
		BackKeyManager.AddWindowCallBack(base.gameObject);
	}

	// Token: 0x060027C8 RID: 10184 RVA: 0x000F7B30 File Offset: 0x000F5D30
	private void RemoveBackKeyCallBack()
	{
		BackKeyManager.RemoveWindowCallBack(base.gameObject);
	}

	// Token: 0x060027C9 RID: 10185 RVA: 0x000F7B40 File Offset: 0x000F5D40
	public void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (base.gameObject.activeSelf)
		{
			bool flag = false;
			if (this.m_panels != null && this.m_parts != null && this.m_parts.Count > 0)
			{
				foreach (UIPanel uipanel in this.m_panels)
				{
					if (uipanel.alpha > 0.1f)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				if (msg != null)
				{
					msg.StaySequence();
				}
				if (!this.m_spin && this.m_wheelSetup && !GeneralWindow.Created)
				{
					ItemGetWindow itemGetWindow = ItemGetWindowUtil.GetItemGetWindow();
					if (((itemGetWindow != null && itemGetWindow.IsEnd) || itemGetWindow == null) && !GeneralWindow.Created && !EventBestChaoWindow.Created && !this.m_isWindow && !this.m_tutorial)
					{
						if (this.m_isTopPage)
						{
							HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.ROULETTE_BACK, false);
						}
						else
						{
							this.SetupTopPage(false);
							SoundManager.SePlay("sys_window_close", "SE");
						}
					}
				}
			}
		}
	}

	// Token: 0x060027CA RID: 10186 RVA: 0x000F7CA4 File Offset: 0x000F5EA4
	private void UpdateDebug()
	{
	}

	// Token: 0x040023A7 RID: 9127
	[Header("ルーレット種別ごとのカラー設定")]
	[SerializeField]
	private Color m_premiumColor;

	// Token: 0x040023A8 RID: 9128
	[SerializeField]
	private Color m_specialColor;

	// Token: 0x040023A9 RID: 9129
	[SerializeField]
	private Color m_defaultColor;

	// Token: 0x040023AA RID: 9130
	[SerializeField]
	private RouletteBoard m_orgRouletteBoard;

	// Token: 0x040023AB RID: 9131
	[SerializeField]
	private RouletteStandardPart m_orgStdPartsBoard;

	// Token: 0x040023AC RID: 9132
	[SerializeField]
	private Animation m_animation;

	// Token: 0x040023AD RID: 9133
	[SerializeField]
	private List<UIPanel> m_panels;

	// Token: 0x040023AE RID: 9134
	[SerializeField]
	private GameObject m_topPageObject;

	// Token: 0x040023AF RID: 9135
	[SerializeField]
	private GameObject m_rouletteBase;

	// Token: 0x040023B0 RID: 9136
	[SerializeField]
	private GameObject m_stdPartsBase;

	// Token: 0x040023B1 RID: 9137
	[SerializeField]
	private GameObject m_buttonsBase;

	// Token: 0x040023B2 RID: 9138
	[SerializeField]
	private GameObject m_buttonsBaseBg;

	// Token: 0x040023B3 RID: 9139
	[SerializeField]
	private window_odds m_odds;

	// Token: 0x040023B4 RID: 9140
	[SerializeField]
	public Texture m_itemRouletteDefaultTexture;

	// Token: 0x040023B5 RID: 9141
	private bool m_updateRequest;

	// Token: 0x040023B6 RID: 9142
	private bool m_close;

	// Token: 0x040023B7 RID: 9143
	private bool m_change;

	// Token: 0x040023B8 RID: 9144
	private bool m_opened;

	// Token: 0x040023B9 RID: 9145
	private ServerWheelOptionsData m_wheelData;

	// Token: 0x040023BA RID: 9146
	private ServerWheelOptionsData m_wheelDataAfter;

	// Token: 0x040023BB RID: 9147
	private List<RoulettePartsBase> m_parts;

	// Token: 0x040023BC RID: 9148
	private List<UIButtonMessage> m_buttons;

	// Token: 0x040023BD RID: 9149
	private bool m_tutorial;

	// Token: 0x040023BE RID: 9150
	private bool m_tutorialSpin;

	// Token: 0x040023BF RID: 9151
	private bool m_addSpecialEgg;

	// Token: 0x040023C0 RID: 9152
	private bool m_word;

	// Token: 0x040023C1 RID: 9153
	private bool m_spin;

	// Token: 0x040023C2 RID: 9154
	private long m_spinCount;

	// Token: 0x040023C3 RID: 9155
	private bool m_spinSkip;

	// Token: 0x040023C4 RID: 9156
	private bool m_spinDecision;

	// Token: 0x040023C5 RID: 9157
	private int m_spinDecisionIndex = -1;

	// Token: 0x040023C6 RID: 9158
	private float m_spinTime;

	// Token: 0x040023C7 RID: 9159
	private float m_multiGetDelayTime;

	// Token: 0x040023C8 RID: 9160
	private float m_closeTime;

	// Token: 0x040023C9 RID: 9161
	private float m_removeTime;

	// Token: 0x040023CA RID: 9162
	private bool m_wheelSetup;

	// Token: 0x040023CB RID: 9163
	private List<RouletteCategory> m_rouletteList;

	// Token: 0x040023CC RID: 9164
	private List<RouletteCategory> m_rouletteCostItemLoadedList;

	// Token: 0x040023CD RID: 9165
	private RouletteCategory m_requestCostItemCategory;

	// Token: 0x040023CE RID: 9166
	private ServerChaoSpinResult m_spinResult;

	// Token: 0x040023CF RID: 9167
	private ServerSpinResultGeneral m_spinResultGeneral;

	// Token: 0x040023D0 RID: 9168
	private RouletteUtility.NextType m_nextType;

	// Token: 0x040023D1 RID: 9169
	private RouletteCategory m_requestCategory;

	// Token: 0x040023D2 RID: 9170
	private RouletteCategory m_setupNoCommunicationCategory;

	// Token: 0x040023D3 RID: 9171
	private UIImageButton m_backButtonImg;

	// Token: 0x040023D4 RID: 9172
	private List<RouletteTop.ROULETTE_EFFECT_TYPE> m_notEffectList;

	// Token: 0x040023D5 RID: 9173
	private bool m_clickBack;

	// Token: 0x040023D6 RID: 9174
	private UIRectItemStorage m_topPageStorage;

	// Token: 0x040023D7 RID: 9175
	private List<GameObject> m_topPageRouletteList;

	// Token: 0x040023D8 RID: 9176
	private List<GameObject> m_topPageHeaderList;

	// Token: 0x040023D9 RID: 9177
	private RouletteCategory m_topPageOddsSelect;

	// Token: 0x040023DA RID: 9178
	private bool m_topPageWheelData;

	// Token: 0x040023DB RID: 9179
	private Dictionary<RouletteCategory, InformationWindow.Information> m_rouletteInfoList;

	// Token: 0x040023DC RID: 9180
	private UILabel m_premiumRouletteLabel;

	// Token: 0x040023DD RID: 9181
	private UILabel m_premiumRouletteShLabel;

	// Token: 0x040023DE RID: 9182
	private SendApollo m_sendApollo;

	// Token: 0x040023DF RID: 9183
	private float m_inputLimitTime;

	// Token: 0x040023E0 RID: 9184
	private bool m_isWindow;

	// Token: 0x040023E1 RID: 9185
	private bool m_isTopPage;

	// Token: 0x040023E2 RID: 9186
	private static RouletteTop s_instance;

	// Token: 0x02000517 RID: 1303
	public enum ROULETTE_EFFECT_TYPE
	{
		// Token: 0x040023E4 RID: 9188
		BG_PARTICLE,
		// Token: 0x040023E5 RID: 9189
		SPIN,
		// Token: 0x040023E6 RID: 9190
		BOARD,
		// Token: 0x040023E7 RID: 9191
		NUM
	}
}
