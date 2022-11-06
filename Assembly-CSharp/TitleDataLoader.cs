using System;
using System.Collections.Generic;
using DataTable;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000336 RID: 822
public class TitleDataLoader : MonoBehaviour
{
	// Token: 0x06001867 RID: 6247 RVA: 0x0008D420 File Offset: 0x0008B620
	private void Start()
	{
	}

	// Token: 0x06001868 RID: 6248 RVA: 0x0008D424 File Offset: 0x0008B624
	public void AddStreamingSoundData(string data)
	{
		this.m_streamingSoundData.Add(data);
	}

	// Token: 0x06001869 RID: 6249 RVA: 0x0008D434 File Offset: 0x0008B634
	public void Setup(bool is_first)
	{
		this.Init(is_first);
		this.m_requestDownloadCount = 0;
		this.m_requestLoadCount = this.m_loadInfo.Count + this.m_streamingSoundData.Count + 1;
		this.m_endCheckExistingCheckDownloadData = false;
	}

	// Token: 0x0600186A RID: 6250 RVA: 0x0008D478 File Offset: 0x0008B678
	private void OnDestroy()
	{
		if (this.m_fsm_behavior)
		{
			this.m_fsm_behavior.ShutDown();
			this.m_fsm_behavior = null;
		}
	}

	// Token: 0x0600186B RID: 6251 RVA: 0x0008D4A8 File Offset: 0x0008B6A8
	private void Update()
	{
	}

	// Token: 0x170003B2 RID: 946
	// (get) Token: 0x0600186C RID: 6252 RVA: 0x0008D4AC File Offset: 0x0008B6AC
	public bool LoadEnd
	{
		get
		{
			return this.m_loadEnd;
		}
	}

	// Token: 0x170003B3 RID: 947
	// (get) Token: 0x0600186D RID: 6253 RVA: 0x0008D4B4 File Offset: 0x0008B6B4
	// (set) Token: 0x0600186E RID: 6254 RVA: 0x0008D518 File Offset: 0x0008B718
	public int LoadEndCount
	{
		get
		{
			int num = (!(this.m_sceneLoader == null)) ? this.m_sceneLoader.LoadEndCount : 0;
			num += StreamingDataLoader.Instance.NumLoaded;
			if (InformationDataTable.Instance != null && InformationDataTable.Instance.Loaded)
			{
				num++;
			}
			return num;
		}
		private set
		{
		}
	}

	// Token: 0x170003B4 RID: 948
	// (get) Token: 0x0600186F RID: 6255 RVA: 0x0008D51C File Offset: 0x0008B71C
	public int RequestedLoadCount
	{
		get
		{
			return this.m_requestLoadCount;
		}
	}

	// Token: 0x170003B5 RID: 949
	// (get) Token: 0x06001870 RID: 6256 RVA: 0x0008D524 File Offset: 0x0008B724
	public int RequestedDownloadCount
	{
		get
		{
			return this.m_requestDownloadCount;
		}
	}

	// Token: 0x170003B6 RID: 950
	// (get) Token: 0x06001871 RID: 6257 RVA: 0x0008D52C File Offset: 0x0008B72C
	public bool EndCheckExistingDownloadData
	{
		get
		{
			return this.m_endCheckExistingCheckDownloadData;
		}
	}

	// Token: 0x06001872 RID: 6258 RVA: 0x0008D534 File Offset: 0x0008B734
	public void StartLoad()
	{
		if (this.m_fsm_behavior != null)
		{
			if (StreamingDataLoader.Instance.NumInLoadList > 0)
			{
				this.m_fsm_behavior.ChangeState(new TinyFsmState(new EventFunction(this.StateLoadStreaming)));
			}
			else
			{
				this.m_fsm_behavior.ChangeState(new TinyFsmState(new EventFunction(this.StateLoadScene)));
			}
		}
	}

	// Token: 0x06001873 RID: 6259 RVA: 0x0008D5A4 File Offset: 0x0008B7A4
	public void RetryStreamingDataLoad(int retryCount)
	{
		StreamingDataLoadRetryProcess process = new StreamingDataLoadRetryProcess(retryCount, base.gameObject, this);
		NetMonitor.Instance.StartMonitor(process, -1f, HudNetworkConnect.DisplayType.ALL);
		StreamingDataLoader.Instance.StartDownload(retryCount, base.gameObject);
	}

