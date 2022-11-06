using System;

namespace Message
{
	// Token: 0x02000643 RID: 1603
	public class MsgTransformPhantom : MessageBase
	{
		// Token: 0x06002BBD RID: 11197 RVA: 0x0010AA68 File Offset: 0x00108C68
		public MsgTransformPhantom(PhantomType type) : base(12304)
		{
			this.m_type = type;
		}

		// Token: 0x0400289F RID: 10399
		public readonly PhantomType m_type;
	}
}
