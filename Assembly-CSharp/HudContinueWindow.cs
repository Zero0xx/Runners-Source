using System;
using System.Collections.Generic;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x02000371 RID: 881
public class HudContinueWindow : MonoBehaviour
{
	// Token: 0x170003D7 RID: 983
	// (get) Token: 0x06001A1A RID: 6682 RVA: 0x00098DDC File Offset: 0x00096FDC
	// (set) Token: 0x06001A1B RID: 6683 RVA: 0x00098DFC File Offset: 0x00096FFC
	public bool IsYesButtonPressed
	{
		get
		{
			return this.m_state == HudContinueWindow.State.TOUCHED_BUTTON && this.m_pressedButton == HudContinueWindow.PressedButton.YES;
		}
		private set
		{
		}
	}

	// Token: 0x170003D8 RID: 984
	// (get) Token: 0x06001A1C RID: 6684 RVA: 0x00098E00 File Offset: 0x00097000
	// (set) Token: 0x06001A1D RID: 6685 RVA: 0x00098E20 File Offset: 0x00097020
	public bool IsNoButtonPressed
	{
		get
		{
			return this.m_state == HudContinueWindow.State.TOUCHED_BUTTON && this.m_pressedButton == HudContinueWindow.PressedButton.NO;
		}
		private set
		{
		}
	}

	// Token: 0x170003D9 RID: 985
	// (get) Token: 0x06001A1E RID: 6686 RVA: 0x00098E24 File Offset: 0x00097024
	// (set) Token: 0x06001A1F RID: 6687 RVA: 0x00098E44 File Offset: 0x00097044
	public bool IsVideoButtonPressed
	{
		get
		{
			return this.m_state == HudContinueWindow.State.TOUCHED_BUTTON && this.m_pressedButton == HudContinueWindow.PressedButton.VIDEO;
		}
		private set
		{
		}
	}

