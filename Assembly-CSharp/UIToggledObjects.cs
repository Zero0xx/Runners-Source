using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009E RID: 158
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Toggled Objects")]
public class UIToggledObjects : MonoBehaviour
{
	// Token: 0x0600041F RID: 1055 RVA: 0x00015234 File Offset: 0x00013434
	private void Awake()
	{
		if (this.target != null)
		{
			if (this.activate.Count == 0 && this.deactivate.Count == 0)
			{
				if (this.inverse)
				{
					this.deactivate.Add(this.target);
				}
				else
				{
					this.activate.Add(this.target);
				}
			}
			else
			{
				this.target = null;
			}
		}
		UIToggle component = base.GetComponent<UIToggle>();
		EventDelegate.Add(component.onChange, new EventDelegate.Callback(this.Toggle));
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x000152D0 File Offset: 0x000134D0
	public void Toggle()
	{
		if (base.enabled)
		{
			for (int i = 0; i < this.activate.Count; i++)
			{
				this.Set(this.activate[i], UIToggle.current.value);
			}
			for (int j = 0; j < this.deactivate.Count; j++)
			{
				this.Set(this.deactivate[j], !UIToggle.current.value);
			}
		}
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x0001535C File Offset: 0x0001355C
	private void Set(GameObject go, bool state)
	{
		if (go != null)
		{
			NGUITools.SetActive(go, state);
		}
	}

	// Token: 0x04000302 RID: 770
	public List<GameObject> activate;

	// Token: 0x04000303 RID: 771
	public List<GameObject> deactivate;

	// Token: 0x04000304 RID: 772
	[SerializeField]
	[HideInInspector]
	private GameObject target;

	// Token: 0x04000305 RID: 773
	[HideInInspector]
	[SerializeField]
	private bool inverse;
}
