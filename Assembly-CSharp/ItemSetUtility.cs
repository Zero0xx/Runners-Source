using System;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000421 RID: 1057
public class ItemSetUtility
{
	// Token: 0x06001FD9 RID: 8153 RVA: 0x000BDF10 File Offset: 0x000BC110
	public static GameObject GetItemSetRootObject()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject == null)
		{
			return null;
		}
		return GameObjectUtil.FindChildGameObject(gameObject, "ItemSet_3_UI");
	}

	// Token: 0x06001FDA RID: 8154 RVA: 0x000BDF44 File Offset: 0x000BC144
	public static ItemSetRingManagement GetItemSetRingManagement()
	{
		GameObject itemSetRootObject = ItemSetUtility.GetItemSetRootObject();
		if (itemSetRootObject != null)
		{
			return itemSetRootObject.GetComponent<ItemSetRingManagement>();
		}
		return null;
	}

	// Token: 0x06001FDB RID: 8155 RVA: 0x000BDF70 File Offset: 0x000BC170
	public static int GetInstantItemCost(BoostItemType itemType)
	{
		ServerItem[] serverItemTable = ServerItem.GetServerItemTable(ServerItem.IdType.BOOST_ITEM);
		int id = (int)serverItemTable[(int)itemType].id;
		int result = 100;
		List<ServerConsumedCostData> costList = ServerInterface.CostList;
		if (costList == null)
		{
			return result;
		}
		foreach (ServerConsumedCostData serverConsumedCostData in costList)
		{
			if (serverConsumedCostData != null)
			{
				if (serverConsumedCostData.consumedItemId == id)
				{
					result = serverConsumedCostData.numItem;
					ServerCampaignData campaignDataInSession = ItemSetUtility.GetCampaignDataInSession(id);
					if (campaignDataInSession != null)
					{
						result = campaignDataInSession.iContent;
					}
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06001FDC RID: 8156 RVA: 0x000BE034 File Offset: 0x000BC234
	public static string GetInstantItemCostString(BoostItemType itemType)
	{
		int instantItemCost = ItemSetUtility.GetInstantItemCost(itemType);
		return HudUtility.GetFormatNumString<int>(instantItemCost);
	}

	// Token: 0x06001FDD RID: 8157 RVA: 0x000BE050 File Offset: 0x000BC250
	public static int GetItemNum(ItemType itemType)
	{
		int num = 0;
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			num = (int)instance.ItemData.GetItemCount(itemType);
			if ((long)num > 99L)
			{
				num = 99;
			}
		}
		if (itemType == ItemType.LASER)
		{
			MsgMenuItemSetStart.SetMode msgMenuItemSetStartMode = ItemSetUtility.GetMsgMenuItemSetStartMode();
			if (msgMenuItemSetStartMode == MsgMenuItemSetStart.SetMode.TUTORIAL && num == 0)
			{
				num = 1;
			}
		}
		return num;
	}

	// Token: 0x06001FDE RID: 8158 RVA: 0x000BE0A8 File Offset: 0x000BC2A8
	public static int GetFreeItemNum(ItemType itemType)
	{
		int result = 0;
		ServerFreeItemState freeItemState = ServerInterface.FreeItemState;
		if (freeItemState != null)
		{
			List<ServerItemState> itemList = freeItemState.itemList;
			for (int i = 0; i < itemList.Count; i++)
			{
				if (itemType == itemList[i].GetItem().itemType)
				{
					result = itemList[i].m_num;
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06001FDF RID: 8159 RVA: 0x000BE110 File Offset: 0x000BC310
	public static int GetFreeBoostItemNum(BoostItemType boostItemType)
	{
		int result = 0;
		ServerFreeItemState freeItemState = ServerInterface.FreeItemState;
		if (freeItemState != null)
		{
			List<ServerItemState> itemList = freeItemState.itemList;
			for (int i = 0; i < itemList.Count; i++)
			{
				if (boostItemType == itemList[i].GetItem().boostItemType)
				{
					result = itemList[i].m_num;
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06001FE0 RID: 8160 RVA: 0x000BE178 File Offset: 0x000BC378
	public static int GetOneRingItemId(ItemType itemType)
	{
		ServerItem[] serverItemTable = ServerItem.GetServerItemTable(ServerItem.IdType.EQUIP_ITEM);
		return (int)serverItemTable[(int)itemType].id;
	}

	// Token: 0x06001FE1 RID: 8161 RVA: 0x000BE19C File Offset: 0x000BC39C
	public static int GetTenRingItemId(ItemType itemType)
	{
		ServerItem[] serverItemTable = ServerItem.GetServerItemTable(ServerItem.IdType.EQUIP_ITEM);
		int num = 11;
		return (int)serverItemTable[(int)(num * ItemSetUtility.TEN_PACK_OFFSET + itemType)].id;
	}

	// Token: 0x06001FE2 RID: 8162 RVA: 0x000BE1C8 File Offset: 0x000BC3C8
	public static int GetOneRingItemContent(ItemType itemType)
	{
		int oneRingItemId = ItemSetUtility.GetOneRingItemId(itemType);
		return ItemSetUtility.GetRingItemContent(oneRingItemId);
	}

	// Token: 0x06001FE3 RID: 8163 RVA: 0x000BE1E4 File Offset: 0x000BC3E4
	public static int GetTenRingTenContent(ItemType itemType)
	{
		int tenRingItemId = ItemSetUtility.GetTenRingItemId(itemType);
		return ItemSetUtility.GetRingItemContent(tenRingItemId);
	}

	// Token: 0x06001FE4 RID: 8164 RVA: 0x000BE200 File Offset: 0x000BC400
	private static int GetRingItemContent(int serverItemId)
	{
		int result = 1;
		List<ServerConsumedCostData> costList = ServerInterface.CostList;
		if (costList == null)
		{
			return result;
		}
		foreach (ServerConsumedCostData serverConsumedCostData in costList)
		{
			if (serverConsumedCostData != null)
			{
				if (serverConsumedCostData.consumedItemId == serverItemId)
				{
					result = serverConsumedCostData.numItem;
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06001FE5 RID: 8165 RVA: 0x000BE290 File Offset: 0x000BC490
	public static ServerCampaignData GetCampaignDataInSession(int serverItemId)
	{
		if (ServerInterface.LoggedInServerInterface != null)
		{
			return ServerInterface.CampaignState.GetCampaignInSession(Constants.Campaign.emType.GameItemCost, serverItemId);
		}
		return null;
	}

	// Token: 0x06001FE6 RID: 8166 RVA: 0x000BE2C0 File Offset: 0x000BC4C0
	public static int GetOneItemCost(ItemType itemType)
	{
		int result = 200;
		int oneRingItemId = ItemSetUtility.GetOneRingItemId(itemType);
		List<ServerConsumedCostData> costList = ServerInterface.CostList;
		if (costList == null)
		{
			return result;
		}
		foreach (ServerConsumedCostData serverConsumedCostData in costList)
		{
			if (serverConsumedCostData != null)
			{
				if (serverConsumedCostData.consumedItemId == oneRingItemId)
				{
					result = serverConsumedCostData.numItem;
					ServerCampaignData campaignDataInSession = ItemSetUtility.GetCampaignDataInSession(serverConsumedCostData.consumedItemId);
					if (campaignDataInSession != null)
					{
						result = campaignDataInSession.iContent;
					}
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06001FE7 RID: 8167 RVA: 0x000BE378 File Offset: 0x000BC578
	public static int GetTenItemCost(ItemType itemType)
	{
		int result = 200;
		int tenRingItemId = ItemSetUtility.GetTenRingItemId(itemType);
		List<ServerConsumedCostData> costList = ServerInterface.CostList;
		foreach (ServerConsumedCostData serverConsumedCostData in costList)
		{
			if (serverConsumedCostData != null)
			{
				if (serverConsumedCostData.consumedItemId == tenRingItemId)
				{
					result = serverConsumedCostData.numItem;
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06001FE8 RID: 8168 RVA: 0x000BE40C File Offset: 0x000BC60C
	public static string GetOneItemCostString(ItemType itemType)
	{
		int oneItemCost = ItemSetUtility.GetOneItemCost(itemType);
		return HudUtility.GetFormatNumString<int>(oneItemCost);
	}

	// Token: 0x06001FE9 RID: 8169 RVA: 0x000BE428 File Offset: 0x000BC628
	public static string GetTenItemCostString(ItemType itemType)
	{
		int tenItemCost = ItemSetUtility.GetTenItemCost(itemType);
		return HudUtility.GetFormatNumString<int>(tenItemCost);
	}

	// Token: 0x06001FEA RID: 8170 RVA: 0x000BE444 File Offset: 0x000BC644
	public static MsgMenuItemSetStart.SetMode GetMsgMenuItemSetStartMode()
	{
		MsgMenuItemSetStart.SetMode result = MsgMenuItemSetStart.SetMode.NORMAL;
		if (HudMenuUtility.IsItemTutorial())
		{
			if (HudMenuUtility.itemSelectMode != HudMenuUtility.ITEM_SELECT_MODE.EVENT_STAGE && HudMenuUtility.itemSelectMode != HudMenuUtility.ITEM_SELECT_MODE.EVENT_BOSS)
			{
				result = MsgMenuItemSetStart.SetMode.TUTORIAL;
			}
		}
		else if (HudMenuUtility.IsTutorial_SubCharaItem())
		{
			result = MsgMenuItemSetStart.SetMode.TUTORIAL_SUBCHARA;
		}
		return result;
	}

	// Token: 0x04001CCB RID: 7371
	public static readonly string[] ButtonObjectName = new string[]
	{
		"boost_switch_1_lt",
		"boost_switch_2_ct",
		"boost_switch_3_rt"
	};

	// Token: 0x04001CCC RID: 7372
	private static readonly int TEN_PACK_OFFSET = 2;

	// Token: 0x02000422 RID: 1058
	public enum ItemSetType
	{
		// Token: 0x04001CCE RID: 7374
		SET_ONE,
		// Token: 0x04001CCF RID: 7375
		SET_TEN
	}
}
