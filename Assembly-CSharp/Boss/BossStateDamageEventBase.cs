using System;
using UnityEngine;

namespace Boss
{
	// Token: 0x020008A2 RID: 2210
	public class BossStateDamageEventBase : FSMState<ObjBossEventBossState>
	{
		// Token: 0x06003B53 RID: 15187 RVA: 0x00139E9C File Offset: 0x0013809C
		public override void Enter(ObjBossEventBossState context)
		{
			context.DebugDrawState("BossStateDamageEvent");
			context.SetHitCheck(false);
			context.AddDamage();
			context.BossEffect.PlayHitEffect(context.BossParam.BoostLevel);
			if (context.BossParam.BossHP > 0)
			{
				ObjUtil.PlaySE("boss_damage", "SE");
			}
			else
			{
				ObjUtil.PlaySE("boss_explosion", "SE");
				context.BossClear();
			}
			this.SetMotion(context, true);
			this.m_state = BossStateDamageEventBase.State.SpeedDown;
			this.m_start_rot = context.transform.rotation;
			this.m_speed_down = 0f;
			this.m_distance = 0f;
			this.m_rot_end = false;
			float damageSpeedParam = context.GetDamageSpeedParam();
			context.SetSpeed(60f * damageSpeedParam);
			this.SetSpeedDown(120f);
			this.SetDistance(context.BossParam.DefaultPlayerDistance);
			if (context.ColorPowerHit)
			{
				context.DebugDrawState("BossStateDamageEvent ColorPowerHit");
				this.SetupColorPowerDamage(context);
			}
			else
			{
				context.CreateEventFeverRing(context.GetDropRingAggressivity());
			}
		}

		// Token: 0x06003B54 RID: 15188 RVA: 0x00139FB0 File Offset: 0x001381B0
		public override void Leave(ObjBossEventBossState context)
		{
			context.ColorPowerHit = false;
			context.ChaoHit = false;
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x00139FC0 File Offset: 0x001381C0
		public override void Step(ObjBossEventBossState context, float delta)
		{
			context.UpdateSpeedDown(delta, this.m_speed_down);
			BossStateDamageEventBase.State state = this.m_state;
			if (state != BossStateDamageEventBase.State.SpeedDown)
			{
				if (state == BossStateDamageEventBase.State.Wait)
				{
					if (context.IsPlayerDead() && context.IsClear())
					{
						this.ChangeStateWait(context);
					}
					else
					{
						float playerDistance = context.GetPlayerDistance();
						if (playerDistance < this.m_distance)
						{
							context.SetSpeed(context.BossParam.PlayerSpeed * 0.7f);
							context.transform.rotation = this.m_start_rot;
							if (!this.m_rot_end)
							{
								this.SetMotion(context, false);
							}
							context.KeepSpeed();
							this.ChangeStateWait(context);
						}
					}
				}
			}
			else if (context.BossParam.Speed < context.BossParam.PlayerSpeed)
			{
				this.SetSpeedDown(1f);
				this.m_state = BossStateDamageEventBase.State.Wait;
			}
		}

		// Token: 0x06003B56 RID: 15190 RVA: 0x0013A0A8 File Offset: 0x001382A8
		private void SetupColorPowerDamage(ObjBossEventBossState context)
		{
			context.SetSpeed(60f);
			this.SetSpeedDown(120f);
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x0013A0C0 File Offset: 0x001382C0
		private void SetMotion(ObjBossEventBossState context, bool flag)
		{
			if (flag)
			{
				context.BossMotion.SetMotion(EventBossMotion.DAMAGE, true);
			}
			else if (context.BossParam.BossHP > 0)
			{
				context.BossMotion.SetMotion(EventBossMotion.ATTACK, true);
			}
			else
			{
				context.BossEffect.PlayEscapeEffect(context);
				context.BossMotion.SetMotion(EventBossMotion.ESCAPE, true);
			}
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x0013A124 File Offset: 0x00138324
		protected virtual void ChangeStateWait(ObjBossEventBossState context)
		{
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x0013A128 File Offset: 0x00138328
		protected void SetSpeedDown(float val)
		{
			this.m_speed_down = val;
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x0013A134 File Offset: 0x00138334
		protected void SetDistance(float val)
		{
			this.m_distance = val;
		}

		// Token: 0x040033A3 RID: 13219
		private const float START_SPEED = 60f;

		// Token: 0x040033A4 RID: 13220
		private const float START_DOWNSPEED = 120f;

		// Token: 0x040033A5 RID: 13221
		private const float WAIT_DOWNSPEED = 1f;

		// Token: 0x040033A6 RID: 13222
		private const float ROT_SPEED = 30f;

		// Token: 0x040033A7 RID: 13223
		private const float ROT_DOWNSPEED = 0.3f;

		// Token: 0x040033A8 RID: 13224
		private const float ROT_MIN = 10f;

		// Token: 0x040033A9 RID: 13225
		private BossStateDamageEventBase.State m_state;

		// Token: 0x040033AA RID: 13226
		private Quaternion m_start_rot = Quaternion.identity;

		// Token: 0x040033AB RID: 13227
		private float m_speed_down;

		// Token: 0x040033AC RID: 13228
		private float m_distance;

		// Token: 0x040033AD RID: 13229
		private bool m_rot_end;

		// Token: 0x020008A3 RID: 2211
		private enum State
		{
			// Token: 0x040033AF RID: 13231
			Idle,
			// Token: 0x040033B0 RID: 13232
			SpeedDown,
			// Token: 0x040033B1 RID: 13233
			Wait
		}
	}
}
