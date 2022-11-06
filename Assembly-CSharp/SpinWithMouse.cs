using System;
using UnityEngine;

// Token: 0x0200006C RID: 108
[AddComponentMenu("NGUI/Examples/Spin With Mouse")]
public class SpinWithMouse : MonoBehaviour
{
	// Token: 0x060002F3 RID: 755 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x0000D2E8 File Offset: 0x0000B4E8
	private void OnDrag(Vector2 delta)
	{
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
		if (this.target != null)
		{
			this.target.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * this.speed, 0f) * this.target.localRotation;
		}
		else
		{
			this.mTrans.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * this.speed, 0f) * this.mTrans.localRotation;
		}
	}

	// Token: 0x040001A9 RID: 425
	public Transform target;

	// Token: 0x040001AA RID: 426
	public float speed = 1f;

	// Token: 0x040001AB RID: 427
	private Transform mTrans;
}
