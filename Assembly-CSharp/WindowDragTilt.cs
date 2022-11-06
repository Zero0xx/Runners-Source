using System;
using UnityEngine;

// Token: 0x02000073 RID: 115
[AddComponentMenu("NGUI/Examples/Window Drag Tilt")]
public class WindowDragTilt : MonoBehaviour
{
	// Token: 0x06000310 RID: 784 RVA: 0x0000DC18 File Offset: 0x0000BE18
	private void Start()
	{
		UpdateManager.AddCoroutine(this, this.updateOrder, new UpdateManager.OnUpdate(this.CoroutineUpdate));
	}

	// Token: 0x06000311 RID: 785 RVA: 0x0000DC34 File Offset: 0x0000BE34
	private void OnEnable()
	{
		this.mInit = true;
	}

	// Token: 0x06000312 RID: 786 RVA: 0x0000DC40 File Offset: 0x0000BE40
	private void CoroutineUpdate(float delta)
	{
		if (this.mInit)
		{
			this.mInit = false;
			this.mTrans = base.transform;
			this.mLastPos = this.mTrans.position;
		}
		Vector3 vector = this.mTrans.position - this.mLastPos;
		this.mLastPos = this.mTrans.position;
		this.mAngle += vector.x * this.degrees;
		this.mAngle = NGUIMath.SpringLerp(this.mAngle, 0f, 20f, delta);
		this.mTrans.localRotation = Quaternion.Euler(0f, 0f, -this.mAngle);
	}

	// Token: 0x040001C4 RID: 452
	public int updateOrder;

	// Token: 0x040001C5 RID: 453
	public float degrees = 30f;

	// Token: 0x040001C6 RID: 454
	private Vector3 mLastPos;

	// Token: 0x040001C7 RID: 455
	private Transform mTrans;

	// Token: 0x040001C8 RID: 456
	private float mAngle;

	// Token: 0x040001C9 RID: 457
	private bool mInit = true;
}
