using System;
using System.Collections;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x020003FB RID: 1019
public class DailyBattleDetailWindow : WindowBase
{
	// Token: 0x17000451 RID: 1105
	// (get) Token: 0x06001E42 RID: 7746 RVA: 0x000B240C File Offset: 0x000B060C
	public static bool isActive
	{
		get
		{
			return DailyBattleDetailWindow.s_isActive;
		}
	}

	// Token: 0x06001E43 RID: 7747 RVA: 0x000B2414 File Offset: 0x000B0614
	private void Start()
	{
	}

	// Token: 0x06001E44 RID: 7748 RVA: 0x000B2418 File Offset: 0x000B0618
	private void OnDestroy()
	{
		base.Destroy();
	}

	// Token: 0x17000452 RID: 1106
	// (get) Token: 0x06001E45 RID: 7749 RVA: 0x000B2420 File Offset: 0x000B0620
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x06001E46 RID: 7750 RVA: 0x000B2428 File Offset: 0x000B0628
	private void Update()
	{
		if (base.gameObject.activeSelf && !this.m_isOpenEffectEnd)
		{
			if (!this.m_isOpenEffectStart && this.m_time >= 0.5f)
			{
				this.m_isOpenEffectStart = true;
				if (this.m_winFlag > 1)
				{
					SoundManager.SePlay("sys_rank_up", "SE");
				}
				else
				{
					SoundManager.SePlay("sys_league_down", "SE");
				}
			}
			else if (this.m_isOpenEffectStart)
			{
				float num = (this.m_time - 0.5f) / 1.5f;
				if (num < 0f)
				{
					num = 0f;
				}
				else if (num > 1f)
				{
					num = 1f;
				}
				long num2 = (long)((float)this.m_targetScore * num);
				if (this.m_mineData != null)
				{
					if (num2 < this.m_mineData.rankerScore)
					{
						this.m_mineData.UpdateViewScore(num2);
					}
					else
					{
						this.m_mineData.UpdateViewScore(-1L);
						if (!this.m_isOpenEffectIssue)
						{
							this.m_isOpenEffectIssue = true;
							this.SetupUserData(this.m_winFlag);
						}
					}
				}
				if (this.m_adversaryData != null)
				{
					if (num2 < this.m_adversaryData.rankerScore)
					{
						this.m_adversaryData.UpdateViewScore(num2);
					}
					else
					{
						this.m_adversaryData.UpdateViewScore(-1L);
						if (!this.m_isOpenEffectIssue)
						{
							this.m_isOpenEffectIssue = true;
							this.SetupUserData(this.m_winFlag);
						}
					}
				}
				if (num >= 1f)
				{
					this.m_isOpenEffectEnd = true;
					if (this.m_mineData != null)
					{
						this.m_mineData.UpdateViewScore(-1L);
					}
					if (this.m_adversaryData != null)
					{
						this.m_adversaryData.UpdateViewScore(-1L);
					}
					this.SetupUserData(this.m_winFlag);
				}
			}
			this.m_time += Time.deltaTime;
		}
	}

