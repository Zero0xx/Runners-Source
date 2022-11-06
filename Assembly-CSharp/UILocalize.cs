using System;
using UnityEngine;

// Token: 0x020000DB RID: 219
[AddComponentMenu("NGUI/UI/Localize")]
[RequireComponent(typeof(UIWidget))]
public class UILocalize : MonoBehaviour
{
	// Token: 0x0600067D RID: 1661 RVA: 0x00024080 File Offset: 0x00022280
	private void OnLocalize(Localization loc)
	{
		if (this.mLanguage != loc.currentLanguage)
		{
			this.Localize();
		}
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x000240A0 File Offset: 0x000222A0
	private void OnEnable()
	{
		if (this.mStarted && Localization.instance != null)
		{
			this.Localize();
		}
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x000240C4 File Offset: 0x000222C4
	private void Start()
	{
		this.mStarted = true;
		if (Localization.instance != null)
		{
			this.Localize();
		}
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x000240E4 File Offset: 0x000222E4
	public void Localize()
	{
		Localization instance = Localization.instance;
		UIWidget component = base.GetComponent<UIWidget>();
		UILabel uilabel = component as UILabel;
		UISprite uisprite = component as UISprite;
		if (string.IsNullOrEmpty(this.mLanguage) && string.IsNullOrEmpty(this.key) && uilabel != null)
		{
			this.key = uilabel.text;
		}
		string text = (!string.IsNullOrEmpty(this.key)) ? instance.Get(this.key) : string.Empty;
		if (uilabel != null)
		{
			UIInput uiinput = NGUITools.FindInParents<UIInput>(uilabel.gameObject);
			if (uiinput != null && uiinput.label == uilabel)
			{
				uiinput.defaultText = text;
			}
			else
			{
				uilabel.text = text;
			}
		}
		else if (uisprite != null)
		{
			uisprite.spriteName = text;
			uisprite.MakePixelPerfect();
		}
		this.mLanguage = instance.currentLanguage;
	}

	// Token: 0x040004D7 RID: 1239
	public string key;

	// Token: 0x040004D8 RID: 1240
	private string mLanguage;

	// Token: 0x040004D9 RID: 1241
	private bool mStarted;
}
