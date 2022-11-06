using System;
using System.Collections.Generic;
using LitJson;
using SaveData;

// Token: 0x020009F2 RID: 2546
public class FacebookUtil
{
	// Token: 0x06004327 RID: 17191 RVA: 0x0015CDE0 File Offset: 0x0015AFE0
	public static void SaveFriendIdList(List<SocialUserData> friends)
	{
		if (friends == null)
		{
			return;
		}
		if (friends.Count > FacebookUtil.MaxFBRankingFriends)
		{
			return;
		}
		List<string> list = new List<string>();
		foreach (SocialUserData socialUserData in friends)
		{
			if (socialUserData != null)
			{
				list.Add(socialUserData.Id);
			}
		}
		SystemData systemSaveData = SystemSaveManager.GetSystemSaveData();
		if (systemSaveData != null)
		{
			systemSaveData.fbFriends = list;
		}
		SystemSaveManager.Instance.SaveSystemData();
	}

	// Token: 0x06004328 RID: 17192 RVA: 0x0015CE8C File Offset: 0x0015B08C
	public static SocialUserData GetUserData(JsonData jdata)
	{
		bool flag = false;
		return FacebookUtil.GetUserData(jdata, ref flag);
	}

	// Token: 0x06004329 RID: 17193 RVA: 0x0015CEA4 File Offset: 0x0015B0A4
	public static SocialUserData GetUserData(JsonData jdata, ref bool isInstalled)
	{
		SocialUserData socialUserData = new SocialUserData();
		string jsonString = NetUtil.GetJsonString(jdata, "id");
		if (jsonString == null)
		{
			return socialUserData;
		}
		socialUserData.Id = jsonString;
		socialUserData.Name = NetUtil.GetJsonString(jdata, "name");
		isInstalled = NetUtil.GetJsonBoolean(jdata, "installed");
		JsonData jsonObject = NetUtil.GetJsonObject(jdata, "picture");
		if (jsonObject == null)
		{
			return socialUserData;
		}
		JsonData jsonObject2 = NetUtil.GetJsonObject(jsonObject, "data");
		if (jsonObject2 == null)
		{
			return socialUserData;
		}
		string jsonString2 = NetUtil.GetJsonString(jsonObject2, "url");
		if (jsonString2 == null)
		{
			return socialUserData;
		}
		socialUserData.IsSilhouette = NetUtil.GetJsonBoolean(jsonObject2, "is_silhouette");
		socialUserData.Url = jsonString2;
		return socialUserData;
	}

	// Token: 0x0600432A RID: 17194 RVA: 0x0015CF48 File Offset: 0x0015B148
	public static void GetDefaultPicture(SocialUserData userData)
	{
	}

	// Token: 0x0600432B RID: 17195 RVA: 0x0015CF4C File Offset: 0x0015B14C
	public static string GetActionId(string text)
	{
		if (text == null)
		{
			return null;
		}
		Debug.Log("Facebook.GetAction:" + text);
		JsonData jsonData = JsonMapper.ToObject(text);
		if (jsonData == null)
		{
			Debug.Log("Failed transform plainText to Json");
			return null;
		}
		JsonData jsonArray = NetUtil.GetJsonArray(jsonData, "data");
		if (jsonArray == null)
		{
			Debug.Log("Not found user data in json data");
			return null;
		}
		if (jsonArray.Count == 0)
		{
			return null;
		}
		return NetUtil.GetJsonString(jsonArray[0], "id");
	}

	// Token: 0x0600432C RID: 17196 RVA: 0x0015CFC8 File Offset: 0x0015B1C8
	public static string GetObjectIdFromAction(string text, string objectName)
	{
		if (text == null)
		{
			return null;
		}
		Debug.Log("Facebook.GetAction:" + text);
		JsonData jsonData = JsonMapper.ToObject(text);
		if (jsonData == null)
		{
			Debug.Log("Failed transform plainText to Json");
			return null;
		}
		JsonData jsonArray = NetUtil.GetJsonArray(jsonData, "data");
		if (jsonArray == null)
		{
			Debug.Log("Not found user data in json data");
			return null;
		}
		if (jsonArray.Count == 0)
		{
			return null;
		}
		JsonData jsonObject = NetUtil.GetJsonObject(jsonArray[0], "data");
		if (jsonObject == null)
		{
			Debug.Log("Not found object in json data");
			return null;
		}
		JsonData jsonObject2 = NetUtil.GetJsonObject(jsonObject, objectName);
		if (jsonObject2 == null)
		{
			Debug.Log("Not found sigoto object in json data");
			return null;
		}
		string jsonString = NetUtil.GetJsonString(jsonObject2, "id");
		if (jsonString == null)
		{
			Debug.Log("Not found object's id in json data");
			return null;
		}
		return jsonString;
	}

