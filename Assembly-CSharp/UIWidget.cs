using System;
using UnityEngine;

// Token: 0x020000B5 RID: 181
public abstract class UIWidget : MonoBehaviour
{
	// Token: 0x170000BB RID: 187
	// (get) Token: 0x0600052B RID: 1323 RVA: 0x0001A694 File Offset: 0x00018894
	// (set) Token: 0x0600052C RID: 1324 RVA: 0x0001A69C File Offset: 0x0001889C
	public UIDrawCall drawCall { get; set; }

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x0600052D RID: 1325 RVA: 0x0001A6A8 File Offset: 0x000188A8
	public bool isVisible
	{
		get
		{
			return this.mVisibleByPanel && this.finalAlpha > 0.001f;
		}
	}

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x0600052E RID: 1326 RVA: 0x0001A6C8 File Offset: 0x000188C8
	// (set) Token: 0x0600052F RID: 1327 RVA: 0x0001A6D0 File Offset: 0x000188D0
	public int width
	{
		get
		{
			return this.mWidth;
		}
		set
		{
			int minWidth = this.minWidth;
			if (value < minWidth)
			{
				value = minWidth;
			}
			if (this.mWidth != value)
			{
				this.mWidth = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001A708 File Offset: 0x00018908
	// (set) Token: 0x06000531 RID: 1329 RVA: 0x0001A710 File Offset: 0x00018910
	public int height
	{
		get
		{
			return this.mHeight;
		}
		set
		{
			int minHeight = this.minHeight;
			if (value < minHeight)
			{
				value = minHeight;
			}
			if (this.mHeight != value)
			{
				this.mHeight = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x170000BF RID: 191
	// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001A748 File Offset: 0x00018948
	// (set) Token: 0x06000533 RID: 1331 RVA: 0x0001A750 File Offset: 0x00018950
	public Color color
	{
		get
		{
			return this.mColor;
		}
		set
		{
			if (!this.mColor.Equals(value))
			{
				this.mColor = value;
				this.mChanged = true;
			}
		}
	}

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001A784 File Offset: 0x00018984
	// (set) Token: 0x06000535 RID: 1333 RVA: 0x0001A794 File Offset: 0x00018994
	public float alpha
	{
		get
		{
			return this.mColor.a;
		}
		set
		{
			Color color = this.mColor;
			color.a = value;
			this.color = color;
		}
	}

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x06000536 RID: 1334 RVA: 0x0001A7B8 File Offset: 0x000189B8
	public float finalAlpha
	{
		get
		{
			if (this.mPanel == null)
			{
				this.CreatePanel();
			}
			return (!(this.mPanel != null)) ? this.mColor.a : (this.mColor.a * this.mPanel.alpha);
		}
	}

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x06000537 RID: 1335 RVA: 0x0001A814 File Offset: 0x00018A14
	// (set) Token: 0x06000538 RID: 1336 RVA: 0x0001A81C File Offset: 0x00018A1C
	public UIWidget.Pivot pivot
	{
		get
		{
			return this.mPivot;
		}
		set
		{
			if (this.mPivot != value)
			{
				Vector3 vector = this.worldCorners[0];
				this.mPivot = value;
				this.mChanged = true;
				Vector3 vector2 = this.worldCorners[0];
				Transform cachedTransform = this.cachedTransform;
				Vector3 vector3 = cachedTransform.position;
				float z = cachedTransform.localPosition.z;
				vector3.x += vector.x - vector2.x;
				vector3.y += vector.y - vector2.y;
				this.cachedTransform.position = vector3;
				vector3 = this.cachedTransform.localPosition;
				vector3.x = Mathf.Round(vector3.x);
				vector3.y = Mathf.Round(vector3.y);
				vector3.z = z;
				this.cachedTransform.localPosition = vector3;
			}
		}
	}

	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x06000539 RID: 1337 RVA: 0x0001A914 File Offset: 0x00018B14
	// (set) Token: 0x0600053A RID: 1338 RVA: 0x0001A91C File Offset: 0x00018B1C
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
				UIPanel.SetDirty();
			}
		}
	}

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x0600053B RID: 1339 RVA: 0x0001A938 File Offset: 0x00018B38
	public int raycastDepth
	{
		get
		{
			return (!(this.mPanel != null)) ? this.mDepth : (this.mDepth + this.mPanel.depth * 1000);
		}
	}

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x0600053C RID: 1340 RVA: 0x0001A97C File Offset: 0x00018B7C
	public virtual Vector3[] localCorners
	{
		get
		{
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float x = num + (float)this.mWidth;
			float y = num2 + (float)this.mHeight;
			this.mCorners[0] = new Vector3(num, num2, 0f);
			this.mCorners[1] = new Vector3(num, y, 0f);
			this.mCorners[2] = new Vector3(x, y, 0f);
			this.mCorners[3] = new Vector3(x, num2, 0f);
			return this.mCorners;
		}
	}

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x0600053D RID: 1341 RVA: 0x0001AA44 File Offset: 0x00018C44
	public virtual Vector2 localSize
	{
		get
		{
			Vector3[] localCorners = this.localCorners;
			return localCorners[2] - localCorners[0];
		}
	}

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x0600053E RID: 1342 RVA: 0x0001AA7C File Offset: 0x00018C7C
	public virtual Vector3[] worldCorners
	{
		get
		{
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float x = num + (float)this.mWidth;
			float y = num2 + (float)this.mHeight;
			Transform cachedTransform = this.cachedTransform;
			this.mCorners[0] = cachedTransform.TransformPoint(num, num2, 0f);
			this.mCorners[1] = cachedTransform.TransformPoint(num, y, 0f);
			this.mCorners[2] = cachedTransform.TransformPoint(x, y, 0f);
			this.mCorners[3] = cachedTransform.TransformPoint(x, num2, 0f);
			return this.mCorners;
		}
	}

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x0600053F RID: 1343 RVA: 0x0001AB54 File Offset: 0x00018D54
	public Vector3[] innerWorldCorners
	{
		get
		{
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			Vector4 border = this.border;
			num += border.x;
			num2 += border.y;
			num3 -= border.z;
			num4 -= border.w;
			Transform cachedTransform = this.cachedTransform;
			this.mCorners[0] = cachedTransform.TransformPoint(num, num2, 0f);
			this.mCorners[1] = cachedTransform.TransformPoint(num, num4, 0f);
			this.mCorners[2] = cachedTransform.TransformPoint(num3, num4, 0f);
			this.mCorners[3] = cachedTransform.TransformPoint(num3, num2, 0f);
			return this.mCorners;
		}
	}

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x06000540 RID: 1344 RVA: 0x0001AC60 File Offset: 0x00018E60
	public bool hasVertices
	{
		get
		{
			return this.mGeom != null && this.mGeom.hasVertices;
		}
	}

	// Token: 0x170000CA RID: 202
	// (get) Token: 0x06000541 RID: 1345 RVA: 0x0001AC7C File Offset: 0x00018E7C
	public Vector2 pivotOffset
	{
		get
		{
			return NGUIMath.GetPivotOffset(this.pivot);
		}
	}

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001AC8C File Offset: 0x00018E8C
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

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x06000543 RID: 1347 RVA: 0x0001ACB4 File Offset: 0x00018EB4
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

	// Token: 0x170000CD RID: 205
	// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001ACDC File Offset: 0x00018EDC
	// (set) Token: 0x06000545 RID: 1349 RVA: 0x0001ACE0 File Offset: 0x00018EE0
	public virtual Material material
	{
		get
		{
			return null;
		}
		set
		{
			throw new NotImplementedException(base.GetType() + " has no material setter");
		}
	}

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001ACF8 File Offset: 0x00018EF8
	// (set) Token: 0x06000547 RID: 1351 RVA: 0x0001AD24 File Offset: 0x00018F24
	public virtual Texture mainTexture
	{
		get
		{
			Material material = this.material;
			return (!(material != null)) ? null : material.mainTexture;
		}
		set
		{
			throw new NotImplementedException(base.GetType() + " has no mainTexture setter");
		}
	}

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x06000548 RID: 1352 RVA: 0x0001AD3C File Offset: 0x00018F3C
	// (set) Token: 0x06000549 RID: 1353 RVA: 0x0001AD5C File Offset: 0x00018F5C
	public UIPanel panel
	{
		get
		{
			if (this.mPanel == null)
			{
				this.CreatePanel();
			}
			return this.mPanel;
		}
		set
		{
			this.mPanel = value;
		}
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x0600054A RID: 1354 RVA: 0x0001AD68 File Offset: 0x00018F68
	[Obsolete("There is no relative scale anymore. Widgets now have width and height instead")]
	public Vector2 relativeSize
	{
		get
		{
			return Vector2.one;
		}
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x0001AD70 File Offset: 0x00018F70
	public static int CompareFunc(UIWidget left, UIWidget right)
	{
		int num = UIPanel.CompareFunc(left.mPanel, right.mPanel);
		if (num == 0)
		{
			if (left.mDepth < right.mDepth)
			{
				return -1;
			}
			if (left.mDepth > right.mDepth)
			{
				return 1;
			}
		}
		return num;
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x0001ADBC File Offset: 0x00018FBC
	public Bounds CalculateBounds()
	{
		return this.CalculateBounds(null);
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x0001ADC8 File Offset: 0x00018FC8
	public Bounds CalculateBounds(Transform relativeParent)
	{
		if (relativeParent == null)
		{
			Vector3[] localCorners = this.localCorners;
			Bounds result = new Bounds(localCorners[0], Vector3.zero);
			for (int i = 1; i < 4; i++)
			{
				result.Encapsulate(localCorners[i]);
			}
			return result;
		}
		Matrix4x4 worldToLocalMatrix = relativeParent.worldToLocalMatrix;
		Vector3[] worldCorners = this.worldCorners;
		Bounds result2 = new Bounds(worldToLocalMatrix.MultiplyPoint3x4(worldCorners[0]), Vector3.zero);
		for (int j = 1; j < 4; j++)
		{
			result2.Encapsulate(worldToLocalMatrix.MultiplyPoint3x4(worldCorners[j]));
		}
		return result2;
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x0001AE8C File Offset: 0x0001908C
	private void SetDirty()
	{
		if (this.drawCall != null)
		{
			this.drawCall.isDirty = true;
		}
		else if (this.isVisible && this.hasVertices)
		{
			UIPanel.SetDirty();
		}
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x0001AED8 File Offset: 0x000190D8
	protected void RemoveFromPanel()
	{
		if (this.mPanel != null)
		{
			this.drawCall = null;
			this.mPanel = null;
			this.SetDirty();
		}
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x0001AF00 File Offset: 0x00019100
	public void MarkAsChangedLite()
	{
		this.mChanged = true;
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x0001AF0C File Offset: 0x0001910C
	public virtual void MarkAsChanged()
	{
		this.mChanged = true;
		if (this.mPanel != null && base.enabled && NGUITools.GetActive(base.gameObject) && !Application.isPlaying && this.material != null)
		{
			this.SetDirty();
			this.CheckLayer();
		}
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x0001AF74 File Offset: 0x00019174
	public void CreatePanel()
	{
		if (this.mPanel == null && base.enabled && NGUITools.GetActive(base.gameObject) && this.material != null)
		{
			this.mPanel = UIPanel.Find(this.cachedTransform, this.mStarted);
			if (this.mPanel != null)
			{
				this.CheckLayer();
				this.mChanged = true;
				UIPanel.SetDirty();
			}
		}
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x0001AFF8 File Offset: 0x000191F8
	public void CheckLayer()
	{
		if (this.mPanel != null && this.mPanel.gameObject.layer != base.gameObject.layer)
		{
			global::Debug.LogWarning("You can't place widgets on a layer different than the UIPanel that manages them.\nIf you want to move widgets to a different layer, parent them to a new panel instead.", this);
			base.gameObject.layer = this.mPanel.gameObject.layer;
		}
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x0001B05C File Offset: 0x0001925C
	public void ParentHasChanged()
	{
		if (this.mPanel != null)
		{
			UIPanel y = UIPanel.Find(this.cachedTransform);
			if (this.mPanel != y)
			{
				this.RemoveFromPanel();
				this.CreatePanel();
			}
		}
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x0001B0A4 File Offset: 0x000192A4
	protected virtual void Awake()
	{
		this.mGo = base.gameObject;
		this.mPlayMode = Application.isPlaying;
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x0001B0C0 File Offset: 0x000192C0
	protected virtual void OnEnable()
	{
		UIWidget.list.Add(this);
		this.mChanged = true;
		this.mPanel = null;
		if (this.mWidth == 0 && this.mHeight == 0)
		{
			this.UpgradeFrom265();
			this.cachedTransform.localScale = Vector3.one;
		}
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x0001B114 File Offset: 0x00019314
	protected virtual void UpgradeFrom265()
	{
		Vector3 localScale = this.cachedTransform.localScale;
		this.mWidth = Mathf.Abs(Mathf.RoundToInt(localScale.x));
		this.mHeight = Mathf.Abs(Mathf.RoundToInt(localScale.y));
		if (base.GetComponent<BoxCollider>() != null)
		{
			NGUITools.AddWidgetCollider(base.gameObject, true);
		}
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x0001B17C File Offset: 0x0001937C
	private void Start()
	{
		this.mStarted = true;
		this.OnStart();
		this.CreatePanel();
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x0001B194 File Offset: 0x00019394
	public virtual void Update()
	{
		if (this.mPanel == null)
		{
			this.CreatePanel();
		}
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x0001B1B0 File Offset: 0x000193B0
	protected virtual void OnDisable()
	{
		UIWidget.list.Remove(this);
		this.RemoveFromPanel();
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x0001B1C4 File Offset: 0x000193C4
	private void OnDestroy()
	{
		this.RemoveFromPanel();
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x0001B1CC File Offset: 0x000193CC
	private bool HasTransformChanged()
	{
		if (this.cachedTransform.hasChanged)
		{
			this.mTrans.hasChanged = false;
			return true;
		}
		return false;
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x0001B1F0 File Offset: 0x000193F0
	public bool UpdateGeometry(UIPanel p, bool forceVisible)
	{
		if (this.material != null && p != null)
		{
			this.mPanel = p;
			bool flag = false;
			float finalAlpha = this.finalAlpha;
			bool flag2 = finalAlpha > 0.001f;
			bool flag3 = forceVisible || this.mVisibleByPanel;
			if (this.HasTransformChanged())
			{
				if (!this.mPanel.widgetsAreStatic)
				{
					this.mLocalToPanel = p.worldToLocal * this.cachedTransform.localToWorldMatrix;
					flag = true;
					Vector2 pivotOffset = this.pivotOffset;
					float num = -pivotOffset.x * (float)this.mWidth;
					float num2 = -pivotOffset.y * (float)this.mHeight;
					float x = num + (float)this.mWidth;
					float y = num2 + (float)this.mHeight;
					Transform cachedTransform = this.cachedTransform;
					Vector3 vector = cachedTransform.TransformPoint(num, num2, 0f);
					Vector3 vector2 = cachedTransform.TransformPoint(x, y, 0f);
					vector = p.worldToLocal.MultiplyPoint3x4(vector);
					vector2 = p.worldToLocal.MultiplyPoint3x4(vector2);
					if (Vector3.SqrMagnitude(this.mOldV0 - vector) > 1E-06f || Vector3.SqrMagnitude(this.mOldV1 - vector2) > 1E-06f)
					{
						this.mChanged = true;
						this.mOldV0 = vector;
						this.mOldV1 = vector2;
					}
				}
				if (flag2 || this.mForceVisible != forceVisible)
				{
					this.mForceVisible = forceVisible;
					flag3 = (forceVisible || this.mPanel.IsVisible(this));
				}
			}
			else if (flag2 && this.mForceVisible != forceVisible)
			{
				this.mForceVisible = forceVisible;
				flag3 = this.mPanel.IsVisible(this);
			}
			if (this.mVisibleByPanel != flag3)
			{
				this.mVisibleByPanel = flag3;
				this.mChanged = true;
			}
			if (this.mVisibleByPanel && this.mLastAlpha != finalAlpha)
			{
				this.mChanged = true;
			}
			this.mLastAlpha = finalAlpha;
			if (this.mChanged)
			{
				this.mChanged = false;
				if (this.isVisible)
				{
					bool hasVertices = this.mGeom.hasVertices;
					this.mGeom.Clear();
					this.OnFill(this.mGeom.verts, this.mGeom.uvs, this.mGeom.cols);
					if (this.mGeom.hasVertices)
					{
						if (!flag)
						{
							this.mLocalToPanel = p.worldToLocal * this.cachedTransform.localToWorldMatrix;
						}
						this.mGeom.ApplyTransform(this.mLocalToPanel);
						return true;
					}
					return hasVertices;
				}
				else if (this.mGeom.hasVertices)
				{
					this.mGeom.Clear();
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x0001B4B4 File Offset: 0x000196B4
	public void WriteToBuffers(BetterList<Vector3> v, BetterList<Vector2> u, BetterList<Color32> c, BetterList<Vector3> n, BetterList<Vector4> t)
	{
		this.mGeom.WriteToBuffers(v, u, c, n, t);
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x0001B4C8 File Offset: 0x000196C8
	public int WriteToBuffers(BetterList<Vector3> v, BetterList<Vector2> u, BetterList<Color32> c, BetterList<Vector3> n, BetterList<Vector4> t, int point)
	{
		return this.mGeom.WriteToBuffers(v, u, c, n, t, point);
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0001B4E0 File Offset: 0x000196E0
	public virtual void MakePixelPerfect()
	{
		Vector3 localPosition = this.cachedTransform.localPosition;
		localPosition.z = Mathf.Round(localPosition.z);
		localPosition.x = Mathf.Round(localPosition.x);
		localPosition.y = Mathf.Round(localPosition.y);
		this.cachedTransform.localPosition = localPosition;
		Vector3 localScale = this.cachedTransform.localScale;
		this.cachedTransform.localScale = new Vector3(Mathf.Sign(localScale.x), Mathf.Sign(localScale.y), 1f);
	}

	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x06000561 RID: 1377 RVA: 0x0001B578 File Offset: 0x00019778
	public virtual int minWidth
	{
		get
		{
			return 4;
		}
	}

	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x06000562 RID: 1378 RVA: 0x0001B57C File Offset: 0x0001977C
	public virtual int minHeight
	{
		get
		{
			return 4;
		}
	}

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x06000563 RID: 1379 RVA: 0x0001B580 File Offset: 0x00019780
	public virtual Vector4 border
	{
		get
		{
			return Vector4.zero;
		}
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x0001B588 File Offset: 0x00019788
	protected virtual void OnStart()
	{
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x0001B58C File Offset: 0x0001978C
	public virtual void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
	}

	// Token: 0x0400038B RID: 907
	public static BetterList<UIWidget> list = new BetterList<UIWidget>();

	// Token: 0x0400038C RID: 908
	[HideInInspector]
	[SerializeField]
	protected Color mColor = Color.white;

	// Token: 0x0400038D RID: 909
	[HideInInspector]
	[SerializeField]
	protected UIWidget.Pivot mPivot = UIWidget.Pivot.Center;

	// Token: 0x0400038E RID: 910
	[HideInInspector]
	[SerializeField]
	protected int mWidth;

	// Token: 0x0400038F RID: 911
	[HideInInspector]
	[SerializeField]
	protected int mHeight;

	// Token: 0x04000390 RID: 912
	[HideInInspector]
	[SerializeField]
	protected int mDepth;

	// Token: 0x04000391 RID: 913
	protected GameObject mGo;

	// Token: 0x04000392 RID: 914
	protected Transform mTrans;

	// Token: 0x04000393 RID: 915
	protected UIPanel mPanel;

	// Token: 0x04000394 RID: 916
	protected bool mChanged = true;

	// Token: 0x04000395 RID: 917
	protected bool mPlayMode = true;

	// Token: 0x04000396 RID: 918
	private bool mStarted;

	// Token: 0x04000397 RID: 919
	private Vector3 mDiffPos;

	// Token: 0x04000398 RID: 920
	private Quaternion mDiffRot;

	// Token: 0x04000399 RID: 921
	private Vector3 mDiffScale;

	// Token: 0x0400039A RID: 922
	private Matrix4x4 mLocalToPanel;

	// Token: 0x0400039B RID: 923
	private bool mVisibleByPanel = true;

	// Token: 0x0400039C RID: 924
	private float mLastAlpha;

	// Token: 0x0400039D RID: 925
	private UIGeometry mGeom = new UIGeometry();

	// Token: 0x0400039E RID: 926
	private Vector3[] mCorners = new Vector3[4];

	// Token: 0x0400039F RID: 927
	private bool mForceVisible;

	// Token: 0x040003A0 RID: 928
	private Vector3 mOldV0;

	// Token: 0x040003A1 RID: 929
	private Vector3 mOldV1;

	// Token: 0x020000B6 RID: 182
	public enum Pivot
	{
		// Token: 0x040003A4 RID: 932
		TopLeft,
		// Token: 0x040003A5 RID: 933
		Top,
		// Token: 0x040003A6 RID: 934
		TopRight,
		// Token: 0x040003A7 RID: 935
		Left,
		// Token: 0x040003A8 RID: 936
		Center,
		// Token: 0x040003A9 RID: 937
		Right,
		// Token: 0x040003AA RID: 938
		BottomLeft,
		// Token: 0x040003AB RID: 939
		Bottom,
		// Token: 0x040003AC RID: 940
		BottomRight
	}
}
