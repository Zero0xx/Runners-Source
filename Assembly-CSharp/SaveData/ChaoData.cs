using System;
using System.Collections.Generic;

namespace SaveData
{
	// Token: 0x020002AB RID: 683
	public class ChaoData
	{
		// Token: 0x06001329 RID: 4905 RVA: 0x00069418 File Offset: 0x00067618
		public ChaoData()
		{
			this.m_info = new ChaoData.ChaoDataInfo[this.CHAO_MAX_NUM];
			for (int i = 0; i < this.CHAO_MAX_NUM; i++)
			{
				this.m_info[i].chao_id = -1;
				this.m_info[i].level = -1;
			}
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00069480 File Offset: 0x00067680
		public ChaoData(List<ServerChaoState> chaoStates)
		{
			int count = chaoStates.Count;
			if (count > 0)
			{
				this.m_info = new ChaoData.ChaoDataInfo[count];
				for (int i = 0; i < count; i++)
				{
					ServerChaoState serverChaoState = chaoStates[i];
					if (serverChaoState.Status > ServerChaoState.ChaoStatus.NotOwned)
					{
						ServerItem serverItem = new ServerItem((ServerItem.Id)serverChaoState.Id);
						this.m_info[i].chao_id = serverItem.chaoId;
						this.m_info[i].level = serverChaoState.Level;
					}
					else
					{
						this.m_info[i].chao_id = -1;
						this.m_info[i].level = -1;
					}
				}
				this.CHAO_MAX_NUM = count;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x00069548 File Offset: 0x00067748
		// (set) Token: 0x0600132C RID: 4908 RVA: 0x00069550 File Offset: 0x00067750
		public ChaoData.ChaoDataInfo[] Info
		{
			get
			{
				return this.m_info;
			}
			set
			{
				this.m_info = value;
			}
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0006955C File Offset: 0x0006775C
		public uint GetChaoCount()
		{
			uint num = 0U;
			foreach (ChaoData.ChaoDataInfo chaoDataInfo in this.Info)
			{
				if (chaoDataInfo.chao_id != -1)
				{
					num += 1U;
				}
			}
			return num;
		}

		// Token: 0x040010C4 RID: 4292
		public int CHAO_MAX_NUM = 2;

		// Token: 0x040010C5 RID: 4293
		private ChaoData.ChaoDataInfo[] m_info;

		// Token: 0x020002AC RID: 684
		public struct ChaoDataInfo
		{
			// Token: 0x040010C6 RID: 4294
			public int chao_id;

			// Token: 0x040010C7 RID: 4295
			public int level;
		}
	}
}
