using System;
using GameScore;
using Message;
using UnityEngine;

// Token: 0x02000315 RID: 789
[AddComponentMenu("Scripts/Runners/GameMode/Stage")]
public class StageScoreManager : MonoBehaviour
{
	// Token: 0x17000379 RID: 889
	// (get) Token: 0x06001730 RID: 5936 RVA: 0x00085650 File Offset: 0x00083850
	public static StageScoreManager Instance
	{
		get
		{
			return StageScoreManager.instance;
		}
	}

	// Token: 0x1700037A RID: 890
	// (get) Token: 0x06001731 RID: 5937 RVA: 0x00085658 File Offset: 0x00083858
	public StageScoreManager.ResultData ScoreData
	{
		get
		{
			return this.GetResultData(StageScoreManager.DataType.Score);
		}
	}

	// Token: 0x1700037B RID: 891
	// (get) Token: 0x06001732 RID: 5938 RVA: 0x00085664 File Offset: 0x00083864
	public StageScoreManager.ResultData MileageBonusScoreData
	{
		get
		{
			return this.GetResultData(StageScoreManager.DataType.MileageBonusScore);
		}
	}

	// Token: 0x1700037C RID: 892
	// (get) Token: 0x06001733 RID: 5939 RVA: 0x00085670 File Offset: 0x00083870
	public StageScoreManager.ResultData CountData
	{
		get
		{
			return this.GetResultData(StageScoreManager.DataType.StageCount);
		}
	}

	// Token: 0x1700037D RID: 893
	// (get) Token: 0x06001734 RID: 5940 RVA: 0x0008567C File Offset: 0x0008387C
	public StageScoreManager.ResultData BonusCountData
	{
		get
		{
			return this.GetResultData(StageScoreManager.DataType.BonusCount);
		}
	}

	// Token: 0x1700037E RID: 894
	// (get) Token: 0x06001735 RID: 5941 RVA: 0x00085688 File Offset: 0x00083888
	public StageScoreManager.ResultData BonusCountMainChaoData
	{
		get
		{
			return this.GetResultData(StageScoreManager.DataType.BonusCount_MainChao);
		}
	}

	// Token: 0x1700037F RID: 895
	// (get) Token: 0x06001736 RID: 5942 RVA: 0x00085694 File Offset: 0x00083894
	public StageScoreManager.ResultData BonusCountSubChaoData
	{
		get
		{
			return this.GetResultData(StageScoreManager.DataType.BonusCount_SubChao);
		}
	}

	// Token: 0x17000380 RID: 896
	// (get) Token: 0x06001737 RID: 5943 RVA: 0x000856A0 File Offset: 0x000838A0
	public StageScoreManager.ResultData BonusCountChaoCountData
	{
		get
		{
			return this.GetResultData(StageScoreManager.DataType.BonusCount_ChaoCount);
		}
	}

	// Token: 0x17000381 RID: 897
	// (get) Token: 0x06001738 RID: 5944 RVA: 0x000856AC File Offset: 0x000838AC
	public StageScoreManager.ResultData BonusCountMainCharaData
	{
		get
		{
			return this.GetResultData(StageScoreManager.DataType.BonusCount_MainChara);
		}
	}

	// Token: 0x17000382 RID: 898
	// (get) Token: 0x06001739 RID: 5945 RVA: 0x000856B8 File Offset: 0x000838B8
	public StageScoreManager.ResultData BonusCountSubCharaData
	{
		get
		{
			return this.GetResultData(StageScoreManager.DataType.BonusCount_SubChara);
		}
	}

	// Token: 0x17000383 RID: 899
	// (get) Token: 0x0600173A RID: 5946 RVA: 0x000856C4 File Offset: 0x000838C4
	public StageScoreManager.ResultData BonusCountCampaignData
	{
		get
		{
			return this.GetResultData(StageScoreManager.DataType.BonusCount_Campaign);
		}
	}

	// Token: 0x17000384 RID: 900
	// (get) Token: 0x0600173B RID: 5947 RVA: 0x000856D0 File Offset: 0x000838D0
	public StageScoreManager.ResultData BonusCountRankData
	{
		get
		{
			return this.GetResultData(StageScoreManager.DataType.BonusCount_Rank);
		}
	}

	// Token: 0x17000385 RID: 901
	// (get) Token: 0x0600173C RID: 5948 RVA: 0x000856DC File Offset: 0x000838DC
	public StageScoreManager.ResultData FinalCountData
	{
		get
		{
			return this.GetResultData(StageScoreManager.DataType.FinalCount);
		}
	}

	// Token: 0x0600173D RID: 5949 RVA: 0x000856E8 File Offset: 0x000838E8
	public StageScoreManager.ResultData GetResultData(StageScoreManager.DataType type)
	{
		if (type < StageScoreManager.DataType.NUM && this.m_results != null && type < (StageScoreManager.DataType)this.m_results.Length)
		{
			return this.m_results[(int)type];
		}
		return new StageScoreManager.ResultData();
	}

	// Token: 0x17000386 RID: 902
	// (get) Token: 0x0600173E RID: 5950 RVA: 0x0008571C File Offset: 0x0008391C
	public long ResultChaoBonusTotal
	{
		get
		{
			long num = 0L;
			num += this.GetResultBonusScore(StageScoreManager.DataType.BonusCount_MainChao);
			num += this.GetResultBonusScore(StageScoreManager.DataType.BonusCount_SubChao);
			return num + this.GetResultBonusScore(StageScoreManager.DataType.BonusCount_ChaoCount);
		}
	}

	// Token: 0x17000387 RID: 903
	// (get) Token: 0x0600173F RID: 5951 RVA: 0x0008574C File Offset: 0x0008394C
	public long ResultCampaignBonusTotal
	{
		get
		{
			long num = 0L;
			return num + this.GetResultBonusScore(StageScoreManager.DataType.BonusCount_Campaign);
		}
	}

