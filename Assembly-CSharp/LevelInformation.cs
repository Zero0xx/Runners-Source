using System;
using App.Utility;
using UnityEngine;

// Token: 0x02000267 RID: 615
public class LevelInformation : MonoBehaviour
{
	// Token: 0x06001081 RID: 4225 RVA: 0x000606C4 File Offset: 0x0005E8C4
	private void Start()
	{
		this.m_showLevelInfo = false;
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x000606D0 File Offset: 0x0005E8D0
	private void Update()
	{
		if (this.NowFeverBoss || this.NowBoss)
		{
			this.m_distanceToBoss = 0f;
		}
		else
		{
			this.m_distanceToBoss = Mathf.Max(0f, this.m_distanceToBossOnStart - this.m_distanceOnStage);
		}
	}

	// Token: 0x1700027C RID: 636
	// (get) Token: 0x06001083 RID: 4227 RVA: 0x00060720 File Offset: 0x0005E920
	public float DistanceToBoss
	{
		get
		{
			return this.m_distanceToBoss;
		}
	}

	// Token: 0x1700027D RID: 637
	// (get) Token: 0x06001084 RID: 4228 RVA: 0x00060728 File Offset: 0x0005E928
	// (set) Token: 0x06001085 RID: 4229 RVA: 0x00060738 File Offset: 0x0005E938
	public bool NowFeverBoss
	{
		get
		{
			return this.m_status.Test(0);
		}
		set
		{
			this.m_status.Set(0, value);
		}
	}

	// Token: 0x1700027E RID: 638
	// (get) Token: 0x06001086 RID: 4230 RVA: 0x00060748 File Offset: 0x0005E948
	// (set) Token: 0x06001087 RID: 4231 RVA: 0x00060758 File Offset: 0x0005E958
	public bool NowBoss
	{
		get
		{
			return this.m_status.Test(1);
		}
		set
		{
			this.m_status.Set(1, value);
		}
	}

	// Token: 0x1700027F RID: 639
	// (get) Token: 0x06001088 RID: 4232 RVA: 0x00060768 File Offset: 0x0005E968
	// (set) Token: 0x06001089 RID: 4233 RVA: 0x00060778 File Offset: 0x0005E978
	public bool BossDestroy
	{
		get
		{
			return this.m_status.Test(2);
		}
		set
		{
			this.m_status.Set(2, value);
		}
	}

	// Token: 0x17000280 RID: 640
	// (get) Token: 0x0600108A RID: 4234 RVA: 0x00060788 File Offset: 0x0005E988
	// (set) Token: 0x0600108B RID: 4235 RVA: 0x00060798 File Offset: 0x0005E998
	public bool NowTutorial
	{
		get
		{
			return this.m_status.Test(3);
		}
		set
		{
			this.m_status.Set(3, value);
		}
	}

	// Token: 0x17000281 RID: 641
	// (get) Token: 0x0600108C RID: 4236 RVA: 0x000607A8 File Offset: 0x0005E9A8
	// (set) Token: 0x0600108D RID: 4237 RVA: 0x000607B8 File Offset: 0x0005E9B8
	public bool BossStage
	{
		get
		{
			return this.m_status.Test(4);
		}
		set
		{
			this.m_status.Set(4, value);
		}
	}

	// Token: 0x17000282 RID: 642
	// (get) Token: 0x0600108E RID: 4238 RVA: 0x000607C8 File Offset: 0x0005E9C8
	// (set) Token: 0x0600108F RID: 4239 RVA: 0x000607D8 File Offset: 0x0005E9D8
	public bool Missed
	{
		get
		{
			return this.m_status.Test(5);
		}
		set
		{
			this.m_status.Set(5, value);
		}
	}

	// Token: 0x17000283 RID: 643
	// (get) Token: 0x06001090 RID: 4240 RVA: 0x000607E8 File Offset: 0x0005E9E8
	// (set) Token: 0x06001091 RID: 4241 RVA: 0x000607F8 File Offset: 0x0005E9F8
	public bool RequestPause
	{
		get
		{
			return this.m_status.Test(6);
		}
		set
		{
			this.m_status.Set(6, value);
		}
	}

	// Token: 0x17000284 RID: 644
	// (get) Token: 0x06001092 RID: 4242 RVA: 0x00060808 File Offset: 0x0005EA08
	// (set) Token: 0x06001093 RID: 4243 RVA: 0x00060818 File Offset: 0x0005EA18
	public bool RequestCharaChange
	{
		get
		{
			return this.m_status.Test(6);
		}
		set
		{
			this.m_status.Set(6, value);
		}
	}

	// Token: 0x17000285 RID: 645
	// (get) Token: 0x06001094 RID: 4244 RVA: 0x00060828 File Offset: 0x0005EA28
	// (set) Token: 0x06001095 RID: 4245 RVA: 0x00060838 File Offset: 0x0005EA38
	public bool RequestEqitpItem
	{
		get
		{
			return this.m_status.Test(8);
		}
		set
		{
			this.m_status.Set(8, value);
		}
	}

	// Token: 0x17000286 RID: 646
	// (get) Token: 0x06001096 RID: 4246 RVA: 0x00060848 File Offset: 0x0005EA48
	// (set) Token: 0x06001097 RID: 4247 RVA: 0x00060850 File Offset: 0x0005EA50
	public float DistanceToBossOnStart
	{
		get
		{
			return this.m_distanceToBossOnStart;
		}
		set
		{
			this.m_distanceToBossOnStart = value;
		}
	}

	// Token: 0x17000287 RID: 647
	// (get) Token: 0x06001098 RID: 4248 RVA: 0x0006085C File Offset: 0x0005EA5C
	// (set) Token: 0x06001099 RID: 4249 RVA: 0x00060864 File Offset: 0x0005EA64
	public float DistanceOnStage
	{
		get
		{
			return this.m_distanceOnStage;
		}
		set
		{
			this.m_distanceOnStage = value;
		}
	}

	// Token: 0x17000288 RID: 648
	// (get) Token: 0x0600109A RID: 4250 RVA: 0x00060870 File Offset: 0x0005EA70
	// (set) Token: 0x0600109B RID: 4251 RVA: 0x00060878 File Offset: 0x0005EA78
	public float BossEndTime
	{
		get
		{
			return this.m_bossEndTime;
		}
		set
		{
			this.m_bossEndTime = value;
		}
	}

	// Token: 0x17000289 RID: 649
	// (get) Token: 0x0600109C RID: 4252 RVA: 0x00060884 File Offset: 0x0005EA84
	// (set) Token: 0x0600109D RID: 4253 RVA: 0x0006088C File Offset: 0x0005EA8C
	public int NumBossAttack
	{
		get
		{
			return this.m_numBossAttack;
		}
		set
		{
			this.m_numBossAttack = value;
		}
	}

	// Token: 0x1700028A RID: 650
	// (get) Token: 0x0600109E RID: 4254 RVA: 0x00060898 File Offset: 0x0005EA98
	// (set) Token: 0x0600109F RID: 4255 RVA: 0x000608A0 File Offset: 0x0005EAA0
	public int NumBossHpMax
	{
		get
		{
			return this.m_numBossHpMax;
		}
		set
		{
			this.m_numBossHpMax = value;
		}
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x000608AC File Offset: 0x0005EAAC
	public void AddNumBossAttack(int count)
	{
		this.m_numBossAttack += count;
	}

	// Token: 0x1700028B RID: 651
	// (get) Token: 0x060010A1 RID: 4257 RVA: 0x000608BC File Offset: 0x0005EABC
	// (set) Token: 0x060010A2 RID: 4258 RVA: 0x000608C4 File Offset: 0x0005EAC4
	public int PlayerRank
	{
		get
		{
			return this.m_playerRank;
		}
		set
		{
			this.m_playerRank = value;
		}
	}

	// Token: 0x1700028C RID: 652
	// (get) Token: 0x060010A3 RID: 4259 RVA: 0x000608D0 File Offset: 0x0005EAD0
	// (set) Token: 0x060010A4 RID: 4260 RVA: 0x000608D8 File Offset: 0x0005EAD8
	public bool LightMode
	{
		get
		{
			return this.m_lightMode;
		}
		set
		{
			this.m_lightMode = value;
		}
	}

	// Token: 0x1700028D RID: 653
	// (get) Token: 0x060010A5 RID: 4261 RVA: 0x000608E4 File Offset: 0x0005EAE4
	// (set) Token: 0x060010A6 RID: 4262 RVA: 0x000608EC File Offset: 0x0005EAEC
	public int FeverBossCount
	{
		get
		{
			return this.m_feverBossCount;
		}
		set
		{
			this.m_feverBossCount = value;
		}
	}

	// Token: 0x1700028E RID: 654
	// (get) Token: 0x060010A7 RID: 4263 RVA: 0x000608F8 File Offset: 0x0005EAF8
	// (set) Token: 0x060010A8 RID: 4264 RVA: 0x00060900 File Offset: 0x0005EB00
	public bool Extreme
	{
		get
		{
			return this.m_extreme;
		}
		set
		{
			this.m_extreme = value;
		}
	}

	// Token: 0x1700028F RID: 655
	// (get) Token: 0x060010A9 RID: 4265 RVA: 0x0006090C File Offset: 0x0005EB0C
	// (set) Token: 0x060010AA RID: 4266 RVA: 0x00060914 File Offset: 0x0005EB14
	public bool InvalidExtreme
	{
		get
		{
			return this.m_invalidExtreme;
		}
		set
		{
			this.m_invalidExtreme = value;
		}
	}

	// Token: 0x17000290 RID: 656
	// (get) Token: 0x060010AB RID: 4267 RVA: 0x00060920 File Offset: 0x0005EB20
	public bool DestroyRingMode
	{
		get
		{
			return this.m_extreme && !this.m_invalidExtreme;
		}
	}

	// Token: 0x17000291 RID: 657
	// (get) Token: 0x060010AC RID: 4268 RVA: 0x0006093C File Offset: 0x0005EB3C
	// (set) Token: 0x060010AD RID: 4269 RVA: 0x00060944 File Offset: 0x0005EB44
	public int MoveTrapBooRand
	{
		get
		{
			return this.m_moveTrapBooRand;
		}
		set
		{
			this.m_moveTrapBooRand = value;
		}
	}

	// Token: 0x04000F11 RID: 3857
	private float m_distanceToBossOnStart;

	// Token: 0x04000F12 RID: 3858
	private float m_distanceOnStage;

	// Token: 0x04000F13 RID: 3859
	private float m_distanceToBoss;

	// Token: 0x04000F14 RID: 3860
	private float m_bossEndTime;

	// Token: 0x04000F15 RID: 3861
	private int m_numBossAttack;

	// Token: 0x04000F16 RID: 3862
	private int m_numBossHpMax;

	// Token: 0x04000F17 RID: 3863
	private Bitset32 m_status;

	// Token: 0x04000F18 RID: 3864
	private int m_playerRank;

	// Token: 0x04000F19 RID: 3865
	private bool m_lightMode;

	// Token: 0x04000F1A RID: 3866
	private Rect m_window;

	// Token: 0x04000F1B RID: 3867
	public bool m_showLevelInfo;

	// Token: 0x04000F1C RID: 3868
	private int m_feverBossCount;

	// Token: 0x04000F1D RID: 3869
	private int m_moveTrapBooRand;

	// Token: 0x04000F1E RID: 3870
	private bool m_extreme;

	// Token: 0x04000F1F RID: 3871
	private bool m_invalidExtreme;

	// Token: 0x02000268 RID: 616
	private enum Status
	{
		// Token: 0x04000F21 RID: 3873
		FEVER_BOSS,
		// Token: 0x04000F22 RID: 3874
		BOSS,
		// Token: 0x04000F23 RID: 3875
		BOSS_DESTROY,
		// Token: 0x04000F24 RID: 3876
		TUTORIAL,
		// Token: 0x04000F25 RID: 3877
		BOSS_STAGE,
		// Token: 0x04000F26 RID: 3878
		MISSED,
		// Token: 0x04000F27 RID: 3879
		REQEST_PAUSE,
		// Token: 0x04000F28 RID: 3880
		REQEST_CHARA_CHANGE,
		// Token: 0x04000F29 RID: 3881
		REQEST_EQUIP_ITEM
	}
}
