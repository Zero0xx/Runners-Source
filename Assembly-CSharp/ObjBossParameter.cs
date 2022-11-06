using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000845 RID: 2117
public class ObjBossParameter : MonoBehaviour
{
	// Token: 0x06003925 RID: 14629 RVA: 0x0012F15C File Offset: 0x0012D35C
	public void Setup()
	{
		this.m_speed = 0f;
		this.m_min_speed = 0f;
		this.m_player_speed = ObjUtil.GetPlayerDefaultSpeed();
		this.m_add_speed = ObjUtil.GetPlayerAddSpeed();
		this.m_add_speed_ratio = ObjUtil.GetPlayerAddSpeedRatio();
		this.m_start_pos = base.transform.position;
		this.m_hp = this.BossHPMax;
		this.OnSetup();
		this.SetupBossTable();
	}

	// Token: 0x06003926 RID: 14630 RVA: 0x0012F1CC File Offset: 0x0012D3CC
	protected virtual void OnSetup()
	{
	}

	// Token: 0x17000874 RID: 2164
	// (get) Token: 0x06003927 RID: 14631 RVA: 0x0012F1D0 File Offset: 0x0012D3D0
	public Vector3 ShotRotBase
	{
		get
		{
			return ObjBossParameter.SHOT_ROT_BASE;
		}
	}

	// Token: 0x17000875 RID: 2165
	// (get) Token: 0x06003929 RID: 14633 RVA: 0x0012F1F4 File Offset: 0x0012D3F4
	// (set) Token: 0x06003928 RID: 14632 RVA: 0x0012F1D8 File Offset: 0x0012D3D8
	public int TypeBoss
	{
		get
		{
			return this.m_bossType;
		}
		set
		{
			this.m_bossType = value;
			this.m_bossCharaType = BossTypeUtil.GetBossCharaType((BossType)this.m_bossType);
		}
	}

	// Token: 0x17000876 RID: 2166
	// (get) Token: 0x0600392A RID: 14634 RVA: 0x0012F1FC File Offset: 0x0012D3FC
	public BossCharaType CharaTypeBoss
	{
		get
		{
			return this.m_bossCharaType;
		}
	}

	// Token: 0x17000877 RID: 2167
	// (get) Token: 0x0600392C RID: 14636 RVA: 0x0012F210 File Offset: 0x0012D410
	// (set) Token: 0x0600392B RID: 14635 RVA: 0x0012F204 File Offset: 0x0012D404
	public float Speed
	{
		get
		{
			return this.m_speed;
		}
		set
		{
			this.m_speed = value;
		}
	}

	// Token: 0x17000878 RID: 2168
	// (get) Token: 0x0600392E RID: 14638 RVA: 0x0012F224 File Offset: 0x0012D424
	// (set) Token: 0x0600392D RID: 14637 RVA: 0x0012F218 File Offset: 0x0012D418
	public float MinSpeed
	{
		get
		{
			return this.m_min_speed;
		}
		set
		{
			this.m_min_speed = value;
		}
	}

	// Token: 0x17000879 RID: 2169
	// (get) Token: 0x0600392F RID: 14639 RVA: 0x0012F22C File Offset: 0x0012D42C
	public float PlayerSpeed
	{
		get
		{
			return this.m_player_speed;
		}
	}

	// Token: 0x1700087A RID: 2170
	// (get) Token: 0x06003930 RID: 14640 RVA: 0x0012F234 File Offset: 0x0012D434
	public float AddSpeed
	{
		get
		{
			return this.m_add_speed;
		}
	}

	// Token: 0x1700087B RID: 2171
	// (get) Token: 0x06003931 RID: 14641 RVA: 0x0012F23C File Offset: 0x0012D43C
	public float AddSpeedRatio
	{
		get
		{
			return this.m_add_speed_ratio;
		}
	}

	// Token: 0x1700087C RID: 2172
	// (get) Token: 0x06003932 RID: 14642 RVA: 0x0012F244 File Offset: 0x0012D444
	public float AddSpeedDistance
	{
		get
		{
			return this.AddSpeed * 0.2f;
		}
	}

