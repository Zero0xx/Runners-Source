using System;
using System.IO;
using Message;
using UnityEngine;

// Token: 0x020009DB RID: 2523
public class AssetBundleRequest
{
	// Token: 0x06004250 RID: 16976 RVA: 0x001595C8 File Offset: 0x001577C8
	public AssetBundleRequest(string path, int version, uint crc, global::AssetBundleRequest.Type type, GameObject returnObject) : this(path, version, crc, type, returnObject, false)
	{
	}

	// Token: 0x06004251 RID: 16977 RVA: 0x001595D8 File Offset: 0x001577D8
	public AssetBundleRequest(string path, int version, uint crc, global::AssetBundleRequest.Type type, GameObject returnObject, bool useCache)
	{
		this.mMaxTryCount = 1;
		this.mTimeOut = global::AssetBundleRequest.DefaultTimeOut;
		base..ctor();
		this.mUseCache = useCache;
		this.mState = global::AssetBundleRequest.State.INVALID;
		this.mPath = path;
		this.mFileName = Path.GetFileNameWithoutExtension(path);
		this.mReturnObject = returnObject;
		this.mWWW = null;
		this.mVersion = version;
		this.mCRC = crc;
		this.mType = type;
		this.mTryCount = 0;
		this.mCancel = false;
		this.mAssetbundleResult = null;
		this.mIsLoaded = false;
	}

	// Token: 0x06004252 RID: 16978 RVA: 0x00159660 File Offset: 0x00157860
	public AssetBundleRequest(global::AssetBundleRequest request)
	{
		this.mMaxTryCount = 1;
		this.mTimeOut = global::AssetBundleRequest.DefaultTimeOut;
		base..ctor();
		this.mUseCache = request.useCache;
		this.mState = global::AssetBundleRequest.State.INVALID;
		this.mPath = request.path;
		this.mFileName = request.mFileName;
		this.mReturnObject = request.returnObject;
		this.mWWW = null;
		this.mVersion = request.version;
		this.mCRC = request.crc;
		this.mType = request.type;
		this.mTryCount = 0;
		this.mCancel = false;
		this.mAssetbundleResult = null;
		this.mIsLoaded = false;
	}

	// Token: 0x17000904 RID: 2308
	// (get) Token: 0x06004254 RID: 16980 RVA: 0x00159710 File Offset: 0x00157910
	public bool useCache
	{
		get
		{
			return this.mUseCache;
		}
	}

	// Token: 0x17000905 RID: 2309
	// (get) Token: 0x06004255 RID: 16981 RVA: 0x00159718 File Offset: 0x00157918
	public bool isCancel
	{
		get
		{
			return this.mCancel;
		}
	}

	// Token: 0x17000906 RID: 2310
	// (get) Token: 0x06004256 RID: 16982 RVA: 0x00159720 File Offset: 0x00157920
	public WWW www
	{
		get
		{
			return this.mWWW;
		}
	}

	// Token: 0x17000907 RID: 2311
	// (get) Token: 0x06004257 RID: 16983 RVA: 0x00159728 File Offset: 0x00157928
	public int version
	{
		get
		{
			return this.mVersion;
		}
	}

	// Token: 0x17000908 RID: 2312
	// (get) Token: 0x06004258 RID: 16984 RVA: 0x00159730 File Offset: 0x00157930
	public uint crc
	{
		get
		{
			return this.mCRC;
		}
	}

	// Token: 0x17000909 RID: 2313
	// (get) Token: 0x06004259 RID: 16985 RVA: 0x00159738 File Offset: 0x00157938
	public global::AssetBundleRequest.Type type
	{
		get
		{
			return this.mType;
		}
	}

	// Token: 0x1700090A RID: 2314
	// (get) Token: 0x0600425A RID: 16986 RVA: 0x00159740 File Offset: 0x00157940
	public string Url
	{
		get
		{
			return this.mURL;
		}
	}

	// Token: 0x1700090B RID: 2315
	// (get) Token: 0x0600425B RID: 16987 RVA: 0x00159748 File Offset: 0x00157948
	public Texture2D Texture
	{
		get
		{
			return this.mAssetbundleResult.Texture;
		}
	}