	// Token: 0x06001E47 RID: 7751 RVA: 0x000B2620 File Offset: 0x000B0820
	public static bool Open(ServerDailyBattleDataPair data)
	{
		bool result = false;
		GameObject menuAnimUIObject = HudMenuUtility.GetMenuAnimUIObject();
		if (menuAnimUIObject != null)
		{
			DailyBattleDetailWindow dailyBattleDetailWindow = GameObjectUtil.FindChildGameObjectComponent<DailyBattleDetailWindow>(menuAnimUIObject, "DailyBattleDetailWindow");
			if (dailyBattleDetailWindow == null)
			{
				GameObject gameObject = GameObjectUtil.FindChildGameObject(menuAnimUIObject, "DailyBattleDetailWindow");
				if (gameObject != null)
				{
					dailyBattleDetailWindow = gameObject.AddComponent<DailyBattleDetailWindow>();
				}
			}
			if (dailyBattleDetailWindow != null)
			{
				dailyBattleDetailWindow.Setup(data);
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06001E48 RID: 7752 RVA: 0x000B2690 File Offset: 0x000B0890
	private IEnumerator SetupObject()
	{
		yield return null;
		if (this.m_mainPanel != null)
		{
			this.m_mainPanel.alpha = 1f;
		}
		if (this.m_animation != null)
		{
			ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim2", Direction.Forward);
		}
		UIPlayAnimation btnClose = GameObjectUtil.FindChildGameObjectComponent<UIPlayAnimation>(base.gameObject, "Btn_close");
		if (btnClose != null && !EventDelegate.IsValid(btnClose.onFinished))
		{
			EventDelegate.Add(btnClose.onFinished, new EventDelegate.Callback(this.OnFinished), true);
		}
		this.m_targetScore = 0L;
		this.m_time = 0f;
		this.m_winFlag = 0;
		if (this.m_battleData != null)
		{
			this.m_winFlag = this.m_battleData.winFlag;
			if (this.m_battleData != null)
			{
				if (this.m_battleData.myBattleData != null)
				{
					this.m_targetScore = this.m_battleData.myBattleData.maxScore;
				}
				if (this.m_battleData.rivalBattleData != null && this.m_targetScore < this.m_battleData.rivalBattleData.maxScore)
				{
					this.m_targetScore = this.m_battleData.rivalBattleData.maxScore;
				}
			}
		}
		GameObject root = GameObjectUtil.FindChildGameObject(base.gameObject, "window_contents");
		this.m_mineData = null;
		this.m_adversaryData = null;
		if (root != null)
		{
			GameObject body = GameObjectUtil.FindChildGameObject(root, "body");
			if (body != null)
			{
				if (this.m_battleData != null)
				{
					body.SetActive(true);
					this.m_mineSet = GameObjectUtil.FindChildGameObject(body, "duel_mine_set");
					this.m_adversarySet = GameObjectUtil.FindChildGameObject(body, "duel_adversary_set");
					GameObject vsObj = GameObjectUtil.FindChildGameObject(body, "img_word_vs");
					if (vsObj != null)
					{
						bool flg = false;
						if (this.m_battleData.myBattleData != null && !string.IsNullOrEmpty(this.m_battleData.myBattleData.userId) && this.m_battleData.rivalBattleData != null && !string.IsNullOrEmpty(this.m_battleData.rivalBattleData.userId))
						{
							flg = true;
						}
						vsObj.SetActive(flg);
					}
					if (this.m_mineSet != null)
					{
						UIRectItemStorage storage = GameObjectUtil.FindChildGameObjectComponent<UIRectItemStorage>(this.m_mineSet, "score_mine");
						if (storage != null)
						{
							if (this.m_battleData.myBattleData != null && !string.IsNullOrEmpty(this.m_battleData.myBattleData.userId))
							{
								storage.maxItemCount = (storage.maxRows = 1);
								storage.Restart();
								this.m_mineData = this.m_mineSet.GetComponentInChildren<ui_ranking_scroll>();
								if (this.m_mineData != null)
								{
									RankingUtil.Ranker ranker = new RankingUtil.Ranker(this.m_battleData.myBattleData);
									ranker.isBoxCollider = false;
									this.m_mineData.UpdateView(RankingUtil.RankingScoreType.HIGH_SCORE, RankingUtil.RankingRankerType.RIVAL, ranker, true);
									this.m_mineData.UpdateViewScore(0L);
									this.m_mineData.SetMyRanker(true);
								}
							}
							else
							{
								storage.maxItemCount = (storage.maxRows = 0);
								storage.Restart();
							}
						}
					}
					if (this.m_adversarySet != null)
					{
						UIRectItemStorage storage2 = GameObjectUtil.FindChildGameObjectComponent<UIRectItemStorage>(this.m_adversarySet, "score_adversary");
						if (storage2 != null)
						{
							if (this.m_battleData.rivalBattleData != null && !string.IsNullOrEmpty(this.m_battleData.rivalBattleData.userId))
							{
								storage2.maxItemCount = (storage2.maxRows = 1);
								storage2.Restart();
								this.m_adversaryData = this.m_adversarySet.GetComponentInChildren<ui_ranking_scroll>();
								if (this.m_adversaryData != null)
								{
									RankingUtil.Ranker ranker2 = new RankingUtil.Ranker(this.m_battleData.rivalBattleData);
									ranker2.isBoxCollider = false;
									if (ranker2.isFriend)
									{
										ranker2.isSentEnergy = true;
									}
									this.m_adversaryData.UpdateView(RankingUtil.RankingScoreType.HIGH_SCORE, RankingUtil.RankingRankerType.RIVAL, ranker2, true);
									this.m_adversaryData.UpdateViewScore(0L);
									this.m_adversaryData.SetMyRanker(false);
								}
							}
							else
							{
								storage2.maxItemCount = (storage2.maxRows = 0);
								storage2.Restart();
							}
						}
					}
					this.m_result = GameObjectUtil.FindChildGameObjectComponent<UILabel>(body, "Lbl_result");
					this.SetupUserData(0);
				}
				else
				{
					body.SetActive(false);
				}
			}
			UILabel caption = GameObjectUtil.FindChildGameObjectComponent<UILabel>(root, "Lbl_caption");
			if (caption != null)
			{
				caption.text = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "DailyMission", "battle_detail_caption");
			}
		}
		this.m_isEnd = false;
		this.m_isClickClose = false;
		yield break;
	}

	// Token: 0x06001E49 RID: 7753 RVA: 0x000B26AC File Offset: 0x000B08AC
	private void Setup(ServerDailyBattleDataPair data)
	{
		DailyBattleDetailWindow.s_isActive = true;
		base.gameObject.SetActive(true);
		this.m_battleData = data;
		this.m_isEnd = false;
		this.m_isClickClose = false;
		base.enabled = true;
		this.m_isOpenEffectStart = false;
		this.m_isOpenEffectIssue = false;
		this.m_isOpenEffectEnd = false;
		base.StartCoroutine(this.SetupObject());
	}

	// Token: 0x06001E4A RID: 7754 RVA: 0x000B270C File Offset: 0x000B090C
	private void SetupUserData(int winOrLose = 0)
	{
		if (this.m_battleData != null)
		{
			if (this.m_mineSet != null)
			{
				GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_mineSet, "duel_win_set");
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_mineSet, "duel_lose_set");
				if (winOrLose <= 0 || this.m_battleData.myBattleData == null || string.IsNullOrEmpty(this.m_battleData.myBattleData.userId))
				{
					gameObject.SetActive(false);
					gameObject2.SetActive(false);
				}
				else if (winOrLose == 1)
				{
					gameObject.SetActive(false);
					gameObject2.SetActive(true);
				}
				else
				{
					gameObject.SetActive(true);
					gameObject2.SetActive(false);
				}
			}
			if (this.m_adversarySet != null)
			{
				GameObject gameObject3 = GameObjectUtil.FindChildGameObject(this.m_adversarySet, "duel_win_set");
				GameObject gameObject4 = GameObjectUtil.FindChildGameObject(this.m_adversarySet, "duel_lose_set");
				if (winOrLose <= 0 || this.m_battleData.rivalBattleData == null || string.IsNullOrEmpty(this.m_battleData.rivalBattleData.userId))
				{
					gameObject3.SetActive(false);
					gameObject4.SetActive(false);
				}
				else if (winOrLose == 1)
				{
					gameObject3.SetActive(true);
					gameObject4.SetActive(false);
				}
				else
				{
					gameObject3.SetActive(false);
					gameObject4.SetActive(true);
				}
			}
			if (this.m_result != null)
			{
				if (winOrLose <= 0)
				{
					this.m_result.text = string.Empty;
				}
				else if (winOrLose == 4 && !GeneralUtil.IsOverTime(this.m_battleData.endTime, 0))
				{
					this.m_result.text = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "DailyMission", "battle_result_still");
				}
				else if (winOrLose == 1)
				{
					this.m_result.text = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "DailyMission", "battle_result_lose");
				}
				else
				{
					this.m_result.text = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "DailyMission", "battle_result_win");
				}
			}
			if (this.m_winFlag == 0)
			{
				this.m_isOpenEffectStart = true;
				this.m_isOpenEffectIssue = true;
				this.m_isOpenEffectEnd = true;
				if (this.m_mineData != null)
				{
					this.m_mineData.UpdateViewScore(-1L);
				}
				if (this.m_adversaryData != null)
				{
					this.m_adversaryData.UpdateViewScore(-1L);
				}
				if (this.m_result != null)
				{
					this.m_result.text = TextUtility.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "DailyMission", "battle_result_failure");
				}
			}
		}
	}

	// Token: 0x06001E4B RID: 7755 RVA: 0x000B2998 File Offset: 0x000B0B98
	public void OnOpen()
	{
	}

	// Token: 0x06001E4C RID: 7756 RVA: 0x000B299C File Offset: 0x000B0B9C
	public void OnFinished()
	{
		DailyBattleDetailWindow.s_isActive = false;
		this.m_isEnd = true;
		if (this.m_mainPanel != null)
		{
			this.m_mainPanel.alpha = 0f;
		}
		base.gameObject.SetActive(false);
		base.enabled = false;
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x06001E4D RID: 7757 RVA: 0x000B29FC File Offset: 0x000B0BFC
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (this.m_isEnd)
		{
			return;
		}
		if (msg != null)
		{
			msg.StaySequence();
		}
		if (!ranking_window.isActive && !this.m_isClickClose)
		{
			this.m_isClickClose = true;
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim2", Direction.Reverse);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinished), true);
			}
		}
	}

	// Token: 0x04001B9D RID: 7069
	private const float OPEN_EFFECT_START = 0.5f;

	// Token: 0x04001B9E RID: 7070
	private const float OPEN_EFFECT_TIME = 1.5f;

	// Token: 0x04001B9F RID: 7071
	private static bool s_isActive;

	// Token: 0x04001BA0 RID: 7072
	[SerializeField]
	private Animation m_animation;

	// Token: 0x04001BA1 RID: 7073
	[SerializeField]
	private UIPanel m_mainPanel;

	// Token: 0x04001BA2 RID: 7074
	private bool m_isClickClose;

	// Token: 0x04001BA3 RID: 7075
	private bool m_isEnd;

	// Token: 0x04001BA4 RID: 7076
	private bool m_isOpenEffectStart;

	// Token: 0x04001BA5 RID: 7077
	private bool m_isOpenEffectIssue;

	// Token: 0x04001BA6 RID: 7078
	private bool m_isOpenEffectEnd;

	// Token: 0x04001BA7 RID: 7079
	private float m_time;

	// Token: 0x04001BA8 RID: 7080
	private long m_targetScore;

	// Token: 0x04001BA9 RID: 7081
	private int m_winFlag;

	// Token: 0x04001BAA RID: 7082
	private GameObject m_mineSet;

	// Token: 0x04001BAB RID: 7083
	private GameObject m_adversarySet;

	// Token: 0x04001BAC RID: 7084
	private UILabel m_result;

	// Token: 0x04001BAD RID: 7085
	private ui_ranking_scroll m_mineData;

	// Token: 0x04001BAE RID: 7086
	private ui_ranking_scroll m_adversaryData;

	// Token: 0x04001BAF RID: 7087
	private ServerDailyBattleDataPair m_battleData;
}
