using System;

namespace Boss
{
	// Token: 0x02000884 RID: 2180
	public class BossStateAppearEvent1 : BossStateAppearEventBase
	{
		// Token: 0x06003AE1 RID: 15073 RVA: 0x00137BE0 File Offset: 0x00135DE0
		protected override EVENTBOSS_STATE_ID GetNextChangeState()
		{
			return EVENTBOSS_STATE_ID.AttackEvent1;
		}
	}
}
