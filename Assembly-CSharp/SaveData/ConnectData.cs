using System;

namespace SaveData
{
	// Token: 0x020002AF RID: 687
	public class ConnectData
	{
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x00069950 File Offset: 0x00067B50
		// (set) Token: 0x0600134A RID: 4938 RVA: 0x00069958 File Offset: 0x00067B58
		public bool ReplaceMessageBox
		{
			get
			{
				return this.m_replaceMessageBox;
			}
			set
			{
				this.m_replaceMessageBox = value;
			}
		}

		// Token: 0x040010D0 RID: 4304
		private bool m_replaceMessageBox;
	}
}
