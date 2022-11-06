using System;
using UnityEngine;

// Token: 0x02000441 RID: 1089
public class HudButtonRoulette : MonoBehaviour
{
	// Token: 0x06002102 RID: 8450 RVA: 0x000C6038 File Offset: 0x000C4238
	private void Start()
	{
		HudMenuUtility.SetTagHudSaveItem(base.gameObject);
		base.enabled = false;
	}

	// Token: 0x06002103 RID: 8451 RVA: 0x000C604C File Offset: 0x000C424C
	private void Initialize()
	{
		if (this.m_initEnd)
		{
			return;
		}
		GameObject mainMenuUIObject = HudMenuUtility.GetMainMenuUIObject();
		if (mainMenuUIObject != null)
		{
			GameObject gameObject = mainMenuUIObject.transform.FindChild("Anchor_8_BC/Btn_roulette").gameObject;
			if (gameObject != null)
			{
				this.m_free_badge = gameObject.transform.FindChild("badge_spin").gameObject;
				if (this.m_free_badge != null)
				{
					GameObject gameObject2 = this.m_free_badge.transform.FindChild("Lbl_roulette_volume").gameObject;
					if (gameObject2 != null)
					{
						this.m_free_label = gameObject2.GetComponent<UILabel>();
					}
				}
				this.m_chao_free_badge = gameObject.transform.FindChild("badge_p_spin").gameObject;
				if (this.m_chao_free_badge != null)
				{
					GameObject gameObject3 = this.m_chao_free_badge.transform.FindChild("Lbl_roulette_p_volume").gameObject;
					if (gameObject3 != null)
					{
						this.m_chao_free_label = gameObject3.GetComponent<UILabel>();
					}
				}
				this.m_egg_obj = gameObject.transform.FindChild("badge_egg").gameObject;
				this.m_sale_obj = gameObject.transform.FindChild("badge_alert").gameObject;
				this.m_event_obj = gameObject.transform.FindChild("event_icon").gameObject;
				GameObject gameObject4 = gameObject.transform.FindChild("img_ad_tex").gameObject;
				if (gameObject4 != null)
				{
					this.m_banner = gameObject4.GetComponent<UITexture>();
				}
			}
		}
		this.m_initEnd = true;
	}

	// Token: 0x06002104 RID: 8452 RVA: 0x000C61E0 File Offset: 0x000C43E0
	public void OnUpdateSaveDataDisplay()
	{
		this.Initialize();
		HudRouletteButtonUtil.SetSpecialEggIcon(this.m_egg_obj);
		bool counterStop = true;
		HudRouletteButtonUtil.SetChaoFreeSpin(this.m_chao_free_badge, this.m_chao_free_label, counterStop);
		HudRouletteButtonUtil.SetFreeSpin(this.m_free_badge, this.m_free_label, counterStop);
		HudRouletteButtonUtil.SetSaleIcon(this.m_sale_obj);
		HudRouletteButtonUtil.SetEventIcon(this.m_event_obj);
		if (this.m_banner != null)
		{
			RouletteInformationManager instance = RouletteInformationManager.Instance;
			if (instance != null)
			{
				instance.LoadInfoBaner(new RouletteInformationManager.InfoBannerRequest(this.m_banner), RouletteCategory.PREMIUM);
			}
		}
	}

	// Token: 0x04001DB9 RID: 7609
	private const string OBJ_PATH = "Anchor_8_BC/Btn_roulette";

	// Token: 0x04001DBA RID: 7610
	private GameObject m_free_badge;

	// Token: 0x04001DBB RID: 7611
	private UILabel m_free_label;

	// Token: 0x04001DBC RID: 7612
	private GameObject m_chao_free_badge;

	// Token: 0x04001DBD RID: 7613
	private UILabel m_chao_free_label;

	// Token: 0x04001DBE RID: 7614
	private GameObject m_egg_obj;

	// Token: 0x04001DBF RID: 7615
	private GameObject m_sale_obj;

	// Token: 0x04001DC0 RID: 7616
	private GameObject m_event_obj;

	// Token: 0x04001DC1 RID: 7617
	private UITexture m_banner;

	// Token: 0x04001DC2 RID: 7618
	private bool m_initEnd;
}
