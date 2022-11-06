using System;
using Text;
using UnityEngine;

// Token: 0x0200044F RID: 1103
public class HudHeaderPageName : MonoBehaviour
{
	// Token: 0x06002143 RID: 8515 RVA: 0x000C8454 File Offset: 0x000C6654
	public void ChangeHeaderText(string headerText)
	{
		if (headerText == null)
		{
			return;
		}
		if (this.m_header_label == null)
		{
			return;
		}
		if (this.m_header_label_sdw == null)
		{
			return;
		}
		this.m_header_label.text = headerText;
		this.m_header_label_sdw.text = headerText;
	}

	// Token: 0x06002144 RID: 8516 RVA: 0x000C84A4 File Offset: 0x000C66A4
	public void OnUpdateSaveDataDisplay()
	{
		this.Initialize();
	}

	// Token: 0x06002145 RID: 8517 RVA: 0x000C84AC File Offset: 0x000C66AC
	public void Initialize()
	{
		if (this.m_initEnd)
		{
			return;
		}
		GameObject mainMenuCmnUIObject = HudMenuUtility.GetMainMenuCmnUIObject();
		if (mainMenuCmnUIObject != null)
		{
			GameObject gameObject = mainMenuCmnUIObject.transform.FindChild("Anchor_1_TL/mainmenu_info_user/img_header/Lbl_header").gameObject;
			if (gameObject != null)
			{
				this.m_header_label = gameObject.GetComponent<UILabel>();
				GameObject gameObject2 = gameObject.transform.FindChild("Lbl_header_sdw").gameObject;
				if (gameObject2 != null)
				{
					this.m_header_label_sdw = gameObject2.GetComponent<UILabel>();
				}
			}
		}
		this.m_initEnd = true;
	}

	// Token: 0x06002146 RID: 8518 RVA: 0x000C853C File Offset: 0x000C673C
	private void Start()
	{
	}

	// Token: 0x06002147 RID: 8519 RVA: 0x000C8540 File Offset: 0x000C6740
	private void Update()
	{
	}

	// Token: 0x06002148 RID: 8520 RVA: 0x000C8544 File Offset: 0x000C6744
	public static string CalcHeaderTextByCellName(string cellName)
	{
		if (cellName == null)
		{
			return null;
		}
		string result = string.Empty;
		TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", cellName);
		if (text != null)
		{
			result = text.text;
		}
		return result;
	}

	// Token: 0x04001E11 RID: 7697
	private UILabel m_header_label;

	// Token: 0x04001E12 RID: 7698
	private UILabel m_header_label_sdw;

	// Token: 0x04001E13 RID: 7699
	private bool m_initEnd;
}
