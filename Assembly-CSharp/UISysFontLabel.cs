using System;
using UnityEngine;

// Token: 0x02000A5E RID: 2654
[AddComponentMenu("NGUI/UI/SysFont Label")]
[ExecuteInEditMode]
public class UISysFontLabel : UIWidget, ISysFontTexturable
{
	// Token: 0x170009A0 RID: 2464
	// (get) Token: 0x06004791 RID: 18321 RVA: 0x00179B44 File Offset: 0x00177D44
	// (set) Token: 0x06004792 RID: 18322 RVA: 0x00179B54 File Offset: 0x00177D54
	public string Text
	{
		get
		{
			return this._texture.Text;
		}
		set
		{
			this._texture.Text = value;
		}
	}

	// Token: 0x170009A1 RID: 2465
	// (get) Token: 0x06004793 RID: 18323 RVA: 0x00179B64 File Offset: 0x00177D64
	// (set) Token: 0x06004794 RID: 18324 RVA: 0x00179B74 File Offset: 0x00177D74
	public string AppleFontName
	{
		get
		{
			return this._texture.AppleFontName;
		}
		set
		{
			this._texture.AppleFontName = value;
		}
	}

	// Token: 0x170009A2 RID: 2466
	// (get) Token: 0x06004795 RID: 18325 RVA: 0x00179B84 File Offset: 0x00177D84
	// (set) Token: 0x06004796 RID: 18326 RVA: 0x00179B94 File Offset: 0x00177D94
	public string AndroidFontName
	{
		get
		{
			return this._texture.AndroidFontName;
		}
		set
		{
			this._texture.AndroidFontName = value;
		}
	}

	// Token: 0x170009A3 RID: 2467
	// (get) Token: 0x06004797 RID: 18327 RVA: 0x00179BA4 File Offset: 0x00177DA4
	// (set) Token: 0x06004798 RID: 18328 RVA: 0x00179BB4 File Offset: 0x00177DB4
	public string FontName
	{
		get
		{
			return this._texture.FontName;
		}
		set
		{
			this._texture.FontName = value;
		}
	}

	// Token: 0x170009A4 RID: 2468
	// (get) Token: 0x06004799 RID: 18329 RVA: 0x00179BC4 File Offset: 0x00177DC4
	// (set) Token: 0x0600479A RID: 18330 RVA: 0x00179BD4 File Offset: 0x00177DD4
	public int FontSize
	{
		get
		{
			return this._texture.FontSize;
		}
		set
		{
			this._texture.FontSize = value;
		}
	}

	// Token: 0x170009A5 RID: 2469
	// (get) Token: 0x0600479B RID: 18331 RVA: 0x00179BE4 File Offset: 0x00177DE4
	// (set) Token: 0x0600479C RID: 18332 RVA: 0x00179BF4 File Offset: 0x00177DF4
	public bool IsBold
	{
		get
		{
			return this._texture.IsBold;
		}
		set
		{
			this._texture.IsBold = value;
		}
	}

	// Token: 0x170009A6 RID: 2470
	// (get) Token: 0x0600479D RID: 18333 RVA: 0x00179C04 File Offset: 0x00177E04
	// (set) Token: 0x0600479E RID: 18334 RVA: 0x00179C14 File Offset: 0x00177E14
	public bool IsItalic
	{
		get
		{
			return this._texture.IsItalic;
		}
		set
		{
			this._texture.IsItalic = value;
		}
	}

	// Token: 0x170009A7 RID: 2471
	// (get) Token: 0x0600479F RID: 18335 RVA: 0x00179C24 File Offset: 0x00177E24
	// (set) Token: 0x060047A0 RID: 18336 RVA: 0x00179C34 File Offset: 0x00177E34
	public SysFont.Alignment Alignment
	{
		get
		{
			return this._texture.Alignment;
		}
		set
		{
			this._texture.Alignment = value;
		}
	}