	// Token: 0x1700087D RID: 2173
	// (get) Token: 0x06003933 RID: 14643 RVA: 0x0012F254 File Offset: 0x0012D454
	public Vector3 StartPos
	{
		get
		{
			return this.m_start_pos;
		}
	}

	// Token: 0x1700087E RID: 2174
	// (get) Token: 0x06003934 RID: 14644 RVA: 0x0012F25C File Offset: 0x0012D45C
	public int RingCount
	{
		get
		{
			return this.m_ring;
		}
	}

	// Token: 0x1700087F RID: 2175
	// (get) Token: 0x06003935 RID: 14645 RVA: 0x0012F264 File Offset: 0x0012D464
	public int SuperRingRatio
	{
		get
		{
			return this.m_super_ring;
		}
	}

	// Token: 0x17000880 RID: 2176
	// (get) Token: 0x06003936 RID: 14646 RVA: 0x0012F26C File Offset: 0x0012D46C
	public int RedStarRingRatio
	{
		get
		{
			return this.m_red_star_ring;
		}
	}

	// Token: 0x17000881 RID: 2177
	// (get) Token: 0x06003937 RID: 14647 RVA: 0x0012F274 File Offset: 0x0012D474
	public int BronzeTimerRatio
	{
		get
		{
			return this.m_bronze_timer;
		}
	}

	// Token: 0x17000882 RID: 2178
	// (get) Token: 0x06003938 RID: 14648 RVA: 0x0012F27C File Offset: 0x0012D47C
	public int SilverTimerRatio
	{
		get
		{
			return this.m_silver_timer;
		}
	}

	// Token: 0x17000883 RID: 2179
	// (get) Token: 0x06003939 RID: 14649 RVA: 0x0012F284 File Offset: 0x0012D484
	public int GoldTimerRatio
	{
		get
		{
			return this.m_gold_timer;
		}
	}

	// Token: 0x17000884 RID: 2180
	// (get) Token: 0x0600393B RID: 14651 RVA: 0x0012F298 File Offset: 0x0012D498
	// (set) Token: 0x0600393A RID: 14650 RVA: 0x0012F28C File Offset: 0x0012D48C
	public int BossHP
	{
		get
		{
			return this.m_hp;
		}
		set
		{
			this.m_hp = value;
		}
	}

	// Token: 0x17000885 RID: 2181
	// (get) Token: 0x0600393D RID: 14653 RVA: 0x0012F2AC File Offset: 0x0012D4AC
	// (set) Token: 0x0600393C RID: 14652 RVA: 0x0012F2A0 File Offset: 0x0012D4A0
	public int BossHPMax
	{
		get
		{
			return this.m_hp_max;
		}
		set
		{
			this.m_hp_max = value;
		}
	}

	// Token: 0x17000886 RID: 2182
	// (get) Token: 0x0600393F RID: 14655 RVA: 0x0012F2DC File Offset: 0x0012D4DC
	// (set) Token: 0x0600393E RID: 14654 RVA: 0x0012F2B4 File Offset: 0x0012D4B4
	public int BossDistance
	{
		get
		{
			return this.m_distance;
		}
		set
		{
			if (this.TypeBoss == 0)
			{
				this.m_distance = ObjUtil.GetChaoAbliltyValue(ChaoAbility.BOSS_STAGE_TIME, value);
			}
			else
			{
				this.m_distance = value;
			}
		}
	}

	// Token: 0x17000887 RID: 2183
	// (get) Token: 0x06003941 RID: 14657 RVA: 0x0012F2F0 File Offset: 0x0012D4F0
	// (set) Token: 0x06003940 RID: 14656 RVA: 0x0012F2E4 File Offset: 0x0012D4E4
	public float StepMoveY
	{
		get
		{
			return this.m_step_move_y;
		}
		set
		{
			this.m_step_move_y = value;
		}
	}

	// Token: 0x17000888 RID: 2184
	// (get) Token: 0x06003943 RID: 14659 RVA: 0x0012F304 File Offset: 0x0012D504
	// (set) Token: 0x06003942 RID: 14658 RVA: 0x0012F2F8 File Offset: 0x0012D4F8
	public int BossLevel
	{
		get
		{
			return this.m_level;
		}
		set
		{
			this.m_level = value;
		}
	}

