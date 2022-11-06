using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x020009AA RID: 2474
	public class StateLastChance : FSMState<CharacterState>
	{
		// Token: 0x060040BF RID: 16575 RVA: 0x00150298 File Offset: 0x0014E498
		public override void Enter(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.Air);
			StateUtil.SetAirMovementToRotateGround(context, false);
			StateUtil.ActiveChaoAbilityMagnetObject(context);
			this.m_time = 3f;
			if (StageAbilityManager.Instance != null)
			{
				this.m_time = StageAbilityManager.Instance.GetChaoAbliltyValue(ChaoAbility.LAST_CHANCE, this.m_time);
			}
			context.SetNotCharaChange(true);
			context.SetNotUseItem(true);
			context.SetLastChance(true);
			context.ClearAirAction();
			context.SetModelNotDraw(true);
			this.GotoTarget(context);
			StateUtil.DeactiveInvincible(context);
			StateUtil.DeactiveBarrier(context);
			StateUtil.DeactiveMagetObject(context);
			StateUtil.DeactiveTrampoline(context);
			MsgStartLastChance value = new MsgStartLastChance(context.gameObject);
			GameObjectUtil.SendMessageToTagObjects("Chao", "OnStartLastChance", value, SendMessageOptions.DontRequireReceiver);
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x0015034C File Offset: 0x0014E54C
		public override void Leave(CharacterState context)
		{
			context.OffAttack();
			context.SetNotCharaChange(false);
			context.SetNotUseItem(false);
			context.SetLastChance(false);
			context.SetModelNotDraw(false);
			StateUtil.DeactiveChaoAbilityMagetObject(context);
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x00150384 File Offset: 0x0014E584
		public override void Step(CharacterState context, float deltaTime)
		{
			switch (this.m_substate)
			{
			case StateLastChance.SubState.GoTarget:
				this.StepTarget(context, deltaTime);
				break;
			case StateLastChance.SubState.Run:
				this.StepRun(context, deltaTime);
				break;
			case StateLastChance.SubState.EndAction:
				this.m_time -= deltaTime;
				if (this.m_time < 0f)
				{
					this.GotoEndWait(context);
				}
				break;
			}
		}

		// Token: 0x060040C2 RID: 16578 RVA: 0x001503FC File Offset: 0x0014E5FC
		private void StepTarget(CharacterState context, float deltaTime)
		{
			float magnitude = context.Movement.Velocity.magnitude;
			float num = Vector3.Distance(this.m_targetPos, context.Position);
			Vector3 a = Vector3.Normalize(this.m_targetPos - context.Position);
			if (num > magnitude * deltaTime)
			{
				context.Movement.Velocity = a * context.Parameter.m_lastChanceSpeed;
				return;
			}
			context.Movement.ResetPosition(this.m_targetPos);
			this.GotoRun(context);
		}

		// Token: 0x060040C3 RID: 16579 RVA: 0x0015048C File Offset: 0x0014E68C
		private void StepRun(CharacterState context, float deltaTime)
		{
			this.m_time -= deltaTime;
			if (this.m_time < 0f)
			{
				this.GotoEndAction(context);
				return;
			}
			if (context.GetLevelInformation().NowFeverBoss)
			{
				this.GotoEndAction(context);
				return;
			}
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x001504D8 File Offset: 0x0014E6D8
		private void GotoTarget(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.IgnoreCollision);
			PathEvaluator stagePathEvaluator = StateUtil.GetStagePathEvaluator(context, BlockPathController.PathType.SV);
			if (stagePathEvaluator != null)
			{
				Vector3 worldPosition = stagePathEvaluator.GetWorldPosition();
				this.m_targetPos = stagePathEvaluator.GetWorldPosition() + Vector3.up * 2f;
				float num = Vector3.Dot(context.Position - worldPosition, -context.Movement.GetGravityDir());
				if (num < 0f)
				{
					context.Movement.ResetPosition(context.Position + num * context.Movement.GetGravityDir());
					context.GetPlayerInformation().SetTransform(context.transform);
				}
			}
			else
			{
				this.m_targetPos = context.Position;
			}
			StateUtil.SetRotation(context, Vector3.up, CharacterDefs.BaseFrontTangent);
			this.m_substate = StateLastChance.SubState.GoTarget;
		}

		// Token: 0x060040C5 RID: 16581 RVA: 0x001505B0 File Offset: 0x0014E7B0
		private void GotoRun(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.RunOnPathPhantomDrill);
			CharacterMoveOnPathPhantomDrill currentState = context.Movement.GetCurrentState<CharacterMoveOnPathPhantomDrill>();
			if (currentState != null)
			{
				currentState.SetupPath(context.Movement, BlockPathController.PathType.SV, true, 2f);
				currentState.SetSpeed(context.Movement, context.Parameter.m_lastChanceSpeed);
			}
			this.m_substate = StateLastChance.SubState.Run;
		}

		// Token: 0x060040C6 RID: 16582 RVA: 0x00150608 File Offset: 0x0014E808
		private void GotoEndAction(CharacterState context)
		{
			context.Movement.ChangeState(MOVESTATE_ID.IgnoreCollision);
			StateUtil.ResetVelocity(context);
			MsgEndLastChance value = new MsgEndLastChance();
			GameObjectUtil.SendMessageToTagObjects("Chao", "OnEndLastChance", value, SendMessageOptions.DontRequireReceiver);
			this.m_time = 1f;
			this.m_substate = StateLastChance.SubState.EndAction;
		}

		// Token: 0x060040C7 RID: 16583 RVA: 0x00150650 File Offset: 0x0014E850
		private void GotoEndWait(CharacterState context)
		{
			MsgNotifyDead value = new MsgNotifyDead();
			GameObject gameObject = GameObject.Find("GameModeStage");
			if (gameObject)
			{
				gameObject.SendMessage("OnMsgNotifyDead", value, SendMessageOptions.DontRequireReceiver);
			}
			GameObjectUtil.SendDelayedMessageToTagObjects("Boss", "OnMsgNotifyDead", value);
			this.m_substate = StateLastChance.SubState.EndWait;
		}

		// Token: 0x0400376C RID: 14188
		private const float offset = 2f;

		// Token: 0x0400376D RID: 14189
		private StateLastChance.SubState m_substate;

		// Token: 0x0400376E RID: 14190
		private float m_time;

		// Token: 0x0400376F RID: 14191
		private Vector3 m_targetPos;

		// Token: 0x020009AB RID: 2475
		private enum SubState
		{
			// Token: 0x04003771 RID: 14193
			GoTarget,
			// Token: 0x04003772 RID: 14194
			Run,
			// Token: 0x04003773 RID: 14195
			EndAction,
			// Token: 0x04003774 RID: 14196
			EndWait
		}
	}
}