	// Token: 0x170009A8 RID: 2472
	// (get) Token: 0x060047A1 RID: 18337 RVA: 0x00179C44 File Offset: 0x00177E44
	// (set) Token: 0x060047A2 RID: 18338 RVA: 0x00179C54 File Offset: 0x00177E54
	public bool IsMultiLine
	{
		get
		{
			return this._texture.IsMultiLine;
		}
		set
		{
			this._texture.IsMultiLine = value;
		}
	}

	// Token: 0x170009A9 RID: 2473
	// (get) Token: 0x060047A3 RID: 18339 RVA: 0x00179C64 File Offset: 0x00177E64
	// (set) Token: 0x060047A4 RID: 18340 RVA: 0x00179C74 File Offset: 0x00177E74
	public int MaxWidthPixels
	{
		get
		{
			return this._texture.MaxWidthPixels;
		}
		set
		{
			this._texture.MaxWidthPixels = value;
		}
	}

	// Token: 0x170009AA RID: 2474
	// (get) Token: 0x060047A5 RID: 18341 RVA: 0x00179C84 File Offset: 0x00177E84
	// (set) Token: 0x060047A6 RID: 18342 RVA: 0x00179C94 File Offset: 0x00177E94
	public int MaxHeightPixels
	{
		get
		{
			return this._texture.MaxHeightPixels;
		}
		set
		{
			this._texture.MaxHeightPixels = value;
		}
	}

	// Token: 0x170009AB RID: 2475
	// (get) Token: 0x060047A7 RID: 18343 RVA: 0x00179CA4 File Offset: 0x00177EA4
	public int WidthPixels
	{
		get
		{
			return this._texture.WidthPixels;
		}
	}

	// Token: 0x170009AC RID: 2476
	// (get) Token: 0x060047A8 RID: 18344 RVA: 0x00179CB4 File Offset: 0x00177EB4
	public int HeightPixels
	{
		get
		{
			return this._texture.HeightPixels;
		}
	}

	// Token: 0x170009AD RID: 2477
	// (get) Token: 0x060047A9 RID: 18345 RVA: 0x00179CC4 File Offset: 0x00177EC4
	public int TextWidthPixels
	{
		get
		{
			return this._texture.TextWidthPixels;
		}
	}

	// Token: 0x170009AE RID: 2478
	// (get) Token: 0x060047AA RID: 18346 RVA: 0x00179CD4 File Offset: 0x00177ED4
	public int TextHeightPixels
	{
		get
		{
			return this._texture.TextHeightPixels;
		}
	}

	// Token: 0x170009AF RID: 2479
	// (get) Token: 0x060047AB RID: 18347 RVA: 0x00179CE4 File Offset: 0x00177EE4
	public Texture2D Texture
	{
		get
		{
			return this._texture.Texture;
		}
	}

	// Token: 0x060047AC RID: 18348 RVA: 0x00179CF4 File Offset: 0x00177EF4
	protected new virtual void Update()
	{
		base.Update();
		this.MarkAsChanged();
		if (this._texture.NeedsRedraw)
		{
			this._texture.Update();
			this._uv = new Vector2((float)this._texture.TextWidthPixels / (float)this._texture.WidthPixels, (float)this._texture.TextHeightPixels / (float)this._texture.HeightPixels);
			global::Debug.Log("_texture.TextWidthPixels = " + this._texture.TextWidthPixels);
			global::Debug.Log("_texture.WidthPixels = " + this._texture.WidthPixels);
			global::Debug.Log("_texture.TextHeightPixels = " + this._texture.TextHeightPixels);
			global::Debug.Log("_texture.HeightPixels = " + this._texture.HeightPixels);
			global::Debug.Log("uv.u = " + this._uv.x.ToString());
			global::Debug.Log("uv.v = " + this._uv.y.ToString());
			NGUITools.Destroy(this._createdMaterial);
			this._createdMaterial = new Material(UISysFontLabel._shader);
			this._createdMaterial.hideFlags = HideFlags.DontSave;
			this._createdMaterial.mainTexture = this._texture.Texture;
			this._createdMaterial.color = base.color;
		}
	}

