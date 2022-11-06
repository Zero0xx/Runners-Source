using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x020001BA RID: 442
public class LogCallback : MonoBehaviour
{
	// Token: 0x06000CA2 RID: 3234 RVA: 0x00047EE4 File Offset: 0x000460E4
	protected void Awake()
	{
		this.CheckInstance();
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x00047EF0 File Offset: 0x000460F0
	private void Start()
	{
		UnityEngine.Object.Destroy(base.gameObject, 0.1f);
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x00047F10 File Offset: 0x00046110
	private void OnEnable()
	{
		if (this.m_saveLogFile)
		{
			Application.RegisterLogCallback(new Application.LogCallback(this.CallbackSaveLog));
		}
		else
		{
			Application.RegisterLogCallback(new Application.LogCallback(this.HandleLog));
		}
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x00047F50 File Offset: 0x00046150
	private void OnDisable()
	{
		Application.RegisterLogCallback(null);
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x00047F58 File Offset: 0x00046158
	private void HandleLog(string condition, string stackTrace, LogType type)
	{
		if (type == LogType.Log && !condition.StartsWith("LS:"))
		{
			return;
		}
		LogCallback.LogData item = new LogCallback.LogData(condition, stackTrace, type.ToString());
		this.m_logData.Add(item);
		if (this.m_logData.Count > 10)
		{
			this.m_logData.Remove(this.m_logData[0]);
		}
		this.m_innerText = null;
		foreach (LogCallback.LogData logData in this.m_logData)
		{
			string innerText = this.m_innerText;
			this.m_innerText = string.Concat(new string[]
			{
				innerText,
				"condition : ",
				logData.m_condition,
				"\nstackTrace : ",
				logData.m_stackTrace,
				"\ntype : ",
				logData.m_type,
				"\n\n"
			});
		}
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x00048078 File Offset: 0x00046278
	private void CallbackSaveLog(string condition, string stackTrace, LogType type)
	{
		if (type == LogType.Log && !condition.StartsWith("LS:"))
		{
			return;
		}
		string value = string.Concat(new object[]
		{
			"condition : ",
			condition,
			"\nstackTrace : ",
			stackTrace,
			"\ntype : ",
			type,
			"\n\n"
		});
		using (Stream stream = this.StreamOpen(true))
		{
			using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8))
			{
				try
				{
					streamWriter.Write(value);
					streamWriter.Close();
				}
				catch (Exception ex)
				{
					global::Debug.Log("Callback SaveLog Error:" + ex.Message);
				}
			}
		}
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x0004818C File Offset: 0x0004638C
	private void OnGUI()
	{
		if (this.m_innerText == null)
		{
			return;
		}
		this.m_scrollViewVector = GUI.BeginScrollView(new Rect(600f, 80f, 400f, 400f), this.m_scrollViewVector, new Rect(0f, 0f, 350f, 10000f));
		this.m_innerText = GUI.TextArea(new Rect(0f, 0f, 350f, 10000f), this.m_innerText);
		GUI.EndScrollView();
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x00048218 File Offset: 0x00046418
	private Stream StreamOpen(bool append)
	{
		FileMode mode = (!append) ? FileMode.Create : FileMode.Append;
		return File.Open(Application.persistentDataPath + "/ErrorLog.log", mode);
	}

	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0004824C File Offset: 0x0004644C
	public static LogCallback Instance
	{
		get
		{
			return LogCallback.instance;
		}
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x00048254 File Offset: 0x00046454
	protected bool CheckInstance()
	{
		if (LogCallback.instance == null)
		{
			LogCallback.instance = this;
			return true;
		}
		if (this == LogCallback.Instance)
		{
			return true;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		return false;
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x00048298 File Offset: 0x00046498
	private void OnDestroy()
	{
		if (this == LogCallback.instance)
		{
			LogCallback.instance = null;
		}
	}

	// Token: 0x040009EF RID: 2543
	private List<LogCallback.LogData> m_logData = new List<LogCallback.LogData>();

	// Token: 0x040009F0 RID: 2544
	private Vector2 m_scrollViewVector = Vector2.zero;

	// Token: 0x040009F1 RID: 2545
	private string m_innerText;

	// Token: 0x040009F2 RID: 2546
	public bool m_saveLogFile = true;

	// Token: 0x040009F3 RID: 2547
	public bool m_offOnEditor = true;

	// Token: 0x040009F4 RID: 2548
	private static LogCallback instance;

	// Token: 0x020001BB RID: 443
	public class LogData
	{
		// Token: 0x06000CAD RID: 3245 RVA: 0x000482B0 File Offset: 0x000464B0
		public LogData(string cond, string stack, string type)
		{
			this.m_condition = cond;
			this.m_stackTrace = stack;
			this.m_type = type;
		}

		// Token: 0x040009F5 RID: 2549
		public string m_condition;

		// Token: 0x040009F6 RID: 2550
		public string m_stackTrace;

		// Token: 0x040009F7 RID: 2551
		public string m_type;
	}
}
