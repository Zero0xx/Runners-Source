using System;

namespace Message
{
	// Token: 0x020005B1 RID: 1457
	public class MsgEventButton : MessageBase
	{
		// Token: 0x06002B20 RID: 11040 RVA: 0x00109CB8 File Offset: 0x00107EB8
		public MsgEventButton(MsgEventButton.ButtonType type) : base(57344)
		{
			this.m_buttonType = type;
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06002B21 RID: 11041 RVA: 0x00109CD4 File Offset: 0x00107ED4
		public MsgEventButton.ButtonType Type
		{
			get
			{
				return this.m_buttonType;
			}
		}

		// Token: 0x0400279A RID: 10138
		private MsgEventButton.ButtonType m_buttonType = MsgEventButton.ButtonType.UNKNOWN;

		// Token: 0x020005B2 RID: 1458
		public enum ButtonType
		{
			// Token: 0x0400279C RID: 10140
			RAID_BOSS,
			// Token: 0x0400279D RID: 10141
			RAID_BOSS_BACK,
			// Token: 0x0400279E RID: 10142
			SPECIAL_STAGE,
			// Token: 0x0400279F RID: 10143
			SPECIAL_STAGE_BACK,
			// Token: 0x040027A0 RID: 10144
			COLLECT_EVENT,
			// Token: 0x040027A1 RID: 10145
			COLLECT_EVENT_BACK,
			// Token: 0x040027A2 RID: 10146
			UNKNOWN
		}
	}
}