	// Token: 0x06001874 RID: 6260 RVA: 0x0008D5E4 File Offset: 0x0008B7E4
	public void RetryInformationDataLoad()
	{
		InformationDataLoadRetryProcess process = new InformationDataLoadRetryProcess(base.gameObject, this);
		NetMonitor.Instance.StartMonitor(process, 0f, HudNetworkConnect.DisplayType.ALL);
		InformationDataTable.Instance.Initialize(base.gameObject);
	}

	// Token: 0x06001875 RID: 6261 RVA: 0x0008D620 File Offset: 0x0008B820
	private void Init(bool is_first)
	{
		if (ResourceManager.Instance == null)
		{
			GameObject gameObject = new GameObject("ResourceManager");
			gameObject.AddComponent<ResourceManager>();
		}
		this.m_loadInfo = new List<ResourceSceneLoader.ResourceInfo>();
		if (is_first)
		{
			foreach (ResourceSceneLoader.ResourceInfo item in this.m_defaultLoadInfoFirst)
			{
				this.m_loadInfo.Add(item);
			}
		}
		else
		{
			foreach (ResourceSceneLoader.ResourceInfo item2 in this.m_defaultLoadInfo)
			{
				this.m_loadInfo.Add(item2);
			}
		}
		string suffixe = TextUtility.GetSuffixe();
		string name = "text_common_text_" + suffixe;
		string name2 = "text_event_common_text_" + suffixe;
		string name3 = "text_chao_text_" + suffixe;
		this.m_loadInfo.Add(new ResourceSceneLoader.ResourceInfo(ResourceCategory.ETC, name, true, false, false, null, false));
		this.m_loadInfo.Add(new ResourceSceneLoader.ResourceInfo(ResourceCategory.ETC, name3, true, false, false, null, false));
		if (!is_first)
		{
			this.m_loadInfo.Add(new ResourceSceneLoader.ResourceInfo(ResourceCategory.ETC, name2, true, false, false, null, false));
		}
		if (!is_first)
		{
			this.AddSceneLoaderChaoTexture();
		}
		this.m_fsm_behavior = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
		if (this.m_fsm_behavior != null)
		{
			TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
			description.initState = new TinyFsmState(new EventFunction(this.StateAddDownloadFile));
			this.m_fsm_behavior.SetUp(description);
		}
		GameObject gameObject2 = new GameObject("ResourceSceneLoader");
		this.m_sceneLoader = gameObject2.AddComponent<ResourceSceneLoader>();
		if (this.m_sceneLoader != null)
		{
			this.m_sceneLoader.Pause(true);
		}
	}

	// Token: 0x06001876 RID: 6262 RVA: 0x0008D844 File Offset: 0x0008BA44
	private void AddSceneLoaderChaoTexture()
	{
		GameObject gameObject = GameObject.Find("AssetBundleLoader");
		if (gameObject != null)
		{
			AssetBundleLoader component = gameObject.GetComponent<AssetBundleLoader>();
			if (component != null)
			{
				string[] chaoTextureList = component.GetChaoTextureList();
				foreach (string name in chaoTextureList)
				{
					this.m_loadInfo.Add(new ResourceSceneLoader.ResourceInfo(ResourceCategory.ETC, name, true, true, false, null, false));
				}
			}
		}
	}

