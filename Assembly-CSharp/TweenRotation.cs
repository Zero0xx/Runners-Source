using System;
using UnityEngine;

// Token: 0x020000C0 RID: 192
[AddComponentMenu("NGUI/Tween/Tween Rotation")]
public class TweenRotation : UITweener
{
	// Token: 0x170000DE RID: 222
	// (get) Token: 0x06000595 RID: 1429 RVA: 0x0001BFD0 File Offset: 0x0001A1D0
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

	// Token: 0x170000DF RID: 223
	// (get) Token: 0x06000596 RID: 1430 RVA: 0x0001BFF8 File Offset: 0x0001A1F8
	// (set) Token: 0x06000597 RID: 1431 RVA: 0x0001C008 File Offset: 0x0001A208
	public Quaternion rotation
	{
		get
		{
			return this.cachedTransform.localRotation;
		}
		set
		{
			this.cachedTransform.localRotation = value;
		}
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x0001C018 File Offset: 0x0001A218
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.cachedTransform.localRotation = Quaternion.Slerp(Quaternion.Euler(this.from), Quaternion.Euler(this.to), factor);
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x0001C04C File Offset: 0x0001A24C
	public static TweenRotation Begin(GameObject go, float duration, Quaternion rot)
	{
		TweenRotation tweenRotation = UITweener.Begin<TweenRotation>(go, duration);
		tweenRotation.from = tweenRotation.rotation.eulerAngles;
		tweenRotation.to = rot.eulerAngles;
		if (duration <= 0f)
		{
			tweenRotation.Sample(1f, true);
			tweenRotation.enabled = false;
		}
		return tweenRotation;
	}

	// Token: 0x040003D4 RID: 980
	public Vector3 from;

	// Token: 0x040003D5 RID: 981
	public Vector3 to;

	// Token: 0x040003D6 RID: 982
	private Transform mTrans;
}
