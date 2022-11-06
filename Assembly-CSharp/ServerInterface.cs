using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007D0 RID: 2000
public class ServerInterface : MonoBehaviour
{
	// Token: 0x1700073B RID: 1851
	// (get) Token: 0x060034BF RID: 13503 RVA: 0x0011E00C File Offset: 0x0011C20C
	public static ServerSettingState SettingState
	{
		get
		{
			return ServerInterface.s_settingState;
		}
	}

	// Token: 0x1700073C RID: 1852
	// (get) Token: 0x060034C0 RID: 13504 RVA: 0x0011E014 File Offset: 0x0011C214
	public static ServerLoginState LoginState
	{
		get
		{
			return ServerInterface.s_loginState;
		}
	}

	// Token: 0x1700073D RID: 1853
	// (get) Token: 0x060034C1 RID: 13505 RVA: 0x0011E01C File Offset: 0x0011C21C
	public static ServerNextVersionState NextVersionState
	{
		get
		{
			return ServerInterface.s_nextState;
		}
	}

	// Token: 0x1700073E RID: 1854
	// (get) Token: 0x060034C2 RID: 13506 RVA: 0x0011E024 File Offset: 0x0011C224
	public static ServerFreeItemState FreeItemState
	{
		get
		{
			return ServerInterface.s_freeItemState;
		}
	}

	// Token: 0x1700073F RID: 1855
	// (get) Token: 0x060034C3 RID: 13507 RVA: 0x0011E02C File Offset: 0x0011C22C
	public static ServerLoginBonusData LoginBonusData
	{
		get
		{
			return ServerInterface.s_loginBonusData;
		}
	}

	// Token: 0x17000740 RID: 1856
	// (get) Token: 0x060034C4 RID: 13508 RVA: 0x0011E034 File Offset: 0x0011C234
	public static ServerNoticeInfo NoticeInfo
	{
		get
		{
			return ServerInterface.s_noticeInfo;
		}
	}

	// Token: 0x17000741 RID: 1857
	// (get) Token: 0x060034C5 RID: 13509 RVA: 0x0011E03C File Offset: 0x0011C23C
	public static ServerTickerInfo TickerInfo
	{
		get
		{
			return ServerInterface.s_tickerInfo;
		}
	}

	// Token: 0x17000742 RID: 1858
	// (get) Token: 0x060034C6 RID: 13510 RVA: 0x0011E044 File Offset: 0x0011C244
	public static ServerPlayerState PlayerState
	{
		get
		{
			return ServerInterface.s_playerState;
		}
	}

	// Token: 0x17000743 RID: 1859
	// (get) Token: 0x060034C7 RID: 13511 RVA: 0x0011E04C File Offset: 0x0011C24C
	public static ServerWheelOptions WheelOptions
	{
		get
		{
			return ServerInterface.s_wheelOptions;
		}
	}

	// Token: 0x17000744 RID: 1860
	// (get) Token: 0x060034C8 RID: 13512 RVA: 0x0011E054 File Offset: 0x0011C254
	public static ServerChaoWheelOptions ChaoWheelOptions
	{
		get
		{
			return ServerInterface.s_chaoWheelOptions;
		}
	}

	// Token: 0x17000745 RID: 1861
	// (get) Token: 0x060034C9 RID: 13513 RVA: 0x0011E05C File Offset: 0x0011C25C
	public static List<ServerRingExchangeList> RingExchangeList
	{
		get
		{
			return ServerInterface.s_ringExchangeList;
		}
	}

	// Token: 0x17000746 RID: 1862
	// (get) Token: 0x060034CA RID: 13514 RVA: 0x0011E064 File Offset: 0x0011C264
	public static List<ServerConsumedCostData> ConsumedCostList
	{
		get
		{
			return ServerInterface.s_consumedCostList;
		}
	}

	// Token: 0x17000747 RID: 1863
	// (get) Token: 0x060034CB RID: 13515 RVA: 0x0011E06C File Offset: 0x0011C26C
	public static List<ServerConsumedCostData> CostList
	{
		get
		{
			return ServerInterface.s_costList;
		}
	}

	// Token: 0x17000748 RID: 1864
	// (get) Token: 0x060034CC RID: 13516 RVA: 0x0011E074 File Offset: 0x0011C274
	public static ServerMileageMapState MileageMapState
	{
		get
		{
			return ServerInterface.s_mileageMapState;
		}
	}

	// Token: 0x17000749 RID: 1865
	// (get) Token: 0x060034CD RID: 13517 RVA: 0x0011E07C File Offset: 0x0011C27C
	public static List<ServerMileageReward> MileageRewardList
	{
		get
		{
			return ServerInterface.s_mileageRewardList;
		}
	}

	// Token: 0x1700074A RID: 1866
	// (get) Token: 0x060034CE RID: 13518 RVA: 0x0011E084 File Offset: 0x0011C284
	public static List<ServerMileageFriendEntry> MileageFriendList
	{
		get
		{
			return ServerInterface.s_mileageFriendList;
		}
	}

	// Token: 0x1700074B RID: 1867
	// (get) Token: 0x060034CF RID: 13519 RVA: 0x0011E08C File Offset: 0x0011C28C
	public static ServerCampaignState CampaignState
	{
		get
		{
			return ServerInterface.s_campaignState;
		}
	}

	// Token: 0x1700074C RID: 1868
	// (get) Token: 0x060034D0 RID: 13520 RVA: 0x0011E094 File Offset: 0x0011C294
	public static List<ServerDistanceFriendEntry> DistanceFriendEntry
	{
		get
		{
			return ServerInterface.s_distanceFriendEntry;
		}
	}

	// Token: 0x1700074D RID: 1869
	// (get) Token: 0x060034D1 RID: 13521 RVA: 0x0011E09C File Offset: 0x0011C29C
	public static ServerLeaderboardEntries LeaderboardEntries
	{
		get
		{
			return ServerInterface.s_leaderboardEntries;
		}
	}

	// Token: 0x1700074E RID: 1870
	// (get) Token: 0x060034D2 RID: 13522 RVA: 0x0011E0A4 File Offset: 0x0011C2A4
	public static ServerLeaderboardEntries LeaderboardEntriesRivalHighScore
	{
		get
		{
			return ServerInterface.s_leaderboardEntriesRivalHighScore;
		}
	}

