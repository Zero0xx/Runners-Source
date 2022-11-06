using System;
using UnityEngine;

// Token: 0x02000499 RID: 1177
public class OptionMenuUtility
{
	// Token: 0x0600230B RID: 8971 RVA: 0x000D2904 File Offset: 0x000D0B04
	public static GameObject CreateSceneLoader(string sceneName)
	{
		GameObject gameObject = new GameObject("SceneLoader");
		if (gameObject != null)
		{
			ResourceSceneLoader resourceSceneLoader = gameObject.AddComponent<ResourceSceneLoader>();
			if (resourceSceneLoader != null)
			{
				bool onAssetBundle = true;
				resourceSceneLoader.AddLoad(sceneName, onAssetBundle, false);
			}
		}
		return gameObject;
	}

	// Token: 0x0600230C RID: 8972 RVA: 0x000D2948 File Offset: 0x000D0B48
	public static GameObject CreateSceneLoader(string sceneName, bool assetBundleFlag)
	{
		GameObject gameObject = new GameObject("SceneLoader");
		if (gameObject != null)
		{
			ResourceSceneLoader resourceSceneLoader = gameObject.AddComponent<ResourceSceneLoader>();
			if (resourceSceneLoader != null)
			{
				resourceSceneLoader.AddLoad(sceneName, assetBundleFlag, false);
			}
		}
		return gameObject;
	}

	// Token: 0x0600230D RID: 8973 RVA: 0x000D298C File Offset: 0x000D0B8C
	public static void DestroySceneLoader(GameObject obj)
	{
		UnityEngine.Object.Destroy(obj);
		obj = null;
	}

	// Token: 0x0600230E RID: 8974 RVA: 0x000D2998 File Offset: 0x000D0B98
	public static bool CheckSceneLoad(GameObject obj)
	{
		if (obj != null)
		{
			ResourceSceneLoader component = obj.GetComponent<ResourceSceneLoader>();
			if (component != null)
			{
				return component.Loaded;
			}
		}
		return true;
	}

	// Token: 0x0600230F RID: 8975 RVA: 0x000D29CC File Offset: 0x000D0BCC
	public static void TranObj(GameObject obj)
	{
		if (obj != null)
		{
			GameObject gameObject = GameObject.Find("UI Root (2D)/Camera/Anchor_5_MC");
			if (gameObject != null)
			{
				Transform transform = obj.transform;
				Transform transform2 = gameObject.transform;
				if (transform != null && transform2 != null)
				{
					Vector3 localScale = transform.transform.localScale;
					transform.parent = transform2;
					transform.transform.localPosition = new Vector3(0f, 0f, 0f);
					transform.transform.localScale = localScale;
				}
			}
		}
	}

	// Token: 0x04001FB4 RID: 8116
	private const string ATTACH_ANTHOR_NAME = "UI Root (2D)/Camera/Anchor_5_MC";
}
