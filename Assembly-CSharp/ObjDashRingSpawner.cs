using System;
using UnityEngine;

// Token: 0x020008DF RID: 2271
public class ObjDashRingSpawner : SpawnableBehavior
{
	// Token: 0x06003C39 RID: 15417 RVA: 0x0013D20C File Offset: 0x0013B40C
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjDashRingParameter objDashRingParameter = srcParameter as ObjDashRingParameter;
		if (objDashRingParameter != null)
		{
			ObjDashRing component = base.GetComponent<ObjDashRing>();
			if (component)
			{
				component.SetObjDashRingParameter(objDashRingParameter);
			}
		}
	}

	// Token: 0x06003C3A RID: 15418 RVA: 0x0013D240 File Offset: 0x0013B440
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x04003476 RID: 13430
	[SerializeField]
	private ObjDashRingParameter m_parameter;
}
