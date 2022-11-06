using System;
using Text;
using UnityEngine;

// Token: 0x02000419 RID: 1049
public class InstantItemWindow : MonoBehaviour
{
	// Token: 0x06001F8B RID: 8075 RVA: 0x000BB1F4 File Offset: 0x000B93F4
	public void SetWindowActive()
	{
		if (this.m_instantItemObject != null && !this.m_instantItemObject.activeSelf)
		{
			this.m_instantItemObject.SetActive(true);
		}
		if (this.m_itemObject != null && this.m_itemObject.activeSelf)
		{
			this.m_itemObject.SetActive(false);
		}
	}

	// Token: 0x06001F8C RID: 8076 RVA: 0x000BB25C File Offset: 0x000B945C
	public void SetInstantItemType(BoostItemType itemType)
	{
		this.m_itemType = itemType;
		this.m_FreeCount = ItemSetUtility.GetFreeBoostItemNum(itemType);
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_instantItemObject, "img_boost_icon");
		if (uisprite != null)
		{
			UISprite uisprite2 = uisprite;
			string str = "ui_itemset_2_boost_icon_";
			int itemType2 = (int)this.m_itemType;
			uisprite2.spriteName = str + itemType2.ToString();
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_instantItemObject, "Lbl_boost_name");
		if (uilabel != null)
		{
			string cellName = "instant_name" + ((int)(this.m_itemType + 1)).ToString();
			string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ShopItem", cellName).text;
			uilabel.text = text;
			UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(uilabel.gameObject, "Lbl_boost_name_sdw");
			if (uilabel2 != null)
			{
				uilabel2.text = text;
			}
		}
		UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_instantItemObject, "Lbl_boost_percent");
		if (uilabel3 != null)
		{
			if (itemType == BoostItemType.SCORE_BONUS)
			{
				uilabel3.gameObject.SetActive(true);
				string text2 = "100.0%";
				uilabel3.text = text2;
				UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_instantItemObject, "Lbl_boost_percent_sdw");
				if (uilabel4 != null)
				{
					uilabel4.text = text2;
				}
			}
			else
			{
				uilabel3.gameObject.SetActive(false);
			}
		}
		UILabel uilabel5 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_instantItemObject, "Lbl_item_info");
		if (uilabel5 != null)
		{
			string cellName2 = "instant_details" + ((int)(this.m_itemType + 1)).ToString();
			uilabel5.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ShopItem", cellName2).text;
		}
		GameObject parent = GameObjectUtil.FindChildGameObject(this.m_instantItemObject, "row_0");
		UILabel uilabel6 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parent, "Lbl_free_number");
		if (uilabel6 != null)
		{
			uilabel6.text = this.m_FreeCount.ToString();
		}
		this.UpdateCampaignView();
	}

	// Token: 0x06001F8D RID: 8077 RVA: 0x000BB448 File Offset: 0x000B9648
	public void SetCheckMark(bool isCheck)
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_instantItemObject, "img_checkmark");
		if (gameObject != null)
		{
			gameObject.SetActive(isCheck);
		}
	}

	// Token: 0x06001F8E RID: 8078 RVA: 0x000BB47C File Offset: 0x000B967C
	private void UpdateCampaignView()
	{
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_instantItemObject, "Lbl_ring_number");
		if (uilabel != null)
		{
			uilabel.text = ItemSetUtility.GetInstantItemCostString(this.m_itemType);
		}
		bool flag = false;
		if (this.m_FreeCount > 0)
		{
			flag = true;
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "img_sale_icon");
		if (gameObject != null)
		{
			ServerItem serverItem = new ServerItem(this.m_itemType);
			ServerCampaignData campaignDataInSession = ItemSetUtility.GetCampaignDataInSession((int)serverItem.id);
			bool active = false;
			if (!flag && campaignDataInSession != null)
			{
				active = true;
			}
			gameObject.SetActive(active);
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_instantItemObject, "row_0");
		if (gameObject2 != null)
		{
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject2.gameObject, "img_use_ring_bg");
			if (gameObject3 != null)
			{
				gameObject3.SetActive(!flag);
			}
			GameObject gameObject4 = GameObjectUtil.FindChildGameObject(gameObject2.gameObject, "img_free_bg");
			if (gameObject4 != null)
			{
				gameObject4.SetActive(flag);
			}
		}
	}

	// Token: 0x06001F8F RID: 8079 RVA: 0x000BB58C File Offset: 0x000B978C
	private void Awake()
	{
		this.m_instantItemObject = GameObjectUtil.FindChildGameObject(base.gameObject, "boost_info_pla");
		this.m_itemObject = GameObjectUtil.FindChildGameObject(base.gameObject, "item_info_pla");
	}

	// Token: 0x06001F90 RID: 8080 RVA: 0x000BB5C8 File Offset: 0x000B97C8
	private void Update()
	{
		this.UpdateCampaignView();
	}

	// Token: 0x04001C87 RID: 7303
	private BoostItemType m_itemType;

	// Token: 0x04001C88 RID: 7304
	private GameObject m_instantItemObject;

	// Token: 0x04001C89 RID: 7305
	private GameObject m_itemObject;

	// Token: 0x04001C8A RID: 7306
	private int m_FreeCount;
}
