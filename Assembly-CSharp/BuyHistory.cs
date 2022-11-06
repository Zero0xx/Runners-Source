using System;
using UnityEngine;

// Token: 0x02000490 RID: 1168
public class BuyHistory : MonoBehaviour
{
	// Token: 0x060022E6 RID: 8934 RVA: 0x000D17B0 File Offset: 0x000CF9B0
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
			if (this.m_history != null)
			{
				this.m_history.PlayOpenWindow();
			}
		}
		else
		{
			this.m_initFlag = false;
			this.m_gameObject = HudMenuUtility.GetLoadMenuChildObject("window_buying_history", true);
		}
	}

	// Token: 0x060022E7 RID: 8935 RVA: 0x000D1848 File Offset: 0x000CFA48
	private void SetBuyingHistory()
	{
		if (this.m_gameObject != null && this.m_history == null)
		{
			this.m_history = this.m_gameObject.GetComponent<window_buying_history>();
		}
	}

	// Token: 0x060022E8 RID: 8936 RVA: 0x000D1880 File Offset: 0x000CFA80
	public void Update()
	{
		if (!this.m_initFlag)
		{
			this.m_initFlag = true;
			this.SetBuyingHistory();
			if (this.m_history != null)
			{
				this.m_history.PlayOpenWindow();
			}
		}
		else if (this.m_history != null && this.m_history.IsEnd)
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

	// Token: 0x04001F86 RID: 8070
	private window_buying_history m_history;

	// Token: 0x04001F87 RID: 8071
	private GameObject m_gameObject;

	// Token: 0x04001F88 RID: 8072
	private ui_option_scroll m_ui_option_scroll;

	// Token: 0x04001F89 RID: 8073
	private bool m_initFlag;
}
