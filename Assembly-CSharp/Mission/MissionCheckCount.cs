using System;

namespace Mission
{
	// Token: 0x02000295 RID: 661
	public class MissionCheckCount : MissionCheck
	{
		// Token: 0x06001229 RID: 4649 RVA: 0x00065DF0 File Offset: 0x00063FF0
		public MissionCheckCount(EventID id)
		{
			this.m_eventID = id;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00065E00 File Offset: 0x00064000
		public override void ProcEvent(MissionEvent missionEvent)
		{
			this.m_count = this.CheckCompletedAddCount(missionEvent, this.m_eventID, this.m_count);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00065E1C File Offset: 0x0006401C
		public override void SetInitialValue(long initialValue)
		{
			this.m_count = initialValue;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00065E28 File Offset: 0x00064028
		public override long GetValue()
		{
			return this.m_count;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00065E30 File Offset: 0x00064030
		private long CheckCompletedAddCount(MissionEvent missionEvent, EventID check_id, long in_count)
		{
			long num = in_count;
			if (!base.IsCompleted() && check_id == missionEvent.m_id)
			{
				num += missionEvent.m_num;
				if (num >= (long)this.m_data.quota)
				{
					base.SetCompleted();
				}
			}
			return num;
		}

		// Token: 0x04001051 RID: 4177
		private long m_count;

		// Token: 0x04001052 RID: 4178
		private EventID m_eventID;
	}
}
