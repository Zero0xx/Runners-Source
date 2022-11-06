using System;

namespace DataTable
{
	// Token: 0x02000196 RID: 406
	public class InformationData
	{
		// Token: 0x06000BAB RID: 2987 RVA: 0x0004420C File Offset: 0x0004240C
		public InformationData()
		{
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00044214 File Offset: 0x00042414
		public InformationData(string tag, string url, string sfx)
		{
			this.tag = tag;
			this.url = url;
			this.sfx = sfx;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0004423C File Offset: 0x0004243C
		// (set) Token: 0x06000BAE RID: 2990 RVA: 0x00044244 File Offset: 0x00042444
		public string tag { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x00044250 File Offset: 0x00042450
		// (set) Token: 0x06000BB0 RID: 2992 RVA: 0x00044258 File Offset: 0x00042458
		public string url { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x00044264 File Offset: 0x00042464
		// (set) Token: 0x06000BB2 RID: 2994 RVA: 0x0004426C File Offset: 0x0004246C
		public string sfx { get; set; }
	}
}
