using System;
using System.Collections.Generic;
using DataTable;

// Token: 0x02000102 RID: 258
public class BonusParam
{
	// Token: 0x060007B5 RID: 1973 RVA: 0x0002C748 File Offset: 0x0002A948
	public BonusParam()
	{
		this.Reset();
	}

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0002C758 File Offset: 0x0002A958
	public Dictionary<BonusParam.BonusTarget, List<float>> orgBonusData
	{
		get
		{
			return this.m_bonusData;
		}
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x0002C760 File Offset: 0x0002A960
	public static Dictionary<BonusParam.BonusTarget, List<float>> GetBonusDataTotal(Dictionary<BonusParam.BonusTarget, List<float>> orgDataA, Dictionary<BonusParam.BonusTarget, List<float>> orgDataB)
	{
		Dictionary<BonusParam.BonusTarget, List<float>> dictionary = null;
		if (orgDataA != null && orgDataA.Count > 0 && orgDataB != null && orgDataB.Count > 0)
		{
			dictionary = new Dictionary<BonusParam.BonusTarget, List<float>>();
			Dictionary<BonusParam.BonusTarget, List<float>>.KeyCollection keys = orgDataA.Keys;
			foreach (BonusParam.BonusTarget key in keys)
			{
				List<float> list = new List<float>();
				if (orgDataA[key] != null && orgDataB[key] != null)
				{
					for (int i = 0; i < orgDataA[key].Count; i++)
					{
						if (i == 5)
						{
							float totalScoreBonus = BonusUtil.GetTotalScoreBonus(orgDataA[key][i], orgDataB[key][i]);
							list.Add(totalScoreBonus);
						}
						else
						{
							list.Add(orgDataA[key][i] + orgDataB[key][i]);
						}
					}
				}
				dictionary.Add(key, list);
			}
		}
		else if (orgDataA != null && orgDataA.Count > 0)
		{
			dictionary = new Dictionary<BonusParam.BonusTarget, List<float>>();
			Dictionary<BonusParam.BonusTarget, List<float>>.KeyCollection keys2 = orgDataA.Keys;
			foreach (BonusParam.BonusTarget key2 in keys2)
			{
				List<float> list2 = new List<float>();
				if (orgDataA[key2] != null)
				{
					for (int j = 0; j < orgDataA[key2].Count; j++)
					{
						list2.Add(orgDataA[key2][j]);
					}
				}
				dictionary.Add(key2, list2);
			}
		}
		else if (orgDataB != null && orgDataB.Count > 0)
		{
			dictionary = new Dictionary<BonusParam.BonusTarget, List<float>>();
			Dictionary<BonusParam.BonusTarget, List<float>>.KeyCollection keys3 = orgDataB.Keys;
			foreach (BonusParam.BonusTarget key3 in keys3)
			{
				List<float> list3 = new List<float>();
				if (orgDataB[key3] != null)
				{
					for (int k = 0; k < orgDataB[key3].Count; k++)
					{
						list3.Add(orgDataB[key3][k]);
					}
				}
				dictionary.Add(key3, list3);
			}
		}
		return dictionary;
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x0002CA2C File Offset: 0x0002AC2C
	public void Reset()
	{
		if (this.m_bonusData != null)
		{
			Dictionary<BonusParam.BonusTarget, List<float>>.KeyCollection keys = this.m_bonusData.Keys;
			foreach (BonusParam.BonusTarget key in keys)
			{
				if (this.m_bonusData[key] != null)
				{
					this.m_bonusData[key].Clear();
				}
			}
			this.m_bonusData.Clear();
		}
		if (this.m_attribute != null)
		{
			this.m_attribute.Clear();
		}
		this.m_bonusData = new Dictionary<BonusParam.BonusTarget, List<float>>();
		this.m_attribute = new Dictionary<BonusParam.BonusTarget, CharacterAttribute>();
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x0002CAF8 File Offset: 0x0002ACF8
	private void SetBonusChao(ChaoData chaoData, BonusParam.BonusTarget target, CharacterAttribute charaAtribute)
	{
		if (chaoData != null && chaoData.chaoAbilitys != null && chaoData.chaoAbilitys.Length > 0)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float item = 0f;
			float item2 = 0f;
			for (int i = 0; i < chaoData.chaoAbilitys.Length; i++)
			{
				ChaoAbility chaoAbility = chaoData.chaoAbilitys[i];
				float num6;
				if (charaAtribute == chaoData.charaAtribute)
				{
					num6 = chaoData.bonusAbilityValue[chaoData.level];
				}
				else
				{
					num6 = chaoData.abilityValue[chaoData.level];
				}
				ChaoAbility chaoAbility2 = chaoAbility;
				switch (chaoAbility2)
				{
				case ChaoAbility.ALL_BONUS_COUNT:
					num += num6;
					num2 += num6;
					num3 += num6;
					num4 += num6;
					break;
				case ChaoAbility.SCORE_COUNT:
					num += num6;
					break;
				case ChaoAbility.RING_COUNT:
					num2 += num6;
					break;
				default:
					if (chaoAbility2 == ChaoAbility.ENEMY_SCORE)
					{
						num5 += num6;
					}
					break;
				case ChaoAbility.ANIMAL_COUNT:
					num3 += num6;
					break;
				case ChaoAbility.RUNNIGN_DISTANCE:
					num4 += num6;
					break;
				}
			}
			List<float> list = new List<float>();
			list.Add(num);
			list.Add(num2);
			list.Add(num3);
			list.Add(num4);
			list.Add(num5);
			list.Add(item);
			list.Add(item2);
			if (!this.m_bonusData.ContainsKey(target))
			{
				this.m_bonusData.Add(target, list);
				this.m_attribute.Add(target, chaoData.charaAtribute);
			}
			else
			{
				this.m_bonusData[target] = list;
				if (this.m_attribute.ContainsKey(target))
				{
					this.m_attribute[target] = chaoData.charaAtribute;
				}
			}
		}
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x0002CCD8 File Offset: 0x0002AED8
	public void AddBonusChao(ChaoData chaoDataMain, ChaoData chaoDataSub = null)
	{
		if (this.m_attribute != null && this.m_attribute.ContainsKey(BonusParam.BonusTarget.CHARA))
		{
			CharacterAttribute charaAtribute = this.m_attribute[BonusParam.BonusTarget.CHARA];
			this.SetBonusChao(chaoDataMain, BonusParam.BonusTarget.CHAO_MAIN, charaAtribute);
			this.SetBonusChao(chaoDataSub, BonusParam.BonusTarget.CHAO_SUB, charaAtribute);
		}
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x0002CD24 File Offset: 0x0002AF24
	public void AddBonusChara(ServerCharacterState charaMainState, CharaType mainType, ServerCharacterState charaSubState, CharaType subType)
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 0f;
			Dictionary<BonusParam.BonusType, float> dictionary;
			if (BonusUtil.GetTeamBonus(mainType, out dictionary) && dictionary != null)
			{
				Dictionary<BonusParam.BonusType, float>.KeyCollection keys = dictionary.Keys;
				foreach (BonusParam.BonusType key in keys)
				{
					switch (key)
					{
					case BonusParam.BonusType.SCORE:
						num += dictionary[key];
						break;
					case BonusParam.BonusType.RING:
						num2 += dictionary[key];
						break;
					case BonusParam.BonusType.ANIMAL:
						num3 += dictionary[key];
						break;
					case BonusParam.BonusType.DISTANCE:
						num4 += dictionary[key];
						break;
					case BonusParam.BonusType.ENEMY_OBJBREAK:
						num5 += dictionary[key];
						break;
					case BonusParam.BonusType.TOTAL_SCORE:
						num6 += dictionary[key];
						break;
					case BonusParam.BonusType.SPEED:
						num7 += dictionary[key];
						break;
					default:
						Debug.Log(" not bonus team !");
						break;
					}
				}
			}
			if (subType != CharaType.UNKNOWN && subType != CharaType.NUM && BonusUtil.GetTeamBonus(subType, out dictionary) && dictionary != null)
			{
				Dictionary<BonusParam.BonusType, float>.KeyCollection keys2 = dictionary.Keys;
				foreach (BonusParam.BonusType key2 in keys2)
				{
					switch (key2)
					{
					case BonusParam.BonusType.SCORE:
						num += dictionary[key2];
						break;
					case BonusParam.BonusType.RING:
						num2 += dictionary[key2];
						break;
					case BonusParam.BonusType.ANIMAL:
						num3 += dictionary[key2];
						break;
					case BonusParam.BonusType.DISTANCE:
						num4 += dictionary[key2];
						break;
					case BonusParam.BonusType.ENEMY_OBJBREAK:
						num5 += dictionary[key2];
						break;
					case BonusParam.BonusType.TOTAL_SCORE:
						num6 = BonusUtil.GetTotalScoreBonus(num6, dictionary[key2]);
						break;
					case BonusParam.BonusType.SPEED:
						num7 += dictionary[key2];
						break;
					default:
						Debug.Log(" not bonus team !");
						break;
					}
				}
			}
			ImportAbilityTable instance2 = ImportAbilityTable.GetInstance();
			if (instance2 != null)
			{
				num2 += instance2.GetAbilityPotential(AbilityType.RING_BONUS, charaMainState.AbilityLevel[8]);
				num3 += instance2.GetAbilityPotential(AbilityType.ANIMAL, charaMainState.AbilityLevel[10]);
				num4 += instance2.GetAbilityPotential(AbilityType.DISTANCE_BONUS, charaMainState.AbilityLevel[9]);
				Dictionary<BonusParam.BonusType, float> starBonusList = charaMainState.GetStarBonusList();
				if (starBonusList != null)
				{
					Dictionary<BonusParam.BonusType, float>.KeyCollection keys3 = starBonusList.Keys;
					foreach (BonusParam.BonusType key3 in keys3)
					{
						switch (key3)
						{
						case BonusParam.BonusType.SCORE:
							num += starBonusList[key3];
							break;
						case BonusParam.BonusType.RING:
							num2 += starBonusList[key3];
							break;
						case BonusParam.BonusType.ANIMAL:
							num3 += starBonusList[key3];
							break;
						case BonusParam.BonusType.DISTANCE:
							num4 += starBonusList[key3];
							break;
						case BonusParam.BonusType.ENEMY_OBJBREAK:
							num5 += starBonusList[key3];
							break;
						case BonusParam.BonusType.TOTAL_SCORE:
							num6 += starBonusList[key3];
							break;
						case BonusParam.BonusType.SPEED:
							num7 += starBonusList[key3];
							break;
						}
					}
				}
				if (charaSubState != null)
				{
					num2 += instance2.GetAbilityPotential(AbilityType.RING_BONUS, charaSubState.AbilityLevel[8]);
					num3 += instance2.GetAbilityPotential(AbilityType.ANIMAL, charaSubState.AbilityLevel[10]);
					num4 += instance2.GetAbilityPotential(AbilityType.DISTANCE_BONUS, charaSubState.AbilityLevel[9]);
					Dictionary<BonusParam.BonusType, float> starBonusList2 = charaSubState.GetStarBonusList();
					if (starBonusList2 != null)
					{
						Dictionary<BonusParam.BonusType, float>.KeyCollection keys4 = starBonusList2.Keys;
						foreach (BonusParam.BonusType key4 in keys4)
						{
							switch (key4)
							{
							case BonusParam.BonusType.SCORE:
								num += starBonusList2[key4];
								break;
							case BonusParam.BonusType.RING:
								num2 += starBonusList2[key4];
								break;
							case BonusParam.BonusType.ANIMAL:
								num3 += starBonusList2[key4];
								break;
							case BonusParam.BonusType.DISTANCE:
								num4 += starBonusList2[key4];
								break;
							case BonusParam.BonusType.ENEMY_OBJBREAK:
								num5 += starBonusList2[key4];
								break;
							case BonusParam.BonusType.TOTAL_SCORE:
								num6 += starBonusList2[key4];
								break;
							case BonusParam.BonusType.SPEED:
								num7 += starBonusList2[key4];
								break;
							}
						}
					}
				}
			}
			List<float> list = new List<float>();
			list.Add(num);
			list.Add(num2);
			list.Add(num3);
			list.Add(num4);
			list.Add(num5);
			list.Add(num6);
			list.Add(num7);
			if (!this.m_bonusData.ContainsKey(BonusParam.BonusTarget.CHARA))
			{
				this.m_bonusData.Add(BonusParam.BonusTarget.CHARA, list);
				this.m_attribute.Add(BonusParam.BonusTarget.CHARA, CharaTypeUtil.GetCharacterAttribute(mainType));
			}
			else
			{
				this.m_bonusData[BonusParam.BonusTarget.CHARA] = list;
				if (this.m_attribute.ContainsKey(BonusParam.BonusTarget.CHARA))
				{
					this.m_attribute[BonusParam.BonusTarget.CHARA] = CharaTypeUtil.GetCharacterAttribute(mainType);
				}
			}
		}
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x0002D34C File Offset: 0x0002B54C
	public BonusParam.BonusAffinity GetBonusAffinity(BonusParam.BonusTarget target)
	{
		BonusParam.BonusAffinity result = BonusParam.BonusAffinity.NONE;
		if (this.m_attribute.ContainsKey(BonusParam.BonusTarget.CHARA) && target != BonusParam.BonusTarget.ALL && target != BonusParam.BonusTarget.CHARA)
		{
			CharacterAttribute characterAttribute = this.m_attribute[BonusParam.BonusTarget.CHARA];
			if (this.m_attribute.ContainsKey(target) && this.m_attribute[target] == characterAttribute)
			{
				result = BonusParam.BonusAffinity.GOOD;
			}
		}
		return result;
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x0002D3AC File Offset: 0x0002B5AC
	public bool IsDetailInfo(out string detailText)
	{
		detailText = BonusParam.GetDetailInfoText(this.m_bonusData);
		return !string.IsNullOrEmpty(detailText);
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x0002D3C8 File Offset: 0x0002B5C8
	public static string GetDetailInfoText(Dictionary<BonusParam.BonusTarget, List<float>> orgBonusData)
	{
		if (orgBonusData == null || orgBonusData.Count == 0)
		{
			return string.Empty;
		}
		string text = string.Empty;
		Dictionary<BonusParam.BonusTarget, List<float>>.KeyCollection keys = orgBonusData.Keys;
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		foreach (BonusParam.BonusTarget bonusTarget in keys)
		{
			if (bonusTarget == BonusParam.BonusTarget.CHARA)
			{
				List<float> list3 = orgBonusData[bonusTarget];
				for (int i = 0; i < list3.Count; i++)
				{
					BonusParam.BonusType bonusType = (BonusParam.BonusType)i;
					float orgValue = list3[i];
					BonusParam.BonusType bonusType2 = bonusType;
					if (bonusType2 != BonusParam.BonusType.TOTAL_SCORE)
					{
						if (bonusType2 == BonusParam.BonusType.SPEED)
						{
							string bonusParamText = BonusUtil.GetBonusParamText(bonusType, orgValue);
							if (!string.IsNullOrEmpty(bonusParamText))
							{
								if (BonusUtil.IsBonusMeritByOrgValue(bonusType, orgValue))
								{
									list.Add(BonusUtil.GetBonusParamText(bonusType, orgValue));
								}
								else
								{
									list2.Add(BonusUtil.GetBonusParamText(bonusType, orgValue));
								}
							}
						}
					}
					else if (!BonusUtil.IsBonusMeritByOrgValue(bonusType, orgValue))
					{
						string bonusParamText = BonusUtil.GetBonusParamText(bonusType, orgValue);
						if (!string.IsNullOrEmpty(bonusParamText))
						{
							list2.Add(BonusUtil.GetBonusParamText(bonusType, orgValue));
						}
					}
				}
			}
		}
		if (list.Count > 0)
		{
			foreach (string str in list)
			{
				if (!string.IsNullOrEmpty(text))
				{
					text += "\n";
				}
				text += str;
			}
		}
		if (list2.Count > 0)
		{
			foreach (string str2 in list2)
			{
				if (!string.IsNullOrEmpty(text))
				{
					text += "\n";
				}
				text += str2;
			}
		}
		return text;
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x0002D628 File Offset: 0x0002B828
	public Dictionary<BonusParam.BonusType, float> GetBonusInfo(BonusParam.BonusTarget target = BonusParam.BonusTarget.ALL, bool offsetUse = true)
	{
		Dictionary<BonusParam.BonusType, float> dictionary = new Dictionary<BonusParam.BonusType, float>();
		if (target == BonusParam.BonusTarget.ALL)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 0f;
			Dictionary<BonusParam.BonusTarget, List<float>>.KeyCollection keys = this.m_bonusData.Keys;
			foreach (BonusParam.BonusTarget key in keys)
			{
				if (this.m_bonusData[key] != null && this.m_bonusData[key].Count > 0)
				{
					num += this.m_bonusData[key][0];
					num2 += this.m_bonusData[key][1];
					num3 += this.m_bonusData[key][2];
					num4 += this.m_bonusData[key][3];
					num5 += this.m_bonusData[key][4];
					num7 += this.m_bonusData[key][6];
					if (num6 == 0f)
					{
						num6 = this.m_bonusData[key][5];
					}
					else if (this.m_bonusData[key][5] != 0f)
					{
						num6 = BonusUtil.GetTotalScoreBonus(num6, this.m_bonusData[key][5]);
					}
				}
			}
			if (offsetUse)
			{
				dictionary.Add(BonusParam.BonusType.SCORE, BonusUtil.GetBonusParamValue(BonusParam.BonusType.SCORE, num));
				dictionary.Add(BonusParam.BonusType.RING, BonusUtil.GetBonusParamValue(BonusParam.BonusType.RING, num2));
				dictionary.Add(BonusParam.BonusType.ANIMAL, BonusUtil.GetBonusParamValue(BonusParam.BonusType.ANIMAL, num3));
				dictionary.Add(BonusParam.BonusType.DISTANCE, BonusUtil.GetBonusParamValue(BonusParam.BonusType.DISTANCE, num4));
				dictionary.Add(BonusParam.BonusType.ENEMY_OBJBREAK, BonusUtil.GetBonusParamValue(BonusParam.BonusType.ENEMY_OBJBREAK, num5));
				dictionary.Add(BonusParam.BonusType.SPEED, BonusUtil.GetBonusParamValue(BonusParam.BonusType.SPEED, num7));
				dictionary.Add(BonusParam.BonusType.TOTAL_SCORE, BonusUtil.GetBonusParamValue(BonusParam.BonusType.TOTAL_SCORE, num6));
			}
			else
			{
				dictionary.Add(BonusParam.BonusType.SCORE, num);
				dictionary.Add(BonusParam.BonusType.RING, num2);
				dictionary.Add(BonusParam.BonusType.ANIMAL, num3);
				dictionary.Add(BonusParam.BonusType.DISTANCE, num4);
				dictionary.Add(BonusParam.BonusType.ENEMY_OBJBREAK, num5);
				dictionary.Add(BonusParam.BonusType.SPEED, num7);
				dictionary.Add(BonusParam.BonusType.TOTAL_SCORE, num6);
			}
		}
		else if (this.m_bonusData != null && this.m_bonusData.ContainsKey(target))
		{
			if (offsetUse)
			{
				dictionary.Add(BonusParam.BonusType.SCORE, BonusUtil.GetBonusParamValue(BonusParam.BonusType.SCORE, this.m_bonusData[target][0]));
				dictionary.Add(BonusParam.BonusType.RING, BonusUtil.GetBonusParamValue(BonusParam.BonusType.RING, this.m_bonusData[target][1]));
				dictionary.Add(BonusParam.BonusType.ANIMAL, BonusUtil.GetBonusParamValue(BonusParam.BonusType.SCORE, this.m_bonusData[target][2]));
				dictionary.Add(BonusParam.BonusType.DISTANCE, BonusUtil.GetBonusParamValue(BonusParam.BonusType.SCORE, this.m_bonusData[target][3]));
				dictionary.Add(BonusParam.BonusType.ENEMY_OBJBREAK, BonusUtil.GetBonusParamValue(BonusParam.BonusType.SCORE, this.m_bonusData[target][4]));
				dictionary.Add(BonusParam.BonusType.SPEED, BonusUtil.GetBonusParamValue(BonusParam.BonusType.SCORE, this.m_bonusData[target][6]));
				dictionary.Add(BonusParam.BonusType.TOTAL_SCORE, BonusUtil.GetBonusParamValue(BonusParam.BonusType.SCORE, this.m_bonusData[target][5]));
			}
			else
			{
				dictionary.Add(BonusParam.BonusType.SCORE, this.m_bonusData[target][0]);
				dictionary.Add(BonusParam.BonusType.RING, this.m_bonusData[target][1]);
				dictionary.Add(BonusParam.BonusType.ANIMAL, this.m_bonusData[target][2]);
				dictionary.Add(BonusParam.BonusType.DISTANCE, this.m_bonusData[target][3]);
				dictionary.Add(BonusParam.BonusType.ENEMY_OBJBREAK, this.m_bonusData[target][4]);
				dictionary.Add(BonusParam.BonusType.SPEED, this.m_bonusData[target][6]);
				dictionary.Add(BonusParam.BonusType.TOTAL_SCORE, this.m_bonusData[target][5]);
			}
		}
		else if (offsetUse)
		{
			dictionary.Add(BonusParam.BonusType.SCORE, BonusUtil.GetBonusParamValue(BonusParam.BonusType.SCORE, 0f));
			dictionary.Add(BonusParam.BonusType.RING, BonusUtil.GetBonusParamValue(BonusParam.BonusType.RING, 0f));
			dictionary.Add(BonusParam.BonusType.ANIMAL, BonusUtil.GetBonusParamValue(BonusParam.BonusType.ANIMAL, 0f));
			dictionary.Add(BonusParam.BonusType.DISTANCE, BonusUtil.GetBonusParamValue(BonusParam.BonusType.DISTANCE, 0f));
			dictionary.Add(BonusParam.BonusType.ENEMY_OBJBREAK, BonusUtil.GetBonusParamValue(BonusParam.BonusType.ENEMY_OBJBREAK, 0f));
			dictionary.Add(BonusParam.BonusType.SPEED, BonusUtil.GetBonusParamValue(BonusParam.BonusType.SPEED, 0f));
			dictionary.Add(BonusParam.BonusType.TOTAL_SCORE, BonusUtil.GetBonusParamValue(BonusParam.BonusType.TOTAL_SCORE, 0f));
		}
		else
		{
			dictionary.Add(BonusParam.BonusType.SCORE, 0f);
			dictionary.Add(BonusParam.BonusType.RING, 0f);
			dictionary.Add(BonusParam.BonusType.ANIMAL, 0f);
			dictionary.Add(BonusParam.BonusType.DISTANCE, 0f);
			dictionary.Add(BonusParam.BonusType.ENEMY_OBJBREAK, 0f);
			dictionary.Add(BonusParam.BonusType.SPEED, 0f);
			dictionary.Add(BonusParam.BonusType.TOTAL_SCORE, 0f);
		}
		return dictionary;
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x0002DB28 File Offset: 0x0002BD28
	public Dictionary<BonusParam.BonusType, float> GetBonusInfo(BonusParam.BonusTarget targetA, BonusParam.BonusTarget targetB, bool offsetUse = true)
	{
		Dictionary<BonusParam.BonusType, float> dictionary = new Dictionary<BonusParam.BonusType, float>();
		if (targetA != BonusParam.BonusTarget.ALL && targetB != BonusParam.BonusTarget.ALL && targetA != targetB)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 0f;
			Dictionary<BonusParam.BonusTarget, List<float>>.KeyCollection keys = this.m_bonusData.Keys;
			foreach (BonusParam.BonusTarget bonusTarget in keys)
			{
				if (this.m_bonusData[bonusTarget] != null && this.m_bonusData[bonusTarget].Count > 0 && (bonusTarget == targetA || bonusTarget == targetB))
				{
					num += this.m_bonusData[bonusTarget][0];
					num2 += this.m_bonusData[bonusTarget][1];
					num3 += this.m_bonusData[bonusTarget][2];
					num4 += this.m_bonusData[bonusTarget][3];
					num5 += this.m_bonusData[bonusTarget][4];
					num7 += this.m_bonusData[bonusTarget][6];
					if (num6 == 0f)
					{
						num6 = this.m_bonusData[bonusTarget][5];
					}
					else if (this.m_bonusData[bonusTarget][5] != 0f)
					{
						num6 = BonusUtil.GetTotalScoreBonus(num6, this.m_bonusData[bonusTarget][5]);
					}
				}
			}
			if (offsetUse)
			{
				dictionary.Add(BonusParam.BonusType.SCORE, BonusUtil.GetBonusParamValue(BonusParam.BonusType.SCORE, num));
				dictionary.Add(BonusParam.BonusType.RING, BonusUtil.GetBonusParamValue(BonusParam.BonusType.RING, num2));
				dictionary.Add(BonusParam.BonusType.ANIMAL, BonusUtil.GetBonusParamValue(BonusParam.BonusType.ANIMAL, num3));
				dictionary.Add(BonusParam.BonusType.DISTANCE, BonusUtil.GetBonusParamValue(BonusParam.BonusType.DISTANCE, num4));
				dictionary.Add(BonusParam.BonusType.ENEMY_OBJBREAK, BonusUtil.GetBonusParamValue(BonusParam.BonusType.ENEMY_OBJBREAK, num5));
				dictionary.Add(BonusParam.BonusType.SPEED, BonusUtil.GetBonusParamValue(BonusParam.BonusType.SPEED, num7));
				dictionary.Add(BonusParam.BonusType.TOTAL_SCORE, BonusUtil.GetBonusParamValue(BonusParam.BonusType.TOTAL_SCORE, num6));
			}
			else
			{
				dictionary.Add(BonusParam.BonusType.SCORE, num);
				dictionary.Add(BonusParam.BonusType.RING, num2);
				dictionary.Add(BonusParam.BonusType.ANIMAL, num3);
				dictionary.Add(BonusParam.BonusType.DISTANCE, num4);
				dictionary.Add(BonusParam.BonusType.ENEMY_OBJBREAK, num5);
				dictionary.Add(BonusParam.BonusType.SPEED, num7);
				dictionary.Add(BonusParam.BonusType.TOTAL_SCORE, num6);
			}
		}
		return dictionary;
	}

	// Token: 0x040005D9 RID: 1497
	private Dictionary<BonusParam.BonusTarget, List<float>> m_bonusData;

	// Token: 0x040005DA RID: 1498
	private Dictionary<BonusParam.BonusTarget, CharacterAttribute> m_attribute;

	// Token: 0x02000103 RID: 259
	public enum BonusTarget
	{
		// Token: 0x040005DC RID: 1500
		CHARA,
		// Token: 0x040005DD RID: 1501
		CHAO_MAIN,
		// Token: 0x040005DE RID: 1502
		CHAO_SUB,
		// Token: 0x040005DF RID: 1503
		ALL
	}

	// Token: 0x02000104 RID: 260
	public enum BonusType
	{
		// Token: 0x040005E1 RID: 1505
		SCORE,
		// Token: 0x040005E2 RID: 1506
		RING,
		// Token: 0x040005E3 RID: 1507
		ANIMAL,
		// Token: 0x040005E4 RID: 1508
		DISTANCE,
		// Token: 0x040005E5 RID: 1509
		ENEMY_OBJBREAK,
		// Token: 0x040005E6 RID: 1510
		TOTAL_SCORE,
		// Token: 0x040005E7 RID: 1511
		SPEED,
		// Token: 0x040005E8 RID: 1512
		NONE
	}

	// Token: 0x02000105 RID: 261
	public enum BonusAffinity
	{
		// Token: 0x040005EA RID: 1514
		NONE,
		// Token: 0x040005EB RID: 1515
		GOOD
	}
}
