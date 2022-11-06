using System;
using UnityEngine;

// Token: 0x020001F2 RID: 498
public class LoadURLComponent : MonoBehaviour
{
	// Token: 0x06000D51 RID: 3409 RVA: 0x0004E69C File Offset: 0x0004C89C
	private void Start()
	{
		if (LoadURLComponent.m_instance == null)
		{
			global::Debug.Log("LoadURLComponent:Created");
			LoadURLComponent.m_instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			this.Init();
		}
		else
		{
			global::Debug.Log("LoadURLComponent:Destroyed");
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x0004E6F0 File Offset: 0x0004C8F0
	private void OnDestroy()
	{
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x0004E6F4 File Offset: 0x0004C8F4
	private void Init()
	{
		DebugSaveServerUrl.LoadURL();
		global::Debug.Log("LoadURLComponent:LoadURL");
	}

	// Token: 0x04000B1F RID: 2847
	private static LoadURLComponent m_instance;
}
