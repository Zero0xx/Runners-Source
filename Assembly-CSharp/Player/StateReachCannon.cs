using System;
using App.Utility;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x02000995 RID: 2453
	public class StateReachCannon : FSMState<CharacterState>
	{
		// Token: 0x0600406D RID: 16493 RVA: 0x0014DEE8 File Offset: 0x0014C0E8
		public override void Enter(CharacterState context)
		{
			CannonReachParameter enteringParameter = context.GetEnteringParameter<CannonReachParameter>();
			if (enteringParameter != null)
			{
				this.m_reachPosition = enteringParameter.m_position;
				this.m_height = enteringParameter.m_height;
				this.m_catchedObject = enteringParameter.m_catchedObject;
			}
			else
			{
				this.m_reachPosition = context.Position;
				this.m_height = 0f;
				this.m_catchedObject = null;
			}
			this.m_flag.Reset();
			context.ChangeMovement(MOVESTATE_ID.Air);
			this.m_substate = StateReachCannon.SubState.JUMP;
			StateUtil.SetAirMovementToRotateGround(context, false);
			context.GetAnimator().CrossFade("SpinBall", 0.1f);
			this.CalcReachedVelocity(context);
			this.m_timer = 0.25f;
			context.SetNotCharaChange(true);
			context.SetNotUseItem(true);
			context.ClearAirAction();
			ObjUtil.PauseCombo(MsgPauseComboTimer.State.PAUSE, -1f);
			ObjUtil.SetDisableEquipItem(true);
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x0014DFB8 File Offset: 0x0014C1B8
		public override void Leave(CharacterState context)
		{
			if (!this.m_flag.Test(0))
			{
				if (this.m_flag.Test(1))
				{
					context.SetModelNotDraw(false);
					StateUtil.SetNotDrawItemEffect(context, false);
				}
				if (this.m_catchedObject != null)
				{
					MsgOnExitAbideObject value = new MsgOnExitAbideObject();
					this.m_catchedObject.SendMessage("OnExitAbideObject", value);
				}
			}
			context.SetNotCharaChange(false);
			context.SetNotUseItem(false);
			ObjUtil.PauseCombo(MsgPauseComboTimer.State.PLAY, -1f);
			ObjUtil.SetDisableEquipItem(false);
		}

		// Token: 0x0600406F RID: 16495 RVA: 0x0014E03C File Offset: 0x0014C23C
		public override void Step(CharacterState context, float deltaTime)
		{
			switch (this.m_substate)
			{
			case StateReachCannon.SubState.JUMP:
				context.Movement.Velocity += context.Movement.GetGravity() * deltaTime;
				this.m_timer -= deltaTime;
				if (this.m_timer <= 0f)
				{
					context.ChangeMovement(MOVESTATE_ID.GoTarget);
					StateUtil.SetTargetMovement(context, this.m_reachPosition, context.transform.rotation, 0.2f);
					this.m_timer = 0.2f;
					this.m_substate = StateReachCannon.SubState.GOTARGET;
				}
				break;
			case StateReachCannon.SubState.GOTARGET:
				if (!this.m_flag.Test(1) && (this.m_reachPosition - context.Position).sqrMagnitude < this.m_height * this.m_height)
				{
					context.SetModelNotDraw(true);
					StateUtil.SetNotDrawItemEffect(context, true);
					this.m_flag.Set(1, true);
				}
				this.m_timer -= deltaTime;
				if (this.m_timer <= 0f)
				{
					if (!this.m_flag.Test(1))
					{
						context.SetModelNotDraw(true);
						StateUtil.SetNotDrawItemEffect(context, true);
						this.m_flag.Set(1, true);
					}
					if (this.m_catchedObject != null)
					{
						MsgOnAbidePlayerLocked value = new MsgOnAbidePlayerLocked();
						this.m_catchedObject.SendMessage("OnAbidePlayerLocked", value);
					}
					StateUtil.ResetVelocity(context);
					context.ChangeMovement(MOVESTATE_ID.Air);
					this.m_substate = StateReachCannon.SubState.HOLD;
				}
				break;
			case StateReachCannon.SubState.HOLD:
				StateUtil.ResetVelocity(context);
				break;
			}
		}

		// Token: 0x06004070 RID: 16496 RVA: 0x0014E1D8 File Offset: 0x0014C3D8
		public override bool DispatchMessage(CharacterState context, int messageId, MessageBase msg)
		{
			if (messageId != 24578)
			{
				return false;
			}
			if (this.m_substate != StateReachCannon.SubState.HOLD)
			{
				return true;
			}
			MsgOnCannonImpulse msgOnCannonImpulse = msg as MsgOnCannonImpulse;
			if (msgOnCannonImpulse != null)
			{
				CannonLaunchParameter cannonLaunchParameter = context.CreateEnteringParameter<CannonLaunchParameter>();
				if (cannonLaunchParameter != null)
				{
					cannonLaunchParameter.Set(msgOnCannonImpulse.m_position, msgOnCannonImpulse.m_rotation, msgOnCannonImpulse.m_firstSpeed, this.m_height, msgOnCannonImpulse.m_outOfControl);
					this.m_flag.Set(0, true);
					context.ChangeState(STATE_ID.LaunchCannon);
					msgOnCannonImpulse.m_succeed = true;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06004071 RID: 16497 RVA: 0x0014E268 File Offset: 0x0014C468
		private void CalcReachedVelocity(CharacterState context)
		{
			Vector3 vector = this.m_reachPosition - context.Position;
			Vector3 b = Vector3.Project(vector, -context.Movement.GetGravityDir());
			Vector3 a = vector - b;
			Vector3 gravity = context.Movement.GetGravity();
			float num = 0.5f;
			float num2 = b.magnitude + 1.5f;
			float d = (num2 + 0.5f * gravity.magnitude * num * num) / num;
			this.m_vertVelocity = Vector3.up * d;
			this.m_horzVelocity = a / 0.5f;
			context.Movement.Velocity = this.m_vertVelocity + this.m_horzVelocity;
		}

		// Token: 0x04003720 RID: 14112
		private const float ReachedTime = 0.5f;

		// Token: 0x04003721 RID: 14113
		private const float HeightOffset = 1.5f;

		// Token: 0x04003722 RID: 14114
		private const float GoTargetTime = 0.2f;

		// Token: 0x04003723 RID: 14115
		private Vector3 m_reachPosition;

		// Token: 0x04003724 RID: 14116
		private float m_height;

		// Token: 0x04003725 RID: 14117
		private GameObject m_catchedObject;

		// Token: 0x04003726 RID: 14118
		private Vector3 m_horzVelocity;

		// Token: 0x04003727 RID: 14119
		private Vector3 m_vertVelocity;

		// Token: 0x04003728 RID: 14120
		private float m_timer;

		// Token: 0x04003729 RID: 14121
		private Bitset32 m_flag;

		// Token: 0x0400372A RID: 14122
		private StateReachCannon.SubState m_substate;

		// Token: 0x02000996 RID: 2454
		private enum Flags
		{
			// Token: 0x0400372C RID: 14124
			LAUNCH_CANNON,
			// Token: 0x0400372D RID: 14125
			MODEL_OFF
		}

		// Token: 0x02000997 RID: 2455
		private enum SubState
		{
			// Token: 0x0400372F RID: 14127
			JUMP,
			// Token: 0x04003730 RID: 14128
			GOTARGET,
			// Token: 0x04003731 RID: 14129
			HOLD
		}
	}
}
