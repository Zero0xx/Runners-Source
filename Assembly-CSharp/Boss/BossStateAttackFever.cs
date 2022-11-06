using System;
using UnityEngine;

namespace Boss
{
	// Token: 0x02000898 RID: 2200
	public class BossStateAttackFever : FSMState<ObjBossEggmanState>
	{
		// Token: 0x06003B2C RID: 15148 RVA: 0x00138C74 File Offset: 0x00136E74
		public override void Enter(ObjBossEggmanState context)
		{
			context.DebugDrawState("BossStateSpeedDown");
			context.SetHitCheck(true);
			context.BossEffect.PlayBoostEffect(ObjBossEggmanEffect.BoostType.Normal);
			this.m_state = BossStateAttackFever.State.Start;
			this.m_time = 0f;
			this.m_speed_down = context.BossParam.DownSpeed * context.BossParam.AddSpeedRatio;
			this.m_speed_down2 = this.m_speed_down;
			this.m_speed_up = 0f;
			this.m_distance_pass = 8f + context.BossParam.AddSpeedDistance;
			this.m_distance_sweat = 5f + context.BossParam.AddSpeedDistance;
			this.m_sweat_effect = false;
			this.m_sweat_effect_time = 0f;
			this.m_bumper = true;
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x00138D2C File Offset: 0x00136F2C
		public override void Leave(ObjBossEggmanState context)
		{
			context.SetHitCheck(false);
			context.BossEffect.PlaySweatEffectEnd();
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x00138D40 File Offset: 0x00136F40
		public override void Step(ObjBossEggmanState context, float delta)
		{
			context.UpdateSpeedDown(delta, this.m_speed_down);
			float playerBossPositionX = context.GetPlayerBossPositionX();
			if (playerBossPositionX < 0f)
			{
				this.SetSweatEffect(context);
				this.m_bumper = false;
				if (Mathf.Abs(playerBossPositionX) > this.m_distance_pass)
				{
					context.ChangeState(STATE_ID.PassFever);
				}
			}
			else
			{
				if (playerBossPositionX < this.m_distance_sweat)
				{
					this.SetSweatEffect(context);
					this.m_bumper = false;
				}
				else
				{
					this.ResetSweatEffect(context, delta);
					this.m_bumper = true;
				}
				switch (this.m_state)
				{
				case BossStateAttackFever.State.Start:
					if (context.IsHitBumper())
					{
						if (playerBossPositionX < 10f)
						{
							this.m_speed_up = context.BossParam.BumperSpeedup;
							this.m_state = BossStateAttackFever.State.Speedup;
						}
					}
					else
					{
						context.CreateBumper(true, 0f);
						this.m_attackInterspace = context.GetAttackInterspace();
						this.ResetTime();
						this.m_state = BossStateAttackFever.State.Bom;
					}
					break;
				case BossStateAttackFever.State.Bom:
					if (context.IsBossDistanceEnd())
					{
						context.ChangeState(STATE_ID.PassFeverDistanceEnd);
					}
					else if (context.IsHitBumper())
					{
						if (playerBossPositionX < 10f)
						{
							this.m_speed_up = context.BossParam.BumperSpeedup;
							this.m_state = BossStateAttackFever.State.Speedup;
						}
					}
					else if (this.UpdateTime(delta, this.m_attackInterspace) && this.m_bumper)
					{
						this.m_state = BossStateAttackFever.State.Start;
					}
					break;
				case BossStateAttackFever.State.Speedup:
					context.SetSpeed(this.m_speed_up * 0.1f);
					this.m_state = BossStateAttackFever.State.SpeedupEnd;
					break;
				case BossStateAttackFever.State.SpeedupEnd:
				{
					float num = context.BossParam.PlayerSpeed * 0.7f;
					if (context.BossParam.Speed < num)
					{
						this.m_speed_down = this.m_speed_down2;
						context.SetSpeed(num);
						this.m_state = BossStateAttackFever.State.Bom;
					}
					else
					{
						this.m_speed_down += delta * this.m_speed_up * 1.2f;
					}
					break;
				}
				}
			}
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x00138F3C File Offset: 0x0013713C
		private bool UpdateTime(float delta, float time_max)
		{
			this.m_time += delta;
			return this.m_time > time_max;
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x00138F5C File Offset: 0x0013715C
		private void ResetTime()
		{
			this.m_time = 0f;
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x00138F6C File Offset: 0x0013716C
		private void SetSweatEffect(ObjBossEggmanState context)
		{
			if (!this.m_sweat_effect)
			{
				context.BossEffect.PlaySweatEffectStart();
				this.m_sweat_effect_time = 1f;
				this.m_sweat_effect = true;
			}
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x00138FA4 File Offset: 0x001371A4
		private void ResetSweatEffect(ObjBossEggmanState context, float delta)
		{
			this.m_sweat_effect_time -= delta;
			if (this.m_sweat_effect_time < 0f && this.m_sweat_effect)
			{
				context.BossEffect.PlaySweatEffectEnd();
				this.m_sweat_effect = false;
			}
		}

		// Token: 0x04003351 RID: 13137
		private const float PASS_DISTANCE = 8f;

		// Token: 0x04003352 RID: 13138
		private const float SWEAT_DISTANCE = 5f;

		// Token: 0x04003353 RID: 13139
		private const float SPEEDUP_DISTANCE = 10f;

		// Token: 0x04003354 RID: 13140
		private BossStateAttackFever.State m_state;

		// Token: 0x04003355 RID: 13141
		private float m_speed_down;

		// Token: 0x04003356 RID: 13142
		private float m_speed_down2;

		// Token: 0x04003357 RID: 13143
		private float m_speed_up;

		// Token: 0x04003358 RID: 13144
		private float m_distance_pass;

		// Token: 0x04003359 RID: 13145
		private float m_distance_sweat;

		// Token: 0x0400335A RID: 13146
		private bool m_sweat_effect;

		// Token: 0x0400335B RID: 13147
		private float m_sweat_effect_time;

		// Token: 0x0400335C RID: 13148
		private float m_time;

		// Token: 0x0400335D RID: 13149
		private float m_attackInterspace;

		// Token: 0x0400335E RID: 13150
		private bool m_bumper = true;

		// Token: 0x02000899 RID: 2201
		private enum State
		{
			// Token: 0x04003360 RID: 13152
			Idle,
			// Token: 0x04003361 RID: 13153
			Start,
			// Token: 0x04003362 RID: 13154
			Bom,
			// Token: 0x04003363 RID: 13155
			Speedup,
			// Token: 0x04003364 RID: 13156
			SpeedupEnd
		}
	}
}
