using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000A1D RID: 2589
public class AssetBundleTest : MonoBehaviour
{
	// Token: 0x060044A6 RID: 17574 RVA: 0x001617E4 File Offset: 0x0015F9E4
	private void Start()
	{
		WWW www = WWW.LoadFromCacheOrDownload("http://web2/HikiData/Sonic_Runners/Soft/Asset/AssetBundles_Win/PrephabKnuckles.unity3d", 5);
		base.StartCoroutine(this.WaitLoard(www));
	}

	// Token: 0x060044A7 RID: 17575 RVA: 0x0016180C File Offset: 0x0015FA0C
	private void Update()
	{
	}

	// Token: 0x060044A8 RID: 17576 RVA: 0x00161810 File Offset: 0x0015FA10
	private IEnumerator WaitLoard(WWW www)
	{
		while (!www.isDone)
		{
			yield return null;
		}
		if (www.error != null)
		{
			global::Debug.LogError(www.error);
		}
		else
		{
			AssetBundle myLoadedAssetBundle = www.assetBundle;
			UnityEngine.Object asset = myLoadedAssetBundle.mainAsset;
			UnityEngine.Object.Instantiate(asset);
			myLoadedAssetBundle.Unload(false);
		}
		yield break;
	}
}
