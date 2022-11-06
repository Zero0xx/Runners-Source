using System;
using UnityEngine;

// Token: 0x02000453 RID: 1107
public class HudHeaderRedRing : MonoBehaviour
{
	// Token: 0x06002152 RID: 8530 RVA: 0x000C8928 File Offset: 0x000C6B28
	private void Start()
	{
		HudMenuUtility.SetTagHudSaveItem(base.gameObject);
		base.enabled = false;
	}

	// Token: 0x06002153 RID: 8531 RVA: 0x000C893C File Offset: 0x000C6B3C
	private void Initialize()
	{
		if (!this.m_initEnd)
		{
			GameObject mainMenuCmnUIObject = HudMenuUtility.GetMainMenuCmnUIObject();
			if (mainMenuCmnUIObject != null)
			{
				GameObject gameObject = mainMenuCmnUIObject.transform.FindChild("Anchor_3_TR/mainmenu_info_quantum/img_bg_rsring/Lbl_rsring").gameObject;
				if (gameObject != null)
				{
					this.m_ui_red_ring_label = gameObject.GetComponent<UILabel>();
				}
				this.m_sale_obj = mainMenuCmnUIObject.transform.FindChild("Anchor_3_TR/mainmenu_info_quantum/Btn_shop/img_sale_icon_rsring").gameObject;
			}
			this.m_initEnd = true;
		}
	}

	// Token: 0x06002154 RID: 8532 RVA: 0x000C89B8 File Offset: 0x000C6BB8
	public void OnUpdateSaveDataDisplay()
	{
		this.Initialize();
		if (this.m_ui_red_ring_label != null)
		{
			int num = 0;
			SaveDataManager instance = SaveDataManager.Instance;
			if (instance != null)
			{
				num = instance.ItemData.DisplayRedRingCount;
			}
			this.m_ui_red_ring_label.text = HudUtility.GetFormatNumString<int>(num);
		}
		if (this.m_sale_obj != null)
		{
			bool flag = HudMenuUtility.IsSale(Constants.Campaign.emType.PurchaseAddRsrings);
			bool flag2 = HudMenuUtility.IsSale(Constants.Campaign.emType.PurchaseAddRsringsNoChargeUser);
			if (flag || flag2)
			{
				this.m_sale_obj.SetActive(true);
			}
			else
			{
				this.m_sale_obj.SetActive(false);
			}
		}
	}

	// Token: 0x04001E20 RID: 7712
	private const string m_redring_path = "Anchor_3_TR/mainmenu_info_quantum/img_bg_rsring";

	// Token: 0x04001E21 RID: 7713
	private const string m_sale_path = "Anchor_3_TR/mainmenu_info_quantum/Btn_shop/img_sale_icon_rsring";

	// Token: 0x04001E22 RID: 7714
	private UILabel m_ui_red_ring_label;

	// Token: 0x04001E23 RID: 7715
	private GameObject m_sale_obj;

	// Token: 0x04001E24 RID: 7716
	private bool m_initEnd;
}
