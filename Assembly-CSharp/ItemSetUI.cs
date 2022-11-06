using System;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000429 RID: 1065
public class ItemSetUI : MonoBehaviour
{
	// Token: 0x06002052 RID: 8274 RVA: 0x000C24C4 File Offset: 0x000C06C4
	private void Start()
	{
		this.m_slot_equip = GameObjectUtil.FindChildGameObject(base.gameObject, "slot_equip");
		this.m_slot_item = GameObjectUtil.FindChildGameObject(base.gameObject, "slot_item");
		foreach (UIRectItemStorage uirectItemStorage in new UIRectItemStorage[]
		{
			this.m_slot_item.GetComponent<UIRectItemStorage>(),
			GameObjectUtil.FindChildGameObjectComponent<UIRectItemStorage>(base.gameObject, "slot_bg")
		})
		{
			uirectItemStorage.maxRows = (ShopItemTable.GetDataTable().Length + uirectItemStorage.maxColumns - 1) / uirectItemStorage.maxColumns;
			uirectItemStorage.maxItemCount = ShopItemTable.GetDataTable().Length;
		}
		ItemSetWindowEquipUI itemSetWindowEquipUI = GameObjectUtil.FindChildGameObjectComponent<ItemSetWindowEquipUI>(base.gameObject.transform.root.gameObject, "ItemSetWindowEquipUI");
		if (itemSetWindowEquipUI != null)
		{
			itemSetWindowEquipUI.gameObject.SetActive(true);
		}
	}

	// Token: 0x06002053 RID: 8275 RVA: 0x000C25A0 File Offset: 0x000C07A0
	private void Update()
	{
		if (!this.m_isInitialized)
		{
			this.UpdateView();
			this.m_isInitialized = true;
		}
	}

	// Token: 0x06002054 RID: 8276 RVA: 0x000C25BC File Offset: 0x000C07BC
	public void UpdateView()
	{
		for (EquippedType equippedType = EquippedType.EQUIPPED_01; equippedType < EquippedType.NUM; equippedType++)
		{
			ItemType equipedItemType = ItemSetUI.SaveDataInterface.GetEquipedItemType(equippedType);
			this.UpdateEquipedItemView(equippedType, equipedItemType);
		}
		for (int i = 0; i < ShopItemTable.GetDataTable().Length; i++)
		{
			ItemType id = (ItemType)ShopItemTable.GetShopItemDataOfIndex(i).id;
			this.UpdateExistItemView(i, id);
		}
	}

	// Token: 0x06002055 RID: 8277 RVA: 0x000C2618 File Offset: 0x000C0818
	public void UpdateEquipedItemView(EquippedType slot, ItemType itemType)
	{
		this.UpdateItemView(this.m_slot_equip, (int)slot, itemType);
	}

	// Token: 0x06002056 RID: 8278 RVA: 0x000C2628 File Offset: 0x000C0828
	public void UpdateExistItemView(int index, ItemType itemType)
	{
		this.UpdateItemView(this.m_slot_item, index, itemType);
	}

	// Token: 0x06002057 RID: 8279 RVA: 0x000C2638 File Offset: 0x000C0838
	private void UpdateItemView(GameObject parent, int index, ItemType itemType)
	{
		List<ui_item_set_cell> list = GameObjectUtil.FindChildGameObjectsComponents<ui_item_set_cell>(parent, "ui_item_set_cell(Clone)");
		if (list != null)
		{
			list[index].UpdateView(itemType, ItemSetUI.SaveDataInterface.GetItemCount(itemType));
		}
	}

	// Token: 0x06002058 RID: 8280 RVA: 0x000C266C File Offset: 0x000C086C
	private void OnStartItemSet()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetRingExchangeList(base.gameObject);
		}
	}

	// Token: 0x06002059 RID: 8281 RVA: 0x000C2698 File Offset: 0x000C0898
	private void ServerGetRingExchangeList_Succeeded(MsgGetRingExchangeListSucceed msg)
	{
		foreach (ServerRingExchangeList serverRingExchangeList in msg.m_exchangeList)
		{
			ServerItem serverItem = new ServerItem((ServerItem.Id)serverRingExchangeList.m_itemId);
			ItemType itemType = serverItem.itemType;
			ShopItemData shopItemData = ShopItemTable.GetShopItemData((int)itemType);
			if (shopItemData != null)
			{
				shopItemData.rings = serverRingExchangeList.m_price;
			}
		}
	}

	// Token: 0x0600205A RID: 8282 RVA: 0x000C2728 File Offset: 0x000C0928
	private void OnClose(string next)
	{
		HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.REDSTAR_TO_SHOP, false);
	}

	// Token: 0x04001D0E RID: 7438
	private GameObject m_slot_equip;

	// Token: 0x04001D0F RID: 7439
	private GameObject m_slot_item;

	// Token: 0x04001D10 RID: 7440
	private bool m_isInitialized;

	// Token: 0x04001D11 RID: 7441
	private string m_next;

	// Token: 0x0200042A RID: 1066
	public class SaveDataInterface
	{
		// Token: 0x0600205C RID: 8284 RVA: 0x000C273C File Offset: 0x000C093C
		public static int GetItemCount(ItemType itemType)
		{
			SaveDataManager instance = SaveDataManager.Instance;
			return (int)((!(instance != null)) ? 0U : instance.ItemData.GetItemCount(itemType));
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x000C2770 File Offset: 0x000C0970
		public static ItemType GetEquipedItemType(EquippedType slot)
		{
			SaveDataManager instance = SaveDataManager.Instance;
			return (!(instance != null) || slot >= EquippedType.NUM) ? ItemType.INVINCIBLE : instance.PlayerData.EquippedItem[(int)slot];
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x000C27AC File Offset: 0x000C09AC
		public static void SetEquipedItemType(EquippedType slot, ItemType itemType)
		{
			SaveDataManager instance = SaveDataManager.Instance;
			if (instance != null && slot < EquippedType.NUM)
			{
				instance.PlayerData.EquippedItem[(int)slot] = itemType;
				HudMenuUtility.SendMsgUpdateSaveDataDisplay();
			}
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x000C27E8 File Offset: 0x000C09E8
		public static EquippedType GetEquipedSlot(ItemType itemType)
		{
			for (EquippedType equippedType = EquippedType.EQUIPPED_01; equippedType < EquippedType.NUM; equippedType++)
			{
				if (ItemSetUI.SaveDataInterface.GetEquipedItemType(equippedType) == itemType)
				{
					return equippedType;
				}
			}
			return EquippedType.NUM;
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x000C2818 File Offset: 0x000C0A18
		public static void SetEquipedItemId(EquippedType slot, int id)
		{
			ItemSetUI.SaveDataInterface.SetEquipedItemType(slot, (ItemType)id);
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x000C2824 File Offset: 0x000C0A24
		public static EquippedType GetEquipedSlot(int id)
		{
			return ItemSetUI.SaveDataInterface.GetEquipedSlot((ItemType)id);
		}
	}

	// Token: 0x0200042B RID: 1067
	public class ShopItemInfo : ShopItemData
	{
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x000C2834 File Offset: 0x000C0A34
		// (set) Token: 0x06002064 RID: 8292 RVA: 0x000C283C File Offset: 0x000C0A3C
		public int count { get; private set; }

		// Token: 0x06002065 RID: 8293 RVA: 0x000C2848 File Offset: 0x000C0A48
		public void SetCount(int count)
		{
			this.count = count;
		}
	}
}
