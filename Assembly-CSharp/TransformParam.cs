using System;
using UnityEngine;

// Token: 0x02000288 RID: 648
public class TransformParam
{
	// Token: 0x060011C8 RID: 4552 RVA: 0x00064850 File Offset: 0x00062A50
	public TransformParam(Vector3 pos, Vector3 rot)
	{
		this.m_pos = pos;
		this.m_rot = rot;
	}

	// Token: 0x170002D8 RID: 728
	// (get) Token: 0x060011CA RID: 4554 RVA: 0x00064874 File Offset: 0x00062A74
	// (set) Token: 0x060011C9 RID: 4553 RVA: 0x00064868 File Offset: 0x00062A68
	public Vector3 m_pos { get; private set; }

	// Token: 0x170002D9 RID: 729
	// (get) Token: 0x060011CC RID: 4556 RVA: 0x00064888 File Offset: 0x00062A88
	// (set) Token: 0x060011CB RID: 4555 RVA: 0x0006487C File Offset: 0x00062A7C
	public Vector3 m_rot { get; private set; }
}
