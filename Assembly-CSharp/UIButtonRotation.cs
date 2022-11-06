using System;
using UnityEngine;

// Token: 0x0200007D RID: 125
[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : MonoBehaviour
{
	// Token: 0x0600033F RID: 831 RVA: 0x0000E9E0 File Offset: 0x0000CBE0
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mRot = this.tweenTarget.localRotation;
		}
	}

	// Token: 0x06000340 RID: 832 RVA: 0x0000EA30 File Offset: 0x0000CC30
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000341 RID: 833 RVA: 0x0000EA5C File Offset: 0x0000CC5C
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenRotation component = this.tweenTarget.GetComponent<TweenRotation>();
			if (component != null)
			{
				component.rotation = this.mRot;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06000342 RID: 834 RVA: 0x0000EAB0 File Offset: 0x0000CCB0
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mRot : (this.mRot * Quaternion.Euler(this.hover))) : (this.mRot * Quaternion.Euler(this.pressed))).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000343 RID: 835 RVA: 0x0000EB48 File Offset: 0x0000CD48
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mRot : (this.mRot * Quaternion.Euler(this.hover))).method = UITweener.Method.EaseInOut;
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x040001F2 RID: 498
	public Transform tweenTarget;

	// Token: 0x040001F3 RID: 499
	public Vector3 hover = Vector3.zero;

	// Token: 0x040001F4 RID: 500
	public Vector3 pressed = Vector3.zero;

	// Token: 0x040001F5 RID: 501
	public float duration = 0.2f;

	// Token: 0x040001F6 RID: 502
	private Quaternion mRot;

	// Token: 0x040001F7 RID: 503
	private bool mStarted;

	// Token: 0x040001F8 RID: 504
	private bool mHighlighted;
}
