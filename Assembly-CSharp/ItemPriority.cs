using System;

// Token: 0x0200025D RID: 605
public class ItemPriority
{
	// Token: 0x06001064 RID: 4196 RVA: 0x0005FFAC File Offset: 0x0005E1AC
	public static int GetItemPriority(ItemType type)
	{
		if (type < ItemType.NUM)
		{
			return ItemPriority.ITEM_PRIORITY[(int)type];
		}
		return 8;
	}

	// Token: 0x04000EDA RID: 3802
	private static readonly int[] ITEM_PRIORITY = new int[]
	{
		6,
		7,
		3,
		4,
		5,
		0,
		2,
		1
	};
}
