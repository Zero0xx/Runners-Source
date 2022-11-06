using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000113 RID: 275
public class CPlusPlusLink : MonoBehaviour
{
	// Token: 0x1700016A RID: 362
	// (get) Token: 0x0600086F RID: 2159 RVA: 0x000303AC File Offset: 0x0002E5AC
	// (set) Token: 0x06000870 RID: 2160 RVA: 0x000303B4 File Offset: 0x0002E5B4
	public static CPlusPlusLink Instance
	{
		get
		{
			return CPlusPlusLink.m_instance;
		}
		private set
		{
		}
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x000303B8 File Offset: 0x0002E5B8
	private void Awake()
	{
		if (CPlusPlusLink.m_instance == null)
		{
			CPlusPlusLink.m_instance = this;
			this.m_binding = new BindingLink();
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x00030404 File Offset: 0x0002E604
	private void OnDestroy()
	{
		if (this.m_binding != null)
		{
			this.m_binding.Dispose();
		}
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x0003041C File Offset: 0x0002E61C
	public string GetMD5String(string loginKey, string userId, string password, int randomNum)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetMD5String(loginKey, userId, password, randomNum);
		}
		return result;
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x0003044C File Offset: 0x0002E64C
	public bool IsEnable()
	{
		return this.m_binding != null;
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x0003045C File Offset: 0x0002E65C
	public bool IsSecure()
	{
		bool result = false;
		if (this.m_binding != null)
		{
			result = this.m_binding.IsSecure();
		}
		return result;
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x00030484 File Offset: 0x0002E684
	public void ResetNativeResultScore()
	{
		if (this.m_binding != null)
		{
			this.m_binding.ResetNativeResultScore();
		}
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x0003049C File Offset: 0x0002E69C
	public NativeScoreResult GetNativeResultScore()
	{
		if (this.m_binding != null)
		{
			return this.m_binding.GetNativeResultScore();
		}
		return default(NativeScoreResult);
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x000304CC File Offset: 0x0002E6CC
	public void CheckNativeHalfWayResultScore(ServerGameResults gameResult)
	{
		if (this.m_binding != null)
		{
			this.m_binding.CheckNativeHalfWayResultScore(gameResult);
			this.GetNativeResultScore();
		}
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x000304EC File Offset: 0x0002E6EC
	public void CheckNativeHalfWayQuickModeResultScore(ServerQuickModeGameResults gameResult)
	{
		if (this.m_binding != null)
		{
			this.m_binding.CheckNativeHalfWayQuickModeResultScore(gameResult);
			this.GetNativeResultScore();
		}
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x0003050C File Offset: 0x0002E70C
	public void CheckNativeQuickModeResultTimer(int gold, int silver, int bronze, int continueCount, int main, int sub, int total, long playTime)
	{
		if (this.m_binding != null)
		{
			this.m_binding.CheckNativeQuickModeResultTimer(gold, silver, bronze, continueCount, main, sub, total, playTime);
		}
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x0003053C File Offset: 0x0002E73C
	public void BootGameCheatCheck()
	{
		if (this.m_binding != null)
		{
			global::Debug.Log("CPlusPlusLink.BootGameCheatCheck");
			this.m_binding.BootGameCheatCheck();
		}
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x0003056C File Offset: 0x0002E76C
	public void BeforeGameCheatCheck()
	{
		if (this.m_binding != null)
		{
			global::Debug.Log("CPlusPlusLink.BeforeGameCheatCheck");
			this.m_binding.BeforeGameCheatCheck();
		}
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x0003059C File Offset: 0x0002E79C
	public string DecodeServerResponseText(string responseText)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.DecodeServerResponseText(responseText);
		}
		return result;
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x000305C8 File Offset: 0x0002E7C8
	public string GetOnlySendBaseParamString()
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetOnlySendBaseParamString();
		}
		return result;
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x000305F4 File Offset: 0x0002E7F4
	public string GetLoginString(string userId, string password, string migrationPassword, int platform, string device, int language, int salesLocale, int storeId, int platformSns)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetLoginString(userId, password, migrationPassword, platform, device, language, salesLocale, storeId, platformSns);
		}
		return result;
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x00030630 File Offset: 0x0002E830
	public string GetMigrationString(string userId, string migrationPassword, string migrationUserPassword, int platform, string device, int language, int salesLocale, int storeId, int platformSns)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetMigrationString(userId, migrationPassword, migrationUserPassword, platform, device, language, salesLocale, storeId, platformSns);
		}
		return result;
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x0003066C File Offset: 0x0002E86C
	public string GetGetMigrationPasswordString(string userPassword)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetGetMigrationPasswordString(userPassword);
		}
		return result;
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x00030698 File Offset: 0x0002E898
	public string GetSetUserNameString(string userName)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetSetUserNameString(userName);
		}
		return result;
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x000306C4 File Offset: 0x0002E8C4
	public string GetActStartString(List<int> modifire, List<string> distanceFriendList, bool tutorial, int eventId)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetActStartString(modifire, distanceFriendList, tutorial, eventId);
		}
		return result;
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x000306F4 File Offset: 0x0002E8F4
	public string GetQuickModeActStartString(List<int> modifire, int tutorial)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetQuickModeActStartString(modifire, tutorial);
		}
		return result;
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x00030724 File Offset: 0x0002E924
	public string GetActRetryString()
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetActRetryString();
		}
		return result;
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x00030750 File Offset: 0x0002E950
	public string GetPostGameResultString(ServerGameResults result)
	{
		string result2 = string.Empty;
		if (this.m_binding != null)
		{
			result2 = this.m_binding.GetPostGameResultString(result);
		}
		return result2;
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x0003077C File Offset: 0x0002E97C
	public string GetQuickModePostGameResultString(ServerQuickModeGameResults result)
	{
		string result2 = string.Empty;
		if (this.m_binding != null)
		{
			result2 = this.m_binding.GetQuickModePostGameResultString(result);
		}
		return result2;
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x000307A8 File Offset: 0x0002E9A8
	public string GetGetMileageRewardString(int episode, int chapter)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetGetMileageRewardString(episode, chapter);
		}
		return result;
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x000307D8 File Offset: 0x0002E9D8
	public string GetUpgradeCharacterString(int characterId, int abilityId)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetUpgradeCharacterString(characterId, abilityId);
		}
		return result;
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x00030808 File Offset: 0x0002EA08
	public string GetUnlockedCharacterString(int characterId, int itemId)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetUnlockedCharacterString(characterId, itemId);
		}
		return result;
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x00030838 File Offset: 0x0002EA38
	public string GetChangeCharacterString(int mainCharacterId, int subCharacterId)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetChangeCharacterString(mainCharacterId, subCharacterId);
		}
		return result;
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x00030868 File Offset: 0x0002EA68
	public string GetGetWeeklyLeaderboardEntries(int mode, int first, int count, int type, List<string> friendIdList, int eventId)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetGetWeeklyLeaderboardEntries(mode, first, count, type, friendIdList, eventId);
		}
		return result;
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x0003089C File Offset: 0x0002EA9C
	public string GetGetLeagueOperatorDataString(int mode, int league)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetGetLeagueOperatorDataString(mode, league);
		}
		return result;
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x000308CC File Offset: 0x0002EACC
	public string GetGetLeagueDataString(int mode)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetGetLeagueDataString(mode);
		}
		return result;
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x000308F8 File Offset: 0x0002EAF8
	public string GetGetWeeklyLeaderboardOptionsString(int mode)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetGetWeeklyLeaderboardOptionsString(mode);
		}
		return result;
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x00030924 File Offset: 0x0002EB24
	public string GetSetInviteCodeString(string friendId)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetSetInviteCodeString(friendId);
		}
		return result;
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x00030950 File Offset: 0x0002EB50
	public string GetGetFacebookIncentiveString(int type, int achievementCount)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetGetFacebookIncentiveString(type, achievementCount);
		}
		return result;
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x00030980 File Offset: 0x0002EB80
	public string GetSetFacebookScopedIdString(string facebookId)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetSetFacebookScopedIdString(facebookId);
		}
		return result;
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x000309AC File Offset: 0x0002EBAC
	public string GetGetFacebookFriendUserIdList(List<string> facebookIdList)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetGetFacebookFriendUserIdList(facebookIdList);
		}
		return result;
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x000309D8 File Offset: 0x0002EBD8
	public string GetSendEnergyString(string friendId)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetSendEnergyString(friendId);
		}
		return result;
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x00030A04 File Offset: 0x0002EC04
	public string GetGetMessageString(List<int> messageId, List<int> operationMessageId)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetGetMessageString(messageId, operationMessageId);
		}
		return result;
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x00030A34 File Offset: 0x0002EC34
	public string GetGetRedStarExchangeListString(int itemType)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetGetRedStarExchangeListString(itemType);
		}
		return result;
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x00030A60 File Offset: 0x0002EC60
	public string GetRedStarExchangeString(int itemId)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetRedStarExchangeString(itemId);
		}
		return result;
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x00030A8C File Offset: 0x0002EC8C
	public string GetBuyIosString(string receiptData)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetBuyIosString(receiptData);
		}
		return result;
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x00030AB8 File Offset: 0x0002ECB8
	public string GetBuyAndroidString(string receiptData, string receiptSignature)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetBuyAndroidString(receiptData, receiptSignature);
		}
		return result;
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x00030AE8 File Offset: 0x0002ECE8
	public string GetSetBirthdayString(string birthday)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetSetBirthdayString(birthday);
		}
		return result;
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x00030B14 File Offset: 0x0002ED14
	public string GetCommitWheelSpinString(int count)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetCommitWheelSpinString(count);
		}
		return result;
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x00030B40 File Offset: 0x0002ED40
	public string GetCommitChaoWheelSpinString(int count)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetCommitChaoWheelSpinString(count);
		}
		return result;
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x00030B6C File Offset: 0x0002ED6C
	public string GetEquipChaoString(int mainChaoId, int subChaoId)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetEquipChaoString(mainChaoId, subChaoId);
		}
		return result;
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x00030B9C File Offset: 0x0002ED9C
	public string GetAddSpecialEggString(int numSpecialEgg)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetAddSpecialEggString(numSpecialEgg);
		}
		return result;
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x00030BC8 File Offset: 0x0002EDC8
	public string GetGetPrizeChaoWheelSpinString(int chaoWheelSpinType)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetGetPrizeChaoWheelSpinString(chaoWheelSpinType);
		}
		return result;
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x00030BF4 File Offset: 0x0002EDF4
	public string GetGetWheelSpinGeneralString(int eventId, int spinId)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetGetWheelSpinGeneralString(eventId, spinId);
		}
		return result;
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x00030C24 File Offset: 0x0002EE24
	public string GetCommitWheelSpinGeneralString(int eventId, int spinCostItemId, int spinId, int spinNum)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetCommitWheelSpinGeneralString(eventId, spinCostItemId, spinId, spinNum);
		}
		return result;
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x00030C54 File Offset: 0x0002EE54
	public string GetGetPrizeWheelSpinGeneralString(int eventId, int spinType)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetGetPrizeWheelSpinGeneralString(eventId, spinType);
		}
		return result;
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x00030C84 File Offset: 0x0002EE84
	public string GetSendApolloString(int type, List<string> value)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetSendApolloString(type, value);
		}
		return result;
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x00030CB4 File Offset: 0x0002EEB4
	public string GetSetSerialCodeString(string campaignId, string serialCode, bool newUser)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetSetSerialCodeString(campaignId, serialCode, newUser);
		}
		return result;
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x00030CE4 File Offset: 0x0002EEE4
	public string GetSetNoahIdString(string noahId)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetSetNoahIdString(noahId);
		}
		return result;
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x00030D10 File Offset: 0x0002EF10
	public string GetEventRewardString(int eventId)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetEventRewardString(eventId);
		}
		return result;
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x00030D3C File Offset: 0x0002EF3C
	public string GetGetEventStateString(int eventId)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetEventRewardString(eventId);
		}
		return result;
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x00030D68 File Offset: 0x0002EF68
	public string GetGetItemStockNumString(int eventId, int[] itemIdList)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetGetItemStockNumString(eventId, itemIdList);
		}
		return result;
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x00030D98 File Offset: 0x0002EF98
	public string GetDailyBattleDataHistoryString(int count)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.GetDailyBattleDataHistoryString(count);
		}
		return result;
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x00030DC4 File Offset: 0x0002EFC4
	public string ResetDailyBattleMatchingString(int type)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.ResetDailyBattleMatchingString(type);
		}
		return result;
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x00030DF0 File Offset: 0x0002EFF0
	public string LoginBonusSelectString(int rewardId, int rewardDays, int rewardSelect, int firstRewardDays, int firstRewardSelect)
	{
		string result = string.Empty;
		if (this.m_binding != null)
		{
			result = this.m_binding.LoginBonusSelectString(rewardId, rewardDays, rewardSelect, firstRewardDays, firstRewardSelect);
		}
		return result;
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x00030E24 File Offset: 0x0002F024
	private void Start()
	{
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x00030E28 File Offset: 0x0002F028
	private void Update()
	{
	}

	// Token: 0x04000632 RID: 1586
	private static CPlusPlusLink m_instance;

	// Token: 0x04000633 RID: 1587
	private BindingLink m_binding;
}
