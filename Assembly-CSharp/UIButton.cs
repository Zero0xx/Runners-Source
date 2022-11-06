using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000075 RID: 117
[AddComponentMenu("NGUI/Interaction/Button")]
public class UIButton : UIButtonColor
{
	// Token: 0x06000317 RID: 791 RVA: 0x0000DE34 File Offset: 0x0000C034
	protected override void OnEnable()
	{
		if (this.isEnabled)
		{
			if (this.mStarted)
			{
				if (this.mHighlighted)
				{
					base.OnEnable();
				}
				else
				{
					this.UpdateColor(true, false);
				}
			}
		}
		else
		{
			this.UpdateColor(false, true);
		}
	}

	// Token: 0x06000318 RID: 792 RVA: 0x0000DE84 File Offset: 0x0000C084
	protected override void OnDisable()
	{
		if (this.mStarted)
		{
			this.UpdateColor(false, false);
		}
	}

	// Token: 0x06000319 RID: 793 RVA: 0x0000DE9C File Offset: 0x0000C09C
	public override void OnHover(bool isOver)
	{
		if (this.isEnabled)
		{
			base.OnHover(isOver);
		}
	}

	// Token: 0x0600031A RID: 794 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
	public override void OnPress(bool isPressed)
	{
		if (this.isEnabled)
		{
			base.OnPress(isPressed);
		}
	}

	// Token: 0x0600031B RID: 795 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
	private void OnClick()
	{
		if (this.isEnabled)
		{
			UIButton.current = this;
			EventDelegate.Execute(this.onClick);
			UIButton.current = null;
		}
	}

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x0600031C RID: 796 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
	// (set) Token: 0x0600031D RID: 797 RVA: 0x0000DF2C File Offset: 0x0000C12C
	public bool isEnabled
	{
		get
		{
			if (!base.enabled)
			{
				return false;
			}
			Collider collider = base.collider;
			return collider && collider.enabled;
		}
		set
		{
			Collider collider = base.collider;
			if (collider != null)
			{
				collider.enabled = value;
			}
			base.enabled = value;
		}
	}

	// Token: 0x0600031E RID: 798 RVA: 0x0000DF5C File Offset: 0x0000C15C
	public void UpdateColor(bool shouldBeEnabled, bool immediate)
	{
		if (this.tweenTarget)
		{
			if (!this.mStarted)
			{
				this.mStarted = true;
				base.Init();
			}
			Color color = (!shouldBeEnabled) ? this.disabledColor : base.defaultColor;
			TweenColor tweenColor = TweenColor.Begin(this.tweenTarget, 0.15f, color);
			if (immediate)
			{
				tweenColor.color = color;
				tweenColor.enabled = false;
			}
		}
	}

	// Token: 0x040001CB RID: 459
	public static UIButton current;

	// Token: 0x040001CC RID: 460
	public Color disabledColor = Color.grey;

	// Token: 0x040001CD RID: 461
	public List<EventDelegate> onClick = new List<EventDelegate>();
}
