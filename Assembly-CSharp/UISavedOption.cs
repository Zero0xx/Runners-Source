using System;
using UnityEngine;

// Token: 0x02000094 RID: 148
[AddComponentMenu("NGUI/Interaction/Saved Option")]
public class UISavedOption : MonoBehaviour
{
	// Token: 0x17000080 RID: 128
	// (get) Token: 0x060003D2 RID: 978 RVA: 0x000131D8 File Offset: 0x000113D8
	private string key
	{
		get
		{
			return (!string.IsNullOrEmpty(this.keyName)) ? this.keyName : ("NGUI State: " + base.name);
		}
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x00013208 File Offset: 0x00011408
	private void Awake()
	{
		this.mList = base.GetComponent<UIPopupList>();
		this.mCheck = base.GetComponent<UIToggle>();
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x00013224 File Offset: 0x00011424
	private void OnEnable()
	{
		if (this.mList != null)
		{
			EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.SaveSelection));
		}
		if (this.mCheck != null)
		{
			EventDelegate.Add(this.mCheck.onChange, new EventDelegate.Callback(this.SaveState));
		}
		if (this.mList != null)
		{
			string @string = PlayerPrefs.GetString(this.key);
			if (!string.IsNullOrEmpty(@string))
			{
				this.mList.value = @string;
			}
			return;
		}
		if (this.mCheck != null)
		{
			this.mCheck.value = (PlayerPrefs.GetInt(this.key, 1) != 0);
		}
		else
		{
			string string2 = PlayerPrefs.GetString(this.key);
			UIToggle[] componentsInChildren = base.GetComponentsInChildren<UIToggle>(true);
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				UIToggle uitoggle = componentsInChildren[i];
				uitoggle.value = (uitoggle.name == string2);
				i++;
			}
		}
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x00013338 File Offset: 0x00011538
	private void OnDisable()
	{
		if (this.mCheck != null)
		{
			EventDelegate.Remove(this.mCheck.onChange, new EventDelegate.Callback(this.SaveState));
		}
		if (this.mList != null)
		{
			EventDelegate.Remove(this.mList.onChange, new EventDelegate.Callback(this.SaveSelection));
		}
		if (this.mCheck == null && this.mList == null)
		{
			UIToggle[] componentsInChildren = base.GetComponentsInChildren<UIToggle>(true);
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				UIToggle uitoggle = componentsInChildren[i];
				if (uitoggle.value)
				{
					PlayerPrefs.SetString(this.key, uitoggle.name);
					break;
				}
				i++;
			}
		}
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x00013408 File Offset: 0x00011608
	private void SaveSelection()
	{
		PlayerPrefs.SetString(this.key, UIPopupList.current.value);
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x00013420 File Offset: 0x00011620
	private void SaveState()
	{
		PlayerPrefs.SetInt(this.key, (!UIToggle.current.value) ? 0 : 1);
	}

	// Token: 0x040002B6 RID: 694
	public string keyName;

	// Token: 0x040002B7 RID: 695
	private UIPopupList mList;

	// Token: 0x040002B8 RID: 696
	private UIToggle mCheck;
}
