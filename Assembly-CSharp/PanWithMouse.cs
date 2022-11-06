using System;
using UnityEngine;

// Token: 0x02000067 RID: 103
[AddComponentMenu("NGUI/Examples/Pan With Mouse")]
public class PanWithMouse : MonoBehaviour
{
	// Token: 0x060002E4 RID: 740 RVA: 0x0000CC98 File Offset: 0x0000AE98
	private void Start()
	{
		this.mTrans = base.transform;
		this.mStart = this.mTrans.localRotation;
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x0000CCB8 File Offset: 0x0000AEB8
	private void Update()
	{
		float deltaTime = RealTime.deltaTime;
		Vector3 mousePosition = Input.mousePosition;
		float num = (float)Screen.width * 0.5f;
		float num2 = (float)Screen.height * 0.5f;
		if (this.range < 0.1f)
		{
			this.range = 0.1f;
		}
		float x = Mathf.Clamp((mousePosition.x - num) / num / this.range, -1f, 1f);
		float y = Mathf.Clamp((mousePosition.y - num2) / num2 / this.range, -1f, 1f);
		this.mRot = Vector2.Lerp(this.mRot, new Vector2(x, y), deltaTime * 5f);
		this.mTrans.localRotation = this.mStart * Quaternion.Euler(-this.mRot.y * this.degrees.y, this.mRot.x * this.degrees.x, 0f);
	}

	// Token: 0x04000199 RID: 409
	public Vector2 degrees = new Vector2(5f, 3f);

	// Token: 0x0400019A RID: 410
	public float range = 1f;

	// Token: 0x0400019B RID: 411
	private Transform mTrans;

	// Token: 0x0400019C RID: 412
	private Quaternion mStart;

	// Token: 0x0400019D RID: 413
	private Vector2 mRot = Vector2.zero;
}
