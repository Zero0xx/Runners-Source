using System;
using UnityEngine;

// Token: 0x02000452 RID: 1106
public class HudHeaderRank : MonoBehaviour
{
	// Token: 0x0600214E RID: 8526 RVA: 0x000C8844 File Offset: 0x000C6A44
	private void Start()
	{
		HudMenuUtility.SetTagHudSaveItem(base.gameObject);
		base.enabled = false;
	}

	// Token: 0x0600214F RID: 8527 RVA: 0x000C8858 File Offset: 0x000C6A58
	private void Initialize()
	{
		if (this.m_initEnd)
		{
			return;
		}
		GameObject mainMenuCmnUIObject = HudMenuUtility.GetMainMenuCmnUIObject();
		if (mainMenuCmnUIObject != null)
		{
			Transform transform = mainMenuCmnUIObject.transform.FindChild("Anchor_1_TL/mainmenu_info_user/Btn_honor/img_bg_name/Lbl_rank");
			if (transform != null)
			{
				GameObject gameObject = transform.gameObject;
				if (gameObject != null)
				{
					this.m_ui_rank_label = gameObject.GetComponent<UILabel>();
				}
			}
		}
		this.m_initEnd = true;
	}

	// Token: 0x06002150 RID: 8528 RVA: 0x000C88C8 File Offset: 0x000C6AC8
	public void OnUpdateSaveDataDisplay()
	{
		this.Initialize();
		if (this.m_ui_rank_label != null)
		{
			uint num = 1U;
			SaveDataManager instance = SaveDataManager.Instance;
			if (instance)
			{
				num = instance.PlayerData.DisplayRank;
			}
			this.m_ui_rank_label.text = num.ToString();
		}
	}

	// Token: 0x04001E1D RID: 7709
	private const string m_rank_label_path = "Anchor_1_TL/mainmenu_info_user/Btn_honor/img_bg_name/Lbl_rank";

	// Token: 0x04001E1E RID: 7710
	private UILabel m_ui_rank_label;

	// Token: 0x04001E1F RID: 7711
	private bool m_initEnd;
}
