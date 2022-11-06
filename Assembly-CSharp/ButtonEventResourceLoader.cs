using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000439 RID: 1081
public class ButtonEventResourceLoader : MonoBehaviour
{
	// Token: 0x17000482 RID: 1154
	// (get) Token: 0x060020DE RID: 8414 RVA: 0x000C51CC File Offset: 0x000C33CC
	// (set) Token: 0x060020DF RID: 8415 RVA: 0x000C51D4 File Offset: 0x000C33D4
	public bool IsLoaded
	{
		get
		{
			return this.m_isLoaded;
		}
		private set
		{
		}
	}

	// Token: 0x17000483 RID: 1155
	// (get) Token: 0x060020E0 RID: 8416 RVA: 0x000C51D8 File Offset: 0x000C33D8
	// (set) Token: 0x060020E1 RID: 8417 RVA: 0x000C51E0 File Offset: 0x000C33E0
	public Dictionary<ButtonInfoTable.PageType, List<string>> ResourceMap
	{
		get
		{
			return this.m_resourceMap;
		}
		private set
		{
		}
	}

	// Token: 0x060020E2 RID: 8418 RVA: 0x000C51E4 File Offset: 0x000C33E4
	public IEnumerator LoadPageResourceIfNotLoadedSync(ButtonInfoTable.PageType pageType, ButtonEventResourceLoader.CallbackIfNotLoaded callbackIfNotLoaded)
	{
		yield return base.StartCoroutine(this.LoadPageResourceIfNotLoadedCoroutine(pageType, callbackIfNotLoaded));
		yield break;
	}

	// Token: 0x060020E3 RID: 8419 RVA: 0x000C521C File Offset: 0x000C341C
	public void LoadResourceIfNotLoadedAsync(ButtonInfoTable.PageType pageType, ButtonEventResourceLoader.CallbackIfNotLoaded callbackIfNotLoaded)
	{
		base.StartCoroutine(this.LoadPageResourceIfNotLoadedCoroutine(pageType, callbackIfNotLoaded));
	}

	// Token: 0x060020E4 RID: 8420 RVA: 0x000C5230 File Offset: 0x000C3430
	public IEnumerator LoadResourceIfNotLoadedSync(string resourceName, ButtonEventResourceLoader.CallbackIfNotLoaded callbackIfNotLoaded)
	{
		yield return base.StartCoroutine(this.LoadPageResourceIfNotLoadedCoroutine(resourceName, callbackIfNotLoaded));
		yield break;
	}

	// Token: 0x060020E5 RID: 8421 RVA: 0x000C5268 File Offset: 0x000C3468
	public void LoadResourceIfNotLoadedAsync(string resourceName, ButtonEventResourceLoader.CallbackIfNotLoaded callbackIfNotLoaded)
	{
		base.StartCoroutine(this.LoadPageResourceIfNotLoadedCoroutine(resourceName, callbackIfNotLoaded));
	}

	// Token: 0x060020E6 RID: 8422 RVA: 0x000C527C File Offset: 0x000C347C
	public IEnumerator LoadAtlasResourceIfNotLoaded()
	{
		AtlasManager atlasManager = AtlasManager.Instance;
		if (atlasManager != null)
		{
			atlasManager.StartLoadAtlasForDividedMenu();
			do
			{
				yield return null;
			}
			while (!atlasManager.IsLoadAtlas());
		}
		yield break;
	}

	// Token: 0x060020E7 RID: 8423 RVA: 0x000C5290 File Offset: 0x000C3490
	private IEnumerator LoadPageResourceIfNotLoadedCoroutine(ButtonInfoTable.PageType pageType, ButtonEventResourceLoader.CallbackIfNotLoaded callbackIfNotLoaded)
	{
		this.m_isLoaded = false;
		if (pageType == ButtonInfoTable.PageType.NON)
		{
			this.m_isLoaded = true;
			yield break;
		}
		GameObject mainMenuPagesObject = GameObject.Find("UI Root (2D)/Camera/menu_Anim");
		if (mainMenuPagesObject == null)
		{
			this.m_isLoaded = true;
			yield break;
		}
		bool isNeedCallback = false;
		if (!this.m_resourceMap.ContainsKey(pageType))
		{
			yield break;
		}
		List<string> resourceNameList;
		this.m_resourceMap.TryGetValue(pageType, out resourceNameList);
		foreach (string resourceName in resourceNameList)
		{
			bool isExistResource = this.IsExistResource(resourceName, mainMenuPagesObject);
			if (!isExistResource)
			{
				yield return base.StartCoroutine(this.LoadResourceRequest(resourceName, mainMenuPagesObject));
				isNeedCallback = true;
			}
		}
		this.m_isLoaded = true;
		if (isNeedCallback && callbackIfNotLoaded != null)
		{
			callbackIfNotLoaded();
		}
		yield break;
	}

