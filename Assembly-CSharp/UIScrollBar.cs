using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000095 RID: 149
[AddComponentMenu("NGUI/Interaction/Scroll Bar")]
public class UIScrollBar : UIWidgetContainer
{
	// Token: 0x17000081 RID: 129
	// (get) Token: 0x060003D9 RID: 985 RVA: 0x00013470 File Offset: 0x00011670
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x060003DA RID: 986 RVA: 0x00013498 File Offset: 0x00011698
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = NGUITools.FindCameraForLayer(base.gameObject.layer);
			}
			return this.mCam;
		}
	}

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x060003DB RID: 987 RVA: 0x000134C8 File Offset: 0x000116C8
	// (set) Token: 0x060003DC RID: 988 RVA: 0x000134D0 File Offset: 0x000116D0
	public UISprite background
	{
		get
		{
			return this.mBG;
		}
		set
		{
			if (this.mBG != value)
			{
				this.mBG = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x17000084 RID: 132
	// (get) Token: 0x060003DD RID: 989 RVA: 0x000134F4 File Offset: 0x000116F4
	// (set) Token: 0x060003DE RID: 990 RVA: 0x000134FC File Offset: 0x000116FC
	public UISprite foreground
	{
		get
		{
			return this.mFG;
		}
		set
		{
			if (this.mFG != value)
			{
				this.mFG = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x060003DF RID: 991 RVA: 0x00013520 File Offset: 0x00011720
	// (set) Token: 0x060003E0 RID: 992 RVA: 0x00013528 File Offset: 0x00011728
	public UIScrollBar.Direction direction
	{
		get
		{
			return this.mDir;
		}
		set
		{
			if (this.mDir != value)
			{
				this.mDir = value;
				this.mIsDirty = true;
				if (this.mBG != null)
				{
					int width = this.mBG.width;
					int height = this.mBG.height;
					if ((this.mDir == UIScrollBar.Direction.Vertical && width > height) || (this.mDir == UIScrollBar.Direction.Horizontal && width < height))
					{
						this.mBG.width = height;
						this.mBG.height = width;
						this.ForceUpdate();
						if (this.mBG.collider != null)
						{
							NGUITools.AddWidgetCollider(this.mBG.gameObject);
						}
						if (this.mFG.collider != null)
						{
							NGUITools.AddWidgetCollider(this.mFG.gameObject);
						}
					}
				}
			}
		}
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x060003E1 RID: 993 RVA: 0x0001360C File Offset: 0x0001180C
	// (set) Token: 0x060003E2 RID: 994 RVA: 0x00013614 File Offset: 0x00011814
	public bool inverted
	{
		get
		{
			return this.mInverted;
		}
		set
		{
			if (this.mInverted != value)
			{
				this.mInverted = value;
				this.mIsDirty = true;
			}
		}
	}

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x060003E3 RID: 995 RVA: 0x00013630 File Offset: 0x00011830
	// (set) Token: 0x060003E4 RID: 996 RVA: 0x00013638 File Offset: 0x00011838
	public float value
	{
		get
		{
			return this.mScroll;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mScroll != num)
			{
				this.mScroll = num;
				this.mIsDirty = true;
				if (this.onChange != null)
				{
					UIScrollBar.current = this;
					EventDelegate.Execute(this.onChange);
					UIScrollBar.current = null;
				}
			}
		}
	}

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x060003E5 RID: 997 RVA: 0x00013688 File Offset: 0x00011888
	// (set) Token: 0x060003E6 RID: 998 RVA: 0x00013690 File Offset: 0x00011890
	[Obsolete("Use 'value' instead")]
	public float scrollValue
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x060003E7 RID: 999 RVA: 0x0001369C File Offset: 0x0001189C
	// (set) Token: 0x060003E8 RID: 1000 RVA: 0x000136A4 File Offset: 0x000118A4
	public float barSize
	{
		get
		{
			return this.mSize;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mSize != num)
			{
				this.mSize = num;
				this.mIsDirty = true;
				if (this.onChange != null)
				{
					UIScrollBar.current = this;
					EventDelegate.Execute(this.onChange);
					UIScrollBar.current = null;
				}
			}
		}
	}

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000136F4 File Offset: 0x000118F4
	// (set) Token: 0x060003EA RID: 1002 RVA: 0x00013740 File Offset: 0x00011940
	public float alpha
	{
		get
		{
			if (this.mFG != null)
			{
				return this.mFG.alpha;
			}
			if (this.mBG != null)
			{
				return this.mBG.alpha;
			}
			return 0f;
		}
		set
		{
			if (this.mFG != null)
			{
				this.mFG.alpha = value;
				NGUITools.SetActiveSelf(this.mFG.gameObject, this.mFG.alpha > 0.001f);
			}
			if (this.mBG != null)
			{
				this.mBG.alpha = value;
				NGUITools.SetActiveSelf(this.mBG.gameObject, this.mBG.alpha > 0.001f);
			}
		}
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x000137CC File Offset: 0x000119CC
	private void CenterOnPos(Vector2 localPos)
	{
		if (this.mBG == null || this.mFG == null)
		{
			return;
		}
		Bounds bounds = NGUIMath.CalculateRelativeInnerBounds(this.cachedTransform, this.mBG);
		Bounds bounds2 = NGUIMath.CalculateRelativeInnerBounds(this.cachedTransform, this.mFG);
		if (this.mDir == UIScrollBar.Direction.Horizontal)
		{
			float num = bounds.size.x - bounds2.size.x;
			float num2 = num * 0.5f;
			float num3 = bounds.center.x - num2;
			float num4 = (num <= 0f) ? 0f : ((localPos.x - num3) / num);
			this.value = ((!this.mInverted) ? num4 : (1f - num4));
		}
		else
		{
			float num5 = bounds.size.y - bounds2.size.y;
			float num6 = num5 * 0.5f;
			float num7 = bounds.center.y - num6;
			float num8 = (num5 <= 0f) ? 0f : (1f - (localPos.y - num7) / num5);
			this.value = ((!this.mInverted) ? num8 : (1f - num8));
		}
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x00013940 File Offset: 0x00011B40
	private void Reposition(Vector2 screenPos)
	{
		Transform cachedTransform = this.cachedTransform;
		Plane plane = new Plane(cachedTransform.rotation * Vector3.back, cachedTransform.position);
		Ray ray = this.cachedCamera.ScreenPointToRay(screenPos);
		float distance;
		if (!plane.Raycast(ray, out distance))
		{
			return;
		}
		this.CenterOnPos(cachedTransform.InverseTransformPoint(ray.GetPoint(distance)));
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x000139AC File Offset: 0x00011BAC
	private void OnPressBackground(GameObject go, bool isPressed)
	{
		this.mCam = UICamera.currentCamera;
		this.Reposition(UICamera.lastTouchPosition);
		if (!isPressed && this.onDragFinished != null)
		{
			this.onDragFinished();
		}
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x000139EC File Offset: 0x00011BEC
	private void OnDragBackground(GameObject go, Vector2 delta)
	{
		this.mCam = UICamera.currentCamera;
		this.Reposition(UICamera.lastTouchPosition);
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x00013A04 File Offset: 0x00011C04
	private void OnPressForeground(GameObject go, bool isPressed)
	{
		if (isPressed)
		{
			this.mCam = UICamera.currentCamera;
			Bounds bounds = NGUIMath.CalculateAbsoluteWidgetBounds(this.mFG.cachedTransform);
			this.mScreenPos = this.mCam.WorldToScreenPoint(bounds.center);
		}
		else if (this.onDragFinished != null)
		{
			this.onDragFinished();
		}
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00013A6C File Offset: 0x00011C6C
	private void OnDragForeground(GameObject go, Vector2 delta)
	{
		this.mCam = UICamera.currentCamera;
		this.Reposition(this.mScreenPos + UICamera.currentTouch.totalDelta);
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x00013AA0 File Offset: 0x00011CA0
	private void Start()
	{
		if (this.background != null && this.background.collider != null)
		{
			UIEventListener uieventListener = UIEventListener.Get(this.background.gameObject);
			UIEventListener uieventListener2 = uieventListener;
			uieventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener2.onPress, new UIEventListener.BoolDelegate(this.OnPressBackground));
			UIEventListener uieventListener3 = uieventListener;
			uieventListener3.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener3.onDrag, new UIEventListener.VectorDelegate(this.OnDragBackground));
		}
		if (this.foreground != null && this.foreground.collider != null)
		{
			UIEventListener uieventListener4 = UIEventListener.Get(this.foreground.gameObject);
			UIEventListener uieventListener5 = uieventListener4;
			uieventListener5.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uieventListener5.onPress, new UIEventListener.BoolDelegate(this.OnPressForeground));
			UIEventListener uieventListener6 = uieventListener4;
			uieventListener6.onDrag = (UIEventListener.VectorDelegate)Delegate.Combine(uieventListener6.onDrag, new UIEventListener.VectorDelegate(this.OnDragForeground));
		}
		if (this.onChange != null)
		{
			UIScrollBar.current = this;
			EventDelegate.Execute(this.onChange);
			UIScrollBar.current = null;
		}
		this.ForceUpdate();
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x00013BD0 File Offset: 0x00011DD0
	private void Update()
	{
		if (this.mIsDirty)
		{
			this.ForceUpdate();
		}
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x00013BE4 File Offset: 0x00011DE4
	public void ForceUpdate()
	{
		this.mIsDirty = false;
		if (this.mBG != null && this.mFG != null)
		{
			this.mSize = Mathf.Clamp01(this.mSize);
			this.mScroll = Mathf.Clamp01(this.mScroll);
			Vector4 border = this.mBG.border;
			Vector4 border2 = this.mFG.border;
			Vector2 vector = new Vector2(Mathf.Max(0f, (float)this.mBG.width - border.x - border.z), Mathf.Max(0f, (float)this.mBG.height - border.y - border.w));
			float num = (!this.mInverted) ? this.mScroll : (1f - this.mScroll);
			if (this.mDir == UIScrollBar.Direction.Horizontal)
			{
				Vector2 vector2 = new Vector2(vector.x * this.mSize, vector.y);
				this.mFG.pivot = UIWidget.Pivot.Left;
				this.mBG.pivot = UIWidget.Pivot.Left;
				this.mBG.cachedTransform.localPosition = Vector3.zero;
				this.mFG.cachedTransform.localPosition = new Vector3(border.x - border2.x + (vector.x - vector2.x) * num, 0f, 0f);
				this.mFG.width = Mathf.RoundToInt(vector2.x + border2.x + border2.z);
				this.mFG.height = Mathf.RoundToInt(vector2.y + border2.y + border2.w);
				if (num < 0.999f && num > 0.001f)
				{
					this.mFG.MakePixelPerfect();
				}
				if (this.mFG.collider != null)
				{
					NGUITools.AddWidgetCollider(this.mFG.gameObject);
				}
			}
			else
			{
				Vector2 vector3 = new Vector2(vector.x, vector.y * this.mSize);
				this.mFG.pivot = UIWidget.Pivot.Top;
				this.mBG.pivot = UIWidget.Pivot.Top;
				this.mBG.cachedTransform.localPosition = Vector3.zero;
				this.mFG.cachedTransform.localPosition = new Vector3(0f, -border.y + border2.y - (vector.y - vector3.y) * num, 0f);
				this.mFG.width = Mathf.RoundToInt(vector3.x + border2.x + border2.z);
				this.mFG.height = Mathf.RoundToInt(vector3.y + border2.y + border2.w);
				if (num < 0.999f && num > 0.001f)
				{
					this.mFG.MakePixelPerfect();
				}
				if (this.mFG.collider != null)
				{
					NGUITools.AddWidgetCollider(this.mFG.gameObject);
				}
			}
		}
	}

	// Token: 0x040002B9 RID: 697
	public static UIScrollBar current;

	// Token: 0x040002BA RID: 698
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x040002BB RID: 699
	public UIScrollBar.OnDragFinished onDragFinished;

	// Token: 0x040002BC RID: 700
	[SerializeField]
	[HideInInspector]
	private UISprite mBG;

	// Token: 0x040002BD RID: 701
	[SerializeField]
	[HideInInspector]
	private UISprite mFG;

	// Token: 0x040002BE RID: 702
	[HideInInspector]
	[SerializeField]
	private UIScrollBar.Direction mDir;

	// Token: 0x040002BF RID: 703
	[HideInInspector]
	[SerializeField]
	private bool mInverted;

	// Token: 0x040002C0 RID: 704
	[HideInInspector]
	[SerializeField]
	private float mScroll;

	// Token: 0x040002C1 RID: 705
	[HideInInspector]
	[SerializeField]
	private float mSize = 1f;

	// Token: 0x040002C2 RID: 706
	private Transform mTrans;

	// Token: 0x040002C3 RID: 707
	private bool mIsDirty;

	// Token: 0x040002C4 RID: 708
	private Camera mCam;

	// Token: 0x040002C5 RID: 709
	private Vector2 mScreenPos = Vector2.zero;

	// Token: 0x02000096 RID: 150
	public enum Direction
	{
		// Token: 0x040002C7 RID: 711
		Horizontal,
		// Token: 0x040002C8 RID: 712
		Vertical
	}

	// Token: 0x02000A65 RID: 2661
	// (Invoke) Token: 0x060047CE RID: 18382
	public delegate void OnDragFinished();
}
