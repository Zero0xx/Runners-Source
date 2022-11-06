using System;
using UnityEngine;

namespace Message
{
	// Token: 0x020005D4 RID: 1492
	public class MsgOnDrawingRings : MessageBase
	{
		// Token: 0x06002B48 RID: 11080 RVA: 0x0010A0EC File Offset: 0x001082EC
		public MsgOnDrawingRings() : base(24583)
		{
			this.m_chaoAbility = ChaoAbility.UNKNOWN;
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x0010A100 File Offset: 0x00108300
		public MsgOnDrawingRings(ChaoAbility chaoAbility) : base(24583)
		{
			this.m_chaoAbility = chaoAbility;
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x0010A114 File Offset: 0x00108314
		public MsgOnDrawingRings(GameObject target) : base(24583)
		{
			this.m_target = target;
			this.m_chaoAbility = ChaoAbility.UNKNOWN;
		}

		// Token: 0x04002805 RID: 10245
		public ChaoAbility m_chaoAbility;

		// Token: 0x04002806 RID: 10246
		public GameObject m_target;
	}
}
