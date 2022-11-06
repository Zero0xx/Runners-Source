using System;
using UnityEngine;

// Token: 0x020008E2 RID: 2274
public class ObjRainbowRingSpawner : SpawnableBehavior
{
	// Token: 0x06003C43 RID: 15427 RVA: 0x0013D2D0 File Offset: 0x0013B4D0
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjRainbowRingParameter objRainbowRingParameter = srcParameter as ObjRainbowRingParameter;
		if (objRainbowRingParameter != null)
		{
			ObjRainbowRing component = base.GetComponent<ObjRainbowRing>();
			if (component)
			{
				component.SetObjRainbowRingParameter(objRainbowRingParameter);
			}
		}
	}

	// Token: 0x06003C44 RID: 15428 RVA: 0x0013D304 File Offset: 0x0013B504
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x04003479 RID: 13433
	[SerializeField]
	private ObjRainbowRingParameter m_parameter;
}
