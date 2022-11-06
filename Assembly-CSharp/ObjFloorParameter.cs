using System;
using UnityEngine;

// Token: 0x020008E4 RID: 2276
[Serializable]
public class ObjFloorParameter : SpawnableParameter
{
	// Token: 0x06003C4F RID: 15439 RVA: 0x0013D660 File Offset: 0x0013B860
	public ObjFloorParameter() : base("ObjAirFloor")
	{
	}

	// Token: 0x04003482 RID: 13442
	public GameObject m_modelObject;
}
