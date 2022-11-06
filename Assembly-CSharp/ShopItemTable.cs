using System;
using Text;

// Token: 0x02000428 RID: 1064
public class ShopItemTable
{
	// Token: 0x0600204E RID: 8270 RVA: 0x000C23B0 File Offset: 0x000C05B0
	public static ShopItemData[] GetDataTable()
	{
		if (ShopItemTable.m_shopItemDataTable == null)
		{
			ShopItemTable.m_shopItemDataTable = new ShopItemData[8];
			for (int i = 0; i < ShopItemTable.m_shopItemDataTable.Length; i++)
			{
				int num = i + 1;
				ShopItemTable.m_shopItemDataTable[i] = new ShopItemData();
				ShopItemTable.m_shopItemDataTable[i].number = num;
				ShopItemTable.m_shopItemDataTable[i].rings = 100;
				ShopItemTable.m_shopItemDataTable[i].SetName(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ShopItem", "name" + num).text);
				ShopItemTable.m_shopItemDataTable[i].SetDetails(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ShopItem", "details" + num).text);
			}
		}
		return ShopItemTable.m_shopItemDataTable;
	}

	// Token: 0x0600204F RID: 8271 RVA: 0x000C2474 File Offset: 0x000C0674
	public static ShopItemData GetShopItemData(int id)
	{
		foreach (ShopItemData shopItemData in ShopItemTable.GetDataTable())
		{
			if (shopItemData.id == id)
			{
				return shopItemData;
			}
		}
		return null;
	}

	// Token: 0x06002050 RID: 8272 RVA: 0x000C24B0 File Offset: 0x000C06B0
	public static ShopItemData GetShopItemDataOfIndex(int index)
	{
		return ShopItemTable.GetDataTable()[index];
	}

	// Token: 0x04001D0D RID: 7437
	private static ShopItemData[] m_shopItemDataTable;
}