	// Token: 0x170009B0 RID: 2480
	// (get) Token: 0x060047AE RID: 18350 RVA: 0x00179E74 File Offset: 0x00178074
	// (set) Token: 0x060047AD RID: 18349 RVA: 0x00179E70 File Offset: 0x00178070
	public override Material material
	{
		get
		{
			return this._createdMaterial;
		}
		set
		{
		}
	}

	// Token: 0x170009B1 RID: 2481
	// (get) Token: 0x060047B0 RID: 18352 RVA: 0x00179E80 File Offset: 0x00178080
	// (set) Token: 0x060047AF RID: 18351 RVA: 0x00179E7C File Offset: 0x0017807C
	public override Texture mainTexture
	{
		get
		{
			return (!(this.material != null)) ? null : this.material.mainTexture;
		}
		set
		{
		}
	}

	// Token: 0x060047B1 RID: 18353 RVA: 0x00179EB0 File Offset: 0x001780B0
	public override void MakePixelPerfect()
	{
		Vector3 localScale = base.cachedTransform.localScale;
		localScale.x = (float)this._texture.TextWidthPixels;
		localScale.y = (float)this._texture.TextHeightPixels;
		base.cachedTransform.localScale = localScale;
		base.MakePixelPerfect();
	}

	// Token: 0x060047B2 RID: 18354 RVA: 0x00179F04 File Offset: 0x00178104
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		Vector4 drawingDimensions = this.drawingDimensions;
		verts.Add(new Vector3(drawingDimensions.x, drawingDimensions.y));
		verts.Add(new Vector3(drawingDimensions.x, drawingDimensions.w));
		verts.Add(new Vector3(drawingDimensions.z, drawingDimensions.w));
		verts.Add(new Vector3(drawingDimensions.z, drawingDimensions.y));
		uvs.Add(Vector2.zero);
		uvs.Add(new Vector2(0f, this._uv.y));
		uvs.Add(this._uv);
		uvs.Add(new Vector2(this._uv.x, 0f));
		cols.Add(base.color);
		cols.Add(base.color);
		cols.Add(base.color);
		cols.Add(base.color);
		this.MakePixelPerfect();
		if (this.material.mainTexture != this._texture.Texture)
		{
			this.material.mainTexture = this._texture.Texture;
		}
	}

	// Token: 0x060047B3 RID: 18355 RVA: 0x0017A04C File Offset: 0x0017824C
	protected new void OnEnable()
	{
		base.OnEnable();
		if (UISysFontLabel._shader == null)
		{
			UISysFontLabel._shader = Shader.Find("SysFont/Unlit Transparent");
		}
	}

	// Token: 0x060047B4 RID: 18356 RVA: 0x0017A074 File Offset: 0x00178274
	protected void OnDestroy()
	{
		this.material = null;
		SysFont.SafeDestroy(this._createdMaterial);
		if (this._texture != null)
		{
			this._texture.Destroy();
			this._texture = null;
		}
	}

	// Token: 0x170009B2 RID: 2482
	// (get) Token: 0x060047B5 RID: 18357 RVA: 0x0017A0A8 File Offset: 0x001782A8
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

	// Token: 0x04003B94 RID: 15252
	[SerializeField]
	protected SysFontTexture _texture = new SysFontTexture();

	// Token: 0x04003B95 RID: 15253
	protected static Shader _shader;

	// Token: 0x04003B96 RID: 15254
	protected Material _createdMaterial;

	// Token: 0x04003B97 RID: 15255
	protected Vector3[] _vertices;

	// Token: 0x04003B98 RID: 15256
	protected Vector2 _uv;
}
