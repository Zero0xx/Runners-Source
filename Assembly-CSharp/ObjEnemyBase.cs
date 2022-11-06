using System;
using System.Collections.Generic;
using Message;
using Mission;
using Tutorial;
using UnityEngine;

// Token: 0x02000930 RID: 2352
public class ObjEnemyBase : SpawnableObject
{
	// Token: 0x06003D9A RID: 15770 RVA: 0x00141A50 File Offset: 0x0013FC50
	protected override void OnSpawned()
	{
		if (StageComboManager.Instance != null && StageComboManager.Instance.IsChaoFlagStatus(StageComboManager.ChaoFlagStatus.ENEMY_DEAD))
		{
			this.SetBroken();
		}
	}

	// Token: 0x06003D9B RID: 15771 RVA: 0x00141A84 File Offset: 0x0013FC84
	private void Update()
	{
		if (this.m_end)
		{
			return;
		}
		if (!this.m_setupAnimFlag)
		{
			this.m_setupAnimFlag = this.SetupAnim();
		}
		if (this.m_destroyFlag)
		{
			this.m_end = true;
			this.CreateBrokenItem();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06003D9C RID: 15772 RVA: 0x00141AD8 File Offset: 0x0013FCD8
	public void OnMsgObjectDead(MsgObjectDead msg)
	{
		if (base.enabled)
		{
			this.SetBroken();
		}
	}

	// Token: 0x06003D9D RID: 15773 RVA: 0x00141AEC File Offset: 0x0013FCEC
	public void OnMsgHeavyBombDead(float goldAnimalPercent)
	{
		if (base.enabled)
		{
			float num = UnityEngine.Random.Range(0f, 99.9f);
			if (goldAnimalPercent >= num)
			{
				this.m_heabyBomFlag = true;
			}
			this.SetBroken();
		}
	}

	// Token: 0x06003D9E RID: 15774 RVA: 0x00141B28 File Offset: 0x0013FD28
	protected override string GetModelName()
	{
		return ObjEnemyUtil.GetModelName(this.GetEnemyType(), this.GetModelFiles());
	}

	// Token: 0x06003D9F RID: 15775 RVA: 0x00141B3C File Offset: 0x0013FD3C
	protected override ResourceCategory GetModelCategory()
	{
		return ResourceCategory.ENEMY_RESOURCE;
	}

	// Token: 0x06003DA0 RID: 15776 RVA: 0x00141B40 File Offset: 0x0013FD40
	protected virtual ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.NORMAL;
	}

	// Token: 0x06003DA1 RID: 15777 RVA: 0x00141B44 File Offset: 0x0013FD44
	protected virtual string[] GetModelFiles()
	{
		return null;
	}

	// Token: 0x06003DA2 RID: 15778 RVA: 0x00141B48 File Offset: 0x0013FD48
	protected virtual int[] GetScoreTable()
	{
		return ObjEnemyUtil.GetDefaultScoreTable();
	}

	// Token: 0x06003DA3 RID: 15779 RVA: 0x00141B50 File Offset: 0x0013FD50
	protected virtual ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnemyUtil.EnemyEffectSize.S;
	}

	// Token: 0x06003DA4 RID: 15780 RVA: 0x00141B54 File Offset: 0x0013FD54
	protected virtual bool IsNormalMotion(float speed)
	{
		float y = base.transform.rotation.eulerAngles.y;
		return y <= 80f || y >= 100f;
	}

	// Token: 0x06003DA5 RID: 15781 RVA: 0x00141B98 File Offset: 0x0013FD98
	protected void SetupEnemy(uint id, float speed)
	{
		this.m_enmyType = this.GetOriginalType();
		this.SetupRareCheck(id);
		this.SetupMetalCheck();
		if (!this.IsNormalMotion(speed))
		{
			this.m_rightAnimFlag = true;
		}
	}

