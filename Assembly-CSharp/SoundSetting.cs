using System;
using UnityEngine;

// Token: 0x020004A0 RID: 1184
public class SoundSetting : MonoBehaviour
{
	// Token: 0x06002325 RID: 8997 RVA: 0x000D3070 File Offset: 0x000D1270
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
			if (this.m_soundSetiing != null)
			{
				this.m_soundSetiing.PlayOpenWindow();
			}
		}
		else
		{
			this.m_initFlag = false;
			this.m_gameObject = HudMenuUtility.GetLoadMenuChildObject("window_sound_setiing", true);
		}
	}

	// Token: 0x06002326 RID: 8998 RVA: 0x000D3108 File Offset: 0x000D1308
	private void SetSoundSetting()
	{
		if (this.m_gameObject != null && this.m_soundSetiing == null)
		{
			this.m_soundSetiing = this.m_gameObject.GetComponent<window_sound_setiing>();
		}
	}

	// Token: 0x06002327 RID: 8999 RVA: 0x000D3140 File Offset: 0x000D1340
	public void Update()
	{
		if (!this.m_initFlag)
		{
			this.m_initFlag = true;
			this.SetSoundSetting();
			if (this.m_soundSetiing != null)
			{
				this.m_soundSetiing.PlayOpenWindow();
			}
		}
		else if (this.m_soundSetiing != null && this.m_soundSetiing.IsEnd)
		{
			if (this.m_ui_option_scroll != null)
			{
				if (this.m_soundSetiing.IsOverwrite)
				{
					this.m_ui_option_scroll.SetSystemSaveFlag();
				}
				this.m_ui_option_scroll.OnEndChildPage();
			}
			base.enabled = false;
			if (this.m_gameObject != null)
			{
				this.m_gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x04001FCA RID: 8138
	private window_sound_setiing m_soundSetiing;

	// Token: 0x04001FCB RID: 8139
	private GameObject m_gameObject;

	// Token: 0x04001FCC RID: 8140
	private ui_option_scroll m_ui_option_scroll;

	// Token: 0x04001FCD RID: 8141
	private bool m_initFlag;
}
