using System;

namespace SaveData
{
	// Token: 0x020002A9 RID: 681
	public class PlayerData
	{
		// Token: 0x060012F9 RID: 4857 RVA: 0x00069088 File Offset: 0x00067288
		public PlayerData()
		{
			this.m_progress_status = 0U;
			this.m_total_distance = 0L;
			this.m_challenge_count = 3U;
			this.m_best_score = 0L;
			this.m_best_score_quick = 0L;
			this.m_main_chao_id = -1;
			this.m_sub_chao_id = -1;
			this.m_friend_chao_id = -1;
			this.m_friend_chao_level = -1;
			this.m_rental_friend_id = string.Empty;
			this.m_main_chara_type = CharaType.SONIC;
			this.m_sub_chara_type = CharaType.UNKNOWN;
			this.m_rank = 1U;
			for (int i = 0; i < 3; i++)
			{
				this.m_equipped_item[i] = ItemType.UNKNOWN;
				this.m_equipped_item_use_status[i] = ItemStatus.NO_USE;
			}
			for (int j = 0; j < 3; j++)
			{
				this.m_boosted_item[j] = false;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x0006917C File Offset: 0x0006737C
		// (set) Token: 0x060012FB RID: 4859 RVA: 0x00069184 File Offset: 0x00067384
		public uint Rank
		{
			get
			{
				return this.m_rank;
			}
			set
			{
				this.m_rank = value;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x00069190 File Offset: 0x00067390
		// (set) Token: 0x060012FD RID: 4861 RVA: 0x00069198 File Offset: 0x00067398
		public int RankOffset
		{
			get
			{
				return this.m_rank_offset;
			}
			set
			{
				this.m_rank_offset = value;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x000691A4 File Offset: 0x000673A4
		public uint DisplayRank
		{
			get
			{
				return ((this.RankOffset < 0) ? (this.Rank - (uint)(-(uint)this.RankOffset)) : (this.Rank + (uint)this.RankOffset)) + 1U;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x000691E0 File Offset: 0x000673E0
		// (set) Token: 0x06001300 RID: 4864 RVA: 0x000691E8 File Offset: 0x000673E8
		public uint ProgressStatus
		{
			get
			{
				return this.m_progress_status;
			}
			set
			{
				this.m_progress_status = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x000691F4 File Offset: 0x000673F4
		// (set) Token: 0x06001302 RID: 4866 RVA: 0x000691FC File Offset: 0x000673FC
		public long TotalDistance
		{
			get
			{
				return this.m_total_distance;
			}
			set
			{
				this.m_total_distance = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x00069208 File Offset: 0x00067408
		// (set) Token: 0x06001304 RID: 4868 RVA: 0x00069210 File Offset: 0x00067410
		public uint ChallengeCount
		{
			get
			{
				return this.m_challenge_count;
			}
			set
			{
				this.m_challenge_count = value;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x0006921C File Offset: 0x0006741C
		// (set) Token: 0x06001306 RID: 4870 RVA: 0x00069224 File Offset: 0x00067424
		public int ChallengeCountOffset
		{
			get
			{
				return this.m_challenge_count_offset;
			}
			set
			{
				this.m_challenge_count_offset = value;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x00069230 File Offset: 0x00067430
		public uint DisplayChallengeCount
		{
			get
			{
				return (this.ChallengeCountOffset < 0) ? (this.ChallengeCount - (uint)(-(uint)this.ChallengeCountOffset)) : (this.ChallengeCount + (uint)this.ChallengeCountOffset);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x0006926C File Offset: 0x0006746C
		// (set) Token: 0x06001309 RID: 4873 RVA: 0x00069274 File Offset: 0x00067474
		public long BestScore
		{
			get
			{
				return this.m_best_score;
			}
			set
			{
				this.m_best_score = value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x00069280 File Offset: 0x00067480
		// (set) Token: 0x0600130B RID: 4875 RVA: 0x00069288 File Offset: 0x00067488
		public long BestScoreQuick
		{
			get
			{
				return this.m_best_score_quick;
			}
			set
			{
				this.m_best_score_quick = value;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x00069294 File Offset: 0x00067494
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x0006929C File Offset: 0x0006749C
		public CharaType MainChara
		{
			get
			{
				return this.m_main_chara_type;
			}
			set
			{
				this.m_main_chara_type = value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x000692A8 File Offset: 0x000674A8
		// (set) Token: 0x0600130F RID: 4879 RVA: 0x000692B0 File Offset: 0x000674B0
		public CharaType SubChara
		{
			get
			{
				return this.m_sub_chara_type;
			}
			set
			{
				this.m_sub_chara_type = value;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x000692BC File Offset: 0x000674BC
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x000692C4 File Offset: 0x000674C4
		public int MainChaoID
		{
			get
			{
				return this.m_main_chao_id;
			}
			set
			{
				this.m_main_chao_id = value;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x000692D0 File Offset: 0x000674D0
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x000692D8 File Offset: 0x000674D8
		public int SubChaoID
		{
			get
			{
				return this.m_sub_chao_id;
			}
			set
			{
				this.m_sub_chao_id = value;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x000692E4 File Offset: 0x000674E4
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x000692EC File Offset: 0x000674EC
		public int FriendChaoID
		{
			get
			{
				return this.m_friend_chao_id;
			}
			set
			{
				this.m_friend_chao_id = value;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x000692F8 File Offset: 0x000674F8
		// (set) Token: 0x06001317 RID: 4887 RVA: 0x00069300 File Offset: 0x00067500
		public int FriendChaoLevel
		{
			get
			{
				return this.m_friend_chao_level;
			}
			set
			{
				this.m_friend_chao_level = value;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x0006930C File Offset: 0x0006750C
		// (set) Token: 0x06001319 RID: 4889 RVA: 0x00069314 File Offset: 0x00067514
		public string RentalFriendId
		{
			get
			{
				return this.m_rental_friend_id;
			}
			set
			{
				this.m_rental_friend_id = value;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x00069320 File Offset: 0x00067520
		// (set) Token: 0x0600131B RID: 4891 RVA: 0x00069328 File Offset: 0x00067528
		public ItemType[] EquippedItem
		{
			get
			{
				return this.m_equipped_item;
			}
			set
			{
				this.m_equipped_item = value;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x00069334 File Offset: 0x00067534
		// (set) Token: 0x0600131D RID: 4893 RVA: 0x0006933C File Offset: 0x0006753C
		public ItemStatus[] EquippedItemUseStatue
		{
			get
			{
				return this.m_equipped_item_use_status;
			}
			set
			{
				this.m_equipped_item_use_status = value;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x00069348 File Offset: 0x00067548
		// (set) Token: 0x0600131F RID: 4895 RVA: 0x00069350 File Offset: 0x00067550
		public bool[] BoostedItem
		{
			get
			{
				return this.m_boosted_item;
			}
			set
			{
				this.m_boosted_item = value;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x0006935C File Offset: 0x0006755C
		// (set) Token: 0x06001321 RID: 4897 RVA: 0x00069364 File Offset: 0x00067564
		public DailyMissionData DailyMission
		{
			get
			{
				return this.m_daily_mission_data;
			}
			set
			{
				this.m_daily_mission_data = value;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x00069370 File Offset: 0x00067570
		// (set) Token: 0x06001323 RID: 4899 RVA: 0x00069378 File Offset: 0x00067578
		public DailyMissionData BeforeDailyMissionData
		{
			get
			{
				return this.m_beforeDailyMissionData;
			}
			set
			{
				this.m_beforeDailyMissionData = value;
			}
		}

		// Token: 0x040010AD RID: 4269
		public const uint MAX_CHALLENGE_COUNT = 99999U;

		// Token: 0x040010AE RID: 4270
		private uint m_progress_status;

		// Token: 0x040010AF RID: 4271
		private long m_total_distance;

		// Token: 0x040010B0 RID: 4272
		private uint m_challenge_count;

		// Token: 0x040010B1 RID: 4273
		private int m_challenge_count_offset;

		// Token: 0x040010B2 RID: 4274
		private long m_best_score;

		// Token: 0x040010B3 RID: 4275
		private long m_best_score_quick;

		// Token: 0x040010B4 RID: 4276
		private uint m_rank;

		// Token: 0x040010B5 RID: 4277
		private int m_rank_offset;

		// Token: 0x040010B6 RID: 4278
		private CharaType m_main_chara_type;

		// Token: 0x040010B7 RID: 4279
		private CharaType m_sub_chara_type;

		// Token: 0x040010B8 RID: 4280
		private int m_main_chao_id;

		// Token: 0x040010B9 RID: 4281
		private int m_sub_chao_id;

		// Token: 0x040010BA RID: 4282
		private int m_friend_chao_id;

		// Token: 0x040010BB RID: 4283
		private int m_friend_chao_level;

		// Token: 0x040010BC RID: 4284
		private string m_rental_friend_id;

		// Token: 0x040010BD RID: 4285
		private DailyMissionData m_daily_mission_data = new DailyMissionData();

		// Token: 0x040010BE RID: 4286
		private DailyMissionData m_beforeDailyMissionData = new DailyMissionData();

		// Token: 0x040010BF RID: 4287
		private ItemType[] m_equipped_item = new ItemType[3];

		// Token: 0x040010C0 RID: 4288
		private ItemStatus[] m_equipped_item_use_status = new ItemStatus[3];

		// Token: 0x040010C1 RID: 4289
		private bool[] m_boosted_item = new bool[3];
	}
}
