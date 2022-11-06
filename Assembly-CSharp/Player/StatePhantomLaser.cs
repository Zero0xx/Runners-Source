using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x020009B5 RID: 2485
	public class StatePhantomLaser : FSMState<CharacterState>
	{
		// Token: 0x060040F8 RID: 16632 RVA: 0x00151C20 File Offset: 0x0014FE20
		public override void Enter(CharacterState context)
		{
			StateUtil.SetRotation(context, Vector3.up, CharacterDefs.BaseFrontTangent);
			PhantomLaserUtil.ChangeVisualOnEnter(context);
			context.OnAttack(AttackPower.PlayerColorPower, DefensePower.PlayerColorPower);
			context.OnAttackAttribute(AttackAttribute.PhantomLaser);
			context.ChangeMovement(MOVESTATE_ID.RunOnPathPhantom);
			CharacterMoveOnPathPhantom currentState = context.Movement.GetCurrentState<CharacterMoveOnPathPhantom>();
			if (currentState != null)
			{
				currentState.SetupPath(context.Movement, BlockPathController.PathType.LASER);
			}
			SoundManager.SePlay("phantom_laser_shoot", "SE");
			this.m_time = -1f;
			ChangePhantomParameter enteringParameter = context.GetEnteringParameter<ChangePhantomParameter>();
			if (enteringParameter != null)
			{
				this.m_time = enteringParameter.Timer;
			}
			this.m_drawnWaitTimer = 0f;
			StateUtil.DeactiveInvincible(context);
			StateUtil.SendMessageTransformPhantom(context, PhantomType.LASER);
			if (context.GetCamera() != null)
			{
				MsgPushCamera value = new MsgPushCamera(CameraType.LASER, 0.5f, null);
				context.GetCamera().SendMessage("OnPushCamera", value);
			}
			StateUtil.SetNotDrawItemEffect(context, true);
			this.SetOffDrawingPhantomMagnet(context, true);
			StateUtil.SetPhantomMagnetColliderRange(context, PhantomType.LASER);
			if (context.GetChangePhantomCancel() == ItemType.LASER)
			{
				this.m_changePhantomCancel = true;
			}
			else
			{
				this.m_changePhantomCancel = false;
			}
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x00151D28 File Offset: 0x0014FF28
		public override void Leave(CharacterState context)
		{
			StateUtil.SetNotDrawItemEffect(context, false);
			StateUtil.SetRotation(context, Vector3.up, CharacterDefs.BaseFrontTangent);
			PhantomLaserUtil.ChangeVisualOnLeave(context);
			context.OffAttack();
			this.SetOffDrawingPhantomMagnet(context, true);
			StateUtil.SendMessageReturnFromPhantom(context, PhantomType.LASER);
			if (context.GetCamera() != null)
			{
				MsgPopCamera value = new MsgPopCamera(CameraType.LASER, 0.5f);
				context.GetCamera().SendMessage("OnPopCamera", value);
			}
			context.SetChangePhantomCancel(ItemType.UNKNOWN);
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x00151D9C File Offset: 0x0014FF9C
		public override void Step(CharacterState context, float deltaTime)
		{
			if (this.m_changePhantomCancel)
			{
				this.m_changePhantomCancel = false;
				this.DispatchMessage(context, 12289, new MsgInvalidateItem(ItemType.LASER));
				return;
			}
			Vector3 velocity = context.Parameter.m_laserSpeed * CharacterDefs.BaseFrontTangent;
			context.Movement.Velocity = velocity;
			if (context.m_input.IsTouched())
			{
				this.m_drawnWaitTimer = context.Parameter.m_laserDrawingTime;
				this.SetOffDrawingPhantomMagnet(context, false);
			}
			if (this.m_drawnWaitTimer > 0f)
			{
				this.m_drawnWaitTimer -= deltaTime;
				if (this.m_drawnWaitTimer <= 0f)
				{
					this.SetOffDrawingPhantomMagnet(context, true);
				}
			}
			if (this.m_time > 0f)
			{
				this.m_time -= deltaTime;
				if (this.m_time < 0f)
				{
					StateUtil.ResetVelocity(context);
					StateUtil.ReturnFromPhantomAndChangeState(context, PhantomType.LASER, false);
					return;
				}
			}
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x00151E90 File Offset: 0x00150090
		public override bool DispatchMessage(CharacterState context, int messageId, MessageBase msg)
		{
			if (messageId != 12289)
			{
				return false;
			}
			MsgInvalidateItem msgInvalidateItem = msg as MsgInvalidateItem;
			if (msgInvalidateItem != null && msgInvalidateItem.m_itemType == ItemType.LASER)
			{
				StateUtil.ResetVelocity(context);
				StateUtil.ReturnFromPhantomAndChangeState(context, PhantomType.LASER, false);
				return true;
			}
			return true;
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x00151EDC File Offset: 0x001500DC
		public void SetOffDrawingPhantomMagnet(CharacterState context, bool value)
		{
			GameObject parent = GameObjectUtil.FindChildGameObject(context.gameObject, CharacterDefs.PhantomBodyName[0]);
			CharacterMagnetPhantom characterMagnetPhantom = GameObjectUtil.FindChildGameObjectComponent<CharacterMagnetPhantom>(parent, "MagnetCollision");
			if (characterMagnetPhantom != null)
			{
				characterMagnetPhantom.SetOffDrawing(value);
			}
		}

		// Token: 0x040037AB RID: 14251
		private float m_time;

		// Token: 0x040037AC RID: 14252
		private float m_drawnWaitTimer;

		// Token: 0x040037AD RID: 14253
		private bool m_changePhantomCancel;
	}
}
