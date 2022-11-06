using System;
using UnityEngine;

// Token: 0x020000DD RID: 221
[AddComponentMenu("NGUI/UI/Panel")]
[ExecuteInEditMode]
public class UIPanel : MonoBehaviour
{
	// Token: 0x17000128 RID: 296
	// (get) Token: 0x06000686 RID: 1670 RVA: 0x00024378 File Offset: 0x00022578
	public GameObject cachedGameObject
	{
		get
		{
			if (this.mGo == null)
			{
				this.mGo = base.gameObject;
			}
			return this.mGo;
		}
	}

	// Token: 0x17000129 RID: 297
	// (get) Token: 0x06000687 RID: 1671 RVA: 0x000243A0 File Offset: 0x000225A0
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x06000688 RID: 1672 RVA: 0x000243C8 File Offset: 0x000225C8
	// (set) Token: 0x06000689 RID: 1673 RVA: 0x000243D0 File Offset: 0x000225D0
	public float alpha
	{
		get
		{
			return this.mAlpha;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mAlpha != num)
			{
				this.mAlpha = num;
				for (int i = 0; i < UIDrawCall.list.size; i++)
				{
					UIDrawCall uidrawCall = UIDrawCall.list[i];
					if (uidrawCall != null && uidrawCall.panel == this)
					{
						uidrawCall.isDirty = true;
					}
				}
				for (int j = 0; j < UIWidget.list.size; j++)
				{
					UIWidget uiwidget = UIWidget.list[j];
					if (uiwidget.panel == this)
					{
						uiwidget.MarkAsChangedLite();
					}
				}
			}
		}
	}

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x0600068A RID: 1674 RVA: 0x00024484 File Offset: 0x00022684
	// (set) Token: 0x0600068B RID: 1675 RVA: 0x0002448C File Offset: 0x0002268C
	public int depth
	{
		get
		{
			return this.mDepth;
		}
		set
		{
			if (this.mDepth != value)
			{
				this.mDepth = value;
				UIPanel.mFullRebuild = true;
				for (int i = 0; i < UIDrawCall.list.size; i++)
				{
					UIDrawCall uidrawCall = UIDrawCall.list[i];
					if (uidrawCall != null)
					{
						uidrawCall.isDirty = true;
					}
				}
				for (int j = 0; j < UIWidget.list.size; j++)
				{
					UIWidget.list[j].MarkAsChangedLite();
				}
				UIPanel.list.Sort(new Comparison<UIPanel>(UIPanel.CompareFunc));
			}
		}
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x00024530 File Offset: 0x00022730
	public static int CompareFunc(UIPanel a, UIPanel b)
	{
		if (a != null && b != null)
		{
			if (a.mDepth < b.mDepth)
			{
				return -1;
			}
			if (a.mDepth > b.mDepth)
			{
				return 1;
			}
		}
		else
		{
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
		}
		return 0;
	}

	// Token: 0x1700012C RID: 300
	// (get) Token: 0x0600068D RID: 1677 RVA: 0x000245A0 File Offset: 0x000227A0
	public int drawCallCount
	{
		get
		{
			int num = 0;
			for (int i = 0; i < UIDrawCall.list.size; i++)
			{
				UIDrawCall uidrawCall = UIDrawCall.list[i];
				if (!(uidrawCall.panel != this))
				{
					num++;
				}
			}
			return num;
		}
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x000245F4 File Offset: 0x000227F4
	public void SetAlphaRecursive(float val, bool rebuildList)
	{
		if (rebuildList || this.mChildPanels == null)
		{
			this.mChildPanels = base.GetComponentsInChildren<UIPanel>(true);
		}
		int i = 0;
		int num = this.mChildPanels.Length;
		while (i < num)
		{
			this.mChildPanels[i].alpha = val;
			i++;
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x0600068F RID: 1679 RVA: 0x00024648 File Offset: 0x00022848
	// (set) Token: 0x06000690 RID: 1680 RVA: 0x00024650 File Offset: 0x00022850
	public UIDrawCall.Clipping clipping
	{
		get
		{
			return this.mClipping;
		}
		set
		{
			if (this.mClipping != value)
			{
				this.mClipping = value;
				this.mMatrixTime = 0f;
				this.UpdateDrawcalls();
			}
		}
	}

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x06000691 RID: 1681 RVA: 0x00024684 File Offset: 0x00022884
	// (set) Token: 0x06000692 RID: 1682 RVA: 0x0002468C File Offset: 0x0002288C
	public Vector4 clipRange
	{
		get
		{
			return this.mClipRange;
		}
		set
		{
			if (this.mClipRange != value)
			{
				this.mCullTime = ((this.mCullTime != 0f) ? (Time.realtimeSinceStartup + 0.15f) : 0.001f);
				this.mClipRange = value;
				this.mMatrixTime = 0f;
				this.UpdateDrawcalls();
			}
		}
	}

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x06000693 RID: 1683 RVA: 0x000246F0 File Offset: 0x000228F0
	// (set) Token: 0x06000694 RID: 1684 RVA: 0x000246F8 File Offset: 0x000228F8
	public Vector2 clipSoftness
	{
		get
		{
			return this.mClipSoftness;
		}
		set
		{
			if (this.mClipSoftness != value)
			{
				this.mClipSoftness = value;
				this.UpdateDrawcalls();
			}
		}
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x00024718 File Offset: 0x00022918
	private bool IsVisible(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		this.UpdateTransformMatrix();
		a = this.worldToLocal.MultiplyPoint3x4(a);
		b = this.worldToLocal.MultiplyPoint3x4(b);
		c = this.worldToLocal.MultiplyPoint3x4(c);
		d = this.worldToLocal.MultiplyPoint3x4(d);
		UIPanel.mTemp[0] = a.x;
		UIPanel.mTemp[1] = b.x;
		UIPanel.mTemp[2] = c.x;
		UIPanel.mTemp[3] = d.x;
		float num = Mathf.Min(UIPanel.mTemp);
		float num2 = Mathf.Max(UIPanel.mTemp);
		UIPanel.mTemp[0] = a.y;
		UIPanel.mTemp[1] = b.y;
		UIPanel.mTemp[2] = c.y;
		UIPanel.mTemp[3] = d.y;
		float num3 = Mathf.Min(UIPanel.mTemp);
		float num4 = Mathf.Max(UIPanel.mTemp);
		return num2 >= this.mMin.x && num4 >= this.mMin.y && num <= this.mMax.x && num3 <= this.mMax.y;
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x00024850 File Offset: 0x00022A50
	public bool IsVisible(Vector3 worldPos)
	{
		if (this.mAlpha < 0.001f)
		{
			return false;
		}
		if (this.mClipping == UIDrawCall.Clipping.None)
		{
			return true;
		}
		this.UpdateTransformMatrix();
		Vector3 vector = this.worldToLocal.MultiplyPoint3x4(worldPos);
		return vector.x >= this.mMin.x && vector.y >= this.mMin.y && vector.x <= this.mMax.x && vector.y <= this.mMax.y;
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x000248F4 File Offset: 0x00022AF4
	public bool IsVisible(UIWidget w)
	{
		if (this.mAlpha < 0.001f)
		{
			return false;
		}
		if (!w.enabled || !NGUITools.GetActive(w.cachedGameObject) || w.alpha < 0.001f)
		{
			return false;
		}
		if (this.mClipping == UIDrawCall.Clipping.None)
		{
			return true;
		}
		Vector3[] worldCorners = w.worldCorners;
		return this.IsVisible(worldCorners[0], worldCorners[1], worldCorners[2], worldCorners[3]);
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x0002498C File Offset: 0x00022B8C
	public static void SetDirty()
	{
		UIPanel.mFullRebuild = true;
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x00024994 File Offset: 0x00022B94
	private UIDrawCall GetDrawCall(int index, Material mat)
	{
		if (index < UIDrawCall.list.size)
		{
			UIDrawCall uidrawCall = UIDrawCall.list.buffer[index];
			if (uidrawCall != null && uidrawCall.panel == this && uidrawCall.material == mat && uidrawCall.mainTexture == mat.mainTexture)
			{
				return uidrawCall;
			}
			int i = UIDrawCall.list.size;
			while (i > index)
			{
				UIDrawCall dc = UIDrawCall.list.buffer[--i];
				UIPanel.DestroyDrawCall(dc, i);
			}
		}
		GameObject gameObject = new GameObject("_UIDrawCall [" + mat.name + "]");
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		gameObject.layer = this.cachedGameObject.layer;
		UIDrawCall uidrawCall2 = gameObject.AddComponent<UIDrawCall>();
		uidrawCall2.material = mat;
		uidrawCall2.renderQueue = UIDrawCall.list.size;
		uidrawCall2.panel = this;
		UIDrawCall.list.Add(uidrawCall2);
		return uidrawCall2;
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x00024A9C File Offset: 0x00022C9C
	private void Awake()
	{
		this.mGo = base.gameObject;
		this.mTrans = base.transform;
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x00024AB8 File Offset: 0x00022CB8
	private void Start()
	{
		this.mLayer = this.mGo.layer;
		UICamera uicamera = UICamera.FindCameraForLayer(this.mLayer);
		this.mCam = ((!(uicamera != null)) ? NGUITools.FindCameraForLayer(this.mLayer) : uicamera.cachedCamera);
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x00024B0C File Offset: 0x00022D0C
	private void OnEnable()
	{
		UIPanel.mFullRebuild = true;
		UIPanel.list.Add(this);
		UIPanel.list.Sort(new Comparison<UIPanel>(UIPanel.CompareFunc));
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x00024B38 File Offset: 0x00022D38
	private void OnDisable()
	{
		int i = UIDrawCall.list.size;
		while (i > 0)
		{
			UIDrawCall uidrawCall = UIDrawCall.list.buffer[--i];
			if (uidrawCall != null && uidrawCall.panel == this)
			{
				UIPanel.DestroyDrawCall(uidrawCall, i);
			}
		}
		UIPanel.list.Remove(this);
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x00024BA0 File Offset: 0x00022DA0
	private void UpdateTransformMatrix()
	{
		if (this.mUpdateTime == 0f || this.mMatrixTime != this.mUpdateTime)
		{
			this.mMatrixTime = this.mUpdateTime;
			this.worldToLocal = this.cachedTransform.worldToLocalMatrix;
			if (this.mClipping != UIDrawCall.Clipping.None)
			{
				Vector2 a = new Vector2(this.mClipRange.z, this.mClipRange.w);
				if (a.x == 0f)
				{
					a.x = ((!(this.mCam == null)) ? this.mCam.pixelWidth : ((float)Screen.width));
				}
				if (a.y == 0f)
				{
					a.y = ((!(this.mCam == null)) ? this.mCam.pixelHeight : ((float)Screen.height));
				}
				a *= 0.5f;
				this.mMin.x = this.mClipRange.x - a.x;
				this.mMin.y = this.mClipRange.y - a.y;
				this.mMax.x = this.mClipRange.x + a.x;
				this.mMax.y = this.mClipRange.y + a.y;
			}
		}
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x00024D18 File Offset: 0x00022F18
	private void UpdateDrawcalls()
	{
		Vector4 zero = Vector4.zero;
		if (this.mClipping != UIDrawCall.Clipping.None)
		{
			zero = new Vector4(this.mClipRange.x, this.mClipRange.y, this.mClipRange.z * 0.5f, this.mClipRange.w * 0.5f);
		}
		if (zero.z == 0f)
		{
			zero.z = (float)Screen.width * 0.5f;
		}
		if (zero.w == 0f)
		{
			zero.w = (float)Screen.height * 0.5f;
		}
		RuntimePlatform platform = Application.platform;
		if (platform == RuntimePlatform.WindowsPlayer || platform == RuntimePlatform.WindowsWebPlayer || platform == RuntimePlatform.WindowsEditor)
		{
			zero.x -= 0.5f;
			zero.y += 0.5f;
		}
		Transform cachedTransform = this.cachedTransform;
		int i = 0;
		while (i < UIDrawCall.list.size)
		{
			UIDrawCall uidrawCall = UIDrawCall.list.buffer[i];
			if (uidrawCall == null)
			{
				UIDrawCall.list.RemoveAt(i);
			}
			else
			{
				if (uidrawCall.panel == this)
				{
					uidrawCall.clipping = this.mClipping;
					uidrawCall.clipRange = zero;
					uidrawCall.clipSoftness = this.mClipSoftness;
					Transform transform = uidrawCall.transform;
					transform.position = cachedTransform.position;
					transform.rotation = cachedTransform.rotation;
					transform.localScale = cachedTransform.lossyScale;
				}
				i++;
			}
		}
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x00024EB0 File Offset: 0x000230B0
	private void LateUpdate()
	{
		if (UIPanel.list[0] != this)
		{
			return;
		}
		for (int i = 0; i < UIPanel.list.size; i++)
		{
			UIPanel uipanel = UIPanel.list[i];
			uipanel.mUpdateTime = RealTime.time;
			uipanel.UpdateTransformMatrix();
			uipanel.UpdateLayers();
			uipanel.UpdateWidgets();
		}
		if (UIPanel.mFullRebuild)
		{
			UIWidget.list.Sort(new Comparison<UIWidget>(UIWidget.CompareFunc));
			UIPanel.Fill();
		}
		else
		{
			int j = 0;
			while (j < UIDrawCall.list.size)
			{
				UIDrawCall uidrawCall = UIDrawCall.list[j];
				if (uidrawCall.isDirty && !UIPanel.Fill(uidrawCall))
				{
					UIPanel.DestroyDrawCall(uidrawCall, j);
				}
				else
				{
					j++;
				}
			}
		}
		for (int k = 0; k < UIPanel.list.size; k++)
		{
			UIPanel uipanel2 = UIPanel.list[k];
			uipanel2.UpdateDrawcalls();
		}
		UIPanel.mFullRebuild = false;
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x00024FC8 File Offset: 0x000231C8
	private static void DestroyDrawCall(UIDrawCall dc, int index)
	{
		if (dc != null)
		{
			UIDrawCall.list.RemoveAt(index);
			NGUITools.DestroyImmediate(dc.gameObject);
		}
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x00024FF8 File Offset: 0x000231F8
	private void UpdateLayers()
	{
		if (this.mLayer != this.cachedGameObject.layer)
		{
			this.mLayer = this.mGo.layer;
			UICamera uicamera = UICamera.FindCameraForLayer(this.mLayer);
			this.mCam = ((!(uicamera != null)) ? NGUITools.FindCameraForLayer(this.mLayer) : uicamera.cachedCamera);
			UIPanel.SetChildLayer(this.cachedTransform, this.mLayer);
			int i = 0;
			int size = UIDrawCall.list.size;
			while (i < size)
			{
				UIDrawCall uidrawCall = UIDrawCall.list[i];
				if (uidrawCall != null && uidrawCall.panel == this)
				{
					uidrawCall.gameObject.layer = this.mLayer;
				}
				i++;
			}
		}
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x000250C8 File Offset: 0x000232C8
	private void UpdateWidgets()
	{
		bool forceVisible = !this.cullWhileDragging && (this.clipping == UIDrawCall.Clipping.None || this.mCullTime > this.mUpdateTime);
		bool flag = false;
		int i = 0;
		int size = UIWidget.list.size;
		while (i < size)
		{
			UIWidget uiwidget = UIWidget.list[i];
			if (uiwidget.enabled && uiwidget.panel == this && uiwidget.UpdateGeometry(this, forceVisible))
			{
				flag = true;
				if (!UIPanel.mFullRebuild)
				{
					UIDrawCall drawCall = uiwidget.drawCall;
					if (drawCall != null)
					{
						drawCall.isDirty = true;
					}
					else
					{
						UIPanel.mFullRebuild = true;
					}
				}
			}
			i++;
		}
		if (flag && this.onChange != null)
		{
			this.onChange();
		}
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x000251B4 File Offset: 0x000233B4
	public void Refresh()
	{
		UIPanel.mFullRebuild = true;
		UIPanel.list[0].LateUpdate();
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x000251CC File Offset: 0x000233CC
	public Vector3 CalculateConstrainOffset(Vector2 min, Vector2 max)
	{
		float num = this.clipRange.z * 0.5f;
		float num2 = this.clipRange.w * 0.5f;
		Vector2 minRect = new Vector2(min.x, min.y);
		Vector2 maxRect = new Vector2(max.x, max.y);
		Vector2 minArea = new Vector2(this.clipRange.x - num, this.clipRange.y - num2);
		Vector2 maxArea = new Vector2(this.clipRange.x + num, this.clipRange.y + num2);
		if (this.clipping == UIDrawCall.Clipping.SoftClip)
		{
			minArea.x += this.clipSoftness.x;
			minArea.y += this.clipSoftness.y;
			maxArea.x -= this.clipSoftness.x;
			maxArea.y -= this.clipSoftness.y;
		}
		return NGUIMath.ConstrainRect(minRect, maxRect, minArea, maxArea);
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x00025314 File Offset: 0x00023514
	public bool ConstrainTargetToBounds(Transform target, ref Bounds targetBounds, bool immediate)
	{
		Vector3 b = this.CalculateConstrainOffset(targetBounds.min, targetBounds.max);
		if (b.magnitude > 0f)
		{
			if (immediate)
			{
				target.localPosition += b;
				targetBounds.center += b;
				SpringPosition component = target.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
			else
			{
				SpringPosition springPosition = SpringPosition.Begin(target.gameObject, target.localPosition + b, 13f);
				springPosition.ignoreTimeScale = true;
				springPosition.worldSpace = false;
			}
			return true;
		}
		return false;
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x000253C8 File Offset: 0x000235C8
	public bool ConstrainTargetToBounds(Transform target, bool immediate)
	{
		Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(this.cachedTransform, target);
		return this.ConstrainTargetToBounds(target, ref bounds, immediate);
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x000253EC File Offset: 0x000235EC
	private static void SetChildLayer(Transform t, int layer)
	{
		for (int i = 0; i < t.childCount; i++)
		{
			Transform child = t.GetChild(i);
			if (child.GetComponent<UIPanel>() == null)
			{
				if (child.GetComponent<UIWidget>() != null)
				{
					child.gameObject.layer = layer;
				}
				UIPanel.SetChildLayer(child, layer);
			}
		}
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x00025450 File Offset: 0x00023650
	public static UIPanel Find(Transform trans, bool createIfMissing)
	{
		Transform y = trans;
		UIPanel uipanel = null;
		while (uipanel == null && trans != null)
		{
			uipanel = trans.GetComponent<UIPanel>();
			if (uipanel != null)
			{
				break;
			}
			if (trans.parent == null)
			{
				break;
			}
			trans = trans.parent;
		}
		if (createIfMissing && uipanel == null && trans != y)
		{
			UIPanel.mFullRebuild = true;
			uipanel = trans.gameObject.AddComponent<UIPanel>();
			UIPanel.SetChildLayer(uipanel.cachedTransform, uipanel.cachedGameObject.layer);
		}
		return uipanel;
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x000254FC File Offset: 0x000236FC
	public static UIPanel Find(Transform trans)
	{
		return UIPanel.Find(trans, true);
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x00025508 File Offset: 0x00023708
	private static void Fill()
	{
		int i = UIDrawCall.list.size;
		while (i > 0)
		{
			UIPanel.DestroyDrawCall(UIDrawCall.list[--i], i);
		}
		int num = 0;
		UIPanel uipanel = null;
		Material material = null;
		UIDrawCall uidrawCall = null;
		int j = 0;
		while (j < UIWidget.list.size)
		{
			UIWidget uiwidget = UIWidget.list[j];
			if (uiwidget == null)
			{
				UIWidget.list.RemoveAt(j);
			}
			else
			{
				if (uiwidget.isVisible && uiwidget.hasVertices)
				{
					if (uipanel != uiwidget.panel || material != uiwidget.material)
					{
						if (uipanel != null && material != null && UIPanel.mVerts.size != 0)
						{
							uipanel.SubmitDrawCall(uidrawCall);
							uidrawCall = null;
						}
						uipanel = uiwidget.panel;
						material = uiwidget.material;
					}
					if (uipanel != null && material != null)
					{
						if (uidrawCall == null)
						{
							uidrawCall = uipanel.GetDrawCall(num++, material);
						}
						uiwidget.drawCall = uidrawCall;
						if (uipanel.generateNormals)
						{
							uiwidget.WriteToBuffers(UIPanel.mVerts, UIPanel.mUvs, UIPanel.mCols, UIPanel.mNorms, UIPanel.mTans);
						}
						else
						{
							uiwidget.WriteToBuffers(UIPanel.mVerts, UIPanel.mUvs, UIPanel.mCols, null, null);
						}
					}
				}
				else
				{
					uiwidget.drawCall = null;
				}
				j++;
			}
		}
		if (UIPanel.mVerts.size != 0)
		{
			uipanel.SubmitDrawCall(uidrawCall);
		}
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x000256BC File Offset: 0x000238BC
	private void SubmitDrawCall(UIDrawCall dc)
	{
		dc.Set(UIPanel.mVerts, (!this.generateNormals) ? null : UIPanel.mNorms, (!this.generateNormals) ? null : UIPanel.mTans, UIPanel.mUvs, UIPanel.mCols);
		UIPanel.mVerts.Clear();
		UIPanel.mNorms.Clear();
		UIPanel.mTans.Clear();
		UIPanel.mUvs.Clear();
		UIPanel.mCols.Clear();
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x0002573C File Offset: 0x0002393C
	private static bool Fill(UIDrawCall dc)
	{
		if (dc != null)
		{
			dc.isDirty = false;
			int i = 0;
			while (i < UIWidget.list.size)
			{
				UIWidget uiwidget = UIWidget.list[i];
				if (uiwidget == null)
				{
					UIWidget.list.RemoveAt(i);
				}
				else
				{
					if (uiwidget.drawCall == dc)
					{
						if (uiwidget.isVisible && uiwidget.hasVertices)
						{
							if (dc.panel.generateNormals)
							{
								uiwidget.WriteToBuffers(UIPanel.mVerts, UIPanel.mUvs, UIPanel.mCols, UIPanel.mNorms, UIPanel.mTans);
							}
							else
							{
								uiwidget.WriteToBuffers(UIPanel.mVerts, UIPanel.mUvs, UIPanel.mCols, null, null);
							}
						}
						else
						{
							uiwidget.drawCall = null;
						}
					}
					i++;
				}
			}
			if (UIPanel.mVerts.size != 0)
			{
				dc.Set(UIPanel.mVerts, (!dc.panel.generateNormals) ? null : UIPanel.mNorms, (!dc.panel.generateNormals) ? null : UIPanel.mTans, UIPanel.mUvs, UIPanel.mCols);
				UIPanel.mVerts.Clear();
				UIPanel.mNorms.Clear();
				UIPanel.mTans.Clear();
				UIPanel.mUvs.Clear();
				UIPanel.mCols.Clear();
				return true;
			}
		}
		return false;
	}

	// Token: 0x040004DC RID: 1244
	public static BetterList<UIPanel> list = new BetterList<UIPanel>();

	// Token: 0x040004DD RID: 1245
	public UIPanel.OnChangeDelegate onChange;

	// Token: 0x040004DE RID: 1246
	public bool showInPanelTool = true;

	// Token: 0x040004DF RID: 1247
	public bool generateNormals;

	// Token: 0x040004E0 RID: 1248
	public bool widgetsAreStatic;

	// Token: 0x040004E1 RID: 1249
	public bool cullWhileDragging;

	// Token: 0x040004E2 RID: 1250
	[HideInInspector]
	public Matrix4x4 worldToLocal = Matrix4x4.identity;

	// Token: 0x040004E3 RID: 1251
	[HideInInspector]
	[SerializeField]
	private float mAlpha = 1f;

	// Token: 0x040004E4 RID: 1252
	[HideInInspector]
	[SerializeField]
	private UIDrawCall.Clipping mClipping;

	// Token: 0x040004E5 RID: 1253
	[HideInInspector]
	[SerializeField]
	private Vector4 mClipRange = Vector4.zero;

	// Token: 0x040004E6 RID: 1254
	[SerializeField]
	[HideInInspector]
	private Vector2 mClipSoftness = new Vector2(40f, 40f);

	// Token: 0x040004E7 RID: 1255
	[SerializeField]
	[HideInInspector]
	private int mDepth;

	// Token: 0x040004E8 RID: 1256
	private static bool mFullRebuild = false;

	// Token: 0x040004E9 RID: 1257
	private static BetterList<Vector3> mVerts = new BetterList<Vector3>();

	// Token: 0x040004EA RID: 1258
	private static BetterList<Vector3> mNorms = new BetterList<Vector3>();

	// Token: 0x040004EB RID: 1259
	private static BetterList<Vector4> mTans = new BetterList<Vector4>();

	// Token: 0x040004EC RID: 1260
	private static BetterList<Vector2> mUvs = new BetterList<Vector2>();

	// Token: 0x040004ED RID: 1261
	private static BetterList<Color32> mCols = new BetterList<Color32>();

	// Token: 0x040004EE RID: 1262
	private GameObject mGo;

	// Token: 0x040004EF RID: 1263
	private Transform mTrans;

	// Token: 0x040004F0 RID: 1264
	private Camera mCam;

	// Token: 0x040004F1 RID: 1265
	private int mLayer = -1;

	// Token: 0x040004F2 RID: 1266
	private float mCullTime;

	// Token: 0x040004F3 RID: 1267
	private float mUpdateTime;

	// Token: 0x040004F4 RID: 1268
	private float mMatrixTime;

	// Token: 0x040004F5 RID: 1269
	private static float[] mTemp = new float[4];

	// Token: 0x040004F6 RID: 1270
	private Vector2 mMin = Vector2.zero;

	// Token: 0x040004F7 RID: 1271
	private Vector2 mMax = Vector2.zero;

	// Token: 0x040004F8 RID: 1272
	private UIPanel[] mChildPanels;

	// Token: 0x020000DE RID: 222
	public enum DebugInfo
	{
		// Token: 0x040004FA RID: 1274
		None,
		// Token: 0x040004FB RID: 1275
		Gizmos,
		// Token: 0x040004FC RID: 1276
		Geometry
	}

	// Token: 0x02000A73 RID: 2675
	// (Invoke) Token: 0x06004806 RID: 18438
	public delegate void OnChangeDelegate();
}
