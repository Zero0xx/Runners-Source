using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020004FD RID: 1277
public class RankingUI : MonoBehaviour
{
	// Token: 0x060025F5 RID: 9717 RVA: 0x000E692C File Offset: 0x000E4B2C
	private void SetRankingMode(RankingUtil.RankingMode mode)
	{
		this.RankerToggleChange(RankingUtil.RankingRankerType.RIVAL);
		global::Debug.Log(string.Concat(new object[]
		{
			"RankingUI SetRankingMode mode:",
			mode,
			" old:",
			this.m_currentMode
		}));
		if (mode != this.m_currentMode || this.m_currentMode == RankingUtil.RankingMode.COUNT)
		{
			this.m_currentMode = mode;
			this.m_isDrawInit = false;
			this.m_isHelp = false;
			if (this.m_loading != null)
			{
				this.m_loading.SetActive(true);
			}
			this.m_callbackTemporarilySavedDelay = 0.25f;
		}
		this.Init();
		RankingUtil.SetCurrentRankingMode(mode);
	}

	// Token: 0x060025F6 RID: 9718 RVA: 0x000E69D8 File Offset: 0x000E4BD8
	public void SetDisplayEndlessModeOn()
	{
		if (!this.m_isInitilalized)
		{
			this.Init();
		}
		this.SetDisplay(true, RankingUtil.RankingMode.ENDLESS);
	}

	// Token: 0x060025F7 RID: 9719 RVA: 0x000E69F4 File Offset: 0x000E4BF4
	public void SetDisplayEndlessModeOff()
	{
		this.SetDisplay(false, RankingUtil.RankingMode.ENDLESS);
	}

	// Token: 0x060025F8 RID: 9720 RVA: 0x000E6A00 File Offset: 0x000E4C00
	public void SetDisplayQuickModeOn()
	{
		if (!this.m_isInitilalized)
		{
			this.Init();
		}
		this.SetDisplay(true, RankingUtil.RankingMode.QUICK);
	}

	// Token: 0x060025F9 RID: 9721 RVA: 0x000E6A1C File Offset: 0x000E4C1C
	public void SetDisplayQuickModeOff()
	{
		this.SetDisplay(false, RankingUtil.RankingMode.QUICK);
	}

	// Token: 0x060025FA RID: 9722 RVA: 0x000E6A28 File Offset: 0x000E4C28
	public void SetDisplay(bool displayFlag, RankingUtil.RankingMode mode)
	{
		this.m_displayFlag = displayFlag;
		base.gameObject.SetActive(this.m_displayFlag);
		if (this.m_displayFlag)
		{
			this.SetRankingMode(mode);
		}
		if (this.m_first && displayFlag && this.m_loading != null)
		{
			this.m_loading.SetActive(true);
		}
		if (this.m_parts != null)
		{
			this.m_parts.SetActive(this.m_displayFlag);
		}
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_ranking_word");
		if (uisprite != null)
		{
			if (mode == RankingUtil.RankingMode.QUICK)
			{
				uisprite.spriteName = "ui_mm_mode_word_quick";
			}
			else
			{
				uisprite.spriteName = "ui_mm_mode_word_endless";
			}
		}
		if (this.m_colorObjects1 != null && this.m_colorObjects2 != null)
		{
			Color color;
			Color color2;
			if (mode == RankingUtil.RankingMode.QUICK)
			{
				color = this.m_quickModeColor1;
				color2 = this.m_quickModeColor2;
			}
			else
			{
				color = this.m_endlessModeColor1;
				color2 = this.m_endlessModeColor2;
			}
			foreach (UISprite uisprite2 in this.m_colorObjects1)
			{
				if (uisprite2 != null)
				{
					uisprite2.color = color;
				}
			}
			foreach (UISprite uisprite3 in this.m_colorObjects2)
			{
				if (uisprite3 != null)
				{
					uisprite3.color = color2;
				}
			}
		}
	}

	// Token: 0x060025FB RID: 9723 RVA: 0x000E6BFC File Offset: 0x000E4DFC
	public void SetLoadingObject()
	{
		if (this.m_tallying != null)
		{
			this.m_tallying.SetActive(false);
		}
	}

	// Token: 0x060025FC RID: 9724 RVA: 0x000E6C1C File Offset: 0x000E4E1C
	public bool IsInitLoading()
	{
		return this.m_loading != null && this.m_loading.activeSelf;
	}

	// Token: 0x060025FD RID: 9725 RVA: 0x000E6C3C File Offset: 0x000E4E3C
	public RankingUtil.Ranker GetCurrentRanker(int slot)
	{
		RankingUtil.Ranker result = null;
		if (this.m_currentRankerList != null && slot >= 0 && this.m_currentRankerList.Count > slot + 1)
		{
			result = this.m_currentRankerList[slot + 1];
		}
		return result;
	}

	// Token: 0x060025FE RID: 9726 RVA: 0x000E6C80 File Offset: 0x000E4E80
	private void Start()
	{
		foreach (UIToggle uitoggle in this.m_partsBtnToggls)
		{
			if (uitoggle != null)
			{
				if (uitoggle.gameObject.name == "Btn_all")
				{
					EventDelegate.Add(uitoggle.onChange, new EventDelegate.Callback(this.OnAllToggleChange));
				}
				else if (uitoggle.gameObject.name == "Btn_friend")
				{
					EventDelegate.Add(uitoggle.onChange, new EventDelegate.Callback(this.OnFriendToggleChange));
				}
				else if (uitoggle.gameObject.name == "Btn_history")
				{
					EventDelegate.Add(uitoggle.onChange, new EventDelegate.Callback(this.OnHistoryToggleChange));
				}
				else if (uitoggle.gameObject.name == "Btn_rival")
				{
					EventDelegate.Add(uitoggle.onChange, new EventDelegate.Callback(this.OnRivalToggleChange));
				}
			}
		}
	}

	// Token: 0x060025FF RID: 9727 RVA: 0x000E6DC0 File Offset: 0x000E4FC0
	public static void DebugInitRankingChange()
	{
		if (RankingUI.s_instance != null)
		{
			RankingUI.s_instance.InitRankingChange();
		}
	}

	// Token: 0x06002600 RID: 9728 RVA: 0x000E6DDC File Offset: 0x000E4FDC
	private void InitRankingChange()
	{
		this.m_rankingChange = SingletonGameObject<RankingManager>.Instance.GetRankingRankChange(RankingUtil.currentRankingMode);
		if (this.m_rankingChange != RankingUtil.RankChange.UP)
		{
			this.m_rankingChange = RankingUtil.RankChange.NONE;
		}
		this.m_rankingChangeTime = 0f;
	}

	// Token: 0x06002601 RID: 9729 RVA: 0x000E6E14 File Offset: 0x000E5014
	private void Init()
	{
		if (this.m_snsLogin)
		{
			return;
		}
		this.m_snsCompGetRanking = false;
		this.m_snsCompGetRankingTime = 0f;
		this.m_first = true;
		this.m_isInitilalized = false;
		this.m_rankingInitloadingTime = 0f;
	}

