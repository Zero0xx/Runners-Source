using System;
using UnityEngine;

// Token: 0x020001AE RID: 430
public static class Debug
{
	// Token: 0x06000C57 RID: 3159 RVA: 0x00046D08 File Offset: 0x00044F08
	public static void Break()
	{
		if (global::Debug.IsEnable())
		{
			UnityEngine.Debug.Break();
		}
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x00046D1C File Offset: 0x00044F1C
	public static void Log(object message)
	{
		if (global::Debug.IsEnable())
		{
			UnityEngine.Debug.Log(message);
			DebugTraceManager instance = DebugTraceManager.Instance;
			if (instance != null)
			{
				string text = message as string;
				if (text != null)
				{
					DebugTrace trace = new DebugTrace(text);
					instance.AddTrace(DebugTraceManager.TraceType.ALL, trace);
				}
			}
		}
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x00046D68 File Offset: 0x00044F68
	public static void Log(object message, UnityEngine.Object context)
	{
		if (global::Debug.IsEnable())
		{
			UnityEngine.Debug.Log(message, context);
		}
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x00046D7C File Offset: 0x00044F7C
	public static void LogError(object message)
	{
		if (global::Debug.IsEnable())
		{
			UnityEngine.Debug.LogError(message);
		}
		DebugTraceManager instance = DebugTraceManager.Instance;
		if (instance != null)
		{
			string text = message as string;
			if (text != null)
			{
				DebugTrace trace = new DebugTrace(text);
				instance.AddTrace(DebugTraceManager.TraceType.ALL, trace);
			}
		}
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x00046DC8 File Offset: 0x00044FC8
	public static void LogError(object message, UnityEngine.Object context)
	{
		if (global::Debug.IsEnable())
		{
			UnityEngine.Debug.LogError(message, context);
		}
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x00046DDC File Offset: 0x00044FDC
	public static void LogWarning(object message)
	{
		if (global::Debug.IsEnable())
		{
			UnityEngine.Debug.LogWarning(message);
			DebugTraceManager instance = DebugTraceManager.Instance;
			if (instance != null)
			{
				string text = message as string;
				if (text != null)
				{
					DebugTrace trace = new DebugTrace(text);
					instance.AddTrace(DebugTraceManager.TraceType.ALL, trace);
				}
			}
		}
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x00046E28 File Offset: 0x00045028
	public static void LogWarning(object message, UnityEngine.Object context)
	{
		if (global::Debug.IsEnable())
		{
			UnityEngine.Debug.LogWarning(message, context);
		}
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x00046E3C File Offset: 0x0004503C
	public static void LogException(Exception exception)
	{
		if (global::Debug.IsEnable())
		{
			UnityEngine.Debug.LogException(exception);
		}
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x00046E50 File Offset: 0x00045050
	public static void LogException(Exception exception, UnityEngine.Object context)
	{
		if (global::Debug.IsEnable())
		{
			UnityEngine.Debug.LogException(exception, context);
		}
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x00046E64 File Offset: 0x00045064
	public static void DrawLine(Vector3 start, Vector3 end)
	{
		if (global::Debug.IsEnable())
		{
			UnityEngine.Debug.DrawLine(start, end);
		}
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x00046E78 File Offset: 0x00045078
	public static void DrawLine(Vector3 start, Vector3 end, Color color)
	{
		if (global::Debug.IsEnable())
		{
			UnityEngine.Debug.DrawLine(start, end, color);
		}
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x00046E8C File Offset: 0x0004508C
	public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
	{
		if (global::Debug.IsEnable())
		{
			UnityEngine.Debug.DrawLine(start, end, color, duration);
		}
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x00046EA4 File Offset: 0x000450A4
	public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration, bool depthTest)
	{
		if (global::Debug.IsEnable())
		{
			UnityEngine.Debug.DrawLine(start, end, color, duration, depthTest);
		}
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x00046EBC File Offset: 0x000450BC
	public static void Log(object message, DebugTraceManager.TraceType type)
	{
		if (global::Debug.IsEnable())
		{
			UnityEngine.Debug.Log(message);
			DebugTraceManager instance = DebugTraceManager.Instance;
			if (instance != null)
			{
				string text = message as string;
				if (text != null)
				{
					DebugTrace trace = new DebugTrace(text);
					instance.AddTrace(type, trace);
				}
			}
		}
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x00046F08 File Offset: 0x00045108
	public static void LogError(object message, DebugTraceManager.TraceType type)
	{
		if (global::Debug.IsEnable())
		{
			UnityEngine.Debug.Log(message);
			DebugTraceManager instance = DebugTraceManager.Instance;
			if (instance != null)
			{
				string text = message as string;
				if (text != null)
				{
					DebugTrace trace = new DebugTrace(text);
					instance.AddTrace(type, trace);
				}
			}
		}
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x00046F54 File Offset: 0x00045154
	public static void LogWarning(object message, DebugTraceManager.TraceType type)
	{
		if (global::Debug.IsEnable())
		{
			UnityEngine.Debug.Log(message);
			DebugTraceManager instance = DebugTraceManager.Instance;
			if (instance != null)
			{
				string text = message as string;
				if (text != null)
				{
					DebugTrace trace = new DebugTrace(text);
					instance.AddTrace(type, trace);
				}
			}
		}
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x00046FA0 File Offset: 0x000451A0
	private static bool IsEnable()
	{
		return false;
	}
}
