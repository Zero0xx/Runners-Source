using System;

namespace Message
{
	// Token: 0x020005E7 RID: 1511
	public class MsgActivePointMarker : MessageBase
	{
		// Token: 0x06002B61 RID: 11105 RVA: 0x0010A390 File Offset: 0x00108590
		public MsgActivePointMarker(PointMarkerType type) : base(12328)
		{
			this.m_type = type;
		}

		// Token: 0x04002828 RID: 10280
		public PointMarkerType m_type;

		// Token: 0x04002829 RID: 10281
		public bool m_activated;
	}
}