	// Token: 0x1700090C RID: 2316
	// (get) Token: 0x0600425C RID: 16988 RVA: 0x00159758 File Offset: 0x00157958
	public string path
	{
		get
		{
			return this.mPath;
		}
	}

	// Token: 0x1700090D RID: 2317
	// (get) Token: 0x0600425D RID: 16989 RVA: 0x00159760 File Offset: 0x00157960
	public string FileName
	{
		get
		{
			return this.mFileName;
		}
	}

	// Token: 0x1700090E RID: 2318
	// (get) Token: 0x0600425E RID: 16990 RVA: 0x00159768 File Offset: 0x00157968
	public AssetBundleResult assetbundleResult
	{
		get
		{
			return this.mAssetbundleResult;
		}
	}

	// Token: 0x1700090F RID: 2319
	// (get) Token: 0x0600425F RID: 16991 RVA: 0x00159770 File Offset: 0x00157970
	public GameObject returnObject
	{
		get
		{
			return this.mReturnObject;
		}
	}

	// Token: 0x17000910 RID: 2320
	// (get) Token: 0x06004260 RID: 16992 RVA: 0x00159778 File Offset: 0x00157978
	public bool IsTypeTexture
	{
		get
		{
			return this.mType == global::AssetBundleRequest.Type.TEXTURE;
		}
	}

	// Token: 0x17000911 RID: 2321
	// (get) Token: 0x06004261 RID: 16993 RVA: 0x00159790 File Offset: 0x00157990
	// (set) Token: 0x06004262 RID: 16994 RVA: 0x00159798 File Offset: 0x00157998
	public float TimeOut
	{
		get
		{
			return this.mTimeOut;
		}
		set
		{
			if (value > global::AssetBundleRequest.DefaultTimeOut * 3f)
			{
				value = global::AssetBundleRequest.DefaultTimeOut * 3f;
			}
			this.mTimeOut = value;
		}
	}

	// Token: 0x17000912 RID: 2322
	// (get) Token: 0x06004263 RID: 16995 RVA: 0x001597C0 File Offset: 0x001579C0
	public int TryCount
	{
		get
		{
			return this.mTryCount;
		}
	}

	// Token: 0x17000913 RID: 2323
	// (get) Token: 0x06004264 RID: 16996 RVA: 0x001597C8 File Offset: 0x001579C8
	public int MaxTryCount
	{
		get
		{
			return this.mMaxTryCount;
		}
	}

	// Token: 0x06004265 RID: 16997 RVA: 0x001597D0 File Offset: 0x001579D0
	public void Load()
	{
		if (this.IsExecuting())
		{
			return;
		}
		if (this.mDownloaderObject == null)
		{
			global::Debug.Log("AssetBundleRequest:" + this.mFileName);
			this.mDownloaderObject = new GameObject("AssetBundleAsyncDownloader");
			AssetBundleAsyncDownloader assetBundleAsyncDownloader = this.mDownloaderObject.AddComponent<AssetBundleAsyncDownloader>();
			assetBundleAsyncDownloader.SetBundleRequest(this);
			assetBundleAsyncDownloader.asyncLoadedCallback = new AsyncDownloadCallback(this.LoadedCallback);
			this.mState = global::AssetBundleRequest.State.EXECUTING;
			this.mElapsedTime = 0f;
		}
	}

	// Token: 0x06004266 RID: 16998 RVA: 0x00159858 File Offset: 0x00157A58
	public void Cancel()
	{
		this.mCancel = true;
	}

	// Token: 0x06004267 RID: 16999 RVA: 0x00159864 File Offset: 0x00157A64
	public void Unload()
	{
		if (!AssetBundleManager.Instance.IsCancelRequest() && this.IsExecuting())
		{
			global::Debug.LogWarning("AssetBundleRequest.Unload : Now executing...");
			return;
		}
		if (this.mAssetbundleResult != null)
		{
			this.mAssetbundleResult.Clear();
			this.mAssetbundleResult = null;
		}
		if (this.mDownloaderObject != null)
		{
			UnityEngine.Object.Destroy(this.mDownloaderObject);
			this.mDownloaderObject = null;
		}
		if (this.mWWW != null)
		{
			this.mWWW.Dispose();
			this.mWWW = null;
		}
	}

