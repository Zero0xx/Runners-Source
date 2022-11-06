using System;
using System.Collections;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x020003FC RID: 1020
public class DailyBattleRewardWindow : WindowBase
{
	// Token: 0x17000453 RID: 1107
	// (get) Token: 0x06001E50 RID: 7760 RVA: 0x000B2A80 File Offset: 0x000B0C80
	public static bool isActive
	{
		get
		{
			return DailyBattleRewardWindow.s_isActive;
		}
	}

	// Token: 0x06001E51 RID: 7761 RVA: 0x000B2A88 File Offset: 0x000B0C88
	private void Start()
	{
	}

	// Token: 0x06001E52 RID: 7762 RVA: 0x000B2A8C File Offset: 0x000B0C8C
	private void OnDestroy()
	{
		base.Destroy();
	}

	// Token: 0x17000454 RID: 1108
	// (get) Token: 0x06001E53 RID: 7763 RVA: 0x000B2A94 File Offset: 0x000B0C94
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x06001E54 RID: 7764 RVA: 0x000B2A9C File Offset: 0x000B0C9C
	private void Update()
	{
	}

	// Token: 0x06001E55 RID: 7765 RVA: 0x000B2AA0 File Offset: 0x000B0CA0
	private IEnumerator SetupObject()
	{
		yield return null;
		if (this.m_battleData != null)
		{
			GameObject loseWindow = GameObjectUtil.FindChildGameObject(base.gameObject, "lose");
			GameObject winWindow = GameObjectUtil.FindChildGameObject(base.gameObject, "win");
			if (winWindow != null && loseWindow != null)
			{
				switch (this.m_battleData.winFlag)
				{
				case 0:
				case 1:
				{
					winWindow.SetActive(false);
					loseWindow.SetActive(true);
					string loseText = TextUtility.GetCommonText("DailyMission", "battle_vsreward_text3");
					UILabel loseLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(loseWindow, "Lbl_daily_battle_lose");
					if (loseLabel != null)
					{
						loseLabel.text = loseText;
					}
					UILabel loseLabel_sh = GameObjectUtil.FindChildGameObjectComponent<UILabel>(loseWindow, "Lbl_daily_battle_lose_sh");
					if (loseLabel_sh != null)
					{
						loseLabel_sh.text = loseText;
					}
					break;
				}
				case 2:
				case 3:
				case 4:
				{
					winWindow.SetActive(true);
					loseWindow.SetActive(false);
					int winCount = this.m_battleData.goOnWin;
					TextObject textObject;
					if (winCount < 2)
					{
						textObject = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "DailyMission", "battle_vsreward_text1");
					}
					else
					{
						textObject = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "DailyMission", "battle_vsreward_text2");
						textObject.ReplaceTag("{PARAM_WIN}", winCount.ToString());
					}
					UILabel winLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(winWindow, "Lbl_daily_battle_win");
					if (winLabel != null)
					{
						winLabel.text = textObject.text;
					}
					UILabel winLabel_sh = GameObjectUtil.FindChildGameObjectComponent<UILabel>(winWindow, "Lbl_daily_battle_win_sh");
					if (winLabel_sh != null)
					{
						winLabel_sh.text = textObject.text;
					}
					break;
				}
				}
			}
		}
		if (this.m_mainPanel != null)
		{
			this.m_mainPanel.alpha = 1f;
		}
		GameObject window = GameObjectUtil.FindChildGameObject(base.gameObject, "ranking_window");
		if (window != null)
		{
			window.SetActive(true);
		}
		if (this.m_animation != null)
		{
			ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim", Direction.Forward);
		}
		UIPlayAnimation btnClose = GameObjectUtil.FindChildGameObjectComponent<UIPlayAnimation>(base.gameObject, "Btn_close");
		if (btnClose != null && !EventDelegate.IsValid(btnClose.onFinished))
		{
			EventDelegate.Add(btnClose.onFinished, new EventDelegate.Callback(this.OnFinished), true);
		}
		UIPlayAnimation blinder = GameObjectUtil.FindChildGameObjectComponent<UIPlayAnimation>(base.gameObject, "blinder");
		if (blinder != null && !EventDelegate.IsValid(blinder.onFinished))
		{
			EventDelegate.Add(blinder.onFinished, new EventDelegate.Callback(this.OnFinished), true);
		}
		this.m_isEnd = false;
		this.m_isClickClose = false;
		if (this.m_battleData != null && this.m_battleData.winFlag >= 2)
		{
			SoundManager.SePlay("sys_league_up", "SE");
		}
		else
		{
			SoundManager.SePlay("sys_league_down", "SE");
		}
		yield break;
	}

	// Token: 0x06001E56 RID: 7766 RVA: 0x000B2ABC File Offset: 0x000B0CBC
	private void Setup(ServerDailyBattleDataPair data)
	{
		DailyBattleRewardWindow.s_isActive = true;
		base.gameObject.SetActive(true);
		this.m_battleData = data;
		this.m_isEnd = false;
		this.m_isClickClose = false;
		base.enabled = true;
		base.StartCoroutine(this.SetupObject());
	}

	// Token: 0x06001E57 RID: 7767 RVA: 0x000B2B04 File Offset: 0x000B0D04
	public static DailyBattleRewardWindow Open(ServerDailyBattleDataPair data)
	{
		DailyBattleRewardWindow dailyBattleRewardWindow = null;
		GameObject menuAnimUIObject = HudMenuUtility.GetMenuAnimUIObject();
		if (menuAnimUIObject != null)
		{
			dailyBattleRewardWindow = GameObjectUtil.FindChildGameObjectComponent<DailyBattleRewardWindow>(menuAnimUIObject, "DailybattleRewardWindowUI");
			if (dailyBattleRewardWindow == null)
			{
				GameObject gameObject = GameObjectUtil.FindChildGameObject(menuAnimUIObject, "DailybattleRewardWindowUI");
				if (gameObject != null)
				{
					dailyBattleRewardWindow = gameObject.AddComponent<DailyBattleRewardWindow>();
				}
			}
			if (dailyBattleRewardWindow != null)
			{
				dailyBattleRewardWindow.Setup(data);
			}
		}
		return dailyBattleRewardWindow;
	}

	// Token: 0x06001E58 RID: 7768 RVA: 0x000B2B70 File Offset: 0x000B0D70
	public void OnFinished()
	{
		DailyBattleRewardWindow.s_isActive = false;
		this.m_isEnd = true;
		if (this.m_mainPanel != null)
		{
			this.m_mainPanel.alpha = 0f;
		}
		base.gameObject.SetActive(false);
		base.enabled = false;
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x06001E59 RID: 7769 RVA: 0x000B2BD0 File Offset: 0x000B0DD0
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
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim", Direction.Reverse);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinished), true);
			}
		}
	}

	// Token: 0x04001BB0 RID: 7088
	private const float OPEN_EFFECT_START = 0.5f;

	// Token: 0x04001BB1 RID: 7089
	private const float OPEN_EFFECT_TIME = 2f;

	// Token: 0x04001BB2 RID: 7090
	private static bool s_isActive;

	// Token: 0x04001BB3 RID: 7091
	[SerializeField]
	private Animation m_animation;

	// Token: 0x04001BB4 RID: 7092
	[SerializeField]
	private UIPanel m_mainPanel;

	// Token: 0x04001BB5 RID: 7093
	private bool m_isClickClose;

	// Token: 0x04001BB6 RID: 7094
	private bool m_isEnd;

	// Token: 0x04001BB7 RID: 7095
	private ServerDailyBattleDataPair m_battleData;
}
