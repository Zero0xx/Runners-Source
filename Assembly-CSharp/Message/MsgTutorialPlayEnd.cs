using System;
using Tutorial;
using UnityEngine;

namespace Message
{
	// Token: 0x0200064C RID: 1612
	public class MsgTutorialPlayEnd : MessageBase
	{
		// Token: 0x06002BC6 RID: 11206 RVA: 0x0010AB0C File Offset: 0x00108D0C
		public MsgTutorialPlayEnd(bool complete, bool retry, EventID nextEventID, Vector3 pos, Quaternion rot) : base(12335)
		{
			this.m_complete = complete;
			this.m_retry = retry;
			this.m_nextEventID = nextEventID;
			this.m_pos = pos;
			this.m_rot = rot;
		}

		// Token: 0x040028A4 RID: 10404
		public readonly bool m_complete;

		// Token: 0x040028A5 RID: 10405
		public readonly bool m_retry;

		// Token: 0x040028A6 RID: 10406
		public readonly EventID m_nextEventID;

		// Token: 0x040028A7 RID: 10407
		public readonly Vector3 m_pos;

		// Token: 0x040028A8 RID: 10408
		public readonly Quaternion m_rot;
	}
}
