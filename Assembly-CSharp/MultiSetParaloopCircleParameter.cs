using System;
using UnityEngine;

// Token: 0x0200094F RID: 2383
[Serializable]
public class MultiSetParaloopCircleParameter : SpawnableParameter
{
	// Token: 0x06003E1C RID: 15900 RVA: 0x0014321C File Offset: 0x0014141C
	public MultiSetParaloopCircleParameter() : base("MultiSetParaloopCircle")
	{
		this.m_count = 2;
		this.m_radius = 1f;
		this.m_size_x = 0f;
		this.m_size_y = 0f;
		this.m_size_z = 0f;
		this.m_center_x = 0f;
		this.m_center_y = 0f;
		this.m_center_z = 0f;
	}

	// Token: 0x06003E1D RID: 15901 RVA: 0x00143288 File Offset: 0x00141488
	public void SetSize(Vector3 size)
	{
		this.m_size_x = size.x;
		this.m_size_y = size.y;
		this.m_size_z = size.z;
	}

	// Token: 0x06003E1E RID: 15902 RVA: 0x001432B4 File Offset: 0x001414B4
	public Vector3 GetSize()
	{
		return new Vector3(this.m_size_x, this.m_size_y, this.m_size_z);
	}

	// Token: 0x06003E1F RID: 15903 RVA: 0x001432D0 File Offset: 0x001414D0
	public void SetCenter(Vector3 center)
	{
		this.m_center_x = center.x;
		this.m_center_y = center.y;
		this.m_center_z = center.z;
	}

	// Token: 0x06003E20 RID: 15904 RVA: 0x001432FC File Offset: 0x001414FC
	public Vector3 GetCenter()
	{
		return new Vector3(this.m_center_x, this.m_center_y, this.m_center_z);
	}

	// Token: 0x0400357D RID: 13693
	public int m_count;

	// Token: 0x0400357E RID: 13694
	public float m_radius;

	// Token: 0x0400357F RID: 13695
	public GameObject m_object;

	// Token: 0x04003580 RID: 13696
	private float m_size_x;

	// Token: 0x04003581 RID: 13697
	private float m_size_y;

	// Token: 0x04003582 RID: 13698
	private float m_size_z;

	// Token: 0x04003583 RID: 13699
	private float m_center_x;

	// Token: 0x04003584 RID: 13700
	private float m_center_y;

	// Token: 0x04003585 RID: 13701
	private float m_center_z;
}
