using System;
using AnimationOrTween;
using App.Utility;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x0200037E RID: 894
public class GameResult : MonoBehaviour
{
	// Token: 0x170003DB RID: 987
	// (get) Token: 0x06001A76 RID: 6774 RVA: 0x0009BD1C File Offset: 0x00099F1C
	// (set) Token: 0x06001A77 RID: 6775 RVA: 0x0009BD2C File Offset: 0x00099F2C
	public bool IsPressNext
	{
		get
		{
			return this.m_status.Test(0);
		}
		private set
		{
			this.m_status.Set(0, value);
		}
	}

	// Token: 0x170003DC RID: 988
	// (get) Token: 0x06001A78 RID: 6776 RVA: 0x0009BD3C File Offset: 0x00099F3C
	// (set) Token: 0x06001A79 RID: 6777 RVA: 0x0009BD4C File Offset: 0x00099F4C
	public bool IsEndOutAnimation
	{
		get
		{
			return this.m_status.Test(1);
		}
		private set
		{
			this.m_status.Set(1, value);
		}
	}

	// Token: 0x06001A7A RID: 6778 RVA: 0x0009BD5C File Offset: 0x00099F5C
	public void PlayBGStart(GameResult.ResultType resultType, bool isNoMiss, bool isBossTutorialClear, bool isBossDestroy, EventResultState eventResultState)
	{
		this.m_isBossDestroyed = isBossDestroy;
		this.SetupResultType(resultType, isNoMiss, isBossTutorialClear, this.m_isBossDestroyed);
		this.m_resultType = resultType;
		this.m_eventResultState = eventResultState;
		this.IsPressNext = false;
		this.IsEndOutAnimation = false;
		GameResultUtility.SetRaidbossBeatBonus(0);
		global::Debug.Log("GameResult:PlayBGStart >>> eventResultState=" + eventResultState.ToString());
		this.m_eventTimeup = false;
		EventResultState eventResultState2 = this.m_eventResultState;
		if (eventResultState2 != EventResultState.TIMEUP)
		{
			if (eventResultState2 != EventResultState.TIMEUP_RESULT)
			{
				this.m_eventTimeup = false;
			}
			else
			{
				this.m_eventTimeup = true;
			}
		}
		else
		{
			this.m_eventTimeup = true;
		}
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(100);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001A7B RID: 6779 RVA: 0x0009BE28 File Offset: 0x0009A028
	public void PlayScoreStart()
	{
		this.m_isScoreStart = true;
	}

	// Token: 0x06001A7C RID: 6780 RVA: 0x0009BE34 File Offset: 0x0009A034
	public void SetRaidbossBeatBonus(int value)
	{
		GameResultUtility.SetRaidbossBeatBonus(value);
	}

	// Token: 0x06001A7D RID: 6781 RVA: 0x0009BE3C File Offset: 0x0009A03C
	private void Start()
	{
		this.m_fsm = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
		if (this.m_fsm == null)
		{
			return;
		}
		TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
		description.initState = new TinyFsmState(new EventFunction(this.StateIdle));
		description.onFixedUpdate = true;
		this.m_fsm.SetUp(description);
		GameObject gameObject = base.transform.Find("result_Anim").gameObject;
		GameObject gameObject2 = base.transform.Find("result_boss_Anim").gameObject;
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
		if (gameObject2 != null)
		{
			gameObject2.SetActive(false);
		}
		GameResultUtility.SaveOldBestScore();
		this.m_isNomiss = false;
		this.m_isBossTutorialClear = false;
	}

	// Token: 0x06001A7E RID: 6782 RVA: 0x0009BF14 File Offset: 0x0009A114
	private void Update()
	{
	}

	// Token: 0x06001A7F RID: 6783 RVA: 0x0009BF18 File Offset: 0x0009A118
	private void SetupHudResultWindow(bool isCampaignBonus)
	{
		string windowName = "HudResultWindow";
		string windowName2 = "HudResultWindow2";
		if (isCampaignBonus)
		{
			windowName = "HudResultWindow2";
			windowName2 = "HudResultWindow";
		}
		HudResultWindow hudResultWindow = this.GetHudResultWindow(windowName);
		if (hudResultWindow != null)
		{
			hudResultWindow.Setup(base.gameObject, this.m_resultType == GameResult.ResultType.BOSS);
			hudResultWindow.gameObject.SetActive(false);
		}
		HudResultWindow hudResultWindow2 = this.GetHudResultWindow(windowName2);
		if (hudResultWindow2 != null)
		{
			hudResultWindow2.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001A80 RID: 6784 RVA: 0x0009BF98 File Offset: 0x0009A198
	private HudResultWindow GetHudResultWindow(string windowName)
	{
		GameObject gameObject = base.transform.Find(windowName).gameObject;
		if (gameObject != null)
		{
			return gameObject.GetComponent<HudResultWindow>();
		}
		return null;
	}

	// Token: 0x06001A81 RID: 6785 RVA: 0x0009BFCC File Offset: 0x0009A1CC
	private TinyFsmState StateIdle(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		default:
			if (signal != 100)
			{
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateInAnimation)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001A82 RID: 6786 RVA: 0x0009C044 File Offset: 0x0009A244
	private TinyFsmState StateInAnimation(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			this.m_AnimationObject.SetActive(true);
			bool isCampaignBonus = false;
			if (this.m_scores != null)
			{
				this.m_scores.Setup(base.gameObject, this.m_resultRoot, this.m_eventRoot);
				isCampaignBonus = this.m_scores.IsCampaignBonus();
			}
			this.SetupHudResultWindow(isCampaignBonus);
			if (this.m_isReplay)
			{
				return TinyFsmState.End();
			}
			Animation anim = GameResultUtility.SearchAnimation(this.m_AnimationObject);
			string clipName = string.Empty;
			GameResult.ResultType resultType = this.m_resultType;
			if (resultType != GameResult.ResultType.NORMAL)
			{
				if (resultType == GameResult.ResultType.BOSS)
				{
					clipName = "ui_result_boss_intro_bg_Anim";
				}
			}
			else
			{
				clipName = "ui_result_intro_bg_Anim";
			}
			ActiveAnimation activeAnimation = ActiveAnimation.Play(anim, clipName, Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.InAnimationEndCallback), true);
			BoxCollider boxCollider = GameObjectUtil.FindChildGameObjectComponent<BoxCollider>(this.m_resultRoot, "Btn_next");
			if (boxCollider != null)
			{
				boxCollider.isTrigger = false;
			}
			BoxCollider boxCollider2 = GameObjectUtil.FindChildGameObjectComponent<BoxCollider>(this.m_resultRoot, "Btn_details");
			if (boxCollider2 != null)
			{
				boxCollider2.isTrigger = false;
			}
			SoundManager.SePlay("sys_window_open", "SE");
			return TinyFsmState.End();
		}
		default:
			if (signal != 101)
			{
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateScoreInWait)));
			return TinyFsmState.End();
		case 4:
			if (this.m_isReplay)
			{
				this.OnSetEnableDetailsButton(true);
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateScoreInAnimation)));
			}
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001A83 RID: 6787 RVA: 0x0009C210 File Offset: 0x0009A410
	private TinyFsmState StateScoreInWait(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			if (this.m_isScoreStart)
			{
				bool flag = false;
				EventManager instance = EventManager.Instance;
				if (instance != null)
				{
					flag = (instance.EventStage || instance.IsCollectEvent());
				}
				if (this.m_resultType == GameResult.ResultType.BOSS)
				{
					flag = EventManager.Instance.IsRaidBossStage();
				}
				if (flag)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEventScoreDisplaying)));
				}
				else
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateScoreInAnimation)));
				}
				if (this.m_isReplay)
				{
					this.OnSetEnableDetailsButton(true);
				}
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001A84 RID: 6788 RVA: 0x0009C30C File Offset: 0x0009A50C
	private TinyFsmState StateEventScoreDisplaying(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (EventManager.Instance.IsSpecialStage())
			{
				this.m_eventResult = GameObjectUtil.FindChildGameObjectComponent<HudEventResult>(base.gameObject, "EventResult_spstage");
			}
			else if (EventManager.Instance.IsRaidBossStage())
			{
				this.m_eventResult = GameObjectUtil.FindChildGameObjectComponent<HudEventResult>(base.gameObject, "EventResult_raidboss");
			}
			else
			{
				this.m_eventResult = GameObjectUtil.FindChildGameObjectComponent<HudEventResult>(base.gameObject, "EventResult_animal");
			}
			if (this.m_eventResult != null)
			{
				this.m_eventResult.Setup(this.m_eventTimeup);
				this.m_eventResult.PlayStart();
			}
			return TinyFsmState.End();
		default:
			if (signal != 103)
			{
				return TinyFsmState.End();
			}
			if (this.m_eventResult != null)
			{
				this.m_eventResult.PlayOutAnimation();
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_eventResult != null)
			{
				if (this.m_eventResult.IsEndOutAnim)
				{
					bool flag = false;
					EventManager instance = EventManager.Instance;
					if (instance != null)
					{
						if (instance.Type == EventManager.EventType.COLLECT_OBJECT)
						{
							flag = true;
						}
						EventResultState eventResultState = this.m_eventResultState;
						if (eventResultState != EventResultState.TIMEUP)
						{
							if (eventResultState != EventResultState.TIMEUP_RESULT)
							{
							}
							flag = true;
						}
						if (instance.IsRaidBossStage())
						{
							flag = false;
						}
					}
					if (!this.m_eventTimeup || flag)
					{
						this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateScoreInAnimation)));
					}
					else
					{
						if (this.m_scores != null)
						{
							this.m_scores.AllSkip();
						}
						this.IsPressNext = true;
						this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateOutScoreAnimation)));
					}
				}
			}
			else
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateScoreInAnimation)));
			}
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001A85 RID: 6789 RVA: 0x0009C524 File Offset: 0x0009A724
	private TinyFsmState StateScoreInAnimation(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (this.m_scores != null)
			{
				this.m_scores.PlayScoreInAnimation(new EventDelegate.Callback(this.ScoreInAnimationEndCallback));
			}
			return TinyFsmState.End();
		default:
			if (signal != 102)
			{
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateScoreChanging)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001A86 RID: 6790 RVA: 0x0009C5C4 File Offset: 0x0009A7C4
	private TinyFsmState StateScoreChanging(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (this.m_scores != null)
			{
				this.m_scores.PlayStart();
			}
			return TinyFsmState.End();
		default:
			if (signal != 103)
			{
				return TinyFsmState.End();
			}
			if (this.m_scores != null)
			{
				this.m_scores.AllSkip();
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_scores != null && this.m_scores.IsEnd)
			{
				bool flag = false;
				if (!this.m_isReplay)
				{
					if (this.m_isBossTutorialClear)
					{
						flag = true;
					}
					else if (this.m_isNomiss && RouletteManager.Instance != null && RouletteManager.Instance.specialEgg >= 10)
					{
						flag = true;
					}
				}
				if (flag)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSpEggMessage)));
				}
				else
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitButtonPressed)));
				}
			}
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001A87 RID: 6791 RVA: 0x0009C70C File Offset: 0x0009A90C
	private TinyFsmState StateSpEggMessage(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			GeneralWindow.CInfo info = default(GeneralWindow.CInfo);
			info.name = "SpEggMessage";
			info.buttonType = GeneralWindow.ButtonType.Ok;
			if (this.m_isBossTutorialClear)
			{
				info.caption = TextUtility.GetCommonText("ChaoRoulette", "sp_egg_get_caption");
				info.message = TextUtility.GetCommonText("ChaoRoulette", "sp_egg_get_text");
			}
			else
			{
				info.caption = TextUtility.GetCommonText("ChaoRoulette", "sp_egg_max_caption");
				info.message = TextUtility.GetCommonText("ChaoRoulette", "sp_egg_max_text");
			}
			GeneralWindow.Create(info);
			return TinyFsmState.End();
		}
		case 4:
			if (GeneralWindow.IsCreated("SpEggMessage") && GeneralWindow.IsButtonPressed)
			{
				GeneralWindow.Close();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitButtonPressed)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001A88 RID: 6792 RVA: 0x0009C820 File Offset: 0x0009AA20
	private TinyFsmState StateWaitButtonPressed(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.OnSetEnableNextButton(true);
			this.OnSetEnableDetailsButton(true);
			if (!this.m_scores.IsBonusEvent())
			{
				this.OnSetEnableDetailsButton(false);
			}
			return TinyFsmState.End();
		default:
			switch (signal)
			{
			case 103:
				if (this.m_scores != null)
				{
					this.m_scores.AllSkip();
				}
				this.IsPressNext = true;
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateOutScoreAnimation)));
				return TinyFsmState.End();
			case 104:
				this.OnSetEnableNextButton(false);
				this.OnSetEnableDetailsButton(false);
				this.m_isReplay = true;
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateInAnimation)));
				return TinyFsmState.End();
			case 105:
				this.OnSetEnableNextButton(true);
				this.OnSetEnableDetailsButton(true);
				return TinyFsmState.End();
			default:
				return TinyFsmState.End();
			}
			break;
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001A89 RID: 6793 RVA: 0x0009C938 File Offset: 0x0009AB38
	private TinyFsmState StateOutScoreAnimation(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (this.m_scores != null)
			{
				this.m_scores.PlayScoreOutAnimation(new EventDelegate.Callback(this.ScoreOutAnimationCallback));
			}
			return TinyFsmState.End();
		default:
			if (signal != 106)
			{
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateShowCharacterGlowUp)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001A8A RID: 6794 RVA: 0x0009C9D8 File Offset: 0x0009ABD8
	private TinyFsmState StateShowCharacterGlowUp(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			GlowUpWindow glowUpWindow = GameObjectUtil.FindChildGameObjectComponent<GlowUpWindow>(base.gameObject, "ResultPlayerExpWindow");
			if (glowUpWindow != null)
			{
				GlowUpWindow.ExpType expType;
				if (this.m_resultType == GameResult.ResultType.BOSS)
				{
					if (this.m_isBossDestroyed)
					{
						expType = GlowUpWindow.ExpType.BOSS_SUCCESS;
					}
					else
					{
						expType = GlowUpWindow.ExpType.BOSS_FAILED;
					}
				}
				else
				{
					expType = GlowUpWindow.ExpType.RUN_STAGE;
				}
				glowUpWindow.PlayStart(expType);
			}
			return TinyFsmState.End();
		}
		case 4:
		{
			bool flag = false;
			GlowUpWindow glowUpWindow2 = GameObjectUtil.FindChildGameObjectComponent<GlowUpWindow>(base.gameObject, "ResultPlayerExpWindow");
			if (glowUpWindow2 != null)
			{
				if (glowUpWindow2.IsPlayEnd)
				{
					flag = true;
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateOutAnimation)));
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001A8B RID: 6795 RVA: 0x0009CACC File Offset: 0x0009ACCC
	private TinyFsmState StateOutAnimation(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (this.m_scores != null)
			{
				this.m_scores.OnFinishScore();
				this.m_scores.PlayScoreOutAnimation(new EventDelegate.Callback(this.OutAnimationEndCallback));
			}
			this.OnSetEnableDetailsButton(false);
			return TinyFsmState.End();
		default:
			if (signal != 107)
			{
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateFinished)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001A8C RID: 6796 RVA: 0x0009CB80 File Offset: 0x0009AD80
	private TinyFsmState StateFinished(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (this.m_resultRoot != null)
			{
				this.m_resultRoot.SetActive(true);
			}
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001A8D RID: 6797 RVA: 0x0009CBEC File Offset: 0x0009ADEC
	private void OnClickNextButton()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(103);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001A8E RID: 6798 RVA: 0x0009CC30 File Offset: 0x0009AE30
	private void OnClickDetailsButton()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(104);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
		if (this.m_scores != null)
		{
			if (this.m_isReplay)
			{
				this.m_scores.PlaySkip();
			}
			else
			{
				this.m_scores.AllSkip();
			}
		}
		if (this.m_scores != null)
		{
			this.m_scores.SetActiveDetailsButton(true);
		}
	}

	// Token: 0x06001A8F RID: 6799 RVA: 0x0009CCC8 File Offset: 0x0009AEC8
	private void OnClickSkipButton()
	{
		if (this.m_scores != null)
		{
			this.m_scores.PlaySkip();
		}
	}

	// Token: 0x06001A90 RID: 6800 RVA: 0x0009CCE8 File Offset: 0x0009AEE8
	private void OnClickDetailsEndButton()
	{
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(105);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
		if (this.m_scores != null)
		{
			this.m_scores.SetActiveDetailsButton(false);
		}
	}

	// Token: 0x06001A91 RID: 6801 RVA: 0x0009CD38 File Offset: 0x0009AF38
	private void SetupResultType(GameResult.ResultType resultType, bool isNoMiss, bool isBossTutorialClear, bool isRaidBossDestroy)
	{
		GameObject gameObject = base.transform.Find("result_Anim").gameObject;
		GameObject gameObject2 = base.transform.Find("result_boss_Anim").gameObject;
		GameObject gameObject3 = base.transform.Find("result_word_Anim").gameObject;
		GameResultUtility.SetBossDestroyFlag(isRaidBossDestroy);
		if (resultType != GameResult.ResultType.NORMAL)
		{
			if (resultType == GameResult.ResultType.BOSS)
			{
				if (EventManager.Instance.IsRaidBossStage())
				{
					GameResultScoresRaidBoss gameResultScoresRaidBoss = base.gameObject.AddComponent<GameResultScoresRaidBoss>();
					gameResultScoresRaidBoss.SetBossDestroyFlag(isRaidBossDestroy);
					this.m_isNomiss = false;
					this.m_isBossTutorialClear = false;
					this.m_scores = gameResultScoresRaidBoss;
					this.m_resultRoot = gameObject2;
				}
				else
				{
					GameResultScoresBoss gameResultScoresBoss = base.gameObject.AddComponent<GameResultScoresBoss>();
					gameResultScoresBoss.SetNoMissFlag(isNoMiss);
					this.m_isNomiss = isNoMiss;
					this.m_isBossTutorialClear = isBossTutorialClear;
					this.m_scores = gameResultScoresBoss;
					this.m_resultRoot = gameObject2;
				}
				this.m_eventRoot = gameObject3;
			}
		}
		else
		{
			this.m_isNomiss = false;
			this.m_isBossTutorialClear = false;
			this.m_scores = base.gameObject.AddComponent<GameResultScoresNormal>();
			this.m_resultRoot = gameObject;
			this.m_eventRoot = gameObject3;
		}
		BackKeyManager.AddWindowCallBack(base.gameObject);
		this.m_AnimationObject = this.m_resultRoot;
		Animation animation = GameResultUtility.SearchAnimation(this.m_AnimationObject);
		if (animation != null)
		{
			animation.Stop();
		}
		this.m_imageButtonNext = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(this.m_resultRoot, "Btn_next");
		if (this.m_imageButtonNext != null)
		{
			this.m_imageButtonNext.isEnabled = false;
		}
		this.m_imageButtonDetails = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(this.m_resultRoot, "Btn_details");
		if (this.m_imageButtonDetails != null)
		{
			this.m_imageButtonDetails.isEnabled = false;
			UIButtonMessage uibuttonMessage = this.m_imageButtonDetails.gameObject.GetComponent<UIButtonMessage>();
			if (uibuttonMessage == null)
			{
				uibuttonMessage = this.m_imageButtonDetails.gameObject.AddComponent<UIButtonMessage>();
			}
			if (uibuttonMessage != null)
			{
				uibuttonMessage.enabled = true;
				uibuttonMessage.trigger = UIButtonMessage.Trigger.OnClick;
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "OnClickDetailsButton";
			}
		}
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			instance.AddPlayCount();
		}
	}

	// Token: 0x06001A92 RID: 6802 RVA: 0x0009CF7C File Offset: 0x0009B17C
	private void InAnimationEndCallback()
	{
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(101);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001A93 RID: 6803 RVA: 0x0009CFB0 File Offset: 0x0009B1B0
	private void ScoreInAnimationEndCallback()
	{
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(102);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001A94 RID: 6804 RVA: 0x0009CFE4 File Offset: 0x0009B1E4
	private void ScoreOutAnimationCallback()
	{
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(106);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001A95 RID: 6805 RVA: 0x0009D018 File Offset: 0x0009B218
	private void OutAnimationEndCallback()
	{
		this.IsEndOutAnimation = true;
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(107);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001A96 RID: 6806 RVA: 0x0009D054 File Offset: 0x0009B254
	private void OnSetEnableNextButton(bool enabled)
	{
		if (this.m_imageButtonNext != null)
		{
			this.m_imageButtonNext.isEnabled = enabled;
		}
	}

	// Token: 0x06001A97 RID: 6807 RVA: 0x0009D074 File Offset: 0x0009B274
	private void OnSetEnableDetailsButton(bool enabled)
	{
		if (this.m_imageButtonDetails != null)
		{
			this.m_imageButtonDetails.isEnabled = enabled;
		}
	}

	// Token: 0x06001A98 RID: 6808 RVA: 0x0009D094 File Offset: 0x0009B294
	public void OnClickPlatformBackButton()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (this.m_eventResult != null && !this.m_eventResult.IsBackkeyEnable())
		{
			return;
		}
		if (this.m_imageButtonNext != null && this.m_imageButtonNext.isEnabled)
		{
			this.OnClickNextButton();
		}
	}

	// Token: 0x040017DB RID: 6107
	private TinyFsmBehavior m_fsm;

	// Token: 0x040017DC RID: 6108
	private GameObject m_AnimationObject;

	// Token: 0x040017DD RID: 6109
	private GameObject m_resultRoot;

	// Token: 0x040017DE RID: 6110
	private GameObject m_eventRoot;

	// Token: 0x040017DF RID: 6111
	private UIImageButton m_imageButtonNext;

	// Token: 0x040017E0 RID: 6112
	private UIImageButton m_imageButtonDetails;

	// Token: 0x040017E1 RID: 6113
	private GameResultScores m_scores;

	// Token: 0x040017E2 RID: 6114
	private bool m_isNomiss;

	// Token: 0x040017E3 RID: 6115
	private bool m_isBossTutorialClear;

	// Token: 0x040017E4 RID: 6116
	private bool m_isReplay;

	// Token: 0x040017E5 RID: 6117
	private bool m_isBossDestroyed;

	// Token: 0x040017E6 RID: 6118
	private GameResult.ResultType m_resultType;

	// Token: 0x040017E7 RID: 6119
	private bool m_isScoreStart;

	// Token: 0x040017E8 RID: 6120
	private bool m_eventTimeup;

	// Token: 0x040017E9 RID: 6121
	private EventResultState m_eventResultState;

	// Token: 0x040017EA RID: 6122
	private HudEventResult m_eventResult;

	// Token: 0x040017EB RID: 6123
	private Bitset32 m_status;

	// Token: 0x0200037F RID: 895
	public enum ResultType
	{
		// Token: 0x040017ED RID: 6125
		NONE = -1,
		// Token: 0x040017EE RID: 6126
		NORMAL,
		// Token: 0x040017EF RID: 6127
		BOSS
	}

	// Token: 0x02000380 RID: 896
	private enum EventSignal
	{
		// Token: 0x040017F1 RID: 6129
		SIG_BG_START_IN_ANIM = 100,
		// Token: 0x040017F2 RID: 6130
		SIG_END_BG_IN_ANIM,
		// Token: 0x040017F3 RID: 6131
		SIG_END_SCORE_IN_ANIM,
		// Token: 0x040017F4 RID: 6132
		SIG_NEXT_BUTTON_PRESSED,
		// Token: 0x040017F5 RID: 6133
		SIG_DETAILS_BUTTON_PRESSED,
		// Token: 0x040017F6 RID: 6134
		SIG_DETAILS_END_BUTTON_PRESSED,
		// Token: 0x040017F7 RID: 6135
		SIG_END_SCORE_OUT_ANIM,
		// Token: 0x040017F8 RID: 6136
		SIG_END_OUT_ANIM
	}

	// Token: 0x02000381 RID: 897
	private enum Status
	{
		// Token: 0x040017FA RID: 6138
		PRESS_NEXT,
		// Token: 0x040017FB RID: 6139
		END_OUT_ANIM
	}
}
