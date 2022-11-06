using System;
using System.Collections.Generic;

namespace DataTable
{
	// Token: 0x02000185 RID: 389
	public class ChaoDataVisitorAttribute : ChaoDataVisitorBase, IChaoDataSorting
	{
		// Token: 0x06000B4B RID: 2891 RVA: 0x0004206C File Offset: 0x0004026C
		public ChaoDataVisitorAttribute()
		{
			this.m_chaoList = new Dictionary<CharacterAttribute, List<ChaoData>>();
			List<ChaoData> value = new List<ChaoData>();
			List<ChaoData> value2 = new List<ChaoData>();
			List<ChaoData> value3 = new List<ChaoData>();
			this.m_chaoList.Add(CharacterAttribute.SPEED, value);
			this.m_chaoList.Add(CharacterAttribute.FLY, value2);
			this.m_chaoList.Add(CharacterAttribute.POWER, value3);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000420C4 File Offset: 0x000402C4
		public override void visit(ChaoData chaoData)
		{
			if (this.m_chaoList != null)
			{
				switch (chaoData.charaAtribute)
				{
				case CharacterAttribute.SPEED:
					this.m_chaoList[CharacterAttribute.SPEED].Add(chaoData);
					break;
				case CharacterAttribute.FLY:
					this.m_chaoList[CharacterAttribute.FLY].Add(chaoData);
					break;
				case CharacterAttribute.POWER:
					this.m_chaoList[CharacterAttribute.POWER].Add(chaoData);
					break;
				}
			}
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00042140 File Offset: 0x00040340
		public List<ChaoData> GetChaoList(CharacterAttribute attribute)
		{
			List<ChaoData> result = null;
			switch (attribute)
			{
			case CharacterAttribute.SPEED:
				result = this.m_chaoList[CharacterAttribute.SPEED];
				break;
			case CharacterAttribute.FLY:
				result = this.m_chaoList[CharacterAttribute.FLY];
				break;
			case CharacterAttribute.POWER:
				result = this.m_chaoList[CharacterAttribute.POWER];
				break;
			}
			return result;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x000421A0 File Offset: 0x000403A0
		public List<ChaoData> GetChaoListAll(bool descending = false, ChaoData.Rarity exclusion = ChaoData.Rarity.NONE)
		{
			return this.GetChaoListAllOffset(0, descending, exclusion);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x000421AC File Offset: 0x000403AC
		public List<ChaoData> GetChaoListAllOffset(int offset, bool descending = false, ChaoData.Rarity exclusion = ChaoData.Rarity.NONE)
		{
			List<ChaoData> list = null;
			Dictionary<int, List<ChaoData>> dictionary = new Dictionary<int, List<ChaoData>>();
			dictionary.Add(0, this.GetChaoList(CharacterAttribute.SPEED));
			dictionary.Add(1, this.GetChaoList(CharacterAttribute.FLY));
			dictionary.Add(2, this.GetChaoList(CharacterAttribute.POWER));
			int count = dictionary.Count;
			for (int i = 0; i < count; i++)
			{
				int key = (i + offset) % count;
				if (dictionary[key] != null)
				{
					dictionary[key].Reverse();
					if (list == null)
					{
						list = dictionary[key];
					}
					else
					{
						list.AddRange(dictionary[key]);
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

		// Token: 0x040008D1 RID: 2257
		private Dictionary<CharacterAttribute, List<ChaoData>> m_chaoList;
	}
}
