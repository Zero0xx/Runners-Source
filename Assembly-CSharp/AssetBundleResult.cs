using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020009D9 RID: 2521
public class AssetBundleResult
{
	// Token: 0x06004236 RID: 16950 RVA: 0x00159124 File Offset: 0x00157324
	public AssetBundleResult(string path, AssetBundle assetBundle, string err)
	{
		this.Initialize(path, assetBundle, null, null, null, err);
		this.mAbAsyncLoaderCallback = new List<AsyncLoadedObjectCallback>(2);
	}

	// Token: 0x06004237 RID: 16951 RVA: 0x00159150 File Offset: 0x00157350
	public AssetBundleResult(string path, AssetBundle assetBundle, Texture2D texture, string err)
	{
		this.Initialize(path, assetBundle, texture, null, null, err);
	}

	// Token: 0x06004238 RID: 16952 RVA: 0x00159170 File Offset: 0x00157370
	public AssetBundleResult(string path, AssetBundle assetBundle, TextAsset textAsset, string err)
	{
		this.Initialize(path, assetBundle, null, textAsset.text, null, err);
	}

	// Token: 0x06004239 RID: 16953 RVA: 0x00159198 File Offset: 0x00157398
	public AssetBundleResult(string path, AssetBundle assetBundle, string text, string err)
	{
		this.Initialize(path, assetBundle, null, text, null, err);
	}

	// Token: 0x0600423A RID: 16954 RVA: 0x001591B8 File Offset: 0x001573B8
	public AssetBundleResult(string path, byte[] bytes, string err)
	{
		this.Initialize(path, null, null, null, bytes, err);
	}

	// Token: 0x170008FC RID: 2300
	// (get) Token: 0x0600423B RID: 16955 RVA: 0x001591D8 File Offset: 0x001573D8
	public Texture2D Texture
	{
		get
		{
			return this.mTexture;
		}
	}

	// Token: 0x170008FD RID: 2301
	// (get) Token: 0x0600423C RID: 16956 RVA: 0x001591E0 File Offset: 0x001573E0
	public string Text
	{
		get
		{
			return this.mText;
		}
	}

	// Token: 0x170008FE RID: 2302
	// (get) Token: 0x0600423D RID: 16957 RVA: 0x001591E8 File Offset: 0x001573E8
	public byte[] bytes
	{
		get
		{
			return this.mBytes;
		}
	}

	// Token: 0x170008FF RID: 2303
	// (get) Token: 0x0600423E RID: 16958 RVA: 0x001591F0 File Offset: 0x001573F0
	public string Error
	{
		get
		{
			return this.mError;
		}
	}

	// Token: 0x17000900 RID: 2304
	// (get) Token: 0x0600423F RID: 16959 RVA: 0x001591F8 File Offset: 0x001573F8
	public string Path
	{
		get
		{
			return this.mPath;
		}
	}

	// Token: 0x17000901 RID: 2305
	// (get) Token: 0x06004240 RID: 16960 RVA: 0x00159200 File Offset: 0x00157400
	public string Name
	{
		get
		{
			return this.mName;
		}
	}

	// Token: 0x17000902 RID: 2306
	// (get) Token: 0x06004241 RID: 16961 RVA: 0x00159208 File Offset: 0x00157408
	// (set) Token: 0x06004242 RID: 16962 RVA: 0x00159210 File Offset: 0x00157410
	public bool isValid
	{
		get
		{
			return this.mIsValid;
		}
		set
		{
			this.mIsValid = value;
		}
	}

	// Token: 0x06004243 RID: 16963 RVA: 0x0015921C File Offset: 0x0015741C
	public void Initialize(string path, AssetBundle assetBundle, Texture2D texture, string text, byte[] bytes, string err)
	{
		this.mAssetBundle = assetBundle;
		this.mAbAsyncLoader = null;
		this.mAbAsyncLoaderCallback = null;
		this.mObjects = null;
		this.mTexture = texture;
		this.mText = text;
		this.mBytes = bytes;
		this.mError = err;
		this.mPath = path;
		this.mName = System.IO.Path.GetFileNameWithoutExtension(path);
		this.mIsValid = true;
	}

