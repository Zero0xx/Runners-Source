using System;
using UnityEngine;

// Token: 0x020008BB RID: 2235
[Serializable]
public class ObjCollisionParameter : SpawnableParameter
{
	// Token: 0x06003B9A RID: 15258 RVA: 0x0013ADB4 File Offset: 0x00138FB4
	public ObjCollisionParameter() : base("ObjCollision")
	{
		this.m_size_x = 1f;
		this.m_size_y = 1f;
		this.m_size_z = 1f;
	}

	// Token: 0x06003B9B RID: 15259 RVA: 0x0013ADF0 File Offset: 0x00138FF0
	public void SetSize(Vector3 size)
	{
		this.m_size_x = size.x;
		this.m_size_y = size.y;
		this.m_size_z = size.z;
	}

	// Token: 0x06003B9C RID: 15260 RVA: 0x0013AE1C File Offset: 0x0013901C
	public Vector3 GetSize()
	{
		return new Vector3(this.m_size_x, this.m_size_y, this.m_size_z);
	}

	// Token: 0x04003411 RID: 13329
	private float m_size_x;

	// Token: 0x04003412 RID: 13330
	private float m_size_y;

	// Token: 0x04003413 RID: 13331
	private float m_size_z;
}
