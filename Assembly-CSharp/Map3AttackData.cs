using System;

// Token: 0x020002E7 RID: 743
public class Map3AttackData
{
	// Token: 0x0600156A RID: 5482 RVA: 0x000767B4 File Offset: 0x000749B4
	public Map3AttackData()
	{
		this.m_type = BossAttackType.NONE;
		this.m_randA = 0;
		this.m_posA = BossAttackPos.NONE;
		this.m_randB = 0;
		this.m_posB = BossAttackPos.NONE;
	}

	// Token: 0x0600156B RID: 5483 RVA: 0x000767E0 File Offset: 0x000749E0
	public Map3AttackData(BossAttackType type, int randA, BossAttackPos posA, int randB, BossAttackPos posB)
	{
		this.m_type = type;
		this.m_randA = randA;
		this.m_posA = posA;
		this.m_randB = randB;
		this.m_posB = posB;
	}

	// Token: 0x0600156C RID: 5484 RVA: 0x00076810 File Offset: 0x00074A10
	public int GetAttackCount()
	{
		return BossMap3Table.GetBossAttackCount(this.m_type);
	}

	// Token: 0x040012F0 RID: 4848
	public BossAttackType m_type;

	// Token: 0x040012F1 RID: 4849
	public int m_randA;

	// Token: 0x040012F2 RID: 4850
	public BossAttackPos m_posA;

	// Token: 0x040012F3 RID: 4851
	public int m_randB;

	// Token: 0x040012F4 RID: 4852
	public BossAttackPos m_posB;
}
