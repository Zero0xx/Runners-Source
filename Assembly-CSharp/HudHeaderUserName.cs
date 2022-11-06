using System;
using UnityEngine;

// Token: 0x02000455 RID: 1109
public class HudHeaderUserName : MonoBehaviour
{
	// Token: 0x0600215A RID: 8538 RVA: 0x000C8B74 File Offset: 0x000C6D74
	private void Start()
	{
		HudMenuUtility.SetTagHudSaveItem(base.gameObject);
		base.enabled = false;
	}

	// Token: 0x0600215B RID: 8539 RVA: 0x000C8B88 File Offset: 0x000C6D88
	private void Initialize()
	{
		if (!this.m_initEnd)
		{
			GameObject mainMenuCmnUIObject = HudMenuUtility.GetMainMenuCmnUIObject();
			if (mainMenuCmnUIObject != null)
			{
				Transform transform = mainMenuCmnUIObject.transform.FindChild("Anchor_1_TL/mainmenu_info_user/Btn_honor/img_bg_name/Lbl_username");
				if (transform != null)
				{
					GameObject gameObject = transform.gameObject;
					if (gameObject != null)
					{
						this.m_ui_name_label = gameObject.GetComponent<UILabel>();
					}
				}
			}
			this.m_initEnd = true;
		}
	}

	// Token: 0x0600215C RID: 8540 RVA: 0x000C8BF8 File Offset: 0x000C6DF8
	public void OnUpdateSaveDataDisplay()
	{
		this.Initialize();
		if (this.m_ui_name_label != null)
		{
			ServerSettingState settingState = ServerInterface.SettingState;
			if (settingState != null)
			{
				this.m_ui_name_label.text = settingState.m_userName;
			}
			else
			{
				this.m_ui_name_label.text = string.Empty;
			}
		}
	}

	// Token: 0x04001E2A RID: 7722
	private const string m_name_label_path = "Anchor_1_TL/mainmenu_info_user/Btn_honor/img_bg_name/Lbl_username";

	// Token: 0x04001E2B RID: 7723
	private UILabel m_ui_name_label;

	// Token: 0x04001E2C RID: 7724
	private bool m_initEnd;
}
