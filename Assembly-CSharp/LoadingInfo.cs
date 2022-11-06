using System;
using UnityEngine;

// Token: 0x020002D1 RID: 721
public class LoadingInfo : MonoBehaviour
{
	// Token: 0x0600147B RID: 5243 RVA: 0x0006D9FC File Offset: 0x0006BBFC
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x0600147C RID: 5244 RVA: 0x0006DA08 File Offset: 0x0006BC08
	public void SetInfo(LoadingInfo.LoadingData data)
	{
		this.m_loadingData = data;
	}

	// Token: 0x0600147D RID: 5245 RVA: 0x0006DA14 File Offset: 0x0006BC14
	public LoadingInfo.LoadingData GetInfo()
	{
		return this.m_loadingData;
	}

	// Token: 0x0600147E RID: 5246 RVA: 0x0006DA1C File Offset: 0x0006BC1C
	public void ResetData()
	{
		this.m_loadingData = new LoadingInfo.LoadingData();
	}

	// Token: 0x0600147F RID: 5247 RVA: 0x0006DA2C File Offset: 0x0006BC2C
	public static LoadingInfo CreateLoadingInfo()
	{
		LoadingInfo loadingInfo = null;
		LoadingInfo loadingInfo2 = GameObjectUtil.FindGameObjectComponent<LoadingInfo>("LoadingInfo");
		if (loadingInfo2 != null)
		{
			UnityEngine.Object.Destroy(loadingInfo2.gameObject);
		}
		GameObject gameObject = new GameObject("LoadingInfo");
		if (gameObject != null)
		{
			loadingInfo = gameObject.AddComponent<LoadingInfo>();
			loadingInfo.ResetData();
			UnityEngine.Object.DontDestroyOnLoad(loadingInfo.gameObject);
		}
		return loadingInfo;
	}

	// Token: 0x040011DA RID: 4570
	private LoadingInfo.LoadingData m_loadingData;

	// Token: 0x020002D2 RID: 722
	public class LoadingData
	{
		// Token: 0x040011DB RID: 4571
		public string m_titleText;

		// Token: 0x040011DC RID: 4572
		public string m_mainText;

		// Token: 0x040011DD RID: 4573
		public string m_bonusNameText;

		// Token: 0x040011DE RID: 4574
		public string m_bonusValueText;

		// Token: 0x040011DF RID: 4575
		public Texture m_texture;

		// Token: 0x040011E0 RID: 4576
		public bool m_optionTutorial;
	}
}
