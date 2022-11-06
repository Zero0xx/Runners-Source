using System;
using System.Collections.Generic;
using AnimationOrTween;
using Message;
using Text;
using UnityEngine;

// Token: 0x0200042D RID: 1069
public class ItemSetWindowEquipUI : MonoBehaviour
{
	// Token: 0x06002074 RID: 8308 RVA: 0x000C2DF0 File Offset: 0x000C0FF0
	public void OpenWindow(int id, int count)
	{
		this.m_id = id;
		this.m_count = count;
		SoundManager.SePlay("sys_window_open", "SE");
		this.UpdateView();
		Animation animation = GameObjectUtil.FindGameObjectComponent<Animation>("item_set_window_equip");
		if (animation != null)
		{
			ActiveAnimation.Play(animation, "ui_cmn_window_Anim", Direction.Forward, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
		}
	}

	// Token: 0x06002075 RID: 8309 RVA: 0x000C2E48 File Offset: 0x000C1048
	private void OnClickClose()
	{
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x06002076 RID: 8310 RVA: 0x000C2E5C File Offset: 0x000C105C
	private static int GetMainCharaItemAbilityLevel(ItemType itemType)
	{
		AbilityType abilityType = StageItemManager.s_dicItemTypeToCharAbilityType[itemType];
		SaveDataManager instance = SaveDataManager.Instance;
		CharaType mainChara = instance.PlayerData.MainChara;
		CharaAbility charaAbility = instance.CharaData.AbilityArray[(int)mainChara];
		return (int)(((ulong)abilityType >= (ulong)((long)charaAbility.Ability.Length)) ? 0U : charaAbility.Ability[(int)abilityType]);
	}

	// Token: 0x06002077 RID: 8311 RVA: 0x000C2EB4 File Offset: 0x000C10B4
	private void UpdateView()
	{
		ShopItemData shopItemData = ShopItemTable.GetShopItemData(this.m_id);
		if (shopItemData != null)
		{
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbt_item_name");
			if (uilabel != null)
			{
				uilabel.text = shopItemData.name;
			}
			UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbt_item_effect");
			if (uilabel2 != null)
			{
				uilabel2.text = ItemSetWindowEquipUI.GetItemDetailsText(shopItemData);
			}
		}
		ui_item_set_cell ui_item_set_cell = GameObjectUtil.FindChildGameObjectComponent<ui_item_set_cell>(base.gameObject, "ui_item_set_cell(Clone)");
		if (ui_item_set_cell != null)
		{
			ui_item_set_cell.UpdateView(this.m_id, this.m_count);
		}
		for (EquippedType equippedType = EquippedType.EQUIPPED_01; equippedType < EquippedType.NUM; equippedType++)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_off_" + (int)(equippedType + 1));
			if (gameObject != null)
			{
				gameObject.SetActive(ItemSetUI.SaveDataInterface.GetEquipedItemType(equippedType) == (ItemType)this.m_id);
			}
		}
	}

	// Token: 0x06002078 RID: 8312 RVA: 0x000C2FB0 File Offset: 0x000C11B0
	private void OnClickEquipSlot0()
	{
		this.EquipItem(EquippedType.EQUIPPED_01);
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x06002079 RID: 8313 RVA: 0x000C2FCC File Offset: 0x000C11CC
	private void OnClickEquipSlot1()
	{
		this.EquipItem(EquippedType.EQUIPPED_02);
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x0600207A RID: 8314 RVA: 0x000C2FE8 File Offset: 0x000C11E8
	private void OnClickEquipSlot2()
	{
		this.EquipItem(EquippedType.EQUIPPED_03);
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x0600207B RID: 8315 RVA: 0x000C3004 File Offset: 0x000C1204
	private void OnClickUnequipSlot0()
	{
		this.UnequipItem(EquippedType.EQUIPPED_01);
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x0600207C RID: 8316 RVA: 0x000C3020 File Offset: 0x000C1220
	private void OnClickUnequipSlot1()
	{
		this.UnequipItem(EquippedType.EQUIPPED_02);
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x0600207D RID: 8317 RVA: 0x000C303C File Offset: 0x000C123C
	private void OnClickUnequipSlot2()
	{
		this.UnequipItem(EquippedType.EQUIPPED_03);
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x0600207E RID: 8318 RVA: 0x000C3058 File Offset: 0x000C1258
	private void OnClickToBuy()
	{
		ItemSetWindowBuyUI itemSetWindowBuyUI = GameObjectUtil.FindGameObjectComponent<ItemSetWindowBuyUI>("ItemSetWindowBuyUI");
		if (itemSetWindowBuyUI != null)
		{
			itemSetWindowBuyUI.OpenWindow(this.m_id, this.m_count);
			SoundManager.SePlay("sys_menu_decide", "SE");
		}
	}

	// Token: 0x0600207F RID: 8319 RVA: 0x000C30A0 File Offset: 0x000C12A0
	private void EquipItem(EquippedType slot)
	{
		ItemType[] equipItems = this.GetEquipItems();
		EquippedType equipedSlot = ItemSetUI.SaveDataInterface.GetEquipedSlot(this.m_id);
		if (equipedSlot < EquippedType.NUM)
		{
			equipItems[(int)equipedSlot] = ItemSetUI.SaveDataInterface.GetEquipedItemType(slot);
		}
		equipItems[(int)slot] = (ItemType)this.m_id;
		this.UpdateItems(equipItems);
	}

	// Token: 0x06002080 RID: 8320 RVA: 0x000C30E0 File Offset: 0x000C12E0
	private void UnequipItem(EquippedType removeSlot)
	{
		ItemType[] equipItems = this.GetEquipItems();
		equipItems[(int)removeSlot] = ItemType.UNKNOWN;
		this.UpdateItems(equipItems);
	}

	// Token: 0x06002081 RID: 8321 RVA: 0x000C3100 File Offset: 0x000C1300
	private void UpdateItems(ItemType[] equipItems)
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			List<ItemType> list = new List<ItemType>();
			for (EquippedType equippedType = EquippedType.EQUIPPED_01; equippedType < EquippedType.NUM; equippedType++)
			{
				list.Add(equipItems[(int)equippedType]);
			}
			loggedInServerInterface.RequestServerEquipItem(list, base.gameObject);
		}
		else
		{
			this.SetEquipItems(equipItems);
			this.UpdateEquipedItemView();
		}
	}

	// Token: 0x06002082 RID: 8322 RVA: 0x000C3160 File Offset: 0x000C1360
	private void ServerEquipItem_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		this.UpdateEquipedItemView();
	}

	// Token: 0x06002083 RID: 8323 RVA: 0x000C3168 File Offset: 0x000C1368
	private void UpdateEquipedItemView()
	{
		ItemSetUI itemSetUI = GameObjectUtil.FindGameObjectComponent<ItemSetUI>("ItemSetUI");
		if (itemSetUI != null)
		{
			itemSetUI.UpdateView();
		}
		HudMenuUtility.SendMsgUpdateSaveDataDisplay();
	}

	// Token: 0x06002084 RID: 8324 RVA: 0x000C3198 File Offset: 0x000C1398
	private ItemType[] GetEquipItems()
	{
		ItemType[] array = new ItemType[3];
		for (EquippedType equippedType = EquippedType.EQUIPPED_01; equippedType < EquippedType.NUM; equippedType++)
		{
			array[(int)equippedType] = ItemSetUI.SaveDataInterface.GetEquipedItemType(equippedType);
		}
		return array;
	}

	// Token: 0x06002085 RID: 8325 RVA: 0x000C31C8 File Offset: 0x000C13C8
	private void SetEquipItems(ItemType[] equipItems)
	{
		for (EquippedType equippedType = EquippedType.EQUIPPED_01; equippedType < EquippedType.NUM; equippedType++)
		{
			ItemSetUI.SaveDataInterface.SetEquipedItemType(equippedType, equipItems[(int)equippedType]);
		}
	}

	// Token: 0x06002086 RID: 8326 RVA: 0x000C31F0 File Offset: 0x000C13F0
	public static string GetItemDetailsText(ShopItemData shopItemData)
	{
		ItemType id = (ItemType)shopItemData.id;
		return TextUtility.Replaces(shopItemData.details, new Dictionary<string, string>
		{
			{
				"{LEVEL}",
				ItemSetWindowEquipUI.GetMainCharaItemAbilityLevel(id).ToString()
			},
			{
				"{TIME}",
				((int)StageItemManager.GetItemTimeFromChara(id)).ToString("0.0")
			}
		});
	}

	// Token: 0x04001D17 RID: 7447
	private int m_id;

	// Token: 0x04001D18 RID: 7448
	private int m_count;
}
