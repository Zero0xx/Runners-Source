using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x0200008E RID: 142
[AddComponentMenu("NGUI/Interaction/Play Animation")]
[ExecuteInEditMode]
public class UIPlayAnimation : MonoBehaviour
{
	// Token: 0x0600039E RID: 926 RVA: 0x00011A30 File Offset: 0x0000FC30
	private void Awake()
	{
		if (this.eventReceiver != null && EventDelegate.IsValid(this.onFinished))
		{
			this.eventReceiver = null;
			this.callWhenFinished = null;
		}
	}

	// Token: 0x0600039F RID: 927 RVA: 0x00011A64 File Offset: 0x0000FC64
	private void Start()
	{
		this.mStarted = true;
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<Animation>();
		}
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00011A98 File Offset: 0x0000FC98
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00011AC4 File Offset: 0x0000FCC4
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (this.trigger == Trigger.OnHover || (this.trigger == Trigger.OnHoverTrue && isOver) || (this.trigger == Trigger.OnHoverFalse && !isOver))
			{
				this.Play(isOver);
			}
			this.mHighlighted = isOver;
		}
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x00011B1C File Offset: 0x0000FD1C
	private void OnPress(bool isPressed)
	{
		if (base.enabled && (this.trigger == Trigger.OnPress || (this.trigger == Trigger.OnPressTrue && isPressed) || (this.trigger == Trigger.OnPressFalse && !isPressed)))
		{
			this.Play(isPressed);
		}
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x00011B6C File Offset: 0x0000FD6C
	private void OnClick()
	{
		if (base.enabled && this.trigger == Trigger.OnClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x00011B8C File Offset: 0x0000FD8C
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == Trigger.OnDoubleClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x00011BB0 File Offset: 0x0000FDB0
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (this.trigger == Trigger.OnSelect || (this.trigger == Trigger.OnSelectTrue && isSelected) || (this.trigger == Trigger.OnSelectFalse && !isSelected)))
		{
			this.Play(true);
		}
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00011C04 File Offset: 0x0000FE04
	private void OnActivate(bool isActive)
	{
		if (base.enabled && (this.trigger == Trigger.OnActivate || (this.trigger == Trigger.OnActivateTrue && isActive) || (this.trigger == Trigger.OnActivateFalse && !isActive)))
		{
			this.Play(isActive);
		}
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x00011C54 File Offset: 0x0000FE54
	public void Play(bool forward)
	{
		if (this.target != null)
		{
			this.mActive = 0;
			if (this.clearSelection && UICamera.selectedObject == base.gameObject)
			{
				UICamera.selectedObject = null;
			}
			int num = (int)(-(int)this.playDirection);
			Direction direction = (Direction)((!forward) ? num : ((int)this.playDirection));
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.target, this.clipName, direction, this.ifDisabledOnPlay, this.disableWhenFinished);
			if (activeAnimation != null)
			{
				if (this.resetOnPlay)
				{
					activeAnimation.Reset();
				}
				for (int i = 0; i < this.onFinished.Count; i++)
				{
					this.mActive++;
					EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinished), true);
				}
			}
		}
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x00011D38 File Offset: 0x0000FF38
	private void OnFinished()
	{
		if (--this.mActive == 0)
		{
			EventDelegate.Execute(this.onFinished);
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
			{
				this.eventReceiver.SendMessage(this.callWhenFinished, SendMessageOptions.DontRequireReceiver);
			}
			this.eventReceiver = null;
		}
	}

	// Token: 0x0400026E RID: 622
	public Animation target;

	// Token: 0x0400026F RID: 623
	public string clipName;

	// Token: 0x04000270 RID: 624
	public Trigger trigger;

	// Token: 0x04000271 RID: 625
	public Direction playDirection = Direction.Forward;

	// Token: 0x04000272 RID: 626
	public bool resetOnPlay;

	// Token: 0x04000273 RID: 627
	public bool clearSelection;

	// Token: 0x04000274 RID: 628
	public EnableCondition ifDisabledOnPlay;

	// Token: 0x04000275 RID: 629
	public DisableCondition disableWhenFinished;

	// Token: 0x04000276 RID: 630
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x04000277 RID: 631
	[SerializeField]
	[HideInInspector]
	private GameObject eventReceiver;

	// Token: 0x04000278 RID: 632
	[HideInInspector]
	[SerializeField]
	private string callWhenFinished;

	// Token: 0x04000279 RID: 633
	private bool mStarted;

	// Token: 0x0400027A RID: 634
	private bool mHighlighted;

	// Token: 0x0400027B RID: 635
	private int mActive;
}
