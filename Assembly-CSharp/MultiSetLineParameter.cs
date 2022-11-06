using System;
using UnityEngine;

// Token: 0x0200094C RID: 2380
[Serializable]
public class MultiSetLineParameter : SpawnableParameter
{
	// Token: 0x06003E0D RID: 15885 RVA: 0x00142D40 File Offset: 0x00140F40
	public MultiSetLineParameter() : base("MultiSetLine")
	{
		this.m_count = 2;
		this.m_distance = 1f;
		this.m_type = 0;
	}

	// Token: 0x04003576 RID: 13686
	public int m_count;

	// Token: 0x04003577 RID: 13687
	public float m_distance;

	// Token: 0x04003578 RID: 13688
	public int m_type;

	// Token: 0x04003579 RID: 13689
	public GameObject m_object;
}
