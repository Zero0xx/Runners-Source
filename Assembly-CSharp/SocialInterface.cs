using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A0B RID: 2571
public class SocialInterface : MonoBehaviour
{
	// Token: 0x1700092B RID: 2347
	// (get) Token: 0x060043D9 RID: 17369 RVA: 0x0015F904 File Offset: 0x0015DB04
	public static SocialInterface Instance
	{
		get
		{
			return SocialInterface.m_instance;
		}
	}

	// Token: 0x1700092C RID: 2348
	// (get) Token: 0x060043DA RID: 17370 RVA: 0x0015F90C File Offset: 0x0015DB0C
	// (set) Token: 0x060043DB RID: 17371 RVA: 0x0015F914 File Offset: 0x0015DB14
	public bool IsInitialized
	{
		get
		{
			return this.m_isInitialized;
		}
		set
		{
			this.m_isInitialized = value;
		}
	}

	// Token: 0x1700092D RID: 2349
	// (get) Token: 0x060043DC RID: 17372 RVA: 0x0015F920 File Offset: 0x0015DB20
	// (set) Token: 0x060043DD RID: 17373 RVA: 0x0015F928 File Offset: 0x0015DB28
	public bool IsLoggedIn
	{
		get
		{
			return this.m_isLoggedIn;
		}
		set
		{
			this.m_isLoggedIn = value;
		}
	}

	// Token: 0x1700092E RID: 2350
	// (get) Token: 0x060043DE RID: 17374 RVA: 0x0015F934 File Offset: 0x0015DB34
	// (set) Token: 0x060043DF RID: 17375 RVA: 0x0015F93C File Offset: 0x0015DB3C
	public SocialUserData MyProfile
	{
		get
		{
			return this.m_myProfile;
		}
		set
		{
			this.m_myProfile = value;
		}
	}

	// Token: 0x1700092F RID: 2351
	// (get) Token: 0x060043E0 RID: 17376 RVA: 0x0015F948 File Offset: 0x0015DB48
	// (set) Token: 0x060043E1 RID: 17377 RVA: 0x0015F950 File Offset: 0x0015DB50
	public bool IsEnableFriendInfo
	{
		get
		{
			return this.m_isEnableFriendInfo;
		}
		set
		{
			this.m_isEnableFriendInfo = value;
		}
	}

	// Token: 0x17000930 RID: 2352
	// (get) Token: 0x060043E2 RID: 17378 RVA: 0x0015F95C File Offset: 0x0015DB5C
	// (set) Token: 0x060043E3 RID: 17379 RVA: 0x0015F964 File Offset: 0x0015DB64
	public List<SocialUserData> FriendList
	{
		get
		{
			return this.m_friendList;
		}
		set
		{
			if (value == null)
			{
				return;
			}
			if (value.Count > FacebookUtil.MaxFBRankingFriends)
			{
				return;
			}
			this.m_friendList = value;
		}
	}

	// Token: 0x17000931 RID: 2353
	// (get) Token: 0x060043E4 RID: 17380 RVA: 0x0015F988 File Offset: 0x0015DB88
	// (set) Token: 0x060043E5 RID: 17381 RVA: 0x0015F990 File Offset: 0x0015DB90
	public List<SocialUserData> AllFriendList
	{
		get
		{
			return this.m_allFriendList;
		}
		set
		{
			this.m_allFriendList = value;
		}
	}

	// Token: 0x17000932 RID: 2354
	// (get) Token: 0x060043E6 RID: 17382 RVA: 0x0015F99C File Offset: 0x0015DB9C
	// (set) Token: 0x060043E7 RID: 17383 RVA: 0x0015F9A4 File Offset: 0x0015DBA4
	public List<SocialUserData> NotInstalledFriendList
	{
		get
		{
			return this.m_notInstalledFriendList;
		}
		set
		{
			this.m_notInstalledFriendList = value;
		}
	}

	// Token: 0x17000933 RID: 2355
	// (get) Token: 0x060043E8 RID: 17384 RVA: 0x0015F9B0 File Offset: 0x0015DBB0
	// (set) Token: 0x060043E9 RID: 17385 RVA: 0x0015F9B8 File Offset: 0x0015DBB8
	public List<SocialUserData> InvitedFriendList
	{
		get
		{
			return this.m_invitedFriendList;
		}
		set
		{
			this.m_invitedFriendList = value;
		}
	}

