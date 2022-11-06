using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x020009DE RID: 2526
public class AssetBundleManager : MonoBehaviour
{
	// Token: 0x17000914 RID: 2324
	// (get) Token: 0x06004279 RID: 17017 RVA: 0x0015A088 File Offset: 0x00158288
	public int Count
	{
		get
		{
			return this.mAssetDic.Count;
		}
	}

	// Token: 0x17000915 RID: 2325
	// (get) Token: 0x0600427A RID: 17018 RVA: 0x0015A098 File Offset: 0x00158298
	public static AssetBundleManager Instance
	{
		get
		{
			return AssetBundleManager.mInstance;
		}
	}

	// Token: 0x17000916 RID: 2326
	// (get) Token: 0x0600427B RID: 17019 RVA: 0x0015A0A0 File Offset: 0x001582A0
	public int RequestCount
	{
		get
		{
			return this.mRequestList.Count;
		}
	}

	// Token: 0x17000917 RID: 2327
	// (get) Token: 0x0600427C RID: 17020 RVA: 0x0015A0B0 File Offset: 0x001582B0
	public bool Executing
	{
		get
		{
			return this.mExecuting;
		}
	}

	// Token: 0x0600427D RID: 17021 RVA: 0x0015A0B8 File Offset: 0x001582B8
	protected void Awake()
	{
		this.CheckInstance();
	}

	// Token: 0x0600427E RID: 17022 RVA: 0x0015A0C4 File Offset: 0x001582C4
	private void OnDestroy()
	{
		if (AssetBundleManager.mInstance == this)
		{
			AssetBundleManager.mInstance = null;
		}
	}

	// Token: 0x0600427F RID: 17023 RVA: 0x0015A0DC File Offset: 0x001582DC
	protected bool CheckInstance()
	{
		if (AssetBundleManager.mInstance == null)
		{
			AssetBundleManager.mInstance = this;
			return true;
		}
		if (this == AssetBundleManager.Instance)
		{
			return true;
		}
		UnityEngine.Object.Destroy(this);
		return false;
	}

	// Token: 0x06004280 RID: 17024 RVA: 0x0015A110 File Offset: 0x00158310
	public static void Create()
	{
		if (AssetBundleManager.mInstance == null)
		{
			GameObject gameObject = new GameObject("AssetBundleManager");
			gameObject.AddComponent<AssetBundleManager>();
			global::Debug.Log("AssetBundleManager.Create", DebugTraceManager.TraceType.ASSETBUNDLE);
		}
	}

	// Token: 0x06004281 RID: 17025 RVA: 0x0015A14C File Offset: 0x0015834C
	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	// Token: 0x06004282 RID: 17026 RVA: 0x0015A154 File Offset: 0x00158354
	private void Update()
	{
		if (this.mReqUnloadList.Count > 0)
		{
			foreach (string url in this.mReqUnloadList)
			{
				this.Unload(url);
			}
			this.mReqUnloadList.Clear();
		}
		if (this.mExecuting)
		{
			return;
		}
		int count = this.mRequestList.Count;
		if (0 < count)
		{
			this.mExecuteList.Clear();
			this.mRemainingList.Clear();
			for (int i = 0; i < count; i++)
			{
				this.mExecuteList.Add(this.mRequestList[i]);
				this.mRemainingList.Add(this.mRequestList[i]);
			}
			this.mRequestList.Clear();
			this.mExecuting = true;
			base.StartCoroutine("ExecuteRequest");
		}
	}

	// Token: 0x06004283 RID: 17027 RVA: 0x0015A26C File Offset: 0x0015846C
	public global::AssetBundleRequest RequestNoCache(string path, global::AssetBundleRequest.Type type, GameObject returnObject)
	{
		return this.Request(path, 0, 0U, type, returnObject, false);
	}

