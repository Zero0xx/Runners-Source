using System;

namespace Player
{
	// Token: 0x020009A6 RID: 2470
	public class JumpParameter : StateEnteringParameter
	{
		// Token: 0x060040AC RID: 16556 RVA: 0x0014F918 File Offset: 0x0014DB18
		public override void Reset()
		{
			this.m_onAir = false;
		}

		// Token: 0x060040AD RID: 16557 RVA: 0x0014F924 File Offset: 0x0014DB24
		public void Set(bool onair)
		{
			this.m_onAir = onair;
		}

		// Token: 0x0400375B RID: 14171
		public bool m_onAir;
	}
}
