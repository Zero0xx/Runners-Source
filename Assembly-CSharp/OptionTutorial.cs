using System;
using UnityEngine;

// Token: 0x0200049B RID: 1179
public class OptionTutorial : MonoBehaviour
{
	// Token: 0x06002315 RID: 8981 RVA: 0x000D2BB4 File Offset: 0x000D0DB4
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
			if (this.m_turoialWindow != null)
			{
				this.m_turoialWindow.PlayOpenWindow();
			}
		}
		else
		{
			this.m_initFlag = false;
			this.m_gameObject = HudMenuUtility.GetLoadMenuChildObject("window_tutorial", true);
		}
	}

	// Token: 0x06002316 RID: 8982 RVA: 0x000D2C4C File Offset: 0x000D0E4C
	private void SetTuroialWindow()
	{
		if (this.m_gameObject != null && this.m_turoialWindow == null)
		{
			this.m_turoialWindow = this.m_gameObject.GetComponent<window_tutorial>();
		}
	}

	// Token: 0x06002317 RID: 8983 RVA: 0x000D2C84 File Offset: 0x000D0E84
	public void Update()
	{
		if (!this.m_initFlag)
		{
			this.m_initFlag = true;
			this.SetTuroialWindow();
			if (this.m_turoialWindow != null)
			{
				this.m_turoialWindow.PlayOpenWindow();
			}
		}
		else if (this.m_turoialWindow != null && this.m_turoialWindow.IsEnd)
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

	// Token: 0x04001FB8 RID: 8120
	private window_tutorial m_turoialWindow;

	// Token: 0x04001FB9 RID: 8121
	private GameObject m_gameObject;

	// Token: 0x04001FBA RID: 8122
	private ui_option_scroll m_ui_option_scroll;

	// Token: 0x04001FBB RID: 8123
	private bool m_initFlag;
}
