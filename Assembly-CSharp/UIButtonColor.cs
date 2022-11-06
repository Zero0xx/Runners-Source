using System;
using UnityEngine;

// Token: 0x02000077 RID: 119
[AddComponentMenu("NGUI/Interaction/Button Color")]
public class UIButtonColor : UIWidgetContainer
{
	// Token: 0x17000071 RID: 113
	// (get) Token: 0x06000322 RID: 802 RVA: 0x0000E050 File Offset: 0x0000C250
	// (set) Token: 0x06000323 RID: 803 RVA: 0x0000E060 File Offset: 0x0000C260
	public Color defaultColor
	{
		get
		{
			this.Start();
			return this.mColor;
		}
		set
		{
			this.Start();
			this.mColor = value;
		}
	}

	// Token: 0x06000324 RID: 804 RVA: 0x0000E070 File Offset: 0x0000C270
	private void Start()
	{
		if (!this.mStarted)
		{
			this.Init();
			this.mStarted = true;
		}
	}

	// Token: 0x06000325 RID: 805 RVA: 0x0000E08C File Offset: 0x0000C28C
	protected virtual void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000326 RID: 806 RVA: 0x0000E0B8 File Offset: 0x0000C2B8
	protected virtual void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenColor component = this.tweenTarget.GetComponent<TweenColor>();
			if (component != null)
			{
				component.color = this.mColor;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06000327 RID: 807 RVA: 0x0000E10C File Offset: 0x0000C30C
	protected void Init()
	{
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.gameObject;
		}
		UIWidget component = this.tweenTarget.GetComponent<UIWidget>();
		if (component != null)
		{
			this.mColor = component.color;
		}
		else
		{
			Renderer renderer = this.tweenTarget.renderer;
			if (renderer != null)
			{
				this.mColor = renderer.material.color;
			}
			else
			{
				Light light = this.tweenTarget.light;
				if (light != null)
				{
					this.mColor = light.color;
				}
				else
				{
					global::Debug.LogWarning(NGUITools.GetHierarchy(base.gameObject) + " has nothing for UIButtonColor to color", this);
					base.enabled = false;
				}
			}
		}
		this.OnEnable();
	}

	// Token: 0x06000328 RID: 808 RVA: 0x0000E1E0 File Offset: 0x0000C3E0
	public virtual void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenColor.Begin(this.tweenTarget, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mColor : this.hover) : this.pressed);
		}
	}

	// Token: 0x06000329 RID: 809 RVA: 0x0000E250 File Offset: 0x0000C450
	public virtual void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenColor.Begin(this.tweenTarget, this.duration, (!isOver) ? this.mColor : this.hover);
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x040001D0 RID: 464
	public GameObject tweenTarget;

	// Token: 0x040001D1 RID: 465
	public Color hover = new Color(0.6f, 1f, 0.2f, 1f);

	// Token: 0x040001D2 RID: 466
	public Color pressed = Color.grey;

	// Token: 0x040001D3 RID: 467
	public float duration = 0.2f;

	// Token: 0x040001D4 RID: 468
	protected Color mColor;

	// Token: 0x040001D5 RID: 469
	protected bool mStarted;

	// Token: 0x040001D6 RID: 470
	protected bool mHighlighted;
}
