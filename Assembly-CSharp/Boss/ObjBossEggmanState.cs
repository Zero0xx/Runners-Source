using System;

namespace Boss
{
	// Token: 0x02000872 RID: 2162
	public class ObjBossEggmanState : ObjBossState
	{
		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06003A7A RID: 14970 RVA: 0x00135754 File Offset: 0x00133954
		public ObjBossEggmanParameter BossParam
		{
			get
			{
				return this.m_bossParam;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06003A7B RID: 14971 RVA: 0x0013575C File Offset: 0x0013395C
		public ObjBossEggmanEffect BossEffect
		{
			get
			{
				return this.m_bossEffect;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06003A7C RID: 14972 RVA: 0x00135764 File Offset: 0x00133964
		public ObjBossEggmanMotion BossMotion
		{
			get
			{
				return this.m_bossMotion;
			}
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x0013576C File Offset: 0x0013396C
		protected override void OnStart()
		{
			this.OnInit();
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x00135774 File Offset: 0x00133974
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
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x001357D8 File Offset: 0x001339D8
		private ObjBossEggmanParameter GetBossParam()
		{
			return base.GetComponent<ObjBossEggmanParameter>();
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x001357E0 File Offset: 0x001339E0
		private ObjBossEggmanEffect GetBossEffect()
		{
			return base.GetComponent<ObjBossEggmanEffect>();
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x001357E8 File Offset: 0x001339E8
		private ObjBossEggmanMotion GetBossMotion()
		{
			return base.GetComponent<ObjBossEggmanMotion>();
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x001357F0 File Offset: 0x001339F0
		protected override ObjBossParameter OnGetBossParam()
		{
			return this.GetBossParam();
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x001357F8 File Offset: 0x001339F8
		protected override ObjBossEffect OnGetBossEffect()
		{
			return this.GetBossEffect();
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x00135800 File Offset: 0x00133A00
		protected override ObjBossMotion OnGetBossMotion()
		{
			return this.GetBossMotion();
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x00135808 File Offset: 0x00133A08
		protected override void OnSetup()
		{
			this.m_bossParam.Setup();
			this.m_bossMotion.Setup();
			this.MakeFSM();
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x00135828 File Offset: 0x00133A28
		private void OnDestroy()
		{
			if (this.m_fsm != null && this.m_fsm.CurrentState != null)
			{
				this.m_fsm.CurrentState.Leave(this);
			}
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x00135864 File Offset: 0x00133A64
		protected override void OnFsmUpdate(float delta)
		{
			if (this.m_fsm != null && this.m_fsm.CurrentState != null)
			{
				this.m_fsm.CurrentState.Step(this, delta);
			}
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x00135894 File Offset: 0x00133A94
		protected override void OnChangeDamageState()
		{
			this.ChangeState(this.m_damageState);
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x001358A4 File Offset: 0x00133AA4
		private void MakeFSM()
		{
			FSMState<ObjBossEggmanState>[] array = new FSMState<ObjBossEggmanState>[]
			{
				new BossStateAppearFever(),
				new BossStateAppearMap1(),
				new BossStateAppearMap2(),
				new BossStateAppearMap2_2(),
				new BossStateAppearMap3(),
				new BossStateAttackFever(),
				new BossStateAttackMap1(),
				new BossStateAttackMap2(),
				new BossStateAttackMap3(),
				new BossStateDamageFever(),
				new BossStateDamageMap1(),
				new BossStateDamageMap2(),
				new BossStateDamageMap3(),
				new BossStatePassFever(),
				new BossStatePassFeverDistanceEnd(),
				new BossStatePassMap(),
				new BossStatePassMapDistanceEnd(),
				new BossStateDeadFever(),
				new BossStateDeadMap()
			};
			this.m_fsm = new FSMSystem<ObjBossEggmanState>();
			int num = 0;
			foreach (FSMState<ObjBossEggmanState> s in array)
			{
				this.m_fsm.AddState(1 + num, s);
				num++;
			}
			base.SetSpeed(0f);
			this.m_fsm.Init(this, (int)this.m_initState);
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x001359B8 File Offset: 0x00133BB8
		public void ChangeState(STATE_ID state)
		{
			this.m_fsm.ChangeState(this, (int)state);
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x001359C8 File Offset: 0x00133BC8
		public void SetInitState(STATE_ID state)
		{
			this.m_initState = state;
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x001359D4 File Offset: 0x00133BD4
		public void SetDamageState(STATE_ID state)
		{
			this.m_damageState = state;
		}

		// Token: 0x04003275 RID: 12917
		private FSMSystem<ObjBossEggmanState> m_fsm;

		// Token: 0x04003276 RID: 12918
		private STATE_ID m_initState = STATE_ID.AppearFever;

		// Token: 0x04003277 RID: 12919
		private STATE_ID m_damageState = STATE_ID.DamageFever;

		// Token: 0x04003278 RID: 12920
		private ObjBossEggmanParameter m_bossParam;

		// Token: 0x04003279 RID: 12921
		private ObjBossEggmanEffect m_bossEffect;

		// Token: 0x0400327A RID: 12922
		private ObjBossEggmanMotion m_bossMotion;
	}
}
