using System;
using UnityEngine;

// Token: 0x02000088 RID: 136
[AddComponentMenu("NGUI/Interaction/Forward Events")]
public class UIForwardEvents : MonoBehaviour
{
	// Token: 0x06000384 RID: 900 RVA: 0x00011170 File Offset: 0x0000F370
	private void OnHover(bool isOver)
	{
		if (this.onHover && this.target != null)
		{
			this.target.SendMessage("OnHover", isOver, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000385 RID: 901 RVA: 0x000111A8 File Offset: 0x0000F3A8
	private void OnPress(bool pressed)
	{
		if (this.onPress && this.target != null)
		{
			this.target.SendMessage("OnPress", pressed, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000386 RID: 902 RVA: 0x000111E0 File Offset: 0x0000F3E0
	private void OnClick()
	{
		if (this.onClick && this.target != null)
		{
			this.target.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000387 RID: 903 RVA: 0x00011210 File Offset: 0x0000F410
	private void OnDoubleClick()
	{
		if (this.onDoubleClick && this.target != null)
		{
			this.target.SendMessage("OnDoubleClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000388 RID: 904 RVA: 0x00011240 File Offset: 0x0000F440
	private void OnSelect(bool selected)
	{
		if (this.onSelect && this.target != null)
		{
			this.target.SendMessage("OnSelect", selected, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000389 RID: 905 RVA: 0x00011278 File Offset: 0x0000F478
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag && this.target != null)
		{
			this.target.SendMessage("OnDrag", delta, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0600038A RID: 906 RVA: 0x000112B0 File Offset: 0x0000F4B0
	private void OnDrop(GameObject go)
	{
		if (this.onDrop && this.target != null)
		{
			this.target.SendMessage("OnDrop", go, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0600038B RID: 907 RVA: 0x000112EC File Offset: 0x0000F4EC
	private void OnInput(string text)
	{
		if (this.onInput && this.target != null)
		{
			this.target.SendMessage("OnInput", text, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0600038C RID: 908 RVA: 0x00011328 File Offset: 0x0000F528
	private void OnSubmit()
	{
		if (this.onSubmit && this.target != null)
		{
			this.target.SendMessage("OnSubmit", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0600038D RID: 909 RVA: 0x00011358 File Offset: 0x0000F558
	private void OnScroll(float delta)
	{
		if (this.onScroll && this.target != null)
		{
			this.target.SendMessage("OnScroll", delta, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0400024B RID: 587
	public GameObject target;

	// Token: 0x0400024C RID: 588
	public bool onHover;

	// Token: 0x0400024D RID: 589
	public bool onPress;

	// Token: 0x0400024E RID: 590
	public bool onClick;

	// Token: 0x0400024F RID: 591
	public bool onDoubleClick;

	// Token: 0x04000250 RID: 592
	public bool onSelect;

	// Token: 0x04000251 RID: 593
	public bool onDrag;

	// Token: 0x04000252 RID: 594
	public bool onDrop;

	// Token: 0x04000253 RID: 595
	public bool onInput;

	// Token: 0x04000254 RID: 596
	public bool onSubmit;

	// Token: 0x04000255 RID: 597
	public bool onScroll;
}
