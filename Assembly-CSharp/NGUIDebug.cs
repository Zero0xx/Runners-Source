using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AC RID: 172
[AddComponentMenu("NGUI/Internal/Debug")]
public class NGUIDebug : MonoBehaviour
{
	// Token: 0x0600048A RID: 1162 RVA: 0x00016D78 File Offset: 0x00014F78
	public static void Log(string text)
	{
		if (Application.isPlaying)
		{
			if (NGUIDebug.mLines.Count > 20)
			{
				NGUIDebug.mLines.RemoveAt(0);
			}
			NGUIDebug.mLines.Add(text);
			if (NGUIDebug.mInstance == null)
			{
				GameObject gameObject = new GameObject("_NGUI Debug");
				NGUIDebug.mInstance = gameObject.AddComponent<NGUIDebug>();
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
			}
		}
		else
		{
			global::Debug.Log(text);
		}
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x00016DF0 File Offset: 0x00014FF0
	public static void DrawBounds(Bounds b)
	{
		Vector3 center = b.center;
		Vector3 vector = b.center - b.extents;
		Vector3 vector2 = b.center + b.extents;
		global::Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector2.x, vector.y, center.z), Color.red);
		global::Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector.x, vector2.y, center.z), Color.red);
		global::Debug.DrawLine(new Vector3(vector2.x, vector.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.red);
		global::Debug.DrawLine(new Vector3(vector.x, vector2.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.red);
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x00016F28 File Offset: 0x00015128
	private void OnGUI()
	{
		int i = 0;
		int count = NGUIDebug.mLines.Count;
		while (i < count)
		{
			GUILayout.Label(NGUIDebug.mLines[i], new GUILayoutOption[0]);
			i++;
		}
	}

	// Token: 0x04000353 RID: 851
	private static List<string> mLines = new List<string>();

	// Token: 0x04000354 RID: 852
	private static NGUIDebug mInstance = null;
}
