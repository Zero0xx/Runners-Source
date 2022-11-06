using System;

namespace Message
{
	// Token: 0x0200065D RID: 1629
	public class MsgUIFlick : MessageBase
	{
		// Token: 0x06002BD7 RID: 11223 RVA: 0x0010ACC0 File Offset: 0x00108EC0
		public MsgUIFlick(FlickType type) : base(57344)
		{
			this.m_flick_type = type;
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06002BD8 RID: 11224 RVA: 0x0010ACD4 File Offset: 0x00108ED4
		public FlickType Flick
		{
			get
			{
				return this.m_flick_type;
			}
		}

		// Token: 0x040028B9 RID: 10425
		private FlickType m_flick_type;
	}
}
