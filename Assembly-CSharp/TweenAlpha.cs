using System;
using UnityEngine;

// Token: 0x020000BA RID: 186
[AddComponentMenu("NGUI/Tween/Tween Alpha")]
public class TweenAlpha : UITweener
{
	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x06000571 RID: 1393 RVA: 0x0001B914 File Offset: 0x00019B14
	// (set) Token: 0x06000572 RID: 1394 RVA: 0x0001B960 File Offset: 0x00019B60
	public float alpha
	{
		get
		{
			if (this.mWidget != null)
			{
				return this.mWidget.alpha;
			}
			if (this.mPanel != null)
			{
				return this.mPanel.alpha;
			}
			return 0f;
		}
		set
		{
			if (this.mWidget != null)
			{
				this.mWidget.alpha = value;
			}
			else if (this.mPanel != null)
			{
				this.mPanel.alpha = value;
			}
		}
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x0001B9AC File Offset: 0x00019BAC
	private void Awake()
	{
		this.mPanel = base.GetComponent<UIPanel>();
		if (this.mPanel == null)
		{
			this.mWidget = base.GetComponentInChildren<UIWidget>();
		}
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x0001B9D8 File Offset: 0x00019BD8
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.alpha = Mathf.Lerp(this.from, this.to, factor);
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x0001B9F4 File Offset: 0x00019BF4
	public static TweenAlpha Begin(GameObject go, float duration, float alpha)
	{
		TweenAlpha tweenAlpha = UITweener.Begin<TweenAlpha>(go, duration);
		tweenAlpha.from = tweenAlpha.alpha;
		tweenAlpha.to = alpha;
		if (duration <= 0f)
		{
			tweenAlpha.Sample(1f, true);
			tweenAlpha.enabled = false;
		}
		return tweenAlpha;
	}

	// Token: 0x040003BB RID: 955
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x040003BC RID: 956
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x040003BD RID: 957
	private Transform mTrans;

	// Token: 0x040003BE RID: 958
	private UIWidget mWidget;

	// Token: 0x040003BF RID: 959
	private UIPanel mPanel;
}
