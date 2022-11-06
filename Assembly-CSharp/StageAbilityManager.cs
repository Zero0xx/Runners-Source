using System;
using System.Collections.Generic;
using DataTable;
using Message;
using UnityEngine;

// Token: 0x020002FC RID: 764
public class StageAbilityManager : MonoBehaviour
{
	// Token: 0x17000361 RID: 865
	// (get) Token: 0x0600164A RID: 5706 RVA: 0x0007F980 File Offset: 0x0007DB80
	public static StageAbilityManager Instance
	{
		get
		{
			return StageAbilityManager.instance;
		}
	}

	// Token: 0x17000362 RID: 866
	// (get) Token: 0x0600164B RID: 5707 RVA: 0x0007F988 File Offset: 0x0007DB88
	public StageAbilityManager.BonusRate BonusValueRate
	{
		get
		{
			return this.m_bonus_value_rate;
		}
	}

	// Token: 0x17000363 RID: 867
	// (get) Token: 0x0600164C RID: 5708 RVA: 0x0007F990 File Offset: 0x0007DB90
	public StageAbilityManager.BonusRate MainChaoBonusValueRate
	{
		get
		{
			return this.m_main_chao_bonus_value_rate;
		}
	}

	// Token: 0x17000364 RID: 868
	// (get) Token: 0x0600164D RID: 5709 RVA: 0x0007F998 File Offset: 0x0007DB98
	public StageAbilityManager.BonusRate SubChaoBonusValueRate
	{
		get
		{
			return this.m_sub_chao_bonus_value_rate;
		}
	}

	// Token: 0x17000365 RID: 869
	// (get) Token: 0x0600164E RID: 5710 RVA: 0x0007F9A0 File Offset: 0x0007DBA0
	public StageAbilityManager.BonusRate CountChaoBonusValueRate
	{
		get
		{
			return this.m_count_chao_bonus_value_rate;
		}
	}

	// Token: 0x17000366 RID: 870
	// (get) Token: 0x0600164F RID: 5711 RVA: 0x0007F9A8 File Offset: 0x0007DBA8
	public StageAbilityManager.BonusRate MainCharaBonusValueRate
	{
		get
		{
			return this.m_main_chara_bonus_value_rate;
		}
	}

	// Token: 0x17000367 RID: 871
	// (get) Token: 0x06001650 RID: 5712 RVA: 0x0007F9B0 File Offset: 0x0007DBB0
	public StageAbilityManager.BonusRate SubCharaBonusValueRate
	{
		get
		{
			return this.m_sub_chara_bonus_value_rate;
		}
	}

	// Token: 0x17000368 RID: 872
	// (get) Token: 0x06001651 RID: 5713 RVA: 0x0007F9B8 File Offset: 0x0007DBB8
	public float CampaignValueRate
	{
		get
		{
			return this.m_campaignBonusValue;
		}
	}

	// Token: 0x17000369 RID: 873
	// (get) Token: 0x06001652 RID: 5714 RVA: 0x0007F9C0 File Offset: 0x0007DBC0
	public StageAbilityManager.BonusRate MileageBonusScoreRate
	{
		get
		{
			return this.m_mileage_bonus_score_rate;
		}
	}

	// Token: 0x1700036A RID: 874
	// (get) Token: 0x06001653 RID: 5715 RVA: 0x0007F9C8 File Offset: 0x0007DBC8
	public float SpecialCrystalRate
	{
		get
		{
			return this.m_bonus_value_rate.sp_crystal;
		}
	}

	// Token: 0x1700036B RID: 875
	// (get) Token: 0x06001654 RID: 5716 RVA: 0x0007F9D8 File Offset: 0x0007DBD8
	public float RadBossRingRate
	{
		get
		{
			return this.m_bonus_value_rate.raid_boss_ring;
		}
	}

	// Token: 0x1700036C RID: 876
	// (get) Token: 0x06001655 RID: 5717 RVA: 0x0007F9E8 File Offset: 0x0007DBE8
	public float[] CharaAbility
	{
		get
		{
			this.CheckPlayerInformation();
			if (!(this.m_playerInformation != null))
			{
				return this.m_mainCharaAbilityValue;
			}
			if (this.m_playerInformation.PlayingCharaType == PlayingCharacterType.MAIN)
			{
				return this.m_mainCharaAbilityValue;
			}
			return this.m_subCharaAbilityValue;
		}
	}

	// Token: 0x1700036D RID: 877
	// (get) Token: 0x06001656 RID: 5718 RVA: 0x0007FA30 File Offset: 0x0007DC30
	// (set) Token: 0x06001657 RID: 5719 RVA: 0x0007FA38 File Offset: 0x0007DC38
	public bool[] BoostItemValidFlag
	{
		get
		{
			return this.m_boostItemValidFlag;
		}
		set
		{
			this.m_boostItemValidFlag = value;
		}
	}

	// Token: 0x06001658 RID: 5720 RVA: 0x0007FA44 File Offset: 0x0007DC44
	public void RequestPlayChaoEffect(ChaoAbility ability)
	{
		this.PlayChaoEffect(ability);
	}

	// Token: 0x06001659 RID: 5721 RVA: 0x0007FA50 File Offset: 0x0007DC50
	public void RequestPlayChaoEffect(ChaoAbility ability, ChaoType chaoType)
	{
		this.PlayChaoEffect(ability, chaoType);
	}

	// Token: 0x0600165A RID: 5722 RVA: 0x0007FA5C File Offset: 0x0007DC5C
	public void RequestPlayChaoEffect(ChaoAbility[] abilities)
	{
		foreach (ChaoAbility ability in abilities)
		{
			this.PlayChaoEffect(ability);
		}
	}

	// Token: 0x0600165B RID: 5723 RVA: 0x0007FA8C File Offset: 0x0007DC8C
	public void RequestPlayChaoEffect(ChaoAbility[] abilities, ChaoType chaoType)
	{
		foreach (ChaoAbility ability in abilities)
		{
			this.PlayChaoEffect(ability, chaoType);
		}
	}

	// Token: 0x0600165C RID: 5724 RVA: 0x0007FABC File Offset: 0x0007DCBC
	public void RequestStopChaoEffect(ChaoAbility ability)
	{
		this.StopChaoEffect(ability);
	}

	// Token: 0x0600165D RID: 5725 RVA: 0x0007FAC8 File Offset: 0x0007DCC8
	public void RequestStopChaoEffect(ChaoAbility[] abilities)
	{
		foreach (ChaoAbility ability in abilities)
		{
			this.StopChaoEffect(ability);
		}
	}

	// Token: 0x0600165E RID: 5726 RVA: 0x0007FAF8 File Offset: 0x0007DCF8
	public bool HasChaoAbility(ChaoAbility ability)
	{
		return ability != ChaoAbility.UNKNOWN && ability < ChaoAbility.NUM && (this.HasChaoAbility(this.m_mainChaoInfo, ability) || this.HasChaoAbility(this.m_subChaoInfo, ability));
	}

	// Token: 0x0600165F RID: 5727 RVA: 0x0007FB30 File Offset: 0x0007DD30
	public bool HasChaoAbility(ChaoAbility ability, ChaoType type)
	{
		if (ability != ChaoAbility.UNKNOWN && ability < ChaoAbility.NUM)
		{
			if (type == ChaoType.MAIN)
			{
				return this.HasChaoAbility(this.m_mainChaoInfo, ability);
			}
			if (type == ChaoType.SUB)
			{
				return this.HasChaoAbility(this.m_subChaoInfo, ability);
			}
		}
		return false;
	}

