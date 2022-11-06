using System;

namespace Message
{
	// Token: 0x02000642 RID: 1602
	public class MsgDisableEquipItem : MessageBase
	{
		// Token: 0x06002BBC RID: 11196 RVA: 0x0010AA54 File Offset: 0x00108C54
		public MsgDisableEquipItem(bool disable) : base(12320)
		{
			this.m_disable = disable;
		}

		// Token: 0x0400289E RID: 10398
		public bool m_disable;
	}
}