	// Token: 0x17000889 RID: 2185
	// (get) Token: 0x06003945 RID: 14661 RVA: 0x0012F318 File Offset: 0x0012D518
	// (set) Token: 0x06003944 RID: 14660 RVA: 0x0012F30C File Offset: 0x0012D50C
	public int BossAttackPower
	{
		get
		{
			return this.m_attackPower;
		}
		set
		{
			this.m_attackPower = value;
		}
	}

	// Token: 0x1700088A RID: 2186
	// (get) Token: 0x06003947 RID: 14663 RVA: 0x0012F32C File Offset: 0x0012D52C
	// (set) Token: 0x06003946 RID: 14662 RVA: 0x0012F320 File Offset: 0x0012D520
	public float DownSpeed
	{
		get
		{
			return this.m_down_speed;
		}
		set
		{
			this.m_down_speed = value;
		}
	}

	// Token: 0x1700088B RID: 2187
	// (get) Token: 0x06003949 RID: 14665 RVA: 0x0012F340 File Offset: 0x0012D540
	// (set) Token: 0x06003948 RID: 14664 RVA: 0x0012F334 File Offset: 0x0012D534
	public float AttackInterspaceMin
	{
		get
		{
			return this.m_attackInterspaceMin;
		}
		set
		{
			this.m_attackInterspaceMin = value;
		}
	}

	// Token: 0x1700088C RID: 2188
	// (get) Token: 0x0600394B RID: 14667 RVA: 0x0012F354 File Offset: 0x0012D554
	// (set) Token: 0x0600394A RID: 14666 RVA: 0x0012F348 File Offset: 0x0012D548
	public float AttackInterspaceMax
	{
		get
		{
			return this.m_attackInterspaceMax;
		}
		set
		{
			this.m_attackInterspaceMax = value;
		}
	}

	// Token: 0x1700088D RID: 2189
	// (get) Token: 0x0600394D RID: 14669 RVA: 0x0012F368 File Offset: 0x0012D568
	// (set) Token: 0x0600394C RID: 14668 RVA: 0x0012F35C File Offset: 0x0012D55C
	public float DefaultPlayerDistance
	{
		get
		{
			return this.m_defaultPlayerDistance;
		}
		set
		{
			this.m_defaultPlayerDistance = value;
		}
	}

	// Token: 0x1700088E RID: 2190
	// (get) Token: 0x0600394F RID: 14671 RVA: 0x0012F37C File Offset: 0x0012D57C
	// (set) Token: 0x0600394E RID: 14670 RVA: 0x0012F370 File Offset: 0x0012D570
	public int TableID
	{
		get
		{
			return this.m_tbl_id;
		}
		set
		{
			this.m_tbl_id = value;
		}
	}

	// Token: 0x1700088F RID: 2191
	// (get) Token: 0x06003951 RID: 14673 RVA: 0x0012F390 File Offset: 0x0012D590
	// (set) Token: 0x06003950 RID: 14672 RVA: 0x0012F384 File Offset: 0x0012D584
	public int AttackTableID
	{
		get
		{
			return this.m_attack_tbl_id;
		}
		set
		{
			this.m_attack_tbl_id = value;
		}
	}

	// Token: 0x17000890 RID: 2192
	// (get) Token: 0x06003953 RID: 14675 RVA: 0x0012F3A4 File Offset: 0x0012D5A4
	// (set) Token: 0x06003952 RID: 14674 RVA: 0x0012F398 File Offset: 0x0012D598
	public int TrapRand
	{
		get
		{
			return this.m_trapRand;
		}
		set
		{
			this.m_trapRand = value;
		}
	}

	// Token: 0x17000891 RID: 2193
	// (get) Token: 0x06003955 RID: 14677 RVA: 0x0012F3B8 File Offset: 0x0012D5B8
	// (set) Token: 0x06003954 RID: 14676 RVA: 0x0012F3AC File Offset: 0x0012D5AC
	public float BoundParamMin
	{
		get
		{
			return this.m_boundParamMin;
		}
		set
		{
			this.m_boundParamMin = value;
		}
	}

