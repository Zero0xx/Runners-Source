using System;
using UnityEngine;

// Token: 0x020000B8 RID: 184
[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
public class AnimatedColor : MonoBehaviour
{
	// Token: 0x0600056A RID: 1386 RVA: 0x0001B62C File Offset: 0x0001982C
	private void Awake()
	{
		this.mWidget = base.GetComponent<UIWidget>();
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x0001B63C File Offset: 0x0001983C
	private void Update()
	{
		this.mWidget.color = this.color;
	}

	// Token: 0x040003B0 RID: 944
	public Color color = Color.white;

	// Token: 0x040003B1 RID: 945
	private UIWidget mWidget;
}
