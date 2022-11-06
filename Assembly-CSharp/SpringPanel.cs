using System;
using UnityEngine;

// Token: 0x020000B0 RID: 176
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Internal/Spring Panel")]
public class SpringPanel : MonoBehaviour
{
	// Token: 0x060004F6 RID: 1270 RVA: 0x00019268 File Offset: 0x00017468
	private void Start()
	{
		this.mPanel = base.GetComponent<UIPanel>();
		this.mDrag = base.GetComponent<UIDraggablePanel>();
		this.mTrans = base.transform;
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x0001929C File Offset: 0x0001749C
	private void Update()
	{
		float deltaTime = RealTime.deltaTime;
		if (this.mThreshold == 0f)
		{
			this.mThreshold = (this.target - this.mTrans.localPosition).magnitude * 0.005f;
			this.mThreshold = Mathf.Max(this.mThreshold, 1E-05f);
		}
		bool flag = false;
		Vector3 localPosition = this.mTrans.localPosition;
		Vector3 vector = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
		if (this.mThreshold >= Vector3.Magnitude(vector - this.target))
		{
			vector = this.target;
			base.enabled = false;
			flag = true;
		}
		this.mTrans.localPosition = vector;
		Vector3 vector2 = vector - localPosition;
		Vector4 clipRange = this.mPanel.clipRange;
		clipRange.x -= vector2.x;
		clipRange.y -= vector2.y;
		this.mPanel.clipRange = clipRange;
		if (this.mDrag != null)
		{
			this.mDrag.UpdateScrollbars(false);
		}
		if (flag && this.onFinished != null)
		{
			this.onFinished();
		}
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x000193EC File Offset: 0x000175EC
	public static SpringPanel Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPanel springPanel = go.GetComponent<SpringPanel>();
		if (springPanel == null)
		{
			springPanel = go.AddComponent<SpringPanel>();
		}
		springPanel.target = pos;
		springPanel.strength = strength;
		springPanel.onFinished = null;
		springPanel.mThreshold = 0f;
		springPanel.enabled = true;
		return springPanel;
	}

	// Token: 0x0400035D RID: 861
	public Vector3 target = Vector3.zero;

	// Token: 0x0400035E RID: 862
	public float strength = 10f;

	// Token: 0x0400035F RID: 863
	public SpringPanel.OnFinished onFinished;

	// Token: 0x04000360 RID: 864
	private UIPanel mPanel;

	// Token: 0x04000361 RID: 865
	private Transform mTrans;

	// Token: 0x04000362 RID: 866
	private float mThreshold;

	// Token: 0x04000363 RID: 867
	private UIDraggablePanel mDrag;

	// Token: 0x02000A68 RID: 2664
	// (Invoke) Token: 0x060047DA RID: 18394
	public delegate void OnFinished();
}
