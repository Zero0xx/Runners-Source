using System;
using UnityEngine;

// Token: 0x02000911 RID: 2321
public class ObjAirTrapSpawner : SpawnableBehavior
{
	// Token: 0x06003D21 RID: 15649 RVA: 0x00140F60 File Offset: 0x0013F160
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjAirTrapParameter objAirTrapParameter = srcParameter as ObjAirTrapParameter;
		if (objAirTrapParameter != null)
		{
			ObjAirTrap component = base.GetComponent<ObjAirTrap>();
			if (component)
			{
				component.SetObjAirTrapParameter(objAirTrapParameter);
			}
		}
	}

	// Token: 0x06003D22 RID: 15650 RVA: 0x00140F94 File Offset: 0x0013F194
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x0400352A RID: 13610
	[SerializeField]
	private ObjAirTrapParameter m_parameter;
}
