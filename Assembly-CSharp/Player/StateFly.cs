using System;
using App.Utility;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x020009A1 RID: 2465
	public class StateFly : FSMState<CharacterState>
	{
		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06004094 RID: 16532 RVA: 0x0014F088 File Offset: 0x0014D288
		// (set) Token: 0x06004093 RID: 16531 RVA: 0x0014F078 File Offset: 0x0014D278
		private bool CannotFly
		{
			get
			{
				return this.m_flag.Test(0);
			}
			set
			{
				this.m_flag.Set(0, value);
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06004096 RID: 16534 RVA: 0x0014F0A8 File Offset: 0x0014D2A8
		// (set) Token: 0x06004095 RID: 16533 RVA: 0x0014F098 File Offset: 0x0014D298
		private bool NowUp
		{
			get
			{
				return this.m_flag.Test(1);
			}
			set
			{
				this.m_flag.Set(1, value);
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06004098 RID: 16536 RVA: 0x0014F0C8 File Offset: 0x0014D2C8
		// (set) Token: 0x06004097 RID: 16535 RVA: 0x0014F0B8 File Offset: 0x0014D2B8
		private bool Hold
		{
			get
			{
				return this.m_flag.Test(2);
			}
			set
			{
				this.m_flag.Set(2, value);
			}
		}

		// Token: 0x06004099 RID: 16537 RVA: 0x0014F0D8 File Offset: 0x0014D2D8
		public override void Enter(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.Air);
			context.GetAnimator().CrossFade("SecondJump", 0.1f);
			CharaSEUtil.PlayFlySE(context.charaType);
			this.m_effectName = "ef_pl_" + context.CharacterName.ToLower() + "_fly01";
			this.m_flag.Reset();
			this.NowUp = true;
			this.Hold = true;
			this.m_canFlyTime = context.Parameter.m_canFlyTime;
			context.Movement.VertVelocity = -context.Movement.GetGravityDir() * context.Parameter.m_flyUpFirstSpeed;
			StateUtil.SetAirMovementToRotateGround(context, true);
			context.OnAttack(AttackPower.PlayerSpin, DefensePower.PlayerSpin);
			this.CreateEffect(context);
			context.AddAirAction();
			StateUtil.SetSpecialtyJumpMagnet(context, CharacterAttribute.FLY, ChaoAbility.MAGNET_FLY_TYPE_JUMP, true);
			StateUtil.SetSpecialtyJumpDestroyEnemy(ChaoAbility.JUMP_DESTROY_ENEMY_AND_TRAP);
			StateUtil.SetSpecialtyJumpDestroyEnemy(ChaoAbility.JUMP_DESTROY_ENEMY);
		}

		// Token: 0x0600409A RID: 16538 RVA: 0x0014F1B8 File Offset: 0x0014D3B8
		public override void Leave(CharacterState context)
		{
			context.OffAttack();
			this.DeleteEffect();
			context.SetStatus(Status.InvincibleByChao, false);
			StateUtil.SetSpecialtyJumpMagnet(context, CharacterAttribute.FLY, ChaoAbility.MAGNET_FLY_TYPE_JUMP, false);
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x0014F1E4 File Offset: 0x0014D3E4
		public override void Step(CharacterState context, float deltaTime)
		{
			Vector3 a = context.Movement.GetForwardDir() * StateUtil.GetForwardSpeedAir(context, context.DefaultSpeed * context.Parameter.m_flySpeedRate, deltaTime);
			Vector3 b = Vector3.zero;
			float limitHeitht = context.Parameter.m_limitHeitht;
			Vector3 position = context.Position;
			StateUtil.GetBaseGroundPosition(context, ref position);
			if (context.m_input.IsHold() && !this.CannotFly)
			{
				float num = Vector3.Magnitude(context.Position - position);
				if (num < limitHeitht)
				{
					float d;
					if (!this.NowUp)
					{
						d = context.Parameter.m_flyUpFirstSpeed;
						this.m_canFlyTime -= context.Parameter.m_flyDecSec2ndPress;
						if (!this.Hold)
						{
							CharaSEUtil.PlayFlySE(context.charaType);
						}
					}
					else
					{
						float vertVelocityScalar = context.Movement.GetVertVelocityScalar();
						d = Mathf.Min(vertVelocityScalar + context.Parameter.m_flyUpForce * deltaTime, context.Parameter.m_flyUpSpeedMax);
					}
					b = -context.Movement.GetGravityDir() * d;
				}
				this.NowUp = true;
				this.Hold = true;
				if (this.m_effect == null)
				{
					this.CreateEffect(context);
				}
				this.m_canFlyTime -= deltaTime;
				if (this.m_canFlyTime < 0f)
				{
					this.CannotFly = true;
				}
			}
			else
			{
				this.NowUp = false;
				this.Hold = false;
				float vertVelocityScalar2 = context.Movement.GetVertVelocityScalar();
				if (vertVelocityScalar2 < -context.Parameter.m_flydownSpeedMax)
				{
					b = context.Parameter.m_flydownSpeedMax * context.Movement.GetGravityDir();
				}
				else
				{
					b = context.Movement.VertVelocity + context.Movement.GetGravity() * context.Parameter.m_flyGravityRate * deltaTime;
				}
				this.DeleteEffect();
			}
			context.Movement.Velocity = a + b;
			if (!context.Movement.IsOnGround())
			{
				return;
			}
			if (StateUtil.ChangeToJumpStateIfPrecedeInputTouch(context, 0.1f, false))
			{
				return;
			}
			StateUtil.NowLanding(context, false);
			context.ChangeState(STATE_ID.Run);
		}

		// Token: 0x0600409C RID: 16540 RVA: 0x0014F428 File Offset: 0x0014D628
		public override bool DispatchMessage(CharacterState context, int messageId, MessageBase msg)
		{
			return StateUtil.ChangeAfterSpinattack(context, messageId, msg);
		}

		// Token: 0x0600409D RID: 16541 RVA: 0x0014F43C File Offset: 0x0014D63C
		private void CreateEffect(CharacterState context)
		{
			this.m_effect = StateUtil.CreateEffect(context, this.m_effectName, true, ResourceCategory.CHARA_EFFECT);
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x0014F454 File Offset: 0x0014D654
		private void DeleteEffect()
		{
			StateUtil.DestroyParticle(this.m_effect, 1f);
			this.m_effect = null;
		}

		// Token: 0x0400374C RID: 14156
		private const float ChaoAbilityExtendTimeRate = 2f;

		// Token: 0x0400374D RID: 14157
		private float m_canFlyTime;

		// Token: 0x0400374E RID: 14158
		private Bitset32 m_flag;

		// Token: 0x0400374F RID: 14159
		private GameObject m_effect;

		// Token: 0x04003750 RID: 14160
		private string m_effectName;

		// Token: 0x020009A2 RID: 2466
		private enum Flag
		{
			// Token: 0x04003752 RID: 14162
			CANNOTFLY,
			// Token: 0x04003753 RID: 14163
			NOWUP,
			// Token: 0x04003754 RID: 14164
			HOLD
		}
	}
}
