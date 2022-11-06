using System;

namespace Boss
{
	// Token: 0x02000886 RID: 2182
	public class BossStateAppearEvent2 : BossStateAppearEventBase
	{
		// Token: 0x06003AE5 RID: 15077 RVA: 0x00137BF8 File Offset: 0x00135DF8
		protected override EVENTBOSS_STATE_ID GetNextChangeState()
		{
			return EVENTBOSS_STATE_ID.AttackEvent2;
		}
	}
}
