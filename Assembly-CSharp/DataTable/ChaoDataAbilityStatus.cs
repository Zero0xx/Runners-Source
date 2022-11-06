using System;
using Text;

namespace DataTable
{
	// Token: 0x0200017B RID: 379
	public class ChaoDataAbilityStatus
	{
		// Token: 0x06000AB5 RID: 2741 RVA: 0x0003F5A4 File Offset: 0x0003D7A4
		public ChaoDataAbilityStatus(string status, int id, int abilityIndex)
		{
			this.m_chaoId = id;
			this.m_abilityIndex = abilityIndex;
			this.m_orgAbilityStatus = status;
			this.update(id);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0003F5D4 File Offset: 0x0003D7D4
		public ChaoDataAbilityStatus(ChaoDataAbilityStatus src)
		{
			this.m_chaoId = src.m_chaoId;
			this.m_orgAbilityStatus = src.m_orgAbilityStatus;
			this.maxLevel = src.maxLevel;
			this.eventId = src.eventId;
			this.extraValue = src.extraValue;
			this.bgmName = src.bgmName;
			this.cueSheetName = src.cueSheetName;
			this.abilityValues = new float[src.abilityValues.Length];
			for (int i = 0; i < src.abilityValues.Length; i++)
			{
				this.abilityValues[i] = src.abilityValues[i];
			}
			this.bonusAbilityValues = new float[src.bonusAbilityValues.Length];
			for (int j = 0; j < src.bonusAbilityValues.Length; j++)
			{
				this.bonusAbilityValues[j] = src.bonusAbilityValues[j];
			}
			this.details = src.details;
			this.loadingDetails = src.loadingDetails;
			this.growDetails = src.growDetails;
			this.menuDetails = src.menuDetails;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0003F6E4 File Offset: 0x0003D8E4
		// (set) Token: 0x06000AB8 RID: 2744 RVA: 0x0003F6EC File Offset: 0x0003D8EC
		public int maxLevel { get; private set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0003F6F8 File Offset: 0x0003D8F8
		// (set) Token: 0x06000ABA RID: 2746 RVA: 0x0003F700 File Offset: 0x0003D900
		public int eventId { get; private set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0003F70C File Offset: 0x0003D90C
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x0003F714 File Offset: 0x0003D914
		public float extraValue { get; private set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0003F720 File Offset: 0x0003D920
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x0003F728 File Offset: 0x0003D928
		public string bgmName { get; private set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0003F734 File Offset: 0x0003D934
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x0003F73C File Offset: 0x0003D93C
		public string cueSheetName { get; private set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0003F748 File Offset: 0x0003D948
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x0003F750 File Offset: 0x0003D950
		public float[] abilityValues { get; private set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0003F75C File Offset: 0x0003D95C
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x0003F764 File Offset: 0x0003D964
		public float[] bonusAbilityValues { get; private set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0003F770 File Offset: 0x0003D970
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x0003F778 File Offset: 0x0003D978
		public string details { get; private set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0003F784 File Offset: 0x0003D984
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x0003F78C File Offset: 0x0003D98C
		public string loadingDetails { get; private set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0003F798 File Offset: 0x0003D998
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x0003F7A0 File Offset: 0x0003D9A0
		public string loadingLongDetails { get; private set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0003F7AC File Offset: 0x0003D9AC
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x0003F7B4 File Offset: 0x0003D9B4
		public string growDetails { get; private set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0003F7C0 File Offset: 0x0003D9C0
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x0003F7C8 File Offset: 0x0003D9C8
		public string menuDetails { get; private set; }

		// Token: 0x06000ACF RID: 2767 RVA: 0x0003F7D4 File Offset: 0x0003D9D4
		public void update(int id)
		{
			this.m_chaoId = id;
			int num = 4;
			string[] array = this.m_orgAbilityStatus.Split(new char[]
			{
				','
			});
			this.maxLevel = (array.Length - num) / 2;
			if (array.Length > 0)
			{
				this.eventId = int.Parse(array[0]);
			}
			if (array.Length > 1)
			{
				this.extraValue = float.Parse(array[1]);
			}
			if (array.Length > 2)
			{
				if (array[2] == "non")
				{
					this.cueSheetName = string.Empty;
				}
				else
				{
					this.cueSheetName = array[2];
				}
			}
			if (array.Length > 3)
			{
				if (array[3] == "non")
				{
					this.bgmName = string.Empty;
				}
				else
				{
					this.bgmName = array[3];
				}
			}
			if (this.maxLevel > 0 && array.Length >= this.maxLevel * 2 + num)
			{
				this.abilityValues = new float[this.maxLevel];
				this.bonusAbilityValues = new float[this.maxLevel];
				for (int i = 0; i < this.maxLevel; i++)
				{
					this.abilityValues[i] = float.Parse(array[i + num]);
					this.bonusAbilityValues[i] = float.Parse(array[i + num + this.maxLevel]);
				}
			}
			if (ChaoTableUtility.IsKingArthur(id) && this.m_abilityIndex == 1)
			{
				return;
			}
			this.details = this.GetDetailsText("details");
			this.growDetails = this.GetDetailsText("grow_details");
			this.loadingDetails = this.GetDetailsText("loading_details");
			this.menuDetails = this.GetDetailsText("main_menu_details");
			this.loadingLongDetails = this.GetDetailsText("loading_long_details");
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0003F994 File Offset: 0x0003DB94
		private string GetDetailsText(string callId)
		{
			string chaoText;
			if (this.m_abilityIndex == 0)
			{
				chaoText = TextUtility.GetChaoText("Chao", callId + this.m_chaoId.ToString("D4"));
			}
			else
			{
				chaoText = TextUtility.GetChaoText("Chao", string.Concat(new object[]
				{
					callId,
					this.m_chaoId.ToString("D4"),
					"_",
					this.m_abilityIndex
				}));
			}
			return chaoText;
		}

		// Token: 0x04000898 RID: 2200
		private int m_chaoId;

		// Token: 0x04000899 RID: 2201
		private int m_abilityIndex;

		// Token: 0x0400089A RID: 2202
		private string m_orgAbilityStatus;
	}
}
