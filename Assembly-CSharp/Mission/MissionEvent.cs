using System;

namespace Mission
{
	// Token: 0x02000296 RID: 662
	public class MissionEvent
	{
		// Token: 0x0600122E RID: 4654 RVA: 0x00065E78 File Offset: 0x00064078
		public MissionEvent(EventID id, long num)
		{
			this.m_id = id;
			this.m_num = num;
		}

		// Token: 0x04001053 RID: 4179
		public EventID m_id;

		// Token: 0x04001054 RID: 4180
		public long m_num;
	}
}
