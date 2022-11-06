using System;
using System.Collections.Generic;

namespace DataTable
{
	// Token: 0x02000181 RID: 385
	internal interface IChaoDataSorting
	{
		// Token: 0x06000B3A RID: 2874
		List<ChaoData> GetChaoListAll(bool descending = false, ChaoData.Rarity exclusion = ChaoData.Rarity.NONE);

		// Token: 0x06000B3B RID: 2875
		List<ChaoData> GetChaoListAllOffset(int offset, bool descending = false, ChaoData.Rarity exclusion = ChaoData.Rarity.NONE);
	}
}