	// Token: 0x06003DA6 RID: 15782 RVA: 0x00141BD4 File Offset: 0x0013FDD4
	private void SetupRareCheck(uint id)
	{
		StageComboManager instance = StageComboManager.Instance;
		if (instance != null && instance.IsActiveComboChaoAbility(ChaoAbility.COMBO_RARE_ENEMY))
		{
			this.m_enmyType = ObjEnemyUtil.EnemyType.RARE;
		}
		if (this.m_enmyType != ObjEnemyUtil.EnemyType.RARE)
		{
			GameObject gameObject = GameObjectUtil.FindGameObjectWithTag("GameModeStage", "GameModeStage");
			if (gameObject != null)
			{
				GameModeStage component = gameObject.GetComponent<GameModeStage>();
				if (component != null)
				{
					RareEnemyTable rareEnemyTable = component.GetRareEnemyTable();
					if (rareEnemyTable != null && rareEnemyTable.IsRareEnemy(id))
					{
						this.m_enmyType = ObjEnemyUtil.EnemyType.RARE;
					}
				}
			}
		}
	}

	// Token: 0x06003DA7 RID: 15783 RVA: 0x00141C64 File Offset: 0x0013FE64
	private void SetupMetalCheck()
	{
		if (this.m_enmyType == ObjEnemyUtil.EnemyType.NORMAL)
		{
			StageComboManager instance = StageComboManager.Instance;
			if (instance != null && instance.IsActiveComboChaoAbility(ChaoAbility.COMBO_METAL_AND_METAL_SCORE))
			{
				this.m_enmyType = ObjEnemyUtil.EnemyType.METAL;
			}
		}
	}

	// Token: 0x06003DA8 RID: 15784 RVA: 0x00141CA4 File Offset: 0x0013FEA4
	private bool SetupAnim()
	{
		Animator componentInChildren = base.GetComponentInChildren<Animator>();
		if (componentInChildren)
		{
			if (this.m_rightAnimFlag)
			{
				componentInChildren.Play("Idle_r");
			}
			return true;
		}
		return false;
	}

	// Token: 0x06003DA9 RID: 15785 RVA: 0x00141CDC File Offset: 0x0013FEDC
	private bool IsRare()
	{
		return this.m_enmyType == ObjEnemyUtil.EnemyType.RARE;
	}

	// Token: 0x06003DAA RID: 15786 RVA: 0x00141CE8 File Offset: 0x0013FEE8
	private bool IsMetal()
	{
		return this.m_enmyType == ObjEnemyUtil.EnemyType.METAL;
	}

	// Token: 0x06003DAB RID: 15787 RVA: 0x00141CF4 File Offset: 0x0013FEF4
	private int GetScore()
	{
		return ObjEnemyUtil.GetScore(this.m_enmyType, this.GetScoreTable());
	}

	// Token: 0x06003DAC RID: 15788 RVA: 0x00141D08 File Offset: 0x0013FF08
	private ObjEnemyUtil.EnemyType GetEnemyType()
	{
		return this.m_enmyType;
	}

