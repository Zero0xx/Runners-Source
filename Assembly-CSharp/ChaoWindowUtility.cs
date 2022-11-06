using System;
using DataTable;

// Token: 0x02000359 RID: 857
public class ChaoWindowUtility
{
	// Token: 0x06001973 RID: 6515 RVA: 0x000943A8 File Offset: 0x000925A8
	public static int GetIdFromServerId(int serverId)
	{
		int num = 400000;
		return serverId - num;
	}

	// Token: 0x06001974 RID: 6516 RVA: 0x000943C0 File Offset: 0x000925C0
	public static string GetChaoSpriteName(int serverChaoId)
	{
		int idFromServerId = ChaoWindowUtility.GetIdFromServerId(serverChaoId);
		return string.Format("ui_tex_chao_{0:D4}", idFromServerId);
	}

	// Token: 0x06001975 RID: 6517 RVA: 0x000943E8 File Offset: 0x000925E8
	public static string GetChaoLabelName(int serverChaoId)
	{
		int idFromServerId = ChaoWindowUtility.GetIdFromServerId(serverChaoId);
		return string.Format("name{0:D4}", idFromServerId);
	}

	// Token: 0x06001976 RID: 6518 RVA: 0x00094410 File Offset: 0x00092610
	public static void ChangeRaritySpriteFromServerChaoId(UISprite sprite, int serverChaoId)
	{
		int clientChaoId = serverChaoId - 400000;
		ChaoWindowUtility.ChangeRaritySrptieFromClientChaoId(sprite, clientChaoId);
	}

	// Token: 0x06001977 RID: 6519 RVA: 0x0009442C File Offset: 0x0009262C
	public static void ChangeRaritySrptieFromClientChaoId(UISprite sprite, int clientChaoId)
	{
		ChaoData chaoData = ChaoTable.GetChaoData(clientChaoId);
		if (chaoData != null)
		{
			int rarity = (int)chaoData.rarity;
			ChaoWindowUtility.ChangeRaritySpriteFromRarity(sprite, rarity);
		}
	}

	// Token: 0x06001978 RID: 6520 RVA: 0x00094454 File Offset: 0x00092654
	public static void ChangeRaritySpriteFromRarity(UISprite sprite, int rarity)
	{
		if (sprite == null)
		{
			return;
		}
		sprite.spriteName = "ui_chao_set_bg_ll_" + rarity.ToString();
	}

	// Token: 0x06001979 RID: 6521 RVA: 0x00094488 File Offset: 0x00092688
	public static void PlaySEChaoBtrth(int chaoId, int rarity)
	{
		if (EventManager.Instance != null && EventCommonDataTable.Instance != null && EventManager.Instance.Type != EventManager.EventType.UNKNOWN && EventCommonDataTable.Instance.IsRouletteEventChao(chaoId))
		{
			string cueSheetName = "SE_" + EventManager.GetEventTypeName(EventManager.Instance.Type);
			if (rarity == 2)
			{
				SoundManager.SePlay("sys_chao_birthS", cueSheetName);
			}
			else
			{
				SoundManager.SePlay("sys_chao_birth", cueSheetName);
			}
			return;
		}
		if (rarity == 2)
		{
			SoundManager.SePlay("sys_chao_birthS", "SE");
		}
		else
		{
			SoundManager.SePlay("sys_chao_birth", "SE");
		}
	}

	// Token: 0x0600197A RID: 6522 RVA: 0x00094540 File Offset: 0x00092740
	public static void PlaySEChaoUnite(int chaoId)
	{
		if (EventManager.Instance != null && EventCommonDataTable.Instance != null && EventManager.Instance.Type != EventManager.EventType.UNKNOWN && EventCommonDataTable.Instance.IsRouletteEventChao(chaoId))
		{
			string cueSheetName = "SE_" + EventManager.GetEventTypeName(EventManager.Instance.Type);
			SoundManager.SePlay("sys_chao_unite", cueSheetName);
			return;
		}
		SoundManager.SePlay("sys_chao_unite", "SE");
	}

	// Token: 0x040016D8 RID: 5848
	public static readonly string SeHatch = "SeHatch";

	// Token: 0x040016D9 RID: 5849
	public static readonly string SeBreak = "SeBreak";

	// Token: 0x040016DA RID: 5850
	public static readonly string SeSpEgg = "SeSpEgg";
}
