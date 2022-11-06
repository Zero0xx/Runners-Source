using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x0200099F RID: 2463
	public class StateDead : FSMState<CharacterState>
	{
		// Token: 0x0600408A RID: 16522 RVA: 0x0014ED2C File Offset: 0x0014CF2C
		public override void Enter(CharacterState context)
		{
			context.GetAnimator().CrossFade("Dead", 0.1f);
			StateUtil.Dead(context);
			context.ChangeMovement(MOVESTATE_ID.Air);
			context.Movement.OffGround();
			Vector3 velocity = context.Movement.GetGravityDir() * -6f + context.Movement.GetForwardDir() * -2f;
			context.Movement.Velocity = velocity;
			SoundManager.SePlay("act_damage", "SE");
			this.m_sendMessage = false;
			this.m_timer = 1f;
			context.ClearAirAction();
			MsgChaoStateUtil.SendMsgChaoState(MsgChaoState.State.STOP);
			if (StageTutorialManager.Instance != null)
			{
				MsgTutorialMiss value = new MsgTutorialMiss();
				StageTutorialManager.Instance.SendMessage("OnMsgTutorialMiss", value, SendMessageOptions.DontRequireReceiver);
			}
			GameObjectUtil.SendDelayedMessageToTagObjects("Boss", "OnPlayerDamage", new MsgBossPlayerDamage(true));
			ObjUtil.SetPlayerDeadRecoveryRing(context.GetPlayerInformation());
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x0014EE18 File Offset: 0x0014D018
		public override void Leave(CharacterState context)
		{
			context.SetStatus(Status.Dead, false);
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x0014EE24 File Offset: 0x0014D024
		public override void Step(CharacterState context, float deltaTime)
		{
			if (!context.Movement.IsOnGround())
			{
				context.Movement.Velocity = context.Movement.Velocity + context.Movement.GetGravity() * deltaTime;
			}
			else
			{
				context.Movement.Velocity = Vector3.zero;
			}
			if (!this.m_sendMessage && this.m_timer > 0f)
			{
				this.m_timer -= deltaTime;
				if (this.m_timer <= 0f)
				{
					StateUtil.CheckCharaChangeOnDieAndSendMessage(context);
					this.m_sendMessage = true;
				}
			}
		}

		// Token: 0x04003749 RID: 14153
		private bool m_sendMessage;

		// Token: 0x0400374A RID: 14154
		private float m_timer = 1f;
	}
}
