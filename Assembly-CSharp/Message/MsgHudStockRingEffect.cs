using System;

namespace Message
{
	// Token: 0x020005B8 RID: 1464
	public class MsgHudStockRingEffect : MessageBase
	{
		// Token: 0x06002B27 RID: 11047 RVA: 0x00109D90 File Offset: 0x00107F90
		public MsgHudStockRingEffect(bool off) : base(49157)
		{
			this.m_off = off;
		}

		// Token: 0x040027AE RID: 10158
		public bool m_off;
	}
}
