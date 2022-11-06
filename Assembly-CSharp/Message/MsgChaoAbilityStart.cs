using System;

namespace Message
{
	// Token: 0x0200059E RID: 1438
	public class MsgChaoAbilityStart : MessageBase
	{
		// Token: 0x06002B09 RID: 11017 RVA: 0x00109A98 File Offset: 0x00107C98
		public MsgChaoAbilityStart(ChaoAbility[] ability) : base(21761)
		{
			this.m_ability = ability;
			this.m_flag = false;
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x00109AB4 File Offset: 0x00107CB4
		public MsgChaoAbilityStart(ChaoAbility ability) : base(21761)
		{
			this.m_ability = new ChaoAbility[1];
			this.m_ability[0] = ability;
			this.m_flag = false;
		}

		// Token: 0x0400277D RID: 10109
		public readonly ChaoAbility[] m_ability;

		// Token: 0x0400277E RID: 10110
		public bool m_flag;
	}
}
