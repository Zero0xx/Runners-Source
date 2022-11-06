using System;
using UnityEngine;

// Token: 0x020000D8 RID: 216
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Label")]
public class UILabel : UIWidget
{
	// Token: 0x17000112 RID: 274
	// (get) Token: 0x0600064B RID: 1611 RVA: 0x00022EAC File Offset: 0x000210AC
	// (set) Token: 0x0600064C RID: 1612 RVA: 0x00022EB4 File Offset: 0x000210B4
	private bool hasChanged
	{
		get
		{
			return this.mShouldBeProcessed;
		}
		set
		{
			if (value)
			{
				this.mChanged = true;
				this.mShouldBeProcessed = true;
			}
			else
			{
				this.mShouldBeProcessed = false;
			}
		}
	}

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x0600064D RID: 1613 RVA: 0x00022EE4 File Offset: 0x000210E4
	public override Material material
	{
		get
		{
			return (!(this.mFont != null)) ? null : this.mFont.material;
		}
	}

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x0600064E RID: 1614 RVA: 0x00022F14 File Offset: 0x00021114
	// (set) Token: 0x0600064F RID: 1615 RVA: 0x00022F1C File Offset: 0x0002111C
	public UIFont font
	{
		get
		{
			return this.mFont;
		}
		set
		{
			if (this.mFont != value)
			{
				if (this.mFont != null && this.mFont.dynamicFont != null)
				{
					Font dynamicFont = this.mFont.dynamicFont;
					dynamicFont.textureRebuildCallback = (Font.FontTextureRebuildCallback)Delegate.Remove(dynamicFont.textureRebuildCallback, new Font.FontTextureRebuildCallback(this.MarkAsChanged));
				}
				base.RemoveFromPanel();
				this.mFont = value;
				this.hasChanged = true;
				if (this.mFont != null && this.mFont.dynamicFont != null)
				{
					Font dynamicFont2 = this.mFont.dynamicFont;
					dynamicFont2.textureRebuildCallback = (Font.FontTextureRebuildCallback)Delegate.Combine(dynamicFont2.textureRebuildCallback, new Font.FontTextureRebuildCallback(this.MarkAsChanged));
					this.mFont.Request(this.mText);
				}
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x06000650 RID: 1616 RVA: 0x00023010 File Offset: 0x00021210
	// (set) Token: 0x06000651 RID: 1617 RVA: 0x00023018 File Offset: 0x00021218
	public string text
	{
		get
		{
			return this.mText;
		}
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				if (!string.IsNullOrEmpty(this.mText))
				{
					this.mText = string.Empty;
				}
				this.hasChanged = true;
			}
			else if (this.mText != value)
			{
				this.mText = value;
				this.hasChanged = true;
				if (this.mFont != null)
				{
					this.mFont.Request(value);
				}
			}
		}
	}

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x06000652 RID: 1618 RVA: 0x00023094 File Offset: 0x00021294
	// (set) Token: 0x06000653 RID: 1619 RVA: 0x0002309C File Offset: 0x0002129C
	public bool supportEncoding
	{
		get
		{
			return this.mEncoding;
		}
		set
		{
			if (this.mEncoding != value)
			{
				this.mEncoding = value;
				this.hasChanged = true;
				if (value)
				{
					this.mPassword = false;
				}
			}
		}
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x06000654 RID: 1620 RVA: 0x000230C8 File Offset: 0x000212C8
	// (set) Token: 0x06000655 RID: 1621 RVA: 0x000230D0 File Offset: 0x000212D0
	public UIFont.SymbolStyle symbolStyle
	{
		get
		{
			return this.mSymbols;
		}
		set
		{
			if (this.mSymbols != value)
			{
				this.mSymbols = value;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x06000656 RID: 1622 RVA: 0x000230EC File Offset: 0x000212EC
	// (set) Token: 0x06000657 RID: 1623 RVA: 0x000230F4 File Offset: 0x000212F4
	public UILabel.Overflow overflowMethod
	{
		get
		{
			return this.mOverflow;
		}
		set
		{
			if (this.mOverflow != value)
			{
				this.mOverflow = value;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x06000658 RID: 1624 RVA: 0x00023110 File Offset: 0x00021310
	// (set) Token: 0x06000659 RID: 1625 RVA: 0x00023118 File Offset: 0x00021318
	[Obsolete("Use 'width' instead")]
	public int lineWidth
	{
		get
		{
			return base.width;
		}
		set
		{
			base.width = value;
		}
	}

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x0600065A RID: 1626 RVA: 0x00023124 File Offset: 0x00021324
	// (set) Token: 0x0600065B RID: 1627 RVA: 0x0002312C File Offset: 0x0002132C
	[Obsolete("Use 'height' instead")]
	public int lineHeight
	{
		get
		{
			return base.height;
		}
		set
		{
			base.height = value;
		}
	}

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x0600065C RID: 1628 RVA: 0x00023138 File Offset: 0x00021338
	// (set) Token: 0x0600065D RID: 1629 RVA: 0x00023148 File Offset: 0x00021348
	public bool multiLine
	{
		get
		{
			return this.mMaxLineCount != 1;
		}
		set
		{
			if (this.mMaxLineCount != 1 != value)
			{
				this.mMaxLineCount = ((!value) ? 1 : 0);
				this.hasChanged = true;
				if (value)
				{
					this.mPassword = false;
				}
			}
		}
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x0600065E RID: 1630 RVA: 0x00023184 File Offset: 0x00021384
	public override Vector3[] localCorners
	{
		get
		{
			if (this.hasChanged)
			{
				this.ProcessText();
			}
			return base.localCorners;
		}
	}

	// Token: 0x1700011D RID: 285
	// (get) Token: 0x0600065F RID: 1631 RVA: 0x000231A0 File Offset: 0x000213A0
	public override Vector3[] worldCorners
	{
		get
		{
			if (this.hasChanged)
			{
				this.ProcessText();
			}
			return base.worldCorners;
		}
	}

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x06000660 RID: 1632 RVA: 0x000231BC File Offset: 0x000213BC
	// (set) Token: 0x06000661 RID: 1633 RVA: 0x000231C4 File Offset: 0x000213C4
	public int maxLineCount
	{
		get
		{
			return this.mMaxLineCount;
		}
		set
		{
			if (this.mMaxLineCount != value)
			{
				this.mMaxLineCount = Mathf.Max(value, 0);
				if (value != 1)
				{
					this.mPassword = false;
				}
				this.hasChanged = true;
				if (this.overflowMethod == UILabel.Overflow.ShrinkContent)
				{
					this.MakePixelPerfect();
				}
			}
		}
	}

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x06000662 RID: 1634 RVA: 0x00023210 File Offset: 0x00021410
	// (set) Token: 0x06000663 RID: 1635 RVA: 0x00023218 File Offset: 0x00021418
	public bool password
	{
		get
		{
			return this.mPassword;
		}
		set
		{
			if (this.mPassword != value)
			{
				if (value)
				{
					this.mMaxLineCount = 1;
					this.mEncoding = false;
				}
				this.mPassword = value;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x06000664 RID: 1636 RVA: 0x00023254 File Offset: 0x00021454
	// (set) Token: 0x06000665 RID: 1637 RVA: 0x0002325C File Offset: 0x0002145C
	public bool showLastPasswordChar
	{
		get
		{
			return this.mShowLastChar;
		}
		set
		{
			if (this.mShowLastChar != value)
			{
				this.mShowLastChar = value;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x06000666 RID: 1638 RVA: 0x00023278 File Offset: 0x00021478
	// (set) Token: 0x06000667 RID: 1639 RVA: 0x00023280 File Offset: 0x00021480
	public UILabel.Effect effectStyle
	{
		get
		{
			return this.mEffectStyle;
		}
		set
		{
			if (this.mEffectStyle != value)
			{
				this.mEffectStyle = value;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x06000668 RID: 1640 RVA: 0x0002329C File Offset: 0x0002149C
	// (set) Token: 0x06000669 RID: 1641 RVA: 0x000232A4 File Offset: 0x000214A4
	public Color effectColor
	{
		get
		{
			return this.mEffectColor;
		}
		set
		{
			if (!this.mEffectColor.Equals(value))
			{
				this.mEffectColor = value;
				if (this.mEffectStyle != UILabel.Effect.None)
				{
					this.hasChanged = true;
				}
			}
		}
	}

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x0600066A RID: 1642 RVA: 0x000232D8 File Offset: 0x000214D8
	// (set) Token: 0x0600066B RID: 1643 RVA: 0x000232E0 File Offset: 0x000214E0
	public Vector2 effectDistance
	{
		get
		{
			return this.mEffectDistance;
		}
		set
		{
			if (this.mEffectDistance != value)
			{
				this.mEffectDistance = value;
				this.hasChanged = true;
			}
		}
	}

	// Token: 0x17000124 RID: 292
	// (get) Token: 0x0600066C RID: 1644 RVA: 0x00023304 File Offset: 0x00021504
	// (set) Token: 0x0600066D RID: 1645 RVA: 0x00023310 File Offset: 0x00021510
	[Obsolete("Use 'overflowMethod == UILabel.Overflow.ShrinkContent' instead")]
	public bool shrinkToFit
	{
		get
		{
			return this.mOverflow == UILabel.Overflow.ShrinkContent;
		}
		set
		{
			if (value)
			{
				this.overflowMethod = UILabel.Overflow.ShrinkContent;
			}
		}
	}

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x0600066E RID: 1646 RVA: 0x00023320 File Offset: 0x00021520
	public string processedText
	{
		get
		{
			if (this.mLastWidth != this.mWidth || this.mLastHeight != this.mHeight)
			{
				this.mLastWidth = this.mWidth;
				this.mLastHeight = this.mHeight;
				this.mShouldBeProcessed = true;
			}
			if (this.hasChanged)
			{
				this.ProcessText();
			}
			return this.mProcessedText;
		}
	}

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x0600066F RID: 1647 RVA: 0x00023388 File Offset: 0x00021588
	public Vector2 printedSize
	{
		get
		{
			if (this.hasChanged)
			{
				this.ProcessText();
			}
			return this.mSize;
		}
	}

	// Token: 0x17000127 RID: 295
	// (get) Token: 0x06000670 RID: 1648 RVA: 0x000233A4 File Offset: 0x000215A4
	public override Vector2 localSize
	{
		get
		{
			if (this.hasChanged)
			{
				this.ProcessText();
			}
			return base.localSize;
		}
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x000233C0 File Offset: 0x000215C0
	protected override void OnEnable()
	{
		if (this.mFont != null && this.mFont.dynamicFont != null)
		{
			Font dynamicFont = this.mFont.dynamicFont;
			dynamicFont.textureRebuildCallback = (Font.FontTextureRebuildCallback)Delegate.Combine(dynamicFont.textureRebuildCallback, new Font.FontTextureRebuildCallback(this.MarkAsChanged));
		}
		base.OnEnable();
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x00023428 File Offset: 0x00021628
	protected override void OnDisable()
	{
		if (this.mFont != null && this.mFont.dynamicFont != null)
		{
			Font dynamicFont = this.mFont.dynamicFont;
			dynamicFont.textureRebuildCallback = (Font.FontTextureRebuildCallback)Delegate.Remove(dynamicFont.textureRebuildCallback, new Font.FontTextureRebuildCallback(this.MarkAsChanged));
		}
		base.OnDisable();
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x00023490 File Offset: 0x00021690
	protected override void UpgradeFrom265()
	{
		this.ProcessText(true);
		if (this.mShrinkToFit)
		{
			this.overflowMethod = UILabel.Overflow.ShrinkContent;
			this.mMaxLineCount = 0;
		}
		if (this.mMaxLineWidth != 0)
		{
			base.width = this.mMaxLineWidth;
			this.overflowMethod = ((this.mMaxLineCount <= 0) ? UILabel.Overflow.ShrinkContent : UILabel.Overflow.ResizeHeight);
		}
		else
		{
			this.overflowMethod = UILabel.Overflow.ResizeFreely;
		}
		if (this.mMaxLineHeight != 0)
		{
			base.height = this.mMaxLineHeight;
		}
		if (this.mFont != null)
		{
			int num = Mathf.RoundToInt((float)this.mFont.size * this.mFont.pixelSize);
			if (base.height < num)
			{
				base.height = num;
			}
		}
		this.mMaxLineWidth = 0;
		this.mMaxLineHeight = 0;
		this.mShrinkToFit = false;
		if (base.GetComponent<BoxCollider>() != null)
		{
			NGUITools.AddWidgetCollider(base.gameObject, true);
		}
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x00023588 File Offset: 0x00021788
	protected override void OnStart()
	{
		if (this.mLineWidth > 0f)
		{
			this.mMaxLineWidth = Mathf.RoundToInt(this.mLineWidth);
			this.mLineWidth = 0f;
		}
		if (!this.mMultiline)
		{
			this.mMaxLineCount = 1;
			this.mMultiline = true;
		}
		this.mPremultiply = (this.font != null && this.font.material != null && this.font.material.shader.name.Contains("Premultiplied"));
		if (this.mFont != null)
		{
			this.mFont.Request(this.mText);
		}
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x0002364C File Offset: 0x0002184C
	public override void MarkAsChanged()
	{
		this.hasChanged = true;
		base.MarkAsChanged();
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x0002365C File Offset: 0x0002185C
	private void ProcessText()
	{
		this.ProcessText(false);
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x00023668 File Offset: 0x00021868
	private void ProcessText(bool legacyMode)
	{
		if (this.mFont == null)
		{
			return;
		}
		this.mChanged = true;
		this.hasChanged = false;
		float num = 1f / this.mFont.pixelSize;
		float num2 = Mathf.Abs((!legacyMode) ? ((float)this.mFont.size) : base.cachedTransform.localScale.x);
		float num3 = (!legacyMode) ? ((float)base.width * num) : ((this.mMaxLineWidth == 0) ? 1000000f : ((float)this.mMaxLineWidth * num));
		float num4 = (!legacyMode) ? ((float)base.height * num) : ((this.mMaxLineHeight == 0) ? 1000000f : ((float)this.mMaxLineHeight * num));
		if (num2 > 0f)
		{
			for (;;)
			{
				this.mScale = num2 / (float)this.mFont.size;
				bool flag = true;
				int width = (this.mOverflow != UILabel.Overflow.ResizeFreely) ? Mathf.RoundToInt(num3 / this.mScale) : 100000;
				int height = (this.mOverflow != UILabel.Overflow.ResizeFreely && this.mOverflow != UILabel.Overflow.ResizeHeight) ? Mathf.RoundToInt(num4 / this.mScale) : 100000;
				if (this.mPassword)
				{
					this.mProcessedText = string.Empty;
					if (this.mShowLastChar)
					{
						int i = 0;
						int num5 = this.mText.Length - 1;
						while (i < num5)
						{
							this.mProcessedText += "*";
							i++;
						}
						if (this.mText.Length > 0)
						{
							this.mProcessedText += this.mText[this.mText.Length - 1];
						}
					}
					else
					{
						int j = 0;
						int length = this.mText.Length;
						while (j < length)
						{
							this.mProcessedText += "*";
							j++;
						}
					}
					flag = this.mFont.WrapText(this.mProcessedText, out this.mProcessedText, width, height, this.mMaxLineCount, false, UIFont.SymbolStyle.None);
				}
				else if (num3 > 0f || num4 > 0f)
				{
					flag = this.mFont.WrapText(this.mText, out this.mProcessedText, width, height, this.mMaxLineCount, this.mEncoding, this.mSymbols);
				}
				else
				{
					this.mProcessedText = this.mText;
				}
				this.mSize = (string.IsNullOrEmpty(this.mProcessedText) ? Vector2.zero : this.mFont.CalculatePrintedSize(this.mProcessedText, this.mEncoding, this.mSymbols));
				if (this.mOverflow == UILabel.Overflow.ResizeFreely)
				{
					break;
				}
				if (this.mOverflow == UILabel.Overflow.ResizeHeight)
				{
					goto Block_17;
				}
				if (this.mOverflow != UILabel.Overflow.ShrinkContent || flag)
				{
					goto IL_395;
				}
				num2 = Mathf.Round(num2 - 1f);
				if (num2 <= 1f)
				{
					goto IL_395;
				}
			}
			this.mWidth = Mathf.RoundToInt(this.mSize.x * this.mFont.pixelSize);
			this.mHeight = Mathf.RoundToInt(this.mSize.y * this.mFont.pixelSize);
			goto IL_395;
			Block_17:
			this.mHeight = Mathf.RoundToInt(this.mSize.y * this.mFont.pixelSize);
			IL_395:
			if (legacyMode)
			{
				base.width = Mathf.RoundToInt(this.mSize.x * this.mFont.pixelSize);
				base.height = Mathf.RoundToInt(this.mSize.y * this.mFont.pixelSize);
				base.cachedTransform.localScale = Vector3.one;
			}
		}
		else
		{
			base.cachedTransform.localScale = Vector3.one;
			this.mProcessedText = string.Empty;
			this.mScale = 1f;
		}
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x00023A9C File Offset: 0x00021C9C
	public override void MakePixelPerfect()
	{
		if (this.font != null)
		{
			float pixelSize = this.font.pixelSize;
			Vector3 localPosition = base.cachedTransform.localPosition;
			localPosition.x = (float)Mathf.RoundToInt(localPosition.x);
			localPosition.y = (float)Mathf.RoundToInt(localPosition.y);
			localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
			base.cachedTransform.localPosition = localPosition;
			base.cachedTransform.localScale = Vector3.one;
			if (this.mOverflow == UILabel.Overflow.ResizeFreely)
			{
				this.AssumeNaturalSize();
			}
			else
			{
				UILabel.Overflow overflow = this.mOverflow;
				this.mOverflow = UILabel.Overflow.ShrinkContent;
				this.ProcessText(false);
				this.mOverflow = overflow;
				int num = Mathf.RoundToInt(this.mSize.x * pixelSize);
				int num2 = Mathf.RoundToInt(this.mSize.y * pixelSize);
				if (base.width < num)
				{
					base.width = num;
				}
				if (base.height < num2)
				{
					base.height = num2;
				}
			}
		}
		else
		{
			base.MakePixelPerfect();
		}
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x00023BB8 File Offset: 0x00021DB8
	public void AssumeNaturalSize()
	{
		if (this.font != null)
		{
			this.ProcessText(false);
			float pixelSize = this.font.pixelSize;
			int num = Mathf.RoundToInt(this.mSize.x * pixelSize);
			int num2 = Mathf.RoundToInt(this.mSize.y * pixelSize);
			if (base.width < num)
			{
				base.width = num;
			}
			if (base.height < num2)
			{
				base.height = num2;
			}
		}
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x00023C38 File Offset: 0x00021E38
	private void ApplyShadow(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols, int start, int end, float x, float y)
	{
		Color color = this.mEffectColor;
		color.a *= base.alpha * this.mPanel.alpha;
		Color32 color2 = (!this.font.premultipliedAlpha) ? color : NGUITools.ApplyPMA(color);
		for (int i = start; i < end; i++)
		{
			verts.Add(verts.buffer[i]);
			uvs.Add(uvs.buffer[i]);
			cols.Add(cols.buffer[i]);
			Vector3 vector = verts.buffer[i];
			vector.x += x;
			vector.y += y;
			verts.buffer[i] = vector;
			cols.buffer[i] = color2;
		}
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x00023D40 File Offset: 0x00021F40
	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		if (this.mFont == null)
		{
			return;
		}
		UIWidget.Pivot pivot = base.pivot;
		int start = verts.size;
		Color c = base.color;
		c.a *= this.mPanel.alpha;
		if (this.font.premultipliedAlpha)
		{
			c = NGUITools.ApplyPMA(c);
		}
		string processedText = this.processedText;
		float num = this.mScale * this.mFont.pixelSize;
		int lineWidth = Mathf.RoundToInt((float)base.width / num);
		int size = verts.size;
		if (pivot == UIWidget.Pivot.Left || pivot == UIWidget.Pivot.TopLeft || pivot == UIWidget.Pivot.BottomLeft)
		{
			this.mFont.Print(processedText, c, verts, uvs, cols, this.mEncoding, this.mSymbols, UIFont.Alignment.Left, lineWidth, this.mPremultiply);
		}
		else if (pivot == UIWidget.Pivot.Right || pivot == UIWidget.Pivot.TopRight || pivot == UIWidget.Pivot.BottomRight)
		{
			this.mFont.Print(processedText, c, verts, uvs, cols, this.mEncoding, this.mSymbols, UIFont.Alignment.Right, lineWidth, this.mPremultiply);
		}
		else
		{
			this.mFont.Print(processedText, c, verts, uvs, cols, this.mEncoding, this.mSymbols, UIFont.Alignment.Center, lineWidth, this.mPremultiply);
		}
		Vector2 pivotOffset = base.pivotOffset;
		float num2 = Mathf.Lerp(0f, (float)(-(float)this.mWidth), pivotOffset.x);
		float num3 = Mathf.Lerp((float)this.mHeight, 0f, pivotOffset.y);
		num3 += Mathf.Lerp(this.mSize.y * num - (float)this.mHeight, 0f, pivotOffset.y);
		if (num == 1f)
		{
			for (int i = size; i < verts.size; i++)
			{
				Vector3[] buffer = verts.buffer;
				int num4 = i;
				buffer[num4].x = buffer[num4].x + num2;
				Vector3[] buffer2 = verts.buffer;
				int num5 = i;
				buffer2[num5].y = buffer2[num5].y + num3;
			}
		}
		else
		{
			for (int j = size; j < verts.size; j++)
			{
				verts.buffer[j].x = num2 + verts.buffer[j].x * num;
				verts.buffer[j].y = num3 + verts.buffer[j].y * num;
			}
		}
		if (this.effectStyle != UILabel.Effect.None)
		{
			int size2 = verts.size;
			float pixelSize = this.mFont.pixelSize;
			num2 = pixelSize * this.mEffectDistance.x;
			num3 = pixelSize * this.mEffectDistance.y;
			this.ApplyShadow(verts, uvs, cols, start, size2, num2, -num3);
			if (this.effectStyle == UILabel.Effect.Outline)
			{
				start = size2;
				size2 = verts.size;
				this.ApplyShadow(verts, uvs, cols, start, size2, -num2, num3);
				start = size2;
				size2 = verts.size;
				this.ApplyShadow(verts, uvs, cols, start, size2, num2, num3);
				start = size2;
				size2 = verts.size;
				this.ApplyShadow(verts, uvs, cols, start, size2, -num2, -num3);
			}
		}
	}

	// Token: 0x040004B7 RID: 1207
	[SerializeField]
	[HideInInspector]
	private UIFont mFont;

	// Token: 0x040004B8 RID: 1208
	[SerializeField]
	[HideInInspector]
	private string mText = string.Empty;

	// Token: 0x040004B9 RID: 1209
	[HideInInspector]
	[SerializeField]
	private bool mEncoding = true;

	// Token: 0x040004BA RID: 1210
	[SerializeField]
	[HideInInspector]
	private int mMaxLineCount;

	// Token: 0x040004BB RID: 1211
	[SerializeField]
	[HideInInspector]
	private bool mPassword;

	// Token: 0x040004BC RID: 1212
	[SerializeField]
	[HideInInspector]
	private bool mShowLastChar;

	// Token: 0x040004BD RID: 1213
	[HideInInspector]
	[SerializeField]
	private UILabel.Effect mEffectStyle;

	// Token: 0x040004BE RID: 1214
	[SerializeField]
	[HideInInspector]
	private Color mEffectColor = Color.black;

	// Token: 0x040004BF RID: 1215
	[HideInInspector]
	[SerializeField]
	private UIFont.SymbolStyle mSymbols = UIFont.SymbolStyle.Uncolored;

	// Token: 0x040004C0 RID: 1216
	[HideInInspector]
	[SerializeField]
	private Vector2 mEffectDistance = Vector2.one;

	// Token: 0x040004C1 RID: 1217
	[HideInInspector]
	[SerializeField]
	private UILabel.Overflow mOverflow;

	// Token: 0x040004C2 RID: 1218
	[HideInInspector]
	[SerializeField]
	private bool mShrinkToFit;

	// Token: 0x040004C3 RID: 1219
	[HideInInspector]
	[SerializeField]
	private int mMaxLineWidth;

	// Token: 0x040004C4 RID: 1220
	[HideInInspector]
	[SerializeField]
	private int mMaxLineHeight;

	// Token: 0x040004C5 RID: 1221
	[HideInInspector]
	[SerializeField]
	private float mLineWidth;

	// Token: 0x040004C6 RID: 1222
	[HideInInspector]
	[SerializeField]
	private bool mMultiline = true;

	// Token: 0x040004C7 RID: 1223
	private bool mShouldBeProcessed = true;

	// Token: 0x040004C8 RID: 1224
	private string mProcessedText;

	// Token: 0x040004C9 RID: 1225
	private bool mPremultiply;

	// Token: 0x040004CA RID: 1226
	private Vector2 mSize = Vector2.zero;

	// Token: 0x040004CB RID: 1227
	private float mScale = 1f;

	// Token: 0x040004CC RID: 1228
	private int mLastWidth;

	// Token: 0x040004CD RID: 1229
	private int mLastHeight;

	// Token: 0x020000D9 RID: 217
	public enum Effect
	{
		// Token: 0x040004CF RID: 1231
		None,
		// Token: 0x040004D0 RID: 1232
		Shadow,
		// Token: 0x040004D1 RID: 1233
		Outline
	}

	// Token: 0x020000DA RID: 218
	public enum Overflow
	{
		// Token: 0x040004D3 RID: 1235
		ShrinkContent,
		// Token: 0x040004D4 RID: 1236
		ClampContent,
		// Token: 0x040004D5 RID: 1237
		ResizeFreely,
		// Token: 0x040004D6 RID: 1238
		ResizeHeight
	}
}