	// Token: 0x06001660 RID: 5728 RVA: 0x0007FB6C File Offset: 0x0007DD6C
	private bool HasChaoAbility(StageAbilityManager.ChaoAbilityInfo info, ChaoAbility ability)
	{
		foreach (StageAbilityManager.ChaoAbilityInfo.AbilityData abilityData in info.abilityDatas)
		{
			if (abilityData.ability == ability)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001661 RID: 5729 RVA: 0x0007FBE4 File Offset: 0x0007DDE4
	public int GetChaoAbliltyValue(ChaoAbility ability, int src_value)
	{
		if (ability < ChaoAbility.NUM)
		{
			return (int)this.CalcPlusAbliltyBonusValue(ability, (float)src_value);
		}
		return 0;
	}

	// Token: 0x06001662 RID: 5730 RVA: 0x0007FBFC File Offset: 0x0007DDFC
	public float GetChaoAbliltyValue(ChaoAbility ability, float src_value)
	{
		if (ability < ChaoAbility.NUM)
		{
			return this.CalcPlusAbliltyBonusValue(ability, src_value);
		}
		return 0f;
	}

	// Token: 0x06001663 RID: 5731 RVA: 0x0007FC14 File Offset: 0x0007DE14
	public float GetChaoAbilityValue(ChaoAbility ability)
	{
		float chaoAbilityValue = this.GetChaoAbilityValue(this.m_mainChaoInfo, ability);
		return chaoAbilityValue + this.GetChaoAbilityValue(this.m_subChaoInfo, ability);
	}

	// Token: 0x06001664 RID: 5732 RVA: 0x0007FC40 File Offset: 0x0007DE40
	public float GetChaoAbilityValue(ChaoAbility ability, ChaoType type)
	{
		if (type == ChaoType.MAIN)
		{
			return this.GetChaoAbilityValue(this.m_mainChaoInfo, ability);
		}
		if (type == ChaoType.SUB)
		{
			return this.GetChaoAbilityValue(this.m_subChaoInfo, ability);
		}
		return 0f;
	}

	// Token: 0x06001665 RID: 5733 RVA: 0x0007FC7C File Offset: 0x0007DE7C
	private float GetChaoAbilityValue(StageAbilityManager.ChaoAbilityInfo info, ChaoAbility ability)
	{
		foreach (StageAbilityManager.ChaoAbilityInfo.AbilityData abilityData in info.abilityDatas)
		{
			if (abilityData.ability == ability)
			{
				return (!this.IsSameAttribute(info.attribute)) ? abilityData.normal : abilityData.bonus;
			}
		}
		return 0f;
	}

	// Token: 0x06001666 RID: 5734 RVA: 0x0007FD18 File Offset: 0x0007DF18
	public float GetChaoAbilityExtraValue(ChaoAbility ability)
	{
		return this.GetChaoAbilityExtraValue(ability, ChaoType.MAIN) + this.GetChaoAbilityExtraValue(ability, ChaoType.SUB);
	}

	// Token: 0x06001667 RID: 5735 RVA: 0x0007FD38 File Offset: 0x0007DF38
	public float GetChaoAbilityExtraValue(ChaoAbility ability, ChaoType type)
	{
		StageAbilityManager.ChaoAbilityInfo chaoAbilityInfo = null;
		if (type == ChaoType.MAIN)
		{
			chaoAbilityInfo = this.m_mainChaoInfo;
		}
		else if (type == ChaoType.SUB)
		{
			chaoAbilityInfo = this.m_subChaoInfo;
		}
		if (chaoAbilityInfo != null)
		{
			foreach (StageAbilityManager.ChaoAbilityInfo.AbilityData abilityData in chaoAbilityInfo.abilityDatas)
			{
				if (abilityData.ability == ability)
				{
					return abilityData.extra;
				}
			}
		}
		return 0f;
	}

	// Token: 0x06001668 RID: 5736 RVA: 0x0007FDE0 File Offset: 0x0007DFE0
	public float GetCharacterOverlapBonusValue(OverlapBonusType bonusType)
	{
		if (OverlapBonusType.SCORE <= bonusType && bonusType < OverlapBonusType.NUM)
		{
			return this.m_mainCharaOverlapBonus[(int)bonusType] + this.m_subCharaOverlapBonus[(int)bonusType];
		}
		return 0f;
	}

	// Token: 0x06001669 RID: 5737 RVA: 0x0007FE08 File Offset: 0x0007E008
	public float GetCharacterOverlapBonusValue(OverlapBonusType bonusType, bool mainChara)
	{
		if (OverlapBonusType.SCORE > bonusType || bonusType >= OverlapBonusType.NUM)
		{
			return 0f;
		}
		if (mainChara)
		{
			return this.m_mainCharaOverlapBonus[(int)bonusType];
		}
		return this.m_subCharaOverlapBonus[(int)bonusType];
	}

	// Token: 0x0600166A RID: 5738 RVA: 0x0007FE38 File Offset: 0x0007E038
	public int GetChaoAndTeamAbliltyScoreValue(List<ChaoAbility> abilityList, TeamAttributeBonusType bonusType, int src_value)
	{
		float num = 0f;
		int count = abilityList.Count;
		for (int i = 0; i < count; i++)
		{
			num += this.GetChaoAbilityValue(this.m_mainChaoInfo, abilityList[i]);
			num += this.GetChaoAbilityValue(this.m_subChaoInfo, abilityList[i]);
		}
		num += this.GetTeamAbilityBonusValue(bonusType);
		return (int)this.GetPlusPercentBonusValue(num, (float)src_value);
	}

	// Token: 0x0600166B RID: 5739 RVA: 0x0007FEA4 File Offset: 0x0007E0A4
	public int GetChaoAndEnemyScoreValue(List<ChaoAbility> abilityList, int src_value)
	{
		float num = 0f;
		int count = abilityList.Count;
		for (int i = 0; i < count; i++)
		{
			num += this.GetChaoAbilityValue(this.m_mainChaoInfo, abilityList[i]);
			num += this.GetChaoAbilityValue(this.m_subChaoInfo, abilityList[i]);
		}
		num += this.GetTeamAbilityBonusValue(TeamAttributeBonusType.ENEMY);
		num += this.GetCharacterOverlapBonusValue(OverlapBonusType.ENEMY);
		return (int)this.GetPlusPercentBonusValue(num, (float)src_value);
	}

	// Token: 0x0600166C RID: 5740 RVA: 0x0007FF1C File Offset: 0x0007E11C
	private float GetTeamAbilityBonusValue(TeamAttributeBonusType bonusType)
	{
		if (bonusType < TeamAttributeBonusType.NUM && bonusType != TeamAttributeBonusType.NONE)
		{
			return this.m_mainTeamAbilityBonusValue[(int)bonusType] + this.m_subTeamAbilityBonusValue[(int)bonusType];
		}
		return 0f;
	}

	// Token: 0x0600166D RID: 5741 RVA: 0x0007FF44 File Offset: 0x0007E144
	public long GetTeamAbliltyResultScore(long score, int coefficient)
	{
		float num = 1f;
		int num2 = 0;
		if (this.m_mainTeamAttributeCategory == TeamAttributeCategory.EASY_SPEED)
		{
			num2++;
		}
		if (this.m_subTeamAttributeCategory == TeamAttributeCategory.EASY_SPEED)
		{
			num2++;
		}
		if (num2 == 1)
		{
			num = 0.8f;
		}
		else if (num2 == 2)
		{
			num = 0.64f;
		}
		double num3 = (double)coefficient * (double)num;
		double num4 = (double)score * num3;
		long num5 = (long)num4;
		if ((double)num5 < num4)
		{
			num4 += 1.0;
		}
		return (long)num4;
	}

	// Token: 0x0600166E RID: 5742 RVA: 0x0007FFC0 File Offset: 0x0007E1C0
	public bool IsEasySpeed(PlayingCharacterType type)
	{
		if (type == PlayingCharacterType.MAIN)
		{
			if (this.m_mainTeamAttributeCategory == TeamAttributeCategory.EASY_SPEED)
			{
				return true;
			}
		}
		else if (type == PlayingCharacterType.SUB && this.m_subTeamAttributeCategory == TeamAttributeCategory.EASY_SPEED)
		{
			return true;
		}
		return false;
	}

	// Token: 0x0600166F RID: 5743 RVA: 0x0007FFF4 File Offset: 0x0007E1F4
	public float GetTeamAbliltyTimeScale(float timeScale)
	{
		float num = (this.m_mainTeamAbilityBonusValue[5] + this.m_subTeamAbilityBonusValue[5]) * 0.01f;
		return timeScale - num;
	}

	// Token: 0x06001670 RID: 5744 RVA: 0x0008001C File Offset: 0x0007E21C
	public float GetItemTimePlusAblityBonus(ItemType itemType)
	{
		return this.CalcItemPlusAbliltyBonusValue(itemType);
	}

	// Token: 0x06001671 RID: 5745 RVA: 0x00080028 File Offset: 0x0007E228
	public float GetChaoCountBonusValue()
	{
		return this.m_chaoCountBonus;
	}

	// Token: 0x06001672 RID: 5746 RVA: 0x00080030 File Offset: 0x0007E230
	public int GetChaoCount()
	{
		return this.m_chaoCount;
	}

	// Token: 0x06001673 RID: 5747 RVA: 0x00080038 File Offset: 0x0007E238
	public void RecalcAbilityVaue()
	{
		this.InitParam();
		this.CalcChaoCountBonus();
		this.SetCharacterAbility();
		this.SetCharacterOverlapBonus();
		this.SetTeamAbility();
		this.SetChaoAbility();
		this.SetChaoBonusValueRate();
		this.SetCharacterBonusValueRate();
		this.SetPampaignBonusValueRate();
	}

	// Token: 0x06001674 RID: 5748 RVA: 0x0008007C File Offset: 0x0007E27C
	public GameObject GetLostRingChao()
	{
		GameObject result = null;
		if (this.HasChaoAbility(ChaoAbility.RECOVERY_RING))
		{
			bool flag = true;
			bool flag2 = false;
			int num = (int)this.GetChaoAbilityValue(this.m_mainChaoInfo, ChaoAbility.RECOVERY_RING);
			int num2 = (int)this.GetChaoAbilityValue(this.m_subChaoInfo, ChaoAbility.RECOVERY_RING);
			if (this.m_getMainChaoRecoveryRingCount < num)
			{
				flag2 = true;
			}
			else if (this.m_getSubChaoRecoveryRingCount < num2)
			{
				flag2 = true;
				flag = false;
			}
			string b = (!flag) ? "SubChao" : "MainChao";
			if (flag2)
			{
				GameObject[] array = GameObject.FindGameObjectsWithTag("Chao");
				foreach (GameObject gameObject in array)
				{
					if (gameObject.name == b)
					{
						result = gameObject;
						break;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06001675 RID: 5749 RVA: 0x0008014C File Offset: 0x0007E34C
	public void SetLostRingCount(int ring)
	{
		bool flag = false;
		int num = 0;
		int num2 = (int)this.GetChaoAbilityValue(this.m_mainChaoInfo, ChaoAbility.RECOVERY_RING);
		int num3 = (int)this.GetChaoAbilityValue(this.m_subChaoInfo, ChaoAbility.RECOVERY_RING);
		if (this.m_getMainChaoRecoveryRingCount < num2)
		{
			int num4 = num2 - this.m_getMainChaoRecoveryRingCount;
			if (num4 < ring)
			{
				num = num4;
				ring -= num4;
			}
			else
			{
				num = ring;
				ring = 0;
			}
			this.m_getMainChaoRecoveryRingCount += num;
			flag = true;
		}
		if (ring > 0 && this.m_getSubChaoRecoveryRingCount < num3)
		{
			int num5 = num3 - this.m_getSubChaoRecoveryRingCount;
			if (num5 < ring)
			{
				num += num5;
				this.m_getSubChaoRecoveryRingCount += num5;
			}
			else
			{
				num += ring;
				this.m_getSubChaoRecoveryRingCount += ring;
			}
			flag = true;
		}
		if (flag)
		{
			MsgAddStockRing value = new MsgAddStockRing(num);
			GameObjectUtil.SendDelayedMessageFindGameObject("HudCockpit", "OnAddStockRing", value);
			if (StageScoreManager.Instance != null)
			{
				StageScoreManager.Instance.AddRecoveryRingCount(num);
			}
		}
	}

	// Token: 0x06001676 RID: 5750 RVA: 0x0008024C File Offset: 0x0007E44C
	private void Setup()
	{
		this.RecalcAbilityVaue();
		this.SetMileageBonusScoreRate();
	}

	// Token: 0x06001677 RID: 5751 RVA: 0x0008025C File Offset: 0x0007E45C
	protected void Awake()
	{
		base.tag = "Manager";
		this.SetInstance();
	}

	// Token: 0x06001678 RID: 5752 RVA: 0x00080270 File Offset: 0x0007E470
	private void Start()
	{
		this.InitParam();
		int childCount = base.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			if (child != null)
			{
				GameObject gameObject = child.gameObject;
				if (gameObject != null)
				{
					gameObject.SetActive(false);
				}
			}
		}
		for (int j = 0; j < 3; j++)
		{
			this.m_boostItemValidFlag[j] = false;
		}
	}

	// Token: 0x06001679 RID: 5753 RVA: 0x000802F4 File Offset: 0x0007E4F4
	private void OnDestroy()
	{
		if (StageAbilityManager.instance == this)
		{
			StageAbilityManager.instance = null;
		}
	}

	// Token: 0x0600167A RID: 5754 RVA: 0x0008030C File Offset: 0x0007E50C
	private void SetInstance()
	{
		if (StageAbilityManager.instance == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			StageAbilityManager.instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600167B RID: 5755 RVA: 0x00080340 File Offset: 0x0007E540
	private void InitParam()
	{
		for (int i = 0; i < 6; i++)
		{
			this.m_mainTeamAbilityBonusValue[i] = 0f;
			this.m_subTeamAbilityBonusValue[i] = 0f;
		}
		for (int j = 0; j < 10; j++)
		{
			this.m_mainCharaAbilityValue[j] = 0f;
			this.m_subCharaAbilityValue[j] = 0f;
		}
		for (int k = 0; k < 5; k++)
		{
			this.m_mainCharaOverlapBonus[k] = 0f;
			this.m_subCharaOverlapBonus[k] = 0f;
		}
		this.m_mainTeamAttributeCategory = TeamAttributeCategory.NONE;
		this.m_subTeamAttributeCategory = TeamAttributeCategory.NONE;
		this.m_playerInformation = null;
		this.m_mainChaoInfo.Init();
		this.m_subChaoInfo.Init();
		this.m_chaoCountBonus = 0f;
		this.m_chaoCount = 0;
		this.m_count_chao_bonus_value_rate.Reset();
		this.m_main_chao_bonus_value_rate.Reset();
		this.m_sub_chao_bonus_value_rate.Reset();
		this.m_main_chara_bonus_value_rate.Reset();
		this.m_sub_chara_bonus_value_rate.Reset();
		this.m_bonus_value_rate.Reset();
		this.m_campaignBonusValue = 0f;
		this.m_boostBonusValue = 0f;
		if (!this.m_initFlag)
		{
			this.m_mileage_bonus_score_rate.Reset();
		}
		this.m_getMainChaoRecoveryRingCount = 0;
		this.m_getSubChaoRecoveryRingCount = 0;
		this.m_initFlag = true;
	}

	// Token: 0x0600167C RID: 5756 RVA: 0x00080494 File Offset: 0x0007E694
	private void SetCharacterAbility()
	{
		SaveDataManager saveDataManager = SaveDataManager.Instance;
		if (saveDataManager != null)
		{
			CharaType mainChara = saveDataManager.PlayerData.MainChara;
			if (this.CheckCharaType(mainChara))
			{
				this.SetCharaAbilityValue(ref this.m_mainCharaAbilityValue, saveDataManager.CharaData.AbilityArray[(int)mainChara]);
			}
			if (this.m_boostItemValidFlag[2])
			{
				CharaType subChara = saveDataManager.PlayerData.SubChara;
				if (this.CheckCharaType(subChara))
				{
					this.SetCharaAbilityValue(ref this.m_subCharaAbilityValue, saveDataManager.CharaData.AbilityArray[(int)subChara]);
				}
			}
		}
	}

	// Token: 0x0600167D RID: 5757 RVA: 0x00080524 File Offset: 0x0007E724
	private void SetTeamAbility()
	{
		SaveDataManager saveDataManager = SaveDataManager.Instance;
		if (saveDataManager != null && CharacterDataNameInfo.Instance != null)
		{
			CharaType mainChara = saveDataManager.PlayerData.MainChara;
			if (this.CheckCharaType(mainChara))
			{
				this.SetTeamAbilityBonusValue(ref this.m_mainTeamAbilityBonusValue, ref this.m_mainTeamAttributeCategory, CharacterDataNameInfo.Instance.GetDataByID(mainChara));
			}
			if (this.m_boostItemValidFlag[2])
			{
				CharaType subChara = saveDataManager.PlayerData.SubChara;
				if (this.CheckCharaType(subChara))
				{
					this.SetTeamAbilityBonusValue(ref this.m_subTeamAbilityBonusValue, ref this.m_subTeamAttributeCategory, CharacterDataNameInfo.Instance.GetDataByID(subChara));
				}
			}
		}
	}

	// Token: 0x0600167E RID: 5758 RVA: 0x000805CC File Offset: 0x0007E7CC
	private void SetCharacterOverlapBonus()
	{
		if (ResourceManager.Instance != null)
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.ETC, "OverlapBonusTable");
			if (gameObject != null)
			{
				OverlapBonusTable component = gameObject.GetComponent<OverlapBonusTable>();
				if (component != null && SaveDataManager.Instance != null)
				{
					CharaType mainChara = SaveDataManager.Instance.PlayerData.MainChara;
					if (this.CheckCharaType(mainChara))
					{
						this.SetOverlapBonusValue(ref this.m_mainCharaOverlapBonus, mainChara, component);
					}
					if (this.m_boostItemValidFlag[2])
					{
						CharaType subChara = SaveDataManager.Instance.PlayerData.SubChara;
						if (this.CheckCharaType(subChara))
						{
							this.SetOverlapBonusValue(ref this.m_subCharaOverlapBonus, subChara, component);
						}
					}
				}
			}
		}
	}

	// Token: 0x0600167F RID: 5759 RVA: 0x0008068C File Offset: 0x0007E88C
	private void SetOverlapBonusValue(ref float[] overlapBonus, CharaType charaType, OverlapBonusTable overlapBonusTable)
	{
		if (ServerInterface.PlayerState != null && overlapBonusTable != null)
		{
			ServerCharacterState serverCharacterState = ServerInterface.PlayerState.CharacterState(charaType);
			if (serverCharacterState != null)
			{
				int star = serverCharacterState.star;
				if (overlapBonus.Length == 5)
				{
					for (int i = 0; i < 5; i++)
					{
						overlapBonus[i] = overlapBonusTable.GetStarBonusList(charaType, star, (OverlapBonusType)i);
					}
				}
			}
		}
	}

	// Token: 0x06001680 RID: 5760 RVA: 0x000806F4 File Offset: 0x0007E8F4
	private bool CheckCharaType(CharaType chara_type)
	{
		return CharaType.SONIC <= chara_type && chara_type < CharaType.NUM;
	}

	// Token: 0x06001681 RID: 5761 RVA: 0x00080708 File Offset: 0x0007E908
	private void SetCharaAbilityValue(ref float[] ability_value, CharaAbility ability)
	{
		if (ability != null)
		{
			ImportAbilityTable importAbilityTable = ImportAbilityTable.GetInstance();
			if (importAbilityTable != null)
			{
				for (int i = 0; i < 10; i++)
				{
					ability_value[i] = importAbilityTable.GetAbilityPotential((AbilityType)i, (int)ability.Ability[i]);
				}
			}
		}
	}

	// Token: 0x06001682 RID: 5762 RVA: 0x00080754 File Offset: 0x0007E954
	private void SetTeamAbilityBonusValue(ref float[] bonusValue, ref TeamAttributeCategory category, CharacterDataNameInfo.Info info)
	{
		for (int i = 0; i < 6; i++)
		{
			bonusValue[i] = 0f;
		}
		if (info != null)
		{
			category = info.m_teamAttributeCategory;
			if (info.m_mainAttributeBonus != TeamAttributeBonusType.NONE && info.m_mainAttributeBonus < TeamAttributeBonusType.NUM)
			{
				bonusValue[(int)info.m_mainAttributeBonus] = info.TeamAttributeValue;
			}
			if (info.m_subAttributeBonus != TeamAttributeBonusType.NONE && info.m_subAttributeBonus < TeamAttributeBonusType.NUM)
			{
				bonusValue[(int)info.m_subAttributeBonus] = info.TeamAttributeSubValue;
			}
		}
	}

	// Token: 0x06001683 RID: 5763 RVA: 0x000807D8 File Offset: 0x0007E9D8
	private void SetChaoAbility()
	{
		SaveDataManager saveDataManager = SaveDataManager.Instance;
		if (saveDataManager != null)
		{
			bool mainChaoFlag = true;
			this.SetChaoAbilityData(saveDataManager.PlayerData.MainChaoID, mainChaoFlag);
			bool mainChaoFlag2 = false;
			this.SetChaoAbilityData(saveDataManager.PlayerData.SubChaoID, mainChaoFlag2);
		}
	}

	// Token: 0x06001684 RID: 5764 RVA: 0x00080820 File Offset: 0x0007EA20
	private void SetChaoAbilityData(int chaoId, bool mainChaoFlag)
	{
		ChaoData chaoData = ChaoTable.GetChaoData(chaoId);
		if (chaoData != null)
		{
			int level = chaoData.level;
			if (level >= 0)
			{
				int abilityNum = chaoData.abilityNum;
				for (int i = 0; i < abilityNum; i++)
				{
					chaoData.currentAbility = i;
					bool flag = false;
					int eventId = chaoData.eventId;
					if (eventId > 0)
					{
						if (EventManager.IsVaildEvent(eventId) && EventManager.Instance != null)
						{
							ChaoAbility chaoAbility = chaoData.chaoAbility;
							if (chaoAbility != ChaoAbility.SPECIAL_CRYSTAL_COUNT)
							{
								if (chaoAbility != ChaoAbility.RAID_BOSS_RING_COUNT)
								{
									if (chaoAbility == ChaoAbility.COMBO_ALL_SPECIAL_CRYSTAL || chaoAbility == ChaoAbility.SPECIAL_CRYSTAL_RATE)
									{
										goto IL_97;
									}
									if (chaoAbility != ChaoAbility.AGGRESSIVITY_UP_FOR_RAID_BOSS)
									{
										goto IL_C5;
									}
								}
								if (EventManager.Instance.IsRaidBossStage())
								{
									flag = true;
								}
								goto IL_C5;
							}
							IL_97:
							if (EventManager.Instance.IsSpecialStage())
							{
								flag = true;
							}
						}
						IL_C5:;
					}
					else
					{
						flag = true;
						if (EventManager.Instance != null && EventManager.Instance.IsRaidBossStage())
						{
							ChaoAbility chaoAbility = chaoData.chaoAbility;
							if (chaoAbility == ChaoAbility.RING_COUNT || chaoAbility == ChaoAbility.COMBO_SUPER_RING || chaoAbility == ChaoAbility.BOSS_SUPER_RING_RATE || chaoAbility == ChaoAbility.RECOVERY_RING)
							{
								flag = false;
							}
						}
					}
					if (flag)
					{
						float num = chaoData.bonusAbilityValue[level];
						float num2 = chaoData.abilityValue[level];
						float extraValue = chaoData.extraValue;
						if (chaoData.chaoAbility == ChaoAbility.RECOVERY_RING)
						{
							num = Mathf.Ceil(num);
							num2 = Mathf.Ceil(num2);
						}
						if (mainChaoFlag)
						{
							this.m_mainChaoInfo.AddAbility(chaoData.chaoAbility, num, num2, extraValue);
							this.m_mainChaoInfo.attribute = chaoData.charaAtribute;
						}
						else
						{
							this.m_subChaoInfo.AddAbility(chaoData.chaoAbility, num, num2, extraValue);
							this.m_subChaoInfo.attribute = chaoData.charaAtribute;
						}
					}
				}
				chaoData.currentAbility = 0;
			}
		}
	}

	// Token: 0x06001685 RID: 5765 RVA: 0x00080A00 File Offset: 0x0007EC00
	private void SetBonusVale(ref StageAbilityManager.BonusRate bonusRate, StageAbilityManager.ChaoAbilityInfo info)
	{
		foreach (StageAbilityManager.ChaoAbilityInfo.AbilityData abilityData in info.abilityDatas)
		{
			float num = 0f;
			switch (abilityData.ability)
			{
			case ChaoAbility.ALL_BONUS_COUNT:
			case ChaoAbility.SCORE_COUNT:
			case ChaoAbility.RING_COUNT:
			case ChaoAbility.RED_RING_COUNT:
			case ChaoAbility.ANIMAL_COUNT:
			case ChaoAbility.RUNNIGN_DISTANCE:
			case ChaoAbility.SPECIAL_CRYSTAL_COUNT:
			case ChaoAbility.RAID_BOSS_RING_COUNT:
				num = ((!this.IsSameAttributeFromSaveData(info.attribute, true)) ? abilityData.normal : abilityData.bonus);
				num *= 0.01f;
				break;
			}
			switch (abilityData.ability)
			{
			case ChaoAbility.ALL_BONUS_COUNT:
				bonusRate.score += num;
				bonusRate.ring += num;
				bonusRate.animal += num;
				bonusRate.distance += num;
				break;
			case ChaoAbility.SCORE_COUNT:
				bonusRate.score += num;
				break;
			case ChaoAbility.RING_COUNT:
				bonusRate.ring += num;
				break;
			case ChaoAbility.ANIMAL_COUNT:
				bonusRate.animal += num;
				break;
			case ChaoAbility.RUNNIGN_DISTANCE:
				bonusRate.distance += num;
				break;
			case ChaoAbility.SPECIAL_CRYSTAL_COUNT:
				bonusRate.sp_crystal += num;
				break;
			case ChaoAbility.RAID_BOSS_RING_COUNT:
				bonusRate.raid_boss_ring += num;
				break;
			}
		}
	}

	// Token: 0x06001686 RID: 5766 RVA: 0x00080BB0 File Offset: 0x0007EDB0
	private bool IsSameAttribute(CharacterAttribute chaoAtribute)
	{
		this.CheckPlayerInformation();
		return this.m_playerInformation != null && this.m_playerInformation.PlayerAttribute == chaoAtribute;
	}

	// Token: 0x06001687 RID: 5767 RVA: 0x00080BDC File Offset: 0x0007EDDC
	private bool IsSameAttributeFromSaveData(CharacterAttribute attribute, bool subCharaCompare)
	{
		CharaType charaType = CharaType.UNKNOWN;
		CharaType charaType2 = CharaType.UNKNOWN;
		SaveDataManager saveDataManager = SaveDataManager.Instance;
		if (saveDataManager != null)
		{
			charaType = saveDataManager.PlayerData.MainChara;
			if (this.m_boostItemValidFlag[2])
			{
				charaType2 = saveDataManager.PlayerData.SubChara;
			}
		}
		return this.IsSameCharaAbility(charaType, attribute) || (subCharaCompare && charaType2 != CharaType.UNKNOWN && this.IsSameCharaAbility(charaType2, attribute));
	}

	// Token: 0x06001688 RID: 5768 RVA: 0x00080C4C File Offset: 0x0007EE4C
	private bool IsSameCharaAbility(CharaType charaType, CharacterAttribute chaoAttribute)
	{
		CharacterAttribute characterAttribute = CharaTypeUtil.GetCharacterAttribute(charaType);
		return characterAttribute == chaoAttribute && chaoAttribute != CharacterAttribute.UNKNOWN;
	}

	// Token: 0x06001689 RID: 5769 RVA: 0x00080C74 File Offset: 0x0007EE74
	private void SetChaoBonusValueRate()
	{
		this.m_count_chao_bonus_value_rate.score = this.m_chaoCountBonus * 0.01f;
		this.SetBonusVale(ref this.m_main_chao_bonus_value_rate, this.m_mainChaoInfo);
		this.SetBonusVale(ref this.m_sub_chao_bonus_value_rate, this.m_subChaoInfo);
		this.m_bonus_value_rate.score = this.m_bonus_value_rate.score + this.m_count_chao_bonus_value_rate.score;
		this.m_bonus_value_rate.score = this.m_bonus_value_rate.score + (this.m_main_chao_bonus_value_rate.score + this.m_sub_chao_bonus_value_rate.score);
		this.m_bonus_value_rate.animal = this.m_bonus_value_rate.animal + (this.m_main_chao_bonus_value_rate.animal + this.m_sub_chao_bonus_value_rate.animal);
		this.m_bonus_value_rate.ring = this.m_bonus_value_rate.ring + (this.m_main_chao_bonus_value_rate.ring + this.m_sub_chao_bonus_value_rate.ring);
		this.m_bonus_value_rate.red_ring = 0f;
		this.m_bonus_value_rate.distance = this.m_bonus_value_rate.distance + (this.m_main_chao_bonus_value_rate.distance + this.m_sub_chao_bonus_value_rate.distance);
		this.m_bonus_value_rate.sp_crystal = this.m_bonus_value_rate.sp_crystal + (this.m_main_chao_bonus_value_rate.sp_crystal + this.m_sub_chao_bonus_value_rate.sp_crystal);
		this.m_bonus_value_rate.raid_boss_ring = this.m_bonus_value_rate.raid_boss_ring + (this.m_main_chao_bonus_value_rate.raid_boss_ring + this.m_sub_chao_bonus_value_rate.raid_boss_ring);
	}

	// Token: 0x0600168A RID: 5770 RVA: 0x00080DE0 File Offset: 0x0007EFE0
	private void SetCharacterBonusValueRate()
	{
		this.m_main_chara_bonus_value_rate.score = (this.m_mainTeamAbilityBonusValue[1] + this.m_mainCharaOverlapBonus[0]) * 0.01f;
		this.m_sub_chara_bonus_value_rate.score = (this.m_subTeamAbilityBonusValue[1] + this.m_subCharaOverlapBonus[0]) * 0.01f;
		this.m_main_chara_bonus_value_rate.animal = (this.m_mainCharaAbilityValue[6] + this.m_mainTeamAbilityBonusValue[3] + this.m_mainCharaOverlapBonus[2]) * 0.01f;
		this.m_sub_chara_bonus_value_rate.animal = (this.m_subCharaAbilityValue[6] + this.m_subTeamAbilityBonusValue[3] + this.m_subCharaOverlapBonus[2]) * 0.01f;
		this.m_main_chara_bonus_value_rate.ring = (this.m_mainCharaAbilityValue[3] + this.m_mainTeamAbilityBonusValue[2] + this.m_mainCharaOverlapBonus[1]) * 0.01f;
		this.m_sub_chara_bonus_value_rate.ring = (this.m_subCharaAbilityValue[3] + this.m_subTeamAbilityBonusValue[2] + this.m_subCharaOverlapBonus[1]) * 0.01f;
		this.m_main_chara_bonus_value_rate.distance = (this.m_mainCharaAbilityValue[4] + this.m_mainTeamAbilityBonusValue[0] + this.m_mainCharaOverlapBonus[3]) * 0.01f;
		this.m_sub_chara_bonus_value_rate.distance = (this.m_subCharaAbilityValue[4] + this.m_subTeamAbilityBonusValue[0] + this.m_subCharaOverlapBonus[3]) * 0.01f;
		if (this.m_boostItemValidFlag[0])
		{
			this.m_boostBonusValue = 1f;
			this.m_main_chara_bonus_value_rate.score = this.m_main_chara_bonus_value_rate.score + this.m_boostBonusValue;
			if (this.m_boostItemValidFlag[2])
			{
				SaveDataManager saveDataManager = SaveDataManager.Instance;
				if (saveDataManager != null && this.CheckCharaType(saveDataManager.PlayerData.SubChara))
				{
					this.m_sub_chara_bonus_value_rate.score = this.m_sub_chara_bonus_value_rate.score + this.m_boostBonusValue;
				}
			}
		}
		this.m_bonus_value_rate.distance = this.m_bonus_value_rate.distance + (this.m_main_chara_bonus_value_rate.distance + this.m_sub_chara_bonus_value_rate.distance);
		this.m_bonus_value_rate.score = this.m_bonus_value_rate.score + (this.m_main_chara_bonus_value_rate.score + this.m_sub_chara_bonus_value_rate.score);
		this.m_bonus_value_rate.animal = this.m_bonus_value_rate.animal + (this.m_main_chara_bonus_value_rate.animal + this.m_sub_chara_bonus_value_rate.animal);
		this.m_bonus_value_rate.ring = this.m_bonus_value_rate.ring + (this.m_main_chara_bonus_value_rate.ring + this.m_sub_chara_bonus_value_rate.ring);
	}

	// Token: 0x0600168B RID: 5771 RVA: 0x00081054 File Offset: 0x0007F254
	private void SetPampaignBonusValueRate()
	{
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			ServerCampaignData campaignInSession = ServerInterface.CampaignState.GetCampaignInSession(Constants.Campaign.emType.BankedRingBonus);
			if (campaignInSession != null)
			{
				float fContent = campaignInSession.fContent;
				this.m_campaignBonusValue = fContent;
			}
			else
			{
				this.m_campaignBonusValue = 0f;
			}
		}
	}

	// Token: 0x0600168C RID: 5772 RVA: 0x000810A4 File Offset: 0x0007F2A4
	private void SetMileageBonusScoreRate()
	{
		GameObject gameObject = GameObject.Find("MileageBonusInfo");
		if (gameObject != null)
		{
			MileageBonusInfo component = gameObject.GetComponent<MileageBonusInfo>();
			if (component != null)
			{
				this.m_mileage_bonus_score_rate.Reset();
				float num = component.BonusData.value - 1f;
				switch (component.BonusData.type)
				{
				case MileageBonus.SCORE:
					this.m_mileage_bonus_score_rate.score = num;
					break;
				case MileageBonus.ANIMAL:
					this.m_mileage_bonus_score_rate.animal = num;
					break;
				case MileageBonus.RING:
					this.m_mileage_bonus_score_rate.ring = num;
					break;
				case MileageBonus.DISTANCE:
					this.m_mileage_bonus_score_rate.distance = num;
					break;
				case MileageBonus.FINAL_SCORE:
					this.m_mileage_bonus_score_rate.final_score = num;
					break;
				}
			}
			UnityEngine.Object.Destroy(gameObject);
		}
		else
		{
			this.m_mileage_bonus_score_rate.Reset();
		}
	}

	// Token: 0x0600168D RID: 5773 RVA: 0x000811A4 File Offset: 0x0007F3A4
	private float GetPlusPercentBonusValue(float percent, float src_value)
	{
		return Mathf.Ceil(src_value + src_value * percent * 0.01f);
	}

	// Token: 0x0600168E RID: 5774 RVA: 0x000811B8 File Offset: 0x0007F3B8
	private float GetPlusPercentBonusTime(float percent, float src_value)
	{
		return src_value + src_value * percent * 0.01f;
	}

	// Token: 0x0600168F RID: 5775 RVA: 0x000811C8 File Offset: 0x0007F3C8
	private float CalcPlusAbliltyBonusValue(ChaoAbility ability, float src_value)
	{
		float num = src_value;
		switch (ability)
		{
		case ChaoAbility.COLOR_POWER_SCORE:
			num = this.GetPlusPercentBonusValue(this.GetChaoAbilityValue(ability), src_value);
			break;
		case ChaoAbility.ASTEROID_SCORE:
			num = this.GetPlusPercentBonusValue(this.GetChaoAbilityValue(ability), src_value);
			break;
		case ChaoAbility.DRILL_SCORE:
			num = this.GetPlusPercentBonusValue(this.GetChaoAbilityValue(ability), src_value);
			break;
		case ChaoAbility.LASER_SCORE:
			num = this.GetPlusPercentBonusValue(this.GetChaoAbilityValue(ability), src_value);
			break;
		case ChaoAbility.COLOR_POWER_TIME:
			num = this.GetPlusPercentBonusTime(this.GetChaoAbilityValue(ability), src_value);
			break;
		case ChaoAbility.ITEM_TIME:
			num = this.GetPlusPercentBonusTime(this.GetChaoAbilityValue(ability), src_value);
			break;
		case ChaoAbility.COMBO_TIME:
			num = this.GetPlusPercentBonusTime(this.GetChaoAbilityValue(ability), src_value);
			break;
		case ChaoAbility.TRAMPOLINE_TIME:
			num = this.GetPlusPercentBonusTime(this.GetChaoAbilityValue(ability), src_value);
			break;
		case ChaoAbility.MAGNET_TIME:
			num = this.GetPlusPercentBonusTime(this.GetChaoAbilityValue(ability), src_value);
			break;
		case ChaoAbility.ASTEROID_TIME:
			num = this.GetPlusPercentBonusTime(this.GetChaoAbilityValue(ability), src_value);
			break;
		case ChaoAbility.DRILL_TIME:
			num = this.GetPlusPercentBonusTime(this.GetChaoAbilityValue(ability), src_value);
			break;
		case ChaoAbility.LASER_TIME:
			num = this.GetPlusPercentBonusTime(this.GetChaoAbilityValue(ability), src_value);
			break;
		case ChaoAbility.BOSS_STAGE_TIME:
			num = this.GetChaoAbilityValue(ability) + src_value;
			break;
		case ChaoAbility.COMBO_RECEPTION_TIME:
			num = this.GetPlusPercentBonusValue(this.GetChaoAbilityValue(ability), src_value);
			break;
		case ChaoAbility.BOSS_RED_RING_RATE:
			num = this.GetPlusPercentBonusValue(this.GetChaoAbilityValue(ability), src_value);
			num = Mathf.Clamp(num, 0f, 100f);
			break;
		case ChaoAbility.BOSS_SUPER_RING_RATE:
			num = this.GetPlusPercentBonusValue(this.GetChaoAbilityValue(ability), src_value);
			num = Mathf.Clamp(num, 0f, 100f);
			break;
		case ChaoAbility.RARE_ENEMY_UP:
			num = this.GetPlusPercentBonusValue(this.GetChaoAbilityValue(ability), src_value);
			num = Mathf.Clamp(num, 0f, 100f);
			break;
		case ChaoAbility.SPECIAL_CRYSTAL_RATE:
			num = this.GetPlusPercentBonusValue(this.GetChaoAbilityValue(ability), src_value);
			num = Mathf.Clamp(num, 0f, 100f);
			break;
		case ChaoAbility.LAST_CHANCE:
			num = this.GetChaoAbilityValue(ChaoAbility.LAST_CHANCE);
			break;
		case ChaoAbility.RECOVERY_RING:
			break;
		default:
			switch (ability)
			{
			}
			break;
		case ChaoAbility.MAP_BOSS_DAMAGE:
			num = src_value - this.GetChaoAbilityValue(ChaoAbility.MAP_BOSS_DAMAGE);
			if (num < 1f)
			{
				num = 1f;
			}
			break;
		case ChaoAbility.ENEMY_SCORE:
			num = this.GetPlusPercentBonusValue(this.GetChaoAbilityValue(ability), src_value);
			break;
		case ChaoAbility.MAGNET_RANGE:
			num = this.GetPlusPercentBonusValue(this.GetChaoAbilityValue(ability), src_value);
			break;
		case ChaoAbility.AGGRESSIVITY_UP_FOR_RAID_BOSS:
			num = this.GetPlusPercentBonusValue(this.GetChaoAbilityValue(ability), src_value);
			break;
		case ChaoAbility.TRANSFER_DOUBLE_RING:
			num = this.GetPlusPercentBonusValue(this.GetChaoAbilityValue(ability), src_value);
			break;
		}
		return num;
	}

	// Token: 0x06001690 RID: 5776 RVA: 0x00081500 File Offset: 0x0007F700
	private float CalcItemPlusAbliltyBonusValue(ItemType itemType)
	{
		float num = 0f;
		switch (itemType)
		{
		case ItemType.INVINCIBLE:
		{
			num = this.CharaAbility[9];
			float percent = this.GetChaoAbilityValue(ChaoAbility.ITEM_TIME);
			num = this.GetPlusPercentBonusTime(percent, num);
			break;
		}
		case ItemType.MAGNET:
		{
			num = this.CharaAbility[8];
			float percent = this.GetChaoAbilityValue(ChaoAbility.MAGNET_TIME) + this.GetChaoAbilityValue(ChaoAbility.ITEM_TIME);
			num = this.GetPlusPercentBonusTime(percent, num);
			break;
		}
		case ItemType.TRAMPOLINE:
		{
			num = this.CharaAbility[5];
			float percent = this.GetChaoAbilityValue(ChaoAbility.TRAMPOLINE_TIME) + this.GetChaoAbilityValue(ChaoAbility.ITEM_TIME);
			num = this.GetPlusPercentBonusTime(percent, num);
			break;
		}
		case ItemType.COMBO:
		{
			num = this.CharaAbility[7];
			float percent = this.GetChaoAbilityValue(ChaoAbility.COMBO_TIME) + this.GetChaoAbilityValue(ChaoAbility.ITEM_TIME);
			num = this.GetPlusPercentBonusTime(percent, num);
			break;
		}
		case ItemType.LASER:
		{
			num = this.CharaAbility[0];
			float percent = this.GetChaoAbilityValue(ChaoAbility.LASER_TIME) + this.GetChaoAbilityValue(ChaoAbility.COLOR_POWER_TIME);
			num = this.GetPlusPercentBonusTime(percent, num);
			break;
		}
		case ItemType.DRILL:
		{
			num = this.CharaAbility[1];
			float percent = this.GetChaoAbilityValue(ChaoAbility.DRILL_TIME) + this.GetChaoAbilityValue(ChaoAbility.COLOR_POWER_TIME);
			num = this.GetPlusPercentBonusTime(percent, num);
			break;
		}
		case ItemType.ASTEROID:
		{
			num = this.CharaAbility[2];
			float percent = this.GetChaoAbilityValue(ChaoAbility.ASTEROID_TIME) + this.GetChaoAbilityValue(ChaoAbility.COLOR_POWER_TIME);
			num = this.GetPlusPercentBonusTime(percent, num);
			break;
		}
		}
		return num;
	}

	// Token: 0x06001691 RID: 5777 RVA: 0x00081660 File Offset: 0x0007F860
	private void CalcChaoCountBonus()
	{
		ServerPlayerState playerState = ServerInterface.PlayerState;
		if (playerState != null)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0.1f;
			float num5 = 0.3f;
			float num6 = 1f;
			List<ServerChaoState> chaoStates = playerState.ChaoStates;
			foreach (ServerChaoState serverChaoState in chaoStates)
			{
				if (serverChaoState != null && serverChaoState.Status > ServerChaoState.ChaoStatus.NotOwned)
				{
					switch (serverChaoState.Rarity)
					{
					case 0:
						num += num4 * (float)serverChaoState.NumAcquired;
						break;
					case 1:
						num2 += num5 * (float)serverChaoState.NumAcquired;
						break;
					case 2:
						num3 += num6 * (float)serverChaoState.NumAcquired;
						break;
					}
					this.m_chaoCount += serverChaoState.NumAcquired;
				}
			}
			float value = num + num2 + num3;
			this.m_chaoCountBonus = Mathf.Clamp(value, 0f, 200f);
		}
	}

	// Token: 0x06001692 RID: 5778 RVA: 0x0008179C File Offset: 0x0007F99C
	private ChaoEffect.TargetType GetChaoEffectTagetType(ChaoAbility ability)
	{
		ChaoEffect.TargetType targetType = ChaoEffect.TargetType.Unknown;
		if (ability != ChaoAbility.UNKNOWN && ability < ChaoAbility.NUM)
		{
			if (this.HasChaoAbility(this.m_mainChaoInfo, ability))
			{
				targetType = ChaoEffect.TargetType.MainChao;
			}
			if (this.HasChaoAbility(this.m_subChaoInfo, ability))
			{
				targetType = ((targetType != ChaoEffect.TargetType.MainChao) ? ChaoEffect.TargetType.SubChao : ChaoEffect.TargetType.BothChao);
				if (ability == ChaoAbility.RECOVERY_RING && targetType == ChaoEffect.TargetType.BothChao)
				{
					targetType = ChaoEffect.TargetType.MainChao;
				}
			}
		}
		return targetType;
	}

	// Token: 0x06001693 RID: 5779 RVA: 0x00081800 File Offset: 0x0007FA00
	private void PlayChaoEffect(ChaoAbility ability)
	{
		ChaoEffect.TargetType chaoEffectTagetType = this.GetChaoEffectTagetType(ability);
		if (chaoEffectTagetType != ChaoEffect.TargetType.Unknown)
		{
			ChaoEffect chaoEffect = ChaoEffect.Instance;
			if (chaoEffect != null)
			{
				chaoEffect.RequestPlayChaoEffect(chaoEffectTagetType, ability);
			}
		}
	}

	// Token: 0x06001694 RID: 5780 RVA: 0x00081838 File Offset: 0x0007FA38
	private void PlayChaoEffect(ChaoAbility ability, ChaoType chaoType)
	{
		ChaoEffect.TargetType chaoEffectTagetType = this.GetChaoEffectTagetType(ability);
		if (chaoType != ChaoType.MAIN)
		{
			if (chaoType == ChaoType.SUB)
			{
				if (chaoEffectTagetType == ChaoEffect.TargetType.SubChao || chaoEffectTagetType == ChaoEffect.TargetType.BothChao)
				{
					ChaoEffect chaoEffect = ChaoEffect.Instance;
					if (chaoEffect != null)
					{
						chaoEffect.RequestPlayChaoEffect(ChaoEffect.TargetType.SubChao, ability);
					}
				}
			}
		}
		else if (chaoEffectTagetType == ChaoEffect.TargetType.MainChao || chaoEffectTagetType == ChaoEffect.TargetType.BothChao)
		{
			ChaoEffect chaoEffect2 = ChaoEffect.Instance;
			if (chaoEffect2 != null)
			{
				chaoEffect2.RequestPlayChaoEffect(ChaoEffect.TargetType.MainChao, ability);
			}
		}
	}

	// Token: 0x06001695 RID: 5781 RVA: 0x000818BC File Offset: 0x0007FABC
	private void StopChaoEffect(ChaoAbility ability)
	{
		ChaoEffect.TargetType chaoEffectTagetType = this.GetChaoEffectTagetType(ability);
		if (chaoEffectTagetType != ChaoEffect.TargetType.Unknown)
		{
			ChaoEffect chaoEffect = ChaoEffect.Instance;
			if (chaoEffect != null)
			{
				chaoEffect.RequestStopChaoEffect(chaoEffectTagetType, ability);
			}
		}
	}

	// Token: 0x06001696 RID: 5782 RVA: 0x000818F4 File Offset: 0x0007FAF4
	private void CheckPlayerInformation()
	{
		if (this.m_playerInformation == null)
		{
			this.m_playerInformation = GameObjectUtil.FindGameObjectComponentWithTag<PlayerInformation>("StageManager", "PlayerInformation");
		}
	}

	// Token: 0x06001697 RID: 5783 RVA: 0x00081928 File Offset: 0x0007FB28
	public static void LoadAbilityDataTable(ResourceSceneLoader loaderComponent)
	{
		if (loaderComponent != null)
		{
			if (!StageAbilityManager.IsExistDataObject(StageAbilityManager.CHAODATA_NAME))
			{
				loaderComponent.AddLoadAndResourceManager(StageAbilityManager.m_loadInfo[0]);
			}
			if (!StageAbilityManager.IsExistDataObject(StageAbilityManager.CHARADATA_NAME))
			{
				loaderComponent.AddLoadAndResourceManager(StageAbilityManager.m_loadInfo[1]);
			}
		}
	}

	// Token: 0x06001698 RID: 5784 RVA: 0x00081984 File Offset: 0x0007FB84
	public static void SetupAbilityDataTable()
	{
		StageAbilityManager stageAbilityManager = StageAbilityManager.Instance;
		if (stageAbilityManager != null)
		{
			stageAbilityManager.Setup();
		}
	}

	// Token: 0x06001699 RID: 5785 RVA: 0x000819AC File Offset: 0x0007FBAC
	private static bool IsExistDataObject(string name)
	{
		GameObject x = GameObject.Find(name);
		return x != null;
	}

	// Token: 0x040013DA RID: 5082
	private const float CHARA_ABILITY_BONUS_VALUE = 20f;

	// Token: 0x040013DB RID: 5083
	[Header("debugFlag にチェックを入れると、指定した値で設定できます")]
	[SerializeField]
	private bool m_debugFlag;

	// Token: 0x040013DC RID: 5084
	[SerializeField]
	private float m_debugCampaignBonusValue;

	// Token: 0x040013DD RID: 5085
	private bool[] m_boostItemValidFlag = new bool[3];

	// Token: 0x040013DE RID: 5086
	private StageAbilityManager.ChaoAbilityInfo m_mainChaoInfo = new StageAbilityManager.ChaoAbilityInfo();

	// Token: 0x040013DF RID: 5087
	private StageAbilityManager.ChaoAbilityInfo m_subChaoInfo = new StageAbilityManager.ChaoAbilityInfo();

	// Token: 0x040013E0 RID: 5088
	private float[] m_mainCharaAbilityValue = new float[10];

	// Token: 0x040013E1 RID: 5089
	private float[] m_subCharaAbilityValue = new float[10];

	// Token: 0x040013E2 RID: 5090
	private float[] m_mainCharaOverlapBonus = new float[5];

	// Token: 0x040013E3 RID: 5091
	private float[] m_subCharaOverlapBonus = new float[5];

	// Token: 0x040013E4 RID: 5092
	private float[] m_mainTeamAbilityBonusValue = new float[6];

	// Token: 0x040013E5 RID: 5093
	private float[] m_subTeamAbilityBonusValue = new float[6];

	// Token: 0x040013E6 RID: 5094
	private TeamAttributeCategory m_mainTeamAttributeCategory = TeamAttributeCategory.NONE;

	// Token: 0x040013E7 RID: 5095
	private TeamAttributeCategory m_subTeamAttributeCategory = TeamAttributeCategory.NONE;

	// Token: 0x040013E8 RID: 5096
	private float m_boostBonusValue;

	// Token: 0x040013E9 RID: 5097
	private float m_campaignBonusValue;

	// Token: 0x040013EA RID: 5098
	private StageAbilityManager.BonusRate m_count_chao_bonus_value_rate;

	// Token: 0x040013EB RID: 5099
	private StageAbilityManager.BonusRate m_main_chao_bonus_value_rate;

	// Token: 0x040013EC RID: 5100
	private StageAbilityManager.BonusRate m_sub_chao_bonus_value_rate;

	// Token: 0x040013ED RID: 5101
	private StageAbilityManager.BonusRate m_main_chara_bonus_value_rate;

	// Token: 0x040013EE RID: 5102
	private StageAbilityManager.BonusRate m_sub_chara_bonus_value_rate;

	// Token: 0x040013EF RID: 5103
	private StageAbilityManager.BonusRate m_bonus_value_rate;

	// Token: 0x040013F0 RID: 5104
	private StageAbilityManager.BonusRate m_mileage_bonus_score_rate;

	// Token: 0x040013F1 RID: 5105
	private PlayerInformation m_playerInformation;

	// Token: 0x040013F2 RID: 5106
	private float m_chaoCountBonus;

	// Token: 0x040013F3 RID: 5107
	private int m_chaoCount;

	// Token: 0x040013F4 RID: 5108
	private int m_getMainChaoRecoveryRingCount;

	// Token: 0x040013F5 RID: 5109
	private int m_getSubChaoRecoveryRingCount;

	// Token: 0x040013F6 RID: 5110
	private bool m_initFlag;

	// Token: 0x040013F7 RID: 5111
	private static StageAbilityManager instance = null;

	// Token: 0x040013F8 RID: 5112
	private static string CHAODATA_SCENENAME = "ChaoDataTable";

	// Token: 0x040013F9 RID: 5113
	private static string CHAODATA_NAME = "ChaoTable";

	// Token: 0x040013FA RID: 5114
	private static string CHARADATA_SCENENAME = "CharaAbilityDataTable";

	// Token: 0x040013FB RID: 5115
	private static string CHARADATA_NAME = "ImportAbilityTable";

	// Token: 0x040013FC RID: 5116
	private static readonly List<ResourceSceneLoader.ResourceInfo> m_loadInfo = new List<ResourceSceneLoader.ResourceInfo>
	{
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.ETC, StageAbilityManager.CHAODATA_SCENENAME, true, false, true, StageAbilityManager.CHAODATA_NAME, false),
		new ResourceSceneLoader.ResourceInfo(ResourceCategory.ETC, StageAbilityManager.CHARADATA_SCENENAME, true, false, true, StageAbilityManager.CHARADATA_NAME, false)
	};

	// Token: 0x020002FD RID: 765
	public struct BonusRate
	{
		// Token: 0x0600169A RID: 5786 RVA: 0x000819D0 File Offset: 0x0007FBD0
		public void Reset()
		{
			this.score = 0f;
			this.animal = 0f;
			this.ring = 0f;
			this.red_ring = 0f;
			this.distance = 0f;
			this.sp_crystal = 0f;
			this.raid_boss_ring = 0f;
			this.final_score = 0f;
		}

		// Token: 0x040013FD RID: 5117
		public float score;

		// Token: 0x040013FE RID: 5118
		public float animal;

		// Token: 0x040013FF RID: 5119
		public float ring;

		// Token: 0x04001400 RID: 5120
		public float red_ring;

		// Token: 0x04001401 RID: 5121
		public float distance;

		// Token: 0x04001402 RID: 5122
		public float sp_crystal;

		// Token: 0x04001403 RID: 5123
		public float raid_boss_ring;

		// Token: 0x04001404 RID: 5124
		public float final_score;
	}

	// Token: 0x020002FE RID: 766
	private class ChaoAbilityInfo
	{
		// Token: 0x0600169C RID: 5788 RVA: 0x00081A54 File Offset: 0x0007FC54
		public void Init()
		{
			this.abilityDatas.Clear();
			this.attribute = CharacterAttribute.UNKNOWN;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00081A68 File Offset: 0x0007FC68
		public void AddAbility(ChaoAbility ability, float bonus, float normal, float extra)
		{
			StageAbilityManager.ChaoAbilityInfo.AbilityData abilityData = new StageAbilityManager.ChaoAbilityInfo.AbilityData();
			abilityData.ability = ability;
			abilityData.bonus = bonus;
			abilityData.normal = normal;
			abilityData.extra = extra;
			this.abilityDatas.Add(abilityData);
		}

		// Token: 0x04001405 RID: 5125
		public List<StageAbilityManager.ChaoAbilityInfo.AbilityData> abilityDatas = new List<StageAbilityManager.ChaoAbilityInfo.AbilityData>();

		// Token: 0x04001406 RID: 5126
		public CharacterAttribute attribute = CharacterAttribute.UNKNOWN;

		// Token: 0x020002FF RID: 767
		public class AbilityData
		{
			// Token: 0x04001407 RID: 5127
			public ChaoAbility ability = ChaoAbility.UNKNOWN;

			// Token: 0x04001408 RID: 5128
			public float bonus;

			// Token: 0x04001409 RID: 5129
			public float normal;

			// Token: 0x0400140A RID: 5130
			public float extra;
		}
	}

	// Token: 0x02000300 RID: 768
	private enum ResType
	{
		// Token: 0x0400140C RID: 5132
		CHAO,
		// Token: 0x0400140D RID: 5133
		CHARA,
		// Token: 0x0400140E RID: 5134
		NUM
	}
}
