using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x020009B2 RID: 2482
	public class StatePhantomDrillBoss : FSMState<CharacterState>
	{
		// Token: 0x060040EA RID: 16618 RVA: 0x0015173C File Offset: 0x0014F93C
		public override void Enter(CharacterState context)
		{
			StateUtil.SetRotation(context, Vector3.up, CharacterDefs.BaseFrontTangent);
			this.m_effect = PhantomDrillUtil.ChangeVisualOnEnter(context);
			this.m_truck = PhantomDrillUtil.CreateTruck(context);
			StateUtil.SetNotDrawItemEffect(context, true);
			context.OnAttack(AttackPower.PlayerColorPower, DefensePower.PlayerColorPower);
			context.OnAttackAttribute(AttackAttribute.PhantomDrill);
			this.m_time = 5f;
			this.m_returnFromPhantom = false;
			this.m_speed = 25f;
			this.m_prevPosition = context.Position;
			this.m_nowInDirt = false;
			StateUtil.DeactiveInvincible(context);
			StateUtil.SendMessageTransformPhantom(context, PhantomType.DRILL);
			MsgBossInfo bossInfo = StateUtil.GetBossInfo(null);
			if (bossInfo != null && bossInfo.m_succeed)
			{
				this.m_boss = bossInfo.m_boss;
				this.GoModeDown(context);
			}
			else
			{
				this.m_returnFromPhantom = true;
			}
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x001517FC File Offset: 0x0014F9FC
		public override void Leave(CharacterState context)
		{
			StateUtil.SetRotation(context, Vector3.up, CharacterDefs.BaseFrontTangent);
			context.OffAttack();
			StateUtil.SetNotDrawItemEffect(context, false);
			PhantomDrillUtil.ChangeVisualOnLeave(context, this.m_effect);
			PhantomDrillUtil.DestroyTruck(this.m_truck);
			this.m_effect = null;
			this.m_truck = null;
			StateUtil.SendMessageReturnFromPhantom(context, PhantomType.DRILL);
			this.m_boss = null;
			context.SetChangePhantomCancel(ItemType.UNKNOWN);
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x00151860 File Offset: 0x0014FA60
		public override void Step(CharacterState context, float deltaTime)
		{
			switch (this.m_mode)
			{
			case StatePhantomDrillBoss.Mode.Down:
				this.StepModeDown(context);
				break;
			case StatePhantomDrillBoss.Mode.Run:
				this.StepModeRun(context);
				break;
			case StatePhantomDrillBoss.Mode.Up:
				this.StepModeUp(context);
				break;
			}
			this.m_time -= deltaTime;
			if (this.m_time < 0f || this.m_returnFromPhantom)
			{
				StateUtil.ReturnFromPhantomAndChangeState(context, PhantomType.DRILL, this.m_returnFromPhantom);
				return;
			}
			bool nowInDirt = this.m_nowInDirt;
			this.m_nowInDirt = PhantomDrillUtil.CheckTruckDraw(context, this.m_truck);
			if ((nowInDirt && !this.m_nowInDirt) || (!nowInDirt && this.m_nowInDirt))
			{
				PhantomDrillUtil.CheckAndCreateFogEffect(context, !nowInDirt && this.m_nowInDirt, this.m_prevPosition);
			}
			this.m_prevPosition = context.Position;
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x00151948 File Offset: 0x0014FB48
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

		// Token: 0x060040EE RID: 16622 RVA: 0x00151994 File Offset: 0x0014FB94
		private void GoModeDown(CharacterState context)
		{
			Vector3 gravityDir = context.Movement.GetGravityDir();
			Vector3 position = context.Position;
			RaycastHit raycastHit;
			if (Physics.Raycast(context.Position, gravityDir, out raycastHit, 30f))
			{
				position = raycastHit.point + gravityDir * 2f;
			}
			context.ChangeMovement(MOVESTATE_ID.GoTarget);
			CharacterMoveTarget movementState = context.GetMovementState<CharacterMoveTarget>();
			if (movementState != null)
			{
				movementState.SetTargetAndSpeed(context.Movement, position, Quaternion.identity, this.m_speed);
				movementState.SetRotateVelocityDir(true);
			}
			this.m_mode = StatePhantomDrillBoss.Mode.Down;
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x00151A20 File Offset: 0x0014FC20
		private void GoModeRun(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.GoTargetBoss);
			CharacterMoveTargetBoss currentState = context.Movement.GetCurrentState<CharacterMoveTargetBoss>();
			if (currentState != null)
			{
				currentState.SetTarget(this.m_boss);
				currentState.SetSpeed(this.m_speed);
				currentState.SetRotateVelocityDir(true);
				currentState.SetOnlyHorizon(true);
			}
			this.m_mode = StatePhantomDrillBoss.Mode.Run;
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x00151A74 File Offset: 0x0014FC74
		private void GoModeUp(CharacterState context)
		{
			CharacterMoveTargetBoss currentState = context.Movement.GetCurrentState<CharacterMoveTargetBoss>();
			if (currentState != null)
			{
				currentState.SetTarget(this.m_boss);
				currentState.SetSpeed(this.m_speed);
				currentState.SetRotateVelocityDir(true);
				currentState.SetOnlyHorizon(false);
			}
			this.m_mode = StatePhantomDrillBoss.Mode.Up;
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x00151AC0 File Offset: 0x0014FCC0
		private void StepModeDown(CharacterState context)
		{
			CharacterMoveTarget movementState = context.GetMovementState<CharacterMoveTarget>();
			if (movementState != null)
			{
				if (movementState.DoesReachTarget())
				{
					this.GoModeRun(context);
				}
			}
			else
			{
				this.m_returnFromPhantom = true;
			}
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x00151AF8 File Offset: 0x0014FCF8
		private void StepModeRun(CharacterState context)
		{
			CharacterMoveTargetBoss movementState = context.GetMovementState<CharacterMoveTargetBoss>();
			if (movementState != null)
			{
				if (movementState.DoesReachTarget())
				{
					this.GoModeUp(context);
				}
				else
				{
					this.m_returnFromPhantom |= movementState.IsTargetNotFound();
				}
			}
			else
			{
				this.m_returnFromPhantom = true;
			}
		}

		// Token: 0x060040F3 RID: 16627 RVA: 0x00151B48 File Offset: 0x0014FD48
		private void StepModeUp(CharacterState context)
		{
			CharacterMoveTargetBoss currentState = context.Movement.GetCurrentState<CharacterMoveTargetBoss>();
			if (currentState != null)
			{
				bool flag = currentState.IsTargetNotFound();
				this.m_returnFromPhantom = (this.m_returnFromPhantom || flag);
			}
			else
			{
				this.m_returnFromPhantom = true;
			}
		}

		// Token: 0x04003799 RID: 14233
		private const float Speed = 25f;

		// Token: 0x0400379A RID: 14234
		private const float SeachGroundLength = 30f;

		// Token: 0x0400379B RID: 14235
		private const float DigLength = 2f;

		// Token: 0x0400379C RID: 14236
		private float m_time;

		// Token: 0x0400379D RID: 14237
		private bool m_returnFromPhantom;

		// Token: 0x0400379E RID: 14238
		private float m_speed;

		// Token: 0x0400379F RID: 14239
		private GameObject m_boss;

		// Token: 0x040037A0 RID: 14240
		private StatePhantomDrillBoss.Mode m_mode;

		// Token: 0x040037A1 RID: 14241
		private GameObject m_effect;

		// Token: 0x040037A2 RID: 14242
		private GameObject m_truck;

		// Token: 0x040037A3 RID: 14243
		private Vector3 m_prevPosition;

		// Token: 0x040037A4 RID: 14244
		private bool m_nowInDirt;

		// Token: 0x020009B3 RID: 2483
		private enum Mode
		{
			// Token: 0x040037A6 RID: 14246
			Down,
			// Token: 0x040037A7 RID: 14247
			Run,
			// Token: 0x040037A8 RID: 14248
			Up
		}
	}
}
