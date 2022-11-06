using System;
using UnityEngine;

// Token: 0x02000063 RID: 99
[AddComponentMenu("NGUI/Examples/Lag Position")]
public class LagPosition : MonoBehaviour
{
	// Token: 0x060002D8 RID: 728 RVA: 0x0000C928 File Offset: 0x0000AB28
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRelative = this.mTrans.localPosition;
		if (this.ignoreTimeScale)
		{
			UpdateManager.AddCoroutine(this, this.updateOrder, new UpdateManager.OnUpdate(this.CoroutineUpdate));
		}
		else
		{
			UpdateManager.AddLateUpdate(this, this.updateOrder, new UpdateManager.OnUpdate(this.CoroutineUpdate));
		}
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x0000C994 File Offset: 0x0000AB94
	private void OnEnable()
	{
		this.mTrans = base.transform;
		this.mAbsolute = this.mTrans.position;
	}

	// Token: 0x060002DA RID: 730 RVA: 0x0000C9B4 File Offset: 0x0000ABB4
	private void CoroutineUpdate(float delta)
	{
		Transform parent = this.mTrans.parent;
		if (parent != null)
		{
			Vector3 vector = parent.position + parent.rotation * this.mRelative;
			this.mAbsolute.x = Mathf.Lerp(this.mAbsolute.x, vector.x, Mathf.Clamp01(delta * this.speed.x));
			this.mAbsolute.y = Mathf.Lerp(this.mAbsolute.y, vector.y, Mathf.Clamp01(delta * this.speed.y));
			this.mAbsolute.z = Mathf.Lerp(this.mAbsolute.z, vector.z, Mathf.Clamp01(delta * this.speed.z));
			this.mTrans.position = this.mAbsolute;
		}
	}

	// Token: 0x04000188 RID: 392
	public int updateOrder;

	// Token: 0x04000189 RID: 393
	public Vector3 speed = new Vector3(10f, 10f, 10f);

	// Token: 0x0400018A RID: 394
	public bool ignoreTimeScale;

	// Token: 0x0400018B RID: 395
	private Transform mTrans;

	// Token: 0x0400018C RID: 396
	private Vector3 mRelative;

	// Token: 0x0400018D RID: 397
	private Vector3 mAbsolute;
}
