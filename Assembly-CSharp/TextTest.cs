using System;
using Text;
using UnityEngine;

// Token: 0x02000A24 RID: 2596
public class TextTest : MonoBehaviour
{
	// Token: 0x060044C2 RID: 17602 RVA: 0x00162284 File Offset: 0x00160484
	private void Start()
	{
		this.m_label = GameObject.Find("TextTestLabel").GetComponent<UILabel>();
		if (this.m_label == null)
		{
			return;
		}
		string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "mission", this.m_labelName).text;
		this.m_label.text = text;
	}

	// Token: 0x060044C3 RID: 17603 RVA: 0x001622DC File Offset: 0x001604DC
	private void Update()
	{
	}

	// Token: 0x040039B7 RID: 14775
	public string m_labelName = "mission001";

	// Token: 0x040039B8 RID: 14776
	private UILabel m_label;
}