	// Token: 0x06001877 RID: 6263 RVA: 0x0008D8BC File Offset: 0x0008BABC
	private TinyFsmState StateAddDownloadFile(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
		{
			string[] array = new string[]
			{
				"CharacterEffectSonic",
				"CharacterModelSonic",
				"w01_StageResource",
				"w01_TerrainData",
				"TenseEffectTable"
			};
			foreach (string scenename in array)
			{
				ResourceSceneLoader.ResourceInfo resourceInfo = new ResourceSceneLoader.ResourceInfo();
				resourceInfo.m_category = ResourceCategory.UNKNOWN;
				resourceInfo.m_scenename = scenename;
				resourceInfo.m_onlyDownload = true;
				resourceInfo.m_isAssetBundle = true;
				this.m_loadInfo.Add(resourceInfo);
			}
			this.m_fsm_behavior.ChangeState(new TinyFsmState(new EventFunction(this.StateCheckDownload)));
			return TinyFsmState.End();
		}
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001878 RID: 6264 RVA: 0x0008D9B0 File Offset: 0x0008BBB0
	private TinyFsmState StateCheckDownload(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
		{
			this.m_requestLoadCount = 0;
			int num = 0;
			if (AssetBundleLoader.Instance != null)
			{
				foreach (ResourceSceneLoader.ResourceInfo resourceInfo in this.m_loadInfo)
				{
					if (resourceInfo.m_isAssetBundle && !AssetBundleLoader.Instance.IsDownloaded(resourceInfo.m_scenename))
					{
						this.m_requestDownloadCount++;
					}
				}
			}
			this.m_nowLoadingList = new List<ResourceSceneLoader.ResourceInfo>();
			foreach (ResourceSceneLoader.ResourceInfo resourceInfo2 in this.m_loadInfo)
			{
				if (this.m_sceneLoader.AddLoadAndResourceManager(resourceInfo2))
				{
					this.m_nowLoadingList.Add(resourceInfo2);
				}
			}
			if (StreamingDataLoader.Instance != null)
			{
				foreach (string str in this.m_streamingSoundData)
				{
					string url = SoundManager.GetDownloadURL() + str;
					string path = SoundManager.GetDownloadedDataPath() + str;
					StreamingDataLoader.Instance.AddFileIfNotDownloaded(url, path);
				}
				this.m_requestDownloadCount += StreamingDataLoader.Instance.NumInLoadList;
				num = StreamingDataLoader.Instance.NumInLoadList;
			}
			this.m_requestLoadCount = this.m_nowLoadingList.Count + num + 1;
			this.m_endCheckExistingCheckDownloadData = true;
			this.m_fsm_behavior.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
			return TinyFsmState.End();
		}
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001879 RID: 6265 RVA: 0x0008DC00 File Offset: 0x0008BE00
	private TinyFsmState StateIdle(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600187A RID: 6266 RVA: 0x0008DC58 File Offset: 0x0008BE58
	private TinyFsmState StateLoadStreaming(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			StreamingDataLoadRetryProcess process = new StreamingDataLoadRetryProcess(0, base.gameObject, this);
			NetMonitor.Instance.StartMonitor(process, -1f, HudNetworkConnect.DisplayType.ALL);
			StreamingDataLoader.Instance.StartDownload(0, base.gameObject);
			return TinyFsmState.End();
		}
		default:
			if (signal != 100)
			{
				return TinyFsmState.End();
			}
			this.m_fsm_behavior.ChangeState(new TinyFsmState(new EventFunction(this.StateLoadScene)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
	}

	// Token: 0x0600187B RID: 6267 RVA: 0x0008DD0C File Offset: 0x0008BF0C
	private TinyFsmState StateLoadScene(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_sceneLoader.Pause(false);
			return TinyFsmState.End();
		case 4:
			if (this.m_sceneLoader.Loaded)
			{
				this.m_nowLoadingList = null;
				this.m_fsm_behavior.ChangeState(new TinyFsmState(new EventFunction(this.StateLoadInfoData)));
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600187C RID: 6268 RVA: 0x0008DDA4 File Offset: 0x0008BFA4
	private TinyFsmState StateLoadInfoData(TinyFsmEvent fsm_event)
	{
		int signal = fsm_event.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (InformationDataTable.Instance == null)
			{
				InformationDataLoadRetryProcess process = new InformationDataLoadRetryProcess(base.gameObject, this);
				NetMonitor.Instance.StartMonitor(process, 0f, HudNetworkConnect.DisplayType.ALL);
				InformationDataTable.Create();
				InformationDataTable.Instance.Initialize(base.gameObject);
			}
			return TinyFsmState.End();
		case 4:
			this.m_loadEnd = true;
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600187D RID: 6269 RVA: 0x0008DE44 File Offset: 0x0008C044
	private void StreamingDataLoad_Succeed()
	{
		if (this.m_fsm_behavior != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(100);
			this.m_fsm_behavior.Dispatch(signal);
		}
	}

	// Token: 0x0600187E RID: 6270 RVA: 0x0008DE78 File Offset: 0x0008C078
	private void StreamingDataLoad_Failed()
	{
		NetMonitor.Instance.EndMonitorForward(new MsgAssetBundleResponseFailedMonitor(), null, null);
		NetMonitor.Instance.EndMonitorBackward();
	}

	// Token: 0x0600187F RID: 6271 RVA: 0x0008DE98 File Offset: 0x0008C098
	private void InformationDataLoad_Succeed()
	{
		NetMonitor.Instance.EndMonitorForward(new MsgAssetBundleResponseSucceed(null, null), null, null);
		NetMonitor.Instance.EndMonitorBackward();
	}

	// Token: 0x06001880 RID: 6272 RVA: 0x0008DEB8 File Offset: 0x0008C0B8
	private void InformationDataLoad_Failed()
	{
		NetMonitor.Instance.EndMonitorForward(new MsgAssetBundleResponseFailedMonitor(), null, null);
		NetMonitor.Instance.EndMonitorBackward();
	}

	// Token: 0x040015F2 RID: 5618
	private TinyFsmBehavior m_fsm_behavior;

	// Token: 0x040015F3 RID: 5619
	private ResourceSceneLoader m_sceneLoader;

	// Token: 0x040015F4 RID: 5620
	private static readonly float LoadWaitTime = 60f;

	// Token: 0x040015F5 RID: 5621
	private static readonly int CountToAskGiveUp = 3;

	// Token: 0x040015F6 RID: 5622
	private bool m_Retry;

	// Token: 0x040015F7 RID: 5623
	private bool m_loadEnd;

	// Token: 0x040015F8 RID: 5624
	private List<ResourceSceneLoader.ResourceInfo> m_defaultLoadInfoFirst = new List<ResourceSceneLoader.ResourceInfo>
	{
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.COMMON_EFFECT, "ResourcesCommonEffect", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.PLAYER_COMMON, "CharacterCommonResource", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.OBJECT_RESOURCE, "CommonObjectResource", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.OBJECT_PREFAB, "CommonObjectPrefabs", false, false, true, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.ENEMY_RESOURCE, "CommonEnemyResource", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.ENEMY_PREFAB, "CommonEnemyPrefabs", false, false, true, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.ETC, "ChaoDataTable", true, false, true, "ChaoTable", false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.ETC, "CharaAbilityDataTable", true, false, true, "ImportAbilityTable", false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.ETC, "CharacterDataNameInfo", true, false, true, null, false)
	};

	// Token: 0x040015F9 RID: 5625
	private List<ResourceSceneLoader.ResourceInfo> m_defaultLoadInfo = new List<ResourceSceneLoader.ResourceInfo>
	{
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.COMMON_EFFECT, "ResourcesCommonEffect", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.PLAYER_COMMON, "CharacterCommonResource", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.OBJECT_RESOURCE, "CommonObjectResource", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.OBJECT_PREFAB, "CommonObjectPrefabs", false, false, true, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.ENEMY_RESOURCE, "CommonEnemyResource", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.ENEMY_PREFAB, "CommonEnemyPrefabs", false, false, true, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.ETC, "AchievementTable", true, false, true, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.ETC, "MissionTable", true, false, true, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.ETC, "ChaoDataTable", true, false, true, "ChaoTable", false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.ETC, "CharaAbilityDataTable", true, false, true, "ImportAbilityTable", false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.ETC, "CharacterDataNameInfo", true, false, true, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.ETC, "OverlapBonusTable", true, false, true, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.QUICK_MODE, "StageTimeTable", true, false, true, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "ChaoTextures", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "MainMenuPages", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "RouletteTopUI", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "ChaoWindows", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "ShopPage", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "ChaoSetUIPage", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "OptionWindows", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "ChaoSetWindowUI", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "DailyInfoUI", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "DailyWindowUI", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "DailybattleRewardWindowUI", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "InformationUI", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "ItemSet_3_UI", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "LoginWindowUI", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "NewsWindow", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "OptionUI", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "PlayerSetWindowUI", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "PlayerSet_3_UI", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "PresentBoxUI", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "StartDashWindowUI", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "WorldRankingWindowUI", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "ui_mm_mileage2_page", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "ui_mm_ranking_page", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "ui_tex_mm_ep_001", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "ui_tex_mm_ep_002", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "window_name_setting", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "DailyBattleDetailWindow", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "DeckViewWindow", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "LeagueResultWindowUI", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "Mileage_rankup", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "RankingFriendOptionWindow", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "RankingResultBitWindow", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "RankingWindowUI", true, true, false, null, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.UNKNOWN, "item_get_Window", true, true, false, null, false)
	};

	// Token: 0x040015FA RID: 5626
	private List<string> m_streamingSoundData = new List<string>();

	// Token: 0x040015FB RID: 5627
	private List<ResourceSceneLoader.ResourceInfo> m_nowLoadingList;

	// Token: 0x040015FC RID: 5628
	private List<ResourceSceneLoader.ResourceInfo> m_loadInfo;

	// Token: 0x040015FD RID: 5629
	private int m_requestDownloadCount;

	// Token: 0x040015FE RID: 5630
	private int m_requestLoadCount;

	// Token: 0x040015FF RID: 5631
	private bool m_endCheckExistingCheckDownloadData;

	// Token: 0x02000337 RID: 823
	private enum EventSignal
	{
		// Token: 0x04001601 RID: 5633
		SuccessStreamingDataLoad = 100,
		// Token: 0x04001602 RID: 5634
		NUM
	}
}
