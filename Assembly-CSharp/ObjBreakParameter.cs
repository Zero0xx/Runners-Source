using System;
using UnityEngine;

// Token: 0x020008C4 RID: 2244
[Serializable]
public class ObjBreakParameter : SpawnableParameter
{
	// Token: 0x06003BCC RID: 15308 RVA: 0x0013BE14 File Offset: 0x0013A014
	public ObjBreakParameter() : base("ObjBreak")
	{
	}

	// Token: 0x04003434 RID: 13364
	public GameObject m_modelObject;
}
