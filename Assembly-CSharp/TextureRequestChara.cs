using System;
using UnityEngine;

// Token: 0x020000FB RID: 251
public class TextureRequestChara : TextureRequest
{
	// Token: 0x06000772 RID: 1906 RVA: 0x0002B818 File Offset: 0x00029A18
	public TextureRequestChara(CharaType type, UITexture uiTex)
	{
		if (type != CharaType.UNKNOWN)
		{
			this.m_type = type;
			this.m_uiTex = uiTex;
			if (this.m_uiTex != null)
			{
				this.m_uiTex.SetTexture(TextureAsyncLoadManager.Instance.CharaDefaultTexture);
			}
			int num = (int)type;
			string[] prefixNameList = CharacterDataNameInfo.PrefixNameList;
			string str = prefixNameList[num];
			this.m_fileName = "ui_tex_player_" + num.ToString("D2") + "_" + str;
		}
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x0002B894 File Offset: 0x00029A94
	public static void RemoveAllCharaTexture()
	{
		TextureAsyncLoadManager instance = TextureAsyncLoadManager.Instance;
		if (instance == null)
		{
			return;
		}
		for (int i = 0; i < 29; i++)
		{
			CharaType type = (CharaType)i;
			TextureRequestChara request = new TextureRequestChara(type, null);
			instance.Remove(request);
		}
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x0002B8D8 File Offset: 0x00029AD8
	public override void LoadDone(Texture tex)
	{
		if (this.m_uiTex == null)
		{
			return;
		}
		this.m_uiTex.mainTexture = tex;
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x0002B8F8 File Offset: 0x00029AF8
	public override bool IsEnableLoad()
	{
		return !string.IsNullOrEmpty(this.GetFileName());
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x0002B910 File Offset: 0x00029B10
	public override string GetFileName()
	{
		return this.m_fileName;
	}

	// Token: 0x040005B2 RID: 1458
	private CharaType m_type;

	// Token: 0x040005B3 RID: 1459
	private string m_fileName;

	// Token: 0x040005B4 RID: 1460
	private UITexture m_uiTex;
}
