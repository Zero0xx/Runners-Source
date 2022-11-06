using System;
using UnityEngine;

// Token: 0x020000ED RID: 237
[AddComponentMenu("NGUI/UI/Viewport Camera")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class UIViewport : MonoBehaviour
{
	// Token: 0x06000717 RID: 1815 RVA: 0x00029854 File Offset: 0x00027A54
	private void Start()
	{
		this.mCam = base.camera;
		if (this.sourceCamera == null)
		{
			this.sourceCamera = Camera.main;
		}
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x0002988C File Offset: 0x00027A8C
	private void LateUpdate()
	{
		if (this.topLeft != null && this.bottomRight != null)
		{
			Vector3 vector = this.sourceCamera.WorldToScreenPoint(this.topLeft.position);
			Vector3 vector2 = this.sourceCamera.WorldToScreenPoint(this.bottomRight.position);
			Rect rect = new Rect(vector.x / (float)Screen.width, vector2.y / (float)Screen.height, (vector2.x - vector.x) / (float)Screen.width, (vector.y - vector2.y) / (float)Screen.height);
			float num = this.fullSize * rect.height;
			if (rect != this.mCam.rect)
			{
				this.mCam.rect = rect;
			}
			if (this.mCam.orthographicSize != num)
			{
				this.mCam.orthographicSize = num;
			}
		}
	}

	// Token: 0x0400056B RID: 1387
	public Camera sourceCamera;

	// Token: 0x0400056C RID: 1388
	public Transform topLeft;

	// Token: 0x0400056D RID: 1389
	public Transform bottomRight;

	// Token: 0x0400056E RID: 1390
	public float fullSize = 1f;

	// Token: 0x0400056F RID: 1391
	private Camera mCam;
}
