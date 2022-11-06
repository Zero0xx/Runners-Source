using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x0200009C RID: 156
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Toggle")]
public class UIToggle : UIWidgetContainer
{
	// Token: 0x1700008F RID: 143
	// (get) Token: 0x06000412 RID: 1042 RVA: 0x00014E5C File Offset: 0x0001305C
	// (set) Token: 0x06000413 RID: 1043 RVA: 0x00014E64 File Offset: 0x00013064
	public bool value
	{
		get
		{
			return this.mIsActive;
		}
		set
		{
			if (this.group == 0 || value || this.optionCanBeNone || !this.mStarted)
			{
				this.Set(value);
			}
		}
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x06000414 RID: 1044 RVA: 0x00014EA0 File Offset: 0x000130A0
	// (set) Token: 0x06000415 RID: 1045 RVA: 0x00014EA8 File Offset: 0x000130A8
	[Obsolete("Use 'value' instead")]
	public bool isChecked
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

	// Token: 0x06000416 RID: 1046 RVA: 0x00014EB4 File Offset: 0x000130B4
	private void OnEnable()
	{
		UIToggle.list.Add(this);
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x00014EC4 File Offset: 0x000130C4
	private void OnDisable()
	{
		UIToggle.list.Remove(this);
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x00014ED4 File Offset: 0x000130D4
	private void Start()
	{
		this.mIsActive = !this.startsActive;
		this.mStarted = true;
		this.Set(this.startsActive);
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x00014F04 File Offset: 0x00013104
	private void OnClick()
	{
		if (base.enabled)
		{
			this.value = !this.value;
		}
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x00014F20 File Offset: 0x00013120
	private void Set(bool state)
	{
		if (!this.mStarted)
		{
			this.mIsActive = state;
			this.startsActive = state;
			if (this.activeSprite != null)
			{
				this.activeSprite.alpha = ((!state) ? 0f : 1f);
			}
		}
		else if (this.mIsActive != state)
		{
			if (this.group != 0 && state)
			{
				int i = 0;
				int size = UIToggle.list.size;
				while (i < size)
				{
					UIToggle uitoggle = UIToggle.list[i];
					if (uitoggle != this && uitoggle.group == this.group)
					{
						uitoggle.Set(false);
					}
					i++;
				}
			}
			this.mIsActive = state;
			if (this.activeSprite != null)
			{
				if (this.instantTween)
				{
					this.activeSprite.alpha = ((!this.mIsActive) ? 0f : 1f);
				}
				else
				{
					TweenAlpha.Begin(this.activeSprite.gameObject, 0.15f, (!this.mIsActive) ? 0f : 1f);
				}
			}
			UIToggle.current = this;
			if (EventDelegate.IsValid(this.onChange))
			{
				EventDelegate.Execute(this.onChange);
			}
			else if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName))
			{
				this.eventReceiver.SendMessage(this.functionName, this.mIsActive, SendMessageOptions.DontRequireReceiver);
			}
			UIToggle.current = null;
			if (this.activeAnimation != null)
			{
				ActiveAnimation.Play(this.activeAnimation, (!state) ? Direction.Reverse : Direction.Forward);
			}
		}
	}

	// Token: 0x040002ED RID: 749
	public static BetterList<UIToggle> list = new BetterList<UIToggle>();

	// Token: 0x040002EE RID: 750
	public static UIToggle current;

	// Token: 0x040002EF RID: 751
	public int group;

	// Token: 0x040002F0 RID: 752
	public UIWidget activeSprite;

	// Token: 0x040002F1 RID: 753
	public Animation activeAnimation;

	// Token: 0x040002F2 RID: 754
	public bool startsActive;

	// Token: 0x040002F3 RID: 755
	public bool instantTween;

	// Token: 0x040002F4 RID: 756
	public bool optionCanBeNone;

	// Token: 0x040002F5 RID: 757
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x040002F6 RID: 758
	[HideInInspector]
	[SerializeField]
	private Transform radioButtonRoot;

	// Token: 0x040002F7 RID: 759
	[HideInInspector]
	[SerializeField]
	private bool startsChecked;

	// Token: 0x040002F8 RID: 760
	[SerializeField]
	[HideInInspector]
	private UISprite checkSprite;

	// Token: 0x040002F9 RID: 761
	[HideInInspector]
	[SerializeField]
	private Animation checkAnimation;

	// Token: 0x040002FA RID: 762
	[SerializeField]
	[HideInInspector]
	private GameObject eventReceiver;

	// Token: 0x040002FB RID: 763
	[SerializeField]
	[HideInInspector]
	private string functionName = "OnActivate";

	// Token: 0x040002FC RID: 764
	private bool mIsActive = true;

	// Token: 0x040002FD RID: 765
	private bool mStarted;
}
