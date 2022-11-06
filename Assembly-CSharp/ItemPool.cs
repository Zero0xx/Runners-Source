using System;
using SaveData;

// Token: 0x0200033C RID: 828
public static class ItemPool
{
	// Token: 0x0600189C RID: 6300 RVA: 0x0008E558 File Offset: 0x0008C758
	public static void SetItemCount(ItemType i_item_type, uint i_count)
	{
		if (i_item_type < ItemType.NUM)
		{
			ItemPool.m_item_data.item_count[(int)((UIntPtr)i_item_type)] = i_count;
			if (ItemPool.m_item_data.item_count[(int)((UIntPtr)i_item_type)] > 99U)
			{
				ItemPool.m_item_data.item_count[(int)((UIntPtr)i_item_type)] = 99U;
			}
		}
	}

	// Token: 0x0600189D RID: 6301 RVA: 0x0008E5A0 File Offset: 0x0008C7A0
	public static uint GetItemCount(ItemType i_item_type)
	{
		if (i_item_type < ItemType.NUM)
		{
			return ItemPool.m_item_data.item_count[(int)((UIntPtr)i_item_type)];
		}
		return 0U;
	}

	// Token: 0x170003B7 RID: 951
	// (get) Token: 0x0600189E RID: 6302 RVA: 0x0008E5B8 File Offset: 0x0008C7B8
	// (set) Token: 0x0600189F RID: 6303 RVA: 0x0008E5C0 File Offset: 0x0008C7C0
	public static uint RingCount
	{
		get
		{
			return ItemPool.m_ring_count;
		}
		set
		{
			ItemPool.m_ring_count = value;
			if (ItemPool.m_ring_count > 9999999U)
			{
				ItemPool.m_ring_count = 9999999U;
			}
		}
	}

	// Token: 0x170003B8 RID: 952
	// (get) Token: 0x060018A0 RID: 6304 RVA: 0x0008E5E4 File Offset: 0x0008C7E4
	// (set) Token: 0x060018A1 RID: 6305 RVA: 0x0008E5EC File Offset: 0x0008C7EC
	public static uint RedRingCount
	{
		get
		{
			return ItemPool.m_red_ring_count;
		}
		set
		{
			ItemPool.m_red_ring_count = value;
			if (ItemPool.m_red_ring_count > 9999999U)
			{
				ItemPool.m_red_ring_count = 9999999U;
			}
		}
	}

	// Token: 0x060018A2 RID: 6306 RVA: 0x0008E610 File Offset: 0x0008C810
	public static void Initialize()
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			SaveData.ItemData itemData = instance.ItemData;
			ItemPool.RingCount = itemData.RingCount;
			ItemPool.RedRingCount = itemData.RedRingCount;
			for (uint num = 0U; num < 8U; num += 1U)
			{
				ItemType itemType = (ItemType)num;
				ItemPool.SetItemCount(itemType, itemData.GetItemCount(itemType));
			}
		}
	}

	// Token: 0x060018A3 RID: 6307 RVA: 0x0008E670 File Offset: 0x0008C870
	public static void ReflctSaveData()
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			SaveData.ItemData itemData = instance.ItemData;
			itemData.RingCount = ItemPool.m_ring_count;
			itemData.RedRingCount = ItemPool.m_red_ring_count;
			for (uint num = 0U; num < 8U; num += 1U)
			{
				itemData.SetItemCount((ItemType)num, ItemPool.m_item_data.item_count[(int)((UIntPtr)num)]);
			}
			instance.ItemData = itemData;
			instance.SaveItemData();
		}
	}

	// Token: 0x0400160A RID: 5642
	public const uint MAX_ITEM_COUNT = 99U;

	// Token: 0x0400160B RID: 5643
	public const uint MAX_RING_COUNT = 9999999U;

	// Token: 0x0400160C RID: 5644
	public const uint MAX_REDRING_COUNT = 9999999U;

	// Token: 0x0400160D RID: 5645
	private static ItemPool.ItemData m_item_data = new ItemPool.ItemData();

	// Token: 0x0400160E RID: 5646
	private static uint m_ring_count = 0U;

	// Token: 0x0400160F RID: 5647
	private static uint m_red_ring_count = 0U;

	// Token: 0x0200033D RID: 829
	private class ItemData
	{
		// Token: 0x060018A4 RID: 6308 RVA: 0x0008E6E0 File Offset: 0x0008C8E0
		public ItemData()
		{
			uint num = 8U;
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				this.item_count[(int)((UIntPtr)num2)] = 0U;
			}
		}

		// Token: 0x04001610 RID: 5648
		public uint[] item_count = new uint[8];
	}
}
