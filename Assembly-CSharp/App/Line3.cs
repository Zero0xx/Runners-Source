using System;
using UnityEngine;

namespace App
{
	// Token: 0x020003ED RID: 1005
	internal struct Line3
	{
		// Token: 0x06001DC8 RID: 7624 RVA: 0x000AEE28 File Offset: 0x000AD028
		public Line3(Vector3 p_, Vector3 d_)
		{
			this.p = p_;
			this.d = d_;
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x000AEE38 File Offset: 0x000AD038
		public void Set(Vector3 p_, Vector3 d_)
		{
			this.p = p_;
			this.d = d_;
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x000AEE48 File Offset: 0x000AD048
		public Vector3 GetP()
		{
			return this.p;
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x000AEE50 File Offset: 0x000AD050
		public Vector3 GetD()
		{
			return this.d;
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x000AEE58 File Offset: 0x000AD058
		public void Normalize()
		{
			this.d.Normalize();
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x000AEE68 File Offset: 0x000AD068
		public float DistanceSq(Vector3 pt, ref float? t)
		{
			Vector3 vector = this.p;
			float num = Vector3.Dot(this.d, pt - vector);
			Vector3 b = vector + this.d * num;
			if (t != null)
			{
				t = new float?(num);
			}
			return (pt - b).sqrMagnitude;
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x000AEECC File Offset: 0x000AD0CC
		public float DistanceSq(Line3 l, ref float? s, ref float? t)
		{
			Vector3 b = this.p;
			Vector3 vector = this.d;
			Vector3 vector2 = l.p - b;
			float num = Vector3.Dot(l.d, vector);
			float num2 = Vector3.Dot(l.d, vector2);
			float num3 = Vector3.Dot(vector, vector2);
			float num4 = 1f - num * num;
			if (Math.NearZero(num4, 0.0001f))
			{
				if (s != null)
				{
					s = new float?(0f);
				}
				if (t != null)
				{
					t = new float?(num3);
				}
				return vector2.sqrMagnitude;
			}
			float num5 = 1f / num4;
			float num6 = (num * num3 - num2) * num5;
			float num7 = (num3 - num * num2) * num5;
			if (s != null)
			{
				s = new float?(num6);
			}
			if (t != null)
			{
				t = new float?(num7);
			}
			return (l.d * num6 - vector * num7 + vector2).sqrMagnitude;
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x000AEFF4 File Offset: 0x000AD1F4
		public static Line3 FromPoints(Vector3 p1, Vector3 p2)
		{
			return new Line3(p1, (p2 - p1).normalized);
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x000AF018 File Offset: 0x000AD218
		public static Line3 FromSegment(ref Segment3 s)
		{
			return new Line3(s.GetP0(), (s.GetP1() - s.GetP0()).normalized);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x000AF04C File Offset: 0x000AD24C
		public override bool Equals(object o)
		{
			return true;
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x000AF050 File Offset: 0x000AD250
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x000AF054 File Offset: 0x000AD254
		public static bool operator ==(Line3 lhs, Line3 rhs)
		{
			return lhs.p == rhs.p && lhs.d == rhs.d;
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x000AF090 File Offset: 0x000AD290
		public static bool operator !=(Line3 lhs, Line3 rhs)
		{
			return lhs.p != rhs.p || lhs.d != rhs.d;
		}

		// Token: 0x04001B21 RID: 6945
		private Vector3 p;

		// Token: 0x04001B22 RID: 6946
		private Vector3 d;
	}
}
