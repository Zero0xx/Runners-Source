using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x020009B0 RID: 2480
	public class StatePhantomDrill : FSMState<CharacterState>
	{
		// Token: 0x060040DE RID: 16606 RVA: 0x00150FD8 File Offset: 0x0014F1D8
		public override void Enter(CharacterState context)
		{
			StateUtil.DeactiveInvincible(context);
			StateUtil.SetNotDrawItemEffect(context, true);
			StateUtil.SetRotation(context, Vector3.up, CharacterDefs.BaseFrontTangent);
			this.m_effect = PhantomDrillUtil.ChangeVisualOnEnter(context);
			this.m_truck = PhantomDrillUtil.CreateTruck(context);
			context.OnAttack(AttackPower.PlayerColorPower, DefensePower.PlayerColorPower);
			context.OnAttackAttribute(AttackAttribute.PhantomDrill);
			this.m_time = -1f;
			ChangePhantomParameter enteringParameter = context.GetEnteringParameter<ChangePhantomParameter>();
			if (enteringParameter != null)
			{
				this.m_time = enteringParameter.Timer;
			}
			this.m_targetPath = StateUtil.GetStagePathEvaluator(context, BlockPathController.PathType.DRILL);
			if (this.m_targetPath != null)
			{
				this.m_targetPos = this.m_targetPath.GetWorldPosition();
			}
			this.GotoDown(context);
			this.m_prevPosition = context.Position;
			this.m_nowInDirt = false;
			StateUtil.SendMessageTransformPhantom(context, PhantomType.DRILL);
			if (context.GetChangePhantomCancel() == ItemType.DRILL)
			{
				this.m_changePhantomCancel = true;
			}
			else
			{
				this.m_changePhantomCancel = false;
			}
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x001510BC File Offset: 0x0014F2BC
		public override void Leave(CharacterState context)
		{
			context.OffAttack();
			StateUtil.SetNotDrawItemEffect(context, false);
			PhantomDrillUtil.ChangeVisualOnLeave(context, this.m_effect);
			PhantomDrillUtil.DestroyTruck(this.m_truck);
			this.m_effect = null;
			this.m_truck = null;
			this.m_targetPath = null;
			StateUtil.SendMessageReturnFromPhantom(context, PhantomType.DRILL);
			context.SetChangePhantomCancel(ItemType.UNKNOWN);
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x00151110 File Offset: 0x0014F310
		public override void Step(CharacterState context, float deltaTime)
		{
			if (this.m_changePhantomCancel)
			{
				this.m_changePhantomCancel = false;
				this.DispatchMessage(context, 12289, new MsgInvalidateItem(ItemType.DRILL));
				return;
			}
			switch (this.m_substate)
			{
			case StatePhantomDrill.SubState.GODOWN:
				if (this.StepGoDown(context, deltaTime))
				{
					return;
				}
				break;
			case StatePhantomDrill.SubState.RUN:
				if (this.StepRunning(context, deltaTime))
				{
					return;
				}
				break;
			case StatePhantomDrill.SubState.RETURN:
				if (this.StepReturn(context, deltaTime))
				{
					return;
				}
				break;
			}
			bool nowInDirt = this.m_nowInDirt;
			this.m_nowInDirt = PhantomDrillUtil.CheckTruckDraw(context, this.m_truck);
			if ((nowInDirt && !this.m_nowInDirt) || (!nowInDirt && this.m_nowInDirt))
			{
				PhantomDrillUtil.CheckAndCreateFogEffect(context, !nowInDirt && this.m_nowInDirt, this.m_prevPosition);
			}
			this.m_prevPosition = context.Position;
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x001511FC File Offset: 0x0014F3FC
		private bool StepGoDown(CharacterState context, float deltaTime)
		{
			float magnitude = context.Movement.Velocity.magnitude;
			float num = Vector3.Distance(this.m_targetPos, context.Position);
			Vector3 vector = Vector3.Normalize(this.m_targetPos - context.Position);
			if (num > magnitude * deltaTime)
			{
				Vector3 up = Vector3.Cross(vector, context.transform.right);
				StateUtil.SetRotation(context, up, vector);
				context.Movement.Velocity = vector * context.Parameter.m_drillSpeed;
			}
			else
			{
				context.Movement.ResetPosition(this.m_targetPos);
				this.GotoRun(context);
			}
			return false;
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x001512A4 File Offset: 0x0014F4A4
		private bool StepRunning(CharacterState context, float deltaTime)
		{
			Vector3 velocity = context.Parameter.m_drillSpeed * CharacterDefs.BaseFrontTangent;
			context.Movement.Velocity = velocity;
			if (this.m_time > 0f)
			{
				this.m_time -= deltaTime;
				if (this.m_time < 0f)
				{
					this.GotoReturn(context);
					return false;
				}
			}
			CharacterMoveOnPathPhantomDrill currentState = context.Movement.GetCurrentState<CharacterMoveOnPathPhantomDrill>();
			if (currentState != null && currentState.IsPathEnd(0f))
			{
				this.GotoReturn(context);
				return false;
			}
			if (currentState != null && context.m_input.IsTouched())
			{
				currentState.Jump(context.Movement);
			}
			return false;
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x00151358 File Offset: 0x0014F558
		private bool StepReturn(CharacterState context, float deltaTime)
		{
			float magnitude = context.Movement.Velocity.magnitude;
			float num = Vector3.Distance(this.m_targetPos, context.Position);
			Vector3 vector = Vector3.Normalize(this.m_targetPos - context.Position);
			if (num > magnitude * deltaTime)
			{
				Vector3 up = Vector3.Cross(vector, context.transform.right);
				StateUtil.SetRotation(context, up, vector);
				context.Movement.Velocity = vector * context.Parameter.m_drillSpeed;
				return false;
			}
			StateUtil.SetRotation(context, Vector3.up, CharacterDefs.BaseFrontTangent);
			context.Movement.ResetPosition(this.m_targetPos);
			context.Movement.Velocity = context.Movement.GetForwardDir() * context.DefaultSpeed + context.Movement.GetUpDir() * 14f;
			StateUtil.ReturnFromPhantomAndChangeState(context, PhantomType.DRILL, false);
			return true;
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x00151450 File Offset: 0x0014F650
		private void GotoDown(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.IgnoreCollision);
			this.m_substate = StatePhantomDrill.SubState.GODOWN;
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x00151460 File Offset: 0x0014F660
		private void GotoRun(CharacterState context)
		{
			this.StartPathMove(context);
			this.m_substate = StatePhantomDrill.SubState.RUN;
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x00151470 File Offset: 0x0014F670
		private void GotoReturn(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.IgnoreCollision);
			PathEvaluator stagePathEvaluator = StateUtil.GetStagePathEvaluator(context, BlockPathController.PathType.SV);
			if (stagePathEvaluator != null)
			{
				this.m_targetPos = stagePathEvaluator.GetWorldPosition();
				Vector3 a = -context.Movement.GetGravityDir();
				Vector3 origin = this.m_targetPos + a * 1.5f;
				Ray ray = new Ray(origin, -a);
				int layerMask = 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Terrain");
				layerMask = -1 - (1 << LayerMask.NameToLayer("Player"));
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit, 3f, layerMask))
				{
					this.m_targetPos = raycastHit.point + raycastHit.normal * 0.1f;
				}
				else
				{
					origin = this.m_targetPos + a * 5f;
					if (Physics.Raycast(ray, out raycastHit, 5f, layerMask))
					{
						this.m_targetPos = raycastHit.point + raycastHit.normal * 0.1f;
					}
					else
					{
						this.m_targetPos = stagePathEvaluator.GetWorldPosition() + a * 2f;
					}
				}
			}
			else
			{
				this.m_targetPos = context.Position;
			}
			CapsuleCollider component = context.GetComponent<CapsuleCollider>();
			if (component != null)
			{
				int layerMask2 = 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Terrain");
				Vector3 vector = -context.Movement.GetGravityDir();
				if (StateUtil.CheckOverlapSphere(this.m_targetPos, vector, component.height * 0.5f + 0.2f, layerMask2))
				{
					float num = 2f;
					StateUtil.CapsuleCast(component, this.m_targetPos + vector * num, vector, layerMask2, -vector, num - 0.1f, ref this.m_targetPos, true);
				}
			}
			this.m_substate = StatePhantomDrill.SubState.RETURN;
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x00151678 File Offset: 0x0014F878
		private void StartPathMove(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.RunOnPathPhantomDrill);
			CharacterMoveOnPathPhantomDrill currentState = context.Movement.GetCurrentState<CharacterMoveOnPathPhantomDrill>();
			if (currentState != null)
			{
				currentState.SetupPath(context.Movement, BlockPathController.PathType.DRILL, false, 0f);
				currentState.SetSpeed(context.Movement, context.Parameter.m_drillSpeed);
			}
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x001516CC File Offset: 0x0014F8CC
		public override bool DispatchMessage(CharacterState context, int messageId, MessageBase msg)
		{
			if (messageId != 12289)
			{
				return false;
			}
			MsgInvalidateItem msgInvalidateItem = msg as MsgInvalidateItem;
			if (msgInvalidateItem != null && msgInvalidateItem.m_itemType == ItemType.DRILL)
			{
				if (this.m_substate == StatePhantomDrill.SubState.RUN)
				{
					this.GotoReturn(context);
				}
				else if (this.m_substate == StatePhantomDrill.SubState.GODOWN)
				{
					StateUtil.ReturnFromPhantomAndChangeState(context, PhantomType.DRILL, false);
				}
				return true;
			}
			return true;
		}

		// Token: 0x04003786 RID: 14214
		private const float jump_force = 14f;

		// Token: 0x04003787 RID: 14215
		private const float lerpDelta = 3f;

		// Token: 0x04003788 RID: 14216
		private const float godown_offset = 1.5f;

		// Token: 0x04003789 RID: 14217
		private const float ray_offset = 3f;

		// Token: 0x0400378A RID: 14218
		private const float ray_offset2 = 5f;

		// Token: 0x0400378B RID: 14219
		private const float offset_noground = 2f;

		// Token: 0x0400378C RID: 14220
		private float m_time;

		// Token: 0x0400378D RID: 14221
		private StatePhantomDrill.SubState m_substate;

		// Token: 0x0400378E RID: 14222
		private GameObject m_truck;

		// Token: 0x0400378F RID: 14223
		private Vector3 m_targetPos = Vector3.zero;

		// Token: 0x04003790 RID: 14224
		private PathEvaluator m_targetPath;

		// Token: 0x04003791 RID: 14225
		private GameObject m_effect;

		// Token: 0x04003792 RID: 14226
		private Vector3 m_prevPosition;

		// Token: 0x04003793 RID: 14227
		private bool m_nowInDirt;

		// Token: 0x04003794 RID: 14228
		private bool m_changePhantomCancel;

		// Token: 0x020009B1 RID: 2481
		private enum SubState
		{
			// Token: 0x04003796 RID: 14230
			GODOWN,
			// Token: 0x04003797 RID: 14231
			RUN,
			// Token: 0x04003798 RID: 14232
			RETURN
		}
	}
}
