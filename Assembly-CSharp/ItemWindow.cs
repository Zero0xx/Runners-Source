using System;
using System.Collections;
using Text;
using UnityEngine;

// Token: 0x02000423 RID: 1059
public class ItemWindow : MonoBehaviour
{
	// Token: 0x06001FEC RID: 8172 RVA: 0x000BE490 File Offset: 0x000BC690
	public void SetItemBuyCallback(ItemWindow.ItemBuyCallback callback)
	{
		this.m_callback = callback;
	}

	// Token: 0x06001FED RID: 8173 RVA: 0x000BE49C File Offset: 0x000BC69C
	public void SetWindowActive()
	{
		if (this.m_instantItemObject != null && this.m_instantItemObject.activeSelf)
		{
			this.m_instantItemObject.SetActive(false);
		}
		if (this.m_itemObject != null && !this.m_itemObject.activeSelf)
		{
			this.m_itemObject.SetActive(true);
		}
	}

	// Token: 0x06001FEE RID: 8174 RVA: 0x000BE504 File Offset: 0x000BC704
	public void SetItemType(ItemType itemType)
	{
		this.m_itemType = itemType;
		this.m_FreeCount = ItemSetUtility.GetFreeItemNum(this.m_itemType);
		this.UpdateView();
	}

	// Token: 0x06001FEF RID: 8175 RVA: 0x000BE524 File Offset: 0x000BC724
	public void UpdateView()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_itemObject, "row_0");
		if (gameObject != null)
		{
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_item_icon");
			if (uisprite != null)
			{
				UISprite uisprite2 = uisprite;
				string str = "ui_cmn_icon_item_";
				int itemType = (int)this.m_itemType;
				uisprite2.spriteName = str + itemType.ToString();
			}
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject.gameObject, "item_stock");
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject.gameObject, "img_use_ring_bg");
			GameObject gameObject4 = GameObjectUtil.FindChildGameObject(gameObject.gameObject, "img_free_bg");
			int itemNum = ItemSetUtility.GetItemNum(this.m_itemType);
			if (gameObject2 != null && gameObject3 != null && gameObject4 != null)
			{
				if (this.m_FreeCount > 0)
				{
					gameObject4.SetActive(true);
					gameObject2.SetActive(false);
					gameObject3.SetActive(false);
					this.UpdateFreeCount(this.m_FreeCount);
				}
				else if (itemNum > 0)
				{
					gameObject4.SetActive(false);
					gameObject2.SetActive(true);
					gameObject3.SetActive(false);
					this.UpdateItemCount();
				}
				else
				{
					gameObject4.SetActive(false);
					gameObject2.SetActive(false);
					gameObject3.SetActive(true);
					this.UpdateCampaignView();
				}
			}
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_item_name");
			if (uilabel != null)
			{
				string cellName = "name" + ((int)(this.m_itemType + 1)).ToString();
				string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ShopItem", cellName).text;
				uilabel.text = text;
				UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(uilabel.gameObject, "Lbl_item_name_sdw");
				if (uilabel2 != null)
				{
					uilabel2.text = text;
				}
			}
		}
		UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_itemObject, "Lbl_item_info");
		if (uilabel3 != null)
		{
			AbilityType abilityType = StageItemManager.s_dicItemTypeToCharAbilityType[this.m_itemType];
			SaveDataManager instance = SaveDataManager.Instance;
			CharaType mainChara = instance.PlayerData.MainChara;
			CharaAbility charaAbility = instance.CharaData.AbilityArray[(int)mainChara];
			int num = (int)(((ulong)abilityType >= (ulong)((long)charaAbility.Ability.Length)) ? 0U : charaAbility.Ability[(int)abilityType]);
			float itemTimeFromChara = StageItemManager.GetItemTimeFromChara(this.m_itemType);
			string cellName2 = "details" + ((int)(this.m_itemType + 1)).ToString();
			TextObject text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ShopItem", cellName2);
			text2.ReplaceTag("{LEVEL}", num.ToString());
			text2.ReplaceTag("{TIME}", itemTimeFromChara.ToString("0.0"));
			uilabel3.text = text2.text;
		}
	}

	// Token: 0x06001FF0 RID: 8176 RVA: 0x000BE7D0 File Offset: 0x000BC9D0
	private void UpdateCampaignView()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_itemObject, "row_0");
		if (gameObject == null)
		{
			return;
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_ring_number");
		if (uilabel != null)
		{
			string oneItemCostString = ItemSetUtility.GetOneItemCostString(this.m_itemType);
			uilabel.text = oneItemCostString;
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "img_sale_icon");
		if (gameObject2 != null)
		{
			ServerItem serverItem = new ServerItem(this.m_itemType);
			ServerCampaignData campaignDataInSession = ItemSetUtility.GetCampaignDataInSession((int)serverItem.id);
			bool active = false;
			if (this.m_FreeCount == 0 && campaignDataInSession != null)
			{
				active = true;
			}
			gameObject2.SetActive(active);
		}
	}

	// Token: 0x06001FF1 RID: 8177 RVA: 0x000BE87C File Offset: 0x000BCA7C
	private void UpdateItemCount()
	{
		int itemNum = ItemSetUtility.GetItemNum(this.m_itemType);
		GameObject parent = GameObjectUtil.FindChildGameObject(this.m_itemObject, "row_0");
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parent, "Lbl_number");
		if (uilabel != null)
		{
			uilabel.text = itemNum.ToString();
			UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parent, "Lbl_number_sdw");
			if (uilabel2 != null)
			{
				uilabel2.text = itemNum.ToString();
			}
		}
	}

	// Token: 0x06001FF2 RID: 8178 RVA: 0x000BE8F0 File Offset: 0x000BCAF0
	private void UpdateFreeCount(int value)
	{
		GameObject parent = GameObjectUtil.FindChildGameObject(this.m_itemObject, "row_0");
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parent, "Lbl_free_number");
		if (uilabel != null)
		{
			uilabel.text = value.ToString();
		}
	}

	// Token: 0x06001FF3 RID: 8179 RVA: 0x000BE934 File Offset: 0x000BCB34
	public void SetEquipMark(bool isEquip)
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_itemObject, "img_cursor");
		if (gameObject != null)
		{
			gameObject.SetActive(isEquip);
		}
	}

	// Token: 0x06001FF4 RID: 8180 RVA: 0x000BE968 File Offset: 0x000BCB68
	public void SetEquipMarkColor(ItemButton.CursorColor cursorColor)
	{
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_itemObject, "img_cursor");
		UISprite uisprite2 = uisprite;
		string str = "ui_itemset_2_cursor_";
		int num = (int)cursorColor;
		uisprite2.spriteName = str + num.ToString();
	}

	// Token: 0x06001FF5 RID: 8181 RVA: 0x000BE9A0 File Offset: 0x000BCBA0
	private void Start()
	{
		this.m_instantItemObject = GameObjectUtil.FindChildGameObject(base.gameObject, "boost_info_pla");
		this.m_itemObject = GameObjectUtil.FindChildGameObject(base.gameObject, "item_info_pla");
		base.StartCoroutine(this.SetupUIRectItemStorage());
	}

	// Token: 0x06001FF6 RID: 8182 RVA: 0x000BE9E8 File Offset: 0x000BCBE8
	private void Update()
	{
		this.UpdateCampaignView();
	}

	// Token: 0x06001FF7 RID: 8183 RVA: 0x000BE9F0 File Offset: 0x000BCBF0
	private IEnumerator SetupUIRectItemStorage()
	{
		if (this.m_itemObject != null)
		{
			this.m_itemObject.SetActive(true);
			yield return null;
			this.m_itemObject.SetActive(false);
		}
		yield return null;
		yield break;
	}

	// Token: 0x06001FF8 RID: 8184 RVA: 0x000BEA0C File Offset: 0x000BCC0C
	private void BuyCompleteCallback(ItemType itemType)
	{
		this.UpdateItemCount();
		this.m_callback(itemType);
	}

	// Token: 0x06001FF9 RID: 8185 RVA: 0x000BEA20 File Offset: 0x000BCC20
	private void BuyCancelledCallback(ItemType itemType)
	{
	}

	// Token: 0x04001CD0 RID: 7376
	private GameObject m_instantItemObject;

	// Token: 0x04001CD1 RID: 7377
	private GameObject m_itemObject;

	// Token: 0x04001CD2 RID: 7378
	private ItemType m_itemType;

	// Token: 0x04001CD3 RID: 7379
	private ItemWindow.ItemBuyCallback m_callback;

	// Token: 0x04001CD4 RID: 7380
	private int m_FreeCount;

	// Token: 0x02000A8D RID: 2701
	// (Invoke) Token: 0x0600486E RID: 18542
	public delegate void ItemBuyCallback(ItemType itemType);
}
