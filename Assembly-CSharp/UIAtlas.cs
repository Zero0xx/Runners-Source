using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CA RID: 202
[AddComponentMenu("NGUI/UI/Atlas")]
public class UIAtlas : MonoBehaviour
{
	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0001D5C0 File Offset: 0x0001B7C0
	// (set) Token: 0x060005C5 RID: 1477 RVA: 0x0001D5EC File Offset: 0x0001B7EC
	public Material spriteMaterial
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.material : this.mReplacement.spriteMaterial;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.spriteMaterial = value;
			}
			else if (this.material == null)
			{
				this.mPMA = 0;
				this.material = value;
			}
			else
			{
				this.MarkAsDirty();
				this.mPMA = -1;
				this.material = value;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0001D65C File Offset: 0x0001B85C
	public bool premultipliedAlpha
	{
		get
		{
			if (this.mReplacement != null)
			{
				return this.mReplacement.premultipliedAlpha;
			}
			if (this.mPMA == -1)
			{
				Material spriteMaterial = this.spriteMaterial;
				this.mPMA = ((!(spriteMaterial != null) || !(spriteMaterial.shader != null) || !spriteMaterial.shader.name.Contains("Premultiplied")) ? 0 : 1);
			}
			return this.mPMA == 1;
		}
	}

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
	// (set) Token: 0x060005C8 RID: 1480 RVA: 0x0001D734 File Offset: 0x0001B934
	public List<UISpriteData> spriteList
	{
		get
		{
			if (this.mSprites.Count == 0)
			{
				this.Upgrade();
			}
			return (!(this.mReplacement != null)) ? this.mSprites : this.mReplacement.spriteList;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.spriteList = value;
			}
			else
			{
				this.mSprites = value;
			}
		}
	}

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0001D760 File Offset: 0x0001B960
	public Texture texture
	{
		get
		{
			return (!(this.mReplacement != null)) ? ((!(this.material != null)) ? null : this.material.mainTexture) : this.mReplacement.texture;
		}
	}

	// Token: 0x170000ED RID: 237
	// (get) Token: 0x060005CA RID: 1482 RVA: 0x0001D7B0 File Offset: 0x0001B9B0
	// (set) Token: 0x060005CB RID: 1483 RVA: 0x0001D7DC File Offset: 0x0001B9DC
	public float pixelSize
	{
		get
		{
			return (!(this.mReplacement != null)) ? this.mPixelSize : this.mReplacement.pixelSize;
		}
		set
		{
			if (this.mReplacement != null)
			{
				this.mReplacement.pixelSize = value;
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

	// Token: 0x170000EE RID: 238
	// (get) Token: 0x060005CC RID: 1484 RVA: 0x0001D838 File Offset: 0x0001BA38
	// (set) Token: 0x060005CD RID: 1485 RVA: 0x0001D840 File Offset: 0x0001BA40
	public UIAtlas replacement
	{
		get
		{
			return this.mReplacement;
		}
		set
		{
			UIAtlas uiatlas = value;
			if (uiatlas == this)
			{
				uiatlas = null;
			}
			if (this.mReplacement != uiatlas)
			{
				if (uiatlas != null && uiatlas.replacement == this)
				{
					uiatlas.replacement = null;
				}
				if (this.mReplacement != null)
				{
					this.MarkAsDirty();
				}
				this.mReplacement = uiatlas;
				this.MarkAsDirty();
			}
		}
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x0001D8B8 File Offset: 0x0001BAB8
	public UISpriteData GetSprite(string name)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.GetSprite(name);
		}
		if (!string.IsNullOrEmpty(name))
		{
			if (this.mSprites.Count == 0)
			{
				this.Upgrade();
			}
			int i = 0;
			int count = this.mSprites.Count;
			while (i < count)
			{
				UISpriteData uispriteData = this.mSprites[i];
				if (!string.IsNullOrEmpty(uispriteData.name) && name == uispriteData.name)
				{
					return uispriteData;
				}
				i++;
			}
		}
		return null;
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x0001D954 File Offset: 0x0001BB54
	public void SortAlphabetically()
	{
		this.mSprites.Sort((UISpriteData s1, UISpriteData s2) => s1.name.CompareTo(s2.name));
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x0001D98C File Offset: 0x0001BB8C
	public BetterList<string> GetListOfSprites()
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.GetListOfSprites();
		}
		if (this.mSprites.Count == 0)
		{
			this.Upgrade();
		}
		BetterList<string> betterList = new BetterList<string>();
		int i = 0;
		int count = this.mSprites.Count;
		while (i < count)
		{
			UISpriteData uispriteData = this.mSprites[i];
			if (uispriteData != null && !string.IsNullOrEmpty(uispriteData.name))
			{
				betterList.Add(uispriteData.name);
			}
			i++;
		}
		return betterList;
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x0001DA24 File Offset: 0x0001BC24
	public BetterList<string> GetListOfSprites(string match)
	{
		if (this.mReplacement != null)
		{
			return this.mReplacement.GetListOfSprites(match);
		}
		if (string.IsNullOrEmpty(match))
		{
			return this.GetListOfSprites();
		}
		if (this.mSprites.Count == 0)
		{
			this.Upgrade();
		}
		BetterList<string> betterList = new BetterList<string>();
		int i = 0;
		int count = this.mSprites.Count;
		while (i < count)
		{
			UISpriteData uispriteData = this.mSprites[i];
			if (uispriteData != null && !string.IsNullOrEmpty(uispriteData.name) && string.Equals(match, uispriteData.name, StringComparison.OrdinalIgnoreCase))
			{
				betterList.Add(uispriteData.name);
				return betterList;
			}
			i++;
		}
		string[] array = match.Split(new char[]
		{
			' '
		}, StringSplitOptions.RemoveEmptyEntries);
		for (int j = 0; j < array.Length; j++)
		{
			array[j] = array[j].ToLower();
		}
		int k = 0;
		int count2 = this.mSprites.Count;
		while (k < count2)
		{
			UISpriteData uispriteData2 = this.mSprites[k];
			if (uispriteData2 != null && !string.IsNullOrEmpty(uispriteData2.name))
			{
				string text = uispriteData2.name.ToLower();
				int num = 0;
				for (int l = 0; l < array.Length; l++)
				{
					if (text.Contains(array[l]))
					{
						num++;
					}
				}
				if (num == array.Length)
				{
					betterList.Add(uispriteData2.name);
				}
			}
			k++;
		}
		return betterList;
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x0001DBC0 File Offset: 0x0001BDC0
	private bool References(UIAtlas atlas)
	{
		return !(atlas == null) && (atlas == this || (this.mReplacement != null && this.mReplacement.References(atlas)));
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x0001DC0C File Offset: 0x0001BE0C
	public static bool CheckIfRelated(UIAtlas a, UIAtlas b)
	{
		return !(a == null) && !(b == null) && (a == b || a.References(b) || b.References(a));
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x0001DC58 File Offset: 0x0001BE58
	public void MarkAsDirty()
	{
		if (this.mReplacement != null)
		{
			this.mReplacement.MarkAsDirty();
		}
		UISprite[] array = NGUITools.FindActive<UISprite>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			UISprite uisprite = array[i];
			if (UIAtlas.CheckIfRelated(this, uisprite.atlas))
			{
				UIAtlas atlas = uisprite.atlas;
				uisprite.atlas = null;
				uisprite.atlas = atlas;
			}
			i++;
		}
		UIFont[] array2 = Resources.FindObjectsOfTypeAll(typeof(UIFont)) as UIFont[];
		int j = 0;
		int num2 = array2.Length;
		while (j < num2)
		{
			UIFont uifont = array2[j];
			if (UIAtlas.CheckIfRelated(this, uifont.atlas))
			{
				UIAtlas atlas2 = uifont.atlas;
				uifont.atlas = null;
				uifont.atlas = atlas2;
			}
			j++;
		}
		UILabel[] array3 = NGUITools.FindActive<UILabel>();
		int k = 0;
		int num3 = array3.Length;
		while (k < num3)
		{
			UILabel uilabel = array3[k];
			if (uilabel.font != null && UIAtlas.CheckIfRelated(this, uilabel.font.atlas))
			{
				UIFont font = uilabel.font;
				uilabel.font = null;
				uilabel.font = font;
			}
			k++;
		}
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x0001DDA0 File Offset: 0x0001BFA0
	private bool Upgrade()
	{
		if (this.mSprites.Count == 0 && this.sprites.Count > 0)
		{
			Texture mainTexture = this.material.mainTexture;
			int width = (!(mainTexture != null)) ? 512 : mainTexture.width;
			int height = (!(mainTexture != null)) ? 512 : mainTexture.height;
			for (int i = 0; i < this.sprites.Count; i++)
			{
				UIAtlas.Sprite sprite = this.sprites[i];
				Rect outer = sprite.outer;
				Rect inner = sprite.inner;
				if (this.mCoordinates == UIAtlas.Coordinates.TexCoords)
				{
					NGUIMath.ConvertToPixels(outer, width, height, true);
					NGUIMath.ConvertToPixels(inner, width, height, true);
				}
				UISpriteData uispriteData = new UISpriteData();
				uispriteData.name = sprite.name;
				uispriteData.x = Mathf.RoundToInt(outer.xMin);
				uispriteData.y = Mathf.RoundToInt(outer.yMin);
				uispriteData.width = Mathf.RoundToInt(outer.width);
				uispriteData.height = Mathf.RoundToInt(outer.height);
				uispriteData.paddingLeft = Mathf.RoundToInt(sprite.paddingLeft * outer.width);
				uispriteData.paddingRight = Mathf.RoundToInt(sprite.paddingRight * outer.width);
				uispriteData.paddingBottom = Mathf.RoundToInt(sprite.paddingBottom * outer.height);
				uispriteData.paddingTop = Mathf.RoundToInt(sprite.paddingTop * outer.height);
				uispriteData.borderLeft = Mathf.RoundToInt(inner.xMin - outer.xMin);
				uispriteData.borderRight = Mathf.RoundToInt(outer.xMax - inner.xMax);
				uispriteData.borderBottom = Mathf.RoundToInt(outer.yMax - inner.yMax);
				uispriteData.borderTop = Mathf.RoundToInt(inner.yMin - outer.yMin);
				this.mSprites.Add(uispriteData);
			}
			this.sprites.Clear();
			return true;
		}
		return false;
	}

	// Token: 0x0400041E RID: 1054
	[SerializeField]
	[HideInInspector]
	private Material material;

	// Token: 0x0400041F RID: 1055
	[HideInInspector]
	[SerializeField]
	private List<UISpriteData> mSprites = new List<UISpriteData>();

	// Token: 0x04000420 RID: 1056
	[HideInInspector]
	[SerializeField]
	private float mPixelSize = 1f;

	// Token: 0x04000421 RID: 1057
	[HideInInspector]
	[SerializeField]
	private UIAtlas mReplacement;

	// Token: 0x04000422 RID: 1058
	[HideInInspector]
	[SerializeField]
	private UIAtlas.Coordinates mCoordinates;

	// Token: 0x04000423 RID: 1059
	[SerializeField]
	[HideInInspector]
	private List<UIAtlas.Sprite> sprites = new List<UIAtlas.Sprite>();

	// Token: 0x04000424 RID: 1060
	private int mPMA = -1;

	// Token: 0x020000CB RID: 203
	[Serializable]
	public class Sprite
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0001E038 File Offset: 0x0001C238
		public bool hasPadding
		{
			get
			{
				return this.paddingLeft != 0f || this.paddingRight != 0f || this.paddingTop != 0f || this.paddingBottom != 0f;
			}
		}

		// Token: 0x04000426 RID: 1062
		public string name = "Unity Bug";

		// Token: 0x04000427 RID: 1063
		public Rect outer = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04000428 RID: 1064
		public Rect inner = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04000429 RID: 1065
		public bool rotated;

		// Token: 0x0400042A RID: 1066
		public float paddingLeft;

		// Token: 0x0400042B RID: 1067
		public float paddingRight;

		// Token: 0x0400042C RID: 1068
		public float paddingTop;

		// Token: 0x0400042D RID: 1069
		public float paddingBottom;
	}

	// Token: 0x020000CC RID: 204
	private enum Coordinates
	{
		// Token: 0x0400042F RID: 1071
		Pixels,
		// Token: 0x04000430 RID: 1072
		TexCoords
	}
}
