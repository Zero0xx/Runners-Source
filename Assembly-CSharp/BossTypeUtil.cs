using System;
using SaveData;
using Text;

// Token: 0x0200085E RID: 2142
public class BossTypeUtil
{
	// Token: 0x06003A3D RID: 14909 RVA: 0x00133514 File Offset: 0x00131714
	public static SystemData.FlagStatus GetBossSaveDataFlagStatus(BossType type)
	{
		if (type < BossType.NUM)
		{
			return BossTypeUtil.BOSS_PARAMS[(int)type].m_flagStatus;
		}
		return SystemData.FlagStatus.NONE;
	}

	// Token: 0x06003A3E RID: 14910 RVA: 0x0013352C File Offset: 0x0013172C
	public static HudTutorial.Id GetBossTutorialID(BossType type)
	{
		if (type < BossType.NUM)
		{
			return BossTypeUtil.BOSS_PARAMS[(int)type].m_tutorialID;
		}
		return HudTutorial.Id.NONE;
	}

	// Token: 0x06003A3F RID: 14911 RVA: 0x00133544 File Offset: 0x00131744
	public static BossCharaType GetBossCharaType(BossType type)
	{
		if (type < BossType.NUM)
		{
			return BossTypeUtil.BOSS_PARAMS[(int)type].m_bossCharaType;
		}
		return BossCharaType.NONE;
	}

	// Token: 0x06003A40 RID: 14912 RVA: 0x0013355C File Offset: 0x0013175C
	public static BossCategory GetBossCategory(BossType type)
	{
		if (type < BossType.NUM)
		{
			return BossTypeUtil.BOSS_PARAMS[(int)type].m_bossCategory;
		}
		return BossCategory.FEVER;
	}

	// Token: 0x06003A41 RID: 14913 RVA: 0x00133574 File Offset: 0x00131774
	public static int GetLayerNumber(BossType type)
	{
		if (type < BossType.NUM)
		{
			return BossTypeUtil.BOSS_PARAMS[(int)type].m_layerNumber;
		}
		return 0;
	}

	// Token: 0x06003A42 RID: 14914 RVA: 0x0013358C File Offset: 0x0013178C
	public static int GetIndexNumber(BossType type)
	{
		if (type < BossType.NUM)
		{
			return BossTypeUtil.BOSS_PARAMS[(int)type].m_indexNumber;
		}
		return 0;
	}

	// Token: 0x06003A43 RID: 14915 RVA: 0x001335A4 File Offset: 0x001317A4
	public static string GetBossBgmName(BossType type)
	{
		if (BossTypeUtil.GetBossCharaType(type) == BossCharaType.EGGMAN)
		{
			return "BGM_boss01";
		}
		return EventBossObjectTable.GetItemData(EventBossObjectTableItem.BgmFile);
	}

	// Token: 0x06003A44 RID: 14916 RVA: 0x001335CC File Offset: 0x001317CC
	public static string GetBossBgmCueSheetName(BossType type)
	{
		if (BossTypeUtil.GetBossCharaType(type) == BossCharaType.EGGMAN)
		{
			return "bgm_z_boss01";
		}
		int num = BossTypeUtil.GetLayerNumber(type) + 1;
		return EventBossObjectTable.GetItemData(EventBossObjectTableItem.BgmCueName) + "_" + num.ToString("D2");
	}

	// Token: 0x06003A45 RID: 14917 RVA: 0x00133614 File Offset: 0x00131814
	public static string GetBossHudSpriteName(BossType type)
	{
		return "ui_gp_word_boss_" + ((int)BossTypeUtil.GetBossCharaType(type)).ToString(string.Empty);
	}

	// Token: 0x06003A46 RID: 14918 RVA: 0x00133640 File Offset: 0x00131840
	public static string GetBossHudSpriteIconName(BossType type)
	{
		return "ui_gp_gauge_boss_icon_" + ((int)BossTypeUtil.GetBossCharaType(type)).ToString(string.Empty);
	}

	// Token: 0x06003A47 RID: 14919 RVA: 0x0013366C File Offset: 0x0013186C
	public static string GetTextCommonBossName(BossType type)
	{
		if (type >= BossType.NUM)
		{
			return string.Empty;
		}
		if (BossTypeUtil.BOSS_PARAMS[(int)type].m_bossCharaType == BossCharaType.EGGMAN)
		{
			return TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "BossName", BossTypeUtil.BOSS_PARAMS[(int)type].m_name).text;
		}
		int specificId = EventManager.GetSpecificId();
		return TextManager.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "EventBossName", "bossname_" + BossTypeUtil.BOSS_PARAMS[(int)type].m_name + "_" + specificId.ToString()).text;
	}

	// Token: 0x06003A48 RID: 14920 RVA: 0x001336EC File Offset: 0x001318EC
	public static string GetTextCommonBossCharaName(BossType type)
	{
		if (type >= BossType.NUM)
		{
			return string.Empty;
		}
		if (BossTypeUtil.BOSS_PARAMS[(int)type].m_bossCharaType == BossCharaType.EGGMAN)
		{
			return TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "BossName", "eggman").text;
		}
		return TextManager.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "EventBossName", "bossname_" + EventManager.GetSpecificId().ToString()).text;
	}

	// Token: 0x06003A49 RID: 14921 RVA: 0x00133754 File Offset: 0x00131954
	public static BossType GetBossTypeRarity(int rarity)
	{
		switch (rarity)
		{
		case 0:
			return BossType.EVENT1;
		case 1:
			return BossType.EVENT2;
		case 2:
			return BossType.EVENT3;
		default:
			return BossType.EVENT1;
		}
	}

	// Token: 0x04003180 RID: 12672
	private const string HUD_SPRITENAME1 = "ui_gp_word_boss_";

	// Token: 0x04003181 RID: 12673
	private const string HUD_SPRITENAME2 = "ui_gp_gauge_boss_icon_";

	// Token: 0x04003182 RID: 12674
	private static readonly BossParam[] BOSS_PARAMS = new BossParam[]
	{
		new BossParam("eggman", SystemData.FlagStatus.TUTORIAL_FEVER_BOSS, HudTutorial.Id.FEVERBOSS, BossCharaType.EGGMAN, BossCategory.FEVER, 0, 0),
		new BossParam("eggman1", SystemData.FlagStatus.TUTORIAL_BOSS_MAP_1, HudTutorial.Id.MAPBOSS_1, BossCharaType.EGGMAN, BossCategory.MAP, 0, 0),
		new BossParam("eggman2", SystemData.FlagStatus.TUTORIAL_BOSS_MAP_2, HudTutorial.Id.MAPBOSS_2, BossCharaType.EGGMAN, BossCategory.MAP, 1, 1),
		new BossParam("eggman3", SystemData.FlagStatus.TUTORIAL_BOSS_MAP_3, HudTutorial.Id.MAPBOSS_3, BossCharaType.EGGMAN, BossCategory.MAP, 2, 2),
		new BossParam("n", SystemData.FlagStatus.NONE, HudTutorial.Id.EVENTBOSS_1, BossCharaType.EVENT, BossCategory.EVENT, 0, 0),
		new BossParam("r", SystemData.FlagStatus.NONE, HudTutorial.Id.EVENTBOSS_1, BossCharaType.EVENT, BossCategory.EVENT, 1, 0),
		new BossParam("p", SystemData.FlagStatus.NONE, HudTutorial.Id.EVENTBOSS_2, BossCharaType.EVENT, BossCategory.EVENT, 2, 1)
	};
}
