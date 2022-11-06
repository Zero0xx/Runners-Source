using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A5 RID: 165
[Serializable]
public class BMFont
{
	// Token: 0x17000092 RID: 146
	// (get) Token: 0x06000430 RID: 1072 RVA: 0x000159F0 File Offset: 0x00013BF0
	public bool isValid
	{
		get
		{
			return this.mSaved.Count > 0;
		}
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x06000431 RID: 1073 RVA: 0x00015A00 File Offset: 0x00013C00
	// (set) Token: 0x06000432 RID: 1074 RVA: 0x00015A08 File Offset: 0x00013C08
	public int charSize
	{
		get
		{
			return this.mSize;
		}
		set
		{
			this.mSize = value;
		}
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x06000433 RID: 1075 RVA: 0x00015A14 File Offset: 0x00013C14
	// (set) Token: 0x06000434 RID: 1076 RVA: 0x00015A1C File Offset: 0x00013C1C
	public int baseOffset
	{
		get
		{
			return this.mBase;
		}
		set
		{
			this.mBase = value;
		}
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x06000435 RID: 1077 RVA: 0x00015A28 File Offset: 0x00013C28
	// (set) Token: 0x06000436 RID: 1078 RVA: 0x00015A30 File Offset: 0x00013C30
	public int texWidth
	{
		get
		{
			return this.mWidth;
		}
		set
		{
			this.mWidth = value;
		}
	}

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x06000437 RID: 1079 RVA: 0x00015A3C File Offset: 0x00013C3C
	// (set) Token: 0x06000438 RID: 1080 RVA: 0x00015A44 File Offset: 0x00013C44
	public int texHeight
	{
		get
		{
			return this.mHeight;
		}
		set
		{
			this.mHeight = value;
		}
	}

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x06000439 RID: 1081 RVA: 0x00015A50 File Offset: 0x00013C50
	public int glyphCount
	{
		get
		{
			return (!this.isValid) ? 0 : this.mSaved.Count;
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x0600043A RID: 1082 RVA: 0x00015A70 File Offset: 0x00013C70
	// (set) Token: 0x0600043B RID: 1083 RVA: 0x00015A78 File Offset: 0x00013C78
	public string spriteName
	{
		get
		{
			return this.mSpriteName;
		}
		set
		{
			this.mSpriteName = value;
		}
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x00015A84 File Offset: 0x00013C84
	public BMGlyph GetGlyph(int index, bool createIfMissing)
	{
		BMGlyph bmglyph = null;
		if (this.mDict.Count == 0)
		{
			int i = 0;
			int count = this.mSaved.Count;
			while (i < count)
			{
				BMGlyph bmglyph2 = this.mSaved[i];
				this.mDict.Add(bmglyph2.index, bmglyph2);
				i++;
			}
		}
		if (!this.mDict.TryGetValue(index, out bmglyph) && createIfMissing)
		{
			bmglyph = new BMGlyph();
			bmglyph.index = index;
			this.mSaved.Add(bmglyph);
			this.mDict.Add(index, bmglyph);
		}
		return bmglyph;
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x00015B20 File Offset: 0x00013D20
	public BMGlyph GetGlyph(int index)
	{
		return this.GetGlyph(index, false);
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x00015B2C File Offset: 0x00013D2C
	public void Clear()
	{
		this.mDict.Clear();
		this.mSaved.Clear();
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x00015B44 File Offset: 0x00013D44
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		if (this.isValid)
		{
			int i = 0;
			int count = this.mSaved.Count;
			while (i < count)
			{
				BMGlyph bmglyph = this.mSaved[i];
				if (bmglyph != null)
				{
					bmglyph.Trim(xMin, yMin, xMax, yMax);
				}
				i++;
			}
		}
	}

	// Token: 0x04000329 RID: 809
	[HideInInspector]
	[SerializeField]
	private int mSize;

	// Token: 0x0400032A RID: 810
	[SerializeField]
	[HideInInspector]
	private int mBase;

	// Token: 0x0400032B RID: 811
	[HideInInspector]
	[SerializeField]
	private int mWidth;

	// Token: 0x0400032C RID: 812
	[SerializeField]
	[HideInInspector]
	private int mHeight;

	// Token: 0x0400032D RID: 813
	[HideInInspector]
	[SerializeField]
	private string mSpriteName;

	// Token: 0x0400032E RID: 814
	[HideInInspector]
	[SerializeField]
	private List<BMGlyph> mSaved = new List<BMGlyph>();

	// Token: 0x0400032F RID: 815
	private Dictionary<int, BMGlyph> mDict = new Dictionary<int, BMGlyph>();
}
