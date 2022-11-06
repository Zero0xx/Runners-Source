using System;

namespace SaveData
{
	// Token: 0x020002B3 RID: 691
	public class GameIDData
	{
		// Token: 0x0600136F RID: 4975 RVA: 0x00069E54 File Offset: 0x00068054
		public GameIDData()
		{
			this.Init();
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x00069E64 File Offset: 0x00068064
		// (set) Token: 0x06001371 RID: 4977 RVA: 0x00069E6C File Offset: 0x0006806C
		public string id { get; set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x00069E78 File Offset: 0x00068078
		// (set) Token: 0x06001373 RID: 4979 RVA: 0x00069E80 File Offset: 0x00068080
		public string password { get; set; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x00069E8C File Offset: 0x0006808C
		// (set) Token: 0x06001375 RID: 4981 RVA: 0x00069E94 File Offset: 0x00068094
		public string device { get; set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x00069EA0 File Offset: 0x000680A0
		// (set) Token: 0x06001377 RID: 4983 RVA: 0x00069EA8 File Offset: 0x000680A8
		public string takeoverId { get; set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x00069EB4 File Offset: 0x000680B4
		// (set) Token: 0x06001379 RID: 4985 RVA: 0x00069EBC File Offset: 0x000680BC
		public string takeoverPassword { get; set; }

		// Token: 0x0600137A RID: 4986 RVA: 0x00069EC8 File Offset: 0x000680C8
		public void Init()
		{
			this.id = "0";
			this.password = string.Empty;
			this.device = string.Empty;
			this.takeoverId = string.Empty;
			this.takeoverPassword = string.Empty;
		}

		// Token: 0x040010DD RID: 4317
		public const string NoUserID = "0";

		// Token: 0x040010DE RID: 4318
		public const string KeyID = "aa7329ab4330306fbdd6dbe9b85c96be";

		// Token: 0x040010DF RID: 4319
		public const string KeyPass = "48521cd1266052bfc25718720e91fa83";
	}
}
