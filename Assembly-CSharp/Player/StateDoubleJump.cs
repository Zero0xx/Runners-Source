using System;
using Message;
using Tutorial;
using UnityEngine;

namespace Player
{
	// Token: 0x020009A8 RID: 2472
	public class StateDoubleJump : FSMState<CharacterState>
	{
		// Token: 0x060040B4 RID: 16564 RVA: 0x0014FCAC File Offset: 0x0014DEAC
		public override void Enter(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.Air);
			StateUtil.SetAirMovementToRotateGround(context, true);
			this.m_jumpForce = context.Parameter.m_doubleJumpForce;
			this.m_addForceTmer = context.Parameter.m_doubleJumpAddSec;
			this.m_addAcc = context.Parameter.m_doubleJumpAddAcc;
			this.m_speed = Mathf.Max(context.Movement.GetForwardVelocityScalar(), 0f);
			context.GetAnimator().CrossFade("Jump", 0.05f);
			context.OnAttack(AttackPower.PlayerSpin, DefensePower.PlayerSpin);
			StateUtil.SetAttackAttributePowerIfPowerType(context, true);
			this.m_effect = context.GetSpinAttackEffect();
			if (this.m_effect != null)
			{
				this.m_effect.SetValid(true);
			}
			this.StartJump(context);
			StateUtil.SetSpecialtyJumpMagnet(context, CharacterAttribute.SPEED, ChaoAbility.MAGNET_SPEED_TYPE_JUMP, true);
			StateUtil.SetSpecialtyJumpDestroyEnemy(ChaoAbility.JUMP_DESTROY_ENEMY_AND_TRAP);
			StateUtil.SetSpecialtyJumpDestroyEnemy(ChaoAbility.JUMP_DESTROY_ENEMY);
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x0014FD84 File Offset: 0x0014DF84
		public override void Leave(CharacterState context)
		{
			context.OffAttack();
			if (this.m_effect != null)
			{
				this.m_effect.SetValid(false);
			}
			StateUtil.SetSpecialtyJumpMagnet(context, CharacterAttribute.SPEED, ChaoAbility.MAGNET_SPEED_TYPE_JUMP, false);
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x0014FDC0 File Offset: 0x0014DFC0
		public override void Step(CharacterState context, float deltaTime)
		{
			Vector3 vector = context.Movement.VertVelocity;
			Vector3 a = context.Movement.Velocity - vector;
			a = context.Movement.GetForwardDir() * StateUtil.GetForwardSpeedAir(context, context.DefaultSpeed, deltaTime);
			vector += context.Movement.GetGravity() * deltaTime;
			if (this.m_addForceTmer >= 0f)
			{
				this.m_addForceTmer -= deltaTime;
				vector += context.Movement.GetUpDir() * this.m_addAcc * deltaTime;
			}
			context.Movement.Velocity = a + vector;
			STATE_ID state = STATE_ID.Non;
			if (StateUtil.CheckHitWallAndGoDeadOrStumble(context, deltaTime, ref state))
			{
				context.ChangeState(state);
				return;
			}
			if (context.m_input.IsTouched())
			{
				STATE_ID state_ID = STATE_ID.Non;
				if (StateUtil.GetNextStateToAirAttack(context, ref state_ID, false))
				{
					if (state_ID == STATE_ID.DoubleJump)
					{
						this.StartJump(context);
					}
					else if (state_ID != STATE_ID.Non)
					{
						ObjUtil.SendMessageTutorialClear(EventID.DOUBLE_JUMP);
						context.ChangeState(state_ID);
					}
					return;
				}
			}
			if (context.Movement.GetVertVelocityScalar() > 0f || !context.Movement.IsOnGround())
			{
				return;
			}
			if (StateUtil.ChangeToJumpStateIfPrecedeInputTouch(context, 0.1f, true))
			{
				return;
			}
			StateUtil.NowLanding(context, true);
			context.ChangeState(STATE_ID.Run);
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x0014FF1C File Offset: 0x0014E11C
		public override bool DispatchMessage(CharacterState context, int messageId, MessageBase msg)
		{
			return StateUtil.ChangeAfterSpinattack(context, messageId, msg);
		}

		// Token: 0x060040B8 RID: 16568 RVA: 0x0014FF30 File Offset: 0x0014E130
		private void StartJump(CharacterState context)
		{
			context.Movement.Velocity = context.transform.forward * this.m_speed + this.m_jumpForce * Vector3.up;
			StateUtil.Create2ndJumpEffect(context);
			context.AddAirAction();
		}

		// Token: 0x04003761 RID: 14177
		private float m_jumpForce;

		// Token: 0x04003762 RID: 14178
		private float m_speed;

		// Token: 0x04003763 RID: 14179
		private float m_addForceTmer;

		// Token: 0x04003764 RID: 14180
		private float m_addAcc;

		// Token: 0x04003765 RID: 14181
		private CharacterLoopEffect m_effect;
	}
}
