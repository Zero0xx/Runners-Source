using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001FD RID: 509
public class TimeProfiler : MonoBehaviour
{
	// Token: 0x06000D8F RID: 3471 RVA: 0x0004FD50 File Offset: 0x0004DF50
	protected void Awake()
	{
		this.CheckInstance();
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x0004FD5C File Offset: 0x0004DF5C
	private void Start()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x0004FD6C File Offset: 0x0004DF6C
	private void Update()
	{
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x0004FD70 File Offset: 0x0004DF70
	public static void StartCountTime(string index)
	{
		if (TimeProfiler.Instance == null)
		{
			return;
		}
		if (TimeProfiler.Instance.m_checkList == null)
		{
			return;
		}
		if (TimeProfiler.Instance.m_checkList.ContainsKey(index))
		{
			global::Debug.Log("TimeProfile:" + index + " is Counting Already.");
			return;
		}
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		TimeProfiler.Instance.m_checkList.Add(index, realtimeSinceStartup);
	}

	// Token: 0x06000D93 RID: 3475 RVA: 0x0004FDE0 File Offset: 0x0004DFE0
	public static float EndCountTime(string index)
	{
		if (TimeProfiler.Instance == null)
		{
			return 0f;
		}
		if (TimeProfiler.Instance.m_checkList == null)
		{
			return 0f;
		}
		if (TimeProfiler.Instance.m_checkList.ContainsKey(index))
		{
			float num = TimeProfiler.Instance.m_checkList[index];
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float result = realtimeSinceStartup - num;
			global::Debug.Log("LS:TimeProfile:Time Count:" + index + ":" + result.ToString("F3"));
			TimeProfiler.Instance.m_checkList.Remove(index);
			return result;
		}
		global::Debug.Log("TimeProfile:" + index + "is Not Found.");
		return 0f;
	}

	// Token: 0x170001EF RID: 495
	// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0004FE98 File Offset: 0x0004E098
	public static TimeProfiler Instance
	{
		get
		{
			return TimeProfiler.instance;
		}
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x0004FEA0 File Offset: 0x0004E0A0
	protected bool CheckInstance()
	{
		if (TimeProfiler.instance == null)
		{
			TimeProfiler.instance = this;
			return true;
		}
		if (this == TimeProfiler.Instance)
		{
			return true;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		return false;
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x0004FEE4 File Offset: 0x0004E0E4
	private void OnDestroy()
	{
		if (this == TimeProfiler.instance)
		{
			TimeProfiler.instance = null;
		}
	}

	// Token: 0x04000B75 RID: 2933
	private Dictionary<string, float> m_checkList;

	// Token: 0x04000B76 RID: 2934
	public static bool m_active = true;

	// Token: 0x04000B77 RID: 2935
	private static TimeProfiler instance;
}
