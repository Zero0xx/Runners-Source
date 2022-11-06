using System;
using UnityEngine;

// Token: 0x020000C3 RID: 195
[AddComponentMenu("NGUI/Tween/Tween Volume")]
public class TweenVolume : UITweener
{
	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001C424 File Offset: 0x0001A624
	public AudioSource audioSource
	{
		get
		{
			if (this.mSource == null)
			{
				this.mSource = base.audio;
				if (this.mSource == null)
				{
					this.mSource = base.GetComponentInChildren<AudioSource>();
					if (this.mSource == null)
					{
						global::Debug.LogError("TweenVolume needs an AudioSource to work with", this);
						base.enabled = false;
					}
				}
			}
			return this.mSource;
		}
	}

	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001C494 File Offset: 0x0001A694
	// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0001C4C8 File Offset: 0x0001A6C8
	public float volume
	{
		get
		{
			return (!(this.audioSource != null)) ? 0f : this.mSource.volume;
		}
		set
		{
			if (this.audioSource != null)
			{
				this.mSource.volume = value;
			}
		}
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x0001C4E8 File Offset: 0x0001A6E8
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.volume = this.from * (1f - factor) + this.to * factor;
		this.mSource.enabled = (this.mSource.volume > 0.01f);
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x0001C530 File Offset: 0x0001A730
	public static TweenVolume Begin(GameObject go, float duration, float targetVolume)
	{
		TweenVolume tweenVolume = UITweener.Begin<TweenVolume>(go, duration);
		tweenVolume.from = tweenVolume.volume;
		tweenVolume.to = targetVolume;
		if (duration <= 0f)
		{
			tweenVolume.Sample(1f, true);
			tweenVolume.enabled = false;
		}
		return tweenVolume;
	}

	// Token: 0x040003E3 RID: 995
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x040003E4 RID: 996
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x040003E5 RID: 997
	private AudioSource mSource;
}
