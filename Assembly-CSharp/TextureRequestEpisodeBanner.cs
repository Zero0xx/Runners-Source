using System;
using UnityEngine;

// Token: 0x020000FC RID: 252
public class TextureRequestEpisodeBanner : TextureRequest
{
	// Token: 0x06000777 RID: 1911 RVA: 0x0002B918 File Offset: 0x00029B18
	public TextureRequestEpisodeBanner(int textureIndex, UITexture uiTex)
	{
		this.m_fileName = "ui_tex_mm_ep_" + (textureIndex + 1).ToString("D3");
		this.m_uiTex = uiTex;
	}

	// Token: 0x17000155 RID: 341
	// (get) Token: 0x06000779 RID: 1913 RVA: 0x0002B95C File Offset: 0x00029B5C
	// (set) Token: 0x0600077A RID: 1914 RVA: 0x0002B964 File Offset: 0x00029B64
	public static int BannerCount
	{
		get
		{
			return TextureRequestEpisodeBanner.s_BannerCount;
		}
		private set
		{
		}
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x0002B968 File Offset: 0x00029B68
	public override void LoadDone(Texture tex)
	{
		if (this.m_uiTex == null)
		{
			return;
		}
		this.m_uiTex.mainTexture = tex;
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x0002B988 File Offset: 0x00029B88
	public override bool IsEnableLoad()
	{
		return !string.IsNullOrEmpty(this.GetFileName());
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x0002B9A0 File Offset: 0x00029BA0
	public override string GetFileName()
	{
		return this.m_fileName;
	}

	// Token: 0x040005B5 RID: 1461
	private static readonly int s_BannerCount = 2;

	// Token: 0x040005B6 RID: 1462
	private string m_fileName;

	// Token: 0x040005B7 RID: 1463
	private UITexture m_uiTex;
}
