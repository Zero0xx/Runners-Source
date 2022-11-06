using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009D RID: 157
[RequireComponent(typeof(UIToggle))]
[AddComponentMenu("NGUI/Interaction/Toggled Components")]
[ExecuteInEditMode]
public class UIToggledComponents : MonoBehaviour
{
	// Token: 0x0600041C RID: 1052 RVA: 0x00015100 File Offset: 0x00013300
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

	// Token: 0x0600041D RID: 1053 RVA: 0x0001519C File Offset: 0x0001339C
	public void Toggle()
	{
		if (base.enabled)
		{
			for (int i = 0; i < this.activate.Count; i++)
			{
				MonoBehaviour monoBehaviour = this.activate[i];
				monoBehaviour.enabled = UIToggle.current.value;
			}
			for (int j = 0; j < this.deactivate.Count; j++)
			{
				MonoBehaviour monoBehaviour2 = this.deactivate[j];
				monoBehaviour2.enabled = !UIToggle.current.value;
			}
		}
	}

	// Token: 0x040002FE RID: 766
	public List<MonoBehaviour> activate;

	// Token: 0x040002FF RID: 767
	public List<MonoBehaviour> deactivate;

	// Token: 0x04000300 RID: 768
	[SerializeField]
	[HideInInspector]
	private MonoBehaviour target;

	// Token: 0x04000301 RID: 769
	[HideInInspector]
	[SerializeField]
	private bool inverse;
}
