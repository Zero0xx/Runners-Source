using System;
using System.Collections;
using UnityEngine;

// Token: 0x020009D8 RID: 2520
public class AssetBundleAsyncObjectLoader : MonoBehaviour
{
	// Token: 0x170008FA RID: 2298
	// (get) Token: 0x0600422E RID: 16942 RVA: 0x001590AC File Offset: 0x001572AC
	// (set) Token: 0x0600422F RID: 16943 RVA: 0x001590B4 File Offset: 0x001572B4
	public AsyncLoadedObjectCallback asyncLoadedCallback
	{
		get
		{
			return this.mAsyncLoadedCallback;
		}
		set
		{
			this.mAsyncLoadedCallback = value;
		}
	}

	// Token: 0x170008FB RID: 2299
	// (get) Token: 0x06004230 RID: 16944 RVA: 0x001590C0 File Offset: 0x001572C0
	// (set) Token: 0x06004231 RID: 16945 RVA: 0x001590C8 File Offset: 0x001572C8
	public UnityEngine.AssetBundleRequest assetBundleRequest
	{
		get
		{
			return this.mAbRquest;
		}
		set
		{
			this.mAbRquest = value;
		}
	}

	// Token: 0x06004232 RID: 16946 RVA: 0x001590D4 File Offset: 0x001572D4
	private void Awake()
	{
		this.mLoading = true;
	}

	// Token: 0x06004233 RID: 16947 RVA: 0x001590E0 File Offset: 0x001572E0
	private void Start()
	{
		base.StartCoroutine("Watch");
	}

	// Token: 0x06004234 RID: 16948 RVA: 0x001590F0 File Offset: 0x001572F0
	private void Update()
	{
		if (!this.mLoading)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06004235 RID: 16949 RVA: 0x00159108 File Offset: 0x00157308
	private IEnumerator Watch()
	{
		yield return this.mAbRquest;
		this.mAsyncLoadedCallback(this.mAbRquest.asset);
		this.mLoading = false;
		yield break;
	}

	// Token: 0x04003853 RID: 14419
	private UnityEngine.AssetBundleRequest mAbRquest;

	// Token: 0x04003854 RID: 14420
	private AsyncLoadedObjectCallback mAsyncLoadedCallback;

	// Token: 0x04003855 RID: 14421
	private bool mLoading;
}
