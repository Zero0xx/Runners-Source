using System;

namespace Message
{
	// Token: 0x020005A3 RID: 1443
	public class MsgPauseComboTimer : MessageBase
	{
		// Token: 0x06002B11 RID: 11025 RVA: 0x00109B74 File Offset: 0x00107D74
		public MsgPauseComboTimer(MsgPauseComboTimer.State value) : base(12356)
		{
			this.m_value = value;
			this.m_time = -1f;
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x00109B94 File Offset: 0x00107D94
		public MsgPauseComboTimer(MsgPauseComboTimer.State value, float time) : base(12356)
		{
			this.m_value = value;
			this.m_time = time;
		}

		// Token: 0x04002787 RID: 10119
		public MsgPauseComboTimer.State m_value;

		// Token: 0x04002788 RID: 10120
		public float m_time;

		// Token: 0x020005A4 RID: 1444
		public enum State
		{
			// Token: 0x0400278A RID: 10122
			PAUSE,
			// Token: 0x0400278B RID: 10123
			PAUSE_TIMER,
			// Token: 0x0400278C RID: 10124
			PLAY,
			// Token: 0x0400278D RID: 10125
			PLAY_SET,
			// Token: 0x0400278E RID: 10126
			PLAY_RESET
		}
	}
}
