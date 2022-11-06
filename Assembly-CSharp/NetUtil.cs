using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using App;
using DataTable;
using LitJson;
using SaveData;

// Token: 0x0200082B RID: 2091
public class NetUtil
{
	// Token: 0x06003853 RID: 14419 RVA: 0x001292B0 File Offset: 0x001274B0
	// Note: this type is marked as 'beforefieldinit'.
	static NetUtil()
	{
		NetUtil.ConnectTimeOut = 15f;
		NetUtil.ReloginStartTime = new TimeSpan(0, 30, 0);
	}

	// Token: 0x06003854 RID: 14420 RVA: 0x001292E0 File Offset: 0x001274E0
	public static string GetWebPageURL(InformationDataTable.Type type)
	{
		return NetBaseUtil.InformationServerURL + InformationDataTable.GetUrl(type);
	}

	// Token: 0x06003855 RID: 14421 RVA: 0x001292F4 File Offset: 0x001274F4
	public static string GetAssetBundleUrl()
	{
		string str = NetBaseUtil.AssetServerURL + "assetbundle/";
		return str + "android/";
	}

	// Token: 0x06003856 RID: 14422 RVA: 0x00129320 File Offset: 0x00127520
	public static string Base64Encode(string text)
	{
		return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
	}

	// Token: 0x06003857 RID: 14423 RVA: 0x00129340 File Offset: 0x00127540
	public static byte[] Base64EncodeToBytes(string text)
	{
		string s = NetUtil.Base64Encode(text);
		return Encoding.ASCII.GetBytes(s);
	}

	// Token: 0x06003858 RID: 14424 RVA: 0x00129364 File Offset: 0x00127564
	public static string Base64Decode(string text)
	{
		byte[] bytes = Convert.FromBase64String(text);
		return Encoding.UTF8.GetString(bytes);
	}

	// Token: 0x06003859 RID: 14425 RVA: 0x00129388 File Offset: 0x00127588
	public static string Base64DecodeFromBytes(byte[] byte_base64)
	{
		string @string = Encoding.ASCII.GetString(byte_base64);
		return NetUtil.Base64Decode(@string);
	}

	// Token: 0x0600385A RID: 14426 RVA: 0x001293AC File Offset: 0x001275AC
	public static byte[] Xor(byte[] bytes)
	{
		byte[] array = new byte[bytes.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (bytes[i] ^ byte.MaxValue);
		}
		return array;
	}

