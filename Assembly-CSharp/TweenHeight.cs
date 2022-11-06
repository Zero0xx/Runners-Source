using System;
using UnityEngine;

// Token: 0x020000BD RID: 189
[AddComponentMenu("NGUI/Tween/Tween Height")]
[RequireComponent(typeof(UIWidget))]
public class TweenHeight : UITweener
{
	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x06000583 RID: 1411 RVA: 0x0001BCF8 File Offset: 0x00019EF8
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

	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001BD20 File Offset: 0x00019F20
	// (set) Token: 0x06000585 RID: 1413 RVA: 0x0001BD30 File Offset: 0x00019F30
	public int height
	{
		get
		{
			return this.cachedWidget.height;
		}
		set
		{
			this.cachedWidget.height = value;
		}
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x0001BD40 File Offset: 0x00019F40
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.cachedWidget.height = Mathf.RoundToInt((float)this.from * (1f - factor) + (float)this.to * factor);
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

	// Token: 0x06000587 RID: 1415 RVA: 0x0001BDC8 File Offset: 0x00019FC8
	public static TweenHeight Begin(UIWidget widget, float duration, int height)
	{
		TweenHeight tweenHeight = UITweener.Begin<TweenHeight>(widget.gameObject, duration);
		tweenHeight.from = widget.height;
		tweenHeight.to = height;
		if (duration <= 0f)
		{
			tweenHeight.Sample(1f, true);
			tweenHeight.enabled = false;
		}
		return tweenHeight;
	}

	// Token: 0x040003C9 RID: 969
	public int from = 100;

	// Token: 0x040003CA RID: 970
	public int to = 100;

	// Token: 0x040003CB RID: 971
	public bool updateTable;

	// Token: 0x040003CC RID: 972
	private UIWidget mWidget;

	// Token: 0x040003CD RID: 973
	private UITable mTable;
}
