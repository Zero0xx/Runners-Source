using System;
using System.Collections.Generic;

// Token: 0x020004BC RID: 1212
public class AbilityUpParamTable
{
	// Token: 0x060023ED RID: 9197 RVA: 0x000D7AEC File Offset: 0x000D5CEC
	public AbilityUpParamTable()
	{
		this.m_table = new Dictionary<AbilityType, AbilityUpParamList>();
	}

	// Token: 0x060023EE RID: 9198 RVA: 0x000D7B00 File Offset: 0x000D5D00
	public void AddList(AbilityType type, AbilityUpParamList list)
	{
		if (this.m_table == null)
		{
			return;
		}
		this.m_table.Add(type, list);
	}

	// Token: 0x060023EF RID: 9199 RVA: 0x000D7B1C File Offset: 0x000D5D1C
	public AbilityUpParamList GetList(AbilityType type)
	{
		if (this.m_table == null)
		{
			return null;
		}
		if (!this.m_table.ContainsKey(type))
		{
			return null;
		}
		return this.m_table[type];
	}

	// Token: 0x04002097 RID: 8343
	private Dictionary<AbilityType, AbilityUpParamList> m_table;
}
