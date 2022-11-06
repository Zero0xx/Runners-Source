using System;
using UnityEngine;

// Token: 0x02000099 RID: 153
[AddComponentMenu("NGUI/Interaction/Sound Volume")]
[RequireComponent(typeof(UISlider))]
public class UISoundVolume : MonoBehaviour
{
	// Token: 0x06000407 RID: 1031 RVA: 0x00014808 File Offset: 0x00012A08
	private void Awake()
	{
		this.mSlider = base.GetComponent<UISlider>();
		this.mSlider.value = NGUITools.soundVolume;
		EventDelegate.Add(this.mSlider.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x00014850 File Offset: 0x00012A50
	private void OnChange()
	{
		NGUITools.soundVolume = UISlider.current.value;
	}

	// Token: 0x040002DD RID: 733
	private UISlider mSlider;
}
