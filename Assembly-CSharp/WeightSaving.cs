using System;
using UnityEngine;

// Token: 0x020004A4 RID: 1188
public class WeightSaving : MonoBehaviour
{
	// Token: 0x06002331 RID: 9009 RVA: 0x000D3500 File Offset: 0x000D1700
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
			if (this.m_performanceSetting != null)
			{
				this.m_performanceSetting.Setup();
				this.m_performanceSetting.PlayOpenWindow();
			}
		}
		else
		{
			this.m_initFlag = false;
			this.m_gameObject = HudMenuUtility.GetLoadMenuChildObject("window_performance_setting", true);
		}
	}

	// Token: 0x06002332 RID: 9010 RVA: 0x000D35A0 File Offset: 0x000D17A0
	private void SetEventSetting()
	{
		if (this.m_gameObject != null && this.m_performanceSetting == null)
		{
			this.m_performanceSetting = this.m_gameObject.GetComponent<window_performance_setting>();
		}
	}

	// Token: 0x06002333 RID: 9011 RVA: 0x000D35D8 File Offset: 0x000D17D8
	public void Update()
	{
		if (!this.m_initFlag)
		{
			this.m_initFlag = true;
			this.SetEventSetting();
			if (this.m_performanceSetting != null)
			{
				this.m_performanceSetting.Setup();
				this.m_performanceSetting.PlayOpenWindow();
			}
		}
		else if (this.m_performanceSetting != null && this.m_performanceSetting.IsEnd)
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

	// Token: 0x04001FD9 RID: 8153
	private window_performance_setting m_performanceSetting;

	// Token: 0x04001FDA RID: 8154
	private GameObject m_gameObject;

	// Token: 0x04001FDB RID: 8155
	private ui_option_scroll m_ui_option_scroll;

	// Token: 0x04001FDC RID: 8156
	private bool m_initFlag;
}
