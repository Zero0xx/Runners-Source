using System;
using System.Collections.Generic;

namespace DataTable
{
	// Token: 0x02000186 RID: 390
	public class ChaoDataVisitorAbility : ChaoDataVisitorBase, IChaoDataSorting
	{
		// Token: 0x06000B50 RID: 2896 RVA: 0x000422F8 File Offset: 0x000404F8
		public ChaoDataVisitorAbility()
		{
			this.m_chaoList = new Dictionary<ChaoAbility, List<ChaoData>>();
			int num = 94;
			for (int i = 0; i < num; i++)
			{
				this.m_chaoList.Add((ChaoAbility)i, new List<ChaoData>());
			}
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0004233C File Offset: 0x0004053C
		public override void visit(ChaoData chaoData)
		{
			if (this.m_chaoList != null && chaoData.chaoAbility >= ChaoAbility.ALL_BONUS_COUNT && chaoData.chaoAbility < ChaoAbility.NUM)
			{
				this.m_chaoList[chaoData.chaoAbility].Add(chaoData);
			}
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00042384 File Offset: 0x00040584
		public List<ChaoData> GetChaoList(ChaoAbility ability)
		{
			List<ChaoData> result = null;
			if (ability >= ChaoAbility.ALL_BONUS_COUNT && ability < ChaoAbility.NUM)
			{
				result = this.m_chaoList[ability];
			}
			return result;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000423B0 File Offset: 0x000405B0
		public List<ChaoData> GetChaoListAll(bool descending = false, ChaoData.Rarity exclusion = ChaoData.Rarity.NONE)
		{
			return this.GetChaoListAllOffset(0, descending, exclusion);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x000423BC File Offset: 0x000405BC
		public List<ChaoData> GetChaoListAllOffset(int offset, bool descending = false, ChaoData.Rarity exclusion = ChaoData.Rarity.NONE)
		{
			List<ChaoData> list = null;
			int num = 94;
			for (int i = 0; i <= num; i++)
			{
				ChaoAbility key = (ChaoAbility)((i + offset) % num);
				if (this.m_chaoList[key].Count > 0)
				{
					this.m_chaoList[key].Reverse();
					if (list == null)
					{
						list = this.m_chaoList[key];
					}
					else
					{
						list.AddRange(this.m_chaoList[key]);
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
			return list;
		}

		// Token: 0x040008D2 RID: 2258
		private Dictionary<ChaoAbility, List<ChaoData>> m_chaoList;
	}
}
