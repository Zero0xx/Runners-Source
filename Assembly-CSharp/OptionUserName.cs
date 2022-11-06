using System;
using UnityEngine;

// Token: 0x0200049C RID: 1180
public class OptionUserName : MonoBehaviour
{
	// Token: 0x06002319 RID: 8985 RVA: 0x000D2D34 File Offset: 0x000D0F34
	public void Setup(ui_option_scroll scroll)
	{
		if (scroll != null)
		{
			this.m_ui_option_scroll = scroll;
		}
		if (this.m_settingName == null)
		{
			GameObject menuAnimUIObject = HudMenuUtility.GetMenuAnimUIObject();
			if (menuAnimUIObject != null)
			{
				this.m_settingName = GameObjectUtil.FindChildGameObjectComponent<SettingUserName>(menuAnimUIObject, "window_name_setting");
			}
		}
		if (this.m_settingName != null)
		{
			this.m_settingName.SetCancelButtonUseFlag(true);
			this.m_settingName.Setup("UI Root (2D)/Camera/Anchor_5_MC");
			this.m_settingName.PlayStart();
		}
		base.enabled = true;
	}

	// Token: 0x0600231A RID: 8986 RVA: 0x000D2DC8 File Offset: 0x000D0FC8
	public void Update()
	{
		if (this.m_settingName != null && this.m_settingName.IsEndPlay())
		{
			if (this.m_ui_option_scroll != null)
			{
				this.m_ui_option_scroll.OnEndChildPage();
			}
			base.enabled = false;
			HudMenuUtility.SendMsgUpdateSaveDataDisplay();
		}
	}

	// Token: 0x04001FBC RID: 8124
	private const string ATTACH_ANTHOR_NAME = "UI Root (2D)/Camera/Anchor_5_MC";

	// Token: 0x04001FBD RID: 8125
	private SettingUserName m_settingName;

	// Token: 0x04001FBE RID: 8126
	private ui_option_scroll m_ui_option_scroll;
}
