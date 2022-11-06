using System;
using System.Collections;
using System.Collections.Generic;
using DataTable;
using UnityEngine;

// Token: 0x020000F4 RID: 244
public class ChaoTextureManager : MonoBehaviour
{
	// Token: 0x17000150 RID: 336
	// (get) Token: 0x0600074B RID: 1867 RVA: 0x0002ABD4 File Offset: 0x00028DD4
	public static ChaoTextureManager Instance
	{
		get
		{
			return ChaoTextureManager.instance;
		}
	}

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x0600074C RID: 1868 RVA: 0x0002ABDC File Offset: 0x00028DDC
	public int LoadingChaoId
	{
		get
		{
			return this.m_loadingChaoId;
		}
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x0002ABE4 File Offset: 0x00028DE4
	public Texture GetLoadedTexture(int chao_id)
	{
		if (chao_id < 0)
		{
			return null;
		}
		Texture result = null;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, this.GetChaoTexName(chao_id));
		if (gameObject != null)
		{
			AssetBundleTexture component = gameObject.GetComponent<AssetBundleTexture>();
			if (component != null)
			{
				result = component.m_tex;
			}
		}
		return result;
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x0002AC38 File Offset: 0x00028E38
	public void GetTexture(int chao_id, ChaoTextureManager.CallbackInfo info)
	{
		if (info == null)
		{
			return;
		}
		if (chao_id < 0)
		{
			info.Disable();
			return;
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, this.GetChaoTexName(chao_id));
		if (gameObject != null)
		{
			AssetBundleTexture component = gameObject.GetComponent<AssetBundleTexture>();
			if (component != null)
			{
				info.LoadDone(component.m_tex);
			}
		}
		else
		{
			bool flag = true;
			foreach (ChaoTextureManager.LoadRequestData loadRequestData in this.m_loadingList)
			{
				if (loadRequestData.m_chaoId == chao_id)
				{
					if (info != null)
					{
						loadRequestData.AddCallback(info);
					}
					flag = false;
					break;
				}
			}
			if (flag)
			{
				if (this.m_currentRequest != null && this.m_currentRequest.m_chaoId == chao_id)
				{
					this.m_currentRequest.AddCallback(info);
					flag = false;
				}
				if (flag)
				{
					ChaoTextureManager.LoadRequestData loadRequestData2 = new ChaoTextureManager.LoadRequestData(base.gameObject, chao_id, info, ChaoTextureManager.LoadRequestData.LoadType.Default);
					if (this.m_currentRequest == null)
					{
						this.m_currentRequest = loadRequestData2;
						this.m_currentRequest.StartLoad();
					}
					else
					{
						this.m_loadingList.Add(loadRequestData2);
					}
					base.enabled = true;
				}
			}
		}
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x0002AD90 File Offset: 0x00028F90
	public void RequestLoadingPageChaoTexture()
	{
		ChaoData loadingChao = ChaoTable.GetLoadingChao();
		if (loadingChao != null)
		{
			this.m_loadingChaoId = loadingChao.id;
			ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(null, null, false);
			this.GetTexture(loadingChao.id, info);
		}
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x0002ADCC File Offset: 0x00028FCC
	public void RequestTitleLoadChaoTexture()
	{
		this.RequestLoadingPageChaoTexture();
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x0002ADD4 File Offset: 0x00028FD4
	public void RemoveChaoTexture(int chao_id)
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_texObj, this.GetChaoTexName(chao_id));
		if (gameObject != null)
		{
			string name = gameObject.name;
			int num = -1;
			int num2 = -1;
			SaveDataManager saveDataManager = SaveDataManager.Instance;
			if (saveDataManager != null)
			{
				num = saveDataManager.PlayerData.MainChaoID;
				num2 = saveDataManager.PlayerData.SubChaoID;
			}
			List<string> list = new List<string>();
			if (num >= 0)
			{
				list.Add(this.GetChaoTexName(num));
			}
			if (num2 >= 0)
			{
				list.Add(this.GetChaoTexName(num2));
			}
			if (this.m_loadingChaoId >= 0)
			{
				list.Add(this.GetChaoTexName(this.m_loadingChaoId));
			}
			if (!list.Contains(name))
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			base.StartCoroutine(this.WaitUnloadUnusedAssets());
		}
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x0002AEAC File Offset: 0x000290AC
	public void RemoveChaoTextureForMainMenuEnd()
	{
		int num = -1;
		int num2 = -1;
		SaveDataManager saveDataManager = SaveDataManager.Instance;
		if (saveDataManager != null)
		{
			num = saveDataManager.PlayerData.MainChaoID;
			num2 = saveDataManager.PlayerData.SubChaoID;
		}
		List<string> list = new List<string>();
		if (num >= 0)
		{
			list.Add(this.GetChaoTexName(num));
		}
		if (num2 >= 0)
		{
			list.Add(this.GetChaoTexName(num2));
		}
		if (this.m_loadingChaoId >= 0)
		{
			list.Add(this.GetChaoTexName(this.m_loadingChaoId));
		}
		if (EventManager.Instance != null)
		{
			RewardChaoData rewardChaoData = EventManager.Instance.GetRewardChaoData();
			if (rewardChaoData != null && rewardChaoData.chao_id >= 0)
			{
				list.Add(this.GetChaoTexName(rewardChaoData.chao_id));
			}
		}
		List<GameObject> list2 = new List<GameObject>();
		int childCount = this.m_texObj.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = this.m_texObj.transform.GetChild(i);
			string name = child.name;
			if (!list.Contains(name))
			{
				list2.Add(child.gameObject);
			}
		}
		foreach (GameObject obj in list2)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.CancelLoad();
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x0002B03C File Offset: 0x0002923C
	public void RemoveChaoTexture()
	{
		int num = -1;
		int num2 = -1;
		SaveDataManager saveDataManager = SaveDataManager.Instance;
		if (saveDataManager != null)
		{
			num = saveDataManager.PlayerData.MainChaoID;
			num2 = saveDataManager.PlayerData.SubChaoID;
		}
		List<string> list = new List<string>();
		if (num >= 0)
		{
			list.Add(this.GetChaoTexName(num));
		}
		if (num2 >= 0)
		{
			list.Add(this.GetChaoTexName(num2));
		}
		if (this.m_loadingChaoId >= 0)
		{
			list.Add(this.GetChaoTexName(this.m_loadingChaoId));
		}
		List<GameObject> list2 = new List<GameObject>();
		int childCount = this.m_texObj.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = this.m_texObj.transform.GetChild(i);
			string name = child.name;
			if (!list.Contains(name))
			{
				list2.Add(child.gameObject);
			}
		}
		foreach (GameObject obj in list2)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.CancelLoad();
		base.StartCoroutine(this.WaitUnloadUnusedAssets());
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x0002B198 File Offset: 0x00029398
	private IEnumerator WaitUnloadUnusedAssets()
	{
		int waite_frame = 1;
		while (waite_frame > 0)
		{
			waite_frame--;
			yield return null;
		}
		Resources.UnloadUnusedAssets();
		yield break;
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x0002B1AC File Offset: 0x000293AC
	public bool IsLoaded()
	{
		return this.m_currentRequest == null && this.m_loadingList.Count <= 0;
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x0002B1D0 File Offset: 0x000293D0
	protected void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x0002B1D8 File Offset: 0x000293D8
	private void Start()
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "Event";
		gameObject.transform.parent = base.transform;
		this.m_texObj = new GameObject();
		this.m_texObj.name = "Texture";
		this.m_texObj.transform.parent = base.transform;
		base.enabled = false;
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x0002B240 File Offset: 0x00029440
	private void OnDestroy()
	{
		if (ChaoTextureManager.instance == this)
		{
			ChaoTextureManager.instance = null;
		}
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x0002B258 File Offset: 0x00029458
	private void SetInstance()
	{
		if (ChaoTextureManager.instance == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			ChaoTextureManager.instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x0002B28C File Offset: 0x0002948C
	private void Update()
	{
		if (this.m_currentRequest != null)
		{
			this.m_currentRequest.Update();
			if (this.m_currentRequest.Loaded)
			{
				this.m_currentRequest = null;
				if (this.m_loadingList.Count > 0)
				{
					base.StartCoroutine(this.LoadNext());
				}
				else
				{
					base.enabled = false;
				}
			}
		}
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x0002B2F0 File Offset: 0x000294F0
	private IEnumerator LoadNext()
	{
		float startTime = Time.realtimeSinceStartup;
		for (;;)
		{
			float spendTime = 0f;
			float currentTime = Time.realtimeSinceStartup;
			spendTime = currentTime - startTime;
			if (spendTime >= 0.1f)
			{
				break;
			}
			yield return null;
		}
		if (this.m_loadingList.Count > 0)
		{
			this.m_currentRequest = this.m_loadingList[0];
			this.m_currentRequest.StartLoad();
			this.m_loadingList.Remove(this.m_loadingList[0]);
		}
		yield break;
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x0002B30C File Offset: 0x0002950C
	private void CancelLoad()
	{
		if (this.m_currentRequest != null)
		{
			this.m_currentRequest.Cancel();
		}
		this.m_loadingList.Clear();
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x0002B330 File Offset: 0x00029530
	private string GetChaoTexName(int chao_id)
	{
		return "ui_tex_chao_" + chao_id.ToString("0000");
	}

	// Token: 0x04000594 RID: 1428
	private const string TEX_FOLDER = "Texture";

	// Token: 0x04000595 RID: 1429
	private const string EVENT_FOLDER = "Event";

	// Token: 0x04000596 RID: 1430
	[SerializeField]
	public Texture m_defaultTexture;

	// Token: 0x04000597 RID: 1431
	private List<ChaoTextureManager.LoadRequestData> m_loadingList = new List<ChaoTextureManager.LoadRequestData>();

	// Token: 0x04000598 RID: 1432
	private ChaoTextureManager.LoadRequestData m_currentRequest;

	// Token: 0x04000599 RID: 1433
	private GameObject m_texObj;

	// Token: 0x0400059A RID: 1434
	private int m_loadingChaoId = -1;

	// Token: 0x0400059B RID: 1435
	private static ChaoTextureManager instance;

	// Token: 0x020000F5 RID: 245
	public class TextureData
	{
		// Token: 0x0600075E RID: 1886 RVA: 0x0002B348 File Offset: 0x00029548
		public TextureData()
		{
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0002B350 File Offset: 0x00029550
		public TextureData(Texture tex, int chao_id)
		{
			this.tex = tex;
			this.chao_id = chao_id;
		}

		// Token: 0x0400059C RID: 1436
		public int chao_id;

		// Token: 0x0400059D RID: 1437
		public Texture tex;
	}

	// Token: 0x020000F6 RID: 246
	public class CallbackInfo
	{
		// Token: 0x06000760 RID: 1888 RVA: 0x0002B368 File Offset: 0x00029568
		public CallbackInfo(UITexture uiTex, ChaoTextureManager.CallbackInfo.LoadFinishCallback callback = null, bool nguiRebuild = false)
		{
			this.m_uiTex = uiTex;
			this.m_callback = callback;
			this.m_nguiRebuild = nguiRebuild;
			if (this.m_uiTex != null)
			{
				this.m_uiTex.enabled = true;
				this.m_uiTex.SetTexture(ChaoTextureManager.Instance.m_defaultTexture);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0002B3C4 File Offset: 0x000295C4
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x0002B3CC File Offset: 0x000295CC
		public Texture Texture
		{
			get
			{
				return this.m_texture;
			}
			private set
			{
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0002B3D0 File Offset: 0x000295D0
		public void Disable()
		{
			if (this.m_uiTex != null)
			{
				this.m_uiTex.enabled = false;
				this.m_uiTex.SetTexture(null);
			}
			if (this.m_callback != null)
			{
				this.m_callback(null);
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0002B420 File Offset: 0x00029620
		public void LoadDone(Texture tex)
		{
			this.m_texture = tex;
			if (this.m_uiTex != null)
			{
				this.m_uiTex.enabled = true;
				if (this.m_nguiRebuild)
				{
					this.m_uiTex.mainTexture = tex;
				}
				else
				{
					this.m_uiTex.SetTexture(tex);
				}
			}
			if (this.m_callback != null)
			{
				this.m_callback(this.m_texture);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0002B498 File Offset: 0x00029698
		public bool LoadEnded
		{
			get
			{
				return this.m_texture != null;
			}
		}

		// Token: 0x0400059E RID: 1438
		private Texture m_texture;

		// Token: 0x0400059F RID: 1439
		private UITexture m_uiTex;

		// Token: 0x040005A0 RID: 1440
		private bool m_nguiRebuild;

		// Token: 0x040005A1 RID: 1441
		private ChaoTextureManager.CallbackInfo.LoadFinishCallback m_callback;

		// Token: 0x02000A74 RID: 2676
		// (Invoke) Token: 0x0600480A RID: 18442
		public delegate void LoadFinishCallback(Texture tex);
	}

	// Token: 0x020000F7 RID: 247
	public class LoadRequestData
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x0002B4B0 File Offset: 0x000296B0
		public LoadRequestData()
		{
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0002B4B8 File Offset: 0x000296B8
		public LoadRequestData(GameObject managerObj, int chaoId, ChaoTextureManager.CallbackInfo info, ChaoTextureManager.LoadRequestData.LoadType type = ChaoTextureManager.LoadRequestData.LoadType.Default)
		{
			this.m_managerObj = managerObj;
			this.m_chaoId = chaoId;
			this.m_type = type;
			this.m_infoList = new List<ChaoTextureManager.CallbackInfo>();
			this.m_infoList.Add(info);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0002B4F0 File Offset: 0x000296F0
		public void AddCallback(ChaoTextureManager.CallbackInfo info)
		{
			if (info == null)
			{
				return;
			}
			if (this.m_infoList.Contains(info))
			{
				return;
			}
			this.m_infoList.Add(info);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0002B518 File Offset: 0x00029718
		public void StartLoad()
		{
			this.m_requestMode = ChaoTextureManager.LoadRequestData.RequestMode.LOAD;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0002B524 File Offset: 0x00029724
		public void Cancel()
		{
			this.m_cancel = true;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0002B530 File Offset: 0x00029730
		public bool Loaded
		{
			get
			{
				return this.m_requestMode == ChaoTextureManager.LoadRequestData.RequestMode.END;
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0002B544 File Offset: 0x00029744
		public void Update()
		{
			switch (this.m_requestMode)
			{
			case ChaoTextureManager.LoadRequestData.RequestMode.LOAD:
			{
				GameObject gameObject = new GameObject("SceneLoader");
				this.m_sceneLoader = gameObject.AddComponent<ResourceSceneLoader>();
				string scenename = "ui_tex_chao_" + this.m_chaoId.ToString("0000");
				ResourceSceneLoader.ResourceInfo resourceInfo = new ResourceSceneLoader.ResourceInfo();
				resourceInfo.m_scenename = scenename;
				resourceInfo.m_isAssetBundle = true;
				resourceInfo.m_onlyDownload = false;
				resourceInfo.m_isAsycnLoad = true;
				resourceInfo.m_category = ResourceCategory.UI;
				this.m_sceneLoader.AddLoadAndResourceManager(resourceInfo);
				this.m_requestMode = ChaoTextureManager.LoadRequestData.RequestMode.LOAD_WAIT;
				break;
			}
			case ChaoTextureManager.LoadRequestData.RequestMode.LOAD_WAIT:
				if (this.m_sceneLoader != null)
				{
					if (this.m_sceneLoader.Loaded)
					{
						this.m_requestMode = ChaoTextureManager.LoadRequestData.RequestMode.SET_TEXTURE;
					}
				}
				else
				{
					this.m_requestMode = ChaoTextureManager.LoadRequestData.RequestMode.SET_TEXTURE;
				}
				break;
			case ChaoTextureManager.LoadRequestData.RequestMode.SET_TEXTURE:
				if (this.m_cancel)
				{
					string name = "ui_tex_chao_" + this.m_chaoId.ToString("0000");
					GameObject gameObject2 = GameObject.Find(name);
					if (gameObject2 != null)
					{
						UnityEngine.Object.Destroy(gameObject2);
					}
				}
				else
				{
					string name2 = "ui_tex_chao_" + this.m_chaoId.ToString("0000");
					GameObject gameObject3 = GameObject.Find(name2);
					if (gameObject3 != null)
					{
						ChaoTextureManager.LoadRequestData.LoadType type = this.m_type;
						if (type != ChaoTextureManager.LoadRequestData.LoadType.Default)
						{
							if (type == ChaoTextureManager.LoadRequestData.LoadType.Event)
							{
								GameObject gameObject4 = GameObjectUtil.FindChildGameObject(this.m_managerObj, "Event");
								gameObject3.transform.parent = gameObject4.transform;
							}
						}
						else
						{
							GameObject gameObject5 = GameObjectUtil.FindChildGameObject(this.m_managerObj, "Texture");
							gameObject3.transform.parent = gameObject5.transform;
						}
						gameObject3.SetActive(false);
					}
					GameObject gameObject6 = GameObjectUtil.FindChildGameObject(this.m_managerObj, name2);
					AssetBundleTexture component = gameObject6.GetComponent<AssetBundleTexture>();
					Texture tex = component.m_tex;
					if (tex != null)
					{
						for (int i = 0; i < this.m_infoList.Count; i++)
						{
							ChaoTextureManager.CallbackInfo callbackInfo = this.m_infoList[i];
							if (callbackInfo != null)
							{
								callbackInfo.LoadDone(tex);
							}
						}
						this.m_infoList.Clear();
					}
				}
				UnityEngine.Object.Destroy(this.m_sceneLoader.gameObject);
				this.m_requestMode = ChaoTextureManager.LoadRequestData.RequestMode.END;
				break;
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0002B7B4 File Offset: 0x000299B4
		public Texture GetTexture(int chao_id)
		{
			string name = "ui_tex_chao_" + this.m_chaoId.ToString("0000");
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_managerObj, name);
			if (gameObject != null)
			{
				AssetBundleTexture component = gameObject.GetComponent<AssetBundleTexture>();
				if (component != null)
				{
					return component.m_tex;
				}
			}
			return null;
		}

		// Token: 0x040005A2 RID: 1442
		private ChaoTextureManager.LoadRequestData.RequestMode m_requestMode;

		// Token: 0x040005A3 RID: 1443
		private ResourceSceneLoader m_sceneLoader;

		// Token: 0x040005A4 RID: 1444
		private GameObject m_managerObj;

		// Token: 0x040005A5 RID: 1445
		private bool m_cancel;

		// Token: 0x040005A6 RID: 1446
		public int m_chaoId;

		// Token: 0x040005A7 RID: 1447
		public ChaoTextureManager.LoadRequestData.LoadType m_type;

		// Token: 0x040005A8 RID: 1448
		private List<ChaoTextureManager.CallbackInfo> m_infoList;

		// Token: 0x020000F8 RID: 248
		public enum LoadType
		{
			// Token: 0x040005AA RID: 1450
			Default,
			// Token: 0x040005AB RID: 1451
			Event
		}

		// Token: 0x020000F9 RID: 249
		private enum RequestMode
		{
			// Token: 0x040005AD RID: 1453
			IDLE,
			// Token: 0x040005AE RID: 1454
			LOAD,
			// Token: 0x040005AF RID: 1455
			LOAD_WAIT,
			// Token: 0x040005B0 RID: 1456
			SET_TEXTURE,
			// Token: 0x040005B1 RID: 1457
			END
		}
	}
}
