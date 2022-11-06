using System;

namespace Message
{
	// Token: 0x02000590 RID: 1424
	public class MsgBossCheckState : MessageBase
	{
		// Token: 0x06002AF4 RID: 10996 RVA: 0x00109864 File Offset: 0x00107A64
		public MsgBossCheckState(MsgBossCheckState.State state) : base(12323)
		{
			this.m_state = state;
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x00109878 File Offset: 0x00107A78
		public bool IsAttackOK()
		{
			return this.m_state == MsgBossCheckState.State.ATTACK_OK;
		}

		// Token: 0x0400275E RID: 10078
		private MsgBossCheckState.State m_state;

		// Token: 0x02000591 RID: 1425
		public enum State
		{
			// Token: 0x04002760 RID: 10080
			IDLE,
			// Token: 0x04002761 RID: 10081
			ATTACK_OK
		}
	}
}
