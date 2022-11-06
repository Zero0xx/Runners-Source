using System;

// Token: 0x02000850 RID: 2128
[Serializable]
public class ObjBossZazz1Parameter : SpawnableParameter
{
	// Token: 0x06003A1F RID: 14879 RVA: 0x001322BC File Offset: 0x001304BC
	public ObjBossZazz1Parameter()
	{
		this.m_playerDistance = 8.5f;
		this.m_bumperFirstSpeed = 10f;
		this.m_bumperOutOfcontrol = 0.3f;
		this.m_bumperSpeedup = 200f;
		this.m_shotSpeed = 15f;
		this.m_LV1_distance = 500;
		this.m_LV1_tblId = 0;
		this.m_LV1_missileSpeed = 6f;
		this.m_LV1_missileInterspace = 1f;
		this.m_LV1_missilePos1 = 40;
		this.m_LV1_missilePos2 = 40;
		this.m_LV1_missileWaitTime = 2f;
		this.m_LV1_missileCount = 5;
		this.m_LV1_boundParamMin = 0f;
		this.m_LV1_boundParamMax = 1.5f;
		this.m_LV1_boundMaxRand = 50;
		this.m_LV1_bumperRand = 30;
		this.m_LV1_ballSpeed = 8f;
		this.m_LV1_wispInterspace = 0.8f;
		this.m_LV1_bumperInterspace = 1.3f;
		this.m_LV1_wispSpeedMin = 0.3f;
		this.m_LV1_wispSpeedMax = 0.7f;
		this.m_LV1_wispSwingMin = 0.3f;
		this.m_LV1_wispSwingMax = 0.5f;
		this.m_LV1_wispAddXMin = -3f;
		this.m_LV1_wispAddXMax = -1f;
		this.m_LV2_distance = 700;
		this.m_LV2_tblId = 0;
		this.m_LV2_missileSpeed = 6f;
		this.m_LV2_missileInterspace = 1f;
		this.m_LV2_missilePos1 = 40;
		this.m_LV2_missilePos2 = 40;
		this.m_LV2_missileWaitTime = 2f;
		this.m_LV2_missileCount = 5;
		this.m_LV2_boundParamMin = 0f;
		this.m_LV2_boundParamMax = 1.5f;
		this.m_LV2_boundMaxRand = 50;
		this.m_LV2_bumperRand = 30;
		this.m_LV2_ballSpeed = 8f;
		this.m_LV2_wispInterspace = 0.8f;
		this.m_LV2_bumperInterspace = 1.3f;
		this.m_LV2_wispSpeedMin = 0.3f;
		this.m_LV2_wispSpeedMax = 0.7f;
		this.m_LV2_wispSwingMin = 0.3f;
		this.m_LV2_wispSwingMax = 0.5f;
		this.m_LV2_wispAddXMin = -3f;
		this.m_LV2_wispAddXMax = -1f;
		this.m_LV3_distance = 1000;
		this.m_LV3_tblId = 0;
		this.m_LV3_missileSpeed = 6f;
		this.m_LV3_missileInterspace = 1f;
		this.m_LV3_missilePos1 = 40;
		this.m_LV3_missilePos2 = 40;
		this.m_LV3_missileWaitTime = 2f;
		this.m_LV3_missileCount = 5;
		this.m_LV3_boundParamMin = 0f;
		this.m_LV3_boundParamMax = 1.5f;
		this.m_LV3_boundMaxRand = 50;
		this.m_LV3_bumperRand = 30;
		this.m_LV3_ballSpeed = 8f;
		this.m_LV3_wispInterspace = 0.8f;
		this.m_LV3_bumperInterspace = 1.3f;
		this.m_LV3_wispSpeedMin = 0.3f;
		this.m_LV3_wispSpeedMax = 0.7f;
		this.m_LV3_wispSwingMin = 0.3f;
		this.m_LV3_wispSwingMax = 0.5f;
		this.m_LV3_wispAddXMin = -3f;
		this.m_LV3_wispAddXMax = -1f;
		this.m_LV4_distance = 1300;
		this.m_LV4_tblId = 0;
		this.m_LV4_missileSpeed = 6f;
		this.m_LV4_missileInterspace = 1f;
		this.m_LV4_missilePos1 = 40;
		this.m_LV4_missilePos2 = 40;
		this.m_LV4_missileWaitTime = 2f;
		this.m_LV4_missileCount = 5;
		this.m_LV4_boundParamMin = 0f;
		this.m_LV4_boundParamMax = 1.5f;
		this.m_LV4_boundMaxRand = 50;
		this.m_LV4_bumperRand = 30;
		this.m_LV4_ballSpeed = 8f;
		this.m_LV4_wispInterspace = 0.8f;
		this.m_LV4_bumperInterspace = 1.3f;
		this.m_LV4_wispSpeedMin = 0.3f;
		this.m_LV4_wispSpeedMax = 0.7f;
		this.m_LV4_wispSwingMin = 0.3f;
		this.m_LV4_wispSwingMax = 0.5f;
		this.m_LV4_wispAddXMin = -3f;
		this.m_LV4_wispAddXMax = -1f;
		this.m_LV5_distance = 1500;
		this.m_LV5_tblId = 0;
		this.m_LV5_missileSpeed = 6f;
		this.m_LV5_missileInterspace = 1f;
		this.m_LV5_missilePos1 = 40;
		this.m_LV5_missilePos2 = 40;
		this.m_LV5_missileWaitTime = 2f;
		this.m_LV5_missileCount = 5;
		this.m_LV5_boundParamMin = 0f;
		this.m_LV5_boundParamMax = 1.5f;
		this.m_LV5_boundMaxRand = 50;
		this.m_LV5_bumperRand = 30;
		this.m_LV5_ballSpeed = 8f;
		this.m_LV5_wispInterspace = 0.8f;
		this.m_LV5_bumperInterspace = 1.3f;
		this.m_LV5_wispSpeedMin = 0.3f;
		this.m_LV5_wispSpeedMax = 0.7f;
		this.m_LV5_wispSwingMin = 0.3f;
		this.m_LV5_wispSwingMax = 0.5f;
		this.m_LV5_wispAddXMin = -3f;
		this.m_LV5_wispAddXMax = -1f;
	}

