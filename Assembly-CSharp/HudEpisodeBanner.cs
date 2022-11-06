using System;
using UnityEngine;

// Token: 0x0200045F RID: 1119
public class HudEpisodeBanner
{
	// Token: 0x060021A6 RID: 8614 RVA: 0x000CA7DC File Offset: 0x000C89DC
	public void Initialize(GameObject mainMenuObject)
	{
		if (mainMenuObject == null)
		{
			return;
		}
		this.m_mainMenuObject = mainMenuObject;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_mainMenuObject, "Anchor_5_MC");
		if (gameObject == null)
		{
			return;
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "0_Endless");
		if (gameObject2 == null)
		{
			return;
		}
		GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject2, "Btn_2_mileage");
		if (gameObject3 == null)
		{
			return;
		}
		UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject3, "img_tex_ep");
		if (uitexture == null)
		{
			return;
		}
		TextureAsyncLoadManager instance = TextureAsyncLoadManager.Instance;
		if (instance == null)
		{
			return;
		}
		int bannerCount = TextureRequestEpisodeBanner.BannerCount;
		for (int i = 0; i < bannerCount; i++)
		{
			TextureRequestEpisodeBanner request = new TextureRequestEpisodeBanner(i, uitexture);
			if (instance.IsLoaded(request))
			{
				this.m_bannerId = i;
				instance.Request(request);
			}
		}
	}

	// Token: 0x060021A7 RID: 8615 RVA: 0x000CA8D0 File Offset: 0x000C8AD0
	public void UpdateView()
	{
	}

	// Token: 0x04001E61 RID: 7777
	private GameObject m_mainMenuObject;

	// Token: 0x04001E62 RID: 7778
	private int m_bannerId;
}
