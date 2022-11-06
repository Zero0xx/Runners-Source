using System;
using System.Collections.Generic;

// Token: 0x020004BB RID: 1211
public class AbilityUpParamList
{
	// Token: 0x170004D5 RID: 1237
	// (get) Token: 0x060023E8 RID: 9192 RVA: 0x000D7A84 File Offset: 0x000D5C84
	// (set) Token: 0x060023E9 RID: 9193 RVA: 0x000D7A94 File Offset: 0x000D5C94
	public int Count
	{
		get
		{
			return this.m_paramList.Count;
		}
		private set
		{
		}
	}

	// Token: 0x060023EA RID: 9194 RVA: 0x000D7A98 File Offset: 0x000D5C98
	public int GetMaxLevel()
	{
		return this.m_paramList.Count - 1;
	}

	// Token: 0x060023EB RID: 9195 RVA: 0x000D7AA8 File Offset: 0x000D5CA8
	public AbilityUpParam GetAbilityUpParam(int level)
	{
		int maxLevel = this.GetMaxLevel();
		if (level > maxLevel)
		{
			return null;
		}
		if (level < 0)
		{
			return null;
		}
		return this.m_paramList[level];
	}

	// Token: 0x060023EC RID: 9196 RVA: 0x000D7ADC File Offset: 0x000D5CDC
	public void AddAbilityUpParam(AbilityUpParam param)
	{
		this.m_paramList.Add(param);
	}

	// Token: 0x04002096 RID: 8342
	private List<AbilityUpParam> m_paramList = new List<AbilityUpParam>();
}
