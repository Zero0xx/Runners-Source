using System;
using System.Collections.Generic;

namespace DataTable
{
	// Token: 0x02000183 RID: 387
	public class ChaoDataVisitorRarity : ChaoDataVisitorBase, IChaoDataSorting
	{
		// Token: 0x06000B41 RID: 2881 RVA: 0x00041B40 File Offset: 0x0003FD40
		public ChaoDataVisitorRarity()
		{
			this.m_chaoRarityList = new Dictionary<ChaoData.Rarity, List<ChaoData>>();
			List<ChaoData> value = new List<ChaoData>();
			List<ChaoData> value2 = new List<ChaoData>();
			List<ChaoData> value3 = new List<ChaoData>();
			this.m_chaoRarityList.Add(ChaoData.Rarity.NORMAL, value);
			this.m_chaoRarityList.Add(ChaoData.Rarity.RARE, value2);
			this.m_chaoRarityList.Add(ChaoData.Rarity.SRARE, value3);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00041B98 File Offset: 0x0003FD98
		public override void visit(ChaoData chaoData)
		{
			if (this.m_chaoRarityList != null)
			{
				switch (chaoData.rarity)
				{
				case ChaoData.Rarity.NORMAL:
					this.m_chaoRarityList[ChaoData.Rarity.NORMAL].Add(chaoData);
					break;
				case ChaoData.Rarity.RARE:
					this.m_chaoRarityList[ChaoData.Rarity.RARE].Add(chaoData);
					break;
				case ChaoData.Rarity.SRARE:
					this.m_chaoRarityList[ChaoData.Rarity.SRARE].Add(chaoData);
					break;
				}
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00041C14 File Offset: 0x0003FE14
		public List<ChaoData> GetChaoList(ChaoData.Rarity rarity, ChaoData.Rarity raritySub = ChaoData.Rarity.NONE)
		{
			List<ChaoData> list = null;
			switch (rarity)
			{
			case ChaoData.Rarity.NORMAL:
				list = this.m_chaoRarityList[ChaoData.Rarity.NORMAL];
				break;
			case ChaoData.Rarity.RARE:
				list = this.m_chaoRarityList[ChaoData.Rarity.RARE];
				break;
			case ChaoData.Rarity.SRARE:
				list = this.m_chaoRarityList[ChaoData.Rarity.SRARE];
				break;
			}
			if (raritySub != ChaoData.Rarity.NONE && rarity != raritySub && list != null)
			{
				switch (raritySub)
				{
				case ChaoData.Rarity.NORMAL:
					list.AddRange(this.m_chaoRarityList[ChaoData.Rarity.NORMAL]);
					break;
				case ChaoData.Rarity.RARE:
					list.AddRange(this.m_chaoRarityList[ChaoData.Rarity.RARE]);
					break;
				case ChaoData.Rarity.SRARE:
					list.AddRange(this.m_chaoRarityList[ChaoData.Rarity.SRARE]);
					break;
				}
			}
			return list;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00041CE8 File Offset: 0x0003FEE8
		public List<ChaoData> GetChaoListAll(bool descending = false, ChaoData.Rarity exclusion = ChaoData.Rarity.NONE)
		{
			return this.GetChaoListAllOffset(0, descending, exclusion);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00041CF4 File Offset: 0x0003FEF4
		public List<ChaoData> GetChaoListAllOffset(int offset, bool descending = false, ChaoData.Rarity exclusion = ChaoData.Rarity.NONE)
		{
			List<ChaoData> list = null;
			List<ChaoData> chaoList = this.GetChaoList(ChaoData.Rarity.NORMAL, ChaoData.Rarity.NONE);
			List<ChaoData> chaoList2 = this.GetChaoList(ChaoData.Rarity.RARE, ChaoData.Rarity.NONE);
			List<ChaoData> chaoList3 = this.GetChaoList(ChaoData.Rarity.SRARE, ChaoData.Rarity.NONE);
			if (chaoList != null && list == null)
			{
				list = chaoList;
			}
			if (chaoList2 != null)
			{
				if (list == null)
				{
					list = chaoList2;
				}
				else
				{
					list.AddRange(chaoList2);
				}
			}
			if (chaoList3 != null)
			{
				if (list == null)
				{
					list = chaoList3;
				}
				else
				{
					list.AddRange(chaoList3);
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
			return list;
		}

		// Token: 0x040008CE RID: 2254
		private Dictionary<ChaoData.Rarity, List<ChaoData>> m_chaoRarityList;
	}
}
