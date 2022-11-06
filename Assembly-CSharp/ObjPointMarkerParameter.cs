using System;

// Token: 0x020008FC RID: 2300
[Serializable]
public class ObjPointMarkerParameter : SpawnableParameter
{
	// Token: 0x06003CB7 RID: 15543 RVA: 0x0013F114 File Offset: 0x0013D314
	public ObjPointMarkerParameter() : base("ObjPointMarker")
	{
		this.m_type = 0;
	}

	// Token: 0x040034D9 RID: 13529
	public int m_type;
}