	// Token: 0x04003089 RID: 12425
	public float m_playerDistance;

	// Token: 0x0400308A RID: 12426
	public float m_bumperFirstSpeed;

	// Token: 0x0400308B RID: 12427
	public float m_bumperOutOfcontrol;

	// Token: 0x0400308C RID: 12428
	public float m_bumperSpeedup;

	// Token: 0x0400308D RID: 12429
	public float m_shotSpeed;

	// Token: 0x0400308E RID: 12430
	public int m_LV1_distance;

	// Token: 0x0400308F RID: 12431
	public int m_LV1_tblId;

	// Token: 0x04003090 RID: 12432
	public float m_LV1_missileSpeed;

	// Token: 0x04003091 RID: 12433
	public float m_LV1_missileInterspace;

	// Token: 0x04003092 RID: 12434
	public int m_LV1_missilePos1;

	// Token: 0x04003093 RID: 12435
	public int m_LV1_missilePos2;

	// Token: 0x04003094 RID: 12436
	public float m_LV1_missileWaitTime;

	// Token: 0x04003095 RID: 12437
	public int m_LV1_missileCount;

	// Token: 0x04003096 RID: 12438
	public float m_LV1_boundParamMin;

	// Token: 0x04003097 RID: 12439
	public float m_LV1_boundParamMax;

	// Token: 0x04003098 RID: 12440
	public int m_LV1_boundMaxRand;

	// Token: 0x04003099 RID: 12441
	public int m_LV1_bumperRand;

	// Token: 0x0400309A RID: 12442
	public float m_LV1_ballSpeed;

	// Token: 0x0400309B RID: 12443
	public float m_LV1_wispInterspace;

	// Token: 0x0400309C RID: 12444
	public float m_LV1_bumperInterspace;

	// Token: 0x0400309D RID: 12445
	public float m_LV1_wispSpeedMin;

