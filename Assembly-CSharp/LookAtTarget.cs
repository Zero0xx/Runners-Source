using System;
using UnityEngine;

// Token: 0x02000066 RID: 102
[AddComponentMenu("NGUI/Examples/Look At Target")]
public class LookAtTarget : MonoBehaviour
{
	// Token: 0x060002E1 RID: 737 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x0000CBE0 File Offset: 0x0000ADE0
	private void LateUpdate()
	{
		if (this.target != null)
		{
			Vector3 forward = this.target.position - this.mTrans.position;
			float magnitude = forward.magnitude;
			if (magnitude > 0.001f)
			{
				Quaternion to = Quaternion.LookRotation(forward);
				this.mTrans.rotation = Quaternion.Slerp(this.mTrans.rotation, to, Mathf.Clamp01(this.speed * Time.deltaTime));
			}
		}
	}

	// Token: 0x04000195 RID: 405
	public int level;

	// Token: 0x04000196 RID: 406
	public Transform target;

	// Token: 0x04000197 RID: 407
	public float speed = 8f;

	// Token: 0x04000198 RID: 408
	private Transform mTrans;
}