	// Token: 0x17000892 RID: 2194
	// (get) Token: 0x06003957 RID: 14679 RVA: 0x0012F3CC File Offset: 0x0012D5CC
	// (set) Token: 0x06003956 RID: 14678 RVA: 0x0012F3C0 File Offset: 0x0012D5C0
	public float BoundParamMax
	{
		get
		{
			return this.m_boundParamMax;
		}
		set
		{
			this.m_boundParamMax = value;
		}
	}

	// Token: 0x17000893 RID: 2195
	// (get) Token: 0x06003959 RID: 14681 RVA: 0x0012F3E0 File Offset: 0x0012D5E0
	// (set) Token: 0x06003958 RID: 14680 RVA: 0x0012F3D4 File Offset: 0x0012D5D4
	public int BoundMaxRand
	{
		get
		{
			return this.m_boundMaxRand;
		}
		set
		{
			this.m_boundMaxRand = value;
		}
	}

	// Token: 0x17000894 RID: 2196
	// (get) Token: 0x0600395B RID: 14683 RVA: 0x0012F3F4 File Offset: 0x0012D5F4
	// (set) Token: 0x0600395A RID: 14682 RVA: 0x0012F3E8 File Offset: 0x0012D5E8
	public float ShotSpeed
	{
		get
		{
			return this.m_shotSpeed;
		}
		set
		{
			this.m_shotSpeed = value;
		}
	}

	// Token: 0x17000895 RID: 2197
	// (get) Token: 0x0600395D RID: 14685 RVA: 0x0012F408 File Offset: 0x0012D608
	// (set) Token: 0x0600395C RID: 14684 RVA: 0x0012F3FC File Offset: 0x0012D5FC
	public float AttackSpeed
	{
		get
		{
			return this.m_attackSpeed;
		}
		set
		{
			this.m_attackSpeed = value;
		}
	}

	// Token: 0x17000896 RID: 2198
	// (get) Token: 0x0600395F RID: 14687 RVA: 0x0012F41C File Offset: 0x0012D61C
	// (set) Token: 0x0600395E RID: 14686 RVA: 0x0012F410 File Offset: 0x0012D610
	public float AttackSpeedMin
	{
		get
		{
			return this.m_attackSpeedMin;
		}
		set
		{
			this.m_attackSpeedMin = value;
		}
	}

	// Token: 0x17000897 RID: 2199
	// (get) Token: 0x06003961 RID: 14689 RVA: 0x0012F430 File Offset: 0x0012D630
	// (set) Token: 0x06003960 RID: 14688 RVA: 0x0012F424 File Offset: 0x0012D624
	public float AttackSpeedMax
	{
		get
		{
			return this.m_attackSpeedMax;
		}
		set
		{
			this.m_attackSpeedMax = value;
		}
	}

	// Token: 0x17000898 RID: 2200
	// (get) Token: 0x06003963 RID: 14691 RVA: 0x0012F444 File Offset: 0x0012D644
	// (set) Token: 0x06003962 RID: 14690 RVA: 0x0012F438 File Offset: 0x0012D638
	public float BumperFirstSpeed
	{
		get
		{
			return this.m_bumperFirstSpeed;
		}
		set
		{
			this.m_bumperFirstSpeed = value;
		}
	}

	// Token: 0x17000899 RID: 2201
	// (get) Token: 0x06003965 RID: 14693 RVA: 0x0012F458 File Offset: 0x0012D658
	// (set) Token: 0x06003964 RID: 14692 RVA: 0x0012F44C File Offset: 0x0012D64C
	public float BumperOutOfcontrol
	{
		get
		{
			return this.m_bumperOutOfcontrol;
		}
		set
		{
			this.m_bumperOutOfcontrol = value;
		}
	}

	// Token: 0x1700089A RID: 2202
	// (get) Token: 0x06003967 RID: 14695 RVA: 0x0012F46C File Offset: 0x0012D66C
	// (set) Token: 0x06003966 RID: 14694 RVA: 0x0012F460 File Offset: 0x0012D660
	public float BumperSpeedup
	{
		get
		{
			return this.m_bumperSpeedup;
		}
		set
		{
			this.m_bumperSpeedup = value;
		}
	}

