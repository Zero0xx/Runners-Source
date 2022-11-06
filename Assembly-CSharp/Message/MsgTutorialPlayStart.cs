using System;
using Tutorial;

namespace Message
{
	// Token: 0x0200064A RID: 1610
	public class MsgTutorialPlayStart : MessageBase
	{
		// Token: 0x06002BC4 RID: 11204 RVA: 0x0010AAE8 File Offset: 0x00108CE8
		public MsgTutorialPlayStart(EventID eventID) : base(12333)
		{
			this.m_eventID = eventID;
		}

		// Token: 0x040028A3 RID: 10403
		public readonly EventID m_eventID;
	}
}
