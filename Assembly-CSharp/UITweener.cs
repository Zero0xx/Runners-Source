using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public abstract class UITweener : MonoBehaviour
{
	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001C744 File Offset: 0x0001A944
	public float amountPerDelta
	{
		get
		{
			if (this.mDuration != this.duration)
			{
				this.mDuration = this.duration;
				this.mAmountPerDelta = Mathf.Abs((this.duration <= 0f) ? 1000f : (1f / this.duration));
			}
			return this.mAmountPerDelta;
		}
	}

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001C7A8 File Offset: 0x0001A9A8
	public float tweenFactor
	{
		get
		{
			return this.mFactor;
		}
	}

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001C7B0 File Offset: 0x0001A9B0
	public Direction direction
	{
		get
		{
			return (this.mAmountPerDelta >= 0f) ? Direction.Forward : Direction.Reverse;
		}
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x0001C7CC File Offset: 0x0001A9CC
	private void Start()
	{
		this.Update();
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x0001C7D4 File Offset: 0x0001A9D4
	private void Update()
	{
		float num = (!this.ignoreTimeScale) ? Time.deltaTime : RealTime.deltaTime;
		float num2 = (!this.ignoreTimeScale) ? Time.time : RealTime.time;
		if (!this.mStarted)
		{
			this.mStarted = true;
			this.mStartTime = num2 + this.delay;
		}
		if (num2 < this.mStartTime)
		{
			return;
		}
		this.mFactor += this.amountPerDelta * num;
		if (this.style == UITweener.Style.Loop)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor -= Mathf.Floor(this.mFactor);
			}
		}
		else if (this.style == UITweener.Style.PingPong)
		{
			if (this.mFactor > 1f)
			{
				this.mFactor = 1f - (this.mFactor - Mathf.Floor(this.mFactor));
				this.mAmountPerDelta = -this.mAmountPerDelta;
			}
			else if (this.mFactor < 0f)
			{
				this.mFactor = -this.mFactor;
				this.mFactor -= Mathf.Floor(this.mFactor);
				this.mAmountPerDelta = -this.mAmountPerDelta;
			}
		}
		if (this.style == UITweener.Style.Once && (this.mFactor > 1f || this.mFactor < 0f))
		{
			this.mFactor = Mathf.Clamp01(this.mFactor);
			this.Sample(this.mFactor, true);
			UITweener.current = this;
			EventDelegate.Execute(this.onFinished);
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
			{
				this.eventReceiver.SendMessage(this.callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
			}
			UITweener.current = null;
			if ((this.mFactor == 1f && this.mAmountPerDelta > 0f) || (this.mFactor == 0f && this.mAmountPerDelta < 0f))
			{
				base.enabled = false;
			}
		}
		else
		{
			this.Sample(this.mFactor, false);
		}
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x0001CA10 File Offset: 0x0001AC10
	private void OnDisable()
	{
		this.mStarted = false;
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x0001CA1C File Offset: 0x0001AC1C
	public void Sample(float factor, bool isFinished)
	{
		float num = Mathf.Clamp01(factor);
		if (this.method == UITweener.Method.EaseIn)
		{
			num = 1f - Mathf.Sin(1.5707964f * (1f - num));
			if (this.steeperCurves)
			{
				num *= num;
			}
		}
		else if (this.method == UITweener.Method.EaseOut)
		{
			num = Mathf.Sin(1.5707964f * num);
			if (this.steeperCurves)
			{
				num = 1f - num;
				num = 1f - num * num;
			}
		}
		else if (this.method == UITweener.Method.EaseInOut)
		{
			num -= Mathf.Sin(num * 6.2831855f) / 6.2831855f;
			if (this.steeperCurves)
			{
				num = num * 2f - 1f;
				float num2 = Mathf.Sign(num);
				num = 1f - Mathf.Abs(num);
				num = 1f - num * num;
				num = num2 * num * 0.5f + 0.5f;
			}
		}
		else if (this.method == UITweener.Method.BounceIn)
		{
			num = this.BounceLogic(num);
		}
		else if (this.method == UITweener.Method.BounceOut)
		{
			num = 1f - this.BounceLogic(1f - num);
		}
		this.OnUpdate((this.animationCurve == null) ? num : this.animationCurve.Evaluate(num), isFinished);
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x0001CB70 File Offset: 0x0001AD70
	private float BounceLogic(float val)
	{
		if (val < 0.363636f)
		{
			val = 7.5685f * val * val;
		}
		else if (val < 0.727272f)
		{
			val = 7.5625f * (val -= 0.545454f) * val + 0.75f;
		}
		else if (val < 0.90909f)
		{
			val = 7.5625f * (val -= 0.818181f) * val + 0.9375f;
		}
		else
		{
			val = 7.5625f * (val -= 0.9545454f) * val + 0.984375f;
		}
		return val;
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x0001CC08 File Offset: 0x0001AE08
	public void Play()
	{
		this.Play(true);
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x0001CC14 File Offset: 0x0001AE14
	public void Play(bool forward)
	{
		this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		if (!forward)
		{
			this.mAmountPerDelta = -this.mAmountPerDelta;
		}
		base.enabled = true;
		this.Update();
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x0001CC48 File Offset: 0x0001AE48
	public void Reset()
	{
		this.mStarted = false;
		this.mFactor = ((this.mAmountPerDelta >= 0f) ? 0f : 1f);
		this.Sample(this.mFactor, false);
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x0001CC84 File Offset: 0x0001AE84
	public void Toggle()
	{
		if (this.mFactor > 0f)
		{
			this.mAmountPerDelta = -this.amountPerDelta;
		}
		else
		{
			this.mAmountPerDelta = Mathf.Abs(this.amountPerDelta);
		}
		base.enabled = true;
	}

	// Token: 0x060005BD RID: 1469
	protected abstract void OnUpdate(float factor, bool isFinished);

	// Token: 0x060005BE RID: 1470 RVA: 0x0001CCCC File Offset: 0x0001AECC
	public static T Begin<T>(GameObject go, float duration) where T : UITweener
	{
		T t = go.GetComponent<T>();
		if (t == null)
		{
			t = go.AddComponent<T>();
		}
		t.mStarted = false;
		t.duration = duration;
		t.mFactor = 0f;
		t.mAmountPerDelta = Mathf.Abs(t.mAmountPerDelta);
		t.style = UITweener.Style.Once;
		t.animationCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f, 0f, 1f),
			new Keyframe(1f, 1f, 1f, 0f)
		});
		t.eventReceiver = null;
		t.callWhenFinished = null;
		t.enabled = true;
		return t;
	}

	// Token: 0x040003EB RID: 1003
	public static UITweener current;

	// Token: 0x040003EC RID: 1004
	[HideInInspector]
	public UITweener.Method method;

	// Token: 0x040003ED RID: 1005
	[HideInInspector]
	public UITweener.Style style;

	// Token: 0x040003EE RID: 1006
	[HideInInspector]
	public AnimationCurve animationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 1f),
		new Keyframe(1f, 1f, 1f, 0f)
	});

	// Token: 0x040003EF RID: 1007
	[HideInInspector]
	public bool ignoreTimeScale = true;

	// Token: 0x040003F0 RID: 1008
	[HideInInspector]
	public float delay;

	// Token: 0x040003F1 RID: 1009
	[HideInInspector]
	public float duration = 1f;

	// Token: 0x040003F2 RID: 1010
	[HideInInspector]
	public bool steeperCurves;

	// Token: 0x040003F3 RID: 1011
	[HideInInspector]
	public int tweenGroup;

	// Token: 0x040003F4 RID: 1012
	[HideInInspector]
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x040003F5 RID: 1013
	[HideInInspector]
	public GameObject eventReceiver;

	// Token: 0x040003F6 RID: 1014
	[HideInInspector]
	public string callWhenFinished;

	// Token: 0x040003F7 RID: 1015
	private bool mStarted;

	// Token: 0x040003F8 RID: 1016
	private float mStartTime;

	// Token: 0x040003F9 RID: 1017
	private float mDuration;

	// Token: 0x040003FA RID: 1018
	private float mAmountPerDelta = 1000f;

	// Token: 0x040003FB RID: 1019
	private float mFactor;

	// Token: 0x020000C6 RID: 198
	public enum Method
	{
		// Token: 0x040003FD RID: 1021
		Linear,
		// Token: 0x040003FE RID: 1022
		EaseIn,
		// Token: 0x040003FF RID: 1023
		EaseOut,
		// Token: 0x04000400 RID: 1024
		EaseInOut,
		// Token: 0x04000401 RID: 1025
		BounceIn,
		// Token: 0x04000402 RID: 1026
		BounceOut
	}

	// Token: 0x020000C7 RID: 199
	public enum Style
	{
		// Token: 0x04000404 RID: 1028
		Once,
		// Token: 0x04000405 RID: 1029
		Loop,
		// Token: 0x04000406 RID: 1030
		PingPong
	}
}
