using System;
using UnityEngine;

// Token: 0x020000BC RID: 188
[AddComponentMenu("NGUI/Tween/Tween Field of View")]
[RequireComponent(typeof(Camera))]
public class TweenFOV : UITweener
{
	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x0600057D RID: 1405 RVA: 0x0001BC20 File Offset: 0x00019E20
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

	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x0600057E RID: 1406 RVA: 0x0001BC48 File Offset: 0x00019E48
	// (set) Token: 0x0600057F RID: 1407 RVA: 0x0001BC58 File Offset: 0x00019E58
	public float fov
	{
		get
		{
			return this.cachedCamera.fieldOfView;
		}
		set
		{
			this.cachedCamera.fieldOfView = value;
		}
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x0001BC68 File Offset: 0x00019E68
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.cachedCamera.fieldOfView = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x0001BC98 File Offset: 0x00019E98
	public static TweenFOV Begin(GameObject go, float duration, float to)
	{
		TweenFOV tweenFOV = UITweener.Begin<TweenFOV>(go, duration);
		tweenFOV.from = tweenFOV.fov;
		tweenFOV.to = to;
		if (duration <= 0f)
		{
			tweenFOV.Sample(1f, true);
			tweenFOV.enabled = false;
		}
		return tweenFOV;
	}

	// Token: 0x040003C6 RID: 966
	public float from = 45f;

	// Token: 0x040003C7 RID: 967
	public float to = 45f;

	// Token: 0x040003C8 RID: 968
	private Camera mCam;
}
