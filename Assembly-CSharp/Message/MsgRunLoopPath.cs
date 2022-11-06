using System;

namespace Message
{
	// Token: 0x020005EA RID: 1514
	public class MsgRunLoopPath : MessageBase
	{
		// Token: 0x06002B64 RID: 11108 RVA: 0x0010A3C4 File Offset: 0x001085C4
		public MsgRunLoopPath(PathComponent component) : base(20481)
		{
			this.m_component = component;
		}

		// Token: 0x0400282A RID: 10282
		public PathComponent m_component;
	}
}