	// Token: 0x0400309E RID: 12446
	public float m_LV1_wispSpeedMax;

	// Token: 0x0400309F RID: 12447
	public float m_LV1_wispSwingMin;

	// Token: 0x040030A0 RID: 12448
	public float m_LV1_wispSwingMax;

	// Token: 0x040030A1 RID: 12449
	public float m_LV1_wispAddXMin;

	// Token: 0x040030A2 RID: 12450
	public float m_LV1_wispAddXMax;

	// Token: 0x040030A3 RID: 12451
	public int m_LV2_distance;

	// Token: 0x040030A4 RID: 12452
	public int m_LV2_tblId;

	// Token: 0x040030A5 RID: 12453
	public float m_LV2_missileSpeed;

	// Token: 0x040030A6 RID: 12454
	public float m_LV2_missileInterspace;

	// Token: 0x040030A7 RID: 12455
	public int m_LV2_missilePos1;

	// Token: 0x040030A8 RID: 12456
	public int m_LV2_missilePos2;

	// Token: 0x040030A9 RID: 12457
	public float m_LV2_missileWaitTime;

	// Token: 0x040030AA RID: 12458
	public int m_LV2_missileCount;

	// Token: 0x040030AB RID: 12459
	public float m_LV2_boundParamMin;

	// Token: 0x040030AC RID: 12460
	public float m_LV2_boundParamMax;

	// Token: 0x040030AD RID: 12461
	public int m_LV2_boundMaxRand;

	// Token: 0x040030AE RID: 12462
	public int m_LV2_bumperRand;

	// Token: 0x040030AF RID: 12463
	public float m_LV2_ballSpeed;

	// Token: 0x040030B0 RID: 12464
	public float m_LV2_wispInterspace;

	// Token: 0x040030B1 RID: 12465
	public float m_LV2_bumperInterspace;

	// Token: 0x040030B2 RID: 12466
	public float m_LV2_wispSpeedMin;

	// Token: 0x040030B3 RID: 12467
	public float m_LV2_wispSpeedMax;

	// Token: 0x040030B4 RID: 12468
	public float m_LV2_wispSwingMin;

	// Token: 0x040030B5 RID: 12469
	public float m_LV2_wispSwingMax;

	// Token: 0x040030B6 RID: 12470
	public float m_LV2_wispAddXMin;

	// Token: 0x040030B7 RID: 12471
	public float m_LV2_wispAddXMax;

	// Token: 0x040030B8 RID: 12472
	public int m_LV3_distance;

	// Token: 0x040030B9 RID: 12473
	public int m_LV3_tblId;

	// Token: 0x040030BA RID: 12474
	public float m_LV3_missileSpeed;

	// Token: 0x040030BB RID: 12475
	public float m_LV3_missileInterspace;

	// Token: 0x040030BC RID: 12476
	public int m_LV3_missilePos1;

	// Token: 0x040030BD RID: 12477
	public int m_LV3_missilePos2;

	// Token: 0x040030BE RID: 12478
	public float m_LV3_missileWaitTime;

	// Token: 0x040030BF RID: 12479
	public int m_LV3_missileCount;

	// Token: 0x040030C0 RID: 12480
	public float m_LV3_boundParamMin;

	// Token: 0x040030C1 RID: 12481
	public float m_LV3_boundParamMax;

	// Token: 0x040030C2 RID: 12482
	public int m_LV3_boundMaxRand;

	// Token: 0x040030C3 RID: 12483
	public int m_LV3_bumperRand;

	// Token: 0x040030C4 RID: 12484
	public float m_LV3_ballSpeed;

	// Token: 0x040030C5 RID: 12485
	public float m_LV3_wispInterspace;

	// Token: 0x040030C6 RID: 12486
	public float m_LV3_bumperInterspace;

	// Token: 0x040030C7 RID: 12487
	public float m_LV3_wispSpeedMin;

	// Token: 0x040030C8 RID: 12488
	public float m_LV3_wispSpeedMax;

	// Token: 0x040030C9 RID: 12489
	public float m_LV3_wispSwingMin;

	// Token: 0x040030CA RID: 12490
	public float m_LV3_wispSwingMax;

