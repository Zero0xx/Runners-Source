using System;
using SaveData;
using UnityEngine;

// Token: 0x0200049A RID: 1178
public class OptionPushNotification : MonoBehaviour
{
	// Token: 0x06002311 RID: 8977 RVA: 0x000D2A68 File Offset: 0x000D0C68
	public void Setup(ui_option_scroll scroll)
	{
		base.enabled = true;
		if (this.m_ui_option_scroll == null && scroll != null)
		{
			this.m_ui_option_scroll = scroll;
		}
		if (this.m_pushNotice != null)
		{
			this.m_pushNotice.PlayStart();
		}
		else
		{
			this.m_pushNotice = base.gameObject.AddComponent<SettingPartsPushNotice>();
			if (this.m_pushNotice)
			{
				this.m_pushNotice.Setup("UI Root (2D)/Camera/Anchor_5_MC");
				this.m_pushNotice.PlayStart();
			}
		}
	}

	// Token: 0x06002312 RID: 8978 RVA: 0x000D2B00 File Offset: 0x000D0D00
	public void Update()
	{
		if (this.m_pushNotice != null && this.m_pushNotice.IsEndPlay())
		{
			if (this.m_ui_option_scroll != null)
			{
				if (this.m_pushNotice.IsOverwrite)
				{
					SystemSaveManager instance = SystemSaveManager.Instance;
					if (instance != null)
					{
						instance.SaveSystemData();
					}
					this.m_ui_option_scroll.ResetSystemSaveFlag();
				}
				this.m_ui_option_scroll.OnEndChildPage();
			}
			base.enabled = false;
			this.SetActivePushNoticeObject(false);
		}
	}

	// Token: 0x06002313 RID: 8979 RVA: 0x000D2B8C File Offset: 0x000D0D8C
	private void SetActivePushNoticeObject(bool flag)
	{
		if (this.m_pushNotice != null)
		{
			this.m_pushNotice.SetWindowActive(flag);
		}
	}

	// Token: 0x04001FB5 RID: 8117
	private const string ATTACH_ANTHOR_NAME = "UI Root (2D)/Camera/Anchor_5_MC";

	// Token: 0x04001FB6 RID: 8118
	private SettingPartsPushNotice m_pushNotice;

	// Token: 0x04001FB7 RID: 8119
	private ui_option_scroll m_ui_option_scroll;
}
