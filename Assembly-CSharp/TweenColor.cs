using System;
using UnityEngine;

// Token: 0x020000BB RID: 187
[AddComponentMenu("NGUI/Tween/Tween Color")]
public class TweenColor : UITweener
{
	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x06000577 RID: 1399 RVA: 0x0001BA5C File Offset: 0x00019C5C
	// (set) Token: 0x06000578 RID: 1400 RVA: 0x0001BAC8 File Offset: 0x00019CC8
	public Color color
	{
		get
		{
			if (this.mWidget != null)
			{
				return this.mWidget.color;
			}
			if (this.mLight != null)
			{
				return this.mLight.color;
			}
			if (this.mMat != null)
			{
				return this.mMat.color;
			}
			return Color.black;
		}
		set
		{
			if (this.mWidget != null)
			{
				this.mWidget.color = value;
			}
			if (this.mMat != null)
			{
				this.mMat.color = value;
			}
			if (this.mLight != null)
			{
				this.mLight.color = value;
				this.mLight.enabled = (value.r + value.g + value.b > 0.01f);
			}
		}
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x0001BB58 File Offset: 0x00019D58
	private void Awake()
	{
		this.mWidget = base.GetComponentInChildren<UIWidget>();
		Renderer renderer = base.renderer;
		if (renderer != null)
		{
			this.mMat = renderer.material;
		}
		this.mLight = base.light;
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x0001BB9C File Offset: 0x00019D9C
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.color = Color.Lerp(this.from, this.to, factor);
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x0001BBB8 File Offset: 0x00019DB8
	public static TweenColor Begin(GameObject go, float duration, Color color)
	{
		TweenColor tweenColor = UITweener.Begin<TweenColor>(go, duration);
		tweenColor.from = tweenColor.color;
		tweenColor.to = color;
		if (duration <= 0f)
		{
			tweenColor.Sample(1f, true);
			tweenColor.enabled = false;
		}
		return tweenColor;
	}

	// Token: 0x040003C0 RID: 960
	public Color from = Color.white;

	// Token: 0x040003C1 RID: 961
	public Color to = Color.white;

	// Token: 0x040003C2 RID: 962
	private Transform mTrans;

	// Token: 0x040003C3 RID: 963
	private UIWidget mWidget;

	// Token: 0x040003C4 RID: 964
	private Material mMat;

	// Token: 0x040003C5 RID: 965
	private Light mLight;
}
