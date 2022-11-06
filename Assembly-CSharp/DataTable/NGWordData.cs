using System;

namespace DataTable
{
	// Token: 0x020001A0 RID: 416
	public class NGWordData
	{
		// Token: 0x06000BF8 RID: 3064 RVA: 0x00044D58 File Offset: 0x00042F58
		public NGWordData()
		{
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00044D60 File Offset: 0x00042F60
		public NGWordData(string word, int param)
		{
			this.word = word;
			this.param = param;
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00044D78 File Offset: 0x00042F78
		// (set) Token: 0x06000BFB RID: 3067 RVA: 0x00044D80 File Offset: 0x00042F80
		public string word { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00044D8C File Offset: 0x00042F8C
		// (set) Token: 0x06000BFD RID: 3069 RVA: 0x00044D94 File Offset: 0x00042F94
		public int param { get; set; }
	}
}
