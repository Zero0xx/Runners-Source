using System;
using System.Collections.Generic;
using DataTable;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000A4E RID: 2638
public class RouletteManager : CustomGameObject
{
	// Token: 0x1700098C RID: 2444
	// (get) Token: 0x060046C0 RID: 18112 RVA: 0x00172528 File Offset: 0x00170728
	// (set) Token: 0x060046C1 RID: 18113 RVA: 0x00172530 File Offset: 0x00170730
	public static int numJackpotRing
	{
		get
		{
			return RouletteManager.s_numJackpotRing;
		}
		set
		{
			RouletteManager.s_numJackpotRing = value;
		}
	}

	// Token: 0x1700098D RID: 2445
	// (get) Token: 0x060046C2 RID: 18114 RVA: 0x00172538 File Offset: 0x00170738
	// (set) Token: 0x060046C3 RID: 18115 RVA: 0x00172540 File Offset: 0x00170740
	public static bool isShowGetWindow
	{
		get
		{
			return RouletteManager.s_isShowGetWindow;
		}
		set
		{
			RouletteManager.s_isShowGetWindow = value;
		}
	}

	// Token: 0x1700098E RID: 2446
	// (get) Token: 0x060046C4 RID: 18116 RVA: 0x00172548 File Offset: 0x00170748
	// (set) Token: 0x060046C5 RID: 18117 RVA: 0x00172550 File Offset: 0x00170750
	public static bool isMultiGetWindow
	{
		get
		{
			return RouletteManager.s_multiGetWindow;
		}
		set
		{
			RouletteManager.s_multiGetWindow = value;
		}
	}

	// Token: 0x1700098F RID: 2447
	// (get) Token: 0x060046C6 RID: 18118 RVA: 0x00172558 File Offset: 0x00170758
	public bool isCurrentPrizeLoading
	{
		get
		{
			return this.m_isCurrentPrizeLoading != RouletteCategory.NONE || this.m_isCurrentPrizeLoadingAuto != RouletteCategory.NONE;
		}
	}

	// Token: 0x17000990 RID: 2448
	// (get) Token: 0x060046C7 RID: 18119 RVA: 0x00172578 File Offset: 0x00170778
	public List<RouletteCategory> rouletteCategorys
	{
		get
		{
			return this.m_basicRouletteCategorys;
		}
	}

	// Token: 0x17000991 RID: 2449
	// (get) Token: 0x060046C8 RID: 18120 RVA: 0x00172580 File Offset: 0x00170780
	// (set) Token: 0x060046C9 RID: 18121 RVA: 0x00172590 File Offset: 0x00170790
	public int specialEgg
	{
		get
		{
			return (int)GeneralUtil.GetItemCount(ServerItem.Id.SPECIAL_EGG);
		}
		set
		{
			GeneralUtil.SetItemCount(ServerItem.Id.SPECIAL_EGG, (long)value);
		}
	}

	// Token: 0x17000992 RID: 2450
	// (get) Token: 0x060046CA RID: 18122 RVA: 0x001725A0 File Offset: 0x001707A0
	public bool currentRankup
	{
		get
		{
			return this.m_currentRankup;
		}
	}

	// Token: 0x17000993 RID: 2451
	// (get) Token: 0x060046CB RID: 18123 RVA: 0x001725A8 File Offset: 0x001707A8
	public List<ServerItem.Id> rouletteCostItemIdList
	{
		get
		{
			return this.m_rouletteCostItemIdList;
		}
	}

