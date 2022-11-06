using System;
using System.Collections.Generic;
using DataTable;
using GooglePlayGames;
using SaveData;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

// Token: 0x02000243 RID: 579
public class AchievementManager : MonoBehaviour
{
	// Token: 0x1700026E RID: 622
	// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0005DDE0 File Offset: 0x0005BFE0
	public static AchievementManager Instance
	{
		get
		{
			return AchievementManager.m_instance;
		}
	}

	// Token: 0x06000FFE RID: 4094 RVA: 0x0005DDE8 File Offset: 0x0005BFE8
	private void Awake()
	{
		if (AchievementManager.m_instance == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			AchievementManager.m_instance = this;
			base.gameObject.AddComponent<HudNetworkConnect>();
			PlayGamesPlatform.DebugLogEnabled = false;
			PlayGamesPlatform.Activate();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
			this.DestroyDataTable();
		}
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x0005DE44 File Offset: 0x0005C044
	private void OnDestroy()
	{
		if (AchievementManager.m_instance == this)
		{
			AchievementManager.m_instance = null;
		}
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x0005DE5C File Offset: 0x0005C05C
	private void Update()
	{
		if (this.m_state == AchievementManager.State.Report && (this.m_reportCount == this.m_reportSuccessCount || this.IsRequestEndAchievement()))
		{
			this.SetDebugDraw(string.Concat(new object[]
			{
				"ReportResult m_reportCount=",
				this.m_reportCount,
				" m_reportSuccessCount=",
				this.m_reportSuccessCount
			}));
			this.SetNetworkConnect(false);
			this.RequestAchievementIncentive(this.m_reportSuccessCount);
			this.m_state = AchievementManager.State.RequestIncentive;
		}
		if (this.m_state == AchievementManager.State.RequestIncentive && this.IsRequestEndIncentive())
		{
			this.m_state = AchievementManager.State.RequestEnd;
		}
		if (this.m_waitTime > 0f)
		{
			this.m_waitTime -= Time.deltaTime;
			if (this.m_waitTime < 0f)
			{
				this.SetNetworkConnect(false);
				this.m_waitTime = 0f;
			}
		}
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x0005DF4C File Offset: 0x0005C14C
	public void RequestUpdateAchievement()
	{
		if (!this.IsSetupEndAchievement())
		{
			this.SetDebugDraw("RequestUpdateAchievement Not Setup m_state=" + this.m_state.ToString());
			return;
		}
		this.SetDebugDraw("RequestUpdateAchievement m_state=" + this.m_state.ToString());
		switch (this.m_state)
		{
		case AchievementManager.State.Idle:
			this.SetNetworkConnect(true);
			this.Authenticate();
			break;
		case AchievementManager.State.LoadAchievementError:
			this.SetNetworkConnect(true);
			this.LoadAchievements();
			break;
		case AchievementManager.State.RequestEnd:
			this.SetNetworkConnect(true);
			this.LoadAchievements2();
			break;
		}
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x0005E018 File Offset: 0x0005C218
	public void RequestResetAchievement()
	{
		this.SetDebugDraw("RequestResetAchievement m_state=" + this.m_state.ToString());
		this.ResetAchievements();
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x0005E04C File Offset: 0x0005C24C
	public void ShowAchievementsUI()
	{
		this.SetDebugDraw("ShowAchievementsUI");
		Social.ShowAchievementsUI();
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x0005E060 File Offset: 0x0005C260
	public bool IsSetupEndAchievement()
	{
		return this.m_setupDataTable;
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x0005E068 File Offset: 0x0005C268
	public bool IsRequestEndAchievement()
	{
		if (this.IsSetupEndAchievement())
		{
			switch (this.m_state)
			{
			case AchievementManager.State.Authenticate:
			case AchievementManager.State.LoadAchievement:
			case AchievementManager.State.Report:
			case AchievementManager.State.RequestIncentive:
				return this.m_waitTime.Equals(0f);
			}
		}
		return true;
	}

	// Token: 0x06001006 RID: 4102 RVA: 0x0005E0CC File Offset: 0x0005C2CC
	private void Authenticate()
	{
		this.SetDebugDraw("Authenticate");
		this.m_state = AchievementManager.State.Authenticate;
		this.SetWaitTime();
		if (!Social.localUser.authenticated)
		{
			Social.localUser.Authenticate(new Action<bool>(this.ProcessAuthentication));
		}
		else
		{
			this.ProcessAuthentication(false);
		}
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x0005E124 File Offset: 0x0005C324
	private void LoadAchievements()
	{
		this.SetDebugDraw("LoadAchievements");
		this.m_state = AchievementManager.State.LoadAchievement;
		this.SetWaitTime();
		if (this.m_setupDataTable && this.m_data.Count > 0)
		{
			string[] array = new string[this.m_data.Count];
			for (int i = 0; i < this.m_data.Count; i++)
			{
				array[i] = this.m_data[i].GetID();
			}
			PlayGamesPlatform.Instance.LoadAchievementDescriptions(array, new Action<IAchievement[]>(this.ProcessLoadedAchievements1));
		}
		else
		{
			this.ProcessLoadedAchievements1(null);
		}
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x0005E1CC File Offset: 0x0005C3CC
	private void LoadAchievements2()
	{
		if (this.m_setupDataTable && this.m_data.Count > 0)
		{
			string[] array = new string[this.m_data.Count];
			for (int i = 0; i < this.m_data.Count; i++)
			{
				array[i] = this.m_data[i].GetID();
			}
			PlayGamesPlatform.Instance.LoadAchievements(array, new Action<IAchievement[]>(this.ProcessLoadedAchievements2));
		}
		else
		{
			this.ProcessLoadedAchievements2(null);
		}
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x0005E25C File Offset: 0x0005C45C
	private void ReportProgress()
	{
		this.SetDebugDraw("ReportProgress");
		this.m_state = AchievementManager.State.Report;
		this.SetWaitTime();
		this.m_reportCount = 0;
		this.m_reportSuccessCount = 0;
		if (this.m_loadData.Count > 0)
		{
			this.UpdateAchievement();
			this.SetDebugDraw(string.Concat(new object[]
			{
				"m_loadData=",
				this.m_loadData.Count,
				" m_clearData=",
				this.m_clearData.Count
			}));
			if (this.m_clearData.Count > 0)
			{
				foreach (AchievementTempData achievementTempData in this.m_loadData)
				{
					if (!achievementTempData.m_reportEnd && this.IsClearAchievement(achievementTempData.m_id))
					{
						this.m_reportCount++;
						Social.ReportProgress(achievementTempData.m_id, 100.0, new Action<bool>(this.ProcessReportProgress));
					}
				}
			}
		}
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x0005E39C File Offset: 0x0005C59C
	private void ResetAchievements()
	{
		this.SetDebugDraw("ResetAchievements");
		this.m_state = AchievementManager.State.Idle;
		GameCenterPlatform.ResetAllAchievements(new Action<bool>(this.ProcessResetAllAchievements));
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x0005E3C4 File Offset: 0x0005C5C4
	private void ProcessAuthentication(bool success)
	{
		if (success)
		{
			this.LoadAchievements();
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = SystemSaveManager.Instance.GetSystemdata();
				if (systemdata != null && systemdata.achievementCancelCount != 0)
				{
					systemdata.achievementCancelCount = 0;
					instance.SaveSystemData();
				}
			}
		}
		else
		{
			this.SetNetworkConnect(false);
			this.m_state = AchievementManager.State.AuthenticateError;
			this.SetDebugDraw("Authenticate ERROR");
			SystemSaveManager instance2 = SystemSaveManager.Instance;
			if (instance2 != null && Application.loadedLevelName == TitleDefine.TitleSceneName)
			{
				SystemData systemdata2 = SystemSaveManager.Instance.GetSystemdata();
				if (systemdata2 != null)
				{
					systemdata2.achievementCancelCount++;
					instance2.SaveSystemData();
				}
			}
		}
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x0005E484 File Offset: 0x0005C684
	private void SetLoadedAchievements(string[] achievementsIDList)
	{
		if (achievementsIDList == null || achievementsIDList.Length == 0)
		{
			this.SetNetworkConnect(false);
			this.m_state = AchievementManager.State.LoadAchievementError;
			this.SetDebugDraw("LoadAchievements ERROR");
		}
		else
		{
			this.SetDebugDraw("LoadAchievements1 OK " + achievementsIDList.Length + " achievements");
			this.m_loadData.Clear();
			foreach (string text in achievementsIDList)
			{
				if (text != null)
				{
					this.SetDebugDraw("Load achievementID=" + text);
					this.m_loadData.Add(new AchievementTempData(text));
				}
			}
			this.LoadAchievements2();
		}
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x0005E530 File Offset: 0x0005C730
	private void ProcessLoadedAchievements1(IAchievement[] achievements)
	{
		if (achievements == null || achievements.Length == 0)
		{
			this.SetLoadedAchievements(null);
		}
		else
		{
			string[] array = new string[achievements.Length];
			for (int i = 0; i < achievements.Length; i++)
			{
				array[i] = achievements[i].id;
			}
			this.SetLoadedAchievements(array);
		}
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x0005E588 File Offset: 0x0005C788
	private void ProcessLoadedAchievements2(IAchievement[] achievements)
	{
		if (achievements != null && achievements.Length > 0)
		{
			this.SetDebugDraw("LoadAchievements2 " + achievements.Length + " achievements");
			foreach (IAchievement achievement in achievements)
			{
				if (achievement != null)
				{
					this.SetReporteEnd(achievement.id);
				}
			}
		}
		this.ReportProgress();
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x0005E5F4 File Offset: 0x0005C7F4
	private void ProcessReportProgress(bool result)
	{
		if (result)
		{
			this.m_reportSuccessCount++;
			this.SetDebugDraw("ReportProgress OK");
		}
		else
		{
			this.SetDebugDraw("ReportProgress ERROR");
		}
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x0005E628 File Offset: 0x0005C828
	private void ProcessResetAllAchievements(bool result)
	{
		if (result)
		{
			this.SetDebugDraw("ProcessResetAllAchievements OK");
		}
		else
		{
			this.SetDebugDraw("ProcessResetAllAchievements ERROR");
		}
	}

	// Token: 0x06001011 RID: 4113 RVA: 0x0005E64C File Offset: 0x0005C84C
	private void UpdateAchievement()
	{
		this.m_clearData.Clear();
		ServerPlayerState playerState = ServerInterface.PlayerState;
		if (playerState != null)
		{
			this.SetDebugDraw2("DataTable m_data.Count=" + this.m_data.Count);
			for (int i = 0; i < this.m_data.Count; i++)
			{
				AchievementData achievementData = this.m_data[i];
				if (achievementData != null)
				{
					this.SetDebugDraw2(string.Concat(new object[]
					{
						"number=",
						achievementData.number,
						" explanation=",
						achievementData.explanation,
						" type=",
						achievementData.type.ToString(),
						" itemID=",
						achievementData.itemID,
						" value=",
						achievementData.value,
						" id=",
						achievementData.GetID()
					}));
					if (achievementData.GetID() != null && achievementData.GetID() != string.Empty)
					{
						bool flag = false;
						switch (achievementData.type)
						{
						case AchievementData.Type.ANIMAL:
							flag = this.CheckClearAnimal(achievementData, playerState.m_numAnimals);
							break;
						case AchievementData.Type.DISTANCE:
							flag = this.CheckClearDistance(achievementData, (uint)playerState.m_totalDistance);
							break;
						case AchievementData.Type.PLAYER_OPEN:
							flag = this.CheckClearPlayerOpen(achievementData, playerState.CharacterStateByItemID(achievementData.itemID));
							break;
						case AchievementData.Type.PLAYER_LEVEL:
							flag = this.CheckClearPlayerLevel(achievementData, playerState.CharacterStateByItemID(achievementData.itemID));
							break;
						case AchievementData.Type.CHAO_OPEN:
							flag = this.CheckClearChaoOpen(achievementData, playerState.ChaoStateByItemID(achievementData.itemID));
							break;
						case AchievementData.Type.CHAO_LEVEL:
							flag = this.CheckClearChaoLevel(achievementData, playerState.ChaoStateByItemID(achievementData.itemID));
							break;
						}
						string text = string.Empty;
						if (this.IsDebugAllOpen())
						{
							flag = true;
							text = "DebugAllOpen ";
						}
						if (flag)
						{
							this.SetDebugDraw2(string.Concat(new string[]
							{
								text,
								"Clear!! ID=",
								achievementData.GetID(),
								" / ",
								achievementData.explanation
							}));
							this.m_clearData.Add(new AchievementTempData(achievementData.GetID()));
						}
					}
				}
			}
		}
	}

	// Token: 0x06001012 RID: 4114 RVA: 0x0005E89C File Offset: 0x0005CA9C
	private bool CheckClearAnimal(AchievementData data, int animal)
	{
		if (data != null)
		{
			this.SetDebugDraw2(string.Concat(new object[]
			{
				"animal=",
				animal,
				" / data.value=",
				data.value
			}));
			if (this.IsClear((uint)animal, (uint)data.value))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001013 RID: 4115 RVA: 0x0005E8FC File Offset: 0x0005CAFC
	private bool CheckClearDistance(AchievementData data, uint distance)
	{
		if (data != null)
		{
			this.SetDebugDraw2(string.Concat(new object[]
			{
				"distance=",
				distance,
				" / data.value=",
				data.value
			}));
			if (this.IsClear(distance, (uint)data.value))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x0005E95C File Offset: 0x0005CB5C
	private bool CheckClearPlayerOpen(AchievementData data, ServerCharacterState state)
	{
		if (data != null && state != null)
		{
			this.SetDebugDraw2("player IsUnlocked=" + state.IsUnlocked.ToString());
			if (state.IsUnlocked)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001015 RID: 4117 RVA: 0x0005E9A4 File Offset: 0x0005CBA4
	private bool CheckClearPlayerLevel(AchievementData data, ServerCharacterState state)
	{
		if (data != null && state != null)
		{
			this.SetDebugDraw2(string.Concat(new object[]
			{
				"player IsUnlocked=",
				state.IsUnlocked.ToString(),
				" level=",
				state.Level,
				" / data.value=",
				data.value
			}));
			if (state.IsUnlocked && this.IsClear((uint)state.Level, (uint)data.value))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001016 RID: 4118 RVA: 0x0005EA38 File Offset: 0x0005CC38
	private bool CheckClearChaoOpen(AchievementData data, ServerChaoState state)
	{
		if (data != null && state != null)
		{
			this.SetDebugDraw2("chao IsOwned=" + state.IsOwned.ToString());
			if (state.IsOwned)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001017 RID: 4119 RVA: 0x0005EA80 File Offset: 0x0005CC80
	private bool CheckClearChaoLevel(AchievementData data, ServerChaoState state)
	{
		if (data != null && state != null)
		{
			this.SetDebugDraw2(string.Concat(new object[]
			{
				"chao IsOwned=",
				state.IsOwned.ToString(),
				" level=",
				state.Level,
				" / data.value=",
				data.value
			}));
			if (state.IsOwned && this.IsClear((uint)state.Level, (uint)data.value))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001018 RID: 4120 RVA: 0x0005EB14 File Offset: 0x0005CD14
	private bool IsClear(uint myParam, uint cmpParam)
	{
		return myParam >= cmpParam;
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x0005EB20 File Offset: 0x0005CD20
	private bool IsClearAchievement(string id)
	{
		foreach (AchievementTempData achievementTempData in this.m_clearData)
		{
			if (achievementTempData.m_id == id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x0005EB9C File Offset: 0x0005CD9C
	private void SetReporteEnd(string id)
	{
		foreach (AchievementTempData achievementTempData in this.m_loadData)
		{
			if (achievementTempData.m_id == id)
			{
				achievementTempData.m_reportEnd = true;
				break;
			}
		}
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x0005EC18 File Offset: 0x0005CE18
	public void SetupDataAchievementTable()
	{
		if (this.m_setupDataTable || this.m_data.Count != 0)
		{
			return;
		}
		AchievementTable achievementTable = GameObjectUtil.FindGameObjectComponent<AchievementTable>("AchievementTable");
		if (achievementTable != null)
		{
			this.m_data.Clear();
			AchievementData[] dataTable = AchievementTable.GetDataTable();
			if (dataTable != null)
			{
				foreach (AchievementData item in dataTable)
				{
					this.m_data.Add(item);
				}
			}
			this.SetDebugDraw("SetupDataAchievementTable m_data=" + this.m_data.Count);
			UnityEngine.Object.Destroy(achievementTable.gameObject);
		}
		else
		{
			this.SetDebugDraw("SetupDataAchievementTable Error");
		}
		this.m_setupDataTable = true;
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x0005ECDC File Offset: 0x0005CEDC
	public void SkipAuthenticate()
	{
		this.m_state = AchievementManager.State.AuthenticateSkip;
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x0005ECE8 File Offset: 0x0005CEE8
	private void DestroyDataTable()
	{
		AchievementTable achievementTable = GameObjectUtil.FindGameObjectComponent<AchievementTable>("AchievementTable");
		if (achievementTable != null)
		{
			UnityEngine.Object.Destroy(achievementTable.gameObject);
		}
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x0005ED18 File Offset: 0x0005CF18
	private void SetWaitTime()
	{
		this.m_waitTime = AchievementManager.WAIT_TIME;
	}

	// Token: 0x0600101F RID: 4127 RVA: 0x0005ED28 File Offset: 0x0005CF28
	private void RequestAchievementIncentive(int incentivCount)
	{
		if (incentivCount > 0)
		{
			AchievementIncentive.AddAchievementIncentiveCount(incentivCount);
		}
		if (this.m_achievementIncentive == null)
		{
			this.SetWaitTime();
			GameObject gameObject = new GameObject("AchievementIncentive");
			this.m_achievementIncentive = gameObject.AddComponent<AchievementIncentive>();
			if (this.m_achievementIncentive != null)
			{
				this.m_achievementIncentive.RequestServer();
			}
			this.SetDebugDraw("RequestAchievementIncentive RequestServer");
		}
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x0005ED98 File Offset: 0x0005CF98
	private bool IsRequestEndIncentive()
	{
		if (this.m_achievementIncentive != null)
		{
			AchievementIncentive.State state = this.m_achievementIncentive.GetState();
			if (state == AchievementIncentive.State.Request)
			{
				return false;
			}
			UnityEngine.Object.Destroy(this.m_achievementIncentive.gameObject);
			this.m_achievementIncentive = null;
			this.SetDebugDraw("IsRequestEndIncentive Destroy state=" + state.ToString());
		}
		return true;
	}

	// Token: 0x06001021 RID: 4129 RVA: 0x0005EE00 File Offset: 0x0005D000
	private void SetNetworkConnect(bool on)
	{
		if (this.m_connectAnim == on)
		{
			return;
		}
		HudNetworkConnect component = base.gameObject.GetComponent<HudNetworkConnect>();
		if (component != null)
		{
			if (on)
			{
				this.SetDebugDraw("SetNetworkConnect PlayStart");
				component.Setup();
				component.PlayStart(HudNetworkConnect.DisplayType.ALL);
				this.m_connectAnim = true;
			}
			else
			{
				this.SetDebugDraw("SetNetworkConnect PlayEnd");
				component.PlayEnd();
				this.m_connectAnim = false;
			}
		}
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x0005EE78 File Offset: 0x0005D078
	private void SetDebugDraw(string msg)
	{
	}

	// Token: 0x06001023 RID: 4131 RVA: 0x0005EE7C File Offset: 0x0005D07C
	private void SetDebugDraw2(string msg)
	{
	}

	// Token: 0x06001024 RID: 4132 RVA: 0x0005EE80 File Offset: 0x0005D080
	private bool IsDebugAllOpen()
	{
		return false;
	}

	// Token: 0x06001025 RID: 4133 RVA: 0x0005EE84 File Offset: 0x0005D084
	public static AchievementManager GetAchievementManager()
	{
		AchievementManager achievementManager = AchievementManager.Instance;
		if (achievementManager == null)
		{
			GameObject gameObject = new GameObject("AchievementManager");
			achievementManager = gameObject.AddComponent<AchievementManager>();
		}
		return achievementManager;
	}

	// Token: 0x06001026 RID: 4134 RVA: 0x0005EEB8 File Offset: 0x0005D0B8
	public static void Setup()
	{
		AchievementManager instance = AchievementManager.Instance;
		if (instance != null)
		{
			instance.SetupDataAchievementTable();
		}
	}

	// Token: 0x06001027 RID: 4135 RVA: 0x0005EEE0 File Offset: 0x0005D0E0
	public static bool IsSetupEnd()
	{
		AchievementManager instance = AchievementManager.Instance;
		return !(instance != null) || instance.IsSetupEndAchievement();
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x0005EF08 File Offset: 0x0005D108
	public static void RequestSkipAuthenticate()
	{
		AchievementManager instance = AchievementManager.Instance;
		if (instance != null)
		{
			instance.SkipAuthenticate();
		}
	}

	// Token: 0x06001029 RID: 4137 RVA: 0x0005EF30 File Offset: 0x0005D130
	public static void RequestUpdate()
	{
		AchievementManager achievementManager = AchievementManager.GetAchievementManager();
		if (achievementManager != null)
		{
			achievementManager.RequestUpdateAchievement();
		}
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x0005EF58 File Offset: 0x0005D158
	public static bool IsRequestEnd()
	{
		AchievementManager instance = AchievementManager.Instance;
		return !(instance != null) || instance.IsRequestEndAchievement();
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x0005EF80 File Offset: 0x0005D180
	public static void RequestReset()
	{
		AchievementManager achievementManager = AchievementManager.GetAchievementManager();
		if (achievementManager != null)
		{
			achievementManager.RequestResetAchievement();
		}
	}

	// Token: 0x0600102C RID: 4140 RVA: 0x0005EFA8 File Offset: 0x0005D1A8
	public static void RequestShowAchievementsUI()
	{
		AchievementManager achievementManager = AchievementManager.GetAchievementManager();
		if (achievementManager != null)
		{
			if (!Social.localUser.authenticated)
			{
				achievementManager.Authenticate();
			}
			achievementManager.ShowAchievementsUI();
		}
	}

	// Token: 0x0600102D RID: 4141 RVA: 0x0005EFE4 File Offset: 0x0005D1E4
	public static void RequestDebugInfo(bool flag)
	{
		AchievementManager achievementManager = AchievementManager.GetAchievementManager();
		if (achievementManager != null)
		{
			achievementManager.m_debugInfo = flag;
		}
	}

	// Token: 0x0600102E RID: 4142 RVA: 0x0005F00C File Offset: 0x0005D20C
	public static void RequestDebugAllOpen(bool flag)
	{
		AchievementManager achievementManager = AchievementManager.GetAchievementManager();
		if (achievementManager != null)
		{
			achievementManager.m_debugAllOpen = flag;
		}
	}

	// Token: 0x04000DAE RID: 3502
	public bool m_debugInfo = true;

	// Token: 0x04000DAF RID: 3503
	private bool m_debugInfo2;

	// Token: 0x04000DB0 RID: 3504
	public bool m_debugAllOpen;

	// Token: 0x04000DB1 RID: 3505
	private AchievementManager.State m_state;

	// Token: 0x04000DB2 RID: 3506
	private int m_reportCount;

	// Token: 0x04000DB3 RID: 3507
	private int m_reportSuccessCount;

	// Token: 0x04000DB4 RID: 3508
	private List<AchievementTempData> m_loadData = new List<AchievementTempData>();

	// Token: 0x04000DB5 RID: 3509
	private List<AchievementTempData> m_clearData = new List<AchievementTempData>();

	// Token: 0x04000DB6 RID: 3510
	private List<AchievementData> m_data = new List<AchievementData>();

	// Token: 0x04000DB7 RID: 3511
	private bool m_setupDataTable;

	// Token: 0x04000DB8 RID: 3512
	private float m_waitTime;

	// Token: 0x04000DB9 RID: 3513
	private static float WAIT_TIME = 10f;

	// Token: 0x04000DBA RID: 3514
	private bool m_connectAnim;

	// Token: 0x04000DBB RID: 3515
	private AchievementIncentive m_achievementIncentive;

	// Token: 0x04000DBC RID: 3516
	private static AchievementManager m_instance;

	// Token: 0x02000244 RID: 580
	private enum State
	{
		// Token: 0x04000DBE RID: 3518
		Idle,
		// Token: 0x04000DBF RID: 3519
		Authenticate,
		// Token: 0x04000DC0 RID: 3520
		AuthenticateError,
		// Token: 0x04000DC1 RID: 3521
		AuthenticateSkip,
		// Token: 0x04000DC2 RID: 3522
		LoadAchievement,
		// Token: 0x04000DC3 RID: 3523
		LoadAchievementError,
		// Token: 0x04000DC4 RID: 3524
		Report,
		// Token: 0x04000DC5 RID: 3525
		RequestIncentive,
		// Token: 0x04000DC6 RID: 3526
		RequestEnd
	}
}
