using System;
using UnityEngine;

namespace Player
{
	// Token: 0x02000976 RID: 2422
	public class CharacterMoveAsteroid : CharacterMoveBase
	{
		// Token: 0x06003F92 RID: 16274 RVA: 0x0014A738 File Offset: 0x00148938
		public override void Enter(CharacterMovement context)
		{
			this.m_sweepHitInfo = default(HitInfo);
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x0014A754 File Offset: 0x00148954
		public override void Leave(CharacterMovement context)
		{
			this.m_sweepHitInfo.Reset();
		}

		// Token: 0x06003F94 RID: 16276 RVA: 0x0014A764 File Offset: 0x00148964
		public override void Step(CharacterMovement context, float deltaTime)
		{
			float num = context.Velocity.magnitude * deltaTime;
			if (num <= 0.0001f)
			{
				context.SetRaycastCheckPosition(context.RaycastCheckPosition);
				return;
			}
			MovementUtil.SweepMoveForRunAndAir(context, deltaTime, ref this.m_sweepHitInfo);
			if (!context.IsOnGround() && !this.m_sweepHitInfo.IsValid())
			{
				MovementUtil.UpdateRotateOnGravityUp(context, 720f, deltaTime);
			}
			Vector3 raycastCheckPosition = context.RaycastCheckPosition;
			MovementUtil.CheckAndPushOutByRaycast(context.transform, context.RaycastCheckPosition, ref raycastCheckPosition);
			context.SetRaycastCheckPosition(raycastCheckPosition);
			context.SetSweepHitInfo(this.m_sweepHitInfo);
			if (Vector3.Dot(context.transform.forward, CharacterDefs.BaseFrontTangent) < -0.866f)
			{
				global::Debug.Log("Warning:CharacterRotate is Reversed.");
				context.SetLookRotation(context.transform.forward, context.transform.up);
			}
		}

		// Token: 0x04003699 RID: 13977
		private const float RotateDegree = 720f;

		// Token: 0x0400369A RID: 13978
		private HitInfo m_sweepHitInfo;
	}
}