	// Token: 0x06001A20 RID: 6688 RVA: 0x00098E48 File Offset: 0x00097048
	public void Setup(bool bossStage)
	{
		this.m_bossStage = bossStage;
		this.m_parentPanel = base.gameObject;
		if (this.m_timeUpObj == null)
		{
			this.m_timeUpObj = GameObjectUtil.FindChildGameObject(base.gameObject, "timesup");
			this.SetTimeUpObj(false);
		}
		bool flag = false;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "HL-AdsEnabled");
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "HL-AdsDisabled");
		if (gameObject2 != null && gameObject != null)
		{
			if (flag)
			{
				this.m_parentPanel = gameObject;
				gameObject.SetActive(true);
				gameObject2.SetActive(false);
			}
			else
			{
				this.m_parentPanel = gameObject2;
				gameObject2.SetActive(true);
				gameObject.SetActive(false);
			}
		}
		UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(this.m_parentPanel, "Btn_yes");
		if (uibuttonMessage != null)
		{
			uibuttonMessage.target = base.gameObject;
			uibuttonMessage.functionName = "OnClickYesButton";
		}
		UIButtonMessage uibuttonMessage2 = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(this.m_parentPanel, "Btn_no");
		if (uibuttonMessage2 != null)
		{
			uibuttonMessage2.target = base.gameObject;
			uibuttonMessage2.functionName = "OnClickNoButton";
		}
		if (flag)
		{
			UIButtonMessage uibuttonMessage3 = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(this.m_parentPanel, "Btn_video");
			if (uibuttonMessage3 != null)
			{
				uibuttonMessage3.target = base.gameObject;
				uibuttonMessage3.functionName = "OnClickVideoButton";
			}
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbt_caption");
		if (uilabel != null)
		{
			string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Continue", "rsring_continue_caption").text;
			uilabel.text = text;
		}
	}

	// Token: 0x06001A21 RID: 6689 RVA: 0x00098FF0 File Offset: 0x000971F0
	public void SetTimeUpObj(bool enablFlag)
	{
		if (this.m_timeUpObj != null)
		{
			this.m_timeUpObj.SetActive(enablFlag);
		}
	}

	// Token: 0x06001A22 RID: 6690 RVA: 0x00099010 File Offset: 0x00097210
	public void SetVideoButton(bool enablFlag)
	{
		this.m_videoEnabled = enablFlag;
		this.Setup(this.m_bossStage);
	}

	// Token: 0x06001A23 RID: 6691 RVA: 0x00099028 File Offset: 0x00097228
	private void UpdateRedStarRingCount()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "textView");
		if (gameObject == null)
		{
			return;
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_body");
		if (uilabel == null)
		{
			return;
		}
		TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Continue", "label_rsring_continue");
		text.ReplaceTag("{RING_NUM}", HudContinueUtility.GetContinueCostString());
		text.ReplaceTag("{OWNED_RSRING}", HudContinueUtility.GetRedStarRingCountString());
		if (string.IsNullOrEmpty(this.m_scoreText))
		{
			this.m_scoreText = this.GetScoreText();
		}
		if (string.IsNullOrEmpty(this.m_dailyBattleText))
		{
			this.m_dailyBattleText = this.GetDailyBattleText();
		}
		uilabel.text = this.m_scoreText + this.m_dailyBattleText + text.text;
	}

	// Token: 0x06001A24 RID: 6692 RVA: 0x000990F4 File Offset: 0x000972F4
	private string GetScoreText()
	{
		if (this.m_bossStage)
		{
			LevelInformation levelInformation = ObjUtil.GetLevelInformation();
			if (!(levelInformation != null))
			{
				return string.Empty;
			}
			int num = levelInformation.NumBossHpMax - levelInformation.NumBossAttack;
			int num2 = GameModeStage.ContinueRestCount();
			if (EventManager.Instance != null && EventManager.Instance.IsRaidBossStage())
			{
				TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Continue", "label_rsring_continue_RAID");
				text.ReplaceTag("{PARAM_LIFE}", num.ToString());
				text.ReplaceTag("{PARAM}", num2.ToString());
				return text.text;
			}
			TextObject text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Continue", "label_rsring_continue_E");
			text2.ReplaceTag("{PARAM_LIFE}", num.ToString());
			text2.ReplaceTag("{PARAM}", num2.ToString());
			return text2.text;
		}
		else
		{
			bool flag = false;
			if (EventManager.Instance != null && EventManager.Instance.IsSpecialStage())
			{
				flag = true;
			}
			ObjUtil.SendMessageFinalScore();
			StageScoreManager instance = StageScoreManager.Instance;
			if (instance == null)
			{
				return string.Empty;
			}
			RankingManager instance2 = SingletonGameObject<RankingManager>.Instance;
			if (instance2 == null)
			{
				return string.Empty;
			}
			long num3;
			if (!flag)
			{
				num3 = instance.FinalScore;
			}
			else
			{
				num3 = instance.FinalCountData.sp_crystal;
			}
			RankingUtil.RankingMode rankingMode = RankingUtil.RankingMode.ENDLESS;
			if (StageModeManager.Instance != null && StageModeManager.Instance.IsQuickMode())
			{
				rankingMode = RankingUtil.RankingMode.QUICK;
			}
			long num4 = 0L;
			long num5 = 0L;
			int num6 = 0;
			RankingManager.GetCurrentRankingStatus(rankingMode, flag, out num4, out num5, out num6);
			long score = num3;
			bool flag2 = false;
			long num7 = 0L;
			long num8 = 0L;
			int rank = 0;
			RankingUtil.RankingScoreType currentRankingScoreType = RankingManager.GetCurrentRankingScoreType(rankingMode, flag);
			int currentHighScoreRank = RankingManager.GetCurrentHighScoreRank(rankingMode, flag, ref score, out flag2, out num7, out num8, out rank);
			if (currentRankingScoreType == RankingUtil.RankingScoreType.TOTAL_SCORE)
			{
				flag2 = true;
			}
			LeagueType league = (LeagueType)num6;
			if (currentHighScoreRank == 1 && flag2 && num7 == 0L && num8 == 0L)
			{
				if (!flag)
				{
					return this.GetTextObject("label_rsring_continue_F", league, 0, 0, 0L, currentRankingScoreType);
				}
				return this.GetSpStageTextObject("label_rsring_continue_SP_A", 1, 0, score, 0L, currentRankingScoreType);
			}
			else if (!flag2)
			{
				if (!flag)
				{
					long score2 = num5 - num3 + 1L;
					return this.GetTextObject("label_rsring_continue_A", league, currentHighScoreRank, 0, score2, currentRankingScoreType);
				}
				return this.GetSpStageTextObject("label_rsring_continue_SP_A", currentHighScoreRank, 0, score, 0L, currentRankingScoreType);
			}
			else if (currentHighScoreRank == 1 && num7 == 0L && num8 == 0L)
			{
				if (!flag)
				{
					return this.GetTextObject("label_rsring_continue_F", league, 0, 0, 0L, currentRankingScoreType);
				}
				return this.GetSpStageTextObject("label_rsring_continue_SP_A", 1, 0, score, 0L, currentRankingScoreType);
			}
			else if (currentHighScoreRank == 1 && num7 == 0L && num8 >= 0L)
			{
				if (!flag)
				{
					return this.GetTextObject("label_rsring_continue_C", league, 0, 0, num8, currentRankingScoreType);
				}
				return this.GetSpStageTextObject("label_rsring_continue_SP_C", 0, 0, score, num8, currentRankingScoreType);
			}
			else
			{
				if (currentHighScoreRank <= 1)
				{
					return string.Empty;
				}
				if (!flag)
				{
					return this.GetTextObject("label_rsring_continue_B", league, currentHighScoreRank, rank, num7, currentRankingScoreType);
				}
				return this.GetSpStageTextObject("label_rsring_continue_SP_B", currentHighScoreRank, rank, score, num7, currentRankingScoreType);
			}
		}
	}

	// Token: 0x06001A25 RID: 6693 RVA: 0x00099434 File Offset: 0x00097634
	private string GetTextObject(string cellName, LeagueType league, int rank1, int rank2, long score, RankingUtil.RankingScoreType scoreType)
	{
		string replaceString = string.Empty;
		if (scoreType == RankingUtil.RankingScoreType.HIGH_SCORE)
		{
			replaceString = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ui_Lbl_high_score").text;
		}
		else
		{
			replaceString = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ui_Lbl_total_score").text;
		}
		TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Continue", cellName);
		text.ReplaceTag("{PARAM_LEAGUE}", RankingUtil.GetLeagueName(league));
		text.ReplaceTag("{PARAM_RANK}", rank1.ToString());
		text.ReplaceTag("{PARAM_RANK2}", rank2.ToString());
		text.ReplaceTag("{PARAM_SCORE}", HudUtility.GetFormatNumString<long>(score));
		text.ReplaceTag("{SCORE}", replaceString);
		return text.text;
	}

	// Token: 0x06001A26 RID: 6694 RVA: 0x000994E8 File Offset: 0x000976E8
	private string GetSpStageTextObject(string cellName, int rank1, int rank2, long score, long score2, RankingUtil.RankingScoreType scoreType)
	{
		string replaceString = string.Empty;
		if (scoreType == RankingUtil.RankingScoreType.HIGH_SCORE)
		{
			replaceString = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ui_Lbl_high_score").text;
		}
		else
		{
			replaceString = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ui_Lbl_total_score").text;
		}
		TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Continue", cellName);
		text.ReplaceTag("{PARAM_STAGE}", HudUtility.GetEventStageName());
		text.ReplaceTag("{PARAM_OBJECT}", HudUtility.GetEventSpObjectName());
		text.ReplaceTag("{PARAM_RANK}", HudUtility.GetFormatNumString<int>(rank1));
		text.ReplaceTag("{PARAM_RANK2}", HudUtility.GetFormatNumString<int>(rank2));
		text.ReplaceTag("{PARAM_SCORE}", HudUtility.GetFormatNumString<long>(score));
		text.ReplaceTag("{PARAM_SCORE2}", HudUtility.GetFormatNumString<long>(score2));
		text.ReplaceTag("{SCORE}", replaceString);
		return text.text;
	}

	// Token: 0x06001A27 RID: 6695 RVA: 0x000995B8 File Offset: 0x000977B8
	private bool IsRankerNone(ServerLeaderboardEntries serverLeaderboardEntries)
	{
		return serverLeaderboardEntries.m_leaderboardEntries == null || serverLeaderboardEntries.m_leaderboardEntries.Count <= 0;
	}

	// Token: 0x06001A28 RID: 6696 RVA: 0x000995DC File Offset: 0x000977DC
	private bool IsRankerAlone(ServerLeaderboardEntries serverLeaderboardEntries, ServerLeaderboardEntry myServerLeaderboardEntry)
	{
		return serverLeaderboardEntries.m_leaderboardEntries.Count == 1 && serverLeaderboardEntries.m_leaderboardEntries[0].m_hspId == myServerLeaderboardEntry.m_hspId;
	}

	// Token: 0x06001A29 RID: 6697 RVA: 0x00099614 File Offset: 0x00097814
	private ServerLeaderboardEntry GetOtherRanker(ServerLeaderboardEntries serverLeaderboardEntries, ServerLeaderboardEntry myServerLeaderboardEntry)
	{
		int num = serverLeaderboardEntries.m_leaderboardEntries.Count - 1;
		for (int i = num; i >= 0; i--)
		{
			if (myServerLeaderboardEntry.m_hspId != serverLeaderboardEntries.m_leaderboardEntries[i].m_hspId)
			{
				return serverLeaderboardEntries.m_leaderboardEntries[i];
			}
		}
		return null;
	}

	// Token: 0x06001A2A RID: 6698 RVA: 0x00099670 File Offset: 0x00097870
	private ServerLeaderboardEntry GetRankUpPlayerData(ServerLeaderboardEntries serverLeaderboardEntries, ServerLeaderboardEntry myServerLeaderboardEntry, long playScore)
	{
		List<ServerLeaderboardEntry> list = new List<ServerLeaderboardEntry>();
		foreach (ServerLeaderboardEntry serverLeaderboardEntry in serverLeaderboardEntries.m_leaderboardEntries)
		{
			if (serverLeaderboardEntry.m_score > playScore && serverLeaderboardEntry.m_hspId != myServerLeaderboardEntry.m_hspId)
			{
				list.Add(serverLeaderboardEntry);
				this.DebugInfo(string.Concat(new object[]
				{
					"GetRankUpPlayerData LIST rank=",
					serverLeaderboardEntry.m_grade,
					" score=",
					serverLeaderboardEntry.m_score
				}));
			}
		}
		if (list.Count > 0)
		{
			list.Sort(new Comparison<ServerLeaderboardEntry>(HudContinueWindow.RankComparer));
			return list[0];
		}
		return null;
	}

	// Token: 0x06001A2B RID: 6699 RVA: 0x00099764 File Offset: 0x00097964
	private static int RankComparer(ServerLeaderboardEntry itemA, ServerLeaderboardEntry itemB)
	{
		if (itemA != null && itemB != null)
		{
			if (itemA.m_grade > itemB.m_grade)
			{
				return -1;
			}
			if (itemA.m_grade < itemB.m_grade)
			{
				return 1;
			}
		}
		return 0;
	}

	// Token: 0x06001A2C RID: 6700 RVA: 0x0009979C File Offset: 0x0009799C
	private string GetDailyBattleText()
	{
		string result = string.Empty;
		long num = 0L;
		if (StageModeManager.Instance != null && !StageModeManager.Instance.IsQuickMode())
		{
			return string.Empty;
		}
		DailyBattleManager instance = SingletonGameObject<DailyBattleManager>.Instance;
		if (instance != null && !this.m_bossStage)
		{
			string cellName = string.Empty;
			long num2 = 0L;
			long num3 = 0L;
			long num4 = 0L;
			if (instance.currentWinFlag > 0)
			{
				ServerDailyBattleDataPair currentDataPair = instance.currentDataPair;
				if (currentDataPair != null)
				{
					bool flag = true;
					StageScoreManager instance2 = StageScoreManager.Instance;
					if (instance2 != null)
					{
						num4 = (num = instance2.FinalScore);
						if (currentDataPair.myBattleData != null && !string.IsNullOrEmpty(currentDataPair.myBattleData.userId) && currentDataPair.myBattleData.maxScore > num)
						{
							num = currentDataPair.myBattleData.maxScore;
							flag = false;
						}
					}
					if (currentDataPair.rivalBattleData == null || string.IsNullOrEmpty(currentDataPair.rivalBattleData.userId))
					{
						if (flag)
						{
							cellName = "label_rsring_continue_DB_C";
						}
						else
						{
							num3 = num - num4;
							cellName = "label_rsring_continue_DB_F";
						}
					}
					else
					{
						long maxScore = currentDataPair.rivalBattleData.maxScore;
						if (maxScore > num)
						{
							num2 = maxScore - num;
							if (flag)
							{
								cellName = "label_rsring_continue_DB_B";
							}
							else
							{
								num3 = num - num4;
								cellName = "label_rsring_continue_DB_E";
							}
						}
						else
						{
							num2 = num - maxScore;
							if (flag)
							{
								cellName = "label_rsring_continue_DB_A";
							}
							else
							{
								num3 = num - num4;
								cellName = "label_rsring_continue_DB_D";
							}
						}
					}
				}
			}
			else
			{
				cellName = "label_rsring_continue_DB_C";
			}
			TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Continue", cellName);
			if (text != null)
			{
				text.ReplaceTag("{PARAM_SCORE}", HudUtility.GetFormatNumString<long>(num2));
				text.ReplaceTag("{PARAM_HIGHSCORE}", HudUtility.GetFormatNumString<long>(num3));
				result = text.text;
			}
		}
		return result;
	}

	// Token: 0x06001A2D RID: 6701 RVA: 0x00099980 File Offset: 0x00097B80
	public void PlayStart()
	{
		this.m_pressedButton = HudContinueWindow.PressedButton.NO_PRESSING;
		this.m_state = HudContinueWindow.State.START;
		this.m_waitTime = 0f;
		base.gameObject.SetActive(true);
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component != null)
		{
			ActiveAnimation.Play(component, Direction.Forward);
		}
	}

	// Token: 0x06001A2E RID: 6702 RVA: 0x000999D4 File Offset: 0x00097BD4
	private void Start()
	{
	}

	// Token: 0x06001A2F RID: 6703 RVA: 0x000999D8 File Offset: 0x00097BD8
	private void Update()
	{
		if (this.m_state == HudContinueWindow.State.IDLE)
		{
			return;
		}
		if (this.m_state == HudContinueWindow.State.START)
		{
			this.m_waitTime += RealTime.deltaTime;
			if (this.m_waitTime > 0.5f)
			{
				this.m_state = HudContinueWindow.State.WAIT_TOUCH_BUTTON;
				this.m_waitTime = 0f;
			}
		}
		if (this.m_parentPanel == null)
		{
			this.m_parentPanel = base.gameObject;
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_parentPanel, "Btn_yes");
		if (gameObject != null)
		{
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_rs_cost");
			if (uilabel != null)
			{
				uilabel.text = HudContinueUtility.GetContinueCostString();
			}
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "img_sale_icon");
			if (gameObject2 != null)
			{
				bool active = HudContinueUtility.IsInContinueCostCampaign();
				gameObject2.SetActive(active);
			}
		}
		this.UpdateRedStarRingCount();
	}

	// Token: 0x06001A30 RID: 6704 RVA: 0x00099ABC File Offset: 0x00097CBC
	private void OnClickYesButton()
	{
		if (this.m_state != HudContinueWindow.State.WAIT_TOUCH_BUTTON)
		{
			return;
		}
		this.m_pressedButton = HudContinueWindow.PressedButton.YES;
		SoundManager.SePlay("sys_menu_decide", "SE");
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(base.animation, Direction.Reverse);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedAnimationCallbck), true);
			}
		}
	}

	// Token: 0x06001A31 RID: 6705 RVA: 0x00099B38 File Offset: 0x00097D38
	private void OnClickNoButton()
	{
		if (this.m_state != HudContinueWindow.State.WAIT_TOUCH_BUTTON)
		{
			return;
		}
		this.m_pressedButton = HudContinueWindow.PressedButton.NO;
		SoundManager.SePlay("sys_window_close", "SE");
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(base.animation, Direction.Reverse);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedAnimationCallbck), true);
			}
		}
	}

	// Token: 0x06001A32 RID: 6706 RVA: 0x00099BB4 File Offset: 0x00097DB4
	private void OnClickVideoButton()
	{
		if (this.m_state != HudContinueWindow.State.WAIT_TOUCH_BUTTON)
		{
			return;
		}
		this.m_pressedButton = HudContinueWindow.PressedButton.VIDEO;
		SoundManager.SePlay("sys_menu_decide", "SE");
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(base.animation, Direction.Reverse);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedAnimationCallbck), true);
			}
		}
	}

	// Token: 0x06001A33 RID: 6707 RVA: 0x00099C30 File Offset: 0x00097E30
	public void OnPushBackKey()
	{
		this.OnClickNoButton();
	}

	// Token: 0x06001A34 RID: 6708 RVA: 0x00099C38 File Offset: 0x00097E38
	private void OnFinishedAnimationCallbck()
	{
		base.gameObject.SetActive(false);
		this.m_scoreText = string.Empty;
		this.m_dailyBattleText = string.Empty;
		this.m_state = HudContinueWindow.State.TOUCHED_BUTTON;
	}

	// Token: 0x06001A35 RID: 6709 RVA: 0x00099C64 File Offset: 0x00097E64
	private void DebugInfo(string msg)
	{
	}

	// Token: 0x0400177A RID: 6010
	private const float WAIT_TIME = 0.5f;

	// Token: 0x0400177B RID: 6011
	private bool m_debugInfo;

	// Token: 0x0400177C RID: 6012
	private HudContinueWindow.PressedButton m_pressedButton;

	// Token: 0x0400177D RID: 6013
	private HudContinueWindow.State m_state;

	// Token: 0x0400177E RID: 6014
	private GameObject m_parentPanel;

	// Token: 0x0400177F RID: 6015
	private GameObject m_timeUpObj;

	// Token: 0x04001780 RID: 6016
	private bool m_videoEnabled;

	// Token: 0x04001781 RID: 6017
	private bool m_bossStage;

	// Token: 0x04001782 RID: 6018
	private float m_waitTime;

	// Token: 0x04001783 RID: 6019
	private string m_scoreText;

	// Token: 0x04001784 RID: 6020
	private string m_dailyBattleText;

	// Token: 0x02000372 RID: 882
	private enum PressedButton
	{
		// Token: 0x04001786 RID: 6022
		NO_PRESSING,
		// Token: 0x04001787 RID: 6023
		YES,
		// Token: 0x04001788 RID: 6024
		NO,
		// Token: 0x04001789 RID: 6025
		VIDEO
	}

	// Token: 0x02000373 RID: 883
	private enum State
	{
		// Token: 0x0400178B RID: 6027
		IDLE,
		// Token: 0x0400178C RID: 6028
		START,
		// Token: 0x0400178D RID: 6029
		WAIT_TOUCH_BUTTON,
		// Token: 0x0400178E RID: 6030
		TOUCHED_BUTTON
	}
}
