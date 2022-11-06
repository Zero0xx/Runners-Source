using System;
using DataTable;
using UnityEngine;

// Token: 0x0200049E RID: 1182
public class OptionWebJump : MonoBehaviour
{
	// Token: 0x06002320 RID: 8992 RVA: 0x000D2FA8 File Offset: 0x000D11A8
	private void Start()
	{
	}

	// Token: 0x06002321 RID: 8993 RVA: 0x000D2FAC File Offset: 0x000D11AC
	public void Setup(ui_option_scroll scroll, OptionWebJump.WebType type)
	{
		base.enabled = true;
		this.m_webType = type;
		if (this.m_ui_option_scroll == null && scroll != null)
		{
			this.m_ui_option_scroll = scroll;
		}
		this.m_setFlag = true;
	}

	// Token: 0x06002322 RID: 8994 RVA: 0x000D2FE8 File Offset: 0x000D11E8
	public void Update()
	{
		if (this.m_setFlag)
		{
			string urlText = this.GetUrlText();
			if (urlText != null)
			{
				Application.OpenURL(urlText);
			}
			base.enabled = false;
			this.m_setFlag = false;
			if (this.m_ui_option_scroll != null)
			{
				this.m_ui_option_scroll.OnEndChildPage();
			}
		}
	}

	// Token: 0x06002323 RID: 8995 RVA: 0x000D3040 File Offset: 0x000D1240
	private string GetUrlText()
	{
		if (this.m_webType == OptionWebJump.WebType.HELP)
		{
			return InformationDataTable.GetUrl(InformationDataTable.Type.HELP);
		}
		if (this.m_webType == OptionWebJump.WebType.TERMS_OF_SERVICE)
		{
			return NetBaseUtil.RedirectTrmsOfServicePageUrl;
		}
		return null;
	}

	// Token: 0x04001FC3 RID: 8131
	private ui_option_scroll m_ui_option_scroll;

	// Token: 0x04001FC4 RID: 8132
	private OptionWebJump.WebType m_webType;

	// Token: 0x04001FC5 RID: 8133
	private bool m_setFlag;

	// Token: 0x0200049F RID: 1183
	public enum WebType
	{
		// Token: 0x04001FC7 RID: 8135
		HELP,
		// Token: 0x04001FC8 RID: 8136
		TERMS_OF_SERVICE,
		// Token: 0x04001FC9 RID: 8137
		NUM
	}
}
