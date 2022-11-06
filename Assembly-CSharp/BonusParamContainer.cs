using System;
using System.Collections.Generic;

// Token: 0x02000106 RID: 262
public class BonusParamContainer
{
	// Token: 0x060007C2 RID: 1986 RVA: 0x0002DDC0 File Offset: 0x0002BFC0
	public void Reset()
	{
		if (this.m_paramList != null)
		{
			foreach (BonusParam bonusParam in this.m_paramList)
			{
				bonusParam.Reset();
			}
			this.m_paramList.Clear();
		}
		this.m_paramList = null;
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x0002DE44 File Offset: 0x0002C044
	public void addBonus(BonusParam bonusParam)
	{
		if (this.m_paramList == null)
		{
			this.m_paramList = new List<BonusParam>();
		}
		this.m_paramList.Add(bonusParam);
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x0002DE74 File Offset: 0x0002C074
	public BonusParam GetBonusParam(int index)
	{
		BonusParam result = null;
		if (this.m_paramList != null && index >= 0 && this.m_paramList.Count > index)
		{
			result = this.m_paramList[index];
		}
		return result;
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x0002DEB4 File Offset: 0x0002C0B4
	public Dictionary<BonusParam.BonusTarget, List<float>> GetBonusParamOrgData(int index)
	{
		Dictionary<BonusParam.BonusTarget, List<float>> result = null;
		if (this.m_paramList != null && index >= 0 && this.m_paramList.Count > index)
		{
			result = this.m_paramList[index].orgBonusData;
		}
		return result;
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x0002DEFC File Offset: 0x0002C0FC
	public bool IsDetailInfo(out string detailText)
	{
		detailText = string.Empty;
		int num = this.m_currentTargetIndex;
		if (num < 0)
		{
			num = 0;
		}
		Dictionary<BonusParam.BonusTarget, List<float>> bonusParamOrgData = this.GetBonusParamOrgData(num);
		if (bonusParamOrgData != null)
		{
			detailText = BonusParam.GetDetailInfoText(bonusParamOrgData);
		}
		return !string.IsNullOrEmpty(detailText);
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x0002DF40 File Offset: 0x0002C140
	public Dictionary<BonusParam.BonusType, float> GetBonusInfo(int index = -1)
	{
		Dictionary<BonusParam.BonusType, float> dictionary = null;
		List<Dictionary<BonusParam.BonusType, float>> list = new List<Dictionary<BonusParam.BonusType, float>>();
		List<Dictionary<BonusParam.BonusType, float>> list2 = new List<Dictionary<BonusParam.BonusType, float>>();
		this.m_currentTargetIndex = index;
		if (index < 0)
		{
			if (this.m_paramList != null)
			{
				foreach (BonusParam bonusParam in this.m_paramList)
				{
					list.Add(bonusParam.GetBonusInfo(BonusParam.BonusTarget.CHAO_MAIN, BonusParam.BonusTarget.CHAO_SUB, false));
					list2.Add(bonusParam.GetBonusInfo(BonusParam.BonusTarget.CHARA, false));
				}
			}
		}
		else if (this.m_paramList.Count > index)
		{
			dictionary = this.m_paramList[index].GetBonusInfo(BonusParam.BonusTarget.ALL, true);
			return dictionary;
		}
		if (list2.Count > 0)
		{
			dictionary = new Dictionary<BonusParam.BonusType, float>();
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 0f;
			int num8 = 0;
			foreach (Dictionary<BonusParam.BonusType, float> dictionary2 in list)
			{
				if (num8 == 0 || num < dictionary2[BonusParam.BonusType.SCORE])
				{
					num = dictionary2[BonusParam.BonusType.SCORE];
				}
				if (num8 == 0 || num2 < dictionary2[BonusParam.BonusType.RING])
				{
					num2 = dictionary2[BonusParam.BonusType.RING];
				}
				if (num8 == 0 || num3 < dictionary2[BonusParam.BonusType.ANIMAL])
				{
					num3 = dictionary2[BonusParam.BonusType.ANIMAL];
				}
				if (num8 == 0 || num4 < dictionary2[BonusParam.BonusType.DISTANCE])
				{
					num4 = dictionary2[BonusParam.BonusType.DISTANCE];
				}
				if (num8 == 0 || num5 < dictionary2[BonusParam.BonusType.ENEMY_OBJBREAK])
				{
					num5 = dictionary2[BonusParam.BonusType.ENEMY_OBJBREAK];
				}
				if (num8 == 0 || num7 < dictionary2[BonusParam.BonusType.SPEED])
				{
					num7 = dictionary2[BonusParam.BonusType.SPEED];
				}
				if (num8 == 0 || num6 < dictionary2[BonusParam.BonusType.TOTAL_SCORE])
				{
					num6 = dictionary2[BonusParam.BonusType.TOTAL_SCORE];
				}
				num8++;
			}
			num += list2[0][BonusParam.BonusType.SCORE];
			num2 += list2[0][BonusParam.BonusType.RING];
			num3 += list2[0][BonusParam.BonusType.ANIMAL];
			num4 += list2[0][BonusParam.BonusType.DISTANCE];
			num5 += list2[0][BonusParam.BonusType.ENEMY_OBJBREAK];
			num7 += list2[0][BonusParam.BonusType.SPEED];
			if (num6 == 0f)
			{
				num6 = list2[0][BonusParam.BonusType.TOTAL_SCORE];
			}
			else if (list2[0][BonusParam.BonusType.TOTAL_SCORE] != 0f)
			{
				num6 = BonusUtil.GetTotalScoreBonus(num6, list2[0][BonusParam.BonusType.TOTAL_SCORE]);
			}
			dictionary.Add(BonusParam.BonusType.SCORE, BonusUtil.GetBonusParamValue(BonusParam.BonusType.SCORE, num));
			dictionary.Add(BonusParam.BonusType.RING, BonusUtil.GetBonusParamValue(BonusParam.BonusType.RING, num2));
			dictionary.Add(BonusParam.BonusType.ANIMAL, BonusUtil.GetBonusParamValue(BonusParam.BonusType.ANIMAL, num3));
			dictionary.Add(BonusParam.BonusType.DISTANCE, BonusUtil.GetBonusParamValue(BonusParam.BonusType.DISTANCE, num4));
			dictionary.Add(BonusParam.BonusType.ENEMY_OBJBREAK, BonusUtil.GetBonusParamValue(BonusParam.BonusType.ENEMY_OBJBREAK, num5));
			dictionary.Add(BonusParam.BonusType.SPEED, BonusUtil.GetBonusParamValue(BonusParam.BonusType.SPEED, num7));
			dictionary.Add(BonusParam.BonusType.TOTAL_SCORE, BonusUtil.GetBonusParamValue(BonusParam.BonusType.TOTAL_SCORE, num6));
		}
		return dictionary;
	}

	// Token: 0x040005EC RID: 1516
	private List<BonusParam> m_paramList;

	// Token: 0x040005ED RID: 1517
	private int m_currentTargetIndex = -1;
}
