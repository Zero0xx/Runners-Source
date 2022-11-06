using System;
using UnityEngine;

// Token: 0x020000BE RID: 190
[AddComponentMenu("NGUI/Tween/Tween Orthographic Size")]
[RequireComponent(typeof(Camera))]
public class TweenOrthoSize : UITweener
{
	// Token: 0x170000DA RID: 218
	// (get) Token: 0x06000589 RID: 1417 RVA: 0x0001BE34 File Offset: 0x0001A034
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.camera;
			}
			return this.mCam;
		}
	}

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x0600058A RID: 1418 RVA: 0x0001BE5C File Offset: 0x0001A05C
	// (set) Token: 0x0600058B RID: 1419 RVA: 0x0001BE6C File Offset: 0x0001A06C
	public float orthoSize
	{
		get
		{
			return this.cachedCamera.orthographicSize;
		}
		set
		{
			this.cachedCamera.orthographicSize = value;
		}
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x0001BE7C File Offset: 0x0001A07C
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.cachedCamera.orthographicSize = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x0001BEAC File Offset: 0x0001A0AC
	public static TweenOrthoSize Begin(GameObject go, float duration, float to)
	{
		TweenOrthoSize tweenOrthoSize = UITweener.Begin<TweenOrthoSize>(go, duration);
		tweenOrthoSize.from = tweenOrthoSize.orthoSize;
		tweenOrthoSize.to = to;
		if (duration <= 0f)
		{
			tweenOrthoSize.Sample(1f, true);
			tweenOrthoSize.enabled = false;
		}
		return tweenOrthoSize;
	}

	// Token: 0x040003CE RID: 974
	public float from = 1f;

	// Token: 0x040003CF RID: 975
	public float to = 1f;

	// Token: 0x040003D0 RID: 976
	private Camera mCam;
}
