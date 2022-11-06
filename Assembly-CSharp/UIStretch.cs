using System;
using UnityEngine;

// Token: 0x020000E6 RID: 230
[AddComponentMenu("NGUI/UI/Stretch")]
[ExecuteInEditMode]
public class UIStretch : MonoBehaviour
{
	// Token: 0x060006F1 RID: 1777 RVA: 0x00027EC0 File Offset: 0x000260C0
	private void OnEnable()
	{
		this.mAnim = base.animation;
		this.mRect = default(Rect);
		this.mTrans = base.transform;
		this.mWidget = base.GetComponent<UIWidget>();
		this.mSprite = base.GetComponent<UISprite>();
		this.mPanel = base.GetComponent<UIPanel>();
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x00027F18 File Offset: 0x00026118
	private void Start()
	{
		if (this.container == null && this.widgetContainer != null)
		{
			this.container = this.widgetContainer.gameObject;
			this.widgetContainer = null;
		}
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
		this.Update();
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x00027FA0 File Offset: 0x000261A0
	private void Update()
	{
		if (this.mAnim != null && this.mAnim.isPlaying)
		{
			return;
		}
		if (this.style != UIStretch.Style.None)
		{
			UIWidget uiwidget = (!(this.container == null)) ? this.container.GetComponent<UIWidget>() : null;
			UIPanel uipanel = (!(this.container == null) || !(uiwidget == null)) ? this.container.GetComponent<UIPanel>() : null;
			float num = 1f;
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
					float num2 = (!(this.mRoot != null)) ? 0.5f : ((float)this.mRoot.activeHeight / (float)Screen.height * 0.5f);
					this.mRect.xMin = (float)(-(float)Screen.width) * num2;
					this.mRect.yMin = (float)(-(float)Screen.height) * num2;
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
				this.mRect = this.uiCamera.pixelRect;
				if (this.mRoot != null)
				{
					num = this.mRoot.pixelSizeAdjustment;
				}
			}
			float num3 = this.mRect.width;
			float num4 = this.mRect.height;
			if (num != 1f && num4 > 1f)
			{
				float num5 = (float)this.mRoot.activeHeight / num4;
				num3 *= num5;
				num4 *= num5;
			}
			Vector3 vector = (!(this.mWidget != null)) ? this.mTrans.localScale : new Vector3((float)this.mWidget.width, (float)this.mWidget.height);
			if (this.style == UIStretch.Style.BasedOnHeight)
			{
				vector.x = this.relativeSize.x * num4;
				vector.y = this.relativeSize.y * num4;
			}
			else if (this.style == UIStretch.Style.FillKeepingRatio)
			{
				float num6 = num3 / num4;
				float num7 = this.initialSize.x / this.initialSize.y;
				if (num7 < num6)
				{
					float num8 = num3 / this.initialSize.x;
					vector.x = num3;
					vector.y = this.initialSize.y * num8;
				}
				else
				{
					float num9 = num4 / this.initialSize.y;
					vector.x = this.initialSize.x * num9;
					vector.y = num4;
				}
			}
			else if (this.style == UIStretch.Style.FitInternalKeepingRatio)
			{
				float num10 = num3 / num4;
				float num11 = this.initialSize.x / this.initialSize.y;
				if (num11 > num10)
				{
					float num12 = num3 / this.initialSize.x;
					vector.x = num3;
					vector.y = this.initialSize.y * num12;
				}
				else
				{
					float num13 = num4 / this.initialSize.y;
					vector.x = this.initialSize.x * num13;
					vector.y = num4;
				}
			}
			else
			{
				if (this.style != UIStretch.Style.Vertical)
				{
					vector.x = this.relativeSize.x * num3;
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					vector.y = this.relativeSize.y * num4;
				}
			}
			if (this.mSprite != null)
			{
				float num14 = (!(this.mSprite.atlas != null)) ? 1f : this.mSprite.atlas.pixelSize;
				vector.x -= this.borderPadding.x * num14;
				vector.y -= this.borderPadding.y * num14;
				if (this.style != UIStretch.Style.Vertical)
				{
					this.mSprite.width = Mathf.RoundToInt(vector.x);
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					this.mSprite.height = Mathf.RoundToInt(vector.y);
				}
				vector = Vector3.one;
			}
			else if (this.mWidget != null)
			{
				if (this.style != UIStretch.Style.Vertical)
				{
					this.mWidget.width = Mathf.RoundToInt(vector.x - this.borderPadding.x);
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					this.mWidget.height = Mathf.RoundToInt(vector.y - this.borderPadding.y);
				}
				vector = Vector3.one;
			}
			else if (this.mPanel != null)
			{
				Vector4 clipRange2 = this.mPanel.clipRange;
				if (this.style != UIStretch.Style.Vertical)
				{
					clipRange2.z = vector.x - this.borderPadding.x;
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					clipRange2.w = vector.y - this.borderPadding.y;
				}
				this.mPanel.clipRange = clipRange2;
				vector = Vector3.one;
			}
			else
			{
				if (this.style != UIStretch.Style.Vertical)
				{
					vector.x -= this.borderPadding.x;
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					vector.y -= this.borderPadding.y;
				}
			}
			if (this.mTrans.localScale != vector)
			{
				this.mTrans.localScale = vector;
			}
			if (this.runOnlyOnce && Application.isPlaying)
			{
				UnityEngine.Object.Destroy(this);
			}
		}
	}

	// Token: 0x04000532 RID: 1330
	public Camera uiCamera;

	// Token: 0x04000533 RID: 1331
	public GameObject container;

	// Token: 0x04000534 RID: 1332
	public UIStretch.Style style;

	// Token: 0x04000535 RID: 1333
	public bool runOnlyOnce;

	// Token: 0x04000536 RID: 1334
	public Vector2 relativeSize = Vector2.one;

	// Token: 0x04000537 RID: 1335
	public Vector2 initialSize = Vector2.one;

	// Token: 0x04000538 RID: 1336
	public Vector2 borderPadding = Vector2.zero;

	// Token: 0x04000539 RID: 1337
	[SerializeField]
	[HideInInspector]
	private UIWidget widgetContainer;

	// Token: 0x0400053A RID: 1338
	private Transform mTrans;

	// Token: 0x0400053B RID: 1339
	private UIWidget mWidget;

	// Token: 0x0400053C RID: 1340
	private UISprite mSprite;

	// Token: 0x0400053D RID: 1341
	private UIPanel mPanel;

	// Token: 0x0400053E RID: 1342
	private UIRoot mRoot;

	// Token: 0x0400053F RID: 1343
	private Animation mAnim;

	// Token: 0x04000540 RID: 1344
	private Rect mRect;

	// Token: 0x020000E7 RID: 231
	public enum Style
	{
		// Token: 0x04000542 RID: 1346
		None,
		// Token: 0x04000543 RID: 1347
		Horizontal,
		// Token: 0x04000544 RID: 1348
		Vertical,
		// Token: 0x04000545 RID: 1349
		Both,
		// Token: 0x04000546 RID: 1350
		BasedOnHeight,
		// Token: 0x04000547 RID: 1351
		FillKeepingRatio,
		// Token: 0x04000548 RID: 1352
		FitInternalKeepingRatio
	}
}
