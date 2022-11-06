using System;
using UnityEngine;

// Token: 0x02000076 RID: 118
[AddComponentMenu("NGUI/Interaction/Button Activate")]
public class UIButtonActivate : MonoBehaviour
{
	// Token: 0x06000320 RID: 800 RVA: 0x0000DFE0 File Offset: 0x0000C1E0
	private void OnClick()
	{
		if (this.target != null)
		{
			NGUITools.SetActive(this.target, this.state);
		}
	}

	// Token: 0x040001CE RID: 462
	public GameObject target;

	// Token: 0x040001CF RID: 463
	public bool state = true;
}
