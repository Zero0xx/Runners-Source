using System;
using UnityEngine;

// Token: 0x02000072 RID: 114
[AddComponentMenu("NGUI/Examples/Window Auto-Yaw")]
public class WindowAutoYaw : MonoBehaviour
{
	// Token: 0x0600030C RID: 780 RVA: 0x0000DB24 File Offset: 0x0000BD24
	private void OnDisable()
	{
		this.mTrans.localRotation = Quaternion.identity;
	}

	// Token: 0x0600030D RID: 781 RVA: 0x0000DB38 File Offset: 0x0000BD38
	private void Start()
	{
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mTrans = base.transform;
		UpdateManager.AddCoroutine(this, this.updateOrder, new UpdateManager.OnUpdate(this.CoroutineUpdate));
	}

	// Token: 0x0600030E RID: 782 RVA: 0x0000DB90 File Offset: 0x0000BD90
	private void CoroutineUpdate(float delta)
	{
		if (this.uiCamera != null)
		{
			Vector3 vector = this.uiCamera.WorldToViewportPoint(this.mTrans.position);
			this.mTrans.localRotation = Quaternion.Euler(0f, (vector.x * 2f - 1f) * this.yawAmount, 0f);
		}
	}

	// Token: 0x040001C0 RID: 448
	public int updateOrder;

	// Token: 0x040001C1 RID: 449
	public Camera uiCamera;

	// Token: 0x040001C2 RID: 450
	public float yawAmount = 20f;

	// Token: 0x040001C3 RID: 451
	private Transform mTrans;
}
