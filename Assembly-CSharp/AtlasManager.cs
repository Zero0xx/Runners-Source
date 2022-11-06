using System;
using System.Collections.Generic;
using Text;
using UnityEngine;

// Token: 0x020000F3 RID: 243
public class AtlasManager : MonoBehaviour
{
	// Token: 0x1700014D RID: 333
	// (get) Token: 0x06000728 RID: 1832 RVA: 0x00029BDC File Offset: 0x00027DDC
	public static AtlasManager Instance
	{
		get
		{
			return AtlasManager.instance;
		}
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x00029BE4 File Offset: 0x00027DE4
	public void StartLoadAtlasForMenu()
	{
		if (this.m_sceneLoader == null)
		{
			GameObject gameObject = new GameObject("SceneLoader");
			if (gameObject != null)
			{
				this.m_sceneLoader = gameObject.AddComponent<ResourceSceneLoader>();
				this.AddLoadAtlasForMenu();
				base.enabled = true;
				this.m_loadedAtlas = false;
			}
		}
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x00029C3C File Offset: 0x00027E3C
	public void StartLoadAtlasForEventMenu()
	{
		if (this.m_sceneLoader == null)
		{
			GameObject gameObject = new GameObject("SceneLoader");
			if (gameObject != null)
			{
				this.m_sceneLoader = gameObject.AddComponent<ResourceSceneLoader>();
				this.AddLoadAtlasForTitle();
				base.enabled = true;
				this.m_loadedAtlas = false;
			}
		}
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x00029C94 File Offset: 0x00027E94
	public void StartLoadAtlasForDividedMenu()
	{
		if (this.m_sceneLoader == null)
		{
			GameObject gameObject = new GameObject("SceneLoader");
			if (gameObject != null)
			{
				this.m_sceneLoader = gameObject.AddComponent<ResourceSceneLoader>();
				this.AddLoadAtlasForDividedMenu();
				base.enabled = true;
				this.m_loadedAtlas = false;
			}
		}
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x00029CEC File Offset: 0x00027EEC
	public void StartLoadAtlasForStage()
	{
		if (this.m_sceneLoader == null)
		{
			GameObject gameObject = new GameObject("SceneLoader");
			if (gameObject != null)
			{
				this.m_sceneLoader = gameObject.AddComponent<ResourceSceneLoader>();
				this.AddLoadAtlasForStage();
				base.enabled = true;
				this.m_loadedAtlas = false;
			}
		}
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x00029D44 File Offset: 0x00027F44
	public void StartLoadAtlasForTitle()
	{
		if (this.m_sceneLoader == null)
		{
			GameObject gameObject = new GameObject("SceneLoader");
			if (gameObject != null)
			{
				this.m_sceneLoader = gameObject.AddComponent<ResourceSceneLoader>();
				this.AddLoadAtlasForTitle();
				base.enabled = true;
				this.m_loadedAtlas = false;
			}
		}
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x00029D9C File Offset: 0x00027F9C
	public void ResetReplaceAtlas()
	{
		if (this.m_itemAtlas != null)
		{
			this.m_itemAtlas = null;
		}
		UIAtlas[] array = Resources.FindObjectsOfTypeAll(typeof(UIAtlas)) as UIAtlas[];
		for (int i = 0; i < array.Length; i++)
		{
			array[i].replacement = null;
		}
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x00029DF4 File Offset: 0x00027FF4
	public void ResetEventRelaceAtlas()
	{
		UIAtlas[] array = Resources.FindObjectsOfTypeAll(typeof(UIAtlas)) as UIAtlas[];
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].name == "ui_event_reference_Atlas")
			{
				array[i].replacement = null;
			}
		}
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x00029E4C File Offset: 0x0002804C
	public void ReplaceAtlasForMenu(bool isReplaceDividedMenu)
	{
		if (this.m_loadedAtlas)
		{
			string str = "_" + TextUtility.GetSuffixe();
			UIAtlas[] array = Resources.FindObjectsOfTypeAll(typeof(UIAtlas)) as UIAtlas[];
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.UI, "ui_cmn_item_Atlas");
			if (gameObject != null)
			{
				this.m_itemAtlas = gameObject.GetComponent<UIAtlas>();
			}
			for (int i = 0; i < this.m_menuLangAtlasName.Length; i++)
			{
				string name = this.m_menuLangAtlasName[i] + str;
				GameObject gameObject2 = ResourceManager.Instance.GetGameObject(ResourceCategory.UI, name);
				if (gameObject2 != null)
				{
					UIAtlas component = gameObject2.GetComponent<UIAtlas>();
					if (component != null)
					{
						foreach (UIAtlas uiatlas in array)
						{
							if (uiatlas.name == this.m_menuLangAtlasName[i])
							{
								uiatlas.replacement = component;
							}
						}
					}
				}
			}
			if (isReplaceDividedMenu)
			{
				for (int k = 0; k < this.m_dividedMenuLangAtlasName.Length; k++)
				{
					string name2 = this.m_dividedMenuLangAtlasName[k] + str;
					GameObject gameObject3 = ResourceManager.Instance.GetGameObject(ResourceCategory.UI, name2);
					if (gameObject3 != null)
					{
						UIAtlas component2 = gameObject3.GetComponent<UIAtlas>();
						if (component2 != null)
						{
							foreach (UIAtlas uiatlas2 in array)
							{
								if (uiatlas2.name == this.m_dividedMenuLangAtlasName[k])
								{
									uiatlas2.replacement = component2;
								}
							}
						}
					}
				}
			}
			GameObject gameObject4 = ResourceManager.Instance.GetGameObject(ResourceCategory.UI, "ui_cmn_player_bundle_Atlas");
			if (gameObject4 != null)
			{
				UIAtlas component3 = gameObject4.GetComponent<UIAtlas>();
				foreach (UIAtlas uiatlas3 in array)
				{
					if (uiatlas3 != null && uiatlas3.name == "ui_cmn_player_Atlas" && uiatlas3 != component3)
					{
						uiatlas3.replacement = component3;
					}
				}
			}
			this.ReplaceEventAtlas(array);
			this.ReplaceItemAtlas(array);
		}
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x0002A094 File Offset: 0x00028294
	public void ReplaceAtlasForStage()
	{
		if (this.m_loadedAtlas)
		{
			string str = "_" + TextUtility.GetSuffixe();
			UIAtlas[] array = Resources.FindObjectsOfTypeAll(typeof(UIAtlas)) as UIAtlas[];
			for (int i = 0; i < this.m_stageLangAtlasName.Length; i++)
			{
				string name = this.m_stageLangAtlasName[i] + str;
				GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.UI, name);
				if (gameObject != null)
				{
					UIAtlas component = gameObject.GetComponent<UIAtlas>();
					if (component != null)
					{
						foreach (UIAtlas uiatlas in array)
						{
							if (uiatlas.name == this.m_stageLangAtlasName[i])
							{
								uiatlas.replacement = component;
							}
						}
					}
				}
			}
			this.ReplaceEventAtlas(array);
			GameObject gameObject2 = ResourceManager.Instance.GetGameObject(ResourceCategory.UI, "ui_result_Atlas");
			if (gameObject2 != null)
			{
				UIAtlas component2 = gameObject2.GetComponent<UIAtlas>();
				foreach (UIAtlas uiatlas2 in array)
				{
					if (uiatlas2 != null && uiatlas2.name == "ui_result_reference_Atlas")
					{
						uiatlas2.replacement = component2;
					}
				}
			}
			this.ReplaceItemAtlas(array);
		}
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x0002A1F4 File Offset: 0x000283F4
	public void ReplaceAtlasForMenuLoading(UIAtlas[] atlasArray)
	{
		if (atlasArray != null)
		{
			this.ReplaceEventAtlas(atlasArray);
		}
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x0002A204 File Offset: 0x00028404
	public void ReplaceAtlasForLoading(UIAtlas referenceLoadingAtlas)
	{
		if (referenceLoadingAtlas != null)
		{
			string str = "_" + TextUtility.GetSuffixe();
			string name = "ui_load_word_Atlas" + str;
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.UI, name);
			if (gameObject != null)
			{
				UIAtlas component = gameObject.GetComponent<UIAtlas>();
				if (component != null)
				{
					referenceLoadingAtlas.replacement = component;
				}
			}
		}
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x0002A26C File Offset: 0x0002846C
	private void ReplaceEventAtlas(UIAtlas[] atlasList)
	{
		if (!string.IsNullOrEmpty(this.m_eventLangDummyAtlasName))
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.UI, this.m_eventLangAtlasName);
			if (gameObject != null)
			{
				UIAtlas component = gameObject.GetComponent<UIAtlas>();
				foreach (UIAtlas uiatlas in atlasList)
				{
					if (uiatlas.name == this.m_eventLangDummyAtlasName || uiatlas.name == "ui_event_00000_Atlas")
					{
						Transform parent = uiatlas.gameObject.transform.parent;
						if (!(parent != null) || !(parent.name == "EventResourceAtlas"))
						{
							uiatlas.replacement = component;
						}
					}
				}
			}
		}
		this.ReplaceEventCommonAtlas(atlasList);
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x0002A340 File Offset: 0x00028540
	private UIAtlas GetEventCommonAtlas()
	{
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.EVENT_RESOURCE, "EventResourceCommon");
		if (gameObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "EventResourceAtlas");
			if (gameObject2 != null)
			{
				for (int i = 0; i < gameObject2.transform.childCount; i++)
				{
					UIAtlas component = gameObject2.transform.GetChild(i).GetComponent<UIAtlas>();
					if (component != null)
					{
						return component;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x0002A3C0 File Offset: 0x000285C0
	private void ReplaceEventCommonAtlas(UIAtlas[] atlasList)
	{
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.EVENT_RESOURCE, "EventResourceCommon");
		if (gameObject == null)
		{
			gameObject = GameObject.Find("EventResourceCommon");
		}
		if (gameObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "EventResourceAtlas");
			if (gameObject2 != null)
			{
				UIAtlas uiatlas = null;
				for (int i = 0; i < gameObject2.transform.childCount; i++)
				{
					uiatlas = gameObject2.transform.GetChild(i).GetComponent<UIAtlas>();
					if (uiatlas != null)
					{
						break;
					}
				}
				if (uiatlas != null)
				{
					foreach (UIAtlas uiatlas2 in atlasList)
					{
						if (uiatlas2.name == "ui_event_reference_Atlas")
						{
							uiatlas2.replacement = uiatlas;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x0002A4A8 File Offset: 0x000286A8
	public void ReplacePlayerAtlasForRaidResult(UIAtlas referenceAtlas)
	{
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.UI, "ui_result_Atlas");
		if (gameObject != null)
		{
			UIAtlas component = gameObject.GetComponent<UIAtlas>();
			if (referenceAtlas != null && referenceAtlas.name == "ui_cmn_player_Atlas" && referenceAtlas != component)
			{
				referenceAtlas.replacement = component;
			}
		}
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0002A510 File Offset: 0x00028710
	private void ReplaceItemAtlas(UIAtlas[] atlasList)
	{
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.UI, "ui_cmn_item_Atlas");
		if (gameObject != null)
		{
			UIAtlas component = gameObject.GetComponent<UIAtlas>();
			foreach (UIAtlas uiatlas in atlasList)
			{
				if (uiatlas != null && uiatlas.name == "ui_cmn_item_reference_Atlas")
				{
					uiatlas.replacement = component;
				}
			}
		}
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x0002A588 File Offset: 0x00028788
	public void ClearAllAtlas()
	{
		List<UIAtlas> list = new List<UIAtlas>();
		foreach (string text in this.m_dontDestryAtlasList)
		{
			if (!string.IsNullOrEmpty(text))
			{
				GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.UI, text);
				if (!(gameObject == null))
				{
					UIAtlas component = gameObject.GetComponent<UIAtlas>();
					if (!(component == null))
					{
						list.Add(component);
					}
				}
			}
		}
		UIAtlas eventCommonAtlas = this.GetEventCommonAtlas();
		if (eventCommonAtlas != null)
		{
			list.Add(eventCommonAtlas);
		}
		UIAtlas[] array = Resources.FindObjectsOfTypeAll<UIAtlas>();
		foreach (UIAtlas uiatlas in array)
		{
			if (!(uiatlas == null))
			{
				bool flag = true;
				foreach (UIAtlas uiatlas2 in list)
				{
					if (list != null)
					{
						if (uiatlas.name == uiatlas2.name)
						{
							flag = false;
							break;
						}
						if (uiatlas.texture != null && uiatlas.texture.name == uiatlas2.name)
						{
							flag = false;
							break;
						}
						if (uiatlas.spriteMaterial != null && uiatlas.spriteMaterial.name == uiatlas2.name)
						{
							flag = false;
							break;
						}
					}
				}
				if (flag)
				{
					Resources.UnloadAsset(uiatlas.texture);
					Resources.UnloadAsset(uiatlas.spriteMaterial);
				}
			}
		}
	}

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x0600073A RID: 1850 RVA: 0x0002A7A4 File Offset: 0x000289A4
	public UIAtlas ItemAtlas
	{
		get
		{
			return this.m_itemAtlas;
		}
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x0002A7AC File Offset: 0x000289AC
	public bool IsLoadAtlas()
	{
		return this.m_loadedAtlas;
	}

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x0600073C RID: 1852 RVA: 0x0002A7B4 File Offset: 0x000289B4
	public string EventLangAtlasName
	{
		get
		{
			return this.m_eventLangAtlasName;
		}
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x0002A7BC File Offset: 0x000289BC
	protected void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x0002A7C4 File Offset: 0x000289C4
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x0002A7D0 File Offset: 0x000289D0
	private void OnDestroy()
	{
		if (AtlasManager.instance == this)
		{
			if (this.m_itemAtlas != null)
			{
				this.m_itemAtlas = null;
			}
			AtlasManager.instance = null;
		}
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x0002A80C File Offset: 0x00028A0C
	private void SetInstance()
	{
		if (AtlasManager.instance == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			AtlasManager.instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x0002A840 File Offset: 0x00028A40
	private void Update()
	{
		if (this.m_sceneLoader != null && this.m_sceneLoader.Loaded)
		{
			this.m_loadedAtlas = true;
			UnityEngine.Object.Destroy(this.m_sceneLoader.gameObject);
			this.m_sceneLoader = null;
			base.enabled = false;
		}
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x0002A894 File Offset: 0x00028A94
	private void AddLoadEventLangAtlas()
	{
		if (EventManager.Instance != null)
		{
			this.m_eventLangDummyAtlasName = string.Empty;
			if (EventManager.Instance.IsInEvent())
			{
				switch (EventManager.GetType(EventManager.Instance.Id))
				{
				case EventManager.EventType.SPECIAL_STAGE:
					this.m_eventLangDummyAtlasName = "ui_event_10000_Atlas";
					break;
				case EventManager.EventType.RAID_BOSS:
					this.m_eventLangDummyAtlasName = "ui_event_20000_Atlas";
					break;
				case EventManager.EventType.COLLECT_OBJECT:
					this.m_eventLangDummyAtlasName = "ui_event_30000_Atlas";
					break;
				case EventManager.EventType.GACHA:
					this.m_eventLangDummyAtlasName = "ui_event_40000_Atlas";
					break;
				case EventManager.EventType.ADVERT:
					this.m_eventLangDummyAtlasName = "ui_event_50000_Atlas";
					break;
				case EventManager.EventType.QUICK:
					this.m_eventLangDummyAtlasName = "ui_event_60000_Atlas";
					break;
				case EventManager.EventType.BGM:
					this.m_eventLangDummyAtlasName = "ui_event_70000_Atlas";
					break;
				}
				int specificId = EventManager.GetSpecificId();
				if (specificId > 0)
				{
					this.m_eventLangAtlasName = "ui_event_" + specificId.ToString() + "_Atlas_" + TextUtility.GetSuffixe();
					ResourceSceneLoader.ResourceInfo resInfo = this.CreateResourceSceneLoader(this.m_eventLangAtlasName, true);
					this.AddSceneLoaderAndResourceManager(resInfo);
				}
			}
		}
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x0002A9B8 File Offset: 0x00028BB8
	private void AddLoadAtlasForMenu()
	{
		for (int i = 0; i < this.m_menuLangAtlasName.Length; i++)
		{
			string sceneName = this.m_menuLangAtlasName[i] + "_" + TextUtility.GetSuffixe();
			ResourceSceneLoader.ResourceInfo resInfo = this.CreateResourceSceneLoader(sceneName, false);
			this.AddSceneLoaderAndResourceManager(resInfo);
		}
		string sceneName2 = "ui_load_word_Atlas_" + TextUtility.GetSuffixe();
		ResourceSceneLoader.ResourceInfo resInfo2 = this.CreateResourceSceneLoader(sceneName2, true);
		this.AddSceneLoaderAndResourceManager(resInfo2);
		this.AddLoadEventLangAtlas();
		ResourceSceneLoader.ResourceInfo resInfo3 = this.CreateResourceSceneLoader("ui_cmn_player_bundle_Atlas", false);
		this.AddSceneLoaderAndResourceManager(resInfo3);
		ResourceSceneLoader.ResourceInfo resInfo4 = this.CreateResourceSceneLoader("ui_cmn_item_Atlas", true);
		this.AddSceneLoaderAndResourceManager(resInfo4);
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x0002AA5C File Offset: 0x00028C5C
	private void AddLoadAtlasForDividedMenu()
	{
		for (int i = 0; i < this.m_dividedMenuLangAtlasName.Length; i++)
		{
			string sceneName = this.m_dividedMenuLangAtlasName[i] + "_" + TextUtility.GetSuffixe();
			ResourceSceneLoader.ResourceInfo resInfo = this.CreateResourceSceneLoader(sceneName, false);
			this.AddSceneLoaderAndResourceManager(resInfo);
		}
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x0002AAAC File Offset: 0x00028CAC
	private void AddLoadAtlasForStage()
	{
		for (int i = 0; i < this.m_stageLangAtlasName.Length; i++)
		{
			string sceneName = this.m_stageLangAtlasName[i] + "_" + TextUtility.GetSuffixe();
			ResourceSceneLoader.ResourceInfo resInfo = this.CreateResourceSceneLoader(sceneName, false);
			this.AddSceneLoaderAndResourceManager(resInfo);
		}
		this.AddLoadEventLangAtlas();
		ResourceSceneLoader.ResourceInfo resInfo2 = this.CreateResourceSceneLoader("ui_result_Atlas", false);
		this.AddSceneLoaderAndResourceManager(resInfo2);
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.UI, "ui_cmn_item_Atlas");
		if (gameObject == null)
		{
			ResourceSceneLoader.ResourceInfo resInfo3 = this.CreateResourceSceneLoader("ui_cmn_item_Atlas", true);
			this.AddSceneLoaderAndResourceManager(resInfo3);
		}
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x0002AB4C File Offset: 0x00028D4C
	private void AddLoadAtlasForTitle()
	{
		this.AddLoadEventLangAtlas();
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x0002AB54 File Offset: 0x00028D54
	private void AddSceneLoaderAndResourceManager(ResourceSceneLoader.ResourceInfo resInfo)
	{
		if (this.m_sceneLoader == null)
		{
			return;
		}
		bool flag = this.m_sceneLoader.AddLoadAndResourceManager(resInfo);
		if (flag && resInfo.m_dontDestroyOnChangeScene)
		{
			this.m_dontDestryAtlasList.Add(resInfo.m_scenename);
		}
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x0002ABA4 File Offset: 0x00028DA4
	private ResourceSceneLoader.ResourceInfo CreateResourceSceneLoader(string sceneName, bool dontDestroy = false)
	{
		return new ResourceSceneLoader.ResourceInfo(ResourceCategory.UI, sceneName, true, false, dontDestroy, null, false);
	}

	// Token: 0x04000583 RID: 1411
	private const string m_loadingLangAtlasName = "ui_load_word_Atlas";

	// Token: 0x04000584 RID: 1412
	private const string m_eventDummyAtlasName = "ui_event_reference_Atlas";

	// Token: 0x04000585 RID: 1413
	private const string m_playerAtlasName = "ui_cmn_player_bundle_Atlas";

	// Token: 0x04000586 RID: 1414
	private const string m_chaoAtlasName = "ChaoTextures";

	// Token: 0x04000587 RID: 1415
	private const string m_resultAtlasName = "ui_result_Atlas";

	// Token: 0x04000588 RID: 1416
	private const string m_itemAtlasName = "ui_cmn_item_Atlas";

	// Token: 0x04000589 RID: 1417
	private readonly string[] m_menuLangAtlasName = new string[]
	{
		"ui_item_set_2_word_Atlas",
		"ui_mm_contents_word_Atlas",
		"ui_ranking_word_Atlas",
		"ui_mm_info_page_word_Atlas"
	};

	// Token: 0x0400058A RID: 1418
	private readonly string[] m_dividedMenuLangAtlasName = new string[]
	{
		"ui_player_set_2_word_Atlas",
		"ui_shop_word_Atlas",
		"ui_roulette_word_Atlas"
	};

	// Token: 0x0400058B RID: 1419
	private readonly string[] m_stageLangAtlasName = new string[]
	{
		"ui_gp_bit_word_Atlas",
		"ui_result_word_Atlas",
		"ui_tutrial_word_Atlas",
		"ui_shop_word_Atlas"
	};

	// Token: 0x0400058C RID: 1420
	private ResourceSceneLoader.ResourceInfo m_loadInfoForEvent = new ResourceSceneLoader.ResourceInfo(ResourceCategory.EVENT_RESOURCE, "EventResourceCommon", true, false, true, "EventResourceCommon", false);

	// Token: 0x0400058D RID: 1421
	private UIAtlas m_itemAtlas;

	// Token: 0x0400058E RID: 1422
	private ResourceSceneLoader m_sceneLoader;

	// Token: 0x0400058F RID: 1423
	private bool m_loadedAtlas;

	// Token: 0x04000590 RID: 1424
	private string m_eventLangDummyAtlasName = string.Empty;

	// Token: 0x04000591 RID: 1425
	private string m_eventLangAtlasName = string.Empty;

	// Token: 0x04000592 RID: 1426
	private static AtlasManager instance;

	// Token: 0x04000593 RID: 1427
	private List<string> m_dontDestryAtlasList = new List<string>();
}
