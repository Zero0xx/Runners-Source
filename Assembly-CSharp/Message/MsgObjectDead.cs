using System;

namespace Message
{
	// Token: 0x020005AA RID: 1450
	public class MsgObjectDead : MessageBase
	{
		// Token: 0x06002B18 RID: 11032 RVA: 0x00109C10 File Offset: 0x00107E10
		public MsgObjectDead() : base(24586)
		{
			this.m_chaoAbility = ChaoAbility.UNKNOWN;
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x00109C24 File Offset: 0x00107E24
		public MsgObjectDead(ChaoAbility chaoAbility) : base(24586)
		{
			this.m_chaoAbility = chaoAbility;
		}

		// Token: 0x04002793 RID: 10131
		public ChaoAbility m_chaoAbility;
	}
}
