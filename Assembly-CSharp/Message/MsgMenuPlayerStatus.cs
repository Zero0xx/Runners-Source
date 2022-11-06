using System;

namespace Message
{
	// Token: 0x020005BF RID: 1471
	public class MsgMenuPlayerStatus : MessageBase
	{
		// Token: 0x06002B30 RID: 11056 RVA: 0x00109E60 File Offset: 0x00108060
		public MsgMenuPlayerStatus(MsgMenuPlayerStatus.StatusType status) : base(57344)
		{
			this.m_status = status;
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06002B31 RID: 11057 RVA: 0x00109E74 File Offset: 0x00108074
		public MsgMenuPlayerStatus.StatusType Status
		{
			get
			{
				return this.m_status;
			}
		}

		// Token: 0x040027B9 RID: 10169
		private MsgMenuPlayerStatus.StatusType m_status;

		// Token: 0x020005C0 RID: 1472
		public enum StatusType
		{
			// Token: 0x040027BB RID: 10171
			USE_SUB_CHAR
		}
	}
}