	// Token: 0x06002602 RID: 9730 RVA: 0x000E6E50 File Offset: 0x000E5050
	private void InitSetting()
	{
		this.m_callbackTemporarilySavedDelay = 0.25f;
		this.m_facebookLockInit = false;
		if (this.m_loading != null)
		{
			this.m_loading.SetActive(true);
		}
		if (this.m_tallying != null)
		{
			this.m_tallying.SetActive(false);
		}
		if (this.m_rankingInitDraw)
		{
			this.m_rankingChange = SingletonGameObject<RankingManager>.Instance.GetRankingRankChange(RankingUtil.currentRankingMode);
			if (this.m_rankingChange != RankingUtil.RankChange.UP)
			{
				this.m_rankingChange = RankingUtil.RankChange.NONE;
			}
			this.m_rankingChangeTime = 0f;
		}
		this.SetupLeague();
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface == null)
		{
			ServerInterface.DebugInit();
		}
		RankingUI.s_socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		global::Debug.Log("RankingUI  Init !!!!!");
		this.m_currentRankerType = RankingUtil.RankingRankerType.RIVAL;
		this.m_currentScoreType = RankingManager.EndlessRivalRankingScoreType;
		RankingManager instance = SingletonGameObject<RankingManager>.Instance;
		if (instance != null && !instance.isLoading && instance.IsRankingTop(RankingUtil.currentRankingMode, RankingManager.EndlessRivalRankingScoreType, RankingUtil.RankingRankerType.RIVAL))
		{
			this.SetRanking(RankingUtil.RankingRankerType.RIVAL, RankingManager.EndlessRivalRankingScoreType, 0);
		}
		this.m_snsCompGetRankingTime = 0f;
		this.m_snsCompGetRanking = false;
		this.m_isDrawInit = false;
		this.m_isInitilalized = true;
		this.m_rankingInitloadingTime = 0f;
		this.m_btnDelay = 5;
		this.m_facebookLock = true;
		if (RegionManager.Instance != null)
		{
			this.m_facebookLock = !RegionManager.Instance.IsUseSNS();
		}
		foreach (UIImageButton uiimageButton in this.m_partsBtns)
		{
			if (uiimageButton.name.IndexOf("friend") != -1)
			{
				uiimageButton.isEnabled = !this.m_facebookLock;
			}
		}
		this.m_first = false;
	}

	// Token: 0x06002603 RID: 9731 RVA: 0x000E701C File Offset: 0x000E521C
	private bool SetRanking(RankingUtil.RankingRankerType type, RankingUtil.RankingScoreType score, int page)
	{
		if (page == -1 || this.m_currentRankerType != type || this.m_currentScoreType != score || this.m_page != page)
		{
			RankingManager instance = SingletonGameObject<RankingManager>.Instance;
			if (instance != null && !instance.isLoading)
			{
				if (page <= 0)
				{
					this.ResetRankerList(0, type);
					this.ResetRankerList(1, type);
				}
				this.m_page = page;
				this.m_currentRankerType = type;
				this.m_currentScoreType = score;
				if (this.m_page < 0)
				{
					this.m_page = 0;
				}
				if (RankingUI.s_socialInterface != null)
				{
					if (type == RankingUtil.RankingRankerType.FRIEND && !RankingUI.s_socialInterface.IsLoggedIn)
					{
						if (this.m_facebook != null)
						{
							this.m_facebook.SetActive(true);
							this.ResetRankerList(0, type);
							if (this.m_partsInfo != null)
							{
								this.m_partsInfo.text = string.Empty;
							}
							return true;
						}
					}
					else if (type == RankingUtil.RankingRankerType.FRIEND && RankingUI.s_socialInterface.IsLoggedIn)
					{
						if (this.m_facebook != null)
						{
							this.m_facebook.SetActive(false);
						}
					}
					else if (this.m_facebook != null)
					{
						this.m_facebook.SetActive(false);
					}
				}
				else
				{
					if (type == RankingUtil.RankingRankerType.SP_FRIEND)
					{
						this.m_facebook.SetActive(true);
						this.ResetRankerList(0, type);
						if (this.m_partsInfo != null)
						{
							this.m_partsInfo.text = string.Empty;
						}
						return true;
					}
					if (this.m_facebook != null)
					{
						this.m_facebook.SetActive(false);
					}
				}
				if (this.m_pattern0More != null)
				{
					this.m_pattern0More.SetActive(false);
				}
				if (this.m_loading != null)
				{
					this.m_loading.SetActive(true);
				}
				return instance.GetRanking(RankingUtil.currentRankingMode, score, type, this.m_page, new RankingManager.CallbackRankingData(this.CallbackRanking));
			}
		}
		return true;
	}

	// Token: 0x06002604 RID: 9732 RVA: 0x000E7238 File Offset: 0x000E5438
	private void ResetRankerList(int page, RankingUtil.RankingRankerType type)
	{
		if (this.m_page > 1)
		{
			return;
		}
		if (this.m_parts != null)
		{
			this.m_parts.SetActive(true);
		}
		if (page > 0)
		{
			if (type == RankingUtil.RankingRankerType.RIVAL)
			{
				if (this.m_pattern0 != null)
				{
					this.m_pattern0.SetActive(false);
				}
				if (this.m_pattern1 != null)
				{
					this.m_pattern1.SetActive(false);
				}
				if (this.m_pattern2 != null)
				{
					this.m_pattern2.SetActive(true);
				}
			}
			else
			{
				if (this.m_pattern0 != null)
				{
					this.m_pattern0.SetActive(false);
				}
				if (this.m_pattern1 != null)
				{
					this.m_pattern1.SetActive(true);
				}
				if (this.m_pattern2 != null)
				{
					this.m_pattern2.SetActive(false);
				}
			}
		}
		else
		{
			if (this.m_pattern0 != null)
			{
				this.m_pattern0.SetActive(true);
			}
			if (this.m_pattern1 != null)
			{
				this.m_pattern1.SetActive(false);
			}
			if (this.m_pattern2 != null)
			{
				this.m_pattern2.SetActive(false);
			}
		}
		if (this.m_pattern1ListArea != null)
		{
			this.m_pattern1ListArea.Reset();
		}
		if (this.m_pattern2ListArea != null)
		{
			this.m_pattern2ListArea.Reset();
		}
		if (this.m_pattern0MyDataArea != null)
		{
			this.m_pattern0MyDataArea.maxItemCount = (this.m_pattern0MyDataArea.maxRows = 0);
			this.m_pattern0MyDataArea.Restart();
		}
		if (this.m_pattern0TopRankerArea != null)
		{
			this.m_pattern0TopRankerArea.maxItemCount = (this.m_pattern0TopRankerArea.maxRows = 0);
			this.m_pattern0TopRankerArea.Restart();
		}
	}

	// Token: 0x06002605 RID: 9733 RVA: 0x000E7430 File Offset: 0x000E5630
	private void CallbackRanking(List<RankingUtil.Ranker> rankerList, RankingUtil.RankingScoreType score, RankingUtil.RankingRankerType type, int page, bool isNext, bool isPrev, bool isCashData)
	{
		if (this.m_callbackTemporarilySavedDelay > 0f)
		{
			this.m_callbackTemporarilySaved = new RankingCallbackTemporarilySaved(rankerList, score, type, page, isNext, isPrev, isCashData, new RankingManager.CallbackRankingData(this.CallbackRanking));
			return;
		}
		this.m_callbackTemporarilySavedDelay = 0f;
		global::Debug.Log(string.Concat(new object[]
		{
			"RankingUI:CallbackRanking  type:",
			type,
			"  score",
			score,
			"  num:",
			rankerList.Count,
			" isNext:",
			isNext,
			" !!!!"
		}));
		if (this.m_currentRankerType != type || this.m_currentScoreType != score)
		{
			return;
		}
		if (type == RankingUtil.RankingRankerType.RIVAL)
		{
			this.m_partsTabRival.SetActive(true);
			this.m_partsTabNormal.SetActive(false);
			this.m_partsTabFriend.SetActive(false);
		}
		else if (type == RankingUtil.RankingRankerType.FRIEND)
		{
			this.m_partsTabRival.SetActive(false);
			this.m_partsTabNormal.SetActive(false);
			this.m_partsTabFriend.SetActive(true);
			this.m_snsLogin = false;
		}
		else
		{
			this.m_partsTabRival.SetActive(false);
			this.m_partsTabNormal.SetActive(true);
			this.m_partsTabFriend.SetActive(false);
		}
		this.m_snsCompGetRankingTime = 0f;
		this.m_snsCompGetRanking = false;
		this.m_pageNext = isNext;
		this.m_pagePrev = isPrev;
		if (this.m_pattern1ListArea != null)
		{
			this.m_pattern1ListArea.rankingType = type;
		}
		if (this.m_pattern2ListArea != null)
		{
			this.m_pattern2ListArea.rankingType = type;
		}
		this.SetRankerList(rankerList, type, page);
		if (this.m_pattern1MainListPanel != null && page <= 1)
		{
			if (type == RankingUtil.RankingRankerType.RIVAL)
			{
				GameObject myDataGameObject = this.m_pattern2ListArea.GetMyDataGameObject();
				if (myDataGameObject != null)
				{
					float num = myDataGameObject.transform.localPosition.y * -1f - 166f;
					if (num < -36f)
					{
						num = -36f;
					}
					this.m_pattern2MainListPanel.transform.localPosition = new Vector3(this.m_pattern2MainListPanel.transform.localPosition.x, num, this.m_pattern2MainListPanel.transform.localPosition.z);
					this.m_pattern2MainListPanel.panel.clipRange = new Vector4(this.m_pattern2MainListPanel.panel.clipRange.x, -num, this.m_pattern2MainListPanel.panel.clipRange.z, this.m_pattern2MainListPanel.panel.clipRange.w);
					this.m_pattern2ListArea.CheckItemDrawAll(true);
				}
				else
				{
					this.m_pattern2MainListPanel.Scroll(0f);
					this.m_pattern2MainListPanel.ResetPosition();
				}
			}
			else
			{
				this.m_pattern1MainListPanel.Scroll(0f);
				this.m_pattern1MainListPanel.ResetPosition();
			}
		}
		if (isNext && this.m_pattern0More != null)
		{
			this.m_pattern0More.SetActive(true);
		}
		this.m_currentResetTimeSpan = SingletonGameObject<RankingManager>.Instance.GetRankigResetTimeSpan(RankingUtil.currentRankingMode, this.m_currentScoreType, this.m_currentRankerType);
		this.m_resetTimeSpanSec = (float)this.m_currentResetTimeSpan.Seconds + 0.1f;
		if (this.m_loading != null && this.m_rankingChange == RankingUtil.RankChange.NONE)
		{
			this.m_loading.SetActive(false);
		}
		this.SetTogglBtn();
		if (this.m_currentResetTimeSpan.Ticks <= 0L && rankerList.Count <= 1)
		{
			if (type != RankingUtil.RankingRankerType.FRIEND || (type == RankingUtil.RankingRankerType.FRIEND && this.IsActiveSnsLoginGameObject()))
			{
				if (this.m_tallying != null)
				{
					this.m_tallying.SetActive(true);
				}
			}
			else if (this.m_tallying != null)
			{
				this.m_tallying.SetActive(false);
			}
		}
		else if (this.m_tallying != null)
		{
			this.m_tallying.SetActive(false);
		}
		this.SetupRankingReset(this.m_currentResetTimeSpan);
	}

	// Token: 0x06002606 RID: 9734 RVA: 0x000E7874 File Offset: 0x000E5A74
	private void SetTogglBtn()
	{
		this.m_toggleLock = true;
		if (this.m_partsBtnToggls != null && this.m_partsBtnToggls.Count > 0)
		{
			UIToggle uitoggle = null;
			switch (this.m_currentRankerType)
			{
			case RankingUtil.RankingRankerType.FRIEND:
				foreach (UIToggle uitoggle2 in this.m_partsBtnToggls)
				{
					if (uitoggle2 != null && (uitoggle2.gameObject.name.IndexOf("friend") != -1 || uitoggle2.gameObject.name.IndexOf("Friend") != -1))
					{
						uitoggle = uitoggle2;
						break;
					}
				}
				break;
			case RankingUtil.RankingRankerType.RIVAL:
				foreach (UIToggle uitoggle3 in this.m_partsBtnToggls)
				{
					if (uitoggle3 != null && (uitoggle3.gameObject.name.IndexOf("rival") != -1 || uitoggle3.gameObject.name.IndexOf("Rival") != -1))
					{
						uitoggle = uitoggle3;
						break;
					}
				}
				break;
			}
			if (uitoggle != null)
			{
				uitoggle.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
			}
		}
		if (this.m_currentRankerType == RankingUtil.RankingRankerType.RIVAL)
		{
			if (this.m_partsTabRivalTogglH != null && this.m_partsTabRivalTogglT != null)
			{
				if (this.m_currentScoreType == RankingUtil.RankingScoreType.HIGH_SCORE)
				{
					this.m_partsTabRivalTogglH.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
				}
				else
				{
					this.m_partsTabRivalTogglT.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		else if ((this.m_currentRankerType == RankingUtil.RankingRankerType.FRIEND && this.IsActiveSnsLoginGameObject()) || this.m_currentRankerType != RankingUtil.RankingRankerType.FRIEND)
		{
			if (this.m_partsTabNormalTogglH != null && this.m_partsTabNormalTogglT != null)
			{
				if (this.m_currentScoreType == RankingUtil.RankingScoreType.HIGH_SCORE)
				{
					this.m_partsTabNormalTogglH.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
				}
				else
				{
					this.m_partsTabNormalTogglT.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		else if (this.m_partsTabFriendTogglH != null && this.m_partsTabFriendTogglT != null)
		{
			if (this.m_currentScoreType == RankingUtil.RankingScoreType.HIGH_SCORE)
			{
				this.m_partsTabFriendTogglH.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				this.m_partsTabFriendTogglT.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
			}
		}
		this.m_toggleLock = false;
	}

	// Token: 0x06002607 RID: 9735 RVA: 0x000E7B58 File Offset: 0x000E5D58
	private void SetRankerList(List<RankingUtil.Ranker> rankers, RankingUtil.RankingRankerType type, int page)
	{
		if (page > 0 || type == RankingUtil.RankingRankerType.RIVAL)
		{
			this.m_currentRankerList = rankers;
		}
		if (type != RankingUtil.RankingRankerType.FRIEND && this.m_facebook != null)
		{
			this.m_facebook.SetActive(false);
		}
		if (page > 0 || type == RankingUtil.RankingRankerType.RIVAL)
		{
			if (this.m_pattern0 != null)
			{
				this.m_pattern0.SetActive(false);
			}
			if (this.m_pattern1 != null)
			{
				this.m_pattern1.SetActive(type != RankingUtil.RankingRankerType.RIVAL);
			}
			if (this.m_pattern2 != null)
			{
				this.m_pattern2.SetActive(type == RankingUtil.RankingRankerType.RIVAL);
			}
			if (type == RankingUtil.RankingRankerType.RIVAL)
			{
				if (this.m_pattern2ListArea != null)
				{
					if (page < 1)
					{
						this.m_pattern2ListArea.Reset();
						this.AddRectItemStorageRanking(this.m_pattern2ListArea, rankers, type);
					}
					else
					{
						if (page == 1)
						{
							this.m_pattern2ListArea.Reset();
						}
						this.AddRectItemStorageRanking(this.m_pattern2ListArea, rankers, type);
					}
				}
			}
			else if (this.m_pattern1ListArea != null)
			{
				if (page == 1)
				{
					this.m_pattern1ListArea.Reset();
				}
				this.AddRectItemStorageRanking(this.m_pattern1ListArea, rankers, type);
			}
			if (this.m_pattern0MyDataArea != null)
			{
				this.m_pattern0MyDataArea.maxItemCount = (this.m_pattern0MyDataArea.maxRows = 0);
				this.m_pattern0MyDataArea.Restart();
			}
			if (this.m_pattern0TopRankerArea != null)
			{
				this.m_pattern0TopRankerArea.maxItemCount = (this.m_pattern0TopRankerArea.maxRows = 0);
				this.m_pattern0TopRankerArea.Restart();
			}
		}
		else
		{
			if (this.m_pattern0 != null)
			{
				this.m_pattern0.SetActive(true);
			}
			if (this.m_pattern1 != null)
			{
				this.m_pattern1.SetActive(false);
			}
			if (this.m_pattern2 != null)
			{
				this.m_pattern2.SetActive(false);
			}
			if (this.m_pattern1ListArea != null)
			{
				this.m_pattern1ListArea.Reset();
			}
			if (this.m_pattern2ListArea != null)
			{
				this.m_pattern2ListArea.Reset();
			}
			if (this.m_pattern0MyDataArea != null)
			{
				if (rankers != null && rankers.Count > 0)
				{
					if (rankers[0] != null)
					{
						this.m_pattern0MyDataArea.maxItemCount = (this.m_pattern0MyDataArea.maxRows = 1);
						this.UpdateRectItemStorage(this.m_pattern0MyDataArea, rankers, 0);
					}
					else
					{
						this.m_pattern0MyDataArea.maxItemCount = (this.m_pattern0MyDataArea.maxRows = 0);
						this.m_pattern0MyDataArea.Restart();
					}
				}
				else
				{
					this.m_pattern0MyDataArea.maxItemCount = (this.m_pattern0MyDataArea.maxRows = 0);
					this.m_pattern0MyDataArea.Restart();
				}
			}
			if (this.m_pattern0TopRankerArea != null && rankers != null)
			{
				if (rankers.Count - 1 >= RankingManager.GetRankingMax(type, page))
				{
					this.m_pattern0TopRankerArea.maxItemCount = (this.m_pattern0TopRankerArea.maxRows = 3);
				}
				else
				{
					this.m_pattern0TopRankerArea.maxItemCount = (this.m_pattern0TopRankerArea.maxRows = rankers.Count - 1);
				}
				this.UpdateRectItemStorage(this.m_pattern0TopRankerArea, rankers, 1);
			}
		}
	}

	// Token: 0x06002608 RID: 9736 RVA: 0x000E7EC4 File Offset: 0x000E60C4
	private void AddRectItemStorageRanking(UIRectItemStorageRanking ui_rankers, List<RankingUtil.Ranker> rankerList, RankingUtil.RankingRankerType type)
	{
		int childCount = ui_rankers.childCount;
		int num = rankerList.Count - childCount;
		if (this.m_pageNext)
		{
			num--;
		}
		if (ui_rankers.callback == null)
		{
			ui_rankers.callback = new UIRectItemStorageRanking.CallbackCreated(this.CallbackItemStorageRanking);
			ui_rankers.callbackTopOrLast = new UIRectItemStorageRanking.CallbackTopOrLast(this.CallbackItemStorageRankingTopOrLast);
		}
		GameObject mainMenuUIObject = HudMenuUtility.GetMainMenuUIObject();
		if (mainMenuUIObject != null && mainMenuUIObject.activeSelf)
		{
			ui_rankers.AddItem(num, 0.02f);
		}
		else
		{
			ui_rankers.AddItem(num, 0f);
		}
	}

	// Token: 0x06002609 RID: 9737 RVA: 0x000E7F5C File Offset: 0x000E615C
	private bool CallbackItemStorageRankingTopOrLast(bool isTop)
	{
		bool result = false;
		RankingManager instance = SingletonGameObject<RankingManager>.Instance;
		if (instance != null && !instance.isLoading)
		{
			if (isTop)
			{
				if (this.m_pagePrev)
				{
					result = false;
				}
			}
			else if (this.m_pageNext)
			{
				result = instance.GetRankingScroll(RankingUtil.currentRankingMode, true, new RankingManager.CallbackRankingData(this.CallbackRanking));
			}
		}
		return result;
	}

	// Token: 0x0600260A RID: 9738 RVA: 0x000E7FC8 File Offset: 0x000E61C8
	private void CallbackItemStorageRanking(ui_ranking_scroll_dummy obj, UIRectItemStorageRanking storage)
	{
		if (obj != null && this.m_currentRankerList != null)
		{
			int num = obj.slot + 1;
			if (num > 0 && this.m_currentRankerList.Count > num)
			{
				RankingUtil.Ranker rankerData = this.m_currentRankerList[num];
				if (obj.myRankerData == null)
				{
					obj.myRankerData = this.m_currentRankerList[0];
				}
				obj.spWindow = null;
				obj.rankingUI = this;
				obj.rankerData = rankerData;
				obj.rankerType = this.m_currentRankerType;
				obj.scoreType = this.m_currentScoreType;
				obj.SetActiveObject(storage.CheckItemDraw(obj.slot), 0f);
				obj.end = (obj.slot + 1 == this.m_currentRankerList.Count);
			}
			else
			{
				UnityEngine.Object.Destroy(obj.gameObject);
			}
		}
	}

	// Token: 0x0600260B RID: 9739 RVA: 0x000E80A8 File Offset: 0x000E62A8
	private void UpdateRectItemStorage(UIRectItemStorage ui_rankers, List<RankingUtil.Ranker> rankerList, int head = 1)
	{
		ui_rankers.Restart();
		ui_ranking_scroll[] componentsInChildren = ui_rankers.GetComponentsInChildren<ui_ranking_scroll>(true);
		for (int i = 0; i < ui_rankers.maxItemCount; i++)
		{
			if (i + head >= rankerList.Count)
			{
				break;
			}
			RankingUtil.Ranker ranker = rankerList[i + head];
			if (ranker != null)
			{
				componentsInChildren[i].UpdateView(this.m_currentScoreType, this.m_currentRankerType, ranker, i == ui_rankers.maxItemCount - 1);
				bool myRanker = false;
				if (rankerList[0] != null && ranker.id == rankerList[0].id)
				{
					myRanker = true;
				}
				componentsInChildren[i].SetMyRanker(myRanker);
			}
		}
	}

	// Token: 0x0600260C RID: 9740 RVA: 0x000E8158 File Offset: 0x000E6358
	private static string[] GetFriendIdList(RankingUtil.RankingRankerType rankerType)
	{
		string[] result = null;
		if (rankerType == RankingUtil.RankingRankerType.FRIEND)
		{
			result = RankingUtil.GetFriendIdList();
		}
		return result;
	}

	// Token: 0x0600260D RID: 9741 RVA: 0x000E8174 File Offset: 0x000E6374
	private void Update()
	{
		if (this.m_isInitilalized)
		{
			if (this.m_easySnsFeed != null)
			{
				EasySnsFeed.Result result = this.m_easySnsFeed.Update();
				if (result != EasySnsFeed.Result.COMPLETED)
				{
					if (result == EasySnsFeed.Result.FAILED)
					{
						this.m_snsLogin = false;
						this.m_snsCompGetRankingTime = 0f;
						this.m_snsCompGetRanking = false;
						this.m_easySnsFeed = null;
					}
				}
				else
				{
					this.m_snsCompGetRanking = true;
					this.m_snsCompGetRankingTime = 0f;
					this.m_currentRankerList = null;
					this.ResetRankerList(0, this.m_currentRankerType);
					global::Debug.Log("SetRanking m_easySnsFeed");
					this.SetRanking(RankingUtil.RankingRankerType.FRIEND, this.m_lastSelectedScoreType, -1);
					this.m_easySnsFeed = null;
				}
			}
			else if (this.m_snsCompGetRanking)
			{
				this.m_snsCompGetRankingTime += Time.deltaTime;
				if (this.m_snsCompGetRankingTime > 5f)
				{
					global::Debug.Log("SetRanking m_easySnsFeed reload !");
					this.SetRanking(RankingUtil.RankingRankerType.FRIEND, this.m_lastSelectedScoreType, -1);
					this.m_snsCompGetRankingTime = -5f;
				}
			}
			else
			{
				this.m_snsCompGetRankingTime = 0f;
			}
		}
		if (this.m_isInitilalized && !this.m_isDrawInit)
		{
			global::Debug.Log("m_isInitilalized");
			RankingManager instance = SingletonGameObject<RankingManager>.Instance;
			if (instance != null && !instance.isLoading && instance.IsRankingTop(RankingUtil.currentRankingMode, RankingManager.EndlessRivalRankingScoreType, RankingUtil.RankingRankerType.RIVAL))
			{
				this.SetRanking(this.m_currentRankerType, this.m_currentScoreType, -1);
			}
			this.m_isDrawInit = true;
		}
		if (this.m_resetTimeSpanSec <= 0f)
		{
			this.m_currentResetTimeSpan = SingletonGameObject<RankingManager>.Instance.GetRankigResetTimeSpan(RankingUtil.currentRankingMode, this.m_currentScoreType, this.m_currentRankerType);
			this.m_resetTimeSpanSec = (float)this.m_currentResetTimeSpan.Seconds + 0.1f;
			if (this.m_currentResetTimeSpan.Ticks > 0L)
			{
				if (this.m_currentResetTimeSpan.Days < 1 && this.m_currentResetTimeSpan.Hours < 1 && this.m_currentResetTimeSpan.Minutes < 1)
				{
					this.m_resetTimeSpanSec = (float)this.m_currentResetTimeSpan.Milliseconds / 1000f + 0.005f;
				}
			}
			else
			{
				this.m_resetTimeSpanSec = 300f;
			}
			this.SetupRankingReset(this.m_currentResetTimeSpan);
		}
		else
		{
			this.m_resetTimeSpanSec -= Time.deltaTime;
		}
		if (this.m_btnDelay > 0)
		{
			this.m_btnDelay--;
		}
		if (this.m_rankingChange != RankingUtil.RankChange.NONE && this.m_rankingInitDraw)
		{
			if (this.IsRankingActive())
			{
				this.m_rankingChangeTime += Time.deltaTime;
			}
			else
			{
				this.m_rankingChangeTime = 0f;
			}
			if (this.m_rankingChangeTime > 0.25f)
			{
				RankingUtil.ShowRankingChangeWindow(RankingUtil.currentRankingMode);
				this.m_rankingChange = RankingUtil.RankChange.NONE;
				if (this.m_loading != null)
				{
					this.m_loading.SetActive(false);
				}
			}
		}
		if (this.m_isInitilalized && !this.m_facebookLockInit && !this.m_facebookLock && this.IsRankingActive())
		{
			if (RegionManager.Instance != null)
			{
				this.m_facebookLock = !RegionManager.Instance.IsUseSNS();
			}
			foreach (UIImageButton uiimageButton in this.m_partsBtns)
			{
				if (uiimageButton.name.IndexOf("friend") != -1)
				{
					uiimageButton.isEnabled = !this.m_facebookLock;
					break;
				}
			}
			this.m_facebookLockInit = true;
		}
		if (!this.m_rankingInitDraw)
		{
			GameObject x = GameObject.Find("UI Root (2D)");
			if (x != null)
			{
				this.m_rankingInitDraw = true;
			}
		}
		else if (this.m_loading != null && this.m_loading.activeSelf)
		{
			if (this.IsRankingActive())
			{
				this.m_rankingInitloadingTime += Time.deltaTime;
			}
			else
			{
				this.m_rankingInitloadingTime = 0f;
			}
			if (this.m_first && !this.m_isInitilalized && this.m_rankingInitloadingTime > 0.5f)
			{
				this.InitSetting();
				this.m_rankingInitloadingTime = -10f;
			}
			else if (this.m_rankingInitloadingTime > 5f)
			{
				if (SingletonGameObject<RankingManager>.Instance != null)
				{
					RankingManager instance2 = SingletonGameObject<RankingManager>.Instance;
					if (!instance2.isLoading)
					{
						if (this.m_currentRankerType == RankingUtil.RankingRankerType.RIVAL && !instance2.IsRankingTop(RankingUtil.currentRankingMode, RankingUtil.RankingScoreType.HIGH_SCORE, RankingUtil.RankingRankerType.RIVAL))
						{
							SingletonGameObject<RankingManager>.Instance.InitNormal(RankingUtil.currentRankingMode, null);
							this.m_rankingInitloadingTime = -30f;
						}
						else
						{
							this.m_rankingInitloadingTime = -60f;
						}
					}
					else
					{
						this.m_rankingInitloadingTime = -40f;
					}
				}
				else
				{
					this.m_rankingInitloadingTime = -60f;
				}
			}
		}
		if (this.m_callbackTemporarilySaved != null && base.gameObject.activeSelf)
		{
			if (this.m_callbackTemporarilySavedDelay <= 0f)
			{
				this.m_callbackTemporarilySaved.SendCallback();
				this.m_callbackTemporarilySaved = null;
			}
			else if (this.IsRankingActive() && base.gameObject.activeSelf)
			{
				if (Time.deltaTime <= 0f)
				{
					this.m_callbackTemporarilySavedDelay -= 1f / (float)Application.targetFrameRate;
				}
				else
				{
					this.m_callbackTemporarilySavedDelay -= Time.deltaTime;
				}
			}
			else
			{
				this.m_callbackTemporarilySavedDelay = 0.25f;
			}
		}
	}

	// Token: 0x0600260E RID: 9742 RVA: 0x000E871C File Offset: 0x000E691C
	private void OnClickNextButton()
	{
	}

	// Token: 0x0600260F RID: 9743 RVA: 0x000E8720 File Offset: 0x000E6920
	public void OnHighScoreToggleChange()
	{
		if (!this.m_toggleLock)
		{
			this.scoreToggleChange(RankingUtil.RankingScoreType.HIGH_SCORE);
		}
	}

	// Token: 0x06002610 RID: 9744 RVA: 0x000E8734 File Offset: 0x000E6934
	public void OnWeeklyToggleChange()
	{
		if (!this.m_toggleLock)
		{
			this.scoreToggleChange(RankingUtil.RankingScoreType.TOTAL_SCORE);
		}
	}

	// Token: 0x06002611 RID: 9745 RVA: 0x000E8748 File Offset: 0x000E6948
	private void OnClickHelpButton()
	{
		if (this.m_help != null)
		{
			this.m_help.Open(!this.m_isHelp);
			this.m_isHelp = true;
		}
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x06002612 RID: 9746 RVA: 0x000E8794 File Offset: 0x000E6994
	private void OnClickScoreType()
	{
		SoundManager.SePlay("sys_page_skip", "SE");
	}

	// Token: 0x06002613 RID: 9747 RVA: 0x000E87A8 File Offset: 0x000E69A8
	public void scoreToggleChange(RankingUtil.RankingScoreType scoreType)
	{
		if (this.m_isInitilalized && scoreType != this.m_currentScoreType)
		{
			SoundManager.SePlay("sys_page_skip", "SE");
			global::Debug.Log("SetRanking scoreToggleChange");
			this.SetRanking(this.m_currentRankerType, scoreType, 0);
			if (this.m_currentRankerType != RankingUtil.RankingRankerType.RIVAL)
			{
				this.m_lastSelectedScoreType = scoreType;
			}
		}
	}

	// Token: 0x06002614 RID: 9748 RVA: 0x000E8808 File Offset: 0x000E6A08
	public void OnFriendToggleChange()
	{
		if (!this.m_toggleLock && this.m_currentRankerType != RankingUtil.RankingRankerType.FRIEND)
		{
			this.RankerToggleChange(RankingUtil.RankingRankerType.FRIEND);
		}
	}

	// Token: 0x06002615 RID: 9749 RVA: 0x000E8828 File Offset: 0x000E6A28
	public void OnAllToggleChange()
	{
		if (!this.m_toggleLock && this.m_currentRankerType != RankingUtil.RankingRankerType.ALL)
		{
			this.RankerToggleChange(RankingUtil.RankingRankerType.ALL);
		}
	}

	// Token: 0x06002616 RID: 9750 RVA: 0x000E8848 File Offset: 0x000E6A48
	public void OnRivalToggleChange()
	{
		if (!this.m_toggleLock && this.m_currentRankerType != RankingUtil.RankingRankerType.RIVAL)
		{
			this.RankerToggleChange(RankingUtil.RankingRankerType.RIVAL);
		}
	}

	// Token: 0x06002617 RID: 9751 RVA: 0x000E8868 File Offset: 0x000E6A68
	public void OnHistoryToggleChange()
	{
		if (!this.m_toggleLock && this.m_currentRankerType != RankingUtil.RankingRankerType.HISTORY)
		{
			this.RankerToggleChange(RankingUtil.RankingRankerType.HISTORY);
		}
	}

	// Token: 0x06002618 RID: 9752 RVA: 0x000E8888 File Offset: 0x000E6A88
	private void RankerToggleChange(RankingUtil.RankingRankerType rankerType)
	{
		if (this.m_isInitilalized)
		{
			SoundManager.SePlay("sys_page_skip", "SE");
			if (rankerType == RankingUtil.RankingRankerType.RIVAL)
			{
				this.m_currentScoreType = ((!this.m_partsTabRivalTogglH.value) ? RankingUtil.RankingScoreType.TOTAL_SCORE : RankingUtil.RankingScoreType.HIGH_SCORE);
			}
			else
			{
				this.m_currentScoreType = this.m_lastSelectedScoreType;
			}
			if (!this.SetRanking(rankerType, this.m_currentScoreType, 0))
			{
				if (this.m_loading != null)
				{
					this.m_loading.SetActive(true);
				}
				if (this.m_tallying != null)
				{
					this.m_tallying.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06002619 RID: 9753 RVA: 0x000E8934 File Offset: 0x000E6B34
	private void OnClickMoreButton()
	{
		if (this.m_isInitilalized)
		{
			SoundManager.SePlay("sys_page_skip", "SE");
			this.SetRanking(this.m_currentRankerType, this.m_currentScoreType, 1);
		}
	}

	// Token: 0x0600261A RID: 9754 RVA: 0x000E8968 File Offset: 0x000E6B68
	private void OnClickSnsLogin()
	{
		this.m_snsLogin = true;
		this.m_snsCompGetRankingTime = 0f;
		this.m_snsCompGetRanking = false;
		this.m_easySnsFeed = new EasySnsFeed(base.gameObject, "Camera/menu_Anim/ui_mm_ranking_page/Anchor_5_MC");
	}

	// Token: 0x0600261B RID: 9755 RVA: 0x000E899C File Offset: 0x000E6B9C
	private void OnClickFriendOption()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		GameObject loadMenuChildObject = HudMenuUtility.GetLoadMenuChildObject("RankingFriendOptionWindow", true);
		if (loadMenuChildObject != null)
		{
			RankingFriendOptionWindow component = loadMenuChildObject.GetComponent<RankingFriendOptionWindow>();
			if (component != null)
			{
				component.StartCoroutine("SetUp");
			}
		}
	}

	// Token: 0x0600261C RID: 9756 RVA: 0x000E89F0 File Offset: 0x000E6BF0
	private void OnClickFriendOptionOk()
	{
		this.SetRanking(RankingUtil.RankingRankerType.FRIEND, this.m_currentScoreType, -1);
		if (SingletonGameObject<RankingManager>.Instance != null && EventManager.Instance != null && EventManager.Instance.Type == EventManager.EventType.SPECIAL_STAGE)
		{
			SingletonGameObject<RankingManager>.Instance.Reset(RankingUtil.RankingMode.ENDLESS, RankingUtil.RankingRankerType.SP_FRIEND);
		}
	}

	// Token: 0x0600261D RID: 9757 RVA: 0x000E8A48 File Offset: 0x000E6C48
	private void SetupRankingReset(TimeSpan span)
	{
		if (this.m_partsInfo != null)
		{
			RankingManager instance = SingletonGameObject<RankingManager>.Instance;
			if (instance != null)
			{
				if (this.m_currentRankerType == RankingUtil.RankingRankerType.FRIEND && RankingUI.s_socialInterface != null && !RankingUI.s_socialInterface.IsLoggedIn)
				{
					this.m_partsInfo.text = string.Empty;
				}
				else
				{
					this.m_partsInfo.text = RankingUtil.GetResetTime(span, true);
				}
			}
		}
	}

	// Token: 0x0600261E RID: 9758 RVA: 0x000E8ACC File Offset: 0x000E6CCC
	private void SetupLeague()
	{
		RankingUtil.SetLeagueObject(RankingUtil.currentRankingMode, ref this.m_partsRankIcon0, ref this.m_partsRankIcon1, ref this.m_partsRankText, ref this.m_partsRankTextEx);
	}

	// Token: 0x0600261F RID: 9759 RVA: 0x000E8AFC File Offset: 0x000E6CFC
	private bool IsActiveSnsLoginGameObject()
	{
		return this.m_currentRankerType == RankingUtil.RankingRankerType.FRIEND && RankingUI.s_socialInterface != null && !RankingUI.s_socialInterface.IsLoggedIn;
	}

	// Token: 0x06002620 RID: 9760 RVA: 0x000E8B2C File Offset: 0x000E6D2C
	private void OnSettingPartsSnsAdditional()
	{
	}

	// Token: 0x06002621 RID: 9761 RVA: 0x000E8B30 File Offset: 0x000E6D30
	[Conditional("DEBUG_INFO")]
	private static void DebugLog(string s)
	{
		global::Debug.Log("@ms DEBUG_INFO " + s);
	}

	// Token: 0x06002622 RID: 9762 RVA: 0x000E8B44 File Offset: 0x000E6D44
	[Conditional("DEBUG_INFO2")]
	private static void DebugLog2(string s)
	{
		global::Debug.Log("@ms DEBUG_INFO2" + s);
	}

	// Token: 0x06002623 RID: 9763 RVA: 0x000E8B58 File Offset: 0x000E6D58
	[Conditional("DEBUG_INFO3")]
	private static void DebugLog3(string s)
	{
		global::Debug.Log("@ms DEBUG_INFO3" + s);
	}

	// Token: 0x06002624 RID: 9764 RVA: 0x000E8B6C File Offset: 0x000E6D6C
	[Conditional("DEBUG_INFO")]
	private static void DebugLogWarning(string s)
	{
		global::Debug.LogWarning("@ms " + s);
	}

	// Token: 0x06002625 RID: 9765 RVA: 0x000E8B80 File Offset: 0x000E6D80
	public static RankingUI Setup()
	{
		if (RankingUI.s_instance != null)
		{
			RankingUI.s_instance.Init();
			return RankingUI.s_instance;
		}
		return null;
	}

	// Token: 0x06002626 RID: 9766 RVA: 0x000E8BA4 File Offset: 0x000E6DA4
	public static void CheckSnsUse()
	{
		if (RankingUI.s_instance != null)
		{
			RankingUI.s_instance.CheckSns();
		}
	}

	// Token: 0x06002627 RID: 9767 RVA: 0x000E8BC0 File Offset: 0x000E6DC0
	public void CheckSns()
	{
		if (RegionManager.Instance != null)
		{
			this.m_facebookLock = !RegionManager.Instance.IsUseSNS();
		}
		foreach (UIImageButton uiimageButton in this.m_partsBtns)
		{
			if (uiimageButton.name.IndexOf("friend") != -1)
			{
				uiimageButton.isEnabled = !this.m_facebookLock;
				break;
			}
		}
		this.m_facebookLockInit = true;
	}

	// Token: 0x06002628 RID: 9768 RVA: 0x000E8C40 File Offset: 0x000E6E40
	public void UpdateSendChallengeOrg(RankingUtil.RankingRankerType type, string id)
	{
		global::Debug.Log("RankingUI:UpdateSendChallengeOrg type:" + type);
		if (this.m_currentRankerType == type)
		{
			if (type == RankingUtil.RankingRankerType.RIVAL)
			{
				if (this.m_pattern2 != null && this.m_pattern2.activeSelf && this.m_pattern2ListArea != null)
				{
					ui_ranking_scroll[] componentsInChildren = this.m_pattern2ListArea.GetComponentsInChildren<ui_ranking_scroll>();
					if (componentsInChildren != null && componentsInChildren.Length > 0)
					{
						foreach (ui_ranking_scroll ui_ranking_scroll in componentsInChildren)
						{
							ui_ranking_scroll.UpdateSendChallenge(id);
						}
					}
				}
			}
			else if (this.m_pattern0 != null && this.m_pattern0.activeSelf && this.m_pattern0TopRankerArea != null)
			{
				ui_ranking_scroll[] componentsInChildren2 = this.m_pattern0TopRankerArea.GetComponentsInChildren<ui_ranking_scroll>();
				if (componentsInChildren2 != null && componentsInChildren2.Length > 0)
				{
					foreach (ui_ranking_scroll ui_ranking_scroll2 in componentsInChildren2)
					{
						ui_ranking_scroll2.UpdateSendChallenge(id);
					}
				}
			}
			else if (this.m_pattern1 != null && this.m_pattern1.activeSelf && this.m_pattern1ListArea != null)
			{
				ui_ranking_scroll[] componentsInChildren3 = this.m_pattern1ListArea.GetComponentsInChildren<ui_ranking_scroll>();
				if (componentsInChildren3 != null && componentsInChildren3.Length > 0)
				{
					foreach (ui_ranking_scroll ui_ranking_scroll3 in componentsInChildren3)
					{
						ui_ranking_scroll3.UpdateSendChallenge(id);
					}
				}
			}
		}
	}

	// Token: 0x06002629 RID: 9769 RVA: 0x000E8DE8 File Offset: 0x000E6FE8
	private bool IsRankingActive()
	{
		return this.m_displayFlag;
	}

	// Token: 0x0600262A RID: 9770 RVA: 0x000E8DF8 File Offset: 0x000E6FF8
	public static void UpdateSendChallenge(RankingUtil.RankingRankerType type, string id)
	{
		if (RankingUI.s_instance != null)
		{
			RankingUI.s_instance.UpdateSendChallengeOrg(type, id);
		}
	}

	// Token: 0x0600262B RID: 9771 RVA: 0x000E8E18 File Offset: 0x000E7018
	public static void SetLoading()
	{
		if (RankingUI.s_instance != null)
		{
			RankingUI.s_instance.SetLoadingObject();
		}
	}

	// Token: 0x170004FD RID: 1277
	// (get) Token: 0x0600262C RID: 9772 RVA: 0x000E8E34 File Offset: 0x000E7034
	private static RankingUI Instance
	{
		get
		{
			return RankingUI.s_instance;
		}
	}

	// Token: 0x0600262D RID: 9773 RVA: 0x000E8E3C File Offset: 0x000E703C
	private void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x0600262E RID: 9774 RVA: 0x000E8E44 File Offset: 0x000E7044
	private void OnDestroy()
	{
		if (RankingUI.s_instance == this)
		{
			RankingUI.s_instance = null;
		}
	}

	// Token: 0x0600262F RID: 9775 RVA: 0x000E8E5C File Offset: 0x000E705C
	private void SetInstance()
	{
		if (RankingUI.s_instance == null)
		{
			RankingUI.s_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002246 RID: 8774
	private const float RANKING_INIT_LOAD_DELAY = 0.25f;

	// Token: 0x04002247 RID: 8775
	private const RankingUtil.RankingScoreType DEFAULT_SCORE_TYPE = RankingUtil.RankingScoreType.HIGH_SCORE;

	// Token: 0x04002248 RID: 8776
	private const RankingUtil.RankingRankerType DEFAULT_RANKER_TYPE = RankingUtil.RankingRankerType.RIVAL;

	// Token: 0x04002249 RID: 8777
	public const int BTN_NOT_COUNT = 5;

	// Token: 0x0400224A RID: 8778
	[SerializeField]
	[Header("モードごとのカラー設定")]
	private Color m_quickModeColor1;

	// Token: 0x0400224B RID: 8779
	[SerializeField]
	private Color m_quickModeColor2;

	// Token: 0x0400224C RID: 8780
	[SerializeField]
	private Color m_endlessModeColor1;

	// Token: 0x0400224D RID: 8781
	[SerializeField]
	private Color m_endlessModeColor2;

	// Token: 0x0400224E RID: 8782
	[SerializeField]
	private List<UISprite> m_colorObjects1;

	// Token: 0x0400224F RID: 8783
	[SerializeField]
	private List<UISprite> m_colorObjects2;

	// Token: 0x04002250 RID: 8784
	[Header("読み込み中表示")]
	[SerializeField]
	private GameObject m_loading;

	// Token: 0x04002251 RID: 8785
	[Header("SNSログインページ")]
	[SerializeField]
	private GameObject m_facebook;

	// Token: 0x04002252 RID: 8786
	[Header("ランキング初期ページ(自分と上位3人)")]
	[SerializeField]
	private GameObject m_pattern0;

	// Token: 0x04002253 RID: 8787
	[SerializeField]
	private UIRectItemStorage m_pattern0MyDataArea;

	// Token: 0x04002254 RID: 8788
	[SerializeField]
	private UIRectItemStorage m_pattern0TopRankerArea;

	// Token: 0x04002255 RID: 8789
	[SerializeField]
	private GameObject m_pattern0More;

	// Token: 0x04002256 RID: 8790
	[SerializeField]
	[Header("ランキング一覧")]
	private GameObject m_pattern1;

	// Token: 0x04002257 RID: 8791
	[SerializeField]
	private UIRectItemStorageRanking m_pattern1ListArea;

	// Token: 0x04002258 RID: 8792
	[SerializeField]
	private UIDraggablePanel m_pattern1MainListPanel;

	// Token: 0x04002259 RID: 8793
	[Header("ランキング一覧(リーグ)")]
	[SerializeField]
	private GameObject m_pattern2;

	// Token: 0x0400225A RID: 8794
	[SerializeField]
	private UIRectItemStorageRanking m_pattern2ListArea;

	// Token: 0x0400225B RID: 8795
	[SerializeField]
	private UIDraggablePanel m_pattern2MainListPanel;

	// Token: 0x0400225C RID: 8796
	[SerializeField]
	[Header("ボタン類などのオブジェクト")]
	private GameObject m_parts;

	// Token: 0x0400225D RID: 8797
	[SerializeField]
	private GameObject m_partsTabNormal;

	// Token: 0x0400225E RID: 8798
	[SerializeField]
	private GameObject m_partsTabRival;

	// Token: 0x0400225F RID: 8799
	[SerializeField]
	private GameObject m_partsTabFriend;

	// Token: 0x04002260 RID: 8800
	[SerializeField]
	private UILabel m_partsInfo;

	// Token: 0x04002261 RID: 8801
	[SerializeField]
	private UIImageButton[] m_partsBtns;

	// Token: 0x04002262 RID: 8802
	[SerializeField]
	private UISprite m_partsRankIcon0;

	// Token: 0x04002263 RID: 8803
	[SerializeField]
	private UISprite m_partsRankIcon1;

	// Token: 0x04002264 RID: 8804
	[SerializeField]
	private UILabel m_partsRankText;

	// Token: 0x04002265 RID: 8805
	[SerializeField]
	private UILabel m_partsRankTextEx;

	// Token: 0x04002266 RID: 8806
	[SerializeField]
	private GameObject m_tallying;

	// Token: 0x04002267 RID: 8807
	[SerializeField]
	private UIToggle m_partsTabNormalTogglH;

	// Token: 0x04002268 RID: 8808
	[SerializeField]
	private UIToggle m_partsTabNormalTogglT;

	// Token: 0x04002269 RID: 8809
	[SerializeField]
	private UIToggle m_partsTabRivalTogglH;

	// Token: 0x0400226A RID: 8810
	[SerializeField]
	private UIToggle m_partsTabRivalTogglT;

	// Token: 0x0400226B RID: 8811
	[SerializeField]
	private UIToggle m_partsTabFriendTogglH;

	// Token: 0x0400226C RID: 8812
	[SerializeField]
	private UIToggle m_partsTabFriendTogglT;

	// Token: 0x0400226D RID: 8813
	[SerializeField]
	private List<UIToggle> m_partsBtnToggls;

	// Token: 0x0400226E RID: 8814
	[SerializeField]
	[Header("ヘルプウインド")]
	private ranking_help m_help;

	// Token: 0x0400226F RID: 8815
	public static SocialInterface s_socialInterface;

	// Token: 0x04002270 RID: 8816
	private bool m_isInitilalized;

	// Token: 0x04002271 RID: 8817
	private bool m_isHelp;

	// Token: 0x04002272 RID: 8818
	private bool m_isDrawInit;

	// Token: 0x04002273 RID: 8819
	private bool m_rankingInitDraw;

	// Token: 0x04002274 RID: 8820
	private RankingUtil.RankChange m_rankingChange;

	// Token: 0x04002275 RID: 8821
	private float m_rankingInitloadingTime;

	// Token: 0x04002276 RID: 8822
	private float m_rankingChangeTime;

	// Token: 0x04002277 RID: 8823
	private RankingUtil.RankingScoreType m_currentScoreType;

	// Token: 0x04002278 RID: 8824
	private RankingUtil.RankingRankerType m_currentRankerType;

	// Token: 0x04002279 RID: 8825
	private List<RankingUtil.Ranker> m_currentRankerList;

	// Token: 0x0400227A RID: 8826
	private int m_page;

	// Token: 0x0400227B RID: 8827
	private bool m_pageNext;

	// Token: 0x0400227C RID: 8828
	private bool m_pagePrev;

	// Token: 0x0400227D RID: 8829
	private bool m_toggleLock;

	// Token: 0x0400227E RID: 8830
	private bool m_facebookLock;

	// Token: 0x0400227F RID: 8831
	private bool m_facebookLockInit;

	// Token: 0x04002280 RID: 8832
	private bool m_snsCompGetRanking;

	// Token: 0x04002281 RID: 8833
	private bool m_snsLogin;

	// Token: 0x04002282 RID: 8834
	private bool m_first;

	// Token: 0x04002283 RID: 8835
	private float m_snsCompGetRankingTime;

	// Token: 0x04002284 RID: 8836
	private RankingCallbackTemporarilySaved m_callbackTemporarilySaved;

	// Token: 0x04002285 RID: 8837
	private float m_callbackTemporarilySavedDelay;

	// Token: 0x04002286 RID: 8838
	private TimeSpan m_currentResetTimeSpan;

	// Token: 0x04002287 RID: 8839
	private float m_resetTimeSpanSec;

	// Token: 0x04002288 RID: 8840
	private int m_btnDelay = 5;

	// Token: 0x04002289 RID: 8841
	private EasySnsFeed m_easySnsFeed;

	// Token: 0x0400228A RID: 8842
	private RankingUtil.RankingMode m_currentMode = RankingUtil.RankingMode.COUNT;

	// Token: 0x0400228B RID: 8843
	private RankingUtil.RankingScoreType m_lastSelectedScoreType;

	// Token: 0x0400228C RID: 8844
	private bool m_displayFlag;

	// Token: 0x0400228D RID: 8845
	private static RankingUI s_instance;
}
