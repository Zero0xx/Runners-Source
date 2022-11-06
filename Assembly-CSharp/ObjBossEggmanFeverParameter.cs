using System;

// Token: 0x02000863 RID: 2147
[Serializable]
public class ObjBossEggmanFeverParameter : SpawnableParameter
{
	// Token: 0x06003A5D RID: 14941 RVA: 0x00133D48 File Offset: 0x00131F48
	public ObjBossEggmanFeverParameter() : base("ObjBossEggmanFever")
	{
		this.m_hp = 3;
		this.m_distance = 500;
		this.m_tblId = 0;
		this.m_downSpeed = 1f;
		this.m_attackInterspaceMin = 1f;
		this.m_attackInterspaceMax = 2f;
		this.m_boundParamMin = 0f;
		this.m_boundParamMax = 1.5f;
		this.m_boundMaxRand = 50;
		this.m_shotSpeed = 15f;
		this.m_bumperFirstSpeed = 10f;
		this.m_bumperOutOfcontrol = 0.3f;
		this.m_bumperSpeedup = 100f;
		this.m_ballSpeed = 8f;
	}

	// Token: 0x0400318F RID: 12687
	public int m_hp;

	// Token: 0x04003190 RID: 12688
	public int m_distance;

	// Token: 0x04003191 RID: 12689
	public int m_tblId;

	// Token: 0x04003192 RID: 12690
	public float m_downSpeed;

	// Token: 0x04003193 RID: 12691
	public float m_attackInterspaceMin;

	// Token: 0x04003194 RID: 12692
	public float m_attackInterspaceMax;

	// Token: 0x04003195 RID: 12693
	public float m_boundParamMin;

	// Token: 0x04003196 RID: 12694
	public float m_boundParamMax;

	// Token: 0x04003197 RID: 12695
	public int m_boundMaxRand;

	// Token: 0x04003198 RID: 12696
	public float m_shotSpeed;

	// Token: 0x04003199 RID: 12697
	public float m_bumperFirstSpeed;

	// Token: 0x0400319A RID: 12698
	public float m_bumperOutOfcontrol;

	// Token: 0x0400319B RID: 12699
	public float m_bumperSpeedup;

	// Token: 0x0400319C RID: 12700
	public float m_ballSpeed;
}
