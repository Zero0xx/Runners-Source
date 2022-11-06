using System;
using UnityEngine;

namespace Boss
{
	// Token: 0x0200089C RID: 2204
	public class BossStateAttackMap2 : BossStateAttackBase
	{
		// Token: 0x06003B3A RID: 15162 RVA: 0x001392A8 File Offset: 0x001374A8
		public override void Enter(ObjBossEggmanState context)
		{
			base.Enter(context);
			context.DebugDrawState("BossStateAttackMap2");
			context.BossMotion.SetMotion(BossMotion.MISSILE_START, true);
			this.m_state = BossStateAttackMap2.State.Start;
			this.m_missile_time = 0f;
			this.m_boss_time = 0f;
			this.m_attackInterspace = context.GetAttackInterspace();
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x00139300 File Offset: 0x00137500
		public override void Leave(ObjBossEggmanState context)
		{
			base.Leave(context);
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x0013930C File Offset: 0x0013750C
		public override void Step(ObjBossEggmanState context, float delta)
		{
			switch (this.m_state)
			{
			case BossStateAttackMap2.State.Start:
				if (!context.IsPlayerDead())
				{
					if (context.IsBossDistanceEnd())
					{
						context.ChangeState(STATE_ID.PassMapDistanceEnd);
					}
					else
					{
						int randomRange = ObjUtil.GetRandomRange100();
						if (randomRange < context.BossParam.BumperRand)
						{
							context.CreateBumper(true, 0f);
						}
						this.m_missile_time = 0f;
						this.m_state = BossStateAttackMap2.State.Bumper;
					}
				}
				break;
			case BossStateAttackMap2.State.Bumper:
				this.m_missile_time += delta;
				if (this.m_missile_time > context.BossParam.MissileInterspace * 0.5f)
				{
					if (!context.IsPlayerDead() && !context.IsBossDistanceEnd())
					{
						int num = UnityEngine.Random.Range(0, BossStateAttackMap2.MISSILE_POSY.Length);
						float num2 = BossStateAttackMap2.MISSILE_POSY[num];
						Vector3 pos = new Vector3(context.transform.position.x + 10f, num2, context.transform.position.z);
						context.CreateMissile(pos);
						if (num < BossStateAttackMap2.BOSS_POSY.Length)
						{
							if (Mathf.Abs(num2 - context.transform.position.y) < 2f)
							{
								base.SetMove(context, 1f, 14f, BossStateAttackMap2.BOSS_POSY[num]);
							}
							else
							{
								base.SetMove(context, 0f, 0f, context.transform.position.y);
							}
						}
					}
					this.m_state = BossStateAttackMap2.State.Missile;
				}
				break;
			case BossStateAttackMap2.State.Missile:
				base.UpdateMove(context, delta);
				this.m_missile_time += delta;
				if (this.m_missile_time > context.BossParam.MissileInterspace)
				{
					this.m_state = BossStateAttackMap2.State.Start;
				}
				break;
			case BossStateAttackMap2.State.BossAttackReady:
				base.UpdateMove(context, delta);
				this.m_boss_time += delta;
				if (this.m_boss_time > 1f)
				{
					context.BossEffect.PlayBoostEffect(ObjBossEggmanEffect.BoostType.Attack);
					ObjUtil.PlaySE("boss_eggmobile_dash", "SE");
					this.m_boss_time = 0f;
					this.m_state = BossStateAttackMap2.State.BossAttack;
				}
				break;
			case BossStateAttackMap2.State.BossAttack:
			{
				if (UnityEngine.Random.Range(0, 2) == 0)
				{
					context.SetSpeed(-context.BossParam.AttackSpeedMin);
				}
				else
				{
					context.SetSpeed(-context.BossParam.AttackSpeedMax);
				}
				context.BossParam.MinSpeed = context.BossParam.Speed;
				float playerBossPositionX = context.GetPlayerBossPositionX();
				if (playerBossPositionX < 0f && Mathf.Abs(playerBossPositionX) > 6f)
				{
					context.SetSpeed(0f);
					context.BossParam.MinSpeed = context.BossParam.Speed;
					Vector3 position = new Vector3(context.transform.position.x + 26f, context.BossParam.StartPos.y, context.transform.position.z);
					context.transform.position = position;
					context.ChangeState(STATE_ID.AppearMap2_2);
				}
				break;
			}
			}
			if (!context.IsPlayerDead() && this.m_state != BossStateAttackMap2.State.BossAttack && this.m_state != BossStateAttackMap2.State.BossAttackReady)
			{
				this.m_boss_time += delta;
				if (this.m_boss_time > this.m_attackInterspace)
				{
					if (context.IsBossDistanceEnd())
					{
						context.ChangeState(STATE_ID.PassMapDistanceEnd);
					}
					else
					{
						this.m_boss_time = 0f;
						base.SetMove(context, 1f, 14f, 1.5f);
						context.BossMotion.SetMotion(BossMotion.ATTACK, true);
						this.m_state = BossStateAttackMap2.State.BossAttackReady;
					}
				}
			}
		}

		// Token: 0x0400336B RID: 13163
		private const float MOVE_SPEED = 14f;

		// Token: 0x0400336C RID: 13164
		private const float ATTACK_READY = 1f;

		// Token: 0x0400336D RID: 13165
		private const float ATTACK_POSY = 1.5f;

		// Token: 0x0400336E RID: 13166
		private const float PASS_DISTANCE = 6f;

		// Token: 0x0400336F RID: 13167
		private const float PASS_WARP_DISTANCE = 26f;

		// Token: 0x04003370 RID: 13168
		private const float MISSILE_POSX = 10f;

		// Token: 0x04003371 RID: 13169
		private static readonly float[] MISSILE_POSY = new float[]
		{
			1f,
			1f,
			2f,
			2f,
			3f
		};

		// Token: 0x04003372 RID: 13170
		private static readonly float[] BOSS_POSY = new float[]
		{
			2.5f,
			2.5f,
			3.5f,
			3.5f,
			1.5f
		};

		// Token: 0x04003373 RID: 13171
		private BossStateAttackMap2.State m_state;

		// Token: 0x04003374 RID: 13172
		private float m_missile_time;

		// Token: 0x04003375 RID: 13173
		private float m_boss_time;

		// Token: 0x04003376 RID: 13174
		private float m_attackInterspace;

		// Token: 0x0200089D RID: 2205
		private enum State
		{
			// Token: 0x04003378 RID: 13176
			Idle,
			// Token: 0x04003379 RID: 13177
			Start,
			// Token: 0x0400337A RID: 13178
			Bumper,
			// Token: 0x0400337B RID: 13179
			Missile,
			// Token: 0x0400337C RID: 13180
			BossAttackReady,
			// Token: 0x0400337D RID: 13181
			BossAttack
		}
	}
}
