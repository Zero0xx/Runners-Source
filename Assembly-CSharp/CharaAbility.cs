using System;

// Token: 0x02000254 RID: 596
public class CharaAbility
{
	// Token: 0x0600104E RID: 4174 RVA: 0x0005FAD8 File Offset: 0x0005DCD8
	public CharaAbility()
	{
		for (uint num = 0U; num < 10U; num += 1U)
		{
			this.m_ability_level[(int)((UIntPtr)num)] = 0U;
		}
	}

	// Token: 0x17000279 RID: 633
	// (get) Token: 0x0600104F RID: 4175 RVA: 0x0005FB18 File Offset: 0x0005DD18
	// (set) Token: 0x06001050 RID: 4176 RVA: 0x0005FB20 File Offset: 0x0005DD20
	public uint[] Ability
	{
		get
		{
			return this.m_ability_level;
		}
		set
		{
			this.m_ability_level = value;
		}
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x0005FB2C File Offset: 0x0005DD2C
	public uint GetTotalLevel()
	{
		uint num = 0U;
		for (uint num2 = 0U; num2 < 10U; num2 += 1U)
		{
			num += this.m_ability_level[(int)((UIntPtr)num2)];
		}
		return num;
	}

	// Token: 0x04000EAE RID: 3758
	private uint[] m_ability_level = new uint[10];
}
