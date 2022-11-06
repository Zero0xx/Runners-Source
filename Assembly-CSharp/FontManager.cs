using System;
using UnityEngine;

// Token: 0x0200023F RID: 575
public class FontManager : MonoBehaviour
{
	// Token: 0x1700026D RID: 621
	// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x0005DA7C File Offset: 0x0005BC7C
	public static FontManager Instance
	{
		get
		{
			return FontManager.instance;
		}
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x0005DA84 File Offset: 0x0005BC84
	public bool IsNecessaryLoadFont()
	{
		return !this.m_loadedFont;
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x0005DA90 File Offset: 0x0005BC90
	public void LoadResourceData()
	{
		if (this.IsNecessaryLoadFont())
		{
			GameObject gameObject = Resources.Load(this.GetResourceName()) as GameObject;
			if (gameObject != null)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, gameObject.transform.localPosition, Quaternion.identity) as GameObject;
				if (gameObject2 != null)
				{
					gameObject2.transform.parent = base.gameObject.transform;
					this.m_uiFont = gameObject2.GetComponent<UIFont>();
					this.m_loadedFont = true;
				}
			}
		}
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x0005DB18 File Offset: 0x0005BD18
	protected void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x0005DB20 File Offset: 0x0005BD20
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x0005DB2C File Offset: 0x0005BD2C
	private void OnDestroy()
	{
		if (FontManager.instance == this)
		{
			FontManager.instance = null;
		}
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x0005DB44 File Offset: 0x0005BD44
	private void SetInstance()
	{
		if (FontManager.instance == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			FontManager.instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x0005DB78 File Offset: 0x0005BD78
	public void ReplaceFont()
	{
		if (this.m_uiFont != null)
		{
			UIFont[] array = Resources.FindObjectsOfTypeAll(typeof(UIFont)) as UIFont[];
			foreach (UIFont uifont in array)
			{
				if (uifont != null && uifont.name == "UCGothic_26_mix_reference")
				{
					uifont.replacement = this.m_uiFont;
				}
			}
		}
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x0005DBF4 File Offset: 0x0005BDF4
	private string GetFontName()
	{
		return "UCGothic_26_mix";
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x0005DBFC File Offset: 0x0005BDFC
	private string GetResourceName()
	{
		return "Prefabs/Font/UCGothic_26_mix";
	}

	// Token: 0x04000DA3 RID: 3491
	private UIFont m_uiFont;

	// Token: 0x04000DA4 RID: 3492
	private bool m_loadedFont;

	// Token: 0x04000DA5 RID: 3493
	private static FontManager instance;
}
