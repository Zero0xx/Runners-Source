using System;
using SaveData;

// Token: 0x0200025C RID: 604
public class ItemTypeName
{
	// Token: 0x0600105D RID: 4189 RVA: 0x0005FF0C File Offset: 0x0005E10C
	public static string GetItemTypeName(ItemType type)
	{
		if (type < ItemType.NUM)
		{
			return ItemTypeName.ITEM_PARAMS[(int)type].m_name;
		}
		return string.Empty;
	}

	// Token: 0x0600105E RID: 4190 RVA: 0x0005FF28 File Offset: 0x0005E128
	public static string GetItemFileName(ItemType type)
	{
		if (type < ItemType.NUM)
		{
			return ItemTypeName.ITEM_PARAMS[(int)type].m_objName;
		}
		return string.Empty;
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x0005FF44 File Offset: 0x0005E144
	public static SystemData.ItemTutorialFlagStatus GetItemTutorialStatus(ItemType type)
	{
		if (type < ItemType.NUM)
		{
			return ItemTypeName.ITEM_PARAMS[(int)type].m_flagStatus;
		}
		return SystemData.ItemTutorialFlagStatus.NONE;
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x0005FF5C File Offset: 0x0005E15C
	public static HudTutorial.Id GetItemTutorialID(ItemType type)
	{
		if (type < ItemType.NUM)
		{
			return ItemTypeName.ITEM_PARAMS[(int)type].m_tutorialID;
		}
		return HudTutorial.Id.NONE;
	}

	// Token: 0x06001061 RID: 4193 RVA: 0x0005FF74 File Offset: 0x0005E174
	public static string GetOtherItemTypeName(OtherItemType type)
	{
		if (type < OtherItemType.NUM)
		{
			return ItemTypeName.OTHERITEM_TYPES[(int)type];
		}
		return string.Empty;
	}

	// Token: 0x04000ED8 RID: 3800
	private static readonly ItemParam[] ITEM_PARAMS = new ItemParam[]
	{
		new ItemParam("INVINCIBLE", "obj_item_invincibility", SystemData.ItemTutorialFlagStatus.INVINCIBLE, HudTutorial.Id.ITEM_1),
		new ItemParam("BARRIER", "obj_item_barrier", SystemData.ItemTutorialFlagStatus.BARRIER, HudTutorial.Id.ITEM_2),
		new ItemParam("MAGNET", "obj_item_magnetbarrier", SystemData.ItemTutorialFlagStatus.MAGNET, HudTutorial.Id.ITEM_3),
		new ItemParam("TRAMPOLINE", "obj_item_trampolinefloor", SystemData.ItemTutorialFlagStatus.TRAMPOLINE, HudTutorial.Id.ITEM_4),
		new ItemParam("COMBO", "obj_item_combobounus", SystemData.ItemTutorialFlagStatus.COMBO, HudTutorial.Id.ITEM_5),
		new ItemParam("LASER", "obj_itemWisp_laser", SystemData.ItemTutorialFlagStatus.LASER, HudTutorial.Id.ITEM_6),
		new ItemParam("DRILL", "obj_itemWisp_dril", SystemData.ItemTutorialFlagStatus.DRILL, HudTutorial.Id.ITEM_7),
		new ItemParam("ASTEROID", "obj_itemWisp_asteroid", SystemData.ItemTutorialFlagStatus.ASTEROID, HudTutorial.Id.ITEM_8)
	};

	// Token: 0x04000ED9 RID: 3801
	private static readonly string[] OTHERITEM_TYPES = new string[]
	{
		"REDSTAR_RING",
		"TIMER_BRONZE",
		"TIMER_SILVER",
		"TIMER_GOLD"
	};
}
