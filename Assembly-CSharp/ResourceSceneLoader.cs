using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020009E7 RID: 2535
public class ResourceSceneLoader : MonoBehaviour
{
	// Token: 0x060042DB RID: 17115 RVA: 0x0015B5F0 File Offset: 0x001597F0
	private void Start()
	{
		this.m_checkTime = false;
		this.StartAssetBundleLoad();
		base.StartCoroutine(this.LoadScene());
	}

	// Token: 0x060042DC RID: 17116 RVA: 0x0015B60C File Offset: 0x0015980C
	private void Update()
	{
		foreach (ResourceSceneLoader.ResourceInfo resourceInfo in this.m_loadInfos)
		{
			if (!resourceInfo.m_isloaded)
			{
				this.m_isloaded = false;
				return;
			}
		}
		foreach (ResourceSceneLoader.ResourceInfo resourceInfo2 in this.m_assetLoadInfos)
		{
			if (!resourceInfo2.m_isloaded)
			{
				this.m_isloaded = false;
				return;
			}
		}
		this.m_isloaded = true;
	}

	// Token: 0x060042DD RID: 17117 RVA: 0x0015B6F0 File Offset: 0x001598F0
	public void Pause(bool value)
	{
		this.m_pause = value;
		base.enabled = !this.m_pause;
	}

	// Token: 0x17000924 RID: 2340
	// (get) Token: 0x060042DE RID: 17118 RVA: 0x0015B708 File Offset: 0x00159908
	// (set) Token: 0x060042DF RID: 17119 RVA: 0x0015B710 File Offset: 0x00159910
	public int LoadEndCount
	{
		get
		{
			return this.m_loadEndCount;
		}
		private set
		{
		}
	}

	// Token: 0x17000925 RID: 2341
	// (get) Token: 0x060042E0 RID: 17120 RVA: 0x0015B714 File Offset: 0x00159914
	// (set) Token: 0x060042E1 RID: 17121 RVA: 0x0015B724 File Offset: 0x00159924
	public int RequestedLoadCount
	{
		get
		{
			return this.m_loadInfos.Count;
		}
		private set
		{
		}
	}

	// Token: 0x060042E2 RID: 17122 RVA: 0x0015B728 File Offset: 0x00159928
	public bool AddLoad(string scenename, bool onAssetBundle, bool onlyDownload)
	{
		if (ResourceManager.Instance && ResourceManager.Instance.IsExistContainer(scenename))
		{
			return false;
		}
		ResourceSceneLoader.ResourceInfo resourceInfo = new ResourceSceneLoader.ResourceInfo();
		resourceInfo.m_scenename = scenename;
		resourceInfo.m_isAssetBundle = onAssetBundle;
		resourceInfo.m_isloaded = false;
		resourceInfo.m_category = ResourceCategory.UNKNOWN;
		resourceInfo.m_onlyDownload = onlyDownload;
		if (onAssetBundle && AssetBundleLoader.Instance != null)
		{
			if (onlyDownload && AssetBundleLoader.Instance.IsDownloaded(scenename))
			{
				return false;
			}
			bool flag = true;
			foreach (ResourceSceneLoader.ResourceInfo resourceInfo2 in this.m_assetLoadInfos)
			{
				if (resourceInfo2.m_scenename == scenename)
				{
					flag = false;
				}
			}
			if (flag)
			{
				this.m_assetLoadInfos.Add(resourceInfo);
			}
		}
		else
		{
			if (onlyDownload)
			{
				return false;
			}
			bool flag2 = true;
			foreach (ResourceSceneLoader.ResourceInfo resourceInfo3 in this.m_assetLoadInfos)
			{
				if (resourceInfo3.m_scenename == scenename)
				{
					flag2 = false;
				}
			}
			if (flag2)
			{
				this.m_loadInfos.Add(resourceInfo);
			}
		}
		return true;
	}

	// Token: 0x060042E3 RID: 17123 RVA: 0x0015B8B0 File Offset: 0x00159AB0
	public bool AddLoadAndResourceManager(string scenename, bool onAssetBundle, ResourceCategory category, bool dontDestroyOnChangeScene, bool onlyDownload, string rootObjectName)
	{
		if (ResourceManager.Instance && ResourceManager.Instance.IsExistContainer(scenename))
		{
			return false;
		}
		this.AddLoadAndResourceManager(new ResourceSceneLoader.ResourceInfo
		{
			m_scenename = scenename,
			m_isAssetBundle = onAssetBundle,
			m_isloaded = false,
			m_category = category,
			m_dontDestroyOnChangeScene = dontDestroyOnChangeScene,
			m_rootObjectName = ((rootObjectName == null) ? scenename : rootObjectName),
			m_onlyDownload = onlyDownload
		});
		return true;
	}

