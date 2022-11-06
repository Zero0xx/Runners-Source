using System;
using System.Collections.Generic;
using Tutorial;

namespace Message
{
	// Token: 0x0200064D RID: 1613
	public class MsgTutorialClear : MessageBase
	{
		// Token: 0x06002BC7 RID: 11207 RVA: 0x0010AB4C File Offset: 0x00108D4C
		public MsgTutorialClear(EventID id) : base(12336)
		{
			this.Add(id);
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x0010AB6C File Offset: 0x00108D6C
		public void Add(EventID id)
		{
			MsgTutorialClear.Data item = default(MsgTutorialClear.Data);
			item.eventid = id;
			this.m_data.Add(item);
		}

		// Token: 0x040028A9 RID: 10409
		public List<MsgTutorialClear.Data> m_data = new List<MsgTutorialClear.Data>();

		// Token: 0x0200064E RID: 1614
		public struct Data
		{
			// Token: 0x040028AA RID: 10410
			public EventID eventid;
		}
	}
}
