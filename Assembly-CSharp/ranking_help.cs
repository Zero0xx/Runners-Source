using System;
using System.Collections.Generic;
using DataTable;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000508 RID: 1288
public class ranking_help : WindowBase
{
	// Token: 0x0600269B RID: 9883 RVA: 0x000EADF8 File Offset: 0x000E8FF8
	private void Start()
	{
		this.Setup();
	}

	// Token: 0x0600269C RID: 9884 RVA: 0x000EAE00 File Offset: 0x000E9000
	public void Open(bool rewardListRest)
	{
		this.m_open = true;
		if (rewardListRest)
		{
			this.m_rewardListInit = false;
		}
		this.m_rankingMode = RankingUtil.currentRankingMode;
	}

	// Token: 0x0600269D RID: 9885 RVA: 0x000EAE24 File Offset: 0x000E9024
	private void Setup()
	{
		int currentLeague = 0;
		RankingUtil.GetMyLeagueData(this.m_rankingMode, ref currentLeague, ref this.m_upCount, ref this.m_dnCount);
		this.m_currentLeague = (LeagueType)currentLeague;
		if (this.m_labelLeague == null)
		{
			this.m_labelLeague = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_league");
		}
		if (this.m_labelLeagueEx == null)
		{
			this.m_labelLeagueEx = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_league_ex");
		}
		if (this.m_labelBody == null)
		{
			this.m_labelBody = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_body");
		}
		if (this.m_imgRanks == null)
		{
			this.m_imgRanks = new Dictionary<string, UISprite>();
			foreach (string text in new List<string>
			{
				"F",
				"E",
				"D",
				"C",
				"B",
				"A",
				"S"
			})
			{
				UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_rank_" + text.ToLower());
				if (uisprite != null)
				{
					this.m_imgRanks.Add(text, uisprite);
				}
			}
		}
		if (this.m_imgLeagueIcon == null)
		{
			this.m_imgLeagueIcon = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_icon_league");
		}
		if (this.m_imgLeagueStar == null)
		{
			this.m_imgLeagueStar = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_icon_league_sub");
		}
		this.m_labelLeague.text = ranking_help.GetRankingCurrent(this.m_currentLeague);
		this.m_labelLeagueEx.text = ranking_help.GetRankingHelpText(this.m_rankingMode, this.m_currentLeague);
		this.m_labelBody.text = ranking_help.GetRankingHelpPresentText();
		if (this.page0 != null)
		{
			UIDraggablePanel uidraggablePanel = GameObjectUtil.FindChildGameObjectComponent<UIDraggablePanel>(this.page0, "ScrollView");
			if (uidraggablePanel != null)
			{
				uidraggablePanel.ResetPosition();
			}
		}
		this.SetCurrentRankImage(this.m_currentLeague);
	}

	// Token: 0x0600269E RID: 9886 RVA: 0x000EB08C File Offset: 0x000E928C
	public void OnClose()
	{
		SoundManager.SePlay("sys_window_close", "SE");
		this.m_open = false;
	}

	// Token: 0x0600269F RID: 9887 RVA: 0x000EB0A8 File Offset: 0x000E92A8
	private static string GetRankingHelpPresentText()
	{
		string result = string.Empty;
		if (SingletonGameObject<RankingManager>.Instance == null)
		{
			return result;
		}
		ServerLeagueData leagueData = SingletonGameObject<RankingManager>.Instance.GetLeagueData(RankingUtil.currentRankingMode);
		if (leagueData != null)
		{
			string itemText = RankingLeagueTable.GetItemText(leagueData.highScoreOpe, null, null, 0, false);
			string itemText2 = RankingLeagueTable.GetItemText(leagueData.totalScoreOpe, null, null, 0, false);
			result = TextUtility.Replaces(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_help_1").text, new Dictionary<string, string>
			{
				{
					"{PARAM1}",
					itemText
				},
				{
					"{PARAM2}",
					itemText2
				}
			});
		}
		return result;
	}