	// Token: 0x17000388 RID: 904
	// (get) Token: 0x06001740 RID: 5952 RVA: 0x00085768 File Offset: 0x00083968
	public long ResultPlayerBonusTotal
	{
		get
		{
			long num = 0L;
			num += this.GetResultBonusScore(StageScoreManager.DataType.BonusCount_MainChara);
			num += this.GetResultBonusScore(StageScoreManager.DataType.BonusCount_SubChara);
			return num + this.GetResultBonusScore(StageScoreManager.DataType.BonusCount_Rank);
		}
	}

	// Token: 0x06001741 RID: 5953 RVA: 0x00085798 File Offset: 0x00083998
	public long GetResultBonusScore(StageScoreManager.DataType type)
	{
		long num = 0L;
		num += this.m_results[(int)type].score * (long)this.m_scoreCoefficient;
		num += this.m_results[(int)type].ring * (long)this.m_stockRingCoefficient;
		num += this.m_results[(int)type].animal * (long)this.m_animalCoefficient;
		return num + this.m_results[(int)type].distance * (long)this.m_totaldistanceCoefficient;
	}

	// Token: 0x17000389 RID: 905
	// (get) Token: 0x06001742 RID: 5954 RVA: 0x0008580C File Offset: 0x00083A0C
	public long FinalScore
	{
		get
		{
			return this.m_final_score.Get();
		}
	}

	// Token: 0x1700038A RID: 906
	// (get) Token: 0x06001743 RID: 5955 RVA: 0x0008581C File Offset: 0x00083A1C
	public long Score
	{
		get
		{
			return this.m_score.Get();
		}
	}

	// Token: 0x1700038B RID: 907
	// (get) Token: 0x06001744 RID: 5956 RVA: 0x0008582C File Offset: 0x00083A2C
	public long Animal
	{
		get
		{
			return this.m_animal.Get();
		}
	}

	// Token: 0x1700038C RID: 908
	// (get) Token: 0x06001745 RID: 5957 RVA: 0x0008583C File Offset: 0x00083A3C
	public long Ring
	{
		get
		{
			return this.m_ring.Get();
		}
	}

	// Token: 0x1700038D RID: 909
	// (get) Token: 0x06001746 RID: 5958 RVA: 0x0008584C File Offset: 0x00083A4C
	public long RedRing
	{
		get
		{
			return this.m_red_ring.Get();
		}
	}

	// Token: 0x1700038E RID: 910
	// (get) Token: 0x06001747 RID: 5959 RVA: 0x0008585C File Offset: 0x00083A5C
	public long SpecialCrystal
	{
		get
		{
			return this.m_special_crystal.Get();
		}
	}

	// Token: 0x1700038F RID: 911
	// (get) Token: 0x06001748 RID: 5960 RVA: 0x0008586C File Offset: 0x00083A6C
	public long RaidBossRing
	{
		get
		{
			return this.m_raid_boss_ring.Get();
		}
	}

	// Token: 0x17000390 RID: 912
	// (get) Token: 0x06001749 RID: 5961 RVA: 0x0008587C File Offset: 0x00083A7C
	public long CollectEventCount
	{
		get
		{
			return this.m_collectEventCount.Get();
		}
	}

	// Token: 0x17000391 RID: 913
	// (get) Token: 0x0600174A RID: 5962 RVA: 0x0008588C File Offset: 0x00083A8C
	public int ContinueRing
	{
		get
		{
			return this.m_continueRing;
		}
	}

	// Token: 0x17000392 RID: 914
	// (get) Token: 0x0600174B RID: 5963 RVA: 0x00085894 File Offset: 0x00083A94
	public int ContinueRaidBossRing
	{
		get
		{
			return this.m_continueRaidBossRing;
		}
	}

	// Token: 0x17000393 RID: 915
	// (get) Token: 0x0600174C RID: 5964 RVA: 0x0008589C File Offset: 0x00083A9C
	// (set) Token: 0x0600174D RID: 5965 RVA: 0x000858A4 File Offset: 0x00083AA4
	public StageScorePool ScorePool
	{
		get
		{
			return this.m_scorePool;
		}
		private set
		{
		}
	}

	// Token: 0x0600174E RID: 5966 RVA: 0x000858A8 File Offset: 0x00083AA8
	protected void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x0600174F RID: 5967 RVA: 0x000858B0 File Offset: 0x00083AB0
	private void Start()
	{
		GameObject gameObject = GameObject.Find("PlayerInformation");
		if (gameObject != null)
		{
			this.m_information = gameObject.GetComponent<PlayerInformation>();
		}
		this.ResetScore();
		this.m_scorePool = new StageScorePool();
	}

	// Token: 0x06001750 RID: 5968 RVA: 0x000858F4 File Offset: 0x00083AF4
	private void OnDestroy()
	{
		if (StageScoreManager.instance == this)
		{
			StageScoreManager.instance = null;
		}
	}

