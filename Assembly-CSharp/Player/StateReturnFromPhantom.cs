using System;

namespace Player
{
	// Token: 0x0200099D RID: 2461
	public class StateReturnFromPhantom : FSMState<CharacterState>
	{
		// Token: 0x06004082 RID: 16514 RVA: 0x0014E9D4 File Offset: 0x0014CBD4
		public override void Enter(CharacterState context)
		{
			StateUtil.ResetVelocity(context);
			context.ChangeMovement(MOVESTATE_ID.Air);
			StateUtil.SetAirMovementToRotateGround(context, true);
			context.OnAttack(AttackPower.PlayerColorPower, DefensePower.PlayerColorPower);
			this.m_phantomType = PhantomType.NONE;
			this.m_animTriggerName = null;
			ChangePhantomParameter enteringParameter = context.GetEnteringParameter<ChangePhantomParameter>();
			if (enteringParameter != null)
			{
				this.m_phantomType = enteringParameter.ChangeType;
				switch (this.m_phantomType)
				{
				case PhantomType.LASER:
					this.m_animTriggerName = "Laser";
					break;
				case PhantomType.DRILL:
					this.m_animTriggerName = "Drill";
					break;
				case PhantomType.ASTEROID:
					this.m_animTriggerName = "Asteroid";
					break;
				}
			}
			context.GetAnimator().CrossFade(this.m_animTriggerName, 0.1f);
			SoundManager.SePlay("phantom_change", "SE");
			StateUtil.SetPhantomQuickTimerPause(true);
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x0014EAA0 File Offset: 0x0014CCA0
		public override void Leave(CharacterState context)
		{
			context.OffAttack();
			this.m_animTriggerName = null;
			StateUtil.SetPhantomQuickTimerPause(false);
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x0014EAB8 File Offset: 0x0014CCB8
		public override void Step(CharacterState context, float deltaTime)
		{
			string animName = "End" + this.m_animTriggerName;
			if (StateUtil.IsAnimationEnd(context, animName))
			{
				context.ChangeState(STATE_ID.Fall);
				return;
			}
		}

		// Token: 0x04003746 RID: 14150
		private string m_animTriggerName;

		// Token: 0x04003747 RID: 14151
		private PhantomType m_phantomType;
	}
}
