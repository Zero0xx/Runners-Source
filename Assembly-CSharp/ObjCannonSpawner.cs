using System;
using UnityEngine;

// Token: 0x020008C9 RID: 2249
public class ObjCannonSpawner : SpawnableBehavior
{
	// Token: 0x06003BE4 RID: 15332 RVA: 0x0013C4B4 File Offset: 0x0013A6B4
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjCannonParameter objCannonParameter = srcParameter as ObjCannonParameter;
		if (objCannonParameter != null)
		{
			ObjCannon component = base.GetComponent<ObjCannon>();
			if (component)
			{
				component.SetObjCannonParameter(objCannonParameter);
			}
		}
	}

	// Token: 0x06003BE5 RID: 15333 RVA: 0x0013C4E8 File Offset: 0x0013A6E8
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x0400344E RID: 13390
	[SerializeField]
	private ObjCannonParameter m_parameter;
}
