using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020000A0 RID: 160
[AddComponentMenu("NGUI/Internal/Active Animation")]
[RequireComponent(typeof(Animation))]
public class ActiveAnimation : MonoBehaviour
{
	// Token: 0x17000091 RID: 145
	// (get) Token: 0x06000424 RID: 1060 RVA: 0x00015398 File Offset: 0x00013598
	public bool isPlaying
	{
		get
		{
			if (this.mAnim == null)
			{
				return false;
			}
			foreach (object obj in this.mAnim)
			{
				AnimationState animationState = (AnimationState)obj;
				if (this.mAnim.IsPlaying(animationState.name))
				{
					if (this.mLastDirection == Direction.Forward)
					{
						if (animationState.time < animationState.length)
						{
							return true;
						}
					}
					else
					{
						if (this.mLastDirection != Direction.Reverse)
						{
							return true;
						}
						if (animationState.time > 0f)
						{
							return true;
						}
					}
				}
			}
			return false;
		}
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x0001548C File Offset: 0x0001368C
	public void Reset()
	{
		if (this.mAnim != null)
		{
			foreach (object obj in this.mAnim)
			{
				AnimationState animationState = (AnimationState)obj;
				if (this.mLastDirection == Direction.Reverse)
				{
					animationState.time = animationState.length;
				}
				else if (this.mLastDirection == Direction.Forward)
				{
					animationState.time = 0f;
				}
			}
		}
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x0001553C File Offset: 0x0001373C
	private void Start()
	{
		if (this.eventReceiver != null && EventDelegate.IsValid(this.onFinished))
		{
			this.eventReceiver = null;
			this.callWhenFinished = null;
		}
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x00015570 File Offset: 0x00013770
	private void Update()
	{
		float num = (!this.mRealTime) ? Time.deltaTime : RealTime.deltaTime;
		if (num == 0f)
		{
			return;
		}
		if (this.mAnim != null)
		{
			bool flag = false;
			foreach (object obj in this.mAnim)
			{
				AnimationState animationState = (AnimationState)obj;
				if (this.mAnim.IsPlaying(animationState.name))
				{
					float num2 = animationState.speed * num;
					animationState.time += num2;
					if (num2 < 0f)
					{
						if (animationState.time > 0f)
						{
							flag = true;
						}
						else
						{
							animationState.time = 0f;
						}
					}
					else if (animationState.time < animationState.length)
					{
						flag = true;
					}
					else
					{
						animationState.time = animationState.length;
					}
				}
			}
			this.mAnim.Sample();
			if (flag)
			{
				return;
			}
			base.enabled = false;
			if (this.mNotify)
			{
				this.mNotify = false;
				ActiveAnimation.current = this;
				EventDelegate.Execute(this.onFinished);
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
				{
					this.eventReceiver.SendMessage(this.callWhenFinished, SendMessageOptions.DontRequireReceiver);
				}
				ActiveAnimation.current = null;
				if (this.mDisableDirection != Direction.Toggle && this.mLastDirection == this.mDisableDirection)
				{
					NGUITools.SetActive(base.gameObject, false);
				}
			}
		}
		else
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x0001574C File Offset: 0x0001394C
	private void Play(string clipName, Direction playDirection)
	{
		if (this.mAnim != null)
		{
			base.enabled = true;
			this.mAnim.enabled = false;
			if (playDirection == Direction.Toggle)
			{
				playDirection = ((this.mLastDirection == Direction.Forward) ? Direction.Reverse : Direction.Forward);
			}
			bool flag = string.IsNullOrEmpty(clipName);
			if (flag)
			{
				if (!this.mAnim.isPlaying)
				{
					this.mAnim.Play();
				}
			}
			else if (!this.mAnim.IsPlaying(clipName))
			{
				this.mAnim.Play(clipName);
			}
			foreach (object obj in this.mAnim)
			{
				AnimationState animationState = (AnimationState)obj;
				if (string.IsNullOrEmpty(clipName) || animationState.name == clipName)
				{
					float num = Mathf.Abs(animationState.speed);
					animationState.speed = num * (float)playDirection;
					if (playDirection == Direction.Reverse && animationState.time == 0f)
					{
						animationState.time = animationState.length;
					}
					else if (playDirection == Direction.Forward && animationState.time == animationState.length)
					{
						animationState.time = 0f;
					}
				}
			}
			this.mLastDirection = playDirection;
			this.mNotify = true;
			this.mAnim.Sample();
		}
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x000158D8 File Offset: 0x00013AD8
	public static ActiveAnimation Play(Animation anim, string clipName, Direction playDirection, EnableCondition enableBeforePlay, DisableCondition disableCondition)
	{
		return ActiveAnimation.Play(anim, clipName, playDirection, enableBeforePlay, disableCondition, true);
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x000158E8 File Offset: 0x00013AE8
	public static ActiveAnimation Play(Animation anim, string clipName, Direction playDirection, EnableCondition enableBeforePlay, DisableCondition disableCondition, bool flagRealTime)
	{
		if (!NGUITools.GetActive(anim.gameObject))
		{
			if (enableBeforePlay != EnableCondition.EnableThenPlay)
			{
				return null;
			}
			NGUITools.SetActive(anim.gameObject, true);
			UIPanel[] componentsInChildren = anim.gameObject.GetComponentsInChildren<UIPanel>();
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				componentsInChildren[i].Refresh();
				i++;
			}
		}
		ActiveAnimation activeAnimation = anim.GetComponent<ActiveAnimation>();
		if (activeAnimation == null)
		{
			activeAnimation = anim.gameObject.AddComponent<ActiveAnimation>();
		}
		activeAnimation.mRealTime = flagRealTime;
		activeAnimation.mAnim = anim;
		activeAnimation.mDisableDirection = (Direction)disableCondition;
		activeAnimation.onFinished.Clear();
		activeAnimation.Play(clipName, playDirection);
		return activeAnimation;
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x00015990 File Offset: 0x00013B90
	public static ActiveAnimation Play(Animation anim, string clipName, Direction playDirection, bool flagRealTime)
	{
		return ActiveAnimation.Play(anim, clipName, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable, flagRealTime);
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x000159A0 File Offset: 0x00013BA0
	public static ActiveAnimation Play(Animation anim, Direction playDirection, bool flagRealTime)
	{
		return ActiveAnimation.Play(anim, null, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable, flagRealTime);
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x000159B0 File Offset: 0x00013BB0
	public static ActiveAnimation Play(Animation anim, string clipName, Direction playDirection)
	{
		return ActiveAnimation.Play(anim, clipName, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable, true);
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x000159C0 File Offset: 0x00013BC0
	public static ActiveAnimation Play(Animation anim, Direction playDirection)
	{
		return ActiveAnimation.Play(anim, null, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable, true);
	}

	// Token: 0x04000306 RID: 774
	public static ActiveAnimation current;

	// Token: 0x04000307 RID: 775
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x04000308 RID: 776
	[HideInInspector]
	public GameObject eventReceiver;

	// Token: 0x04000309 RID: 777
	[HideInInspector]
	public string callWhenFinished;

	// Token: 0x0400030A RID: 778
	private Animation mAnim;

	// Token: 0x0400030B RID: 779
	private Direction mLastDirection;

	// Token: 0x0400030C RID: 780
	private Direction mDisableDirection;

	// Token: 0x0400030D RID: 781
	private bool mNotify;

	// Token: 0x0400030E RID: 782
	private bool mRealTime = true;
}
