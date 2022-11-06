using System;
using App;
using UnityEngine;

namespace Player
{
	// Token: 0x0200097A RID: 2426
	public class CharacterMoveOnPathPhantom : CharacterMoveBase
	{
		// Token: 0x06003FA1 RID: 16289 RVA: 0x0014AB58 File Offset: 0x00148D58
		public override void Enter(CharacterMovement context)
		{
			this.m_path = null;
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x0014AB64 File Offset: 0x00148D64
		public override void Leave(CharacterMovement context)
		{
			this.m_path = null;
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x0014AB70 File Offset: 0x00148D70
		public override void Step(CharacterMovement context, float deltaTime)
		{
			float num = context.Velocity.magnitude * deltaTime;
			if (this.m_path == null)
			{
				return;
			}
			if (!this.m_path.IsValid())
			{
				return;
			}
			if (this.m_path.Distance > this.m_path.GetLength() - num)
			{
				PathEvaluator stagePathEvaluator = CharacterMoveOnPathPhantom.GetStagePathEvaluator(context, this.m_pathType);
				if (stagePathEvaluator == null)
				{
					this.m_path = null;
					return;
				}
				if (stagePathEvaluator.GetPathObject().GetID() == this.m_path.GetPathObject().GetID())
				{
					context.transform.position += context.transform.forward * num;
					return;
				}
				this.SetupPath(context, stagePathEvaluator, false);
			}
			Vector3? vector = null;
			this.m_path.Advance(num);
			float distance = this.m_path.Distance;
			Vector3? vector2 = new Vector3?(Vector3.zero);
			this.m_path.GetPNT(ref vector2, ref vector, ref vector);
			Vector3 position = context.transform.position;
			Vector3 a;
			if (App.Math.Vector3NormalizeIfNotZero(vector2.Value - position, out a))
			{
				vector2 = new Vector3?(position + a * num);
			}
			float num2;
			this.m_path.GetClosestPositionAlongSpline(vector2.Value, distance - num, distance + num, out num2);
			Vector3? vector3 = new Vector3?(Vector3.zero);
			Vector3? vector4 = new Vector3?(Vector3.zero);
			Vector3? vector5 = new Vector3?(Vector3.zero);
			this.m_path.GetPNT(num2, ref vector3, ref vector4, ref vector5);
			this.m_path.Distance = num2;
			context.transform.position = vector2.Value;
			Quaternion identity = Quaternion.identity;
			if (Mathf.Abs(Vector3.Dot(vector5.Value, CharacterDefs.BaseRightTangent)) > 0.001f)
			{
				Vector3 vector6 = Vector3.Cross(vector4.Value, CharacterDefs.BaseRightTangent);
				if (Vector3.Dot(vector6, vector5.Value) < 0f)
				{
					vector5 = new Vector3?(-vector6);
				}
				else
				{
					vector5 = new Vector3?(vector6);
				}
			}
			identity.SetLookRotation(vector5.Value, vector4.Value);
			context.transform.rotation = identity;
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x0014ADB8 File Offset: 0x00148FB8
		public void SetupPath(CharacterMovement context, BlockPathController.PathType pathtype)
		{
			this.m_pathType = pathtype;
			PathEvaluator stagePathEvaluator = CharacterMoveOnPathPhantom.GetStagePathEvaluator(context, this.m_pathType);
			if (stagePathEvaluator != null)
			{
				this.SetupPath(context, stagePathEvaluator, true);
			}
			else
			{
				this.m_path = null;
			}
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x0014ADF4 File Offset: 0x00148FF4
		private void SetupPath(CharacterMovement context, PathEvaluator pathEvaluator, bool setup)
		{
			this.m_path = new PathEvaluator(pathEvaluator);
			float closestPositionAlongSpline = pathEvaluator.GetClosestPositionAlongSpline(context.transform.position, 0f, this.m_path.GetLength());
			this.m_path.Distance = closestPositionAlongSpline;
			if (setup)
			{
				Vector3 position = context.transform.position;
				Vector3? vector = null;
				Vector3? vector2 = new Vector3?(Vector3.zero);
				this.m_path.GetPNT(ref vector2, ref vector, ref vector);
				Vector3 value = vector2.Value;
				if (position.x - value.x > 0.5f)
				{
					position = new Vector3(position.x, context.SideViewPathPos.y, position.z);
					closestPositionAlongSpline = pathEvaluator.GetClosestPositionAlongSpline(position, 0f, this.m_path.GetLength());
					if (this.m_path.Distance < closestPositionAlongSpline)
					{
						this.m_path.Distance = closestPositionAlongSpline;
					}
					this.SetFrontAddPath(context, pathEvaluator, position, 5f, 3);
				}
			}
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x0014AF00 File Offset: 0x00149100
		private void SetFrontAddPath(CharacterMovement context, PathEvaluator pathEvaluator, Vector3 playerPos, float pathY, int count)
		{
			for (int i = 0; i < count; i++)
			{
				Vector3? vector = null;
				Vector3? vector2 = new Vector3?(Vector3.zero);
				this.m_path.GetPNT(ref vector2, ref vector, ref vector);
				Vector3 value = vector2.Value;
				if (playerPos.x - value.x <= 0.5f)
				{
					return;
				}
				playerPos += new Vector3(0f, pathY, 0f);
				float closestPositionAlongSpline = pathEvaluator.GetClosestPositionAlongSpline(playerPos, 0f, this.m_path.GetLength());
				if (this.m_path.Distance < closestPositionAlongSpline)
				{
					this.m_path.Distance = closestPositionAlongSpline;
				}
			}
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x0014AFC0 File Offset: 0x001491C0
		public bool IsPathEnd(float remainDist)
		{
			return this.m_path == null || !this.m_path.IsValid() || this.m_path.GetLength() - this.m_path.Distance < remainDist;
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x0014B00C File Offset: 0x0014920C
		public bool IsPathValid()
		{
			return this.m_path.IsValid();
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x0014B024 File Offset: 0x00149224
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

		// Token: 0x0400369C RID: 13980
		private PathEvaluator m_path;

		// Token: 0x0400369D RID: 13981
		private BlockPathController.PathType m_pathType;
	}
}
