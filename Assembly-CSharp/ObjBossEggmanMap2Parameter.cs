using System;

// Token: 0x02000869 RID: 2153
[Serializable]
public class ObjBossEggmanMap2Parameter : SpawnableParameter
{
	// Token: 0x06003A69 RID: 14953 RVA: 0x00134BC4 File Offset: 0x00132DC4
	public ObjBossEggmanMap2Parameter() : base("ObjBossEggmanMap2")
	{
		this.m_playerDistance = 8.5f;
		this.m_bumperFirstSpeed = 10f;
		this.m_bumperOutOfcontrol = 0.3f;
		this.m_shotSpeed = 15f;
		this.m_LV1_hp = 3;
		this.m_LV1_distance = 500;
		this.m_LV1_tblId = 0;
		this.m_LV1_attackInterspaceMin = 5f;
		this.m_LV1_attackInterspaceMax = 5f;
		this.m_LV1_attackSpeedMin = 2f;
		this.m_LV1_attackSpeedMax = 4f;
		this.m_LV1_missileSpeed = 6f;
		this.m_LV1_missileInterspace = 1f;
		this.m_LV1_boundParamMin = 0f;
		this.m_LV1_boundParamMax = 1.5f;
		this.m_LV1_boundMaxRand = 50;
		this.m_LV1_bumperRand = 30;
		this.m_LV1_ballSpeed = 8f;
		this.m_LV2_hp = 5;
		this.m_LV2_distance = 700;
		this.m_LV2_tblId = 0;
		this.m_LV2_attackInterspaceMin = 5f;
		this.m_LV2_attackInterspaceMax = 5f;
		this.m_LV2_attackSpeedMin = 2f;
		this.m_LV2_attackSpeedMax = 4f;
		this.m_LV2_missileSpeed = 6f;
		this.m_LV2_missileInterspace = 1f;
		this.m_LV2_boundParamMin = 0f;
		this.m_LV2_boundParamMax = 1.5f;
		this.m_LV2_boundMaxRand = 50;
		this.m_LV2_bumperRand = 30;
		this.m_LV2_ballSpeed = 8f;
		this.m_LV3_hp = 7;
		this.m_LV3_distance = 1000;
		this.m_LV3_tblId = 0;
		this.m_LV3_attackInterspaceMin = 5f;
		this.m_LV3_attackInterspaceMax = 5f;
		this.m_LV3_attackSpeedMin = 2f;
		this.m_LV3_attackSpeedMax = 4f;
		this.m_LV3_missileSpeed = 6f;
		this.m_LV3_missileInterspace = 1f;
		this.m_LV3_boundParamMin = 0f;
		this.m_LV3_boundParamMax = 1.5f;
		this.m_LV3_boundMaxRand = 50;
		this.m_LV3_bumperRand = 30;
		this.m_LV3_ballSpeed = 8f;
		this.m_LV4_hp = 9;
		this.m_LV4_distance = 1300;
		this.m_LV4_tblId = 0;
		this.m_LV4_attackInterspaceMin = 5f;
		this.m_LV4_attackInterspaceMax = 5f;
		this.m_LV4_attackSpeedMin = 2f;
		this.m_LV4_attackSpeedMax = 4f;
		this.m_LV4_missileSpeed = 6f;
		this.m_LV4_missileInterspace = 1f;
		this.m_LV4_boundParamMin = 0f;
		this.m_LV4_boundParamMax = 1.5f;
		this.m_LV4_boundMaxRand = 50;
		this.m_LV4_bumperRand = 30;
		this.m_LV4_ballSpeed = 8f;
		this.m_LV5_hp = 12;
		this.m_LV5_distance = 1500;
		this.m_LV5_tblId = 0;
		this.m_LV5_attackInterspaceMin = 5f;
		this.m_LV5_attackInterspaceMax = 5f;
		this.m_LV5_attackSpeedMin = 2f;
		this.m_LV5_attackSpeedMax = 4f;
		this.m_LV5_missileSpeed = 6f;
		this.m_LV5_missileInterspace = 1f;
		this.m_LV5_boundParamMin = 0f;
		this.m_LV5_boundParamMax = 1.5f;
		this.m_LV5_boundMaxRand = 50;
		this.m_LV5_bumperRand = 30;
		this.m_LV5_ballSpeed = 8f;
	}

	// Token: 0x040031DB RID: 12763
	public float m_playerDistance;

	// Token: 0x040031DC RID: 12764
	public float m_bumperFirstSpeed;

	// Token: 0x040031DD RID: 12765
	public float m_bumperOutOfcontrol;

	// Token: 0x040031DE RID: 12766
	public float m_shotSpeed;

	// Token: 0x040031DF RID: 12767
	public int m_LV1_hp;

	// Token: 0x040031E0 RID: 12768
	public int m_LV1_distance;

	// Token: 0x040031E1 RID: 12769
	public int m_LV1_tblId;

	// Token: 0x040031E2 RID: 12770
	public float m_LV1_attackInterspaceMin;

	// Token: 0x040031E3 RID: 12771
	public float m_LV1_attackInterspaceMax;

	// Token: 0x040031E4 RID: 12772
	public float m_LV1_attackSpeedMin;

	// Token: 0x040031E5 RID: 12773
	public float m_LV1_attackSpeedMax;

	// Token: 0x040031E6 RID: 12774
	public float m_LV1_missileSpeed;

	// Token: 0x040031E7 RID: 12775
	public float m_LV1_missileInterspace;

