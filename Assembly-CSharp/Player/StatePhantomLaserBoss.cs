using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x020009B6 RID: 2486
	public class StatePhantomLaserBoss : FSMState<CharacterState>
	{
		// Token: 0x060040FE RID: 16638 RVA: 0x00151F24 File Offset: 0x00150124
		public override void Enter(CharacterState context)
		{
			this.m_mode = StatePhantomLaserBoss.Mode.Up;
			StateUtil.SetRotation(context, Vector3.up, CharacterDefs.BaseFrontTangent);
			PhantomLaserUtil.ChangeVisualOnEnter(context);
			StateUtil.SetNotDrawItemEffect(context, true);
			context.OnAttack(AttackPower.PlayerColorPower, DefensePower.PlayerColorPower);
			context.OnAttackAttribute(AttackAttribute.PhantomLaser);
			SoundManager.SePlay("phantom_laser_shoot", "SE");
			this.m_time = 5f;
			this.m_returnFromPhantom = false;
			this.m_speed = 25f;
			StateUtil.DeactiveInvincible(context);
			StateUtil.SendMessageTransformPhantom(context, PhantomType.LASER);
			MsgBossInfo bossInfo = StateUtil.GetBossInfo(null);
			if (bossInfo != null && bossInfo.m_succeed)
			{
				this.m_boss = bossInfo.m_boss;
				this.GoModeUp(context, bossInfo.m_position);
			}
			else
			{
				this.m_returnFromPhantom = true;
			}
			StateUtil.SetPhantomMagnetColliderRange(context, PhantomType.LASER);
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x00151FE4 File Offset: 0x001501E4
		public override void Leave(CharacterState context)
		{
			StateUtil.SetRotation(context, Vector3.up, CharacterDefs.BaseFrontTangent);
			PhantomLaserUtil.ChangeVisualOnLeave(context);
			context.OffAttack();
			SoundManager.SeStop("phantom_laser_shoot", "SE");
			StateUtil.SetNotDrawItemEffect(context, false);
			StateUtil.SendMessageReturnFromPhantom(context, PhantomType.LASER);
			this.m_boss = null;
			context.SetChangePhantomCancel(ItemType.UNKNOWN);
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x00152038 File Offset: 0x00150238
		public override void Step(CharacterState context, float deltaTime)
		{
			StatePhantomLaserBoss.Mode mode = this.m_mode;
			if (mode != StatePhantomLaserBoss.Mode.Up)
			{
				if (mode == StatePhantomLaserBoss.Mode.GoTarget)
				{
					this.StepModeGoTarget(context);
				}
			}
			else
			{
				this.StepModeUp(context);
			}
			this.m_time -= deltaTime;
			if (this.m_time < 0f || this.m_returnFromPhantom)
			{
				StateUtil.ResetVelocity(context);
				StateUtil.ReturnFromPhantomAndChangeState(context, PhantomType.LASER, this.m_returnFromPhantom);
				return;
			}
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x001520B4 File Offset: 0x001502B4
		public override bool DispatchMessage(CharacterState context, int messageId, MessageBase msg)
		{
			if (messageId != 16385)
			{
				return false;
			}
			MsgHitDamageSucceed msgHitDamageSucceed = msg as MsgHitDamageSucceed;
			if (msgHitDamageSucceed != null && msgHitDamageSucceed.m_sender == this.m_boss)
			{
				this.m_returnFromPhantom = true;
			}
			return true;
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x00152100 File Offset: 0x00150300
		private void GoModeUp(CharacterState context, Vector3 targetPos)
		{
			Vector3 lhs = targetPos - context.Position;
			Vector3 upDir = context.Movement.GetUpDir();
			Vector3 b = Vector3.Dot(lhs, upDir) * upDir;
			Vector3 position = context.Position + b;
			context.ChangeMovement(MOVESTATE_ID.GoTarget);
			CharacterMoveTarget movementState = context.GetMovementState<CharacterMoveTarget>();
			if (movementState != null)
			{
				movementState.SetTargetAndSpeed(context.Movement, position, Quaternion.identity, this.m_speed);
				movementState.SetRotateVelocityDir(true);
			}
			this.m_mode = StatePhantomLaserBoss.Mode.Up;
		}

		// Token: 0x06004103 RID: 16643 RVA: 0x00152180 File Offset: 0x00150380
		private void GoModeGoTarget(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.GoTargetBoss);
			CharacterMoveTargetBoss currentState = context.Movement.GetCurrentState<CharacterMoveTargetBoss>();
			if (currentState != null)
			{
				currentState.SetTarget(this.m_boss);
				currentState.SetSpeed(this.m_speed);
				currentState.SetRotateVelocityDir(true);
			}
			this.m_mode = StatePhantomLaserBoss.Mode.GoTarget;
		}

		// Token: 0x06004104 RID: 16644 RVA: 0x001521CC File Offset: 0x001503CC
		private void StepModeUp(CharacterState context)
		{
			CharacterMoveTarget movementState = context.GetMovementState<CharacterMoveTarget>();
			if (movementState != null)
			{
				if (movementState.DoesReachTarget())
				{
					this.GoModeGoTarget(context);
				}
			}
			else
			{
				this.m_returnFromPhantom = true;
			}
		}

		// Token: 0x06004105 RID: 16645 RVA: 0x00152204 File Offset: 0x00150404
		private void StepModeGoTarget(CharacterState context)
		{
			CharacterMoveTargetBoss currentState = context.Movement.GetCurrentState<CharacterMoveTargetBoss>();
			if (currentState != null)
			{
				currentState.SetSpeed(this.m_speed);
				bool flag = currentState.IsTargetNotFound();
				this.m_returnFromPhantom = (this.m_returnFromPhantom || flag);
			}
			else
			{
				this.m_returnFromPhantom = true;
			}
		}

		// Token: 0x040037AE RID: 14254
		private const float Speed = 25f;

		// Token: 0x040037AF RID: 14255
		private float m_time;

		// Token: 0x040037B0 RID: 14256
		private bool m_returnFromPhantom;

		// Token: 0x040037B1 RID: 14257
		private float m_speed;

		// Token: 0x040037B2 RID: 14258
		private GameObject m_boss;

		// Token: 0x040037B3 RID: 14259
		private StatePhantomLaserBoss.Mode m_mode;

		// Token: 0x020009B7 RID: 2487
		private enum Mode
		{
			// Token: 0x040037B5 RID: 14261
			Up,
			// Token: 0x040037B6 RID: 14262
			GoTarget
		}
	}
}