	// Token: 0x06004268 RID: 17000 RVA: 0x001598F4 File Offset: 0x00157AF4
	public bool IsInvalid()
	{
		return this.mState == global::AssetBundleRequest.State.INVALID;
	}

	// Token: 0x06004269 RID: 17001 RVA: 0x00159908 File Offset: 0x00157B08
	public bool IsRetry()
	{
		return this.mState == global::AssetBundleRequest.State.RETRY;
	}

	// Token: 0x0600426A RID: 17002 RVA: 0x0015991C File Offset: 0x00157B1C
	public bool IsSucceeded()
	{
		return this.mState == global::AssetBundleRequest.State.SUCCEEDED;
	}

	// Token: 0x0600426B RID: 17003 RVA: 0x00159930 File Offset: 0x00157B30
	public bool IsFailed()
	{
		return this.mState == global::AssetBundleRequest.State.FAILED;
	}

	// Token: 0x0600426C RID: 17004 RVA: 0x00159944 File Offset: 0x00157B44
	public bool IsExecuting()
	{
		return this.mState == global::AssetBundleRequest.State.EXECUTING;
	}

	// Token: 0x0600426D RID: 17005 RVA: 0x00159954 File Offset: 0x00157B54
	private void LoadedCallback(WWW www)
	{
		this.mWWW = www;
		this.mURL = this.mWWW.url;
		UnityEngine.Object.Destroy(this.mDownloaderObject);
		this.mDownloaderObject = null;
	}

	// Token: 0x0600426E RID: 17006 RVA: 0x0015998C File Offset: 0x00157B8C
	public bool IsLoaded()
	{
		if (this.mDownloaderObject != null)
		{
			return false;
		}
		if (this.mWWW == null)
		{
			return this.mIsLoaded;
		}
		return this.mWWW.isDone;
	}

	// Token: 0x0600426F RID: 17007 RVA: 0x001599CC File Offset: 0x00157BCC
	public bool IsLoading()
	{
		if (this.mDownloaderObject != null)
		{
			return true;
		}
		if (this.mWWW == null)
		{
			return !this.mIsLoaded;
		}
		return !this.mWWW.isDone;
	}

	// Token: 0x06004270 RID: 17008 RVA: 0x00159A10 File Offset: 0x00157C10
	public bool IsTimeOut()
	{
		return this.mTimeOut <= this.mElapsedTime;
	}

	// Token: 0x06004271 RID: 17009 RVA: 0x00159A28 File Offset: 0x00157C28
	public float UpdateElapsedTime(float addElapsedTime)
	{
		this.mElapsedTime += addElapsedTime;
		return 0.1f;
	}

	// Token: 0x06004272 RID: 17010 RVA: 0x00159A40 File Offset: 0x00157C40
	private bool IsErrorTexture()
	{
		if (this.mAssetbundleResult == null)
		{
			return false;
		}
		Texture2D texture = this.mAssetbundleResult.Texture;
		return !(null == texture) && !(string.Empty != texture.name) && texture.height == 8 && texture.width == 8 && texture.filterMode == FilterMode.Bilinear && texture.anisoLevel == 1 && texture.wrapMode == TextureWrapMode.Repeat && texture.mipMapBias == 0f;
	}

	// Token: 0x06004273 RID: 17011 RVA: 0x00159AE4 File Offset: 0x00157CE4
	public Texture2D MakeTexture()
	{
		Texture2D result = null;
		if (this.mType == global::AssetBundleRequest.Type.TEXTURE && this.mWWW.error == null)
		{
			result = this.mWWW.texture;
		}
		return result;
	}

	// Token: 0x06004274 RID: 17012 RVA: 0x00159B1C File Offset: 0x00157D1C
	public string MakeText()
	{
		string result = null;
		if (this.mType == global::AssetBundleRequest.Type.TEXT && this.mWWW.error == null)
		{
			result = this.mWWW.text;
		}
		return result;
	}

