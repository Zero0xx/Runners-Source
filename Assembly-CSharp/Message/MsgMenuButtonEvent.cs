using System;

namespace Message
{
	// Token: 0x020005BE RID: 1470
	public class MsgMenuButtonEvent : MessageBase
	{
		// Token: 0x06002B2E RID: 11054 RVA: 0x00109E44 File Offset: 0x00108044
		public MsgMenuButtonEvent(ButtonInfoTable.ButtonType type) : base(57344)
		{
			this.m_buttonType = type;
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x00109E58 File Offset: 0x00108058
		public ButtonInfoTable.ButtonType ButtonType
		{
			get
			{
				return this.m_buttonType;
			}
		}

		// Token: 0x040027B7 RID: 10167
		private ButtonInfoTable.ButtonType m_buttonType;

		// Token: 0x040027B8 RID: 10168
		public bool m_clearHistories;
	}
}
