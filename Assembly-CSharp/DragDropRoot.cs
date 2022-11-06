using System;
using UnityEngine;

// Token: 0x02000061 RID: 97
[AddComponentMenu("NGUI/Examples/Drag and Drop Root")]
public class DragDropRoot : MonoBehaviour
{
	// Token: 0x060002D4 RID: 724 RVA: 0x0000C858 File Offset: 0x0000AA58
	private void Awake()
	{
		DragDropRoot.root = base.transform;
	}

	// Token: 0x04000186 RID: 390
	public static Transform root;
}
