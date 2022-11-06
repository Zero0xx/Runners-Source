using System;
using UnityEngine;

// Token: 0x0200006D RID: 109
[RequireComponent(typeof(UILabel))]
[AddComponentMenu("NGUI/Examples/Typewriter Effect")]
public class TypewriterEffect : MonoBehaviour
{
	// Token: 0x060002F6 RID: 758 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
	private void Update()
	{
		if (this.mLabel == null)
		{
			this.mLabel = base.GetComponent<UILabel>();
			this.mLabel.supportEncoding = false;
			this.mLabel.symbolStyle = UIFont.SymbolStyle.None;
			this.mLabel.font.WrapText(this.mLabel.text, out this.mText, this.mLabel.width, this.mLabel.height, this.mLabel.maxLineCount, false, UIFont.SymbolStyle.None);
		}
		if (this.mOffset < this.mText.Length)
		{
			if (this.mNextChar <= Time.time)
			{
				this.charsPerSecond = Mathf.Max(1, this.charsPerSecond);
				float num = 1f / (float)this.charsPerSecond;
				char c = this.mText[this.mOffset];
				if (c == '.' || c == '\n' || c == '!' || c == '?')
				{
					num *= 4f;
				}
				this.mNextChar = Time.time + num;
				this.mLabel.text = this.mText.Substring(0, ++this.mOffset);
			}
		}
		else
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x040001AC RID: 428
	public int charsPerSecond = 40;

	// Token: 0x040001AD RID: 429
	private UILabel mLabel;

	// Token: 0x040001AE RID: 430
	private string mText;

	// Token: 0x040001AF RID: 431
	private int mOffset;

	// Token: 0x040001B0 RID: 432
	private float mNextChar;
}
