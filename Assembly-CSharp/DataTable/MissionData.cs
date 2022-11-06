using System;

namespace DataTable
{
	// Token: 0x0200019D RID: 413
	public class MissionData
	{
		// Token: 0x06000BE1 RID: 3041 RVA: 0x00044AC8 File Offset: 0x00042CC8
		public MissionData()
		{
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00044AD0 File Offset: 0x00042CD0
		public MissionData(int id, MissionData.Type type, string text, int quota, bool save)
		{
			this.id = id;
			this.type = type;
			this.text = text;
			this.quota = quota;
			this.save = save;
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x00044B08 File Offset: 0x00042D08
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x00044B10 File Offset: 0x00042D10
		public int id { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00044B1C File Offset: 0x00042D1C
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x00044B24 File Offset: 0x00042D24
		public MissionData.Type type { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x00044B30 File Offset: 0x00042D30
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x00044B38 File Offset: 0x00042D38
		public string text { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x00044B44 File Offset: 0x00042D44
		// (set) Token: 0x06000BEA RID: 3050 RVA: 0x00044B4C File Offset: 0x00042D4C
		public int quota { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00044B58 File Offset: 0x00042D58
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x00044B60 File Offset: 0x00042D60
		public bool save { get; set; }

		// Token: 0x06000BED RID: 3053 RVA: 0x00044B6C File Offset: 0x00042D6C
		public void SetText(string text)
		{
			this.text = text;
		}

		// Token: 0x04000970 RID: 2416
		public const int ID_MIN_VALUE = 1;

		// Token: 0x0200019E RID: 414
		public enum Type
		{
			// Token: 0x04000977 RID: 2423
			ENEMY,
			// Token: 0x04000978 RID: 2424
			G_ENEMY,
			// Token: 0x04000979 RID: 2425
			DISTANCE,
			// Token: 0x0400097A RID: 2426
			ANIMAL,
			// Token: 0x0400097B RID: 2427
			SCORE,
			// Token: 0x0400097C RID: 2428
			RING,
			// Token: 0x0400097D RID: 2429
			COUNT,
			// Token: 0x0400097E RID: 2430
			NONE = -1
		}
	}
}
