using System;
using System.Collections.Generic;

namespace DataTable
{
	// Token: 0x02000184 RID: 388
	public class ChaoDataVisitorLevel : ChaoDataVisitorBase, IChaoDataSorting
	{
		// Token: 0x06000B46 RID: 2886 RVA: 0x00041E10 File Offset: 0x00040010
		public ChaoDataVisitorLevel()
		{
			this.m_chaoList = new Dictionary<int, List<ChaoData>>();
			for (int i = 0; i <= 11; i++)
			{
				this.m_chaoList.Add(i, new List<ChaoData>());
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00041E54 File Offset: 0x00040054
		public override void visit(ChaoData chaoData)
		{
			if (this.m_chaoList != null)
			{
				if (chaoData.level >= 0 && chaoData.level <= 10)
				{
					this.m_chaoList[chaoData.level].Add(chaoData);
				}
				else
				{
					this.m_chaoList[this.m_chaoList.Count - 1].Add(chaoData);
				}
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00041EC0 File Offset: 0x000400C0
		public List<ChaoData> GetChaoList(int level)
		{
			List<ChaoData> result = null;
			if (level >= 0 && level <= 10)
			{
				result = this.m_chaoList[level];
			}
			else if (level == -1)
			{
				result = this.m_chaoList[this.m_chaoList.Count - 1];
			}
			return result;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00041F14 File Offset: 0x00040114
		public List<ChaoData> GetChaoListAll(bool descending = false, ChaoData.Rarity exclusion = ChaoData.Rarity.NONE)
		{
			return this.GetChaoListAllOffset(0, descending, exclusion);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00041F20 File Offset: 0x00040120
		public List<ChaoData> GetChaoListAllOffset(int offset, bool descending = false, ChaoData.Rarity exclusion = ChaoData.Rarity.NONE)
		{
			List<ChaoData> list = null;
			for (int i = 0; i <= 10; i++)
			{
				if (this.m_chaoList[i].Count > 0)
				{
					if (list == null)
					{
						list = this.m_chaoList[i];
					}
					else
					{
						list.AddRange(this.m_chaoList[i]);
					}
				}
			}
			if (descending && list != null)
			{
				list.Reverse();
			}
			if (exclusion != ChaoData.Rarity.NONE && list != null)
			{
				List<ChaoData> list2 = new List<ChaoData>();
				List<ChaoData> list3 = new List<ChaoData>();
				foreach (ChaoData chaoData in list)
				{
					if (chaoData.rarity != exclusion)
					{
						list2.Add(chaoData);
					}
					else
					{
						list3.Add(chaoData);
					}
				}
				list = list2;
				list.AddRange(list3);
			}
			if (list == null)
			{
				list = this.m_chaoList[this.m_chaoList.Count - 1];
			}
			else
			{
				list.AddRange(this.m_chaoList[this.m_chaoList.Count - 1]);
			}
			return list;
		}

		// Token: 0x040008CF RID: 2255
		private const int LV_MAX = 10;

		// Token: 0x040008D0 RID: 2256
		private Dictionary<int, List<ChaoData>> m_chaoList;
	}
}
