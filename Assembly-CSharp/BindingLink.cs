using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

// Token: 0x02000111 RID: 273
public class BindingLink : IDisposable
{
	// Token: 0x060007F7 RID: 2039
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern void CreateMD5String([MarshalAs(UnmanagedType.LPStr)] StringBuilder result, string loginKey, string userId, string password, int randomNum);

	// Token: 0x060007F8 RID: 2040
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl)]
	private static extern bool IsEnableEncrypto();

	// Token: 0x060007F9 RID: 2041
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl)]
	private static extern void ResetResultScore();

	// Token: 0x060007FA RID: 2042
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl)]
	private static extern NativeScoreResult GetResultScore();

	// Token: 0x060007FB RID: 2043
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl)]
	private static extern void CheckHalfWayResultScore(PostGameResultNativeParam param, [In] [Out] StageScoreData[] scoreDatas, int dataSize);

	// Token: 0x060007FC RID: 2044
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl)]
	private static extern void CheckHalfWayQuickModeResultScore(QuickModePostGameResultNativeParam param, [In] [Out] StageScoreData[] scoreDatas, int dataSize);

	// Token: 0x060007FD RID: 2045
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl)]
	private static extern void CheckQuickModeResultTimer(QuickModeTimerNativeParam param);

	// Token: 0x060007FE RID: 2046
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcResponseString(string responseText, string encryptInitVector);

	// Token: 0x060007FF RID: 2047
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl)]
	private static extern void GetSendString([MarshalAs(UnmanagedType.LPStr)] StringBuilder result, int stringSize);

	// Token: 0x06000800 RID: 2048
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern void GetResponseString([MarshalAs(UnmanagedType.LPStr)] StringBuilder result, string responseText, string encryptInitVector);

	// Token: 0x06000801 RID: 2049
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcOnlySendBaseparamString([In] SendBaseNativeParam baseInfo, string encryptInitVector);

	// Token: 0x06000802 RID: 2050
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcLoginString([In] SendBaseNativeParam baseInfo, string userId, string password, string migrationPassword, int platform, string device, int language, int salesLocale, int storeId, int platformSns, string encryptInitVector);

	// Token: 0x06000803 RID: 2051
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcMigrationString([In] SendBaseNativeParam baseInfo, string userId, string migrationPassword, string migrationUserPassword, int platform, string device, int language, int salesLocale, int storeId, int platformSns, string encryptInitVector);

	// Token: 0x06000804 RID: 2052
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcGetMigrationPasswordString([In] SendBaseNativeParam baseInfo, string userPassword, string encryptInitVector);

	// Token: 0x06000805 RID: 2053
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcSetUserNameString([In] SendBaseNativeParam baseInfo, string userName, string encryptInitVector);

	// Token: 0x06000806 RID: 2054
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcActStartString([In] SendBaseNativeParam baseInfo, [In] ActStartNativeParam actStartParam, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeConst = 50)] string[] distanceFriendList, string encryptInitVector);

	// Token: 0x06000807 RID: 2055
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcQuickModeActStartString([In] SendBaseNativeParam baseInfo, [In] QuickModeActStartNativeParam actStartParam, string encryptInitVector);

	// Token: 0x06000808 RID: 2056
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcActRetryString([In] SendBaseNativeParam baseInfo, string encryptInitVector);

	// Token: 0x06000809 RID: 2057
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcPostGameResultString([In] SendBaseNativeParam baseInfo, [In] PostGameResultNativeParam param, [In] [Out] StageScoreData[] scoreDatas, int dataSize, string encryptInitVector);

	// Token: 0x0600080A RID: 2058
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcQuickModePostGameResultString([In] SendBaseNativeParam baseInfo, [In] QuickModePostGameResultNativeParam param, [In] [Out] StageScoreData[] scoreDatas, int dataSize, string encryptInitVector);

	// Token: 0x0600080B RID: 2059
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcGetMileageRewardString([In] SendBaseNativeParam baseInfo, int episode, int chapter, string encryptInitVector);

	// Token: 0x0600080C RID: 2060
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcUpgradeCharacterString([In] SendBaseNativeParam baseInfo, int characterId, int abilityId, string encryptInitVector);

	// Token: 0x0600080D RID: 2061
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcUnlockedCharacterString([In] SendBaseNativeParam baseInfo, int characterId, int itemId, string encryptInitVector);

	// Token: 0x0600080E RID: 2062
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcChangeCharacterString([In] SendBaseNativeParam baseInfo, int mainCharacterId, int subCharacterId, string encryptInitVector);

	// Token: 0x0600080F RID: 2063
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcGetWeeklyLeaderboardEntryString([In] SendBaseNativeParam baseInfo, LeaderboardEntryNativeParam leaderboardEntryParam, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeConst = 50)] string[] friendIdList, string encryptInitVector);

	// Token: 0x06000810 RID: 2064
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcLeagueOperatorDataString([In] SendBaseNativeParam baseInfo, int mode, int league, string encryptInitVector);

	// Token: 0x06000811 RID: 2065
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcLeagueDataString([In] SendBaseNativeParam baseInfo, int mode, string encryptInitVector);

	// Token: 0x06000812 RID: 2066
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcWeeklyLeaderboardOptionsString([In] SendBaseNativeParam baseInfo, int mode, string encryptInitVector);

	// Token: 0x06000813 RID: 2067
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcInviteCodeString([In] SendBaseNativeParam baseInfo, string friendId, string encryptInitVector);

	// Token: 0x06000814 RID: 2068
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcGetFacebookIncentiveString([In] SendBaseNativeParam baseInfo, int type, int achievementCount, string encryptInitVector);

	// Token: 0x06000815 RID: 2069
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcSetFacebookScopedIdString([In] SendBaseNativeParam baseInfo, string facebookId, string cryptoInitVector);

	// Token: 0x06000816 RID: 2070
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcGetFriendUserIdListString([In] SendBaseNativeParam baseInfo, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeConst = 50)] string[] friendIdList, string encryptInitVector);

	// Token: 0x06000817 RID: 2071
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcSendEnergyString([In] SendBaseNativeParam baseInfo, string friendId, string encryptInitVector);

	// Token: 0x06000818 RID: 2072
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcGetMessageString([In] SendBaseNativeParam baseInfo, [In] [Out] int[] messageIdList, int messageIdSize, [In] [Out] int[] operationMessageIdList, int operationMessageIdSize, string encryptInitVector);

	// Token: 0x06000819 RID: 2073
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcGetRedStarExchangeListString([In] SendBaseNativeParam baseInfo, int itemType, string encryptInitVector);

	// Token: 0x0600081A RID: 2074
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcRedStarExchangeString([In] SendBaseNativeParam baseInfo, int itemId, string encryptInitVector);

	// Token: 0x0600081B RID: 2075
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcBuyIosString([In] SendBaseNativeParam baseInfo, string receiptData, string encryptInitVector);

	// Token: 0x0600081C RID: 2076
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcBuyAndroidString([In] SendBaseNativeParam baseInfo, string receiptData, string receiptSignature, string encryptInitVector);

	// Token: 0x0600081D RID: 2077
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcSetBirthdayString([In] SendBaseNativeParam baseInfo, string birthday, string encryptInitVector);

	// Token: 0x0600081E RID: 2078
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcCommitWheelSpinString([In] SendBaseNativeParam baseInfo, int count, string encryptInitVector);

	// Token: 0x0600081F RID: 2079
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcCommitChaoWheelSpinString([In] SendBaseNativeParam baseInfo, int count, string encryptInitVector);

	// Token: 0x06000820 RID: 2080
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcEquipChaoString([In] SendBaseNativeParam baseInfo, int mainChaoId, int subChaoId, string encryptInitVector);

	// Token: 0x06000821 RID: 2081
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcAddSpecialEggString([In] SendBaseNativeParam baseInfo, int numSpecialEgg, string encryptInitVector);

	// Token: 0x06000822 RID: 2082
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcGetPrizeChaoWheelSpinString([In] SendBaseNativeParam baseInfo, int chaoWheelSpinType, string encryptInitVector);

	// Token: 0x06000823 RID: 2083
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcGetWheelSpinGeneralString([In] SendBaseNativeParam baseInfo, int eventId, int spinId, string encryptInitVector);

	// Token: 0x06000824 RID: 2084
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcCommitWheelSpinGeneralString([In] SendBaseNativeParam baseInfo, int eventId, int spinCostItemId, int spinId, int spinNum, string encryptInitVector);

	// Token: 0x06000825 RID: 2085
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcGetPrizeWheelSpinGeneralString([In] SendBaseNativeParam baseInfo, int eventId, int spinType, string encryptInitVector);

	// Token: 0x06000826 RID: 2086
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcSendAppoloString([In] SendBaseNativeParam baseInfo, int type, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeConst = 100)] string[] value, int paramSize, string encryptInitVector);

	// Token: 0x06000827 RID: 2087
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcSetSerialCodeString([In] SendBaseNativeParam baseInfo, string campaignId, string serialCode, bool newUser, string encryptInitVector);

	// Token: 0x06000828 RID: 2088
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcSetNoahIdString([In] SendBaseNativeParam baseInfo, string noahId, string encryptInitVector);

	// Token: 0x06000829 RID: 2089
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcGetEventRewardString([In] SendBaseNativeParam baseInfo, int eventId, string encryptInitVector);

	// Token: 0x0600082A RID: 2090
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcEventStateString([In] SendBaseNativeParam baseInfo, int eventId, string encryptInitVector);

	// Token: 0x0600082B RID: 2091
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	private static extern int CalcGetItemStockNum([In] SendBaseNativeParam baseInfo, int eventId, [In] [Out] int[] itemIdList, int listSize, string encryptInitVector);

	// Token: 0x0600082C RID: 2092
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl)]
	private static extern void BootGameAction();

	// Token: 0x0600082D RID: 2093
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl)]
	private static extern void BeforeGameAction();

	// Token: 0x0600082E RID: 2094
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl)]
	private static extern int CalcGetDailyBattleDataHistoryString([In] SendBaseNativeParam baseInfo, int count, string encryptInitVector);

	// Token: 0x0600082F RID: 2095
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl)]
	private static extern int CalcResetDailyBattleMatchingString([In] SendBaseNativeParam baseInfo, int type, string encryptInitVector);

	// Token: 0x06000830 RID: 2096
	[DllImport("UnmanagedProcess", CallingConvention = CallingConvention.Cdecl)]
	private static extern int CalcLoginBonusSelectString([In] SendBaseNativeParam baseInfo, int rewardId, int rewardDays, int rewardSelect, int firstRewardDays, int firstRewardSelect, string encryptInitVector);

	// Token: 0x06000831 RID: 2097 RVA: 0x0002F2D4 File Offset: 0x0002D4D4
	public void Dispose()
	{
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x0002F2D8 File Offset: 0x0002D4D8
	public string GetMD5String(string loginKey, string userId, string password, int randomNum)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = 34;
		BindingLink.CreateMD5String(stringBuilder, loginKey, userId, password, randomNum);
		return stringBuilder.ToString();
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x0002F304 File Offset: 0x0002D504
	public bool IsSecure()
	{
		return BindingLink.IsEnableEncrypto();
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x0002F318 File Offset: 0x0002D518
	public void ResetNativeResultScore()
	{
		BindingLink.ResetResultScore();
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x0002F320 File Offset: 0x0002D520
	public NativeScoreResult GetNativeResultScore()
	{
		NativeScoreResult resultScore = BindingLink.GetResultScore();
		Debug.Log("Kenzan-score.stageScore = " + resultScore.stageScore.ToString(), DebugTraceManager.TraceType.SERVER);
		Debug.Log("Kenzan-score.ringScore = " + resultScore.ringScore.ToString(), DebugTraceManager.TraceType.SERVER);
		Debug.Log("Kenzan-score.animalScore = " + resultScore.animalScore.ToString(), DebugTraceManager.TraceType.SERVER);
		Debug.Log("Kenzan-score.distanceScore = " + resultScore.distanceScore.ToString(), DebugTraceManager.TraceType.SERVER);
		Debug.Log("Kenzan-score.finalScore = " + resultScore.finalScore.ToString());
		Debug.Log("Kenzan-score.redStarRingCount = " + resultScore.redStarRingCount.ToString(), DebugTraceManager.TraceType.SERVER);
		return resultScore;
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x0002F3F0 File Offset: 0x0002D5F0
	public void CheckNativeHalfWayResultScore(ServerGameResults gameResult)
	{
		PostGameResultNativeParam param = default(PostGameResultNativeParam);
		param.Init(gameResult);
		StageScoreManager instance = StageScoreManager.Instance;
		StageScorePool scorePool = instance.ScorePool;
		BindingLink.CheckHalfWayResultScore(param, scorePool.scoreDatas, scorePool.StoredCount);
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x0002F42C File Offset: 0x0002D62C
	public void CheckNativeHalfWayQuickModeResultScore(ServerQuickModeGameResults gameResult)
	{
		QuickModePostGameResultNativeParam param = default(QuickModePostGameResultNativeParam);
		param.Init(gameResult);
		StageScoreManager instance = StageScoreManager.Instance;
		StageScorePool scorePool = instance.ScorePool;
		BindingLink.CheckHalfWayQuickModeResultScore(param, scorePool.scoreDatas, scorePool.StoredCount);
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x0002F468 File Offset: 0x0002D668
	public void CheckNativeQuickModeResultTimer(int gold, int silver, int bronze, int continueCount, int main, int sub, int total, long playTime)
	{
		QuickModeTimerNativeParam param = default(QuickModeTimerNativeParam);
		param.Init(gold, silver, bronze, continueCount, main, sub, total, playTime);
		BindingLink.CheckQuickModeResultTimer(param);
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x0002F498 File Offset: 0x0002D698
	public void BootGameCheatCheck()
	{
		BindingLink.BootGameAction();
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x0002F4A0 File Offset: 0x0002D6A0
	public void BeforeGameCheatCheck()
	{
		BindingLink.BeforeGameAction();
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x0002F4A8 File Offset: 0x0002D6A8
	public string DecodeServerResponseText(string responseText)
	{
		int capacity = BindingLink.CalcResponseString(responseText, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = capacity;
		BindingLink.GetResponseString(stringBuilder, responseText, CryptoUtility.code);
		Debug.Log("DecodeServerResponseText() = " + stringBuilder, DebugTraceManager.TraceType.SERVER);
		return stringBuilder.ToString();
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x0002F4F4 File Offset: 0x0002D6F4
	public string GetOnlySendBaseParamString()
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcOnlySendBaseparamString(baseInfo, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x0002F538 File Offset: 0x0002D738
	public string GetLoginString(string userId, string password, string migrationPassword, int platform, string device, int language, int salesLocale, int storeId, int platformSns)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcLoginString(baseInfo, userId, password, migrationPassword, platform, device, language, salesLocale, storeId, platformSns, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x0002F58C File Offset: 0x0002D78C
	public string GetMigrationString(string userId, string migrationPassword, string migrationUserPassword, int platform, string device, int language, int salesLocale, int storeId, int platformSns)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcMigrationString(baseInfo, userId, migrationPassword, migrationUserPassword, platform, device, language, salesLocale, storeId, platformSns, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x0002F5E0 File Offset: 0x0002D7E0
	public string GetGetMigrationPasswordString(string userPassword)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcGetMigrationPasswordString(baseInfo, userPassword, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x0002F624 File Offset: 0x0002D824
	public string GetSetUserNameString(string userName)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcSetUserNameString(baseInfo, userName, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x0002F668 File Offset: 0x0002D868
	public string GetActStartString(List<int> modifire, List<string> distanceFriendList, bool tutorial, int eventId)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		ActStartNativeParam actStartParam = default(ActStartNativeParam);
		actStartParam.Init(modifire, tutorial, eventId);
		string[] array = new string[50];
		for (int i = 0; i < 50; i++)
		{
			if (i < distanceFriendList.Count)
			{
				array[i] = distanceFriendList[i];
			}
			else
			{
				array[i] = null;
			}
		}
		int num = BindingLink.CalcActStartString(baseInfo, actStartParam, array, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x0002F700 File Offset: 0x0002D900
	public string GetQuickModeActStartString(List<int> modifire, int tutorial)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		QuickModeActStartNativeParam actStartParam = default(QuickModeActStartNativeParam);
		actStartParam.Init(modifire, tutorial);
		int num = BindingLink.CalcQuickModeActStartString(baseInfo, actStartParam, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x0002F754 File Offset: 0x0002D954
	public string GetActRetryString()
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcActRetryString(baseInfo, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x0002F798 File Offset: 0x0002D998
	public string GetPostGameResultString(ServerGameResults gameResult)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		PostGameResultNativeParam param = default(PostGameResultNativeParam);
		param.Init(gameResult);
		StageScoreManager instance = StageScoreManager.Instance;
		StageScorePool scorePool = instance.ScorePool;
		int num = BindingLink.CalcPostGameResultString(baseInfo, param, scorePool.scoreDatas, scorePool.StoredCount, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x0002F80C File Offset: 0x0002DA0C
	public string GetQuickModePostGameResultString(ServerQuickModeGameResults gameResult)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		QuickModePostGameResultNativeParam param = default(QuickModePostGameResultNativeParam);
		param.Init(gameResult);
		StageScoreManager instance = StageScoreManager.Instance;
		StageScorePool scorePool = instance.ScorePool;
		int num = BindingLink.CalcQuickModePostGameResultString(baseInfo, param, scorePool.scoreDatas, scorePool.StoredCount, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x0002F880 File Offset: 0x0002DA80
	public string GetGetMileageRewardString(int episode, int chapter)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcGetMileageRewardString(baseInfo, episode, chapter, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x0002F8C4 File Offset: 0x0002DAC4
	public string GetUpgradeCharacterString(int characterId, int abilityId)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcUpgradeCharacterString(baseInfo, characterId, abilityId, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x0002F908 File Offset: 0x0002DB08
	public string GetUnlockedCharacterString(int characterId, int itemId)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcUnlockedCharacterString(baseInfo, characterId, itemId, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x0002F94C File Offset: 0x0002DB4C
	public string GetChangeCharacterString(int mainCharacterId, int subCharacterId)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcChangeCharacterString(baseInfo, mainCharacterId, subCharacterId, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x0002F990 File Offset: 0x0002DB90
	public string GetGetWeeklyLeaderboardEntries(int mode, int first, int count, int type, List<string> friendIdList, int eventId)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		LeaderboardEntryNativeParam leaderboardEntryParam = default(LeaderboardEntryNativeParam);
		leaderboardEntryParam.Init(mode, first, count, type, eventId);
		string[] array = new string[50];
		for (int i = 0; i < 50; i++)
		{
			if (i < friendIdList.Count)
			{
				array[i] = friendIdList[i];
			}
			else
			{
				array[i] = null;
			}
		}
		int num = BindingLink.CalcGetWeeklyLeaderboardEntryString(baseInfo, leaderboardEntryParam, array, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x0002FA30 File Offset: 0x0002DC30
	public string GetGetLeagueOperatorDataString(int mode, int league)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcLeagueOperatorDataString(baseInfo, mode, league, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x0002FA74 File Offset: 0x0002DC74
	public string GetGetLeagueDataString(int mode)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcLeagueDataString(baseInfo, mode, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x0002FAB8 File Offset: 0x0002DCB8
	public string GetGetWeeklyLeaderboardOptionsString(int mode)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcWeeklyLeaderboardOptionsString(baseInfo, mode, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x0002FAFC File Offset: 0x0002DCFC
	public string GetSetInviteCodeString(string friendId)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcInviteCodeString(baseInfo, friendId, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x0002FB40 File Offset: 0x0002DD40
	public string GetGetFacebookIncentiveString(int type, int achievementCount)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcGetFacebookIncentiveString(baseInfo, type, achievementCount, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x0002FB84 File Offset: 0x0002DD84
	public string GetSetFacebookScopedIdString(string facebookId)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcSetFacebookScopedIdString(baseInfo, facebookId, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x0002FBC8 File Offset: 0x0002DDC8
	public string GetGetFacebookFriendUserIdList(List<string> facebookIdList)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		string[] array = new string[50];
		for (int i = 0; i < 50; i++)
		{
			if (i < facebookIdList.Count)
			{
				array[i] = facebookIdList[i];
			}
			else
			{
				array[i] = null;
			}
		}
		int num = BindingLink.CalcGetFriendUserIdListString(baseInfo, array, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x0002FC4C File Offset: 0x0002DE4C
	public string GetSendEnergyString(string friendId)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcSendEnergyString(baseInfo, friendId, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x0002FC90 File Offset: 0x0002DE90
	public string GetGetMessageString(List<int> messageId, List<int> operationMessageId)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		List<int> list = new List<int>();
		if (messageId != null)
		{
			foreach (int item in messageId)
			{
				list.Add(item);
			}
		}
		List<int> list2 = new List<int>();
		if (operationMessageId != null)
		{
			foreach (int item2 in operationMessageId)
			{
				list2.Add(item2);
			}
		}
		int num = BindingLink.CalcGetMessageString(baseInfo, list.ToArray(), list.Count, list2.ToArray(), list2.Count, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x0002FDB8 File Offset: 0x0002DFB8
	public string GetGetRedStarExchangeListString(int itemType)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcGetRedStarExchangeListString(baseInfo, itemType, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x0002FDFC File Offset: 0x0002DFFC
	public string GetRedStarExchangeString(int itemId)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcRedStarExchangeString(baseInfo, itemId, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x0002FE40 File Offset: 0x0002E040
	public string GetBuyIosString(string receiptData)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcBuyIosString(baseInfo, receiptData, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x0002FE84 File Offset: 0x0002E084
	public string GetBuyAndroidString(string receiptData, string receiptSignature)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcBuyAndroidString(baseInfo, receiptData, receiptSignature, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x0002FEC8 File Offset: 0x0002E0C8
	public string GetSetBirthdayString(string birthday)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcSetBirthdayString(baseInfo, birthday, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x0002FF0C File Offset: 0x0002E10C
	public string GetCommitWheelSpinString(int count)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcCommitWheelSpinString(baseInfo, count, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x0002FF50 File Offset: 0x0002E150
	public string GetCommitChaoWheelSpinString(int count)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcCommitChaoWheelSpinString(baseInfo, count, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x0002FF94 File Offset: 0x0002E194
	public string GetEquipChaoString(int mainChaoId, int subChaoId)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcEquipChaoString(baseInfo, mainChaoId, subChaoId, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x0002FFD8 File Offset: 0x0002E1D8
	public string GetAddSpecialEggString(int numSpecialEgg)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcAddSpecialEggString(baseInfo, numSpecialEgg, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x0003001C File Offset: 0x0002E21C
	public string GetGetPrizeChaoWheelSpinString(int chaoWheelSpinType)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcGetPrizeChaoWheelSpinString(baseInfo, chaoWheelSpinType, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x00030060 File Offset: 0x0002E260
	public string GetGetWheelSpinGeneralString(int eventId, int spinId)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcGetWheelSpinGeneralString(baseInfo, eventId, spinId, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x000300A4 File Offset: 0x0002E2A4
	public string GetCommitWheelSpinGeneralString(int eventId, int spinCostItemId, int spinId, int spinNum)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcCommitWheelSpinGeneralString(baseInfo, eventId, spinCostItemId, spinId, spinNum, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x000300EC File Offset: 0x0002E2EC
	public string GetGetPrizeWheelSpinGeneralString(int eventId, int spinType)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcGetPrizeWheelSpinGeneralString(baseInfo, eventId, spinType, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x00030130 File Offset: 0x0002E330
	public string GetSendApolloString(int type, List<string> value)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcSendAppoloString(baseInfo, type, value.ToArray(), value.Count, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x00030180 File Offset: 0x0002E380
	public string GetSetSerialCodeString(string campaignId, string serialCode, bool newUser)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcSetSerialCodeString(baseInfo, campaignId, serialCode, newUser, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x000301C8 File Offset: 0x0002E3C8
	public string GetSetNoahIdString(string noahId)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcSetNoahIdString(baseInfo, noahId, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x0003020C File Offset: 0x0002E40C
	public string GetEventRewardString(int eventId)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcGetEventRewardString(baseInfo, eventId, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x00030250 File Offset: 0x0002E450
	public string GetGetItemStockNumString(int eventId, int[] itemIdList)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int listSize = 0;
		if (itemIdList != null)
		{
			listSize = itemIdList.Length;
		}
		int num = BindingLink.CalcGetItemStockNum(baseInfo, eventId, itemIdList, listSize, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x000302A4 File Offset: 0x0002E4A4
	public string GetDailyBattleDataHistoryString(int count)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcGetDailyBattleDataHistoryString(baseInfo, count, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x000302E8 File Offset: 0x0002E4E8
	public string ResetDailyBattleMatchingString(int type)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcResetDailyBattleMatchingString(baseInfo, type, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x0003032C File Offset: 0x0002E52C
	public string LoginBonusSelectString(int rewardId, int rewardDays, int rewardSelect, int firstRewardDays, int firstRewardSelect)
	{
		SendBaseNativeParam baseInfo = default(SendBaseNativeParam);
		baseInfo.Init();
		int num = BindingLink.CalcLoginBonusSelectString(baseInfo, rewardId, rewardDays, rewardSelect, firstRewardDays, firstRewardSelect, CryptoUtility.code);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Capacity = num;
		BindingLink.GetSendString(stringBuilder, num);
		return stringBuilder.ToString();
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x00030378 File Offset: 0x0002E578
	private static void DebugLogCallbackFromCPlusPlus(string log)
	{
		Debug.Log("From C++ [" + log + "]", DebugTraceManager.TraceType.SERVER);
	}

	// Token: 0x04000631 RID: 1585
	public const string PluginName = "UnmanagedProcess";
}