	// Token: 0x1700074F RID: 1871
	// (get) Token: 0x060034D3 RID: 13523 RVA: 0x0011E0AC File Offset: 0x0011C2AC
	public static ServerLeaderboardEntry LeaderboardEntryRivalHighScoreTop
	{
		get
		{
			return ServerInterface.s_leaderboardEntryRivalHighScoreTop;
		}
	}

	// Token: 0x17000750 RID: 1872
	// (get) Token: 0x060034D4 RID: 13524 RVA: 0x0011E0B4 File Offset: 0x0011C2B4
	public static ServerPrizeState PremiumRoulettePrizeList
	{
		get
		{
			return ServerInterface.s_premiumRoulettePrizeList;
		}
	}

	// Token: 0x17000751 RID: 1873
	// (get) Token: 0x060034D5 RID: 13525 RVA: 0x0011E0BC File Offset: 0x0011C2BC
	public static ServerPrizeState SpecialRoulettePrizeList
	{
		get
		{
			return ServerInterface.s_specialRoulettePrizeList;
		}
	}

	// Token: 0x17000752 RID: 1874
	// (get) Token: 0x060034D6 RID: 13526 RVA: 0x0011E0C4 File Offset: 0x0011C2C4
	public static ServerPrizeState RaidRoulettePrizeList
	{
		get
		{
			return ServerInterface.s_raidRoulettePrizeList;
		}
	}

	// Token: 0x17000753 RID: 1875
	// (get) Token: 0x060034D7 RID: 13527 RVA: 0x0011E0CC File Offset: 0x0011C2CC
	public static List<ServerMessageEntry> MessageList
	{
		get
		{
			return ServerInterface.s_messageList;
		}
	}

	// Token: 0x17000754 RID: 1876
	// (get) Token: 0x060034D8 RID: 13528 RVA: 0x0011E0D4 File Offset: 0x0011C2D4
	public static List<ServerOperatorMessageEntry> OperatorMessageList
	{
		get
		{
			return ServerInterface.s_operatorMessageList;
		}
	}

	// Token: 0x17000755 RID: 1877
	// (get) Token: 0x060034D9 RID: 13529 RVA: 0x0011E0DC File Offset: 0x0011C2DC
	public static ServerEventState EventState
	{
		get
		{
			return ServerInterface.s_eventState;
		}
	}

	// Token: 0x17000756 RID: 1878
	// (get) Token: 0x060034DA RID: 13530 RVA: 0x0011E0E4 File Offset: 0x0011C2E4
	public static List<ServerEventEntry> EventEntryList
	{
		get
		{
			return ServerInterface.s_eventEntryList;
		}
	}

	// Token: 0x17000757 RID: 1879
	// (get) Token: 0x060034DB RID: 13531 RVA: 0x0011E0EC File Offset: 0x0011C2EC
	public static List<ServerEventReward> EventRewardList
	{
		get
		{
			return ServerInterface.s_eventRewardList;
		}
	}

	// Token: 0x17000758 RID: 1880
	// (get) Token: 0x060034DC RID: 13532 RVA: 0x0011E0F4 File Offset: 0x0011C2F4
	public static List<ServerRedStarItemState> RedStarItemList
	{
		get
		{
			return ServerInterface.s_redStartItemState;
		}
	}

	// Token: 0x17000759 RID: 1881
	// (get) Token: 0x060034DD RID: 13533 RVA: 0x0011E0FC File Offset: 0x0011C2FC
	public static List<ServerRedStarItemState> RedStarExchangeRingItemList
	{
		get
		{
			return ServerInterface.s_redStartExchangeRingItemState;
		}
	}

	// Token: 0x1700075A RID: 1882
	// (get) Token: 0x060034DE RID: 13534 RVA: 0x0011E104 File Offset: 0x0011C304
	public static List<ServerRedStarItemState> RedStarExchangeEnergyItemList
	{
		get
		{
			return ServerInterface.s_redStartExchangeEnergyItemState;
		}
	}

	// Token: 0x1700075B RID: 1883
	// (get) Token: 0x060034DF RID: 13535 RVA: 0x0011E10C File Offset: 0x0011C30C
	public static List<ServerRedStarItemState> RedStarExchangeRaidbossEnergyItemList
	{
		get
		{
			return ServerInterface.s_redStartExchangeRaidbossEnergyItemState;
		}
	}

	// Token: 0x1700075C RID: 1884
	// (get) Token: 0x060034E0 RID: 13536 RVA: 0x0011E114 File Offset: 0x0011C314
	public static ServerDailyChallengeState DailyChallengeState
	{
		get
		{
			return ServerInterface.s_dailyChallengeState;
		}
	}

	// Token: 0x1700075D RID: 1885
	// (get) Token: 0x060034E1 RID: 13537 RVA: 0x0011E11C File Offset: 0x0011C31C
	public static List<ServerUserTransformData> UserTransformDataList
	{
		get
		{
			return ServerInterface.s_userTransformDataList;
		}
	}

	// Token: 0x1700075E RID: 1886
	// (get) Token: 0x060034E2 RID: 13538 RVA: 0x0011E124 File Offset: 0x0011C324
	// (set) Token: 0x060034E3 RID: 13539 RVA: 0x0011E12C File Offset: 0x0011C32C
	public static string MigrationPassword
	{
		get
		{
			return ServerInterface.s_migrationPassword;
		}
		set
		{
			ServerInterface.s_migrationPassword = value;
		}
	}

