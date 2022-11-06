using System;

// Token: 0x02000866 RID: 2150
[Serializable]
public class ObjBossEggmanMap1Parameter : SpawnableParameter
{
	// Token: 0x06003A63 RID: 14947 RVA: 0x00134330 File Offset: 0x00132530
	public ObjBossEggmanMap1Parameter() : base("ObjBossEggmanMap1")
	{
		this.m_playerDistance = 8.5f;
		this.m_shotSpeed = 15f;
		this.m_attackSpeed = 0.3f;
		this.m_LV1_hp = 3;
		this.m_LV1_distance = 500;
		this.m_LV1_tblId = 0;
		this.m_LV1_boundParamMin = 0f;
		this.m_LV1_boundParamMax = 1.5f;
		this.m_LV1_boundMaxRand = 50;
		this.m_LV1_trapRand = 50;
		this.m_LV1_attackInterspaceMin = 1f;
		this.m_LV1_attackInterspaceMax = 1f;
		this.m_LV1_attackTrapCountMax = 10;
		this.m_LV1_ballSpeed = 8f;
		this.m_LV2_hp = 5;
		this.m_LV2_distance = 700;
		this.m_LV2_tblId = 0;
		this.m_LV2_boundParamMin = 0f;
		this.m_LV2_boundParamMax = 1.5f;
		this.m_LV2_boundMaxRand = 50;
		this.m_LV2_trapRand = 50;
		this.m_LV2_attackInterspaceMin = 1f;
		this.m_LV2_attackInterspaceMax = 1f;
		this.m_LV2_attackTrapCountMax = 10;
		this.m_LV2_ballSpeed = 8f;
		this.m_LV3_hp = 7;
		this.m_LV3_distance = 1000;
		this.m_LV3_tblId = 0;
		this.m_LV3_boundParamMin = 0f;
		this.m_LV3_boundParamMax = 1.5f;
		this.m_LV3_boundMaxRand = 50;
		this.m_LV3_trapRand = 50;
		this.m_LV3_attackInterspaceMin = 1f;
		this.m_LV3_attackInterspaceMax = 1f;
		this.m_LV3_attackTrapCountMax = 10;
		this.m_LV3_ballSpeed = 8f;
		this.m_LV4_hp = 9;
		this.m_LV4_distance = 1300;
		this.m_LV4_tblId = 0;
		this.m_LV4_boundParamMin = 0f;
		this.m_LV4_boundParamMax = 1.5f;
		this.m_LV4_boundMaxRand = 50;
		this.m_LV4_trapRand = 50;
		this.m_LV4_attackInterspaceMin = 1f;
		this.m_LV4_attackInterspaceMax = 1f;
		this.m_LV4_attackTrapCountMax = 10;
		this.m_LV4_ballSpeed = 8f;
		this.m_LV5_hp = 12;
		this.m_LV5_distance = 1500;
		this.m_LV5_tblId = 0;
		this.m_LV5_boundParamMin = 0f;
		this.m_LV5_boundParamMax = 1.5f;
		this.m_LV5_boundMaxRand = 50;
		this.m_LV5_trapRand = 50;
		this.m_LV5_attackInterspaceMin = 1f;
		this.m_LV5_attackInterspaceMax = 1f;
		this.m_LV5_attackTrapCountMax = 10;
		this.m_LV5_ballSpeed = 8f;
	}

	// Token: 0x0400319F RID: 12703
	public float m_playerDistance;

	// Token: 0x040031A0 RID: 12704
	public float m_shotSpeed;

	// Token: 0x040031A1 RID: 12705
	public float m_attackSpeed;

	// Token: 0x040031A2 RID: 12706
	public int m_LV1_hp;

	// Token: 0x040031A3 RID: 12707
	public int m_LV1_distance;

	// Token: 0x040031A4 RID: 12708
	public int m_LV1_tblId;

	// Token: 0x040031A5 RID: 12709
	public float m_LV1_boundParamMin;

	// Token: 0x040031A6 RID: 12710
	public float m_LV1_boundParamMax;

	// Token: 0x040031A7 RID: 12711
	public int m_LV1_boundMaxRand;

	// Token: 0x040031A8 RID: 12712
	public int m_LV1_trapRand;

	// Token: 0x040031A9 RID: 12713
	public float m_LV1_attackInterspaceMin;

