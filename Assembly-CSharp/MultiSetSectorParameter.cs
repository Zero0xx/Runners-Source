using System;
using UnityEngine;

// Token: 0x02000955 RID: 2389
[Serializable]
public class MultiSetSectorParameter : SpawnableParameter
{
	// Token: 0x06003E3D RID: 15933 RVA: 0x00143AE4 File Offset: 0x00141CE4
	public MultiSetSectorParameter() : base("MultiSetSector")
	{
		this.m_count = 2;
		this.m_radius = 1f;
		this.m_angle = 180f;
	}

	// Token: 0x04003592 RID: 13714
	public int m_count;

	// Token: 0x04003593 RID: 13715
	public float m_radius;

	// Token: 0x04003594 RID: 13716
	public float m_angle;

	// Token: 0x04003595 RID: 13717
	public GameObject m_object;
}
