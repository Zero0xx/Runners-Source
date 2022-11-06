using System;
using UnityEngine;

// Token: 0x02000949 RID: 2377
[Serializable]
public class MultiSetCircleParameter : SpawnableParameter
{
	// Token: 0x06003E06 RID: 15878 RVA: 0x00142AF8 File Offset: 0x00140CF8
	public MultiSetCircleParameter() : base("MultiSetCircle")
	{
		this.m_count = 2;
		this.m_radius = 1f;
	}

	// Token: 0x04003572 RID: 13682
	public int m_count;

	// Token: 0x04003573 RID: 13683
	public float m_radius;

	// Token: 0x04003574 RID: 13684
	public GameObject m_object;
}
