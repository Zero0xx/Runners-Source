using System;
using System.Collections.Generic;
using Text;
using UnityEngine;

// Token: 0x020007F7 RID: 2039
public class ServerCharacterState
{
	// Token: 0x06003698 RID: 13976 RVA: 0x00121804 File Offset: 0x0011FA04
	public ServerCharacterState()
	{
		this.Id = -1;
		this.Status = ServerCharacterState.CharacterStatus.Locked;
		this.Level = 10;
		this.Cost = 0;
		this.star = 0;
		this.starMax = 0;
		this.priceNumRings = 0;
		this.priceNumRedRings = 0;
	}

	// Token: 0x170007E5 RID: 2021
	// (get) Token: 0x06003699 RID: 13977 RVA: 0x00121874 File Offset: 0x0011FA74
	// (set) Token: 0x0600369A RID: 13978 RVA: 0x0012187C File Offset: 0x0011FA7C
	public int Id { get; set; }

	// Token: 0x170007E6 RID: 2022
	// (get) Token: 0x0600369B RID: 13979 RVA: 0x00121888 File Offset: 0x0011FA88
	// (set) Token: 0x0600369C RID: 13980 RVA: 0x00121890 File Offset: 0x0011FA90
	public ServerCharacterState.CharacterStatus Status { get; set; }

	// Token: 0x170007E7 RID: 2023
	// (get) Token: 0x0600369D RID: 13981 RVA: 0x0012189C File Offset: 0x0011FA9C
	// (set) Token: 0x0600369E RID: 13982 RVA: 0x001218A4 File Offset: 0x0011FAA4
	public ServerCharacterState.CharacterStatus OldStatus { get; set; }

	// Token: 0x170007E8 RID: 2024
	// (get) Token: 0x0600369F RID: 13983 RVA: 0x001218B0 File Offset: 0x0011FAB0
	// (set) Token: 0x060036A0 RID: 13984 RVA: 0x001218B8 File Offset: 0x0011FAB8
	public int Level { get; set; }

	// Token: 0x170007E9 RID: 2025
	// (get) Token: 0x060036A1 RID: 13985 RVA: 0x001218C4 File Offset: 0x0011FAC4
	// (set) Token: 0x060036A2 RID: 13986 RVA: 0x001218CC File Offset: 0x0011FACC
	public int Cost { get; set; }

	// Token: 0x170007EA RID: 2026
	// (get) Token: 0x060036A3 RID: 13987 RVA: 0x001218D8 File Offset: 0x0011FAD8
	// (set) Token: 0x060036A4 RID: 13988 RVA: 0x001218E0 File Offset: 0x0011FAE0
	public int OldCost { get; set; }

	// Token: 0x170007EB RID: 2027
	// (get) Token: 0x060036A5 RID: 13989 RVA: 0x001218EC File Offset: 0x0011FAEC
	// (set) Token: 0x060036A6 RID: 13990 RVA: 0x001218F4 File Offset: 0x0011FAF4
	public int NumRedRings { get; set; }

	// Token: 0x170007EC RID: 2028
	// (get) Token: 0x060036A7 RID: 13991 RVA: 0x00121900 File Offset: 0x0011FB00
	// (set) Token: 0x060036A8 RID: 13992 RVA: 0x00121908 File Offset: 0x0011FB08
	public ServerCharacterState.LockCondition Condition { get; set; }

	// Token: 0x170007ED RID: 2029
	// (get) Token: 0x060036A9 RID: 13993 RVA: 0x00121914 File Offset: 0x0011FB14
	// (set) Token: 0x060036AA RID: 13994 RVA: 0x0012191C File Offset: 0x0011FB1C
	public int Exp { get; set; }

	// Token: 0x170007EE RID: 2030
	// (get) Token: 0x060036AB RID: 13995 RVA: 0x00121928 File Offset: 0x0011FB28
	// (set) Token: 0x060036AC RID: 13996 RVA: 0x00121930 File Offset: 0x0011FB30
	public int OldExp { get; set; }

	// Token: 0x170007EF RID: 2031
	// (get) Token: 0x060036AD RID: 13997 RVA: 0x0012193C File Offset: 0x0011FB3C
	// (set) Token: 0x060036AE RID: 13998 RVA: 0x00121944 File Offset: 0x0011FB44
	public int star { get; set; }

