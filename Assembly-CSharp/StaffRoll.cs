using System;
using UnityEngine;

// Token: 0x020004A1 RID: 1185
public class StaffRoll : MonoBehaviour
{
	// Token: 0x06002329 RID: 9001 RVA: 0x000D320C File Offset: 0x000D140C
	public void SetTextType(StaffRoll.TextType type)
	{
		this.m_textType = type;
	}

	// Token: 0x0600232A RID: 9002 RVA: 0x000D3218 File Offset: 0x000D1418
	public void Setup(ui_option_scroll scroll)
	{
		base.enabled = true;
		if (this.m_ui_option_scroll == null && scroll != null)
		{
			this.m_ui_option_scroll = scroll;
		}
		if (this.m_windoObj == null)
		{
			this.m_windoObj = HudMenuUtility.GetLoadMenuChildObject("window_staffroll", false);
		}
		if (this.m_windoObj != null)
		{
			if (this.m_staffRoll == null)
			{
				this.m_staffRoll = this.m_windoObj.GetComponent<window_staffroll>();
			}
			if (this.m_staffRoll != null)
			{
				StaffRoll.TextType textType = this.m_textType;
				if (textType != StaffRoll.TextType.STAFF_ROLL)
				{
					if (textType == StaffRoll.TextType.COPYRIGHT)
					{
						this.m_staffRoll.SetCopyrightText();
					}
				}
				else
				{
					this.m_staffRoll.SetStaffRollText();
				}
				this.m_windoObj.SetActive(true);
				this.m_staffRoll.PlayOpenWindow();
			}
		}
	}

	// Token: 0x0600232B RID: 9003 RVA: 0x000D3308 File Offset: 0x000D1508
	public void Update()
	{
		if (this.m_staffRoll != null && this.m_staffRoll.IsEnd)
		{
			if (this.m_ui_option_scroll != null)
			{
				this.m_ui_option_scroll.OnEndChildPage();
			}
			base.enabled = false;
			if (this.m_windoObj != null)
			{
				this.m_windoObj.SetActive(false);
			}
		}
	}

	// Token: 0x04001FCE RID: 8142
	private window_staffroll m_staffRoll;

	// Token: 0x04001FCF RID: 8143
	private GameObject m_windoObj;

	// Token: 0x04001FD0 RID: 8144
	private ui_option_scroll m_ui_option_scroll;

	// Token: 0x04001FD1 RID: 8145
	private StaffRoll.TextType m_textType;

	// Token: 0x020004A2 RID: 1186
	public enum TextType
	{
		// Token: 0x04001FD3 RID: 8147
		STAFF_ROLL,
		// Token: 0x04001FD4 RID: 8148
		COPYRIGHT
	}
}
