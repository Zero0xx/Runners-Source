using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x02000992 RID: 2450
	public class StateFallingDead : FSMState<CharacterState>
	{
		// Token: 0x06004063 RID: 16483 RVA: 0x0014DCCC File Offset: 0x0014BECC
		public override void Enter(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.Air);
			StateUtil.SetAirMovementToRotateGround(context, true);
			context.Movement.HorzVelocity = Vector3.zero;
			context.GetAnimator().CrossFade("Fall", 0.2f);
			StateUtil.Dead(context);
			SoundManager.SePlay("act_fall", "SE");
			this.m_timer = 0f;
			this.m_sendMessage = false;
			MsgChaoStateUtil.SendMsgChaoState(MsgChaoState.State.STOP);
			if (StageTutorialManager.Instance)
			{
				MsgTutorialMiss value = new MsgTutorialMiss();
				StageTutorialManager.Instance.SendMessage("OnMsgTutorialMiss", value, SendMessageOptions.DontRequireReceiver);
			}
			ObjUtil.SetPlayerDeadRecoveryRing(context.GetPlayerInformation());
			if (context.NowPhantomType != PhantomType.NONE)
			{
				ItemType item = ItemType.UNKNOWN;
				switch (context.NowPhantomType)
				{
				case PhantomType.LASER:
					item = ItemType.LASER;
					break;
				case PhantomType.DRILL:
					item = ItemType.DRILL;
					break;
				case PhantomType.ASTEROID:
					item = ItemType.ASTEROID;
					break;
				}
				StateUtil.SendMessageToTerminateItem(item);
				context.NowPhantomType = PhantomType.NONE;
			}
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x0014DDBC File Offset: 0x0014BFBC
		public override void Leave(CharacterState context)
		{
			context.SetStatus(Status.Dead, false);
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x0014DDC8 File Offset: 0x0014BFC8
		public override void Step(CharacterState context, float deltaTime)
		{
			if (this.m_timer >= 1f)
			{
				context.Movement.Velocity = Vector3.zero;
				if (!this.m_sendMessage)
				{
					StateUtil.CheckCharaChangeOnDieAndSendMessage(context);
					this.m_sendMessage = true;
				}
			}
			else
			{
				this.m_timer += deltaTime;
				context.Movement.Velocity = context.Movement.Velocity + context.Movement.GetGravity() * deltaTime;
			}
		}

		// Token: 0x04003715 RID: 14101
		private float m_timer;

		// Token: 0x04003716 RID: 14102
		private bool m_sendMessage;
	}
}
