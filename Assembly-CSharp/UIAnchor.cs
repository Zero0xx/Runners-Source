using System;
using UnityEngine;

// Token: 0x020000C8 RID: 200
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Anchor")]
public class UIAnchor : MonoBehaviour
{
	// Token: 0x060005C0 RID: 1472 RVA: 0x0001CE18 File Offset: 0x0001B018
	private void Awake()
	{
		this.mTrans = base.transform;
		this.mAnim = base.animation;
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x0001CE34 File Offset: 0x0001B034
	private void Start()
	{
		if (this.container == null && this.widgetContainer != null)
		{
			this.container = this.widgetContainer.gameObject;
			this.widgetContainer = null;
		}
		this.mRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
		this.mNeedsHalfPixelOffset = (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.XBOX360 || Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.WindowsEditor);
		if (this.mNeedsHalfPixelOffset)
		{
			this.mNeedsHalfPixelOffset = (SystemInfo.graphicsShaderLevel < 40);
		}
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.Update();
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x0001CF08 File Offset: 0x0001B108
	private void Update()
	{
		if (this.mAnim != null && this.mAnim.enabled && this.mAnim.isPlaying)
		{
			return;
		}
		bool flag = false;
		UIWidget uiwidget = (!(this.container == null)) ? this.container.GetComponent<UIWidget>() : null;
		UIPanel uipanel = (!(this.container == null) || !(uiwidget == null)) ? this.container.GetComponent<UIPanel>() : null;
		if (uiwidget != null)
		{
			Bounds bounds = uiwidget.CalculateBounds(base.transform.parent);
			this.mRect.x = bounds.min.x;
			this.mRect.y = bounds.min.y;
			this.mRect.width = bounds.size.x;
			this.mRect.height = bounds.size.y;
		}
		else if (uipanel != null)
		{
			if (uipanel.clipping == UIDrawCall.Clipping.None)
			{
				float num = (!(this.mRoot != null)) ? 0.5f : ((float)this.mRoot.activeHeight / (float)Screen.height * 0.5f);
				this.mRect.xMin = (float)(-(float)Screen.width) * num;
				this.mRect.yMin = (float)(-(float)Screen.height) * num;
				this.mRect.xMax = -this.mRect.xMin;
				this.mRect.yMax = -this.mRect.yMin;
			}
			else
			{
				Vector4 clipRange = uipanel.clipRange;
				this.mRect.x = clipRange.x - clipRange.z * 0.5f;
				this.mRect.y = clipRange.y - clipRange.w * 0.5f;
				this.mRect.width = clipRange.z;
				this.mRect.height = clipRange.w;
			}
		}
		else if (this.container != null)
		{
			Transform parent = base.transform.parent;
			Bounds bounds2 = (!(parent != null)) ? NGUIMath.CalculateRelativeWidgetBounds(this.container.transform) : NGUIMath.CalculateRelativeWidgetBounds(parent, this.container.transform);
			this.mRect.x = bounds2.min.x;
			this.mRect.y = bounds2.min.y;
			this.mRect.width = bounds2.size.x;
			this.mRect.height = bounds2.size.y;
		}
		else
		{
			if (!(this.uiCamera != null))
			{
				return;
			}
			flag = true;
			this.mRect = this.uiCamera.pixelRect;
		}
		float x = (this.mRect.xMin + this.mRect.xMax) * 0.5f;
		float y = (this.mRect.yMin + this.mRect.yMax) * 0.5f;
		Vector3 vector = new Vector3(x, y, 0f);
		if (this.side != UIAnchor.Side.Center)
		{
			if (this.side == UIAnchor.Side.Right || this.side == UIAnchor.Side.TopRight || this.side == UIAnchor.Side.BottomRight)
			{
				vector.x = this.mRect.xMax;
			}
			else if (this.side == UIAnchor.Side.Top || this.side == UIAnchor.Side.Center || this.side == UIAnchor.Side.Bottom)
			{
				vector.x = x;
			}
			else
			{
				vector.x = this.mRect.xMin;
			}
			if (this.side == UIAnchor.Side.Top || this.side == UIAnchor.Side.TopRight || this.side == UIAnchor.Side.TopLeft)
			{
				vector.y = this.mRect.yMax;
			}
			else if (this.side == UIAnchor.Side.Left || this.side == UIAnchor.Side.Center || this.side == UIAnchor.Side.Right)
			{
				vector.y = y;
			}
			else
			{
				vector.y = this.mRect.yMin;
			}
		}
		float width = this.mRect.width;
		float height = this.mRect.height;
		vector.x += this.pixelOffset.x + this.relativeOffset.x * width;
		vector.y += this.pixelOffset.y + this.relativeOffset.y * height;
		if (flag)
		{
			if (this.uiCamera.orthographic)
			{
				vector.x = Mathf.Round(vector.x);
				vector.y = Mathf.Round(vector.y);
				if (this.halfPixelOffset && this.mNeedsHalfPixelOffset)
				{
					vector.x -= 0.5f;
					vector.y += 0.5f;
				}
			}
			vector.z = this.uiCamera.WorldToScreenPoint(this.mTrans.position).z;
			vector = this.uiCamera.ScreenToWorldPoint(vector);
		}
		else
		{
			vector.x = Mathf.Round(vector.x);
			vector.y = Mathf.Round(vector.y);
			if (uipanel != null)
			{
				vector = uipanel.cachedTransform.TransformPoint(vector);
			}
			else if (this.container != null)
			{
				Transform parent2 = this.container.transform.parent;
				if (parent2 != null)
				{
					vector = parent2.TransformPoint(vector);
				}
			}
			vector.z = this.mTrans.position.z;
		}
		if (this.mTrans.position != vector)
		{
			this.mTrans.position = vector;
		}
		if (this.runOnlyOnce && Application.isPlaying)
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x04000407 RID: 1031
	private bool mNeedsHalfPixelOffset;

	// Token: 0x04000408 RID: 1032
	public Camera uiCamera;

	// Token: 0x04000409 RID: 1033
	public GameObject container;

	// Token: 0x0400040A RID: 1034
	public UIAnchor.Side side = UIAnchor.Side.Center;

	// Token: 0x0400040B RID: 1035
	public bool halfPixelOffset = true;

	// Token: 0x0400040C RID: 1036
	public bool runOnlyOnce;

	// Token: 0x0400040D RID: 1037
	public Vector2 relativeOffset = Vector2.zero;

	// Token: 0x0400040E RID: 1038
	public Vector2 pixelOffset = Vector2.zero;

	// Token: 0x0400040F RID: 1039
	[SerializeField]
	[HideInInspector]
	private UIWidget widgetContainer;

	// Token: 0x04000410 RID: 1040
	private Transform mTrans;

	// Token: 0x04000411 RID: 1041
	private Animation mAnim;

	// Token: 0x04000412 RID: 1042
	private Rect mRect = default(Rect);

	// Token: 0x04000413 RID: 1043
	private UIRoot mRoot;

	// Token: 0x020000C9 RID: 201
	public enum Side
	{
		// Token: 0x04000415 RID: 1045
		BottomLeft,
		// Token: 0x04000416 RID: 1046
		Left,
		// Token: 0x04000417 RID: 1047
		TopLeft,
		// Token: 0x04000418 RID: 1048
		Top,
		// Token: 0x04000419 RID: 1049
		TopRight,
		// Token: 0x0400041A RID: 1050
		Right,
		// Token: 0x0400041B RID: 1051
		BottomRight,
		// Token: 0x0400041C RID: 1052
		Bottom,
		// Token: 0x0400041D RID: 1053
		Center
	}
}
