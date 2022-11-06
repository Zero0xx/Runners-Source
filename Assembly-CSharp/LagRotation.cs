using System;
using UnityEngine;

// Token: 0x02000064 RID: 100
[AddComponentMenu("NGUI/Examples/Lag Rotation")]
public class LagRotation : MonoBehaviour
{
	// Token: 0x060002DC RID: 732 RVA: 0x0000CAB8 File Offset: 0x0000ACB8
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRelative = this.mTrans.localRotation;
		this.mAbsolute = this.mTrans.rotation;
		if (this.ignoreTimeScale)
		{
			UpdateManager.AddCoroutine(this, this.updateOrder, new UpdateManager.OnUpdate(this.CoroutineUpdate));
		}
		else
		{
			UpdateManager.AddLateUpdate(this, this.updateOrder, new UpdateManager.OnUpdate(this.CoroutineUpdate));
		}
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0000CB34 File Offset: 0x0000AD34
	private void CoroutineUpdate(float delta)
	{
		Transform parent = this.mTrans.parent;
		if (parent != null)
		{
			this.mAbsolute = Quaternion.Slerp(this.mAbsolute, parent.rotation * this.mRelative, delta * this.speed);
			this.mTrans.rotation = this.mAbsolute;
		}
	}

	// Token: 0x0400018E RID: 398
	public int updateOrder;

	// Token: 0x0400018F RID: 399
	public float speed = 10f;

	// Token: 0x04000190 RID: 400
	public bool ignoreTimeScale;

	// Token: 0x04000191 RID: 401
	private Transform mTrans;

	// Token: 0x04000192 RID: 402
	private Quaternion mRelative;

	// Token: 0x04000193 RID: 403
	private Quaternion mAbsolute;
}
