using System;

namespace Message
{
	// Token: 0x02000582 RID: 1410
	public class MsgAbilityEffectEnd : MessageBase
	{
		// Token: 0x06002AE7 RID: 10983 RVA: 0x001096E8 File Offset: 0x001078E8
		public MsgAbilityEffectEnd(ChaoAbility ability, string effectName) : base(16388)
		{
			this.m_ability = ability;
			this.m_effectName = effectName;
		}

		// Token: 0x0400273E RID: 10046
		public readonly ChaoAbility m_ability;

		// Token: 0x0400273F RID: 10047
		public readonly string m_effectName;
	}
}
