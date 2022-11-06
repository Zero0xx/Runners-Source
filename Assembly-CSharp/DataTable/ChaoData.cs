using System;
using System.Collections.Generic;
using SaveData;
using Text;

namespace DataTable
{
	// Token: 0x0200017C RID: 380
	public class ChaoData : IComparable
	{
		// Token: 0x06000AD1 RID: 2769 RVA: 0x0003FA18 File Offset: 0x0003DC18
		public ChaoData()
		{
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0003FA20 File Offset: 0x0003DC20
		public ChaoData(ChaoData src)
		{
			this.id = src.id;
			this.rarity = src.rarity;
			this.charaAtribute = src.charaAtribute;
			this.currentAbility = src.currentAbility;
			this.chaoAbilitys = new ChaoAbility[src.chaoAbilitys.Length];
			for (int i = 0; i < src.chaoAbilitys.Length; i++)
			{
				this.chaoAbilitys[i] = src.chaoAbilitys[i];
			}
			this.m_abilityStatus = new string[src.m_abilityStatus.Length];
			for (int j = 0; j < src.m_abilityStatus.Length; j++)
			{
				this.m_abilityStatus[j] = src.m_abilityStatus[j];
			}
			this.m_abilityStatusData = new ChaoDataAbilityStatus[src.m_abilityStatusData.Length];
			for (int k = 0; k < src.m_abilityStatusData.Length; k++)
			{
				this.m_abilityStatusData[k] = src.m_abilityStatusData[k];
			}
			this.name = src.name;
			this.nameTwolines = src.nameTwolines;
			this.index = src.index;
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0003FB3C File Offset: 0x0003DD3C
		// (set) Token: 0x06000AD4 RID: 2772 RVA: 0x0003FB44 File Offset: 0x0003DD44
		public int id { get; private set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0003FB50 File Offset: 0x0003DD50
		// (set) Token: 0x06000AD6 RID: 2774 RVA: 0x0003FB58 File Offset: 0x0003DD58
		public ChaoData.Rarity rarity { get; private set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0003FB64 File Offset: 0x0003DD64
		// (set) Token: 0x06000AD8 RID: 2776 RVA: 0x0003FB6C File Offset: 0x0003DD6C
		public CharacterAttribute charaAtribute { get; private set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0003FB78 File Offset: 0x0003DD78
		// (set) Token: 0x06000ADA RID: 2778 RVA: 0x0003FB80 File Offset: 0x0003DD80
		public int currentAbility
		{
			get
			{
				return this.m_currentAbility;
			}
			set
			{
				this.m_currentAbility = value;
				if (this.m_currentAbility >= this.abilityNum)
				{
					this.m_currentAbility = this.abilityNum - 1;
				}
				if (this.m_currentAbility < 0)
				{
					this.m_currentAbility = 0;
				}
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0003FBBC File Offset: 0x0003DDBC
		public int abilityNum
		{
			get
			{
				return this.m_abilityStatus.Length;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0003FBC8 File Offset: 0x0003DDC8
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x0003FBD0 File Offset: 0x0003DDD0
		public ChaoAbility[] chaoAbilitys { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0003FBDC File Offset: 0x0003DDDC
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x0003FBE4 File Offset: 0x0003DDE4
		public string[] abilityStatus
		{
			get
			{
				return this.m_abilityStatus;
			}
			set
			{
				this.m_abilityStatus = value;
				this.m_abilityStatusData = new ChaoDataAbilityStatus[this.m_abilityStatus.Length];
				for (int i = 0; i < this.m_abilityStatus.Length; i++)
				{
					this.m_abilityStatusData[i] = new ChaoDataAbilityStatus(this.m_abilityStatus[i], this.id, i);
				}
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0003FC40 File Offset: 0x0003DE40
		public ChaoAbility chaoAbility
		{
			get
			{
				return this.chaoAbilitys[this.currentAbility];
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0003FC50 File Offset: 0x0003DE50
		public float[] abilityValue
		{
			get
			{
				return this.GetAbilityValues();
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0003FC58 File Offset: 0x0003DE58
		public float[] bonusAbilityValue
		{
			get
			{
				return this.GetBonusAbilityValues();
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0003FC60 File Offset: 0x0003DE60
		public int eventId
		{
			get
			{
				return this.GetAbilityEventId();
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0003FC68 File Offset: 0x0003DE68
		public float extraValue
		{
			get
			{
				return this.GetAbilitiyExtraValue();
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0003FC70 File Offset: 0x0003DE70
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x0003FC78 File Offset: 0x0003DE78
		public string name { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x0003FC84 File Offset: 0x0003DE84
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x0003FC8C File Offset: 0x0003DE8C
		public string nameTwolines { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0003FC98 File Offset: 0x0003DE98
		public string details
		{
			get
			{
				return this.m_abilityStatusData[this.currentAbility].details;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0003FCAC File Offset: 0x0003DEAC
		public string loadingDetails
		{
			get
			{
				return this.m_abilityStatusData[this.currentAbility].loadingDetails;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0003FCC0 File Offset: 0x0003DEC0
		public string loadingLongDetails
		{
			get
			{
				return this.m_abilityStatusData[this.currentAbility].loadingLongDetails;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0003FCD4 File Offset: 0x0003DED4
		public string growDetails
		{
			get
			{
				return this.m_abilityStatusData[this.currentAbility].growDetails;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0003FCE8 File Offset: 0x0003DEE8
		public string menuDetails
		{
			get
			{
				return this.m_abilityStatusData[this.currentAbility].menuDetails;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0003FCFC File Offset: 0x0003DEFC
		public string bgmName
		{
			get
			{
				return this.m_abilityStatusData[this.currentAbility].bgmName;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0003FD10 File Offset: 0x0003DF10
		public string cueSheetName
		{
			get
			{
				return this.m_abilityStatusData[this.currentAbility].cueSheetName;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0003FD24 File Offset: 0x0003DF24
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x0003FD2C File Offset: 0x0003DF2C
		public int index { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0003FD38 File Offset: 0x0003DF38
		public List<int> eventIdList
		{
			get
			{
				List<int> list = null;
				if (this.m_abilityStatusData != null && this.m_abilityStatusData.Length > 0)
				{
					list = new List<int>();
					int num = this.m_abilityStatusData.Length;
					for (int i = 0; i < num; i++)
					{
						list.Add(this.m_abilityStatusData[i].eventId);
					}
				}
				return list;
			}
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0003FD98 File Offset: 0x0003DF98
		public int CompareTo(object obj)
		{
			return this.id - ((ChaoData)obj).id;
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0003FDAC File Offset: 0x0003DFAC
		public bool IsValidate
		{
			get
			{
				return this.id != -1 && this.level != -1;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0003FDCC File Offset: 0x0003DFCC
		public int level
		{
			get
			{
				return (this.id == -1) ? -1 : this.GetChaoLevel();
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x0003FDE8 File Offset: 0x0003DFE8
		public string spriteNameSuffix
		{
			get
			{
				return this.id.ToString("D4");
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0003FE08 File Offset: 0x0003E008
		public static string GetSpriteNameSuffix(int id)
		{
			return id.ToString("D4");
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0003FE18 File Offset: 0x0003E018
		private int GetChaoLevel()
		{
			SaveDataManager instance = SaveDataManager.Instance;
			if (instance != null && instance.ChaoData != null && instance.ChaoData.Info != null)
			{
				foreach (ChaoData.ChaoDataInfo chaoDataInfo in instance.ChaoData.Info)
				{
					if (chaoDataInfo.chao_id == this.id)
					{
						return chaoDataInfo.level;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0003FE9C File Offset: 0x0003E09C
		private int GetAbilityEventId()
		{
			ChaoDataAbilityStatus chaoDataAbilityStatus = this.m_abilityStatusData[this.currentAbility];
			return chaoDataAbilityStatus.eventId;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0003FEC4 File Offset: 0x0003E0C4
		private float GetAbilitiyExtraValue()
		{
			ChaoDataAbilityStatus chaoDataAbilityStatus = this.m_abilityStatusData[this.currentAbility];
			return chaoDataAbilityStatus.extraValue;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0003FEF0 File Offset: 0x0003E0F0
		private float[] GetAbilityValues()
		{
			ChaoDataAbilityStatus chaoDataAbilityStatus = this.m_abilityStatusData[this.currentAbility];
			return chaoDataAbilityStatus.abilityValues;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0003FF14 File Offset: 0x0003E114
		private float[] GetBonusAbilityValues()
		{
			ChaoDataAbilityStatus chaoDataAbilityStatus = this.m_abilityStatusData[this.currentAbility];
			return chaoDataAbilityStatus.bonusAbilityValues;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0003FF38 File Offset: 0x0003E138
		public string GetFeaturedDetail()
		{
			string cellID = "featured_footnote" + this.id.ToString("D4");
			return TextUtility.GetChaoText("Chao", cellID);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0003FF70 File Offset: 0x0003E170
		public string GetDetailLevelPlusSP(int chaoLevel)
		{
			return this.GetDetailLevelPlusSP(chaoLevel, "[ffff00]");
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0003FF80 File Offset: 0x0003E180
		public string GetLoadingPageDetailLevelPlusSP(int chaoLevel)
		{
			return this.GetLoadingPageDetailLevelPlusSP(chaoLevel, "[e0b000]");
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0003FF90 File Offset: 0x0003E190
		private string GetDetailLevelPlusSP(int chaoLevel, string color)
		{
			string text = this.GetSPDetailsLevel(chaoLevel);
			if (string.IsNullOrEmpty(text))
			{
				text = this.GetDetailsLevel(chaoLevel);
			}
			else
			{
				text = color + text + "[-]";
				text += "\n";
				text += this.GetDetailsLevel(chaoLevel);
			}
			return text;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0003FFE8 File Offset: 0x0003E1E8
		private string GetLoadingPageDetailLevelPlusSP(int chaoLevel, string color)
		{
			string text = this.GetSPLoadingLongDetailsLevel(chaoLevel);
			if (string.IsNullOrEmpty(text))
			{
				text = this.GetLoadingLongDetailsLevel(chaoLevel);
			}
			else
			{
				text = color + text + "[-]";
				text += "\n";
				text += this.GetLoadingLongDetailsLevel(chaoLevel);
			}
			return text;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00040040 File Offset: 0x0003E240
		public string GetSPLoadingLongDetailsLevel(int chaoLevel)
		{
			string result = string.Empty;
			for (int i = 0; i < this.abilityNum; i++)
			{
				this.currentAbility = i;
				if (EventManager.IsVaildEvent(this.eventId))
				{
					result = this.GetLoadingLongDetailsLevel(chaoLevel);
					break;
				}
			}
			this.currentAbility = 0;
			return result;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00040098 File Offset: 0x0003E298
		private bool IsRateText(string text)
		{
			bool result = false;
			if (!string.IsNullOrEmpty(text) && text.IndexOf("{RATE") != -1)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000400C8 File Offset: 0x0003E2C8
		private Dictionary<string, string> CreateReplacesDic(string targetText, float param1, float param2)
		{
			return this.CreateReplacesDic(targetText, new List<float>
			{
				param1,
				param2
			});
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000400F4 File Offset: 0x0003E2F4
		private Dictionary<string, string> CreateReplacesDic(string targetText, float param1, float param2, float param3, float param4)
		{
			return this.CreateReplacesDic(targetText, new List<float>
			{
				param1,
				param2,
				param3,
				param4
			});
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00040130 File Offset: 0x0003E330
		private Dictionary<string, string> CreateReplacesDic(string targetText, List<float> paramList)
		{
			if (paramList == null || paramList.Count <= 0)
			{
				return null;
			}
			int count = paramList.Count;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			bool flag = this.IsRateText(targetText);
			for (int i = 0; i < count; i++)
			{
				int num = i + 1;
				int num2 = (int)paramList[i];
				dictionary.Add("{PARAM" + num + "}", num2.ToString());
				if (flag)
				{
					float num3 = (paramList[i] + 100f) / 100f;
					dictionary.Add("{RATE" + num + "}", num3.ToString());
				}
			}
			return dictionary;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x000401FC File Offset: 0x0003E3FC
		public string GetLoadingLongDetailsLevel(int chaoLevel)
		{
			if (ChaoTableUtility.IsKingArthur(this.id))
			{
				return this.GetKingArtherDetailsLevel(chaoLevel, true);
			}
			string loadingLongDetails = this.loadingLongDetails;
			float param = 0f;
			float param2 = 0f;
			if (chaoLevel >= 0)
			{
				if (chaoLevel < this.abilityValue.Length)
				{
					param = this.abilityValue[chaoLevel];
				}
				if (chaoLevel < this.bonusAbilityValue.Length)
				{
					param2 = this.bonusAbilityValue[chaoLevel];
				}
			}
			Dictionary<string, string> replaceDic = this.CreateReplacesDic(loadingLongDetails, param, param2);
			return TextUtility.Replaces(loadingLongDetails, replaceDic);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00040280 File Offset: 0x0003E480
		public string GetDetailsLevel(int chaoLevel)
		{
			if (ChaoTableUtility.IsKingArthur(this.id))
			{
				return this.GetKingArtherDetailsLevel(chaoLevel, false);
			}
			string details = this.details;
			float param = 0f;
			float param2 = 0f;
			if (chaoLevel >= 0)
			{
				if (chaoLevel < this.abilityValue.Length)
				{
					param = this.abilityValue[chaoLevel];
				}
				if (chaoLevel < this.bonusAbilityValue.Length)
				{
					param2 = this.bonusAbilityValue[chaoLevel];
				}
			}
			Dictionary<string, string> replaceDic = this.CreateReplacesDic(details, param, param2);
			return TextUtility.Replaces(details, replaceDic);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00040304 File Offset: 0x0003E504
		public string GetKingArtherDetailsLevel(int chaoLevel, bool loadingLoingDetalFlag)
		{
			string text = (!loadingLoingDetalFlag) ? this.details : this.loadingLongDetails;
			float param = 0f;
			float param2 = 0f;
			float param3 = 0f;
			float param4 = 0f;
			if (chaoLevel >= 0)
			{
				if (chaoLevel < this.abilityValue.Length)
				{
					param = this.abilityValue[chaoLevel];
				}
				if (chaoLevel < this.bonusAbilityValue.Length)
				{
					param2 = this.bonusAbilityValue[chaoLevel];
				}
				this.currentAbility = 1;
				if (chaoLevel < this.abilityValue.Length)
				{
					param3 = this.abilityValue[chaoLevel];
				}
				if (chaoLevel < this.bonusAbilityValue.Length)
				{
					param4 = this.bonusAbilityValue[chaoLevel];
				}
				this.currentAbility = 0;
			}
			Dictionary<string, string> replaceDic = this.CreateReplacesDic(text, param, param2, param3, param4);
			return TextUtility.Replaces(text, replaceDic);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000403CC File Offset: 0x0003E5CC
		public string GetSPDetailsLevel(int chaoLevel)
		{
			string result = string.Empty;
			for (int i = 0; i < this.abilityNum; i++)
			{
				this.currentAbility = i;
				if (EventManager.IsVaildEvent(this.eventId))
				{
					result = this.GetDetailsLevel(chaoLevel);
					break;
				}
			}
			this.currentAbility = 0;
			return result;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00040424 File Offset: 0x0003E624
		public string GetGrowDetailLevelPlusSP(int chaoLevel)
		{
			string text = this.GetSPGrowDetailsLevel(chaoLevel);
			if (string.IsNullOrEmpty(text))
			{
				text = this.GetGrowDetailsLevel(chaoLevel);
			}
			else
			{
				text += "\n\n";
				text += this.GetGrowDetailsLevel(chaoLevel);
			}
			return text;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0004046C File Offset: 0x0003E66C
		public string GetGrowDetailsLevel(int chaoLevel)
		{
			if (ChaoTableUtility.IsKingArthur(this.id))
			{
				return this.GetKingArtherGrowDetailsLevel(chaoLevel);
			}
			float param = 0f;
			float param2 = 0f;
			float param3 = 0f;
			float param4 = 0f;
			int num = chaoLevel - 1;
			if (num >= 0)
			{
				if (chaoLevel < this.abilityValue.Length)
				{
					param = this.abilityValue[chaoLevel];
				}
				if (chaoLevel < this.bonusAbilityValue.Length)
				{
					param2 = this.bonusAbilityValue[chaoLevel];
				}
				if (num < this.abilityValue.Length)
				{
					param3 = this.abilityValue[num];
				}
				if (num < this.bonusAbilityValue.Length)
				{
					param4 = this.bonusAbilityValue[num];
				}
			}
			Dictionary<string, string> replaceDic = this.CreateReplacesDic(this.growDetails, param, param2, param3, param4);
			return TextUtility.Replaces(this.growDetails, replaceDic);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00040538 File Offset: 0x0003E738
		private string GetKingArtherGrowDetailsLevel(int chaoLevel)
		{
			float[] array = new float[8];
			int num = chaoLevel - 1;
			if (num >= 0)
			{
				if (chaoLevel < this.abilityValue.Length)
				{
					array[0] = this.abilityValue[chaoLevel];
				}
				if (chaoLevel < this.bonusAbilityValue.Length)
				{
					array[1] = this.bonusAbilityValue[chaoLevel];
				}
				if (num < this.abilityValue.Length)
				{
					array[2] = this.abilityValue[num];
				}
				if (num < this.bonusAbilityValue.Length)
				{
					array[3] = this.bonusAbilityValue[num];
				}
				this.currentAbility = 1;
				if (chaoLevel < this.abilityValue.Length)
				{
					array[4] = this.abilityValue[chaoLevel];
				}
				if (chaoLevel < this.bonusAbilityValue.Length)
				{
					array[5] = this.bonusAbilityValue[chaoLevel];
				}
				if (num < this.abilityValue.Length)
				{
					array[6] = this.abilityValue[num];
				}
				if (num < this.bonusAbilityValue.Length)
				{
					array[7] = this.bonusAbilityValue[num];
				}
				this.currentAbility = 0;
			}
			List<float> paramList = new List<float>(array);
			Dictionary<string, string> replaceDic = this.CreateReplacesDic(this.growDetails, paramList);
			return TextUtility.Replaces(this.growDetails, replaceDic);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00040650 File Offset: 0x0003E850
		public string GetSPGrowDetailsLevel(int chaoLevel)
		{
			string text = string.Empty;
			for (int i = 0; i < this.abilityNum; i++)
			{
				this.currentAbility = i;
				if (EventManager.IsVaildEvent(this.eventId))
				{
					text = this.GetGrowDetailsLevel(chaoLevel);
					if (!string.IsNullOrEmpty(text))
					{
						text = "[ffff00]" + text + "[ffffff]";
					}
					break;
				}
			}
			this.currentAbility = 0;
			return text;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x000406C4 File Offset: 0x0003E8C4
		public string GetLoadingDetailsLevel(int chaoLevel)
		{
			if (ChaoTableUtility.IsKingArthur(this.id))
			{
				return this.GetKingArtherLoadingDetailsLevel(chaoLevel);
			}
			float param = 0f;
			float param2 = 0f;
			if (chaoLevel >= 0)
			{
				if (chaoLevel < this.abilityValue.Length)
				{
					param = this.abilityValue[chaoLevel];
				}
				if (chaoLevel < this.bonusAbilityValue.Length)
				{
					param2 = this.bonusAbilityValue[chaoLevel];
				}
			}
			Dictionary<string, string> replaceDic = this.CreateReplacesDic(this.loadingDetails, param, param2);
			return TextUtility.Replaces(this.loadingDetails, replaceDic);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00040748 File Offset: 0x0003E948
		public string GetKingArtherLoadingDetailsLevel(int chaoLevel)
		{
			float param = 0f;
			float param2 = 0f;
			float param3 = 0f;
			float param4 = 0f;
			if (chaoLevel >= 0)
			{
				if (chaoLevel < this.abilityValue.Length)
				{
					param = this.abilityValue[chaoLevel];
				}
				if (chaoLevel < this.bonusAbilityValue.Length)
				{
					param2 = this.bonusAbilityValue[chaoLevel];
				}
				this.currentAbility = 1;
				if (chaoLevel < this.abilityValue.Length)
				{
					param3 = this.abilityValue[chaoLevel];
				}
				if (chaoLevel < this.bonusAbilityValue.Length)
				{
					param4 = this.bonusAbilityValue[chaoLevel];
				}
				this.currentAbility = 0;
			}
			Dictionary<string, string> replaceDic = this.CreateReplacesDic(this.loadingDetails, param, param2, param3, param4);
			return TextUtility.Replaces(this.loadingDetails, replaceDic);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00040800 File Offset: 0x0003EA00
		public string GetSPLoadingDetailsLevel(int chaoLevel)
		{
			string result = string.Empty;
			for (int i = 0; i < this.abilityNum; i++)
			{
				this.currentAbility = i;
				if (EventManager.IsVaildEvent(this.eventId))
				{
					result = this.GetLoadingDetailsLevel(chaoLevel);
					break;
				}
			}
			this.currentAbility = 0;
			return result;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00040858 File Offset: 0x0003EA58
		public string GetMainMenuDetailsLevel(int chaoLevel)
		{
			if (ChaoTableUtility.IsKingArthur(this.id))
			{
				return this.GetKingArtherMainMenuDetailsLevel(chaoLevel);
			}
			float param = 0f;
			float param2 = 0f;
			if (chaoLevel >= 0)
			{
				if (chaoLevel < this.abilityValue.Length)
				{
					param = this.abilityValue[chaoLevel];
				}
				if (chaoLevel < this.bonusAbilityValue.Length)
				{
					param2 = this.bonusAbilityValue[chaoLevel];
				}
			}
			Dictionary<string, string> replaceDic = this.CreateReplacesDic(this.menuDetails, param, param2);
			return TextUtility.Replaces(this.menuDetails, replaceDic);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x000408DC File Offset: 0x0003EADC
		public string GetKingArtherMainMenuDetailsLevel(int chaoLevel)
		{
			float param = 0f;
			float param2 = 0f;
			float param3 = 0f;
			float param4 = 0f;
			if (chaoLevel >= 0)
			{
				if (chaoLevel < this.abilityValue.Length)
				{
					param = this.abilityValue[chaoLevel];
				}
				if (chaoLevel < this.bonusAbilityValue.Length)
				{
					param2 = this.bonusAbilityValue[chaoLevel];
				}
				this.currentAbility = 1;
				if (chaoLevel < this.abilityValue.Length)
				{
					param3 = this.abilityValue[chaoLevel];
				}
				if (chaoLevel < this.bonusAbilityValue.Length)
				{
					param4 = this.bonusAbilityValue[chaoLevel];
				}
				this.currentAbility = 0;
			}
			Dictionary<string, string> replaceDic = this.CreateReplacesDic(this.menuDetails, param, param2, param3, param4);
			return TextUtility.Replaces(this.menuDetails, replaceDic);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00040994 File Offset: 0x0003EB94
		public string GetSPMainMenuDetailsLevel(int chaoLevel)
		{
			string text = string.Empty;
			for (int i = 0; i < this.abilityNum; i++)
			{
				this.currentAbility = i;
				if (EventManager.IsVaildEvent(this.eventId))
				{
					text = this.GetMainMenuDetailsLevel(chaoLevel);
					if (!string.IsNullOrEmpty(text))
					{
						text = "[ffff00]" + text + "[-]";
					}
					break;
				}
			}
			this.currentAbility = 0;
			if (string.IsNullOrEmpty(text))
			{
				text = this.GetMainMenuDetailsLevel(chaoLevel);
			}
			return text;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00040A1C File Offset: 0x0003EC1C
		public void StatusUpdate()
		{
			for (int i = 0; i < this.m_abilityStatusData.Length; i++)
			{
				this.m_abilityStatusData[i].update(this.id);
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00040A58 File Offset: 0x0003EC58
		public bool SetChaoAbility(int abilityEventId)
		{
			bool result = false;
			if (this.abilityNum > 1)
			{
				for (int i = 0; i < this.abilityNum; i++)
				{
					ChaoDataAbilityStatus chaoDataAbilityStatus = this.m_abilityStatusData[i];
					if (abilityEventId == chaoDataAbilityStatus.eventId)
					{
						this.currentAbility = i;
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00040AB0 File Offset: 0x0003ECB0
		public bool SetChaoAbilityIndex(int abilityIndex)
		{
			bool result = false;
			if (this.abilityNum > 1 && abilityIndex >= 0 && this.abilityNum > abilityIndex)
			{
				this.currentAbility = abilityIndex;
				result = true;
			}
			return result;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00040AE8 File Offset: 0x0003ECE8
		public void accept(ref ChaoDataVisitorBase visitor)
		{
			visitor.visit(this);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00040AF4 File Offset: 0x0003ECF4
		public static int ChaoCompareById(ChaoData x, ChaoData y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			return x.id - y.id;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00040B2C File Offset: 0x0003ED2C
		public void SetDummyData()
		{
			this.id = 0;
			this.rarity = ChaoData.Rarity.NORMAL;
			this.charaAtribute = CharacterAttribute.SPEED;
			this.chaoAbilitys = new ChaoAbility[1];
			this.chaoAbilitys[0] = ChaoAbility.ALL_BONUS_COUNT;
			this.m_abilityStatus = new string[1];
			this.m_abilityStatus[0] = "dummy";
			this.m_abilityStatusData = new ChaoDataAbilityStatus[1];
			this.m_abilityStatusData[0] = new ChaoDataAbilityStatus("0,0", 0, 0);
			this.currentAbility = 0;
		}

		// Token: 0x040008A7 RID: 2215
		private const string SP_TEXT_COLOR = "[ffff00]";

		// Token: 0x040008A8 RID: 2216
		private const string SP_LOADING_TEXT_COLOR = "[e0b000]";

		// Token: 0x040008A9 RID: 2217
		private const string TEXT_WHITE_COLOR = "[ffffff]";

		// Token: 0x040008AA RID: 2218
		public const int ID_NONE = -1;

		// Token: 0x040008AB RID: 2219
		public const int ID_MIN = 0;

		// Token: 0x040008AC RID: 2220
		public const int LEVEL_NONE = -1;

		// Token: 0x040008AD RID: 2221
		public const int LEVEL_MIN = 0;

		// Token: 0x040008AE RID: 2222
		public const int LEVEL_MAX = 10;

		// Token: 0x040008AF RID: 2223
		private int m_currentAbility;

		// Token: 0x040008B0 RID: 2224
		private string[] m_abilityStatus;

		// Token: 0x040008B1 RID: 2225
		private ChaoDataAbilityStatus[] m_abilityStatusData;

		// Token: 0x0200017D RID: 381
		public enum Rarity
		{
			// Token: 0x040008BA RID: 2234
			NORMAL,
			// Token: 0x040008BB RID: 2235
			RARE,
			// Token: 0x040008BC RID: 2236
			SRARE,
			// Token: 0x040008BD RID: 2237
			NONE
		}
	}
}
