using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using App;
using Message;
using UnityEngine;

// Token: 0x020009D4 RID: 2516
public class AssetBundleLoader : MonoBehaviour
{
	// Token: 0x06004214 RID: 16916 RVA: 0x0015875C File Offset: 0x0015695C
	public string[] GetChaoTextureList()
	{
		Dictionary<string, AssetBundleLoader.CachedFileInfo>.KeyCollection keys = this.m_bundleInfoList.Keys;
		List<string> list = new List<string>();
		foreach (string text in keys)
		{
			if (text.Contains("ui_tex_chao_"))
			{
				list.Add(text);
			}
			else if (text.Contains("ui_tex_player_"))
			{
				list.Add(text);
			}
			else if (text.Contains("ui_tex_mile_w"))
			{
				list.Add(text);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06004215 RID: 16917 RVA: 0x00158820 File Offset: 0x00156A20
	private void Start()
	{
		if (AssetBundleManager.Instance == null)
		{
			AssetBundleManager.Create();
		}
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	// Token: 0x06004216 RID: 16918 RVA: 0x00158840 File Offset: 0x00156A40
	private void Update()
	{
		AssetBundleManager assetBundleManager = AssetBundleManager.Instance;
		if (assetBundleManager != null)
		{
			bool flag = assetBundleManager.Executing || assetBundleManager.RequestCount > 0;
			bool flag2 = this.m_retryRequest != null && this.m_retryRequest.Count > 0;
			if (!flag && !flag2)
			{
				AssetBundleLoader.LoadingDataInfo loadingDataInfo = null;
				foreach (KeyValuePair<string, AssetBundleLoader.LoadingDataInfo> keyValuePair in this.m_loadingDataList)
				{
					loadingDataInfo = keyValuePair.Value;
				}
				if (loadingDataInfo != null)
				{
					global::Debug.Log("ExecuteRequest:" + loadingDataInfo.fullPath);
					this.ExecuteLoadScene(loadingDataInfo);
				}
			}
		}
	}

	// Token: 0x06004217 RID: 16919 RVA: 0x00158924 File Offset: 0x00156B24
	public void Initialize()
	{
		if (Env.useAssetBundle)
		{
			string path = NetUtil.GetAssetBundleUrl() + "ablist.txt";
			AssetBundleManager.Instance.RequestNoCache(path, global::AssetBundleRequest.Type.TEXT, base.gameObject);
			if (NetMonitor.Instance != null)
			{
				this.m_connectUIDisplayTime = 0.01f;
				NetMonitor.Instance.StartMonitor(new AssetBundleRetryProcess(base.gameObject), this.m_connectUIDisplayTime, HudNetworkConnect.DisplayType.ALL);
			}
			this.m_mode = AssetBundleLoader.Mode.DownloadList;
		}
		else
		{
			this.m_mode = AssetBundleLoader.Mode.EnableLoad;
		}
	}

	// Token: 0x06004218 RID: 16920 RVA: 0x001589A8 File Offset: 0x00156BA8
	public void ClearDownloadList()
	{
		this.m_mode = AssetBundleLoader.Mode.Non;
		this.m_bundleInfoList = null;
		this.m_loadingDataList.Clear();
		this.m_retryRequest.Clear();
	}

	// Token: 0x06004219 RID: 16921 RVA: 0x001589DC File Offset: 0x00156BDC
	public bool IsEnableDownlad()
	{
		return this.m_mode == AssetBundleLoader.Mode.EnableLoad;
	}

	// Token: 0x0600421A RID: 16922 RVA: 0x001589E8 File Offset: 0x00156BE8
	public void RequestLoadScene(string filePath, bool cashed, GameObject returnObject)
	{
		if (this.m_mode != AssetBundleLoader.Mode.EnableLoad)
		{
			global::Debug.Log("AssetBundleLoader Not Initialized.");
			return;
		}
		if (Env.useAssetBundle)
		{
			AssetBundleLoader.LoadingDataInfo loadingDataInfo = new AssetBundleLoader.LoadingDataInfo();
			loadingDataInfo.name = Path.GetFileNameWithoutExtension(filePath);
			loadingDataInfo.filePath = filePath;
			loadingDataInfo.fullPath = this.GetDownloadURL(filePath);
			loadingDataInfo.cashed = cashed;
			loadingDataInfo.returnObject = returnObject;
			bool flag = !this.IsDownloaded(filePath);
			if (flag)
			{
				this.m_isConnecting = true;
				if (NetMonitor.Instance != null && NetMonitor.Instance.IsIdle())
				{
					this.m_connectUIDisplayTime = -0.1f;
					NetMonitor.Instance.StartMonitor(new AssetBundleRetryProcess(base.gameObject), this.m_connectUIDisplayTime, HudNetworkConnect.DisplayType.ALL);
				}
			}
			this.m_loadingDataList.Add(loadingDataInfo.name, loadingDataInfo);
		}
		else
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
			Application.LoadLevelAdditive(fileNameWithoutExtension);
		}
	}

	// Token: 0x0600421B RID: 16923 RVA: 0x00158ACC File Offset: 0x00156CCC
	public void RetryLoadScene(AssetBundleRetryProcess retryProcess)
	{
		if (NetMonitor.Instance != null)
		{
			NetMonitor.Instance.StartMonitor(new AssetBundleRetryProcess(base.gameObject), this.m_connectUIDisplayTime, HudNetworkConnect.DisplayType.ALL);
		}
		foreach (global::AssetBundleRequest request in this.m_retryRequest)
		{
			AssetBundleManager.Instance.ReRequest(request);
		}
		this.m_retryRequest.Clear();
	}

	// Token: 0x0600421C RID: 16924 RVA: 0x00158B70 File Offset: 0x00156D70
	public bool IsDownloaded(string fileName)
	{
		AssetBundleLoader.CachedFileInfo fileInfo = this.GetFileInfo(fileName);
		return fileInfo != null && fileInfo.downloaded;
	}

	// Token: 0x0600421D RID: 16925 RVA: 0x00158B94 File Offset: 0x00156D94
	private void ExecuteLoadScene(AssetBundleLoader.LoadingDataInfo loadDataInfo)
	{
		string fullPath = loadDataInfo.fullPath;
		bool cashed = loadDataInfo.cashed;
		if (cashed)
		{
			string filePath = loadDataInfo.filePath;
			AssetBundleLoader.CachedFileInfo fileInfo = this.GetFileInfo(filePath);
			loadDataInfo.cashedInfo = fileInfo;
			this.LoadSceneCache(fullPath, fileInfo);
		}
		else
		{
			AssetBundleManager.Instance.RequestNoCache(fullPath, global::AssetBundleRequest.Type.SCENE, base.gameObject);
		}
	}

	// Token: 0x0600421E RID: 16926 RVA: 0x00158BEC File Offset: 0x00156DEC
	private void LoadSceneCache(string fullPath, AssetBundleLoader.CachedFileInfo info)
	{
		int version = 0;
		uint crc = 0U;
		if (info != null)
		{
			version = info.version;
			crc = info.crc;
		}
		AssetBundleManager.Instance.Request(fullPath, version, crc, global::AssetBundleRequest.Type.SCENE, base.gameObject, true);
	}

	// Token: 0x0600421F RID: 16927 RVA: 0x00158C28 File Offset: 0x00156E28
	private void AssetBundleResponseSucceed(MsgAssetBundleResponseSucceed msg)
	{
		if (this.m_mode == AssetBundleLoader.Mode.DownloadList)
		{
			base.StartCoroutine(this.WaitCachingReady());
			string text = msg.m_result.Text;
			if (text != null)
			{
				this.ParseAssetBundleList(text);
			}
			AssetBundleManager.Instance.RequestUnload(msg.m_request.path);
			if (NetMonitor.Instance != null)
			{
				NetMonitor.Instance.EndMonitorForward(msg, base.gameObject, null);
				NetMonitor.Instance.EndMonitorBackward();
			}
			this.m_mode = AssetBundleLoader.Mode.EnableLoad;
		}
		else
		{
			AssetBundleResult result = msg.m_result;
			if (result != null)
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(result.Path);
				AssetBundleLoader.LoadingDataInfo loadingDataInfo = null;
				if (this.m_loadingDataList.TryGetValue(fileNameWithoutExtension, out loadingDataInfo))
				{
					if (loadingDataInfo.cashedInfo != null)
					{
						loadingDataInfo.cashedInfo.downloaded = true;
					}
					if (loadingDataInfo.returnObject != null)
					{
						loadingDataInfo.returnObject.SendMessage("AssetBundleResponseSucceed", msg, SendMessageOptions.DontRequireReceiver);
					}
					this.m_loadingDataList.Remove(fileNameWithoutExtension);
					if (NetMonitor.Instance != null && this.m_isConnecting && this.m_loadingDataList.Count <= 0)
					{
						this.m_isConnecting = false;
						NetMonitor.Instance.EndMonitorForward(msg, base.gameObject, null);
						NetMonitor.Instance.EndMonitorBackward();
					}
				}
			}
		}
	}

	// Token: 0x06004220 RID: 16928 RVA: 0x00158D78 File Offset: 0x00156F78
	private void AssetBundleResponseFailed(MsgAssetBundleResponseFailed msg)
	{
		global::Debug.Log("Load Failed.");
		if (this.m_mode == AssetBundleLoader.Mode.DownloadList)
		{
			this.m_retryRequest.Add(msg.m_request);
			if (NetMonitor.Instance != null)
			{
				NetMonitor.Instance.EndMonitorForward(new MsgAssetBundleResponseFailedMonitor(), null, null);
				NetMonitor.Instance.EndMonitorBackward();
			}
		}
		else
		{
			string fileName = msg.m_request.FileName;
			AssetBundleLoader.LoadingDataInfo loadingDataInfo = null;
			if (this.m_loadingDataList.TryGetValue(fileName, out loadingDataInfo))
			{
				this.m_retryRequest.Add(msg.m_request);
				NetMonitor.Instance.EndMonitorForward(new MsgAssetBundleResponseFailedMonitor(), loadingDataInfo.returnObject, null);
				NetMonitor.Instance.EndMonitorBackward();
			}
		}
	}

	// Token: 0x06004221 RID: 16929 RVA: 0x00158E30 File Offset: 0x00157030
	private IEnumerator WaitCachingReady()
	{
		while (!Caching.ready)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06004222 RID: 16930 RVA: 0x00158E44 File Offset: 0x00157044
	private void AssetBundleResponseFailedMonitor(MsgAssetBundleResponseFailedMonitor msg)
	{
	}

	// Token: 0x06004223 RID: 16931 RVA: 0x00158E48 File Offset: 0x00157048
	private void ParseAssetBundleList(string str)
	{
		List<CsvParser.CsvFields> list = CsvParser.ParseCsvFromText(str);
		if (list != null && list.Count > 0)
		{
			this.m_bundleInfoList = new Dictionary<string, AssetBundleLoader.CachedFileInfo>();
			foreach (CsvParser.CsvFields csvFields in list)
			{
				List<string> fieldList = csvFields.FieldList;
				if (fieldList.Count >= 2)
				{
					AssetBundleLoader.CachedFileInfo cachedFileInfo = new AssetBundleLoader.CachedFileInfo();
					cachedFileInfo.name = Path.GetFileNameWithoutExtension(fieldList[0]);
					int.TryParse(fieldList[1], out cachedFileInfo.version);
					if (fieldList.Count >= 3)
					{
						uint.TryParse(fieldList[2], out cachedFileInfo.crc);
					}
					string downloadURL = this.GetDownloadURL(cachedFileInfo.name);
					if (Caching.IsVersionCached(downloadURL, cachedFileInfo.version))
					{
						cachedFileInfo.downloaded = true;
					}
					else
					{
						cachedFileInfo.downloaded = false;
					}
					this.m_bundleInfoList.Add(cachedFileInfo.name, cachedFileInfo);
				}
			}
		}
	}

	// Token: 0x06004224 RID: 16932 RVA: 0x00158F74 File Offset: 0x00157174
	private AssetBundleLoader.CachedFileInfo GetFileInfo(string path)
	{
		if (this.m_bundleInfoList == null)
		{
			return null;
		}
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
		AssetBundleLoader.CachedFileInfo result = null;
		this.m_bundleInfoList.TryGetValue(fileNameWithoutExtension, out result);
		return result;
	}

	// Token: 0x06004225 RID: 16933 RVA: 0x00158FA8 File Offset: 0x001571A8
	private string GetDownloadURL(string filePath)
	{
		string text = NetUtil.GetAssetBundleUrl() + filePath;
		string extension = Path.GetExtension(filePath);
		if (string.IsNullOrEmpty(extension))
		{
			text += ".unity3d";
		}
		return text;
	}

	// Token: 0x06004226 RID: 16934 RVA: 0x00158FE0 File Offset: 0x001571E0
	protected void Awake()
	{
		this.CheckInstance();
	}

	// Token: 0x06004227 RID: 16935 RVA: 0x00158FEC File Offset: 0x001571EC
	public static void Create()
	{
		if (AssetBundleLoader.instance == null)
		{
			GameObject gameObject = new GameObject("AssetBundleLoader");
			gameObject.AddComponent<AssetBundleLoader>();
			if (AssetBundleManager.Instance == null)
			{
				AssetBundleManager.Create();
			}
		}
	}

	// Token: 0x170008F9 RID: 2297
	// (get) Token: 0x06004228 RID: 16936 RVA: 0x00159030 File Offset: 0x00157230
	public static AssetBundleLoader Instance
	{
		get
		{
			return AssetBundleLoader.instance;
		}
	}

	// Token: 0x06004229 RID: 16937 RVA: 0x00159038 File Offset: 0x00157238
	protected bool CheckInstance()
	{
		if (AssetBundleLoader.instance == null)
		{
			AssetBundleLoader.instance = this;
			return true;
		}
		if (this == AssetBundleLoader.Instance)
		{
			return true;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		return false;
	}

	// Token: 0x0600422A RID: 16938 RVA: 0x0015907C File Offset: 0x0015727C
	private void OnDestroy()
	{
		if (this == AssetBundleLoader.instance)
		{
			AssetBundleLoader.instance = null;
		}
	}

	// Token: 0x0400383E RID: 14398
	private Dictionary<string, AssetBundleLoader.CachedFileInfo> m_bundleInfoList;

	// Token: 0x0400383F RID: 14399
	private Dictionary<string, AssetBundleLoader.LoadingDataInfo> m_loadingDataList = new Dictionary<string, AssetBundleLoader.LoadingDataInfo>();

	// Token: 0x04003840 RID: 14400
	private List<global::AssetBundleRequest> m_retryRequest = new List<global::AssetBundleRequest>();

	// Token: 0x04003841 RID: 14401
	private AssetBundleLoader.Mode m_mode;

	// Token: 0x04003842 RID: 14402
	private bool m_isConnecting;

	// Token: 0x04003843 RID: 14403
	private float m_connectUIDisplayTime;

	// Token: 0x04003844 RID: 14404
	private static AssetBundleLoader instance;

	// Token: 0x020009D5 RID: 2517
	public class CachedFileInfo
	{
		// Token: 0x04003845 RID: 14405
		public string name;

		// Token: 0x04003846 RID: 14406
		public int version;

		// Token: 0x04003847 RID: 14407
		public uint crc;

		// Token: 0x04003848 RID: 14408
		public bool downloaded;
	}

	// Token: 0x020009D6 RID: 2518
	public class LoadingDataInfo
	{
		// Token: 0x04003849 RID: 14409
		public string name;

		// Token: 0x0400384A RID: 14410
		public string filePath;

		// Token: 0x0400384B RID: 14411
		public string fullPath;

		// Token: 0x0400384C RID: 14412
		public bool cashed;

		// Token: 0x0400384D RID: 14413
		public AssetBundleLoader.CachedFileInfo cashedInfo;

		// Token: 0x0400384E RID: 14414
		public GameObject returnObject;
	}

	// Token: 0x020009D7 RID: 2519
	private enum Mode
	{
		// Token: 0x04003850 RID: 14416
		Non,
		// Token: 0x04003851 RID: 14417
		DownloadList,
		// Token: 0x04003852 RID: 14418
		EnableLoad
	}
}
