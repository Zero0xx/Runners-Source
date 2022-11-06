using System;
using UnityEngine;

// Token: 0x020000EC RID: 236
[AddComponentMenu("NGUI/UI/Tooltip")]
public class UITooltip : MonoBehaviour
{
	// Token: 0x0600070F RID: 1807 RVA: 0x000292A0 File Offset: 0x000274A0
	private void Awake()
	{
		UITooltip.mInstance = this;
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x000292A8 File Offset: 0x000274A8
	private void OnDestroy()
	{
		UITooltip.mInstance = null;
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x000292B0 File Offset: 0x000274B0
	private void Start()
	{
		this.mTrans = base.transform;
		this.mWidgets = base.GetComponentsInChildren<UIWidget>();
		this.mPos = this.mTrans.localPosition;
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.SetAlpha(0f);
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x00029318 File Offset: 0x00027518
	private void Update()
	{
		if (this.mCurrent != this.mTarget)
		{
			this.mCurrent = Mathf.Lerp(this.mCurrent, this.mTarget, Time.deltaTime * this.appearSpeed);
			if (Mathf.Abs(this.mCurrent - this.mTarget) < 0.001f)
			{
				this.mCurrent = this.mTarget;
			}
			this.SetAlpha(this.mCurrent * this.mCurrent);
			if (this.scalingTransitions)
			{
				Vector3 b = this.mSize * 0.25f;
				b.y = -b.y;
				Vector3 localScale = Vector3.one * (1.5f - this.mCurrent * 0.5f);
				Vector3 localPosition = Vector3.Lerp(this.mPos - b, this.mPos, this.mCurrent);
				this.mTrans.localPosition = localPosition;
				this.mTrans.localScale = localScale;
			}
		}
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x00029414 File Offset: 0x00027614
	private void SetAlpha(float val)
	{
		int i = 0;
		int num = this.mWidgets.Length;
		while (i < num)
		{
			UIWidget uiwidget = this.mWidgets[i];
			Color color = uiwidget.color;
			color.a = val;
			uiwidget.color = color;
			i++;
		}
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x0002945C File Offset: 0x0002765C
	private void SetText(string tooltipText)
	{
		if (this.text != null && !string.IsNullOrEmpty(tooltipText))
		{
			this.mTarget = 1f;
			if (this.text != null)
			{
				this.text.text = tooltipText;
			}
			this.mPos = Input.mousePosition;
			if (this.background != null)
			{
				Transform transform = this.text.transform;
				Vector3 localPosition = transform.localPosition;
				Vector3 localScale = transform.localScale;
				this.mSize = this.text.printedSize;
				this.mSize.x = this.mSize.x * localScale.x;
				this.mSize.y = this.mSize.y * localScale.y;
				Vector4 border = this.background.border;
				this.mSize.x = this.mSize.x + (border.x + border.z + (localPosition.x - border.x) * 2f);
				this.mSize.y = this.mSize.y + (border.y + border.w + (-localPosition.y - border.y) * 2f);
				this.background.width = Mathf.RoundToInt(this.mSize.x);
				this.background.height = Mathf.RoundToInt(this.mSize.y);
			}
			if (this.uiCamera != null)
			{
				this.mPos.x = Mathf.Clamp01(this.mPos.x / (float)Screen.width);
				this.mPos.y = Mathf.Clamp01(this.mPos.y / (float)Screen.height);
				float num = this.uiCamera.orthographicSize / this.mTrans.parent.lossyScale.y;
				float num2 = (float)Screen.height * 0.5f / num;
				Vector2 vector = new Vector2(num2 * this.mSize.x / (float)Screen.width, num2 * this.mSize.y / (float)Screen.height);
				this.mPos.x = Mathf.Min(this.mPos.x, 1f - vector.x);
				this.mPos.y = Mathf.Max(this.mPos.y, vector.y);
				this.mTrans.position = this.uiCamera.ViewportToWorldPoint(this.mPos);
				this.mPos = this.mTrans.localPosition;
				this.mPos.x = Mathf.Round(this.mPos.x);
				this.mPos.y = Mathf.Round(this.mPos.y);
				this.mTrans.localPosition = this.mPos;
			}
			else
			{
				if (this.mPos.x + this.mSize.x > (float)Screen.width)
				{
					this.mPos.x = (float)Screen.width - this.mSize.x;
				}
				if (this.mPos.y - this.mSize.y < 0f)
				{
					this.mPos.y = this.mSize.y;
				}
				this.mPos.x = this.mPos.x - (float)Screen.width * 0.5f;
				this.mPos.y = this.mPos.y - (float)Screen.height * 0.5f;
			}
		}
		else
		{
			this.mTarget = 0f;
		}
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x00029820 File Offset: 0x00027A20
	public static void ShowText(string tooltipText)
	{
		if (UITooltip.mInstance != null)
		{
			UITooltip.mInstance.SetText(tooltipText);
		}
	}

	// Token: 0x0400055F RID: 1375
	private static UITooltip mInstance;

	// Token: 0x04000560 RID: 1376
	public Camera uiCamera;

	// Token: 0x04000561 RID: 1377
	public UILabel text;

	// Token: 0x04000562 RID: 1378
	public UISprite background;

	// Token: 0x04000563 RID: 1379
	public float appearSpeed = 10f;

	// Token: 0x04000564 RID: 1380
	public bool scalingTransitions = true;

	// Token: 0x04000565 RID: 1381
	private Transform mTrans;

	// Token: 0x04000566 RID: 1382
	private float mTarget;

	// Token: 0x04000567 RID: 1383
	private float mCurrent;

	// Token: 0x04000568 RID: 1384
	private Vector3 mPos;

	// Token: 0x04000569 RID: 1385
	private Vector3 mSize = Vector3.zero;

	// Token: 0x0400056A RID: 1386
	private UIWidget[] mWidgets;
}
