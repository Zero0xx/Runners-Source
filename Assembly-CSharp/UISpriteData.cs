using System;

// Token: 0x020000E5 RID: 229
[Serializable]
public class UISpriteData
{
	// Token: 0x17000144 RID: 324
	// (get) Token: 0x060006E9 RID: 1769 RVA: 0x00027D00 File Offset: 0x00025F00
	public bool hasBorder
	{
		get
		{
			return (this.borderLeft | this.borderRight | this.borderTop | this.borderBottom) != 0;
		}
	}

	// Token: 0x17000145 RID: 325
	// (get) Token: 0x060006EA RID: 1770 RVA: 0x00027D24 File Offset: 0x00025F24
	public bool hasPadding
	{
		get
		{
			return (this.paddingLeft | this.paddingRight | this.paddingTop | this.paddingBottom) != 0;
		}
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x00027D48 File Offset: 0x00025F48
	public void SetRect(int x, int y, int width, int height)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x00027D68 File Offset: 0x00025F68
	public void SetPadding(int left, int bottom, int right, int top)
	{
		this.paddingLeft = left;
		this.paddingBottom = bottom;
		this.paddingRight = right;
		this.paddingTop = top;
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x00027D88 File Offset: 0x00025F88
	public void SetBorder(int left, int bottom, int right, int top)
	{
		this.borderLeft = left;
		this.borderBottom = bottom;
		this.borderRight = right;
		this.borderTop = top;
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x00027DA8 File Offset: 0x00025FA8
	public void CopyFrom(UISpriteData sd)
	{
		this.name = sd.name;
		this.x = sd.x;
		this.y = sd.y;
		this.width = sd.width;
		this.height = sd.height;
		this.borderLeft = sd.borderLeft;
		this.borderRight = sd.borderRight;
		this.borderTop = sd.borderTop;
		this.borderBottom = sd.borderBottom;
		this.paddingLeft = sd.paddingLeft;
		this.paddingRight = sd.paddingRight;
		this.paddingTop = sd.paddingTop;
		this.paddingBottom = sd.paddingBottom;
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x00027E54 File Offset: 0x00026054
	public void CopyBorderFrom(UISpriteData sd)
	{
		this.borderLeft = sd.borderLeft;
		this.borderRight = sd.borderRight;
		this.borderTop = sd.borderTop;
		this.borderBottom = sd.borderBottom;
	}

	// Token: 0x04000525 RID: 1317
	public string name = "Sprite";

	// Token: 0x04000526 RID: 1318
	public int x;

	// Token: 0x04000527 RID: 1319
	public int y;

	// Token: 0x04000528 RID: 1320
	public int width;

	// Token: 0x04000529 RID: 1321
	public int height;

	// Token: 0x0400052A RID: 1322
	public int borderLeft;

	// Token: 0x0400052B RID: 1323
	public int borderRight;

	// Token: 0x0400052C RID: 1324
	public int borderTop;

	// Token: 0x0400052D RID: 1325
	public int borderBottom;

	// Token: 0x0400052E RID: 1326
	public int paddingLeft;

	// Token: 0x0400052F RID: 1327
	public int paddingRight;

	// Token: 0x04000530 RID: 1328
	public int paddingTop;

	// Token: 0x04000531 RID: 1329
	public int paddingBottom;
}