	// Token: 0x060042E4 RID: 17124 RVA: 0x0015B930 File Offset: 0x00159B30
	public bool AddLoadAndResourceManager(ResourceSceneLoader.ResourceInfo info)
	{
		if (ResourceManager.Instance && ResourceManager.Instance.IsExistContainer(info.m_scenename))
		{
			return false;
		}
		bool isAssetBundle = info.m_isAssetBundle;
		bool onlyDownload = info.m_onlyDownload;
		if (isAssetBundle && AssetBundleLoader.Instance != null)
		{
			if (onlyDownload && AssetBundleLoader.Instance.IsDownloaded(info.m_scenename))
			{
				return false;
			}
			this.m_assetLoadInfos.Add(info);
		}
		else
		{
			if (onlyDownload)
			{
				return false;
			}
			this.m_loadInfos.Add(info);
		}
		return true;
	}

	// Token: 0x060042E5 RID: 17125 RVA: 0x0015B9CC File Offset: 0x00159BCC
	private IEnumerator LoadScene()
	{
		this.m_loadEndCount = 0;
		float loadStartTime = Time.realtimeSinceStartup;
		foreach (ResourceSceneLoader.ResourceInfo loadInfo in this.m_loadInfos)
		{
			if (loadInfo.m_scenename != null)
			{
				float oldTime = Time.realtimeSinceStartup;
				if (this.m_checkTime)
				{
					global::Debug.Log("start load scene " + loadInfo.m_scenename);
				}
				if (loadInfo.m_isAsycnLoad)
				{
					AsyncOperation operation = Application.LoadLevelAdditiveAsync(loadInfo.m_scenename);
					yield return base.StartCoroutine(this.WaitLoard(operation));
				}
				else
				{
					Application.LoadLevelAdditive(loadInfo.m_scenename);
					yield return base.StartCoroutine(this.WaitLoard());
				}
				this.RegisterResourceManager(loadInfo);
				loadInfo.m_isloaded = true;
				this.m_loadEndCount++;
				if (this.m_checkTime)
				{
					float loadTime = Time.realtimeSinceStartup;
					global::Debug.Log("LS:Load File " + loadInfo.m_scenename + " Time is " + (loadTime - oldTime).ToString());
				}
			}
		}
		if (this.m_checkTime)
		{
			float loadEndTime = Time.realtimeSinceStartup;
			global::Debug.Log("LS:All Loading Time is " + (loadEndTime - loadStartTime).ToString());
		}
		yield break;
	}

	// Token: 0x060042E6 RID: 17126 RVA: 0x0015B9E8 File Offset: 0x00159BE8
	private IEnumerator LoadSceneAssetBundle(ResourceSceneLoader.ResourceInfo loadInfo, AssetBundleResult result)
	{
		float oldTime = Time.realtimeSinceStartup;
		if (this.m_checkTime)
		{
			global::Debug.Log("start load scene " + loadInfo.m_scenename);
		}
		if (loadInfo.m_isAsycnLoad)
		{
			AsyncOperation operation = Application.LoadLevelAdditiveAsync(loadInfo.m_scenename);
			yield return base.StartCoroutine(this.WaitLoard(operation));
		}
		else
		{
			Application.LoadLevelAdditive(loadInfo.m_scenename);
			yield return base.StartCoroutine(this.WaitLoard());
		}
		this.RegisterResourceManager(loadInfo);
		loadInfo.m_isloaded = true;
		this.m_loadEndCount++;
		if (this.m_checkTime)
		{
			float loadTime = Time.realtimeSinceStartup;
			global::Debug.Log("LS:Load File " + loadInfo.m_scenename + " Time is " + (loadTime - oldTime).ToString());
		}
		if (result != null && AssetBundleManager.Instance)
		{
			AssetBundleManager.Instance.RequestUnload(result.Path);
		}
		yield break;
	}

	// Token: 0x060042E7 RID: 17127 RVA: 0x0015BA20 File Offset: 0x00159C20
	private void StartAssetBundleLoad()
	{
		foreach (ResourceSceneLoader.ResourceInfo resourceInfo in this.m_assetLoadInfos)
		{
			AssetBundleLoader.Instance.RequestLoadScene(resourceInfo.m_scenename, true, base.gameObject);
		}
	}

