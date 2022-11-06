using System;
using UnityEngine;

// Token: 0x020000C4 RID: 196
[AddComponentMenu("NGUI/Tween/Tween Width")]
[RequireComponent(typeof(UIWidget))]
public class TweenWidth : UITweener
{
	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x060005AB RID: 1451 RVA: 0x0001C590 File Offset: 0x0001A790
	public UIWidget cachedWidget
	{
		get
		{
			if (this.mWidget == null)
			{
				this.mWidget = base.GetComponent<UIWidget>();
			}
			return this.mWidget;
		}
	}

	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x060005AC RID: 1452 RVA: 0x0001C5B8 File Offset: 0x0001A7B8
	// (set) Token: 0x060005AD RID: 1453 RVA: 0x0001C5C8 File Offset: 0x0001A7C8
	public int width
	{
		get
		{
			return this.cachedWidget.width;
		}
		set
		{
			this.cachedWidget.width = value;
		}
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x0001C5D8 File Offset: 0x0001A7D8
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.cachedWidget.width = Mathf.RoundToInt((float)this.from * (1f - factor) + (float)this.to * factor);
		if (this.updateTable)
		{
			if (this.mTable == null)
			{
				this.mTable = NGUITools.FindInParents<UITable>(base.gameObject);
				if (this.mTable == null)
				{
					this.updateTable = false;
					return;
				}
			}
			this.mTable.repositionNow = true;
		}
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x0001C660 File Offset: 0x0001A860
	public static TweenWidth Begin(UIWidget widget, float duration, int width)
	{
		TweenWidth tweenWidth = UITweener.Begin<TweenWidth>(widget.gameObject, duration);
		tweenWidth.from = widget.width;
		tweenWidth.to = width;
		if (duration <= 0f)
		{
			tweenWidth.Sample(1f, true);
			tweenWidth.enabled = false;
		}
		return tweenWidth;
	}

	// Token: 0x040003E6 RID: 998
	public int from = 100;

	// Token: 0x040003E7 RID: 999
	public int to = 100;

	// Token: 0x040003E8 RID: 1000
	public bool updateTable;

	// Token: 0x040003E9 RID: 1001
	private UIWidget mWidget;

	// Token: 0x040003EA RID: 1002
	private UITable mTable;
}
