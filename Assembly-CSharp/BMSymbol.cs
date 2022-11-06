using System;
using UnityEngine;

// Token: 0x020000A7 RID: 167
[Serializable]
public class BMSymbol
{
	// Token: 0x17000099 RID: 153
	// (get) Token: 0x06000445 RID: 1093 RVA: 0x00015D54 File Offset: 0x00013F54
	public int length
	{
		get
		{
			if (this.mLength == 0)
			{
				this.mLength = this.sequence.Length;
			}
			return this.mLength;
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x06000446 RID: 1094 RVA: 0x00015D84 File Offset: 0x00013F84
	public int offsetX
	{
		get
		{
			return this.mOffsetX;
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x06000447 RID: 1095 RVA: 0x00015D8C File Offset: 0x00013F8C
	public int offsetY
	{
		get
		{
			return this.mOffsetY;
		}
	}

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x06000448 RID: 1096 RVA: 0x00015D94 File Offset: 0x00013F94
	public int width
	{
		get
		{
			return this.mWidth;
		}
	}

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x06000449 RID: 1097 RVA: 0x00015D9C File Offset: 0x00013F9C
	public int height
	{
		get
		{
			return this.mHeight;
		}
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x0600044A RID: 1098 RVA: 0x00015DA4 File Offset: 0x00013FA4
	public int advance
	{
		get
		{
			return this.mAdvance;
		}
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x0600044B RID: 1099 RVA: 0x00015DAC File Offset: 0x00013FAC
	public Rect uvRect
	{
		get
		{
			return this.mUV;
		}
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x00015DB4 File Offset: 0x00013FB4
	public void MarkAsDirty()
	{
		this.mIsValid = false;
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x00015DC0 File Offset: 0x00013FC0
	public bool Validate(UIAtlas atlas)
	{
		if (atlas == null)
		{
			return false;
		}
		if (!this.mIsValid)
		{
			if (string.IsNullOrEmpty(this.spriteName))
			{
				return false;
			}
			this.mSprite = ((!(atlas != null)) ? null : atlas.GetSprite(this.spriteName));
			if (this.mSprite != null)
			{
				Texture texture = atlas.texture;
				if (texture == null)
				{
					this.mSprite = null;
				}
				else
				{
					this.mUV = new Rect((float)this.mSprite.x, (float)this.mSprite.y, (float)this.mSprite.width, (float)this.mSprite.height);
					this.mUV = NGUIMath.ConvertToTexCoords(this.mUV, texture.width, texture.height);
					this.mOffsetX = this.mSprite.paddingLeft;
					this.mOffsetY = this.mSprite.paddingTop;
					this.mWidth = this.mSprite.width;
					this.mHeight = this.mSprite.height;
					this.mAdvance = this.mSprite.width + (this.mSprite.paddingLeft + this.mSprite.paddingRight);
					this.mIsValid = true;
				}
			}
		}
		return this.mSprite != null;
	}

	// Token: 0x0400033A RID: 826
	public string sequence;

	// Token: 0x0400033B RID: 827
	public string spriteName;

	// Token: 0x0400033C RID: 828
	private UISpriteData mSprite;

	// Token: 0x0400033D RID: 829
	private bool mIsValid;

	// Token: 0x0400033E RID: 830
	private int mLength;

	// Token: 0x0400033F RID: 831
	private int mOffsetX;

	// Token: 0x04000340 RID: 832
	private int mOffsetY;

	// Token: 0x04000341 RID: 833
	private int mWidth;

	// Token: 0x04000342 RID: 834
	private int mHeight;

	// Token: 0x04000343 RID: 835
	private int mAdvance;

	// Token: 0x04000344 RID: 836
	private Rect mUV;
}
