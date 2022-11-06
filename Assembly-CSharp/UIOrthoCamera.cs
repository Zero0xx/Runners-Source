using System;
using UnityEngine;

// Token: 0x020000DC RID: 220
[AddComponentMenu("NGUI/UI/Orthographic Camera")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class UIOrthoCamera : MonoBehaviour
{
	// Token: 0x06000682 RID: 1666 RVA: 0x000241F0 File Offset: 0x000223F0
	private void Start()
	{
		this.mCam = base.camera;
		this.mTrans = base.transform;
		this.mCam.orthographic = true;
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x00024224 File Offset: 0x00022424
	private void Update()
	{
		float num = this.mCam.rect.yMin * (float)Screen.height;
		float num2 = this.mCam.rect.yMax * (float)Screen.height;
		float num3 = (num2 - num) * 0.5f * this.mTrans.lossyScale.y;
		if (!Mathf.Approximately(this.mCam.orthographicSize, num3))
		{
			this.mCam.orthographicSize = num3;
		}
	}

	// Token: 0x040004DA RID: 1242
	private Camera mCam;

	// Token: 0x040004DB RID: 1243
	private Transform mTrans;
}
