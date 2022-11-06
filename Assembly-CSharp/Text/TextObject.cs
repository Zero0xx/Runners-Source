using System;

namespace Text
{
	// Token: 0x02000A28 RID: 2600
	public class TextObject
	{
		// Token: 0x060044D9 RID: 17625 RVA: 0x001627C8 File Offset: 0x001609C8
		public TextObject(string text)
		{
			this.m_text = text;
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x060044DA RID: 17626 RVA: 0x001627E4 File Offset: 0x001609E4
		// (set) Token: 0x060044DB RID: 17627 RVA: 0x001627EC File Offset: 0x001609EC
		public string text
		{
			get
			{
				return this.m_text;
			}
			set
			{
				this.m_text = value;
			}
		}

		// Token: 0x060044DC RID: 17628 RVA: 0x001627F8 File Offset: 0x001609F8
		public void ReplaceTag(string tagString, string replaceString)
		{
			if (tagString == null)
			{
				return;
			}
			if (replaceString == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(this.m_text))
			{
				return;
			}
			this.m_text = this.m_text.Replace(tagString, replaceString);
		}

		// Token: 0x040039BF RID: 14783
		private string m_text = string.Empty;
	}
}
