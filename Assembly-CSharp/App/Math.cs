using System;
using UnityEngine;

namespace App
{
	// Token: 0x020003EC RID: 1004
	public class Math
	{
		// Token: 0x06001DBF RID: 7615 RVA: 0x000AECD8 File Offset: 0x000ACED8
		public static float Reciprocal(float x)
		{
			return 1f / x;
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x000AECE4 File Offset: 0x000ACEE4
		public static bool NearZero(float a, float epsilon = 1E-06f)
		{
			return Mathf.Abs(a) <= epsilon;
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x000AECF4 File Offset: 0x000ACEF4
		public static bool NearEqual(float a, float b, float epsilon = 1E-06f)
		{
			return Math.NearZero(a - b, epsilon);
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x000AED00 File Offset: 0x000ACF00
		public static float Sqr(float a)
		{
			return a * a;
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x000AED08 File Offset: 0x000ACF08
		public static Matrix4x4 Matrix44OrthonormalDirection2(Vector3 zAxis, Vector3 yAxis)
		{
			Vector3 vector = Vector3.Cross(yAxis, zAxis);
			if (!Math.Vector3NormalizeIfNotZero(vector, out vector))
			{
				vector = Vector3.right;
			}
			Vector3 c = Vector3.Cross(zAxis, vector);
			return Math.Matrix44SetColumn3(vector, c, zAxis);
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x000AED40 File Offset: 0x000ACF40
		public static Matrix4x4 Matrix44SetColumn3(Vector3 c0, Vector3 c1, Vector3 c2)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.SetColumn(0, c0);
			result.SetColumn(1, c1);
			result.SetColumn(2, c2);
			result.SetColumn(3, Vector3.zero);
			return result;
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000AED94 File Offset: 0x000ACF94
		public static Matrix4x4 Matrix34InverseNonSingular(Matrix4x4 m)
		{
			Matrix4x4 matrix4x = default(Matrix4x4);
			return m.inverse;
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x000AEDB4 File Offset: 0x000ACFB4
		public static bool Vector3NormalizeIfNotZero(Vector3 src, out Vector3 dst)
		{
			if (src.sqrMagnitude < 0.0001f)
			{
				dst = Vector3.zero;
				return false;
			}
			dst = src.normalized;
			return true;
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x000AEDF0 File Offset: 0x000ACFF0
		public static void Vector3NormalizeZero(Vector3 src, out Vector3 dst)
		{
			if (src.sqrMagnitude < 0.0001f)
			{
				dst = Vector3.zero;
				return;
			}
			dst = src.normalized;
		}

		// Token: 0x04001B11 RID: 6929
		public const float EPSILON = 1E-06f;

		// Token: 0x04001B12 RID: 6930
		public const float PI = 3.1415927f;

		// Token: 0x04001B13 RID: 6931
		public const float F_E = 2.7182817f;

		// Token: 0x04001B14 RID: 6932
		public const float F_LOG2E = 1.442695f;

		// Token: 0x04001B15 RID: 6933
		public const float F_LOG10E = 0.4342945f;

		// Token: 0x04001B16 RID: 6934
		public const float F_LN2 = 0.6931472f;

		// Token: 0x04001B17 RID: 6935
		public const float F_LN10 = 2.3025851f;

		// Token: 0x04001B18 RID: 6936
		public const float F_PI = 3.1415927f;

		// Token: 0x04001B19 RID: 6937
		public const float F_SQRTPI = 1.7724539f;

		// Token: 0x04001B1A RID: 6938
		public const float F_SQRT2 = 1.4142135f;

		// Token: 0x04001B1B RID: 6939
		public const float F_SQRT3 = 1.7320508f;

		// Token: 0x04001B1C RID: 6940
		public const float F_INVLN2 = 1.442695f;

		// Token: 0x04001B1D RID: 6941
		public const float F_MAX = 3.4028235E+38f;

		// Token: 0x04001B1E RID: 6942
		public const float F_MIN = 1.1754944E-38f;

		// Token: 0x04001B1F RID: 6943
		public const float F_EPSILON = 1E-06f;

		// Token: 0x04001B20 RID: 6944
		private const float VEC3_NORMALIZE_EPS = 0.0001f;
	}
}
