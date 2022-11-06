using System;

// Token: 0x02000302 RID: 770
public class ComboChaoParam
{
	// Token: 0x0600169F RID: 5791 RVA: 0x00081AB4 File Offset: 0x0007FCB4
	public ComboChaoParam(ChaoAbility chaoAbility, ComboChaoAbilityType type, float extra, int comboNum, int nextCombo)
	{
		this.m_chaoAbility = chaoAbility;
		this.m_type = type;
		this.m_extraValue = extra;
		this.m_comboNum = comboNum;
		this.m_nextCombo = nextCombo;
		this.m_continuationNum = 0;
		this.m_movement = false;
	}

	// Token: 0x04001413 RID: 5139
	public ChaoAbility m_chaoAbility;

	// Token: 0x04001414 RID: 5140
	public ComboChaoAbilityType m_type;

	// Token: 0x04001415 RID: 5141
	public float m_extraValue;

	// Token: 0x04001416 RID: 5142
	public int m_comboNum;

	// Token: 0x04001417 RID: 5143
	public int m_nextCombo;

	// Token: 0x04001418 RID: 5144
	public int m_continuationNum;

	// Token: 0x04001419 RID: 5145
	public bool m_movement;
}
