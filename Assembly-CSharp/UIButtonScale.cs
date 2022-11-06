using System;
using UnityEngine;

// Token: 0x0200007E RID: 126
[AddComponentMenu("NGUI/Interaction/Button Scale")]
public class UIButtonScale : MonoBehaviour
{
	// Token: 0x06000345 RID: 837 RVA: 0x0000EC10 File Offset: 0x0000CE10
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mScale = this.tweenTarget.localScale;
		}
	}

	// Token: 0x06000346 RID: 838 RVA: 0x0000EC60 File Offset: 0x0000CE60
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000347 RID: 839 RVA: 0x0000EC8C File Offset: 0x0000CE8C
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenScale component = this.tweenTarget.GetComponent<TweenScale>();
			if (component != null)
			{
				component.scale = this.mScale;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06000348 RID: 840 RVA: 0x0000ECE0 File Offset: 0x0000CEE0
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenScale.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mScale : Vector3.Scale(this.mScale, this.hover)) : Vector3.Scale(this.mScale, this.pressed)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000349 RID: 841 RVA: 0x0000ED70 File Offset: 0x0000CF70
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenScale.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mScale : Vector3.Scale(this.mScale, this.hover)).method = UITweener.Method.EaseInOut;
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x040001F9 RID: 505
	public Transform tweenTarget;

	// Token: 0x040001FA RID: 506
	public Vector3 hover = new Vector3(1.1f, 1.1f, 1.1f);

	// Token: 0x040001FB RID: 507
	public Vector3 pressed = new Vector3(1.05f, 1.05f, 1.05f);

	// Token: 0x040001FC RID: 508
	public float duration = 0.2f;

	// Token: 0x040001FD RID: 509
	private Vector3 mScale;

	// Token: 0x040001FE RID: 510
	private bool mStarted;

	// Token: 0x040001FF RID: 511
	private bool mHighlighted;
}
