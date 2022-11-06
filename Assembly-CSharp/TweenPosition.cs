using System;
using UnityEngine;

// Token: 0x020000BF RID: 191
[AddComponentMenu("NGUI/Tween/Tween Position")]
public class TweenPosition : UITweener
{
	// Token: 0x170000DC RID: 220
	// (get) Token: 0x0600058F RID: 1423 RVA: 0x0001BEFC File Offset: 0x0001A0FC
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

	// Token: 0x170000DD RID: 221
	// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001BF24 File Offset: 0x0001A124
	// (set) Token: 0x06000591 RID: 1425 RVA: 0x0001BF34 File Offset: 0x0001A134
	public Vector3 position
	{
		get
		{
			return this.cachedTransform.localPosition;
		}
		set
		{
			this.cachedTransform.localPosition = value;
		}
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x0001BF44 File Offset: 0x0001A144
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.cachedTransform.localPosition = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x0001BF80 File Offset: 0x0001A180
	public static TweenPosition Begin(GameObject go, float duration, Vector3 pos)
	{
		TweenPosition tweenPosition = UITweener.Begin<TweenPosition>(go, duration);
		tweenPosition.from = tweenPosition.position;
		tweenPosition.to = pos;
		if (duration <= 0f)
		{
			tweenPosition.Sample(1f, true);
			tweenPosition.enabled = false;
		}
		return tweenPosition;
	}

	// Token: 0x040003D1 RID: 977
	public Vector3 from;

	// Token: 0x040003D2 RID: 978
	public Vector3 to;

	// Token: 0x040003D3 RID: 979
	private Transform mTrans;
}
