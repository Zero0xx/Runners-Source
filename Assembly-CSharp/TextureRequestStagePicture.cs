using System;
using UnityEngine;

// Token: 0x020000FD RID: 253
public class TextureRequestStagePicture : TextureRequest
{
	// Token: 0x0600077E RID: 1918 RVA: 0x0002B9A8 File Offset: 0x00029BA8
	public TextureRequestStagePicture(int stageIndex, UITexture uiTex)
	{
		if (stageIndex >= 1)
		{
			this.m_stageIndex = stageIndex;
			this.m_fileName = "ui_tex_mile_w" + this.m_stageIndex.ToString("D2") + "A";
			this.m_uiTex = uiTex;
		}
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x0002B9FC File Offset: 0x00029BFC
	public override void LoadDone(Texture tex)
	{
		if (this.m_uiTex == null)
		{
			return;
		}
		this.m_uiTex.mainTexture = tex;
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x0002BA1C File Offset: 0x00029C1C
	public override bool IsEnableLoad()
	{
		return !string.IsNullOrEmpty(this.GetFileName());
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x0002BA34 File Offset: 0x00029C34
	public override string GetFileName()
	{
		return this.m_fileName;
	}

	// Token: 0x040005B8 RID: 1464
	private int m_stageIndex = 1;

	// Token: 0x040005B9 RID: 1465
	private string m_fileName;

	// Token: 0x040005BA RID: 1466
	private UITexture m_uiTex;
}
