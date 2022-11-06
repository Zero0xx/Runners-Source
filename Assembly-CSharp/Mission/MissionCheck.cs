using System;
using DataTable;

namespace Mission
{
	// Token: 0x02000294 RID: 660
	public abstract class MissionCheck
	{
		// Token: 0x0600121D RID: 4637
		public abstract void ProcEvent(MissionEvent missionEvent);

		// Token: 0x0600121E RID: 4638 RVA: 0x00065D68 File Offset: 0x00063F68
		public virtual void Update(float deltaTime)
		{
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00065D6C File Offset: 0x00063F6C
		public virtual void SetInitialValue(long initialValue)
		{
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00065D70 File Offset: 0x00063F70
		public virtual long GetValue()
		{
			return 0L;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00065D74 File Offset: 0x00063F74
		public void SetData(MissionData data)
		{
			this.m_data = data;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00065D80 File Offset: 0x00063F80
		public MissionData GetData()
		{
			return this.m_data;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00065D88 File Offset: 0x00063F88
		public virtual void SetDataExtra()
		{
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00065D8C File Offset: 0x00063F8C
		public bool IsCompleted()
		{
			return this.m_isCompleted;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00065D94 File Offset: 0x00063F94
		public int GetIndex()
		{
			return this.m_data.id;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00065DA4 File Offset: 0x00063FA4
		public void SetOnCompleteFunc(MissionCompleteFunc func)
		{
			this.m_funcMissionComplete = (MissionCompleteFunc)Delegate.Combine(this.m_funcMissionComplete, func);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00065DC0 File Offset: 0x00063FC0
		public virtual void DebugComplete(int missionNo)
		{
			this.SetCompleted();
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00065DC8 File Offset: 0x00063FC8
		protected void SetCompleted()
		{
			this.m_isCompleted = true;
			if (this.m_funcMissionComplete != null)
			{
				this.m_funcMissionComplete(this.m_data);
			}
		}

		// Token: 0x0400104E RID: 4174
		protected MissionData m_data;

		// Token: 0x0400104F RID: 4175
		protected MissionCompleteFunc m_funcMissionComplete;

		// Token: 0x04001050 RID: 4176
		protected bool m_isCompleted;
	}
}