	// Token: 0x170007F0 RID: 2032
	// (get) Token: 0x060036AF RID: 13999 RVA: 0x00121950 File Offset: 0x0011FB50
	// (set) Token: 0x060036B0 RID: 14000 RVA: 0x00121958 File Offset: 0x0011FB58
	public int starMax { get; set; }

	// Token: 0x170007F1 RID: 2033
	// (get) Token: 0x060036B1 RID: 14001 RVA: 0x00121964 File Offset: 0x0011FB64
	// (set) Token: 0x060036B2 RID: 14002 RVA: 0x0012196C File Offset: 0x0011FB6C
	public int priceNumRings { get; set; }

	// Token: 0x170007F2 RID: 2034
	// (get) Token: 0x060036B3 RID: 14003 RVA: 0x00121978 File Offset: 0x0011FB78
	// (set) Token: 0x060036B4 RID: 14004 RVA: 0x00121980 File Offset: 0x0011FB80
	public int priceNumRedRings { get; set; }

	// Token: 0x170007F3 RID: 2035
	// (get) Token: 0x060036B5 RID: 14005 RVA: 0x0012198C File Offset: 0x0011FB8C
	public CharaType charaType
	{
		get
		{
			CharaType result = CharaType.UNKNOWN;
			if (this.Id >= 0)
			{
				ServerItem serverItem = new ServerItem((ServerItem.Id)this.Id);
				result = serverItem.charaType;
			}
			return result;
		}
	}

	// Token: 0x170007F4 RID: 2036
	// (get) Token: 0x060036B6 RID: 14006 RVA: 0x001219C0 File Offset: 0x0011FBC0
	public CharacterDataNameInfo.Info charaInfo
	{
		get
		{
			CharacterDataNameInfo.Info result = null;
			CharacterDataNameInfo instance = CharacterDataNameInfo.Instance;
			if (instance != null)
			{
				result = instance.GetDataByID(this.charaType);
			}
			return result;
		}
	}

	// Token: 0x170007F5 RID: 2037
	// (get) Token: 0x060036B7 RID: 14007 RVA: 0x001219F0 File Offset: 0x0011FBF0
	public bool IsBuy
	{
		get
		{
			bool result = false;
			if (this.starMax > 0 && this.star < this.starMax && (this.priceNumRings > 0 || this.priceNumRedRings > 0))
			{
				result = true;
			}
			return result;
		}
	}

	// Token: 0x170007F6 RID: 2038
	// (get) Token: 0x060036B8 RID: 14008 RVA: 0x00121A38 File Offset: 0x0011FC38
	public bool IsRoulette
	{
		get
		{
			bool result = false;
			if (RouletteManager.Instance != null)
			{
				if (this.Condition == ServerCharacterState.LockCondition.ROULETTE)
				{
					result = true;
				}
				else if (RouletteManager.Instance.IsPicupChara(this.charaType))
				{
					result = true;
				}
			}
			return result;
		}
	}

	// Token: 0x060036B9 RID: 14009 RVA: 0x00121A84 File Offset: 0x0011FC84
	public Dictionary<ServerItem.Id, int> GetBuyCostItemList()
	{
		Dictionary<ServerItem.Id, int> dictionary = null;
		if (this.IsBuy)
		{
			if (this.priceNumRings > 0)
			{
				if (dictionary == null)
				{
					dictionary = new Dictionary<ServerItem.Id, int>();
				}
				dictionary.Add(ServerItem.Id.RING, this.priceNumRings);
			}
			if (this.priceNumRedRings > 0)
			{
				if (dictionary == null)
				{
					dictionary = new Dictionary<ServerItem.Id, int>();
				}
				dictionary.Add(ServerItem.Id.RSRING, this.priceNumRedRings);
			}
		}
		return dictionary;
	}

