using System;
using UnityEngine;

// Token: 0x02000454 RID: 1108
public class HudHeaderRing : MonoBehaviour
{
	// Token: 0x06002156 RID: 8534 RVA: 0x000C8A60 File Offset: 0x000C6C60
	private void Start()
	{
		HudMenuUtility.SetTagHudSaveItem(base.gameObject);
		base.enabled = false;
	}

	// Token: 0x06002157 RID: 8535 RVA: 0x000C8A74 File Offset: 0x000C6C74
	private void Initialize()
	{
		if (!this.m_initEnd)
		{
			GameObject mainMenuCmnUIObject = HudMenuUtility.GetMainMenuCmnUIObject();
			if (mainMenuCmnUIObject != null)
			{
				GameObject gameObject = mainMenuCmnUIObject.transform.FindChild("Anchor_3_TR/mainmenu_info_quantum/img_bg_stock_ring/Lbl_stock").gameObject;
				if (gameObject != null)
				{
					this.m_ui_ring_label = gameObject.GetComponent<UILabel>();
				}
				this.m_sale_obj = mainMenuCmnUIObject.transform.FindChild("Anchor_3_TR/mainmenu_info_quantum/Btn_shop/img_sale_icon_ring").gameObject;
			}
			this.m_initEnd = true;
		}
	}

	// Token: 0x06002158 RID: 8536 RVA: 0x000C8AF0 File Offset: 0x000C6CF0
	public void OnUpdateSaveDataDisplay()
	{
		this.Initialize();
		if (this.m_ui_ring_label != null)
		{
			int num = 0;
			SaveDataManager instance = SaveDataManager.Instance;
			if (instance != null)
			{
				num = instance.ItemData.DisplayRingCount;
			}
			this.m_ui_ring_label.text = HudUtility.GetFormatNumString<int>(num);
		}
		if (this.m_sale_obj != null)
		{
			bool active = HudMenuUtility.IsSale(Constants.Campaign.emType.PurchaseAddRings);
			this.m_sale_obj.SetActive(active);
		}
	}

	// Token: 0x04001E25 RID: 7717
	private const string m_ring_path = "Anchor_3_TR/mainmenu_info_quantum/img_bg_stock_ring";

	// Token: 0x04001E26 RID: 7718
	private const string m_sale_path = "Anchor_3_TR/mainmenu_info_quantum/Btn_shop/img_sale_icon_ring";

	// Token: 0x04001E27 RID: 7719
	private UILabel m_ui_ring_label;

	// Token: 0x04001E28 RID: 7720
	private GameObject m_sale_obj;

	// Token: 0x04001E29 RID: 7721
	private bool m_initEnd;
}
