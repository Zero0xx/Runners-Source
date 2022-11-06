using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020000E8 RID: 232
[AddComponentMenu("NGUI/UI/Text List")]
public class UITextList : MonoBehaviour
{
	// Token: 0x060006F5 RID: 1781 RVA: 0x000287B4 File Offset: 0x000269B4
	public void Clear()
	{
		this.mParagraphs.Clear();
		this.UpdateVisibleText();
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x000287C8 File Offset: 0x000269C8
	public void Add(string text)
	{
		this.Add(text, true);
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x000287D4 File Offset: 0x000269D4
	protected void Add(string text, bool updateVisible)
	{
		UITextList.Paragraph paragraph;
		if (this.mParagraphs.Count < this.maxEntries)
		{
			paragraph = new UITextList.Paragraph();
		}
		else
		{
			paragraph = this.mParagraphs[0];
			this.mParagraphs.RemoveAt(0);
		}
		paragraph.text = text;
		this.mParagraphs.Add(paragraph);
		if (this.textLabel != null && this.textLabel.font != null)
		{
			string text2;
			this.textLabel.font.WrapText(paragraph.text, out text2, this.textLabel.width, 100000, 0, this.textLabel.supportEncoding, this.textLabel.symbolStyle);
			paragraph.lines = text2.Split(this.mSeparator);
			this.mTotalLines = 0;
			int i = 0;
			int count = this.mParagraphs.Count;
			while (i < count)
			{
				this.mTotalLines += this.mParagraphs[i].lines.Length;
				i++;
			}
		}
		if (updateVisible)
		{
			this.UpdateVisibleText();
		}
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x000288FC File Offset: 0x00026AFC
	private void Awake()
	{
		if (this.textLabel == null)
		{
			this.textLabel = base.GetComponentInChildren<UILabel>();
		}
		Collider collider = base.collider;
		if (collider != null && this.maxHeight <= 0f)
		{
			this.maxHeight = collider.bounds.size.y / base.transform.lossyScale.y;
		}
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x0002897C File Offset: 0x00026B7C
	private void OnSelect(bool selected)
	{
		this.mSelected = selected;
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x00028988 File Offset: 0x00026B88
	protected void UpdateVisibleText()
	{
		if (this.textLabel != null)
		{
			UIFont font = this.textLabel.font;
			if (font != null)
			{
				int num = 0;
				int num2 = (this.maxHeight <= 0f) ? 100000 : Mathf.FloorToInt(this.maxHeight / ((float)this.textLabel.font.size * this.textLabel.font.pixelSize));
				int num3 = Mathf.RoundToInt(this.mScroll);
				if (num2 + num3 > this.mTotalLines)
				{
					num3 = Mathf.Max(0, this.mTotalLines - num2);
					this.mScroll = (float)num3;
				}
				if (this.style == UITextList.Style.Chat)
				{
					num3 = Mathf.Max(0, this.mTotalLines - num2 - num3);
				}
				StringBuilder stringBuilder = new StringBuilder();
				int i = 0;
				int count = this.mParagraphs.Count;
				while (i < count)
				{
					UITextList.Paragraph paragraph = this.mParagraphs[i];
					int j = 0;
					int num4 = paragraph.lines.Length;
					while (j < num4)
					{
						string value = paragraph.lines[j];
						if (num3 > 0)
						{
							num3--;
						}
						else
						{
							if (stringBuilder.Length > 0)
							{
								stringBuilder.Append("\n");
							}
							stringBuilder.Append(value);
							num++;
							if (num >= num2)
							{
								break;
							}
						}
						j++;
					}
					if (num >= num2)
					{
						break;
					}
					i++;
				}
				this.textLabel.text = stringBuilder.ToString();
			}
		}
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x00028B24 File Offset: 0x00026D24
	private void OnScroll(float val)
	{
		if (this.mSelected && this.supportScrollWheel)
		{
			val *= ((this.style != UITextList.Style.Chat) ? -10f : 10f);
			this.mScroll = Mathf.Max(0f, this.mScroll + val);
			this.UpdateVisibleText();
		}
	}

	// Token: 0x04000549 RID: 1353
	public UITextList.Style style;

	// Token: 0x0400054A RID: 1354
	public UILabel textLabel;

	// Token: 0x0400054B RID: 1355
	public float maxHeight;

	// Token: 0x0400054C RID: 1356
	public int maxEntries = 50;

	// Token: 0x0400054D RID: 1357
	public bool supportScrollWheel = true;

	// Token: 0x0400054E RID: 1358
	protected char[] mSeparator = new char[]
	{
		'\n'
	};

	// Token: 0x0400054F RID: 1359
	protected List<UITextList.Paragraph> mParagraphs = new List<UITextList.Paragraph>();

	// Token: 0x04000550 RID: 1360
	protected float mScroll;

	// Token: 0x04000551 RID: 1361
	protected bool mSelected;

	// Token: 0x04000552 RID: 1362
	protected int mTotalLines;

	// Token: 0x020000E9 RID: 233
	public enum Style
	{
		// Token: 0x04000554 RID: 1364
		Text,
		// Token: 0x04000555 RID: 1365
		Chat
	}

	// Token: 0x020000EA RID: 234
	protected class Paragraph
	{
		// Token: 0x04000556 RID: 1366
		public string text;

		// Token: 0x04000557 RID: 1367
		public string[] lines;
	}
}
