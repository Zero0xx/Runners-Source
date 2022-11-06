using System;
using UnityEngine;

// Token: 0x0200049D RID: 1181
public class OptionUserResult : MonoBehaviour
{
	// Token: 0x0600231C RID: 8988 RVA: 0x000D2E28 File Offset: 0x000D1028
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
			if (this.m_userData != null)
			{
				this.m_userData.PlayOpenWindow();
			}
		}
		else
		{
			this.m_initFlag = false;
			this.m_gameObject = HudMenuUtility.GetLoadMenuChildObject("window_user_date", true);
		}
	}

	// Token: 0x0600231D RID: 8989 RVA: 0x000D2EC0 File Offset: 0x000D10C0
	private void SetUserData()
	{
		if (this.m_gameObject != null && this.m_userData == null)
		{
			this.m_userData = this.m_gameObject.GetComponent<window_user_date>();
		}
	}

	// Token: 0x0600231E RID: 8990 RVA: 0x000D2EF8 File Offset: 0x000D10F8
	public void Update()
	{
		if (!this.m_initFlag)
		{
			this.m_initFlag = true;
			this.SetUserData();
			if (this.m_userData != null)
			{
				this.m_userData.PlayOpenWindow();
			}
		}
		else if (this.m_userData != null && this.m_userData.IsEnd)
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

	// Token: 0x04001FBF RID: 8127
	private window_user_date m_userData;

	// Token: 0x04001FC0 RID: 8128
	private GameObject m_gameObject;

	// Token: 0x04001FC1 RID: 8129
	private ui_option_scroll m_ui_option_scroll;

	// Token: 0x04001FC2 RID: 8130
	private bool m_initFlag;
}
