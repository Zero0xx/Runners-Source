using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020000D3 RID: 211
[AddComponentMenu("NGUI/UI/Font")]
[ExecuteInEditMode]
public class UIFont : MonoBehaviour
{
	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x060005FF RID: 1535 RVA: 0x0001FE04 File Offset: 0x0001E004
	public BMFont bmFont
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mFont : this.mReplacement.bmFont;
		}
	}

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x06000600 RID: 1536 RVA: 0x0001FE30 File Offset: 0x0001E030
	public int texWidth
	{
		get
		{
			return (!(this.mReplacement != null)) ? ((this.mFont == null) ? 1 : this.mFont.texWidth) : this.mReplacement.texWidth;
		}
	}

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x06000601 RID: 1537 RVA: 0x0001FE70 File Offset: 0x0001E070
	public int texHeight
	{
		get
		{
			return (!(this.mReplacement != null)) ? ((this.mFont == null) ? 1 : this.mFont.texHeight) : this.mReplacement.texHeight;
		}
	}

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001FEB0 File Offset: 0x0001E0B0
	public bool hasSymbols
	{
		get
		{
			return (!(this.mReplacement != null)) ? (this.mSymbols.Count != 0) : this.mReplacement.hasSymbols;
		}
	}

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x06000603 RID: 1539 RVA: 0x0001FEF0 File Offset: 0x0001E0F0
	public List<BMSymbol> symbols
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mSymbols : this.mReplacement.symbols;
		}
	}

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x06000604 RID: 1540 RVA: 0x0001FF1C File Offset: 0x0001E11C
	// (set) Token: 0x06000605 RID: 1541 RVA: 0x0001FF48 File Offset: 0x0001E148
	public UIAtlas atlas
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mAtlas : this.mReplacement.atlas;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.atlas = value;
			}
			else if (this.mAtlas != value)
			{
				if (value == null)
				{
					if (this.mAtlas != null)
					{
						this.mMat = this.mAtlas.spriteMaterial;
					}
					if (this.sprite != null)
					{
						this.mUVRect = this.uvRect;
					}
				}
				this.mPMA = -1;
				this.mAtlas = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x06000606 RID: 1542 RVA: 0x0001FFE4 File Offset: 0x0001E1E4
	// (set) Token: 0x06000607 RID: 1543 RVA: 0x000200A8 File Offset: 0x0001E2A8
	public Material material
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.material;
			}
			if (this.mAtlas != null)
			{
				return this.mAtlas.spriteMaterial;
			}
			if (this.mMat != null)
			{
				if (this.mDynamicFont != null && this.mMat != this.mDynamicFont.material)
				{
					this.mMat.mainTexture = this.mDynamicFont.material.mainTexture;
				}
				return this.mMat;
			}
			if (this.mDynamicFont != null)
			{
				return this.mDynamicFont.material;
			}
			return null;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.material = value;
			}
			else if (this.mMat != value)
			{
				this.mPMA = -1;
				this.mMat = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x06000608 RID: 1544 RVA: 0x000200FC File Offset: 0x0001E2FC
	// (set) Token: 0x06000609 RID: 1545 RVA: 0x0002014C File Offset: 0x0001E34C
	public float pixelSize
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.pixelSize;
			}
			if (this.mAtlas != null)
			{
				return this.mAtlas.pixelSize;
			}
			return this.mPixelSize;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.pixelSize = value;
			}
			else if (this.mAtlas != null)
			{
				this.mAtlas.pixelSize = value;
			}
			else
			{
				float num = Mathf.Clamp(value, 0.25f, 4f);
				if (this.mPixelSize != num)
				{
					this.mPixelSize = num;
					this.MarkAsDirty();
				}
			}
		}
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x0600060A RID: 1546 RVA: 0x000201C8 File Offset: 0x0001E3C8
	public bool premultipliedAlpha
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.premultipliedAlpha;
			}
			if (this.mAtlas != null)
			{
				return this.mAtlas.premultipliedAlpha;
			}
			if (this.mPMA == -1)
			{
				Material material = this.material;
				this.mPMA = ((!(material != null) || !(material.shader != null) || !material.shader.name.Contains("Premultiplied")) ? 0 : 1);
			}
			return this.mPMA == 1;
		}
	}

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x0600060B RID: 1547 RVA: 0x00020270 File Offset: 0x0001E470
	public Texture2D texture
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.texture;
			}
			Material material = this.material;
			return (!(material != null)) ? null : (material.mainTexture as Texture2D);
		}
	}

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x0600060C RID: 1548 RVA: 0x000202C0 File Offset: 0x0001E4C0
	// (set) Token: 0x0600060D RID: 1549 RVA: 0x000203E8 File Offset: 0x0001E5E8
	public Rect uvRect
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.uvRect;
			}
			if (this.mAtlas != null && this.mSprite == null && this.sprite != null)
			{
				Texture texture = this.mAtlas.texture;
				if (texture != null)
				{
					this.mUVRect = new Rect((float)(this.mSprite.x - this.mSprite.paddingLeft), (float)(this.mSprite.y - this.mSprite.paddingTop), (float)(this.mSprite.width + this.mSprite.paddingLeft + this.mSprite.paddingRight), (float)(this.mSprite.height + this.mSprite.paddingTop + this.mSprite.paddingBottom));
					this.mUVRect = NGUIMath.ConvertToTexCoords(this.mUVRect, texture.width, texture.height);
					if (this.mSprite.hasPadding)
					{
						this.Trim();
					}
				}
			}
			return this.mUVRect;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.uvRect = value;
			}
			else if (this.sprite == null && this.mUVRect != value)
			{
				this.mUVRect = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x0600060E RID: 1550 RVA: 0x00020440 File Offset: 0x0001E640
	// (set) Token: 0x0600060F RID: 1551 RVA: 0x0002047C File Offset: 0x0001E67C
	public string spriteName
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mFont.spriteName : this.mReplacement.spriteName;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.spriteName = value;
			}
			else if (this.mFont.spriteName != value)
			{
				this.mFont.spriteName = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x06000610 RID: 1552 RVA: 0x000204D4 File Offset: 0x0001E6D4
	// (set) Token: 0x06000611 RID: 1553 RVA: 0x00020500 File Offset: 0x0001E700
	public int horizontalSpacing
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mSpacingX : this.mReplacement.horizontalSpacing;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.horizontalSpacing = value;
			}
			else if (this.mSpacingX != value)
			{
				this.mSpacingX = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x06000612 RID: 1554 RVA: 0x00020540 File Offset: 0x0001E740
	// (set) Token: 0x06000613 RID: 1555 RVA: 0x0002056C File Offset: 0x0001E76C
	public int verticalSpacing
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mSpacingY : this.mReplacement.verticalSpacing;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.verticalSpacing = value;
			}
			else if (this.mSpacingY != value)
			{
				this.mSpacingY = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x06000614 RID: 1556 RVA: 0x000205AC File Offset: 0x0001E7AC
	public bool isValid
	{
		get
		{
			return this.mDynamicFont != null || this.mFont.isValid;
		}
	}

	// Token: 0x17000106 RID: 262
	// (get) Token: 0x06000615 RID: 1557 RVA: 0x000205D0 File Offset: 0x0001E7D0
	public int size
	{
		get
		{
			return (!(this.mReplacement != null)) ? ((!this.isDynamic) ? this.mFont.charSize : this.mDynamicFontSize) : this.mReplacement.size;
		}
	}

	// Token: 0x17000107 RID: 263
	// (get) Token: 0x06000616 RID: 1558 RVA: 0x00020620 File Offset: 0x0001E820
	public UISpriteData sprite
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.sprite;
			}
			if (!this.mSpriteSet)
			{
				this.mSprite = null;
			}
			if (this.mSprite == null)
			{
				if (this.mAtlas != null && !string.IsNullOrEmpty(this.mFont.spriteName))
				{
					this.mSprite = this.mAtlas.GetSprite(this.mFont.spriteName);
					if (this.mSprite == null)
					{
						this.mSprite = this.mAtlas.GetSprite(base.name);
					}
					this.mSpriteSet = true;
					if (this.mSprite == null)
					{
						this.mFont.spriteName = null;
					}
				}
				int i = 0;
				int count = this.mSymbols.Count;
				while (i < count)
				{
					this.symbols[i].MarkAsDirty();
					i++;
				}
			}
			return this.mSprite;
		}
	}

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06000617 RID: 1559 RVA: 0x00020720 File Offset: 0x0001E920
	// (set) Token: 0x06000618 RID: 1560 RVA: 0x00020728 File Offset: 0x0001E928
	public UIFont replacement
	{
		get
		{
			return this.mReplacement;
		}
		set
		{
			UIFont uifont = value;
			if (uifont == this)
			{
				uifont = null;
			}
			if (this.mReplacement != uifont)
			{
				if (uifont != null && uifont.replacement == this)
				{
					uifont.replacement = null;
				}
				if (this.mReplacement != null)
				{
					this.MarkAsDirty();
				}
				this.mReplacement = uifont;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x06000619 RID: 1561 RVA: 0x000207A0 File Offset: 0x0001E9A0
	public bool isDynamic
	{
		get
		{
			return this.mDynamicFont != null;
		}
	}

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x0600061A RID: 1562 RVA: 0x000207B0 File Offset: 0x0001E9B0
	// (set) Token: 0x0600061B RID: 1563 RVA: 0x000207DC File Offset: 0x0001E9DC
	public Font dynamicFont
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mDynamicFont : this.mReplacement.dynamicFont;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.dynamicFont = value;
			}
			else if (this.mDynamicFont != value)
			{
				if (this.mDynamicFont != null)
				{
					this.material = null;
				}
				this.mDynamicFont = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x0600061C RID: 1564 RVA: 0x00020844 File Offset: 0x0001EA44
	// (set) Token: 0x0600061D RID: 1565 RVA: 0x00020870 File Offset: 0x0001EA70
	public int dynamicFontSize
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mDynamicFontSize : this.mReplacement.dynamicFontSize;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.dynamicFontSize = value;
			}
			else
			{
				value = Mathf.Clamp(value, 4, 128);
				if (this.mDynamicFontSize != value)
				{
					this.mDynamicFontSize = value;
					this.MarkAsDirty();
				}
			}
		}
	}

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x0600061E RID: 1566 RVA: 0x000208C8 File Offset: 0x0001EAC8
	// (set) Token: 0x0600061F RID: 1567 RVA: 0x000208F4 File Offset: 0x0001EAF4
	public FontStyle dynamicFontStyle
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mDynamicFontStyle : this.mReplacement.dynamicFontStyle;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.dynamicFontStyle = value;
			}
			else if (this.mDynamicFontStyle != value)
			{
				this.mDynamicFontStyle = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x00020934 File Offset: 0x0001EB34
	private void Trim()
	{
		Texture texture = this.mAtlas.texture;
		if (texture != null && this.mSprite != null)
		{
			Rect rect = NGUIMath.ConvertToPixels(this.mUVRect, this.texture.width, this.texture.height, true);
			Rect rect2 = new Rect((float)this.mSprite.x, (float)this.mSprite.y, (float)this.mSprite.width, (float)this.mSprite.height);
			int xMin = Mathf.RoundToInt(rect2.xMin - rect.xMin);
			int yMin = Mathf.RoundToInt(rect2.yMin - rect.yMin);
			int xMax = Mathf.RoundToInt(rect2.xMax - rect.xMin);
			int yMax = Mathf.RoundToInt(rect2.yMax - rect.yMin);
			this.mFont.Trim(xMin, yMin, xMax, yMax);
		}
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x00020A28 File Offset: 0x0001EC28
	private bool References(UIFont font)
	{
		return !(font == null) && (font == this || (this.mReplacement != null && this.mReplacement.References(font)));
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x00020A74 File Offset: 0x0001EC74
	public static bool CheckIfRelated(UIFont a, UIFont b)
	{
		return !(a == null) && !(b == null) && ((a.isDynamic && b.isDynamic && a.dynamicFont.fontNames[0] == b.dynamicFont.fontNames[0]) || a == b || a.References(b) || b.References(a));
	}

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x06000623 RID: 1571 RVA: 0x00020AFC File Offset: 0x0001ECFC
	private Texture dynamicTexture
	{
		get
		{
			if (this.mReplacement)
			{
				return this.mReplacement.dynamicTexture;
			}
			if (this.isDynamic)
			{
				return this.mDynamicFont.material.mainTexture;
			}
			return null;
		}
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x00020B44 File Offset: 0x0001ED44
	public void MarkAsDirty()
	{
		if (this.mReplacement != null)
		{
			this.mReplacement.MarkAsDirty();
		}
		this.mSprite = null;
		UILabel[] array = NGUITools.FindActive<UILabel>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			UILabel uilabel = array[i];
			if (uilabel.enabled && NGUITools.GetActive(uilabel.gameObject) && UIFont.CheckIfRelated(this, uilabel.font))
			{
				UIFont font = uilabel.font;
				uilabel.font = null;
				uilabel.font = font;
			}
			i++;
		}
		int j = 0;
		int count = this.mSymbols.Count;
		while (j < count)
		{
			this.symbols[j].MarkAsDirty();
			j++;
		}
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x00020C10 File Offset: 0x0001EE10
	public void Request(string text)
	{
		if (!string.IsNullOrEmpty(text))
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.Request(text);
			}
			else if (this.mDynamicFont != null)
			{
				this.mDynamicFont.RequestCharactersInTexture("j", this.mDynamicFontSize, this.mDynamicFontStyle);
				this.mDynamicFont.GetCharacterInfo('j', out UIFont.mTemp, this.mDynamicFontSize, this.mDynamicFontStyle);
				this.mDynamicFontOffset = (float)this.mDynamicFontSize + UIFont.mTemp.vert.yMax;
				this.mDynamicFont.RequestCharactersInTexture(text, this.mDynamicFontSize, this.mDynamicFontStyle);
			}
		}
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x00020CCC File Offset: 0x0001EECC
	public Vector2 CalculatePrintedSize(string text, bool encoding, UIFont.SymbolStyle symbolStyle)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.CalculatePrintedSize(text, encoding, symbolStyle);
		}
		Vector2 zero = Vector2.zero;
		bool isDynamic = this.isDynamic;
		if (isDynamic || (this.mFont != null && this.mFont.isValid && !string.IsNullOrEmpty(text)))
		{
			if (encoding)
			{
				text = NGUITools.StripSymbols(text);
			}
			if (this.mDynamicFont != null)
			{
				this.mDynamicFont.RequestCharactersInTexture(text, this.mDynamicFontSize, this.mDynamicFontStyle);
			}
			int length = text.Length;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int size = this.size;
			int num5 = size + this.mSpacingY;
			bool flag = encoding && symbolStyle != UIFont.SymbolStyle.None && this.hasSymbols;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (c == '\n')
				{
					if (num2 > num)
					{
						num = num2;
					}
					num2 = 0;
					num3 += num5;
					num4 = 0;
				}
				else if (c < ' ')
				{
					num4 = 0;
				}
				else if (!isDynamic)
				{
					BMSymbol bmsymbol = (!flag) ? null : this.MatchSymbol(text, i, length);
					if (bmsymbol == null)
					{
						BMGlyph glyph = this.mFont.GetGlyph((int)c);
						if (glyph != null)
						{
							num2 += this.mSpacingX + ((num4 == 0) ? glyph.advance : (glyph.advance + glyph.GetKerning(num4)));
							num4 = (int)c;
						}
					}
					else
					{
						num2 += this.mSpacingX + bmsymbol.width;
						i += bmsymbol.length - 1;
						num4 = 0;
					}
				}
				else if (this.mDynamicFont.GetCharacterInfo(c, out UIFont.mChar, this.mDynamicFontSize, this.mDynamicFontStyle))
				{
					num2 += (int)((float)this.mSpacingX + UIFont.mChar.width);
				}
			}
			zero.x = (float)((num2 <= num) ? num : num2);
			zero.y = (float)(num3 + num5);
		}
		return zero;
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x00020EFC File Offset: 0x0001F0FC
	private static void EndLine(ref StringBuilder s)
	{
		int num = s.Length - 1;
		if (num > 0 && s[num] == ' ')
		{
			s[num] = '\n';
		}
		else
		{
			s.Append('\n');
		}
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x00020F44 File Offset: 0x0001F144
	public string GetEndOfLineThatFits(string text, float maxWidth, bool encoding, UIFont.SymbolStyle symbolStyle)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.GetEndOfLineThatFits(text, maxWidth, encoding, symbolStyle);
		}
		int num = Mathf.RoundToInt(maxWidth);
		if (num < 1)
		{
			return text;
		}
		if (this.mDynamicFont != null)
		{
			this.mDynamicFont.RequestCharactersInTexture(text, this.mDynamicFontSize, this.mDynamicFontStyle);
		}
		int length = text.Length;
		int num2 = num;
		BMGlyph bmglyph = null;
		int num3 = length;
		bool flag = encoding && symbolStyle != UIFont.SymbolStyle.None && this.hasSymbols;
		bool isDynamic = this.isDynamic;
		while (num3 > 0 && num2 > 0)
		{
			char c = text[--num3];
			BMSymbol bmsymbol = (!flag) ? null : this.MatchSymbol(text, num3, length);
			int num4 = this.mSpacingX;
			if (!isDynamic)
			{
				if (bmsymbol != null)
				{
					num4 += bmsymbol.advance;
				}
				else
				{
					BMGlyph glyph = this.mFont.GetGlyph((int)c);
					if (glyph == null)
					{
						bmglyph = null;
						continue;
					}
					num4 += glyph.advance + ((bmglyph != null) ? bmglyph.GetKerning((int)c) : 0);
					bmglyph = glyph;
				}
			}
			else if (this.mDynamicFont.GetCharacterInfo(c, out UIFont.mChar, this.mDynamicFontSize, this.mDynamicFontStyle))
			{
				num4 += (int)UIFont.mChar.width;
			}
			num2 -= num4;
		}
		if (num2 < 0)
		{
			num3++;
		}
		return text.Substring(num3, length - num3);
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x000210DC File Offset: 0x0001F2DC
	public bool WrapText(string text, out string finalText, int width, int height, int maxLines, bool encoding, UIFont.SymbolStyle symbolStyle)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.WrapText(text, out finalText, width, height, maxLines, encoding, symbolStyle);
		}
		if (width < 1 || height < 1)
		{
			finalText = string.Empty;
			return false;
		}
		int num = (maxLines <= 0) ? 999999 : maxLines;
		num = Mathf.Min(num, height / this.size);
		if (num == 0)
		{
			finalText = string.Empty;
			return false;
		}
		if (this.mDynamicFont != null)
		{
			this.mDynamicFont.RequestCharactersInTexture(text, this.mDynamicFontSize, this.mDynamicFontStyle);
		}
		StringBuilder stringBuilder = new StringBuilder();
		int length = text.Length;
		int num2 = width;
		int num3 = 0;
		int num4 = 0;
		int i = 0;
		bool flag = true;
		bool flag2 = maxLines != 1;
		int num5 = 1;
		bool flag3 = encoding && symbolStyle != UIFont.SymbolStyle.None && this.hasSymbols;
		bool isDynamic = this.isDynamic;
		while (i < length)
		{
			char c = text[i];
			if (c == '\n')
			{
				if (!flag2 || num5 == num)
				{
					break;
				}
				num2 = width;
				if (num4 < i)
				{
					stringBuilder.Append(text.Substring(num4, i - num4 + 1));
				}
				else
				{
					stringBuilder.Append(c);
				}
				flag = true;
				num5++;
				num4 = i + 1;
				num3 = 0;
			}
			else
			{
				if (c == ' ' && num3 != 32 && num4 < i)
				{
					stringBuilder.Append(text.Substring(num4, i - num4 + 1));
					flag = false;
					num4 = i + 1;
					num3 = (int)c;
				}
				if (encoding && c == '[' && i + 2 < length)
				{
					if (text[i + 1] == '-' && text[i + 2] == ']')
					{
						i += 2;
						goto IL_407;
					}
					if (i + 7 < length && text[i + 7] == ']' && NGUITools.EncodeColor(NGUITools.ParseColor(text, i + 1)) == text.Substring(i + 1, 6).ToUpper())
					{
						i += 7;
						goto IL_407;
					}
				}
				BMSymbol bmsymbol = (!flag3) ? null : this.MatchSymbol(text, i, length);
				int num6 = this.mSpacingX;
				if (!isDynamic)
				{
					if (bmsymbol != null)
					{
						num6 += bmsymbol.advance;
					}
					else
					{
						BMGlyph bmglyph = (bmsymbol != null) ? null : this.mFont.GetGlyph((int)c);
						if (bmglyph == null)
						{
							goto IL_407;
						}
						num6 += ((num3 == 0) ? bmglyph.advance : (bmglyph.advance + bmglyph.GetKerning(num3)));
					}
				}
				else if (this.mDynamicFont.GetCharacterInfo(c, out UIFont.mChar, this.mDynamicFontSize, this.mDynamicFontStyle))
				{
					num6 += Mathf.RoundToInt(UIFont.mChar.width);
				}
				num2 -= num6;
				if (num2 < 0)
				{
					if (flag || !flag2 || num5 == num)
					{
						stringBuilder.Append(text.Substring(num4, Mathf.Max(0, i - num4)));
						if (!flag2 || num5 == num)
						{
							num4 = i;
							break;
						}
						UIFont.EndLine(ref stringBuilder);
						flag = true;
						num5++;
						if (c == ' ')
						{
							num4 = i + 1;
							num2 = width;
						}
						else
						{
							num4 = i;
							num2 = width - num6;
						}
						num3 = 0;
					}
					else
					{
						while (num4 < length && text[num4] == ' ')
						{
							num4++;
						}
						flag = true;
						num2 = width;
						i = num4 - 1;
						num3 = 0;
						if (!flag2 || num5 == num)
						{
							break;
						}
						num5++;
						UIFont.EndLine(ref stringBuilder);
						goto IL_407;
					}
				}
				else
				{
					num3 = (int)c;
				}
				if (!isDynamic && bmsymbol != null)
				{
					i += bmsymbol.length - 1;
					num3 = 0;
				}
			}
			IL_407:
			i++;
		}
		if (num4 < i)
		{
			stringBuilder.Append(text.Substring(num4, i - num4));
		}
		finalText = stringBuilder.ToString();
		return !flag2 || i == length || (maxLines > 0 && num5 <= maxLines);
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x0002154C File Offset: 0x0001F74C
	public bool WrapText(string text, out string finalText, int width, int height, int maxLines, bool encoding)
	{
		return this.WrapText(text, out finalText, width, height, maxLines, encoding, UIFont.SymbolStyle.None);
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x0002156C File Offset: 0x0001F76C
	public bool WrapText(string text, out string finalText, int width, int height, int maxLineCount)
	{
		return this.WrapText(text, out finalText, width, height, maxLineCount, false, UIFont.SymbolStyle.None);
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x00021588 File Offset: 0x0001F788
	private void Align(BetterList<Vector3> verts, int indexOffset, UIFont.Alignment alignment, int x, int lineWidth)
	{
		if (alignment != UIFont.Alignment.Left)
		{
			float num;
			if (alignment == UIFont.Alignment.Right)
			{
				num = (float)(lineWidth - x);
				if (num < 0f)
				{
					num = 0f;
				}
			}
			else
			{
				num = (float)Mathf.RoundToInt((float)(lineWidth - x) * 0.5f);
				if (num < 0f)
				{
					num = 0f;
				}
				if ((lineWidth & 1) == 1)
				{
					num += 0.5f;
				}
			}
			for (int i = indexOffset; i < verts.size; i++)
			{
				Vector3 vector = verts.buffer[i];
				vector.x += num;
				verts.buffer[i] = vector;
			}
		}
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x00021648 File Offset: 0x0001F848
	public void Print(string text, Color32 color, BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols, bool encoding, UIFont.SymbolStyle symbolStyle, UIFont.Alignment alignment, int lineWidth, bool premultiply)
	{
		if (this.mReplacement != null)
		{
			this.mReplacement.Print(text, color, verts, uvs, cols, encoding, symbolStyle, alignment, lineWidth, premultiply);
		}
		else if (text != null)
		{
			if (!this.isValid)
			{
				global::Debug.LogError("Attempting to print using an invalid font!");
				return;
			}
			if (this.mDynamicFont != null)
			{
				this.mDynamicFont.RequestCharactersInTexture(text, this.mDynamicFontSize, this.mDynamicFontStyle);
			}
			bool isDynamic = this.isDynamic;
			this.mColors.Clear();
			this.mColors.Add(color);
			int size = this.size;
			int size2 = verts.size;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = size + this.mSpacingY;
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			Vector2 zero3 = Vector2.zero;
			Vector2 zero4 = Vector2.zero;
			float num6 = this.uvRect.width / (float)this.mFont.texWidth;
			float num7 = this.mUVRect.height / (float)this.mFont.texHeight;
			int length = text.Length;
			bool flag = encoding && symbolStyle != UIFont.SymbolStyle.None && this.hasSymbols && this.sprite != null;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (c == '\n')
				{
					if (num2 > num)
					{
						num = num2;
					}
					if (alignment != UIFont.Alignment.Left)
					{
						this.Align(verts, size2, alignment, num2, lineWidth);
						size2 = verts.size;
					}
					num2 = 0;
					num3 += num5;
					num4 = 0;
				}
				else if (c < ' ')
				{
					num4 = 0;
				}
				else
				{
					if (encoding && c == '[')
					{
						int num8 = NGUITools.ParseSymbol(text, i, this.mColors, premultiply);
						if (num8 > 0)
						{
							color = this.mColors[this.mColors.Count - 1];
							i += num8 - 1;
							goto IL_8CA;
						}
					}
					if (!isDynamic)
					{
						BMSymbol bmsymbol = (!flag) ? null : this.MatchSymbol(text, i, length);
						if (bmsymbol == null)
						{
							BMGlyph glyph = this.mFont.GetGlyph((int)c);
							if (glyph == null)
							{
								goto IL_8CA;
							}
							if (num4 != 0)
							{
								num2 += glyph.GetKerning(num4);
							}
							if (c == ' ')
							{
								num2 += this.mSpacingX + glyph.advance;
								num4 = (int)c;
								goto IL_8CA;
							}
							zero.x = (float)(num2 + glyph.offsetX);
							zero.y = (float)(-(float)(num3 + glyph.offsetY));
							zero2.x = zero.x + (float)glyph.width;
							zero2.y = zero.y - (float)glyph.height;
							zero3.x = this.mUVRect.xMin + num6 * (float)glyph.x;
							zero3.y = this.mUVRect.yMax - num7 * (float)glyph.y;
							zero4.x = zero3.x + num6 * (float)glyph.width;
							zero4.y = zero3.y - num7 * (float)glyph.height;
							num2 += this.mSpacingX + glyph.advance;
							num4 = (int)c;
							if (glyph.channel == 0 || glyph.channel == 15)
							{
								for (int j = 0; j < 4; j++)
								{
									cols.Add(color);
								}
							}
							else
							{
								Color color2 = color;
								color2 *= 0.49f;
								switch (glyph.channel)
								{
								case 1:
									color2.b += 0.51f;
									break;
								case 2:
									color2.g += 0.51f;
									break;
								case 4:
									color2.r += 0.51f;
									break;
								case 8:
									color2.a += 0.51f;
									break;
								}
								for (int k = 0; k < 4; k++)
								{
									cols.Add(color2);
								}
							}
						}
						else
						{
							zero.x = (float)(num2 + bmsymbol.offsetX);
							zero.y = (float)(-(float)(num3 + bmsymbol.offsetY));
							zero2.x = zero.x + (float)bmsymbol.width;
							zero2.y = zero.y - (float)bmsymbol.height;
							Rect uvRect = bmsymbol.uvRect;
							zero3.x = uvRect.xMin;
							zero3.y = uvRect.yMax;
							zero4.x = uvRect.xMax;
							zero4.y = uvRect.yMin;
							num2 += this.mSpacingX + bmsymbol.advance;
							i += bmsymbol.length - 1;
							num4 = 0;
							if (symbolStyle == UIFont.SymbolStyle.Colored)
							{
								for (int l = 0; l < 4; l++)
								{
									cols.Add(color);
								}
							}
							else
							{
								Color32 item = Color.white;
								item.a = color.a;
								for (int m = 0; m < 4; m++)
								{
									cols.Add(item);
								}
							}
						}
						verts.Add(new Vector3(zero2.x, zero.y));
						verts.Add(new Vector3(zero2.x, zero2.y));
						verts.Add(new Vector3(zero.x, zero2.y));
						verts.Add(new Vector3(zero.x, zero.y));
						uvs.Add(new Vector2(zero4.x, zero3.y));
						uvs.Add(new Vector2(zero4.x, zero4.y));
						uvs.Add(new Vector2(zero3.x, zero4.y));
						uvs.Add(new Vector2(zero3.x, zero3.y));
					}
					else if (this.mDynamicFont.GetCharacterInfo(c, out UIFont.mChar, this.mDynamicFontSize, this.mDynamicFontStyle))
					{
						zero.x = (float)num2 + UIFont.mChar.vert.xMin;
						zero.y = -((float)num3 - UIFont.mChar.vert.yMax + this.mDynamicFontOffset);
						zero2.x = zero.x + UIFont.mChar.vert.width;
						zero2.y = zero.y - UIFont.mChar.vert.height;
						zero3.x = UIFont.mChar.uv.xMin;
						zero3.y = UIFont.mChar.uv.yMin;
						zero4.x = UIFont.mChar.uv.xMax;
						zero4.y = UIFont.mChar.uv.yMax;
						num2 += this.mSpacingX + (int)UIFont.mChar.width;
						for (int n = 0; n < 4; n++)
						{
							cols.Add(color);
						}
						if (UIFont.mChar.flipped)
						{
							uvs.Add(new Vector2(zero3.x, zero4.y));
							uvs.Add(new Vector2(zero3.x, zero3.y));
							uvs.Add(new Vector2(zero4.x, zero3.y));
							uvs.Add(new Vector2(zero4.x, zero4.y));
						}
						else
						{
							uvs.Add(new Vector2(zero4.x, zero3.y));
							uvs.Add(new Vector2(zero3.x, zero3.y));
							uvs.Add(new Vector2(zero3.x, zero4.y));
							uvs.Add(new Vector2(zero4.x, zero4.y));
						}
						verts.Add(new Vector3(zero2.x, zero.y));
						verts.Add(new Vector3(zero.x, zero.y));
						verts.Add(new Vector3(zero.x, zero2.y));
						verts.Add(new Vector3(zero2.x, zero2.y));
					}
				}
				IL_8CA:;
			}
			if (alignment != UIFont.Alignment.Left && size2 < verts.size)
			{
				this.Align(verts, size2, alignment, num2, lineWidth);
				size2 = verts.size;
			}
		}
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x00021F58 File Offset: 0x00020158
	private BMSymbol GetSymbol(string sequence, bool createIfMissing)
	{
		int i = 0;
		int count = this.mSymbols.Count;
		while (i < count)
		{
			BMSymbol bmsymbol = this.mSymbols[i];
			if (bmsymbol.sequence == sequence)
			{
				return bmsymbol;
			}
			i++;
		}
		if (createIfMissing)
		{
			BMSymbol bmsymbol2 = new BMSymbol();
			bmsymbol2.sequence = sequence;
			this.mSymbols.Add(bmsymbol2);
			return bmsymbol2;
		}
		return null;
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x00021FC8 File Offset: 0x000201C8
	private BMSymbol MatchSymbol(string text, int offset, int textLength)
	{
		int count = this.mSymbols.Count;
		if (count == 0)
		{
			return null;
		}
		textLength -= offset;
		for (int i = 0; i < count; i++)
		{
			BMSymbol bmsymbol = this.mSymbols[i];
			int length = bmsymbol.length;
			if (length != 0 && textLength >= length)
			{
				bool flag = true;
				for (int j = 0; j < length; j++)
				{
					if (text[offset + j] != bmsymbol.sequence[j])
					{
						flag = false;
						break;
					}
				}
				if (flag && bmsymbol.Validate(this.atlas))
				{
					return bmsymbol;
				}
			}
		}
		return null;
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x00022080 File Offset: 0x00020280
	public void AddSymbol(string sequence, string spriteName)
	{
		BMSymbol symbol = this.GetSymbol(sequence, true);
		symbol.spriteName = spriteName;
		this.MarkAsDirty();
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x000220A4 File Offset: 0x000202A4
	public void RemoveSymbol(string sequence)
	{
		BMSymbol symbol = this.GetSymbol(sequence, false);
		if (symbol != null)
		{
			this.symbols.Remove(symbol);
		}
		this.MarkAsDirty();
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x000220D4 File Offset: 0x000202D4
	public void RenameSymbol(string before, string after)
	{
		BMSymbol symbol = this.GetSymbol(before, false);
		if (symbol != null)
		{
			symbol.sequence = after;
		}
		this.MarkAsDirty();
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x00022100 File Offset: 0x00020300
	public bool UsesSprite(string s)
	{
		if (!string.IsNullOrEmpty(s))
		{
			if (s.Equals(this.spriteName))
			{
				return true;
			}
			int i = 0;
			int count = this.symbols.Count;
			while (i < count)
			{
				BMSymbol bmsymbol = this.symbols[i];
				if (s.Equals(bmsymbol.spriteName))
				{
					return true;
				}
				i++;
			}
		}
		return false;
	}

	// Token: 0x0400047D RID: 1149
	[SerializeField]
	[HideInInspector]
	private Material mMat;

	// Token: 0x0400047E RID: 1150
	[SerializeField]
	[HideInInspector]
	private Rect mUVRect = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x0400047F RID: 1151
	[SerializeField]
	[HideInInspector]
	private BMFont mFont = new BMFont();

	// Token: 0x04000480 RID: 1152
	[HideInInspector]
	[SerializeField]
	private int mSpacingX;

	// Token: 0x04000481 RID: 1153
	[SerializeField]
	[HideInInspector]
	private int mSpacingY;

	// Token: 0x04000482 RID: 1154
	[SerializeField]
	[HideInInspector]
	private UIAtlas mAtlas;

	// Token: 0x04000483 RID: 1155
	[SerializeField]
	[HideInInspector]
	private UIFont mReplacement;

	// Token: 0x04000484 RID: 1156
	[HideInInspector]
	[SerializeField]
	private float mPixelSize = 1f;

	// Token: 0x04000485 RID: 1157
	[HideInInspector]
	[SerializeField]
	private List<BMSymbol> mSymbols = new List<BMSymbol>();

	// Token: 0x04000486 RID: 1158
	[SerializeField]
	[HideInInspector]
	private Font mDynamicFont;

	// Token: 0x04000487 RID: 1159
	[SerializeField]
	[HideInInspector]
	private int mDynamicFontSize = 16;

	// Token: 0x04000488 RID: 1160
	[HideInInspector]
	[SerializeField]
	private FontStyle mDynamicFontStyle;

	// Token: 0x04000489 RID: 1161
	[HideInInspector]
	[SerializeField]
	private float mDynamicFontOffset;

	// Token: 0x0400048A RID: 1162
	private UISpriteData mSprite;

	// Token: 0x0400048B RID: 1163
	private int mPMA = -1;

	// Token: 0x0400048C RID: 1164
	private bool mSpriteSet;

	// Token: 0x0400048D RID: 1165
	private List<Color> mColors = new List<Color>();

	// Token: 0x0400048E RID: 1166
	private static CharacterInfo mTemp;

	// Token: 0x0400048F RID: 1167
	private static CharacterInfo mChar;

	// Token: 0x020000D4 RID: 212
	public enum Alignment
	{
		// Token: 0x04000491 RID: 1169
		Left,
		// Token: 0x04000492 RID: 1170
		Center,
		// Token: 0x04000493 RID: 1171
		Right
	}

	// Token: 0x020000D5 RID: 213
	public enum SymbolStyle
	{
		// Token: 0x04000495 RID: 1173
		None,
		// Token: 0x04000496 RID: 1174
		Uncolored,
		// Token: 0x04000497 RID: 1175
		Colored
	}
}
