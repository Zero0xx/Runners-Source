using System;
using UnityEngine;

// Token: 0x020008BE RID: 2238
[Serializable]
public class ObjTrampolineFloorCollisionParameter : SpawnableParameter
{
	// Token: 0x06003BAB RID: 15275 RVA: 0x0013B2CC File Offset: 0x001394CC
	public ObjTrampolineFloorCollisionParameter() : base("ObjTrampolineFloorCollisionParameter")
	{
		this.m_size_x = 1f;
		this.m_size_y = 1f;
		this.m_size_z = 1f;
		this.m_firstSpeed = 8f;
		this.m_outOfcontrol = 0.1f;
	}

	// Token: 0x06003BAC RID: 15276 RVA: 0x0013B31C File Offset: 0x0013951C
	public void SetSize(Vector3 size)
	{
		this.m_size_x = size.x;
		this.m_size_y = size.y;
		this.m_size_z = size.z;
	}

	// Token: 0x06003BAD RID: 15277 RVA: 0x0013B348 File Offset: 0x00139548
	public Vector3 GetSize()
	{
		return new Vector3(this.m_size_x, this.m_size_y, this.m_size_z);
	}

	// Token: 0x0400341A RID: 13338
	public float m_firstSpeed;

	// Token: 0x0400341B RID: 13339
	public float m_outOfcontrol;

	// Token: 0x0400341C RID: 13340
	private float m_size_x;

	// Token: 0x0400341D RID: 13341
	private float m_size_y;

	// Token: 0x0400341E RID: 13342
	private float m_size_z;
}
