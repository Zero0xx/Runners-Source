using System;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

// Token: 0x0200041C RID: 1052
public class ItemSet : MonoBehaviour
{
	// Token: 0x06001FA7 RID: 8103 RVA: 0x000BBDCC File Offset: 0x000B9FCC
	private void Awake()
	{
		for (int i = 0; i < this.m_itemType.Length; i++)
		{
			this.m_itemType[i] = ItemType.UNKNOWN;
		}
	}

	// Token: 0x06001FA8 RID: 8104 RVA: 0x000BBDFC File Offset: 0x000B9FFC
	private void Start()
	{
	}

	// Token: 0x06001FA9 RID: 8105 RVA: 0x000BBE00 File Offset: 0x000BA000
	private void Update()
	{
	}

	// Token: 0x06001FAA RID: 8106 RVA: 0x000BBE04 File Offset: 0x000BA004
	private void OnEnable()
	{
		if (this.m_window != null)
		{
			if (StageAbilityManager.Instance != null)
			{
				StageAbilityManager.Instance.RecalcAbilityVaue();
			}
			this.m_window.SetItemType(ItemType.INVINCIBLE);
		}
	}

	// Token: 0x06001FAB RID: 8107 RVA: 0x000BBE48 File Offset: 0x000BA048
	private void OnDestroy()
	{
		StageInfo stageInfo = GameObjectUtil.FindGameObjectComponent<StageInfo>("StageInfo");
		if (stageInfo != null)
		{
			for (int i = 0; i < 3; i++)
			{
				stageInfo.EquippedItems[i] = this.m_itemType[i];
			}
		}
	}

	// Token: 0x06001FAC RID: 8108 RVA: 0x000BBE90 File Offset: 0x000BA090
	public void Setup()
	{
		this.SetupItem();
	}

	// Token: 0x06001FAD RID: 8109 RVA: 0x000BBE98 File Offset: 0x000BA098
	public void ResetCheckMark()
	{
		for (int i = 0; i < 3; i++)
		{
			this.m_itemType[i] = ItemType.UNKNOWN;
			this.m_enableColor[i] = true;
		}
		foreach (ItemButton itemButton in this.m_buttons)
		{
			if (!(itemButton == null))
			{
				ItemButton.CursorColor cursorColor = itemButton.GetCursorColor();
				if (cursorColor != ItemButton.CursorColor.NONE)
				{
					itemButton.RemoveCursor();
				}
				itemButton.SetButtonActive(true);
			}
		}
		if (this.m_window != null)
		{
			this.m_window.SetWindowActive();
			this.m_window.SetEquipMark(false);
		}
	}

