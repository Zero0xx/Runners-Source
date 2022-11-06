using System;
using App;
using UnityEngine;

namespace Player
{
	// Token: 0x02000979 RID: 2425
	public class CharacterMoveOnPath : CharacterMoveBase
	{
		// Token: 0x06003F9B RID: 16283 RVA: 0x0014A89C File Offset: 0x00148A9C
		public override void Enter(CharacterMovement context)
		{
			this.m_path = null;
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x0014A8A8 File Offset: 0x00148AA8
		public override void Leave(CharacterMovement context)
		{
			this.m_path = null;
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x0014A8B4 File Offset: 0x00148AB4
		public override void Step(CharacterMovement context, float deltaTime)
		{
			if (this.m_path == null)
			{
				return;
			}
			if (!this.m_path.IsValid())
			{
				return;
			}
			Vector3? vector = null;
			float num = context.Velocity.magnitude * deltaTime;
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
			global::Debug.DrawLine(vector3.Value, vector3.Value + vector4.Value * 1f, Color.red, 1f);
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

		// Token: 0x06003F9E RID: 16286 RVA: 0x0014AAA0 File Offset: 0x00148CA0
		public void SetupPath(Vector3 position, PathComponent component, float? distance)
		{
			this.m_path = new PathEvaluator(component);
			if (distance == null)
			{
				float distance2 = 0f;
				this.m_path.GetClosestPositionAlongSpline(position, 0f, this.m_path.GetLength(), out distance2);
				this.m_path.Distance = distance2;
			}
			else
			{
				this.m_path.Distance = distance.Value;
			}
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x0014AB10 File Offset: 0x00148D10
		public bool IsPathEnd(float remainDist)
		{
			return !this.m_path.IsValid() || this.m_path.GetLength() - this.m_path.Distance < remainDist;
		}

		// Token: 0x0400369B RID: 13979
		private PathEvaluator m_path;
	}
}
