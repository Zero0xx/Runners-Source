using System;

namespace Player
{
	// Token: 0x0200099B RID: 2459
	public class ChangePhantomParameter : StateEnteringParameter
	{
		// Token: 0x06004078 RID: 16504 RVA: 0x0014E6C8 File Offset: 0x0014C8C8
		public override void Reset()
		{
			this.m_changeType = PhantomType.NONE;
			this.m_time = 0f;
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x0014E6DC File Offset: 0x0014C8DC
		public void Set(PhantomType type, float time)
		{
			this.m_changeType = type;
			this.m_time = time;
		}

		// Token: 0x0600407A RID: 16506 RVA: 0x0014E6EC File Offset: 0x0014C8EC
		public void Set(PhantomType type)
		{
			this.m_changeType = type;
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x0600407B RID: 16507 RVA: 0x0014E6F8 File Offset: 0x0014C8F8
		public PhantomType ChangeType
		{
			get
			{
				return this.m_changeType;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x0600407C RID: 16508 RVA: 0x0014E700 File Offset: 0x0014C900
		public float Timer
		{
			get
			{
				return this.m_time;
			}
		}

		// Token: 0x04003740 RID: 14144
		public PhantomType m_changeType;

		// Token: 0x04003741 RID: 14145
		public float m_time;
	}
}