	// Token: 0x060026A0 RID: 9888 RVA: 0x000EB144 File Offset: 0x000E9344
	private bool SetCurrentRankImage(LeagueType leagueType)
	{
		if (this.m_imgRanks != null && this.m_imgRanks.Count > 0)
		{
			string leagueCategoryName = RankingUtil.GetLeagueCategoryName(leagueType);
			if (this.m_imgRanks.ContainsKey(leagueCategoryName))
			{
				Dictionary<string, UISprite>.KeyCollection keys = this.m_imgRanks.Keys;
				foreach (string key in keys)
				{
					this.m_imgRanks[key].gameObject.SetActive(false);
				}
				this.m_imgRanks[leagueCategoryName].gameObject.SetActive(true);
				this.m_imgLeagueIcon.spriteName = "ui_ranking_league_icon_" + leagueCategoryName.ToLower();
				this.m_imgLeagueStar.spriteName = "ui_ranking_league_icon_" + RankingUtil.GetLeagueCategoryClass(leagueType);
			}
		}
		return false;
	}

	// Token: 0x060026A1 RID: 9889 RVA: 0x000EB248 File Offset: 0x000E9448
	private static string GetRankingCurrent(LeagueType leagueType)
	{
		string empty = string.Empty;
		string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_help_5").text;
		return TextUtility.Replaces(text, new Dictionary<string, string>
		{
			{
				"{PARAM_1}",
				RankingUtil.GetLeagueName(leagueType)
			}
		});
	}

	// Token: 0x060026A2 RID: 9890 RVA: 0x000EB294 File Offset: 0x000E9494
	private static string GetRankingHelpText(RankingUtil.RankingMode rankingMode, LeagueType leagueType)
	{
		int num = 21;
		int num2 = (int)(LeagueType.NUM - leagueType);
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		RankingUtil.GetMyLeagueData(rankingMode, ref num5, ref num3, ref num4);
		string result = string.Empty;
		if (num2 == 1)
		{
			result = TextUtility.Replaces(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_help_3").text, new Dictionary<string, string>
			{
				{
					"{PARAM_1}",
					num.ToString()
				},
				{
					"{PARAM_2}",
					num2.ToString()
				},
				{
					"{PARAM_4}",
					num4.ToString()
				}
			});
		}
		else if (num2 == 21 || num4 <= 0)
		{
			result = TextUtility.Replaces(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_help_4").text, new Dictionary<string, string>
			{
				{
					"{PARAM_1}",
					num.ToString()
				},
				{
					"{PARAM_2}",
					num2.ToString()
				},
				{
					"{PARAM_3}",
					num3.ToString()
				}
			});
		}
		else
		{
			result = TextUtility.Replaces(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_help_2").text, new Dictionary<string, string>
			{
				{
					"{PARAM_1}",
					num.ToString()
				},
				{
					"{PARAM_2}",
					num2.ToString()
				},
				{
					"{PARAM_3}",
					num3.ToString()
				},
				{
					"{PARAM_4}",
					num4.ToString()
				}
			});
		}
		return result;
	}

