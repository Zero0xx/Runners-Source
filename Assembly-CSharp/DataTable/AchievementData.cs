using System;

namespace DataTable
{
	// Token: 0x02000177 RID: 375
	public class AchievementData
	{
		// Token: 0x06000A9B RID: 2715 RVA: 0x0003F3B0 File Offset: 0x0003D5B0
		public AchievementData()
		{
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0003F3B8 File Offset: 0x0003D5B8
		public AchievementData(int number, string explanation, AchievementData.Type type, int itemID, int value, string iosId, string androidId)
		{
			this.number = number;
			this.explanation = explanation;
			this.type = type;
			this.itemID = itemID;
			this.value = value;
			this.iosId = iosId;
			this.androidId = androidId;
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0003F400 File Offset: 0x0003D600
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x0003F408 File Offset: 0x0003D608
		public int number { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0003F414 File Offset: 0x0003D614
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x0003F41C File Offset: 0x0003D61C
		public string explanation { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0003F428 File Offset: 0x0003D628
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x0003F430 File Offset: 0x0003D630
		public AchievementData.Type type { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0003F43C File Offset: 0x0003D63C
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x0003F444 File Offset: 0x0003D644
		public int itemID { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0003F450 File Offset: 0x0003D650
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x0003F458 File Offset: 0x0003D658
		public int value { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0003F464 File Offset: 0x0003D664
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x0003F46C File Offset: 0x0003D66C
		public string iosId { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0003F478 File Offset: 0x0003D678
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x0003F480 File Offset: 0x0003D680
		public string androidId { get; set; }

		// Token: 0x06000AAB RID: 2731 RVA: 0x0003F48C File Offset: 0x0003D68C
		public string GetID()
		{
			return this.androidId;
		}

		// Token: 0x04000884 RID: 2180
		public const int ID_MIN_VALUE = 1;

		// Token: 0x02000178 RID: 376
		public enum Type
		{
			// Token: 0x0400088D RID: 2189
			ANIMAL,
			// Token: 0x0400088E RID: 2190
			DISTANCE,
			// Token: 0x0400088F RID: 2191
			PLAYER_OPEN,
			// Token: 0x04000890 RID: 2192
			PLAYER_LEVEL,
			// Token: 0x04000891 RID: 2193
			CHAO_OPEN,
			// Token: 0x04000892 RID: 2194
			CHAO_LEVEL,
			// Token: 0x04000893 RID: 2195
			COUNT,
			// Token: 0x04000894 RID: 2196
			NONE = -1
		}
	}
}
