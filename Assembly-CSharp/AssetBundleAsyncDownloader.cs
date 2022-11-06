using System;
using System.Collections;
using UnityEngine;

// Token: 0x020009DA RID: 2522
public class AssetBundleAsyncDownloader : MonoBehaviour
{
	// Token: 0x17000903 RID: 2307
	// (get) Token: 0x06004249 RID: 16969 RVA: 0x00159514 File Offset: 0x00157714
	// (set) Token: 0x0600424A RID: 16970 RVA: 0x0015951C File Offset: 0x0015771C
	public AsyncDownloadCallback asyncLoadedCallback
	{
		get
		{
			return this.mAsyncDownloadCallback;
		}
		set
		{
			this.mAsyncDownloadCallback = value;
		}
	}

	// Token: 0x0600424B RID: 16971 RVA: 0x00159528 File Offset: 0x00157728
	private void Awake()
	{
	}

	// Token: 0x0600424C RID: 16972 RVA: 0x0015952C File Offset: 0x0015772C
	private void Start()
	{
		if (this.mAbRquest == null)
		{
			return;
		}
		try
		{
			base.StartCoroutine(this.Load());
		}
		catch (Exception ex)
		{
			global::Debug.Log("AssetBundleAsyncDownloader.Start() ExceptionMessage = " + ex.Message + "ToString() = " + ex.ToString());
		}
	}

	// Token: 0x0600424D RID: 16973 RVA: 0x0015959C File Offset: 0x0015779C
	public void SetBundleRequest(global::AssetBundleRequest request)
	{
		this.mAbRquest = request;
	}

	// Token: 0x0600424E RID: 16974 RVA: 0x001595A8 File Offset: 0x001577A8
	private void Update()
	{
	}

	// Token: 0x0600424F RID: 16975 RVA: 0x001595AC File Offset: 0x001577AC
	private IEnumerator Load()
	{
		WWW www = null;
		if (this.mAbRquest.useCache)
		{
			if (this.mAbRquest.crc > 0U)
			{
				www = WWW.LoadFromCacheOrDownload(this.mAbRquest.path, this.mAbRquest.version, this.mAbRquest.crc);
			}
			else
			{
				www = WWW.LoadFromCacheOrDownload(this.mAbRquest.path, this.mAbRquest.version);
			}
		}
		else
		{
			www = new WWW(this.mAbRquest.path);
		}
		yield return www;
		if (this.mAsyncDownloadCallback != null)
		{
			this.mAsyncDownloadCallback(www);
		}
		yield break;
	}

	// Token: 0x04003861 RID: 14433
	private global::AssetBundleRequest mAbRquest;

	// Token: 0x04003862 RID: 14434
	private AsyncDownloadCallback mAsyncDownloadCallback;

	// Token: 0x04003863 RID: 14435
	private bool mDownloading;
}
