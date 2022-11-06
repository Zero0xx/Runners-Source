using System;

namespace Message
{
	// Token: 0x020005A0 RID: 1440
	public class MsgChaoState : MessageBase
	{
		// Token: 0x06002B0D RID: 11021 RVA: 0x00109B24 File Offset: 0x00107D24
		public MsgChaoState(MsgChaoState.State state) : base(21760)
		{
			this.m_state = state;
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06002B0E RID: 11022 RVA: 0x00109B38 File Offset: 0x00107D38
		public MsgChaoState.State state
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x04002780 RID: 10112
		private MsgChaoState.State m_state;

		// Token: 0x020005A1 RID: 1441
		public enum State
		{
			// Token: 0x04002782 RID: 10114
			COME_IN,
			// Token: 0x04002783 RID: 10115
			STOP,
			// Token: 0x04002784 RID: 10116
			STOP_END,
			// Token: 0x04002785 RID: 10117
			LAST_CHANCE,
			// Token: 0x04002786 RID: 10118
			LAST_CHANCE_END
		}
	}
}
