using System;
using UnityEngine;

// Token: 0x020000B9 RID: 185
[AddComponentMenu("NGUI/Tween/Spring Position")]
public class SpringPosition : MonoBehaviour
{
	// Token: 0x0600056D RID: 1389 RVA: 0x0001B670 File Offset: 0x00019870
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x0001B680 File Offset: 0x00019880
	private void Update()
	{
		float deltaTime = (!this.ignoreTimeScale) ? Time.deltaTime : RealTime.deltaTime;
		if (this.worldSpace)
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.position).magnitude * 0.001f;
			}
			this.mTrans.position = NGUIMath.SpringLerp(this.mTrans.position, this.target, this.strength, deltaTime);
			if (this.mThreshold >= (this.target - this.mTrans.position).magnitude)
			{
				this.mTrans.position = this.target;
				if (this.onFinished != null)
				{
					this.onFinished(this);
				}
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
				{
					this.eventReceiver.SendMessage(this.callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
				}
				base.enabled = false;
			}
		}
		else
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.localPosition).magnitude * 0.001f;
			}
			this.mTrans.localPosition = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
			if (this.mThreshold >= (this.target - this.mTrans.localPosition).magnitude)
			{
				this.mTrans.localPosition = this.target;
				if (this.onFinished != null)
				{
					this.onFinished(this);
				}
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
				{
					this.eventReceiver.SendMessage(this.callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
				}
				base.enabled = false;
			}
		}
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x0001B898 File Offset: 0x00019A98
	public static SpringPosition Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPosition springPosition = go.GetComponent<SpringPosition>();
		if (springPosition == null)
		{
			springPosition = go.AddComponent<SpringPosition>();
		}
		springPosition.target = pos;
		springPosition.strength = strength;
		springPosition.onFinished = null;
		if (!springPosition.enabled)
		{
			springPosition.mThreshold = 0f;
			springPosition.enabled = true;
		}
		return springPosition;
	}

	// Token: 0x040003B2 RID: 946
	public Vector3 target = Vector3.zero;

	// Token: 0x040003B3 RID: 947
	public float strength = 10f;

	// Token: 0x040003B4 RID: 948
	public bool worldSpace;

	// Token: 0x040003B5 RID: 949
	public bool ignoreTimeScale;

	// Token: 0x040003B6 RID: 950
	public GameObject eventReceiver;

	// Token: 0x040003B7 RID: 951
	public string callWhenFinished;

	// Token: 0x040003B8 RID: 952
	public SpringPosition.OnFinished onFinished;

	// Token: 0x040003B9 RID: 953
	private Transform mTrans;

	// Token: 0x040003BA RID: 954
	private float mThreshold;

	// Token: 0x02000A70 RID: 2672
	// (Invoke) Token: 0x060047FA RID: 18426
	public delegate void OnFinished(SpringPosition spring);
}
