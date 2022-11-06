using System;

namespace Message
{
	// Token: 0x0200059B RID: 1435
	public class MsgChangeCharaEnable : MessageBase
	{
		// Token: 0x06002B06 RID: 11014 RVA: 0x00109A58 File Offset: 0x00107C58
		public MsgChangeCharaEnable(bool value_) : base(12314)
		{
			this.value = value_;
		}

		// Token: 0x04002779 RID: 10105
		public bool value;
	}
}
