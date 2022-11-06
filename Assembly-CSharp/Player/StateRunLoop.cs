using System;
using Message;
using Tutorial;
using UnityEngine;

namespace Player
{
	// Token: 0x020009BA RID: 2490
	public class StateRunLoop : FSMState<CharacterState>
	{
		// Token: 0x06004112 RID: 16658 RVA: 0x001526A8 File Offset: 0x001508A8
		public override void Enter(CharacterState context)
		{
			if (context.TestStatus(Status.NowLanding))
			{
				context.GetAnimator().CrossFade("Landing", 0.05f);
			}
			else
			{
				context.GetAnimator().CrossFade("Run", 0.1f);
			}
			RunLoopPathParameter enteringParameter = context.GetEnteringParameter<RunLoopPathParameter>();
			if (enteringParameter != null && enteringParameter.m_pathComponent != null)
			{
				float? distance = null;
				context.ChangeMovement(MOVESTATE_ID.RunOnPath);
				CharacterMoveOnPath currentState = context.Movement.GetCurrentState<CharacterMoveOnPath>();
				if (currentState != null)
				{
					currentState.SetupPath(context.Position, enteringParameter.m_pathComponent, distance);
				}
			}
			this.m_speed = context.Movement.HorzVelocity.magnitude;
			this.m_loopEffect = GameObjectUtil.FindChildGameObjectComponent<CharacterLoopEffect>(context.gameObject, "CharacterBoost");
			this.m_exLoopEffect = GameObjectUtil.FindChildGameObjectComponent<CharacterLoopEffect>(context.gameObject, "CharacterBoostEx");
			this.m_effectTime = 0f;
			float num = 0f;
			this.m_onBoost = StateUtil.SetRunningAnimationSpeed(context, ref num);
			if (this.m_onBoost)
			{
				this.CreateParaLoop(context);
			}
			context.ClearAirAction();
			context.SetNotCharaChange(true);
			context.SetNotUseItem(true);
			if (context.GetCamera() != null)
			{
				MsgPushCamera value = new MsgPushCamera(CameraType.LOOP_TERRAIN, 0.5f, null);
				context.GetCamera().SendMessage("OnPushCamera", value);
			}
			ObjUtil.PauseCombo(MsgPauseComboTimer.State.PAUSE, -1f);
			ObjUtil.SetQuickModeTimePause(true);
			ObjUtil.SetDisableEquipItem(true);
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x00152818 File Offset: 0x00150A18
		public override void Leave(CharacterState context)
		{
			this.m_onBoost = false;
			StateUtil.SetOnBoost(context, this.m_loopEffect, false);
			this.DestroyParaloop(context);
			context.SetNotCharaChange(false);
			context.SetNotUseItem(false);
			if (context.GetCamera() != null)
			{
				MsgPopCamera value = new MsgPopCamera(CameraType.LOOP_TERRAIN, 2.5f);
				context.GetCamera().SendMessage("OnPopCamera", value);
			}
			ObjUtil.PauseCombo(MsgPauseComboTimer.State.PLAY, -1f);
			this.m_loopEffect = null;
			if (this.m_exLoopEffect != null)
			{
				StateUtil.SetOnBoostEx(context, this.m_loopEffect, false);
				this.m_exLoopEffect = null;
			}
			ObjUtil.SetQuickModeTimePause(false);
			ObjUtil.SetDisableEquipItem(false);
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x001528C0 File Offset: 0x00150AC0
		public override void Step(CharacterState context, float deltaTime)
		{
			CharacterMoveOnPath currentState = context.Movement.GetCurrentState<CharacterMoveOnPath>();
			if (currentState != null && currentState.IsPathEnd(0.1f))
			{
				context.ChangeState(STATE_ID.Run);
				return;
			}
			this.m_speed = this.GetRunningSpeed(context, deltaTime);
			context.Movement.Velocity = context.Movement.GetForwardDir() * this.m_speed;
			float num = 0f;
			bool onBoost = this.m_onBoost;
			this.m_onBoost = StateUtil.SetRunningAnimationSpeed(context, ref num);
			this.CheckOnBoost(context, onBoost);
			this.CheckOnBoostEx(context, num);
			if (!this.m_onBoost)
			{
				StateUtil.CheckAndCreateRunEffect(context, ref this.m_effectTime, this.m_speed, num, deltaTime);
			}
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x00152970 File Offset: 0x00150B70
		private float GetRunningSpeed(CharacterState context, float deltaTime)
		{
			float speed = this.m_speed;
			float num = 20f;
			float minLoopRunSpeed = context.Parameter.m_minLoopRunSpeed;
			float num2 = Mathf.Max(minLoopRunSpeed, context.DefaultSpeed);
			Vector3 forwardDir = context.Movement.GetForwardDir();
			bool flag = Vector3.Dot(forwardDir, context.Movement.GetGravityDir()) > 0.1736f;
			float runLoopAccel = context.Parameter.m_runLoopAccel;
			if (flag)
			{
				num2 = num;
			}
			if (speed > num2)
			{
				this.m_speed = Mathf.Max(speed - context.Parameter.m_runDec * deltaTime, num2);
			}
			else if (speed < minLoopRunSpeed)
			{
				this.m_speed = minLoopRunSpeed;
			}
			else
			{
				this.m_speed = Mathf.Min(speed + runLoopAccel * deltaTime, num2);
			}
			return this.m_speed;
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x00152A34 File Offset: 0x00150C34
		private void CheckOnBoost(CharacterState context, bool oldBoost)
		{
			if (!oldBoost && this.m_onBoost)
			{
				StateUtil.SetOnBoost(context, this.m_loopEffect, true);
			}
			else if (oldBoost && !this.m_onBoost)
			{
				StateUtil.SetOnBoost(context, this.m_loopEffect, false);
			}
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x00152A84 File Offset: 0x00150C84
		private void CheckOnBoostEx(CharacterState context, float speed)
		{
			if (this.m_exLoopEffect != null)
			{
				bool flag = 0.6f < speed && speed < 0.9f;
				if (flag && !this.m_onBoostEx)
				{
					StateUtil.SetOnBoostEx(context, this.m_exLoopEffect, true);
					this.m_onBoostEx = true;
				}
				else if (!flag && this.m_onBoostEx)
				{
					StateUtil.SetOnBoostEx(context, this.m_exLoopEffect, false);
					this.m_onBoostEx = false;
				}
			}
		}

		// Token: 0x06004118 RID: 16664 RVA: 0x00152B08 File Offset: 0x00150D08
		private void CreateParaLoop(CharacterState context)
		{
			this.m_effectParaloop = StateUtil.CreateEffect(context, "ef_pl_paraloop01", false);
			if (this.m_effectParaloop)
			{
				StateUtil.SetObjectLocalPositionToCenter(context, this.m_effectParaloop);
			}
			SoundManager.SePlay("act_paraloop", "SE");
			context.SetStatus(Status.Paraloop, true);
			ObjUtil.RequestStartAbilityToChao(ChaoAbility.LOOP_COMBO_UP, false);
			ObjUtil.RequestStartAbilityToChao(ChaoAbility.LOOP_MAGNET, false);
			ObjUtil.SendMessageTutorialClear(EventID.PARA_LOOP);
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x00152B74 File Offset: 0x00150D74
		private void DestroyParaloop(CharacterState context)
		{
			if (this.m_effectParaloop != null)
			{
				StateUtil.DestroyParticle(this.m_effectParaloop, 1f);
				this.m_effectParaloop = null;
			}
			context.SetStatus(Status.Paraloop, false);
		}

		// Token: 0x040037BF RID: 14271
		private const float MaxSpeed = 20f;

		// Token: 0x040037C0 RID: 14272
		private float m_speed;

		// Token: 0x040037C1 RID: 14273
		private bool m_onBoost;

		// Token: 0x040037C2 RID: 14274
		private bool m_onBoostEx;

		// Token: 0x040037C3 RID: 14275
		private CharacterLoopEffect m_loopEffect;

		// Token: 0x040037C4 RID: 14276
		private CharacterLoopEffect m_exLoopEffect;

		// Token: 0x040037C5 RID: 14277
		private GameObject m_effectParaloop;

		// Token: 0x040037C6 RID: 14278
		private float m_effectTime;
	}
}