	// Token: 0x1700089B RID: 2203
	// (get) Token: 0x06003969 RID: 14697 RVA: 0x0012F480 File Offset: 0x0012D680
	// (set) Token: 0x06003968 RID: 14696 RVA: 0x0012F474 File Offset: 0x0012D674
	public float BallSpeed
	{
		get
		{
			return this.m_ballSpeed;
		}
		set
		{
			this.m_ballSpeed = value;
		}
	}

	// Token: 0x1700089C RID: 2204
	// (get) Token: 0x0600396B RID: 14699 RVA: 0x0012F494 File Offset: 0x0012D694
	// (set) Token: 0x0600396A RID: 14698 RVA: 0x0012F488 File Offset: 0x0012D688
	public int BumperRand
	{
		get
		{
			return this.m_bumperRand;
		}
		set
		{
			this.m_bumperRand = value;
		}
	}

	// Token: 0x1700089D RID: 2205
	// (get) Token: 0x0600396D RID: 14701 RVA: 0x0012F4A8 File Offset: 0x0012D6A8
	// (set) Token: 0x0600396C RID: 14700 RVA: 0x0012F49C File Offset: 0x0012D69C
	public bool AttackBallFlag
	{
		get
		{
			return this.m_attackBallFlag;
		}
		set
		{
			this.m_attackBallFlag = value;
		}
	}

	// Token: 0x1700089E RID: 2206
	// (get) Token: 0x0600396F RID: 14703 RVA: 0x0012F4BC File Offset: 0x0012D6BC
	// (set) Token: 0x0600396E RID: 14702 RVA: 0x0012F4B0 File Offset: 0x0012D6B0
	public int AttackTrapCount
	{
		get
		{
			return this.m_attackTrapCount;
		}
		set
		{
			this.m_attackTrapCount = value;
		}
	}

	// Token: 0x1700089F RID: 2207
	// (get) Token: 0x06003971 RID: 14705 RVA: 0x0012F4D0 File Offset: 0x0012D6D0
	// (set) Token: 0x06003970 RID: 14704 RVA: 0x0012F4C4 File Offset: 0x0012D6C4
	public int AttackTrapCountMax
	{
		get
		{
			return this.m_attackTrapCountMax;
		}
		set
		{
			this.m_attackTrapCountMax = value;
		}
	}

	// Token: 0x170008A0 RID: 2208
	// (get) Token: 0x06003973 RID: 14707 RVA: 0x0012F4E4 File Offset: 0x0012D6E4
	// (set) Token: 0x06003972 RID: 14706 RVA: 0x0012F4D8 File Offset: 0x0012D6D8
	public float MissileSpeed
	{
		get
		{
			return this.m_missileSpeed;
		}
		set
		{
			this.m_missileSpeed = value;
		}
	}

	// Token: 0x170008A1 RID: 2209
	// (get) Token: 0x06003975 RID: 14709 RVA: 0x0012F4F8 File Offset: 0x0012D6F8
	// (set) Token: 0x06003974 RID: 14708 RVA: 0x0012F4EC File Offset: 0x0012D6EC
	public float MissileInterspace
	{
		get
		{
			return this.m_missileInterspace;
		}
		set
		{
			this.m_missileInterspace = value;
		}
	}

	// Token: 0x170008A2 RID: 2210
	// (get) Token: 0x06003977 RID: 14711 RVA: 0x0012F50C File Offset: 0x0012D70C
	// (set) Token: 0x06003976 RID: 14710 RVA: 0x0012F500 File Offset: 0x0012D700
	public float RotSpeed
	{
		get
		{
			return this.m_rotSpeed;
		}
		set
		{
			this.m_rotSpeed = value;
		}
	}

	// Token: 0x170008A3 RID: 2211
	// (get) Token: 0x06003979 RID: 14713 RVA: 0x0012F520 File Offset: 0x0012D720
	// (set) Token: 0x06003978 RID: 14712 RVA: 0x0012F514 File Offset: 0x0012D714
	public bool AfterAttack
	{
		get
		{
			return this.m_afterAttack;
		}
		set
		{
			this.m_afterAttack = value;
		}
	}

