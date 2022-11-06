using System;

namespace Message
{
	// Token: 0x0200059F RID: 1439
	public class MsgChaoAbilityEnd : MessageBase
	{
		// Token: 0x06002B0B RID: 11019 RVA: 0x00109AE0 File Offset: 0x00107CE0
		public MsgChaoAbilityEnd(ChaoAbility[] ability) : base(21762)
		{
			this.m_ability = ability;
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x00109AF4 File Offset: 0x00107CF4
		public MsgChaoAbilityEnd(ChaoAbility ability) : base(21762)
		{
			this.m_ability = new ChaoAbility[1];
			this.m_ability[0] = ability;
		}

		// Token: 0x0400277F RID: 10111
		public readonly ChaoAbility[] m_ability;
	}
}
