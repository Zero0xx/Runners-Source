using System;
using UnityEngine;

// Token: 0x020000B3 RID: 179
[AddComponentMenu("NGUI/Internal/Event Listener")]
public class UIEventListener : MonoBehaviour
{
	// Token: 0x06000516 RID: 1302 RVA: 0x00019FB4 File Offset: 0x000181B4
	private void OnSubmit()
	{
		if (this.onSubmit != null)
		{
			this.onSubmit(base.gameObject);
		}
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x00019FD4 File Offset: 0x000181D4
	private void OnClick()
	{
		if (this.onClick != null)
		{
			this.onClick(base.gameObject);
		}
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x00019FF4 File Offset: 0x000181F4
	private void OnDoubleClick()
	{
		if (this.onDoubleClick != null)
		{
			this.onDoubleClick(base.gameObject);
		}
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x0001A014 File Offset: 0x00018214
	private void OnHover(bool isOver)
	{
		if (this.onHover != null)
		{
			this.onHover(base.gameObject, isOver);
		}
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x0001A034 File Offset: 0x00018234
	private void OnPress(bool isPressed)
	{
		if (this.onPress != null)
		{
			this.onPress(base.gameObject, isPressed);
		}
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x0001A054 File Offset: 0x00018254
	private void OnSelect(bool selected)
	{
		if (this.onSelect != null)
		{
			this.onSelect(base.gameObject, selected);
		}
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x0001A074 File Offset: 0x00018274
	private void OnScroll(float delta)
	{
		if (this.onScroll != null)
		{
			this.onScroll(base.gameObject, delta);
		}
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x0001A094 File Offset: 0x00018294
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag != null)
		{
			this.onDrag(base.gameObject, delta);
		}
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x0001A0B4 File Offset: 0x000182B4
	private void OnDrop(GameObject go)
	{
		if (this.onDrop != null)
		{
			this.onDrop(base.gameObject, go);
		}
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x0001A0D4 File Offset: 0x000182D4
	private void OnInput(string text)
	{
		if (this.onInput != null)
		{
			this.onInput(base.gameObject, text);
		}
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x0001A0F4 File Offset: 0x000182F4
	private void OnKey(KeyCode key)
	{
		if (this.onKey != null)
		{
			this.onKey(base.gameObject, key);
		}
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x0001A114 File Offset: 0x00018314
	public static UIEventListener Get(GameObject go)
	{
		UIEventListener uieventListener = go.GetComponent<UIEventListener>();
		if (uieventListener == null)
		{
			uieventListener = go.AddComponent<UIEventListener>();
		}
		return uieventListener;
	}

	// Token: 0x04000379 RID: 889
	public object parameter;

	// Token: 0x0400037A RID: 890
	public UIEventListener.VoidDelegate onSubmit;

	// Token: 0x0400037B RID: 891
	public UIEventListener.VoidDelegate onClick;

	// Token: 0x0400037C RID: 892
	public UIEventListener.VoidDelegate onDoubleClick;

	// Token: 0x0400037D RID: 893
	public UIEventListener.BoolDelegate onHover;

	// Token: 0x0400037E RID: 894
	public UIEventListener.BoolDelegate onPress;

	// Token: 0x0400037F RID: 895
	public UIEventListener.BoolDelegate onSelect;

	// Token: 0x04000380 RID: 896
	public UIEventListener.FloatDelegate onScroll;

	// Token: 0x04000381 RID: 897
	public UIEventListener.VectorDelegate onDrag;

	// Token: 0x04000382 RID: 898
	public UIEventListener.ObjectDelegate onDrop;

	// Token: 0x04000383 RID: 899
	public UIEventListener.StringDelegate onInput;

	// Token: 0x04000384 RID: 900
	public UIEventListener.KeyCodeDelegate onKey;

	// Token: 0x02000A69 RID: 2665
	// (Invoke) Token: 0x060047DE RID: 18398
	public delegate void VoidDelegate(GameObject go);

	// Token: 0x02000A6A RID: 2666
	// (Invoke) Token: 0x060047E2 RID: 18402
	public delegate void BoolDelegate(GameObject go, bool state);

	// Token: 0x02000A6B RID: 2667
	// (Invoke) Token: 0x060047E6 RID: 18406
	public delegate void FloatDelegate(GameObject go, float delta);

	// Token: 0x02000A6C RID: 2668
	// (Invoke) Token: 0x060047EA RID: 18410
	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	// Token: 0x02000A6D RID: 2669
	// (Invoke) Token: 0x060047EE RID: 18414
	public delegate void StringDelegate(GameObject go, string text);

	// Token: 0x02000A6E RID: 2670
	// (Invoke) Token: 0x060047F2 RID: 18418
	public delegate void ObjectDelegate(GameObject go, GameObject draggedObject);

	// Token: 0x02000A6F RID: 2671
	// (Invoke) Token: 0x060047F6 RID: 18422
	public delegate void KeyCodeDelegate(GameObject go, KeyCode key);
}