	// Token: 0x0600397A RID: 14714 RVA: 0x0012F528 File Offset: 0x0012D728
	public Map3AttackData GetMap3AttackData()
	{
		if (this.m_map3AttackDataList != null && this.m_map3AttackDataList.Count > 0)
		{
			int num = UnityEngine.Random.Range(0, this.m_map3AttackDataList.Count);
			if (num < this.m_map3AttackDataList.Count)
			{
				return this.m_map3AttackDataList[num];
			}
		}
		return null;
	}

	// Token: 0x0600397B RID: 14715 RVA: 0x0012F584 File Offset: 0x0012D784
	public Vector3 GetMap3BomTblA(BossAttackType type)
	{
		if ((ulong)type < (ulong)((long)ObjBossParameter.BOM_TYPE_A.Length))
		{
			return ObjBossParameter.BOM_TYPE_A[(int)type];
		}
		return ObjBossParameter.BOM_TYPE_A[0];
	}

	// Token: 0x0600397C RID: 14716 RVA: 0x0012F5C4 File Offset: 0x0012D7C4
	public Vector3 GetMap3BomTblB(BossAttackType type)
	{
		if ((ulong)type < (ulong)((long)ObjBossParameter.BOM_TYPE_B.Length))
		{
			return ObjBossParameter.BOM_TYPE_B[(int)type];
		}
		return ObjBossParameter.BOM_TYPE_B[0];
	}

	// Token: 0x0600397D RID: 14717 RVA: 0x0012F604 File Offset: 0x0012D804
	public void SetupBossTable()
	{
		if (this.m_data_setup)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("GameModeStage");
		if (gameObject != null)
		{
			GameModeStage component = gameObject.GetComponent<GameModeStage>();
			if (component != null)
			{
				BossTable bossTable = component.GetBossTable();
				BossMap3Table bossMap3Table = component.GetBossMap3Table();
				if (bossTable != null && bossTable.IsSetupEnd() && bossMap3Table != null && bossMap3Table.IsSetupEnd() && this.m_map3AttackDataList == null)
				{
					this.m_super_ring = bossTable.GetItemData(this.TableID, BossTableItem.SuperRing);
					this.m_red_star_ring = bossTable.GetItemData(this.TableID, BossTableItem.RedStarRing);
					this.m_bronze_timer = bossTable.GetItemData(this.TableID, BossTableItem.BronzeWatch);
					this.m_silver_timer = bossTable.GetItemData(this.TableID, BossTableItem.SilverWatch);
					this.m_gold_timer = bossTable.GetItemData(this.TableID, BossTableItem.GoldWatch);
					this.m_map3AttackDataList = new List<Map3AttackData>();
					for (int i = 0; i < 16; i++)
					{
						Map3AttackData map3AttackData = bossMap3Table.GetMap3AttackData(this.AttackTableID, i);
						if (map3AttackData.GetAttackCount() == 0)
						{
							break;
						}
						this.m_map3AttackDataList.Add(map3AttackData);
					}
					this.m_data_setup = true;
				}
			}
		}
	}

	// Token: 0x04003001 RID: 12289
	private const float ADDSPEED_DISTANCE = 0.2f;

	// Token: 0x04003002 RID: 12290
	private static Vector3 SHOT_ROT_BASE = new Vector3(-1f, 0f, 0f);

	// Token: 0x04003003 RID: 12291
	private int m_bossType;

	// Token: 0x04003004 RID: 12292
	private BossCharaType m_bossCharaType;

	// Token: 0x04003005 RID: 12293
	private float m_speed;

	// Token: 0x04003006 RID: 12294
	private float m_min_speed;

	// Token: 0x04003007 RID: 12295
	private float m_player_speed;

	// Token: 0x04003008 RID: 12296
	private float m_add_speed;

	// Token: 0x04003009 RID: 12297
	private float m_add_speed_ratio = 1f;

	// Token: 0x0400300A RID: 12298
	private Vector3 m_start_pos = Vector3.zero;

	// Token: 0x0400300B RID: 12299
	private int m_ring = 20;

	// Token: 0x0400300C RID: 12300
	private int m_super_ring;

	// Token: 0x0400300D RID: 12301
	private int m_red_star_ring;

	// Token: 0x0400300E RID: 12302
	private int m_bronze_timer;

	// Token: 0x0400300F RID: 12303
	private int m_silver_timer;

