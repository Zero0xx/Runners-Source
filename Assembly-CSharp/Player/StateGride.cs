using System;
using App.Utility;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x020009A3 RID: 2467
	public class StateGride : FSMState<CharacterState>
	{
		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x060040A1 RID: 16545 RVA: 0x0014F488 File Offset: 0x0014D688
		// (set) Token: 0x060040A0 RID: 16544 RVA: 0x0014F478 File Offset: 0x0014D678
		private bool DisableGravity
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

		// Token: 0x060040A2 RID: 16546 RVA: 0x0014F498 File Offset: 0x0014D698
		public override void Enter(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.Air);
			context.GetAnimator().CrossFade("SecondJump", 0.1f);
			CharaSEUtil.PlayPowerAttackSE(context.charaType);
			context.Movement.Velocity = context.Movement.GetForwardDir() * context.DefaultSpeed * context.Parameter.m_powerGrideSpeedRate + -context.Movement.GetGravityDir() * context.Parameter.m_grideFirstUpForce;
			StateUtil.SetAirMovementToRotateGround(context, true);
			context.OnAttack(AttackPower.PlayerPower, DefensePower.PlayerPower);
			context.OnAttackAttribute(AttackAttribute.Power);
			this.m_effectname = "ef_pl_" + context.CharacterName.ToLower() + "_attack_aura01";
			this.m_attackEffectname = "ef_pl_" + context.CharacterName.ToLower() + "_attack01";
			GameObject gameobj = StateUtil.CreateEffect(context, this.m_effectname, false, ResourceCategory.CHARA_EFFECT);
			StateUtil.SetObjectLocalPositionToCenter(context, gameobj);
			this.m_timer = 0f;
			this.m_flag.Reset();
			this.DisableGravity = true;
			context.AddAirAction();
			StateUtil.ThroughBreakable(context, true);
			StateUtil.SetSpecialtyJumpMagnet(context, CharacterAttribute.POWER, ChaoAbility.MAGNET_POWER_TYPE_JUMP, true);
			StateUtil.SetSpecialtyJumpDestroyEnemy(ChaoAbility.JUMP_DESTROY_ENEMY_AND_TRAP);
			StateUtil.SetSpecialtyJumpDestroyEnemy(ChaoAbility.JUMP_DESTROY_ENEMY);
		}

		// Token: 0x060040A3 RID: 16547 RVA: 0x0014F5D4 File Offset: 0x0014D7D4
		public override void Leave(CharacterState context)
		{
			context.OffAttack();
			StateUtil.StopEffect(context, this.m_effectname, 0.5f);
			StateUtil.ThroughBreakable(context, false);
			StateUtil.SetSpecialtyJumpMagnet(context, CharacterAttribute.POWER, ChaoAbility.MAGNET_POWER_TYPE_JUMP, false);
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x0014F60C File Offset: 0x0014D80C
		public override void Step(CharacterState context, float deltaTime)
		{
			this.m_timer += deltaTime;
			if (this.DisableGravity && this.m_timer > context.Parameter.m_disableGravityTime)
			{
				this.DisableGravity = false;
			}
			Vector3 a = context.Movement.VertVelocity;
			if (!this.DisableGravity)
			{
				a += context.Movement.GetGravity() * context.Parameter.m_grideGravityRate * deltaTime;
			}
			Vector3 b = context.Movement.GetForwardDir() * context.DefaultSpeed * context.Parameter.m_powerGrideSpeedRate;
			context.Movement.Velocity = a + b;
			if (context.m_input.IsTouched() && StateUtil.CheckAndChangeStateToAirAttack(context, true, false))
			{
				return;
			}
			if (context.Movement.IsOnGround())
			{
				if (StateUtil.ChangeToJumpStateIfPrecedeInputTouch(context, 0.1f, false))
				{
					return;
				}
				StateUtil.SetVelocityForwardRun(context, false);
				StateUtil.NowLanding(context, false);
				context.ChangeState(STATE_ID.Run);
				return;
			}
			else
			{
				if (this.m_timer > context.Parameter.m_grideTime)
				{
					StateUtil.SetVelocityForwardRun(context, true);
					context.ChangeState(STATE_ID.Fall);
					return;
				}
				return;
			}
		}

		// Token: 0x060040A5 RID: 16549 RVA: 0x0014F744 File Offset: 0x0014D944
		public override bool DispatchMessage(CharacterState context, int messageId, MessageBase msg)
		{
			if (messageId != 16385)
			{
				return false;
			}
			StateUtil.CreateEffect(context, context.Position, context.transform.rotation, this.m_attackEffectname, true, ResourceCategory.CHARA_EFFECT);
			context.Movement.Velocity = context.Movement.GetForwardDir() * context.DefaultSpeed * context.Parameter.m_powerGrideSpeedRate;
			this.m_timer = 0f;
			return true;
		}

		// Token: 0x04003755 RID: 14165
		private float m_timer;

		// Token: 0x04003756 RID: 14166
		private Bitset32 m_flag;

		// Token: 0x04003757 RID: 14167
		private string m_effectname;

		// Token: 0x04003758 RID: 14168
		private string m_attackEffectname;

		// Token: 0x020009A4 RID: 2468
		private enum Flag
		{
			// Token: 0x0400375A RID: 14170
			DISABLE_GRAVITY
		}
	}
}
