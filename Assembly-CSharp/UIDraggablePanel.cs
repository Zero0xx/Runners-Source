using System;
using UnityEngine;

// Token: 0x02000085 RID: 133
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Interaction/Draggable Panel")]
[ExecuteInEditMode]
public class UIDraggablePanel : MonoBehaviour
{
	// Token: 0x17000074 RID: 116
	// (get) Token: 0x0600036B RID: 875 RVA: 0x0000FF9C File Offset: 0x0000E19C
	public UIPanel panel
	{
		get
		{
			return this.mPanel;
		}
	}

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x0600036C RID: 876 RVA: 0x0000FFA4 File Offset: 0x0000E1A4
	public Bounds bounds
	{
		get
		{
			if (!this.mCalculatedBounds)
			{
				this.mCalculatedBounds = true;
				this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.mTrans, this.mTrans);
			}
			return this.mBounds;
		}
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x0600036D RID: 877 RVA: 0x0000FFD8 File Offset: 0x0000E1D8
	public bool shouldMoveHorizontally
	{
		get
		{
			float num = this.bounds.size.x;
			if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				num += this.mPanel.clipSoftness.x * 2f;
			}
			return num > this.mPanel.clipRange.z;
		}
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x0600036E RID: 878 RVA: 0x00010040 File Offset: 0x0000E240
	public bool shouldMoveVertically
	{
		get
		{
			float num = this.bounds.size.y;
			if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				num += this.mPanel.clipSoftness.y * 2f;
			}
			return num > this.mPanel.clipRange.w;
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x0600036F RID: 879 RVA: 0x000100A8 File Offset: 0x0000E2A8
	private bool shouldMove
	{
		get
		{
			if (!this.disableDragIfFits)
			{
				return true;
			}
			if (this.mPanel == null)
			{
				this.mPanel = base.GetComponent<UIPanel>();
			}
			Vector4 clipRange = this.mPanel.clipRange;
			Bounds bounds = this.bounds;
			float num = (clipRange.z != 0f) ? (clipRange.z * 0.5f) : ((float)Screen.width);
			float num2 = (clipRange.w != 0f) ? (clipRange.w * 0.5f) : ((float)Screen.height);
			if (!Mathf.Approximately(this.scale.x, 0f))
			{
				if (bounds.min.x < clipRange.x - num)
				{
					return true;
				}
				if (bounds.max.x > clipRange.x + num)
				{
					return true;
				}
			}
			if (!Mathf.Approximately(this.scale.y, 0f))
			{
				if (bounds.min.y < clipRange.y - num2)
				{
					return true;
				}
				if (bounds.max.y > clipRange.y + num2)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x06000370 RID: 880 RVA: 0x000101FC File Offset: 0x0000E3FC
	// (set) Token: 0x06000371 RID: 881 RVA: 0x00010204 File Offset: 0x0000E404
	public Vector3 currentMomentum
	{
		get
		{
			return this.mMomentum;
		}
		set
		{
			this.mMomentum = value;
			this.mShouldMove = true;
		}
	}

	// Token: 0x06000372 RID: 882 RVA: 0x00010214 File Offset: 0x0000E414
	private void Awake()
	{
		this.mTrans = base.transform;
		this.mPanel = base.GetComponent<UIPanel>();
		if (Application.isPlaying)
		{
			UIPanel uipanel = this.mPanel;
			uipanel.onChange = (UIPanel.OnChangeDelegate)Delegate.Combine(uipanel.onChange, new UIPanel.OnChangeDelegate(this.OnPanelChange));
		}
	}

	// Token: 0x06000373 RID: 883 RVA: 0x0001026C File Offset: 0x0000E46C
	private void OnDestroy()
	{
		if (Application.isPlaying && this.mPanel != null)
		{
			UIPanel uipanel = this.mPanel;
			uipanel.onChange = (UIPanel.OnChangeDelegate)Delegate.Remove(uipanel.onChange, new UIPanel.OnChangeDelegate(this.OnPanelChange));
		}
	}

	// Token: 0x06000374 RID: 884 RVA: 0x000102BC File Offset: 0x0000E4BC
	private void OnPanelChange()
	{
		this.UpdateScrollbars(true);
	}

	// Token: 0x06000375 RID: 885 RVA: 0x000102C8 File Offset: 0x0000E4C8
	private void Start()
	{
		if (Application.isPlaying)
		{
			this.UpdateScrollbars(true);
			if (this.horizontalScrollBar != null)
			{
				this.horizontalScrollBar.onChange.Add(new EventDelegate(new EventDelegate.Callback(this.OnHorizontalBar)));
				this.horizontalScrollBar.alpha = ((this.showScrollBars != UIDraggablePanel.ShowCondition.Always && !this.shouldMoveHorizontally) ? 0f : 1f);
			}
			if (this.verticalScrollBar != null)
			{
				this.verticalScrollBar.onChange.Add(new EventDelegate(new EventDelegate.Callback(this.OnVerticalBar)));
				this.verticalScrollBar.alpha = ((this.showScrollBars != UIDraggablePanel.ShowCondition.Always && !this.shouldMoveVertically) ? 0f : 1f);
			}
		}
	}

	// Token: 0x06000376 RID: 886 RVA: 0x000103AC File Offset: 0x0000E5AC
	public bool RestrictWithinBounds(bool instant)
	{
		Vector3 vector = this.mPanel.CalculateConstrainOffset(this.bounds.min, this.bounds.max);
		if (vector.magnitude > 0.001f)
		{
			if (!instant && this.dragEffect == UIDraggablePanel.DragEffect.MomentumAndSpring)
			{
				SpringPanel.Begin(this.mPanel.gameObject, this.mTrans.localPosition + vector, 13f);
			}
			else
			{
				this.MoveRelative(vector);
				this.mMomentum = Vector3.zero;
				this.mScroll = 0f;
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000377 RID: 887 RVA: 0x0001045C File Offset: 0x0000E65C
	public void DisableSpring()
	{
		SpringPanel component = base.GetComponent<SpringPanel>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	// Token: 0x06000378 RID: 888 RVA: 0x00010484 File Offset: 0x0000E684
	public void UpdateScrollbars(bool recalculateBounds)
	{
		if (this.mPanel == null)
		{
			return;
		}
		if (this.horizontalScrollBar != null || this.verticalScrollBar != null)
		{
			if (recalculateBounds)
			{
				this.mCalculatedBounds = false;
				this.mShouldMove = this.shouldMove;
			}
			Bounds bounds = this.bounds;
			Vector2 a = bounds.min;
			Vector2 a2 = bounds.max;
			if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				Vector2 clipSoftness = this.mPanel.clipSoftness;
				a -= clipSoftness;
				a2 += clipSoftness;
			}
			if (this.horizontalScrollBar != null && a2.x > a.x)
			{
				Vector4 clipRange = this.mPanel.clipRange;
				float num = clipRange.z * 0.5f;
				float num2 = clipRange.x - num - bounds.min.x;
				float num3 = bounds.max.x - num - clipRange.x;
				float num4 = a2.x - a.x;
				num2 = Mathf.Clamp01(num2 / num4);
				num3 = Mathf.Clamp01(num3 / num4);
				float num5 = num2 + num3;
				this.mIgnoreCallbacks = true;
				this.horizontalScrollBar.barSize = 1f - num5;
				this.horizontalScrollBar.value = ((num5 <= 0.001f) ? 0f : (num2 / num5));
				this.mIgnoreCallbacks = false;
			}
			if (this.verticalScrollBar != null && a2.y > a.y)
			{
				Vector4 clipRange2 = this.mPanel.clipRange;
				float num6 = clipRange2.w * 0.5f;
				float num7 = clipRange2.y - num6 - a.y;
				float num8 = a2.y - num6 - clipRange2.y;
				float num9 = a2.y - a.y;
				num7 = Mathf.Clamp01(num7 / num9);
				num8 = Mathf.Clamp01(num8 / num9);
				float num10 = num7 + num8;
				this.mIgnoreCallbacks = true;
				this.verticalScrollBar.barSize = 1f - num10;
				this.verticalScrollBar.value = ((num10 <= 0.001f) ? 0f : (1f - num7 / num10));
				this.mIgnoreCallbacks = false;
			}
		}
		else if (recalculateBounds)
		{
			this.mCalculatedBounds = false;
		}
	}

	// Token: 0x06000379 RID: 889 RVA: 0x00010714 File Offset: 0x0000E914
	public void SetDragAmount(float x, float y, bool updateScrollbars)
	{
		this.DisableSpring();
		Bounds bounds = this.bounds;
		if (bounds.min.x == bounds.max.x || bounds.min.y == bounds.max.y)
		{
			return;
		}
		Vector4 clipRange = this.mPanel.clipRange;
		float num = clipRange.z * 0.5f;
		float num2 = clipRange.w * 0.5f;
		float num3 = bounds.min.x + num;
		float num4 = bounds.max.x - num;
		float num5 = bounds.min.y + num2;
		float num6 = bounds.max.y - num2;
		if (this.mPanel.clipping == UIDrawCall.Clipping.SoftClip)
		{
			num3 -= this.mPanel.clipSoftness.x;
			num4 += this.mPanel.clipSoftness.x;
			num5 -= this.mPanel.clipSoftness.y;
			num6 += this.mPanel.clipSoftness.y;
		}
		float num7 = Mathf.Lerp(num3, num4, x);
		float num8 = Mathf.Lerp(num6, num5, y);
		if (!updateScrollbars)
		{
			Vector3 localPosition = this.mTrans.localPosition;
			if (this.scale.x != 0f)
			{
				localPosition.x += clipRange.x - num7;
			}
			if (this.scale.y != 0f)
			{
				localPosition.y += clipRange.y - num8;
			}
			this.mTrans.localPosition = localPosition;
		}
		clipRange.x = num7;
		clipRange.y = num8;
		this.mPanel.clipRange = clipRange;
		if (updateScrollbars)
		{
			this.UpdateScrollbars(false);
		}
	}

	// Token: 0x0600037A RID: 890 RVA: 0x00010924 File Offset: 0x0000EB24
	public void ResetPosition()
	{
		this.mCalculatedBounds = false;
		this.SetDragAmount(this.relativePositionOnReset.x, this.relativePositionOnReset.y, false);
		this.SetDragAmount(this.relativePositionOnReset.x, this.relativePositionOnReset.y, true);
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00010974 File Offset: 0x0000EB74
	private void OnHorizontalBar()
	{
		if (!this.mIgnoreCallbacks)
		{
			float x = (!(this.horizontalScrollBar != null)) ? 0f : this.horizontalScrollBar.value;
			float y = (!(this.verticalScrollBar != null)) ? 0f : this.verticalScrollBar.value;
			this.SetDragAmount(x, y, false);
		}
	}

	// Token: 0x0600037C RID: 892 RVA: 0x000109E4 File Offset: 0x0000EBE4
	private void OnVerticalBar()
	{
		if (!this.mIgnoreCallbacks)
		{
			float x = (!(this.horizontalScrollBar != null)) ? 0f : this.horizontalScrollBar.value;
			float y = (!(this.verticalScrollBar != null)) ? 0f : this.verticalScrollBar.value;
			this.SetDragAmount(x, y, false);
		}
	}

	// Token: 0x0600037D RID: 893 RVA: 0x00010A54 File Offset: 0x0000EC54
	public void MoveRelative(Vector3 relative)
	{
		this.mTrans.localPosition += relative;
		Vector4 clipRange = this.mPanel.clipRange;
		clipRange.x -= relative.x;
		clipRange.y -= relative.y;
		this.mPanel.clipRange = clipRange;
		this.UpdateScrollbars(false);
	}

	// Token: 0x0600037E RID: 894 RVA: 0x00010AC4 File Offset: 0x0000ECC4
	public void MoveAbsolute(Vector3 absolute)
	{
		Vector3 a = this.mTrans.InverseTransformPoint(absolute);
		Vector3 b = this.mTrans.InverseTransformPoint(Vector3.zero);
		this.MoveRelative(a - b);
	}

	// Token: 0x0600037F RID: 895 RVA: 0x00010AFC File Offset: 0x0000ECFC
	public void Press(bool pressed)
	{
		if (this.smoothDragStart && pressed)
		{
			this.mDragStarted = false;
			this.mDragStartOffset = Vector2.zero;
		}
		if (base.enabled && NGUITools.GetActive(base.gameObject))
		{
			if (!pressed && this.mDragID == UICamera.currentTouchID)
			{
				this.mDragID = -10;
			}
			this.mCalculatedBounds = false;
			this.mShouldMove = this.shouldMove;
			if (!this.mShouldMove)
			{
				return;
			}
			this.mPressed = pressed;
			if (pressed)
			{
				this.mMomentum = Vector3.zero;
				this.mScroll = 0f;
				this.DisableSpring();
				this.mLastPos = UICamera.lastHit.point;
				this.mPlane = new Plane(this.mTrans.rotation * Vector3.back, this.mLastPos);
			}
			else
			{
				if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None && this.dragEffect == UIDraggablePanel.DragEffect.MomentumAndSpring)
				{
					this.RestrictWithinBounds(false);
				}
				if (this.onDragFinished != null)
				{
					this.onDragFinished();
				}
			}
		}
	}

	// Token: 0x06000380 RID: 896 RVA: 0x00010C30 File Offset: 0x0000EE30
	public void Drag()
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.mShouldMove)
		{
			if (this.mDragID == -10)
			{
				this.mDragID = UICamera.currentTouchID;
			}
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			if (this.smoothDragStart && !this.mDragStarted)
			{
				this.mDragStarted = true;
				this.mDragStartOffset = UICamera.currentTouch.totalDelta;
			}
			Ray ray = (!this.smoothDragStart) ? UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos) : UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos - this.mDragStartOffset);
			float distance = 0f;
			if (this.mPlane.Raycast(ray, out distance))
			{
				Vector3 point = ray.GetPoint(distance);
				Vector3 vector = point - this.mLastPos;
				this.mLastPos = point;
				if (vector.x != 0f || vector.y != 0f)
				{
					vector = this.mTrans.InverseTransformDirection(vector);
					vector.Scale(this.scale);
					vector = this.mTrans.TransformDirection(vector);
				}
				this.mMomentum = Vector3.Lerp(this.mMomentum, this.mMomentum + vector * (0.01f * this.momentumAmount), 0.67f);
				if (!this.iOSDragEmulation)
				{
					this.MoveAbsolute(vector);
				}
				else if (this.mPanel.CalculateConstrainOffset(this.bounds.min, this.bounds.max).magnitude > 0.001f)
				{
					this.MoveAbsolute(vector * 0.5f);
					this.mMomentum *= 0.5f;
				}
				else
				{
					this.MoveAbsolute(vector);
				}
				if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None && this.dragEffect != UIDraggablePanel.DragEffect.MomentumAndSpring)
				{
					this.RestrictWithinBounds(true);
				}
			}
		}
	}

	// Token: 0x06000381 RID: 897 RVA: 0x00010E74 File Offset: 0x0000F074
	public void Scroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.scrollWheelFactor != 0f)
		{
			this.DisableSpring();
			this.mShouldMove = this.shouldMove;
			if (Mathf.Sign(this.mScroll) != Mathf.Sign(delta))
			{
				this.mScroll = 0f;
			}
			this.mScroll += delta * this.scrollWheelFactor;
		}
	}

