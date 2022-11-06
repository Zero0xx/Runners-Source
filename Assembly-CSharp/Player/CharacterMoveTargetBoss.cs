using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x0200097E RID: 2430
	public class CharacterMoveTargetBoss : CharacterMoveBase
	{
		// Token: 0x06003FC7 RID: 16327 RVA: 0x0014B8C0 File Offset: 0x00149AC0
		public override void Enter(CharacterMovement context)
		{
			this.m_bossObject = null;
			this.m_rotateVelocityDir = false;
			this.m_targetNotFound = false;
			this.m_onlyHorizon = false;
			this.m_reachTarget = false;
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x0014B8E8 File Offset: 0x00149AE8
		public override void Leave(CharacterMovement context)
		{
			this.m_bossObject = null;
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x0014B8F4 File Offset: 0x00149AF4
		public override void Step(CharacterMovement context, float deltaTime)
		{
			bool flag = false;
			Vector3 vector = context.transform.position;
			if (this.m_bossObject != null)
			{
				MsgBossInfo msgBossInfo = new MsgBossInfo();
				this.m_bossObject.SendMessage("OnMsgBossInfo", msgBossInfo);
				if (msgBossInfo.m_succeed)
				{
					vector = this.m_bossObject.transform.position;
					flag = true;
				}
			}
			this.m_targetNotFound = !flag;
			if (this.m_onlyHorizon)
			{
				Vector3 lhs = vector - context.transform.position;
				Vector3 vector2 = -context.GetGravityDir();
				Vector3 b = Vector3.Dot(lhs, vector2) * vector2;
				vector -= b;
			}
			float sqrMagnitude = (context.transform.position - vector).sqrMagnitude;
			float num = this.m_speed * deltaTime;
			if (sqrMagnitude < num * num)
			{
				context.transform.position = vector;
				this.m_reachTarget = true;
			}
			else
			{
				Vector3 normalized = (vector - context.transform.position).normalized;
				context.transform.position += normalized * this.m_speed * deltaTime;
				if (this.m_rotateVelocityDir)
				{
					Vector3 front = normalized;
					Vector3 up = Vector3.Cross(normalized, CharacterDefs.BaseRightTangent);
					context.SetLookRotation(front, up);
				}
			}
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x0014BA5C File Offset: 0x00149C5C
		public void SetTarget(GameObject targetObject)
		{
			this.m_bossObject = targetObject;
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x0014BA68 File Offset: 0x00149C68
		public void SetSpeed(float speed)
		{
			this.m_speed = speed;
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x0014BA74 File Offset: 0x00149C74
		public bool IsTargetNotFound()
		{
			return this.m_targetNotFound;
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x0014BA7C File Offset: 0x00149C7C
		public void SetRotateVelocityDir(bool value)
		{
			this.m_rotateVelocityDir = value;
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x0014BA88 File Offset: 0x00149C88
		public void SetOnlyHorizon(bool value)
		{
			this.m_onlyHorizon = value;
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x0014BA94 File Offset: 0x00149C94
		public bool DoesReachTarget()
		{
			return this.m_reachTarget;
		}

		// Token: 0x040036B1 RID: 14001
		private GameObject m_bossObject;

		// Token: 0x040036B2 RID: 14002
		private float m_speed;

		// Token: 0x040036B3 RID: 14003
		private bool m_rotateVelocityDir;

		// Token: 0x040036B4 RID: 14004
		private bool m_targetNotFound;

		// Token: 0x040036B5 RID: 14005
		private bool m_onlyHorizon;

		// Token: 0x040036B6 RID: 14006
		private bool m_reachTarget;
	}
}
