using System;
using UnityEngine;

namespace App
{
	// Token: 0x020003EE RID: 1006
	public struct Segment3
	{
		// Token: 0x06001DD5 RID: 7637 RVA: 0x000AF0CC File Offset: 0x000AD2CC
		public Segment3(Vector3 pt0, Vector3 pt1)
		{
			this.p0 = pt0;
			this.p1 = pt1;
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x000AF0DC File Offset: 0x000AD2DC
		public void Set(Vector3 pt0, Vector3 pt1)
		{
			this.p0 = pt0;
			this.p1 = pt1;
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x000AF0EC File Offset: 0x000AD2EC
		public Vector3 GetP0()
		{
			return this.p0;
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x000AF0F4 File Offset: 0x000AD2F4
		public Vector3 GetP1()
		{
			return this.p1;
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x000AF0FC File Offset: 0x000AD2FC
		public static float DistanceSq(ref Vector3 pt0, ref Vector3 pt1, ref Vector3 pt, ref float? t)
		{
			Line3 line = Line3.FromPoints(pt0, pt1);
			Vector3 a = pt0;
			Vector3 b = pt1;
			float magnitude = (a - b).magnitude;
			float? num = new float?(0f);
			float result = line.DistanceSq(pt, ref num);
			if (num != null && num.Value < 0f)
			{
				if (t != null)
				{
					t = new float?(0f);
				}
				result = Vector3.SqrMagnitude(pt - pt0);
			}
			else if (num != null && num.Value > magnitude)
			{
				if (t != null)
				{
					t = new float?(1f);
				}
				result = Vector3.SqrMagnitude(pt - pt1);
			}
			else if (t != null)
			{
				t = new float?(((num == null) ? null : new float?(num.Value / magnitude)).Value);
			}
			return result;
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x000AF250 File Offset: 0x000AD450
		public float DistanceSq(Vector3 pt, ref float? t)
		{
			Line3 line = Line3.FromPoints(this.p0, this.p1);
			Vector3 a = this.p0;
			Vector3 b = this.p1;
			float magnitude = (a - b).magnitude;
			float? num = new float?(0f);
			float result = line.DistanceSq(pt, ref num);
			if (num != null && num.Value < 0f)
			{
				if (t != null)
				{
					t = new float?(0f);
				}
				result = Vector3.SqrMagnitude(pt - this.p0);
			}
			else if (num != null && num.Value > magnitude)
			{
				if (t != null)
				{
					t = new float?(1f);
				}
				result = Vector3.SqrMagnitude(pt - this.p1);
			}
			else if (t != null)
			{
				t = new float?(((num == null) ? null : new float?(num.Value / magnitude)).Value);
			}
			return result;
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x000AF394 File Offset: 0x000AD594
		public float DistanceSq(Segment3 seg, ref float? s, ref float? t)
		{
			Vector3 b = this.p0;
			Vector3 a = this.p1;
			Vector3 vector = seg.p1 - seg.p0;
			Vector3 vector2 = a - b;
			Vector3 vector3 = seg.p0 - b;
			float sqrMagnitude = vector.sqrMagnitude;
			float num = Vector3.Dot(vector, vector2);
			float sqrMagnitude2 = vector2.sqrMagnitude;
			float num2 = Vector3.Dot(vector, vector3);
			float num3 = Vector3.Dot(vector2, vector3);
			float num4 = sqrMagnitude * sqrMagnitude2 - num * num;
			float num5 = num4;
			float num6 = num4;
			float num7;
			float num8;
			if (Math.NearZero(num4, 0.0001f))
			{
				num7 = 0f;
				num5 = 1f;
				num8 = num3;
				num6 = sqrMagnitude2;
			}
			else
			{
				num7 = num * num3 - sqrMagnitude2 * num2;
				num8 = sqrMagnitude * num3 - num * num2;
				if (num7 < 0f)
				{
					num7 = 0f;
					num8 = num3;
					num6 = sqrMagnitude2;
				}
				else if (num7 > num5)
				{
					num7 = num5;
					num8 = num3 + num;
					num6 = sqrMagnitude2;
				}
			}
			if (num8 < 0f)
			{
				num8 = 0f;
				if (-num2 < 0f)
				{
					num7 = 0f;
				}
				else if (-num2 > sqrMagnitude)
				{
					num7 = num5;
				}
				else
				{
					num7 = -num2;
					num5 = sqrMagnitude;
				}
			}
			else if (num8 > num6)
			{
				num8 = num6;
				if (-num2 + num < 0f)
				{
					num7 = 0f;
				}
				else if (-num2 + num > sqrMagnitude)
				{
					num7 = num5;
				}
				else
				{
					num7 = -num2 + num;
					num5 = sqrMagnitude;
				}
			}
			float num9 = (!Math.NearZero(num7, 0.0001f)) ? (num7 / num5) : 0f;
			float num10 = (!Math.NearZero(num8, 0.0001f)) ? (num8 / num6) : 0f;
			if (s != null)
			{
				s = new float?(num9);
			}
			if (t != null)
			{
				t = new float?(num10);
			}
			return Vector3.SqrMagnitude(vector3 + vector * num9 - vector2 * num10);
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x000AF5CC File Offset: 0x000AD7CC
		public override bool Equals(object o)
		{
			return true;
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x000AF5D0 File Offset: 0x000AD7D0
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x000AF5D4 File Offset: 0x000AD7D4
		public static bool operator ==(Segment3 lhs, Segment3 rhs)
		{
			return lhs.p0 == rhs.p0 && lhs.p1 == rhs.p1;
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x000AF610 File Offset: 0x000AD810
		public static bool operator !=(Segment3 lhs, Segment3 rhs)
		{
			return lhs.p0 != rhs.p0 || lhs.p1 == rhs.p1;
		}

		// Token: 0x04001B23 RID: 6947
		private Vector3 p0;

		// Token: 0x04001B24 RID: 6948
		private Vector3 p1;
	}
}
