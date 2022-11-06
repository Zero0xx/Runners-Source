using System;

namespace SaveData
{
	// Token: 0x020002AA RID: 682
	public class CharaData
	{
		// Token: 0x06001324 RID: 4900 RVA: 0x00069384 File Offset: 0x00067584
		public CharaData()
		{
			for (int i = 0; i < 29; i++)
			{
				if (i == 0)
				{
					this.m_status[i] = 1;
				}
				else
				{
					this.m_status[i] = 0;
				}
				this.m_ability_array[i] = new CharaAbility();
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x000693F0 File Offset: 0x000675F0
		// (set) Token: 0x06001326 RID: 4902 RVA: 0x000693F8 File Offset: 0x000675F8
		public CharaAbility[] AbilityArray
		{
			get
			{
				return this.m_ability_array;
			}
			set
			{
				this.m_ability_array = value;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x00069404 File Offset: 0x00067604
		// (set) Token: 0x06001328 RID: 4904 RVA: 0x0006940C File Offset: 0x0006760C
		public int[] Status
		{
			get
			{
				return this.m_status;
			}
			set
			{
				this.m_status = value;
			}
		}

		// Token: 0x040010C2 RID: 4290
		private CharaAbility[] m_ability_array = new CharaAbility[29];

		// Token: 0x040010C3 RID: 4291
		private int[] m_status = new int[29];
	}
}
