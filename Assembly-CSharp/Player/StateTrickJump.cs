using System;
using App.Utility;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x020009C1 RID: 2497
	public class StateTrickJump : FSMState<CharacterState>
	{
		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06004133 RID: 16691 RVA: 0x00153744 File Offset: 0x00151944
		// (set) Token: 0x06004134 RID: 16692 RVA: 0x00153754 File Offset: 0x00151954
		private bool Succeed
		{
			get
			{
				return this.m_flag.Test(0);
			}
			set
			{
				this.m_flag.Set(0, value);
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06004135 RID: 16693 RVA: 0x00153764 File Offset: 0x00151964
		// (set) Token: 0x06004136 RID: 16694 RVA: 0x00153774 File Offset: 0x00151974
		private bool EnableTrick
		{
			get
			{
				return this.m_flag.Test(1);
			}
			set
			{
				this.m_flag.Set(1, value);
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06004137 RID: 16695 RVA: 0x00153784 File Offset: 0x00151984
		// (set) Token: 0x06004138 RID: 16696 RVA: 0x00153794 File Offset: 0x00151994
		private bool TrickEnd
		{
			get
			{
				return this.m_flag.Test(2);
			}
			set
			{
				this.m_flag.Set(2, value);
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06004139 RID: 16697 RVA: 0x001537A4 File Offset: 0x001519A4
		// (set) Token: 0x0600413A RID: 16698 RVA: 0x001537B4 File Offset: 0x001519B4
		private bool Falling
		{
			get
			{
				return this.m_flag.Test(3);
			}
			set
			{
				this.m_flag.Set(3, value);
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x0600413B RID: 16699 RVA: 0x001537C4 File Offset: 0x001519C4
		// (set) Token: 0x0600413C RID: 16700 RVA: 0x001537D4 File Offset: 0x001519D4
		private bool AutoSucceed
		{
			get
			{
				return this.m_flag.Test(4);
			}
			set
			{
				this.m_flag.Set(4, value);
			}
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x001537E4 File Offset: 0x001519E4
		public override void Enter(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.Air);
			StateUtil.SetAirMovementToRotateGround(context, false);
			this.m_flag.Reset();
			this.m_outOfControlTime = 0f;
			Vector3 velocity = Vector3.zero;
			TrickJumpParameter enteringParameter = context.GetEnteringParameter<TrickJumpParameter>();
			if (enteringParameter != null)
			{
				context.Movement.ResetPosition(enteringParameter.m_position);
				StateUtil.SetRotation(context, -context.Movement.GetGravityDir());
				this.m_outOfControlTime = enteringParameter.m_outOfControlTime;
				float d = enteringParameter.m_firstSpeed;
				velocity = enteringParameter.m_rotation * Vector3.up * d;
				this.Succeed = enteringParameter.m_succeed;
			}
			StageAbilityManager instance = StageAbilityManager.Instance;
			if (instance != null)
			{
				ChaoAbility ability = ChaoAbility.JUMP_RAMP;
				if (instance.HasChaoAbility(ability))
				{
					float chaoAbilityValue = instance.GetChaoAbilityValue(ability);
					float num = UnityEngine.Random.Range(0f, 99.9f);
					if (chaoAbilityValue >= num)
					{
						if (!this.Succeed)
						{
							this.m_outOfControlTime = enteringParameter.m_succeedOutOfcontrol;
							float d = enteringParameter.m_succeedFirstSpeed;
							velocity = enteringParameter.m_succeedRotation * Vector3.up * d;
						}
						this.Succeed = true;
						instance.RequestPlayChaoEffect(ability);
					}
				}
			}
			context.Movement.Velocity = velocity;
			if (this.Succeed)
			{
				context.GetAnimator().CrossFade("TrickJumpIdle", 0.1f);
				this.EnableTrick = true;
				this.m_jumpCamera = true;
				SoundManager.SePlay("obj_jumpboard_ok", "SE");
				StateUtil.SetJumpRampMagnet(context, true);
				ChaoAbility ability2 = ChaoAbility.JUMP_RAMP_TRICK_SUCCESS;
				if (instance.HasChaoAbility(ability2))
				{
					float chaoAbilityValue2 = instance.GetChaoAbilityValue(ability2);
					float num2 = UnityEngine.Random.Range(0f, 99.9f);
					if (chaoAbilityValue2 >= num2)
					{
						this.AutoSucceed = true;
						ObjUtil.RequestStartAbilityToChao(ability2, false);
					}
				}
			}
			else
			{
				context.GetAnimator().CrossFade("Damaged", 0.1f);
				this.m_jumpCamera = false;
				SoundManager.SePlay("obj_jumpboard_ng", "SE");
			}
			context.OnAttack(AttackPower.PlayerSpin, DefensePower.PlayerSpin);
			StateUtil.SetAttackAttributePowerIfPowerType(context, true);
			context.SetNotCharaChange(true);
			this.m_numTrick = 0;
			context.ClearAirAction();
			if (this.m_jumpCamera && context.GetCamera() != null)
			{
				MsgPushCamera value = new MsgPushCamera(CameraType.JUMPBOARD, 0.5f, null);
				context.GetCamera().SendMessage("OnPushCamera", value);
			}
			StateUtil.ThroughBreakable(context, true);
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x00153A44 File Offset: 0x00151C44
		public override void Leave(CharacterState context)
		{
			context.OffAttack();
			context.SetNotCharaChange(false);
			if (this.m_jumpCamera && context.GetCamera() != null)
			{
				MsgPopCamera value = new MsgPopCamera(CameraType.JUMPBOARD, 0.5f);
				context.GetCamera().SendMessage("OnPopCamera", value);
			}
			StateUtil.ThroughBreakable(context, false);
			StateUtil.SetJumpRampMagnet(context, false);
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x00153AA8 File Offset: 0x00151CA8
		public override void Step(CharacterState context, float deltaTime)
		{
			this.m_outOfControlTime -= deltaTime;
			this.CheckTrick(context, deltaTime);
			if (this.Succeed && !this.TrickEnd && !this.EnableTrick)
			{
				this.CheckAnimation(context, deltaTime);
			}
			if (this.m_outOfControlTime < 0f)
			{
				StateUtil.ThroughBreakable(context, false);
				if (!this.Falling && !this.Succeed)
				{
					context.GetAnimator().CrossFade("Fall", 0.3f);
				}
				this.Falling = true;
				Vector3 vector = context.Movement.Velocity;
				vector += context.Movement.GetGravity() * deltaTime;
				context.Movement.Velocity = vector;
			}
			if (context.Movement.GetVertVelocityScalar() <= 0f && context.Movement.IsOnGround())
			{
				StateUtil.NowLanding(context, true);
				context.ChangeState(STATE_ID.Run);
				return;
			}
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x00153BA4 File Offset: 0x00151DA4
		private void CheckTrick(CharacterState context, float deltaTime)
		{
			CharacterInput component = context.GetComponent<CharacterInput>();
			if (component != null && this.EnableTrick && !StateUtil.IsAnimationInTransition(context) && (this.AutoSucceed || component.IsTouched()))
			{
				int num = CharacterDefs.TrickScore[this.m_numTrick];
				MsgCaution caution = new MsgCaution(HudCaution.Type.TRICK0 + this.m_numTrick);
				HudCaution.Instance.SetCaution(caution);
				MsgCaution caution2 = new MsgCaution(HudCaution.Type.TRICK_BONUS_N, num);
				HudCaution.Instance.SetCaution(caution2);
				ObjUtil.SendMessageAddScore(num);
				ObjUtil.SendMessageScoreCheck(new StageScoreData(1, num));
				string text = "TrickJump";
				text += (this.m_numTrick % 3 + 1).ToString("D1");
				context.GetAnimator().CrossFade(text, 0.05f);
				GameObject gameobj = StateUtil.CreateEffect(context, "ef_pl_trick01", true);
				StateUtil.SetObjectLocalPositionToCenter(context, gameobj);
				this.EnableTrick = false;
				this.m_numTrick++;
				this.m_animTime = 0.25f;
				if (this.m_numTrick < 5)
				{
					SoundManager.SePlay("obj_jumpboard_trick", "SE");
				}
				else
				{
					SoundManager.SePlay("obj_jumpboard_trick_last", "SE");
				}
			}
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x00153CE0 File Offset: 0x00151EE0
		private void CheckAnimation(CharacterState context, float deltaTime)
		{
			if (!this.EnableTrick && this.m_numTrick > 0)
			{
				if (this.m_animTime > 0f)
				{
					this.m_animTime -= deltaTime;
					if (this.m_animTime <= 0f)
					{
						context.GetAnimator().CrossFade("TrickJumpIdle", 0.05f);
						this.m_animTime = -1f;
					}
				}
				else if (this.m_numTrick >= 5)
				{
					context.GetAnimator().CrossFade("Fall", 0.5f);
					this.TrickEnd = true;
					this.EnableTrick = false;
				}
				else
				{
					this.EnableTrick = true;
				}
			}
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x00153D94 File Offset: 0x00151F94
		public override bool DispatchMessage(CharacterState context, int messageId, MessageBase msg)
		{
			return this.Falling && StateUtil.ChangeAfterSpinattack(context, messageId, msg);
		}

		// Token: 0x040037E9 RID: 14313
		private const float LerpDelta = 0.5f;

		// Token: 0x040037EA RID: 14314
		private const float cos5 = 0.9962f;

		// Token: 0x040037EB RID: 14315
		private bool m_jumpCamera;

		// Token: 0x040037EC RID: 14316
		private float m_animTime = 0.2f;

		// Token: 0x040037ED RID: 14317
		private Bitset32 m_flag;

		// Token: 0x040037EE RID: 14318
		private float m_outOfControlTime;

		// Token: 0x040037EF RID: 14319
		private int m_numTrick;

		// Token: 0x020009C2 RID: 2498
		private enum Flags
		{
			// Token: 0x040037F1 RID: 14321
			SUCCEED,
			// Token: 0x040037F2 RID: 14322
			ENABLE_TRICK,
			// Token: 0x040037F3 RID: 14323
			TRICK_END,
			// Token: 0x040037F4 RID: 14324
			ISFALL,
			// Token: 0x040037F5 RID: 14325
			AUTO_SUCCEED
		}
	}
}
