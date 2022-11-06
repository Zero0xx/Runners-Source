using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x020009AE RID: 2478
	public class StatePhantomAsteroidBoss : FSMState<CharacterState>
	{
		// Token: 0x060040D2 RID: 16594 RVA: 0x00150AA0 File Offset: 0x0014ECA0
		public override void Enter(CharacterState context)
		{
			StateUtil.SetRotation(context, Vector3.up, CharacterDefs.BaseFrontTangent);
			this.m_effect = PhantomAsteroidUtil.ChangeVisualOnEnter(context);
			StateUtil.SetNotDrawItemEffect(context, true);
			context.OnAttack(AttackPower.PlayerColorPower, DefensePower.PlayerColorPower);
			context.OnAttackAttribute(AttackAttribute.PhantomAsteroid);
			this.m_time = 5f;
			StateUtil.DeactiveInvincible(context);
			StateUtil.SendMessageTransformPhantom(context, PhantomType.ASTEROID);
			this.m_returnFromPhantom = false;
			context.ChangeMovement(MOVESTATE_ID.GoTargetBoss);
			MsgBossInfo bossInfo = StateUtil.GetBossInfo(null);
			if (bossInfo != null && bossInfo.m_succeed)
			{
				this.m_boss = bossInfo.m_boss;
			}
			this.m_speed = 10f;
			CharacterMoveTargetBoss currentState = context.Movement.GetCurrentState<CharacterMoveTargetBoss>();
			if (currentState != null)
			{
				currentState.SetTarget(this.m_boss);
				currentState.SetSpeed(this.m_speed);
			}
			StateUtil.SetPhantomMagnetColliderRange(context, PhantomType.ASTEROID);
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x00150B68 File Offset: 0x0014ED68
		public override void Leave(CharacterState context)
		{
			context.OffAttack();
			PhantomAsteroidUtil.ChangeVisualOnLeave(context, this.m_effect);
			this.m_effect = null;
			StateUtil.SetNotDrawItemEffect(context, false);
			StateUtil.SendMessageReturnFromPhantom(context, PhantomType.ASTEROID);
			this.m_boss = null;
			context.SetChangePhantomCancel(ItemType.UNKNOWN);
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x00150BA0 File Offset: 0x0014EDA0
		public override void Step(CharacterState context, float deltaTime)
		{
			bool flag = false;
			this.m_speed = Mathf.Min(this.m_speed + 15f * deltaTime, 15f);
			CharacterMoveTargetBoss currentState = context.Movement.GetCurrentState<CharacterMoveTargetBoss>();
			if (currentState != null)
			{
				currentState.SetSpeed(this.m_speed);
				flag = currentState.IsTargetNotFound();
			}
			this.m_time -= deltaTime;
			if (this.m_time < 0f || this.m_returnFromPhantom || flag)
			{
				StateUtil.ReturnFromPhantomAndChangeState(context, PhantomType.ASTEROID, this.m_returnFromPhantom);
				return;
			}
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x00150C30 File Offset: 0x0014EE30
		public override bool DispatchMessage(CharacterState context, int messageId, MessageBase msg)
		{
			if (messageId != 16385)
			{
				return false;
			}
			MsgHitDamageSucceed msgHitDamageSucceed = msg as MsgHitDamageSucceed;
			if (msgHitDamageSucceed != null)
			{
				if (msgHitDamageSucceed.m_sender == this.m_boss)
				{
					this.m_returnFromPhantom = true;
				}
				StateUtil.CreateEffect(context, msgHitDamageSucceed.m_position, msgHitDamageSucceed.m_rotation, "ef_ph_aste_bom01", true);
			}
			return true;
		}

		// Token: 0x0400377A RID: 14202
		private const float FirstSpeed = 10f;

		// Token: 0x0400377B RID: 14203
		private const float MaxSpeed = 15f;

		// Token: 0x0400377C RID: 14204
		private const float SpeedAcc = 15f;

		// Token: 0x0400377D RID: 14205
		private float m_time;

		// Token: 0x0400377E RID: 14206
		private GameObject m_effect;

		// Token: 0x0400377F RID: 14207
		private bool m_returnFromPhantom;

		// Token: 0x04003780 RID: 14208
		private float m_speed;

		// Token: 0x04003781 RID: 14209
		private GameObject m_boss;
	}
}