	// Token: 0x040031AA RID: 12714
	public float m_LV1_attackInterspaceMax;

	// Token: 0x040031AB RID: 12715
	public int m_LV1_attackTrapCountMax;

	// Token: 0x040031AC RID: 12716
	public float m_LV1_ballSpeed;

	// Token: 0x040031AD RID: 12717
	public int m_LV2_hp;

	// Token: 0x040031AE RID: 12718
	public int m_LV2_distance;

	// Token: 0x040031AF RID: 12719
	public int m_LV2_tblId;

	// Token: 0x040031B0 RID: 12720
	public float m_LV2_boundParamMin;

	// Token: 0x040031B1 RID: 12721
	public float m_LV2_boundParamMax;

	// Token: 0x040031B2 RID: 12722
	public int m_LV2_boundMaxRand;

	// Token: 0x040031B3 RID: 12723
	public int m_LV2_trapRand;

	// Token: 0x040031B4 RID: 12724
	public float m_LV2_attackInterspaceMin;

	// Token: 0x040031B5 RID: 12725
	public float m_LV2_attackInterspaceMax;

	// Token: 0x040031B6 RID: 12726
	public int m_LV2_attackTrapCountMax;

	// Token: 0x040031B7 RID: 12727
	public float m_LV2_ballSpeed;

	// Token: 0x040031B8 RID: 12728
	public int m_LV3_hp;

	// Token: 0x040031B9 RID: 12729
	public int m_LV3_distance;

	// Token: 0x040031BA RID: 12730
	public int m_LV3_tblId;

	// Token: 0x040031BB RID: 12731
	public float m_LV3_boundParamMin;

	// Token: 0x040031BC RID: 12732
	public float m_LV3_boundParamMax;

	// Token: 0x040031BD RID: 12733
	public int m_LV3_boundMaxRand;

	// Token: 0x040031BE RID: 12734
	public int m_LV3_trapRand;

	// Token: 0x040031BF RID: 12735
	public float m_LV3_attackInterspaceMin;

	// Token: 0x040031C0 RID: 12736
	public float m_LV3_attackInterspaceMax;

	// Token: 0x040031C1 RID: 12737
	public int m_LV3_attackTrapCountMax;

	// Token: 0x040031C2 RID: 12738
	public float m_LV3_ballSpeed;

	// Token: 0x040031C3 RID: 12739
	public int m_LV4_hp;

	// Token: 0x040031C4 RID: 12740
	public int m_LV4_distance;

	// Token: 0x040031C5 RID: 12741
	public int m_LV4_tblId;

	// Token: 0x040031C6 RID: 12742
	public float m_LV4_boundParamMin;

	// Token: 0x040031C7 RID: 12743
	public float m_LV4_boundParamMax;

	// Token: 0x040031C8 RID: 12744
	public int m_LV4_boundMaxRand;

	// Token: 0x040031C9 RID: 12745
	public int m_LV4_trapRand;

	// Token: 0x040031CA RID: 12746
	public float m_LV4_attackInterspaceMin;

	// Token: 0x040031CB RID: 12747
	public float m_LV4_attackInterspaceMax;

	// Token: 0x040031CC RID: 12748
	public int m_LV4_attackTrapCountMax;

	// Token: 0x040031CD RID: 12749
	public float m_LV4_ballSpeed;

	// Token: 0x040031CE RID: 12750
	public int m_LV5_hp;

	// Token: 0x040031CF RID: 12751
	public int m_LV5_distance;

	// Token: 0x040031D0 RID: 12752
	public int m_LV5_tblId;

	// Token: 0x040031D1 RID: 12753
	public float m_LV5_boundParamMin;

	// Token: 0x040031D2 RID: 12754
	public float m_LV5_boundParamMax;

	// Token: 0x040031D3 RID: 12755
	public int m_LV5_boundMaxRand;

	// Token: 0x040031D4 RID: 12756
	public int m_LV5_trapRand;

	// Token: 0x040031D5 RID: 12757
	public float m_LV5_attackInterspaceMin;

	// Token: 0x040031D6 RID: 12758
	public float m_LV5_attackInterspaceMax;

	// Token: 0x040031D7 RID: 12759
	public int m_LV5_attackTrapCountMax;

	// Token: 0x040031D8 RID: 12760
	public float m_LV5_ballSpeed;
}