	// Token: 0x06000382 RID: 898 RVA: 0x00010EF4 File Offset: 0x0000F0F4
	private void LateUpdate()
	{
		if (this.repositionClipping)
		{
			this.repositionClipping = false;
			this.mCalculatedBounds = false;
			this.SetDragAmount(this.relativePositionOnReset.x, this.relativePositionOnReset.y, true);
		}
		if (!Application.isPlaying)
		{
			return;
		}
		float deltaTime = RealTime.deltaTime;
		if (this.showScrollBars != UIDraggablePanel.ShowCondition.Always)
		{
			bool flag = false;
			bool flag2 = false;
			if (this.showScrollBars != UIDraggablePanel.ShowCondition.WhenDragging || this.mDragID != -10 || this.mMomentum.magnitude > 0.01f)
			{
				flag = this.shouldMoveVertically;
				flag2 = this.shouldMoveHorizontally;
			}
			if (this.verticalScrollBar)
			{
				float num = this.verticalScrollBar.alpha;
				num += ((!flag) ? (-deltaTime * 3f) : (deltaTime * 6f));
				num = Mathf.Clamp01(num);
				if (this.verticalScrollBar.alpha != num)
				{
					this.verticalScrollBar.alpha = num;
				}
			}
			if (this.horizontalScrollBar)
			{
				float num2 = this.horizontalScrollBar.alpha;
				num2 += ((!flag2) ? (-deltaTime * 3f) : (deltaTime * 6f));
				num2 = Mathf.Clamp01(num2);
				if (this.horizontalScrollBar.alpha != num2)
				{
					this.horizontalScrollBar.alpha = num2;
				}
			}
		}
		if (this.mShouldMove && !this.mPressed)
		{
			this.mMomentum -= this.scale * (this.mScroll * 0.05f);
			if (this.mMomentum.magnitude > 0.0001f)
			{
				this.mScroll = NGUIMath.SpringLerp(this.mScroll, 0f, 20f, deltaTime);
				Vector3 absolute = NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
				this.MoveAbsolute(absolute);
				if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None)
				{
					this.RestrictWithinBounds(false);
				}
				if (this.mMomentum.magnitude < 0.0001f && this.onDragFinished != null)
				{
					this.onDragFinished();
				}
				return;
			}
			this.mScroll = 0f;
			this.mMomentum = Vector3.zero;
		}
		else
		{
			this.mScroll = 0f;
		}
		NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
	}

	// Token: 0x04000227 RID: 551
	public UIDraggablePanel.DragEffect dragEffect = UIDraggablePanel.DragEffect.MomentumAndSpring;

	// Token: 0x04000228 RID: 552
	public bool restrictWithinPanel = true;

	// Token: 0x04000229 RID: 553
	public bool disableDragIfFits;

	// Token: 0x0400022A RID: 554
	public bool smoothDragStart = true;

	// Token: 0x0400022B RID: 555
	public bool repositionClipping;

	// Token: 0x0400022C RID: 556
	public bool iOSDragEmulation = true;

	// Token: 0x0400022D RID: 557
	public float scrollWheelFactor;

	// Token: 0x0400022E RID: 558
	public float momentumAmount = 35f;

	// Token: 0x0400022F RID: 559
	public UIScrollBar horizontalScrollBar;

	// Token: 0x04000230 RID: 560
	public UIScrollBar verticalScrollBar;

	// Token: 0x04000231 RID: 561
	public UIDraggablePanel.ShowCondition showScrollBars = UIDraggablePanel.ShowCondition.OnlyIfNeeded;

	// Token: 0x04000232 RID: 562
	public Vector3 scale = new Vector3(1f, 0f, 0f);

	// Token: 0x04000233 RID: 563
	public Vector2 relativePositionOnReset = Vector2.zero;

	// Token: 0x04000234 RID: 564
	public UIDraggablePanel.OnDragFinished onDragFinished;

	// Token: 0x04000235 RID: 565
	private Transform mTrans;

	// Token: 0x04000236 RID: 566
	private UIPanel mPanel;

	// Token: 0x04000237 RID: 567
	private Plane mPlane;

	// Token: 0x04000238 RID: 568
	private Vector3 mLastPos;

	// Token: 0x04000239 RID: 569
	private bool mPressed;

	// Token: 0x0400023A RID: 570
	private Vector3 mMomentum = Vector3.zero;

	// Token: 0x0400023B RID: 571
	private float mScroll;

	// Token: 0x0400023C RID: 572
	private Bounds mBounds;

	// Token: 0x0400023D RID: 573
	private bool mCalculatedBounds;

	// Token: 0x0400023E RID: 574
	private bool mShouldMove;

	// Token: 0x0400023F RID: 575
	private bool mIgnoreCallbacks;

	// Token: 0x04000240 RID: 576
	private int mDragID = -10;

	// Token: 0x04000241 RID: 577
	private Vector2 mDragStartOffset = Vector2.zero;

	// Token: 0x04000242 RID: 578
	private bool mDragStarted;

	// Token: 0x02000086 RID: 134
	public enum DragEffect
	{
		// Token: 0x04000244 RID: 580
		None,
		// Token: 0x04000245 RID: 581
		Momentum,
		// Token: 0x04000246 RID: 582
		MomentumAndSpring
	}

	// Token: 0x02000087 RID: 135
	public enum ShowCondition
	{
		// Token: 0x04000248 RID: 584
		Always,
		// Token: 0x04000249 RID: 585
		OnlyIfNeeded,
		// Token: 0x0400024A RID: 586
		WhenDragging
	}

	// Token: 0x02000A63 RID: 2659
	// (Invoke) Token: 0x060047C6 RID: 18374
	public delegate void OnDragFinished();
}