	// Token: 0x06001FAE RID: 8110 RVA: 0x000BBF70 File Offset: 0x000BA170
	public void SetupEquipItem()
	{
		if (this.m_window != null)
		{
			this.m_window.SetItemType(ItemType.INVINCIBLE);
			this.m_window.SetWindowActive();
		}
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			PlayerData playerData = instance.PlayerData;
			if (playerData != null)
			{
				for (int i = 0; i < 3; i++)
				{
					ItemType item = playerData.EquippedItem[i];
					this.SetupEquipItemOne(i, item);
				}
			}
		}
	}

	// Token: 0x06001FAF RID: 8111 RVA: 0x000BBFEC File Offset: 0x000BA1EC
	private void SetupEquipItemOne(int equipIndex, ItemType item)
	{
		if (item == ItemType.UNKNOWN)
		{
			return;
		}
		if (this.m_window != null && !this.m_buttons[(int)item].itemLock)
		{
			this.m_window.SetWindowActive();
			this.m_window.SetEquipMark(true);
			this.m_window.SetItemType(item);
		}
		this.m_buttons[(int)item].SetCursor((ItemButton.CursorColor)equipIndex);
		if (this.m_buttons[(int)item].IsEquiped())
		{
			if (this.m_window != null)
			{
				this.m_window.SetEquipMarkColor((ItemButton.CursorColor)equipIndex);
			}
			this.m_itemType[equipIndex] = item;
			this.m_enableColor[equipIndex] = false;
		}
		this.SetButtonActive();
	}

	// Token: 0x06001FB0 RID: 8112 RVA: 0x000BC0AC File Offset: 0x000BA2AC
	public void UpdateView()
	{
		if (this.m_window != null)
		{
			this.m_window.UpdateView();
		}
	}

	// Token: 0x06001FB1 RID: 8113 RVA: 0x000BC0CC File Offset: 0x000BA2CC
	public void UpdateFreeItemList(ServerFreeItemState freeItemState)
	{
		List<ServerItemState> itemList = freeItemState.itemList;
		foreach (ItemButton itemButton in this.m_buttons)
		{
			if (!(itemButton == null))
			{
				for (int i = 0; i < itemList.Count; i++)
				{
					if (itemButton.itemType == itemList[i].GetItem().itemType)
					{
						itemButton.UpdateFreeItemCount(itemList[i].m_num);
					}
				}
			}
		}
	}

	// Token: 0x06001FB2 RID: 8114 RVA: 0x000BC18C File Offset: 0x000BA38C
	private void SetupItem()
	{
		for (int i = 0; i < 3; i++)
		{
			this.m_itemType[i] = ItemType.UNKNOWN;
			this.m_enableColor[i] = true;
		}
		GameObject itemSetRootObject = ItemSetUtility.GetItemSetRootObject();
		this.m_window = GameObjectUtil.FindChildGameObjectComponent<ItemWindow>(itemSetRootObject, "info_pla");
		this.m_window.SetItemBuyCallback(new ItemWindow.ItemBuyCallback(this.ItemBuyCallback));
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "slot_bg");
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "slot_item");
		if (gameObject != null && gameObject2 != null)
		{
			int childCount = gameObject.transform.childCount;
			int childCount2 = gameObject2.transform.childCount;
			if (childCount == childCount2)
			{
				int num = 8;
				this.m_buttons.Clear();
				for (int j = 0; j < num; j++)
				{
					Transform child = gameObject2.transform.GetChild(j);
					if (!(child == null))
					{
						GameObject gameObject3 = child.gameObject;
						if (!(gameObject3 == null))
						{
							ItemButton itemButton = gameObject3.GetComponent<ItemButton>();
							if (itemButton == null)
							{
								itemButton = gameObject3.AddComponent<ItemButton>();
							}
							itemButton.Setup((ItemType)j, gameObject.transform.GetChild(j).gameObject);
							itemButton.SetCallback(new ItemButton.ClickCallback(this.ClickButtonCallback));
							this.m_buttons.Add(itemButton);
						}
					}
				}
				int num2 = childCount;
				if (num < num2)
				{
					for (int k = num; k < num2; k++)
					{
						Transform child2 = gameObject2.transform.GetChild(k);
						if (child2 != null)
						{
							GameObject gameObject4 = child2.gameObject;
							if (gameObject4 != null)
							{
								gameObject4.SetActive(false);
							}
						}
						Transform child3 = gameObject.transform.GetChild(k);
						if (child3 != null)
						{
							GameObject gameObject5 = child3.gameObject;
							if (gameObject5 != null)
							{
								gameObject5.SetActive(false);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06001FB3 RID: 8115 RVA: 0x000BC39C File Offset: 0x000BA59C
	private void ClickButtonCallback(ItemType itemType, bool isEquiped)
	{
		if (this.m_window != null)
		{
			this.m_window.SetWindowActive();
			this.m_window.SetEquipMark(isEquiped);
			this.m_window.SetItemType(itemType);
		}
		if (isEquiped)
		{
			int num = 0;
			for (int i = 0; i < 3; i++)
			{
				if (this.m_enableColor[i])
				{
					num = i;
					this.m_enableColor[i] = false;
					break;
				}
			}
			ItemButton.CursorColor cursorColor = (ItemButton.CursorColor)num;
			this.m_buttons[(int)itemType].SetCursor(cursorColor);
			if (this.m_window != null)
			{
				this.m_window.SetEquipMarkColor(cursorColor);
			}
			this.m_itemType[num] = itemType;
			SaveDataManager instance = SaveDataManager.Instance;
			if (instance != null)
			{
				PlayerData playerData = instance.PlayerData;
				if (playerData != null)
				{
					playerData.EquippedItem[num] = itemType;
				}
			}
		}
		else
		{
			ItemButton.CursorColor cursorColor2 = this.m_buttons[(int)itemType].GetCursorColor();
			if (cursorColor2 != ItemButton.CursorColor.NONE)
			{
				this.m_enableColor[(int)cursorColor2] = true;
				this.m_itemType[(int)cursorColor2] = ItemType.UNKNOWN;
				this.m_buttons[(int)itemType].RemoveCursor();
				SaveDataManager instance2 = SaveDataManager.Instance;
				if (instance2 != null)
				{
					PlayerData playerData2 = instance2.PlayerData;
					if (playerData2 != null)
					{
						playerData2.EquippedItem[(int)cursorColor2] = ItemType.UNKNOWN;
					}
				}
			}
		}
		this.SetButtonActive();
	}

	// Token: 0x06001FB4 RID: 8116 RVA: 0x000BC4F4 File Offset: 0x000BA6F4
	private void SetButtonActive()
	{
		int num = 0;
		foreach (ItemButton itemButton in this.m_buttons)
		{
			if (!(itemButton == null))
			{
				if (itemButton.IsEquiped())
				{
					num++;
				}
			}
		}
		if (num >= 3)
		{
			foreach (ItemButton itemButton2 in this.m_buttons)
			{
				if (!(itemButton2 == null))
				{
					if (!itemButton2.IsEquiped())
					{
						itemButton2.SetButtonActive(false);
					}
				}
			}
		}
		else
		{
			foreach (ItemButton itemButton3 in this.m_buttons)
			{
				if (!(itemButton3 == null))
				{
					itemButton3.SetButtonActive(true);
				}
			}
		}
	}

	// Token: 0x06001FB5 RID: 8117 RVA: 0x000BC660 File Offset: 0x000BA860
	private void ItemBuyCallback(ItemType itemType)
	{
		ItemButton itemButton = this.m_buttons[(int)itemType];
		if (itemButton == null)
		{
			return;
		}
		itemButton.UpdateItemCount();
	}

	// Token: 0x06001FB6 RID: 8118 RVA: 0x000BC690 File Offset: 0x000BA890
	public ItemType[] GetItem()
	{
		return this.m_itemType;
	}

	// Token: 0x04001C9A RID: 7322
	private List<ItemButton> m_buttons = new List<ItemButton>();

	// Token: 0x04001C9B RID: 7323
	private ItemWindow m_window;

	// Token: 0x04001C9C RID: 7324
	private bool[] m_enableColor = new bool[3];

	// Token: 0x04001C9D RID: 7325
	private ItemType[] m_itemType = new ItemType[3];
}
