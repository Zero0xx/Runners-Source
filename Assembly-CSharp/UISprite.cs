using System;
using UnityEngine;

// Token: 0x020000E1 RID: 225
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Sprite")]
public class UISprite : UIWidget
{
	// Token: 0x17000132 RID: 306
	// (get) Token: 0x060006BC RID: 1724 RVA: 0x00025C3C File Offset: 0x00023E3C
	// (set) Token: 0x060006BD RID: 1725 RVA: 0x00025C44 File Offset: 0x00023E44
	public virtual UISprite.Type type
	{
		get
		{
			return this.mType;
		}
		set
		{
			if (this.mType != value)
			{
				this.mType = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x060006BE RID: 1726 RVA: 0x00025C60 File Offset: 0x00023E60
	public override Material material
	{
		get
		{
			return (!(this.mAtlas != null)) ? null : this.mAtlas.spriteMaterial;
		}
	}

	// Token: 0x17000134 RID: 308
	// (get) Token: 0x060006BF RID: 1727 RVA: 0x00025C90 File Offset: 0x00023E90
	// (set) Token: 0x060006C0 RID: 1728 RVA: 0x00025C98 File Offset: 0x00023E98
	public UIAtlas atlas
	{
		get
		{
			return this.mAtlas;
		}
		set
		{
			if (this.mAtlas != value)
			{
				base.RemoveFromPanel();
				this.mAtlas = value;
				this.mSpriteSet = false;
				this.mSprite = null;
				if (string.IsNullOrEmpty(this.mSpriteName) && this.mAtlas != null && this.mAtlas.spriteList.Count > 0)
				{
					this.SetAtlasSprite(this.mAtlas.spriteList[0]);
					this.mSpriteName = this.mSprite.name;
				}
				if (!string.IsNullOrEmpty(this.mSpriteName))
				{
					string spriteName = this.mSpriteName;
					this.mSpriteName = string.Empty;
					this.spriteName = spriteName;
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00025D60 File Offset: 0x00023F60
	// (set) Token: 0x060006C2 RID: 1730 RVA: 0x00025D68 File Offset: 0x00023F68
	public string spriteName
	{
		get
		{
			return this.mSpriteName;
		}
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				if (string.IsNullOrEmpty(this.mSpriteName))
				{
					return;
				}
				this.mSpriteName = string.Empty;
				this.mSprite = null;
				this.mChanged = true;
				this.mSpriteSet = false;
			}
			else if (this.mSpriteName != value)
			{
				this.mSpriteName = value;
				this.mSprite = null;
				this.mChanged = true;
				this.mSpriteSet = false;
			}
		}
	}

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00025DE4 File Offset: 0x00023FE4
	public bool isValid
	{
		get
		{
			return this.GetAtlasSprite() != null;
		}
	}

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00025DF4 File Offset: 0x00023FF4
	// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00025DFC File Offset: 0x00023FFC
	public bool fillCenter
	{
		get
		{
			return this.mFillCenter;
		}
		set
		{
			if (this.mFillCenter != value)
			{
				this.mFillCenter = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00025E18 File Offset: 0x00024018
	// (set) Token: 0x060006C7 RID: 1735 RVA: 0x00025E20 File Offset: 0x00024020
	public UISprite.FillDirection fillDirection
	{
		get
		{
			return this.mFillDirection;
		}
		set
		{
			if (this.mFillDirection != value)
			{
				this.mFillDirection = value;
				this.mChanged = true;
			}
		}
	}

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00025E3C File Offset: 0x0002403C
	// (set) Token: 0x060006C9 RID: 1737 RVA: 0x00025E44 File Offset: 0x00024044
	public float fillAmount
	{
		get
		{
			return this.mFillAmount;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mFillAmount != num)
			{
				this.mFillAmount = num;
				this.mChanged = true;
			}
		}
	}

	// Token: 0x1700013A RID: 314
	// (get) Token: 0x060006CA RID: 1738 RVA: 0x00025E74 File Offset: 0x00024074
	// (set) Token: 0x060006CB RID: 1739 RVA: 0x00025E7C File Offset: 0x0002407C
	public bool invert
	{
		get
		{
			return this.mInvert;
		}
		set
		{
			if (this.mInvert != value)
			{
				this.mInvert = value;
				this.mChanged = true;
			}
		}
	}

	// Token: 0x1700013B RID: 315
	// (get) Token: 0x060006CC RID: 1740 RVA: 0x00025E98 File Offset: 0x00024098
	public override Vector4 border
	{
		get
		{
			if (this.type != UISprite.Type.Sliced)
			{
				return base.border;
			}
			UISpriteData atlasSprite = this.GetAtlasSprite();
			if (atlasSprite == null)
			{
				return Vector2.zero;
			}
			return new Vector4((float)atlasSprite.borderLeft, (float)atlasSprite.borderBottom, (float)atlasSprite.borderRight, (float)atlasSprite.borderTop);
		}
	}

	// Token: 0x1700013C RID: 316
	// (get) Token: 0x060006CD RID: 1741 RVA: 0x00025EF4 File Offset: 0x000240F4
	public override int minWidth
	{
		get
		{
			if (this.type == UISprite.Type.Sliced)
			{
				Vector4 border = this.border;
				return Mathf.RoundToInt(border.x + border.z);
			}
			return base.minWidth;
		}
	}

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x060006CE RID: 1742 RVA: 0x00025F30 File Offset: 0x00024130
	public override int minHeight
	{
		get
		{
			if (this.type == UISprite.Type.Sliced)
			{
				Vector4 border = this.border;
				return Mathf.RoundToInt(border.y + border.w);
			}
			return base.minHeight;
		}
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x00025F6C File Offset: 0x0002416C
	public UISpriteData GetAtlasSprite()
	{
		if (!this.mSpriteSet)
		{
			this.mSprite = null;
		}
		if (this.mSprite == null && this.mAtlas != null)
		{
			if (!string.IsNullOrEmpty(this.mSpriteName))
			{
				UISpriteData sprite = this.mAtlas.GetSprite(this.mSpriteName);
				if (sprite == null)
				{
					return null;
				}
				this.SetAtlasSprite(sprite);
			}
			if (this.mSprite == null && this.mAtlas.spriteList.Count > 0)
			{
				UISpriteData uispriteData = this.mAtlas.spriteList[0];
				if (uispriteData == null)
				{
					return null;
				}
				this.SetAtlasSprite(uispriteData);
				if (this.mSprite == null)
				{
					global::Debug.LogError(this.mAtlas.name + " seems to have a null sprite!");
					return null;
				}
				this.mSpriteName = this.mSprite.name;
			}
		}
		return this.mSprite;
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x00026058 File Offset: 0x00024258
	protected void SetAtlasSprite(UISpriteData sp)
	{
		this.mChanged = true;
		this.mSpriteSet = true;
		if (sp != null)
		{
			this.mSprite = sp;
			this.mSpriteName = this.mSprite.name;
		}
		else
		{
			this.mSpriteName = ((this.mSprite == null) ? string.Empty : this.mSprite.name);
			this.mSprite = sp;
		}
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x000260C4 File Offset: 0x000242C4
	public override void MakePixelPerfect()
	{
		if (!this.isValid)
		{
			return;
		}
		base.MakePixelPerfect();
		UISprite.Type type = this.type;
		if (type == UISprite.Type.Simple || type == UISprite.Type.Filled)
		{
			Texture mainTexture = this.mainTexture;
			UISpriteData atlasSprite = this.GetAtlasSprite();
			if (mainTexture != null && atlasSprite != null)
			{
				int num = Mathf.RoundToInt(this.atlas.pixelSize * (float)(atlasSprite.width + atlasSprite.paddingLeft + atlasSprite.paddingRight));
				int num2 = Mathf.RoundToInt(this.atlas.pixelSize * (float)(atlasSprite.height + atlasSprite.paddingTop + atlasSprite.paddingBottom));
				if ((num & 1) == 1)
				{
					num++;
				}
				if ((num2 & 1) == 1)
				{
					num2++;
				}
				base.width = num;
				base.height = num2;
			}
		}
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x00026194 File Offset: 0x00024394
	public override void Update()
	{
		base.Update();
		if (this.mChanged || !this.mSpriteSet)
		{
			this.mSpriteSet = true;
			this.mSprite = null;
			this.mChanged = true;
		}
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x000261C8 File Offset: 0x000243C8
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		Texture mainTexture = this.mainTexture;
		if (mainTexture != null)
		{
			if (this.mSprite == null)
			{
				this.mSprite = this.atlas.GetSprite(this.spriteName);
			}
			if (this.mSprite == null)
			{
				return;
			}
			this.mOuterUV.Set((float)this.mSprite.x, (float)this.mSprite.y, (float)this.mSprite.width, (float)this.mSprite.height);
			this.mInnerUV.Set((float)(this.mSprite.x + this.mSprite.borderLeft), (float)(this.mSprite.y + this.mSprite.borderTop), (float)(this.mSprite.width - this.mSprite.borderLeft - this.mSprite.borderRight), (float)(this.mSprite.height - this.mSprite.borderBottom - this.mSprite.borderTop));
			this.mOuterUV = NGUIMath.ConvertToTexCoords(this.mOuterUV, mainTexture.width, mainTexture.height);
			this.mInnerUV = NGUIMath.ConvertToTexCoords(this.mInnerUV, mainTexture.width, mainTexture.height);
		}
		switch (this.type)
		{
		case UISprite.Type.Simple:
			this.SimpleFill(verts, uvs, cols);
			break;
		case UISprite.Type.Sliced:
			this.SlicedFill(verts, uvs, cols);
			break;
		case UISprite.Type.Tiled:
			this.TiledFill(verts, uvs, cols);
			break;
		case UISprite.Type.Filled:
			this.FilledFill(verts, uvs, cols);
			break;
		}
	}

	// Token: 0x1700013E RID: 318
	// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00026368 File Offset: 0x00024568
	private Vector4 drawingDimensions
	{
		get
		{
			if (this.mSprite == null)
			{
				return new Vector4(0f, 0f, (float)this.mWidth, (float)this.mHeight);
			}
			int paddingLeft = this.mSprite.paddingLeft;
			int paddingBottom = this.mSprite.paddingBottom;
			int num = this.mSprite.paddingRight;
			int num2 = this.mSprite.paddingTop;
			Vector2 pivotOffset = base.pivotOffset;
			int num3 = this.mSprite.width + this.mSprite.paddingLeft + this.mSprite.paddingRight;
			int num4 = this.mSprite.height + this.mSprite.paddingBottom + this.mSprite.paddingTop;
			if ((num3 & 1) == 1)
			{
				num++;
			}
			if ((num4 & 1) == 1)
			{
				num2++;
			}
			float num5 = 1f / (float)num3;
			float num6 = 1f / (float)num4;
			Vector4 result = new Vector4((float)paddingLeft * num5, (float)paddingBottom * num6, (float)(num3 - num) * num5, (float)(num4 - num2) * num6);
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

	// Token: 0x060006D5 RID: 1749 RVA: 0x00026520 File Offset: 0x00024720
	protected void SimpleFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		Vector2 item = new Vector2(this.mOuterUV.xMin, this.mOuterUV.yMin);
		Vector2 item2 = new Vector2(this.mOuterUV.xMax, this.mOuterUV.yMax);
		Vector4 drawingDimensions = this.drawingDimensions;
		verts.Add(new Vector3(drawingDimensions.x, drawingDimensions.y));
		verts.Add(new Vector3(drawingDimensions.x, drawingDimensions.w));
		verts.Add(new Vector3(drawingDimensions.z, drawingDimensions.w));
		verts.Add(new Vector3(drawingDimensions.z, drawingDimensions.y));
		uvs.Add(item);
		uvs.Add(new Vector2(item.x, item2.y));
		uvs.Add(item2);
		uvs.Add(new Vector2(item2.x, item.y));
		Color color = base.color;
		color.a *= this.mPanel.alpha;
		Color32 item3 = (!this.atlas.premultipliedAlpha) ? color : NGUITools.ApplyPMA(color);
		cols.Add(item3);
		cols.Add(item3);
		cols.Add(item3);
		cols.Add(item3);
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x00026678 File Offset: 0x00024878
	protected void SlicedFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		if (this.mSprite == null)
		{
			return;
		}
		if (!this.mSprite.hasBorder)
		{
			this.SimpleFill(verts, uvs, cols);
			return;
		}
		Vector4 vector = this.border * this.atlas.pixelSize;
		Vector2 pivotOffset = base.pivotOffset;
		float num = 1f / (float)this.mWidth;
		float num2 = 1f / (float)this.mHeight;
		Vector2[] array = new Vector2[]
		{
			new Vector2((float)this.mSprite.paddingLeft * num, (float)this.mSprite.paddingBottom * num2),
			default(Vector2),
			default(Vector2),
			new Vector2(1f - (float)this.mSprite.paddingRight * num, 1f - (float)this.mSprite.paddingTop * num2)
		};
		array[1].x = array[0].x + num * vector.x;
		array[1].y = array[0].y + num2 * vector.y;
		array[2].x = array[3].x - num * vector.z;
		array[2].y = array[3].y - num2 * vector.w;
		for (int i = 0; i < 4; i++)
		{
			Vector2[] array2 = array;
			int num3 = i;
			array2[num3].x = array2[num3].x - pivotOffset.x;
			Vector2[] array3 = array;
			int num4 = i;
			array3[num4].y = array3[num4].y - pivotOffset.y;
			Vector2[] array4 = array;
			int num5 = i;
			array4[num5].x = array4[num5].x * (float)this.mWidth;
			Vector2[] array5 = array;
			int num6 = i;
			array5[num6].y = array5[num6].y * (float)this.mHeight;
		}
		Vector2[] array6 = new Vector2[]
		{
			new Vector2(this.mOuterUV.xMin, this.mOuterUV.yMin),
			new Vector2(this.mInnerUV.xMin, this.mInnerUV.yMin),
			new Vector2(this.mInnerUV.xMax, this.mInnerUV.yMax),
			new Vector2(this.mOuterUV.xMax, this.mOuterUV.yMax)
		};
		Color color = base.color;
		color.a *= this.mPanel.alpha;
		Color32 item = (!this.atlas.premultipliedAlpha) ? color : NGUITools.ApplyPMA(color);
		for (int j = 0; j < 3; j++)
		{
			int num7 = j + 1;
			for (int k = 0; k < 3; k++)
			{
				if (this.mFillCenter || j != 1 || k != 1)
				{
					int num8 = k + 1;
					verts.Add(new Vector3(array[j].x, array[k].y));
					verts.Add(new Vector3(array[j].x, array[num8].y));
					verts.Add(new Vector3(array[num7].x, array[num8].y));
					verts.Add(new Vector3(array[num7].x, array[k].y));
					uvs.Add(new Vector2(array6[j].x, array6[k].y));
					uvs.Add(new Vector2(array6[j].x, array6[num8].y));
					uvs.Add(new Vector2(array6[num7].x, array6[num8].y));
					uvs.Add(new Vector2(array6[num7].x, array6[k].y));
					cols.Add(item);
					cols.Add(item);
					cols.Add(item);
					cols.Add(item);
				}
			}
		}
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x00026B0C File Offset: 0x00024D0C
	protected void TiledFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		Texture mainTexture = this.material.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		Vector2 a = new Vector2(this.mInnerUV.width * (float)mainTexture.width, this.mInnerUV.height * (float)mainTexture.height);
		a *= this.atlas.pixelSize;
		float num = Mathf.Abs(a.x / (float)this.mWidth);
		float num2 = Mathf.Abs(a.y / (float)this.mHeight);
		if (num * num2 < 0.0001f)
		{
			num = 0.01f;
			num2 = 0.01f;
		}
		Color color = base.color;
		color.a *= this.mPanel.alpha;
		Color32 item = (!this.atlas.premultipliedAlpha) ? color : NGUITools.ApplyPMA(color);
		Vector2 pivotOffset = base.pivotOffset;
		Vector2 vector = new Vector2(this.mInnerUV.xMin, this.mInnerUV.yMin);
		Vector2 vector2 = new Vector2(this.mInnerUV.xMax, this.mInnerUV.yMax);
		Vector2 vector3 = vector2;
		for (float num3 = 0f; num3 < 1f; num3 += num2)
		{
			float num4 = 0f;
			vector3.x = vector2.x;
			float num5 = num3 + num2;
			if (num5 > 1f)
			{
				vector3.y = vector.y + (vector2.y - vector.y) * (1f - num3) / (num5 - num3);
				num5 = 1f;
			}
			while (num4 < 1f)
			{
				float num6 = num4 + num;
				if (num6 > 1f)
				{
					vector3.x = vector.x + (vector2.x - vector.x) * (1f - num4) / (num6 - num4);
					num6 = 1f;
				}
				float x = (num4 - pivotOffset.x) * (float)this.mWidth;
				float x2 = (num6 - pivotOffset.x) * (float)this.mWidth;
				float y = (num3 - pivotOffset.y) * (float)this.mHeight;
				float y2 = (num5 - pivotOffset.y) * (float)this.mHeight;
				verts.Add(new Vector3(x, y));
				verts.Add(new Vector3(x, y2));
				verts.Add(new Vector3(x2, y2));
				verts.Add(new Vector3(x2, y));
				uvs.Add(new Vector2(vector.x, vector.y));
				uvs.Add(new Vector2(vector.x, vector3.y));
				uvs.Add(new Vector2(vector3.x, vector3.y));
				uvs.Add(new Vector2(vector3.x, vector.y));
				cols.Add(item);
				cols.Add(item);
				cols.Add(item);
				cols.Add(item);
				num4 += num;
			}
		}
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x00026E28 File Offset: 0x00025028
	protected void FilledFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		if (this.mFillAmount < 0.001f)
		{
			return;
		}
		Color color = base.color;
		color.a *= this.mPanel.alpha;
		Color32 item = (!this.atlas.premultipliedAlpha) ? color : NGUITools.ApplyPMA(color);
		Vector2[] array = new Vector2[4];
		Vector2[] array2 = new Vector2[4];
		Vector4 drawingDimensions = this.drawingDimensions;
		float num = this.mOuterUV.xMin;
		float num2 = this.mOuterUV.yMin;
		float num3 = this.mOuterUV.xMax;
		float num4 = this.mOuterUV.yMax;
		if (this.mFillDirection == UISprite.FillDirection.Horizontal || this.mFillDirection == UISprite.FillDirection.Vertical)
		{
			if (this.mFillDirection == UISprite.FillDirection.Horizontal)
			{
				float num5 = (num3 - num) * this.mFillAmount;
				if (this.mInvert)
				{
					drawingDimensions.x = drawingDimensions.z - (drawingDimensions.z - drawingDimensions.x) * this.mFillAmount;
					num = num3 - num5;
				}
				else
				{
					drawingDimensions.z = drawingDimensions.x + (drawingDimensions.z - drawingDimensions.x) * this.mFillAmount;
					num3 = num + num5;
				}
			}
			else if (this.mFillDirection == UISprite.FillDirection.Vertical)
			{
				float num6 = (num4 - num2) * this.mFillAmount;
				if (this.mInvert)
				{
					drawingDimensions.y = drawingDimensions.w - (drawingDimensions.w - drawingDimensions.y) * this.mFillAmount;
					num2 = num4 - num6;
				}
				else
				{
					drawingDimensions.w = drawingDimensions.y + (drawingDimensions.w - drawingDimensions.y) * this.mFillAmount;
					num4 = num2 + num6;
				}
			}
		}
		array[0] = new Vector2(drawingDimensions.x, drawingDimensions.y);
		array[1] = new Vector2(drawingDimensions.x, drawingDimensions.w);
		array[2] = new Vector2(drawingDimensions.z, drawingDimensions.w);
		array[3] = new Vector2(drawingDimensions.z, drawingDimensions.y);
		array2[0] = new Vector2(num, num2);
		array2[1] = new Vector2(num, num4);
		array2[2] = new Vector2(num3, num4);
		array2[3] = new Vector2(num3, num2);
		if (this.mFillAmount < 1f)
		{
			if (this.mFillDirection == UISprite.FillDirection.Radial90)
			{
				if (UISprite.RadialCut(array, array2, this.mFillAmount, this.mInvert, 0))
				{
					for (int i = 0; i < 4; i++)
					{
						verts.Add(array[i]);
						uvs.Add(array2[i]);
						cols.Add(item);
					}
				}
				return;
			}
			if (this.mFillDirection == UISprite.FillDirection.Radial180)
			{
				for (int j = 0; j < 2; j++)
				{
					float t = 0f;
					float t2 = 1f;
					float t3;
					float t4;
					if (j == 0)
					{
						t3 = 0f;
						t4 = 0.5f;
					}
					else
					{
						t3 = 0.5f;
						t4 = 1f;
					}
					array[0].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, t3);
					array[1].x = array[0].x;
					array[2].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, t4);
					array[3].x = array[2].x;
					array[0].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, t);
					array[1].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, t2);
					array[2].y = array[1].y;
					array[3].y = array[0].y;
					array2[0].x = Mathf.Lerp(num, num3, t3);
					array2[1].x = array2[0].x;
					array2[2].x = Mathf.Lerp(num, num3, t4);
					array2[3].x = array2[2].x;
					array2[0].y = Mathf.Lerp(num2, num4, t);
					array2[1].y = Mathf.Lerp(num2, num4, t2);
					array2[2].y = array2[1].y;
					array2[3].y = array2[0].y;
					float value = this.mInvert ? (this.mFillAmount * 2f - (float)(1 - j)) : (this.fillAmount * 2f - (float)j);
					if (UISprite.RadialCut(array, array2, Mathf.Clamp01(value), !this.mInvert, NGUIMath.RepeatIndex(j + 3, 4)))
					{
						for (int k = 0; k < 4; k++)
						{
							verts.Add(array[k]);
							uvs.Add(array2[k]);
							cols.Add(item);
						}
					}
				}
				return;
			}
			if (this.mFillDirection == UISprite.FillDirection.Radial360)
			{
				for (int l = 0; l < 4; l++)
				{
					float t5;
					float t6;
					if (l < 2)
					{
						t5 = 0f;
						t6 = 0.5f;
					}
					else
					{
						t5 = 0.5f;
						t6 = 1f;
					}
					float t7;
					float t8;
					if (l == 0 || l == 3)
					{
						t7 = 0f;
						t8 = 0.5f;
					}
					else
					{
						t7 = 0.5f;
						t8 = 1f;
					}
					array[0].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, t5);
					array[1].x = array[0].x;
					array[2].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, t6);
					array[3].x = array[2].x;
					array[0].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, t7);
					array[1].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, t8);
					array[2].y = array[1].y;
					array[3].y = array[0].y;
					array2[0].x = Mathf.Lerp(num, num3, t5);
					array2[1].x = array2[0].x;
					array2[2].x = Mathf.Lerp(num, num3, t6);
					array2[3].x = array2[2].x;
					array2[0].y = Mathf.Lerp(num2, num4, t7);
					array2[1].y = Mathf.Lerp(num2, num4, t8);
					array2[2].y = array2[1].y;
					array2[3].y = array2[0].y;
					float value2 = (!this.mInvert) ? (this.mFillAmount * 4f - (float)(3 - NGUIMath.RepeatIndex(l + 2, 4))) : (this.mFillAmount * 4f - (float)NGUIMath.RepeatIndex(l + 2, 4));
					if (UISprite.RadialCut(array, array2, Mathf.Clamp01(value2), this.mInvert, NGUIMath.RepeatIndex(l + 2, 4)))
					{
						for (int m = 0; m < 4; m++)
						{
							verts.Add(array[m]);
							uvs.Add(array2[m]);
							cols.Add(item);
						}
					}
				}
				return;
			}
		}
		for (int n = 0; n < 4; n++)
		{
			verts.Add(array[n]);
			uvs.Add(array2[n]);
			cols.Add(item);
		}
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x000276F0 File Offset: 0x000258F0
	private static bool RadialCut(Vector2[] xy, Vector2[] uv, float fill, bool invert, int corner)
	{
		if (fill < 0.001f)
		{
			return false;
		}
		if ((corner & 1) == 1)
		{
			invert = !invert;
		}
		if (!invert && fill > 0.999f)
		{
			return true;
		}
		float num = Mathf.Clamp01(fill);
		if (invert)
		{
			num = 1f - num;
		}
		num *= 1.5707964f;
		float cos = Mathf.Cos(num);
		float sin = Mathf.Sin(num);
		UISprite.RadialCut(xy, cos, sin, invert, corner);
		UISprite.RadialCut(uv, cos, sin, invert, corner);
		return true;
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x00027770 File Offset: 0x00025970
	private static void RadialCut(Vector2[] xy, float cos, float sin, bool invert, int corner)
	{
		int num = NGUIMath.RepeatIndex(corner + 1, 4);
		int num2 = NGUIMath.RepeatIndex(corner + 2, 4);
		int num3 = NGUIMath.RepeatIndex(corner + 3, 4);
		if ((corner & 1) == 1)
		{
			if (sin > cos)
			{
				cos /= sin;
				sin = 1f;
				if (invert)
				{
					xy[num].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
					xy[num2].x = xy[num].x;
				}
			}
			else if (cos > sin)
			{
				sin /= cos;
				cos = 1f;
				if (!invert)
				{
					xy[num2].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
					xy[num3].y = xy[num2].y;
				}
			}
			else
			{
				cos = 1f;
				sin = 1f;
			}
			if (!invert)
			{
				xy[num3].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
			}
			else
			{
				xy[num].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
			}
		}
		else
		{
			if (cos > sin)
			{
				sin /= cos;
				cos = 1f;
				if (!invert)
				{
					xy[num].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
					xy[num2].y = xy[num].y;
				}
			}
			else if (sin > cos)
			{
				cos /= sin;
				sin = 1f;
				if (invert)
				{
					xy[num2].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
					xy[num3].x = xy[num2].x;
				}
			}
			else
			{
				cos = 1f;
				sin = 1f;
			}
			if (invert)
			{
				xy[num3].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
			}
			else
			{
				xy[num].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
			}
		}
	}

	// Token: 0x04000507 RID: 1287
	[SerializeField]
	[HideInInspector]
	private UIAtlas mAtlas;

	// Token: 0x04000508 RID: 1288
	[HideInInspector]
	[SerializeField]
	private string mSpriteName;

	// Token: 0x04000509 RID: 1289
	[SerializeField]
	[HideInInspector]
	private bool mFillCenter = true;

	// Token: 0x0400050A RID: 1290
	[SerializeField]
	[HideInInspector]
	private UISprite.Type mType;

	// Token: 0x0400050B RID: 1291
	[HideInInspector]
	[SerializeField]
	private UISprite.FillDirection mFillDirection = UISprite.FillDirection.Radial360;

	// Token: 0x0400050C RID: 1292
	[HideInInspector]
	[SerializeField]
	private float mFillAmount = 1f;

	// Token: 0x0400050D RID: 1293
	[HideInInspector]
	[SerializeField]
	private bool mInvert;

	// Token: 0x0400050E RID: 1294
	protected UISpriteData mSprite;

	// Token: 0x0400050F RID: 1295
	protected Rect mInnerUV = default(Rect);

	// Token: 0x04000510 RID: 1296
	protected Rect mOuterUV = default(Rect);

	// Token: 0x04000511 RID: 1297
	private bool mSpriteSet;

	// Token: 0x020000E2 RID: 226
	public enum Type
	{
		// Token: 0x04000513 RID: 1299
		Simple,
		// Token: 0x04000514 RID: 1300
		Sliced,
		// Token: 0x04000515 RID: 1301
		Tiled,
		// Token: 0x04000516 RID: 1302
		Filled
	}

	// Token: 0x020000E3 RID: 227
	public enum FillDirection
	{
		// Token: 0x04000518 RID: 1304
		Horizontal,
		// Token: 0x04000519 RID: 1305
		Vertical,
		// Token: 0x0400051A RID: 1306
		Radial90,
		// Token: 0x0400051B RID: 1307
		Radial180,
		// Token: 0x0400051C RID: 1308
		Radial360
	}
}
