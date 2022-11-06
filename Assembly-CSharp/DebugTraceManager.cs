using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020001B0 RID: 432
public class DebugTraceManager : MonoBehaviour
{
	// Token: 0x06000C6D RID: 3181 RVA: 0x0004701C File Offset: 0x0004521C
	private void Awake()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x0004702C File Offset: 0x0004522C
	private void OnDestroy()
	{
	}

	// Token: 0x170001E7 RID: 487
	// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00047030 File Offset: 0x00045230
	// (set) Token: 0x06000C70 RID: 3184 RVA: 0x00047038 File Offset: 0x00045238
	public static DebugTraceManager Instance
	{
		get
		{
			return DebugTraceManager.m_instance;
		}
		private set
		{
		}
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x0004703C File Offset: 0x0004523C
	public string GetTraceText(DebugTraceManager.TraceType type)
	{
		return this.m_textList[(int)type].ToString();
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x0004704C File Offset: 0x0004524C
	public void AddTrace(DebugTraceManager.TraceType type, DebugTrace trace)
	{
		if (type != DebugTraceManager.TraceType.ALL)
		{
			this.m_traceList[(int)type].Add(trace);
			this.m_textList[(int)type].Append("+" + trace.text + "\n");
		}
		this.m_traceList[0].Add(trace);
		this.m_textList[0].Append("+" + trace.text + "\n");
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x000470C4 File Offset: 0x000452C4
	public void ClearTrace(DebugTraceManager.TraceType type)
	{
		if (type == DebugTraceManager.TraceType.ALL)
		{
			for (int i = 0; i < 5; i++)
			{
				List<DebugTrace> list = this.m_traceList[i];
				if (list != null)
				{
					list.Clear();
					this.m_textList[i].Length = 0;
				}
			}
		}
		else
		{
			List<DebugTrace> list2 = this.m_traceList[(int)type];
			if (list2 != null)
			{
				list2.Clear();
			}
			this.m_textList[(int)type].Length = 0;
		}
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x0004713C File Offset: 0x0004533C
	public bool IsTracing()
	{
		return this.m_menu != null && this.m_menu.currentState == DebugTraceMenu.State.ON;
	}

	// Token: 0x06000C75 RID: 3189 RVA: 0x00047164 File Offset: 0x00045364
	private void Init()
	{
		for (int i = 0; i < 5; i++)
		{
			this.m_traceList[i] = new List<DebugTrace>();
			this.m_textList[i] = new StringBuilder();
			this.m_textList[i].Capacity = 1048576;
		}
		this.m_menu = base.gameObject.AddComponent<DebugTraceMenu>();
	}

	// Token: 0x040009AA RID: 2474
	public static readonly string[] TypeName = new string[]
	{
		"All",
		"Server",
		"AssetBundle",
		"UI",
		"Game"
	};

	// Token: 0x040009AB RID: 2475
	private static DebugTraceManager m_instance = null;

	// Token: 0x040009AC RID: 2476
	private List<DebugTrace>[] m_traceList = new List<DebugTrace>[5];

	// Token: 0x040009AD RID: 2477
	private StringBuilder[] m_textList = new StringBuilder[5];

	// Token: 0x040009AE RID: 2478
	private DebugTraceMenu m_menu;

	// Token: 0x020001B1 RID: 433
	public enum TraceType
	{
		// Token: 0x040009B0 RID: 2480
		ALL,
		// Token: 0x040009B1 RID: 2481
		SERVER,
		// Token: 0x040009B2 RID: 2482
		ASSETBUNDLE,
		// Token: 0x040009B3 RID: 2483
		UI,
		// Token: 0x040009B4 RID: 2484
		GAME,
		// Token: 0x040009B5 RID: 2485
		NUM,
		// Token: 0x040009B6 RID: 2486
		BEGIN = 0,
		// Token: 0x040009B7 RID: 2487
		END = 4
	}
}
