using System;
using UnityEngine;

namespace Boss
{
	// Token: 0x0200089A RID: 2202
	public class BossStateAttackMap1 : BossStateAttackBase
	{
		// Token: 0x06003B34 RID: 15156 RVA: 0x00138FEC File Offset: 0x001371EC
		public override void Enter(ObjBossEggmanState context)
		{
			base.Enter(context);
			context.DebugDrawState("BossStateAttackMap1");
			this.m_state = BossStateAttackMap1.State.Start;
			this.m_attackInterspace = 0f;
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x00139020 File Offset: 0x00137220
		public override void Leave(ObjBossEggmanState context)
		{
			base.Leave(context);
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x0013902C File Offset: 0x0013722C
		public override void Step(ObjBossEggmanState context, float delta)
		{
			BossStateAttackMap1.State state = this.m_state;
			if (state != BossStateAttackMap1.State.Start)
			{
				if (state == BossStateAttackMap1.State.Bom)
				{
					if (base.UpdateTime(delta, this.m_attackInterspace))
					{
						this.m_state = BossStateAttackMap1.State.Start;
					}
				}
			}
			else if (!context.IsPlayerDead())
			{
				if (context.IsBossDistanceEnd())
				{
					context.ChangeState(STATE_ID.PassMapDistanceEnd);
				}
				else
				{
					bool flag = true;
					if (context.BossParam.AttackBallFlag)
					{
						flag = false;
						context.BossParam.AttackBallFlag = false;
					}
					else
					{
						int randomRange = ObjUtil.GetRandomRange100();
						if (randomRange < context.BossParam.TrapRand && context.BossParam.AttackTrapCount < context.BossParam.AttackTrapCountMax)
						{
							flag = false;
						}
					}
					if (!flag)
					{
						this.CreateBall(context, BossBallType.TRAP);
						context.BossParam.AttackTrapCount++;
					}
					else
					{
						this.CreateBall(context, BossBallType.ATTACK);
						context.BossParam.AttackBallFlag = true;
						context.BossParam.AttackTrapCount = 0;
					}
					base.ResetTime();
					this.m_attackInterspace = context.GetAttackInterspace();
					this.m_state = BossStateAttackMap1.State.Bom;
				}
			}
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x00139154 File Offset: 0x00137354
		private void CreateBall(ObjBossEggmanState context, BossBallType type)
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.ENEMY_PREFAB, "ObjBossBall");
			if (gameObject != null)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, ObjBossUtil.GetBossHatchPos(context.gameObject), Quaternion.identity) as GameObject;
				if (gameObject2)
				{
					gameObject2.gameObject.SetActive(true);
					ObjBossBall component = gameObject2.GetComponent<ObjBossBall>();
					if (component)
					{
						component.Setup(new ObjBossBall.SetData
						{
							obj = context.gameObject,
							bound_param = context.GetBoundParam(),
							type = type,
							shot_rot = context.GetShotRotation(context.BossParam.ShotRotBase),
							shot_speed = context.BossParam.ShotSpeed,
							attack_speed = context.BossParam.AttackSpeed,
							firstSpeed = 0f,
							outOfcontrol = 0f,
							ballSpeed = context.BossParam.BallSpeed,
							bossAppear = true
						});
					}
				}
			}
		}

		// Token: 0x04003365 RID: 13157
		private BossStateAttackMap1.State m_state;

		// Token: 0x04003366 RID: 13158
		private float m_attackInterspace;

		// Token: 0x0200089B RID: 2203
		private enum State
		{
			// Token: 0x04003368 RID: 13160
			Idle,
			// Token: 0x04003369 RID: 13161
			Start,
			// Token: 0x0400336A RID: 13162
			Bom
		}
	}
}
