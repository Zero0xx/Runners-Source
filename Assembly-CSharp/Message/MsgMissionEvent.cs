using System;
using System.Collections.Generic;
using Mission;

namespace Message
{
	// Token: 0x020005C6 RID: 1478
	public class MsgMissionEvent : MessageBase
	{
		// Token: 0x06002B36 RID: 11062 RVA: 0x00109ECC File Offset: 0x001080CC
		public MsgMissionEvent() : base(12318)
		{
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x00109EE4 File Offset: 0x001080E4
		public MsgMissionEvent(EventID id_) : base(12318)
		{
			this.Add(id_, 0L);
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x00109F08 File Offset: 0x00108108
		public MsgMissionEvent(EventID id_, long num) : base(12318)
		{
			this.Add(id_, num);
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x00109F28 File Offset: 0x00108128
		public void Add(EventID id_, long num)
		{
			MsgMissionEvent.Data item = default(MsgMissionEvent.Data);
			item.eventid = id_;
			item.num = num;
			this.m_missions.Add(item);
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x00109F5C File Offset: 0x0010815C
		public void Add(EventID id_)
		{
			this.Add(id_, 0L);
		}

		// Token: 0x040027E7 RID: 10215
		public List<MsgMissionEvent.Data> m_missions = new List<MsgMissionEvent.Data>();

		// Token: 0x020005C7 RID: 1479
		public struct Data
		{
			// Token: 0x040027E8 RID: 10216
			public EventID eventid;

			// Token: 0x040027E9 RID: 10217
			public long num;
		}
	}
}
