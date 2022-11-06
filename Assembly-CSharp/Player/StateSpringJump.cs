using System;
using UnityEngine;

namespace Player
{
	// Token: 0x020009BD RID: 2493
	public class StateSpringJump : FSMState<CharacterState>
	{
		// Token: 0x06004123 RID: 16675 RVA: 0x00152E30 File Offset: 0x00151030
		public override void Enter(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.Air);
			StateUtil.SetAirMovementToRotateGround(context, false);
			this.m_outOfControlTime = 0f;
			this.m_lerpRotate = 0f;
			this.m_isJumpUpSide = false;
			Vector3 velocity = Vector3.zero;
			JumpSpringParameter enteringParameter = context.GetEnteringParameter<JumpSpringParameter>();
			if (enteringParameter != null)
			{
				Vector3 vector = enteringParameter.m_rotation * Vector3.up;
				context.Movement.ResetPosition(enteringParameter.m_position);
				StateUtil.SetRotation(context, enteringParameter.m_rotation * vector);
				if (Vector3.Dot(context.transform.forward, Vector3.up) > 0.1736f)
				{
					StateUtil.SetRotation(context, context.Movement.GetUpDir(), -context.transform.forward);
				}
				this.m_outOfControlTime = enteringParameter.m_outOfControlTime;
				float firstSpeed = enteringParameter.m_firstSpeed;
				velocity = enteringParameter.m_rotation * Vector3.up * firstSpeed;
				if (Vector3.Dot(vector, context.Movement.GetGravityDir()) < -0.9962f)
				{
					this.m_isJumpUpSide = true;
				}
			}
			context.Movement.Velocity = velocity;
			context.GetAnimator().CrossFade("SpringJump", 0.1f);
			this.m_isFalling = false;
			this.m_isNotCharaChange = false;
			context.OnAttack(AttackPower.PlayerPower, DefensePower.PlayerPower);
			context.SetNotCharaChange(true);
			context.ClearAirAction();
			StateUtil.ThroughBreakable(context, true);
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x00152F8C File Offset: 0x0015118C
		public override void Leave(CharacterState context)
		{
			context.OffAttack();
			context.SetNotCharaChange(false);
			StateUtil.ThroughBreakable(context, false);
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x00152FA4 File Offset: 0x001511A4
		public override void Step(CharacterState context, float deltaTime)
		{
			this.m_outOfControlTime -= deltaTime;
			if (this.m_outOfControlTime < 0f)
			{
				if (!this.m_isNotCharaChange)
				{
					context.SetNotCharaChange(false);
				}
				this.m_isNotCharaChange = true;
				if (!this.m_isFalling)
				{
					StateUtil.ThroughBreakable(context, false);
					context.GetAnimator().CrossFade("Fall", 0.3f);
				}
				this.m_isFalling = true;
				Vector3 vector = context.Movement.Velocity;
				Vector3 gravityDir = context.Movement.GetGravityDir();
				if (this.m_isJumpUpSide)
				{
					Vector3 a = context.Movement.GetForwardDir() * StateUtil.GetForwardSpeedAir(context, context.DefaultSpeed, deltaTime);
					vector = a + context.Movement.VertVelocity + context.Movement.GetGravity() * deltaTime;
				}
				else
				{
					vector += context.Movement.GetGravity() * deltaTime;
				}
				context.Movement.Velocity = vector;
				if (this.m_lerpRotate < 1f)
				{
					this.m_lerpRotate = Mathf.Min(this.m_lerpRotate + 0.5f * deltaTime, 1f);
					Vector3 up;
					if (this.m_lerpRotate < 1f)
					{
						up = Vector3.Lerp(context.Movement.GetUpDir(), -gravityDir, this.m_lerpRotate);
					}
					else
					{
						up = -gravityDir;
						this.m_isFalling = true;
					}
					MovementUtil.RotateByCollision(context.transform, context.GetComponent<CapsuleCollider>(), up);
				}
				if (context.m_input.IsTouched() && StateUtil.CheckAndChangeStateToAirAttack(context, true, true))
				{
					return;
				}
			}
			if (context.Movement.GetVertVelocityScalar() <= 0f && context.Movement.IsOnGround())
			{
				StateUtil.NowLanding(context, true);
				context.ChangeState(STATE_ID.Run);
				return;
			}
		}

		// Token: 0x040037CE RID: 14286
		private const float lerpDelta = 0.5f;

		// Token: 0x040037CF RID: 14287
		private const float cos5 = 0.9962f;

		// Token: 0x040037D0 RID: 14288
		private const float cos80 = 0.1736f;

		// Token: 0x040037D1 RID: 14289
		private float m_outOfControlTime;

		// Token: 0x040037D2 RID: 14290
		private float m_lerpRotate;

		// Token: 0x040037D3 RID: 14291
		private bool m_isFalling;

		// Token: 0x040037D4 RID: 14292
		private bool m_isJumpUpSide;

		// Token: 0x040037D5 RID: 14293
		private bool m_isNotCharaChange;
	}
}
