using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x020009AD RID: 2477
	public class StatePhantomAsteroid : FSMState<CharacterState>
	{
		// Token: 0x060040CD RID: 16589 RVA: 0x00150784 File Offset: 0x0014E984
		public override void Enter(CharacterState context)
		{
			context.ChangeMovement(MOVESTATE_ID.Asteroid);
			StateUtil.SetRotation(context, Vector3.up, CharacterDefs.BaseFrontTangent);
			this.m_effect = PhantomAsteroidUtil.ChangeVisualOnEnter(context);
			context.OnAttack(AttackPower.PlayerColorPower, DefensePower.PlayerColorPower);
			context.OnAttackAttribute(AttackAttribute.PhantomAsteroid);
			StateUtil.DeactiveInvincible(context);
			StateUtil.SetNotDrawItemEffect(context, true);
			StateUtil.SetPhantomMagnetColliderRange(context, PhantomType.ASTEROID);
			this.m_time = -1f;
			ChangePhantomParameter enteringParameter = context.GetEnteringParameter<ChangePhantomParameter>();
			if (enteringParameter != null)
			{
				this.m_time = enteringParameter.Timer;
			}
			StateUtil.SendMessageTransformPhantom(context, PhantomType.ASTEROID);
			if (context.GetChangePhantomCancel() == ItemType.ASTEROID)
			{
				this.m_changePhantomCancel = true;
			}
			else
			{
				this.m_changePhantomCancel = false;
			}
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x00150824 File Offset: 0x0014EA24
		public override void Leave(CharacterState context)
		{
			context.OffAttack();
			StateUtil.SetNotDrawItemEffect(context, false);
			PhantomAsteroidUtil.ChangeVisualOnLeave(context, this.m_effect);
			this.m_effect = null;
			StateUtil.SendMessageReturnFromPhantom(context, PhantomType.ASTEROID);
			context.SetNotCharaChange(false);
			context.SetNotUseItem(false);
			context.SetChangePhantomCancel(ItemType.UNKNOWN);
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x00150870 File Offset: 0x0014EA70
		public override void Step(CharacterState context, float deltaTime)
		{
			if (this.m_changePhantomCancel)
			{
				this.m_changePhantomCancel = false;
				this.DispatchMessage(context, 12289, new MsgInvalidateItem(ItemType.ASTEROID));
				return;
			}
			Vector3 a = context.Parameter.m_asteroidSpeed * context.Movement.GetForwardDir();
			Vector3 b = Vector3.zero;
			float limitHeitht = context.Parameter.m_limitHeitht;
			Vector3 position = context.Position;
			StateUtil.GetBaseGroundPosition(context, ref position);
			if (context.m_input.IsHold())
			{
				float num = Vector3.Magnitude(context.Position - position);
				if (num < limitHeitht)
				{
					b = -context.Movement.GetGravityDir() * context.Parameter.m_asteroidUpForce;
				}
			}
			else if (!context.Movement.IsOnGround())
			{
				b = context.Movement.GetGravityDir() * context.Parameter.m_asteroidDownForce;
			}
			context.Movement.Velocity = a + b;
			GameObject modelObject = PhantomAsteroidUtil.GetModelObject(context);
			if (modelObject != null)
			{
				Transform transform = modelObject.transform;
				Quaternion identity = Quaternion.identity;
				identity.SetLookRotation(CharacterDefs.BaseFrontTangent, -context.Movement.GetGravityDir());
				transform.rotation = identity;
			}
			if (this.m_time > 0f)
			{
				this.m_time -= deltaTime;
				if (this.m_time < 0f)
				{
					StateUtil.ResetVelocity(context);
					StateUtil.ReturnFromPhantomAndChangeState(context, PhantomType.ASTEROID, false);
					return;
				}
			}
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x001509F8 File Offset: 0x0014EBF8
		public override bool DispatchMessage(CharacterState context, int messageId, MessageBase msg)
		{
			switch (messageId)
			{
			case 12289:
			{
				MsgInvalidateItem msgInvalidateItem = msg as MsgInvalidateItem;
				if (msgInvalidateItem != null && msgInvalidateItem.m_itemType == ItemType.ASTEROID)
				{
					StateUtil.ResetVelocity(context);
					StateUtil.ReturnFromPhantomAndChangeState(context, PhantomType.ASTEROID, false);
					return true;
				}
				return true;
			}
			default:
			{
				if (messageId != 16385)
				{
					return false;
				}
				MsgHitDamageSucceed msgHitDamageSucceed = msg as MsgHitDamageSucceed;
				if (msgHitDamageSucceed != null)
				{
					StateUtil.CreateEffect(context, msgHitDamageSucceed.m_position, msgHitDamageSucceed.m_rotation, "ef_ph_aste_bom01", true);
				}
				return true;
			}
			case 12292:
				StateUtil.ResetVelocity(context);
				StateUtil.ReturnFromPhantomAndChangeState(context, PhantomType.ASTEROID, false);
				return true;
			}
		}

		// Token: 0x04003777 RID: 14199
		private float m_time;

		// Token: 0x04003778 RID: 14200
		private GameObject m_effect;

		// Token: 0x04003779 RID: 14201
		private bool m_changePhantomCancel;
	}
}
