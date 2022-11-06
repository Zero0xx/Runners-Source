using System;
using UnityEngine;

// Token: 0x020008E9 RID: 2281
public class FriendSignPointData
{
	// Token: 0x06003C61 RID: 15457 RVA: 0x0013DA30 File Offset: 0x0013BC30
	public FriendSignPointData(GameObject obj, float distance, float nextDistance, bool myPoint)
	{
		this.m_obj = obj;
		this.m_distance = distance;
		this.m_nextDistance = nextDistance;
		this.m_myPoint = myPoint;
	}

	// Token: 0x04003493 RID: 13459
	public GameObject m_obj;

	// Token: 0x04003494 RID: 13460
	public float m_distance;

	// Token: 0x04003495 RID: 13461
	public float m_nextDistance;

	// Token: 0x04003496 RID: 13462
	public bool m_myPoint;
}
