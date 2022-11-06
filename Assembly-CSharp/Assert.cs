using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020001A8 RID: 424
public class Assert
{
	// Token: 0x06000C28 RID: 3112 RVA: 0x00046120 File Offset: 0x00044320
	[Conditional("UNITY_EDITOR")]
	public static void True(bool test, string message)
	{
		if (!test)
		{
			if (Application.isEditor && !Application.isPlaying)
			{
				throw new UnityException(message);
			}
			global::Debug.LogError(message);
			global::Debug.Break();
		}
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x0004615C File Offset: 0x0004435C
	[Conditional("UNITY_EDITOR")]
	public static void NotInvalidFloat(float f, string msg)
	{
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x00046160 File Offset: 0x00044360
	[Conditional("UNITY_EDITOR")]
	public static void NotInvalid(Vector3 v, string msg)
	{
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x00046164 File Offset: 0x00044364
	[Conditional("UNITY_EDITOR")]
	public static void NotInvalid(Quaternion q, string msg)
	{
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x00046168 File Offset: 0x00044368
	[Conditional("UNITY_EDITOR")]
	public static void NotInvalid(Transform t, string msg)
	{
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x0004616C File Offset: 0x0004436C
	[Conditional("UNITY_EDITOR")]
	public static void Fail(string message)
	{
	}

	// Token: 0x0400099A RID: 2458
	private const string ASSERT_GUARD = "UNITY_EDITOR";
}
