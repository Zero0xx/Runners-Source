using System;
using UnityEngine;

// Token: 0x0200006B RID: 107
[AddComponentMenu("NGUI/Examples/Spin")]
public class Spin : MonoBehaviour
{
	// Token: 0x060002EE RID: 750 RVA: 0x0000D1F0 File Offset: 0x0000B3F0
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRb = base.rigidbody;
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0000D20C File Offset: 0x0000B40C
	private void Update()
	{
		if (this.mRb == null)
		{
			this.ApplyDelta(Time.deltaTime);
		}
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x0000D22C File Offset: 0x0000B42C
	private void FixedUpdate()
	{
		if (this.mRb != null)
		{
			this.ApplyDelta(Time.deltaTime);
		}
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x0000D24C File Offset: 0x0000B44C
	public void ApplyDelta(float delta)
	{
		delta *= 360f;
		Quaternion rhs = Quaternion.Euler(this.rotationsPerSecond * delta);
		if (this.mRb == null)
		{
			this.mTrans.rotation = this.mTrans.rotation * rhs;
		}
		else
		{
			this.mRb.MoveRotation(this.mRb.rotation * rhs);
		}
	}

	// Token: 0x040001A6 RID: 422
	public Vector3 rotationsPerSecond = new Vector3(0f, 0.1f, 0f);

	// Token: 0x040001A7 RID: 423
	private Rigidbody mRb;

	// Token: 0x040001A8 RID: 424
	private Transform mTrans;
}
