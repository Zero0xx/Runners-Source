using System;
using App;
using UnityEngine;

namespace Player
{
	// Token: 0x0200097B RID: 2427
	public class CharacterMoveOnPathPhantomDrill : CharacterMoveBase
	{
		// Token: 0x06003FAB RID: 16299 RVA: 0x0014B060 File Offset: 0x00149260
		public override void Enter(CharacterMovement context)
		{
			this.m_path = null;
			this.m_basePosition = context.transform.position;
			this.m_baseVelocity = context.Velocity;
			this.m_nowJump = false;
			this.m_speed = 0f;
			this.m_offset = Vector3.zero;
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x0014B0B0 File Offset: 0x001492B0
		public override void Leave(CharacterMovement context)
		{
			this.m_path = null;
			this.EndJump();
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x0014B0C0 File Offset: 0x001492C0
		public override void Step(CharacterMovement context, float deltaTime)
		{
			this.StepRunOnPath(context, deltaTime);
			this.StepJump(context, deltaTime);
			context.Velocity = this.m_baseVelocity + this.m_jumpVelocity;
			context.transform.position += context.Velocity * deltaTime;
			this.Rotate(context, deltaTime);
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x0014B120 File Offset: 0x00149320
		public void StepRunOnPath(CharacterMovement context, float deltaTime)
		{
			if (this.m_path == null)
			{
				return;
			}
			if (!this.m_path.IsValid())
			{
				return;
			}
			Vector3 basePosition = this.m_basePosition;
			float num = this.m_speed * deltaTime;
			float num2 = num * 3f;
			Vector3? vector = new Vector3?(Vector3.zero);
			if (!this.CheckAndChangeToNextPath(context, num))
			{
				this.m_basePosition += this.m_baseVelocity * deltaTime;
				return;
			}
			Vector3? vector2 = null;
			float distance = this.m_path.Distance;
			this.m_path.GetPNT(distance + num2, ref vector, ref vector2, ref vector2);
			Vector3 baseVelocity = this.m_baseVelocity;
			Vector3 vector3 = Vector3.SmoothDamp(basePosition - this.m_offset, vector.Value, ref baseVelocity, deltaTime * 3f, float.PositiveInfinity, deltaTime);
			float distance2;
			this.m_path.GetClosestPositionAlongSpline(vector3, distance - num, distance + num, out distance2);
			this.m_path.Distance = distance2;
			this.m_basePosition = vector3 + this.m_offset;
			this.m_baseVelocity = (this.m_basePosition - basePosition) / deltaTime;
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x0014B248 File Offset: 0x00149448
		public void StepJump(CharacterMovement context, float deltaTime)
		{
			if (!this.m_nowJump)
			{
				return;
			}
			Vector3 rhs = -context.GetGravityDir();
			if (Vector3.Dot(this.m_jumpVelocity, rhs) < 0f && Vector3.Dot(context.transform.position - this.m_basePosition, rhs) < 0f)
			{
				this.EndJump();
				return;
			}
			this.m_jumpVelocity += context.GetGravityDir() * context.Parameter.m_drillJumpGravity * deltaTime;
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x0014B2E0 File Offset: 0x001494E0
		private bool CheckAndChangeToNextPath(CharacterMovement context, float runLength)
		{
			if (this.m_path.Distance > this.m_path.GetLength() - runLength)
			{
				PathEvaluator stagePathEvaluator = CharacterMoveOnPathPhantomDrill.GetStagePathEvaluator(context, this.m_pathType);
				if (stagePathEvaluator == null)
				{
					this.m_path = null;
					return false;
				}
				if (stagePathEvaluator.GetPathObject().GetID() == this.m_path.GetPathObject().GetID())
				{
					return false;
				}
				this.SetupPath(context, stagePathEvaluator);
			}
			return true;
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x0014B358 File Offset: 0x00149558
		public void Jump(CharacterMovement context)
		{
			if (this.m_nowJump || this.m_noJump)
			{
				return;
			}
			this.m_nowJump = true;
			this.m_jumpVelocity = -context.GetGravityDir() * context.Parameter.m_drillJumpForce;
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x0014B3A4 File Offset: 0x001495A4
		private void EndJump()
		{
			this.m_nowJump = false;
			this.m_jumpVelocity = Vector3.zero;
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x0014B3B8 File Offset: 0x001495B8
		private void Rotate(CharacterMovement context, float deltaTime)
		{
			Vector3 vector = context.GetForwardDir();
			Vector3 current = vector;
			Vector3 vector2 = context.GetUpDir();
			if (App.Math.Vector3NormalizeIfNotZero(context.Velocity, out vector))
			{
				vector = Vector3.RotateTowards(current, vector, 180f * deltaTime, 0f);
				vector2 = Vector3.Cross(vector, CharacterDefs.BaseRightTangent);
				if (Mathf.Abs(Vector3.Dot(vector, CharacterDefs.BaseRightTangent)) > 0.001f)
				{
					Vector3 vector3 = Vector3.Cross(vector2, CharacterDefs.BaseRightTangent);
					if (Vector3.Dot(vector3, vector) < 0f)
					{
						vector = -vector3;
					}
					else
					{
						vector = vector3;
					}
					vector2 = Vector3.Cross(vector, CharacterDefs.BaseRightTangent);
				}
				context.SetLookRotation(vector, vector2);
			}
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x0014B460 File Offset: 0x00149660
		public void SetupPath(CharacterMovement context, BlockPathController.PathType pathtype, bool noJump, float offset)
		{
			this.m_pathType = pathtype;
			this.m_noJump = noJump;
			this.m_offset = offset * Vector3.up;
			PathEvaluator stagePathEvaluator = CharacterMoveOnPathPhantomDrill.GetStagePathEvaluator(context, this.m_pathType);
			if (stagePathEvaluator != null)
			{
				this.SetupPath(context, stagePathEvaluator);
			}
			else
			{
				this.m_path = null;
			}
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x0014B4B4 File Offset: 0x001496B4
		private void SetupPath(CharacterMovement context, PathEvaluator pathEvaluator)
		{
			this.m_path = new PathEvaluator(pathEvaluator);
			float closestPositionAlongSpline = pathEvaluator.GetClosestPositionAlongSpline(context.transform.position, 0f, this.m_path.GetLength());
			this.m_path.Distance = closestPositionAlongSpline;
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x0014B4FC File Offset: 0x001496FC
		public void SetSpeed(CharacterMovement context, float speed)
		{
			this.m_speed = speed;
			this.m_baseVelocity = context.GetForwardDir() * speed;
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x0014B518 File Offset: 0x00149718
		public bool IsPathEnd(float remainDist)
		{
			return this.m_path == null || !this.m_path.IsValid() || this.m_path.GetLength() - this.m_path.Distance < remainDist;
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x0014B564 File Offset: 0x00149764
		public bool IsPathValid()
		{
			return this.m_path.IsValid();
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x0014B57C File Offset: 0x0014977C
		public static PathEvaluator GetStagePathEvaluator(CharacterMovement context, BlockPathController.PathType patytype)
		{
			StageBlockPathManager blockPathManager = context.GetBlockPathManager();
			if (blockPathManager != null)
			{
				PathEvaluator curentPathEvaluator = blockPathManager.GetCurentPathEvaluator(patytype);
				if (curentPathEvaluator != null)
				{
					return curentPathEvaluator;
				}
			}
			return null;
		}

		// Token: 0x0400369E RID: 13982
		private const float TargetSearchRate = 3f;

		// Token: 0x0400369F RID: 13983
		private const float MaxRotate = 180f;

		// Token: 0x040036A0 RID: 13984
		private PathEvaluator m_path;

		// Token: 0x040036A1 RID: 13985
		private BlockPathController.PathType m_pathType;

		// Token: 0x040036A2 RID: 13986
		private bool m_noJump;

		// Token: 0x040036A3 RID: 13987
		private Vector3 m_offset;

		// Token: 0x040036A4 RID: 13988
		private float m_speed;

		// Token: 0x040036A5 RID: 13989
		private bool m_nowJump;

		// Token: 0x040036A6 RID: 13990
		private Vector3 m_basePosition;

		// Token: 0x040036A7 RID: 13991
		private Vector3 m_baseVelocity;

		// Token: 0x040036A8 RID: 13992
		private Vector3 m_jumpVelocity;
	}
}