	// Token: 0x060034E4 RID: 13540 RVA: 0x0011E134 File Offset: 0x0011C334
	private static void Init()
	{
		if (ServerInterface.s_isCreated)
		{
			return;
		}
		ServerInterface.s_settingState = new ServerSettingState();
		ServerInterface.s_loginState = new ServerLoginState();
		ServerInterface.s_nextState = new ServerNextVersionState();
		ServerInterface.s_playerState = new ServerPlayerState();
		ServerInterface.s_freeItemState = new ServerFreeItemState();
		ServerInterface.s_loginBonusData = new ServerLoginBonusData();
		ServerInterface.s_noticeInfo = new ServerNoticeInfo();
		ServerInterface.s_tickerInfo = new ServerTickerInfo();
		ServerInterface.s_wheelOptions = new ServerWheelOptions(null);
		ServerInterface.s_chaoWheelOptions = new ServerChaoWheelOptions();
		ServerInterface.s_ringExchangeList = new List<ServerRingExchangeList>();
		ServerInterface.s_consumedCostList = new List<ServerConsumedCostData>();
		ServerInterface.s_costList = new List<ServerConsumedCostData>();
		ServerInterface.s_mileageMapState = new ServerMileageMapState();
		ServerInterface.s_mileageRewardList = new List<ServerMileageReward>();
		ServerInterface.s_mileageFriendList = new List<ServerMileageFriendEntry>();
		ServerInterface.s_distanceFriendEntry = new List<ServerDistanceFriendEntry>();
		ServerInterface.s_campaignState = new ServerCampaignState();
		ServerInterface.s_leaderboardEntries = new ServerLeaderboardEntries();
		ServerInterface.s_leaderboardEntriesRivalHighScore = new ServerLeaderboardEntries();
		ServerInterface.s_leaderboardEntryRivalHighScoreTop = new ServerLeaderboardEntry();
		ServerInterface.s_premiumRoulettePrizeList = new ServerPrizeState();
		ServerInterface.s_specialRoulettePrizeList = new ServerPrizeState();
		ServerInterface.s_raidRoulettePrizeList = new ServerPrizeState();
		ServerInterface.s_messageList = new List<ServerMessageEntry>();
		ServerInterface.s_operatorMessageList = new List<ServerOperatorMessageEntry>();
		ServerInterface.s_eventState = new ServerEventState();
		ServerInterface.s_eventEntryList = new List<ServerEventEntry>();
		ServerInterface.s_eventRewardList = new List<ServerEventReward>();
		ServerInterface.s_redStartItemState = new List<ServerRedStarItemState>();
		ServerInterface.s_redStartExchangeRingItemState = new List<ServerRedStarItemState>();
		ServerInterface.s_redStartExchangeEnergyItemState = new List<ServerRedStarItemState>();
		ServerInterface.s_redStartExchangeRaidbossEnergyItemState = new List<ServerRedStarItemState>();
		ServerInterface.s_dailyChallengeState = new ServerDailyChallengeState();
		ServerInterface.s_userTransformDataList = new List<ServerUserTransformData>();
		ServerInterface.s_leagueData = new ServerLeagueData();
		ServerInterface.s_migrationPassword = string.Empty;
		ServerInterface.s_isCreated = true;
	}

	// Token: 0x060034E5 RID: 13541 RVA: 0x0011E2C8 File Offset: 0x0011C4C8
	private static void Reset()
	{
		ServerInterface.Init();
	}

	// Token: 0x060034E6 RID: 13542 RVA: 0x0011E2D0 File Offset: 0x0011C4D0
	private void Start()
	{
		if (!ServerInterface.s_isCreated)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			ServerInterface.Init();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060034E7 RID: 13543 RVA: 0x0011E308 File Offset: 0x0011C508
	public void RequestServerLogin(string userId, string password, GameObject callbackObject)
	{
		base.StartCoroutine(ServerLogin.Process(userId, password, callbackObject));
	}

	// Token: 0x060034E8 RID: 13544 RVA: 0x0011E31C File Offset: 0x0011C51C
	public void RequestServerReLogin(GameObject callbackObject)
	{
		base.StartCoroutine(ServerReLogin.Process(callbackObject));
	}

	// Token: 0x060034E9 RID: 13545 RVA: 0x0011E32C File Offset: 0x0011C52C
	public void RequestServerMigration(string migrationID, string migrationPassword, GameObject callbackObject)
	{
		base.StartCoroutine(ServerMigration.Process(migrationID, migrationPassword, callbackObject));
	}

	// Token: 0x060034EA RID: 13546 RVA: 0x0011E340 File Offset: 0x0011C540
	public void RequestServerGetMigrationPassword(string userPassword, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetMigrationPassword.Process(userPassword, callbackObject));
	}