	// Token: 0x17000934 RID: 2356
	// (get) Token: 0x060043EA RID: 17386 RVA: 0x0015F9C4 File Offset: 0x0015DBC4
	// (set) Token: 0x060043EB RID: 17387 RVA: 0x0015F9CC File Offset: 0x0015DBCC
	public bool[] IsGrantedPermission
	{
		get
		{
			return this.m_isGrantedPermission;
		}
		set
		{
			this.m_isGrantedPermission = value;
		}
	}

	// Token: 0x17000935 RID: 2357
	// (get) Token: 0x060043EC RID: 17388 RVA: 0x0015F9D8 File Offset: 0x0015DBD8
	public List<SocialUserData> FriendWithMeList
	{
		get
		{
			List<SocialUserData> list = new List<SocialUserData>();
			if (this.m_friendList != null)
			{
				foreach (SocialUserData item in this.m_friendList)
				{
					list.Add(item);
				}
			}
			if (this.m_myProfile != null)
			{
				list.Add(this.m_myProfile);
			}
			return list;
		}
	}

	// Token: 0x060043ED RID: 17389 RVA: 0x0015FA68 File Offset: 0x0015DC68
	public static List<string> GetGameIdList(List<SocialUserData> socialUserDataList)
	{
		List<string> list = new List<string>();
		if (socialUserDataList == null)
		{
			return list;
		}
		foreach (SocialUserData socialUserData in socialUserDataList)
		{
			string gameId = socialUserData.CustomData.GameId;
			if (!string.IsNullOrEmpty(gameId))
			{
				list.Add(gameId);
			}
		}
		return list;
	}

	// Token: 0x060043EE RID: 17390 RVA: 0x0015FAF0 File Offset: 0x0015DCF0
	public static SocialUserData GetSocialUserDataFromGameId(List<SocialUserData> socialUserDataList, string gameId)
	{
		if (socialUserDataList != null)
		{
			foreach (SocialUserData socialUserData in socialUserDataList)
			{
				if (socialUserData.CustomData.GameId == gameId)
				{
					return socialUserData;
				}
			}
		}
		return null;
	}

