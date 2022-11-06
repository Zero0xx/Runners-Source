using System;
using UnityEngine;

// Token: 0x02000081 RID: 129
[AddComponentMenu("NGUI/Interaction/Drag Object")]
public class UIDragObject : MonoBehaviour
{
	// Token: 0x06000355 RID: 853 RVA: 0x0000F258 File Offset: 0x0000D458
	private void FindPanel()
	{
		this.mPanel = ((!(this.target != null)) ? null : UIPanel.Find(this.target.transform, false));
		if (this.mPanel == null)
		{
			this.restrictWithinPanel = false;
		}
	}

	// Token: 0x06000356 RID: 854 RVA: 0x0000F2AC File Offset: 0x0000D4AC
	private void OnPress(bool pressed)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && this.target != null)
		{
			if (pressed)
			{
				if (!this.mPressed)
				{
					this.mTouchID = UICamera.currentTouchID;
					this.mMomentum = Vector3.zero;
					this.mPressed = true;
					this.mStarted = false;
					this.mScroll = 0f;
					if (this.restrictWithinPanel && this.mPanel == null)
					{
						this.FindPanel();
					}
					if (this.restrictWithinPanel)
					{
						this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.mPanel.cachedTransform, this.target);
					}
					SpringPosition component = this.target.GetComponent<SpringPosition>();
					if (component != null)
					{
						component.enabled = false;
					}
					Transform transform = UICamera.currentCamera.transform;
					this.mPlane = new Plane(((!(this.mPanel != null)) ? transform.rotation : this.mPanel.cachedTransform.rotation) * Vector3.back, UICamera.lastHit.point);
				}
			}
			else if (this.mPressed && this.mTouchID == UICamera.currentTouchID)
			{
				this.mPressed = false;
				if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None && this.dragEffect == UIDragObject.DragEffect.MomentumAndSpring)
				{
					this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, false);
				}
			}
		}
	}

	// Token: 0x06000357 RID: 855 RVA: 0x0000F44C File Offset: 0x0000D64C
	private void OnDrag(Vector2 delta)
	{
		if (this.mPressed && this.mTouchID == UICamera.currentTouchID && base.enabled && NGUITools.GetActive(base.gameObject) && this.target != null)
		{
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
			float distance = 0f;
			if (this.mPlane.Raycast(ray, out distance))
			{
				Vector3 point = ray.GetPoint(distance);
				Vector3 vector = point - this.mLastPos;
				this.mLastPos = point;
				if (!this.mStarted)
				{
					this.mStarted = true;
					vector = Vector3.zero;
				}
				if (vector.x != 0f || vector.y != 0f)
				{
					vector = this.target.InverseTransformDirection(vector);
					vector.Scale(this.scale);
					vector = this.target.TransformDirection(vector);
				}
				if (this.dragEffect != UIDragObject.DragEffect.None)
				{
					this.mMomentum = Vector3.Lerp(this.mMomentum, this.mMomentum + vector * (0.01f * this.momentumAmount), 0.67f);
				}
				if (this.restrictWithinPanel)
				{
					Vector3 localPosition = this.target.localPosition;
					this.target.position += vector;
					this.mBounds.center = this.mBounds.center + (this.target.localPosition - localPosition);
					if (this.dragEffect != UIDragObject.DragEffect.MomentumAndSpring && this.mPanel.clipping != UIDrawCall.Clipping.None && this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, true))
					{
						this.mMomentum = Vector3.zero;
						this.mScroll = 0f;
					}
				}
				else
				{
					this.target.position += vector;
				}
			}
		}
	}

	// Token: 0x06000358 RID: 856 RVA: 0x0000F664 File Offset: 0x0000D864
	private void LateUpdate()
	{
		float deltaTime = RealTime.deltaTime;
		if (this.target == null)
		{
			return;
		}
		if (this.mPressed)
		{
			SpringPosition component = this.target.GetComponent<SpringPosition>();
			if (component != null)
			{
				component.enabled = false;
			}
			this.mScroll = 0f;
		}
		else
		{
			this.mMomentum += this.scale * (-this.mScroll * 0.05f);
			this.mScroll = NGUIMath.SpringLerp(this.mScroll, 0f, 20f, deltaTime);
			if (this.mMomentum.magnitude > 0.0001f)
			{
				if (this.mPanel == null)
				{
					this.FindPanel();
				}
				if (this.mPanel != null)
				{
					this.target.position += NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
					if (this.restrictWithinPanel && this.mPanel.clipping != UIDrawCall.Clipping.None)
					{
						this.mBounds = NGUIMath.CalculateRelativeWidgetBounds(this.mPanel.cachedTransform, this.target);
						if (!this.mPanel.ConstrainTargetToBounds(this.target, ref this.mBounds, this.dragEffect == UIDragObject.DragEffect.None))
						{
							SpringPosition component2 = this.target.GetComponent<SpringPosition>();
							if (component2 != null)
							{
								component2.enabled = false;
							}
						}
					}
					return;
				}
			}
			else
			{
				this.mScroll = 0f;
			}
		}
		NGUIMath.SpringDampen(ref this.mMomentum, 9f, deltaTime);
	}

	// Token: 0x06000359 RID: 857 RVA: 0x0000F80C File Offset: 0x0000DA0C
	private void OnScroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject))
		{
			if (Mathf.Sign(this.mScroll) != Mathf.Sign(delta))
			{
				this.mScroll = 0f;
			}
			this.mScroll += delta * this.scrollWheelFactor;
		}
	}

	// Token: 0x04000205 RID: 517
	public Transform target;

	// Token: 0x04000206 RID: 518
	public Vector3 scale = Vector3.one;

	// Token: 0x04000207 RID: 519
	public float scrollWheelFactor;

	// Token: 0x04000208 RID: 520
	public bool restrictWithinPanel;

	// Token: 0x04000209 RID: 521
	public UIDragObject.DragEffect dragEffect = UIDragObject.DragEffect.MomentumAndSpring;

	// Token: 0x0400020A RID: 522
	public float momentumAmount = 35f;

	// Token: 0x0400020B RID: 523
	private Plane mPlane;

	// Token: 0x0400020C RID: 524
	private Vector3 mLastPos;

	// Token: 0x0400020D RID: 525
	private UIPanel mPanel;

	// Token: 0x0400020E RID: 526
	private bool mPressed;

	// Token: 0x0400020F RID: 527
	private Vector3 mMomentum = Vector3.zero;

	// Token: 0x04000210 RID: 528
	private float mScroll;

	// Token: 0x04000211 RID: 529
	private Bounds mBounds;

	// Token: 0x04000212 RID: 530
	private int mTouchID;

	// Token: 0x04000213 RID: 531
	private bool mStarted;

	// Token: 0x02000082 RID: 130
	public enum DragEffect
	{
		// Token: 0x04000215 RID: 533
		None,
		// Token: 0x04000216 RID: 534
		Momentum,
		// Token: 0x04000217 RID: 535
		MomentumAndSpring
	}
}
