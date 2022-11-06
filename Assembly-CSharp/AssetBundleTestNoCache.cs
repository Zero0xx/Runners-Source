using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000A1E RID: 2590
public class AssetBundleTestNoCache : MonoBehaviour
{
	// Token: 0x060044AA RID: 17578 RVA: 0x0016183C File Offset: 0x0015FA3C
	private void Start()
	{
		WWW www = new WWW("http://web2/HikiData/Sonic_Runners/Soft/Asset/AssetBundles_Win/PrephabKnuckles.unity3d");
		base.StartCoroutine(this.WaitLoard(www));
	}

	// Token: 0x060044AB RID: 17579 RVA: 0x00161864 File Offset: 0x0015FA64
	private void Update()
	{
	}

	// Token: 0x060044AC RID: 17580 RVA: 0x00161868 File Offset: 0x0015FA68
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
