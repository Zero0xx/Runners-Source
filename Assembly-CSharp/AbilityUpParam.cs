using System;

// Token: 0x020004BA RID: 1210
public class AbilityUpParam
{
	// Token: 0x060023E2 RID: 9186 RVA: 0x000D7A28 File Offset: 0x000D5C28
	public AbilityUpParam()
	{
		this.m_abilityPotential = 0f;
		this.m_abilityCost = 0f;
	}

	// Token: 0x170004D3 RID: 1235
	// (get) Token: 0x060023E3 RID: 9187 RVA: 0x000D7A48 File Offset: 0x000D5C48
	// (set) Token: 0x060023E4 RID: 9188 RVA: 0x000D7A50 File Offset: 0x000D5C50
	public float Potential
	{
		get
		{
			return this.m_abilityPotential;
		}
		set
		{
			this.m_abilityPotential = value;
		}
	}

	// Token: 0x170004D4 RID: 1236
	// (get) Token: 0x060023E5 RID: 9189 RVA: 0x000D7A5C File Offset: 0x000D5C5C
	// (set) Token: 0x060023E6 RID: 9190 RVA: 0x000D7A64 File Offset: 0x000D5C64
	public float Cost
	{
		get
		{
			return this.m_abilityCost;
		}
		set
		{
			this.m_abilityCost = value;
		}
	}

	// Token: 0x04002094 RID: 8340
	private float m_abilityPotential;

	// Token: 0x04002095 RID: 8341
	private float m_abilityCost;
}
