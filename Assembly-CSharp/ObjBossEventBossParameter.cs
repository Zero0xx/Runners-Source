using System;

// Token: 0x0200084C RID: 2124
public class ObjBossEventBossParameter : ObjBossParameter
{
	// Token: 0x060039D7 RID: 14807 RVA: 0x00131044 File Offset: 0x0012F244
	protected override void OnSetup()
	{
		int num = 0;
		LevelInformation levelInformation = ObjUtil.GetLevelInformation();
		if (levelInformation != null)
		{
			levelInformation.NumBossHpMax = base.BossHPMax;
			num = base.BossHPMax - levelInformation.NumBossAttack;
		}
		if (num < 1)
		{
			num = 1;
		}
		base.BossHP = num;
		if (EventManager.Instance != null)
		{
			int useRaidbossChallengeCount = EventManager.Instance.UseRaidbossChallengeCount;
			this.m_challengeValue = EventManager.Instance.GetRaidAttackRate(useRaidbossChallengeCount);
		}
		else
		{
			this.m_challengeValue = 1f;
		}
	}

	// Token: 0x170008A6 RID: 2214
	// (get) Token: 0x060039D8 RID: 14808 RVA: 0x001310CC File Offset: 0x0012F2CC
	public float ChallengeValue
	{
		get
		{
			return this.m_challengeValue;
		}
	}

	// Token: 0x170008A7 RID: 2215
	// (get) Token: 0x060039DA RID: 14810 RVA: 0x001310E0 File Offset: 0x0012F2E0
	// (set) Token: 0x060039D9 RID: 14809 RVA: 0x001310D4 File Offset: 0x0012F2D4
	public float BoostRatio
	{
		get
		{
			return this.m_wispBoostRatio;
		}
		set
		{
			this.m_wispBoostRatio = value;
		}
	}

	// Token: 0x170008A8 RID: 2216
	// (get) Token: 0x060039DB RID: 14811 RVA: 0x001310E8 File Offset: 0x0012F2E8
	public float BoostRatioDown
	{
		get
		{
			return EventBossParamTable.GetItemData(EventBossParamTableItem.WispRatioDown);
		}
	}

	// Token: 0x170008A9 RID: 2217
	// (get) Token: 0x060039DC RID: 14812 RVA: 0x001310F0 File Offset: 0x0012F2F0
	public float BoostRatioAdd
	{
		get
		{
			return EventBossParamTable.GetItemData(EventBossParamTableItem.WispRatio);
		}
	}

	// Token: 0x170008AA RID: 2218
	// (get) Token: 0x060039DE RID: 14814 RVA: 0x00131104 File Offset: 0x0012F304
	// (set) Token: 0x060039DD RID: 14813 RVA: 0x001310F8 File Offset: 0x0012F2F8
	public WispBoostLevel BoostLevel
	{
		get
		{
			return this.m_wispBoostLevel;
		}
		set
		{
			this.m_wispBoostLevel = value;
		}
	}

	// Token: 0x170008AB RID: 2219
	// (get) Token: 0x060039E0 RID: 14816 RVA: 0x00131118 File Offset: 0x0012F318
	// (set) Token: 0x060039DF RID: 14815 RVA: 0x0013110C File Offset: 0x0012F30C
	public int MissilePos1
	{
		get
		{
			return this.m_missilePos1;
		}
		set
		{
			this.m_missilePos1 = value;
		}
	}

	// Token: 0x170008AC RID: 2220
	// (get) Token: 0x060039E2 RID: 14818 RVA: 0x0013112C File Offset: 0x0012F32C
	// (set) Token: 0x060039E1 RID: 14817 RVA: 0x00131120 File Offset: 0x0012F320
	public int MissilePos2
	{
		get
		{
			return this.m_missilePos2;
		}
		set
		{
			this.m_missilePos2 = value;
		}
	}

	// Token: 0x170008AD RID: 2221
	// (get) Token: 0x060039E4 RID: 14820 RVA: 0x00131140 File Offset: 0x0012F340
	// (set) Token: 0x060039E3 RID: 14819 RVA: 0x00131134 File Offset: 0x0012F334
	public float WispInterspace
	{
		get
		{
			return this.m_wispInterspace;
		}
		set
		{
			this.m_wispInterspace = value;
		}
	}

	// Token: 0x170008AE RID: 2222
	// (get) Token: 0x060039E6 RID: 14822 RVA: 0x00131154 File Offset: 0x0012F354
	// (set) Token: 0x060039E5 RID: 14821 RVA: 0x00131148 File Offset: 0x0012F348
	public float BumperInterspace
	{
		get
		{
			return this.m_bumperInterspace;
		}
		set
		{
			this.m_bumperInterspace = value;
		}
	}

	// Token: 0x170008AF RID: 2223
	// (get) Token: 0x060039E8 RID: 14824 RVA: 0x00131168 File Offset: 0x0012F368
	// (set) Token: 0x060039E7 RID: 14823 RVA: 0x0013115C File Offset: 0x0012F35C
	public float WispSpeedMin
	{
		get
		{
			return this.m_wispSpeedMin;
		}
		set
		{
			this.m_wispSpeedMin = value;
		}
	}

	// Token: 0x170008B0 RID: 2224
	// (get) Token: 0x060039EA RID: 14826 RVA: 0x0013117C File Offset: 0x0012F37C
	// (set) Token: 0x060039E9 RID: 14825 RVA: 0x00131170 File Offset: 0x0012F370
	public float WispSpeedMax
	{
		get
		{
			return this.m_wispSpeedMax;
		}
		set
		{
			this.m_wispSpeedMax = value;
		}
	}

