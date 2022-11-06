using System;
using UnityEngine;

// Token: 0x0200007A RID: 122
[AddComponentMenu("NGUI/Interaction/Button Message")]
public class UIButtonMessage : MonoBehaviour
{
	// Token: 0x06000331 RID: 817 RVA: 0x0000E610 File Offset: 0x0000C810
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x06000332 RID: 818 RVA: 0x0000E61C File Offset: 0x0000C81C
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000333 RID: 819 RVA: 0x0000E648 File Offset: 0x0000C848
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if ((isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOver) || (!isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOut))
			{
				this.Send();
			}
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x06000334 RID: 820 RVA: 0x0000E694 File Offset: 0x0000C894
	private void OnPress(bool isPressed)
	{
		if (base.enabled && ((isPressed && this.trigger == UIButtonMessage.Trigger.OnPress) || (!isPressed && this.trigger == UIButtonMessage.Trigger.OnRelease)))
		{
			this.Send();
		}
	}

	// Token: 0x06000335 RID: 821 RVA: 0x0000E6CC File Offset: 0x0000C8CC
	private void OnClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnClick)
		{
			this.Send();
		}
	}

	// Token: 0x06000336 RID: 822 RVA: 0x0000E6EC File Offset: 0x0000C8EC
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnDoubleClick)
		{
			this.Send();
		}
	}

	// Token: 0x06000337 RID: 823 RVA: 0x0000E70C File Offset: 0x0000C90C
	private void Send()
	{
		if (string.IsNullOrEmpty(this.functionName))
		{
			return;
		}
		if (this.target == null)
		{
			this.target = base.gameObject;
		}
		if (this.includeChildren)
		{
			Transform[] componentsInChildren = this.target.GetComponentsInChildren<Transform>();
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.SendMessage(this.functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
				i++;
			}
		}
		else
		{
			this.target.SendMessage(this.functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x040001DE RID: 478
	public GameObject target;

	// Token: 0x040001DF RID: 479
	public string functionName;

	// Token: 0x040001E0 RID: 480
	public UIButtonMessage.Trigger trigger;

	// Token: 0x040001E1 RID: 481
	public bool includeChildren;

	// Token: 0x040001E2 RID: 482
	private bool mStarted;

	// Token: 0x040001E3 RID: 483
	private bool mHighlighted;

	// Token: 0x0200007B RID: 123
	public enum Trigger
	{
		// Token: 0x040001E5 RID: 485
		OnClick,
		// Token: 0x040001E6 RID: 486
		OnMouseOver,
		// Token: 0x040001E7 RID: 487
		OnMouseOut,
		// Token: 0x040001E8 RID: 488
		OnPress,
		// Token: 0x040001E9 RID: 489
		OnRelease,
		// Token: 0x040001EA RID: 490
		OnDoubleClick
	}
}
