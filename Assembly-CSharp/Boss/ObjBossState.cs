using System;
using Message;
using UnityEngine;

namespace Boss
{
	// Token: 0x02000846 RID: 2118
	public class ObjBossState : MonoBehaviour
	{
		// Token: 0x0600397F RID: 14719 RVA: 0x0012F754 File Offset: 0x0012D954
		private void Start()
		{
			this.SetBossStateAttackOK(false);
			this.OnStart();
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x0012F764 File Offset: 0x0012D964
		protected virtual void OnStart()
		{
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x0012F768 File Offset: 0x0012D968
		public void Init()
		{
			if (this.m_param == null)
			{
				this.m_param = this.OnGetBossParam();
			}
			if (this.m_effect == null)
			{
				this.m_effect = this.OnGetBossEffect();
			}
			if (this.m_motion == null)
			{
				this.m_motion = this.OnGetBossMotion();
			}
			this.OnInit();
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x0012F7D4 File Offset: 0x0012D9D4
		protected virtual void OnInit()
		{
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x0012F7D8 File Offset: 0x0012D9D8
		protected virtual ObjBossParameter OnGetBossParam()
		{
			return null;
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x0012F7DC File Offset: 0x0012D9DC
		protected virtual ObjBossEffect OnGetBossEffect()
		{
			return null;
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x0012F7E0 File Offset: 0x0012D9E0
		protected virtual ObjBossMotion OnGetBossMotion()
		{
			return null;
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x0012F7E4 File Offset: 0x0012D9E4
		protected virtual void OnChangeChara()
		{
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x0012F7E8 File Offset: 0x0012D9E8
		public void Setup()
		{
			this.m_playerInfo = ObjUtil.GetPlayerInformation();
			this.m_levelInfo = ObjUtil.GetLevelInformation();
			if (this.m_levelInfo != null)
			{
				this.m_levelInfo.BossEndTime = (float)this.m_param.BossDistance;
			}
			this.OnSetup();
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x0012F83C File Offset: 0x0012DA3C
		protected virtual void OnSetup()
		{
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x0012F840 File Offset: 0x0012DA40
		private void OnDestroy()
		{
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x0012F844 File Offset: 0x0012DA44
		private void Update()
		{
			if (this.m_playerInfo != null)
			{
				if (!this.m_playerDead)
				{
					if (this.m_playerInfo.IsDead())
					{
						GameObject[] array = GameObject.FindGameObjectsWithTag("Gimmick");
						foreach (GameObject gameObject in array)
						{
							gameObject.SendMessage("OnMsgNotifyDead", new MsgNotifyDead(), SendMessageOptions.DontRequireReceiver);
						}
						this.m_playerDead = true;
					}
				}
				else if (this.m_playerChange && !this.m_playerInfo.IsDead())
				{
					this.m_playerDead = false;
				}
			}
			float deltaTime = Time.deltaTime;
			this.OnFsmUpdate(deltaTime);
			this.m_hitBumper = false;
			this.UpdateMove(deltaTime);
			this.DebugDrawInfo();
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x0012F908 File Offset: 0x0012DB08
		protected virtual void OnFsmUpdate(float delta)
		{
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x0012F90C File Offset: 0x0012DB0C
		public void UpdateMove(float delta)
		{
			if (this.m_speedKeep && !this.m_phantom)
			{
				base.transform.position = new Vector3(this.GetPlayerPosition().x + this.m_keepDistance, base.transform.position.y, base.transform.position.z);
			}
			else
			{
				Vector3 playerPosition = this.GetPlayerPosition();
				Vector3 prevPlayerPos = this.m_prevPlayerPos;
				if (this.m_playerDead || prevPlayerPos != playerPosition)
				{
					float keepDistance = this.m_keepDistance;
					float playerBossPositionX = this.GetPlayerBossPositionX();
					float num = keepDistance - playerBossPositionX;
					if (Mathf.Abs(num) > 0.01f && this.m_param.PlayerSpeed > this.m_param.Speed)
					{
						float num2 = num / this.m_param.PlayerSpeed;
						float num3 = this.m_param.Speed * num2;
						this.m_keepDistance = playerBossPositionX + num3;
					}
					else
					{
						float num4 = this.m_param.Speed * delta;
						this.m_keepDistance = playerBossPositionX + num4 + this.m_moveAddStep;
					}
					this.m_moveStep -= this.m_moveAddStep;
					if (this.m_moveStep < 0f)
					{
						this.m_moveStep = 0f;
						this.m_moveAddStep = 0f;
					}
				}
				else
				{
					float num5 = this.m_param.Speed * delta;
					this.m_moveStep += num5;
					this.m_moveAddStep = this.m_moveStep * 0.05f;
				}
				this.m_prevPlayerPos = playerPosition;
				base.transform.position = new Vector3(this.GetPlayerPosition().x + this.m_keepDistance, base.transform.position.y, base.transform.position.z);
			}
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x0012FAF8 File Offset: 0x0012DCF8
		public void UpdateSpeedDown(float delta, float down)
		{
			this.m_speedKeep = false;
			this.m_param.Speed -= delta * down;
			if (this.m_param.Speed < this.m_param.MinSpeed)
			{
				this.m_param.Speed = this.m_param.MinSpeed;
			}
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x0012FB54 File Offset: 0x0012DD54
		public void UpdateSpeedUp(float delta, float up)
		{
			this.m_speedKeep = false;
			this.m_param.Speed += delta * up;
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x0012FB74 File Offset: 0x0012DD74
		public void SetSpeed(float speed)
		{
			this.m_speedKeep = false;
			this.m_param.Speed = speed;
			this.m_keepDistance = this.GetPlayerBossPositionX();
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x0012FB98 File Offset: 0x0012DD98
		public void KeepSpeed()
		{
			this.m_speedKeep = true;
			this.m_param.Speed = this.m_param.PlayerSpeed;
			this.m_keepDistance = this.GetPlayerBossPositionX();
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x0012FBC4 File Offset: 0x0012DDC4
		public void SetupMoveY(float step)
		{
			this.m_param.StepMoveY = step;
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x0012FBD4 File Offset: 0x0012DDD4
		public void UpdateMoveY(float delta, float pos_y, float speed)
		{
			this.m_param.StepMoveY -= delta * this.m_param.StepMoveY * 0.5f * speed;
			if (this.m_param.StepMoveY < 0.01f)
			{
				this.m_param.StepMoveY = 0f;
			}
			Vector3 zero = Vector3.zero;
			Vector3 target = new Vector3(base.transform.position.x, pos_y, base.transform.position.z);
			base.transform.position = Vector3.SmoothDamp(base.transform.position, target, ref zero, this.m_param.StepMoveY);
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x0012FC8C File Offset: 0x0012DE8C
		public float GetPlayerDistance()
		{
			return Mathf.Abs(this.GetPlayerBossPositionX());
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x0012FC9C File Offset: 0x0012DE9C
		public Vector3 GetPlayerPosition()
		{
			if (this.m_playerInfo != null)
			{
				return this.m_playerInfo.Position;
			}
			return Vector3.zero;
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x0012FCCC File Offset: 0x0012DECC
		public float GetPlayerBossPositionX()
		{
			return base.transform.position.x - this.GetPlayerPosition().x;
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x0012FCFC File Offset: 0x0012DEFC
		public void DebugDrawState(string name)
		{
			if (this.m_debugDrawState)
			{
				global::Debug.Log("BossState(" + name + ")");
			}
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x0012FD2C File Offset: 0x0012DF2C
		public void SetHitCheck(bool flag)
		{
			if (this.m_hitCheck != flag)
			{
				this.m_hitCheck = flag;
				this.SetBossStateAttackOK(flag);
			}
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x0012FD48 File Offset: 0x0012DF48
		public bool IsBossDistanceEnd()
		{
			return this.m_bossDistanceEnd;
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x0012FD50 File Offset: 0x0012DF50
		public bool IsPlayerDead()
		{
			return this.m_playerDead;
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x0012FD58 File Offset: 0x0012DF58
		public bool IsHitBumper()
		{
			return this.m_hitBumper;
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x0012FD60 File Offset: 0x0012DF60
		public bool IsClear()
		{
			return this.m_clear;
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x0600399D RID: 14749 RVA: 0x0012FD74 File Offset: 0x0012DF74
		// (set) Token: 0x0600399C RID: 14748 RVA: 0x0012FD68 File Offset: 0x0012DF68
		public bool ColorPowerHit
		{
			get
			{
				return this.m_colorPowerHit;
			}
			set
			{
				this.m_colorPowerHit = value;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x0600399F RID: 14751 RVA: 0x0012FD88 File Offset: 0x0012DF88
		// (set) Token: 0x0600399E RID: 14750 RVA: 0x0012FD7C File Offset: 0x0012DF7C
		public bool ChaoHit
		{
			get
			{
				return this.m_chaoHit;
			}
			set
			{
				this.m_chaoHit = value;
			}
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x0012FD90 File Offset: 0x0012DF90
		public void AddDamage()
		{
			int hpDown = ObjUtil.GetChaoAbliltyValue(ChaoAbility.AGGRESSIVITY_UP_FOR_RAID_BOSS, this.m_param.BossAttackPower);
			if (this.ColorPowerHit || this.ChaoHit)
			{
				hpDown = 1;
			}
			this.SetHpDown(hpDown);
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x0012FDD0 File Offset: 0x0012DFD0
		private void SetHpDown(int addDamage)
		{
			int count = this.m_param.BossHP;
			this.m_param.BossHP -= addDamage;
			if (this.m_param.BossHP < 0)
			{
				this.m_param.BossHP = 0;
			}
			else
			{
				count = addDamage;
			}
			if (this.m_param.TypeBoss != 0 && this.m_levelInfo != null)
			{
				this.m_levelInfo.AddNumBossAttack(count);
			}
			this.SetHpGauge(this.m_param.BossHP);
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x0012FE60 File Offset: 0x0012E060
		public void ChaoHpDown()
		{
			int bossHP = this.m_param.BossHP;
			int num = bossHP - ObjUtil.GetChaoAbliltyValue(ChaoAbility.MAP_BOSS_DAMAGE, bossHP);
			if (num > 0)
			{
				this.SetHpDown(num);
			}
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x0012FE94 File Offset: 0x0012E094
		public void RequestStartChaoAbility()
		{
			if (EventManager.Instance != null && !EventManager.Instance.IsRaidBossStage())
			{
				ObjUtil.RequestStartAbilityToChao(ChaoAbility.BOSS_SUPER_RING_RATE, true);
				ObjUtil.RequestStartAbilityToChao(ChaoAbility.BOSS_RED_RING_RATE, true);
			}
			BossType typeBoss = (BossType)this.m_param.TypeBoss;
			if (typeBoss != BossType.FEVER)
			{
				ObjUtil.RequestStartAbilityToChao(ChaoAbility.MAP_BOSS_DAMAGE, true);
				this.ChaoHpDown();
			}
			else
			{
				ObjUtil.RequestStartAbilityToChao(ChaoAbility.BOSS_STAGE_TIME, true);
			}
			this.m_effect.PlayChaoEffect();
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x0012FF18 File Offset: 0x0012E118
		public void BossEnd(bool dead)
		{
			bool flag = false;
			if (StageTutorialManager.Instance && !StageTutorialManager.Instance.IsCompletedTutorial())
			{
				flag = true;
			}
			if (flag)
			{
				GameObjectUtil.SendMessageFindGameObject("StageTutorialManager", "OnMsgTutorialEnd", new MsgTutorialEnd(), SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				if (this.m_param.TypeBoss != 0)
				{
					this.AddStockRing();
				}
				MsgBossEnd value = new MsgBossEnd(dead);
				GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnBossEnd", value, SendMessageOptions.DontRequireReceiver);
			}
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x0012FF98 File Offset: 0x0012E198
		public void BossClear()
		{
			this.m_clear = true;
			MsgBossClear value = new MsgBossClear();
			GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnBossClear", value, SendMessageOptions.DontRequireReceiver);
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x0012FFC4 File Offset: 0x0012E1C4
		public void SetBossStateAttackOK(bool flag)
		{
			if (this.m_bossDistanceEndArea)
			{
				return;
			}
			if (flag && this.m_param.AfterAttack)
			{
				if (ObjUtil.RequestStartAbilityToChao(ChaoAbility.PURSUES_TO_BOSS_AFTER_ATTACK, false))
				{
					flag = false;
				}
				else
				{
					this.m_param.AfterAttack = false;
				}
			}
			bool flag2 = false;
			if (flag)
			{
				flag2 = ObjUtil.RequestStartAbilityToChao(ChaoAbility.BOSS_ATTACK, false);
			}
			else
			{
				ObjUtil.RequestEndAbilityToChao(ChaoAbility.BOSS_ATTACK);
			}
			MsgBossCheckState.State state = MsgBossCheckState.State.IDLE;
			if (flag && !flag2)
			{
				state = MsgBossCheckState.State.ATTACK_OK;
			}
			if (StageItemManager.Instance != null)
			{
				MsgBossCheckState msg = new MsgBossCheckState(state);
				StageItemManager.Instance.OnMsgBossCheckState(msg);
			}
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x00130064 File Offset: 0x0012E264
		public void UpdateBossStateAfterAttack()
		{
			this.m_param.AfterAttack = !this.m_param.AfterAttack;
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x00130080 File Offset: 0x0012E280
		public void CreateFeverRing()
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_PREFAB, "ObjFeverRing");
			if (gameObject != null)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, base.transform.position, Quaternion.identity) as GameObject;
				if (gameObject2)
				{
					gameObject2.gameObject.SetActive(true);
					ObjFeverRing component = gameObject2.GetComponent<ObjFeverRing>();
					if (component)
					{
						component.Setup(this.m_param.RingCount, this.m_param.SuperRingRatio, this.m_param.RedStarRingRatio, this.m_param.BronzeTimerRatio, this.m_param.SilverTimerRatio, this.m_param.GoldTimerRatio, (BossType)this.m_param.TypeBoss);
					}
				}
			}
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x00130144 File Offset: 0x0012E344
		public void CreateEventFeverRing(int playerAggressivity)
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.OBJECT_PREFAB, "ObjFeverRing");
			if (gameObject != null)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, base.transform.position, Quaternion.identity) as GameObject;
				if (gameObject2)
				{
					int num = EventBossParamTable.GetSuperRingDropData((BossType)this.m_param.TypeBoss, playerAggressivity);
					gameObject2.gameObject.SetActive(true);
					ObjFeverRing component = gameObject2.GetComponent<ObjFeverRing>();
					if (component)
					{
						if (this.m_param.RedStarRingRatio + num > 100)
						{
							num = 100 - this.m_param.RedStarRingRatio;
						}
						component.Setup(this.m_param.RingCount, num, this.m_param.RedStarRingRatio, this.m_param.BronzeTimerRatio, this.m_param.SilverTimerRatio, this.m_param.GoldTimerRatio, (BossType)this.m_param.TypeBoss);
					}
				}
			}
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x00130234 File Offset: 0x0012E434
		public void CreateMissile(Vector3 pos)
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.ENEMY_PREFAB, "ObjBossMissile");
			if (gameObject != null)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, pos, Quaternion.identity) as GameObject;
				if (gameObject2)
				{
					gameObject2.gameObject.SetActive(true);
					ObjBossMissile component = gameObject2.GetComponent<ObjBossMissile>();
					if (component)
					{
						component.Setup(base.gameObject, this.m_param.MissileSpeed, (BossType)this.m_param.TypeBoss);
					}
				}
			}
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x001302BC File Offset: 0x0012E4BC
		public void CreateTrapBall(Vector3 colli, float attackPos, int randBall, bool bossAppear)
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.ENEMY_PREFAB, "ObjBossTrapBall");
			if (gameObject != null)
			{
				Vector3 position = base.transform.position;
				if (!bossAppear)
				{
					position = new Vector3(this.GetPlayerPosition().x + 18f, attackPos, position.z);
				}
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, position, Quaternion.identity) as GameObject;
				if (gameObject2)
				{
					gameObject2.gameObject.SetActive(true);
					ObjBossTrapBall component = gameObject2.GetComponent<ObjBossTrapBall>();
					if (component)
					{
						bool flag = true;
						if (this.m_param.AttackBallFlag)
						{
							flag = false;
							this.m_param.AttackBallFlag = false;
						}
						else
						{
							int randomRange = ObjUtil.GetRandomRange100();
							if (randomRange >= randBall && this.m_param.AttackTrapCount < this.m_param.AttackTrapCountMax)
							{
								flag = false;
							}
						}
						BossTrapBallType type;
						if (!flag)
						{
							type = BossTrapBallType.BREAK;
							this.m_param.AttackTrapCount++;
						}
						else
						{
							type = BossTrapBallType.ATTACK;
							this.m_param.AttackBallFlag = true;
							this.m_param.AttackTrapCount = 0;
						}
						component.Setup(base.gameObject, colli, this.m_param.RotSpeed, this.m_param.AttackSpeed, type, (BossType)this.m_param.TypeBoss, bossAppear);
					}
				}
			}
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x00130420 File Offset: 0x0012E620
		public GameObject CreateBom(bool colli, float shot_speed, bool shot)
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.ENEMY_PREFAB, "ObjBossBom");
			if (gameObject != null)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, ObjBossUtil.GetBossHatchPos(base.gameObject), Quaternion.identity) as GameObject;
				if (gameObject2)
				{
					gameObject2.gameObject.SetActive(true);
					ObjBossBom component = gameObject2.GetComponent<ObjBossBom>();
					if (component)
					{
						component.Setup(base.gameObject, colli, this.GetShotRotation(this.m_param.ShotRotBase), shot_speed, this.m_param.AddSpeedRatio, shot);
					}
					return gameObject2;
				}
			}
			return null;
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x001304C0 File Offset: 0x0012E6C0
		public void BlastBom(GameObject bom_obj)
		{
			if (bom_obj)
			{
				ObjBossBom component = bom_obj.GetComponent<ObjBossBom>();
				if (component)
				{
					component.Blast("ef_bo_em_dead_bom01", 5f);
				}
			}
		}

		// Token: 0x060039AE RID: 14766 RVA: 0x001304FC File Offset: 0x0012E6FC
		public void ShotBom(GameObject bom_obj)
		{
			if (bom_obj)
			{
				ObjBossBom component = bom_obj.GetComponent<ObjBossBom>();
				if (component)
				{
					component.SetShot(true);
				}
			}
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x00130530 File Offset: 0x0012E730
		public void CreateBumper(bool bossAppear, float addX = 0f)
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.ENEMY_PREFAB, "ObjBossBall");
			if (gameObject != null)
			{
				Vector3 position = Vector3.zero;
				if (!bossAppear)
				{
					position = new Vector3(this.GetPlayerPosition().x + addX, base.transform.position.y, base.transform.position.z);
				}
				else
				{
					position = ObjBossUtil.GetBossHatchPos(base.gameObject);
				}
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, position, Quaternion.identity) as GameObject;
				if (gameObject2)
				{
					gameObject2.gameObject.SetActive(true);
					ObjBossBall component = gameObject2.GetComponent<ObjBossBall>();
					if (component)
					{
						component.Setup(new ObjBossBall.SetData
						{
							obj = base.gameObject,
							bound_param = this.GetBoundParam(),
							type = BossBallType.BUMPER,
							shot_rot = this.GetShotRotation(this.m_param.ShotRotBase),
							shot_speed = this.m_param.ShotSpeed,
							attack_speed = 0f,
							firstSpeed = this.m_param.BumperFirstSpeed,
							outOfcontrol = this.m_param.BumperOutOfcontrol,
							ballSpeed = this.m_param.BallSpeed,
							bossAppear = bossAppear
						});
					}
				}
			}
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x00130698 File Offset: 0x0012E898
		public Quaternion GetShotRotation(Vector3 rot_angle)
		{
			float num = 0f;
			if (this.m_param.Speed > 0f)
			{
				num = this.m_param.Speed / this.m_param.PlayerSpeed;
			}
			float num2 = 30f * num * this.m_param.AddSpeedRatio;
			if (num2 > 60f)
			{
				num2 = 60f;
			}
			Vector3 euler = rot_angle * num2;
			return base.transform.rotation * Quaternion.FromToRotation(base.transform.up, -base.transform.up) * Quaternion.Euler(euler);
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x00130740 File Offset: 0x0012E940
		public void OpenHpGauge()
		{
			if (ObjBossUtil.IsNowLastChance(this.m_playerInfo))
			{
				return;
			}
			GameObjectUtil.SendMessageFindGameObject("HudCockpit", "HudBossHpGaugeOpen", new MsgHudBossHpGaugeOpen((BossType)this.m_param.TypeBoss, this.m_param.BossLevel, this.m_param.BossHP, this.m_param.BossHPMax), SendMessageOptions.DontRequireReceiver);
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x001307A0 File Offset: 0x0012E9A0
		public void StartGauge()
		{
			if (ObjBossUtil.IsNowLastChance(this.m_playerInfo))
			{
				return;
			}
			GameObjectUtil.SendMessageFindGameObject("HudCockpit", "HudBossGaugeStart", new MsgHudBossGaugeStart(), SendMessageOptions.DontRequireReceiver);
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x001307CC File Offset: 0x0012E9CC
		public void SetHpGauge(int hp)
		{
			if (ObjBossUtil.IsNowLastChance(this.m_playerInfo))
			{
				return;
			}
			GameObjectUtil.SendMessageFindGameObject("HudCockpit", "HudBossHpGaugeSet", new MsgHudBossHpGaugeSet(this.m_param.BossHP, this.m_param.BossHPMax), SendMessageOptions.DontRequireReceiver);
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x00130818 File Offset: 0x0012EA18
		public void SetClear()
		{
			if (HudCaution.Instance != null)
			{
				MsgCaution caution = new MsgCaution(HudCaution.Type.MAP_BOSS_CLEAR);
				HudCaution.Instance.SetCaution(caution);
			}
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x00130848 File Offset: 0x0012EA48
		public void SetFailed()
		{
			if (HudCaution.Instance != null)
			{
				MsgCaution caution = new MsgCaution(HudCaution.Type.MAP_BOSS_FAILED);
				HudCaution.Instance.SetCaution(caution);
				ObjUtil.PlaySE("boss_failed", "SE");
			}
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x00130888 File Offset: 0x0012EA88
		private void AddStockRing()
		{
			ObjUtil.SendMessageTransferRing();
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x00130890 File Offset: 0x0012EA90
		public float GetBoundParam()
		{
			int randomRange = ObjUtil.GetRandomRange100();
			if (randomRange < this.m_param.BoundMaxRand)
			{
				return this.GetBoundParam(this.m_param.BoundParamMax);
			}
			return this.GetBoundParam(this.m_param.BoundParamMin);
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x001308D8 File Offset: 0x0012EAD8
		public float GetBoundParam(float param)
		{
			return param;
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x001308DC File Offset: 0x0012EADC
		public float GetAttackInterspace()
		{
			return UnityEngine.Random.Range(this.m_param.AttackInterspaceMin, this.m_param.AttackInterspaceMax);
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x001308FC File Offset: 0x0012EAFC
		public float GetDamageSpeedParam()
		{
			float playerDistance = this.GetPlayerDistance();
			float num = 1f - playerDistance * 0.04f;
			if (num < 0.5f)
			{
				num = 0.5f;
			}
			return num;
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x00130938 File Offset: 0x0012EB38
		public void OnMsgBossDistanceEnd(MsgBossDistanceEnd msg)
		{
			if (msg.m_end)
			{
				this.m_bossDistanceEnd = true;
			}
			else
			{
				this.m_bossDistanceEndArea = true;
			}
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x00130958 File Offset: 0x0012EB58
		private void OnTriggerEnter(Collider other)
		{
			GameObject gameObject = other.gameObject;
			if (gameObject)
			{
				string a = LayerMask.LayerToName(gameObject.layer);
				if (a == "Player")
				{
					bool flag = false;
					if (gameObject.name == "ChaoPartsAttackEnemy" || gameObject.name.Contains("pha_"))
					{
						flag = true;
					}
					else if (this.m_hitCheck)
					{
						flag = true;
					}
					if (flag)
					{
						MsgHitDamage value = new MsgHitDamage(base.gameObject, AttackPower.PlayerSpin);
						gameObject.SendMessage("OnDamageHit", value, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
			if (this.m_hitCheck)
			{
				this.m_effect.SetHitOffset(gameObject.transform.position - base.transform.position);
			}
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x00130A24 File Offset: 0x0012EC24
		private void OnDamageHit(MsgHitDamage msg)
		{
			if (msg.m_sender)
			{
				GameObject gameObject = msg.m_sender.gameObject;
				if (gameObject && msg.m_attackPower > 0)
				{
					if (this.m_hitCheck)
					{
						MsgHitDamageSucceed value = new MsgHitDamageSucceed(base.gameObject, 0, ObjUtil.GetCollisionCenterPosition(base.gameObject), base.transform.rotation);
						gameObject.SendMessage("OnDamageSucceed", value, SendMessageOptions.DontRequireReceiver);
						if (gameObject.tag == "ChaoAttack" || gameObject.tag == "Chao")
						{
							this.ChaoHit = true;
						}
						if (msg.m_attackPower == 4)
						{
							this.ColorPowerHit = true;
						}
						this.OnChangeDamageState();
					}
					else if (gameObject.tag == "ChaoAttack" || gameObject.tag == "Chao" || msg.m_attackPower == 4)
					{
						MsgHitDamageSucceed value2 = new MsgHitDamageSucceed(base.gameObject, 0, ObjUtil.GetCollisionCenterPosition(base.gameObject), base.transform.rotation);
						gameObject.SendMessage("OnDamageSucceed", value2, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x00130B54 File Offset: 0x0012ED54
		protected virtual void OnChangeDamageState()
		{
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x00130B58 File Offset: 0x0012ED58
		public void OnTransformPhantom(MsgTransformPhantom msg)
		{
			this.m_phantom = true;
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x00130B64 File Offset: 0x0012ED64
		public void OnReturnFromPhantom(MsgReturnFromPhantom msg)
		{
			this.m_phantom = false;
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x00130B70 File Offset: 0x0012ED70
		public void OnChangeCharaSucceed(MsgChangeCharaSucceed msg)
		{
			this.m_playerChange = true;
			this.OnChangeChara();
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x00130B80 File Offset: 0x0012ED80
		public void OnMsgPrepareContinue(MsgPrepareContinue msg)
		{
			this.m_playerChange = true;
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x00130B8C File Offset: 0x0012ED8C
		public void OnMsgDebugDead()
		{
			if (this.m_param.BossHP > 0 && this.m_param.TypeBoss != 0 && this.m_levelInfo != null)
			{
				this.m_levelInfo.AddNumBossAttack(this.m_param.BossHP);
			}
			this.BossEnd(true);
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x00130BE8 File Offset: 0x0012EDE8
		public void OnHitBumper()
		{
			this.m_hitBumper = true;
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x00130BF4 File Offset: 0x0012EDF4
		private void DebugDrawInfo()
		{
			if (this.m_debugDrawInfo)
			{
				global::Debug.Log(string.Concat(new object[]
				{
					"BossInfo BossSpeed=",
					this.m_param.Speed,
					" PlayerSpeed=",
					this.m_param.PlayerSpeed,
					"AddSpeedRatio=",
					this.m_param.AddSpeedRatio,
					"AddSpeed=",
					this.m_param.AddSpeed
				}));
			}
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x00130C88 File Offset: 0x0012EE88
		public void DebugDrawInfo(string str)
		{
			if (this.m_debugDrawInfo)
			{
				global::Debug.Log("BossInfo " + str);
			}
		}

		// Token: 0x04003035 RID: 12341
		public bool m_debugDrawState;

		// Token: 0x04003036 RID: 12342
		public bool m_debugDrawInfo;

		// Token: 0x04003037 RID: 12343
		protected PlayerInformation m_playerInfo;

		// Token: 0x04003038 RID: 12344
		protected LevelInformation m_levelInfo;

		// Token: 0x04003039 RID: 12345
		private ObjBossParameter m_param;

		// Token: 0x0400303A RID: 12346
		private ObjBossEffect m_effect;

		// Token: 0x0400303B RID: 12347
		private ObjBossMotion m_motion;

		// Token: 0x0400303C RID: 12348
		private bool m_hitCheck;

		// Token: 0x0400303D RID: 12349
		private bool m_colorPowerHit;

		// Token: 0x0400303E RID: 12350
		private bool m_chaoHit;

		// Token: 0x0400303F RID: 12351
		private bool m_speedKeep;

		// Token: 0x04003040 RID: 12352
		private float m_keepDistance;

		// Token: 0x04003041 RID: 12353
		private bool m_bossDistanceEnd;

		// Token: 0x04003042 RID: 12354
		private bool m_bossDistanceEndArea;

		// Token: 0x04003043 RID: 12355
		private bool m_phantom;

		// Token: 0x04003044 RID: 12356
		private bool m_hitBumper;

		// Token: 0x04003045 RID: 12357
		private bool m_playerDead;

		// Token: 0x04003046 RID: 12358
		private bool m_playerChange;

		// Token: 0x04003047 RID: 12359
		private Vector3 m_prevPlayerPos = Vector3.zero;

		// Token: 0x04003048 RID: 12360
		private float m_moveStep;

		// Token: 0x04003049 RID: 12361
		private float m_moveAddStep;

		// Token: 0x0400304A RID: 12362
		private bool m_clear;
	}
}
