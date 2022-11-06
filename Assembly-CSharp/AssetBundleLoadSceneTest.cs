using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000A1C RID: 2588
public class AssetBundleLoadSceneTest : MonoBehaviour
{
	// Token: 0x060044A2 RID: 17570 RVA: 0x0016175C File Offset: 0x0015F95C
	private void Start()
	{
		WWW www = WWW.LoadFromCacheOrDownload("http://web2/HikiData/Sonic_Runners/Soft/Asset/AssetBundles_Win/ResourcesCommonPrefabs.unity3d", 5);
		base.StartCoroutine(this.WaitLoard(www, "ResourcesCommonPrefabs"));
		WWW www2 = WWW.LoadFromCacheOrDownload("http://web2/HikiData/Sonic_Runners/Soft/Asset/AssetBundles_Win/ResourcesCommonObject.unity3d", 5);
		base.StartCoroutine(this.WaitLoard(www2, "ResourcesCommonObject"));
	}

	// Token: 0x060044A3 RID: 17571 RVA: 0x001617A8 File Offset: 0x0015F9A8
	private void Update()
	{
	}

	// Token: 0x060044A4 RID: 17572 RVA: 0x001617AC File Offset: 0x0015F9AC
	private IEnumerator WaitLoard(WWW www, string scenename)
	{
		while (www == null)
		{
			yield return null;
		}
		if (www.error != null || www.error != null)
		{
			global::Debug.LogError(www.error);
		}
		else
		{
			Application.LoadLevelAdditive(scenename);
		}
		yield break;
	}
}