	// Token: 0x06001751 RID: 5969 RVA: 0x0008590C File Offset: 0x00083B0C
	private void SetInstance()
	{
		if (StageScoreManager.instance == null)
		{
			StageScoreManager.instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001752 RID: 5970 RVA: 0x00085940 File Offset: 0x00083B40
	public void Setup(bool bossStage)
	{
		this.m_bossStage = bossStage;
		if (StageModeManager.Instance != null)
		{
			this.m_quickMode = StageModeManager.Instance.IsQuickMode();
		}
		this.m_levelInformation = GameObjectUtil.FindGameObjectComponent<LevelInformation>("LevelInformation");
		if (this.m_scorePool != null)
		{
			this.m_scorePool.CheckHalfWay();
		}
		CPlusPlusLink cplusPlusLink = CPlusPlusLink.Instance;
		if (cplusPlusLink != null)
		{
			cplusPlusLink.ResetNativeResultScore();
		}
	}

	// Token: 0x06001753 RID: 5971 RVA: 0x000859B4 File Offset: 0x00083BB4
	public void ResetScore(MsgResetScore msg)
	{
		this.ResetScore((long)msg.m_score, (long)msg.m_animal, (long)msg.m_ring, (long)msg.m_red_ring, (long)msg.m_final_score);
	}

	// Token: 0x06001754 RID: 5972 RVA: 0x000859E0 File Offset: 0x00083BE0
	public void AddScore(long score)
	{
		this.m_score.Set(this.m_score.Get() + score);
	}

	// Token: 0x06001755 RID: 5973 RVA: 0x000859FC File Offset: 0x00083BFC
	public void AddAnimal(long addCount)
	{
		this.m_animal.Set(this.m_animal.Get() + addCount);
	}

	// Token: 0x06001756 RID: 5974 RVA: 0x00085A18 File Offset: 0x00083C18
	public void AddRedRing()
	{
		this.m_red_ring.Set(this.m_red_ring.Get() + 1L);
	}

	// Token: 0x06001757 RID: 5975 RVA: 0x00085A40 File Offset: 0x00083C40
	public void AddRecoveryRingCount(int addCount)
	{
		int transforDoubleRing = this.GetTransforDoubleRing(addCount);
		this.m_ring.Set(this.m_ring.Get() + (long)transforDoubleRing);
		ObjUtil.SendMessageScoreCheck(new StageScoreData(9, transforDoubleRing));
	}

	// Token: 0x06001758 RID: 5976 RVA: 0x00085A7C File Offset: 0x00083C7C
	public void AddSpecialCrystal(long addCount)
	{
		this.m_special_crystal.Set(this.m_special_crystal.Get() + addCount);
	}

	// Token: 0x06001759 RID: 5977 RVA: 0x00085A98 File Offset: 0x00083C98
	public void AddScoreCheck(StageScoreData scoreData)
	{
		if (this.m_scorePool != null)
		{
			this.m_scorePool.AddScore(scoreData);
		}
	}

	// Token: 0x0600175A RID: 5978 RVA: 0x00085AB4 File Offset: 0x00083CB4
	private void ResetScore()
	{
		this.ResetScore(0L, 0L, 0L, 0L, 0L);
		this.m_special_crystal.Set(0L);
		this.m_raid_boss_ring.Set(0L);
		this.m_collectEventCount.Set(0L);
		this.m_realtime_score_back = 0L;
		this.m_animal_back = 0L;
		this.m_event_score_back = 0L;
		this.m_realtime_score_old = 0L;
		this.m_animal_old = 0L;
		this.m_event_score_old = 0L;
	}

	// Token: 0x0600175B RID: 5979 RVA: 0x00085B28 File Offset: 0x00083D28
	private void ResetScore(long score, long animal, long ring, long red_ring, long final_score)
	{
		this.m_score.Set(score);
		this.m_animal.Set(animal);
		this.m_ring.Set(ring);
		this.m_red_ring.Set(red_ring);
		this.m_final_score.Set(final_score);
	}

	// Token: 0x0600175C RID: 5980 RVA: 0x00085B74 File Offset: 0x00083D74
	private int GetTransforDoubleRing(int transferRingCount)
	{
		if (StageAbilityManager.Instance != null && StageAbilityManager.Instance.HasChaoAbility(ChaoAbility.TRANSFER_DOUBLE_RING))
		{
			ObjUtil.RequestStartAbilityToChao(ChaoAbility.TRANSFER_DOUBLE_RING, false);
			return StageAbilityManager.Instance.GetChaoAbliltyValue(ChaoAbility.TRANSFER_DOUBLE_RING, transferRingCount);
		}
		return transferRingCount;
	}

	// Token: 0x0600175D RID: 5981 RVA: 0x00085BBC File Offset: 0x00083DBC
	public void TransferRingForContinue(int ring)
	{
		if (this.m_information != null)
		{
			int transforDoubleRing = this.GetTransforDoubleRing(this.m_information.NumRings);
			this.m_ring.Set(this.m_ring.Get() + (long)transforDoubleRing);
			ObjUtil.SendMessageScoreCheck(new StageScoreData(9, transforDoubleRing));
			this.m_information.SetNumRings(ring);
			GameObjectUtil.SendMessageToTagObjects("Player", "OnResetRingsForContinue", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0600175E RID: 5982 RVA: 0x00085C30 File Offset: 0x00083E30
	public void TransferRingForChaoAbility(int ring)
	{
		int transforDoubleRing = this.GetTransforDoubleRing(ring);
		this.m_ring.Set(this.m_ring.Get() + (long)transforDoubleRing);
		ObjUtil.SendMessageScoreCheck(new StageScoreData(9, transforDoubleRing));
	}

	// Token: 0x0600175F RID: 5983 RVA: 0x00085C6C File Offset: 0x00083E6C
	public bool DefrayItemCostByRing(long costRing)
	{
		bool flag = false;
		if (this.m_ring.Get() > 0L)
		{
			if (this.m_ring.Get() > costRing)
			{
				this.m_ring.Set(this.m_ring.Get() - costRing);
			}
			else
			{
				this.m_ring.Set(0L);
			}
			flag = true;
		}
		else if (this.m_information != null)
		{
			long num = (long)this.m_information.NumRings;
			if (num > 0L)
			{
				if (num > costRing)
				{
					this.m_information.SetNumRings((int)(num - costRing));
				}
				else
				{
					this.m_information.SetNumRings(0);
				}
				flag = true;
			}
		}
		if (flag)
		{
			GameObjectUtil.SendMessageToTagObjects("Player", "OnDefrayRing", null, SendMessageOptions.DontRequireReceiver);
		}
		return flag;
	}

	// Token: 0x06001760 RID: 5984 RVA: 0x00085D38 File Offset: 0x00083F38
	public void TransferRing()
	{
		if (this.m_information != null && this.m_levelInformation != null)
		{
			int transforDoubleRing = this.GetTransforDoubleRing(this.m_information.NumRings);
			this.m_ring.Set(this.m_ring.Get() + (long)transforDoubleRing);
			ObjUtil.SendMessageScoreCheck(new StageScoreData(9, transforDoubleRing));
			bool flag = true;
			if (this.m_information.NumRings > 0 && !this.m_levelInformation.DestroyRingMode)
			{
				flag = false;
			}
			if (!flag)
			{
				this.m_information.SetNumRings(1);
			}
			else
			{
				this.m_information.SetNumRings(0);
			}
			GameObjectUtil.SendMessageToTagObjects("Player", "OnResetRingsForCheckPoint", new MsgPlayerTransferRing(flag), SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06001761 RID: 5985 RVA: 0x00085E00 File Offset: 0x00084000
	public void TransferRingCountToRaidBossRingCount()
	{
		if (EventManager.Instance != null && EventManager.Instance.IsRaidBossStage())
		{
			this.m_raid_boss_ring = this.m_ring;
			this.m_ring.Set(0L);
		}
	}

	// Token: 0x06001762 RID: 5986 RVA: 0x00085E48 File Offset: 0x00084048
	public void SendMessageFinalScoreBeforeResult()
	{
		if (!this.m_isFinalScore)
		{
			this.m_isFinalScore = true;
		}
		this.OnCalcFinalScore();
	}

	// Token: 0x06001763 RID: 5987 RVA: 0x00085E64 File Offset: 0x00084064
	public void OnCalcFinalScore()
	{
		this.m_results = null;
		this.m_results = new StageScoreManager.ResultData[12];
		for (int i = 0; i < 12; i++)
		{
			this.m_results[i] = new StageScoreManager.ResultData();
		}
		float num = (!(this.m_information != null)) ? 0f : this.m_information.TotalDistance;
		int num2 = (int)num;
		this.SetStageCount(num2);
		this.SetBonusCountType(num2, StageScoreManager.DataType.BonusCount_MainChao);
		this.SetBonusCountType(num2, StageScoreManager.DataType.BonusCount_SubChao);
		this.SetBonusCountType(num2, StageScoreManager.DataType.BonusCount_ChaoCount);
		this.SetBonusCountType(num2, StageScoreManager.DataType.BonusCount_MainChara);
		this.SetBonusCountType(num2, StageScoreManager.DataType.BonusCount_SubChara);
		this.SetBonusCountRank();
		this.SetBonusCountCampaign();
		this.SetBonusCount();
		this.SetFinalCount(num2);
		this.SetScore();
		this.SetMileageBonusScore();
		this.SetCollectEventCount();
		this.SetFinalScore();
	}

	// Token: 0x06001764 RID: 5988 RVA: 0x00085F30 File Offset: 0x00084130
	private void SetStageCount(int distance)
	{
		int num = 0;
		this.m_results[num].score = this.m_score.Get();
		this.m_results[num].animal = this.m_animal.Get();
		this.m_results[num].ring = this.m_ring.Get();
		this.m_results[num].red_ring = this.m_red_ring.Get();
		this.m_results[num].distance = (long)distance;
		this.m_results[num].sp_crystal = this.m_special_crystal.Get();
		this.m_results[num].raid_boss_ring = this.m_raid_boss_ring.Get();
	}

	// Token: 0x06001765 RID: 5989 RVA: 0x00085FE0 File Offset: 0x000841E0
	private void SetBonusCountType(int distance, StageScoreManager.DataType type)
	{
		if (StageAbilityManager.Instance != null)
		{
			StageAbilityManager.BonusRate bonusRate;
			switch (type)
			{
			case StageScoreManager.DataType.BonusCount_MainChao:
				bonusRate = StageAbilityManager.Instance.MainChaoBonusValueRate;
				break;
			case StageScoreManager.DataType.BonusCount_SubChao:
				bonusRate = StageAbilityManager.Instance.SubChaoBonusValueRate;
				break;
			case StageScoreManager.DataType.BonusCount_ChaoCount:
				bonusRate = StageAbilityManager.Instance.CountChaoBonusValueRate;
				break;
			case StageScoreManager.DataType.BonusCount_MainChara:
				bonusRate = StageAbilityManager.Instance.MainCharaBonusValueRate;
				break;
			case StageScoreManager.DataType.BonusCount_SubChara:
				bonusRate = StageAbilityManager.Instance.SubCharaBonusValueRate;
				break;
			default:
				return;
			}
			double value = (double)this.m_score.Get() * (double)bonusRate.score;
			double value2 = (double)this.m_animal.Get() * (double)bonusRate.animal;
			double value3 = (double)this.m_ring.Get() * (double)bonusRate.ring;
			double value4 = (double)distance * (double)bonusRate.distance;
			if (this.m_isFinalScore)
			{
				ObjUtil.SendMessageScoreCheck(new StageScoreData(13, (int)(bonusRate.score * 1000000f)));
				ObjUtil.SendMessageScoreCheck(new StageScoreData(14, (int)(bonusRate.ring * 1000000f)));
				ObjUtil.SendMessageScoreCheck(new StageScoreData(15, (int)(bonusRate.animal * 1000000f)));
				ObjUtil.SendMessageScoreCheck(new StageScoreData(16, (int)(bonusRate.distance * 1000000f)));
			}
			this.m_results[(int)type].score = this.GetRoundUpValue(value);
			this.m_results[(int)type].animal = this.GetRoundUpValue(value2);
			this.m_results[(int)type].ring = this.GetRoundUpValue(value3);
			this.m_results[(int)type].red_ring = 0L;
			this.m_results[(int)type].distance = this.GetRoundUpValue(value4);
		}
	}

	// Token: 0x06001766 RID: 5990 RVA: 0x000861A0 File Offset: 0x000843A0
	private void SetBonusCountRank()
	{
		int num = 8;
		float num2 = 0f;
		if (this.m_quickMode)
		{
			this.m_results[num].score = 0L;
		}
		else
		{
			uint num3 = 1U;
			if (SaveDataManager.Instance != null)
			{
				num3 = SaveDataManager.Instance.PlayerData.DisplayRank;
				if (num3 < 1U)
				{
					num3 = 1U;
				}
			}
			num2 = num3 * 0.01f;
			this.m_results[num].score = this.GetRoundUpValue((double)((float)this.m_score.Get() * num2));
		}
		if (this.m_isFinalScore)
		{
			ObjUtil.SendMessageScoreCheck(new StageScoreData(12, (int)(num2 * 1000000f)));
		}
	}

	// Token: 0x06001767 RID: 5991 RVA: 0x0008624C File Offset: 0x0008444C
	private void SetBonusCountCampaign()
	{
		if (StageAbilityManager.Instance != null)
		{
			float num = 0f;
			float campaignValueRate = StageAbilityManager.Instance.CampaignValueRate;
			if (campaignValueRate > 0f)
			{
				num = (float)this.m_ring.Get() * campaignValueRate;
			}
			int num2 = 7;
			this.m_results[num2].ring += this.GetRoundUpValue((double)num);
			if (this.m_isFinalScore)
			{
				ObjUtil.SendMessageScoreCheck(new StageScoreData(14, (int)(campaignValueRate * 1000000f)));
			}
		}
	}

	// Token: 0x06001768 RID: 5992 RVA: 0x000862D4 File Offset: 0x000844D4
	private void SetBonusCount()
	{
		if (StageAbilityManager.Instance != null)
		{
			int[] array = new int[]
			{
				2,
				3,
				4,
				5,
				6,
				7,
				8
			};
			int num = 1;
			for (int i = 0; i < array.Length; i++)
			{
				this.m_results[num].score += this.m_results[array[i]].score;
			}
			for (int j = 0; j < array.Length; j++)
			{
				this.m_results[num].animal += this.m_results[array[j]].animal;
			}
			for (int k = 0; k < array.Length; k++)
			{
				this.m_results[num].ring += this.m_results[array[k]].ring;
			}
			for (int l = 0; l < array.Length; l++)
			{
				this.m_results[num].distance += this.m_results[array[l]].distance;
			}
			for (int m = 0; m < array.Length; m++)
			{
				this.m_results[num].sp_crystal += this.m_results[array[m]].sp_crystal;
			}
			for (int n = 0; n < array.Length; n++)
			{
				this.m_results[num].raid_boss_ring += this.m_results[array[n]].raid_boss_ring;
			}
		}
	}

	// Token: 0x06001769 RID: 5993 RVA: 0x00086464 File Offset: 0x00084664
	private void SetFinalCount(int distance)
	{
		int num = 1;
		int num2 = 9;
		this.m_results[num2].score = this.m_score.Get() + this.m_results[num].score;
		this.m_results[num2].animal = this.m_animal.Get() + this.m_results[num].animal;
		this.m_results[num2].ring = this.m_ring.Get() + this.m_results[num].ring;
		this.m_results[num2].red_ring = this.m_red_ring.Get();
		this.m_results[num2].distance = (long)distance + this.m_results[num].distance;
		this.m_results[num2].sp_crystal = this.m_special_crystal.Get() + this.m_results[num].sp_crystal;
		this.m_results[num2].raid_boss_ring = this.m_raid_boss_ring.Get() + this.m_results[num].raid_boss_ring;
	}

	// Token: 0x0600176A RID: 5994 RVA: 0x0008656C File Offset: 0x0008476C
	private void SetScore()
	{
		int num = 10;
		int num2 = 9;
		this.m_results[num].score = this.m_results[num2].score * (long)this.m_scoreCoefficient;
		this.m_results[num].ring = this.m_results[num2].ring * (long)this.m_stockRingCoefficient;
		this.m_results[num].animal = this.m_results[num2].animal * (long)this.m_animalCoefficient;
		this.m_results[num].distance = this.m_results[num2].distance * (long)this.m_totaldistanceCoefficient;
	}

	// Token: 0x0600176B RID: 5995 RVA: 0x00086608 File Offset: 0x00084808
	private void SetMileageBonusScore()
	{
		if (StageAbilityManager.Instance != null)
		{
			StageAbilityManager.BonusRate mileageBonusScoreRate = StageAbilityManager.Instance.MileageBonusScoreRate;
			int num = 10;
			int num2 = 11;
			float num3 = (float)this.m_results[num].score * mileageBonusScoreRate.score;
			float num4 = (float)this.m_results[num].animal * mileageBonusScoreRate.animal;
			float num5 = (float)this.m_results[num].ring * mileageBonusScoreRate.ring;
			float num6 = (float)this.m_results[num].distance * mileageBonusScoreRate.distance;
			this.m_results[num2].score = this.GetRoundUpValue((double)num3);
			this.m_results[num2].animal = this.GetRoundUpValue((double)num4);
			this.m_results[num2].ring = this.GetRoundUpValue((double)num5);
			this.m_results[num2].distance = this.GetRoundUpValue((double)num6);
			if (this.m_isFinalScore)
			{
				ObjUtil.SendMessageScoreCheck(new StageScoreData(13, (int)(mileageBonusScoreRate.score * 1000000f)));
				ObjUtil.SendMessageScoreCheck(new StageScoreData(15, (int)(mileageBonusScoreRate.animal * 1000000f)));
				ObjUtil.SendMessageScoreCheck(new StageScoreData(14, (int)(mileageBonusScoreRate.ring * 1000000f)));
				ObjUtil.SendMessageScoreCheck(new StageScoreData(16, (int)(mileageBonusScoreRate.distance * 1000000f)));
			}
		}
	}

	// Token: 0x0600176C RID: 5996 RVA: 0x00086760 File Offset: 0x00084960
	private void SetFinalScore()
	{
		if (this.m_bossStage)
		{
			this.m_final_score.Set(0L);
		}
		else
		{
			this.m_final_score.Set(this.m_results[10].Sum() + this.m_results[11].Sum());
			if (StageAbilityManager.Instance != null)
			{
				float final_score = StageAbilityManager.Instance.MileageBonusScoreRate.final_score;
				if (final_score > 0f)
				{
					this.m_results[11].final_score = this.GetRoundUpValue((double)((float)this.m_final_score.Get() * final_score));
					this.m_final_score.Set(this.m_final_score.Get() + (long)((int)this.m_results[11].final_score));
				}
			}
			this.m_final_score.Set(this.GetTeamAbliltyResultScore(this.m_final_score.Get(), 1));
		}
	}

	// Token: 0x0600176D RID: 5997 RVA: 0x00086848 File Offset: 0x00084A48
	private void SetCollectEventCount()
	{
		if (EventManager.Instance != null && EventManager.Instance.IsCollectEvent())
		{
			switch (EventManager.Instance.CollectType)
			{
			case EventManager.CollectEventType.GET_ANIMALS:
				this.m_collectEventCount.Set(this.GetResultData(StageScoreManager.DataType.FinalCount).animal);
				break;
			case EventManager.CollectEventType.GET_RING:
				this.m_collectEventCount.Set(this.GetResultData(StageScoreManager.DataType.FinalCount).ring);
				break;
			case EventManager.CollectEventType.RUN_DISTANCE:
				this.m_collectEventCount.Set(this.GetResultData(StageScoreManager.DataType.FinalCount).distance);
				break;
			}
		}
	}

	// Token: 0x0600176E RID: 5998 RVA: 0x000868F0 File Offset: 0x00084AF0
	private long GetRoundUpValue(double value)
	{
		return (long)Math.Ceiling(value);
	}

	// Token: 0x0600176F RID: 5999 RVA: 0x000868FC File Offset: 0x00084AFC
	private long GetTeamAbliltyResultScore(long score, int coefficient)
	{
		StageAbilityManager stageAbilityManager = StageAbilityManager.Instance;
		if (stageAbilityManager != null)
		{
			return stageAbilityManager.GetTeamAbliltyResultScore(score, coefficient);
		}
		return score * (long)coefficient;
	}

	// Token: 0x06001770 RID: 6000 RVA: 0x00086928 File Offset: 0x00084B28
	private void OnMsgExitStage(MsgExitStage msg)
	{
		base.enabled = false;
	}

	// Token: 0x06001771 RID: 6001 RVA: 0x00086934 File Offset: 0x00084B34
	public void SetupScoreRate()
	{
		this.m_rank_rate = 0f;
		if (!this.m_quickMode && SaveDataManager.Instance != null)
		{
			uint num = SaveDataManager.Instance.PlayerData.DisplayRank;
			if (num < 1U)
			{
				num = 1U;
			}
			this.m_rank_rate = num * 0.01f;
		}
	}

	// Token: 0x06001772 RID: 6002 RVA: 0x00086994 File Offset: 0x00084B94
	private StageAbilityManager.BonusRate GetBonusRate(StageScoreManager.DataType type)
	{
		StageAbilityManager.BonusRate result = default(StageAbilityManager.BonusRate);
		switch (type)
		{
		case StageScoreManager.DataType.BonusCount_MainChao:
			result = StageAbilityManager.Instance.MainChaoBonusValueRate;
			break;
		case StageScoreManager.DataType.BonusCount_SubChao:
			result = StageAbilityManager.Instance.SubChaoBonusValueRate;
			break;
		case StageScoreManager.DataType.BonusCount_ChaoCount:
			result = StageAbilityManager.Instance.CountChaoBonusValueRate;
			break;
		case StageScoreManager.DataType.BonusCount_MainChara:
			result = StageAbilityManager.Instance.MainCharaBonusValueRate;
			break;
		case StageScoreManager.DataType.BonusCount_SubChara:
			result = StageAbilityManager.Instance.SubCharaBonusValueRate;
			break;
		}
		return result;
	}

	// Token: 0x06001773 RID: 6003 RVA: 0x00086A24 File Offset: 0x00084C24
	public long GetRealtimeScore()
	{
		long num = this.m_score.Get();
		long num2 = this.m_animal.Get();
		long num3 = this.m_ring.Get();
		long num4 = (long)this.m_information.TotalDistance;
		long num5 = num + num2 + num3 + num4;
		if (this.m_realtime_score_back == num5)
		{
			return this.m_realtime_score_old;
		}
		this.m_realtime_score_back = num5;
		if (StageAbilityManager.Instance != null)
		{
			double num6 = (double)this.m_score.Get();
			num += this.GetRoundUpValue(num6 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_MainChao).score);
			num += this.GetRoundUpValue(num6 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_SubChao).score);
			num += this.GetRoundUpValue(num6 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_ChaoCount).score);
			num += this.GetRoundUpValue(num6 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_MainChara).score);
			num += this.GetRoundUpValue(num6 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_SubChara).score);
			num += this.GetRoundUpValue(num6 * (double)this.m_rank_rate);
			num *= (long)this.m_scoreCoefficient;
			double num7 = (double)this.m_animal.Get();
			num2 += this.GetRoundUpValue(num7 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_MainChao).animal);
			num2 += this.GetRoundUpValue(num7 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_SubChao).animal);
			num2 += this.GetRoundUpValue(num7 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_MainChara).animal);
			num2 += this.GetRoundUpValue(num7 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_SubChara).animal);
			num2 *= (long)this.m_animalCoefficient;
			double num8 = (double)this.m_ring.Get();
			num3 += this.GetRoundUpValue(num8 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_MainChao).ring);
			num3 += this.GetRoundUpValue(num8 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_SubChao).ring);
			num3 += this.GetRoundUpValue(num8 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_MainChara).ring);
			num3 += this.GetRoundUpValue(num8 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_SubChara).ring);
			num3 += this.GetRoundUpValue(num8 * (double)StageAbilityManager.Instance.CampaignValueRate);
			num3 *= (long)this.m_stockRingCoefficient;
			double num9 = (double)num4;
			num4 += this.GetRoundUpValue(num9 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_MainChao).distance);
			num4 += this.GetRoundUpValue(num9 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_SubChao).distance);
			num4 += this.GetRoundUpValue(num9 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_MainChara).distance);
			num4 += this.GetRoundUpValue(num9 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_SubChara).distance);
			num4 *= (long)this.m_totaldistanceCoefficient;
		}
		long num10 = 0L;
		num10 += num;
		num10 += num2;
		num10 += num3;
		num10 += num4;
		this.m_realtime_score_old = this.GetTeamAbliltyResultScore(num10, 1);
		return this.m_realtime_score_old;
	}

	// Token: 0x06001774 RID: 6004 RVA: 0x00086D38 File Offset: 0x00084F38
	public long GetRealtimeEventScore()
	{
		long num = this.m_special_crystal.Get();
		if (this.m_event_score_back == num)
		{
			return this.m_event_score_old;
		}
		this.m_realtime_score_back = num;
		if (StageAbilityManager.Instance != null)
		{
			double num2 = (double)this.m_special_crystal.Get();
			num += this.GetRoundUpValue(num2 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_MainChao).sp_crystal);
			num += this.GetRoundUpValue(num2 * (double)this.GetBonusRate(StageScoreManager.DataType.BonusCount_SubChao).sp_crystal);
		}
		this.m_event_score_old = num;
		return num;
	}

	// Token: 0x06001775 RID: 6005 RVA: 0x00086DC8 File Offset: 0x00084FC8
	public long GetRealtimeEventAnimal()
	{
		long num = this.m_animal.Get();
		if (this.m_animal_back == num)
		{
			return this.m_animal_old;
		}
		this.m_animal_back = num;
		if (StageAbilityManager.Instance != null)
		{
			float num2 = (float)this.m_animal.Get();
			num += this.GetRoundUpValue((double)(num2 * this.GetBonusRate(StageScoreManager.DataType.BonusCount_MainChao).animal));
			num += this.GetRoundUpValue((double)(num2 * this.GetBonusRate(StageScoreManager.DataType.BonusCount_SubChao).animal));
			num += this.GetRoundUpValue((double)(num2 * this.GetBonusRate(StageScoreManager.DataType.BonusCount_MainChara).animal));
			num += this.GetRoundUpValue((double)(num2 * this.GetBonusRate(StageScoreManager.DataType.BonusCount_SubChara).animal));
		}
		this.m_animal_old = num;
		return num;
	}

	// Token: 0x040014B8 RID: 5304
	private const float FloatToInt = 1000000f;

	// Token: 0x040014B9 RID: 5305
	private readonly int m_stockRingCoefficient = Data.ResultRing;

	// Token: 0x040014BA RID: 5306
	private readonly int m_scoreCoefficient = 1;

	// Token: 0x040014BB RID: 5307
	private readonly int m_animalCoefficient = Data.ResultAnimal;

	// Token: 0x040014BC RID: 5308
	private readonly int m_totaldistanceCoefficient = Data.ResultDistance;

	// Token: 0x040014BD RID: 5309
	[SerializeField]
	private int m_continueRing = 1000;

	// Token: 0x040014BE RID: 5310
	[SerializeField]
	private int m_continueRaidBossRing = 500;

	// Token: 0x040014BF RID: 5311
	private StageScoreManager.MaskedLong m_score = default(StageScoreManager.MaskedLong);

	// Token: 0x040014C0 RID: 5312
	private StageScoreManager.MaskedLong m_animal = default(StageScoreManager.MaskedLong);

	// Token: 0x040014C1 RID: 5313
	private StageScoreManager.MaskedLong m_ring = default(StageScoreManager.MaskedLong);

	// Token: 0x040014C2 RID: 5314
	private StageScoreManager.MaskedLong m_red_ring = default(StageScoreManager.MaskedLong);

	// Token: 0x040014C3 RID: 5315
	private StageScoreManager.MaskedLong m_special_crystal = default(StageScoreManager.MaskedLong);

	// Token: 0x040014C4 RID: 5316
	private StageScoreManager.MaskedLong m_raid_boss_ring = default(StageScoreManager.MaskedLong);

	// Token: 0x040014C5 RID: 5317
	private StageScoreManager.MaskedLong m_final_score = default(StageScoreManager.MaskedLong);

	// Token: 0x040014C6 RID: 5318
	private StageScoreManager.MaskedLong m_collectEventCount = default(StageScoreManager.MaskedLong);

	// Token: 0x040014C7 RID: 5319
	private StageScoreManager.ResultData[] m_results;

	// Token: 0x040014C8 RID: 5320
	private PlayerInformation m_information;

	// Token: 0x040014C9 RID: 5321
	private LevelInformation m_levelInformation;

	// Token: 0x040014CA RID: 5322
	private bool m_bossStage;

	// Token: 0x040014CB RID: 5323
	private bool m_quickMode;

	// Token: 0x040014CC RID: 5324
	private bool m_isFinalScore;

	// Token: 0x040014CD RID: 5325
	private StageScorePool m_scorePool;

	// Token: 0x040014CE RID: 5326
	private float m_rank_rate;

	// Token: 0x040014CF RID: 5327
	private long m_realtime_score_back;

	// Token: 0x040014D0 RID: 5328
	private long m_animal_back;

	// Token: 0x040014D1 RID: 5329
	private long m_event_score_back;

	// Token: 0x040014D2 RID: 5330
	private long m_realtime_score_old;

	// Token: 0x040014D3 RID: 5331
	private long m_animal_old;

	// Token: 0x040014D4 RID: 5332
	private long m_event_score_old;

	// Token: 0x040014D5 RID: 5333
	private static StageScoreManager instance;

	// Token: 0x02000316 RID: 790
	public enum DataType
	{
		// Token: 0x040014D7 RID: 5335
		StageCount,
		// Token: 0x040014D8 RID: 5336
		BonusCount,
		// Token: 0x040014D9 RID: 5337
		BonusCount_MainChao,
		// Token: 0x040014DA RID: 5338
		BonusCount_SubChao,
		// Token: 0x040014DB RID: 5339
		BonusCount_ChaoCount,
		// Token: 0x040014DC RID: 5340
		BonusCount_MainChara,
		// Token: 0x040014DD RID: 5341
		BonusCount_SubChara,
		// Token: 0x040014DE RID: 5342
		BonusCount_Campaign,
		// Token: 0x040014DF RID: 5343
		BonusCount_Rank,
		// Token: 0x040014E0 RID: 5344
		FinalCount,
		// Token: 0x040014E1 RID: 5345
		Score,
		// Token: 0x040014E2 RID: 5346
		MileageBonusScore,
		// Token: 0x040014E3 RID: 5347
		NUM
	}

	// Token: 0x02000317 RID: 791
	public struct MaskedInt
	{
		// Token: 0x06001776 RID: 6006 RVA: 0x00086E90 File Offset: 0x00085090
		public void Set(int input)
		{
			if (this.m_mask == 0)
			{
				this.m_mask = UnityEngine.Random.Range(15, int.MaxValue);
				this.m_addNum = UnityEngine.Random.Range(15, 1024);
			}
			input += this.m_addNum;
			this.m_valueUp = (input & this.m_mask);
			this.m_valueDown = (input & ~this.m_mask);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00086EF4 File Offset: 0x000850F4
		public int Get()
		{
			int num = this.m_valueUp | this.m_valueDown;
			return num - this.m_addNum;
		}

		// Token: 0x040014E4 RID: 5348
		private int m_valueUp;

		// Token: 0x040014E5 RID: 5349
		private int m_valueDown;

		// Token: 0x040014E6 RID: 5350
		private int m_mask;

		// Token: 0x040014E7 RID: 5351
		private int m_addNum;
	}

	// Token: 0x02000318 RID: 792
	public struct MaskedLong
	{
		// Token: 0x06001778 RID: 6008 RVA: 0x00086F1C File Offset: 0x0008511C
		public void Set(long input)
		{
			if (this.m_mask == 0L)
			{
				this.m_mask = (long)UnityEngine.Random.Range(15, int.MaxValue);
				this.m_addNum = (long)UnityEngine.Random.Range(15, 1024);
			}
			input += this.m_addNum;
			this.m_valueUp = (input & this.m_mask);
			this.m_valueDown = (input & ~this.m_mask);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00086F84 File Offset: 0x00085184
		public long Get()
		{
			long num = this.m_valueUp | this.m_valueDown;
			return num - this.m_addNum;
		}

		// Token: 0x040014E8 RID: 5352
		private long m_valueUp;

		// Token: 0x040014E9 RID: 5353
		private long m_valueDown;

		// Token: 0x040014EA RID: 5354
		private long m_mask;

		// Token: 0x040014EB RID: 5355
		private long m_addNum;
	}

	// Token: 0x02000319 RID: 793
	public class ResultData
	{
		// Token: 0x0600177A RID: 6010 RVA: 0x00086FAC File Offset: 0x000851AC
		public ResultData()
		{
			this.m_animal = default(StageScoreManager.MaskedLong);
			this.m_ring = default(StageScoreManager.MaskedLong);
			this.m_red_ring = default(StageScoreManager.MaskedLong);
			this.m_sp_crystal = default(StageScoreManager.MaskedLong);
			this.m_raid_boss_ring = default(StageScoreManager.MaskedLong);
			this.m_raid_boss_reward = default(StageScoreManager.MaskedLong);
			this.m_score = default(StageScoreManager.MaskedLong);
			this.m_distance = default(StageScoreManager.MaskedLong);
			this.m_final_score = default(StageScoreManager.MaskedLong);
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x0008704C File Offset: 0x0008524C
		// (set) Token: 0x0600177C RID: 6012 RVA: 0x0008705C File Offset: 0x0008525C
		public long score
		{
			get
			{
				return this.m_score.Get();
			}
			set
			{
				this.m_score.Set(value);
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x0008706C File Offset: 0x0008526C
		// (set) Token: 0x0600177E RID: 6014 RVA: 0x0008707C File Offset: 0x0008527C
		public long animal
		{
			get
			{
				return this.m_animal.Get();
			}
			set
			{
				this.m_animal.Set(value);
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x0008708C File Offset: 0x0008528C
		// (set) Token: 0x06001780 RID: 6016 RVA: 0x0008709C File Offset: 0x0008529C
		public long ring
		{
			get
			{
				return this.m_ring.Get();
			}
			set
			{
				this.m_ring.Set(value);
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x000870AC File Offset: 0x000852AC
		// (set) Token: 0x06001782 RID: 6018 RVA: 0x000870BC File Offset: 0x000852BC
		public long red_ring
		{
			get
			{
				return this.m_red_ring.Get();
			}
			set
			{
				this.m_red_ring.Set(value);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x000870CC File Offset: 0x000852CC
		// (set) Token: 0x06001784 RID: 6020 RVA: 0x000870DC File Offset: 0x000852DC
		public long distance
		{
			get
			{
				return this.m_distance.Get();
			}
			set
			{
				this.m_distance.Set(value);
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x000870EC File Offset: 0x000852EC
		// (set) Token: 0x06001786 RID: 6022 RVA: 0x000870FC File Offset: 0x000852FC
		public long sp_crystal
		{
			get
			{
				return this.m_sp_crystal.Get();
			}
			set
			{
				this.m_sp_crystal.Set(value);
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x0008710C File Offset: 0x0008530C
		// (set) Token: 0x06001788 RID: 6024 RVA: 0x0008711C File Offset: 0x0008531C
		public long raid_boss_ring
		{
			get
			{
				return this.m_raid_boss_ring.Get();
			}
			set
			{
				this.m_raid_boss_ring.Set(value);
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x0008712C File Offset: 0x0008532C
		// (set) Token: 0x0600178A RID: 6026 RVA: 0x0008713C File Offset: 0x0008533C
		public long raid_boss_reward
		{
			get
			{
				return this.m_raid_boss_reward.Get();
			}
			set
			{
				this.m_raid_boss_reward.Set(value);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x0008714C File Offset: 0x0008534C
		// (set) Token: 0x0600178C RID: 6028 RVA: 0x0008715C File Offset: 0x0008535C
		public long final_score
		{
			get
			{
				return this.m_final_score.Get();
			}
			set
			{
				this.m_final_score.Set(value);
			}
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0008716C File Offset: 0x0008536C
		public long Sum()
		{
			long num = 0L;
			num += this.score;
			num += this.animal;
			num += this.ring;
			num += this.red_ring;
			return num + this.distance;
		}

		// Token: 0x040014EC RID: 5356
		private StageScoreManager.MaskedLong m_score;

		// Token: 0x040014ED RID: 5357
		private StageScoreManager.MaskedLong m_animal;

		// Token: 0x040014EE RID: 5358
		private StageScoreManager.MaskedLong m_ring;

		// Token: 0x040014EF RID: 5359
		private StageScoreManager.MaskedLong m_red_ring;

		// Token: 0x040014F0 RID: 5360
		private StageScoreManager.MaskedLong m_distance;

		// Token: 0x040014F1 RID: 5361
		private StageScoreManager.MaskedLong m_sp_crystal;

		// Token: 0x040014F2 RID: 5362
		private StageScoreManager.MaskedLong m_raid_boss_ring;

		// Token: 0x040014F3 RID: 5363
		private StageScoreManager.MaskedLong m_raid_boss_reward;

		// Token: 0x040014F4 RID: 5364
		private StageScoreManager.MaskedLong m_final_score;
	}
}