	// Token: 0x060026A3 RID: 9891 RVA: 0x000EB414 File Offset: 0x000E9614
	private void OnClickToggle()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		if (!this.page1.activeSelf && !this.m_rewardListInit)
		{
			this.page1Table.repositionNow = false;
			List<GameObject> list = GameObjectUtil.FindChildGameObjects(this.page1Table.gameObject, "ui_ranking_reward(Clone)");
			if (list != null && list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					GameObject parent = list[i];
					UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(parent, "img_icon_league");
					UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(parent, "img_icon_league_sub");
					UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parent, "Lbl_body");
					uilabel.text = string.Empty;
					uilabel.alpha = 0f;
					uisprite.alpha = 0f;
					uisprite2.alpha = 0f;
					string leagueCategoryName = RankingUtil.GetLeagueCategoryName((LeagueType)(list.Count - (i + 1)));
					uisprite.spriteName = "ui_ranking_league_icon_s_" + leagueCategoryName.ToLower();
					uisprite2.spriteName = "ui_ranking_league_icon_s_" + RankingUtil.GetLeagueCategoryClass((LeagueType)(list.Count - (i + 1)));
				}
			}
			if (ServerInterface.LoggedInServerInterface != null)
			{
				ServerInterface.LoggedInServerInterface.RequestServerGetLeagueOperatorData((int)RankingUtil.currentRankingMode, base.gameObject);
			}
			this.m_rewardListInit = true;
			this.page1Table.repositionNow = true;
		}
	}

	// Token: 0x060026A4 RID: 9892 RVA: 0x000EB57C File Offset: 0x000E977C
	private void ServerGetLeagueOperatorData_Succeeded(MsgGetLeagueOperatorDataSucceed msg)
	{
		this.page1Table.repositionNow = false;
		List<GameObject> list = GameObjectUtil.FindChildGameObjects(this.page1Table.gameObject, "ui_ranking_reward(Clone)");
		if (list != null && list.Count > 0)
		{
			for (int i = 0; i < msg.m_leagueOperatorData.Count; i++)
			{
				int index = msg.m_leagueOperatorData.Count - (i + 1);
				ServerLeagueOperatorData serverLeagueOperatorData = msg.m_leagueOperatorData[index];
				if (serverLeagueOperatorData != null)
				{
					GameObject gameObject = list[i];
					UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_icon_league");
					UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_icon_league_sub");
					UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_body");
					uisprite.alpha = 1f;
					uisprite2.alpha = 1f;
					uilabel.alpha = 1f;
					string leagueCategoryName = RankingUtil.GetLeagueCategoryName((LeagueType)serverLeagueOperatorData.leagueId);
					uisprite.spriteName = "ui_ranking_league_icon_s_" + leagueCategoryName.ToLower();
					uisprite2.spriteName = "ui_ranking_league_icon_s_" + RankingUtil.GetLeagueCategoryClass((LeagueType)serverLeagueOperatorData.leagueId);
					string itemText = RankingLeagueTable.GetItemText(serverLeagueOperatorData.highScoreOpe, null, null, 0, false);
					string itemText2 = RankingLeagueTable.GetItemText(serverLeagueOperatorData.totalScoreOpe, null, null, 0, false);
					string text = string.Empty;
					text = TextUtility.Replaces(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_help_1").text, new Dictionary<string, string>
					{
						{
							"{PARAM1}",
							itemText
						},
						{
							"{PARAM2}",
							itemText2
						}
					});
					uilabel.text = text;
					gameObject.SendMessage("OnClickBg");
				}
			}
		}
		this.page1Table.repositionNow = true;
	}

	// Token: 0x060026A5 RID: 9893 RVA: 0x000EB724 File Offset: 0x000E9924
	private void ServerGetLeagueOperatorData_Failed()
	{
		this.m_rewardListInit = false;
	}

	// Token: 0x060026A6 RID: 9894 RVA: 0x000EB730 File Offset: 0x000E9930
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (!this.m_open)
		{
			return;
		}
		if (msg != null)
		{
			msg.StaySequence();
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_close");
		if (gameObject != null)
		{
			UIButtonMessage component = gameObject.GetComponent<UIButtonMessage>();
			if (component != null)
			{
				component.SendMessage("OnClick");
			}
		}
	}

	// Token: 0x040022D2 RID: 8914
	[SerializeField]
	private GameObject page0;

	// Token: 0x040022D3 RID: 8915
	[SerializeField]
	private GameObject page1;

	// Token: 0x040022D4 RID: 8916
	[SerializeField]
	private UITable page1Table;

	// Token: 0x040022D5 RID: 8917
	private UILabel m_labelLeague;

	// Token: 0x040022D6 RID: 8918
	private UILabel m_labelLeagueEx;

	// Token: 0x040022D7 RID: 8919
	private UILabel m_labelBody;

	// Token: 0x040022D8 RID: 8920
	private Dictionary<string, UISprite> m_imgRanks;

	// Token: 0x040022D9 RID: 8921
	private UISprite m_imgLeagueIcon;

	// Token: 0x040022DA RID: 8922
	private UISprite m_imgLeagueStar;

	// Token: 0x040022DB RID: 8923
	private LeagueType m_currentLeague;

	// Token: 0x040022DC RID: 8924
	private int m_upCount;

	// Token: 0x040022DD RID: 8925
	private int m_dnCount;

	// Token: 0x040022DE RID: 8926
	private bool m_rewardListInit;

	// Token: 0x040022DF RID: 8927
	private bool m_open;

	// Token: 0x040022E0 RID: 8928
	private RankingUtil.RankingMode m_rankingMode;
}