	// Token: 0x040030CB RID: 12491
	public float m_LV3_wispAddXMin;

	// Token: 0x040030CC RID: 12492
	public float m_LV3_wispAddXMax;

	// Token: 0x040030CD RID: 12493
	public int m_LV4_distance;

	// Token: 0x040030CE RID: 12494
	public int m_LV4_tblId;

	// Token: 0x040030CF RID: 12495
	public float m_LV4_missileSpeed;

	// Token: 0x040030D0 RID: 12496
	public float m_LV4_missileInterspace;

	// Token: 0x040030D1 RID: 12497
	public int m_LV4_missilePos1;

	// Token: 0x040030D2 RID: 12498
	public int m_LV4_missilePos2;

	// Token: 0x040030D3 RID: 12499
	public float m_LV4_missileWaitTime;

	// Token: 0x040030D4 RID: 12500
	public int m_LV4_missileCount;

	// Token: 0x040030D5 RID: 12501
	public float m_LV4_boundParamMin;

	// Token: 0x040030D6 RID: 12502
	public float m_LV4_boundParamMax;

	// Token: 0x040030D7 RID: 12503
	public int m_LV4_boundMaxRand;

	// Token: 0x040030D8 RID: 12504
	public int m_LV4_bumperRand;

	// Token: 0x040030D9 RID: 12505
	public float m_LV4_ballSpeed;

	// Token: 0x040030DA RID: 12506
	public float m_LV4_wispInterspace;

	// Token: 0x040030DB RID: 12507
	public float m_LV4_bumperInterspace;

	// Token: 0x040030DC RID: 12508
	public float m_LV4_wispSpeedMin;

	// Token: 0x040030DD RID: 12509
	public float m_LV4_wispSpeedMax;

	// Token: 0x040030DE RID: 12510
	public float m_LV4_wispSwingMin;

	// Token: 0x040030DF RID: 12511
	public float m_LV4_wispSwingMax;

	// Token: 0x040030E0 RID: 12512
	public float m_LV4_wispAddXMin;

	// Token: 0x040030E1 RID: 12513
	public float m_LV4_wispAddXMax;

	// Token: 0x040030E2 RID: 12514
	public int m_LV5_distance;

	// Token: 0x040030E3 RID: 12515
	public int m_LV5_tblId;

	// Token: 0x040030E4 RID: 12516
	public float m_LV5_missileSpeed;

	// Token: 0x040030E5 RID: 12517
	public float m_LV5_missileInterspace;

	// Token: 0x040030E6 RID: 12518
	public int m_LV5_missilePos1;

	// Token: 0x040030E7 RID: 12519
	public int m_LV5_missilePos2;

	// Token: 0x040030E8 RID: 12520
	public float m_LV5_missileWaitTime;

	// Token: 0x040030E9 RID: 12521
	public int m_LV5_missileCount;

	// Token: 0x040030EA RID: 12522
	public float m_LV5_boundParamMin;

	// Token: 0x040030EB RID: 12523
	public float m_LV5_boundParamMax;

	// Token: 0x040030EC RID: 12524
	public int m_LV5_boundMaxRand;

	// Token: 0x040030ED RID: 12525
	public int m_LV5_bumperRand;

	// Token: 0x040030EE RID: 12526
	public float m_LV5_ballSpeed;

	// Token: 0x040030EF RID: 12527
	public float m_LV5_wispInterspace;

	// Token: 0x040030F0 RID: 12528
	public float m_LV5_bumperInterspace;

	// Token: 0x040030F1 RID: 12529
	public float m_LV5_wispSpeedMin;

	// Token: 0x040030F2 RID: 12530
	public float m_LV5_wispSpeedMax;

	// Token: 0x040030F3 RID: 12531
	public float m_LV5_wispSwingMin;

	// Token: 0x040030F4 RID: 12532
	public float m_LV5_wispSwingMax;

	// Token: 0x040030F5 RID: 12533
	public float m_LV5_wispAddXMin;

	// Token: 0x040030F6 RID: 12534
	public float m_LV5_wispAddXMax;
}
