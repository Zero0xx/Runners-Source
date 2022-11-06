using System;
using UnityEngine;

// Token: 0x020000C1 RID: 193
[AddComponentMenu("NGUI/Tween/Tween Scale")]
public class TweenScale : UITweener
{
	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x0600059B RID: 1435 RVA: 0x0001C0C4 File Offset: 0x0001A2C4
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x0600059C RID: 1436 RVA: 0x0001C0EC File Offset: 0x0001A2EC
	// (set) Token: 0x0600059D RID: 1437 RVA: 0x0001C0FC File Offset: 0x0001A2FC
	public Vector3 scale
	{
		get
		{
			return this.cachedTransform.localScale;
		}
		set
		{
			this.cachedTransform.localScale = value;
		}
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0001C10C File Offset: 0x0001A30C
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.cachedTransform.localScale = this.from * (1f - factor) + this.to * factor;
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

	// Token: 0x0600059F RID: 1439 RVA: 0x0001C19C File Offset: 0x0001A39C
	public static TweenScale Begin(GameObject go, float duration, Vector3 scale)
	{
		TweenScale tweenScale = UITweener.Begin<TweenScale>(go, duration);
		tweenScale.from = tweenScale.scale;
		tweenScale.to = scale;
		if (duration <= 0f)
		{
			tweenScale.Sample(1f, true);
			tweenScale.enabled = false;
		}
		return tweenScale;
	}

	// Token: 0x040003D7 RID: 983
	public Vector3 from = Vector3.one;

	// Token: 0x040003D8 RID: 984
	public Vector3 to = Vector3.one;

	// Token: 0x040003D9 RID: 985
	public bool updateTable;

	// Token: 0x040003DA RID: 986
	private Transform mTrans;

	// Token: 0x040003DB RID: 987
	private UITable mTable;
}
