using System;

namespace Text
{
	// Token: 0x02000A25 RID: 2597
	internal class CellData
	{
		// Token: 0x060044C4 RID: 17604 RVA: 0x001622E0 File Offset: 0x001604E0
		public CellData(string cellID, string cellString)
		{
			this.m_cellID = cellID;
			this.m_cellString = cellString;
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060044C5 RID: 17605 RVA: 0x001622F8 File Offset: 0x001604F8
		// (set) Token: 0x060044C6 RID: 17606 RVA: 0x00162300 File Offset: 0x00160500
		public string m_cellID { get; private set; }

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x060044C7 RID: 17607 RVA: 0x0016230C File Offset: 0x0016050C
		// (set) Token: 0x060044C8 RID: 17608 RVA: 0x00162314 File Offset: 0x00160514
		public string m_cellString { get; private set; }
	}
}
