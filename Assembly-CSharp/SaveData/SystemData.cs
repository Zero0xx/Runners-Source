using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using App.Utility;

namespace SaveData
{
	// Token: 0x020002B4 RID: 692
	public class SystemData
	{
		// Token: 0x0600137B RID: 4987 RVA: 0x00069F0C File Offset: 0x0006810C
		public SystemData()
		{
			this.Init();
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x00069F28 File Offset: 0x00068128
		// (set) Token: 0x0600137D RID: 4989 RVA: 0x00069F30 File Offset: 0x00068130
		public int version { get; set; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x00069F3C File Offset: 0x0006813C
		// (set) Token: 0x0600137F RID: 4991 RVA: 0x00069F44 File Offset: 0x00068144
		public int bgmVolume { get; set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x00069F50 File Offset: 0x00068150
		// (set) Token: 0x06001381 RID: 4993 RVA: 0x00069F58 File Offset: 0x00068158
		public int seVolume { get; set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x00069F64 File Offset: 0x00068164
		// (set) Token: 0x06001383 RID: 4995 RVA: 0x00069F6C File Offset: 0x0006816C
		public int achievementIncentiveCount { get; set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x00069F78 File Offset: 0x00068178
		// (set) Token: 0x06001385 RID: 4997 RVA: 0x00069F80 File Offset: 0x00068180
		public int iap { get; set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x00069F8C File Offset: 0x0006818C
		// (set) Token: 0x06001387 RID: 4999 RVA: 0x00069F94 File Offset: 0x00068194
		public bool pushNotice { get; set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x00069FA0 File Offset: 0x000681A0
		// (set) Token: 0x06001389 RID: 5001 RVA: 0x00069FA8 File Offset: 0x000681A8
		public bool lightMode { get; set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x00069FB4 File Offset: 0x000681B4
		// (set) Token: 0x0600138B RID: 5003 RVA: 0x00069FBC File Offset: 0x000681BC
		public bool highTexture { get; set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x00069FC8 File Offset: 0x000681C8
		// (set) Token: 0x0600138D RID: 5005 RVA: 0x00069FD0 File Offset: 0x000681D0
		public string noahId { get; set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x00069FDC File Offset: 0x000681DC
		// (set) Token: 0x0600138F RID: 5007 RVA: 0x00069FE4 File Offset: 0x000681E4
		public string purchasedRecipt { get; set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x00069FF0 File Offset: 0x000681F0
		// (set) Token: 0x06001391 RID: 5009 RVA: 0x00069FF8 File Offset: 0x000681F8
		public string country { get; set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x0006A004 File Offset: 0x00068204
		// (set) Token: 0x06001393 RID: 5011 RVA: 0x0006A00C File Offset: 0x0006820C
		public string deckData { get; set; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x0006A018 File Offset: 0x00068218
		// (set) Token: 0x06001395 RID: 5013 RVA: 0x0006A020 File Offset: 0x00068220
		public string facebookTime { get; set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x0006A02C File Offset: 0x0006822C
		// (set) Token: 0x06001397 RID: 5015 RVA: 0x0006A034 File Offset: 0x00068234
		public string gameStartTime { get; set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x0006A040 File Offset: 0x00068240
		// (set) Token: 0x06001399 RID: 5017 RVA: 0x0006A048 File Offset: 0x00068248
		public int playCount { get; set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x0006A054 File Offset: 0x00068254
		// (set) Token: 0x0600139B RID: 5019 RVA: 0x0006A05C File Offset: 0x0006825C
		public string loginRankigTime { get; set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x0006A068 File Offset: 0x00068268
		// (set) Token: 0x0600139D RID: 5021 RVA: 0x0006A070 File Offset: 0x00068270
		public int achievementCancelCount { get; set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x0006A07C File Offset: 0x0006827C
		// (set) Token: 0x0600139F RID: 5023 RVA: 0x0006A084 File Offset: 0x00068284
		public Bitset32 flags { get; set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0006A090 File Offset: 0x00068290
		// (set) Token: 0x060013A1 RID: 5025 RVA: 0x0006A098 File Offset: 0x00068298
		public Bitset32 itemTutorialFlags { get; set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x0006A0A4 File Offset: 0x000682A4
		// (set) Token: 0x060013A3 RID: 5027 RVA: 0x0006A0AC File Offset: 0x000682AC
		public Bitset32 charaTutorialFlags { get; set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x0006A0B8 File Offset: 0x000682B8
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x0006A0C0 File Offset: 0x000682C0
		public Bitset32 actionTutorialFlags { get; set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x0006A0CC File Offset: 0x000682CC
		// (set) Token: 0x060013A7 RID: 5031 RVA: 0x0006A0D4 File Offset: 0x000682D4
		public Bitset32 quickModeTutorialFlags { get; set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x0006A0E0 File Offset: 0x000682E0
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x0006A0E8 File Offset: 0x000682E8
		public Bitset32 pushNoticeFlags { get; set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x0006A0F4 File Offset: 0x000682F4
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x0006A0FC File Offset: 0x000682FC
		public int pictureShowEventId { get; set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x0006A108 File Offset: 0x00068308
		// (set) Token: 0x060013AD RID: 5037 RVA: 0x0006A110 File Offset: 0x00068310
		public int pictureShowProgress { get; set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x0006A11C File Offset: 0x0006831C
		// (set) Token: 0x060013AF RID: 5039 RVA: 0x0006A124 File Offset: 0x00068324
		public int pictureShowEmergeRaidBossProgress { get; set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x0006A130 File Offset: 0x00068330
		// (set) Token: 0x060013B1 RID: 5041 RVA: 0x0006A138 File Offset: 0x00068338
		public int pictureShowRaidBossFirstBattle { get; set; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x0006A144 File Offset: 0x00068344
		// (set) Token: 0x060013B3 RID: 5043 RVA: 0x0006A14C File Offset: 0x0006834C
		public long currentRaidDrawIndex { get; set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x0006A158 File Offset: 0x00068358
		// (set) Token: 0x060013B5 RID: 5045 RVA: 0x0006A160 File Offset: 0x00068360
		public bool raidEntryFlag { get; set; }

		// Token: 0x060013B6 RID: 5046 RVA: 0x0006A16C File Offset: 0x0006836C
		public void SetFlagStatus(SystemData.FlagStatus status, bool flag)
		{
			this.flags = this.flags.Set((int)status, flag);
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0006A190 File Offset: 0x00068390
		public bool IsFlagStatus(SystemData.FlagStatus status)
		{
			return this.flags.Test((int)status);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0006A1AC File Offset: 0x000683AC
		public void SetFlagStatus(SystemData.ItemTutorialFlagStatus status, bool flag)
		{
			this.itemTutorialFlags = this.itemTutorialFlags.Set((int)status, flag);
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0006A1D0 File Offset: 0x000683D0
		public bool IsFlagStatus(SystemData.ItemTutorialFlagStatus status)
		{
			return this.itemTutorialFlags.Test((int)status);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0006A1EC File Offset: 0x000683EC
		public void SetFlagStatus(SystemData.CharaTutorialFlagStatus status, bool flag)
		{
			this.charaTutorialFlags = this.charaTutorialFlags.Set((int)status, flag);
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0006A210 File Offset: 0x00068410
		public bool IsFlagStatus(SystemData.CharaTutorialFlagStatus status)
		{
			return this.charaTutorialFlags.Test((int)status);
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0006A22C File Offset: 0x0006842C
		public void SetFlagStatus(SystemData.ActionTutorialFlagStatus status, bool flag)
		{
			this.actionTutorialFlags = this.actionTutorialFlags.Set((int)status, flag);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0006A250 File Offset: 0x00068450
		public bool IsFlagStatus(SystemData.ActionTutorialFlagStatus status)
		{
			return this.actionTutorialFlags.Test((int)status);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0006A26C File Offset: 0x0006846C
		public void SetFlagStatus(SystemData.QuickModeTutorialFlagStatus status, bool flag)
		{
			this.quickModeTutorialFlags = this.quickModeTutorialFlags.Set((int)status, flag);
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0006A290 File Offset: 0x00068490
		public bool IsFlagStatus(SystemData.QuickModeTutorialFlagStatus status)
		{
			return this.quickModeTutorialFlags.Test((int)status);
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0006A2AC File Offset: 0x000684AC
		public void SetFlagStatus(SystemData.PushNoticeFlagStatus status, bool flag)
		{
			this.pushNoticeFlags = this.pushNoticeFlags.Set((int)status, flag);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0006A2D0 File Offset: 0x000684D0
		public bool IsFlagStatus(SystemData.PushNoticeFlagStatus status)
		{
			return this.pushNoticeFlags.Test((int)status);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0006A2EC File Offset: 0x000684EC
		public void Init(int ver)
		{
			this.Init();
			this.version = ver;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0006A2FC File Offset: 0x000684FC
		public void Init()
		{
			this.iap = 0;
			this.version = 0;
			this.bgmVolume = 100;
			this.seVolume = 100;
			this.achievementIncentiveCount = 0;
			this.pushNotice = false;
			this.lightMode = false;
			this.highTexture = false;
			this.noahId = string.Empty;
			this.purchasedRecipt = string.Empty;
			this.country = string.Empty;
			this.deckData = SystemData.DeckAllDefalut();
			this.facebookTime = DateTime.Now.ToString();
			this.gameStartTime = DateTime.Now.ToString();
			this.playCount = 0;
			this.loginRankigTime = string.Empty;
			this.achievementCancelCount = 0;
			this.flags = new Bitset32(0U);
			this.itemTutorialFlags = new Bitset32(0U);
			this.charaTutorialFlags = new Bitset32(0U);
			this.actionTutorialFlags = new Bitset32(0U);
			this.quickModeTutorialFlags = new Bitset32(0U);
			this.pushNoticeFlags = new Bitset32(0U);
			this.SetFlagStatus(SystemData.PushNoticeFlagStatus.EVENT_INFO, false);
			this.SetFlagStatus(SystemData.PushNoticeFlagStatus.CHALLENGE_INFO, false);
			this.SetFlagStatus(SystemData.PushNoticeFlagStatus.FRIEND_INFO, false);
			this.pictureShowEventId = -1;
			this.pictureShowProgress = -1;
			this.pictureShowEmergeRaidBossProgress = -1;
			this.pictureShowRaidBossFirstBattle = -1;
			this.currentRaidDrawIndex = -1L;
			this.raidEntryFlag = false;
			this.chaoSortType01 = 0;
			this.chaoSortType02 = 0;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0006A448 File Offset: 0x00068648
		public static string DeckAllDefalut()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 6; i++)
			{
				stringBuilder.Append(SystemData.deckDefalut);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x0006A480 File Offset: 0x00068680
		private static string deckDefalut
		{
			get
			{
				int num = 6;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(string.Empty + 300000 + ",");
				for (int i = 0; i < num - 1; i++)
				{
					stringBuilder.Append("-1,");
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0006A4DC File Offset: 0x000686DC
		public bool CheckDeck()
		{
			if (string.IsNullOrEmpty(this.deckData))
			{
				return false;
			}
			bool result = false;
			string[] array = this.deckData.Split(new char[]
			{
				','
			});
			if (array != null && array.Length > 0 && array.Length >= 36)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0006A530 File Offset: 0x00068730
		public bool CheckExsitDeck()
		{
			SaveDataManager instance = SaveDataManager.Instance;
			CharaType mainChara = instance.PlayerData.MainChara;
			CharaType subChara = instance.PlayerData.SubChara;
			int mainChaoID = instance.PlayerData.MainChaoID;
			int subChaoID = instance.PlayerData.SubChaoID;
			for (int i = 0; i < 6; i++)
			{
				CharaType charaType;
				CharaType charaType2;
				int num;
				int num2;
				this.GetDeckData(i, out charaType, out charaType2, out num, out num2);
				if (mainChara == charaType && subChara == charaType2 && mainChaoID == num && subChaoID == num2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0006A5C0 File Offset: 0x000687C0
		public int GetDeckCurrentStockIndex()
		{
			int result = 0;
			SaveDataManager instance = SaveDataManager.Instance;
			CharaType mainChara = instance.PlayerData.MainChara;
			CharaType subChara = instance.PlayerData.SubChara;
			int mainChaoID = instance.PlayerData.MainChaoID;
			int subChaoID = instance.PlayerData.SubChaoID;
			for (int i = 0; i < 6; i++)
			{
				CharaType charaType;
				CharaType charaType2;
				int num;
				int num2;
				this.GetDeckData(i, out charaType, out charaType2, out num, out num2);
				if (mainChara == charaType && subChara == charaType2 && mainChaoID == num && subChaoID == num2)
				{
					result = i;
					break;
				}
			}
			return result;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0006A658 File Offset: 0x00068858
		public void RestDeckData(int stock)
		{
			this.SaveDeckData(stock, CharaType.SONIC, CharaType.UNKNOWN, -1, -1);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0006A668 File Offset: 0x00068868
		public void SaveDeckData(int stock, CharaType currentMainCharaType, CharaType currentSubCharaType, int currentMainId, int currentSubId)
		{
			if (stock < 0 || stock >= 6)
			{
				return;
			}
			int num = -1;
			int id = -1;
			ServerCharacterState serverCharacterState = ServerInterface.PlayerState.CharacterState(currentMainCharaType);
			ServerCharacterState serverCharacterState2 = ServerInterface.PlayerState.CharacterState(currentSubCharaType);
			if (serverCharacterState != null)
			{
				num = serverCharacterState.Id;
			}
			if (serverCharacterState2 != null)
			{
				id = serverCharacterState2.Id;
			}
			if (num < 0)
			{
				num = 300000;
			}
			this.SetDeckId(stock, SystemData.DeckType.CHARA_MAIN, num);
			this.SetDeckId(stock, SystemData.DeckType.CHARA_SUB, id);
			this.SetDeckId(stock, SystemData.DeckType.CHAO_MAIN, currentMainId);
			this.SetDeckId(stock, SystemData.DeckType.CHAO_SUB, currentSubId);
			this.SetDeckId(stock, SystemData.DeckType.YOBI_A, -1);
			this.SetDeckId(stock, SystemData.DeckType.YOBI_B, -1);
			SystemSaveManager.Save();
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0006A714 File Offset: 0x00068914
		public void SaveDeckDataChara(int stock)
		{
			if (stock < 0 || stock >= 6)
			{
				return;
			}
			SaveDataManager instance = SaveDataManager.Instance;
			int id = -1;
			int id2 = -1;
			ServerCharacterState serverCharacterState = ServerInterface.PlayerState.CharacterState(instance.PlayerData.MainChara);
			ServerCharacterState serverCharacterState2 = ServerInterface.PlayerState.CharacterState(instance.PlayerData.SubChara);
			if (serverCharacterState != null)
			{
				id = serverCharacterState.Id;
			}
			if (serverCharacterState2 != null)
			{
				id2 = serverCharacterState2.Id;
			}
			this.SetDeckId(stock, SystemData.DeckType.CHARA_MAIN, id);
			this.SetDeckId(stock, SystemData.DeckType.CHARA_SUB, id2);
			this.SetDeckId(stock, SystemData.DeckType.YOBI_A, -1);
			this.SetDeckId(stock, SystemData.DeckType.YOBI_B, -1);
			SystemSaveManager.Save();
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0006A7B0 File Offset: 0x000689B0
		public void SaveDeckDataChao(int stock)
		{
			if (stock < 0 || stock >= 6)
			{
				return;
			}
			SaveDataManager instance = SaveDataManager.Instance;
			int mainChaoID = instance.PlayerData.MainChaoID;
			int subChaoID = instance.PlayerData.SubChaoID;
			this.SetDeckId(stock, SystemData.DeckType.CHAO_MAIN, mainChaoID);
			this.SetDeckId(stock, SystemData.DeckType.CHAO_SUB, subChaoID);
			this.SetDeckId(stock, SystemData.DeckType.YOBI_A, -1);
			this.SetDeckId(stock, SystemData.DeckType.YOBI_B, -1);
			SystemSaveManager.Save();
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0006A818 File Offset: 0x00068A18
		public bool IsSaveDeckData(int stock)
		{
			if (stock < 0 || stock >= 6)
			{
				return false;
			}
			bool result = true;
			CharaType charaType;
			CharaType charaType2;
			int num;
			int num2;
			this.GetDeckData(stock, out charaType, out charaType2, out num, out num2);
			if (charaType == CharaType.SONIC && charaType2 == CharaType.UNKNOWN && num == -1 && num2 == -1)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0006A868 File Offset: 0x00068A68
		public int GetCurrentDeckData(out CharaType mainCharaType, out CharaType subCharaType, out int mainChaoId, out int subChaoId)
		{
			int deckCurrentStockIndex = this.GetDeckCurrentStockIndex();
			this.GetDeckData(deckCurrentStockIndex, out mainCharaType, out subCharaType, out mainChaoId, out subChaoId);
			return deckCurrentStockIndex;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0006A88C File Offset: 0x00068A8C
		public int GetCurrentDeckData(out CharaType mainCharaType, out CharaType subCharaType)
		{
			int deckCurrentStockIndex = this.GetDeckCurrentStockIndex();
			this.GetDeckData(deckCurrentStockIndex, out mainCharaType, out subCharaType);
			return deckCurrentStockIndex;
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0006A8AC File Offset: 0x00068AAC
		public void GetDeckData(int stock, out CharaType mainCharaType, out CharaType subCharaType, out int mainChaoId, out int subChaoId)
		{
			int deckId = this.GetDeckId(stock, SystemData.DeckType.CHARA_MAIN);
			int deckId2 = this.GetDeckId(stock, SystemData.DeckType.CHARA_SUB);
			ServerItem serverItem = new ServerItem((ServerItem.Id)deckId);
			mainCharaType = serverItem.charaType;
			ServerItem serverItem2 = new ServerItem((ServerItem.Id)deckId2);
			subCharaType = serverItem2.charaType;
			mainChaoId = this.GetDeckId(stock, SystemData.DeckType.CHAO_MAIN);
			subChaoId = this.GetDeckId(stock, SystemData.DeckType.CHAO_SUB);
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0006A904 File Offset: 0x00068B04
		public void GetDeckData(int stock, out CharaType mainCharaType, out CharaType subCharaType)
		{
			int deckId = this.GetDeckId(stock, SystemData.DeckType.CHARA_MAIN);
			int deckId2 = this.GetDeckId(stock, SystemData.DeckType.CHARA_SUB);
			ServerItem serverItem = new ServerItem((ServerItem.Id)deckId);
			mainCharaType = serverItem.charaType;
			ServerItem serverItem2 = new ServerItem((ServerItem.Id)deckId2);
			subCharaType = serverItem2.charaType;
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0006A948 File Offset: 0x00068B48
		public void GetDeckData(int stock, out int mainChaoId, out int subChaoId)
		{
			mainChaoId = this.GetDeckId(stock, SystemData.DeckType.CHAO_MAIN);
			subChaoId = this.GetDeckId(stock, SystemData.DeckType.CHAO_SUB);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0006A960 File Offset: 0x00068B60
		private int GetDeckId(int index, SystemData.DeckType type)
		{
			if (string.IsNullOrEmpty(this.deckData))
			{
				this.deckData = SystemData.DeckAllDefalut();
			}
			int num = -1;
			string[] array = this.deckData.Split(new char[]
			{
				','
			});
			int num2 = 6;
			int num3 = index * num2;
			if (array.Length >= 6 * num2 && array.Length > num3 && type != SystemData.DeckType.NUM)
			{
				string s = array[(int)(num3 + type)];
				num = int.Parse(s);
			}
			if (num < 0)
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0006A9DC File Offset: 0x00068BDC
		private bool SetDeckId(int index, SystemData.DeckType type, int id)
		{
			if (string.IsNullOrEmpty(this.deckData))
			{
				this.deckData = SystemData.DeckAllDefalut();
			}
			bool result = false;
			string[] array = this.deckData.Split(new char[]
			{
				','
			});
			int num = 6;
			int num2 = index * num;
			if (array.Length >= 6 * num && array.Length > num2 && type != SystemData.DeckType.NUM)
			{
				array[(int)(num2 + type)] = id.ToString();
				this.deckData = string.Empty;
				for (int i = 0; i < array.Length; i++)
				{
					this.deckData = this.deckData + array[i] + ",";
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0006AA8C File Offset: 0x00068C8C
		private bool IsFacebookWindowOrg()
		{
			bool result = false;
			if (string.IsNullOrEmpty(this.facebookTime))
			{
				result = true;
			}
			else
			{
				DateTime now = DateTime.Now;
				DateTime t = Convert.ToDateTime(this.facebookTime, DateTimeFormatInfo.InvariantInfo);
				if (now > t)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0006AAD8 File Offset: 0x00068CD8
		public bool IsFacebookWindow()
		{
			return this.IsFacebookWindowOrg();
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0006AAE0 File Offset: 0x00068CE0
		public void SetFacebookWindow(bool isActive, float hideTime)
		{
			if (isActive)
			{
				this.facebookTime = null;
			}
			else
			{
				this.facebookTime = DateTime.Now.AddHours((double)hideTime).ToString();
			}
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0006AB1C File Offset: 0x00068D1C
		public void SetFacebookWindow(bool isActive)
		{
			if (isActive)
			{
				this.facebookTime = null;
			}
			else
			{
				this.facebookTime = DateTime.Now.AddHours(48.0).ToString();
			}
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0006AB60 File Offset: 0x00068D60
		public bool CheckLoginTime()
		{
			bool result = false;
			if (string.IsNullOrEmpty(this.loginRankigTime))
			{
				result = true;
			}
			else
			{
				DateTime currentTime = NetBase.GetCurrentTime();
				DateTime d = Convert.ToDateTime(this.loginRankigTime, DateTimeFormatInfo.InvariantInfo);
				TimeSpan timeSpan = currentTime - d;
				Debug.Log("LoginRanking Span TotalHours =" + timeSpan.TotalHours.ToString());
				if (timeSpan.TotalHours >= 24.0)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0006ABDC File Offset: 0x00068DDC
		public void SetLoginTime()
		{
			this.loginRankigTime = NetBase.GetCurrentTime().ToString();
			Debug.Log("LoginRankingTime=" + this.loginRankigTime);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0006AC14 File Offset: 0x00068E14
		public bool IsNewUser()
		{
			if (this.gameStartTime == null)
			{
				return true;
			}
			DateTime now = DateTime.Now;
			DateTime t = Convert.ToDateTime(this.gameStartTime, DateTimeFormatInfo.InvariantInfo).AddHours(24.0);
			return !(now > t);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0006AC68 File Offset: 0x00068E68
		public void CopyTo(SystemData dst, bool temp)
		{
			dst.version = this.version;
			dst.bgmVolume = this.bgmVolume;
			dst.seVolume = this.seVolume;
			dst.achievementIncentiveCount = this.achievementIncentiveCount;
			dst.pushNotice = this.pushNotice;
			dst.lightMode = this.lightMode;
			dst.highTexture = this.highTexture;
			dst.noahId = this.noahId;
			dst.purchasedRecipt = this.purchasedRecipt;
			dst.country = this.country;
			dst.flags = new Bitset32(this.flags);
			dst.itemTutorialFlags = new Bitset32(this.itemTutorialFlags);
			dst.charaTutorialFlags = new Bitset32(this.charaTutorialFlags);
			dst.actionTutorialFlags = new Bitset32(this.actionTutorialFlags);
			dst.quickModeTutorialFlags = new Bitset32(this.quickModeTutorialFlags);
			dst.pushNoticeFlags = new Bitset32(this.pushNoticeFlags);
			dst.deckData = this.deckData;
			dst.pictureShowEventId = this.pictureShowEventId;
			dst.pictureShowProgress = this.pictureShowProgress;
			dst.pictureShowEmergeRaidBossProgress = this.pictureShowEmergeRaidBossProgress;
			dst.pictureShowRaidBossFirstBattle = this.pictureShowRaidBossFirstBattle;
			dst.currentRaidDrawIndex = this.currentRaidDrawIndex;
			dst.raidEntryFlag = this.raidEntryFlag;
			dst.chaoSortType01 = this.chaoSortType01;
			dst.chaoSortType02 = this.chaoSortType02;
			dst.facebookTime = this.facebookTime;
			dst.gameStartTime = this.gameStartTime;
			dst.playCount = this.playCount;
			dst.loginRankigTime = this.loginRankigTime;
			dst.achievementCancelCount = this.achievementCancelCount;
		}

		// Token: 0x040010E5 RID: 4325
		public const int MAX_STOCK_DECK = 6;

		// Token: 0x040010E6 RID: 4326
		public int chaoSortType01;

		// Token: 0x040010E7 RID: 4327
		public int chaoSortType02;

		// Token: 0x040010E8 RID: 4328
		public List<string> fbFriends = new List<string>();

		// Token: 0x020002B5 RID: 693
		public enum DeckType
		{
			// Token: 0x04001107 RID: 4359
			CHARA_MAIN,
			// Token: 0x04001108 RID: 4360
			CHARA_SUB,
			// Token: 0x04001109 RID: 4361
			CHAO_MAIN,
			// Token: 0x0400110A RID: 4362
			CHAO_SUB,
			// Token: 0x0400110B RID: 4363
			YOBI_A,
			// Token: 0x0400110C RID: 4364
			YOBI_B,
			// Token: 0x0400110D RID: 4365
			NUM
		}

		// Token: 0x020002B6 RID: 694
		public enum FlagStatus
		{
			// Token: 0x0400110F RID: 4367
			NONE = -1,
			// Token: 0x04001110 RID: 4368
			ROULETTE_RULE_EXPLAINED,
			// Token: 0x04001111 RID: 4369
			TUTORIAL_BOSS_PRESENT,
			// Token: 0x04001112 RID: 4370
			TUTORIAL_END,
			// Token: 0x04001113 RID: 4371
			TUTORIAL_BOSS_MAP_1,
			// Token: 0x04001114 RID: 4372
			TUTORIAL_BOSS_MAP_2,
			// Token: 0x04001115 RID: 4373
			TUTORIAL_BOSS_MAP_3,
			// Token: 0x04001116 RID: 4374
			TUTORIAL_ANOTHER_CHARA,
			// Token: 0x04001117 RID: 4375
			INVITE_FRIEND_SEQUENCE_END,
			// Token: 0x04001118 RID: 4376
			RECOMMEND_REVIEW_END,
			// Token: 0x04001119 RID: 4377
			SUB_CHARA_ITEM_EXPLAINED,
			// Token: 0x0400111A RID: 4378
			ANOTHER_CHARA_EXPLAINED,
			// Token: 0x0400111B RID: 4379
			CHARA_LEVEL_UP_EXPLAINED,
			// Token: 0x0400111C RID: 4380
			FRIEDN_ACCEPT_INVITE,
			// Token: 0x0400111D RID: 4381
			TUTORIAL_RANKING,
			// Token: 0x0400111E RID: 4382
			FACEBOOK_FRIEND_INIT,
			// Token: 0x0400111F RID: 4383
			TUTORIAL_FEVER_BOSS,
			// Token: 0x04001120 RID: 4384
			FIRST_LAUNCH_TUTORIAL_END,
			// Token: 0x04001121 RID: 4385
			TUTORIAL_EQIP_ITEM_END
		}

		// Token: 0x020002B7 RID: 695
		public enum ItemTutorialFlagStatus
		{
			// Token: 0x04001123 RID: 4387
			NONE = -1,
			// Token: 0x04001124 RID: 4388
			INVINCIBLE,
			// Token: 0x04001125 RID: 4389
			BARRIER,
			// Token: 0x04001126 RID: 4390
			MAGNET,
			// Token: 0x04001127 RID: 4391
			TRAMPOLINE,
			// Token: 0x04001128 RID: 4392
			COMBO,
			// Token: 0x04001129 RID: 4393
			LASER,
			// Token: 0x0400112A RID: 4394
			DRILL,
			// Token: 0x0400112B RID: 4395
			ASTEROID
		}

		// Token: 0x020002B8 RID: 696
		public enum CharaTutorialFlagStatus
		{
			// Token: 0x0400112D RID: 4397
			NONE = -1,
			// Token: 0x0400112E RID: 4398
			CHARA_1,
			// Token: 0x0400112F RID: 4399
			CHARA_2,
			// Token: 0x04001130 RID: 4400
			CHARA_3,
			// Token: 0x04001131 RID: 4401
			CHARA_4,
			// Token: 0x04001132 RID: 4402
			CHARA_5,
			// Token: 0x04001133 RID: 4403
			CHARA_6,
			// Token: 0x04001134 RID: 4404
			CHARA_7,
			// Token: 0x04001135 RID: 4405
			CHARA_8,
			// Token: 0x04001136 RID: 4406
			CHARA_9,
			// Token: 0x04001137 RID: 4407
			CHARA_10,
			// Token: 0x04001138 RID: 4408
			CHARA_11,
			// Token: 0x04001139 RID: 4409
			CHARA_12,
			// Token: 0x0400113A RID: 4410
			CHARA_13,
			// Token: 0x0400113B RID: 4411
			CHARA_14,
			// Token: 0x0400113C RID: 4412
			CHARA_15,
			// Token: 0x0400113D RID: 4413
			CHARA_16,
			// Token: 0x0400113E RID: 4414
			CHARA_17,
			// Token: 0x0400113F RID: 4415
			CHARA_0,
			// Token: 0x04001140 RID: 4416
			CHARA_18,
			// Token: 0x04001141 RID: 4417
			CHARA_19,
			// Token: 0x04001142 RID: 4418
			CHARA_20,
			// Token: 0x04001143 RID: 4419
			CHARA_21,
			// Token: 0x04001144 RID: 4420
			CHARA_22,
			// Token: 0x04001145 RID: 4421
			CHARA_23,
			// Token: 0x04001146 RID: 4422
			CHARA_24,
			// Token: 0x04001147 RID: 4423
			CHARA_25,
			// Token: 0x04001148 RID: 4424
			CHARA_26,
			// Token: 0x04001149 RID: 4425
			CHARA_27,
			// Token: 0x0400114A RID: 4426
			CHARA_28
		}

		// Token: 0x020002B9 RID: 697
		public enum ActionTutorialFlagStatus
		{
			// Token: 0x0400114C RID: 4428
			NONE = -1,
			// Token: 0x0400114D RID: 4429
			ACTION_1
		}

		// Token: 0x020002BA RID: 698
		public enum QuickModeTutorialFlagStatus
		{
			// Token: 0x0400114F RID: 4431
			NONE = -1,
			// Token: 0x04001150 RID: 4432
			QUICK_1
		}

		// Token: 0x020002BB RID: 699
		public enum PushNoticeFlagStatus
		{
			// Token: 0x04001152 RID: 4434
			NONE = -1,
			// Token: 0x04001153 RID: 4435
			EVENT_INFO,
			// Token: 0x04001154 RID: 4436
			CHALLENGE_INFO,
			// Token: 0x04001155 RID: 4437
			FRIEND_INFO,
			// Token: 0x04001156 RID: 4438
			NUM
		}
	}
}
