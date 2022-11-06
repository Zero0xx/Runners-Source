using System;
using System.Collections.Generic;

namespace DataTable
{
	// Token: 0x02000182 RID: 386
	public class ChaoDataSorting
	{
		// Token: 0x06000B3C RID: 2876 RVA: 0x000419A8 File Offset: 0x0003FBA8
		public ChaoDataSorting(ChaoSort sort)
		{
			switch (sort)
			{
			case ChaoSort.RARE:
				this.m_visitor = new ChaoDataVisitorRarity();
				break;
			case ChaoSort.LEVEL:
				this.m_visitor = new ChaoDataVisitorLevel();
				break;
			case ChaoSort.ATTRIBUTE:
				this.m_visitor = new ChaoDataVisitorAttribute();
				break;
			case ChaoSort.ABILITY:
				this.m_visitor = new ChaoDataVisitorAbility();
				break;
			case ChaoSort.EVENT:
				this.m_visitor = new ChaoDataVisitorEvent();
				break;
			}
			if (this.m_visitor != null)
			{
				this.m_chaoSorting = (IChaoDataSorting)this.m_visitor;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00041A48 File Offset: 0x0003FC48
		public ChaoDataVisitorBase visitor
		{
			get
			{
				return this.m_visitor;
			}
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00041A50 File Offset: 0x0003FC50
		public List<ChaoData> GetChaoList(bool descending = false, ChaoData.Rarity exclusion = ChaoData.Rarity.NONE)
		{
			List<ChaoData> list = null;
			if (this.m_chaoSorting != null)
			{
				List<ChaoData> chaoListAll = this.m_chaoSorting.GetChaoListAll(descending, exclusion);
				if (chaoListAll != null)
				{
					list = new List<ChaoData>();
					foreach (ChaoData chaoData in chaoListAll)
					{
						if (chaoData.level >= 0)
						{
							list.Add(chaoData);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00041AE8 File Offset: 0x0003FCE8
		public List<ChaoData> GetChaoListAll(bool descending = false, ChaoData.Rarity exclusion = ChaoData.Rarity.NONE)
		{
			List<ChaoData> result = null;
			if (this.m_chaoSorting != null)
			{
				result = this.m_chaoSorting.GetChaoListAll(descending, exclusion);
			}
			return result;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00041B14 File Offset: 0x0003FD14
		public List<ChaoData> GetChaoListAllOffset(int offset, bool descending = false, ChaoData.Rarity exclusion = ChaoData.Rarity.NONE)
		{
			List<ChaoData> result = null;
			if (this.m_chaoSorting != null)
			{
				result = this.m_chaoSorting.GetChaoListAllOffset(offset, descending, exclusion);
			}
			return result;
		}

		// Token: 0x040008CC RID: 2252
		private ChaoDataVisitorBase m_visitor;

		// Token: 0x040008CD RID: 2253
		private IChaoDataSorting m_chaoSorting;
	}
}
