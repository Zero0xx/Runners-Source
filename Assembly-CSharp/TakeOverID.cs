using System;
using UnityEngine;

// Token: 0x020004A3 RID: 1187
public class TakeOverID : MonoBehaviour
{
	// Token: 0x0600232D RID: 9005 RVA: 0x000D3380 File Offset: 0x000D1580
	public void Setup(ui_option_scroll scroll)
	{
		base.enabled = true;
		if (this.m_ui_option_scroll == null && scroll != null)
		{
			this.m_ui_option_scroll = scroll;
		}
		if (this.m_gameObject != null)
		{
			this.m_initFlag = true;
			this.m_gameObject.SetActive(true);
			if (this.m_takeOverId != null)
			{
				this.m_takeOverId.PlayOpenWindow();
			}
		}
		else
		{
			this.m_initFlag = false;
			this.m_gameObject = HudMenuUtility.GetLoadMenuChildObject("window_takeover_id", true);
		}
	}

	// Token: 0x0600232E RID: 9006 RVA: 0x000D3418 File Offset: 0x000D1618
	private void SetTakeOverId()
	{
		if (this.m_gameObject != null && this.m_takeOverId == null)
		{
			this.m_takeOverId = this.m_gameObject.GetComponent<window_takeover_id>();
		}
	}

	// Token: 0x0600232F RID: 9007 RVA: 0x000D3450 File Offset: 0x000D1650
	public void Update()
	{
		if (!this.m_initFlag)
		{
			this.m_initFlag = true;
			this.SetTakeOverId();
			if (this.m_takeOverId != null)
			{
				this.m_takeOverId.PlayOpenWindow();
			}
		}
		else if (this.m_takeOverId != null && this.m_takeOverId.IsEnd)
		{
			if (this.m_ui_option_scroll != null)
			{
				this.m_ui_option_scroll.OnEndChildPage();
			}
			base.enabled = false;
			if (this.m_gameObject != null)
			{
				this.m_gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x04001FD5 RID: 8149
	private window_takeover_id m_takeOverId;

	// Token: 0x04001FD6 RID: 8150
	private GameObject m_gameObject;

	// Token: 0x04001FD7 RID: 8151
	private ui_option_scroll m_ui_option_scroll;

	// Token: 0x04001FD8 RID: 8152
	private bool m_initFlag;
}
