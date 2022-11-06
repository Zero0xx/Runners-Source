using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x02000991 RID: 2449
	public class StateFall : FSMState<CharacterState>
	{
		// Token: 0x0600405E RID: 16478 RVA: 0x0014DB90 File Offset: 0x0014BD90
		public override void Enter(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.Air);
			StateUtil.SetAirMovementToRotateGround(context, true);
			context.Movement.OffGround();
			float d = Mathf.Max(context.Movement.GetForwardVelocityScalar(), 0f);
			context.Movement.Velocity = context.transform.forward * d + context.Movement.VertVelocity;
			context.GetAnimator().CrossFade("Fall", 0.2f);
			context.OnAttack(AttackPower.PlayerStomp, DefensePower.PlayerStomp);
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x0014DC18 File Offset: 0x0014BE18
		public override void Leave(CharacterState context)
		{
			context.OffAttack();
		}

		// Token: 0x06004060 RID: 16480 RVA: 0x0014DC20 File Offset: 0x0014BE20
		public override void Step(CharacterState context, float deltaTime)
		{
			context.Movement.Velocity = context.Movement.Velocity + context.Movement.GetGravity() * deltaTime;
			if (context.m_input.IsTouched() && StateUtil.CheckAndChangeStateToAirAttack(context, true, false))
			{
				return;
			}
			STATE_ID state = STATE_ID.Non;
			if (StateUtil.CheckHitWallAndGoDeadOrStumble(context, deltaTime, ref state))
			{
				context.ChangeState(state);
				return;
			}
			if (context.Movement.IsOnGround())
			{
				StateUtil.NowLanding(context, true);
				context.ChangeState(STATE_ID.Run);
			}
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x0014DCB0 File Offset: 0x0014BEB0
		public override bool DispatchMessage(CharacterState context, int messageId, MessageBase msg)
		{
			return StateUtil.ChangeAfterSpinattack(context, messageId, msg);
		}
	}
}
