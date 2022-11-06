using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000091 RID: 145
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Play Tween")]
public class UIPlayTween : MonoBehaviour
{
	// Token: 0x060003AE RID: 942 RVA: 0x00011EC0 File Offset: 0x000100C0
	private void Awake()
	{
		if (this.eventReceiver != null && EventDelegate.IsValid(this.onFinished))
		{
			this.eventReceiver = null;
			this.callWhenFinished = null;
		}
	}

	// Token: 0x060003AF RID: 943 RVA: 0x00011EF4 File Offset: 0x000100F4
	private void Start()
	{
		this.mStarted = true;
		if (this.tweenTarget == null)
		{
			this.tweenTarget = base.gameObject;
		}
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x00011F28 File Offset: 0x00010128
	private void OnEnable()
	{
		if (this.mStarted && this.mHighlighted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x00011F54 File Offset: 0x00010154
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

	// Token: 0x060003B2 RID: 946 RVA: 0x00011FAC File Offset: 0x000101AC
	private void OnPress(bool isPressed)
	{
		if (base.enabled && (this.trigger == Trigger.OnPress || (this.trigger == Trigger.OnPressTrue && isPressed) || (this.trigger == Trigger.OnPressFalse && !isPressed)))
		{
			this.Play(isPressed);
		}
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00011FFC File Offset: 0x000101FC
	private void OnClick()
	{
		if (base.enabled && this.trigger == Trigger.OnClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x0001201C File Offset: 0x0001021C
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == Trigger.OnDoubleClick)
		{
			this.Play(true);
		}
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00012040 File Offset: 0x00010240
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (this.trigger == Trigger.OnSelect || (this.trigger == Trigger.OnSelectTrue && isSelected) || (this.trigger == Trigger.OnSelectFalse && !isSelected)))
		{
			this.Play(true);
		}
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00012094 File Offset: 0x00010294
	private void OnActivate(bool isActive)
	{
		if (base.enabled && (this.trigger == Trigger.OnActivate || (this.trigger == Trigger.OnActivateTrue && isActive) || (this.trigger == Trigger.OnActivateFalse && !isActive)))
		{
			this.Play(isActive);
		}
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x000120E4 File Offset: 0x000102E4
	private void Update()
	{
		if (this.disableWhenFinished != DisableCondition.DoNotDisable && this.mTweens != null)
		{
			bool flag = true;
			bool flag2 = true;
			int i = 0;
			int num = this.mTweens.Length;
			while (i < num)
			{
				UITweener uitweener = this.mTweens[i];
				if (uitweener.tweenGroup == this.tweenGroup)
				{
					if (uitweener.enabled)
					{
						flag = false;
						break;
					}
					if (uitweener.direction != (Direction)this.disableWhenFinished)
					{
						flag2 = false;
					}
				}
				i++;
			}
			if (flag)
			{
				if (flag2)
				{
					NGUITools.SetActive(this.tweenTarget, false);
				}
				this.mTweens = null;
			}
		}
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x00012190 File Offset: 0x00010390
	public void Play(bool forward)
	{
		this.mActive = 0;
		GameObject gameObject = (!(this.tweenTarget == null)) ? this.tweenTarget : base.gameObject;
		if (!NGUITools.GetActive(gameObject))
		{
			if (this.ifDisabledOnPlay != EnableCondition.EnableThenPlay)
			{
				return;
			}
			NGUITools.SetActive(gameObject, true);
		}
		this.mTweens = ((!this.includeChildren) ? gameObject.GetComponents<UITweener>() : gameObject.GetComponentsInChildren<UITweener>());
		if (this.mTweens.Length == 0)
		{
			if (this.disableWhenFinished != DisableCondition.DoNotDisable)
			{
				NGUITools.SetActive(this.tweenTarget, false);
			}
		}
		else
		{
			bool flag = false;
			if (this.playDirection == Direction.Reverse)
			{
				forward = !forward;
			}
			int i = 0;
			int num = this.mTweens.Length;
			while (i < num)
			{
				UITweener uitweener = this.mTweens[i];
				if (uitweener.tweenGroup == this.tweenGroup)
				{
					if (!flag && !NGUITools.GetActive(gameObject))
					{
						flag = true;
						NGUITools.SetActive(gameObject, true);
					}
					this.mActive++;
					if (this.playDirection == Direction.Toggle)
					{
						uitweener.Toggle();
					}
					else
					{
						if (this.resetOnPlay || (this.resetIfDisabled && !uitweener.enabled))
						{
							uitweener.Reset();
						}
						uitweener.Play(forward);
					}
					EventDelegate.Add(uitweener.onFinished, new EventDelegate.Callback(this.OnFinished), true);
				}
				i++;
			}
		}
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x00012308 File Offset: 0x00010508
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

	// Token: 0x04000286 RID: 646
	public GameObject tweenTarget;

	// Token: 0x04000287 RID: 647
	public int tweenGroup;

	// Token: 0x04000288 RID: 648
	public Trigger trigger;

	// Token: 0x04000289 RID: 649
	public Direction playDirection = Direction.Forward;

	// Token: 0x0400028A RID: 650
	public bool resetOnPlay;

	// Token: 0x0400028B RID: 651
	public bool resetIfDisabled;

	// Token: 0x0400028C RID: 652
	public EnableCondition ifDisabledOnPlay;

	// Token: 0x0400028D RID: 653
	public DisableCondition disableWhenFinished;

	// Token: 0x0400028E RID: 654
	public bool includeChildren;

	// Token: 0x0400028F RID: 655
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x04000290 RID: 656
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x04000291 RID: 657
	[HideInInspector]
	[SerializeField]
	private string callWhenFinished;

	// Token: 0x04000292 RID: 658
	private UITweener[] mTweens;

	// Token: 0x04000293 RID: 659
	private bool mStarted;

	// Token: 0x04000294 RID: 660
	private bool mHighlighted;

	// Token: 0x04000295 RID: 661
	private int mActive;
}