	// Token: 0x060043EF RID: 17391 RVA: 0x0015FB70 File Offset: 0x0015DD70
	private void Awake()
	{
		if (SocialInterface.m_instance == null)
		{
			this.m_platform = base.gameObject.AddComponent<SocialPlatformFacebook>();
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			SocialInterface.m_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060043F0 RID: 17392 RVA: 0x0015FBC0 File Offset: 0x0015DDC0
	private void Update()
	{
	}

	// Token: 0x060043F1 RID: 17393 RVA: 0x0015FBC4 File Offset: 0x0015DDC4
	public void Initialize(GameObject callbackObject)
	{
		if (this.m_platform == null)
		{
			return;
		}
		this.m_platform.Initialize(callbackObject);
	}

	// Token: 0x060043F2 RID: 17394 RVA: 0x0015FBE4 File Offset: 0x0015DDE4
	public void Login(GameObject callbackObject)
	{
		if (this.m_platform == null)
		{
			return;
		}
		this.m_platform.Login(callbackObject);
	}

	// Token: 0x060043F3 RID: 17395 RVA: 0x0015FC04 File Offset: 0x0015DE04
	public void Logout()
	{
		if (this.m_platform == null)
		{
			return;
		}
		this.m_platform.Logout();
	}

	// Token: 0x060043F4 RID: 17396 RVA: 0x0015FC24 File Offset: 0x0015DE24
	public void RequestMyProfile(GameObject callbackObject)
	{
		if (this.m_platform == null)
		{
			return;
		}
		this.m_platform.RequestMyProfile(callbackObject);
	}

	// Token: 0x060043F5 RID: 17397 RVA: 0x0015FC44 File Offset: 0x0015DE44
	public void RequestFriendList(GameObject callbackObject)
	{
		if (this.m_platform == null)
		{
			return;
		}
		this.m_platform.RequestFriendList(callbackObject);
	}

	// Token: 0x060043F6 RID: 17398 RVA: 0x0015FC64 File Offset: 0x0015DE64
	public void SetScore(SocialDefine.ScoreType type, int score, GameObject callbackObject)
	{
		if (this.m_platform == null)
		{
			return;
		}
		this.m_platform.SetScore(type, score, callbackObject);
	}

	// Token: 0x060043F7 RID: 17399 RVA: 0x0015FC94 File Offset: 0x0015DE94
	public void CreateMyGameData(string gameId, GameObject callbackObject)
	{
		if (this.m_platform == null)
		{
			return;
		}
		this.m_platform.CreateMyGameData(gameId, callbackObject);
	}

	// Token: 0x060043F8 RID: 17400 RVA: 0x0015FCB8 File Offset: 0x0015DEB8
	public void RequestGameData(string userId, GameObject callbackObject)
	{
		if (this.m_platform == null)
		{
			return;
		}
		this.m_platform.RequestGameData(userId, callbackObject);
	}

	// Token: 0x060043F9 RID: 17401 RVA: 0x0015FCDC File Offset: 0x0015DEDC
	public void DeleteGameData(GameObject callbackObject)
	{
		if (this.m_platform == null)
		{
			return;
		}
		this.m_platform.DeleteGameData(callbackObject);
	}

	// Token: 0x060043FA RID: 17402 RVA: 0x0015FCFC File Offset: 0x0015DEFC
	public void InviteFriend(GameObject callbackObject)
	{
		if (this.m_platform == null)
		{
			return;
		}
		this.m_platform.InviteFriend(callbackObject);
	}

	// Token: 0x060043FB RID: 17403 RVA: 0x0015FD1C File Offset: 0x0015DF1C
	public void SendEnergy(SocialUserData userData, GameObject callbackObject)
	{
		if (this.m_platform == null)
		{
			return;
		}
		this.m_platform.SendEnergy(userData, callbackObject);
	}

	// Token: 0x060043FC RID: 17404 RVA: 0x0015FD40 File Offset: 0x0015DF40
	public void Feed(string feedCaption, string feedText, GameObject callbackObject)
	{
		if (this.m_platform == null)
		{
			return;
		}
		this.m_platform.Feed(feedCaption, feedText, callbackObject);
	}

	// Token: 0x060043FD RID: 17405 RVA: 0x0015FD70 File Offset: 0x0015DF70
	public void RequestInvitedFriend(GameObject callbackObject)
	{
		if (this.m_platform == null)
		{
			return;
		}
		this.m_platform.RequestInvitedFriend(callbackObject);
	}

	// Token: 0x060043FE RID: 17406 RVA: 0x0015FD90 File Offset: 0x0015DF90
	public void RequestPermission(GameObject callbackObject)
	{
		if (this.m_platform == null)
		{
			return;
		}
		this.m_platform.RequestPermission(callbackObject);
	}

	// Token: 0x060043FF RID: 17407 RVA: 0x0015FDB0 File Offset: 0x0015DFB0
	public void AddPermission(List<SocialInterface.Permission> permissions, GameObject callbackObject)
	{
		if (this.m_platform == null)
		{
			return;
		}
		this.m_platform.AddPermission(permissions, callbackObject);
	}

	// Token: 0x06004400 RID: 17408 RVA: 0x0015FDD4 File Offset: 0x0015DFD4
	public void RequestFriendRankingInfoSet(string gameObjectName, string functionName, SettingPartsSnsAdditional.Mode mode)
	{
		global::Debug.Log("RequestFriendRankingInfoSet");
		SettingPartsSnsAdditional settingPartsSnsAdditional = base.gameObject.GetComponent<SettingPartsSnsAdditional>();
		if (settingPartsSnsAdditional == null)
		{
			settingPartsSnsAdditional = base.gameObject.AddComponent<SettingPartsSnsAdditional>();
		}
		settingPartsSnsAdditional.PlayStart(gameObjectName, functionName, mode);
	}

	// Token: 0x04003961 RID: 14689
	private SocialPlatform m_platform;

	// Token: 0x04003962 RID: 14690
	private bool m_isInitialized;

	// Token: 0x04003963 RID: 14691
	private bool m_isLoggedIn;

	// Token: 0x04003964 RID: 14692
	private SocialUserData m_myProfile = new SocialUserData();

	// Token: 0x04003965 RID: 14693
	private bool m_isEnableFriendInfo;

	// Token: 0x04003966 RID: 14694
	private List<SocialUserData> m_allFriendList = new List<SocialUserData>();

	// Token: 0x04003967 RID: 14695
	private List<SocialUserData> m_friendList;

	// Token: 0x04003968 RID: 14696
	private List<SocialUserData> m_notInstalledFriendList = new List<SocialUserData>();

	// Token: 0x04003969 RID: 14697
	private List<SocialUserData> m_invitedFriendList;

	// Token: 0x0400396A RID: 14698
	private bool[] m_isGrantedPermission = new bool[2];

	// Token: 0x0400396B RID: 14699
	private static SocialInterface m_instance;

	// Token: 0x02000A0C RID: 2572
	public enum Permission
	{
		// Token: 0x0400396D RID: 14701
		PUBLIC_PROFILE,
		// Token: 0x0400396E RID: 14702
		USER_FRIENDS,
		// Token: 0x0400396F RID: 14703
		NUM
	}
}