	// Token: 0x0600385B RID: 14427 RVA: 0x001293E4 File Offset: 0x001275E4
	public static string CalcMD5String(string plainText)
	{
		MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] bytes = Encoding.UTF8.GetBytes(plainText);
		byte[] array = md5CryptoServiceProvider.ComputeHash(bytes);
		string text = string.Empty;
		foreach (byte b in array)
		{
			string text2 = b.ToString("X2");
			text += text2.ToLower();
		}
		return text;
	}

	// Token: 0x0600385C RID: 14428 RVA: 0x00129454 File Offset: 0x00127654
	public static void SyncSaveDataAndDataBase(ServerPlayerState playerState)
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance == null)
		{
			return;
		}
		ItemData itemData = instance.ItemData;
		if (itemData != null)
		{
			itemData.RingCount = (uint)playerState.m_numRings;
			itemData.RedRingCount = (uint)playerState.m_numRedRings;
			for (ItemType itemType = ItemType.INVINCIBLE; itemType < ItemType.NUM; itemType++)
			{
				ServerItem serverItem = new ServerItem(itemType);
				ServerItemState itemStateById = playerState.GetItemStateById((int)serverItem.id);
				if (itemStateById != null)
				{
					itemData.ItemCount[(int)itemType] = (uint)itemStateById.m_num;
				}
			}
		}
		PlayerData playerData = instance.PlayerData;
		if (playerData != null)
		{
			playerData.ProgressStatus = (uint)playerState.m_dailyMissionValue;
			playerData.TotalDistance = playerState.m_totalDistance;
			playerData.ChallengeCount = (uint)playerState.m_numEnergy;
			playerData.BestScore = playerState.m_totalHighScore;
			playerData.BestScoreQuick = playerState.m_totalHighScoreQuick;
			PlayerData playerData2 = playerData;
			ServerItem serverItem2 = new ServerItem((ServerItem.Id)playerState.m_mainCharaId);
			playerData2.MainChara = serverItem2.charaType;
			PlayerData playerData3 = playerData;
			ServerItem serverItem3 = new ServerItem((ServerItem.Id)playerState.m_subCharaId);
			playerData3.SubChara = serverItem3.charaType;
			playerData.Rank = (uint)playerState.m_numRank;
			PlayerData playerData4 = playerData;
			ServerItem serverItem4 = new ServerItem((ServerItem.Id)playerState.m_mainChaoId);
			playerData4.MainChaoID = serverItem4.chaoId;
			PlayerData playerData5 = playerData;
			ServerItem serverItem5 = new ServerItem((ServerItem.Id)playerState.m_subChaoId);
			playerData5.SubChaoID = serverItem5.chaoId;
			playerData.DailyMission.id = playerState.m_dailyMissionId;
			playerData.DailyMission.progress = (long)playerState.m_dailyMissionValue;
			playerData.DailyMission.missions_complete = playerState.m_dailyChallengeComplete;
			playerData.DailyMission.clear_count = playerState.m_numDailyChalCont;
		}
	}

	// Token: 0x0600385D RID: 14429 RVA: 0x001295E8 File Offset: 0x001277E8
	public static void SyncSaveDataAndDataBase(ServerCharacterState[] charaState)
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance == null)
		{
			return;
		}
		CharaData charaData = instance.CharaData;
		if (charaData != null)
		{
			for (int i = 0; i < 29; i++)
			{
				ServerCharacterState serverCharacterState = charaState[i];
				if (serverCharacterState != null)
				{
					CharaAbility charaAbility = charaData.AbilityArray[i];
					if (charaAbility != null)
					{
						for (int j = 0; j < serverCharacterState.AbilityLevel.Count; j++)
						{
							int id = 120000 + j;
							ServerItem serverItem = new ServerItem((ServerItem.Id)id);
							AbilityType abilityType = serverItem.abilityType;
							if (abilityType != AbilityType.NONE)
							{
								charaAbility.Ability[(int)abilityType] = (uint)serverCharacterState.AbilityLevel[j];
							}
						}
						if (serverCharacterState != null)
						{
							if (serverCharacterState.IsUnlocked)
							{
								charaData.Status[i] = 1;
							}
							else
							{
								charaData.Status[i] = 0;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600385E RID: 14430 RVA: 0x001296D0 File Offset: 0x001278D0
	public static void SyncSaveDataAndDataBase(List<ServerChaoState> chaoState)
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance == null)
		{
			return;
		}
		if (chaoState != null)
		{
			instance.ChaoData = new SaveData.ChaoData(chaoState);
		}
	}

	// Token: 0x0600385F RID: 14431 RVA: 0x00129704 File Offset: 0x00127904
	public static void SyncSaveDataAndDailyMission(ServerDailyChallengeState dailyChallengeState = null)
	{
		if (dailyChallengeState == null)
		{
			dailyChallengeState = ServerInterface.DailyChallengeState;
		}
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance == null)
		{
			return;
		}
		PlayerData playerData = instance.PlayerData;
		if (playerData == null)
		{
			return;
		}
		playerData.DailyMission.clear_count = dailyChallengeState.m_numIncentiveCont;
		playerData.DailyMission.date = dailyChallengeState.m_numDailyChalDay;
		playerData.DailyMission.max = dailyChallengeState.m_maxDailyChalDay;
		playerData.DailyMission.reward_max = dailyChallengeState.m_maxIncentive;
		foreach (ServerDailyChallengeIncentive serverDailyChallengeIncentive in dailyChallengeState.m_incentiveList)
		{
			int num = serverDailyChallengeIncentive.m_numIncentiveCont - 1;
			Debug.Log(string.Empty);
			int[] reward_id = playerData.DailyMission.reward_id;
			int num2 = num;
			ServerItem serverItem = new ServerItem((ServerItem.Id)serverDailyChallengeIncentive.m_itemId);
			reward_id[num2] = serverItem.rewardType;
			playerData.DailyMission.reward_count[num] = serverDailyChallengeIncentive.m_num;
		}
	}

	// Token: 0x06003860 RID: 14432 RVA: 0x00129820 File Offset: 0x00127A20
	public static bool IsAlreadySessionTimeOut(DateTime sessionTimeLimit, DateTime currentTime)
	{
		if (sessionTimeLimit <= currentTime)
		{
			return true;
		}
		TimeSpan t = new TimeSpan(0, 0, 0, (int)NetUtil.ConnectTimeOut);
		DateTime t2 = currentTime + t;
		return sessionTimeLimit <= t2;
	}

	// Token: 0x06003861 RID: 14433 RVA: 0x00129864 File Offset: 0x00127A64
	public static bool IsSessionTimeOutSoon(DateTime sessionTimeLimit, DateTime currentTime)
	{
		return NetUtil.IsAlreadySessionTimeOut(sessionTimeLimit, currentTime + NetUtil.ReloginStartTime);
	}

	// Token: 0x06003862 RID: 14434 RVA: 0x00129880 File Offset: 0x00127A80
	private static JsonData Find(JsonData from, string key)
	{
		return from[key];
	}

	// Token: 0x06003863 RID: 14435 RVA: 0x0012988C File Offset: 0x00127A8C
	public static bool IsExist(JsonData from, string key)
	{
		return from.ContainsKey(key);
	}

	// Token: 0x06003864 RID: 14436 RVA: 0x00129898 File Offset: 0x00127A98
	public static JsonData GetJsonArray(JsonData jdata, string key)
	{
		if (jdata == null)
		{
			return null;
		}
		if (!NetUtil.IsExist(jdata, key))
		{
			return null;
		}
		JsonData jsonData = NetUtil.Find(jdata, key);
		if (jsonData == null)
		{
			return null;
		}
		if (jsonData.IsArray)
		{
			return jsonData;
		}
		Debug.Log("GetJsonArray : There is not array : " + key);
		return null;
	}

	// Token: 0x06003865 RID: 14437 RVA: 0x001298EC File Offset: 0x00127AEC
	public static JsonData GetJsonArrayObject(JsonData jdata, string key, int index)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, key);
		if (jsonArray != null)
		{
			return jsonArray[index];
		}
		return null;
	}

	// Token: 0x06003866 RID: 14438 RVA: 0x00129914 File Offset: 0x00127B14
	public static JsonData GetJsonObject(JsonData jdata, string key)
	{
		if (!jdata.ContainsKey(key))
		{
			return null;
		}
		JsonData jsonData = NetUtil.Find(jdata, key);
		if (jsonData != null && jsonData.IsObject)
		{
			return jsonData;
		}
		return null;
	}

	// Token: 0x06003867 RID: 14439 RVA: 0x0012994C File Offset: 0x00127B4C
	public static string GetJsonString(JsonData jdata, string key)
	{
		if (!jdata.ContainsKey(key))
		{
			return string.Empty;
		}
		JsonData jdata2 = NetUtil.Find(jdata, key);
		return NetUtil.GetJsonString(jdata2);
	}

	// Token: 0x06003868 RID: 14440 RVA: 0x0012997C File Offset: 0x00127B7C
	public static string GetJsonString(JsonData jdata)
	{
		string result = null;
		if (jdata != null)
		{
			if (jdata.IsString)
			{
				result = (string)jdata;
			}
			else if (jdata.IsInt)
			{
				result = ((int)jdata).ToString();
			}
			else if (jdata.IsLong)
			{
				result = ((long)jdata).ToString();
			}
			else
			{
				Debug.Log("GetJsonIntValue : Illegal JSON Object");
			}
		}
		return result;
	}

	// Token: 0x06003869 RID: 14441 RVA: 0x001299F4 File Offset: 0x00127BF4
	public static float GetJsonFloat(JsonData jdata, string key)
	{
		if (!jdata.ContainsKey(key))
		{
			return 0f;
		}
		JsonData jdata2 = NetUtil.Find(jdata, key);
		return NetUtil.GetJsonFloat(jdata2);
	}

	// Token: 0x0600386A RID: 14442 RVA: 0x00129A24 File Offset: 0x00127C24
	public static float GetJsonFloat(JsonData jdata)
	{
		float result = 0f;
		if (jdata != null)
		{
			if (jdata.IsDouble)
			{
				result = (float)((long)jdata);
			}
			else if (jdata.IsString)
			{
				string s = (string)jdata;
				if (!float.TryParse(s, out result))
				{
					result = 0f;
				}
			}
			else
			{
				Debug.Log("GetJsonFloat : Illegal JSON Object");
			}
		}
		return result;
	}

	// Token: 0x0600386B RID: 14443 RVA: 0x00129A8C File Offset: 0x00127C8C
	public static int GetJsonInt(JsonData jdata, string key)
	{
		if (!jdata.ContainsKey(key))
		{
			return 0;
		}
		JsonData jdata2 = NetUtil.Find(jdata, key);
		return NetUtil.GetJsonInt(jdata2);
	}

	// Token: 0x0600386C RID: 14444 RVA: 0x00129AB8 File Offset: 0x00127CB8
	public static int GetJsonInt(JsonData jdata)
	{
		int result = 0;
		if (jdata != null)
		{
			if (jdata.IsInt)
			{
				result = (int)jdata;
			}
			else if (jdata.IsString)
			{
				string s = (string)jdata;
				if (!int.TryParse(s, out result))
				{
					result = 0;
				}
			}
			else
			{
				Debug.Log("GetJsonIntValue : Illegal JSON Object");
			}
		}
		return result;
	}

	// Token: 0x0600386D RID: 14445 RVA: 0x00129B18 File Offset: 0x00127D18
	public static long GetJsonLong(JsonData jdata, string key)
	{
		if (!jdata.ContainsKey(key))
		{
			return 0L;
		}
		JsonData jdata2 = NetUtil.Find(jdata, key);
		return NetUtil.GetJsonLong(jdata2);
	}

	// Token: 0x0600386E RID: 14446 RVA: 0x00129B44 File Offset: 0x00127D44
	public static long GetJsonLong(JsonData jdata)
	{
		long result = 0L;
		if (jdata != null)
		{
			if (jdata.IsLong)
			{
				result = (long)jdata;
			}
			else if (jdata.IsInt)
			{
				result = (long)((int)jdata);
			}
			else if (jdata.IsString)
			{
				string s = (string)jdata;
				if (!long.TryParse(s, out result))
				{
					result = 0L;
				}
			}
			else
			{
				Debug.Log("GetJsonIntValue : Illegal JSON Object");
			}
		}
		return result;
	}

	// Token: 0x0600386F RID: 14447 RVA: 0x00129BBC File Offset: 0x00127DBC
	public static bool GetJsonFlag(JsonData jdata, string key)
	{
		if (!jdata.ContainsKey(key))
		{
			return false;
		}
		JsonData jdata2 = NetUtil.Find(jdata, key);
		return NetUtil.GetJsonFlag(jdata2);
	}

	// Token: 0x06003870 RID: 14448 RVA: 0x00129BE8 File Offset: 0x00127DE8
	public static bool GetJsonFlag(JsonData jdata)
	{
		return NetUtil.GetJsonInt(jdata) != 0;
	}

	// Token: 0x06003871 RID: 14449 RVA: 0x00129C08 File Offset: 0x00127E08
	public static bool GetJsonBoolean(JsonData jdata, string key)
	{
		if (!jdata.ContainsKey(key))
		{
			return false;
		}
		JsonData jdata2 = NetUtil.Find(jdata, key);
		return NetUtil.GetJsonBoolean(jdata2);
	}

	// Token: 0x06003872 RID: 14450 RVA: 0x00129C34 File Offset: 0x00127E34
	public static bool GetJsonBoolean(JsonData jdata)
	{
		return jdata.IsBoolean && (bool)jdata;
	}

	// Token: 0x06003873 RID: 14451 RVA: 0x00129C58 File Offset: 0x00127E58
	public static void WriteValue(JsonWriter writer, string propertyName, object value)
	{
		if (writer == null)
		{
			return;
		}
		if (propertyName != null && string.Empty != propertyName)
		{
			writer.WritePropertyName(propertyName);
		}
		if (value is string)
		{
			writer.Write((string)value);
		}
		else if (value is int)
		{
			writer.Write(((int)value).ToString());
		}
		else if (value is long)
		{
			writer.Write(((long)value).ToString());
		}
		else if (value is ulong)
		{
			writer.Write(((ulong)value).ToString());
		}
		else if (value is float)
		{
			writer.Write(((float)value).ToString());
		}
		else if (value is bool)
		{
			writer.Write(((bool)value).ToString());
		}
	}

	// Token: 0x06003874 RID: 14452 RVA: 0x00129D58 File Offset: 0x00127F58
	public static void WriteObject(JsonWriter writer, string objectName, Dictionary<string, object> properties)
	{
		if (writer == null)
		{
			return;
		}
		if (properties == null)
		{
			return;
		}
		if (objectName != null && string.Empty != objectName)
		{
			writer.WritePropertyName(objectName);
		}
		writer.WriteObjectStart();
		foreach (KeyValuePair<string, object> keyValuePair in properties)
		{
			string key = keyValuePair.Key;
			object value = keyValuePair.Value;
			NetUtil.WriteValue(writer, key, value);
		}
		writer.WriteObjectEnd();
	}

	// Token: 0x06003875 RID: 14453 RVA: 0x00129E04 File Offset: 0x00128004
	public static void WriteArray(JsonWriter writer, string arrayName, List<object> objects)
	{
		if (writer == null)
		{
			return;
		}
		if (objects == null)
		{
			return;
		}
		writer.WritePropertyName(arrayName);
		writer.WriteArrayStart();
		int count = objects.Count;
		for (int i = 0; i < count; i++)
		{
			object value = objects[i];
			NetUtil.WriteValue(writer, string.Empty, value);
		}
		writer.WriteArrayEnd();
	}

	// Token: 0x06003876 RID: 14454 RVA: 0x00129E60 File Offset: 0x00128060
	public static List<ServerDailyBattleDataPair> AnalyzeDailyBattleDataPairListJson(JsonData jdata, string key = "battleDataList")
	{
		List<ServerDailyBattleDataPair> list = new List<ServerDailyBattleDataPair>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, key);
		if (jsonArray == null)
		{
			return list;
		}
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jsonData = jsonArray[i];
			if (jsonData != null)
			{
				ServerDailyBattleDataPair item = NetUtil.AnalyzeDailyBattleDataPairJson(jsonData, "battleData", "rivalBattleData", "startTime", "endTime");
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x06003877 RID: 14455 RVA: 0x00129ED4 File Offset: 0x001280D4
	public static ServerDailyBattleDataPair AnalyzeDailyBattleDataPairJson(JsonData jdata, string myKey = "battleData", string rivalKey = "rivalBattleData", string startTimeKey = "startTime", string endTimeKey = "endTime")
	{
		ServerDailyBattleDataPair serverDailyBattleDataPair = new ServerDailyBattleDataPair();
		if (string.IsNullOrEmpty(myKey))
		{
			myKey = "battleData";
		}
		if (string.IsNullOrEmpty(rivalKey))
		{
			rivalKey = "rivalBattleData";
		}
		if (string.IsNullOrEmpty(startTimeKey))
		{
			startTimeKey = "startTime";
		}
		if (string.IsNullOrEmpty(endTimeKey))
		{
			endTimeKey = "endTime";
		}
		serverDailyBattleDataPair.starTime = NetUtil.AnalyzeDateTimeJson(jdata, startTimeKey);
		serverDailyBattleDataPair.endTime = NetUtil.AnalyzeDateTimeJson(jdata, endTimeKey);
		serverDailyBattleDataPair.myBattleData = NetUtil.AnalyzeDailyBattleDataJson(jdata, myKey);
		serverDailyBattleDataPair.rivalBattleData = NetUtil.AnalyzeDailyBattleDataJson(jdata, rivalKey);
		return serverDailyBattleDataPair;
	}

	// Token: 0x06003878 RID: 14456 RVA: 0x00129F68 File Offset: 0x00128168
	public static ServerDailyBattleData AnalyzeDailyBattleDataJson(JsonData jdata, string key)
	{
		ServerDailyBattleData serverDailyBattleData = new ServerDailyBattleData();
		JsonData jsonObject = NetUtil.GetJsonObject(jdata, key);
		if (jsonObject != null)
		{
			serverDailyBattleData.maxScore = NetUtil.GetJsonLong(jsonObject, "maxScore");
			serverDailyBattleData.league = NetUtil.GetJsonInt(jsonObject, "league");
			serverDailyBattleData.userId = NetUtil.GetJsonString(jsonObject, "userId");
			serverDailyBattleData.name = NetUtil.GetJsonString(jsonObject, "name");
			serverDailyBattleData.loginTime = NetUtil.GetJsonLong(jsonObject, "loginTime");
			serverDailyBattleData.mainChaoId = NetUtil.GetJsonInt(jsonObject, "mainChaoId");
			serverDailyBattleData.mainChaoLevel = NetUtil.GetJsonInt(jsonObject, "mainChaoLevel");
			serverDailyBattleData.subChaoId = NetUtil.GetJsonInt(jsonObject, "subChaoId");
			serverDailyBattleData.subChaoLevel = NetUtil.GetJsonInt(jsonObject, "subChaoLevel");
			serverDailyBattleData.numRank = NetUtil.GetJsonInt(jsonObject, "numRank");
			serverDailyBattleData.charaId = NetUtil.GetJsonInt(jsonObject, "charaId");
			serverDailyBattleData.charaLevel = NetUtil.GetJsonInt(jsonObject, "charaLevel");
			serverDailyBattleData.subCharaId = NetUtil.GetJsonInt(jsonObject, "subCharaId");
			serverDailyBattleData.subCharaLevel = NetUtil.GetJsonInt(jsonObject, "subCharaLevel");
			serverDailyBattleData.goOnWin = NetUtil.GetJsonInt(jsonObject, "goOnWin");
			serverDailyBattleData.isSentEnergy = NetUtil.GetJsonFlag(jsonObject, "energyFlg");
			serverDailyBattleData.language = (Env.Language)NetUtil.GetJsonInt(jsonObject, "language");
		}
		return serverDailyBattleData;
	}

	// Token: 0x06003879 RID: 14457 RVA: 0x0012A0AC File Offset: 0x001282AC
	public static ServerDailyBattleStatus AnalyzeDailyBattleStatusJson(JsonData jdata, string key)
	{
		ServerDailyBattleStatus serverDailyBattleStatus = new ServerDailyBattleStatus();
		JsonData jsonObject = NetUtil.GetJsonObject(jdata, key);
		if (jsonObject != null)
		{
			serverDailyBattleStatus.numWin = NetUtil.GetJsonInt(jsonObject, "numWin");
			serverDailyBattleStatus.numLose = NetUtil.GetJsonInt(jsonObject, "numLose");
			serverDailyBattleStatus.numDraw = NetUtil.GetJsonInt(jsonObject, "numDraw");
			serverDailyBattleStatus.numLoseByDefault = NetUtil.GetJsonInt(jsonObject, "numLoseByDefault");
			serverDailyBattleStatus.goOnWin = NetUtil.GetJsonInt(jsonObject, "goOnWin");
			serverDailyBattleStatus.goOnLose = NetUtil.GetJsonInt(jsonObject, "goOnLose");
		}
		return serverDailyBattleStatus;
	}

	// Token: 0x0600387A RID: 14458 RVA: 0x0012A134 File Offset: 0x00128334
	public static List<ServerDailyBattlePrizeData> AnalyzeDailyBattlePrizeDataJson(JsonData jdata, string key)
	{
		List<ServerDailyBattlePrizeData> list = new List<ServerDailyBattlePrizeData>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, key);
		if (jsonArray == null)
		{
			return list;
		}
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jsonData = jsonArray[i];
			if (jsonData != null)
			{
				ServerDailyBattlePrizeData serverDailyBattlePrizeData = new ServerDailyBattlePrizeData();
				serverDailyBattlePrizeData.operatorData = NetUtil.GetJsonInt(jsonData, "operator");
				serverDailyBattlePrizeData.number = NetUtil.GetJsonInt(jsonData, "number");
				JsonData jsonArray2 = NetUtil.GetJsonArray(jsonData, "presentList");
				int count2 = jsonArray2.Count;
				for (int j = 0; j < count2; j++)
				{
					JsonData jsonData2 = jsonArray2[j];
					if (jsonData2 != null)
					{
						ServerItemState itemState = NetUtil.AnalyzeItemStateJson(jsonData2, string.Empty);
						serverDailyBattlePrizeData.AddItemState(itemState);
					}
				}
				list.Add(serverDailyBattlePrizeData);
			}
		}
		return list;
	}

	// Token: 0x0600387B RID: 14459 RVA: 0x0012A210 File Offset: 0x00128410
	public static DateTime AnalyzeDateTimeJson(JsonData jdata, string key)
	{
		long jsonLong = NetUtil.GetJsonLong(jdata, key);
		return NetUtil.GetLocalDateTime(jsonLong);
	}

	// Token: 0x0600387C RID: 14460 RVA: 0x0012A22C File Offset: 0x0012842C
	public static ServerPlayerState AnalyzePlayerStateJson(JsonData jdata, string key)
	{
		JsonData jsonObject = NetUtil.GetJsonObject(jdata, key);
		if (jsonObject == null)
		{
			return null;
		}
		ServerPlayerState serverPlayerState = new ServerPlayerState();
		NetUtil.AnalyzePlayerState_Scores(jsonObject, ref serverPlayerState);
		NetUtil.AnalyzePlayerState_Rings(jsonObject, ref serverPlayerState);
		NetUtil.AnalyzePlayerState_Enegies(jsonObject, ref serverPlayerState);
		NetUtil.AnalyzePlayerState_Messages(jsonObject, ref serverPlayerState);
		NetUtil.AnalyzePlayerState_DailyChallenge(jsonObject, ref serverPlayerState);
		ServerCharacterState[] array = NetUtil.AnalyzePlayerState_CharactersStates(jsonObject);
		foreach (ServerCharacterState serverCharacterState in array)
		{
			if (serverCharacterState != null)
			{
				serverPlayerState.SetCharacterState(serverCharacterState);
			}
		}
		List<ServerChaoState> list = NetUtil.AnalyzePlayerState_ChaoStates(jsonObject);
		foreach (ServerChaoState serverChaoState in list)
		{
			if (serverChaoState != null)
			{
				serverPlayerState.ChaoStates.Add(serverChaoState);
			}
		}
		NetUtil.AnalyzePlayerState_ItemsStates(jsonObject, ref serverPlayerState);
		NetUtil.AnalyzePlayerState_EquipItemList(jsonObject, ref serverPlayerState);
		NetUtil.AnalyzePlayerState_Other(jsonObject, ref serverPlayerState);
		return serverPlayerState;
	}

	// Token: 0x0600387D RID: 14461 RVA: 0x0012A340 File Offset: 0x00128540
	private static void AnalyzePlayerState_Scores(JsonData jdata, ref ServerPlayerState playerState)
	{
		playerState.m_totalHighScore = NetUtil.GetJsonLong(jdata, "totalHighScore");
		playerState.m_totalHighScoreQuick = NetUtil.GetJsonLong(jdata, "quickTotalHighScore");
		playerState.m_totalDistance = NetUtil.GetJsonLong(jdata, "totalDistance");
		playerState.m_maxDistance = NetUtil.GetJsonLong(jdata, "maximumDistance");
		playerState.m_leagueIndex = NetUtil.GetJsonInt(jdata, "rankingLeague");
		playerState.m_leagueIndexQuick = NetUtil.GetJsonInt(jdata, "quickRankingLeague");
	}

	// Token: 0x0600387E RID: 14462 RVA: 0x0012A3BC File Offset: 0x001285BC
	private static void AnalyzePlayerState_Rings(JsonData jdata, ref ServerPlayerState playerState)
	{
		playerState.m_numFreeRings = NetUtil.GetJsonInt(jdata, "numRings");
		playerState.m_numBuyRings = NetUtil.GetJsonInt(jdata, "numBuyRings");
		playerState.m_numRings = playerState.m_numFreeRings + playerState.m_numBuyRings;
		playerState.m_numFreeRedRings = NetUtil.GetJsonInt(jdata, "numRedRings");
		playerState.m_numBuyRedRings = NetUtil.GetJsonInt(jdata, "numBuyRedRings");
		playerState.m_numRedRings = playerState.m_numFreeRedRings + playerState.m_numBuyRedRings;
	}

	// Token: 0x0600387F RID: 14463 RVA: 0x0012A440 File Offset: 0x00128640
	private static void AnalyzePlayerState_Enegies(JsonData jdata, ref ServerPlayerState playerState)
	{
		playerState.m_numFreeEnergy = NetUtil.GetJsonInt(jdata, "energy");
		playerState.m_numBuyEnergy = NetUtil.GetJsonInt(jdata, "energyBuy");
		playerState.m_numEnergy = playerState.m_numFreeEnergy + playerState.m_numBuyEnergy;
		long jsonLong = NetUtil.GetJsonLong(jdata, "energyRenewsAt");
		playerState.m_energyRenewsAt = NetUtil.GetLocalDateTime(jsonLong);
	}

	// Token: 0x06003880 RID: 14464 RVA: 0x0012A4A0 File Offset: 0x001286A0
	private static void AnalyzePlayerState_Messages(JsonData jdata, ref ServerPlayerState playerState)
	{
		playerState.m_numUnreadMessages = NetUtil.GetJsonInt(jdata, "mumMessages");
	}

	// Token: 0x06003881 RID: 14465 RVA: 0x0012A4B4 File Offset: 0x001286B4
	private static void AnalyzePlayerState_DailyChallenge(JsonData jdata, ref ServerPlayerState playerState)
	{
		playerState.m_dailyMissionId = NetUtil.GetJsonInt(jdata, "dailyMissionId");
		playerState.m_dailyMissionValue = NetUtil.GetJsonInt(jdata, "dailyChallengeValue");
		playerState.m_dailyChallengeComplete = NetUtil.GetJsonFlag(jdata, "dailyChallengeComplete");
		long jsonLong = NetUtil.GetJsonLong(jdata, "dailyMissionEndTime");
		playerState.m_endDailyMissionDate = NetUtil.GetLocalDateTime(jsonLong);
		playerState.m_numDailyChalCont = NetUtil.GetJsonInt(jdata, "numDailyChalCont");
	}

	// Token: 0x06003882 RID: 14466 RVA: 0x0012A524 File Offset: 0x00128724
	public static ServerCharacterState[] AnalyzePlayerState_CharactersStates(JsonData jdata, string arrayName)
	{
		ServerCharacterState[] array = new ServerCharacterState[29];
		for (int i = 0; i < 29; i++)
		{
			array[i] = new ServerCharacterState();
		}
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, arrayName);
		if (jsonArray == null)
		{
			return array;
		}
		int count = jsonArray.Count;
		for (int j = 0; j < count; j++)
		{
			JsonData jsonData = jsonArray[j];
			int jsonInt = NetUtil.GetJsonInt(jsonData, "characterId");
			ServerItem serverItem = new ServerItem((ServerItem.Id)jsonInt);
			CharaType charaType = serverItem.charaType;
			ServerCharacterState serverCharacterState = array[(int)charaType];
			if (serverCharacterState != null)
			{
				serverCharacterState.Id = jsonInt;
				serverCharacterState.Status = (ServerCharacterState.CharacterStatus)NetUtil.GetJsonInt(jsonData, "status");
				serverCharacterState.OldStatus = serverCharacterState.Status;
				serverCharacterState.Level = NetUtil.GetJsonInt(jsonData, "level");
				serverCharacterState.Cost = NetUtil.GetJsonInt(jsonData, "numRings");
				serverCharacterState.OldCost = serverCharacterState.Cost;
				serverCharacterState.NumRedRings = NetUtil.GetJsonInt(jsonData, "numRedRings");
				JsonData jsonArray2 = NetUtil.GetJsonArray(jsonData, "abilityLevel");
				int count2 = jsonArray2.Count;
				serverCharacterState.AbilityLevel.Clear();
				for (int k = 0; k < count2; k++)
				{
					serverCharacterState.AbilityLevel.Add(NetUtil.GetJsonInt(jsonArray2[k]));
					serverCharacterState.OldAbiltyLevel.Add(NetUtil.GetJsonInt(jsonArray2[k]));
				}
				serverCharacterState.Condition = (ServerCharacterState.LockCondition)NetUtil.GetJsonInt(jsonData, "lockCondition");
				serverCharacterState.star = NetUtil.GetJsonInt(jsonData, "star");
				serverCharacterState.starMax = NetUtil.GetJsonInt(jsonData, "starMax");
				serverCharacterState.priceNumRings = NetUtil.GetJsonInt(jsonData, "priceNumRings");
				serverCharacterState.priceNumRedRings = NetUtil.GetJsonInt(jsonData, "priceNumRedRings");
				serverCharacterState.Exp = NetUtil.GetJsonInt(jsonData, "exp");
				serverCharacterState.OldExp = serverCharacterState.Exp;
				if (NetUtil.IsExist(jsonData, "campaignList"))
				{
					JsonData jsonArray3 = NetUtil.GetJsonArray(jsonData, "campaignList");
					if (jsonArray3 != null)
					{
						ServerCampaignData serverCampaignData = new ServerCampaignData();
						serverCampaignData.campaignType = Constants.Campaign.emType.CharacterUpgradeCost;
						serverCampaignData.id = serverCharacterState.Id;
						ServerInterface.CampaignState.RemoveCampaign(serverCampaignData);
						int count3 = jsonArray3.Count;
						for (int l = 0; l < count3; l++)
						{
							ServerCampaignData serverCampaignData2 = NetUtil.AnalyzeCampaignDataJson(jsonArray3[l], string.Empty);
							if (serverCampaignData2 != null)
							{
								serverCampaignData2.id = serverCharacterState.Id;
								ServerInterface.CampaignState.RegistCampaign(serverCampaignData2);
							}
						}
					}
				}
			}
		}
		return array;
	}

	// Token: 0x06003883 RID: 14467 RVA: 0x0012A7C8 File Offset: 0x001289C8
	public static ServerPlayCharacterState[] AnalyzePlayerState_PlayCharactersStates(JsonData jdata)
	{
		ServerCharacterState[] array = NetUtil.AnalyzePlayerState_CharactersStates(jdata, "playCharacterState");
		List<ServerPlayCharacterState> list = new List<ServerPlayCharacterState>();
		foreach (ServerCharacterState serverCharacterState in array)
		{
			if (serverCharacterState != null)
			{
				ServerPlayCharacterState serverPlayCharacterState = ServerPlayCharacterState.CreatePlayCharacterState(serverCharacterState);
				if (serverPlayCharacterState != null)
				{
					list.Add(serverPlayCharacterState);
				}
			}
		}
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "playCharacterState");
		if (jsonArray == null)
		{
			return list.ToArray();
		}
		int count = jsonArray.Count;
		for (int j = 0; j < count; j++)
		{
			JsonData jsonData = jsonArray[j];
			if (jsonData != null)
			{
				if (j >= list.Count)
				{
					break;
				}
				int jsonInt = NetUtil.GetJsonInt(jsonArray[j], "characterId");
				ServerItem serverItem = new ServerItem((ServerItem.Id)jsonInt);
				CharaType charaType = serverItem.charaType;
				ServerPlayCharacterState serverPlayCharacterState2 = list[(int)charaType];
				if (serverPlayCharacterState2 != null)
				{
					serverPlayCharacterState2.star = NetUtil.GetJsonInt(jsonData, "star");
					serverPlayCharacterState2.starMax = NetUtil.GetJsonInt(jsonData, "starMax");
					serverPlayCharacterState2.priceNumRings = NetUtil.GetJsonInt(jsonData, "priceNumRings");
					serverPlayCharacterState2.priceNumRedRings = NetUtil.GetJsonInt(jsonData, "priceNumRedRings");
					JsonData jsonArray2 = NetUtil.GetJsonArray(jsonData, "abilityLevelup");
					int count2 = jsonArray2.Count;
					serverPlayCharacterState2.abilityLevelUp.Clear();
					for (int k = 0; k < count2; k++)
					{
						ServerItem serverItem2 = new ServerItem((ServerItem.Id)NetUtil.GetJsonInt(jsonArray2[k]));
						serverPlayCharacterState2.abilityLevelUp.Add((int)serverItem2.abilityType);
					}
					if (NetUtil.IsExist(jsonData, "abilityLevelupExp"))
					{
						JsonData jsonArray3 = NetUtil.GetJsonArray(jsonData, "abilityLevelupExp");
						int count3 = jsonArray3.Count;
						serverPlayCharacterState2.abilityLevelUpExp.Clear();
						for (int l = 0; l < count3; l++)
						{
							int jsonInt2 = NetUtil.GetJsonInt(jsonArray3[l]);
							serverPlayCharacterState2.abilityLevelUpExp.Add(jsonInt2);
						}
					}
				}
			}
		}
		return list.ToArray();
	}

	// Token: 0x06003884 RID: 14468 RVA: 0x0012A9F0 File Offset: 0x00128BF0
	public static ServerCharacterState[] AnalyzePlayerState_CharactersStates(JsonData jdata)
	{
		return NetUtil.AnalyzePlayerState_CharactersStates(jdata, "characterState");
	}

	// Token: 0x06003885 RID: 14469 RVA: 0x0012AA00 File Offset: 0x00128C00
	public static List<ServerChaoState> AnalyzePlayerState_ChaoStates(JsonData jdata)
	{
		List<ServerChaoState> list = new List<ServerChaoState>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "chaoState");
		if (jsonArray == null)
		{
			jsonArray = NetUtil.GetJsonArray(jdata, "chaoStatus");
		}
		if (jsonArray == null)
		{
			return list;
		}
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			int jsonInt = NetUtil.GetJsonInt(jdata2, "chaoId");
			list.Add(new ServerChaoState
			{
				Id = jsonInt,
				Status = (ServerChaoState.ChaoStatus)NetUtil.GetJsonInt(jdata2, "status"),
				Level = NetUtil.GetJsonInt(jdata2, "level"),
				Dealing = (ServerChaoState.ChaoDealing)NetUtil.GetJsonInt(jdata2, "setStatus"),
				Rarity = NetUtil.GetJsonInt(jdata2, "rarity"),
				NumInvite = NetUtil.GetJsonInt(jdata2, "numInvite"),
				NumAcquired = NetUtil.GetJsonInt(jdata2, "acquired"),
				Hidden = NetUtil.GetJsonFlag(jdata2, "hidden")
			});
		}
		return list;
	}

	// Token: 0x06003886 RID: 14470 RVA: 0x0012AB08 File Offset: 0x00128D08
	private static void AnalyzePlayerState_ItemsStates(JsonData jdata, ref ServerPlayerState playerState)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "items");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerItemState itemState = NetUtil.AnalyzeItemStateJson(jdata2, string.Empty);
			playerState.AddItemState(itemState);
		}
	}

	// Token: 0x06003887 RID: 14471 RVA: 0x0012AB58 File Offset: 0x00128D58
	private static void AnalyzePlayerState_EquipItemList(JsonData jdata, ref ServerPlayerState playerState)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "equipItemList");
		for (int i = 0; i < playerState.m_equipItemList.Length; i++)
		{
			if (i < jsonArray.Count)
			{
				int jsonInt = NetUtil.GetJsonInt(jsonArray[i]);
				playerState.m_equipItemList[i] = jsonInt;
			}
			else
			{
				playerState.m_equipItemList[i] = -1;
			}
		}
	}

	// Token: 0x06003888 RID: 14472 RVA: 0x0012ABC0 File Offset: 0x00128DC0
	private static void AnalyzePlayerState_Other(JsonData jdata, ref ServerPlayerState playerState)
	{
		playerState.m_numContinuesUsed = NetUtil.GetJsonInt(jdata, "numContinuesUsed");
		playerState.m_mainCharaId = NetUtil.GetJsonInt(jdata, "mainCharaID");
		playerState.m_subCharaId = NetUtil.GetJsonInt(jdata, "subCharaID");
		playerState.m_useSubCharacter = NetUtil.GetJsonFlag(jdata, "useSubCharacter");
		playerState.m_mainChaoId = NetUtil.GetJsonInt(jdata, "mainChaoID");
		playerState.m_subChaoId = NetUtil.GetJsonInt(jdata, "subChaoID");
		playerState.m_numPlaying = NetUtil.GetJsonInt(jdata, "numPlaying");
		playerState.m_numAnimals = NetUtil.GetJsonInt(jdata, "numAnimals");
		playerState.m_numRank = NetUtil.GetJsonInt(jdata, "numRank");
	}

	// Token: 0x06003889 RID: 14473 RVA: 0x0012AC70 File Offset: 0x00128E70
	public static List<ServerWheelSpinInfo> AnalyzeWheelSpinInfo(JsonData jdata, string key)
	{
		List<ServerWheelSpinInfo> list = new List<ServerWheelSpinInfo>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, key);
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			list.Add(new ServerWheelSpinInfo
			{
				id = NetUtil.GetJsonInt(jdata2, "id"),
				start = NetUtil.GetLocalDateTime(NetUtil.GetJsonLong(jdata2, "start")),
				end = NetUtil.GetLocalDateTime(NetUtil.GetJsonLong(jdata2, "end")),
				param = NetUtil.GetJsonString(jdata2, "param")
			});
		}
		return list;
	}

	// Token: 0x0600388A RID: 14474 RVA: 0x0012AD1C File Offset: 0x00128F1C
	public static ServerWheelOptions AnalyzeWheelOptionsJson(JsonData jdata, string key)
	{
		JsonData jsonObject = NetUtil.GetJsonObject(jdata, key);
		if (jsonObject == null)
		{
			return null;
		}
		ServerWheelOptions result = new ServerWheelOptions(null);
		NetUtil.AnalyzeWheelOptions_Items(jsonObject, ref result);
		NetUtil.AnalyzeWheelOptions_Other(jsonObject, ref result);
		return result;
	}

	// Token: 0x0600388B RID: 14475 RVA: 0x0012AD54 File Offset: 0x00128F54
	private static void AnalyzeWheelOptions_Items(JsonData jdata, ref ServerWheelOptions wheelOptions)
	{
		wheelOptions.m_itemWon = NetUtil.GetJsonInt(jdata, "itemWon");
		wheelOptions.ResetItemList();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "itemList");
		if (jsonArray != null)
		{
			int count = jsonArray.Count;
			for (int i = 0; i < count; i++)
			{
				JsonData jdata2 = jsonArray[i];
				ServerItemState serverItemState = NetUtil.AnalyzeItemStateJson(jdata2, string.Empty);
				if (serverItemState != null)
				{
					wheelOptions.AddItemList(serverItemState);
				}
			}
		}
		JsonData jsonArray2 = NetUtil.GetJsonArray(jdata, "items");
		int count2 = jsonArray2.Count;
		for (int j = 0; j < count2; j++)
		{
			int jsonInt = NetUtil.GetJsonInt(jsonArray2[j]);
			wheelOptions.m_items[j] = jsonInt;
		}
		JsonData jsonArray3 = NetUtil.GetJsonArray(jdata, "item");
		int count3 = jsonArray3.Count;
		for (int k = 0; k < count3; k++)
		{
			int jsonInt2 = NetUtil.GetJsonInt(jsonArray3[k]);
			wheelOptions.m_itemQuantities[k] = jsonInt2;
		}
		JsonData jsonArray4 = NetUtil.GetJsonArray(jdata, "itemWeight");
		int count4 = jsonArray4.Count;
		for (int l = 0; l < count4; l++)
		{
			int jsonInt3 = NetUtil.GetJsonInt(jsonArray4[l]);
			wheelOptions.m_itemWeight[l] = jsonInt3;
		}
		if (wheelOptions.NumRequiredSpEggs > 0)
		{
			RouletteManager.Instance.specialEgg = RouletteManager.Instance.specialEgg + wheelOptions.NumRequiredSpEggs;
		}
	}

	// Token: 0x0600388C RID: 14476 RVA: 0x0012AED4 File Offset: 0x001290D4
	private static void AnalyzeWheelOptions_Other(JsonData jdata, ref ServerWheelOptions wheelOptions)
	{
		long jsonLong = NetUtil.GetJsonLong(jdata, "nextFreeSpin");
		wheelOptions.m_nextFreeSpin = NetUtil.GetLocalDateTime(jsonLong);
		wheelOptions.m_spinCost = NetUtil.GetJsonInt(jdata, "spinCost");
		wheelOptions.m_rouletteRank = (RouletteUtility.WheelRank)NetUtil.GetJsonInt(jdata, "rouletteRank");
		wheelOptions.m_numRouletteToken = NetUtil.GetJsonInt(jdata, "numRouletteToken");
		wheelOptions.m_numJackpotRing = NetUtil.GetJsonInt(jdata, "numJackpotRing");
		wheelOptions.m_numRemaining = NetUtil.GetJsonInt(jdata, "numRemainingRoulette");
		if (NetUtil.IsExist(jdata, "campaignList"))
		{
			JsonData jsonArray = NetUtil.GetJsonArray(jdata, "campaignList");
			if (jsonArray != null)
			{
				int count = jsonArray.Count;
				for (int i = 0; i < count; i++)
				{
					ServerCampaignData serverCampaignData = NetUtil.AnalyzeCampaignDataJson(jsonArray[i], string.Empty);
					if (serverCampaignData != null)
					{
						ServerInterface.CampaignState.RegistCampaign(serverCampaignData);
					}
				}
			}
		}
		GeneralUtil.SetItemCount(ServerItem.Id.ROULLETE_TICKET_ITEM, (long)wheelOptions.m_numRouletteToken);
	}

	// Token: 0x0600388D RID: 14477 RVA: 0x0012AFC8 File Offset: 0x001291C8
	public static ServerWheelOptionsGeneral AnalyzeWheelOptionsGeneralJson(JsonData jdata, string key)
	{
		JsonData jsonObject = NetUtil.GetJsonObject(jdata, key);
		if (jsonObject == null)
		{
			return null;
		}
		ServerWheelOptionsGeneral result = new ServerWheelOptionsGeneral();
		NetUtil.AnalyzeChaoWheelOptionsGeneral_Items(jsonObject, ref result);
		NetUtil.AnalyzeChaoWheelOptionsGeneral_Other(jsonObject, ref result);
		return result;
	}

	// Token: 0x0600388E RID: 14478 RVA: 0x0012AFFC File Offset: 0x001291FC
	private static void AnalyzeChaoWheelOptionsGeneral_Items(JsonData jdata, ref ServerWheelOptionsGeneral chaoWheelOptions)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "items");
		JsonData jsonArray2 = NetUtil.GetJsonArray(jdata, "item");
		JsonData jsonArray3 = NetUtil.GetJsonArray(jdata, "itemWeight");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			int jsonInt = NetUtil.GetJsonInt(jsonArray[i]);
			int num = 1;
			int weight = 1;
			if (jsonArray2 != null && jsonArray2.Count > i)
			{
				num = NetUtil.GetJsonInt(jsonArray2[i]);
			}
			if (jsonArray3 != null && jsonArray3.Count > i)
			{
				weight = NetUtil.GetJsonInt(jsonArray3[i]);
			}
			chaoWheelOptions.SetupItem(i, jsonInt, weight, num);
		}
	}

	// Token: 0x0600388F RID: 14479 RVA: 0x0012B0B4 File Offset: 0x001292B4
	private static void AnalyzeChaoWheelOptionsGeneral_Other(JsonData jdata, ref ServerWheelOptionsGeneral wheelOptionsGeneral)
	{
		int jsonInt = NetUtil.GetJsonInt(jdata, "spinID");
		int jsonInt2 = NetUtil.GetJsonInt(jdata, "rouletteRank");
		int jsonInt3 = NetUtil.GetJsonInt(jdata, "numRemainingRoulette");
		int jsonInt4 = NetUtil.GetJsonInt(jdata, "numJackpotRing");
		int jsonInt5 = NetUtil.GetJsonInt(jdata, "numSpecialEgg");
		long jsonLong = NetUtil.GetJsonLong(jdata, "nextFreeSpin");
		if (RouletteManager.Instance != null)
		{
			RouletteManager.Instance.specialEgg = jsonInt5;
		}
		if (NetUtil.IsExist(jdata, "campaignList"))
		{
			JsonData jsonArray = NetUtil.GetJsonArray(jdata, "campaignList");
			if (jsonArray != null)
			{
				int count = jsonArray.Count;
				for (int i = 0; i < count; i++)
				{
					ServerCampaignData serverCampaignData = NetUtil.AnalyzeCampaignDataJson(jsonArray[i], string.Empty);
					if (serverCampaignData != null)
					{
						serverCampaignData.id = serverCampaignData.iSubContent;
						ServerInterface.CampaignState.RegistCampaign(serverCampaignData);
					}
				}
			}
		}
		wheelOptionsGeneral.SetupParam(jsonInt, jsonInt3, jsonInt4, jsonInt2, jsonInt5, NetUtil.GetLocalDateTime(jsonLong));
		wheelOptionsGeneral.ResetupCostItem();
		JsonData jsonArray2 = NetUtil.GetJsonArray(jdata, "costItemList");
		int count2 = jsonArray2.Count;
		for (int j = 0; j < count2; j++)
		{
			JsonData jdata2 = jsonArray2[j];
			int jsonInt6 = NetUtil.GetJsonInt(jdata2, "itemId");
			int jsonInt7 = NetUtil.GetJsonInt(jdata2, "numItem");
			int jsonInt8 = NetUtil.GetJsonInt(jdata2, "itemStock");
			if (jsonInt6 > 0)
			{
				wheelOptionsGeneral.AddCostItem(jsonInt6, jsonInt8, jsonInt7);
			}
		}
	}

	// Token: 0x06003890 RID: 14480 RVA: 0x0012B234 File Offset: 0x00129434
	public static ServerChaoWheelOptions AnalyzeChaoWheelOptionsJson(JsonData jdata, string key)
	{
		JsonData jsonObject = NetUtil.GetJsonObject(jdata, key);
		if (jsonObject == null)
		{
			return null;
		}
		ServerChaoWheelOptions result = new ServerChaoWheelOptions();
		NetUtil.AnalyzeChaoWheelOptions_Items(jsonObject, ref result);
		NetUtil.AnalyzeChaoWheelOptions_Other(jsonObject, ref result);
		if (NetUtil.IsExist(jsonObject, "campaignList"))
		{
			JsonData jsonArray = NetUtil.GetJsonArray(jsonObject, "campaignList");
			if (jsonArray != null)
			{
				int count = jsonArray.Count;
				for (int i = 0; i < count; i++)
				{
					ServerCampaignData serverCampaignData = NetUtil.AnalyzeCampaignDataJson(jsonArray[i], string.Empty);
					if (serverCampaignData != null)
					{
						serverCampaignData.id = serverCampaignData.iSubContent;
						ServerInterface.CampaignState.RegistCampaign(serverCampaignData);
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06003891 RID: 14481 RVA: 0x0012B2E0 File Offset: 0x001294E0
	private static void AnalyzeChaoWheelOptions_Items(JsonData jdata, ref ServerChaoWheelOptions chaoWheelOptions)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "rarity");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			chaoWheelOptions.Rarities[i] = NetUtil.GetJsonInt(jsonArray[i]);
		}
		JsonData jsonArray2 = NetUtil.GetJsonArray(jdata, "itemWeight");
		int count2 = jsonArray2.Count;
		for (int j = 0; j < count2; j++)
		{
			chaoWheelOptions.ItemWeight[j] = NetUtil.GetJsonInt(jsonArray2[j]);
		}
	}

	// Token: 0x06003892 RID: 14482 RVA: 0x0012B36C File Offset: 0x0012956C
	private static void AnalyzeChaoWheelOptions_Other(JsonData jdata, ref ServerChaoWheelOptions chaoWheelOptions)
	{
		chaoWheelOptions.Cost = NetUtil.GetJsonInt(jdata, "spinCost");
		chaoWheelOptions.SpinType = (ServerChaoWheelOptions.ChaoSpinType)NetUtil.GetJsonInt(jdata, "chaoRouletteType");
		chaoWheelOptions.NumSpecialEggs = NetUtil.GetJsonInt(jdata, "numSpecialEgg");
		chaoWheelOptions.IsValid = (NetUtil.GetJsonInt(jdata, "rouletteAvailable") != 0);
		chaoWheelOptions.NumRouletteToken = NetUtil.GetJsonInt(jdata, "numChaoRouletteToken");
		chaoWheelOptions.IsTutorial = (NetUtil.GetJsonInt(jdata, "numChaoRoulette") == 0);
		if (RouletteManager.Instance != null)
		{
			RouletteManager.Instance.specialEgg = chaoWheelOptions.NumSpecialEggs;
		}
		GeneralUtil.SetItemCount(ServerItem.Id.ROULLETE_TICKET_PREMIAM, (long)chaoWheelOptions.NumRouletteToken);
	}

	// Token: 0x06003893 RID: 14483 RVA: 0x0012B430 File Offset: 0x00129630
	public static ServerSpinResultGeneral AnalyzeSpinResultJson(JsonData jdata, string key)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, key);
		ServerSpinResultGeneral serverSpinResultGeneral = null;
		if (jsonArray != null)
		{
			int count = jsonArray.Count;
			if (count > 0)
			{
				int itemWon = 0;
				serverSpinResultGeneral = new ServerSpinResultGeneral();
				for (int i = 0; i < count; i++)
				{
					JsonData jsonData = jsonArray[i];
					if (jsonData != null)
					{
						ServerChaoData serverChaoData = NetUtil.AnalyzeChaoDataJson(jsonData, "getChao");
						if (serverChaoData != null)
						{
							serverSpinResultGeneral.AddChaoState(serverChaoData);
						}
						else
						{
							ServerChaoData serverChaoData2 = NetUtil.AnalyzeItemDataJson(jsonData, "getItem");
							if (serverChaoData2 != null)
							{
								serverSpinResultGeneral.AddChaoState(serverChaoData2);
							}
						}
						JsonData jsonArray2 = NetUtil.GetJsonArray(jsonData, "itemList");
						int count2 = jsonArray2.Count;
						for (int j = 0; j < count2; j++)
						{
							JsonData jdata2 = jsonArray2[j];
							ServerItemState serverItemState = NetUtil.AnalyzeItemStateJson(jdata2, string.Empty);
							if (serverItemState != null)
							{
								serverSpinResultGeneral.AddItemState(serverItemState);
							}
						}
						itemWon = NetUtil.GetJsonInt(jsonData, "itemWon");
					}
				}
				if (count > 1)
				{
					serverSpinResultGeneral.ItemWon = -1;
				}
				else
				{
					serverSpinResultGeneral.ItemWon = itemWon;
				}
			}
		}
		return serverSpinResultGeneral;
	}

	// Token: 0x06003894 RID: 14484 RVA: 0x0012B54C File Offset: 0x0012974C
	public static ServerSpinResultGeneral AnalyzeSpinResultGeneralJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		ServerSpinResultGeneral serverSpinResultGeneral = new ServerSpinResultGeneral();
		JsonData jsonArray = NetUtil.GetJsonArray(jsonData, "getChao");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerChaoData serverChaoData = NetUtil.AnalyzeChaoDataJson(jdata2, string.Empty);
			if (serverChaoData != null)
			{
				serverSpinResultGeneral.AddChaoState(serverChaoData);
			}
		}
		JsonData jsonArray2 = NetUtil.GetJsonArray(jsonData, "itemList");
		int count2 = jsonArray2.Count;
		for (int j = 0; j < count2; j++)
		{
			JsonData jdata3 = jsonArray2[j];
			ServerItemState serverItemState = NetUtil.AnalyzeItemStateJson(jdata3, string.Empty);
			if (serverItemState != null)
			{
				serverSpinResultGeneral.AddItemState(serverItemState);
			}
		}
		serverSpinResultGeneral.ItemWon = NetUtil.GetJsonInt(jsonData, "itemWon");
		return serverSpinResultGeneral;
	}

	// Token: 0x06003895 RID: 14485 RVA: 0x0012B648 File Offset: 0x00129848
	public static ServerChaoSpinResult AnalyzeChaoSpinResultJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		ServerChaoSpinResult serverChaoSpinResult = new ServerChaoSpinResult();
		serverChaoSpinResult.AcquiredChaoData = NetUtil.AnalyzeChaoDataJson(jsonData, "getChao");
		serverChaoSpinResult.NumRequiredSpEggs = 0;
		JsonData jsonArray = NetUtil.GetJsonArray(jsonData, "itemList");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerItemState serverItemState = NetUtil.AnalyzeItemStateJson(jdata2, string.Empty);
			if (serverItemState != null)
			{
				serverChaoSpinResult.AddItemState(serverItemState);
				if (serverItemState.m_itemId == 220000)
				{
					serverChaoSpinResult.NumRequiredSpEggs += serverItemState.m_num;
				}
			}
		}
		serverChaoSpinResult.ItemWon = NetUtil.GetJsonInt(jsonData, "itemWon");
		return serverChaoSpinResult;
	}

	// Token: 0x06003896 RID: 14486 RVA: 0x0012B728 File Offset: 0x00129928
	public static ServerChaoData AnalyzeChaoDataJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		return new ServerChaoData
		{
			Id = NetUtil.GetJsonInt(jsonData, "chaoId"),
			Level = NetUtil.GetJsonInt(jsonData, "level"),
			Rarity = NetUtil.GetJsonInt(jsonData, "rarity")
		};
	}

	// Token: 0x06003897 RID: 14487 RVA: 0x0012B798 File Offset: 0x00129998
	public static ServerChaoData AnalyzeItemDataJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		return new ServerChaoData
		{
			Id = NetUtil.GetJsonInt(jsonData, "itemId"),
			Level = NetUtil.GetJsonInt(jsonData, "level_after"),
			Rarity = NetUtil.GetJsonInt(jsonData, "rarity")
		};
	}

	// Token: 0x06003898 RID: 14488 RVA: 0x0012B808 File Offset: 0x00129A08
	public static ServerChaoRentalState AnalyzeChaoRentalStateJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		return new ServerChaoRentalState
		{
			FriendId = NetUtil.GetJsonString(jsonData, "friendId"),
			Name = NetUtil.GetJsonString(jsonData, "name"),
			Url = NetUtil.GetJsonString(jsonData, "url"),
			ChaoData = NetUtil.AnalyzeChaoDataJson(jsonData, "chaoData"),
			RentalState = NetUtil.GetJsonInt(jsonData, "rentalFlg"),
			NextRentalAt = (long)NetUtil.GetJsonInt(jsonData, "nextRentalAt")
		};
	}

	// Token: 0x06003899 RID: 14489 RVA: 0x0012B8AC File Offset: 0x00129AAC
	public static ServerPrizeState AnalyzePrizeChaoWheelSpin(JsonData jdata)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "prizeList");
		if (jsonArray == null)
		{
			return null;
		}
		ServerPrizeState serverPrizeState = new ServerPrizeState();
		for (int i = 0; i < jsonArray.Count; i++)
		{
			serverPrizeState.AddPrize(new ServerPrizeData
			{
				itemId = NetUtil.GetJsonInt(jsonArray[i], "chao_id"),
				num = 1,
				weight = 1
			});
		}
		return serverPrizeState;
	}

	// Token: 0x0600389A RID: 14490 RVA: 0x0012B920 File Offset: 0x00129B20
	public static ServerPrizeState AnalyzePrizeWheelSpinGeneral(JsonData jdata)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "raidbossPrizeList");
		if (jsonArray == null)
		{
			return null;
		}
		ServerPrizeState serverPrizeState = new ServerPrizeState();
		for (int i = 0; i < jsonArray.Count; i++)
		{
			serverPrizeState.AddPrize(new ServerPrizeData
			{
				itemId = NetUtil.GetJsonInt(jsonArray[i], "itemId"),
				num = NetUtil.GetJsonInt(jsonArray[i], "numItem"),
				weight = NetUtil.GetJsonInt(jsonArray[i], "itemRate"),
				spinId = NetUtil.GetJsonInt(jsonArray[i], "spinId")
			});
		}
		return serverPrizeState;
	}

	// Token: 0x0600389B RID: 14491 RVA: 0x0012B9CC File Offset: 0x00129BCC
	public static ServerLeaderboardEntry AnalyzeLeaderboardEntryJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		return new ServerLeaderboardEntry
		{
			m_hspId = NetUtil.GetJsonString(jsonData, "friendId"),
			m_score = NetUtil.GetJsonLong(jsonData, "rankingScore"),
			m_hiScore = NetUtil.GetJsonLong(jsonData, "maxScore"),
			m_userData = NetUtil.GetJsonInt(jsonData, "userData"),
			m_name = NetUtil.GetJsonString(jsonData, "name"),
			m_url = NetUtil.GetJsonString(jsonData, "url"),
			m_energyFlg = NetUtil.GetJsonFlag(jsonData, "energyFlg"),
			m_grade = NetUtil.GetJsonInt(jsonData, "grade"),
			m_gradeChanged = NetUtil.GetJsonInt(jsonData, "rankChanged"),
			m_expireTime = NetUtil.GetJsonLong(jsonData, "expireTime"),
			m_numRank = NetUtil.GetJsonInt(jsonData, "numRank"),
			m_loginTime = (long)NetUtil.GetJsonInt(jsonData, "loginTime"),
			m_charaId = NetUtil.GetJsonInt(jsonData, "charaId"),
			m_charaLevel = NetUtil.GetJsonInt(jsonData, "charaLevel"),
			m_subCharaId = NetUtil.GetJsonInt(jsonData, "subCharaId"),
			m_subCharaLevel = NetUtil.GetJsonInt(jsonData, "subCharaLevel"),
			m_mainChaoId = NetUtil.GetJsonInt(jsonData, "mainChaoId"),
			m_mainChaoLevel = NetUtil.GetJsonInt(jsonData, "mainChaoLevel"),
			m_subChaoId = NetUtil.GetJsonInt(jsonData, "subChaoId"),
			m_subChaoLevel = NetUtil.GetJsonInt(jsonData, "subChaoLevel"),
			m_leagueIndex = NetUtil.GetJsonInt(jsonData, "league"),
			m_language = (Env.Language)NetUtil.GetJsonInt(jsonData, "language")
		};
	}

	// Token: 0x0600389C RID: 14492 RVA: 0x0012BB80 File Offset: 0x00129D80
	public static ServerMileageFriendEntry AnalyzeMileageFriendEntryJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		return new ServerMileageFriendEntry
		{
			m_friendId = NetUtil.GetJsonString(jsonData, "friendId"),
			m_name = NetUtil.GetJsonString(jsonData, "name"),
			m_url = NetUtil.GetJsonString(jsonData, "url"),
			m_mapState = NetUtil.AnalyzeMileageMapStateJson(jsonData, "mapState")
		};
	}

	// Token: 0x0600389D RID: 14493 RVA: 0x0012BC00 File Offset: 0x00129E00
	public static ServerMileageMapState AnalyzeMileageMapStateJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		ServerMileageMapState serverMileageMapState = new ServerMileageMapState();
		serverMileageMapState.m_episode = NetUtil.GetJsonInt(jsonData, "episode");
		serverMileageMapState.m_chapter = NetUtil.GetJsonInt(jsonData, "chapter");
		serverMileageMapState.m_point = NetUtil.GetJsonInt(jsonData, "point");
		serverMileageMapState.m_numBossAttack = NetUtil.GetJsonInt(jsonData, "numBossAttack");
		serverMileageMapState.m_stageTotalScore = NetUtil.GetJsonLong(jsonData, "stageTotalScore");
		serverMileageMapState.m_stageMaxScore = NetUtil.GetJsonLong(jsonData, "stageMaxScore");
		long unixTime = (long)NetUtil.GetJsonInt(jsonData, "chapterStartTime");
		serverMileageMapState.m_chapterStartTime = NetUtil.GetLocalDateTime(unixTime);
		return serverMileageMapState;
	}

	// Token: 0x0600389E RID: 14494 RVA: 0x0012BCBC File Offset: 0x00129EBC
	public static PresentItem AnalyzePresentItemJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		return new PresentItem
		{
			m_itemId = NetUtil.GetJsonInt(jsonData, "itemId"),
			m_numItem = NetUtil.GetJsonInt(jsonData, "numItem"),
			m_additionalInfo1 = NetUtil.GetJsonInt(jsonData, "additionalInfo1"),
			m_additionalInfo1 = NetUtil.GetJsonInt(jsonData, "additionalInfo2")
		};
	}

	// Token: 0x0600389F RID: 14495 RVA: 0x0012BD3C File Offset: 0x00129F3C
	public static ServerMessageEntry AnalyzeMessageEntryJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		return new ServerMessageEntry
		{
			m_messageId = NetUtil.GetJsonInt(jsonData, "messageId"),
			m_messageType = (ServerMessageEntry.MessageType)NetUtil.GetJsonInt(jsonData, "messageType"),
			m_fromId = NetUtil.GetJsonString(jsonData, "friendId"),
			m_name = NetUtil.GetJsonString(jsonData, "name"),
			m_url = NetUtil.GetJsonString(jsonData, "url"),
			m_presentState = NetUtil.AnalyzePresentStateJson(jsonData, "item"),
			m_expireTiem = NetUtil.GetJsonInt(jsonData, "expireTime")
		};
	}

	// Token: 0x060038A0 RID: 14496 RVA: 0x0012BDF0 File Offset: 0x00129FF0
	public static ServerOperatorMessageEntry AnalyzeOperatorMessageEntryJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		return new ServerOperatorMessageEntry
		{
			m_messageId = NetUtil.GetJsonInt(jsonData, "messageId"),
			m_content = NetUtil.GetJsonString(jsonData, "contents"),
			m_presentState = NetUtil.AnalyzePresentStateJson(jsonData, "item"),
			m_expireTiem = NetUtil.GetJsonInt(jsonData, "expireTime")
		};
	}

	// Token: 0x060038A1 RID: 14497 RVA: 0x0012BE70 File Offset: 0x0012A070
	public static ServerItemState AnalyzeItemStateJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		return new ServerItemState
		{
			m_itemId = NetUtil.GetJsonInt(jsonData, "itemId"),
			m_num = NetUtil.GetJsonInt(jsonData, "numItem")
		};
	}

	// Token: 0x060038A2 RID: 14498 RVA: 0x0012BED0 File Offset: 0x0012A0D0
	public static ServerPresentState AnalyzePresentStateJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		return new ServerPresentState
		{
			m_itemId = NetUtil.GetJsonInt(jsonData, "itemId"),
			m_numItem = NetUtil.GetJsonInt(jsonData, "numItem"),
			m_additionalInfo1 = NetUtil.GetJsonInt(jsonData, "additionalInfo1"),
			m_additionalInfo2 = NetUtil.GetJsonInt(jsonData, "additionalInfo2")
		};
	}

	// Token: 0x060038A3 RID: 14499 RVA: 0x0012BF50 File Offset: 0x0012A150
	public static ServerRedStarItemState AnalyzeRedStarItemStateJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		ServerRedStarItemState serverRedStarItemState = new ServerRedStarItemState();
		serverRedStarItemState.m_storeItemId = NetUtil.GetJsonInt(jsonData, "storeItemId");
		serverRedStarItemState.m_itemId = NetUtil.GetJsonInt(jsonData, "itemId");
		serverRedStarItemState.m_numItem = NetUtil.GetJsonInt(jsonData, "numItem");
		serverRedStarItemState.m_price = NetUtil.GetJsonInt(jsonData, "price");
		serverRedStarItemState.m_priceDisp = NetUtil.GetJsonString(jsonData, "priceDisp");
		serverRedStarItemState.m_productId = NetUtil.GetJsonString(jsonData, "productId");
		if (serverRedStarItemState.m_itemId == 900000)
		{
			ServerCampaignData serverCampaignData = new ServerCampaignData();
			serverCampaignData.id = serverRedStarItemState.m_storeItemId;
			serverCampaignData.campaignType = Constants.Campaign.emType.PurchaseAddRsrings;
			ServerInterface.CampaignState.RemoveCampaign(serverCampaignData);
			serverCampaignData.campaignType = Constants.Campaign.emType.PurchaseAddRsringsNoChargeUser;
			ServerInterface.CampaignState.RemoveCampaign(serverCampaignData);
		}
		if (NetUtil.IsExist(jsonData, "campaign"))
		{
			ServerCampaignData serverCampaignData2 = NetUtil.AnalyzeCampaignDataJson(jsonData, "campaign");
			if (serverCampaignData2 != null)
			{
				serverCampaignData2.id = serverRedStarItemState.m_storeItemId;
				ServerInterface.CampaignState.RegistCampaign(serverCampaignData2);
			}
		}
		return serverRedStarItemState;
	}

	// Token: 0x060038A4 RID: 14500 RVA: 0x0012C074 File Offset: 0x0012A274
	public static ServerRingItemState AnalyzeRingItemStateJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		ServerRingItemState serverRingItemState = new ServerRingItemState();
		serverRingItemState.m_itemId = NetUtil.GetJsonInt(jsonData, "item_id");
		serverRingItemState.m_cost = NetUtil.GetJsonInt(jsonData, "price");
		if (NetUtil.IsExist(jsonData, "campaign"))
		{
			ServerCampaignData serverCampaignData = NetUtil.AnalyzeCampaignDataJson(jsonData, "campaign");
			if (serverCampaignData != null)
			{
				serverCampaignData.id = serverRingItemState.m_itemId;
				ServerInterface.CampaignState.RegistCampaign(serverCampaignData);
			}
		}
		return serverRingItemState;
	}

	// Token: 0x060038A5 RID: 14501 RVA: 0x0012C10C File Offset: 0x0012A30C
	public static ServerMileageIncentive AnalyzeMileageIncentiveJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		ServerMileageIncentive serverMileageIncentive = new ServerMileageIncentive();
		serverMileageIncentive.m_type = (ServerMileageIncentive.Type)NetUtil.GetJsonInt(jsonData, "type");
		serverMileageIncentive.m_itemId = NetUtil.GetJsonInt(jsonData, "itemId");
		serverMileageIncentive.m_num = NetUtil.GetJsonInt(jsonData, "numItem");
		serverMileageIncentive.m_pointId = NetUtil.GetJsonInt(jsonData, "pointId");
		if (jsonData.ContainsKey("friendId"))
		{
			serverMileageIncentive.m_friendId = NetUtil.GetJsonString(jsonData, "friendId");
		}
		return serverMileageIncentive;
	}

	// Token: 0x060038A6 RID: 14502 RVA: 0x0012C1B0 File Offset: 0x0012A3B0
	public static ServerMileageEvent AnalyzeMileageEventJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		return new ServerMileageEvent
		{
			Distance = NetUtil.GetJsonInt(jsonData, "distance"),
			EventType = (ServerMileageEvent.emEventType)NetUtil.GetJsonInt(jsonData, "eventType"),
			Content = NetUtil.GetJsonInt(jsonData, "content"),
			NumType = (ServerConstants.NumType)NetUtil.GetJsonInt(jsonData, "numType"),
			Num = NetUtil.GetJsonInt(jsonData, "numContent"),
			Level = NetUtil.GetJsonInt(jsonData, "level")
		};
	}

	// Token: 0x060038A7 RID: 14503 RVA: 0x0012C254 File Offset: 0x0012A454
	public static List<ServerMileageFriendEntry> AnalyzeMileageFriendListJson(JsonData jdata, string key)
	{
		List<ServerMileageFriendEntry> list = new List<ServerMileageFriendEntry>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, key);
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			ServerMileageFriendEntry item = NetUtil.AnalyzeMileageFriendEntryJson(jsonArray[i], string.Empty);
			list.Add(item);
		}
		return list;
	}

	// Token: 0x060038A8 RID: 14504 RVA: 0x0012C2A4 File Offset: 0x0012A4A4
	public static ServerDailyChallengeIncentive AnalyzeDailyMissionIncentiveJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		return new ServerDailyChallengeIncentive
		{
			m_itemId = NetUtil.GetJsonInt(jsonData, "itemId"),
			m_num = NetUtil.GetJsonInt(jsonData, "numItem"),
			m_numIncentiveCont = NetUtil.GetJsonInt(jsonData, "numIncentiveCont")
		};
	}

	// Token: 0x060038A9 RID: 14505 RVA: 0x0012C314 File Offset: 0x0012A514
	public static ServerCampaignData AnalyzeCampaignDataJson(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		return new ServerCampaignData
		{
			campaignType = (Constants.Campaign.emType)NetUtil.GetJsonInt(jsonData, "campaignType"),
			iContent = NetUtil.GetJsonInt(jsonData, "campaignContent"),
			iSubContent = NetUtil.GetJsonInt(jsonData, "campaignSubContent"),
			beginDate = NetUtil.GetJsonLong(jsonData, "campaignStartTime"),
			endDate = NetUtil.GetJsonLong(jsonData, "campaignEndTime")
		};
	}

	// Token: 0x060038AA RID: 14506 RVA: 0x0012C3A8 File Offset: 0x0012A5A8
	public static List<ServerRingExchangeList> AnalyzeRingExchangeList(JsonData jdata)
	{
		if (!NetUtil.IsExist(jdata, "itemList"))
		{
			return null;
		}
		List<ServerRingExchangeList> list = new List<ServerRingExchangeList>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "itemList");
		for (int i = 0; i < jsonArray.Count; i++)
		{
			ServerRingExchangeList serverRingExchangeList = new ServerRingExchangeList();
			serverRingExchangeList.m_ringItemId = NetUtil.GetJsonInt(jsonArray[i], "ringItemId");
			serverRingExchangeList.m_itemId = NetUtil.GetJsonInt(jsonArray[i], "itemId");
			serverRingExchangeList.m_itemNum = NetUtil.GetJsonInt(jsonArray[i], "numItem");
			serverRingExchangeList.m_price = NetUtil.GetJsonInt(jsonArray[i], "price");
			if (NetUtil.IsExist(jsonArray[i], "campaign"))
			{
				ServerCampaignData serverCampaignData = NetUtil.AnalyzeCampaignDataJson(jsonArray[i], "campaign");
				if (serverCampaignData != null)
				{
					serverCampaignData.id = serverRingExchangeList.m_itemId;
					ServerInterface.CampaignState.RegistCampaign(serverCampaignData);
				}
			}
			list.Add(serverRingExchangeList);
		}
		return list;
	}

	// Token: 0x060038AB RID: 14507 RVA: 0x0012C4A4 File Offset: 0x0012A6A4
	public static int AnalyzeRingExchangeListTotalItems(JsonData jdata)
	{
		if (NetUtil.GetJsonObject(jdata, "totalItems") == null)
		{
			return 0;
		}
		return NetUtil.GetJsonInt(jdata, "totalItems");
	}

	// Token: 0x060038AC RID: 14508 RVA: 0x0012C4D4 File Offset: 0x0012A6D4
	public static ServerLeagueData AnalyzeLeagueData(JsonData jdata, string key)
	{
		JsonData jsonData = jdata;
		if (key != null && string.Empty != key)
		{
			jsonData = NetUtil.GetJsonObject(jsonData, key);
		}
		if (jsonData == null)
		{
			return null;
		}
		ServerLeagueData serverLeagueData = new ServerLeagueData();
		serverLeagueData.mode = NetUtil.GetJsonInt(jdata, "mode");
		serverLeagueData.leagueId = NetUtil.GetJsonInt(jsonData, "leagueId");
		serverLeagueData.groupId = NetUtil.GetJsonInt(jsonData, "groupId");
		serverLeagueData.numGroupMember = NetUtil.GetJsonInt(jsonData, "numGroupMember");
		serverLeagueData.numLeagueMember = NetUtil.GetJsonInt(jsonData, "numLeagueMember");
		serverLeagueData.numUp = NetUtil.GetJsonInt(jsonData, "numUp");
		serverLeagueData.numDown = NetUtil.GetJsonInt(jsonData, "numDown");
		JsonData jsonArray = NetUtil.GetJsonArray(jsonData, "highScoreOpe");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerRemainOperator remainOperator = NetUtil.AnalyzeRemainOperator(jdata2);
			serverLeagueData.AddHighScoreRemainOperator(remainOperator);
		}
		JsonData jsonArray2 = NetUtil.GetJsonArray(jsonData, "totalScoreOpe");
		int count2 = jsonArray2.Count;
		for (int j = 0; j < count2; j++)
		{
			JsonData jdata3 = jsonArray2[j];
			ServerRemainOperator remainOperator2 = NetUtil.AnalyzeRemainOperator(jdata3);
			serverLeagueData.AddTotalScoreRemainOperator(remainOperator2);
		}
		return serverLeagueData;
	}

	// Token: 0x060038AD RID: 14509 RVA: 0x0012C614 File Offset: 0x0012A814
	public static ServerWeeklyLeaderboardOptions AnalyzeWeeklyLeaderboardOptions(JsonData jdata)
	{
		return new ServerWeeklyLeaderboardOptions
		{
			mode = NetUtil.GetJsonInt(jdata, "mode"),
			type = NetUtil.GetJsonInt(jdata, "type"),
			param = NetUtil.GetJsonInt(jdata, "param"),
			startTime = NetUtil.GetJsonInt(jdata, "startTime"),
			endTime = NetUtil.GetJsonInt(jdata, "endTime")
		};
	}

	// Token: 0x060038AE RID: 14510 RVA: 0x0012C680 File Offset: 0x0012A880
	public static List<ServerLeagueOperatorData> AnalyzeLeagueDatas(JsonData jdata, string key)
	{
		List<ServerLeagueOperatorData> list = new List<ServerLeagueOperatorData>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, key);
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerLeagueOperatorData serverLeagueOperatorData = new ServerLeagueOperatorData();
			serverLeagueOperatorData.leagueId = NetUtil.GetJsonInt(jdata2, "leagueId");
			serverLeagueOperatorData.numUp = NetUtil.GetJsonInt(jdata2, "numUp");
			serverLeagueOperatorData.numDown = NetUtil.GetJsonInt(jdata2, "numDown");
			JsonData jsonArray2 = NetUtil.GetJsonArray(jdata2, "highScoreOpe");
			int count2 = jsonArray2.Count;
			for (int j = 0; j < count2; j++)
			{
				JsonData jdata3 = jsonArray2[j];
				ServerRemainOperator remainOperator = NetUtil.AnalyzeRemainOperator(jdata3);
				serverLeagueOperatorData.AddHighScoreRemainOperator(remainOperator);
			}
			JsonData jsonArray3 = NetUtil.GetJsonArray(jdata2, "totalScoreOpe");
			int count3 = jsonArray3.Count;
			for (int k = 0; k < count3; k++)
			{
				JsonData jdata4 = jsonArray3[k];
				ServerRemainOperator remainOperator2 = NetUtil.AnalyzeRemainOperator(jdata4);
				serverLeagueOperatorData.AddTotalScoreRemainOperator(remainOperator2);
			}
			list.Add(serverLeagueOperatorData);
		}
		return list;
	}

	// Token: 0x060038AF RID: 14511 RVA: 0x0012C7A4 File Offset: 0x0012A9A4
	private static ServerRemainOperator AnalyzeRemainOperator(JsonData jdata)
	{
		ServerRemainOperator serverRemainOperator = new ServerRemainOperator();
		serverRemainOperator.operatorData = NetUtil.GetJsonInt(jdata, "operator");
		serverRemainOperator.number = NetUtil.GetJsonInt(jdata, "number");
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "presentList");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerItemState itemState = NetUtil.AnalyzeItemStateJson(jdata2, string.Empty);
			serverRemainOperator.AddItemState(itemState);
		}
		return serverRemainOperator;
	}

	// Token: 0x060038B0 RID: 14512 RVA: 0x0012C820 File Offset: 0x0012AA20
	public static void GetResponse_CampaignList(JsonData jdata)
	{
		if (NetUtil.IsExist(jdata, "campaignList"))
		{
			JsonData jsonArray = NetUtil.GetJsonArray(jdata, "campaignList");
			if (jsonArray != null)
			{
				int count = jsonArray.Count;
				for (int i = 0; i < count; i++)
				{
					ServerCampaignData serverCampaignData = NetUtil.AnalyzeCampaignDataJson(jsonArray[i], string.Empty);
					if (serverCampaignData != null)
					{
						ServerInterface.CampaignState.RegistCampaign(serverCampaignData);
					}
				}
			}
		}
	}

	// Token: 0x060038B1 RID: 14513 RVA: 0x0012C88C File Offset: 0x0012AA8C
	public static ServerFreeItemState AnalyzeFreeItemList(JsonData jdata)
	{
		ServerFreeItemState serverFreeItemState = new ServerFreeItemState();
		if (NetUtil.IsExist(jdata, "freeItemList"))
		{
			JsonData jsonArray = NetUtil.GetJsonArray(jdata, "freeItemList");
			if (jsonArray != null)
			{
				int count = jsonArray.Count;
				for (int i = 0; i < count; i++)
				{
					serverFreeItemState.AddItem(new ServerItemState
					{
						m_itemId = NetUtil.GetJsonInt(jsonArray[i], "itemId"),
						m_num = NetUtil.GetJsonInt(jsonArray[i], "numItem")
					});
				}
			}
		}
		return serverFreeItemState;
	}

	// Token: 0x060038B2 RID: 14514 RVA: 0x0012C91C File Offset: 0x0012AB1C
	public static List<ServerConsumedCostData> AnalyzeConsumedCostDataList(JsonData jdata)
	{
		if (!NetUtil.IsExist(jdata, "consumedCostList"))
		{
			return null;
		}
		List<ServerConsumedCostData> list = new List<ServerConsumedCostData>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "consumedCostList");
		for (int i = 0; i < jsonArray.Count; i++)
		{
			list.Add(new ServerConsumedCostData
			{
				consumedItemId = NetUtil.GetJsonInt(jsonArray[i], "consumedItemId"),
				itemId = NetUtil.GetJsonInt(jsonArray[i], "itemId"),
				numItem = NetUtil.GetJsonInt(jsonArray[i], "numItem")
			});
		}
		return list;
	}

	// Token: 0x060038B3 RID: 14515 RVA: 0x0012C9B8 File Offset: 0x0012ABB8
	public static List<ServerEventEntry> AnalyzeEventList(JsonData jdata)
	{
		if (!NetUtil.IsExist(jdata, "eventList"))
		{
			return null;
		}
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "eventList");
		if (jsonArray == null)
		{
			return null;
		}
		List<ServerEventEntry> list = new List<ServerEventEntry>();
		for (int i = 0; i < jsonArray.Count; i++)
		{
			list.Add(new ServerEventEntry
			{
				EventId = NetUtil.GetJsonInt(jsonArray[i], "eventId"),
				EventType = NetUtil.GetJsonInt(jsonArray[i], "eventType"),
				EventStartTime = NetUtil.GetLocalDateTime(NetUtil.GetJsonLong(jsonArray[i], "eventStartTime")),
				EventEndTime = NetUtil.GetLocalDateTime(NetUtil.GetJsonLong(jsonArray[i], "eventEndTime")),
				EventCloseTime = NetUtil.GetLocalDateTime(NetUtil.GetJsonLong(jsonArray[i], "eventCloseTime"))
			});
		}
		return list;
	}

	// Token: 0x060038B4 RID: 14516 RVA: 0x0012CA98 File Offset: 0x0012AC98
	public static ServerEventState AnalyzeEventState(JsonData jdata)
	{
		if (!NetUtil.IsExist(jdata, "eventState"))
		{
			return null;
		}
		JsonData jsonObject = NetUtil.GetJsonObject(jdata, "eventState");
		if (jsonObject == null)
		{
			return null;
		}
		return new ServerEventState
		{
			Param = NetUtil.GetJsonLong(jsonObject, "param"),
			RewardId = NetUtil.GetJsonInt(jsonObject, "rewardId")
		};
	}

	// Token: 0x060038B5 RID: 14517 RVA: 0x0012CAF4 File Offset: 0x0012ACF4
	public static List<ServerEventReward> AnalyzeEventReward(JsonData jdata)
	{
		if (!NetUtil.IsExist(jdata, "eventRewardList"))
		{
			return null;
		}
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "eventRewardList");
		if (jsonArray == null)
		{
			return null;
		}
		List<ServerEventReward> list = new List<ServerEventReward>();
		for (int i = 0; i < jsonArray.Count; i++)
		{
			list.Add(new ServerEventReward
			{
				RewardId = NetUtil.GetJsonInt(jsonArray[i], "rewardId"),
				Param = NetUtil.GetJsonLong(jsonArray[i], "param"),
				m_itemId = NetUtil.GetJsonInt(jsonArray[i], "itemId"),
				m_num = NetUtil.GetJsonInt(jsonArray[i], "numItem")
			});
		}
		return list;
	}

	// Token: 0x060038B6 RID: 14518 RVA: 0x0012CBB0 File Offset: 0x0012ADB0
	public static ServerEventRaidBossState AnalyzeRaidBossState(JsonData jdata)
	{
		if (!NetUtil.IsExist(jdata, "eventRaidboss"))
		{
			return null;
		}
		JsonData jsonObject = NetUtil.GetJsonObject(jdata, "eventRaidboss");
		if (jsonObject == null)
		{
			return null;
		}
		return new ServerEventRaidBossState
		{
			Id = NetUtil.GetJsonLong(jsonObject, "raidbossId"),
			Level = NetUtil.GetJsonInt(jsonObject, "raidbossLevel"),
			Rarity = NetUtil.GetJsonInt(jsonObject, "raidbossRarity"),
			HitPoint = NetUtil.GetJsonInt(jsonObject, "raidbossHitPoint"),
			MaxHitPoint = NetUtil.GetJsonInt(jsonObject, "raidbossMaxHitPoint"),
			Status = NetUtil.GetJsonInt(jsonObject, "raidbossStatus"),
			EscapeAt = NetUtil.GetLocalDateTime(NetUtil.GetJsonLong(jsonObject, "raidbossEscapeAt")),
			EncounterName = NetUtil.GetJsonString(jsonObject, "encounterName"),
			Encounter = NetUtil.GetJsonBoolean(jsonObject, "encounterFlg"),
			Crowded = NetUtil.GetJsonBoolean(jsonObject, "crowdedFlg"),
			Participation = (NetUtil.GetJsonInt(jsonObject, "participateCount") != 0)
		};
	}

	// Token: 0x060038B7 RID: 14519 RVA: 0x0012CCB0 File Offset: 0x0012AEB0
	public static List<ServerEventRaidBossState> AnalyzeRaidBossStateList(JsonData jdata)
	{
		if (!NetUtil.IsExist(jdata, "eventUserRaidbossList"))
		{
			return null;
		}
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "eventUserRaidbossList");
		if (jsonArray == null)
		{
			return null;
		}
		List<ServerEventRaidBossState> list = new List<ServerEventRaidBossState>();
		for (int i = 0; i < jsonArray.Count; i++)
		{
			list.Add(new ServerEventRaidBossState
			{
				Id = NetUtil.GetJsonLong(jsonArray[i], "raidbossId"),
				Level = NetUtil.GetJsonInt(jsonArray[i], "raidbossLevel"),
				Rarity = NetUtil.GetJsonInt(jsonArray[i], "raidbossRarity"),
				HitPoint = NetUtil.GetJsonInt(jsonArray[i], "raidbossHitPoint"),
				MaxHitPoint = NetUtil.GetJsonInt(jsonArray[i], "raidbossMaxHitPoint"),
				Status = NetUtil.GetJsonInt(jsonArray[i], "raidbossStatus"),
				EscapeAt = NetUtil.GetLocalDateTime(NetUtil.GetJsonLong(jsonArray[i], "raidbossEscapeAt")),
				EncounterName = NetUtil.GetJsonString(jsonArray[i], "encounterName"),
				Encounter = NetUtil.GetJsonBoolean(jsonArray[i], "encounterFlg"),
				Crowded = NetUtil.GetJsonBoolean(jsonArray[i], "crowdedFlg"),
				Participation = (NetUtil.GetJsonInt(jsonArray[i], "participateCount") != 0)
			});
		}
		return list;
	}

	// Token: 0x060038B8 RID: 14520 RVA: 0x0012CE18 File Offset: 0x0012B018
	public static ServerEventUserRaidBossState AnalyzeEventUserRaidBossState(JsonData jdata)
	{
		if (!NetUtil.IsExist(jdata, "eventUserRaidboss"))
		{
			return null;
		}
		JsonData jsonObject = NetUtil.GetJsonObject(jdata, "eventUserRaidboss");
		if (jsonObject == null)
		{
			return null;
		}
		ServerEventUserRaidBossState serverEventUserRaidBossState = new ServerEventUserRaidBossState();
		int raidBossEnergy = 0;
		int raidbossEnergyBuy = 0;
		serverEventUserRaidBossState.NumRaidbossRings = NetUtil.GetJsonInt(jsonObject, "numRaidbossRings");
		raidBossEnergy = NetUtil.GetJsonInt(jsonObject, "raidbossEnergy");
		raidbossEnergyBuy = NetUtil.GetJsonInt(jsonObject, "raidbossEnergyBuy");
		serverEventUserRaidBossState.NumBeatedEncounter = NetUtil.GetJsonInt(jsonObject, "numBeatedEncounter");
		serverEventUserRaidBossState.NumBeatedEnterprise = NetUtil.GetJsonInt(jsonObject, "numBeatedEnterprise");
		serverEventUserRaidBossState.NumRaidBossEncountered = NetUtil.GetJsonInt(jsonObject, "numTotalEncountered");
		DateTime localDateTime = NetUtil.GetLocalDateTime(NetUtil.GetJsonLong(jsonObject, "raidbossEnergyRenewsAt"));
		RaidEnergyStorage.CheckEnergy(ref raidBossEnergy, ref raidbossEnergyBuy, ref localDateTime);
		serverEventUserRaidBossState.RaidBossEnergy = raidBossEnergy;
		serverEventUserRaidBossState.RaidbossEnergyBuy = raidbossEnergyBuy;
		serverEventUserRaidBossState.EnergyRenewsAt = localDateTime;
		return serverEventUserRaidBossState;
	}

	// Token: 0x060038B9 RID: 14521 RVA: 0x0012CEE8 File Offset: 0x0012B0E8
	public static List<ServerEventRaidBossUserState> AnalyzeEventRaidBossUserStateList(JsonData jdata)
	{
		if (!NetUtil.IsExist(jdata, "eventRaidbossUserList"))
		{
			return null;
		}
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "eventRaidbossUserList");
		if (jsonArray == null)
		{
			return null;
		}
		List<ServerEventRaidBossUserState> list = new List<ServerEventRaidBossUserState>();
		for (int i = 0; i < jsonArray.Count; i++)
		{
			list.Add(new ServerEventRaidBossUserState
			{
				WrestleId = NetUtil.GetJsonString(jsonArray[i], "wrestleId"),
				UserName = NetUtil.GetJsonString(jsonArray[i], "name"),
				Grade = NetUtil.GetJsonInt(jsonArray[i], "grade"),
				NumRank = NetUtil.GetJsonInt(jsonArray[i], "numRank"),
				LoginTime = NetUtil.GetJsonInt(jsonArray[i], "loginTime"),
				CharaId = NetUtil.GetJsonInt(jsonArray[i], "charaId"),
				CharaLevel = NetUtil.GetJsonInt(jsonArray[i], "charaLevel"),
				SubCharaId = NetUtil.GetJsonInt(jsonArray[i], "subCharaId"),
				SubCharaLevel = NetUtil.GetJsonInt(jsonArray[i], "subCharaLevel"),
				MainChaoId = NetUtil.GetJsonInt(jsonArray[i], "mainChaoId"),
				MainChaoLevel = NetUtil.GetJsonInt(jsonArray[i], "mainChaoLevel"),
				SubChaoId = NetUtil.GetJsonInt(jsonArray[i], "subChaoId"),
				SubChaoLevel = NetUtil.GetJsonInt(jsonArray[i], "subChaoLevel"),
				Language = NetUtil.GetJsonInt(jsonArray[i], "language"),
				League = NetUtil.GetJsonInt(jsonArray[i], "league"),
				WrestleCount = NetUtil.GetJsonInt(jsonArray[i], "wrestleCount"),
				WrestleDamage = NetUtil.GetJsonInt(jsonArray[i], "wrestleDamage"),
				WrestleBeatFlg = NetUtil.GetJsonBoolean(jsonArray[i], "wrestleBeatFlg")
			});
		}
		return list;
	}

	// Token: 0x060038BA RID: 14522 RVA: 0x0012D0E4 File Offset: 0x0012B2E4
	public static List<ServerEventRaidBossDesiredState> AnalyzeEventRaidbossDesiredList(JsonData jdata)
	{
		if (!NetUtil.IsExist(jdata, "eventRaidbossDesiredList"))
		{
			return null;
		}
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "eventRaidbossDesiredList");
		if (jsonArray == null)
		{
			return null;
		}
		List<ServerEventRaidBossDesiredState> list = new List<ServerEventRaidBossDesiredState>();
		for (int i = 0; i < jsonArray.Count; i++)
		{
			list.Add(new ServerEventRaidBossDesiredState
			{
				DesireId = NetUtil.GetJsonString(jsonArray[i], "desireId"),
				UserName = NetUtil.GetJsonString(jsonArray[i], "name"),
				NumRank = NetUtil.GetJsonInt(jsonArray[i], "numRank"),
				LoginTime = NetUtil.GetJsonInt(jsonArray[i], "loginTime"),
				CharaId = NetUtil.GetJsonInt(jsonArray[i], "charaId"),
				CharaLevel = NetUtil.GetJsonInt(jsonArray[i], "charaLevel"),
				SubCharaId = NetUtil.GetJsonInt(jsonArray[i], "subCharaId"),
				SubCharaLevel = NetUtil.GetJsonInt(jsonArray[i], "subCharaLevel"),
				MainChaoId = NetUtil.GetJsonInt(jsonArray[i], "mainChaoId"),
				MainChaoLevel = NetUtil.GetJsonInt(jsonArray[i], "mainChaoLevel"),
				SubChaoId = NetUtil.GetJsonInt(jsonArray[i], "subChaoId"),
				SubChaoLevel = NetUtil.GetJsonInt(jsonArray[i], "subChaoLevel"),
				Language = NetUtil.GetJsonInt(jsonArray[i], "language"),
				League = NetUtil.GetJsonInt(jsonArray[i], "league"),
				NumBeatedEnterprise = NetUtil.GetJsonInt(jsonArray[i], "numBeatedEnterprise")
			});
		}
		return list;
	}

	// Token: 0x060038BB RID: 14523 RVA: 0x0012D29C File Offset: 0x0012B49C
	public static ServerEventRaidBossBonus AnalyzeEventRaidBossBonus(JsonData jdata)
	{
		if (!NetUtil.IsExist(jdata, "eventRaidbossBonus"))
		{
			return null;
		}
		JsonData jsonObject = NetUtil.GetJsonObject(jdata, "eventRaidbossBonus");
		if (jsonObject == null)
		{
			return null;
		}
		return new ServerEventRaidBossBonus
		{
			EncounterBonus = NetUtil.GetJsonInt(jsonObject, "eventRaidbossEncounterBonus"),
			WrestleBonus = NetUtil.GetJsonInt(jsonObject, "eventRaidbossWrestleBonus"),
			DamageRateBonus = NetUtil.GetJsonInt(jsonObject, "eventRaidbossDamageRateBonus"),
			DamageTopBonus = NetUtil.GetJsonInt(jsonObject, "eventRaidbossDamageTopBonus"),
			BeatBonus = NetUtil.GetJsonInt(jsonObject, "eventRaidbossBeatBonus")
		};
	}

	// Token: 0x060038BC RID: 14524 RVA: 0x0012D32C File Offset: 0x0012B52C
	public static List<ServerUserTransformData> AnalyzeUserTransformData(JsonData jdata)
	{
		if (!NetUtil.IsExist(jdata, "transformDataList"))
		{
			return null;
		}
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "transformDataList");
		if (jsonArray == null)
		{
			return null;
		}
		List<ServerUserTransformData> list = new List<ServerUserTransformData>();
		for (int i = 0; i < jsonArray.Count; i++)
		{
			list.Add(new ServerUserTransformData
			{
				m_userId = NetUtil.GetJsonString(jsonArray[i], "userId"),
				m_facebookId = NetUtil.GetJsonString(jsonArray[i], "facebookId")
			});
		}
		return list;
	}

	// Token: 0x060038BD RID: 14525 RVA: 0x0012D3B8 File Offset: 0x0012B5B8
	public static DateTime GetLocalDateTime(long unixTime)
	{
		return NetUtil.UNIX_EPOCH.AddSeconds((double)unixTime).ToLocalTime();
	}

	// Token: 0x060038BE RID: 14526 RVA: 0x0012D3DC File Offset: 0x0012B5DC
	public static long GetUnixTime(DateTime dateTime)
	{
		dateTime = dateTime.ToUniversalTime();
		return (long)(dateTime - NetUtil.UNIX_EPOCH).TotalSeconds;
	}

	// Token: 0x060038BF RID: 14527 RVA: 0x0012D408 File Offset: 0x0012B608
	public static int GetCurrentUnixTime()
	{
		DateTime utcNow = DateTime.UtcNow;
		return (int)(utcNow - NetUtil.UNIX_EPOCH).TotalSeconds;
	}

	// Token: 0x060038C0 RID: 14528 RVA: 0x0012D430 File Offset: 0x0012B630
	public static DateTime GetCurrentTime()
	{
		return NetBase.GetCurrentTime();
	}

	// Token: 0x060038C1 RID: 14529 RVA: 0x0012D438 File Offset: 0x0012B638
	public static bool IsServerTimeWithinPeriod(long start, long end)
	{
		return NetUtil.IsWithinPeriod(NetBase.LastNetServerTime, start, end);
	}

	// Token: 0x060038C2 RID: 14530 RVA: 0x0012D454 File Offset: 0x0012B654
	public static bool IsWithinPeriod(long current, long start, long end)
	{
		if (start == 0L && end == 0L)
		{
			return true;
		}
		if (start == 0L)
		{
			if (current <= end)
			{
				return true;
			}
		}
		else if (end == 0L)
		{
			if (start <= current)
			{
				return true;
			}
		}
		else if (start <= current && current <= end)
		{
			return true;
		}
		return false;
	}

	// Token: 0x04002F87 RID: 12167
	public static readonly float ConnectTimeOut;

	// Token: 0x04002F88 RID: 12168
	private static readonly TimeSpan ReloginStartTime;

	// Token: 0x04002F89 RID: 12169
	private static DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
}
