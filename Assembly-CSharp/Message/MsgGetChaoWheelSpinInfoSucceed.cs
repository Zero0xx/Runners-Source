using System;
using System.Collections.Generic;

namespace Message
{
	// Token: 0x02000608 RID: 1544
	public class MsgGetChaoWheelSpinInfoSucceed : MessageBase
	{
		// Token: 0x06002B82 RID: 11138 RVA: 0x0010A5B0 File Offset: 0x001087B0
		public MsgGetChaoWheelSpinInfoSucceed() : base(61466)
		{
		}

		// Token: 0x04002858 RID: 10328
		public List<ServerWheelSpinInfo> m_wheelSpinInfos;
	}
}
