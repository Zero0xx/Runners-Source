using System;
using UnityEngine;

namespace Player
{
	// Token: 0x0200097C RID: 2428
	public class CharacterMoveRun : CharacterMoveBase
	{
		// Token: 0x06003FBB RID: 16315 RVA: 0x0014B5C4 File Offset: 0x001497C4
		public override void Enter(CharacterMovement context)
		{
			this.m_sweepHitInfo = default(HitInfo);
			this.m_errorTime = 0f;
			this.m_prevPos = Vector3.zero;
			this.m_errorTimeMax = context.Parameter.m_hitWallDeadTime * 2f;
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x0014B610 File Offset: 0x00149810
		public override void Leave(CharacterMovement context)
		{
			this.m_sweepHitInfo.Reset();
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x0014B620 File Offset: 0x00149820
		public override void Step(CharacterMovement context, float deltaTime)
		{
			float num = context.Velocity.magnitude * deltaTime;
			if (num <= 0.0001f)
			{
				context.SetRaycastCheckPosition(context.RaycastCheckPosition);
				return;
			}
			MovementUtil.SweepMoveForRunAndAir(context, deltaTime, ref this.m_sweepHitInfo);
			Vector3 raycastCheckPosition = context.RaycastCheckPosition;
			MovementUtil.CheckAndPushOutByRaycast(context.transform, context.RaycastCheckPosition, ref raycastCheckPosition);
			context.SetRaycastCheckPosition(raycastCheckPosition);
			if (this.m_prevPos == raycastCheckPosition)
			{
				this.m_errorTime += deltaTime;
				if (this.m_errorTime > this.m_errorTimeMax)
				{
					CapsuleCollider component = context.GetComponent<CapsuleCollider>();
					if (component != null)
					{
						int layer = -1 - (1 << LayerMask.NameToLayer("Player"));
						MovementUtil.SweepMoveInnerParam innerParam = new MovementUtil.SweepMoveInnerParam(component, new Vector3(-0.2f, 0.1f, 0f), layer);
						MovementUtil.SweepMoveOuterParam outerParam = new MovementUtil.SweepMoveOuterParam();
						MovementUtil.SweepMove(context.transform, innerParam, outerParam);
					}
				}
			}
			else
			{
				this.m_errorTime = 0f;
			}
			this.m_prevPos = raycastCheckPosition;
			context.SetSweepHitInfo(this.m_sweepHitInfo);
			if (Vector3.Dot(context.transform.forward, CharacterDefs.BaseFrontTangent) < -0.866f)
			{
				global::Debug.Log("Warning:CharacterRotate is Reversed.");
				context.SetLookRotation(context.transform.forward, context.transform.up);
			}
		}

		// Token: 0x040036A9 RID: 13993
		private float m_errorTime;

		// Token: 0x040036AA RID: 13994
		private float m_errorTimeMax;

		// Token: 0x040036AB RID: 13995
		private Vector3 m_prevPos = Vector3.zero;

		// Token: 0x040036AC RID: 13996
		private HitInfo m_sweepHitInfo;
	}
}