	// Token: 0x060036BA RID: 14010 RVA: 0x00121AF4 File Offset: 0x0011FCF4
	public Dictionary<BonusParam.BonusType, float> GetStarBonusList()
	{
		Dictionary<BonusParam.BonusType, float> result = null;
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.ETC, "OverlapBonusTable");
		if (gameObject != null)
		{
			OverlapBonusTable component = gameObject.GetComponent<OverlapBonusTable>();
			if (component != null)
			{
				result = component.GetStarBonusList(this.charaType);
			}
		}
		return result;
	}

	// Token: 0x060036BB RID: 14011 RVA: 0x00121B44 File Offset: 0x0011FD44
	public Dictionary<BonusParam.BonusType, float> GetTeamBonusList()
	{
		Dictionary<BonusParam.BonusType, float> result = null;
		BonusUtil.GetTeamBonus(this.charaType, out result);
		return result;
	}

	// Token: 0x060036BC RID: 14012 RVA: 0x00121B64 File Offset: 0x0011FD64
	public string GetCharaAttributeText()
	{
		string text = string.Empty;
		string cellName = "chara_attribute_" + CharaName.Name[(int)this.charaType];
		if (!this.IsUnlocked)
		{
			cellName = "recommend_chara_unlock_text_" + CharaName.Name[(int)this.charaType];
			string text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", cellName).text;
			if (string.IsNullOrEmpty(text2))
			{
				List<string> list = new List<string>();
				Dictionary<ServerItem.Id, int> buyCostItemList = this.GetBuyCostItemList();
				if (this.IsRoulette)
				{
					list.Add(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "GeneralWindow", "ui_Lbl_roulette").text);
				}
				if (buyCostItemList != null && buyCostItemList.Count > 0)
				{
					Dictionary<ServerItem.Id, int>.KeyCollection keys = buyCostItemList.Keys;
					foreach (ServerItem.Id id in keys)
					{
						ServerItem serverItem = new ServerItem(id);
						list.Add(serverItem.serverItemName);
					}
				}
				global::Debug.Log("nameList.Count:" + list.Count);
				if (list.Count > 0)
				{
					int num = list.Count;
					if (num > 3)
					{
						num = 3;
					}
					text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "recommend_chara_unlock_text_type_" + num).text;
					text = text2;
					for (int i = 0; i < num; i++)
					{
						text = text.Replace("{PARAM" + (i + 1) + "}", list[i]);
					}
					text = text.Replace("{BONUS}", this.GetTeamBonusText());
				}
				else
				{
					text = string.Empty;
				}
			}
			else
			{
				text = text2;
				text = text.Replace("{BONUS}", this.GetTeamBonusText());
			}
		}
		else
		{
			text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", cellName).text;
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Replace("{BONUS}", this.GetTeamBonusText());
			}
			else
			{
				text = "Unknown";
			}
		}
		if (this.starMax > 0 && this.star > 0)
		{
			string text3 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "WindowText", "chara_overlap_text_2").text;
			float quickModeTimeExtension = this.QuickModeTimeExtension;
			float num2 = 0f;
			Dictionary<BonusParam.BonusType, float> starBonusList = this.GetStarBonusList();
			if (starBonusList != null && starBonusList.Count > 0)
			{
				Dictionary<BonusParam.BonusType, float>.KeyCollection keys2 = starBonusList.Keys;
				using (Dictionary<BonusParam.BonusType, float>.KeyCollection.Enumerator enumerator2 = keys2.GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						BonusParam.BonusType key = enumerator2.Current;
						float num3 = starBonusList[key];
						num2 = num3;
					}
				}
			}
			text3 = text3.Replace("{TIME}", quickModeTimeExtension.ToString());
			text3 = text3.Replace("{PARAM}", num2.ToString());
			text = text + "\n\n" + text3;
		}
		return text;
	}

	// Token: 0x060036BD RID: 14013 RVA: 0x00121E94 File Offset: 0x00120094
	private string GetTeamBonusText()
	{
		string text = string.Empty;
		Dictionary<BonusParam.BonusType, float> teamBonusList = this.GetTeamBonusList();
		Dictionary<BonusParam.BonusType, float>.KeyCollection keys = teamBonusList.Keys;
		string text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoSet", "bonus_percent").text;
		foreach (BonusParam.BonusType bonusType in keys)
		{
			global::Debug.Log("GetTeamBonusText key:" + bonusType);
			float num = teamBonusList[bonusType];
			string text3 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoSet", "ui_Lbl_bonusname_" + (int)bonusType).text;
			if (bonusType == BonusParam.BonusType.SPEED)
			{
				text3 = text3 + " " + text2.Replace("{BONUS}", (100f - num).ToString());
			}
			else if (bonusType == BonusParam.BonusType.TOTAL_SCORE && Mathf.Abs(num) <= 1f)
			{
				num *= 100f;
				if (num >= 0f)
				{
					text3 = text3 + " +" + text2.Replace("{BONUS}", num.ToString());
				}
				else
				{
					text3 = text3 + " " + text2.Replace("{BONUS}", num.ToString());
				}
			}
			else if (num >= 0f)
			{
				text3 = text3 + " +" + text2.Replace("{BONUS}", num.ToString());
			}
			else
			{
				text3 = text3 + " " + text2.Replace("{BONUS}", num.ToString());
			}
			if (string.IsNullOrEmpty(text))
			{
				text = text3;
			}
			else
			{
				text = text + "\n" + text3;
			}
		}
		return text;
	}

	// Token: 0x170007F7 RID: 2039
	// (get) Token: 0x060036BE RID: 14014 RVA: 0x00122084 File Offset: 0x00120284
	public int UnlockCost
	{
		get
		{
			if (this.Status == ServerCharacterState.CharacterStatus.Locked)
			{
				return this.Cost;
			}
			return -1;
		}
	}

	// Token: 0x170007F8 RID: 2040
	// (get) Token: 0x060036BF RID: 14015 RVA: 0x0012209C File Offset: 0x0012029C
	public int LevelUpCost
	{
		get
		{
			if (this.Status == ServerCharacterState.CharacterStatus.Unlocked)
			{
				return this.Cost;
			}
			return -1;
		}
	}

	// Token: 0x170007F9 RID: 2041
	// (get) Token: 0x060036C0 RID: 14016 RVA: 0x001220B4 File Offset: 0x001202B4
	public bool IsUnlocked
	{
		get
		{
			return ServerCharacterState.CharacterStatus.Locked != this.Status;
		}
	}

	// Token: 0x170007FA RID: 2042
	// (get) Token: 0x060036C1 RID: 14017 RVA: 0x001220C4 File Offset: 0x001202C4
	public bool IsMaxLevel
	{
		get
		{
			return ServerCharacterState.CharacterStatus.MaxLevel == this.Status;
		}
	}

	// Token: 0x170007FB RID: 2043
	// (get) Token: 0x060036C2 RID: 14018 RVA: 0x001220D0 File Offset: 0x001202D0
	public float QuickModeTimeExtension
	{
		get
		{
			float result = 0f;
			if (this.starMax > 0)
			{
				StageTimeTable stageTimeTable = GameObjectUtil.FindGameObjectComponent<StageTimeTable>("StageTimeTable");
				if (stageTimeTable != null)
				{
					float num = (float)stageTimeTable.GetTableItemData(StageTimeTableItem.OverlapBonus);
					result = (float)this.star * num;
				}
			}
			return result;
		}
	}

	// Token: 0x060036C3 RID: 14019 RVA: 0x0012211C File Offset: 0x0012031C
	public bool IsExpGot()
	{
		if (this.OldExp < this.Exp)
		{
			return true;
		}
		if (this.OldAbiltyLevel.Count == this.AbilityLevel.Count)
		{
			int count = this.AbilityLevel.Count;
			for (int i = 0; i < count; i++)
			{
				int num = this.OldAbiltyLevel[i];
				int num2 = this.AbilityLevel[i];
				if (num < num2)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060036C4 RID: 14020 RVA: 0x0012219C File Offset: 0x0012039C
	public void Dump()
	{
		global::Debug.Log(string.Concat(new object[]
		{
			"Id=",
			this.Id,
			", Status=",
			this.Status,
			", Level=",
			this.Level,
			", Cost=",
			this.Cost,
			", UnlockCost=",
			this.UnlockCost,
			", LevelUpCost=",
			this.LevelUpCost
		}));
	}

	// Token: 0x04002DED RID: 11757
	public int m_currentUnlocks;

	// Token: 0x04002DEE RID: 11758
	public int m_numTokens;

	// Token: 0x04002DEF RID: 11759
	public int m_tokenCost;

	// Token: 0x04002DF0 RID: 11760
	public List<int> AbilityLevel = new List<int>();

	// Token: 0x04002DF1 RID: 11761
	public List<int> OldAbiltyLevel = new List<int>();

	// Token: 0x04002DF2 RID: 11762
	public List<int> AbilityNumRings = new List<int>();

	// Token: 0x020007F8 RID: 2040
	public enum CharacterStatus
	{
		// Token: 0x04002E02 RID: 11778
		Locked,
		// Token: 0x04002E03 RID: 11779
		Unlocked,
		// Token: 0x04002E04 RID: 11780
		MaxLevel
	}

	// Token: 0x020007F9 RID: 2041
	public enum LockCondition
	{
		// Token: 0x04002E06 RID: 11782
		OPEN,
		// Token: 0x04002E07 RID: 11783
		MILEAGE_EPISODE,
		// Token: 0x04002E08 RID: 11784
		RING_OR_RED_STAR_RING,
		// Token: 0x04002E09 RID: 11785
		ROULETTE
	}
}