	// Token: 0x040031E8 RID: 12776
	public float m_LV1_boundParamMin;

	// Token: 0x040031E9 RID: 12777
	public float m_LV1_boundParamMax;

	// Token: 0x040031EA RID: 12778
	public int m_LV1_boundMaxRand;

	// Token: 0x040031EB RID: 12779
	public int m_LV1_bumperRand;

	// Token: 0x040031EC RID: 12780
	public float m_LV1_ballSpeed;

	// Token: 0x040031ED RID: 12781
	public int m_LV2_hp;

	// Token: 0x040031EE RID: 12782
	public int m_LV2_distance;

	// Token: 0x040031EF RID: 12783
	public int m_LV2_tblId;

	// Token: 0x040031F0 RID: 12784
	public float m_LV2_attackInterspaceMin;

	// Token: 0x040031F1 RID: 12785
	public float m_LV2_attackInterspaceMax;

	// Token: 0x040031F2 RID: 12786
	public float m_LV2_attackSpeedMin;

	// Token: 0x040031F3 RID: 12787
	public float m_LV2_attackSpeedMax;

	// Token: 0x040031F4 RID: 12788
	public float m_LV2_missileSpeed;

	// Token: 0x040031F5 RID: 12789
	public float m_LV2_missileInterspace;

	// Token: 0x040031F6 RID: 12790
	public float m_LV2_boundParamMin;

	// Token: 0x040031F7 RID: 12791
	public float m_LV2_boundParamMax;

	// Token: 0x040031F8 RID: 12792
	public int m_LV2_boundMaxRand;

	// Token: 0x040031F9 RID: 12793
	public int m_LV2_bumperRand;

	// Token: 0x040031FA RID: 12794
	public float m_LV2_ballSpeed;

	// Token: 0x040031FB RID: 12795
	public int m_LV3_hp;

	// Token: 0x040031FC RID: 12796
	public int m_LV3_distance;

	// Token: 0x040031FD RID: 12797
	public int m_LV3_tblId;

	// Token: 0x040031FE RID: 12798
	public float m_LV3_attackInterspaceMin;

	// Token: 0x040031FF RID: 12799
	public float m_LV3_attackInterspaceMax;

	// Token: 0x04003200 RID: 12800
	public float m_LV3_attackSpeedMin;

	// Token: 0x04003201 RID: 12801
	public float m_LV3_attackSpeedMax;

	// Token: 0x04003202 RID: 12802
	public float m_LV3_missileSpeed;

	// Token: 0x04003203 RID: 12803
	public float m_LV3_missileInterspace;

	// Token: 0x04003204 RID: 12804
	public float m_LV3_boundParamMin;

	// Token: 0x04003205 RID: 12805
	public float m_LV3_boundParamMax;

	// Token: 0x04003206 RID: 12806
	public int m_LV3_boundMaxRand;

	// Token: 0x04003207 RID: 12807
	public int m_LV3_bumperRand;

	// Token: 0x04003208 RID: 12808
	public float m_LV3_ballSpeed;

	// Token: 0x04003209 RID: 12809
	public int m_LV4_hp;

	// Token: 0x0400320A RID: 12810
	public int m_LV4_distance;

	// Token: 0x0400320B RID: 12811
	public int m_LV4_tblId;

	// Token: 0x0400320C RID: 12812
	public float m_LV4_attackInterspaceMin;

	// Token: 0x0400320D RID: 12813
	public float m_LV4_attackInterspaceMax;

	// Token: 0x0400320E RID: 12814
	public float m_LV4_attackSpeedMin;

	// Token: 0x0400320F RID: 12815
	public float m_LV4_attackSpeedMax;

	// Token: 0x04003210 RID: 12816
	public float m_LV4_missileSpeed;

	// Token: 0x04003211 RID: 12817
	public float m_LV4_missileInterspace;

	// Token: 0x04003212 RID: 12818
	public float m_LV4_boundParamMin;

	// Token: 0x04003213 RID: 12819
	public float m_LV4_boundParamMax;

	// Token: 0x04003214 RID: 12820
	public int m_LV4_boundMaxRand;

	// Token: 0x04003215 RID: 12821
	public int m_LV4_bumperRand;

	// Token: 0x04003216 RID: 12822
	public float m_LV4_ballSpeed;

	// Token: 0x04003217 RID: 12823
	public int m_LV5_hp;

	// Token: 0x04003218 RID: 12824
	public int m_LV5_distance;

	// Token: 0x04003219 RID: 12825
	public int m_LV5_tblId;

	// Token: 0x0400321A RID: 12826
	public float m_LV5_attackInterspaceMin;

	// Token: 0x0400321B RID: 12827
	public float m_LV5_attackInterspaceMax;

	// Token: 0x0400321C RID: 12828
	public float m_LV5_attackSpeedMin;

	// Token: 0x0400321D RID: 12829
	public float m_LV5_attackSpeedMax;

	// Token: 0x0400321E RID: 12830
	public float m_LV5_missileSpeed;

	// Token: 0x0400321F RID: 12831
	public float m_LV5_missileInterspace;

	// Token: 0x04003220 RID: 12832
	public float m_LV5_boundParamMin;

	// Token: 0x04003221 RID: 12833
	public float m_LV5_boundParamMax;

	// Token: 0x04003222 RID: 12834
	public int m_LV5_boundMaxRand;

	// Token: 0x04003223 RID: 12835
	public int m_LV5_bumperRand;

	// Token: 0x04003224 RID: 12836
	public float m_LV5_ballSpeed;
}