	// Token: 0x06004275 RID: 17013 RVA: 0x00159B54 File Offset: 0x00157D54
	public void Result()
	{
		try
		{
			if (this.mCancel)
			{
				global::Debug.LogWarning("!!! AssetBundleRequest Cancel : " + this.mPath, DebugTraceManager.TraceType.ASSETBUNDLE);
				if (this.mWWW != null)
				{
					this.mWWW.Dispose();
					this.mWWW = null;
				}
				this.mState = global::AssetBundleRequest.State.FAILED;
			}
			else
			{
				bool flag = false;
				if (this.IsTimeOut())
				{
					global::Debug.Log("AssetBundle TimeOut : " + this.mPath, DebugTraceManager.TraceType.ASSETBUNDLE);
					flag = true;
				}
				else
				{
					if (this.mWWW == null)
					{
						return;
					}
					if (this.mUseCache)
					{
					}
					if (!this.mWWW.isDone)
					{
						return;
					}
				}
				bool flag2 = true;
				bool flag3 = false;
				if (this.mWWW != null && this.mWWW.error != null && !this.mWWW.error.Contains("Cannot load cached AssetBundle"))
				{
					flag3 = true;
				}
				if (flag3 || flag)
				{
					global::Debug.Log("!!!!! AssetBundle.Result : Error : " + ((this.mWWW != null) ? this.mWWW.error : "null"), DebugTraceManager.TraceType.ASSETBUNDLE);
					this.mTryCount++;
					if (this.mMaxTryCount > this.mTryCount)
					{
						global::Debug.LogWarning(string.Concat(new object[]
						{
							"AssetBundle.Result : Retry[",
							this.mTryCount,
							"/",
							this.mMaxTryCount,
							"]"
						}), DebugTraceManager.TraceType.ASSETBUNDLE);
						this.mState = global::AssetBundleRequest.State.RETRY;
						if (this.mWWW != null)
						{
							this.mWWW.Dispose();
							this.mWWW = null;
						}
						return;
					}
					global::Debug.LogWarning("AssetBundle.Result : Failed", DebugTraceManager.TraceType.ASSETBUNDLE);
					this.mState = global::AssetBundleRequest.State.FAILED;
					flag2 = false;
				}
				if (flag2)
				{
					this.mAssetbundleResult = this.CreateResult();
					this.mState = global::AssetBundleRequest.State.SUCCEEDED;
					global::Debug.Log("AssetBundle.Result : Success : " + this.mFileName);
					if (this.mReturnObject != null)
					{
						MsgAssetBundleResponseSucceed value = new MsgAssetBundleResponseSucceed(this, this.mAssetbundleResult);
						this.mReturnObject.SendMessage("AssetBundleResponseSucceed", value, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					this.mState = global::AssetBundleRequest.State.FAILED;
					global::Debug.LogWarning("!!!!! AssetBundle.Result : Failure : " + ((this.mWWW == null) ? "-----" : this.mWWW.error), DebugTraceManager.TraceType.ASSETBUNDLE);
					if (this.mReturnObject != null)
					{
						MsgAssetBundleResponseFailed value2 = new MsgAssetBundleResponseFailed(this, this.mAssetbundleResult);
						this.mReturnObject.SendMessage("AssetBundleResponseFailed", value2, SendMessageOptions.DontRequireReceiver);
					}
				}
				if (this.mWWW != null)
				{
					this.mWWW.Dispose();
					this.mWWW = null;
				}
				this.mIsLoaded = true;
			}
		}
		catch (Exception ex)
		{
			global::Debug.Log("AssetBundleRequest.Result() Exception:Message = " + ex.Message + "ToString() = " + ex.ToString());
		}
	}

	// Token: 0x06004276 RID: 17014 RVA: 0x00159E74 File Offset: 0x00158074
	public AssetBundleResult CreateResult()
	{
		AssetBundleResult result = null;
		if (this.mWWW == null)
		{
			byte[] bytes = null;
			string empty = string.Empty;
			result = new AssetBundleResult(this.mPath, bytes, empty);
			return result;
		}
		try
		{
			string text = (this.mWWW.error == null) ? null : (this.mWWW.error.Clone() as string);
			AssetBundle assetBundle = (text != null) ? null : this.mWWW.assetBundle;
			switch (this.mType)
			{
			case global::AssetBundleRequest.Type.GAMEOBJECT:
				result = new AssetBundleResult(this.mPath, assetBundle, text);
				break;
			case global::AssetBundleRequest.Type.TEXTURE:
			{
				Texture2D texture;
				if (assetBundle != null)
				{
					texture = (assetBundle.mainAsset as Texture2D);
				}
				else
				{
					texture = this.MakeTexture();
				}
				result = new AssetBundleResult(this.mPath, assetBundle, texture, text);
				break;
			}
			case global::AssetBundleRequest.Type.TEXT:
			{
				string text2 = null;
				if (assetBundle != null)
				{
					TextAsset textAsset = assetBundle.mainAsset as TextAsset;
					if (textAsset)
					{
						text2 = textAsset.text;
					}
				}
				else
				{
					text2 = this.MakeText();
				}
				result = new AssetBundleResult(this.mPath, assetBundle, text2, text);
				break;
			}
			case global::AssetBundleRequest.Type.SCENE:
				result = new AssetBundleResult(this.mPath, assetBundle, text);
				break;
			default:
			{
				byte[] bytes2 = this.mWWW.bytes.Clone() as byte[];
				result = new AssetBundleResult(this.mPath, bytes2, text);
				break;
			}
			}
		}
		catch (Exception ex)
		{
			global::Debug.Log("AssetBundleManager.CreateResult:Exception , Message = " + ex.Message + ", ToString() = " + ex.ToString());
		}
		return result;
	}

	// Token: 0x04003864 RID: 14436
	private bool mUseCache;

	// Token: 0x04003865 RID: 14437
	private global::AssetBundleRequest.State mState;

	// Token: 0x04003866 RID: 14438
	private string mPath;

	// Token: 0x04003867 RID: 14439
	private string mFileName;

	// Token: 0x04003868 RID: 14440
	private GameObject mReturnObject;

	// Token: 0x04003869 RID: 14441
	private WWW mWWW;

	// Token: 0x0400386A RID: 14442
	private int mVersion;

	// Token: 0x0400386B RID: 14443
	private uint mCRC;

	// Token: 0x0400386C RID: 14444
	private global::AssetBundleRequest.Type mType;

	// Token: 0x0400386D RID: 14445
	private string mURL;

	// Token: 0x0400386E RID: 14446
	private int mTryCount;

	// Token: 0x0400386F RID: 14447
	private int mMaxTryCount;

	// Token: 0x04003870 RID: 14448
	private bool mIsLoaded;

	// Token: 0x04003871 RID: 14449
	private AssetBundleResult mAssetbundleResult;

	// Token: 0x04003872 RID: 14450
	private GameObject mDownloaderObject;

	// Token: 0x04003873 RID: 14451
	public static readonly float DefaultTimeOut = 60f;

	// Token: 0x04003874 RID: 14452
	private float mTimeOut;

	// Token: 0x04003875 RID: 14453
	private float mElapsedTime;

	// Token: 0x04003876 RID: 14454
	private bool mCancel;

	// Token: 0x020009DC RID: 2524
	public enum Type
	{
		// Token: 0x04003878 RID: 14456
		GAMEOBJECT,
		// Token: 0x04003879 RID: 14457
		TEXTURE,
		// Token: 0x0400387A RID: 14458
		TEXT,
		// Token: 0x0400387B RID: 14459
		SCENE,
		// Token: 0x0400387C RID: 14460
		OTHER
	}

	// Token: 0x020009DD RID: 2525
	private enum State
	{
		// Token: 0x0400387E RID: 14462
		INVALID = -1,
		// Token: 0x0400387F RID: 14463
		EXECUTING,
		// Token: 0x04003880 RID: 14464
		SUCCEEDED,
		// Token: 0x04003881 RID: 14465
		FAILED,
		// Token: 0x04003882 RID: 14466
		RETRY
	}
}
