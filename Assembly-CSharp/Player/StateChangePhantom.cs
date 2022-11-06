using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x0200099C RID: 2460
	public class StateChangePhantom : FSMState<CharacterState>
	{
		// Token: 0x0600407E RID: 16510 RVA: 0x0014E710 File Offset: 0x0014C910
		public override void Enter(CharacterState context)
		{
			StateUtil.ResetVelocity(context);
			context.ChangeMovement(MOVESTATE_ID.Air);
			context.OnAttack(AttackPower.PlayerColorPower, DefensePower.PlayerColorPower);
			this.m_phantomType = PhantomType.NONE;
			this.m_animTriggerName = null;
			this.m_transformTime = -1f;
			string effectname = null;
			this.m_chaoAbility = new ChaoAbility[]
			{
				ChaoAbility.COLOR_POWER_SCORE,
				ChaoAbility.COLOR_POWER_TIME,
				ChaoAbility.UNKNOWN,
				ChaoAbility.UNKNOWN
			};
			ChangePhantomParameter enteringParameter = context.GetEnteringParameter<ChangePhantomParameter>();
			if (enteringParameter != null)
			{
				this.m_phantomType = enteringParameter.ChangeType;
				switch (this.m_phantomType)
				{
				case PhantomType.LASER:
					this.m_animTriggerName = "StartLaser";
					effectname = "ef_pl_change_laser01";
					this.m_chaoAbility[2] = ChaoAbility.LASER_SCORE;
					this.m_chaoAbility[3] = ChaoAbility.LASER_TIME;
					break;
				case PhantomType.DRILL:
					this.m_animTriggerName = "StartDrill";
					effectname = "ef_pl_change_drill01";
					this.m_chaoAbility[2] = ChaoAbility.DRILL_SCORE;
					this.m_chaoAbility[3] = ChaoAbility.DRILL_TIME;
					break;
				case PhantomType.ASTEROID:
					this.m_animTriggerName = "StartAsteroid";
					effectname = "ef_pl_change_asteroid01";
					this.m_chaoAbility[2] = ChaoAbility.ASTEROID_SCORE;
					this.m_chaoAbility[3] = ChaoAbility.ASTEROID_TIME;
					break;
				}
				this.m_transformTime = enteringParameter.Timer;
			}
			context.GetAnimator().CrossFade(this.m_animTriggerName, 0.1f);
			GameObject gameobj = StateUtil.CreateEffect(context, effectname, true);
			StateUtil.SetObjectLocalPositionToCenter(context, gameobj);
			SoundManager.SePlay("phantom_change", "SE");
			context.SetNotCharaChange(true);
			context.SetNotUseItem(true);
			context.ClearAirAction();
			MsgPhantomActStart value = new MsgPhantomActStart(this.m_phantomType);
			GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnSendToGameModeStage", value, SendMessageOptions.DontRequireReceiver);
			ObjUtil.RequestStartAbilityToChao(this.m_chaoAbility, true);
			StateUtil.SetPhantomQuickTimerPause(true);
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x0014E8A8 File Offset: 0x0014CAA8
		public override void Leave(CharacterState context)
		{
			context.OffAttack();
			this.m_animTriggerName = null;
			if (!context.IsOnDestroy())
			{
				MsgPhantomActEnd value = new MsgPhantomActEnd(this.m_phantomType);
				GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnSendToGameModeStage", value, SendMessageOptions.DontRequireReceiver);
			}
			StateUtil.SetPhantomQuickTimerPause(false);
			this.m_chaoAbility = null;
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x0014E8F8 File Offset: 0x0014CAF8
		public override void Step(CharacterState context, float deltaTime)
		{
			string animTriggerName = this.m_animTriggerName;
			if (StateUtil.IsAnimationEnd(context, animTriggerName))
			{
				bool flag = false;
				if (context.GetLevelInformation() != null)
				{
					flag = context.GetLevelInformation().NowBoss;
				}
				STATE_ID state = STATE_ID.Run;
				switch (this.m_phantomType)
				{
				case PhantomType.LASER:
					state = ((!flag) ? STATE_ID.PhantomLaser : STATE_ID.PhantomLaserBoss);
					break;
				case PhantomType.DRILL:
					state = ((!flag) ? STATE_ID.PhantomDrill : STATE_ID.PhantomDrillBoss);
					break;
				case PhantomType.ASTEROID:
					state = ((!flag) ? STATE_ID.PhantomAsteroid : STATE_ID.PhantomAsteroidBoss);
					break;
				}
				if (!flag)
				{
					ChangePhantomParameter changePhantomParameter = context.CreateEnteringParameter<ChangePhantomParameter>();
					changePhantomParameter.Set(this.m_phantomType, this.m_transformTime);
				}
				ObjUtil.RequestEndAbilityToChao(this.m_chaoAbility);
				context.ChangeState(state);
				return;
			}
		}

		// Token: 0x04003742 RID: 14146
		private string m_animTriggerName;

		// Token: 0x04003743 RID: 14147
		private PhantomType m_phantomType;

		// Token: 0x04003744 RID: 14148
		private float m_transformTime;

		// Token: 0x04003745 RID: 14149
		private ChaoAbility[] m_chaoAbility;
	}
}
