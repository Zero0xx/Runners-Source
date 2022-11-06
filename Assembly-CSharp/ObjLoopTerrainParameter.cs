using System;
using UnityEngine;

// Token: 0x020008F8 RID: 2296
[Serializable]
public class ObjLoopTerrainParameter : SpawnableParameter
{
	// Token: 0x06003CA2 RID: 15522 RVA: 0x0013EC94 File Offset: 0x0013CE94
	public ObjLoopTerrainParameter() : base("ObjLoopTerrain")
	{
	}

	// Token: 0x170008C0 RID: 2240
	// (get) Token: 0x06003CA4 RID: 15524 RVA: 0x0013ECD0 File Offset: 0x0013CED0
	// (set) Token: 0x06003CA3 RID: 15523 RVA: 0x0013ECA4 File Offset: 0x0013CEA4
	public Vector3 Center
	{
		get
		{
			return new Vector3(this.m_centerx, this.m_centery, this.m_centerz);
		}
		set
		{
			this.m_centerx = value.x;
			this.m_centery = value.y;
			this.m_centerz = value.z;
		}
	}

	// Token: 0x170008C1 RID: 2241
	// (get) Token: 0x06003CA6 RID: 15526 RVA: 0x0013ED18 File Offset: 0x0013CF18
	// (set) Token: 0x06003CA5 RID: 15525 RVA: 0x0013ECEC File Offset: 0x0013CEEC
	public Vector3 Size
	{
		get
		{
			return new Vector3(this.m_scalex, this.m_scaley, this.m_scalez);
		}
		set
		{
			this.m_scalex = value.x;
			this.m_scaley = value.y;
			this.m_scalez = value.z;
		}
	}

	// Token: 0x040034C6 RID: 13510
	public string m_pathName;

	// Token: 0x040034C7 RID: 13511
	public float m_pathZOffset;

	// Token: 0x040034C8 RID: 13512
	private float m_scalex;

	// Token: 0x040034C9 RID: 13513
	private float m_scaley;

	// Token: 0x040034CA RID: 13514
	private float m_scalez;

	// Token: 0x040034CB RID: 13515
	private float m_centerx;

	// Token: 0x040034CC RID: 13516
	private float m_centery;

	// Token: 0x040034CD RID: 13517
	private float m_centerz;
}