	// Token: 0x06003DAD RID: 15789 RVA: 0x00141D10 File Offset: 0x0013FF10
	private void OnTriggerEnter(Collider other)
	{
		if (this.m_destroyFlag)
		{
			return;
		}
		if (other)
		{
			GameObject gameObject = other.gameObject;
			if (gameObject)
			{
				AttackPower attack = AttackPower.PlayerSpin;
				if (this.m_enmyType == ObjEnemyUtil.EnemyType.METAL)
				{
					attack = AttackPower.PlayerSpin;
				}
				MsgHitDamage value = new MsgHitDamage(base.gameObject, attack);
				gameObject.SendMessage("OnDamageHit", value, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x06003DAE RID: 15790 RVA: 0x00141D70 File Offset: 0x0013FF70
	private void OnDamageHit(MsgHitDamage msg)
	{
		if (this.m_destroyFlag)
		{
			return;
		}
		if (msg.m_sender)
		{
			GameObject gameObject = msg.m_sender.gameObject;
			if (gameObject)
			{
				bool flag = this.IsMetal();
				if (this.IsEnemyBroken(flag, msg.m_attackPower, msg.m_attackAttribute))
				{
					MsgHitDamageSucceed value = new MsgHitDamageSucceed(base.gameObject, 0, ObjUtil.GetCollisionCenterPosition(base.gameObject), base.transform.rotation);
					gameObject.SendMessage("OnDamageSucceed", value, SendMessageOptions.DontRequireReceiver);
					this.SetPlayerBroken(flag, msg.m_attackAttribute);
					ObjUtil.CreateBrokenBonus(base.gameObject, gameObject, msg.m_attackAttribute);
				}
				else if (flag && msg.m_attackPower > 0)
				{
					MsgAttackGuard value2 = new MsgAttackGuard(base.gameObject);
					gameObject.SendMessage("OnAttackGuard", value2, SendMessageOptions.DontRequireReceiver);
					this.PlayGuardEffect();
					ObjUtil.LightPlaySE("enm_metal_hit", "SE");
				}
			}
		}
	}

	// Token: 0x06003DAF RID: 15791 RVA: 0x00141E64 File Offset: 0x00140064
	private bool IsEnemyBroken(bool metal, int attackPower, uint attribute_state)
	{
		AttackPower attackPower2 = (!metal) ? AttackPower.PlayerSpin : AttackPower.PlayerPower;
		return attackPower >= (int)attackPower2 || (metal && attackPower == 2 && (attribute_state & 8U) > 0U);
	}

	// Token: 0x06003DB0 RID: 15792 RVA: 0x00141EA0 File Offset: 0x001400A0
	private void PlayHitEffect()
	{
		EffectPlayType type = EffectPlayType.UNKNOWN;
		switch (this.GetEffectSize())
		{
		case ObjEnemyUtil.EnemyEffectSize.S:
			type = EffectPlayType.ENEMY_S;
			break;
		case ObjEnemyUtil.EnemyEffectSize.M:
			type = EffectPlayType.ENEMY_M;
			break;
		case ObjEnemyUtil.EnemyEffectSize.L:
			type = EffectPlayType.ENEMY_L;
			break;
		}
		if (StageEffectManager.Instance != null)
		{
			StageEffectManager.Instance.PlayEffect(type, ObjUtil.GetCollisionCenterPosition(base.gameObject), Quaternion.identity);
		}
	}

	// Token: 0x06003DB1 RID: 15793 RVA: 0x00141F10 File Offset: 0x00140110
	private void PlayGuardEffect()
	{
		if (StageEffectManager.Instance != null)
		{
			StageEffectManager.Instance.PlayEffect(EffectPlayType.ENEMY_GUARD, ObjUtil.GetCollisionCenterPosition(base.gameObject), Quaternion.identity);
		}
	}

	// Token: 0x06003DB2 RID: 15794 RVA: 0x00141F48 File Offset: 0x00140148
	private void CreateBrokenItem()
	{
		TimerType timerType = this.GetTimerType();
		if (timerType != TimerType.ERROR)
		{
			ObjTimerUtil.CreateTimer(base.gameObject, timerType);
		}
		else
		{
			this.CreateAnimal();
		}
	}

	// Token: 0x06003DB3 RID: 15795 RVA: 0x00141F7C File Offset: 0x0014017C
	private TimerType GetTimerType()
	{
		if (StageModeManager.Instance != null && StageModeManager.Instance.IsQuickMode() && ObjTimerUtil.IsEnableCreateTimer())
		{
			GameObject gameObject = GameObjectUtil.FindGameObjectWithTag("GameModeStage", "GameModeStage");
			if (gameObject != null)
			{
				GameModeStage component = gameObject.GetComponent<GameModeStage>();
				if (component != null)
				{
					EnemyExtendItemTable enemyExtendItemTable = component.GetEnemyExtendItemTable();
					if (enemyExtendItemTable != null)
					{
						int randomRange = ObjUtil.GetRandomRange100();
						int tableItemData = enemyExtendItemTable.GetTableItemData(EnemyExtendItemTableItem.BronzeTimer);
						int num = tableItemData + enemyExtendItemTable.GetTableItemData(EnemyExtendItemTableItem.SilverTimer);
						int num2 = num + enemyExtendItemTable.GetTableItemData(EnemyExtendItemTableItem.GoldTimer);
						if (randomRange < tableItemData)
						{
							return TimerType.BRONZE;
						}
						if (randomRange < num)
						{
							return TimerType.SILVER;
						}
						if (randomRange < num2)
						{
							return TimerType.GOLD;
						}
					}
				}
			}
		}
		return TimerType.ERROR;
	}

	// Token: 0x06003DB4 RID: 15796 RVA: 0x00142034 File Offset: 0x00140234
	private void CreateAnimal()
	{
		if (this.m_heabyBomFlag)
		{
			ObjAnimalUtil.CreateAnimal(base.gameObject, AnimalType.FLICKY);
			this.m_heabyBomFlag = false;
		}
		else
		{
			ObjAnimalUtil.CreateAnimal(base.gameObject);
		}
	}

	// Token: 0x06003DB5 RID: 15797 RVA: 0x00142070 File Offset: 0x00140270
	private void SetPlayerBroken(bool metal_type, uint attribute_state)
	{
		int num = this.GetScore();
		if (metal_type && StageAbilityManager.Instance.HasChaoAbility(ChaoAbility.COMBO_METAL_AND_METAL_SCORE))
		{
			num = (int)StageAbilityManager.Instance.GetChaoAbilityExtraValue(ChaoAbility.COMBO_METAL_AND_METAL_SCORE);
		}
		if (StageAbilityManager.Instance != null && StageAbilityManager.Instance.HasChaoAbility(ChaoAbility.ENEMY_SCORE_SEVERALFOLD))
		{
			float chaoAbilityValue = StageAbilityManager.Instance.GetChaoAbilityValue(ChaoAbility.ENEMY_SCORE_SEVERALFOLD);
			float num2 = UnityEngine.Random.Range(0f, 99.9f);
			if (chaoAbilityValue >= num2)
			{
				num *= (int)StageAbilityManager.Instance.GetChaoAbilityExtraValue(ChaoAbility.ENEMY_SCORE_SEVERALFOLD);
				ObjUtil.RequestStartAbilityToChao(ChaoAbility.ENEMY_SCORE_SEVERALFOLD, false);
			}
		}
		List<ChaoAbility> list = new List<ChaoAbility>();
		list.Add(ChaoAbility.ENEMY_SCORE);
		ObjUtil.RequestStartAbilityToChao(ChaoAbility.ENEMY_SCORE, true);
		ObjUtil.GetChaoAbliltyPhantomFlag(attribute_state, ref list);
		num = ObjUtil.GetChaoAndEnemyScore(list, num);
		ObjUtil.SendMessageAddScore(num);
		ObjUtil.SendMessageScoreCheck(new StageScoreData(1, num));
		ObjUtil.SendMessageMission(Mission.EventID.ENEMYDEAD, 1);
		if (this.IsRare())
		{
			ObjUtil.SendMessageMission(Mission.EventID.GOLDENENEMYDEAD, 1);
		}
		ObjUtil.SendMessageTutorialClear(Tutorial.EventID.ENEMY);
		if (StageAbilityManager.Instance != null && StageAbilityManager.Instance.HasChaoAbility(ChaoAbility.ENEMY_COUNT_BOMB))
		{
			GameObjectUtil.SendMessageToTagObjects("StageManager", "OnChaoAbilityEnemyBreak", null, SendMessageOptions.DontRequireReceiver);
		}
		this.SetBroken();
	}

	// Token: 0x06003DB6 RID: 15798 RVA: 0x00142198 File Offset: 0x00140398
	private void SetBroken()
	{
		if (this.m_destroyFlag)
		{
			return;
		}
		this.PlayHitEffect();
		ObjUtil.LightPlaySE(ObjEnemyUtil.GetSEName(this.m_enmyType), "SE");
		this.m_destroyFlag = true;
	}

	// Token: 0x0400354D RID: 13645
	private bool m_rightAnimFlag;

	// Token: 0x0400354E RID: 13646
	private bool m_setupAnimFlag;

	// Token: 0x0400354F RID: 13647
	private bool m_destroyFlag;

	// Token: 0x04003550 RID: 13648
	private bool m_heabyBomFlag;

	// Token: 0x04003551 RID: 13649
	private bool m_end;

	// Token: 0x04003552 RID: 13650
	private ObjEnemyUtil.EnemyType m_enmyType;
}
