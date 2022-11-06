using System;
using UnityEngine;

// Token: 0x020000C2 RID: 194
[AddComponentMenu("NGUI/Tween/Tween Transform")]
public class TweenTransform : UITweener
{
	// Token: 0x060005A1 RID: 1441 RVA: 0x0001C1EC File Offset: 0x0001A3EC
	protected override void OnUpdate(float factor, bool isFinished)
	{
		if (this.to != null)
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
				this.mPos = this.mTrans.position;
				this.mRot = this.mTrans.rotation;
				this.mScale = this.mTrans.localScale;
			}
			if (this.from != null)
			{
				this.mTrans.position = this.from.position * (1f - factor) + this.to.position * factor;
				this.mTrans.localScale = this.from.localScale * (1f - factor) + this.to.localScale * factor;
				this.mTrans.rotation = Quaternion.Slerp(this.from.rotation, this.to.rotation, factor);
			}
			else
			{
				this.mTrans.position = this.mPos * (1f - factor) + this.to.position * factor;
				this.mTrans.localScale = this.mScale * (1f - factor) + this.to.localScale * factor;
				this.mTrans.rotation = Quaternion.Slerp(this.mRot, this.to.rotation, factor);
			}
			if (this.parentWhenFinished && isFinished)
			{
				this.mTrans.parent = this.to;
			}
		}
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x0001C3B4 File Offset: 0x0001A5B4
	public static TweenTransform Begin(GameObject go, float duration, Transform to)
	{
		return TweenTransform.Begin(go, duration, null, to);
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x0001C3C0 File Offset: 0x0001A5C0
	public static TweenTransform Begin(GameObject go, float duration, Transform from, Transform to)
	{
		TweenTransform tweenTransform = UITweener.Begin<TweenTransform>(go, duration);
		tweenTransform.from = from;
		tweenTransform.to = to;
		if (duration <= 0f)
		{
			tweenTransform.Sample(1f, true);
			tweenTransform.enabled = false;
		}
		return tweenTransform;
	}

	// Token: 0x040003DC RID: 988
	public Transform from;

	// Token: 0x040003DD RID: 989
	public Transform to;

	// Token: 0x040003DE RID: 990
	public bool parentWhenFinished;

	// Token: 0x040003DF RID: 991
	private Transform mTrans;

	// Token: 0x040003E0 RID: 992
	private Vector3 mPos;

	// Token: 0x040003E1 RID: 993
	private Quaternion mRot;

	// Token: 0x040003E2 RID: 994
	private Vector3 mScale;
}