	// Token: 0x0600432D RID: 17197 RVA: 0x0015D094 File Offset: 0x0015B294
	public static void UpdatePermissionInfo(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		JsonData jsonData = JsonMapper.ToObject(text);
		if (jsonData == null)
		{
			Debug.Log("Failed transform plainText to Json");
			return;
		}
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null)
		{
			JsonData jsonArray = NetUtil.GetJsonArray(jsonData, "data");
			if (jsonArray != null)
			{
				for (int i = 0; i < jsonArray.Count; i++)
				{
					string jsonString = NetUtil.GetJsonString(jsonArray[i], "permission");
					if (!string.IsNullOrEmpty(jsonString))
					{
						string jsonString2 = NetUtil.GetJsonString(jsonArray[i], "status");
						if (!string.IsNullOrEmpty(jsonString2))
						{
							for (int j = 0; j < 2; j++)
							{
								string text2 = FacebookUtil.PermissionString[j];
								if (!string.IsNullOrEmpty(text2))
								{
									if (jsonString == text2)
									{
										if (jsonString2 == "granted")
										{
											socialInterface.IsGrantedPermission[j] = true;
											Debug.Log("FB permission:" + text2 + " is granted");
										}
										else
										{
											socialInterface.IsGrantedPermission[j] = false;
											Debug.Log("FB permission:" + text2 + " is not granted");
										}
										break;
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600432E RID: 17198 RVA: 0x0015D1E8 File Offset: 0x0015B3E8
	public static bool IsLoggedIn()
	{
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		return !(socialInterface == null) && socialInterface.IsLoggedIn;
	}

	// Token: 0x0600432F RID: 17199 RVA: 0x0015D214 File Offset: 0x0015B414
	public static List<SocialUserData> GetInvitedFriendList(string text)
	{
		List<SocialUserData> list = new List<SocialUserData>();
		if (text == null)
		{
			return list;
		}
		JsonData jsonData = JsonMapper.ToObject(text);
		if (jsonData == null)
		{
			Debug.Log("Failed transform plainText to Json");
			return list;
		}
		JsonData jsonData2 = null;
		try
		{
			jsonData2 = NetUtil.GetJsonArray(jsonData, "data");
			if (jsonData2 == null)
			{
				Debug.Log("Not found user data in json data");
				return list;
			}
		}
		catch (Exception ex)
		{
			return list;
		}
		int count = jsonData2.Count;
		List<string> list2 = new List<string>();
		for (int i = 0; i < count; i++)
		{
			JsonData jsonObject = NetUtil.GetJsonObject(jsonData2[i], "application");
			if (jsonObject != null)
			{
				string jsonString = NetUtil.GetJsonString(jsonObject, "id");
				if (!(jsonString != FacebookUtil.SonicRunnersId))
				{
					string jsonString2 = NetUtil.GetJsonString(jsonData2[i], "data");
					list2.Add(jsonString2);
				}
			}
		}
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface == null)
		{
			return list;
		}
		foreach (string text2 in list2)
		{
			if (!string.IsNullOrEmpty(text2))
			{
				foreach (SocialUserData socialUserData in socialInterface.FriendList)
				{
					if (socialUserData != null)
					{
						if (socialUserData.CustomData.GameId == text2)
						{
							list.Add(socialUserData);
							break;
						}
					}
				}
			}
		}
		return list;
	}

	// Token: 0x040038FF RID: 14591
	public static readonly string EnergyMarkString = "SonicRunnersEnergy";

	// Token: 0x04003900 RID: 14592
	public static readonly string SonicRunnersId = "203227836537595";

	// Token: 0x04003901 RID: 14593
	public static readonly string FBVersionString = "/v2.1/";

	// Token: 0x04003902 RID: 14594
	public static readonly int MaxFBFriends = 5000;

	// Token: 0x04003903 RID: 14595
	public static readonly int MaxFBRankingFriends = 50;

	// Token: 0x04003904 RID: 14596
	public static readonly string[] PermissionString = new string[]
	{
		"public_profile",
		"user_friends"
	};
}