	// Token: 0x170008B1 RID: 2225
	// (get) Token: 0x060039EC RID: 14828 RVA: 0x00131190 File Offset: 0x0012F390
	// (set) Token: 0x060039EB RID: 14827 RVA: 0x00131184 File Offset: 0x0012F384
	public float WispSwingMin
	{
		get
		{
			return this.m_wispSwingMin;
		}
		set
		{
			this.m_wispSwingMin = value;
		}
	}

	// Token: 0x170008B2 RID: 2226
	// (get) Token: 0x060039EE RID: 14830 RVA: 0x001311A4 File Offset: 0x0012F3A4
	// (set) Token: 0x060039ED RID: 14829 RVA: 0x00131198 File Offset: 0x0012F398
	public float WispSwingMax
	{
		get
		{
			return this.m_wispSwingMax;
		}
		set
		{
			this.m_wispSwingMax = value;
		}
	}

	// Token: 0x170008B3 RID: 2227
	// (get) Token: 0x060039F0 RID: 14832 RVA: 0x001311B8 File Offset: 0x0012F3B8
	// (set) Token: 0x060039EF RID: 14831 RVA: 0x001311AC File Offset: 0x0012F3AC
	public float WispAddXMin
	{
		get
		{
			return this.m_wispAddXMin;
		}
		set
		{
			this.m_wispAddXMin = value;
		}
	}

	// Token: 0x170008B4 RID: 2228
	// (get) Token: 0x060039F2 RID: 14834 RVA: 0x001311CC File Offset: 0x0012F3CC
	// (set) Token: 0x060039F1 RID: 14833 RVA: 0x001311C0 File Offset: 0x0012F3C0
	public float WispAddXMax
	{
		get
		{
			return this.m_wispAddXMax;
		}
		set
		{
			this.m_wispAddXMax = value;
		}
	}

	// Token: 0x170008B5 RID: 2229
	// (get) Token: 0x060039F4 RID: 14836 RVA: 0x001311E0 File Offset: 0x0012F3E0
	// (set) Token: 0x060039F3 RID: 14835 RVA: 0x001311D4 File Offset: 0x0012F3D4
	public float MissileWaitTime
	{
		get
		{
			return this.m_missileWaitTime;
		}
		set
		{
			this.m_missileWaitTime = value;
		}
	}

	// Token: 0x170008B6 RID: 2230
	// (get) Token: 0x060039F6 RID: 14838 RVA: 0x001311F4 File Offset: 0x0012F3F4
	// (set) Token: 0x060039F5 RID: 14837 RVA: 0x001311E8 File Offset: 0x0012F3E8
	public int MissileCount
	{
		get
		{
			return this.m_missileCount;
		}
		set
		{
			this.m_missileCount = value;
		}
	}

	// Token: 0x060039F7 RID: 14839 RVA: 0x001311FC File Offset: 0x0012F3FC
	private int GetBoostAttackParam(float attack, float challengeVal)
	{
		float num = attack * challengeVal;
		return (int)num;
	}

	// Token: 0x060039F8 RID: 14840 RVA: 0x00131210 File Offset: 0x0012F410
	public int GetBoostAttackParam(WispBoostLevel level)
	{
		int result = 0;
		switch (level)
		{
		case WispBoostLevel.LEVEL1:
			result = this.GetBoostAttackParam(EventBossParamTable.GetItemData(EventBossParamTableItem.BoostAttack1), this.m_challengeValue);
			break;
		case WispBoostLevel.LEVEL2:
			result = this.GetBoostAttackParam(EventBossParamTable.GetItemData(EventBossParamTableItem.BoostAttack2), this.m_challengeValue);
			break;
		case WispBoostLevel.LEVEL3:
			result = this.GetBoostAttackParam(EventBossParamTable.GetItemData(EventBossParamTableItem.BoostAttack3), this.m_challengeValue);
			break;
		}
		return result;
	}

	// Token: 0x060039F9 RID: 14841 RVA: 0x00131284 File Offset: 0x0012F484
	public float GetBoostSpeedParam(WispBoostLevel level)
	{
		float result = 0f;
		switch (level)
		{
		case WispBoostLevel.LEVEL1:
			result = EventBossParamTable.GetItemData(EventBossParamTableItem.BoostSpeed1);
			break;
		case WispBoostLevel.LEVEL2:
			result = EventBossParamTable.GetItemData(EventBossParamTableItem.BoostSpeed2);
			break;
		case WispBoostLevel.LEVEL3:
			result = EventBossParamTable.GetItemData(EventBossParamTableItem.BoostSpeed3);
			break;
		}
		return result;
	}

	// Token: 0x0400305C RID: 12380
	private float m_wispBoostRatio;

	// Token: 0x0400305D RID: 12381
	private WispBoostLevel m_wispBoostLevel = WispBoostLevel.NONE;

	// Token: 0x0400305E RID: 12382
	private int m_missilePos1;

	// Token: 0x0400305F RID: 12383
	private int m_missilePos2;

	// Token: 0x04003060 RID: 12384
	private float m_wispInterspace;

	// Token: 0x04003061 RID: 12385
	private float m_bumperInterspace;

	// Token: 0x04003062 RID: 12386
	private float m_wispSpeedMin;

	// Token: 0x04003063 RID: 12387
	private float m_wispSpeedMax;

	// Token: 0x04003064 RID: 12388
	private float m_wispSwingMin;

	// Token: 0x04003065 RID: 12389
	private float m_wispSwingMax;

	// Token: 0x04003066 RID: 12390
	private float m_wispAddXMin;

	// Token: 0x04003067 RID: 12391
	private float m_wispAddXMax;

	// Token: 0x04003068 RID: 12392
	private float m_missileWaitTime;

	// Token: 0x04003069 RID: 12393
	private int m_missileCount;

	// Token: 0x0400306A RID: 12394
	private float m_challengeValue;
}
