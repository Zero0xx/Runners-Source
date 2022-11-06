using System;
using System.Collections.Generic;

// Token: 0x020000A6 RID: 166
[Serializable]
public class BMGlyph
{
	// Token: 0x06000441 RID: 1089 RVA: 0x00015BA0 File Offset: 0x00013DA0
	public int GetKerning(int previousChar)
	{
		if (this.kerning != null)
		{
			int i = 0;
			int count = this.kerning.Count;
			while (i < count)
			{
				if (this.kerning[i] == previousChar)
				{
					return this.kerning[i + 1];
				}
				i += 2;
			}
		}
		return 0;
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x00015BF8 File Offset: 0x00013DF8
	public void SetKerning(int previousChar, int amount)
	{
		if (this.kerning == null)
		{
			this.kerning = new List<int>();
		}
		for (int i = 0; i < this.kerning.Count; i += 2)
		{
			if (this.kerning[i] == previousChar)
			{
				this.kerning[i + 1] = amount;
				return;
			}
		}
		this.kerning.Add(previousChar);
		this.kerning.Add(amount);
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x00015C74 File Offset: 0x00013E74
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		int num = this.x + this.width;
		int num2 = this.y + this.height;
		if (this.x < xMin)
		{
			int num3 = xMin - this.x;
			this.x += num3;
			this.width -= num3;
			this.offsetX += num3;
		}
		if (this.y < yMin)
		{
			int num4 = yMin - this.y;
			this.y += num4;
			this.height -= num4;
			this.offsetY += num4;
		}
		if (num > xMax)
		{
			this.width -= num - xMax;
		}
		if (num2 > yMax)
		{
			this.height -= num2 - yMax;
		}
	}

	// Token: 0x04000330 RID: 816
	public int index;

	// Token: 0x04000331 RID: 817
	public int x;

	// Token: 0x04000332 RID: 818
	public int y;

	// Token: 0x04000333 RID: 819
	public int width;

	// Token: 0x04000334 RID: 820
	public int height;

	// Token: 0x04000335 RID: 821
	public int offsetX;

	// Token: 0x04000336 RID: 822
	public int offsetY;

	// Token: 0x04000337 RID: 823
	public int advance;

	// Token: 0x04000338 RID: 824
	public int channel;

	// Token: 0x04000339 RID: 825
	public List<int> kerning;
}