	// Token: 0x060020E8 RID: 8424 RVA: 0x000C52C8 File Offset: 0x000C34C8
	private IEnumerator LoadPageResourceIfNotLoadedCoroutine(string resourceName, ButtonEventResourceLoader.CallbackIfNotLoaded callbackIfNotLoaded)
	{
		this.m_isLoaded = false;
		GameObject mainMenuPagesObject = GameObject.Find("UI Root (2D)/Camera/menu_Anim");
		if (mainMenuPagesObject == null)
		{
			this.m_isLoaded = true;
			yield break;
		}
		bool isNeedCallback = false;
		bool isExistResource = this.IsExistResource(resourceName, mainMenuPagesObject);
		if (isExistResource)
		{
			this.m_isLoaded = true;
			yield break;
		}
		yield return base.StartCoroutine(this.LoadResourceRequest(resourceName, mainMenuPagesObject));
		isNeedCallback = true;
		this.m_isLoaded = true;
		if (isNeedCallback && callbackIfNotLoaded != null)
		{
			callbackIfNotLoaded();
		}
		yield break;
	}

	// Token: 0x060020E9 RID: 8425 RVA: 0x000C5300 File Offset: 0x000C3500
	private bool IsExistResource(string resourceName, GameObject parentObject)
	{
		int childCount = parentObject.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = parentObject.transform.GetChild(i).gameObject;
			if (!(gameObject == null))
			{
				if (gameObject.name == resourceName)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060020EA RID: 8426 RVA: 0x000C5364 File Offset: 0x000C3564
	public IEnumerator LoadResourceRequest(string resourceName, GameObject attachObject)
	{
		HudNetworkConnect hudConnect = NetMonitor.Instance.GetComponent<HudNetworkConnect>();
		if (hudConnect != null)
		{
			hudConnect.Setup();
			hudConnect.PlayStart(HudNetworkConnect.DisplayType.LOADING);
		}
		GameObject sceneLoaderObject = GameObject.Find("ButtonEventSceneLoader");
		if (sceneLoaderObject == null)
		{
			sceneLoaderObject = new GameObject("ButtonEventSceneLoader");
		}
		this.m_sceneLoader = sceneLoaderObject.AddComponent<ResourceSceneLoader>();
		this.m_sceneLoader.AddLoadAndResourceManager(resourceName, true, ResourceCategory.UI, false, false, null);
		do
		{
			yield return null;
		}
		while (!this.m_sceneLoader.Loaded);
		UnityEngine.Object.Destroy(this.m_sceneLoader);
		this.m_sceneLoader = null;
		yield return null;
		GameObject resourceObject = ResourceManager.Instance.GetGameObject(ResourceCategory.UI, resourceName);
		if (resourceObject != null)
		{
			ResourceFolderMarker marker = resourceObject.GetComponent<ResourceFolderMarker>();
			if (marker == null)
			{
				resourceObject.SetActive(false);
				foreach (object obj in resourceObject.transform)
				{
					Transform child = (Transform)obj;
					child.gameObject.SetActive(true);
				}
			}
			Vector3 localPosition = resourceObject.transform.localPosition;
			Vector3 localScale = resourceObject.transform.localScale;
			resourceObject.transform.parent = attachObject.transform;
			resourceObject.transform.localPosition = localPosition;
			resourceObject.transform.localScale = localScale;
		}
		if (hudConnect != null)
		{
			hudConnect.PlayEnd();
		}
		yield return null;
		yield break;
	}

	// Token: 0x060020EB RID: 8427 RVA: 0x000C539C File Offset: 0x000C359C
	private void Start()
	{
	}

	// Token: 0x060020EC RID: 8428 RVA: 0x000C53A0 File Offset: 0x000C35A0
	private void Update()
	{
	}

	// Token: 0x04001D56 RID: 7510
	private ResourceSceneLoader m_sceneLoader;

	// Token: 0x04001D57 RID: 7511
	private bool m_isLoaded;

	// Token: 0x04001D58 RID: 7512
	private Dictionary<ButtonInfoTable.PageType, List<string>> m_resourceMap = new Dictionary<ButtonInfoTable.PageType, List<string>>
	{
		{
			ButtonInfoTable.PageType.PRESENT_BOX,
			new List<string>
			{
				"item_get_Window",
				"PlayerSetWindowUI",
				"ChaoSetWindowUI",
				"PresentBoxUI",
				"ChaoWindows"
			}
		},
		{
			ButtonInfoTable.PageType.ROULETTE,
			new List<string>
			{
				"item_get_Window",
				"PlayerSetWindowUI",
				"ChaoSetWindowUI",
				"RouletteTopUI",
				"ChaoWindows",
				"NewsWindow"
			}
		},
		{
			ButtonInfoTable.PageType.DAILY_BATTLE,
			new List<string>
			{
				"PlayerSetWindowUI",
				"ChaoSetWindowUI",
				"RankingWindowUI",
				"DailyBattleDetailWindow",
				"DailyInfoUI"
			}
		},
		{
			ButtonInfoTable.PageType.CHAO,
			new List<string>
			{
				"PlayerSetWindowUI",
				"ChaoSetWindowUI",
				"DeckViewWindow",
				"ChaoSetUIPage"
			}
		},
		{
			ButtonInfoTable.PageType.OPTION,
			new List<string>
			{
				"OptionUI",
				"window_name_setting",
				"OptionWindows"
			}
		},
		{
			ButtonInfoTable.PageType.SHOP_RSR,
			new List<string>
			{
				"item_get_Window",
				"ShopPage"
			}
		},
		{
			ButtonInfoTable.PageType.SHOP_RING,
			new List<string>
			{
				"item_get_Window",
				"ShopPage"
			}
		},
		{
			ButtonInfoTable.PageType.SHOP_ENERGY,
			new List<string>
			{
				"item_get_Window",
				"ShopPage"
			}
		},
		{
			ButtonInfoTable.PageType.INFOMATION,
			new List<string>
			{
				"NewsWindow",
				"WorldRankingWindowUI",
				"LeagueResultWindowUI",
				"InformationUI"
			}
		},
		{
			ButtonInfoTable.PageType.EPISODE_RANKING,
			new List<string>
			{
				"PlayerSetWindowUI",
				"ChaoSetWindowUI",
				"RankingFriendOptionWindow",
				"RankingResultBitWindow",
				"RankingWindowUI",
				"ui_mm_ranking_page"
			}
		},
		{
			ButtonInfoTable.PageType.QUICK_RANKING,
			new List<string>
			{
				"PlayerSetWindowUI",
				"ChaoSetWindowUI",
				"RankingFriendOptionWindow",
				"RankingResultBitWindow",
				"RankingWindowUI",
				"ui_mm_ranking_page"
			}
		},
		{
			ButtonInfoTable.PageType.QUICK,
			new List<string>
			{
				"PlayerSetWindowUI",
				"ChaoSetWindowUI",
				"DeckViewWindow",
				"ItemSet_3_UI"
			}
		},
		{
			ButtonInfoTable.PageType.EPISODE,
			new List<string>
			{
				"item_get_Window",
				"Mileage_rankup",
				"ui_mm_mileage2_page"
			}
		},
		{
			ButtonInfoTable.PageType.EPISODE_PLAY,
			new List<string>
			{
				"PlayerSetWindowUI",
				"ChaoSetWindowUI",
				"DeckViewWindow",
				"ItemSet_3_UI"
			}
		},
		{
			ButtonInfoTable.PageType.PLAY_AT_EPISODE_PAGE,
			new List<string>
			{
				"PlayerSetWindowUI",
				"ChaoSetWindowUI",
				"DeckViewWindow",
				"ItemSet_3_UI"
			}
		},
		{
			ButtonInfoTable.PageType.PLAYER_MAIN,
			new List<string>
			{
				"PlayerSetWindowUI",
				"ChaoSetWindowUI",
				"DeckViewWindow",
				"PlayerSet_3_UI"
			}
		},
		{
			ButtonInfoTable.PageType.DAILY_CHALLENGE,
			new List<string>
			{
				"DailyWindowUI"
			}
		}
	};

	// Token: 0x02000A91 RID: 2705
	// (Invoke) Token: 0x0600487E RID: 18558
	public delegate void CallbackIfNotLoaded();
}