	// Token: 0x06004244 RID: 16964 RVA: 0x00159280 File Offset: 0x00157480
	public GameObject LoadObject(string objectName)
	{
		if (null == this.mObjects)
		{
			if (null == this.mAssetBundle)
			{
				global::Debug.LogError("AssetBundleResult : LoadObject : mAssetBundle is null");
			}
			else
			{
				this.mObjects = (this.mAssetBundle.Load(objectName, typeof(GameObject)) as GameObject);
				this.mAssetBundle.Unload(false);
				this.mAssetBundle = null;
			}
		}
		return this.mObjects;
	}

	// Token: 0x06004245 RID: 16965 RVA: 0x001592F8 File Offset: 0x001574F8
	public void LoadGameObjectAsync(string objectName, AsyncLoadedObjectCallback callback)
	{
		if (null == this.mObjects)
		{
			if (null == this.mAssetBundle)
			{
				global::Debug.LogError("AssetBundleResult : LoadObject : mAssetBundle is null");
			}
			else
			{
				this.mAbAsyncLoaderCallback.Add(callback);
				if (null == this.mAbAsyncLoader)
				{
					GameObject gameObject = new GameObject("async load object");
					this.mAbAsyncLoader = gameObject.AddComponent<AssetBundleAsyncObjectLoader>();
					this.mAbAsyncLoader.assetBundleRequest = this.mAssetBundle.LoadAsync(objectName, typeof(GameObject));
					this.mAbAsyncLoader.asyncLoadedCallback = new AsyncLoadedObjectCallback(this.AsyncLoadCallback);
				}
			}
		}
		else if (callback != null)
		{
			callback(this.mObjects);
		}
	}

	// Token: 0x06004246 RID: 16966 RVA: 0x001593BC File Offset: 0x001575BC
	private void AsyncLoadCallback(UnityEngine.Object loadedObject)
	{
		this.mObjects = (loadedObject as GameObject);
		int count = this.mAbAsyncLoaderCallback.Count;
		for (int i = 0; i < count; i++)
		{
			AsyncLoadedObjectCallback asyncLoadedObjectCallback = this.mAbAsyncLoaderCallback[i];
			if (asyncLoadedObjectCallback != null)
			{
				asyncLoadedObjectCallback(this.mObjects);
			}
		}
		this.mAbAsyncLoaderCallback.Clear();
		this.mAbAsyncLoaderCallback = null;
		this.mAssetBundle.Unload(false);
		this.mAssetBundle = null;
		this.mAbAsyncLoader = null;
	}

	// Token: 0x06004247 RID: 16967 RVA: 0x00159440 File Offset: 0x00157640
	public void Clear()
	{
		if (null != this.mAbAsyncLoader)
		{
			this.mAbAsyncLoader.asyncLoadedCallback = null;
		}
		bool flag = false;
		if (null != this.mAssetBundle)
		{
			flag = true;
			this.mAssetBundle.Unload(false);
		}
		if (null != this.mObjects)
		{
			UnityEngine.Object.DestroyImmediate(this.mObjects, true);
		}
		if (!flag && null != this.mTexture)
		{
			UnityEngine.Object.Destroy(this.mTexture);
		}
		this.mAbAsyncLoader = null;
		this.mAssetBundle = null;
		this.mObjects = null;
		this.mTexture = null;
		this.mText = null;
		this.mBytes = null;
		this.mPath = null;
		this.mError = null;
		this.mIsValid = false;
	}

	// Token: 0x04003856 RID: 14422
	private AssetBundle mAssetBundle;

	// Token: 0x04003857 RID: 14423
	private GameObject mObjects;

	// Token: 0x04003858 RID: 14424
	private AssetBundleAsyncObjectLoader mAbAsyncLoader;

	// Token: 0x04003859 RID: 14425
	private List<AsyncLoadedObjectCallback> mAbAsyncLoaderCallback;

	// Token: 0x0400385A RID: 14426
	private Texture2D mTexture;

	// Token: 0x0400385B RID: 14427
	private string mText;

	// Token: 0x0400385C RID: 14428
	private byte[] mBytes;

	// Token: 0x0400385D RID: 14429
	private string mPath;

	// Token: 0x0400385E RID: 14430
	private string mName;

	// Token: 0x0400385F RID: 14431
	public string mError;

	// Token: 0x04003860 RID: 14432
	private bool mIsValid;
}