	// Token: 0x060034EB RID: 13547 RVA: 0x0011E350 File Offset: 0x0011C550
	public void RequestServerGetInformation(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetInformation.Process(callbackObject));
	}

	// Token: 0x060034EC RID: 13548 RVA: 0x0011E360 File Offset: 0x0011C560
	public void RequestServerGetVersion(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetVersion.Process(callbackObject));
	}

	// Token: 0x060034ED RID: 13549 RVA: 0x0011E370 File Offset: 0x0011C570
	public void RequestServerGetTicker(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetTicker.Process(callbackObject));
	}

	// Token: 0x060034EE RID: 13550 RVA: 0x0011E380 File Offset: 0x0011C580
	public void RequestServerGetCountry(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetCountry.Process(callbackObject));
	}

	// Token: 0x060034EF RID: 13551 RVA: 0x0011E390 File Offset: 0x0011C590
	public void RequestServerGetVariousParameter(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetVariousParameter.Process(callbackObject));
	}

	// Token: 0x060034F0 RID: 13552 RVA: 0x0011E3A0 File Offset: 0x0011C5A0
	public void RequestServerLoginBonus(GameObject callbackObject)
	{
		base.StartCoroutine(ServerLoginBonus.Process(callbackObject));
	}

	// Token: 0x060034F1 RID: 13553 RVA: 0x0011E3B0 File Offset: 0x0011C5B0
	public void RequestServerLoginBonusSelect(int rewardId, int rewardDays, int rewardSelect, int firstRewardDays, int firstRewardSelect, GameObject callbackObject)
	{
		base.StartCoroutine(ServerLoginBonusSelect.Process(rewardId, rewardDays, rewardSelect, firstRewardDays, firstRewardSelect, callbackObject));
	}

	// Token: 0x060034F2 RID: 13554 RVA: 0x0011E3C8 File Offset: 0x0011C5C8
	public void RequestServerRetrievePlayerState(GameObject callbackObject)
	{
		base.StartCoroutine(ServerRetrievePlayerState.Process(callbackObject));
	}

	// Token: 0x060034F3 RID: 13555 RVA: 0x0011E3D8 File Offset: 0x0011C5D8
	public void RequestServerGetCharacterState(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetCharacterState.Process(callbackObject));
	}

	// Token: 0x060034F4 RID: 13556 RVA: 0x0011E3E8 File Offset: 0x0011C5E8
	public void RequestServerGetChaoState(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetChaoState.Process(callbackObject));
	}

	// Token: 0x060034F5 RID: 13557 RVA: 0x0011E3F8 File Offset: 0x0011C5F8
	public void RequestServerSetUserName(string userName, GameObject callbackObject)
	{
		base.StartCoroutine(ServerSetUserName.Process(userName, callbackObject));
	}

	// Token: 0x060034F6 RID: 13558 RVA: 0x0011E408 File Offset: 0x0011C608
	private void Event_ServerSetTutorialComplete(int tutorialId)
	{
	}

	// Token: 0x060034F7 RID: 13559 RVA: 0x0011E40C File Offset: 0x0011C60C
	public void RequestServerGetWheelOptions(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetWheelOptions.Process(callbackObject));
	}

	// Token: 0x060034F8 RID: 13560 RVA: 0x0011E41C File Offset: 0x0011C61C
	public void RequestServerGetWheelOptionsGeneral(int eventId, int spinId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetWheelOptionsGeneral.Process(eventId, spinId, callbackObject));
	}

	// Token: 0x060034F9 RID: 13561 RVA: 0x0011E430 File Offset: 0x0011C630
	public void RequestServerGetWheelSpinInfo(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetWheelSpinInfo.Process(callbackObject));
	}

	// Token: 0x060034FA RID: 13562 RVA: 0x0011E440 File Offset: 0x0011C640
	public void RequestServerCommitWheelSpin(int count, GameObject callbackObject)
	{
		base.StartCoroutine(ServerCommitWheelSpin.Process(count, callbackObject));
	}

	// Token: 0x060034FB RID: 13563 RVA: 0x0011E450 File Offset: 0x0011C650
	public void RequestServerCommitWheelSpinGeneral(int eventId, int spinId, int spinCostItemId, int spinNum, GameObject callbackObject)
	{
		base.StartCoroutine(ServerCommitWheelSpinGeneral.Process(eventId, spinId, spinCostItemId, spinNum, callbackObject));
	}

	// Token: 0x060034FC RID: 13564 RVA: 0x0011E468 File Offset: 0x0011C668
	public void RequestServerGetDailyBattleStatus(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetDailyBattleStatus.Process(callbackObject));
	}

	// Token: 0x060034FD RID: 13565 RVA: 0x0011E478 File Offset: 0x0011C678
	public void RequestServerUpdateDailyBattleStatus(GameObject callbackObject)
	{
		base.StartCoroutine(ServerUpdateDailyBattleStatus.Process(callbackObject));
	}

	// Token: 0x060034FE RID: 13566 RVA: 0x0011E488 File Offset: 0x0011C688
	public void RequestServerPostDailyBattleResult(GameObject callbackObject)
	{
		base.StartCoroutine(ServerPostDailyBattleResult.Process(callbackObject));
	}

	// Token: 0x060034FF RID: 13567 RVA: 0x0011E498 File Offset: 0x0011C698
	public void RequestServerGetDailyBattleData(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetDailyBattleData.Process(callbackObject));
	}

	// Token: 0x06003500 RID: 13568 RVA: 0x0011E4A8 File Offset: 0x0011C6A8
	public void RequestServerGetPrizeDailyBattle(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetPrizeDailyBattle.Process(callbackObject));
	}

	// Token: 0x06003501 RID: 13569 RVA: 0x0011E4B8 File Offset: 0x0011C6B8
	public void RequestServerGetDailyBattleDataHistory(int count, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetDailyBattleDataHistory.Process(count, callbackObject));
	}

	// Token: 0x06003502 RID: 13570 RVA: 0x0011E4C8 File Offset: 0x0011C6C8
	public void RequestServerResetDailyBattleMatching(int type, GameObject callbackObject)
	{
		base.StartCoroutine(ServerResetDailyBattleMatching.Process(type, callbackObject));
	}

	// Token: 0x06003503 RID: 13571 RVA: 0x0011E4D8 File Offset: 0x0011C6D8
	public void RequestServerStartAct(List<ItemType> modifiersItem, List<BoostItemType> modifiersBoostItem, List<string> distanceFriendIdList, bool tutorial, int? eventId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerStartAct.Process(modifiersItem, modifiersBoostItem, distanceFriendIdList, tutorial, eventId, callbackObject));
	}

	// Token: 0x06003504 RID: 13572 RVA: 0x0011E4F0 File Offset: 0x0011C6F0
	public void RequestServerQuickModeStartAct(List<ItemType> modifiersItem, List<BoostItemType> modifiersBoostItem, bool tutorial, GameObject callbackObject)
	{
		base.StartCoroutine(ServerQuickModeStartAct.Process(modifiersItem, modifiersBoostItem, tutorial, callbackObject));
	}

	// Token: 0x06003505 RID: 13573 RVA: 0x0011E504 File Offset: 0x0011C704
	public void RequestServerActRetry(GameObject callbackObject)
	{
		base.StartCoroutine(ServerActRetry.Process(callbackObject));
	}

	// Token: 0x06003506 RID: 13574 RVA: 0x0011E514 File Offset: 0x0011C714
	public void RequestServerPostGameResults(ServerGameResults results, GameObject callbackObject)
	{
		base.StartCoroutine(ServerPostGameResults.Process(results, callbackObject));
	}

	// Token: 0x06003507 RID: 13575 RVA: 0x0011E524 File Offset: 0x0011C724
	public void RequestServerQuickModePostGameResults(ServerQuickModeGameResults results, GameObject callbackObject)
	{
		base.StartCoroutine(ServerQuickModePostGameResults.Process(results, callbackObject));
	}

	// Token: 0x06003508 RID: 13576 RVA: 0x0011E534 File Offset: 0x0011C734
	public void RequestServerGetMenuData(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetMenuData.Process(callbackObject));
	}

	// Token: 0x06003509 RID: 13577 RVA: 0x0011E544 File Offset: 0x0011C744
	public void RequestServerGetMileageReward(int episode, int chapter, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetMileageReward.Process(episode, chapter, callbackObject));
	}

	// Token: 0x0600350A RID: 13578 RVA: 0x0011E558 File Offset: 0x0011C758
	public void RequestServerGetCostList(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetCostList.Process(callbackObject));
	}

	// Token: 0x0600350B RID: 13579 RVA: 0x0011E568 File Offset: 0x0011C768
	public void RequestServerActRetryFree(GameObject callbackObject)
	{
		base.StartCoroutine(ServerActRetryFree.Process(callbackObject));
	}

	// Token: 0x0600350C RID: 13580 RVA: 0x0011E578 File Offset: 0x0011C778
	public void RequestServerGetFreeItemList(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetFreeItemList.Process(callbackObject));
	}

	// Token: 0x0600350D RID: 13581 RVA: 0x0011E588 File Offset: 0x0011C788
	public void RequestServerGetCampaignList(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetCampaignList.Process(callbackObject));
	}

	// Token: 0x0600350E RID: 13582 RVA: 0x0011E598 File Offset: 0x0011C798
	public void RequestServerGetMileageData(string[] distanceFriendList, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetMileageData.Process(distanceFriendList, callbackObject));
	}

	// Token: 0x0600350F RID: 13583 RVA: 0x0011E5A8 File Offset: 0x0011C7A8
	public void RequestServerGetDailyMissionData(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetDailyMissionData.Process(callbackObject));
	}

	// Token: 0x06003510 RID: 13584 RVA: 0x0011E5B8 File Offset: 0x0011C7B8
	public void RequestServerGetRingItemList(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetRingItemList.Process(callbackObject));
	}

	// Token: 0x06003511 RID: 13585 RVA: 0x0011E5C8 File Offset: 0x0011C7C8
	public void RequestServerUpgradeCharacter(int characterId, int abilityId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerUpgradeCharacter.Process(characterId, abilityId, callbackObject));
	}

	// Token: 0x06003512 RID: 13586 RVA: 0x0011E5DC File Offset: 0x0011C7DC
	public void RequestServerUnlockedCharacter(CharaType charaType, ServerItem item, GameObject callbackObject)
	{
		base.StartCoroutine(ServerUnlockedCharacter.Process(charaType, item, callbackObject));
	}

	// Token: 0x06003513 RID: 13587 RVA: 0x0011E5F0 File Offset: 0x0011C7F0
	public void RequestServerChangeCharacter(int mainCharaId, int subCharaId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerChangeCharacter.Process(mainCharaId, subCharaId, callbackObject));
	}

	// Token: 0x06003514 RID: 13588 RVA: 0x0011E604 File Offset: 0x0011C804
	public void RequestServerUseSubCharacter(bool useFlag, GameObject callbackObject)
	{
		base.StartCoroutine(ServerUseSubCharacter.Process(useFlag, callbackObject));
	}

	// Token: 0x06003515 RID: 13589 RVA: 0x0011E614 File Offset: 0x0011C814
	public void RequestServerGetLeaderboardEntries(int mode, int first, int count, int index, int rankingType, int eventId, string[] friendIdList, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetLeaderboardEntries.Process(mode, first, count, index, rankingType, eventId, friendIdList, callbackObject));
	}

	// Token: 0x06003516 RID: 13590 RVA: 0x0011E63C File Offset: 0x0011C83C
	public void RequestServerGetWeeklyLeaderboardOptions(int mode, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetWeeklyLeaderboardOptions.Process(mode, callbackObject));
	}

	// Token: 0x06003517 RID: 13591 RVA: 0x0011E64C File Offset: 0x0011C84C
	public void RequestServerGetLeagueData(int mode, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetLeagueData.Process(mode, callbackObject));
	}

	// Token: 0x06003518 RID: 13592 RVA: 0x0011E65C File Offset: 0x0011C85C
	public void RequestServerGetLeagueOperatorData(int mode, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetLeagueOperatorData.Process(mode, callbackObject));
	}

	// Token: 0x06003519 RID: 13593 RVA: 0x0011E66C File Offset: 0x0011C86C
	private void Event_ServerGetFriendsList(int first, int count)
	{
	}

	// Token: 0x0600351A RID: 13594 RVA: 0x0011E670 File Offset: 0x0011C870
	private void Event_ServerGetGameFriendsList(int first, int count)
	{
	}

	// Token: 0x0600351B RID: 13595 RVA: 0x0011E674 File Offset: 0x0011C874
	public void RequestServerRequestEnergy(string friendId, GameObject gameObject)
	{
		base.StartCoroutine(ServerRequestEnergy.Process(friendId, gameObject));
	}

	// Token: 0x0600351C RID: 13596 RVA: 0x0011E684 File Offset: 0x0011C884
	public void RequestServerGetFacebookIncentive(int incentiveType, int achievementCount, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetFacebookIncentive.Process(incentiveType, achievementCount, callbackObject));
	}

	// Token: 0x0600351D RID: 13597 RVA: 0x0011E698 File Offset: 0x0011C898
	public void RequestServerSetFacebookScopedId(string userId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerSetFacebookScopedId.Process(userId, callbackObject));
	}

	// Token: 0x0600351E RID: 13598 RVA: 0x0011E6A8 File Offset: 0x0011C8A8
	public void RequestServerGetFriendUserIdList(List<string> friendFBIdList, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetFriendUserIdList.Process(friendFBIdList, callbackObject));
	}

	// Token: 0x0600351F RID: 13599 RVA: 0x0011E6B8 File Offset: 0x0011C8B8
	public void RequestServerSetInviteHistory(string facebookIdHash, GameObject callbackObject)
	{
		base.StartCoroutine(ServerSetInviteHistory.Process(facebookIdHash, callbackObject));
	}

	// Token: 0x06003520 RID: 13600 RVA: 0x0011E6C8 File Offset: 0x0011C8C8
	public void RequestServerSetInviteCode(string friendId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerSetInviteCode.Process(friendId, callbackObject));
	}

	// Token: 0x06003521 RID: 13601 RVA: 0x0011E6D8 File Offset: 0x0011C8D8
	private void Event_ServerSendInvite(string friendId)
	{
	}

	// Token: 0x06003522 RID: 13602 RVA: 0x0011E6DC File Offset: 0x0011C8DC
	public void RequestServerSendEnergy(string friendId, GameObject gameObject)
	{
		base.StartCoroutine(ServerSendEnergy.Process(friendId, gameObject));
	}

	// Token: 0x06003523 RID: 13603 RVA: 0x0011E6EC File Offset: 0x0011C8EC
	public void RequestServerUpdateMessage(List<int> messageIdList, List<int> operatorMessageIdList, GameObject callbackObject)
	{
		base.StartCoroutine(ServerUpdateMessage.Process(messageIdList, operatorMessageIdList, callbackObject));
	}

	// Token: 0x06003524 RID: 13604 RVA: 0x0011E700 File Offset: 0x0011C900
	public void RequestServerGetMessageList(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetMessageList.Process(callbackObject));
	}

	// Token: 0x06003525 RID: 13605 RVA: 0x0011E710 File Offset: 0x0011C910
	public void RequestServerPreparePurchase(int itemId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerPreparePurchase.Process(itemId, callbackObject));
	}

	// Token: 0x06003526 RID: 13606 RVA: 0x0011E720 File Offset: 0x0011C920
	public void RequestServerPurchase(bool isSuccess, GameObject callbackObject)
	{
		base.StartCoroutine(ServerPurchase.Process(isSuccess, callbackObject));
	}

	// Token: 0x06003527 RID: 13607 RVA: 0x0011E730 File Offset: 0x0011C930
	public void RequestServerGetRedStarExchangeList(int itemType, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetRedStarExchangeList.Process(itemType, callbackObject));
	}

	// Token: 0x06003528 RID: 13608 RVA: 0x0011E740 File Offset: 0x0011C940
	public void RequestServerRedStarExchange(int storeItemId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerRedStarExchange.Process(storeItemId, callbackObject));
	}

	// Token: 0x06003529 RID: 13609 RVA: 0x0011E750 File Offset: 0x0011C950
	public void RequestServerBuyIos(string receiptData, GameObject callbackObject)
	{
		base.StartCoroutine(ServerBuyIos.Process(receiptData, callbackObject));
	}

	// Token: 0x0600352A RID: 13610 RVA: 0x0011E760 File Offset: 0x0011C960
	public void RequestServerBuyAndroid(string receiptData, string signature, GameObject callbackObject)
	{
		base.StartCoroutine(ServerBuyAndroid.Process(receiptData, signature, callbackObject));
	}

	// Token: 0x0600352B RID: 13611 RVA: 0x0011E774 File Offset: 0x0011C974
	public void RequestServerGetRingExchangeList(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetRingExchangeList.Process(callbackObject));
	}

	// Token: 0x0600352C RID: 13612 RVA: 0x0011E784 File Offset: 0x0011C984
	public void RequestServerSetBirthday(string birthday, GameObject callbackObject)
	{
		base.StartCoroutine(ServerSetBirthday.Process(birthday, callbackObject));
	}

	// Token: 0x0600352D RID: 13613 RVA: 0x0011E794 File Offset: 0x0011C994
	public void RequestServerRingExchange(int itemId, int itemNum, GameObject callbackObject)
	{
		base.StartCoroutine(ServerRingExchange.Process(itemId, itemNum, callbackObject));
	}

	// Token: 0x0600352E RID: 13614 RVA: 0x0011E7A8 File Offset: 0x0011C9A8
	public void RequestServerGetItemStockNum(int eventId, List<int> itemId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetItemStockNum.Process(eventId, itemId, callbackObject));
	}

	// Token: 0x0600352F RID: 13615 RVA: 0x0011E7BC File Offset: 0x0011C9BC
	public void RequestServerGetItemStockNum(int eventId, int itemId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetItemStockNum.Process(eventId, new List<int>
		{
			itemId
		}, callbackObject));
	}

	// Token: 0x06003530 RID: 13616 RVA: 0x0011E7E8 File Offset: 0x0011C9E8
	public void RequestServerGetItemStockNum(int eventId, ServerItem.Id itemId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetItemStockNum.Process(eventId, new List<int>
		{
			(int)itemId
		}, callbackObject));
	}

	// Token: 0x06003531 RID: 13617 RVA: 0x0011E814 File Offset: 0x0011CA14
	public void RequestServerGetChaoWheelOptions(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetChaoWheelOptions.Process(callbackObject));
	}

	// Token: 0x06003532 RID: 13618 RVA: 0x0011E824 File Offset: 0x0011CA24
	public void RequestServerGetPrizeChaoWheelSpin(int chaoWheelSpinType, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetPrizeChaoWheelSpin.Process(chaoWheelSpinType, callbackObject));
	}

	// Token: 0x06003533 RID: 13619 RVA: 0x0011E834 File Offset: 0x0011CA34
	public void RequestServerGetPrizeWheelSpinGeneral(int eventId, int spinType, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetPrizeWheelSpinGeneral.Process(eventId, spinType, callbackObject));
	}

	// Token: 0x06003534 RID: 13620 RVA: 0x0011E848 File Offset: 0x0011CA48
	public void RequestServerCommitChaoWheelSpin(int count, GameObject callbackObject)
	{
		base.StartCoroutine(ServerCommitChaoWheelSpin.Process(count, callbackObject));
	}

	// Token: 0x06003535 RID: 13621 RVA: 0x0011E858 File Offset: 0x0011CA58
	public void RequestServerGetChaoRentalStates(string[] frindId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetChaoRentalStates.Process(frindId, callbackObject));
	}

	// Token: 0x06003536 RID: 13622 RVA: 0x0011E868 File Offset: 0x0011CA68
	public void RequestServerEquipChao(int mainChaoId, int subChaoId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerEquipChao.Process(mainChaoId, subChaoId, callbackObject));
	}

	// Token: 0x06003537 RID: 13623 RVA: 0x0011E87C File Offset: 0x0011CA7C
	public void RequestServerGetFirstLaunchChao(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetFirstLaunchChao.Process(callbackObject));
	}

	// Token: 0x06003538 RID: 13624 RVA: 0x0011E88C File Offset: 0x0011CA8C
	public void RequestServerAddSpecialEgg(int numSpecialEgg, GameObject callbackObject)
	{
		base.StartCoroutine(ServerAddSpecialEgg.Process(numSpecialEgg, callbackObject));
	}

	// Token: 0x06003539 RID: 13625 RVA: 0x0011E89C File Offset: 0x0011CA9C
	public void RequestServerEquipItem(List<ItemType> items, GameObject callbackObject)
	{
		base.StartCoroutine(ServerEquipItem.Process(items, callbackObject));
	}

	// Token: 0x0600353A RID: 13626 RVA: 0x0011E8AC File Offset: 0x0011CAAC
	public void RequestServerOptionUserResult(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetOptionUserResult.Process(callbackObject));
	}

	// Token: 0x0600353B RID: 13627 RVA: 0x0011E8BC File Offset: 0x0011CABC
	public void RequestServerAtomSerial(string campaignId, string serial, bool new_user, GameObject callbackObject)
	{
		base.StartCoroutine(ServerAtomSerial.Process(campaignId, serial, new_user, callbackObject));
	}

	// Token: 0x0600353C RID: 13628 RVA: 0x0011E8D0 File Offset: 0x0011CAD0
	public void RequestServerSendApollo(int type, string[] value, GameObject callbackObject)
	{
		base.StartCoroutine(ServerSendApollo.Process(type, value, callbackObject));
	}

	// Token: 0x0600353D RID: 13629 RVA: 0x0011E8E4 File Offset: 0x0011CAE4
	public void RequestServerSetNoahId(string noahId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerSetNoahId.Process(noahId, callbackObject));
	}

	// Token: 0x0600353E RID: 13630 RVA: 0x0011E8F4 File Offset: 0x0011CAF4
	public void RequestServerGetEventList(GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetEventList.Process(callbackObject));
	}

	// Token: 0x0600353F RID: 13631 RVA: 0x0011E904 File Offset: 0x0011CB04
	public void RequestServerGetEventReward(int eventId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetEventReward.Process(eventId, callbackObject));
	}

	// Token: 0x06003540 RID: 13632 RVA: 0x0011E914 File Offset: 0x0011CB14
	public void RequestServerEventStartAct(int eventId, int energyExpend, long raidBossId, List<ItemType> modifiersItem, List<BoostItemType> modifiersBoostItem, GameObject callbackObject)
	{
		base.StartCoroutine(ServerEventStartAct.Process(eventId, energyExpend, raidBossId, modifiersItem, modifiersBoostItem, callbackObject));
	}

	// Token: 0x06003541 RID: 13633 RVA: 0x0011E92C File Offset: 0x0011CB2C
	public void RequestServerEventUpdateGameResults(ServerEventGameResults results, GameObject callbackObject)
	{
		base.StartCoroutine(ServerEventUpdateGameResults.Process(results, callbackObject));
	}

	// Token: 0x06003542 RID: 13634 RVA: 0x0011E93C File Offset: 0x0011CB3C
	public void RequestServerEventPostGameResults(int eventId, int numRaidBossRings, GameObject callbackObject)
	{
		base.StartCoroutine(ServerEventPostGameResults.Process(eventId, numRaidBossRings, callbackObject));
	}

	// Token: 0x06003543 RID: 13635 RVA: 0x0011E950 File Offset: 0x0011CB50
	public void RequestServerGetEventState(int eventId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetEventState.Process(eventId, callbackObject));
	}

	// Token: 0x06003544 RID: 13636 RVA: 0x0011E960 File Offset: 0x0011CB60
	public void RequestServerGetEventUserRaidBossList(int eventId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetEventUserRaidBossList.Process(eventId, callbackObject));
	}

	// Token: 0x06003545 RID: 13637 RVA: 0x0011E970 File Offset: 0x0011CB70
	public void RequestServerGetEventUserRaidBossState(int eventId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetEventUserRaidBossState.Process(eventId, callbackObject));
	}

	// Token: 0x06003546 RID: 13638 RVA: 0x0011E980 File Offset: 0x0011CB80
	public void RequestServerGetEventRaidBossUserList(int eventId, long raidBossId, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetEventRaidBossUserList.Process(eventId, raidBossId, callbackObject));
	}

	// Token: 0x06003547 RID: 13639 RVA: 0x0011E994 File Offset: 0x0011CB94
	public void RequestServerGetEventRaidBossDesiredList(int eventId, long raidBossId, List<string> friendIdList, GameObject callbackObject)
	{
		base.StartCoroutine(ServerGetEventRaidBossDesiredList.Process(eventId, raidBossId, friendIdList, callbackObject));
	}

	// Token: 0x06003548 RID: 13640 RVA: 0x0011E9A8 File Offset: 0x0011CBA8
	public void RequestServerDrawRaidBoss(int eventId, long score, GameObject callbackObject)
	{
		base.StartCoroutine(ServerDrawRaidBoss.Process(eventId, score, callbackObject));
	}

	// Token: 0x06003549 RID: 13641 RVA: 0x0011E9BC File Offset: 0x0011CBBC
	private void Event_SPLoginUpgrade_Success()
	{
		ServerInterface.Reset();
	}

	// Token: 0x1700075F RID: 1887
	// (get) Token: 0x0600354A RID: 13642 RVA: 0x0011E9C4 File Offset: 0x0011CBC4
	public static ServerInterface LoggedInServerInterface
	{
		get
		{
			return (ServerInterface.LoginState == null || !ServerInterface.LoginState.IsLoggedIn) ? null : GameObjectUtil.FindGameObjectComponent<ServerInterface>("ServerInterface");
		}
	}

	// Token: 0x0600354B RID: 13643 RVA: 0x0011E9F0 File Offset: 0x0011CBF0
	public static bool IsRSREnable()
	{
		return ServerInterface.s_redStartItemState != null && ServerInterface.s_redStartItemState.Count > 0;
	}

	// Token: 0x0600354C RID: 13644 RVA: 0x0011EA0C File Offset: 0x0011CC0C
	public static void DebugInit()
	{
		ServerInterface.Init();
	}

	// Token: 0x04002C64 RID: 11364
	public ServerLeaderboardEntry m_myLeaderboardEntry;

	// Token: 0x04002C65 RID: 11365
	private static ServerSettingState s_settingState;

	// Token: 0x04002C66 RID: 11366
	private static ServerLoginState s_loginState;

	// Token: 0x04002C67 RID: 11367
	private static ServerNextVersionState s_nextState;

	// Token: 0x04002C68 RID: 11368
	private static ServerPlayerState s_playerState;

	// Token: 0x04002C69 RID: 11369
	private static ServerFreeItemState s_freeItemState;

	// Token: 0x04002C6A RID: 11370
	private static ServerLoginBonusData s_loginBonusData;

	// Token: 0x04002C6B RID: 11371
	private static ServerNoticeInfo s_noticeInfo;

	// Token: 0x04002C6C RID: 11372
	private static ServerTickerInfo s_tickerInfo;

	// Token: 0x04002C6D RID: 11373
	private static ServerWheelOptions s_wheelOptions;

	// Token: 0x04002C6E RID: 11374
	private static ServerChaoWheelOptions s_chaoWheelOptions;

	// Token: 0x04002C6F RID: 11375
	private static List<ServerRingExchangeList> s_ringExchangeList;

	// Token: 0x04002C70 RID: 11376
	private static List<ServerConsumedCostData> s_consumedCostList;

	// Token: 0x04002C71 RID: 11377
	private static List<ServerConsumedCostData> s_costList;

	// Token: 0x04002C72 RID: 11378
	private static ServerMileageMapState s_mileageMapState;

	// Token: 0x04002C73 RID: 11379
	private static List<ServerMileageReward> s_mileageRewardList;

	// Token: 0x04002C74 RID: 11380
	private static List<ServerMileageFriendEntry> s_mileageFriendList;

	// Token: 0x04002C75 RID: 11381
	private static List<ServerDistanceFriendEntry> s_distanceFriendEntry;

	// Token: 0x04002C76 RID: 11382
	private static ServerCampaignState s_campaignState;

	// Token: 0x04002C77 RID: 11383
	private static List<ServerMessageEntry> s_messageList;

	// Token: 0x04002C78 RID: 11384
	private static List<ServerOperatorMessageEntry> s_operatorMessageList;

	// Token: 0x04002C79 RID: 11385
	private static ServerLeaderboardEntries s_leaderboardEntries;

	// Token: 0x04002C7A RID: 11386
	private static ServerLeaderboardEntries s_leaderboardEntriesRivalHighScore;

	// Token: 0x04002C7B RID: 11387
	private static ServerLeaderboardEntry s_leaderboardEntryRivalHighScoreTop;

	// Token: 0x04002C7C RID: 11388
	private static ServerPrizeState s_premiumRoulettePrizeList;

	// Token: 0x04002C7D RID: 11389
	private static ServerPrizeState s_specialRoulettePrizeList;

	// Token: 0x04002C7E RID: 11390
	private static ServerPrizeState s_raidRoulettePrizeList;

	// Token: 0x04002C7F RID: 11391
	private static ServerEventState s_eventState;

	// Token: 0x04002C80 RID: 11392
	private static List<ServerEventEntry> s_eventEntryList;

	// Token: 0x04002C81 RID: 11393
	private static List<ServerEventReward> s_eventRewardList;

	// Token: 0x04002C82 RID: 11394
	private static List<ServerRedStarItemState> s_redStartItemState;

	// Token: 0x04002C83 RID: 11395
	private static List<ServerRedStarItemState> s_redStartExchangeRingItemState;

	// Token: 0x04002C84 RID: 11396
	private static List<ServerRedStarItemState> s_redStartExchangeEnergyItemState;

	// Token: 0x04002C85 RID: 11397
	private static List<ServerRedStarItemState> s_redStartExchangeRaidbossEnergyItemState;

	// Token: 0x04002C86 RID: 11398
	private static ServerDailyChallengeState s_dailyChallengeState;

	// Token: 0x04002C87 RID: 11399
	private static List<ServerUserTransformData> s_userTransformDataList;

	// Token: 0x04002C88 RID: 11400
	private static string s_migrationPassword;

	// Token: 0x04002C89 RID: 11401
	private static ServerLeagueData s_leagueData;

	// Token: 0x04002C8A RID: 11402
	private static bool s_isCreated;

	// Token: 0x020007D1 RID: 2001
	public enum StatusCode
	{
		// Token: 0x04002C8C RID: 11404
		Ok,
		// Token: 0x04002C8D RID: 11405
		ServerSecurityError = -19001,
		// Token: 0x04002C8E RID: 11406
		VersionDifference = -19002,
		// Token: 0x04002C8F RID: 11407
		DecryptionFailure = -19003,
		// Token: 0x04002C90 RID: 11408
		ParamHashDifference = -19004,
		// Token: 0x04002C91 RID: 11409
		ServerNextVersion = -19990,
		// Token: 0x04002C92 RID: 11410
		ServerMaintenance = -19997,
		// Token: 0x04002C93 RID: 11411
		ServerBusyError = -19998,
		// Token: 0x04002C94 RID: 11412
		ServerSystemError = -19999,
		// Token: 0x04002C95 RID: 11413
		RequestParamError = -10100,
		// Token: 0x04002C96 RID: 11414
		NotAvailablePlayer = -10101,
		// Token: 0x04002C97 RID: 11415
		MissingPlayer = -10102,
		// Token: 0x04002C98 RID: 11416
		ExpirationSession = -10103,
		// Token: 0x04002C99 RID: 11417
		PassWordError = -10104,
		// Token: 0x04002C9A RID: 11418
		InvalidSerialCode = -10105,
		// Token: 0x04002C9B RID: 11419
		UsedSerialCode = -10106,
		// Token: 0x04002C9C RID: 11420
		HspWebApiError = -10110,
		// Token: 0x04002C9D RID: 11421
		ApolloWebApiError = -10115,
		// Token: 0x04002C9E RID: 11422
		DataMismatch = -30120,
		// Token: 0x04002C9F RID: 11423
		MasterDataMismatch = -10121,
		// Token: 0x04002CA0 RID: 11424
		NotEnoughRedStarRings = -20130,
		// Token: 0x04002CA1 RID: 11425
		NotEnoughRings = -20131,
		// Token: 0x04002CA2 RID: 11426
		NotEnoughEnergy = -20132,
		// Token: 0x04002CA3 RID: 11427
		RouletteUseLimit = -30401,
		// Token: 0x04002CA4 RID: 11428
		RouletteBoardReset = -30411,
		// Token: 0x04002CA5 RID: 11429
		CharacterLevelLimit = -20601,
		// Token: 0x04002CA6 RID: 11430
		AllChaoLevelLimit = -20602,
		// Token: 0x04002CA7 RID: 11431
		AlreadyInvitedFriend = -30801,
		// Token: 0x04002CA8 RID: 11432
		AlreadyRequestedEnergy = -30901,
		// Token: 0x04002CA9 RID: 11433
		AlreadySentEnergy = -30902,
		// Token: 0x04002CAA RID: 11434
		ReceiveFailureMessage = -30910,
		// Token: 0x04002CAB RID: 11435
		AlreadyExistedPrePurchase = -11001,
		// Token: 0x04002CAC RID: 11436
		AlreadyRemovedPrePurchase = -11002,
		// Token: 0x04002CAD RID: 11437
		InvalidReceiptData = -11003,
		// Token: 0x04002CAE RID: 11438
		AlreadyProcessedReceipt = -11004,
		// Token: 0x04002CAF RID: 11439
		EnergyLimitPurchaseTrigger = -21010,
		// Token: 0x04002CB0 RID: 11440
		NotStartEvent = -10201,
		// Token: 0x04002CB1 RID: 11441
		AlreadyEndEvent = -10202,
		// Token: 0x04002CB2 RID: 11442
		VersionForApplication = -999002,
		// Token: 0x04002CB3 RID: 11443
		TimeOut = -7,
		// Token: 0x04002CB4 RID: 11444
		OtherError = -8,
		// Token: 0x04002CB5 RID: 11445
		NotReachability = -10,
		// Token: 0x04002CB6 RID: 11446
		InvalidResponse = -20,
		// Token: 0x04002CB7 RID: 11447
		CliendError = -400,
		// Token: 0x04002CB8 RID: 11448
		InternalServerError = -500,
		// Token: 0x04002CB9 RID: 11449
		HspPurchaseError = -600,
		// Token: 0x04002CBA RID: 11450
		ServerBusy = -700
	}
}