	// Token: 0x06004284 RID: 17028 RVA: 0x0015A27C File Offset: 0x0015847C
	public global::AssetBundleRequest Request(string path, int version, uint crc, global::AssetBundleRequest.Type type, GameObject returnObject, bool useCache)
	{
		if (this.IsCancelRequest())
		{
			return null;
		}
		global::AssetBundleRequest assetBundleRequest = null;
		if (this.mAssetDic.TryGetValue(path, out assetBundleRequest))
		{
			global::Debug.Log("AssetBundleManager.Request : already exist : " + path, DebugTraceManager.TraceType.ASSETBUNDLE);
			assetBundleRequest.Result();
			if (returnObject != null)
			{
				MsgAssetBundleResponseSucceed value = new MsgAssetBundleResponseSucceed(assetBundleRequest, assetBundleRequest.assetbundleResult);
				assetBundleRequest.returnObject.SendMessage("AssetBundleResponseSucceed", value, SendMessageOptions.DontRequireReceiver);
			}
			return assetBundleRequest;
		}
		assetBundleRequest = new global::AssetBundleRequest(path, version, crc, type, returnObject, useCache);
		assetBundleRequest.TimeOut = global::AssetBundleRequest.DefaultTimeOut;
		this.mAssetDic[assetBundleRequest.path] = assetBundleRequest;
		this.mRequestList.Add(assetBundleRequest);
		return assetBundleRequest;
	}

	// Token: 0x06004285 RID: 17029 RVA: 0x0015A32C File Offset: 0x0015852C
	public global::AssetBundleRequest ReRequest(global::AssetBundleRequest request)
	{
		if (this.IsCancelRequest())
		{
			return null;
		}
		global::AssetBundleRequest assetBundleRequest = new global::AssetBundleRequest(request);
		request.TimeOut += global::AssetBundleRequest.DefaultTimeOut;
		this.mAssetDic[request.path] = assetBundleRequest;
		this.mRequestList.Add(assetBundleRequest);
		return assetBundleRequest;
	}

	// Token: 0x06004286 RID: 17030 RVA: 0x0015A380 File Offset: 0x00158580
	public global::AssetBundleRequest GetResource(string path)
	{
		global::AssetBundleRequest result = null;
		if (this.mAssetDic.TryGetValue(path, out result))
		{
			return result;
		}
		return null;
	}

	// Token: 0x06004287 RID: 17031 RVA: 0x0015A3A8 File Offset: 0x001585A8
	public void Unload(string url)
	{
		global::AssetBundleRequest assetBundleRequest = null;
		bool flag = false;
		if (this.mAssetDic.TryGetValue(url, out assetBundleRequest))
		{
			assetBundleRequest.Unload();
			if (this.mAssetDic.Remove(url))
			{
				flag = true;
			}
		}
		if (flag)
		{
			global::Debug.Log("AssetBundleManager.Unload() : " + url, DebugTraceManager.TraceType.ASSETBUNDLE);
		}
		else
		{
			global::Debug.LogWarning("AssetBundleManager.Unload : But [" + url + "] is not exist", DebugTraceManager.TraceType.ASSETBUNDLE);
		}
	}

	// Token: 0x06004288 RID: 17032 RVA: 0x0015A418 File Offset: 0x00158618
	public void UnloadWithCancel(string url)
	{
		global::AssetBundleRequest assetBundleRequest = null;
		if (this.mAssetDic.TryGetValue(url, out assetBundleRequest))
		{
			if (assetBundleRequest.IsLoading())
			{
				assetBundleRequest.Cancel();
			}
			else
			{
				assetBundleRequest.Unload();
			}
			if (this.mAssetDic.Remove(url))
			{
			}
		}
	}

	// Token: 0x06004289 RID: 17033 RVA: 0x0015A474 File Offset: 0x00158674
	public void RemoveAll()
	{
		foreach (KeyValuePair<string, global::AssetBundleRequest> keyValuePair in this.mAssetDic)
		{
			keyValuePair.Value.Unload();
		}
		this.mAssetDic.Clear();
		global::Debug.Log("Remove all asset bundles", DebugTraceManager.TraceType.ASSETBUNDLE);
	}

