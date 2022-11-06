using System;

// Token: 0x02000304 RID: 772
public class ComboChaoAbilityData
{
	// Token: 0x060016A1 RID: 5793 RVA: 0x00081B18 File Offset: 0x0007FD18
	public ComboChaoAbilityData(ChaoAbility chaoAbility, ComboChaoAbilityType type, float timeMax)
	{
		this.m_chaoAbility = chaoAbility;
		this.m_type = type;
		this.m_timeMax = timeMax;
	}

	// Token: 0x0400141D RID: 5149
	public ChaoAbility m_chaoAbility;

	// Token: 0x0400141E RID: 5150
	public ComboChaoAbilityType m_type;

	// Token: 0x0400141F RID: 5151
	public float m_timeMax;
}
