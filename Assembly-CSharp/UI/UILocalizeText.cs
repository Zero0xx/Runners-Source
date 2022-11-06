using System;
using Text;
using UnityEngine;

namespace UI
{
	// Token: 0x0200056D RID: 1389
	[AddComponentMenu("Scripts/UI/LocalizeText")]
	public class UILocalizeText : MonoBehaviour
	{
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06002AC1 RID: 10945 RVA: 0x0010882C File Offset: 0x00106A2C
		public string MainText
		{
			get
			{
				return this.m_main_text;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06002AC2 RID: 10946 RVA: 0x00108834 File Offset: 0x00106A34
		// (set) Token: 0x06002AC3 RID: 10947 RVA: 0x0010883C File Offset: 0x00106A3C
		public UILocalizeText.TextData MainTextData
		{
			get
			{
				return this.m_main_text_data;
			}
			set
			{
				this.m_main_text_data = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06002AC4 RID: 10948 RVA: 0x00108848 File Offset: 0x00106A48
		// (set) Token: 0x06002AC5 RID: 10949 RVA: 0x00108850 File Offset: 0x00106A50
		public UILocalizeText.TagTextData[] TagTextDatas
		{
			get
			{
				return this.m_tag_text_data;
			}
			set
			{
				this.m_tag_text_data = value;
			}
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x0010885C File Offset: 0x00106A5C
		private void Start()
		{
			base.enabled = false;
			this.SetUILabelText();
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x0010886C File Offset: 0x00106A6C
		public void SetUILabelText()
		{
			this.m_main_text = this.GetMainText();
			if (this.m_main_text != null)
			{
				UILabel component = base.gameObject.GetComponent<UILabel>();
				if (component != null)
				{
					component.text = this.m_main_text;
				}
			}
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x001088B4 File Offset: 0x00106AB4
		private string GetMainText()
		{
			if (this.m_main_text_data != null && this.m_main_text_data.group_id != null && this.m_main_text_data.cell_id != null && this.m_main_text_data.group_id != string.Empty && this.m_main_text_data.cell_id != string.Empty)
			{
				TextObject text = TextManager.GetText(this.m_main_text_data.text_type, this.m_main_text_data.group_id, this.m_main_text_data.cell_id);
				if (text != null && text.text != null)
				{
					this.ReplaceTag(ref text);
					return text.text;
				}
			}
			return null;
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x00108968 File Offset: 0x00106B68
		private string GetText(ref UILocalizeText.TextData text_data)
		{
			if (text_data != null && text_data.group_id != null && text_data.cell_id != null && text_data.group_id != string.Empty && text_data.cell_id != string.Empty)
			{
				TextObject text = TextManager.GetText(text_data.text_type, text_data.group_id, text_data.cell_id);
				if (text != null)
				{
					return text.text;
				}
			}
			return null;
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x001089EC File Offset: 0x00106BEC
		private void ReplaceTag(ref TextObject text_obj)
		{
			if (this.m_tag_text_data != null && text_obj != null)
			{
				int num = this.m_tag_text_data.Length;
				for (int i = 0; i < num; i++)
				{
					if (this.m_tag_text_data[i] != null)
					{
						string text = this.GetText(ref this.m_tag_text_data[i].text_data);
						if (text != null)
						{
							text_obj.ReplaceTag(this.m_tag_text_data[i].tag, text);
						}
					}
				}
			}
		}

		// Token: 0x04002630 RID: 9776
		[SerializeField]
		public UILocalizeText.TextData m_main_text_data = new UILocalizeText.TextData();

		// Token: 0x04002631 RID: 9777
		[SerializeField]
		public UILocalizeText.TagTextData[] m_tag_text_data = new UILocalizeText.TagTextData[3];

		// Token: 0x04002632 RID: 9778
		private string m_main_text;

		// Token: 0x0200056E RID: 1390
		private enum TagType
		{
			// Token: 0x04002634 RID: 9780
			TAG_01,
			// Token: 0x04002635 RID: 9781
			TAG_02,
			// Token: 0x04002636 RID: 9782
			TAG_03,
			// Token: 0x04002637 RID: 9783
			NUM
		}

		// Token: 0x0200056F RID: 1391
		[Serializable]
		public class TextData
		{
			// Token: 0x04002638 RID: 9784
			[SerializeField]
			public TextManager.TextType text_type;

			// Token: 0x04002639 RID: 9785
			[SerializeField]
			public string group_id;

			// Token: 0x0400263A RID: 9786
			[SerializeField]
			public string cell_id;
		}

		// Token: 0x02000570 RID: 1392
		[Serializable]
		public class TagTextData
		{
			// Token: 0x0400263B RID: 9787
			[SerializeField]
			public UILocalizeText.TextData text_data;

			// Token: 0x0400263C RID: 9788
			[SerializeField]
			public string tag;
		}
	}
}