	// Token: 0x0600428A RID: 17034 RVA: 0x0015A4F8 File Offset: 0x001586F8
	private IEnumerator ExecuteRequest()
	{
		int count = this.mExecuteList.Count;
		for (int i = 0; i < count; i++)
		{
			global::AssetBundleRequest req = this.mExecuteList[i];
			bool cancel = false;
			bool exec = true;
			if (req.isCancel)
			{
				exec = false;
			}
			else
			{
				req.Load();
			}
			while (exec)
			{
				float spendTime = 0f;
				while (!req.IsLoaded())
				{
					float waitTime = req.UpdateElapsedTime(spendTime);
					if (req.IsTimeOut())
					{
						global::Debug.Log("!!!!!!!!! AssetBundle TimeOut !!!!!!!!!", DebugTraceManager.TraceType.ASSETBUNDLE);
						break;
					}
					float startTime = Time.realtimeSinceStartup;
					do
					{
						yield return null;
						spendTime = Time.realtimeSinceStartup - startTime;
					}
					while (spendTime < waitTime);
				}
				if (this.mCancelState == AssetBundleManager.CancelState.CANCELING)
				{
					global::Debug.LogWarning("-------------- AssetBundleRequest.ExecuteRequest Cancel --------------", DebugTraceManager.TraceType.ASSETBUNDLE);
					this.mCancelState = AssetBundleManager.CancelState.CANCELED;
					this.mExecuting = false;
					exec = false;
					cancel = true;
				}
				else
				{
					req.Result();
					if (req.IsRetry())
					{
						yield return new WaitForSeconds(2f);
						global::Debug.LogWarning(string.Concat(new object[]
						{
							"!!!!!! Load Retry [",
							req.TryCount,
							"/",
							req.MaxTryCount,
							"] : ",
							req.Url
						}), DebugTraceManager.TraceType.ASSETBUNDLE);
						req.Load();
					}
					else
					{
						if (req.IsFailed())
						{
							this.mAssetDic.Remove(req.path);
						}
						exec = false;
						this.mRemainingList.Remove(req);
					}
				}
			}
			if (cancel)
			{
				break;
			}
		}
		this.mExecuteList.Clear();
		this.mRemainingList.Clear();
		this.mExecuting = false;
		this.ClearCancel();
		yield break;
	}

	// Token: 0x0600428B RID: 17035 RVA: 0x0015A514 File Offset: 0x00158714
	public bool IsAssetStandby(global::AssetBundleRequest request)
	{
		return request.assetbundleResult != null;
	}

	// Token: 0x0600428C RID: 17036 RVA: 0x0015A524 File Offset: 0x00158724
	public void RequestWaitAsset(global::AssetBundleRequest request)
	{
		if (this.IsAssetStandby(request))
		{
			AssetBundleResult assetbundleResult = request.assetbundleResult;
			if (request.returnObject != null)
			{
				MsgAssetBundleResponseSucceed value = new MsgAssetBundleResponseSucceed(request, assetbundleResult);
				request.returnObject.SendMessage("AssetBundleResponseSucceed", value, SendMessageOptions.DontRequireReceiver);
			}
		}
		else
		{
			base.StartCoroutine("WaitAsset", new AssetBundleManager.WaitInfo
			{
				mRequest = request
			});
		}
	}

	// Token: 0x0600428D RID: 17037 RVA: 0x0015A590 File Offset: 0x00158790
	private IEnumerator WaitAsset(AssetBundleManager.WaitInfo info)
	{
		while (!this.IsAssetStandby(info.mRequest))
		{
			yield return new WaitForSeconds(0.1f);
		}
		AssetBundleResult result = info.mRequest.CreateResult();
		if (info.mRequest.returnObject != null)
		{
			MsgAssetBundleResponseSucceed msg = new MsgAssetBundleResponseSucceed(info.mRequest, result);
			info.mRequest.returnObject.SendMessage("AssetBundleResponseSucceed", msg, SendMessageOptions.DontRequireReceiver);
		}
		yield break;
	}

