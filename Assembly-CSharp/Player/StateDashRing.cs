using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x020009BE RID: 2494
	public class StateDashRing : FSMState<CharacterState>
	{
		// Token: 0x06004127 RID: 16679 RVA: 0x00153184 File Offset: 0x00151384
		public override void Enter(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.Air);
			StateUtil.SetAirMovementToRotateGround(context, false);
			this.m_outOfControlTime = 0f;
			this.m_lerpRotate = 0f;
			Vector3 velocity = Vector3.zero;
			JumpSpringParameter enteringParameter = context.GetEnteringParameter<JumpSpringParameter>();
			if (enteringParameter != null)
			{
				Vector3 up = enteringParameter.m_rotation * Vector3.up;
				context.Movement.ResetPosition(enteringParameter.m_position);
				StateUtil.SetRotation(context, up);
				this.m_outOfControlTime = enteringParameter.m_outOfControlTime;
				this.m_speed = enteringParameter.m_firstSpeed;
				velocity = enteringParameter.m_rotation * Vector3.up * this.m_speed;
			}
			context.Movement.Velocity = velocity;
			context.GetAnimator().CrossFade("DashRing", 0.1f);
			this.m_isFalling = false;
			context.OnAttack(AttackPower.PlayerPower, DefensePower.PlayerPower);
			StateUtil.ThroughBreakable(context, true);
			context.ClearAirAction();
			StateUtil.SetDashRingMagnet(context, true);
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x0015326C File Offset: 0x0015146C
		public override void Leave(CharacterState context)
		{
			context.OffAttack();
			StateUtil.ThroughBreakable(context, false);
			StateUtil.SetDashRingMagnet(context, false);
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x00153284 File Offset: 0x00151484
		public override void Step(CharacterState context, float deltaTime)
		{
			this.m_outOfControlTime -= deltaTime;
			if (this.m_outOfControlTime < 0f)
			{
				if (!this.m_isFalling)
				{
					StateUtil.ThroughBreakable(context, false);
					context.GetAnimator().CrossFade("Fall", 0.3f);
				}
				this.m_isFalling = true;
				Vector3 gravityDir = context.Movement.GetGravityDir();
				context.Movement.Velocity += context.Movement.GetGravity() * deltaTime;
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
				context.Movement.Velocity = context.transform.forward * this.m_speed;
				context.ChangeState(STATE_ID.Run);
				return;
			}
		}

		// Token: 0x0600412A RID: 16682 RVA: 0x00153404 File Offset: 0x00151604
		public override bool DispatchMessage(CharacterState context, int messageId, MessageBase msg)
		{
			return StateUtil.ChangeAfterSpinattack(context, messageId, msg);
		}

		// Token: 0x040037D6 RID: 14294
		private const float lerpDelta = 0.5f;

		// Token: 0x040037D7 RID: 14295
		private float m_outOfControlTime;

		// Token: 0x040037D8 RID: 14296
		private float m_lerpRotate;

		// Token: 0x040037D9 RID: 14297
		private bool m_isFalling;

		// Token: 0x040037DA RID: 14298
		private float m_speed;
	}
}
