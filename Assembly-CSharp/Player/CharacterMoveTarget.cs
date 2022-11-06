using System;
using UnityEngine;

namespace Player
{
	// Token: 0x0200097D RID: 2429
	public class CharacterMoveTarget : CharacterMoveBase
	{
		// Token: 0x06003FBF RID: 16319 RVA: 0x0014B780 File Offset: 0x00149980
		public override void Enter(CharacterMovement context)
		{
			this.m_rotateVelocityDir = false;
			this.m_reachTarget = false;
		}

		// Token: 0x06003FC0 RID: 16320 RVA: 0x0014B790 File Offset: 0x00149990
		public override void Leave(CharacterMovement context)
		{
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x0014B794 File Offset: 0x00149994
		public override void Step(CharacterMovement context, float deltaTime)
		{
			float sqrMagnitude = (context.transform.position - this.m_targetPosition).sqrMagnitude;
			float num = this.m_speed * deltaTime;
			if (sqrMagnitude < num * num)
			{
				context.transform.position = this.m_targetPosition;
				this.m_reachTarget = true;
			}
			else
			{
				Vector3 normalized = (this.m_targetPosition - context.transform.position).normalized;
				context.transform.position += normalized * this.m_speed * deltaTime;
				if (this.m_rotateVelocityDir)
				{
					Vector3 front = normalized;
					Vector3 up = Vector3.Cross(normalized, CharacterDefs.BaseRightTangent);
					context.SetLookRotation(front, up);
				}
			}
		}

		// Token: 0x06003FC2 RID: 16322 RVA: 0x0014B85C File Offset: 0x00149A5C
		public void SetTarget(CharacterMovement context, Vector3 position, Quaternion rotation, float time)
		{
			this.m_targetPosition = position;
			this.m_speed = Vector3.Distance(this.m_targetPosition, context.transform.position) / time;
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x0014B890 File Offset: 0x00149A90
		public void SetTargetAndSpeed(CharacterMovement context, Vector3 position, Quaternion rotation, float speed)
		{
			this.m_targetPosition = position;
			this.m_speed = speed;
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x0014B8A4 File Offset: 0x00149AA4
		public void SetRotateVelocityDir(bool value)
		{
			this.m_rotateVelocityDir = value;
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x0014B8B0 File Offset: 0x00149AB0
		public bool DoesReachTarget()
		{
			return this.m_reachTarget;
		}

		// Token: 0x040036AD RID: 13997
		private Vector3 m_targetPosition;

		// Token: 0x040036AE RID: 13998
		private float m_speed;

		// Token: 0x040036AF RID: 13999
		private bool m_rotateVelocityDir;

		// Token: 0x040036B0 RID: 14000
		private bool m_reachTarget;
	}
}
