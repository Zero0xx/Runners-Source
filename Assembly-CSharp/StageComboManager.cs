using System;
using App.Utility;
using Message;
using Tutorial;
using UnityEngine;

// Token: 0x02000305 RID: 773
public class StageComboManager : MonoBehaviour
{
	// Token: 0x1700036E RID: 878
	// (get) Token: 0x060016A4 RID: 5796 RVA: 0x00081DE8 File Offset: 0x0007FFE8
	public int MaxComboCount
	{
		get
		{
			return this.m_maxComboCount;
		}
	}

	// Token: 0x1700036F RID: 879
	// (get) Token: 0x060016A5 RID: 5797 RVA: 0x00081DF0 File Offset: 0x0007FFF0
	public static StageComboManager Instance
	{
		get
		{
			return StageComboManager.m_instance;
		}
	}

	// Token: 0x060016A6 RID: 5798 RVA: 0x00081DF8 File Offset: 0x0007FFF8
	private void Awake()
	{
		if (StageComboManager.m_instance == null)
		{
			StageComboManager.m_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060016A7 RID: 5799 RVA: 0x00081E2C File Offset: 0x0008002C
	private void OnDestroy()
	{
		if (StageComboManager.m_instance == this)
		{
			StageComboManager.m_instance = null;
		}
	}

	// Token: 0x060016A8 RID: 5800 RVA: 0x00081E44 File Offset: 0x00080044
	private StageItemManager GetItemManager()
	{
		if (StageItemManager.Instance != null)
		{
			return StageItemManager.Instance;
		}
		return null;
	}

	// Token: 0x060016A9 RID: 5801 RVA: 0x00081E60 File Offset: 0x00080060
	public void SetComboTime(bool quick)
	{
		if (quick)
		{
			this.COMBO_TIME = this.QUICK_COMBO_TIME;
		}
		else
		{
			this.COMBO_TIME = this.ENDLESS_COMBO_TIME;
		}
	}

	// Token: 0x060016AA RID: 5802 RVA: 0x00081E88 File Offset: 0x00080088
	public void Setup()
	{
		this.m_comboCount = 0;
		this.m_viewComboNum = 0;
		this.m_viewBonusNum = 0;
		this.m_viewBonusMax = false;
		this.m_pauseCombo = false;
		this.m_pauseTimer = false;
		this.m_stopCombo = false;
		this.m_comboItem = false;
		this.m_reservedTimeFlag = false;
		this.m_playerInfo = ObjUtil.GetPlayerInformation();
		GameObject gameObject = GameObject.Find("GameModeStage");
		if (gameObject != null)
		{
			GameModeStage component = gameObject.GetComponent<GameModeStage>();
			if (component != null)
			{
				ObjectPartTable objectPartTable = component.GetObjectPartTable();
				if (objectPartTable != null)
				{
					for (int i = 0; i < StageComboManager.COMBO_NUM; i++)
					{
						this.m_comboNumList[i] = objectPartTable.GetComboBonusComboNum(i);
					}
					for (int j = 0; j < StageComboManager.BONUS_NUM; j++)
					{
						this.m_bonusNumList[j] = objectPartTable.GetComboBonusBonusNum(j);
					}
				}
			}
		}
		bool changeChara = false;
		this.SetupChao(changeChara);
	}

	// Token: 0x060016AB RID: 5803 RVA: 0x00081F74 File Offset: 0x00080174
	private void Update()
	{
		float deltaTime = Time.deltaTime;
		if (this.m_reservedTime > 0f)
		{
			if (!this.m_pauseCombo && !this.m_pauseTimer)
			{
				this.m_reservedTime -= deltaTime;
			}
			if (this.m_time < this.COMBO_TIME)
			{
				this.m_time = this.COMBO_TIME;
			}
		}
		this.UpdateChao(deltaTime);
		bool flag = this.IsComboItem();
		if (flag && this.m_time < this.COMBO_TIME)
		{
			this.m_time = this.COMBO_TIME;
		}
		if (!this.m_pauseCombo && !this.m_pauseTimer && !flag && this.m_time > 0f)
		{
			this.m_time -= deltaTime;
			if (this.m_time <= 0f)
			{
				if (this.m_maxComboCount < this.m_comboCount)
				{
					this.m_maxComboCount = this.m_comboCount;
				}
				this.m_time = 0f;
				this.m_comboCount = 0;
				this.ResetChao();
			}
		}
		if (this.m_viewComboNum != 0)
		{
			MsgCaution caution = new MsgCaution(HudCaution.Type.COMBO_N, this.m_viewComboNum, flag || this.m_reservedTime > 0f);
			HudCaution.Instance.SetCaution(caution);
		}
		else if ((this.m_reservedTimeFlag || this.m_comboItem) && this.m_comboCount > 0 && !flag && this.m_reservedTime <= 0f)
		{
			MsgCaution caution2 = new MsgCaution(HudCaution.Type.COMBO_N, this.m_comboCount, false);
			HudCaution.Instance.SetCaution(caution2);
		}
		if (this.m_viewBonusNum != 0)
		{
			if (flag)
			{
				MsgCaution caution3 = new MsgCaution(HudCaution.Type.COMBOITEM_BONUS_N, this.m_viewBonusNum, this.m_viewBonusMax);
				HudCaution.Instance.SetCaution(caution3);
			}
			else
			{
				MsgCaution caution4 = new MsgCaution(HudCaution.Type.BONUS_N, this.m_viewBonusNum, this.m_viewBonusMax);
				HudCaution.Instance.SetCaution(caution4);
			}
		}
		if (this.m_stopCombo)
		{
			MsgCaution invisibleCaution = new MsgCaution(HudCaution.Type.COMBO_N);
			MsgCaution invisibleCaution2 = new MsgCaution(HudCaution.Type.BONUS_N);
			MsgCaution invisibleCaution3 = new MsgCaution(HudCaution.Type.COMBOITEM_BONUS_N);
			HudCaution.Instance.SetInvisibleCaution(invisibleCaution);
			HudCaution.Instance.SetInvisibleCaution(invisibleCaution2);
			HudCaution.Instance.SetInvisibleCaution(invisibleCaution3);
		}
		this.m_viewComboNum = 0;
		this.m_viewBonusNum = 0;
		this.m_viewBonusMax = false;
		this.m_stopCombo = false;
		this.m_comboItem = flag;
		this.m_reservedTimeFlag = (this.m_reservedTime > 0f);
	}

	// Token: 0x060016AC RID: 5804 RVA: 0x000821F0 File Offset: 0x000803F0
	public void AddComboForChaoAbilityValue(int addCombo)
	{
		this.m_comboCount += addCombo;
		if (this.m_comboCount > 999999)
		{
			this.m_comboCount = 999999;
		}
		this.CheckChao(this.m_comboCount);
		int level = this.GetLevel(this.m_comboCount);
		int num = this.GetBonus(level);
		if (num > 2500)
		{
			num = 2500;
		}
		ObjUtil.SendMessageAddScore(num);
		ObjUtil.SendMessageScoreCheck(new StageScoreData(0, num));
		if (this.m_time < this.COMBO_TIME)
		{
			this.m_time = this.COMBO_TIME;
		}
		this.m_viewComboNum = this.m_comboCount;
		this.m_viewBonusNum = num;
		if (level == StageComboManager.COMBO_NUM)
		{
			this.m_viewBonusMax = true;
		}
		else
		{
			this.m_viewBonusMax = false;
		}
	}

	// Token: 0x060016AD RID: 5805 RVA: 0x000822B8 File Offset: 0x000804B8
	public void AddCombo()
	{
		if (this.m_pauseCombo)
		{
			return;
		}
		this.AddCombo(1);
	}

	// Token: 0x060016AE RID: 5806 RVA: 0x000822D0 File Offset: 0x000804D0
	public void DebugAddCombo(int val)
	{
	}

	// Token: 0x060016AF RID: 5807 RVA: 0x000822D4 File Offset: 0x000804D4
	private void AddCombo(int add)
	{
		this.m_comboCount += add;
		if (this.m_comboCount > 999999)
		{
			this.m_comboCount = 999999;
		}
		this.CheckChao(this.m_comboCount);
		int level = this.GetLevel(this.m_comboCount);
		int bonus = this.GetBonus(level);
		int num = this.GetComboScore(bonus);
		if (num > 2500)
		{
			num = 2500;
		}
		ObjUtil.SendMessageAddScore(num);
		ObjUtil.SendMessageScoreCheck(new StageScoreData(0, num));
		if (this.m_comboCount >= 3)
		{
			ObjUtil.SendMessageTutorialClear(EventID.RING_BONUS);
		}
		if (this.m_receptionRate > 0f)
		{
			this.m_reservedTime = this.m_receptionRate * this.COMBO_TIME;
		}
		if (this.m_time < this.COMBO_TIME)
		{
			this.m_time = this.COMBO_TIME;
		}
		this.m_viewComboNum = this.m_comboCount;
		this.m_viewBonusNum = num;
		if (level == StageComboManager.COMBO_NUM)
		{
			this.m_viewBonusMax = true;
		}
		else
		{
			this.m_viewBonusMax = false;
		}
	}

	// Token: 0x060016B0 RID: 5808 RVA: 0x000823DC File Offset: 0x000805DC
	public int GetLevel(int comboCount)
	{
		if (comboCount > this.m_comboNumList[StageComboManager.COMBO_NUM - 1])
		{
			return StageComboManager.COMBO_NUM;
		}
		for (int i = 0; i < StageComboManager.COMBO_NUM; i++)
		{
			int num = 0;
			int num2 = this.m_comboNumList[i];
			if (i > 0)
			{
				num = this.m_comboNumList[i - 1];
			}
			if (num < comboCount && comboCount <= num2)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x060016B1 RID: 5809 RVA: 0x00082448 File Offset: 0x00080648
	public int GetBonus(int level)
	{
		if (level < StageComboManager.BONUS_NUM)
		{
			return this.m_bonusNumList[level] + this.m_chaoComboAbilityBonus;
		}
		return 0;
	}

	// Token: 0x060016B2 RID: 5810 RVA: 0x00082468 File Offset: 0x00080668
	private int GetComboScore(int score)
	{
		if (this.IsDrillItem())
		{
			return score * 5;
		}
		if (this.IsComboItem())
		{
			StageItemManager itemManager = this.GetItemManager();
			if (itemManager != null)
			{
				return score * itemManager.GetComboScoreRate();
			}
		}
		return score;
	}

	// Token: 0x060016B3 RID: 5811 RVA: 0x000824AC File Offset: 0x000806AC
	private bool IsComboItem()
	{
		return (this.m_playerInfo != null && this.m_playerInfo.IsNowCombo()) || this.IsDrillItem();
	}

	// Token: 0x060016B4 RID: 5812 RVA: 0x000824E0 File Offset: 0x000806E0
	private bool IsDrillItem()
	{
		return this.m_playerInfo != null && this.m_playerInfo.PhantomType == PhantomType.DRILL;
	}

	// Token: 0x060016B5 RID: 5813 RVA: 0x00082508 File Offset: 0x00080708
	private void OnMsgStopCombo(MsgStopCombo msg)
	{
		if (this.IsComboItem())
		{
			return;
		}
		if (this.m_maxComboCount < this.m_comboCount)
		{
			this.m_maxComboCount = this.m_comboCount;
		}
		this.m_time = 0f;
		this.m_reservedTime = 0f;
		this.m_comboCount = 0;
		this.m_stopCombo = true;
		this.ResetChao();
	}

	// Token: 0x060016B6 RID: 5814 RVA: 0x00082568 File Offset: 0x00080768
	private void OnMsgPauseComboTimer(MsgPauseComboTimer msg)
	{
		switch (msg.m_value)
		{
		case MsgPauseComboTimer.State.PAUSE:
			this.m_pauseCombo = true;
			break;
		case MsgPauseComboTimer.State.PAUSE_TIMER:
			this.m_pauseTimer = true;
			break;
		case MsgPauseComboTimer.State.PLAY:
			this.m_pauseCombo = false;
			this.m_pauseTimer = false;
			break;
		case MsgPauseComboTimer.State.PLAY_SET:
			this.m_pauseCombo = false;
			this.m_pauseTimer = false;
			this.m_reservedTime = 0f;
			if (this.m_comboCount > 0 && msg.m_time > 0f)
			{
				if (msg.m_time > this.COMBO_TIME)
				{
					this.m_reservedTime = msg.m_time - this.COMBO_TIME;
					this.m_time = this.COMBO_TIME;
				}
				else if (this.m_time < msg.m_time)
				{
					this.m_time = msg.m_time;
				}
			}
			break;
		case MsgPauseComboTimer.State.PLAY_RESET:
			this.m_pauseCombo = false;
			this.m_pauseTimer = false;
			this.m_reservedTime = 0f;
			if (this.m_comboCount > 0 && this.m_time > 0f)
			{
				this.m_time = this.COMBO_TIME;
			}
			break;
		}
	}

	// Token: 0x060016B7 RID: 5815 RVA: 0x00082698 File Offset: 0x00080898
	private void OnMsgExitStage(MsgExitStage msg)
	{
		base.enabled = false;
	}

	// Token: 0x060016B8 RID: 5816 RVA: 0x000826A4 File Offset: 0x000808A4
	private void SetupChao(bool changeChara)
	{
		StageAbilityManager instance = StageAbilityManager.Instance;
		if (instance != null)
		{
			this.m_chaoComboAbilityBonus = (int)instance.GetChaoAbilityValue(ChaoAbility.COMBO_BONUS_UP);
			if (!changeChara)
			{
				for (int i = 0; i < this.m_chaoCombActiveParam.Length; i++)
				{
					if (this.m_chaoCombActiveParam[i] == null)
					{
						this.m_chaoCombActiveParam[i] = new ComboAcviteParam();
						for (int j = 0; j < StageComboManager.ChaoAbilityTbl.Length; j++)
						{
							ChaoAbility chaoAbility = StageComboManager.ChaoAbilityTbl[j].m_chaoAbility;
							if (instance.HasChaoAbility(chaoAbility, (ChaoType)i) && StageComboManager.ChaoAbilityTbl[j].m_type == ComboChaoAbilityType.TIME)
							{
								this.m_chaoCombActiveParam[i].m_chaoAbility = StageComboManager.ChaoAbilityTbl[j].m_chaoAbility;
								this.m_chaoCombActiveParam[i].m_timeMax = StageComboManager.ChaoAbilityTbl[j].m_timeMax;
							}
						}
					}
				}
			}
			for (int k = 0; k < this.m_chaoParam.Length; k++)
			{
				int continuationNum = 0;
				if (this.m_chaoParam[k] != null)
				{
					continuationNum = this.m_chaoParam[k].m_continuationNum;
				}
				this.m_chaoParam[k] = new ComboChaoParam(ChaoAbility.UNKNOWN, ComboChaoAbilityType.NORMAL, 0f, 0, 0);
				for (int l = 0; l < StageComboManager.ChaoAbilityTbl.Length; l++)
				{
					ChaoAbility chaoAbility2 = StageComboManager.ChaoAbilityTbl[l].m_chaoAbility;
					if (instance.HasChaoAbility(chaoAbility2, (ChaoType)k))
					{
						this.m_chaoParam[k].m_chaoAbility = chaoAbility2;
						this.m_chaoParam[k].m_extraValue = instance.GetChaoAbilityExtraValue(chaoAbility2, (ChaoType)k);
						this.m_chaoParam[k].m_continuationNum = continuationNum;
						if (StageComboManager.ChaoAbilityTbl[l].m_type == ComboChaoAbilityType.EXTRA)
						{
							this.m_chaoParam[k].m_comboNum = (int)this.m_chaoParam[k].m_extraValue;
						}
						else
						{
							this.m_chaoParam[k].m_comboNum = (int)instance.GetChaoAbilityValue(chaoAbility2, (ChaoType)k);
						}
						if (chaoAbility2 == ChaoAbility.COMBO_ADD_COMBO_VALUE && this.m_chaoParam[k].m_extraValue > (float)this.m_chaoParam[k].m_comboNum)
						{
							this.m_chaoParam[k].m_extraValue = (float)Math.Max(this.m_chaoParam[k].m_comboNum - 10, 1);
						}
					}
				}
			}
		}
		this.SetReceptionTime();
		if (changeChara)
		{
			this.ResetChaoForChangeChara();
		}
		else
		{
			this.ResetChao();
		}
		for (int m = 0; m < this.m_chaoParam.Length; m++)
		{
			this.SetDebugDraw(string.Concat(new object[]
			{
				"SetupChao ChaoAbility=",
				this.m_chaoParam[m].m_chaoAbility.ToString(),
				" index=",
				m,
				" m_comboNum=",
				this.m_chaoParam[m].m_comboNum,
				" m_nextCombo=",
				this.m_chaoParam[m].m_nextCombo
			}));
		}
	}

	// Token: 0x060016B9 RID: 5817 RVA: 0x000829A4 File Offset: 0x00080BA4
	private void UpdateChao(float delta)
	{
		for (int i = 0; i < this.m_chaoParam.Length; i++)
		{
			if (this.m_chaoParam[i] != null)
			{
				this.m_chaoParam[i].m_movement = false;
			}
		}
		for (int j = 0; j < this.m_chaoCombActiveParam.Length; j++)
		{
			if (this.m_chaoCombActiveParam[j] != null && this.m_chaoCombActiveParam[j].m_chaoAbility != ChaoAbility.UNKNOWN && this.m_chaoCombActiveParam[j].m_time > 0f)
			{
				this.m_chaoCombActiveParam[j].m_time -= delta;
				if (this.m_chaoCombActiveParam[j].m_time < 0f)
				{
					this.m_chaoCombActiveParam[j].m_time = 0f;
				}
				this.SetDebugDraw(string.Concat(new object[]
				{
					"UpdateChao ChaoAbility=",
					this.m_chaoCombActiveParam[j].m_chaoAbility.ToString(),
					" index=",
					j,
					" time=",
					this.m_chaoCombActiveParam[j].m_time
				}));
			}
		}
	}

	// Token: 0x060016BA RID: 5818 RVA: 0x00082AD4 File Offset: 0x00080CD4
	private void CheckChao(int comboCount)
	{
		for (int i = 0; i < this.m_chaoParam.Length; i++)
		{
			if (this.m_chaoParam[i] != null && this.m_chaoParam[i].m_comboNum != 0 && comboCount >= this.m_chaoParam[i].m_nextCombo)
			{
				this.m_chaoParam[i].m_nextCombo += this.m_chaoParam[i].m_comboNum;
				this.m_chaoParam[i].m_continuationNum++;
				if (comboCount >= this.m_chaoParam[i].m_nextCombo)
				{
					this.CheckChao(comboCount);
				}
				if (!this.m_chaoParam[i].m_movement)
				{
					this.SetDebugDraw(string.Concat(new object[]
					{
						"CheckChao ChaoAbility=",
						this.m_chaoParam[i].m_chaoAbility.ToString(),
						" index=",
						i,
						" m_comboNum=",
						this.m_chaoParam[i].m_comboNum,
						" m_nextCombo=",
						this.m_chaoParam[i].m_nextCombo
					}));
					this.SetActiveComboChaoAbility((ChaoType)i);
					this.m_chaoParam[i].m_movement = true;
				}
			}
		}
	}

	// Token: 0x060016BB RID: 5819 RVA: 0x00082C24 File Offset: 0x00080E24
	private void SetReceptionTime()
	{
		this.m_receptionRate = 0f;
		if (StageAbilityManager.Instance != null && StageAbilityManager.Instance.HasChaoAbility(ChaoAbility.COMBO_RECEPTION_TIME))
		{
			this.m_receptionRate = StageAbilityManager.Instance.GetChaoAbilityValue(ChaoAbility.COMBO_RECEPTION_TIME) * 0.01f;
		}
	}

	// Token: 0x060016BC RID: 5820 RVA: 0x00082C78 File Offset: 0x00080E78
	private void ResetChao()
	{
		for (int i = 0; i < this.m_chaoParam.Length; i++)
		{
			if (this.m_chaoParam[i] != null && this.m_chaoParam[i].m_comboNum != 0)
			{
				this.m_chaoParam[i].m_nextCombo = this.m_chaoParam[i].m_comboNum;
				this.m_chaoParam[i].m_continuationNum = 0;
			}
		}
	}

	// Token: 0x060016BD RID: 5821 RVA: 0x00082CE8 File Offset: 0x00080EE8
	private void ResetChaoForChangeChara()
	{
		for (int i = 0; i < this.m_chaoParam.Length; i++)
		{
			if (this.m_chaoParam[i] != null && this.m_chaoParam[i].m_comboNum != 0)
			{
				this.m_chaoParam[i].m_nextCombo = this.m_chaoParam[i].m_comboNum;
				this.m_chaoParam[i].m_nextCombo += this.m_chaoParam[i].m_continuationNum * this.m_chaoParam[i].m_comboNum;
			}
		}
	}

	// Token: 0x060016BE RID: 5822 RVA: 0x00082D78 File Offset: 0x00080F78
	public void SetChaoFlagStatus(StageComboManager.ChaoFlagStatus status, bool flag)
	{
		this.m_chaoFlag.Set((int)status, flag);
	}

	// Token: 0x060016BF RID: 5823 RVA: 0x00082D88 File Offset: 0x00080F88
	public bool IsChaoFlagStatus(StageComboManager.ChaoFlagStatus status)
	{
		return this.m_chaoFlag.Test((int)status);
	}

	// Token: 0x060016C0 RID: 5824 RVA: 0x00082D98 File Offset: 0x00080F98
	public bool IsActiveComboChaoAbility(ChaoAbility chaoAbility)
	{
		for (int i = 0; i < this.m_chaoCombActiveParam.Length; i++)
		{
			if (this.m_chaoCombActiveParam[i] != null && this.m_chaoCombActiveParam[i].m_chaoAbility == chaoAbility && this.m_chaoCombActiveParam[i].m_time > 0f)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060016C1 RID: 5825 RVA: 0x00082DF8 File Offset: 0x00080FF8
	private void SetActiveComboChaoAbility(ChaoType chaoType)
	{
		if ((ulong)chaoType >= (ulong)((long)this.m_chaoParam.Length))
		{
			return;
		}
		if (this.m_chaoParam[(int)chaoType] == null)
		{
			return;
		}
		if (this.m_chaoCombActiveParam[(int)chaoType] != null && this.m_chaoCombActiveParam[(int)chaoType].m_chaoAbility != ChaoAbility.UNKNOWN)
		{
			this.m_chaoCombActiveParam[(int)chaoType].m_time = this.m_chaoCombActiveParam[(int)chaoType].m_timeMax;
		}
		bool withEffect = true;
		ChaoAbility chaoAbility = this.m_chaoParam[(int)chaoType].m_chaoAbility;
		switch (chaoAbility)
		{
		case ChaoAbility.CHAO_RING_MAGNET:
		case ChaoAbility.CHAO_CRYSTAL_MAGNET:
		case ChaoAbility.COMBO_RING_BANK:
		case ChaoAbility.COMBO_METAL_AND_METAL_SCORE:
		case ChaoAbility.COMBO_EQUIP_ITEM_EXTRA:
		case ChaoAbility.COMBO_CHANGE_EQUIP_ITEM:
		case ChaoAbility.COMBO_COLOR_POWER_DRILL:
		case ChaoAbility.COMBO_COLOR_POWER_LASER:
		case ChaoAbility.COMBO_COLOR_POWER_ASTEROID:
		case ChaoAbility.COMBO_RANDOM_ITEM_MINUS_RING:
		case ChaoAbility.COMBO_RANDOM_ITEM:
			break;
		default:
			switch (chaoAbility)
			{
			case ChaoAbility.COMBO_ITEM_BOX:
			case ChaoAbility.COMBO_BARRIER:
			case ChaoAbility.COMBO_WIPE_OUT_ENEMY:
			case ChaoAbility.COMBO_COLOR_POWER:
			case ChaoAbility.COMBO_DESTROY_AIR_TRAP:
			case ChaoAbility.COMBO_STEP_DESTROY_GET_10_RING:
			case ChaoAbility.COMBO_EQUIP_ITEM:
				break;
			case ChaoAbility.COMBO_SMALL_CRYSTAL_RED:
			case ChaoAbility.COMBO_SMALL_CRYSTAL_GREEN:
			case ChaoAbility.COMBO_BIG_CRYSTAL_RED:
			case ChaoAbility.COMBO_BIG_CRYSTAL_GREEN:
			case ChaoAbility.COMBO_RARE_ENEMY:
			case ChaoAbility.COMBO_RECOVERY_ANIMAL:
			case ChaoAbility.COMBO_SUPER_RING:
			case ChaoAbility.COMBO_ALL_SPECIAL_CRYSTAL:
			case ChaoAbility.COMBO_DESTROY_TRAP:
				goto IL_1B2;
			case ChaoAbility.COMBO_ADD_COMBO_VALUE:
			{
				int add = (int)this.m_chaoParam[(int)chaoType].m_extraValue;
				this.AddCombo(add);
				goto IL_1B2;
			}
			case ChaoAbility.COMBO_RECOVERY_ALL_OBJ:
				goto IL_13D;
			default:
				goto IL_1B2;
			}
			break;
		case ChaoAbility.COMBO_DESTROY_AND_RECOVERY:
			goto IL_13D;
		}
		withEffect = false;
		goto IL_1B2;
		IL_13D:
		withEffect = false;
		GameObjectUtil.SendDelayedMessageToTagObjects("Ring", "OnDrawingRingsChaoAbility", new MsgOnDrawingRings(this.m_chaoParam[(int)chaoType].m_chaoAbility));
		GameObjectUtil.SendDelayedMessageToTagObjects("Crystal", "OnDrawingRingsChaoAbility", new MsgOnDrawingRings(this.m_chaoParam[(int)chaoType].m_chaoAbility));
		GameObjectUtil.SendDelayedMessageToTagObjects("Timer", "OnDrawingRingsChaoAbility", new MsgOnDrawingRings(this.m_chaoParam[(int)chaoType].m_chaoAbility));
		this.m_reservedTime = 7f;
		IL_1B2:
		ObjUtil.RequestStartAbilityToChao(this.m_chaoParam[(int)chaoType].m_chaoAbility, withEffect);
		this.SetDebugDraw(string.Concat(new object[]
		{
			"SetActiveComboChaoAbility ChaoAbility=",
			this.m_chaoParam[(int)chaoType].m_chaoAbility.ToString(),
			" index=",
			(int)chaoType
		}));
	}

	// Token: 0x060016C2 RID: 5826 RVA: 0x00083010 File Offset: 0x00081210
	private void OnPassPointMarker()
	{
		StageAbilityManager instance = StageAbilityManager.Instance;
		if (instance == null)
		{
			return;
		}
		if (!instance.HasChaoAbility(ChaoAbility.CHECK_POINT_COMBO_UP))
		{
			return;
		}
		int num = (int)instance.GetChaoAbilityValue(ChaoAbility.CHECK_POINT_COMBO_UP);
		if (num > 0)
		{
			this.AddCombo(num);
			ObjUtil.RequestStartAbilityToChao(ChaoAbility.CHECK_POINT_COMBO_UP, true);
		}
		this.SetDebugDraw("OnPassPointMarker upComboCount=" + num);
	}

	// Token: 0x060016C3 RID: 5827 RVA: 0x00083078 File Offset: 0x00081278
	public void OnChangeCharaSucceed(MsgChangeCharaSucceed msg)
	{
		this.SetDebugDraw("OnChangeCharaSucceed");
		bool changeChara = true;
		this.SetupChao(changeChara);
	}

	// Token: 0x060016C4 RID: 5828 RVA: 0x0008309C File Offset: 0x0008129C
	public void OnChaoAbilityEnemyBreak()
	{
		if (StageAbilityManager.Instance != null && StageAbilityManager.Instance.HasChaoAbility(ChaoAbility.ENEMY_COUNT_BOMB))
		{
			this.m_enemyBreak++;
			global::Debug.LogWarning("OnChaoAbilityEnemyBreak m_enemyBreak = " + this.m_enemyBreak.ToString());
			int num = (int)StageAbilityManager.Instance.GetChaoAbilityValue(ChaoAbility.ENEMY_COUNT_BOMB);
			if (this.m_enemyBreak >= num)
			{
				this.m_enemyBreak = 0;
				ObjUtil.RequestStartAbilityToChao(ChaoAbility.ENEMY_COUNT_BOMB, false);
			}
		}
	}

	// Token: 0x060016C5 RID: 5829 RVA: 0x0008311C File Offset: 0x0008131C
	private void SetDebugDraw(string msg)
	{
	}

	// Token: 0x04001420 RID: 5152
	private const int COMBO_MAX = 999999;

	// Token: 0x04001421 RID: 5153
	private const int COMBO_UP_CORRECTION_VALUE = 50;

	// Token: 0x04001422 RID: 5154
	private const int TUTORIAL_CLEAR_COUNT = 3;

	// Token: 0x04001423 RID: 5155
	private const int ITEM_DRILL_SCORE = 5;

	// Token: 0x04001424 RID: 5156
	private const int COMBO_RESERVED_TIME = 7;

	// Token: 0x04001425 RID: 5157
	private const int MAX_COMBO_BONUS = 2500;

	// Token: 0x04001426 RID: 5158
	public bool m_debugInfo;

	// Token: 0x04001427 RID: 5159
	[SerializeField]
	private float ENDLESS_COMBO_TIME = 1.5f;

	// Token: 0x04001428 RID: 5160
	[SerializeField]
	private float QUICK_COMBO_TIME = 3f;

	// Token: 0x04001429 RID: 5161
	private float COMBO_TIME = 1.5f;

	// Token: 0x0400142A RID: 5162
	private static readonly ComboChaoAbilityData[] ChaoAbilityTbl = new ComboChaoAbilityData[]
	{
		new ComboChaoAbilityData(ChaoAbility.COMBO_ITEM_BOX, ComboChaoAbilityType.NORMAL, 0f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_SMALL_CRYSTAL_RED, ComboChaoAbilityType.TIME, 5f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_SMALL_CRYSTAL_GREEN, ComboChaoAbilityType.TIME, 5f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_BIG_CRYSTAL_RED, ComboChaoAbilityType.TIME, 5f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_BIG_CRYSTAL_GREEN, ComboChaoAbilityType.TIME, 5f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_RARE_ENEMY, ComboChaoAbilityType.TIME, 5f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_BARRIER, ComboChaoAbilityType.NORMAL, 0f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_RECOVERY_ANIMAL, ComboChaoAbilityType.TIME, 5f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_RECOVERY_ALL_OBJ, ComboChaoAbilityType.TIME, 0.5f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_WIPE_OUT_ENEMY, ComboChaoAbilityType.NORMAL, 0f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_DESTROY_TRAP, ComboChaoAbilityType.NORMAL, 0f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_DESTROY_AIR_TRAP, ComboChaoAbilityType.NORMAL, 0f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_EQUIP_ITEM, ComboChaoAbilityType.NORMAL, 0f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_STEP_DESTROY_GET_10_RING, ComboChaoAbilityType.NORMAL, 0f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_METAL_AND_METAL_SCORE, ComboChaoAbilityType.TIME, 5f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_DESTROY_AND_RECOVERY, ComboChaoAbilityType.TIME, 0.5f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_RING_BANK, ComboChaoAbilityType.NORMAL, 0f),
		new ComboChaoAbilityData(ChaoAbility.CHAO_RING_MAGNET, ComboChaoAbilityType.EXTRA, 0f),
		new ComboChaoAbilityData(ChaoAbility.CHAO_CRYSTAL_MAGNET, ComboChaoAbilityType.EXTRA, 0f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_EQUIP_ITEM_EXTRA, ComboChaoAbilityType.NORMAL, 0f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_COLOR_POWER_ASTEROID, ComboChaoAbilityType.NORMAL, 0f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_COLOR_POWER_DRILL, ComboChaoAbilityType.NORMAL, 0f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_COLOR_POWER_LASER, ComboChaoAbilityType.NORMAL, 0f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_CHANGE_EQUIP_ITEM, ComboChaoAbilityType.NORMAL, 0f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_COLOR_POWER, ComboChaoAbilityType.NORMAL, 0f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_SUPER_RING, ComboChaoAbilityType.TIME, 5f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_ADD_COMBO_VALUE, ComboChaoAbilityType.NORMAL, 0f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_RANDOM_ITEM_MINUS_RING, ComboChaoAbilityType.NORMAL, 0f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_RANDOM_ITEM, ComboChaoAbilityType.NORMAL, 0f),
		new ComboChaoAbilityData(ChaoAbility.COMBO_ALL_SPECIAL_CRYSTAL, ComboChaoAbilityType.TIME, 5f)
	};

	// Token: 0x0400142B RID: 5163
	public static int COMBO_NUM = 7;

	// Token: 0x0400142C RID: 5164
	public static int BONUS_NUM = 8;

	// Token: 0x0400142D RID: 5165
	private int[] m_comboNumList = new int[StageComboManager.COMBO_NUM];

	// Token: 0x0400142E RID: 5166
	private int[] m_bonusNumList = new int[StageComboManager.BONUS_NUM];

	// Token: 0x0400142F RID: 5167
	private int m_comboCount;

	// Token: 0x04001430 RID: 5168
	private int m_viewComboNum;

	// Token: 0x04001431 RID: 5169
	private int m_viewBonusNum;

	// Token: 0x04001432 RID: 5170
	private int m_chaoComboAbilityBonus;

	// Token: 0x04001433 RID: 5171
	private bool m_viewBonusMax;

	// Token: 0x04001434 RID: 5172
	private float m_time;

	// Token: 0x04001435 RID: 5173
	private PlayerInformation m_playerInfo;

	// Token: 0x04001436 RID: 5174
	private bool m_pauseCombo;

	// Token: 0x04001437 RID: 5175
	private bool m_pauseTimer;

	// Token: 0x04001438 RID: 5176
	private bool m_stopCombo;

	// Token: 0x04001439 RID: 5177
	private bool m_comboItem;

	// Token: 0x0400143A RID: 5178
	private bool m_reservedTimeFlag;

	// Token: 0x0400143B RID: 5179
	private float m_reservedTime;

	// Token: 0x0400143C RID: 5180
	private float m_receptionRate = 1f;

	// Token: 0x0400143D RID: 5181
	private StageItemManager m_itemManager;

	// Token: 0x0400143E RID: 5182
	private int m_maxComboCount;

	// Token: 0x0400143F RID: 5183
	private int m_enemyBreak;

	// Token: 0x04001440 RID: 5184
	private Bitset32 m_chaoFlag = new Bitset32(0U);

	// Token: 0x04001441 RID: 5185
	public static float CHAO_OBJ_DEAD_TIME = 2f;

	// Token: 0x04001442 RID: 5186
	private ComboChaoParam[] m_chaoParam = new ComboChaoParam[2];

	// Token: 0x04001443 RID: 5187
	private ComboAcviteParam[] m_chaoCombActiveParam = new ComboAcviteParam[2];

	// Token: 0x04001444 RID: 5188
	private static StageComboManager m_instance = null;

	// Token: 0x02000306 RID: 774
	public enum ChaoFlagStatus
	{
		// Token: 0x04001446 RID: 5190
		NONE = -1,
		// Token: 0x04001447 RID: 5191
		ENEMY_DEAD,
		// Token: 0x04001448 RID: 5192
		DESTROY_TRAP,
		// Token: 0x04001449 RID: 5193
		DESTROY_AIRTRAP
	}
}
