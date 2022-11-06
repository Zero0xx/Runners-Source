using System;
using UnityEngine;

// Token: 0x0200007C RID: 124
[AddComponentMenu("NGUI/Interaction/Button Offset")]
public class UIButtonOffset : MonoBehaviour
{
	// Token: 0x06000339 RID: 825 RVA: 0x0000E7E4 File Offset: 0x0000C9E4
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mPos = this.tweenTarget.localPosition;
		}
	}

	// Token: 0x0600033A RID: 826 RVA: 0x0000E834 File Offset: 0x0000CA34
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x0600033B RID: 827 RVA: 0x0000E860 File Offset: 0x0000CA60
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenPosition component = this.tweenTarget.GetComponent<TweenPosition>();
			if (component != null)
			{
				component.position = this.mPos;
				component.enabled = false;
			}
		}
	}

	// Token: 0x0600033C RID: 828 RVA: 0x0000E8B4 File Offset: 0x0000CAB4
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mPos : (this.mPos + this.hover)) : (this.mPos + this.pressed)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x0600033D RID: 829 RVA: 0x0000E944 File Offset: 0x0000CB44
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mPos : (this.mPos + this.hover)).method = UITweener.Method.EaseInOut;
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x040001EB RID: 491
	public Transform tweenTarget;

	// Token: 0x040001EC RID: 492
	public Vector3 hover = Vector3.zero;

	// Token: 0x040001ED RID: 493
	public Vector3 pressed = new Vector3(2f, -2f);

	// Token: 0x040001EE RID: 494
	public float duration = 0.2f;

	// Token: 0x040001EF RID: 495
	private Vector3 mPos;

	// Token: 0x040001F0 RID: 496
	private bool mStarted;

	// Token: 0x040001F1 RID: 497
	private bool mHighlighted;
}
