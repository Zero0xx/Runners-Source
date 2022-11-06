using System;
using System.Collections.Generic;

namespace Text
{
	// Token: 0x02000A26 RID: 2598
	internal class CellDataList
	{
		// Token: 0x060044C9 RID: 17609 RVA: 0x00162320 File Offset: 0x00160520
		public CellDataList(string categoryName)
		{
			this.m_categoryName = categoryName;
			this.m_cellDataList = new Dictionary<string, CellData>();
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060044CA RID: 17610 RVA: 0x0016233C File Offset: 0x0016053C
		// (set) Token: 0x060044CB RID: 17611 RVA: 0x00162344 File Offset: 0x00160544
		public string m_categoryName { get; private set; }

		// Token: 0x060044CC RID: 17612 RVA: 0x00162350 File Offset: 0x00160550
		public void Add(CellData cellData)
		{
			if (cellData == null)
			{
				return;
			}
			if (this.m_cellDataList.ContainsKey(cellData.m_cellID))
			{
				Debug.LogWarning("CellDataList.Add() same cellID = " + cellData.m_cellID);
				return;
			}
			this.m_cellDataList.Add(cellData.m_cellID, cellData);
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x001623A4 File Offset: 0x001605A4
		public CellData Get(string searchId)
		{
			if (!this.m_cellDataList.ContainsKey(searchId))
			{
				return null;
			}
			return this.m_cellDataList[searchId];
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x001623C8 File Offset: 0x001605C8
		public int GetCount()
		{
			return this.m_cellDataList.Count;
		}

		// Token: 0x040039BB RID: 14779
		private Dictionary<string, CellData> m_cellDataList;
	}
}
