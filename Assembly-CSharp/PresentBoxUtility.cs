using System;
using System.Collections.Generic;
using Text;

// Token: 0x020004E5 RID: 1253
public class PresentBoxUtility
{
	// Token: 0x06002557 RID: 9559 RVA: 0x000E1348 File Offset: 0x000DF548
	public static string GetItemSpriteName(ServerItem serverItem)
	{
		string result = string.Empty;
		ServerItem.IdType idType = serverItem.idType;
		switch (idType)
		{
		case ServerItem.IdType.ROULLETE_TOKEN:
			result = "ui_cmn_icon_item_210000";
			break;
		case ServerItem.IdType.EGG_ITEM:
			break;
		case ServerItem.IdType.PREMIUM_ROULLETE_TICKET:
			result = "ui_cmn_icon_item_230000";
			break;
		case ServerItem.IdType.ITEM_ROULLETE_TICKET:
			result = "ui_cmn_icon_item_240000";
			break;
		default:
			switch (idType)
			{
			case ServerItem.IdType.RSRING:
				result = "ui_cmn_icon_item_9";
				break;
			case ServerItem.IdType.RING:
				result = "ui_cmn_icon_item_8";
				break;
			case ServerItem.IdType.ENERGY:
				result = "ui_cmn_icon_item_920000";
				break;
			default:
				if (idType != ServerItem.IdType.EQUIP_ITEM)
				{
					if (idType == ServerItem.IdType.CHAO)
					{
						result = "ui_tex_chao_" + serverItem.idIndex.ToString("D4");
					}
				}
				else
				{
					result = "ui_cmn_icon_item_" + serverItem.idIndex.ToString();
				}
				break;
			case ServerItem.IdType.RAIDRING:
				result = "ui_cmn_icon_item_960000";
				break;
			}
			break;
		case ServerItem.IdType.CHARA:
			result = "ui_tex_player_" + serverItem.idIndex.ToString("D2") + "_" + CharaName.PrefixName[(int)serverItem.charaType];
			break;
		}
		return result;
	}

	// Token: 0x06002558 RID: 9560 RVA: 0x000E149C File Offset: 0x000DF69C
	public static string GetItemName(ServerItem serverItem)
	{
		return serverItem.serverItemName;
	}

	// Token: 0x06002559 RID: 9561 RVA: 0x000E14A8 File Offset: 0x000DF6A8
	public static string GetItemInfo(PresentBoxUI.PresentInfo info)
	{
		string result = string.Empty;
		if (info != null)
		{
			switch (info.messageType)
			{
			case ServerMessageEntry.MessageType.SendEnergy:
				result = TextUtility.GetCommonText("PresentBox", "present_from_friend", "{FRIEND_NAME}", info.name);
				break;
			case ServerMessageEntry.MessageType.ReturnSendEnergy:
				result = TextUtility.GetCommonText("PresentBox", "remuneration_friend_present");
				break;
			case ServerMessageEntry.MessageType.InviteCode:
				result = TextUtility.GetCommonText("PresentBox", "remuneration_friend_invite");
				break;
			}
		}
		return result;
	}

	// Token: 0x0600255A RID: 9562 RVA: 0x000E1530 File Offset: 0x000DF730
	public static string GetReceivedTime(int expireTime)
	{
		string result = string.Empty;
		if (expireTime == 0)
		{
			result = TextUtility.GetCommonText("PresentBox", "unlimited_duration");
		}
		else
		{
			int num = expireTime - NetUtil.GetCurrentUnixTime();
			if (num >= 86400)
			{
				result = TextUtility.GetCommonText("PresentBox", "expire_days", "{DAYS}", (num / 86400).ToString());
			}
			else if (num >= 3600)
			{
				result = TextUtility.GetCommonText("PresentBox", "expire_hours", "{HOURS}", (num / 3600).ToString());
			}
			else
			{
				result = TextUtility.GetCommonText("PresentBox", "expire_minutes", "{MINUTES}", (num / 60).ToString());
			}
		}
		return result;
	}

	// Token: 0x0600255B RID: 9563 RVA: 0x000E15F4 File Offset: 0x000DF7F4
	public static bool IsWithinTimeLimit(int expireTime)
	{
		int num = expireTime - NetUtil.GetCurrentUnixTime();
		return num > 0;
	}

	// Token: 0x0600255C RID: 9564 RVA: 0x000E1610 File Offset: 0x000DF810
	public static string GetPresetTextList(List<ServerPresentState> presentStateList)
	{
		string text = string.Empty;
		foreach (ServerPresentState serverPresentState in presentStateList)
		{
			ServerItem serverItem = new ServerItem((ServerItem.Id)serverPresentState.m_itemId);
			string itemName = PresentBoxUtility.GetItemName(serverItem);
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				itemName,
				"×",
				serverPresentState.m_numItem.ToString(),
				"\n"
			});
		}
		return text;
	}
}