	// Token: 0x060042E8 RID: 17128 RVA: 0x0015BA98 File Offset: 0x00159C98
	private void RegisterResourceManager(ResourceSceneLoader.ResourceInfo loadInfo)
	{
		if (loadInfo.m_category != ResourceCategory.UNKNOWN && ResourceManager.Instance != null)
		{
			GameObject gameObject = GameObject.Find(loadInfo.m_rootObjectName);
			if (gameObject != null)
			{
				ResourceManager.Instance.AddCategorySceneObjects(loadInfo.m_category, loadInfo.m_scenename, gameObject, loadInfo.m_dontDestroyOnChangeScene);
			}
		}
	}

	// Token: 0x060042E9 RID: 17129 RVA: 0x0015BAF8 File Offset: 0x00159CF8
	private IEnumerator WaitLoard(AsyncOperation async)
	{
		while (async.progress < 0.9f)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x060042EA RID: 17130 RVA: 0x0015BB1C File Offset: 0x00159D1C
	private IEnumerator WaitLoard()
	{
		yield return null;
		yield break;
	}

	// Token: 0x060042EB RID: 17131 RVA: 0x0015BB30 File Offset: 0x00159D30
	private void AssetBundleResponseSucceed(MsgAssetBundleResponseSucceed msg)
	{
		string fileName = msg.m_request.FileName;
		AssetBundleResult result = msg.m_result;
		foreach (ResourceSceneLoader.ResourceInfo resourceInfo in this.m_assetLoadInfos)
		{
			if (resourceInfo.m_scenename.Equals(fileName))
			{
				if (resourceInfo.m_onlyDownload)
				{
					resourceInfo.m_isloaded = true;
					this.m_loadEndCount++;
					if (result != null && AssetBundleManager.Instance)
					{
						AssetBundleManager.Instance.RequestUnload(result.Path);
					}
				}
				else
				{
					base.StartCoroutine(this.LoadSceneAssetBundle(resourceInfo, result));
				}
				break;
			}
		}
	}

	// Token: 0x17000926 RID: 2342
	// (get) Token: 0x060042EC RID: 17132 RVA: 0x0015BC14 File Offset: 0x00159E14
	public bool Loaded
	{
		get
		{
			return this.m_isloaded;
		}
	}

	// Token: 0x040038B4 RID: 14516
	private bool m_checkTime = true;

	// Token: 0x040038B5 RID: 14517
	private List<ResourceSceneLoader.ResourceInfo> m_loadInfos = new List<ResourceSceneLoader.ResourceInfo>();

	// Token: 0x040038B6 RID: 14518
	private List<ResourceSceneLoader.ResourceInfo> m_assetLoadInfos = new List<ResourceSceneLoader.ResourceInfo>();

	// Token: 0x040038B7 RID: 14519
	private bool m_isloaded;

	// Token: 0x040038B8 RID: 14520
	private bool m_pause;

	// Token: 0x040038B9 RID: 14521
	private int m_loadEndCount;

	// Token: 0x020009E8 RID: 2536
	public class ResourceInfo
	{
		// Token: 0x060042ED RID: 17133 RVA: 0x0015BC1C File Offset: 0x00159E1C
		public ResourceInfo()
		{
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x0015BC2C File Offset: 0x00159E2C
		public ResourceInfo(ResourceCategory cate, string name, bool assetbundle, bool onlyDownload, bool dontdestroyOnScene, string rootObjectName = null, bool isAsyncLoad = false)
		{
			this.m_scenename = name;
			this.m_category = cate;
			this.m_dontDestroyOnChangeScene = (!onlyDownload && dontdestroyOnScene);
			this.m_isAssetBundle = assetbundle;
			this.m_onlyDownload = onlyDownload;
			this.m_rootObjectName = ((rootObjectName != null) ? rootObjectName : this.m_scenename);
			this.m_isAsycnLoad = isAsyncLoad;
			this.m_isloaded = false;
		}

		// Token: 0x040038BA RID: 14522
		public bool m_isAssetBundle;

		// Token: 0x040038BB RID: 14523
		public bool m_onlyDownload;

		// Token: 0x040038BC RID: 14524
		public bool m_isloaded;

		// Token: 0x040038BD RID: 14525
		public string m_scenename;

		// Token: 0x040038BE RID: 14526
		public ResourceCategory m_category = ResourceCategory.UNKNOWN;

		// Token: 0x040038BF RID: 14527
		public bool m_dontDestroyOnChangeScene;

		// Token: 0x040038C0 RID: 14528
		public string m_rootObjectName;

		// Token: 0x040038C1 RID: 14529
		public bool m_isAsycnLoad;
	}
}