	// Token: 0x060046CC RID: 18124 RVA: 0x001725B0 File Offset: 0x001707B0
	public void InitRouletteRequest(RouletteManager.CallbackRouletteInit callback = null)
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			this.m_initReq = true;
			this.m_initReqCallback = callback;
			if (!this.RequestRouletteOrg(RouletteCategory.PREMIUM, null))
			{
				callback(0);
				this.m_initReq = false;
				this.m_initReqCallback = null;
			}
		}
		else if (callback != null)
		{
			callback(0);
		}
	}

	// Token: 0x060046CD RID: 18125 RVA: 0x00172614 File Offset: 0x00170814
	private bool RouletteOpenOrg(RouletteCategory category)
	{
		this.m_isCurrentPrizeLoadingAuto = RouletteCategory.NONE;
		bool flag = GeneralUtil.IsNetwork();
		if (this.m_networkError && flag)
		{
			this.m_networkError = false;
		}
		if (!flag)
		{
			if (RouletteTop.Instance != null)
			{
				RouletteTop.Instance.BtnInit();
			}
			this.m_networkError = true;
		}
		this.m_rouletteTop = RouletteTop.RouletteTopPageCreate();
		return this.m_rouletteTop != null;
	}

	// Token: 0x060046CE RID: 18126 RVA: 0x00172684 File Offset: 0x00170884
	private bool RouletteCloseOrg()
	{
		this.m_isCurrentPrizeLoadingAuto = RouletteCategory.NONE;
		return this.m_rouletteTop != null && RouletteManager.IsRouletteEnabled() && this.m_rouletteTop.Close(RouletteUtility.NextType.NONE);
	}

	// Token: 0x060046CF RID: 18127 RVA: 0x001726C4 File Offset: 0x001708C4
	private void GetWindowClose(RouletteUtility.AchievementType achievement, RouletteUtility.NextType nextType = RouletteUtility.NextType.NONE)
	{
		if (this.m_rouletteTop != null)
		{
			if (achievement != RouletteUtility.AchievementType.Multi)
			{
				this.UpdateChangeBotton(RouletteCategory.NONE);
			}
			this.m_rouletteTop.CloseGetWindow(achievement, nextType);
		}
	}

	// Token: 0x060046D0 RID: 18128 RVA: 0x00172700 File Offset: 0x00170900
	public void UpdateChangeBotton(RouletteCategory target = RouletteCategory.NONE)
	{
		if (this.m_rouletteTop != null)
		{
			if (target != RouletteCategory.NONE)
			{
				global::Debug.Log("UpdateChangeBotton target:" + target + " !!!!!!!!!!!!!!!!!! ");
				this.m_rouletteTop.UpdateChangeBotton(target);
			}
			else if (this.m_lastCommitCategory != RouletteCategory.NONE)
			{
				this.m_rouletteTop.UpdateChangeBotton(this.m_lastCommitCategory);
			}
		}
	}

	// Token: 0x060046D1 RID: 18129 RVA: 0x0017276C File Offset: 0x0017096C
	private bool IsRouletteEnabledOrg()
	{
		bool flag = false;
		if (this.m_rouletteTop != null)
		{
			flag = this.m_rouletteTop.gameObject.activeSelf;
			if (flag)
			{
				float panelsAlpha = this.m_rouletteTop.GetPanelsAlpha();
				if (panelsAlpha == 0f)
				{
					flag = false;
				}
			}
		}
		return flag;
	}

	// Token: 0x060046D2 RID: 18130 RVA: 0x001727C0 File Offset: 0x001709C0
	private bool OpenRouletteWindowOrg()
	{
		bool result = false;
		if (this.m_rouletteTop != null)
		{
			this.m_rouletteTop.OpenRouletteWindow();
			result = true;
		}
		return result;
	}

	// Token: 0x060046D3 RID: 18131 RVA: 0x001727F0 File Offset: 0x001709F0
	private bool CloseRouletteWindowOrg()
	{
		bool result = false;
		if (this.m_rouletteTop != null)
		{
			this.m_rouletteTop.CloseRouletteWindow();
			result = true;
		}
		return result;
	}

	// Token: 0x060046D4 RID: 18132 RVA: 0x00172820 File Offset: 0x00170A20
	public static bool RouletteOpen(RouletteCategory category = RouletteCategory.NONE)
	{
		if (category == RouletteCategory.NONE || category == RouletteCategory.ALL)
		{
			category = RouletteUtility.rouletteDefault;
		}
		if (category == RouletteCategory.RAID && EventManager.Instance != null)
		{
			EventManager.EventType typeInTime = EventManager.Instance.TypeInTime;
			if (typeInTime != EventManager.EventType.RAID_BOSS)
			{
				RouletteUtility.rouletteDefault = RouletteCategory.PREMIUM;
				category = RouletteUtility.rouletteDefault;
			}
		}
		return RouletteManager.s_instance != null && RouletteManager.s_instance.RouletteOpenOrg(category);
	}

	// Token: 0x060046D5 RID: 18133 RVA: 0x00172898 File Offset: 0x00170A98
	public static bool RouletteClose()
	{
		bool result = false;
		if (RouletteManager.s_instance != null)
		{
			RouletteManager.s_instance.RouletteCloseOrg();
		}
		return result;
	}

	// Token: 0x060046D6 RID: 18134 RVA: 0x001728C4 File Offset: 0x00170AC4
	public static bool IsRouletteClose()
	{
		bool result = false;
		if (RouletteManager.s_instance != null && RouletteManager.s_instance.m_rouletteTop != null)
		{
			return RouletteManager.s_instance.m_rouletteTop.IsClose();
		}
		return result;
	}

	// Token: 0x060046D7 RID: 18135 RVA: 0x0017290C File Offset: 0x00170B0C
	public static void RouletteGetWindowClose(RouletteUtility.AchievementType achievement, RouletteUtility.NextType nextType = RouletteUtility.NextType.NONE)
	{
		if (achievement != RouletteUtility.AchievementType.NONE)
		{
			if (RouletteManager.IsRouletteEnabled())
			{
				if (achievement == RouletteUtility.AchievementType.Multi)
				{
					if (!RouletteUtility.IsGetOtomoOrCharaWindow())
					{
						if (!RouletteUtility.ShowGetAllOtomoAndChara())
						{
							RouletteUtility.ShowGetAllListEnd();
							RouletteManager.s_multiGetWindow = false;
							if (RouletteManager.s_instance != null)
							{
								RouletteManager.s_instance.UpdateChangeBotton(RouletteCategory.NONE);
							}
						}
					}
					else
					{
						RouletteManager.s_multiGetWindow = true;
					}
				}
				else
				{
					RouletteManager.m_achievementType = achievement;
					RouletteManager.m_nextType = nextType;
				}
			}
			else
			{
				RouletteManager.m_achievementType = RouletteUtility.AchievementType.NONE;
				RouletteManager.m_nextType = RouletteUtility.NextType.NONE;
			}
			AchievementManager.RequestUpdate();
		}
	}

	// Token: 0x060046D8 RID: 18136 RVA: 0x0017299C File Offset: 0x00170B9C
	public static bool IsRouletteEnabled()
	{
		bool result = false;
		if (RouletteManager.s_instance != null)
		{
			result = RouletteManager.s_instance.IsRouletteEnabledOrg();
		}
		return result;
	}

	// Token: 0x060046D9 RID: 18137 RVA: 0x001729C8 File Offset: 0x00170BC8
	public static bool OpenRouletteWindow()
	{
		bool result = false;
		if (RouletteManager.s_instance != null)
		{
			result = RouletteManager.s_instance.OpenRouletteWindowOrg();
		}
		return result;
	}

	// Token: 0x060046DA RID: 18138 RVA: 0x001729F4 File Offset: 0x00170BF4
	public static bool CloseRouletteWindow()
	{
		bool result = false;
		if (RouletteManager.s_instance != null)
		{
			result = RouletteManager.s_instance.CloseRouletteWindowOrg();
		}
		return result;
	}

	// Token: 0x060046DB RID: 18139 RVA: 0x00172A20 File Offset: 0x00170C20
	public void Init()
	{
		base.AddUpdateCustom(new CustomGameObject.UpdateCustom(this.OnUpdateCustom), "Check", 0.1f);
		this.m_currentRankup = false;
	}

	// Token: 0x060046DC RID: 18140 RVA: 0x00172A54 File Offset: 0x00170C54
	protected override void UpdateStd(float deltaTime, float timeRate)
	{
		this.UpdateLoading(deltaTime);
		if (RouletteManager.m_achievementType != RouletteUtility.AchievementType.NONE && AchievementManager.IsRequestEnd())
		{
			if (RouletteManager.IsRouletteEnabled())
			{
				this.GetWindowClose(RouletteManager.m_achievementType, RouletteManager.m_nextType);
			}
			RouletteManager.m_achievementType = RouletteUtility.AchievementType.NONE;
			RouletteManager.m_nextType = RouletteUtility.NextType.NONE;
		}
		if (this.m_dummyTime > 0f)
		{
			this.m_dummyTime -= Time.deltaTime;
			if (this.m_dummyTime <= 0f && this.m_dummyCallback != null && this.m_dummyData != null)
			{
				this.m_dummyCallback.SendMessage("RequestCommitRoulette_Succeeded", this.m_dummyData, SendMessageOptions.DontRequireReceiver);
				this.m_dummyTime = 0f;
				this.m_dummyCallback = null;
			}
		}
		if (this.m_updateRouletteDelay > 0f)
		{
			this.m_updateRouletteDelay -= Time.deltaTime;
			if (this.m_updateRouletteDelay <= 0f)
			{
				if (this.m_rouletteItemBak != null && this.m_rouletteList != null && this.m_rouletteList.ContainsKey(RouletteCategory.ITEM) && this.m_rouletteGeneralBakCategory == RouletteCategory.NONE)
				{
					this.m_rouletteList[RouletteCategory.ITEM].Setup(this.m_rouletteItemBak);
					this.m_rouletteItemBak = null;
					if (this.m_rouletteTop != null)
					{
						this.m_rouletteTop.UpdateWheel(this.m_rouletteList[RouletteCategory.ITEM], false);
					}
				}
				else if (this.m_rouletteGeneralBak != null && this.m_rouletteGeneralBakCategory != RouletteCategory.NONE)
				{
					RouletteCategory rouletteGeneralBakCategory = this.m_rouletteGeneralBakCategory;
					if (this.m_rouletteList != null && this.m_rouletteList.ContainsKey(rouletteGeneralBakCategory))
					{
						this.m_rouletteList[rouletteGeneralBakCategory].Setup(this.m_rouletteGeneralBak);
						this.m_rouletteGeneralBak = null;
						this.m_rouletteGeneralBakCategory = RouletteCategory.NONE;
						if (this.m_rouletteTop != null)
						{
							this.m_rouletteTop.UpdateWheel(this.m_rouletteList[rouletteGeneralBakCategory], false);
						}
					}
				}
				else if (this.m_rouletteChaoBak != null && this.m_rouletteChaoBakCategory != RouletteCategory.NONE)
				{
					RouletteCategory rouletteChaoBakCategory = this.m_rouletteChaoBakCategory;
					if (this.m_rouletteList != null && this.m_rouletteList.ContainsKey(rouletteChaoBakCategory))
					{
						this.m_rouletteList[rouletteChaoBakCategory].Setup(this.m_rouletteChaoBak);
						this.m_rouletteChaoBak = null;
						this.m_rouletteChaoBakCategory = RouletteCategory.NONE;
						if (this.m_rouletteTop != null)
						{
							this.m_rouletteTop.UpdateWheel(this.m_rouletteList[rouletteChaoBakCategory], false);
						}
					}
				}
				this.m_updateRouletteDelay = 0f;
			}
		}
		if (this.m_rouletteCostItemIdListGetTime < 0f && GeneralWindow.IsCreated("ShowNoCommunicationCostItem") && GeneralWindow.IsOkButtonPressed)
		{
			if (GeneralUtil.IsNetwork())
			{
				if (this.m_rouletteCostItemIdList != null && this.m_rouletteCostItemIdList.Count > 0)
				{
					List<int> list = new List<int>();
					foreach (ServerItem.Id item in this.m_rouletteCostItemIdList)
					{
						list.Add((int)item);
					}
					ServerInterface.LoggedInServerInterface.RequestServerGetItemStockNum(EventManager.Instance.Id, list, base.gameObject);
				}
			}
			else
			{
				GeneralUtil.ShowNoCommunication("ShowNoCommunicationCostItem");
			}
		}
	}

	// Token: 0x060046DD RID: 18141 RVA: 0x00172DC4 File Offset: 0x00170FC4
	private void OnUpdateCustom(string updateName, float timeRate)
	{
		if (RouletteManager.isShowGetWindow)
		{
			ItemGetWindow itemGetWindow = ItemGetWindowUtil.GetItemGetWindow();
			if (itemGetWindow != null && itemGetWindow.IsCreated("Jackpot") && itemGetWindow.IsEnd)
			{
				if (itemGetWindow.IsYesButtonPressed)
				{
					this.m_easySnsFeed = new EasySnsFeed(this.m_rouletteTop.gameObject, "Camera/menu_Anim/RouletteTopUI/Anchor_5_MC", RouletteManager.GetText("feed_jackpot_caption", null), RouletteUtility.jackpotFeedText, null);
					global::Debug.Log("Jackpot feed text:" + RouletteUtility.jackpotFeedText + " !!!!!");
				}
				else if (RouletteManager.IsRouletteEnabled())
				{
					this.GetWindowClose(RouletteUtility.AchievementType.NONE, RouletteUtility.NextType.NONE);
				}
				RouletteManager.isShowGetWindow = false;
				itemGetWindow.Reset();
			}
			if (GeneralWindow.IsCreated("RouletteGetAllList"))
			{
				if (GeneralWindow.IsButtonPressed)
				{
					RouletteManager.s_multiGetWindow = false;
					RouletteManager.isShowGetWindow = false;
					GeneralWindow.Close();
				}
			}
			else if (GeneralWindow.IsCreated("RouletteGetAllListEnd"))
			{
				if (GeneralWindow.IsButtonPressed)
				{
					RouletteManager.isShowGetWindow = false;
					GeneralWindow.Close();
				}
			}
			else if (GeneralWindow.IsCreated("RouletteGetAllListEndChara"))
			{
				if (GeneralWindow.IsButtonPressed)
				{
					if (GeneralWindow.IsYesButtonPressed)
					{
						HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.CHARA_MAIN, true);
					}
					RouletteManager.isShowGetWindow = false;
					GeneralWindow.Close();
				}
			}
			else if (GeneralWindow.IsCreated("RouletteGetAllListEndChao") && GeneralWindow.IsButtonPressed)
			{
				if (GeneralWindow.IsYesButtonPressed)
				{
					HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.CHAO, true);
				}
				RouletteManager.isShowGetWindow = false;
				GeneralWindow.Close();
			}
		}
		if (this.m_easySnsFeed != null)
		{
			EasySnsFeed.Result result = this.m_easySnsFeed.Update();
			if (result == EasySnsFeed.Result.COMPLETED || result == EasySnsFeed.Result.FAILED)
			{
				if (RouletteManager.IsRouletteEnabled())
				{
					this.GetWindowClose(RouletteUtility.AchievementType.NONE, RouletteUtility.NextType.NONE);
				}
				RouletteManager.isShowGetWindow = false;
				this.m_easySnsFeed = null;
			}
		}
		if (RouletteManager.s_multiGetWindow && !RouletteUtility.IsGetOtomoOrCharaWindow() && !RouletteUtility.ShowGetAllOtomoAndChara())
		{
			RouletteUtility.ShowGetAllListEnd();
			RouletteManager.s_multiGetWindow = false;
		}
		if (this.m_isCurrentPrizeLoadingAuto != RouletteCategory.NONE)
		{
			RouletteCategory isCurrentPrizeLoadingAuto = this.m_isCurrentPrizeLoadingAuto;
			switch (isCurrentPrizeLoadingAuto)
			{
			case RouletteCategory.PREMIUM:
				if (ServerInterface.PremiumRoulettePrizeList != null && ServerInterface.PremiumRoulettePrizeList.IsData())
				{
					this.SetPrizeList(this.m_isCurrentPrizeLoadingAuto, ServerInterface.PremiumRoulettePrizeList);
					this.m_isCurrentPrizeLoadingAuto = RouletteCategory.NONE;
				}
				break;
			default:
				if (isCurrentPrizeLoadingAuto != RouletteCategory.SPECIAL)
				{
					this.m_isCurrentPrizeLoadingAuto = RouletteCategory.NONE;
				}
				else if (ServerInterface.SpecialRoulettePrizeList != null && ServerInterface.SpecialRoulettePrizeList.IsData())
				{
					this.SetPrizeList(this.m_isCurrentPrizeLoadingAuto, ServerInterface.SpecialRoulettePrizeList);
					this.m_isCurrentPrizeLoadingAuto = RouletteCategory.NONE;
				}
				break;
			case RouletteCategory.RAID:
				if (ServerInterface.RaidRoulettePrizeList != null && ServerInterface.RaidRoulettePrizeList.IsData())
				{
					this.SetPrizeList(this.m_isCurrentPrizeLoadingAuto, ServerInterface.RaidRoulettePrizeList);
					this.m_isCurrentPrizeLoadingAuto = RouletteCategory.NONE;
				}
				break;
			}
		}
	}

	// Token: 0x17000994 RID: 2452
	// (get) Token: 0x060046DE RID: 18142 RVA: 0x001730A0 File Offset: 0x001712A0
	public string oldBgmName
	{
		get
		{
			return this.m_oldBgm;
		}
	}

	// Token: 0x060046DF RID: 18143 RVA: 0x001730A8 File Offset: 0x001712A8
	public void RouletteBgmResetOrg()
	{
		SoundManager.BgmStop();
		this.m_oldBgm = null;
		this.m_bgmReset = false;
		base.RemoveCallback(null);
	}

	// Token: 0x060046E0 RID: 18144 RVA: 0x001730C8 File Offset: 0x001712C8
	public void PlayBgmOrg(string soundName, float delay = 0f, string cueSheetName = "BGM", bool bgmReset = false)
	{
		if (!string.IsNullOrEmpty(soundName) && RouletteManager.IsRouletteEnabled())
		{
			string text = soundName + ":" + cueSheetName;
			if (delay <= 0f)
			{
				bool flag = false;
				if (bgmReset)
				{
					flag = true;
				}
				else if (!string.IsNullOrEmpty(this.m_oldBgm))
				{
					if (this.m_oldBgm != text)
					{
						flag = true;
					}
					string text2 = this.m_oldBgm;
					if (text2.IndexOf(":") >= 0)
					{
						string[] array = text2.Split(new char[]
						{
							':'
						});
						if (array != null && array.Length > 1)
						{
							text2 = array[0];
						}
					}
					if (text2 == soundName)
					{
						return;
					}
				}
				this.m_oldBgm = text;
				if (flag)
				{
					SoundManager.BgmChange(soundName, cueSheetName);
				}
				else
				{
					SoundManager.BgmChange(soundName, cueSheetName);
				}
			}
			else
			{
				bool flag2 = true;
				if (!bgmReset)
				{
					if (!string.IsNullOrEmpty(this.m_oldBgm) && this.m_oldBgm == text)
					{
						flag2 = false;
					}
				}
				else
				{
					this.m_oldBgm = null;
				}
				if (flag2)
				{
					this.m_bgmReset = bgmReset;
					base.RemoveCallbackPartialMatch("bgm_sys_");
					base.AddCallback(new CustomGameObject.Callback(this.OnCallbackBgm), text, delay);
				}
				else
				{
					this.m_bgmReset = false;
					this.m_oldBgm = null;
				}
			}
		}
	}

	// Token: 0x060046E1 RID: 18145 RVA: 0x00173224 File Offset: 0x00171424
	public void PlaySeOrg(string soundName, float delay = 0f, string cueSheetName = "SE")
	{
		if (!string.IsNullOrEmpty(soundName) && RouletteManager.IsRouletteEnabled())
		{
			if (delay <= 0f)
			{
				SoundManager.SePlay(soundName, cueSheetName);
			}
			else
			{
				base.AddCallback(new CustomGameObject.Callback(this.OnCallbackSe), soundName + ":" + cueSheetName, delay);
			}
		}
	}

	// Token: 0x060046E2 RID: 18146 RVA: 0x00173280 File Offset: 0x00171480
	private void OnCallbackBgm(string callbackName)
	{
		if (!string.IsNullOrEmpty(callbackName) && RouletteManager.IsRouletteEnabled())
		{
			string cueName = callbackName;
			string cueSheetName = "BGM";
			if (callbackName.IndexOf(":") >= 0)
			{
				string[] array = callbackName.Split(new char[]
				{
					':'
				});
				if (array.Length > 1)
				{
					cueSheetName = array[1];
					cueName = array[0];
				}
			}
			bool flag = false;
			if (this.m_bgmReset)
			{
				flag = true;
			}
			else if (!string.IsNullOrEmpty(this.m_oldBgm) && this.m_oldBgm != callbackName)
			{
				flag = true;
			}
			this.m_oldBgm = callbackName;
			if (flag)
			{
				SoundManager.BgmChange(cueName, cueSheetName);
			}
			else
			{
				SoundManager.BgmChange(cueName, cueSheetName);
			}
		}
	}

	// Token: 0x060046E3 RID: 18147 RVA: 0x00173338 File Offset: 0x00171538
	private void OnCallbackSe(string callbackName)
	{
		if (!string.IsNullOrEmpty(callbackName) && RouletteManager.IsRouletteEnabled())
		{
			string cueName = callbackName;
			string cueSheetName = "SE";
			if (callbackName.IndexOf(":") >= 0)
			{
				string[] array = callbackName.Split(new char[]
				{
					':'
				});
				if (array.Length > 1)
				{
					cueSheetName = array[1];
					cueName = array[0];
				}
			}
			SoundManager.SePlay(cueName, cueSheetName);
		}
	}

	// Token: 0x060046E4 RID: 18148 RVA: 0x001733A0 File Offset: 0x001715A0
	public static void RouletteBgmReset()
	{
		if (RouletteManager.s_instance != null)
		{
			RouletteManager.s_instance.RouletteBgmResetOrg();
		}
	}

	// Token: 0x060046E5 RID: 18149 RVA: 0x001733BC File Offset: 0x001715BC
	public static void PlayBgm(string soundName, float delay = 0f, string cueSheetName = "BGM", bool bgmReset = false)
	{
		if (RouletteManager.s_instance != null)
		{
			RouletteManager.s_instance.PlayBgmOrg(soundName, delay, cueSheetName, bgmReset);
		}
	}

	// Token: 0x060046E6 RID: 18150 RVA: 0x001733DC File Offset: 0x001715DC
	public static void PlaySe(string soundName, float delay = 0f, string cueSheetName = "SE")
	{
		if (RouletteManager.s_instance != null)
		{
			RouletteManager.s_instance.PlaySeOrg(soundName, delay, cueSheetName);
		}
	}

	// Token: 0x060046E7 RID: 18151 RVA: 0x001733FC File Offset: 0x001715FC
	public Dictionary<RouletteCategory, float> GetCurrentLoadingOrg()
	{
		if (this.m_loadingList != null && this.m_loadingList.Count > 0)
		{
			return this.m_loadingList;
		}
		return null;
	}

	// Token: 0x060046E8 RID: 18152 RVA: 0x00173430 File Offset: 0x00171630
	public bool IsLoadingOrg(RouletteCategory category)
	{
		bool result = false;
		if (category != RouletteCategory.ALL)
		{
			if (this.m_loadingList != null && this.m_loadingList.ContainsKey(category))
			{
				result = true;
			}
		}
		else if (this.m_loadingList != null && this.m_loadingList.Count > 0)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x060046E9 RID: 18153 RVA: 0x0017348C File Offset: 0x0017168C
	public bool IsPrizeLoadingOrg(RouletteCategory category)
	{
		if (category == RouletteCategory.ALL)
		{
			return this.m_isCurrentPrizeLoading != RouletteCategory.NONE;
		}
		return this.m_isCurrentPrizeLoading == category;
	}

	// Token: 0x060046EA RID: 18154 RVA: 0x001734BC File Offset: 0x001716BC
	private bool StartLoading(RouletteCategory category)
	{
		bool result = false;
		if (category != RouletteCategory.NONE && category != RouletteCategory.ALL)
		{
			if (this.m_loadingList == null)
			{
				this.m_loadingList = new Dictionary<RouletteCategory, float>();
				this.m_loadingList.Add(category, 0f);
				result = true;
			}
			else if (this.m_loadingList.ContainsKey(category))
			{
				if (this.m_loadingList[category] < 0f)
				{
					this.m_loadingList[category] = 0f;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			else
			{
				this.m_loadingList.Add(category, 0f);
				result = true;
			}
		}
		this.m_currentRankup = false;
		return result;
	}

	// Token: 0x060046EB RID: 18155 RVA: 0x00173568 File Offset: 0x00171768
	private void UpdateLoading(float deltaTime)
	{
		if (this.m_loadingList != null && this.m_loadingList.Count > 0)
		{
			List<RouletteCategory> list = new List<RouletteCategory>(this.m_loadingList.Keys);
			RouletteCategory rouletteCategory = RouletteCategory.NONE;
			foreach (RouletteCategory rouletteCategory2 in list)
			{
				if (this.m_loadingList[rouletteCategory2] < 0f)
				{
					rouletteCategory = rouletteCategory2;
				}
				else
				{
					Dictionary<RouletteCategory, float> loadingList;
					Dictionary<RouletteCategory, float> dictionary = loadingList = this.m_loadingList;
					RouletteCategory key2;
					RouletteCategory key = key2 = rouletteCategory2;
					float num = loadingList[key2];
					dictionary[key] = num + deltaTime;
				}
			}
			if (rouletteCategory != RouletteCategory.NONE)
			{
				this.m_loadingList.Remove(rouletteCategory);
			}
		}
	}

	// Token: 0x060046EC RID: 18156 RVA: 0x00173644 File Offset: 0x00171844
	private float GetLoadingTime(RouletteCategory category)
	{
		float result = -1f;
		if (category != RouletteCategory.NONE && category != RouletteCategory.ALL && this.m_loadingList != null && this.m_loadingList.Count > 0 && this.m_loadingList.ContainsKey(category))
		{
			result = this.m_loadingList[category];
		}
		return result;
	}

	// Token: 0x060046ED RID: 18157 RVA: 0x001736A0 File Offset: 0x001718A0
	private bool EndLoading(RouletteCategory category)
	{
		bool result = false;
		if (category != RouletteCategory.NONE && category != RouletteCategory.ALL && this.m_loadingList != null && this.m_loadingList.Count > 0 && this.m_loadingList.ContainsKey(category))
		{
			this.m_loadingList[category] = -1f;
			result = true;
		}
		return result;
	}

	// Token: 0x060046EE RID: 18158 RVA: 0x00173700 File Offset: 0x00171900
	public bool RequestRouletteOrg(RouletteCategory category, GameObject callbackObject = null)
	{
		bool result = false;
		this.m_isCurrentPrizeLoadingAuto = RouletteCategory.NONE;
		if (category != RouletteCategory.NONE && category != RouletteCategory.ALL)
		{
			if (category == RouletteCategory.SPECIAL)
			{
				category = RouletteCategory.PREMIUM;
			}
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				if (this.StartLoading(category))
				{
					int id = EventManager.Instance.Id;
					switch (category)
					{
					case RouletteCategory.PREMIUM:
						global::Debug.Log("RequestRouletteOrg : RouletteCategory.PREMIUM");
						this.SetCallbackObject((int)(1000 + category), callbackObject);
						loggedInServerInterface.RequestServerGetChaoWheelOptions(base.gameObject);
						goto IL_100;
					case RouletteCategory.ITEM:
						global::Debug.Log("RequestRouletteOrg : RouletteCategory.ITEM");
						this.SetCallbackObject((int)(1000 + category), callbackObject);
						loggedInServerInterface.RequestServerGetWheelOptions(base.gameObject);
						goto IL_100;
					}
					this.EndLoading(category);
					this.StartLoading(RouletteCategory.GENERAL);
					this.m_requestRouletteId = (int)category;
					this.m_requestRouletteId = 0;
					global::Debug.Log("RequestRouletteOrg : RouletteCategory.RAID");
					this.SetCallbackObject(this.m_requestRouletteId, callbackObject);
					loggedInServerInterface.RequestServerGetWheelOptionsGeneral(id, 0, base.gameObject);
					IL_100:
					result = true;
				}
				else
				{
					result = false;
				}
			}
		}
		return result;
	}

	// Token: 0x060046EF RID: 18159 RVA: 0x00173818 File Offset: 0x00171A18
	private ServerWheelOptionsData UpdateRouletteOrg(RouletteCategory category = RouletteCategory.NONE, float delay = 0f)
	{
		if (delay <= 0f)
		{
			if (category != RouletteCategory.NONE && category != RouletteCategory.ALL)
			{
				if (this.m_rouletteItemBak != null && category == RouletteCategory.ITEM && this.m_rouletteList != null && this.m_rouletteList.ContainsKey(RouletteCategory.ITEM) && this.m_rouletteGeneralBakCategory == RouletteCategory.NONE)
				{
					this.m_rouletteList[RouletteCategory.ITEM].Setup(this.m_rouletteItemBak);
					this.m_rouletteItemBak = null;
					return this.m_rouletteList[RouletteCategory.ITEM];
				}
				if (this.m_rouletteGeneralBak != null && this.m_rouletteGeneralBakCategory != RouletteCategory.NONE)
				{
					RouletteCategory rouletteGeneralBakCategory = this.m_rouletteGeneralBakCategory;
					if (this.m_rouletteList != null && this.m_rouletteList.ContainsKey(rouletteGeneralBakCategory))
					{
						this.m_rouletteList[rouletteGeneralBakCategory].Setup(this.m_rouletteGeneralBak);
						this.m_rouletteGeneralBak = null;
						this.m_rouletteGeneralBakCategory = RouletteCategory.NONE;
						return this.m_rouletteList[rouletteGeneralBakCategory];
					}
				}
				else if (this.m_rouletteChaoBak != null && this.m_rouletteChaoBakCategory != RouletteCategory.NONE)
				{
					RouletteCategory rouletteChaoBakCategory = this.m_rouletteChaoBakCategory;
					if (this.m_rouletteList != null && this.m_rouletteList.ContainsKey(rouletteChaoBakCategory))
					{
						this.m_rouletteList[rouletteChaoBakCategory].Setup(this.m_rouletteChaoBak);
						this.m_rouletteChaoBak = null;
						this.m_rouletteChaoBakCategory = RouletteCategory.NONE;
						return this.m_rouletteList[rouletteChaoBakCategory];
					}
				}
			}
		}
		else if (category != RouletteCategory.NONE && category != RouletteCategory.ALL)
		{
			if (this.m_rouletteItemBak != null && category == RouletteCategory.ITEM && this.m_rouletteList != null && this.m_rouletteList.ContainsKey(RouletteCategory.ITEM))
			{
				this.m_updateRouletteDelay = delay;
			}
			else if (this.m_rouletteGeneralBak != null && this.m_rouletteGeneralBakCategory != RouletteCategory.NONE)
			{
				if (this.m_rouletteList != null && this.m_rouletteList.ContainsKey(this.m_rouletteGeneralBakCategory))
				{
					this.m_updateRouletteDelay = delay;
				}
			}
			else if (this.m_rouletteChaoBak != null && this.m_rouletteChaoBakCategory != RouletteCategory.NONE && this.m_rouletteList != null && this.m_rouletteList.ContainsKey(this.m_rouletteChaoBakCategory))
			{
				this.m_updateRouletteDelay = delay;
			}
		}
		return null;
	}

	// Token: 0x060046F0 RID: 18160 RVA: 0x00173A4C File Offset: 0x00171C4C
	public bool IsRequestPicupCharaList()
	{
		bool result = false;
		if (this.m_isPicupCharaListInit && (NetBase.GetCurrentTime() - this.m_picupCharaListTime).TotalHours <= 1.0)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x060046F1 RID: 18161 RVA: 0x00173A90 File Offset: 0x00171C90
	public bool RequestPicupCharaList(bool isImmediatelyUpdate = false)
	{
		bool result = false;
		bool flag = false;
		if (!this.m_isPicupCharaListInit)
		{
			flag = true;
		}
		else if (isImmediatelyUpdate)
		{
			if ((NetBase.GetCurrentTime() - this.m_picupCharaListTime).TotalMinutes > 1.0)
			{
				flag = true;
			}
		}
		else if ((NetBase.GetCurrentTime() - this.m_picupCharaListTime).TotalHours > 1.0)
		{
			flag = true;
		}
		if (flag)
		{
			this.m_picupCharaListTime = NetBase.GetCurrentTime();
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				if (this.GetPrizeList(RouletteCategory.PREMIUM) == null)
				{
					loggedInServerInterface.RequestServerGetPrizeChaoWheelSpin(0, base.gameObject);
				}
				if (this.GetPrizeList(RouletteCategory.SPECIAL) == null)
				{
					loggedInServerInterface.RequestServerGetPrizeChaoWheelSpin(1, base.gameObject);
				}
				if (EventManager.Instance != null && EventManager.Instance.TypeInTime == EventManager.EventType.RAID_BOSS && this.GetPrizeList(RouletteCategory.RAID) == null)
				{
					int id = EventManager.Instance.Id;
					int spinType = 0;
					loggedInServerInterface.RequestServerGetPrizeWheelSpinGeneral(id, spinType, base.gameObject);
				}
				result = true;
			}
		}
		this.m_isPicupCharaListInit = true;
		return result;
	}

	// Token: 0x060046F2 RID: 18162 RVA: 0x00173BC0 File Offset: 0x00171DC0
	public bool RequestRoulettePrizeOrg(RouletteCategory category, GameObject callbackObject = null)
	{
		bool result = false;
		if (category != RouletteCategory.NONE && category != RouletteCategory.ALL)
		{
			this.m_isCurrentPrizeLoading = RouletteCategory.NONE;
			ServerPrizeState serverPrizeState = this.GetPrizeList(category);
			this.m_prizeCallback = callbackObject;
			if (category == RouletteCategory.PREMIUM && this.specialEgg >= 10)
			{
				category = RouletteCategory.SPECIAL;
			}
			if (serverPrizeState == null && this.m_rouletteList != null && this.m_rouletteList.ContainsKey(category))
			{
				ServerWheelOptionsData serverWheelOptionsData = this.m_rouletteList[category];
				if (serverWheelOptionsData != null)
				{
					if (serverWheelOptionsData.IsPrizeDataList())
					{
						if (!this.IsPrizeLoadingOrg(category))
						{
							ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
							if (loggedInServerInterface != null)
							{
								if (serverWheelOptionsData.isGeneral)
								{
									int eventId = 0;
									int spinType = 0;
									if (EventManager.Instance != null)
									{
										eventId = EventManager.Instance.Id;
									}
									loggedInServerInterface.RequestServerGetPrizeWheelSpinGeneral(eventId, spinType, base.gameObject);
								}
								else
								{
									int chaoWheelSpinType = (category != RouletteCategory.SPECIAL) ? 0 : 1;
									loggedInServerInterface.RequestServerGetPrizeChaoWheelSpin(chaoWheelSpinType, base.gameObject);
								}
								this.m_isCurrentPrizeLoading = category;
							}
						}
					}
					else
					{
						serverPrizeState = this.CreatePrizeList(category);
						if (this.m_prizeCallback != null && serverPrizeState != null)
						{
							if (RouletteManager.IsRouletteEnabled())
							{
								this.m_prizeCallback.SendMessage("RequestRoulettePrize_Succeeded", serverPrizeState, SendMessageOptions.DontRequireReceiver);
								this.m_prizeCallback = null;
								this.m_isCurrentPrizeLoading = RouletteCategory.NONE;
							}
							else
							{
								global::Debug.Log("RouletteManager RequestRoulettePrizeOrg RouletteTop:false");
							}
						}
					}
				}
			}
			else if (this.m_prizeCallback != null)
			{
				if (RouletteManager.IsRouletteEnabled())
				{
					if (serverPrizeState != null)
					{
						this.m_prizeCallback.SendMessage("RequestRoulettePrize_Succeeded", serverPrizeState, SendMessageOptions.DontRequireReceiver);
						this.m_prizeCallback = null;
						this.m_isCurrentPrizeLoading = RouletteCategory.NONE;
					}
					else
					{
						ServerInterface loggedInServerInterface2 = ServerInterface.LoggedInServerInterface;
						if (loggedInServerInterface2 != null)
						{
							if (category == RouletteCategory.ITEM)
							{
								serverPrizeState = this.CreatePrizeList(category);
								if (serverPrizeState == null)
								{
									this.m_isCurrentPrizeLoading = category;
									loggedInServerInterface2.RequestServerGetWheelOptions(base.gameObject);
								}
								else
								{
									this.m_prizeCallback.SendMessage("RequestRoulettePrize_Succeeded", serverPrizeState, SendMessageOptions.DontRequireReceiver);
									this.m_prizeCallback = null;
									this.m_isCurrentPrizeLoading = RouletteCategory.NONE;
								}
							}
							else if (category == RouletteCategory.PREMIUM || category == RouletteCategory.SPECIAL)
							{
								this.m_isCurrentPrizeLoading = category;
								int chaoWheelSpinType2 = (category != RouletteCategory.SPECIAL) ? 0 : 1;
								loggedInServerInterface2.RequestServerGetPrizeChaoWheelSpin(chaoWheelSpinType2, base.gameObject);
							}
							else if (category == RouletteCategory.RAID)
							{
								int eventId2 = 0;
								int spinType2 = 0;
								if (EventManager.Instance != null)
								{
									eventId2 = EventManager.Instance.Id;
								}
								this.m_isCurrentPrizeLoading = category;
								loggedInServerInterface2.RequestServerGetPrizeWheelSpinGeneral(eventId2, spinType2, base.gameObject);
							}
							else
							{
								this.m_isCurrentPrizeLoading = RouletteCategory.NONE;
								global::Debug.Log("RouletteManager RequestRoulettePrizeOrg RouletteTop:false");
							}
						}
						else
						{
							this.m_isCurrentPrizeLoading = RouletteCategory.NONE;
							global::Debug.Log("RouletteManager RequestRoulettePrizeOrg RouletteTop:false");
						}
					}
				}
				else
				{
					this.m_isCurrentPrizeLoading = RouletteCategory.NONE;
					global::Debug.Log("RouletteManager RequestRoulettePrizeOrg RouletteTop:false");
				}
			}
		}
		return result;
	}

	// Token: 0x060046F3 RID: 18163 RVA: 0x00173E9C File Offset: 0x0017209C
	public void SetPrizeList(RouletteCategory category, ServerPrizeState prizeState)
	{
		if (category != RouletteCategory.NONE && category != RouletteCategory.ALL && prizeState != null && prizeState.IsData())
		{
			if (this.m_prizeList == null)
			{
				this.m_prizeList = new Dictionary<RouletteCategory, ServerPrizeState>();
				this.m_prizeList.Add(category, prizeState);
			}
			else if (this.m_prizeList.ContainsKey(category))
			{
				this.m_prizeList[category] = prizeState;
			}
			else
			{
				this.m_prizeList.Add(category, prizeState);
			}
			this.SetPicupCharaList(category, prizeState);
		}
	}

	// Token: 0x060046F4 RID: 18164 RVA: 0x00173F28 File Offset: 0x00172128
	private void SetPicupCharaList(RouletteCategory category, ServerPrizeState prizeState)
	{
		if (category != RouletteCategory.NONE && category != RouletteCategory.ALL && prizeState != null)
		{
			if (this.m_picupCharaList == null)
			{
				this.m_picupCharaList = new Dictionary<RouletteCategory, List<CharaType>>();
			}
			if (this.m_picupCharaList.ContainsKey(category))
			{
				this.m_picupCharaList[category].Clear();
				if (prizeState.IsData())
				{
					foreach (ServerPrizeData serverPrizeData in prizeState.prizeList)
					{
						if (serverPrizeData.itemId >= 300000 && serverPrizeData.itemId < 400000)
						{
							ServerItem serverItem = new ServerItem((ServerItem.Id)serverPrizeData.itemId);
							if (serverItem.idType == ServerItem.IdType.CHARA && !this.m_picupCharaList[category].Contains(serverItem.charaType))
							{
								this.m_picupCharaList[category].Add(serverItem.charaType);
							}
						}
					}
				}
			}
			else
			{
				List<CharaType> list = new List<CharaType>();
				if (prizeState.IsData())
				{
					foreach (ServerPrizeData serverPrizeData2 in prizeState.prizeList)
					{
						if (serverPrizeData2.itemId >= 300000 && serverPrizeData2.itemId < 400000)
						{
							ServerItem serverItem2 = new ServerItem((ServerItem.Id)serverPrizeData2.itemId);
							if (serverItem2.idType == ServerItem.IdType.CHARA && !list.Contains(serverItem2.charaType))
							{
								list.Add(serverItem2.charaType);
							}
						}
					}
				}
				this.m_picupCharaList.Add(category, list);
			}
		}
	}

	// Token: 0x060046F5 RID: 18165 RVA: 0x00174120 File Offset: 0x00172320
	public ServerPrizeState GetPrizeList(RouletteCategory category)
	{
		ServerPrizeState result = null;
		if (category != RouletteCategory.NONE && category != RouletteCategory.ALL)
		{
			if (this.m_prizeList == null)
			{
				this.m_prizeList = new Dictionary<RouletteCategory, ServerPrizeState>();
				result = null;
			}
			else if (this.m_prizeList.ContainsKey(category) && !this.m_prizeList[category].IsExpired())
			{
				result = this.m_prizeList[category];
			}
			switch (category)
			{
			case RouletteCategory.PREMIUM:
				if (ServerInterface.PremiumRoulettePrizeList != null && ServerInterface.PremiumRoulettePrizeList.IsData())
				{
					this.SetPrizeList(category, ServerInterface.PremiumRoulettePrizeList);
					result = ServerInterface.PremiumRoulettePrizeList;
				}
				break;
			default:
				if (category == RouletteCategory.SPECIAL)
				{
					if (ServerInterface.SpecialRoulettePrizeList != null && ServerInterface.SpecialRoulettePrizeList.IsData())
					{
						this.SetPrizeList(category, ServerInterface.SpecialRoulettePrizeList);
						result = ServerInterface.SpecialRoulettePrizeList;
					}
				}
				break;
			case RouletteCategory.RAID:
				if (ServerInterface.RaidRoulettePrizeList != null && ServerInterface.RaidRoulettePrizeList.IsData())
				{
					this.SetPrizeList(category, ServerInterface.RaidRoulettePrizeList);
					result = ServerInterface.RaidRoulettePrizeList;
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x060046F6 RID: 18166 RVA: 0x00174244 File Offset: 0x00172444
	private ServerPrizeState CreatePrizeList(RouletteCategory category)
	{
		ServerPrizeState serverPrizeState = null;
		if (category != RouletteCategory.NONE && category != RouletteCategory.ALL && this.m_rouletteList != null && this.m_rouletteList.ContainsKey(category))
		{
			ServerWheelOptionsData data = this.m_rouletteList[category];
			serverPrizeState = new ServerPrizeState(data);
			if (this.m_prizeList == null)
			{
				this.m_prizeList = new Dictionary<RouletteCategory, ServerPrizeState>();
			}
			if (this.m_prizeList.ContainsKey(category))
			{
				this.m_prizeList[category] = serverPrizeState;
			}
			else
			{
				this.m_prizeList.Add(category, serverPrizeState);
			}
		}
		return serverPrizeState;
	}

	// Token: 0x060046F7 RID: 18167 RVA: 0x001742D8 File Offset: 0x001724D8
	public void DummyCommit(ServerWheelOptionsData data, GameObject callbackObject = null)
	{
		global::Debug.Log("DummyCommit !!!!!!!!!!!!!!!!!!!!!!!!!");
		if (this.m_rouletteList != null && this.m_rouletteList.ContainsKey(RouletteCategory.PREMIUM))
		{
			ServerWheelOptionsData serverWheelOptionsData = this.m_rouletteList[RouletteCategory.PREMIUM];
			if (serverWheelOptionsData != null)
			{
				ServerSpinResultGeneral serverSpinResultGeneral = null;
				ServerChaoSpinResult serverChaoSpinResult = null;
				if (data.isGeneral)
				{
					serverSpinResultGeneral = new ServerSpinResultGeneral();
					this.m_dummyData = new ServerWheelOptionsData(data.GetOrgGeneralData());
				}
				else
				{
					serverChaoSpinResult = new ServerChaoSpinResult();
					this.m_dummyData = new ServerWheelOptionsData(data.GetOrgNormalData());
				}
				ServerChaoData serverChaoData = null;
				ChaoData[] dataTable = ChaoTable.GetDataTable();
				if (dataTable != null)
				{
					for (int i = 0; i < dataTable.Length; i++)
					{
						if (dataTable[i].level >= 0)
						{
							serverChaoData = new ServerChaoData();
							serverChaoData.Id = dataTable[i].id + 400000;
							serverChaoData.Level = dataTable[i].level;
							serverChaoData.Rarity = (int)dataTable[i].rarity;
							break;
						}
					}
				}
				List<int> list = new List<int>();
				if (serverChaoData != null)
				{
					for (int j = 0; j < 16; j++)
					{
						int cellEgg = data.GetCellEgg(j);
						if (cellEgg == serverChaoData.Rarity)
						{
							list.Add(j);
						}
					}
					if (list.Count <= 0)
					{
						list.Add(1);
					}
					int itemWon = list[UnityEngine.Random.Range(0, list.Count)];
					if (serverChaoSpinResult != null)
					{
						serverChaoSpinResult.AcquiredChaoData = serverChaoData;
						serverChaoSpinResult.IsRequiredChao = true;
						serverChaoSpinResult.ItemWon = itemWon;
					}
					if (serverSpinResultGeneral != null)
					{
						serverSpinResultGeneral.AddChaoState(serverChaoData);
						serverSpinResultGeneral.ItemWon = itemWon;
					}
					this.m_resultData = serverChaoSpinResult;
					this.m_resultGeneral = serverSpinResultGeneral;
				}
				this.m_dummyCallback = callbackObject;
				this.m_dummyTime = 2f;
			}
		}
	}

	// Token: 0x060046F8 RID: 18168 RVA: 0x001744A4 File Offset: 0x001726A4
	public bool RequestCommitRouletteOrg(ServerWheelOptionsData data, int num, GameObject callbackObject = null)
	{
		if (data == null)
		{
			return false;
		}
		bool result = false;
		RouletteCategory rouletteCategory = data.category;
		if (rouletteCategory != RouletteCategory.NONE && rouletteCategory != RouletteCategory.ALL)
		{
			if (rouletteCategory == RouletteCategory.SPECIAL)
			{
				rouletteCategory = RouletteCategory.PREMIUM;
			}
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				this.ResetResult();
				this.m_lastCommitCategory = RouletteCategory.NONE;
				if (this.StartLoading(rouletteCategory))
				{
					int id = EventManager.Instance.Id;
					switch (rouletteCategory)
					{
					case RouletteCategory.PREMIUM:
					{
						global::Debug.Log("RequestCommitRouletteOrg : RouletteCategory.PREMIUM");
						int multi = data.multi;
						this.SetCallbackObject((int)(1000 + rouletteCategory), callbackObject);
						loggedInServerInterface.RequestServerCommitChaoWheelSpin(multi, base.gameObject);
						goto IL_13A;
					}
					case RouletteCategory.ITEM:
						global::Debug.Log("RequestCommitRouletteOrg : RouletteCategory.ITEM");
						this.SetCallbackObject((int)(1000 + rouletteCategory), callbackObject);
						loggedInServerInterface.RequestServerCommitWheelSpin(1, base.gameObject);
						goto IL_13A;
					}
					this.m_requestRouletteId = (int)rouletteCategory;
					this.m_requestRouletteId = 0;
					global::Debug.Log("RequestCommitRouletteOrg : RouletteCategory.RAID");
					this.EndLoading(rouletteCategory);
					this.StartLoading(RouletteCategory.GENERAL);
					int spinCostItemId = data.GetSpinCostItemId();
					int multi2 = data.multi;
					this.SetCallbackObject(this.m_requestRouletteId, callbackObject);
					loggedInServerInterface.RequestServerCommitWheelSpinGeneral(id, this.m_requestRouletteId, spinCostItemId, multi2, base.gameObject);
					IL_13A:
					result = true;
				}
				else
				{
					result = false;
				}
			}
		}
		return result;
	}

	// Token: 0x060046F9 RID: 18169 RVA: 0x001745F8 File Offset: 0x001727F8
	public void ResetRouletteOrg(RouletteCategory category)
	{
		if (this.m_rouletteList != null && this.m_rouletteList.Count > 0 && category != RouletteCategory.NONE)
		{
			if (category == RouletteCategory.SPECIAL)
			{
				category = RouletteCategory.PREMIUM;
			}
			if (category != RouletteCategory.ALL)
			{
				if (this.m_rouletteList.ContainsKey(category))
				{
					this.m_rouletteList.Remove(category);
				}
			}
			else
			{
				this.m_rouletteList.Clear();
			}
		}
	}

	// Token: 0x060046FA RID: 18170 RVA: 0x00174668 File Offset: 0x00172868
	public void RequestRouletteBasicInformation(GameObject callback = null)
	{
		bool flag = false;
		this.m_basicInfoCallback = callback;
		if (this.m_basicRouletteCategorys == null || this.m_basicRouletteCategorys.Count <= 0 || this.m_basicInfoCallback == null)
		{
			flag = true;
			this.m_basicRouletteCategorysGetLastTime = NetUtil.GetCurrentTime();
		}
		else if ((NetUtil.GetCurrentTime() - this.m_basicRouletteCategorysGetLastTime).TotalMinutes > 5.0)
		{
			flag = true;
		}
		if (flag)
		{
			this.ServerGetRouletteList_Succeeded(null);
		}
		else
		{
			if (this.m_basicInfoCallback != null)
			{
				this.m_basicInfoCallback.SendMessage("RequestBasicInfo_Succeeded", this.m_basicRouletteCategorys, SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				GeneralUtil.SetRouletteBtnIcon(null, "Btn_roulette");
			}
			if (this.m_rouletteCostItemIdList != null && this.m_rouletteCostItemIdList.Count > 0)
			{
				RouletteTop.Instance.UpdateCostItemList(this.m_rouletteCostItemIdList);
				this.m_rouletteCostItemIdListGetTime = Time.realtimeSinceStartup;
			}
		}
	}

	// Token: 0x060046FB RID: 18171 RVA: 0x00174768 File Offset: 0x00172968
	private void ServerGetRouletteList_Succeeded(MsgGetItemStockNumSucceed msg)
	{
		this.m_basicRouletteCategorysGetLastTime = NetUtil.GetCurrentTime();
		EventManager.EventType typeInTime = EventManager.Instance.TypeInTime;
		if (this.m_basicRouletteCategorys != null && this.m_basicRouletteCategorys.Contains(RouletteCategory.RAID) && typeInTime != EventManager.EventType.RAID_BOSS)
		{
			this.m_rouletteCostItemIdListGetTime = -1f;
		}
		this.m_basicRouletteCategorys = new List<RouletteCategory>();
		this.m_rouletteCostItemIdList = new List<ServerItem.Id>();
		this.m_basicRouletteCategorys.Add(RouletteCategory.PREMIUM);
		this.m_basicRouletteCategorys.Add(RouletteCategory.ITEM);
		this.m_rouletteCostItemIdList.Add(ServerItem.Id.ROULLETE_TICKET_PREMIAM);
		this.m_rouletteCostItemIdList.Add(ServerItem.Id.ROULLETE_TICKET_ITEM);
		this.m_rouletteCostItemIdList.Add(ServerItem.Id.SPECIAL_EGG);
		if (typeInTime == EventManager.EventType.RAID_BOSS)
		{
			this.m_basicRouletteCategorys.Add(RouletteCategory.RAID);
			this.m_rouletteCostItemIdList.Add(ServerItem.Id.RAIDRING);
		}
		if (this.m_rouletteCostItemIdListGetTime <= 0f || Time.realtimeSinceStartup - this.m_rouletteCostItemIdListGetTime > 1000f)
		{
			List<int> list = new List<int>();
			foreach (ServerItem.Id item in this.m_rouletteCostItemIdList)
			{
				list.Add((int)item);
			}
			if (GeneralUtil.IsNetwork())
			{
				ServerInterface.LoggedInServerInterface.RequestServerGetItemStockNum(EventManager.Instance.Id, list, base.gameObject);
			}
			else
			{
				GeneralUtil.ShowNoCommunication("ShowNoCommunicationCostItem");
			}
		}
		GeneralUtil.SetRouletteBtnIcon(null, "Btn_roulette");
		if (this.m_basicInfoCallback != null)
		{
			this.m_basicInfoCallback.SendMessage("RequestBasicInfo_Succeeded", this.m_basicRouletteCategorys, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060046FC RID: 18172 RVA: 0x00174928 File Offset: 0x00172B28
	private void ServerGetItemStockNum_Succeeded(MsgGetItemStockNumSucceed msg)
	{
		if (RouletteTop.Instance != null)
		{
			RouletteTop.Instance.UpdateCostItemList(this.m_rouletteCostItemIdList);
			this.m_rouletteCostItemIdListGetTime = Time.realtimeSinceStartup;
		}
	}

	// Token: 0x060046FD RID: 18173 RVA: 0x00174958 File Offset: 0x00172B58
	public static Dictionary<RouletteCategory, float> GetCurrentLoading()
	{
		Dictionary<RouletteCategory, float> result = null;
		if (RouletteManager.s_instance != null)
		{
			result = RouletteManager.s_instance.GetCurrentLoadingOrg();
		}
		return result;
	}

	// Token: 0x060046FE RID: 18174 RVA: 0x00174984 File Offset: 0x00172B84
	public static bool IsLoading(RouletteCategory category)
	{
		bool result = false;
		if (RouletteManager.s_instance != null)
		{
			result = RouletteManager.s_instance.IsLoadingOrg(category);
		}
		return result;
	}

	// Token: 0x060046FF RID: 18175 RVA: 0x001749B0 File Offset: 0x00172BB0
	public static bool IsPrizeLoading(RouletteCategory category)
	{
		bool result = false;
		if (RouletteManager.s_instance != null)
		{
			result = RouletteManager.s_instance.IsPrizeLoadingOrg(category);
		}
		return result;
	}

	// Token: 0x06004700 RID: 18176 RVA: 0x001749DC File Offset: 0x00172BDC
	public static bool RequestRoulettePrize(RouletteCategory category, GameObject callbackObject = null)
	{
		bool result = false;
		if (RouletteManager.s_instance != null)
		{
			result = RouletteManager.s_instance.RequestRoulettePrizeOrg(category, callbackObject);
		}
		return result;
	}

	// Token: 0x06004701 RID: 18177 RVA: 0x00174A0C File Offset: 0x00172C0C
	public static bool RequestRoulette(RouletteCategory category, GameObject callbackObject = null)
	{
		bool result = false;
		if (RouletteManager.s_instance != null)
		{
			result = RouletteManager.s_instance.RequestRouletteOrg(category, callbackObject);
		}
		return result;
	}

	// Token: 0x06004702 RID: 18178 RVA: 0x00174A3C File Offset: 0x00172C3C
	public static bool RequestCommitRoulette(ServerWheelOptionsData data, int num, GameObject callbackObject = null)
	{
		bool result = false;
		if (RouletteManager.s_instance != null)
		{
			result = RouletteManager.s_instance.RequestCommitRouletteOrg(data, num, callbackObject);
		}
		return result;
	}

	// Token: 0x06004703 RID: 18179 RVA: 0x00174A6C File Offset: 0x00172C6C
	public static void ResetRoulette(RouletteCategory category = RouletteCategory.ALL)
	{
		if (RouletteManager.s_instance != null)
		{
			RouletteManager.s_instance.ResetRouletteOrg(category);
		}
	}

	// Token: 0x06004704 RID: 18180 RVA: 0x00174A8C File Offset: 0x00172C8C
	public static ServerWheelOptionsData UpdateRoulette(RouletteCategory category, float delay = 0f)
	{
		if (RouletteManager.s_instance != null)
		{
			return RouletteManager.s_instance.UpdateRouletteOrg(category, delay);
		}
		return null;
	}

	// Token: 0x06004705 RID: 18181 RVA: 0x00174AAC File Offset: 0x00172CAC
	public static void Remove()
	{
		if (RouletteManager.s_instance != null)
		{
			UnityEngine.Object.Destroy(RouletteManager.s_instance.gameObject);
			RouletteManager.s_instance = null;
		}
	}

	// Token: 0x06004706 RID: 18182 RVA: 0x00174AD4 File Offset: 0x00172CD4
	private void ResetResult()
	{
		this.m_resultData = null;
		this.m_resultGeneral = null;
	}

	// Token: 0x06004707 RID: 18183 RVA: 0x00174AE4 File Offset: 0x00172CE4
	public bool IsResult()
	{
		bool result = false;
		if (this.m_resultData != null || this.m_resultGeneral != null)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06004708 RID: 18184 RVA: 0x00174B0C File Offset: 0x00172D0C
	public bool IsPicupChara(CharaType charaType)
	{
		bool flag = false;
		if (this.m_picupCharaList != null && this.m_picupCharaList.Count > 0)
		{
			Dictionary<RouletteCategory, List<CharaType>>.KeyCollection keys = this.m_picupCharaList.Keys;
			foreach (RouletteCategory key in keys)
			{
				if (this.m_picupCharaList[key].Count > 0)
				{
					foreach (CharaType charaType2 in this.m_picupCharaList[key])
					{
						if (charaType2 == charaType)
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					break;
				}
			}
		}
		return flag;
	}

	// Token: 0x06004709 RID: 18185 RVA: 0x00174C1C File Offset: 0x00172E1C
	public RouletteCategory GetPicupCharaCategry(CharaType charaType)
	{
		RouletteCategory rouletteCategory = RouletteCategory.NONE;
		if (this.m_picupCharaList != null && this.m_picupCharaList.Count > 0)
		{
			Dictionary<RouletteCategory, List<CharaType>>.KeyCollection keys = this.m_picupCharaList.Keys;
			foreach (RouletteCategory rouletteCategory2 in keys)
			{
				if (this.m_picupCharaList[rouletteCategory2].Count > 0)
				{
					foreach (CharaType charaType2 in this.m_picupCharaList[rouletteCategory2])
					{
						if (charaType2 == charaType)
						{
							rouletteCategory = rouletteCategory2;
							break;
						}
					}
				}
				if (rouletteCategory != RouletteCategory.NONE)
				{
					break;
				}
			}
		}
		return rouletteCategory;
	}

	// Token: 0x0600470A RID: 18186 RVA: 0x00174D2C File Offset: 0x00172F2C
	public ServerSpinResultGeneral GetResult()
	{
		return this.m_resultGeneral;
	}

	// Token: 0x0600470B RID: 18187 RVA: 0x00174D34 File Offset: 0x00172F34
	public ServerChaoSpinResult GetResultChao()
	{
		return this.m_resultData;
	}

	// Token: 0x0600470C RID: 18188 RVA: 0x00174D3C File Offset: 0x00172F3C
	private void ServerGetWheelOptionsGeneral_Succeeded(MsgGetWheelOptionsGeneralSucceed msg)
	{
		if (msg != null)
		{
			ServerWheelOptionsGeneral wheelOptionsGeneral = msg.m_wheelOptionsGeneral;
			RouletteCategory rouletteCategory = RouletteUtility.GetRouletteCategory(wheelOptionsGeneral);
			global::Debug.Log("ServerGetWheelOptionsGeneral_Succeeded Category:" + rouletteCategory);
			if (wheelOptionsGeneral != null)
			{
				if (wheelOptionsGeneral.jackpotRing >= 30000)
				{
					RouletteManager.s_numJackpotRing = wheelOptionsGeneral.jackpotRing;
				}
				if (this.m_rouletteList == null)
				{
					this.m_rouletteList = new Dictionary<RouletteCategory, ServerWheelOptionsData>();
					this.m_rouletteList.Add(rouletteCategory, new ServerWheelOptionsData(wheelOptionsGeneral));
				}
				else if (this.m_rouletteList.ContainsKey(rouletteCategory))
				{
					this.m_rouletteList[rouletteCategory].Setup(wheelOptionsGeneral);
				}
				else
				{
					this.m_rouletteList.Add(rouletteCategory, new ServerWheelOptionsData(wheelOptionsGeneral));
				}
				this.EndLoading(RouletteCategory.GENERAL);
				global::Debug.Log("rouletteId:" + wheelOptionsGeneral.rouletteId);
				this.RequestPrizeAuto(this.m_rouletteList[rouletteCategory]);
				if (this.GetCallbackObject(wheelOptionsGeneral.rouletteId) != null && this.m_rouletteList.ContainsKey(rouletteCategory))
				{
					if (RouletteManager.IsRouletteEnabled())
					{
						this.GetCallbackObject(wheelOptionsGeneral.rouletteId).SendMessage("RequestGetRoulette_Succeeded", this.m_rouletteList[rouletteCategory], SendMessageOptions.DontRequireReceiver);
						this.UpdateChangeBotton(rouletteCategory);
					}
					else
					{
						global::Debug.Log("RouletteManager ServerGetWheelOptionsGeneral_Succeeded RouletteTop:false");
					}
				}
			}
		}
	}

	// Token: 0x0600470D RID: 18189 RVA: 0x00174E9C File Offset: 0x0017309C
	private void ServerGetChaoWheelOptions_Succeeded(MsgGetChaoWheelOptionsSucceed msg)
	{
		if (msg != null)
		{
			ServerChaoWheelOptions options = msg.m_options;
			RouletteCategory rouletteCategory = RouletteCategory.PREMIUM;
			if (options.SpinType == ServerChaoWheelOptions.ChaoSpinType.Special)
			{
				rouletteCategory = RouletteCategory.SPECIAL;
			}
			if (this.m_rouletteList == null)
			{
				this.m_rouletteList = new Dictionary<RouletteCategory, ServerWheelOptionsData>();
				this.m_rouletteList.Add(rouletteCategory, new ServerWheelOptionsData(options));
			}
			else if (this.m_rouletteList.ContainsKey(rouletteCategory))
			{
				this.m_rouletteList[rouletteCategory].Setup(options);
			}
			else
			{
				this.m_rouletteList.Add(rouletteCategory, new ServerWheelOptionsData(options));
			}
			this.RequestPrizeAuto(this.m_rouletteList[rouletteCategory]);
			this.EndLoading(RouletteCategory.PREMIUM);
			if (!this.m_initReq)
			{
				if (this.GetCallbackObject(1001) != null && this.m_rouletteList.ContainsKey(rouletteCategory))
				{
					if (RouletteManager.IsRouletteEnabled())
					{
						this.GetCallbackObject(1001).SendMessage("RequestGetRoulette_Succeeded", this.m_rouletteList[rouletteCategory], SendMessageOptions.DontRequireReceiver);
						this.UpdateChangeBotton(rouletteCategory);
					}
					else
					{
						global::Debug.Log("RouletteManager ServerGetChaoWheelOptions_Succeeded RouletteTop:false");
					}
				}
			}
			else if (this.m_initReqCallback != null)
			{
				this.m_initReqCallback(this.specialEgg);
				this.m_initReqCallback = null;
				this.m_initReq = false;
			}
		}
	}

	// Token: 0x0600470E RID: 18190 RVA: 0x00174FEC File Offset: 0x001731EC
	private void ServerGetWheelOptions_Succeeded(MsgGetWheelOptionsSucceed msg)
	{
		if (msg != null)
		{
			ServerWheelOptions wheelOptions = msg.m_wheelOptions;
			if (wheelOptions != null)
			{
				if (wheelOptions.m_numJackpotRing >= 30000)
				{
					RouletteManager.s_numJackpotRing = wheelOptions.m_numJackpotRing;
				}
				if (this.m_rouletteList == null)
				{
					this.m_rouletteList = new Dictionary<RouletteCategory, ServerWheelOptionsData>();
					this.m_rouletteList.Add(RouletteCategory.ITEM, new ServerWheelOptionsData(wheelOptions));
				}
				else if (this.m_rouletteList.ContainsKey(RouletteCategory.ITEM))
				{
					this.m_rouletteList[RouletteCategory.ITEM].Setup(wheelOptions);
				}
				else
				{
					this.m_rouletteList.Add(RouletteCategory.ITEM, new ServerWheelOptionsData(wheelOptions));
				}
				this.EndLoading(RouletteCategory.ITEM);
				if (this.m_isCurrentPrizeLoading == RouletteCategory.ITEM)
				{
					ServerPrizeState value = this.CreatePrizeList(RouletteCategory.ITEM);
					if (this.m_prizeCallback != null)
					{
						this.m_prizeCallback.SendMessage("RequestRoulettePrize_Succeeded", value, SendMessageOptions.DontRequireReceiver);
					}
					this.m_prizeCallback = null;
					this.m_isCurrentPrizeLoading = RouletteCategory.NONE;
				}
				else
				{
					this.RequestPrizeAuto(this.m_rouletteList[RouletteCategory.ITEM]);
					if (this.GetCallbackObject(1002) != null && this.m_rouletteList.ContainsKey(RouletteCategory.ITEM))
					{
						if (RouletteManager.IsRouletteEnabled() || RouletteUtility.rouletteDefault == RouletteCategory.ITEM)
						{
							this.GetCallbackObject(1002).SendMessage("RequestGetRoulette_Succeeded", this.m_rouletteList[RouletteCategory.ITEM], SendMessageOptions.DontRequireReceiver);
							this.UpdateChangeBotton(RouletteCategory.ITEM);
						}
						else
						{
							global::Debug.Log("RouletteManager ServerGetWheelOptions_Succeeded RouletteTop:false");
						}
					}
				}
			}
		}
	}

	// Token: 0x0600470F RID: 18191 RVA: 0x00175168 File Offset: 0x00173368
	private void ServerCommitWheelSpinGeneral_Succeeded(MsgCommitWheelSpinGeneralSucceed msg)
	{
		global::Debug.Log("RouletteManager ServerCommitWheelSpinGeneral_Succeeded !!!");
		if (msg != null)
		{
			ServerWheelOptionsData.SPIN_BUTTON spin_BUTTON = ServerWheelOptionsData.SPIN_BUTTON.FREE;
			RouletteCategory rouletteCategory = RouletteCategory.NONE;
			ServerSpinResultGeneral resultSpinResultGeneral = msg.m_resultSpinResultGeneral;
			this.m_currentRankup = false;
			ServerWheelOptionsData serverWheelOptionsData = new ServerWheelOptionsData(msg.m_wheelOptionsGeneral);
			if (serverWheelOptionsData != null)
			{
				rouletteCategory = serverWheelOptionsData.category;
			}
			if (this.m_rouletteList != null && this.m_rouletteList.ContainsKey(rouletteCategory))
			{
				spin_BUTTON = this.m_rouletteList[rouletteCategory].GetSpinButtonSeting();
			}
			if (rouletteCategory != RouletteCategory.NONE)
			{
				ServerWheelOptionsData serverWheelOptionsData2 = null;
				if (this.m_rouletteList != null && this.m_rouletteList.ContainsKey(rouletteCategory))
				{
					serverWheelOptionsData2 = this.m_rouletteList[rouletteCategory];
				}
				int itemWon = resultSpinResultGeneral.ItemWon;
				if (serverWheelOptionsData2 != null)
				{
					if (resultSpinResultGeneral.ItemWon >= 0 && serverWheelOptionsData2.GetRouletteRank() != RouletteUtility.WheelRank.Super)
					{
						int num = 0;
						if (serverWheelOptionsData2.GetCellItem(resultSpinResultGeneral.ItemWon, out num).idType == ServerItem.IdType.ITEM_ROULLETE_WIN)
						{
							this.m_currentRankup = true;
						}
						else
						{
							this.m_currentRankup = false;
						}
					}
					else
					{
						this.m_currentRankup = false;
					}
				}
				else
				{
					if (this.m_rouletteList != null)
					{
						this.m_rouletteList.Add(rouletteCategory, serverWheelOptionsData);
					}
					global::Debug.Log("RouletteManager ServerCommitWheelSpinGeneral_Succeeded error?");
				}
				this.m_rouletteItemBak = null;
				this.m_rouletteChaoBak = null;
				this.m_rouletteGeneralBakCategory = RouletteCategory.NONE;
				this.m_rouletteGeneralBak = msg.m_wheelOptionsGeneral;
				if (this.m_rouletteGeneralBak != null)
				{
					this.m_rouletteGeneralBakCategory = rouletteCategory;
				}
				this.EndLoading(RouletteCategory.GENERAL);
				if (rouletteCategory == RouletteCategory.PREMIUM)
				{
					FoxManager.SendLtvPointPremiumRoulette(spin_BUTTON == ServerWheelOptionsData.SPIN_BUTTON.FREE);
				}
				if (this.GetCallbackObject(serverWheelOptionsData.rouletteId) != null)
				{
					if (RouletteManager.IsRouletteEnabled())
					{
						this.m_resultGeneral = resultSpinResultGeneral;
						this.m_resultData = null;
						this.m_resultGeneral.ItemWon = itemWon;
						this.m_lastCommitCategory = rouletteCategory;
						this.GetCallbackObject(serverWheelOptionsData.rouletteId).SendMessage("RequestCommitRoulette_Succeeded", serverWheelOptionsData, SendMessageOptions.DontRequireReceiver);
					}
					else
					{
						global::Debug.Log("RouletteManager ServerCommitWheelSpinGeneral_Succeeded RouletteTop:false");
					}
				}
			}
		}
	}

	// Token: 0x06004710 RID: 18192 RVA: 0x00175360 File Offset: 0x00173560
	private void ServerCommitWheelSpinGeneral_Failed(MsgServerConnctFailed msg)
	{
		if (this.GetCallbackObject(9) != null)
		{
			if (RouletteManager.IsRouletteEnabled())
			{
				this.GetCallbackObject(9).SendMessage("RequestCommitRoulette_Failed", SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				global::Debug.Log("RouletteManager ServerCommitWheelSpinGeneral_Failed RouletteTop:false");
			}
		}
	}

	// Token: 0x06004711 RID: 18193 RVA: 0x001753AC File Offset: 0x001735AC
	private void ServerCommitChaoWheelSpin_Succeeded(MsgCommitChaoWheelSpicSucceed msg)
	{
		if (msg != null)
		{
			ServerSpinResultGeneral resultSpinResultGeneral = msg.m_resultSpinResultGeneral;
			this.m_currentRankup = false;
			bool flag = false;
			ServerChaoWheelOptions.ChaoSpinType spinType = msg.m_chaoWheelOptions.SpinType;
			ServerWheelOptionsData.SPIN_BUTTON spin_BUTTON = ServerWheelOptionsData.SPIN_BUTTON.FREE;
			if (this.m_rouletteList != null && this.m_rouletteList.ContainsKey(RouletteCategory.PREMIUM))
			{
				spin_BUTTON = this.m_rouletteList[RouletteCategory.PREMIUM].GetSpinButtonSeting();
			}
			RouletteCategory rouletteCategory;
			if (spinType == ServerChaoWheelOptions.ChaoSpinType.Special)
			{
				if (this.m_rouletteList != null && this.m_rouletteList.ContainsKey(RouletteCategory.SPECIAL))
				{
					ServerWheelOptionsData serverWheelOptionsData = this.m_rouletteList[RouletteCategory.SPECIAL];
				}
				rouletteCategory = RouletteCategory.SPECIAL;
			}
			else
			{
				if (this.m_rouletteList != null && this.m_rouletteList.ContainsKey(RouletteCategory.PREMIUM))
				{
					ServerWheelOptionsData serverWheelOptionsData = this.m_rouletteList[RouletteCategory.PREMIUM];
					if (serverWheelOptionsData.category == RouletteCategory.PREMIUM)
					{
						flag = true;
					}
				}
				rouletteCategory = RouletteCategory.PREMIUM;
			}
			if (this.m_rouletteList == null || !this.m_rouletteList.ContainsKey(rouletteCategory))
			{
				ServerWheelOptionsData serverWheelOptionsData = new ServerWheelOptionsData(msg.m_chaoWheelOptions);
				if (this.m_rouletteList != null)
				{
					this.m_rouletteList.Add(rouletteCategory, serverWheelOptionsData);
				}
			}
			this.m_currentRankup = false;
			this.m_rouletteItemBak = null;
			this.m_rouletteChaoBak = msg.m_chaoWheelOptions;
			this.m_rouletteGeneralBakCategory = RouletteCategory.NONE;
			this.m_rouletteChaoBakCategory = RouletteCategory.NONE;
			this.m_rouletteGeneralBak = null;
			if (this.m_rouletteChaoBak != null)
			{
				this.m_rouletteChaoBakCategory = rouletteCategory;
			}
			this.EndLoading(RouletteCategory.PREMIUM);
			if (flag)
			{
				FoxManager.SendLtvPointPremiumRoulette(spin_BUTTON == ServerWheelOptionsData.SPIN_BUTTON.FREE);
			}
			if (this.GetCallbackObject(1001) != null)
			{
				if (RouletteManager.IsRouletteEnabled())
				{
					if (resultSpinResultGeneral != null)
					{
						this.m_resultGeneral = resultSpinResultGeneral;
						this.m_resultData = null;
					}
					else
					{
						this.m_resultGeneral = null;
					}
					this.m_lastCommitCategory = rouletteCategory;
					this.GetCallbackObject(1001).SendMessage("RequestCommitRoulette_Succeeded", this.m_rouletteList[rouletteCategory], SendMessageOptions.DontRequireReceiver);
				}
				else
				{
					global::Debug.Log("RouletteManager ServerCommitChaoWheelSpin_Succeeded RouletteTop:false");
				}
			}
		}
	}

	// Token: 0x06004712 RID: 18194 RVA: 0x00175598 File Offset: 0x00173798
	private void ServerCommitChaoWheelSpin_Failed(MsgServerConnctFailed msg)
	{
		if (this.GetCallbackObject(1001) != null)
		{
			if (RouletteManager.IsRouletteEnabled())
			{
				this.GetCallbackObject(1001).SendMessage("RequestCommitRoulette_Failed", SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				global::Debug.Log("RouletteManager ServerCommitChaoWheelSpin_Failed RouletteTop:false");
			}
		}
	}

	// Token: 0x06004713 RID: 18195 RVA: 0x001755EC File Offset: 0x001737EC
	private void ServerCommitWheelSpin_Succeeded(MsgCommitWheelSpinSucceed msg)
	{
		if (msg != null)
		{
			ServerWheelOptionsData serverWheelOptionsData = null;
			if (this.m_rouletteList != null && this.m_rouletteList.ContainsKey(RouletteCategory.ITEM))
			{
				serverWheelOptionsData = this.m_rouletteList[RouletteCategory.ITEM];
			}
			if (serverWheelOptionsData != null)
			{
				if (serverWheelOptionsData.itemWonData.idType == ServerItem.IdType.ITEM_ROULLETE_WIN && serverWheelOptionsData.GetRouletteRank() != RouletteUtility.WheelRank.Super)
				{
					this.m_currentRankup = true;
				}
				else
				{
					this.m_currentRankup = false;
				}
			}
			else
			{
				if (this.m_rouletteList != null)
				{
					serverWheelOptionsData = new ServerWheelOptionsData(msg.m_wheelOptions);
					this.m_rouletteList.Add(RouletteCategory.ITEM, serverWheelOptionsData);
				}
				global::Debug.Log("RouletteManager ServerCommitWheelSpin_Succeeded error?");
			}
			this.m_rouletteItemBak = msg.m_wheelOptions;
			this.EndLoading(RouletteCategory.ITEM);
			if (this.GetCallbackObject(1002) != null)
			{
				if (RouletteManager.IsRouletteEnabled() && this.m_rouletteList != null && this.m_rouletteList.ContainsKey(RouletteCategory.ITEM) && this.m_rouletteItemBak != null)
				{
					serverWheelOptionsData = new ServerWheelOptionsData(this.m_rouletteItemBak);
					if (msg.m_resultSpinResultGeneral != null)
					{
						this.m_resultData = null;
						this.m_resultGeneral = msg.m_resultSpinResultGeneral;
					}
					else
					{
						this.m_resultData = null;
						this.m_resultGeneral = new ServerSpinResultGeneral(this.m_rouletteItemBak, this.m_rouletteList[RouletteCategory.ITEM].GetOrgRankupData());
					}
					this.m_lastCommitCategory = RouletteCategory.ITEM;
					this.GetCallbackObject(1002).SendMessage("RequestCommitRoulette_Succeeded", serverWheelOptionsData, SendMessageOptions.DontRequireReceiver);
				}
				else
				{
					global::Debug.Log("RouletteManager ServerCommitWheelSpin_Succeeded RouletteTop:false");
				}
			}
		}
	}

	// Token: 0x06004714 RID: 18196 RVA: 0x00175778 File Offset: 0x00173978
	private void ServerCommitWheelSpin_Failed(MsgServerConnctFailed msg)
	{
		if (this.GetCallbackObject(1002) != null)
		{
			if (RouletteManager.IsRouletteEnabled())
			{
				this.GetCallbackObject(1002).SendMessage("RequestCommitRoulette_Failed", SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				global::Debug.Log("RouletteManager ServerCommitWheelSpin_Failed RouletteTop:false");
			}
		}
	}

	// Token: 0x06004715 RID: 18197 RVA: 0x001757CC File Offset: 0x001739CC
	private void ServerGetPrizeChaoWheelSpin_Succeeded(MsgGetPrizeChaoWheelSpinSucceed msg)
	{
		RouletteCategory rouletteCategory = (msg.m_type != 0) ? RouletteCategory.SPECIAL : RouletteCategory.PREMIUM;
		if (this.m_prizeList == null)
		{
			this.m_prizeList = new Dictionary<RouletteCategory, ServerPrizeState>();
		}
		this.SetPrizeList(rouletteCategory, msg.m_prizeState);
		if (rouletteCategory != RouletteCategory.NONE && this.m_prizeCallback != null)
		{
			this.m_isCurrentPrizeLoading = RouletteCategory.NONE;
			if (RouletteManager.IsRouletteEnabled())
			{
				this.m_prizeCallback.SendMessage("RequestRoulettePrize_Succeeded", this.m_prizeList[rouletteCategory], SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				global::Debug.Log("RouletteManager ServerGetPrizeChaoWheelSpin_Succeeded RouletteTop:false");
			}
		}
		this.m_isCurrentPrizeLoading = RouletteCategory.NONE;
		this.m_prizeCallback = null;
	}

	// Token: 0x06004716 RID: 18198 RVA: 0x00175874 File Offset: 0x00173A74
	private void ServerGetPrizeWheelSpinGeneral_Succeeded(MsgGetPrizeWheelSpinGeneralSucceed msg)
	{
		RouletteCategory rouletteCategory = RouletteCategory.RAID;
		if (this.m_prizeList == null)
		{
			this.m_prizeList = new Dictionary<RouletteCategory, ServerPrizeState>();
		}
		this.SetPrizeList(rouletteCategory, msg.prizeState);
		if (rouletteCategory != RouletteCategory.NONE && this.m_prizeCallback != null)
		{
			this.m_isCurrentPrizeLoading = RouletteCategory.NONE;
			if (RouletteManager.IsRouletteEnabled())
			{
				this.m_prizeCallback.SendMessage("RequestRoulettePrize_Succeeded", this.m_prizeList[rouletteCategory], SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				global::Debug.Log("RouletteManager ServerGetPrizeWheelSpinGeneral_Succeeded RouletteTop:false");
			}
		}
		this.m_isCurrentPrizeLoading = RouletteCategory.NONE;
		this.m_prizeCallback = null;
	}

	// Token: 0x06004717 RID: 18199 RVA: 0x00175908 File Offset: 0x00173B08
	private void ServerGetPrizeChaoWheelSpin_Failed()
	{
		RouletteCategory isCurrentPrizeLoading = this.m_isCurrentPrizeLoading;
		if (isCurrentPrizeLoading != RouletteCategory.NONE && this.m_prizeCallback != null)
		{
			this.m_isCurrentPrizeLoading = RouletteCategory.NONE;
			if (RouletteManager.IsRouletteEnabled())
			{
				this.m_prizeCallback.SendMessage("RequestRoulettePrize_Failed", SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				global::Debug.Log("RouletteManager ServerGetPrizeChaoWheelSpin_Failed RouletteTop:false");
			}
		}
		this.m_isCurrentPrizeLoading = RouletteCategory.NONE;
		this.m_prizeCallback = null;
	}

	// Token: 0x06004718 RID: 18200 RVA: 0x00175974 File Offset: 0x00173B74
	private void ServerGetPrizeWheelSpinGeneral_Failed()
	{
		RouletteCategory isCurrentPrizeLoading = this.m_isCurrentPrizeLoading;
		if (isCurrentPrizeLoading != RouletteCategory.NONE && this.m_prizeCallback != null)
		{
			this.m_isCurrentPrizeLoading = RouletteCategory.NONE;
			if (RouletteManager.IsRouletteEnabled())
			{
				this.m_prizeCallback.SendMessage("RequestRoulettePrize_Failed", SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				global::Debug.Log("RouletteManager ServerGetPrizeWheelSpinGeneral_Failed RouletteTop:false");
			}
		}
		this.m_isCurrentPrizeLoading = RouletteCategory.NONE;
		this.m_prizeCallback = null;
	}

	// Token: 0x06004719 RID: 18201 RVA: 0x001759E0 File Offset: 0x00173BE0
	private void ServerAddSpecialEgg_Succeeded(MsgAddSpecialEggSucceed msg)
	{
	}

	// Token: 0x0600471A RID: 18202 RVA: 0x001759E4 File Offset: 0x00173BE4
	private void ServerAddSpecialEgg_Failed(MsgServerConnctFailed msg)
	{
	}

	// Token: 0x0600471B RID: 18203 RVA: 0x001759E8 File Offset: 0x00173BE8
	private void ServerRetrievePlayerState_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		this.specialEgg = msg.m_playerState.GetNumItemById(220000);
	}

	// Token: 0x0600471C RID: 18204 RVA: 0x00175A00 File Offset: 0x00173C00
	public ServerWheelOptionsData GetRouletteDataOrg(RouletteCategory category)
	{
		ServerWheelOptionsData serverWheelOptionsData = null;
		if (category != RouletteCategory.NONE && category != RouletteCategory.ALL && this.m_rouletteList != null && this.m_rouletteList.ContainsKey(category))
		{
			ServerWheelOptionsData serverWheelOptionsData2 = this.m_rouletteList[category];
			if (serverWheelOptionsData2.isValid)
			{
				serverWheelOptionsData = serverWheelOptionsData2;
				if (category == RouletteCategory.PREMIUM && this.specialEgg >= 10 && !RouletteUtility.isTutorial && serverWheelOptionsData != null && serverWheelOptionsData.category == RouletteCategory.PREMIUM)
				{
					serverWheelOptionsData = null;
				}
			}
		}
		return serverWheelOptionsData;
	}

	// Token: 0x0600471D RID: 18205 RVA: 0x00175A88 File Offset: 0x00173C88
	public Dictionary<RouletteCategory, ServerWheelOptionsData> GetRouletteDataAllOrg()
	{
		Dictionary<RouletteCategory, ServerWheelOptionsData> dictionary = null;
		if (this.m_rouletteList != null && this.m_rouletteList.Count > 0)
		{
			Dictionary<RouletteCategory, ServerWheelOptionsData>.KeyCollection keys = this.m_rouletteList.Keys;
			foreach (RouletteCategory key in keys)
			{
				if (this.m_rouletteList[key].isValid)
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<RouletteCategory, ServerWheelOptionsData>();
					}
					dictionary.Add(key, this.m_rouletteList[key]);
				}
			}
		}
		return dictionary;
	}

	// Token: 0x0600471E RID: 18206 RVA: 0x00175B44 File Offset: 0x00173D44
	public static ServerWheelOptionsData GetRouletteData(RouletteCategory category)
	{
		ServerWheelOptionsData result = null;
		if (RouletteManager.s_instance != null)
		{
			result = RouletteManager.s_instance.GetRouletteDataOrg(category);
		}
		return result;
	}

	// Token: 0x0600471F RID: 18207 RVA: 0x00175B70 File Offset: 0x00173D70
	public static Dictionary<RouletteCategory, ServerWheelOptionsData> GetRouletteDataAll()
	{
		Dictionary<RouletteCategory, ServerWheelOptionsData> result = null;
		if (RouletteManager.s_instance != null)
		{
			result = RouletteManager.s_instance.GetRouletteDataAllOrg();
		}
		return result;
	}

	// Token: 0x06004720 RID: 18208 RVA: 0x00175B9C File Offset: 0x00173D9C
	public GameObject GetCallbackObject(int key)
	{
		GameObject result = null;
		if (this.m_callbackList != null)
		{
			if (this.m_callbackList.ContainsKey(key))
			{
				result = this.m_callbackList[key];
			}
			else if (key == 8 && this.m_callbackList.ContainsKey(1))
			{
				result = this.m_callbackList[1];
			}
		}
		return result;
	}

	// Token: 0x06004721 RID: 18209 RVA: 0x00175C00 File Offset: 0x00173E00
	public void SetCallbackObject(int key, GameObject obj)
	{
		if (this.m_callbackList == null)
		{
			this.m_callbackList = new Dictionary<int, GameObject>();
			this.m_callbackList.Add(key, obj);
		}
		else if (this.m_callbackList.ContainsKey(key))
		{
			this.m_callbackList[key] = obj;
		}
		else
		{
			this.m_callbackList.Add(key, obj);
		}
	}

	// Token: 0x06004722 RID: 18210 RVA: 0x00175C68 File Offset: 0x00173E68
	private void RequestPrizeAuto(ServerWheelOptionsData data)
	{
		this.m_isCurrentPrizeLoadingAuto = RouletteCategory.NONE;
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (data != null && loggedInServerInterface != null)
		{
			RouletteCategory category = data.category;
			if (this.m_prizeList != null && this.m_prizeList.ContainsKey(category) && this.m_prizeList[category] != null && this.m_prizeList[category].IsData())
			{
				return;
			}
			if (category != RouletteCategory.NONE && category != RouletteCategory.ALL && category != RouletteCategory.ITEM)
			{
				if (data.isGeneral)
				{
					int eventId = 0;
					int spinType = 0;
					if (EventManager.Instance != null)
					{
						eventId = EventManager.Instance.Id;
					}
					loggedInServerInterface.RequestServerGetPrizeWheelSpinGeneral(eventId, spinType, null);
				}
				else
				{
					int chaoWheelSpinType = (category != RouletteCategory.SPECIAL) ? 0 : 1;
					loggedInServerInterface.RequestServerGetPrizeChaoWheelSpin(chaoWheelSpinType, null);
				}
				this.m_isCurrentPrizeLoadingAuto = category;
				global::Debug.Log("RequestPrizeAuto category:" + data.category + " isReq:true");
			}
			else
			{
				global::Debug.Log("RequestPrizeAuto category:" + data.category + " isReq:false");
			}
		}
	}

	// Token: 0x17000995 RID: 2453
	// (get) Token: 0x06004723 RID: 18211 RVA: 0x00175D90 File Offset: 0x00173F90
	public static RouletteManager Instance
	{
		get
		{
			return RouletteManager.s_instance;
		}
	}

	// Token: 0x06004724 RID: 18212 RVA: 0x00175D98 File Offset: 0x00173F98
	private void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x06004725 RID: 18213 RVA: 0x00175DA0 File Offset: 0x00173FA0
	private void OnDestroy()
	{
		if (RouletteManager.s_instance == this)
		{
			RouletteManager.s_instance = null;
		}
	}

	// Token: 0x06004726 RID: 18214 RVA: 0x00175DB8 File Offset: 0x00173FB8
	private void SetInstance()
	{
		if (RouletteManager.s_instance == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			RouletteManager.s_instance = this;
			RouletteManager.s_instance.Init();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06004727 RID: 18215 RVA: 0x00175E00 File Offset: 0x00174000
	private static string GetText(string cellName, Dictionary<string, string> dicReplaces = null)
	{
		string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ItemRoulette", cellName).text;
		if (dicReplaces != null)
		{
			text = TextUtility.Replaces(text, dicReplaces);
		}
		return text;
	}

	// Token: 0x06004728 RID: 18216 RVA: 0x00175E30 File Offset: 0x00174030
	private static string GetText(string cellName, string srcText, string dstText)
	{
		return RouletteManager.GetText(cellName, new Dictionary<string, string>
		{
			{
				srcText,
				dstText
			}
		});
	}

	// Token: 0x04003AE9 RID: 15081
	public const string REQUEST_GET_SUCCEEDED = "RequestGetRoulette_Succeeded";

	// Token: 0x04003AEA RID: 15082
	public const string REQUEST_GET_FAILED = "RequestGetRoulette_Failed";

	// Token: 0x04003AEB RID: 15083
	public const string REQUEST_COMMIT_SUCCEEDED = "RequestCommitRoulette_Succeeded";

	// Token: 0x04003AEC RID: 15084
	public const string REQUEST_COMMIT_FAILED = "RequestCommitRoulette_Failed";

	// Token: 0x04003AED RID: 15085
	public const string REQUEST_PRIZE_SUCCEEDED = "RequestRoulettePrize_Succeeded";

	// Token: 0x04003AEE RID: 15086
	public const string REQUEST_PRIZE_FAILED = "RequestRoulettePrize_Failed";

	// Token: 0x04003AEF RID: 15087
	public const string REQUEST_BASIC_INFO_SUCCEEDED = "RequestBasicInfo_Succeeded";

	// Token: 0x04003AF0 RID: 15088
	public const string REQUEST_BASIC_INFO_FAILED = "RequestBasicInfo_Failed";

	// Token: 0x04003AF1 RID: 15089
	private const float DUMMY_COMMIT_DELAY = 2f;

	// Token: 0x04003AF2 RID: 15090
	private Dictionary<int, GameObject> m_callbackList;

	// Token: 0x04003AF3 RID: 15091
	private GameObject m_prizeCallback;

	// Token: 0x04003AF4 RID: 15092
	private GameObject m_basicInfoCallback;

	// Token: 0x04003AF5 RID: 15093
	private Dictionary<RouletteCategory, ServerWheelOptionsData> m_rouletteList;

	// Token: 0x04003AF6 RID: 15094
	private Dictionary<RouletteCategory, ServerPrizeState> m_prizeList;

	// Token: 0x04003AF7 RID: 15095
	private ServerWheelOptions m_rouletteItemBak;

	// Token: 0x04003AF8 RID: 15096
	private ServerWheelOptionsGeneral m_rouletteGeneralBak;

	// Token: 0x04003AF9 RID: 15097
	private ServerChaoWheelOptions m_rouletteChaoBak;

	// Token: 0x04003AFA RID: 15098
	private ServerChaoSpinResult m_resultData;

	// Token: 0x04003AFB RID: 15099
	private ServerSpinResultGeneral m_resultGeneral;

	// Token: 0x04003AFC RID: 15100
	private RouletteCategory m_rouletteGeneralBakCategory;

	// Token: 0x04003AFD RID: 15101
	private RouletteCategory m_rouletteChaoBakCategory;

	// Token: 0x04003AFE RID: 15102
	private Dictionary<RouletteCategory, float> m_loadingList;

	// Token: 0x04003AFF RID: 15103
	private RouletteCategory m_isCurrentPrizeLoading;

	// Token: 0x04003B00 RID: 15104
	private RouletteCategory m_isCurrentPrizeLoadingAuto;

	// Token: 0x04003B01 RID: 15105
	private RouletteCategory m_lastCommitCategory;

	// Token: 0x04003B02 RID: 15106
	private RouletteTop m_rouletteTop;

	// Token: 0x04003B03 RID: 15107
	private EasySnsFeed m_easySnsFeed;

	// Token: 0x04003B04 RID: 15108
	private List<RouletteCategory> m_basicRouletteCategorys;

	// Token: 0x04003B05 RID: 15109
	private DateTime m_basicRouletteCategorysGetLastTime;

	// Token: 0x04003B06 RID: 15110
	private int m_requestRouletteId;

	// Token: 0x04003B07 RID: 15111
	private bool m_currentRankup;

	// Token: 0x04003B08 RID: 15112
	private bool m_networkError;

	// Token: 0x04003B09 RID: 15113
	private static bool s_multiGetWindow;

	// Token: 0x04003B0A RID: 15114
	private float m_updateRouletteDelay;

	// Token: 0x04003B0B RID: 15115
	private static RouletteUtility.AchievementType m_achievementType;

	// Token: 0x04003B0C RID: 15116
	private static RouletteUtility.NextType m_nextType;

	// Token: 0x04003B0D RID: 15117
	private static bool s_isShowGetWindow;

	// Token: 0x04003B0E RID: 15118
	private static int s_numJackpotRing;

	// Token: 0x04003B0F RID: 15119
	private List<ServerItem.Id> m_rouletteCostItemIdList;

	// Token: 0x04003B10 RID: 15120
	private float m_rouletteCostItemIdListGetTime = -1f;

	// Token: 0x04003B11 RID: 15121
	private GameObject m_dummyCallback;

	// Token: 0x04003B12 RID: 15122
	private ServerWheelOptionsData m_dummyData;

	// Token: 0x04003B13 RID: 15123
	private float m_dummyTime;

	// Token: 0x04003B14 RID: 15124
	private Dictionary<RouletteCategory, List<CharaType>> m_picupCharaList;

	// Token: 0x04003B15 RID: 15125
	private bool m_isPicupCharaListInit;

	// Token: 0x04003B16 RID: 15126
	private DateTime m_picupCharaListTime;

	// Token: 0x04003B17 RID: 15127
	private bool m_initReq;

	// Token: 0x04003B18 RID: 15128
	private RouletteManager.CallbackRouletteInit m_initReqCallback;

	// Token: 0x04003B19 RID: 15129
	private string m_oldBgm;

	// Token: 0x04003B1A RID: 15130
	private bool m_bgmReset;

	// Token: 0x04003B1B RID: 15131
	private static RouletteManager s_instance;

	// Token: 0x02000AB7 RID: 2743
	// (Invoke) Token: 0x06004916 RID: 18710
	public delegate void CallbackRouletteInit(int specialEggNum);
}
