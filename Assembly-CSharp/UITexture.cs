using System;
using UnityEngine;

// Token: 0x020000EB RID: 235
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Texture")]
public class UITexture : UIWidget
{
	// Token: 0x17000146 RID: 326
	// (get) Token: 0x060006FE RID: 1790 RVA: 0x00028BC8 File Offset: 0x00026DC8
	// (set) Token: 0x060006FF RID: 1791 RVA: 0x00028BD0 File Offset: 0x00026DD0
	public Rect uvRect
	{
		get
		{
			return this.mRect;
		}
		set
		{
			if (this.mRect != value)
			{
				this.mRect = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x06000700 RID: 1792 RVA: 0x00028BF0 File Offset: 0x00026DF0
	// (set) Token: 0x06000701 RID: 1793 RVA: 0x00028C54 File Offset: 0x00026E54
	public Shader shader
	{
		get
		{
			if (this.mShader == null)
			{
				Material material = this.material;
				if (material != null)
				{
					this.mShader = material.shader;
				}
				if (this.mShader == null)
				{
					this.mShader = Shader.Find("Unlit/Texture");
				}
			}
			return this.mShader;
		}
		set
		{
			if (this.mShader != value)
			{
				this.mShader = value;
				Material material = this.material;
				if (material != null)
				{
					material.shader = value;
				}
				this.mPMA = -1;
			}
		}
	}

	// Token: 0x17000148 RID: 328
	// (get) Token: 0x06000702 RID: 1794 RVA: 0x00028C9C File Offset: 0x00026E9C
	public bool hasDynamicMaterial
	{
		get
		{
			return this.mDynamicMat != null;
		}
	}

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x06000703 RID: 1795 RVA: 0x00028CAC File Offset: 0x00026EAC
	// (set) Token: 0x06000704 RID: 1796 RVA: 0x00028D78 File Offset: 0x00026F78
	public override Material material
	{
		get
		{
			if (this.mMat != null)
			{
				return this.mMat;
			}
			if (this.mDynamicMat != null)
			{
				return this.mDynamicMat;
			}
			if (!this.mCreatingMat && this.mDynamicMat == null)
			{
				this.mCreatingMat = true;
				if (this.mShader == null)
				{
					this.mShader = Shader.Find("Unlit/Transparent Colored");
				}
				this.Cleanup();
				this.mDynamicMat = new Material(this.mShader);
				this.mDynamicMat.hideFlags = HideFlags.DontSave;
				this.mDynamicMat.mainTexture = this.mTexture;
				this.mPMA = 0;
				this.mCreatingMat = false;
			}
			return this.mDynamicMat;
		}
		set
		{
			if (this.mMat != value)
			{
				this.Cleanup();
				this.mMat = value;
				this.mPMA = -1;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x06000705 RID: 1797 RVA: 0x00028DA8 File Offset: 0x00026FA8
	public bool premultipliedAlpha
	{
		get
		{
			if (this.mPMA == -1)
			{
				Material material = this.material;
				this.mPMA = ((!(material != null) || !(material.shader != null) || !material.shader.name.Contains("Premultiplied")) ? 0 : 1);
			}
			return this.mPMA == 1;
		}
	}

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x06000706 RID: 1798 RVA: 0x00028E18 File Offset: 0x00027018
	// (set) Token: 0x06000707 RID: 1799 RVA: 0x00028E5C File Offset: 0x0002705C
	public override Texture mainTexture
	{
		get
		{
			if (this.mMat != null)
			{
				return this.mMat.mainTexture;
			}
			if (this.mTexture != null)
			{
				return this.mTexture;
			}
			return null;
		}
		set
		{
			base.RemoveFromPanel();
			Material material = this.material;
			if (material != null)
			{
				this.mPanel = null;
				this.mTexture = value;
				material.mainTexture = value;
				base.MarkAsChangedLite();
				if (base.enabled)
				{
					base.CreatePanel();
				}
				if (this.mPanel != null)
				{
					this.mPanel.Refresh();
				}
			}
		}
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x00028ECC File Offset: 0x000270CC
	public void SetTexture(Texture value)
	{
		Material material = this.material;
		if (material != null)
		{
			this.mTexture = value;
			material.mainTexture = value;
		}
	}

	// Token: 0x1700014C RID: 332
	// (get) Token: 0x06000709 RID: 1801 RVA: 0x00028EFC File Offset: 0x000270FC
	private Vector4 drawingDimensions
	{
		get
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			Texture mainTexture = this.mainTexture;
			Rect rect = (!(mainTexture != null)) ? new Rect(0f, 0f, (float)this.mWidth, (float)this.mHeight) : new Rect(0f, 0f, (float)mainTexture.width, (float)mainTexture.height);
			Vector2 pivotOffset = base.pivotOffset;
			int num5 = Mathf.RoundToInt(rect.width);
			int num6 = Mathf.RoundToInt(rect.height);
			float num7 = (float)(((num5 & 1) != 0) ? (num5 + 1) : num5);
			float num8 = (float)(((num6 & 1) != 0) ? (num6 + 1) : num6);
			Vector4 result = new Vector4(num / num7, num2 / num8, ((float)num5 - num3) / num7, ((float)num6 - num4) / num8);
			result.x -= pivotOffset.x;
			result.y -= pivotOffset.y;
			result.z -= pivotOffset.x;
			result.w -= pivotOffset.y;
			result.x *= (float)this.mWidth;
			result.y *= (float)this.mHeight;
			result.z *= (float)this.mWidth;
			result.w *= (float)this.mHeight;
			return result;
		}
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x00029094 File Offset: 0x00027294
	private void OnDestroy()
	{
		this.Cleanup();
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0002909C File Offset: 0x0002729C
	private void Cleanup()
	{
		if (this.mDynamicMat != null)
		{
			NGUITools.Destroy(this.mDynamicMat);
			this.mDynamicMat = null;
		}
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x000290C4 File Offset: 0x000272C4
	public override void MakePixelPerfect()
	{
		Texture mainTexture = this.mainTexture;
		if (mainTexture != null)
		{
			int num = mainTexture.width;
			if ((num & 1) == 1)
			{
				num++;
			}
			int num2 = mainTexture.height;
			if ((num2 & 1) == 1)
			{
				num2++;
			}
			base.width = num;
			base.height = num2;
		}
		base.MakePixelPerfect();
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x00029120 File Offset: 0x00027320
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		Color color = base.color;
		color.a *= this.mPanel.alpha;
		Color32 item = (!this.premultipliedAlpha) ? color : NGUITools.ApplyPMA(color);
		Vector4 drawingDimensions = this.drawingDimensions;
		verts.Add(new Vector3(drawingDimensions.x, drawingDimensions.y));
		verts.Add(new Vector3(drawingDimensions.x, drawingDimensions.w));
		verts.Add(new Vector3(drawingDimensions.z, drawingDimensions.w));
		verts.Add(new Vector3(drawingDimensions.z, drawingDimensions.y));
		uvs.Add(new Vector2(this.mRect.xMin, this.mRect.yMin));
		uvs.Add(new Vector2(this.mRect.xMin, this.mRect.yMax));
		uvs.Add(new Vector2(this.mRect.xMax, this.mRect.yMax));
		uvs.Add(new Vector2(this.mRect.xMax, this.mRect.yMin));
		cols.Add(item);
		cols.Add(item);
		cols.Add(item);
		cols.Add(item);
	}

	// Token: 0x04000558 RID: 1368
	[HideInInspector]
	[SerializeField]
	private Rect mRect = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x04000559 RID: 1369
	[HideInInspector]
	[SerializeField]
	private Shader mShader;

	// Token: 0x0400055A RID: 1370
	[HideInInspector]
	[SerializeField]
	private Texture mTexture;

	// Token: 0x0400055B RID: 1371
	[SerializeField]
	[HideInInspector]
	private Material mMat;

	// Token: 0x0400055C RID: 1372
	private bool mCreatingMat;

	// Token: 0x0400055D RID: 1373
	private Material mDynamicMat;

	// Token: 0x0400055E RID: 1374
	private int mPMA = -1;
}
