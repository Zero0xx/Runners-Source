using System;
using Message;
using UnityEngine;

namespace Boss
{
	// Token: 0x0200084E RID: 2126
	public class ObjBossEventBossState : ObjBossState
	{
		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060039FC RID: 14844 RVA: 0x00131330 File Offset: 0x0012F530
		public ObjBossEventBossParameter BossParam
		{
			get
			{
				return this.m_bossParam;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060039FD RID: 14845 RVA: 0x00131338 File Offset: 0x0012F538
		public ObjBossEventBossEffect BossEffect
		{
			get
			{
				return this.m_bossEffect;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060039FE RID: 14846 RVA: 0x00131340 File Offset: 0x0012F540
		public ObjBossEventBossMotion BossMotion
		{
			get
			{
				return this.m_bossMotion;
			}
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x00131348 File Offset: 0x0012F548
		protected override void OnStart()
		{
			this.OnInit();
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x00131350 File Offset: 0x0012F550
		protected override void OnInit()
		{
			if (this.m_bossParam == null)
			{
				this.m_bossParam = this.GetBossParam();
			}
			if (this.m_bossEffect == null)
			{
				this.m_bossEffect = this.GetBossEffect();
			}
			if (this.m_bossMotion == null)
			{
				this.m_bossMotion = this.GetBossMotion();
			}
			this.m_wispTime = 0f;
			this.m_wispTimeMax = 0f;
			this.m_currentBoostLevel = WispBoostLevel.NONE;
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x001313D4 File Offset: 0x0012F5D4
		private ObjBossEventBossParameter GetBossParam()
		{
			return base.GetComponent<ObjBossEventBossParameter>();
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x001313DC File Offset: 0x0012F5DC
		private ObjBossEventBossEffect GetBossEffect()
		{
			return base.GetComponent<ObjBossEventBossEffect>();
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x001313E4 File Offset: 0x0012F5E4
		private ObjBossEventBossMotion GetBossMotion()
		{
			return base.GetComponent<ObjBossEventBossMotion>();
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x001313EC File Offset: 0x0012F5EC
		protected override ObjBossParameter OnGetBossParam()
		{
			return this.GetBossParam();
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x001313F4 File Offset: 0x0012F5F4
		protected override ObjBossEffect OnGetBossEffect()
		{
			return this.GetBossEffect();
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x001313FC File Offset: 0x0012F5FC
		protected override ObjBossMotion OnGetBossMotion()
		{
			return this.GetBossMotion();
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x00131404 File Offset: 0x0012F604
		protected override void OnChangeChara()
		{
			this.ResetWisp();
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x0013140C File Offset: 0x0012F60C
		protected override void OnSetup()
		{
			this.m_bossParam.Setup();
			this.m_bossMotion.Setup();
			this.MakeFSM();
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x0013142C File Offset: 0x0012F62C
		private void OnDestroy()
		{
			if (this.m_fsm != null && this.m_fsm.CurrentState != null)
			{
				this.m_fsm.CurrentState.Leave(this);
			}
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x00131468 File Offset: 0x0012F668
		protected override void OnFsmUpdate(float delta)
		{
			if (this.m_fsm != null && this.m_fsm.CurrentState != null)
			{
				this.m_fsm.CurrentState.Step(this, delta);
			}
			this.UpdateWisp(delta);
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x001314AC File Offset: 0x0012F6AC
		protected override void OnChangeDamageState()
		{
			this.ChangeState(this.m_damageState);
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x001314BC File Offset: 0x0012F6BC
		private void MakeFSM()
		{
			FSMState<ObjBossEventBossState>[] array = new FSMState<ObjBossEventBossState>[]
			{
				new BossStateAppearEvent1(),
				new BossStateAppearEvent2(),
				new BossStateAppearEvent1_2(),
				new BossStateAppearEvent2_2(),
				new BossStateAttackEvent1(),
				new BossStateAttackEvent2(),
				new BossStateDamageEvent1(),
				new BossStateDamageEvent2(),
				new BossStatePassEvent(),
				new BossStatePassEventDistanceEnd(),
				new BossStateDeadEvent()
			};
			this.m_fsm = new FSMSystem<ObjBossEventBossState>();
			int num = 0;
			foreach (FSMState<ObjBossEventBossState> s in array)
			{
				this.m_fsm.AddState(1 + num, s);
				num++;
			}
			base.SetSpeed(0f);
			this.m_fsm.Init(this, (int)this.m_initState);
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x00131588 File Offset: 0x0012F788
		public void ChangeState(EVENTBOSS_STATE_ID state)
		{
			this.m_fsm.ChangeState(this, (int)state);
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x00131598 File Offset: 0x0012F798
		public void SetInitState(EVENTBOSS_STATE_ID state)
		{
			this.m_initState = state;
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x001315A4 File Offset: 0x0012F7A4
		public void SetDamageState(EVENTBOSS_STATE_ID state)
		{
			this.m_damageState = state;
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x001315B0 File Offset: 0x0012F7B0
		private void OnGetWisp()
		{
			int num = (int)this.m_bossParam.BoostLevel;
			float num2 = this.m_bossParam.BoostRatio + this.m_bossParam.BoostRatioAdd;
			if ((double)num2 >= 1.0)
			{
				num2 = 1f;
				if (num < 2)
				{
					num++;
				}
			}
			this.SetBoostLevel((WispBoostLevel)num, num2);
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x0013160C File Offset: 0x0012F80C
		private void ResetWisp()
		{
			if (this.m_bossParam.BoostRatio > 0f)
			{
				this.m_bossParam.BoostRatio = 0f;
				this.m_bossParam.BoostLevel = WispBoostLevel.NONE;
				this.SetBoostLevel(this.m_bossParam.BoostLevel, this.m_bossParam.BoostRatio);
			}
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x00131668 File Offset: 0x0012F868
		public int GetDropRingAggressivity()
		{
			int result;
			if (base.ColorPowerHit || base.ChaoHit)
			{
				result = 1;
			}
			else
			{
				result = ObjUtil.GetChaoAbliltyValue(ChaoAbility.AGGRESSIVITY_UP_FOR_RAID_BOSS, (int)this.m_bossParam.ChallengeValue);
			}
			return result;
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x001316AC File Offset: 0x0012F8AC
		private void BoostMeter()
		{
			MsgCaution caution = new MsgCaution(HudCaution.Type.WISPBOOST, this.m_bossParam);
			HudCaution.Instance.SetCaution(caution);
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x001316D4 File Offset: 0x0012F8D4
		private void UpdateWisp(float delta)
		{
			this.m_wispTime += delta;
			if (this.m_wispTime > this.m_wispTimeMax)
			{
				this.m_wispTime = 0f;
				this.m_wispTimeMax = this.m_bossParam.WispInterspace;
				float y = UnityEngine.Random.Range(0.5f, 3f);
				Vector3 pos = new Vector3(base.GetPlayerPosition().x + 15f, y, base.transform.position.z);
				this.CreateWisp(pos);
			}
			if (this.m_bossParam.BoostRatio > 0f)
			{
				this.m_bossParam.BoostRatio -= delta * this.m_bossParam.BoostRatioDown;
				if (this.m_bossParam.BoostRatio <= 0f)
				{
					this.m_bossParam.BoostRatio = 0f;
					this.m_bossParam.BoostLevel = WispBoostLevel.NONE;
					this.SetBoostLevel(this.m_bossParam.BoostLevel, this.m_bossParam.BoostRatio);
				}
			}
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x001317E4 File Offset: 0x0012F9E4
		private void CreateWisp(Vector3 pos)
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.EVENT_RESOURCE, "ObjBossWisp");
			if (gameObject != null)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, pos, Quaternion.identity) as GameObject;
				if (gameObject2)
				{
					gameObject2.gameObject.SetActive(true);
					ObjBossWisp component = gameObject2.GetComponent<ObjBossWisp>();
					if (component)
					{
						float speed = UnityEngine.Random.Range(this.m_bossParam.WispSpeedMin, this.m_bossParam.WispSpeedMax);
						float num = UnityEngine.Random.Range(this.m_bossParam.WispSwingMin, this.m_bossParam.WispSwingMax);
						float addX = UnityEngine.Random.Range(this.m_bossParam.WispAddXMin, this.m_bossParam.WispAddXMax);
						float num2 = pos.y - num;
						if (num2 < 0f)
						{
							num = pos.y;
						}
						component.Setup(base.gameObject, speed, num, addX);
					}
				}
			}
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x001318D4 File Offset: 0x0012FAD4
		private void SetBoostLevel(WispBoostLevel level, float ratio)
		{
			bool flag = false;
			if (this.m_currentBoostLevel != level)
			{
				flag = true;
				this.m_currentBoostLevel = level;
			}
			this.m_bossParam.BoostLevel = level;
			this.m_bossParam.BoostRatio = ratio;
			if (flag)
			{
				if (level == WispBoostLevel.NONE)
				{
					this.m_bossParam.BossAttackPower = 1;
				}
				else
				{
					this.m_bossParam.BossAttackPower = this.m_bossParam.GetBoostAttackParam(level);
					ObjUtil.PlayEventSE(ObjBossEventBossState.GetBoostSE(level), EventManager.EventType.RAID_BOSS);
				}
				this.BoostMeter();
				this.SendPlayerBoostLevel(level);
			}
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x00131960 File Offset: 0x0012FB60
		private void SendPlayerBoostLevel(WispBoostLevel level)
		{
			string boostEffect = ObjBossEventBossEffect.GetBoostEffect(level);
			GameObjectUtil.SendMessageToTagObjects("Player", "OnBossBoostLevel", new MsgBossBoostLevel(level, boostEffect), SendMessageOptions.DontRequireReceiver);
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x0013198C File Offset: 0x0012FB8C
		private static string GetBoostSE(WispBoostLevel level)
		{
			if ((ulong)level < (ulong)((long)ObjBossEventBossState.BOOST_SE_NAME.Length))
			{
				return ObjBossEventBossState.BOOST_SE_NAME[(int)level];
			}
			return string.Empty;
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x001319AC File Offset: 0x0012FBAC
		private void OnPlayerDamage(MsgBossPlayerDamage msg)
		{
			bool flag = this.m_damageWispCancel;
			if (msg.m_dead)
			{
				flag = true;
			}
			if (flag && this.m_bossParam.BoostLevel != WispBoostLevel.NONE)
			{
				this.m_bossParam.BoostRatio = 0f;
				this.m_bossParam.BoostLevel = WispBoostLevel.NONE;
				this.SetBoostLevel(this.m_bossParam.BoostLevel, this.m_bossParam.BoostRatio);
			}
		}

		// Token: 0x04003078 RID: 12408
		private const float WISPSTART_TIME = 0f;

		// Token: 0x04003079 RID: 12409
		private const float WISP_POSY_MIN = 0.5f;

		// Token: 0x0400307A RID: 12410
		private const float WISP_POSY_MAX = 3f;

		// Token: 0x0400307B RID: 12411
		private const float WISP_POSX = 15f;

		// Token: 0x0400307C RID: 12412
		private bool m_damageWispCancel = true;

		// Token: 0x0400307D RID: 12413
		private FSMSystem<ObjBossEventBossState> m_fsm;

		// Token: 0x0400307E RID: 12414
		private EVENTBOSS_STATE_ID m_initState = EVENTBOSS_STATE_ID.AppearEvent1;

		// Token: 0x0400307F RID: 12415
		private EVENTBOSS_STATE_ID m_damageState = EVENTBOSS_STATE_ID.DamageEvent1;

		// Token: 0x04003080 RID: 12416
		private ObjBossEventBossParameter m_bossParam;

		// Token: 0x04003081 RID: 12417
		private ObjBossEventBossEffect m_bossEffect;

		// Token: 0x04003082 RID: 12418
		private ObjBossEventBossMotion m_bossMotion;

		// Token: 0x04003083 RID: 12419
		private float m_wispTime;

		// Token: 0x04003084 RID: 12420
		private float m_wispTimeMax;

		// Token: 0x04003085 RID: 12421
		private WispBoostLevel m_currentBoostLevel = WispBoostLevel.NONE;

		// Token: 0x04003086 RID: 12422
		private static readonly string[] BOOST_SE_NAME = new string[]
		{
			"rb_boost_1",
			"rb_boost_2",
			"rb_boost_3"
		};
	}
}
