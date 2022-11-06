using System;

namespace Message
{
	// Token: 0x0200059C RID: 1436
	public class MsgChangeCharaButton : MessageBase
	{
		// Token: 0x06002B07 RID: 11015 RVA: 0x00109A6C File Offset: 0x00107C6C
		public MsgChangeCharaButton(bool value_, bool pause_) : base(12315)
		{
			this.value = value_;
			this.pause = pause_;
		}

		// Token: 0x0400277A RID: 10106
		public bool value;

		// Token: 0x0400277B RID: 10107
		public bool pause;
	}
}
