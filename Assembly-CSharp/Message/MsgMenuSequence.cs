using System;

namespace Message
{
	// Token: 0x020005C1 RID: 1473
	public class MsgMenuSequence : MessageBase
	{
		// Token: 0x06002B32 RID: 11058 RVA: 0x00109E7C File Offset: 0x0010807C
		public MsgMenuSequence(MsgMenuSequence.SequeneceType sequenece_type) : base(57344)
		{
			this.m_sequenece_type = sequenece_type;
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x00109E90 File Offset: 0x00108090
		public MsgMenuSequence.SequeneceType Sequenece
		{
			get
			{
				return this.m_sequenece_type;
			}
		}

		// Token: 0x040027BC RID: 10172
		private MsgMenuSequence.SequeneceType m_sequenece_type;

		// Token: 0x020005C2 RID: 1474
		public enum SequeneceType
		{
			// Token: 0x040027BE RID: 10174
			MAIN,
			// Token: 0x040027BF RID: 10175
			TITLE,
			// Token: 0x040027C0 RID: 10176
			STAGE,
			// Token: 0x040027C1 RID: 10177
			STAGE_CHECK,
			// Token: 0x040027C2 RID: 10178
			EQUIP_ENTRANCE,
			// Token: 0x040027C3 RID: 10179
			PRESENT_BOX,
			// Token: 0x040027C4 RID: 10180
			DAILY_CHALLENGE,
			// Token: 0x040027C5 RID: 10181
			DAILY_BATTLE,
			// Token: 0x040027C6 RID: 10182
			CHARA_MAIN,
			// Token: 0x040027C7 RID: 10183
			CHAO,
			// Token: 0x040027C8 RID: 10184
			ITEM,
			// Token: 0x040027C9 RID: 10185
			PLAY_ITEM,
			// Token: 0x040027CA RID: 10186
			OPTION,
			// Token: 0x040027CB RID: 10187
			RANKING,
			// Token: 0x040027CC RID: 10188
			RANKING_END,
			// Token: 0x040027CD RID: 10189
			INFOMATION,
			// Token: 0x040027CE RID: 10190
			ROULETTE,
			// Token: 0x040027CF RID: 10191
			CHAO_ROULETTE,
			// Token: 0x040027D0 RID: 10192
			ITEM_ROULETTE,
			// Token: 0x040027D1 RID: 10193
			SHOP,
			// Token: 0x040027D2 RID: 10194
			EPISODE,
			// Token: 0x040027D3 RID: 10195
			EPISODE_PLAY,
			// Token: 0x040027D4 RID: 10196
			EPISODE_RANKING,
			// Token: 0x040027D5 RID: 10197
			QUICK,
			// Token: 0x040027D6 RID: 10198
			QUICK_RANKING,
			// Token: 0x040027D7 RID: 10199
			PLAY_AT_EPISODE_PAGE,
			// Token: 0x040027D8 RID: 10200
			MAIN_PLAY_BUTTON,
			// Token: 0x040027D9 RID: 10201
			TUTORIAL_PAGE_MOVE,
			// Token: 0x040027DA RID: 10202
			CLOSE_DAILY_MISSION_WINDOW,
			// Token: 0x040027DB RID: 10203
			EVENT_TOP,
			// Token: 0x040027DC RID: 10204
			EVENT_SPECIAL,
			// Token: 0x040027DD RID: 10205
			EVENT_RAID,
			// Token: 0x040027DE RID: 10206
			EVENT_COLLECT,
			// Token: 0x040027DF RID: 10207
			BACK,
			// Token: 0x040027E0 RID: 10208
			NON = -1
		}
	}
}
