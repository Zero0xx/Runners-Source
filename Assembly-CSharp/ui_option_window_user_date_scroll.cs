using System;
using Text;
using UnityEngine;

// Token: 0x020004A6 RID: 1190
public class ui_option_window_user_date_scroll : MonoBehaviour
{
	// Token: 0x0600233C RID: 9020 RVA: 0x000D3B40 File Offset: 0x000D1D40
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x0600233D RID: 9021 RVA: 0x000D3B4C File Offset: 0x000D1D4C
	public void UpdateView(ui_option_window_user_date_scroll.ResultType type, ServerOptionUserResult optionUserResult)
	{
		this.m_rusultType = type;
		this.TextInit(optionUserResult);
	}

	// Token: 0x0600233E RID: 9022 RVA: 0x000D3B5C File Offset: 0x000D1D5C
	public void TextInit(ServerOptionUserResult optionUserResult)
	{
		if (this.m_rusultType < ui_option_window_user_date_scroll.ResultType.NUM)
		{
			TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, this.m_textInfos[(int)this.m_rusultType].grop, this.m_textInfos[(int)this.m_rusultType].cell);
			if (this.m_textLabel != null && text != null)
			{
				this.m_textLabel.text = text.text;
			}
		}
		if (this.m_socoreLabel != null)
		{
			this.m_socoreLabel.text = ui_option_window_user_date_scroll.GetScore(this.m_rusultType, optionUserResult);
		}
	}

	// Token: 0x0600233F RID: 9023 RVA: 0x000D3BF4 File Offset: 0x000D1DF4
	private static string GetScore(ui_option_window_user_date_scroll.ResultType type, ServerOptionUserResult optionUserResult)
	{
		long num = 0L;
		if (ServerInterface.PlayerState != null && optionUserResult != null)
		{
			switch (type)
			{
			case ui_option_window_user_date_scroll.ResultType.QUICK_HIGHT_SCORE:
				num = ServerInterface.PlayerState.m_totalHighScoreQuick;
				break;
			case ui_option_window_user_date_scroll.ResultType.QUICK_TOTAL_SCORE:
				num = optionUserResult.m_quickTotalSumHightScore;
				break;
			case ui_option_window_user_date_scroll.ResultType.HIGHT_SCORE:
				num = ServerInterface.PlayerState.m_totalHighScore;
				break;
			case ui_option_window_user_date_scroll.ResultType.TOTAL_SCORE:
				num = optionUserResult.m_totalSumHightScore;
				break;
			case ui_option_window_user_date_scroll.ResultType.DISTANCE:
				num = ServerInterface.PlayerState.m_maxDistance;
				break;
			case ui_option_window_user_date_scroll.ResultType.CUMULATIVE_DISTANCE:
				num = ServerInterface.PlayerState.m_totalDistance;
				break;
			case ui_option_window_user_date_scroll.ResultType.PLAYING_NUM:
				num = (long)ServerInterface.PlayerState.m_numPlaying;
				break;
			case ui_option_window_user_date_scroll.ResultType.RING:
				num = optionUserResult.m_numTakeAllRings;
				break;
			case ui_option_window_user_date_scroll.ResultType.RED_RING:
				num = optionUserResult.m_numTakeAllRedRings;
				break;
			case ui_option_window_user_date_scroll.ResultType.ANIMAL:
				num = (long)ServerInterface.PlayerState.m_numAnimals;
				break;
			case ui_option_window_user_date_scroll.ResultType.CHAO_ROULETTE:
				num = (long)optionUserResult.m_numChaoRoulette;
				break;
			case ui_option_window_user_date_scroll.ResultType.ITEM_ROULETTE:
				num = (long)optionUserResult.m_numItemRoulette;
				break;
			case ui_option_window_user_date_scroll.ResultType.JACK_POT_NUM:
				num = (long)optionUserResult.m_numJackPot;
				break;
			case ui_option_window_user_date_scroll.ResultType.JACK_POT_RING:
				num = (long)optionUserResult.m_numMaximumJackPotRings;
				break;
			}
		}
		if (num < 0L)
		{
			num = 0L;
		}
		else if (num > 999999999999L)
		{
			num = 999999999999L;
		}
		return HudUtility.GetFormatNumString<long>(num);
	}

	// Token: 0x04001FE3 RID: 8163
	private readonly ui_option_window_user_date_scroll.textInfo[] m_textInfos = new ui_option_window_user_date_scroll.textInfo[]
	{
		new ui_option_window_user_date_scroll.textInfo
		{
			grop = "Option",
			cell = "quick_high_score"
		},
		new ui_option_window_user_date_scroll.textInfo
		{
			grop = "Option",
			cell = "quick_total_sum_high_score"
		},
		new ui_option_window_user_date_scroll.textInfo
		{
			grop = "Option",
			cell = "endless_high_score"
		},
		new ui_option_window_user_date_scroll.textInfo
		{
			grop = "Option",
			cell = "endless_total_sum_high_score"
		},
		new ui_option_window_user_date_scroll.textInfo
		{
			grop = "Option",
			cell = "maximum_distance"
		},
		new ui_option_window_user_date_scroll.textInfo
		{
			grop = "Option",
			cell = "cumulative_distance"
		},
		new ui_option_window_user_date_scroll.textInfo
		{
			grop = "Option",
			cell = "playing_num"
		},
		new ui_option_window_user_date_scroll.textInfo
		{
			grop = "Option",
			cell = "take_all_rings"
		},
		new ui_option_window_user_date_scroll.textInfo
		{
			grop = "Option",
			cell = "take_all_red_rings"
		},
		new ui_option_window_user_date_scroll.textInfo
		{
			grop = "Option",
			cell = "take_all_red_animals"
		},
		new ui_option_window_user_date_scroll.textInfo
		{
			grop = "Option",
			cell = "chao_roulette_num"
		},
		new ui_option_window_user_date_scroll.textInfo
		{
			grop = "Option",
			cell = "item_roulette_num"
		},
		new ui_option_window_user_date_scroll.textInfo
		{
			grop = "Option",
			cell = "jack_pot_num"
		},
		new ui_option_window_user_date_scroll.textInfo
		{
			grop = "Option",
			cell = "jack_pot_ring"
		}
	};

	// Token: 0x04001FE4 RID: 8164
	[SerializeField]
	private UILabel m_textLabel;

	// Token: 0x04001FE5 RID: 8165
	[SerializeField]
	private UILabel m_socoreLabel;

	// Token: 0x04001FE6 RID: 8166
	private ui_option_window_user_date_scroll.ResultType m_rusultType = ui_option_window_user_date_scroll.ResultType.HIGHT_SCORE;

	// Token: 0x020004A7 RID: 1191
	public enum ResultType
	{
		// Token: 0x04001FE8 RID: 8168
		QUICK_HIGHT_SCORE,
		// Token: 0x04001FE9 RID: 8169
		QUICK_TOTAL_SCORE,
		// Token: 0x04001FEA RID: 8170
		HIGHT_SCORE,
		// Token: 0x04001FEB RID: 8171
		TOTAL_SCORE,
		// Token: 0x04001FEC RID: 8172
		DISTANCE,
		// Token: 0x04001FED RID: 8173
		CUMULATIVE_DISTANCE,
		// Token: 0x04001FEE RID: 8174
		PLAYING_NUM,
		// Token: 0x04001FEF RID: 8175
		RING,
		// Token: 0x04001FF0 RID: 8176
		RED_RING,
		// Token: 0x04001FF1 RID: 8177
		ANIMAL,
		// Token: 0x04001FF2 RID: 8178
		CHAO_ROULETTE,
		// Token: 0x04001FF3 RID: 8179
		ITEM_ROULETTE,
		// Token: 0x04001FF4 RID: 8180
		JACK_POT_NUM,
		// Token: 0x04001FF5 RID: 8181
		JACK_POT_RING,
		// Token: 0x04001FF6 RID: 8182
		NUM
	}

	// Token: 0x020004A8 RID: 1192
	public class textInfo
	{
		// Token: 0x04001FF7 RID: 8183
		public string grop;

		// Token: 0x04001FF8 RID: 8184
		public string cell;
	}
}
