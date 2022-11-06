using System;
using UnityEngine;

// Token: 0x02000074 RID: 116
[AddComponentMenu("NGUI/Interaction/Language Selection")]
[RequireComponent(typeof(UIPopupList))]
public class LanguageSelection : MonoBehaviour
{
	// Token: 0x06000314 RID: 788 RVA: 0x0000DD04 File Offset: 0x0000BF04
	private void Start()
	{
		this.mList = base.GetComponent<UIPopupList>();
		if (Localization.instance != null && Localization.instance.languages != null && Localization.instance.languages.Length > 0)
		{
			this.mList.items.Clear();
			int i = 0;
			int num = Localization.instance.languages.Length;
			while (i < num)
			{
				TextAsset textAsset = Localization.instance.languages[i];
				if (textAsset != null)
				{
					this.mList.items.Add(textAsset.name);
				}
				i++;
			}
			this.mList.value = Localization.instance.currentLanguage;
		}
		EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x06000315 RID: 789 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
	private void OnChange()
	{
		if (Localization.instance != null)
		{
			Localization.instance.currentLanguage = UIPopupList.current.value;
		}
	}

	// Token: 0x040001CA RID: 458
	private UIPopupList mList;
}
