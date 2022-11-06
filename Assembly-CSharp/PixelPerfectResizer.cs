using System;
using UnityEngine;

// Token: 0x02000A5F RID: 2655
[ExecuteInEditMode]
public class PixelPerfectResizer : MonoBehaviour
{
	// Token: 0x060047B7 RID: 18359 RVA: 0x0017A248 File Offset: 0x00178448
	private void Awake()
	{
		this._transform = base.transform;
	}

	// Token: 0x060047B8 RID: 18360 RVA: 0x0017A258 File Offset: 0x00178458
	private void Update()
	{
		if (this.cam == null)
		{
			this.cam = Camera.main;
		}
		if (this.cam != null && (this.cam.orthographicSize != this._lastOrthographicSize || this.cam.pixelWidth != this._lastPixelWidth || this.cam.pixelHeight != this._lastPixelHeight))
		{
			this._transform.localScale = new Vector3((float)((int)(this.cam.orthographicSize * 2000f * this.cam.aspect / this.cam.pixelWidth)) / 1000f, (float)((int)(this.cam.orthographicSize * 2000f / this.cam.pixelHeight)) / 1000f, this._transform.localScale.z);
		}
	}

	// Token: 0x04003B99 RID: 15257
	public Camera cam;

	// Token: 0x04003B9A RID: 15258
	private Transform _transform;

	// Token: 0x04003B9B RID: 15259
	private float _lastOrthographicSize;

	// Token: 0x04003B9C RID: 15260
	private float _lastPixelWidth;

	// Token: 0x04003B9D RID: 15261
	private float _lastPixelHeight;
}
