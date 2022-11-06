using System;
using UnityEngine;

// Token: 0x020001FA RID: 506
[ExecuteInEditMode]
public class AllocationStatus : MonoBehaviour
{
	// Token: 0x06000D85 RID: 3461 RVA: 0x0004F42C File Offset: 0x0004D62C
	public void Start()
	{
		base.useGUILayout = false;
		this.m_collectCount = GC.CollectionCount(0);
	}

	// Token: 0x04000B49 RID: 2889
	public static bool hide;

	// Token: 0x04000B4A RID: 2890
	public bool show = true;

	// Token: 0x04000B4B RID: 2891
	public bool showFPS;

	// Token: 0x04000B4C RID: 2892
	public bool showInEditor;

	// Token: 0x04000B4D RID: 2893
	public string version = string.Empty;

	// Token: 0x04000B4E RID: 2894
	private int m_collectCount;

	// Token: 0x04000B4F RID: 2895
	private float lastCollect;

	// Token: 0x04000B50 RID: 2896
	private float lastCollectNum;

	// Token: 0x04000B51 RID: 2897
	private float delta;

	// Token: 0x04000B52 RID: 2898
	private float lastDeltaTime;

	// Token: 0x04000B53 RID: 2899
	private int allocRate;

	// Token: 0x04000B54 RID: 2900
	private int lastAllocMemory;

	// Token: 0x04000B55 RID: 2901
	private float lastAllocSet = -9999f;

	// Token: 0x04000B56 RID: 2902
	private int allocMem;

	// Token: 0x04000B57 RID: 2903
	private int collectAlloc;

	// Token: 0x04000B58 RID: 2904
	private int peakAlloc;
}
