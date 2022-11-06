using System;
using UnityEngine;

namespace Player
{
	// Token: 0x02000975 RID: 2421
	public class CharacterMoveAir : CharacterMoveBase
	{
		// Token: 0x06003F8D RID: 16269 RVA: 0x0014A484 File Offset: 0x00148684
		public override void Enter(CharacterMovement context)
		{
			this.m_sweepHitInfo = default(HitInfo);
			context.OffGround();
			this.SetRotateToGround(false);
			this.m_errorTime = 0f;
			this.m_prevPos = Vector3.zero;
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x0014A4C4 File Offset: 0x001486C4
		public override void Leave(CharacterMovement context)
		{
			this.m_sweepHitInfo.Reset();
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x0014A4D4 File Offset: 0x001486D4
		public override void Step(CharacterMovement context, float deltaTime)
		{
			float num = context.Velocity.magnitude * deltaTime;
			if (num <= 0.0001f)
			{
				context.SetRaycastCheckPosition(context.RaycastCheckPosition);
				return;
			}
			MovementUtil.SweepMoveForRunAndAir(context, deltaTime, ref this.m_sweepHitInfo);
			CapsuleCollider component = context.GetComponent<CapsuleCollider>();
			if (this.m_sweepHitInfo.IsValid() && !context.IsOnGround())
			{
				RaycastHit info = this.m_sweepHitInfo.info;
				Vector3 vector = info.point - component.bounds.center;
				vector -= Vector3.Project(vector, Vector3.up);
				if (Vector3.Dot(info.normal, CharacterDefs.BaseFrontTangent) < -0.707f && vector.sqrMagnitude < component.radius * component.radius)
				{
					int layer = -1 - (1 << LayerMask.NameToLayer("Player"));
					MovementUtil.SweepMoveInnerParam innerParam = new MovementUtil.SweepMoveInnerParam(component, info.normal * 0.1f, layer);
					MovementUtil.SweepMoveOuterParam outerParam = new MovementUtil.SweepMoveOuterParam();
					MovementUtil.SweepMove(context.transform, innerParam, outerParam);
				}
			}
			if (this.m_rotateToGround && !this.m_sweepHitInfo.IsValid())
			{
				MovementUtil.UpdateRotateOnGravityUp(context, 720f, deltaTime);
			}
			Vector3 raycastCheckPosition = context.RaycastCheckPosition;
			MovementUtil.CheckAndPushOutByRaycast(context.transform, context.RaycastCheckPosition, ref raycastCheckPosition);
			context.SetRaycastCheckPosition(raycastCheckPosition);
			if (this.m_prevPos == raycastCheckPosition)
			{
				this.m_errorTime += deltaTime;
				if (this.m_errorTime > 0.5f)
				{
					RaycastHit info2 = this.m_sweepHitInfo.info;
					int layer2 = -1 - (1 << LayerMask.NameToLayer("Player"));
					MovementUtil.SweepMoveInnerParam innerParam2 = new MovementUtil.SweepMoveInnerParam(component, info2.normal * 0.1f, layer2);
					MovementUtil.SweepMoveOuterParam outerParam2 = new MovementUtil.SweepMoveOuterParam();
					MovementUtil.SweepMove(context.transform, innerParam2, outerParam2);
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

		// Token: 0x06003F90 RID: 16272 RVA: 0x0014A724 File Offset: 0x00148924
		public void SetRotateToGround(bool value)
		{
			this.m_rotateToGround = value;
		}

		// Token: 0x04003693 RID: 13971
		private const float cos135 = -0.707f;

		// Token: 0x04003694 RID: 13972
		private const float RotateDegree = 720f;

		// Token: 0x04003695 RID: 13973
		private HitInfo m_sweepHitInfo;

		// Token: 0x04003696 RID: 13974
		private bool m_rotateToGround;

		// Token: 0x04003697 RID: 13975
		private float m_errorTime;

		// Token: 0x04003698 RID: 13976
		private Vector3 m_prevPos = Vector3.zero;
	}
}
