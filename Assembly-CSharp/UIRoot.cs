using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DF RID: 223
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Root")]
public class UIRoot : MonoBehaviour
{
	// Token: 0x17000130 RID: 304
	// (get) Token: 0x060006B0 RID: 1712 RVA: 0x000258F4 File Offset: 0x00023AF4
	public int activeHeight
	{
		get
		{
			int num = Mathf.Max(2, Screen.height);
			if (this.scalingStyle == UIRoot.Scaling.FixedSize)
			{
				return this.manualHeight;
			}
			if (this.scalingStyle == UIRoot.Scaling.FixedSizeOnMobiles)
			{
				return this.manualHeight;
			}
			if (num < this.minimumHeight)
			{
				return this.minimumHeight;
			}
			if (num > this.maximumHeight)
			{
				return this.maximumHeight;
			}
			return num;
		}
	}

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0002595C File Offset: 0x00023B5C
	public float pixelSizeAdjustment
	{
		get
		{
			return this.GetPixelSizeAdjustment(Screen.height);
		}
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x0002596C File Offset: 0x00023B6C
	public static float GetPixelSizeAdjustment(GameObject go)
	{
		UIRoot uiroot = NGUITools.FindInParents<UIRoot>(go);
		return (!(uiroot != null)) ? 1f : uiroot.pixelSizeAdjustment;
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x0002599C File Offset: 0x00023B9C
	public float GetPixelSizeAdjustment(int height)
	{
		height = Mathf.Max(2, height);
		if (this.scalingStyle == UIRoot.Scaling.FixedSize)
		{
			return (float)this.manualHeight / (float)height;
		}
		if (this.scalingStyle == UIRoot.Scaling.FixedSizeOnMobiles)
		{
			return (float)this.manualHeight / (float)height;
		}
		if (height < this.minimumHeight)
		{
			return (float)this.minimumHeight / (float)height;
		}
		if (height > this.maximumHeight)
		{
			return (float)this.maximumHeight / (float)height;
		}
		return 1f;
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x00025A14 File Offset: 0x00023C14
	protected virtual void Awake()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x00025A24 File Offset: 0x00023C24
	protected virtual void OnEnable()
	{
		UIRoot.list.Add(this);
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x00025A34 File Offset: 0x00023C34
	protected virtual void OnDisable()
	{
		UIRoot.list.Remove(this);
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x00025A44 File Offset: 0x00023C44
	protected virtual void Start()
	{
		UIOrthoCamera componentInChildren = base.GetComponentInChildren<UIOrthoCamera>();
		if (componentInChildren != null)
		{
			global::Debug.LogWarning("UIRoot should not be active at the same time as UIOrthoCamera. Disabling UIOrthoCamera.", componentInChildren);
			Camera component = componentInChildren.gameObject.GetComponent<Camera>();
			componentInChildren.enabled = false;
			if (component != null)
			{
				component.orthographicSize = 1f;
			}
		}
		else
		{
			this.Update();
		}
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x00025AA4 File Offset: 0x00023CA4
	private void Update()
	{
		if (this.mTrans != null)
		{
			float num = (float)this.activeHeight;
			if (num > 0f)
			{
				float num2 = 2f / num;
				Vector3 localScale = this.mTrans.localScale;
				if (Mathf.Abs(localScale.x - num2) > 1E-45f || Mathf.Abs(localScale.y - num2) > 1E-45f || Mathf.Abs(localScale.z - num2) > 1E-45f)
				{
					this.mTrans.localScale = new Vector3(num2, num2, num2);
				}
			}
		}
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x00025B44 File Offset: 0x00023D44
	public static void Broadcast(string funcName)
	{
		int i = 0;
		int count = UIRoot.list.Count;
		while (i < count)
		{
			UIRoot uiroot = UIRoot.list[i];
			if (uiroot != null)
			{
				uiroot.BroadcastMessage(funcName, SendMessageOptions.DontRequireReceiver);
			}
			i++;
		}
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x00025B90 File Offset: 0x00023D90
	public static void Broadcast(string funcName, object param)
	{
		if (param == null)
		{
			global::Debug.LogError("SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
		}
		else
		{
			int i = 0;
			int count = UIRoot.list.Count;
			while (i < count)
			{
				UIRoot uiroot = UIRoot.list[i];
				if (uiroot != null)
				{
					uiroot.BroadcastMessage(funcName, param, SendMessageOptions.DontRequireReceiver);
				}
				i++;
			}
		}
	}

	// Token: 0x040004FD RID: 1277
	public static List<UIRoot> list = new List<UIRoot>();

	// Token: 0x040004FE RID: 1278
	public UIRoot.Scaling scalingStyle = UIRoot.Scaling.FixedSize;

	// Token: 0x040004FF RID: 1279
	public int manualHeight = 720;

	// Token: 0x04000500 RID: 1280
	public int minimumHeight = 320;

	// Token: 0x04000501 RID: 1281
	public int maximumHeight = 1536;

	// Token: 0x04000502 RID: 1282
	private Transform mTrans;

	// Token: 0x020000E0 RID: 224
	public enum Scaling
	{
		// Token: 0x04000504 RID: 1284
		PixelPerfect,
		// Token: 0x04000505 RID: 1285
		FixedSize,
		// Token: 0x04000506 RID: 1286
		FixedSizeOnMobiles
	}
}
