using System;
using UnityEngine;

// Token: 0x0200095F RID: 2399
public class ObjCreateData
{
	// Token: 0x06003E6C RID: 15980 RVA: 0x001447B8 File Offset: 0x001429B8
	public ObjCreateData(GameObject src, Vector3 pos, Quaternion rot)
	{
		this.m_obj = null;
		this.m_src = src;
		this.m_pos = pos;
		this.m_rot = rot;
		this.m_create = false;
	}

	// Token: 0x040035BF RID: 13759
	public GameObject m_obj;

	// Token: 0x040035C0 RID: 13760
	public GameObject m_src;

	// Token: 0x040035C1 RID: 13761
	public Vector3 m_pos;

	// Token: 0x040035C2 RID: 13762
	public Quaternion m_rot;

	// Token: 0x040035C3 RID: 13763
	public bool m_create;
}