	// Token: 0x04003010 RID: 12304
	private int m_gold_timer;

	// Token: 0x04003011 RID: 12305
	private int m_hp;

	// Token: 0x04003012 RID: 12306
	private int m_hp_max;

	// Token: 0x04003013 RID: 12307
	private int m_distance;

	// Token: 0x04003014 RID: 12308
	private float m_step_move_y;

	// Token: 0x04003015 RID: 12309
	private bool m_data_setup;

	// Token: 0x04003016 RID: 12310
	private int m_level;

	// Token: 0x04003017 RID: 12311
	private int m_attackPower = 1;

	// Token: 0x04003018 RID: 12312
	private float m_down_speed;

	// Token: 0x04003019 RID: 12313
	private float m_attackInterspaceMin;

	// Token: 0x0400301A RID: 12314
	private float m_attackInterspaceMax;

	// Token: 0x0400301B RID: 12315
	private float m_defaultPlayerDistance;

	// Token: 0x0400301C RID: 12316
	private int m_tbl_id;

	// Token: 0x0400301D RID: 12317
	private int m_attack_tbl_id;

	// Token: 0x0400301E RID: 12318
	private int m_trapRand;

	// Token: 0x0400301F RID: 12319
	private float m_boundParamMin;

	// Token: 0x04003020 RID: 12320
	private float m_boundParamMax;

	// Token: 0x04003021 RID: 12321
	private int m_boundMaxRand;

	// Token: 0x04003022 RID: 12322
	private float m_shotSpeed;

	// Token: 0x04003023 RID: 12323
	private float m_attackSpeed;

	// Token: 0x04003024 RID: 12324
	private float m_attackSpeedMin;

	// Token: 0x04003025 RID: 12325
	private float m_attackSpeedMax;

	// Token: 0x04003026 RID: 12326
	private float m_bumperFirstSpeed;

	// Token: 0x04003027 RID: 12327
	private float m_bumperOutOfcontrol;

	// Token: 0x04003028 RID: 12328
	private float m_bumperSpeedup;

	// Token: 0x04003029 RID: 12329
	private float m_ballSpeed = 8f;

	// Token: 0x0400302A RID: 12330
	private int m_bumperRand;

	// Token: 0x0400302B RID: 12331
	private bool m_attackBallFlag;

	// Token: 0x0400302C RID: 12332
	private int m_attackTrapCount;

	// Token: 0x0400302D RID: 12333
	private int m_attackTrapCountMax;

	// Token: 0x0400302E RID: 12334
	private float m_missileSpeed;

	// Token: 0x0400302F RID: 12335
	private float m_missileInterspace;

	// Token: 0x04003030 RID: 12336
	private float m_rotSpeed;

	// Token: 0x04003031 RID: 12337
	private bool m_afterAttack;

	// Token: 0x04003032 RID: 12338
	private List<Map3AttackData> m_map3AttackDataList;

	// Token: 0x04003033 RID: 12339
	private static readonly Vector3[] BOM_TYPE_A = new Vector3[]
	{
		new Vector3(0f, 0f, 0f),
		new Vector3(1f, 0f, 0f),
		new Vector3(0f, 1f, 0f),
		new Vector3(1f, 0f, 0f),
		new Vector3(1f, 0f, 0f),
		new Vector3(0f, 1f, 0f),
		new Vector3(0f, 1f, 0f),
		new Vector3(1f, 0f, 0f),
		new Vector3(1f, 0f, 0f),
		new Vector3(0f, 1f, 0f),
		new Vector3(0f, 1f, 0f)
	};

	// Token: 0x04003034 RID: 12340
	private static readonly Vector3[] BOM_TYPE_B = new Vector3[]
	{
		new Vector3(0f, 0f, 0f),
		new Vector3(0f, 0f, 0f),
		new Vector3(0f, 0f, 0f),
		new Vector3(1f, 0f, 0f),
		new Vector3(0f, 1f, 0f),
		new Vector3(0f, 1f, 0f),
		new Vector3(1f, 0f, 0f),
		new Vector3(1f, 0f, 0f),
		new Vector3(0f, 1f, 0f),
		new Vector3(0f, 1f, 0f),
		new Vector3(1f, 0f, 0f)
	};
}
