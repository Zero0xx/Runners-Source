using System;
using Message;
using UnityEngine;

namespace Boss
{
	// Token: 0x0200088A RID: 2186
	public class BossStateAppearMapBase : FSMState<ObjBossEggmanState>
	{
		// Token: 0x06003AEF RID: 15087 RVA: 0x00137E0C File Offset: 0x0013600C
		public override void Enter(ObjBossEggmanState context)
		{
			context.DebugDrawState("BossStateAppearMap");
			context.SetHitCheck(false);
			context.transform.rotation = Quaternion.Euler(BossStateAppearMapBase.APPEAR_ROT);
			context.OpenHpGauge();
			this.m_time = 0f;
			this.m_state = BossStateAppearMapBase.State.Start;
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x00137E58 File Offset: 0x00136058
		public override void Leave(ObjBossEggmanState context)
		{
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x00137E5C File Offset: 0x0013605C
		public override void Step(ObjBossEggmanState context, float delta)
		{
			switch (this.m_state)
			{
			case BossStateAppearMapBase.State.Start:
				context.BossEffect.PlayBoostEffect(ObjBossEggmanEffect.BoostType.Normal);
				this.m_state = BossStateAppearMapBase.State.Wait1;
				break;
			case BossStateAppearMapBase.State.Wait1:
			{
				float playerDistance = context.GetPlayerDistance();
				if (playerDistance < context.BossParam.DefaultPlayerDistance)
				{
					context.KeepSpeed();
					MsgTutorialMapBoss value = new MsgTutorialMapBoss();
					GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnMsgTutorialMapBoss", value, SendMessageOptions.DontRequireReceiver);
					this.m_state = BossStateAppearMapBase.State.Wait2;
				}
				break;
			}
			case BossStateAppearMapBase.State.Wait2:
				this.SetMotion1(context);
				this.m_state = BossStateAppearMapBase.State.Event1;
				break;
			case BossStateAppearMapBase.State.Event1:
				this.m_time += delta;
				if (this.m_time > this.GetTime1())
				{
					context.RequestStartChaoAbility();
					this.SetMotion2(context);
					this.m_time = 0f;
					this.m_state = BossStateAppearMapBase.State.Event2;
				}
				break;
			case BossStateAppearMapBase.State.Event2:
				this.m_time += delta;
				if (this.m_time > this.GetTime2())
				{
					this.SetMotion3(context);
					this.m_state = BossStateAppearMapBase.State.Event3;
				}
				break;
			case BossStateAppearMapBase.State.Event3:
				this.m_time += delta;
				if (this.m_time > this.GetTime3())
				{
					context.BossParam.SetupBossTable();
					context.StartGauge();
					context.ChangeState(this.GetNextChangeState());
					this.m_state = BossStateAppearMapBase.State.Idle;
				}
				break;
			}
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x00137FC0 File Offset: 0x001361C0
		protected virtual float GetTime1()
		{
			return 0f;
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x00137FC8 File Offset: 0x001361C8
		protected virtual float GetTime2()
		{
			return 0f;
		}

		// Token: 0x06003AF4 RID: 15092 RVA: 0x00137FD0 File Offset: 0x001361D0
		protected virtual float GetTime3()
		{
			return 0f;
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x00137FD8 File Offset: 0x001361D8
		protected virtual void SetMotion1(ObjBossEggmanState context)
		{
			context.BossMotion.SetMotion(BossMotion.APPEAR, true);
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x00137FE8 File Offset: 0x001361E8
		protected virtual void SetMotion2(ObjBossEggmanState context)
		{
			context.BossMotion.SetMotion(BossMotion.BOM_START, true);
			ObjUtil.PlaySE("boss_bomb_drop", "SE");
		}

		// Token: 0x06003AF7 RID: 15095 RVA: 0x00138008 File Offset: 0x00136208
		protected virtual void SetMotion3(ObjBossEggmanState context)
		{
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x0013800C File Offset: 0x0013620C
		protected virtual STATE_ID GetNextChangeState()
		{
			return STATE_ID.AttackMap1;
		}

		// Token: 0x04003317 RID: 13079
		private static Vector3 APPEAR_ROT = new Vector3(0f, 90f, 0f);

		// Token: 0x04003318 RID: 13080
		private BossStateAppearMapBase.State m_state;

		// Token: 0x04003319 RID: 13081
		private float m_time;

		// Token: 0x0200088B RID: 2187
		private enum State
		{
			// Token: 0x0400331B RID: 13083
			Idle,
			// Token: 0x0400331C RID: 13084
			Start,
			// Token: 0x0400331D RID: 13085
			Wait1,
			// Token: 0x0400331E RID: 13086
			Wait2,
			// Token: 0x0400331F RID: 13087
			Event1,
			// Token: 0x04003320 RID: 13088
			Event2,
			// Token: 0x04003321 RID: 13089
			Event3
		}
	}
}