	// Token: 0x0600428E RID: 17038 RVA: 0x0015A5BC File Offset: 0x001587BC
	public void Cancel()
	{
		if (0 >= this.mExecuteList.Count)
		{
			this.mCancelState = AssetBundleManager.CancelState.IDLE;
			return;
		}
		this.mCancelRequest = true;
		this.mCancelState = AssetBundleManager.CancelState.CANCELING;
	}

	// Token: 0x0600428F RID: 17039 RVA: 0x0015A5E8 File Offset: 0x001587E8
	public bool IsCanceled()
	{
		return this.mCancelState == AssetBundleManager.CancelState.IDLE || AssetBundleManager.CancelState.CANCELED == this.mCancelState;
	}

	// Token: 0x06004290 RID: 17040 RVA: 0x0015A600 File Offset: 0x00158800
	public bool IsCancelRequest()
	{
		return this.mCancelRequest;
	}

	// Token: 0x06004291 RID: 17041 RVA: 0x0015A608 File Offset: 0x00158808
	public void ClearCancel()
	{
		this.mCancelRequest = false;
		this.mCancelState = AssetBundleManager.CancelState.IDLE;
	}

	// Token: 0x06004292 RID: 17042 RVA: 0x0015A618 File Offset: 0x00158818
	public int GetRemainingCount()
	{
		if (this.mRemainingList == null)
		{
			return 0;
		}
		return this.mRemainingList.Count;
	}

	// Token: 0x06004293 RID: 17043 RVA: 0x0015A634 File Offset: 0x00158834
	public global::AssetBundleRequest GetRemainingRequest(int index)
	{
		if (this.mRemainingList != null && 0 <= index && this.mRemainingList.Count > index)
		{
			return this.mRemainingList[index];
		}
		return null;
	}

	// Token: 0x06004294 RID: 17044 RVA: 0x0015A668 File Offset: 0x00158868
	public void DebugPrintLoadedList()
	{
		string text = string.Empty;
		foreach (string str in this.mAssetDic.Keys)
		{
			text = text + str + "\n";
		}
		global::Debug.Log(this + ".DebugPrintLoadedList : \n" + text, DebugTraceManager.TraceType.ASSETBUNDLE);
	}

	// Token: 0x06004295 RID: 17045 RVA: 0x0015A6F4 File Offset: 0x001588F4
	public void RequestUnload(string url)
	{
		this.mReqUnloadList.Add(url);
	}

	// Token: 0x04003883 RID: 14467
	private Dictionary<string, global::AssetBundleRequest> mAssetDic = new Dictionary<string, global::AssetBundleRequest>();

	// Token: 0x04003884 RID: 14468
	private static AssetBundleManager mInstance;

	// Token: 0x04003885 RID: 14469
	private bool mExecuting;

	// Token: 0x04003886 RID: 14470
	private List<global::AssetBundleRequest> mRequestList = new List<global::AssetBundleRequest>();

	// Token: 0x04003887 RID: 14471
	private List<global::AssetBundleRequest> mExecuteList = new List<global::AssetBundleRequest>();

	// Token: 0x04003888 RID: 14472
	private List<global::AssetBundleRequest> mRemainingList = new List<global::AssetBundleRequest>();

	// Token: 0x04003889 RID: 14473
	private List<string> mReqUnloadList = new List<string>();

	// Token: 0x0400388A RID: 14474
	public int mAllLoadedAssetCount;

	// Token: 0x0400388B RID: 14475
	public int mLoadedTextureAssetCount;

	// Token: 0x0400388C RID: 14476
	private AssetBundleManager.CancelState mCancelState;

	// Token: 0x0400388D RID: 14477
	private bool mCancelRequest;

	// Token: 0x020009DF RID: 2527
	private class WaitInfo
	{
		// Token: 0x0400388E RID: 14478
		public global::AssetBundleRequest mRequest;
	}

	// Token: 0x020009E0 RID: 2528
	private enum CancelState
	{
		// Token: 0x04003890 RID: 14480
		IDLE,
		// Token: 0x04003891 RID: 14481
		CANCELING,
		// Token: 0x04003892 RID: 14482
		CANCELED
	}
}
