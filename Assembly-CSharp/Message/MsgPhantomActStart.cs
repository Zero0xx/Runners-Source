using System;

namespace Message
{
	// Token: 0x02000645 RID: 1605
	public class MsgPhantomActStart : MessageBase
	{
		// Token: 0x06002BBF RID: 11199 RVA: 0x0010AA8C File Offset: 0x00108C8C
		public MsgPhantomActStart(PhantomType type) : base(12350)
		{
			this.m_type = type;
		}

		// Token: 0x040028A0 RID: 10400
		public readonly PhantomType m_type;
	}
}
