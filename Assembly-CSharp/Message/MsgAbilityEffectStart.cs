using System;

namespace Message
{
	// Token: 0x02000581 RID: 1409
	public class MsgAbilityEffectStart : MessageBase
	{
		// Token: 0x06002AE6 RID: 10982 RVA: 0x001096B0 File Offset: 0x001078B0
		public MsgAbilityEffectStart(ChaoAbility ability, string effectName, bool loop, bool center) : base(16387)
		{
			this.m_ability = ability;
			this.m_effectName = effectName;
			this.m_loop = loop;
			this.m_center = center;
		}

		// Token: 0x0400273A RID: 10042
		public readonly ChaoAbility m_ability;

		// Token: 0x0400273B RID: 10043
		public readonly string m_effectName;

		// Token: 0x0400273C RID: 10044
		public readonly bool m_loop;

		// Token: 0x0400273D RID: 10045
		public readonly bool m_center;
	}
}
