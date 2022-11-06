using System;
using UnityEngine;

// Token: 0x02000952 RID: 2386
[Serializable]
public class MultiSetParaloopItemPointCircleParameter : SpawnableParameter
{
	// Token: 0x06003E31 RID: 15921 RVA: 0x00143878 File Offset: 0x00141A78
	public MultiSetParaloopItemPointCircleParameter() : base("MultiSetParaloopItemPointCircle")
	{
		this.m_tblID = 0;
		this.m_size_x = 0f;
		this.m_size_y = 0f;
		this.m_size_z = 0f;
		this.m_center_x = 0f;
		this.m_center_y = 0f;
		this.m_center_z = 0f;
	}

	// Token: 0x06003E32 RID: 15922 RVA: 0x001438DC File Offset: 0x00141ADC
	public void SetSize(Vector3 size)
	{
		this.m_size_x = size.x;
		this.m_size_y = size.y;
		this.m_size_z = size.z;
	}

	// Token: 0x06003E33 RID: 15923 RVA: 0x00143908 File Offset: 0x00141B08
	public Vector3 GetSize()
	{
		return new Vector3(this.m_size_x, this.m_size_y, this.m_size_z);
	}

	// Token: 0x06003E34 RID: 15924 RVA: 0x00143924 File Offset: 0x00141B24
	public void SetCenter(Vector3 center)
	{
		this.m_center_x = center.x;
		this.m_center_y = center.y;
		this.m_center_z = center.z;
	}

	// Token: 0x06003E35 RID: 15925 RVA: 0x00143950 File Offset: 0x00141B50
	public Vector3 GetCenter()
	{
		return new Vector3(this.m_center_x, this.m_center_y, this.m_center_z);
	}

	// Token: 0x04003589 RID: 13705
	public GameObject m_object;

	// Token: 0x0400358A RID: 13706
	public int m_tblID;

	// Token: 0x0400358B RID: 13707
	private float m_size_x;

	// Token: 0x0400358C RID: 13708
	private float m_size_y;

	// Token: 0x0400358D RID: 13709
	private float m_size_z;

	// Token: 0x0400358E RID: 13710
	private float m_center_x;

	// Token: 0x0400358F RID: 13711
	private float m_center_y;

	// Token: 0x04003590 RID: 13712
	private float m_center_z;
}
