using System;
using UnityEngine;

// Token: 0x020000B7 RID: 183
public class AnimatedAlpha : MonoBehaviour
{
	// Token: 0x06000567 RID: 1383 RVA: 0x0001B5A4 File Offset: 0x000197A4
	private void Awake()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.mPanel = base.GetComponent<UIPanel>();
		this.Update();
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x0001B5C4 File Offset: 0x000197C4
	private void Update()
	{
		if (this.mWidget != null)
		{
			this.mWidget.alpha = this.alpha;
		}
		if (this.mPanel != null)
		{
			this.mPanel.alpha = this.alpha;
		}
	}

	// Token: 0x040003AD RID: 941
	public float alpha = 1f;

	// Token: 0x040003AE RID: 942
	private UIWidget mWidget;

	// Token: 0x040003AF RID: 943
	private UIPanel mPanel;
}
