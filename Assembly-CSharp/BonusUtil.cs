using System;
using System.Collections.Generic;
using DataTable;
using Text;

// Token: 0x02000107 RID: 263
public class BonusUtil
{
	// Token: 0x060007CA RID: 1994 RVA: 0x0002E2B8 File Offset: 0x0002C4B8
	public static float GetTotalScoreBonus(float currentBonusRate, float addBonusRate)
	{
		float result;
		if (currentBonusRate == 0f)
		{
			result = addBonusRate;
		}
		else if (currentBonusRate < 0f)
		{
			if (addBonusRate > 0f)
			{
				result = currentBonusRate + addBonusRate;
			}
			else
			{
				float num = 1f + addBonusRate;
				float num2 = 1f + currentBonusRate;
				result = -1f + num2 * num;
			}
		}
		else if (addBonusRate > 0f)
		{
			float num3 = 1f - addBonusRate;
			float num4 = 1f - currentBonusRate;
			result = 1f - num4 * num3;
		}
		else
		{
			result = currentBonusRate + addBonusRate;
		}
		return result;
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x0002E34C File Offset: 0x0002C54C
	public static string GetAbilityIconSpriteName(BonusParam.BonusType type, float value)
	{
		string result = string.Empty;
		switch (type)
		{
		case BonusParam.BonusType.SCORE:
			if (BonusUtil.IsBonusMerit(type, value))
			{
				result = "ui_chao_set_ability_icon_{PARAM}".Replace("{PARAM}", "Uscore");
			}
			else
			{
				result = "ui_chao_set_ability_icon_{PARAM}".Replace("{PARAM}", "Dscore");
			}
			break;
		case BonusParam.BonusType.RING:
			if (BonusUtil.IsBonusMerit(type, value))
			{
				result = "ui_chao_set_ability_icon_{PARAM}".Replace("{PARAM}", "Uring");
			}
			else
			{
				result = "ui_chao_set_ability_icon_{PARAM}".Replace("{PARAM}", "Dring");
			}
			break;
		case BonusParam.BonusType.ANIMAL:
			if (BonusUtil.IsBonusMerit(type, value))
			{
				result = "ui_chao_set_ability_icon_{PARAM}".Replace("{PARAM}", "Uanimal");
			}
			else
			{
				result = "ui_chao_set_ability_icon_{PARAM}".Replace("{PARAM}", "Danimal");
			}
			break;
		case BonusParam.BonusType.DISTANCE:
			if (BonusUtil.IsBonusMerit(type, value))
			{
				result = "ui_chao_set_ability_icon_{PARAM}".Replace("{PARAM}", "Urange");
			}
			else
			{
				result = "ui_chao_set_ability_icon_{PARAM}".Replace("{PARAM}", "Drange");
			}
			break;
		case BonusParam.BonusType.ENEMY_OBJBREAK:
			if (BonusUtil.IsBonusMerit(type, value))
			{
				result = "ui_chao_set_ability_icon_{PARAM}".Replace("{PARAM}", "Uenemy");
			}
			else
			{
				result = "ui_chao_set_ability_icon_{PARAM}".Replace("{PARAM}", "Denemy");
			}
			break;
		case BonusParam.BonusType.TOTAL_SCORE:
			if (BonusUtil.IsBonusMerit(type, value))
			{
				result = "ui_chao_set_ability_icon_{PARAM}".Replace("{PARAM}", "Ufscore");
			}
			else
			{
				result = "ui_chao_set_ability_icon_{PARAM}".Replace("{PARAM}", "Dfscore");
			}
			break;
		case BonusParam.BonusType.SPEED:
			if (value > 100f)
			{
				result = "ui_chao_set_ability_icon_{PARAM}".Replace("{PARAM}", "Uspeed");
			}
			else
			{
				result = "ui_chao_set_ability_icon_{PARAM}".Replace("{PARAM}", "Dspeed");
			}
			break;
		}
		return result;
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x0002E548 File Offset: 0x0002C748
	private static float GetTeamDemritBonus(TeamAttribute type)
	{
		float result = 0f;
		if (type == TeamAttribute.EASY)
		{
			result = -0.2f;
		}
		return result;
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x0002E578 File Offset: 0x0002C778
	public static Dictionary<BonusParam.BonusType, bool> IsTeamBonus(CharaType charaType, List<BonusParam.BonusType> types)
	{
		Dictionary<BonusParam.BonusType, bool> dictionary = null;
		if (charaType != CharaType.UNKNOWN && charaType != CharaType.NUM && types != null)
		{
			dictionary = new Dictionary<BonusParam.BonusType, bool>();
			Dictionary<BonusParam.BonusType, float> dictionary2;
			if (BonusUtil.GetTeamBonus(charaType, out dictionary2))
			{
				for (int i = 0; i < types.Count; i++)
				{
					dictionary.Add(types[i], dictionary2.ContainsKey(types[i]));
				}
			}
		}
		return dictionary;
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x0002E5E4 File Offset: 0x0002C7E4
	public static bool IsTeamBonus(CharaType charaType, BonusParam.BonusType type)
	{
		bool result = false;
		Dictionary<BonusParam.BonusType, float> dictionary;
		if (charaType != CharaType.UNKNOWN && charaType != CharaType.NUM && type != BonusParam.BonusType.NONE && BonusUtil.GetTeamBonus(charaType, out dictionary) && dictionary.ContainsKey(type))
		{
			result = true;
		}
		return result;
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x0002E628 File Offset: 0x0002C828
	public static bool IsBonusMerit(BonusParam.BonusType type, float value)
	{
		bool result = false;
		switch (type)
		{
		case BonusParam.BonusType.SCORE:
		case BonusParam.BonusType.RING:
		case BonusParam.BonusType.ANIMAL:
		case BonusParam.BonusType.DISTANCE:
		case BonusParam.BonusType.ENEMY_OBJBREAK:
		case BonusParam.BonusType.TOTAL_SCORE:
			if (value >= 0f)
			{
				result = true;
			}
			break;
		case BonusParam.BonusType.SPEED:
			if (value < 100f)
			{
				result = true;
			}
			break;
		}
		return result;
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x0002E688 File Offset: 0x0002C888
	public static bool IsBonusMeritByOrgValue(BonusParam.BonusType type, float orgValue)
	{
		bool result = false;
		switch (type)
		{
		case BonusParam.BonusType.SCORE:
		case BonusParam.BonusType.RING:
		case BonusParam.BonusType.ANIMAL:
		case BonusParam.BonusType.DISTANCE:
		case BonusParam.BonusType.ENEMY_OBJBREAK:
		case BonusParam.BonusType.TOTAL_SCORE:
			if (orgValue >= 0f)
			{
				result = true;
			}
			break;
		case BonusParam.BonusType.SPEED:
			if (orgValue > 0f)
			{
				result = true;
			}
			break;
		}
		return result;
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x0002E6E8 File Offset: 0x0002C8E8
	public static float GetBonusParamValue(BonusParam.BonusType type, float orgValue, ref bool merit)
	{
		float result = 0f;
		switch (type)
		{
		case BonusParam.BonusType.SCORE:
		case BonusParam.BonusType.RING:
		case BonusParam.BonusType.ANIMAL:
		case BonusParam.BonusType.DISTANCE:
		case BonusParam.BonusType.ENEMY_OBJBREAK:
			result = orgValue;
			merit = (orgValue >= 0f);
			break;
		case BonusParam.BonusType.TOTAL_SCORE:
			result = orgValue * 100f;
			merit = (orgValue >= 0f);
			break;
		case BonusParam.BonusType.SPEED:
			result = 100f - orgValue;
			merit = (orgValue > 0f);
			break;
		}
		return result;
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x0002E76C File Offset: 0x0002C96C
	public static float GetBonusParamValue(BonusParam.BonusType type, float orgValue)
	{
		float result = 0f;
		switch (type)
		{
		case BonusParam.BonusType.SCORE:
		case BonusParam.BonusType.RING:
		case BonusParam.BonusType.ANIMAL:
		case BonusParam.BonusType.DISTANCE:
		case BonusParam.BonusType.ENEMY_OBJBREAK:
			result = orgValue;
			break;
		case BonusParam.BonusType.TOTAL_SCORE:
			result = orgValue * 100f;
			break;
		case BonusParam.BonusType.SPEED:
			result = 100f - orgValue;
			break;
		}
		return result;
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x0002E7CC File Offset: 0x0002C9CC
	public static string GetBonusParamText(BonusParam.BonusType type, float orgValue)
	{
		string result = string.Empty;
		bool flag = false;
		float bonusParamValue = BonusUtil.GetBonusParamValue(type, orgValue, ref flag);
		string text = string.Empty;
		switch (type)
		{
		case BonusParam.BonusType.SCORE:
		case BonusParam.BonusType.RING:
		case BonusParam.BonusType.ANIMAL:
		case BonusParam.BonusType.DISTANCE:
		case BonusParam.BonusType.ENEMY_OBJBREAK:
		case BonusParam.BonusType.TOTAL_SCORE:
			if (bonusParamValue != 0f)
			{
				if (flag)
				{
					text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "bonus", "info_type_up_" + type.ToString()).text;
				}
				else
				{
					text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "bonus", "info_type_down_" + type.ToString()).text;
				}
			}
			break;
		case BonusParam.BonusType.SPEED:
			if (bonusParamValue != 100f)
			{
				if (flag)
				{
					text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "bonus", "info_type_down_" + type.ToString()).text;
				}
				else
				{
					text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "bonus", "info_type_up_" + type.ToString()).text;
				}
			}
			break;
		}
		if (!string.IsNullOrEmpty(text))
		{
			result = text.Replace("{PARAM}", bonusParamValue.ToString());
		}
		return result;
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x0002E90C File Offset: 0x0002CB0C
	public static bool GetTeamBonus(CharaType type, out Dictionary<BonusParam.BonusType, float> bonusParam)
	{
		bonusParam = new Dictionary<BonusParam.BonusType, float>();
		if (type != CharaType.UNKNOWN && type != CharaType.NUM && CharacterDataNameInfo.Instance != null)
		{
			CharacterDataNameInfo.Info dataByID = CharacterDataNameInfo.Instance.GetDataByID(type);
			if (dataByID != null)
			{
				TeamAttribute teamAttribute = CharaTypeUtil.GetTeamAttribute(type);
				switch (dataByID.m_teamAttributeCategory)
				{
				case TeamAttributeCategory.DISTANCE:
				{
					float value = dataByID.GetTeamAttributeValue(TeamAttributeBonusType.DISTANCE);
					bonusParam.Add(BonusParam.BonusType.DISTANCE, value);
					break;
				}
				case TeamAttributeCategory.SCORE:
				{
					float value = dataByID.GetTeamAttributeValue(TeamAttributeBonusType.SCORE);
					bonusParam.Add(BonusParam.BonusType.SCORE, value);
					break;
				}
				case TeamAttributeCategory.RING:
				{
					float value = dataByID.GetTeamAttributeValue(TeamAttributeBonusType.RING);
					bonusParam.Add(BonusParam.BonusType.RING, value);
					break;
				}
				case TeamAttributeCategory.ANIMAL:
				{
					float value = dataByID.GetTeamAttributeValue(TeamAttributeBonusType.ANIMAL);
					bonusParam.Add(BonusParam.BonusType.ANIMAL, value);
					break;
				}
				case TeamAttributeCategory.ENEMY_OBJBREAK:
				{
					float value = dataByID.GetTeamAttributeValue(TeamAttributeBonusType.ENEMY);
					bonusParam.Add(BonusParam.BonusType.ENEMY_OBJBREAK, value);
					break;
				}
				case TeamAttributeCategory.EASY_SPEED:
				{
					float value = dataByID.GetTeamAttributeValue(TeamAttributeBonusType.SPEED);
					bonusParam.Add(BonusParam.BonusType.SPEED, value);
					if (BonusUtil.GetTeamDemritBonus(teamAttribute) != 0f)
					{
						value = BonusUtil.GetTeamDemritBonus(teamAttribute);
						bonusParam.Add(BonusParam.BonusType.TOTAL_SCORE, value);
					}
					break;
				}
				case TeamAttributeCategory.DISTANCE_ANIMAL:
				{
					float value = dataByID.GetTeamAttributeValue(TeamAttributeBonusType.DISTANCE);
					bonusParam.Add(BonusParam.BonusType.DISTANCE, value);
					value = dataByID.GetTeamAttributeValue(TeamAttributeBonusType.ANIMAL);
					bonusParam.Add(BonusParam.BonusType.ANIMAL, value);
					break;
				}
				case TeamAttributeCategory.LOW_SPEED_SCORE:
				{
					float value = dataByID.GetTeamAttributeValue(TeamAttributeBonusType.SCORE);
					bonusParam.Add(BonusParam.BonusType.SCORE, value);
					value = dataByID.GetTeamAttributeValue(TeamAttributeBonusType.SPEED);
					bonusParam.Add(BonusParam.BonusType.SPEED, value);
					break;
				}
				}
			}
		}
		return bonusParam.Count > 0;
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x0002EA94 File Offset: 0x0002CC94
	private static void ResetUserBelongings()
	{
		if (BonusUtil.s_charaList != null)
		{
			BonusUtil.s_charaList.Clear();
			BonusUtil.s_charaList = null;
		}
		if (BonusUtil.s_chaoList != null)
		{
			BonusUtil.s_chaoList.Clear();
			BonusUtil.s_chaoList = null;
		}
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x0002EAD8 File Offset: 0x0002CCD8
	private static void SetupUserBelongings()
	{
		ServerPlayerState playerState = ServerInterface.PlayerState;
		if (playerState != null)
		{
			int num = 29;
			if (BonusUtil.s_charaList != null)
			{
				BonusUtil.s_charaList.Clear();
				BonusUtil.s_charaList = null;
			}
			for (int i = 0; i < num; i++)
			{
				ServerCharacterState serverCharacterState = playerState.CharacterState((CharaType)i);
				if (serverCharacterState != null && serverCharacterState.IsUnlocked)
				{
					if (BonusUtil.s_charaList == null)
					{
						BonusUtil.s_charaList = new List<CharaType>();
						BonusUtil.s_charaList.Add((CharaType)i);
					}
					else
					{
						BonusUtil.s_charaList.Add((CharaType)i);
					}
				}
			}
		}
		if (BonusUtil.s_chaoList != null)
		{
			BonusUtil.s_chaoList.Clear();
			BonusUtil.s_chaoList = null;
		}
		BonusUtil.s_chaoList = ChaoTable.GetPossessionChaoData();
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x0002EB8C File Offset: 0x0002CD8C
	public static BonusParamContainer GetCurrentBonusData(CharaType charaMainType, CharaType charaSubType, int chaoMainId, int chaoSubId)
	{
		BonusParamContainer bonusParamContainer = null;
		if (charaMainType != CharaType.UNKNOWN && charaMainType != CharaType.NUM)
		{
			BonusUtil.SetupUserBelongings();
			bonusParamContainer = new BonusParamContainer();
			BonusParam currentBonusParam = BonusUtil.GetCurrentBonusParam(charaMainType, charaSubType, chaoMainId, chaoSubId);
			if (currentBonusParam != null)
			{
				bonusParamContainer.addBonus(currentBonusParam);
			}
			if (charaSubType != CharaType.UNKNOWN && charaSubType != CharaType.NUM)
			{
				currentBonusParam = BonusUtil.GetCurrentBonusParam(charaSubType, charaMainType, chaoMainId, chaoSubId);
				if (currentBonusParam != null)
				{
					bonusParamContainer.addBonus(currentBonusParam);
				}
			}
		}
		return bonusParamContainer;
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x0002EBF8 File Offset: 0x0002CDF8
	private static BonusParam GetCurrentBonusParam(CharaType charaMainType, CharaType charaSubType, int chaoMainId, int chaoSubId)
	{
		BonusParam bonusParam = null;
		if (charaMainType != CharaType.UNKNOWN && charaMainType != CharaType.NUM)
		{
			if (BonusUtil.s_chaoList == null || BonusUtil.s_charaList == null)
			{
				BonusUtil.SetupUserBelongings();
			}
			if (BonusUtil.s_charaList != null && BonusUtil.s_charaList.Count > 0)
			{
				int num = -1;
				int num2 = -1;
				for (int i = 0; i < BonusUtil.s_charaList.Count; i++)
				{
					if (BonusUtil.s_charaList[i] == charaMainType)
					{
						num = i;
						if (charaSubType == CharaType.UNKNOWN || charaSubType == CharaType.NUM || num2 != -1)
						{
							break;
						}
					}
					if (BonusUtil.s_charaList[i] == charaSubType)
					{
						num2 = i;
						if (num != -1)
						{
							break;
						}
					}
				}
				if (num >= 0)
				{
					ServerPlayerState playerState = ServerInterface.PlayerState;
					if (playerState != null)
					{
						ServerCharacterState charaSubState = null;
						ServerCharacterState serverCharacterState = playerState.CharacterState(BonusUtil.s_charaList[num]);
						if (num2 >= 0)
						{
							charaSubState = playerState.CharacterState(BonusUtil.s_charaList[num2]);
						}
						if (serverCharacterState != null)
						{
							bonusParam = new BonusParam();
							bonusParam.AddBonusChara(serverCharacterState, charaMainType, charaSubState, charaSubType);
						}
					}
				}
				if (bonusParam != null)
				{
					ChaoData chaoData = null;
					ChaoData chaoData2 = null;
					if (BonusUtil.s_chaoList != null && BonusUtil.s_chaoList.Count > 0 && (chaoMainId >= 0 || chaoSubId >= 0))
					{
						foreach (ChaoData chaoData3 in BonusUtil.s_chaoList)
						{
							if (chaoData3.id == chaoMainId)
							{
								chaoData = chaoData3;
								if (chaoData2 != null || chaoSubId < 0)
								{
									break;
								}
							}
							else if (chaoData3.id == chaoSubId)
							{
								chaoData2 = chaoData3;
								if (chaoData != null || chaoMainId < 0)
								{
									break;
								}
							}
						}
					}
					if (chaoData != null || chaoData2 != null)
					{
						if (chaoData2 == null)
						{
							bonusParam.AddBonusChao(chaoData, null);
						}
						else if (chaoData == null)
						{
							bonusParam.AddBonusChao(chaoData2, null);
						}
						else
						{
							bonusParam.AddBonusChao(chaoData, chaoData2);
						}
					}
				}
			}
		}
		return bonusParam;
	}

	// Token: 0x040005EE RID: 1518
	private const int BOUNES_OFFSET_POINT = 0;

	// Token: 0x040005EF RID: 1519
	public const float TEAM_ATTRIBUTE_DEMERIT_VALUE_EASY = -0.2f;

	// Token: 0x040005F0 RID: 1520
	public const float TEAM_ATTRIBUTE_DEMERIT_DOUBLE_VALUE_EASY = -0.36f;

	// Token: 0x040005F1 RID: 1521
	public const float TEAM_ATTRIBUTE_DEMERIT_VALUE_EASY_RATIO = 0.8f;

	// Token: 0x040005F2 RID: 1522
	public const float TEAM_ATTRIBUTE_DEMERIT_VALUE_EASY_DOUBLE_RATIO = 0.64f;

	// Token: 0x040005F3 RID: 1523
	private const string ABILITY_ICON_SPRITE_NAME_BASE = "ui_chao_set_ability_icon_{PARAM}";

	// Token: 0x040005F4 RID: 1524
	private static List<CharaType> s_charaList;

	// Token: 0x040005F5 RID: 1525
	private static List<ChaoData> s_chaoList;
}
