using System;
using UnityEngine;

// Token: 0x0200090C RID: 2316
public class ObjSpringAirSpawner : SpawnableBehavior
{
	// Token: 0x06003D0F RID: 15631 RVA: 0x00140D10 File Offset: 0x0013EF10
	public override void SetParameters(SpawnableParameter srcParameter)
	{
		ObjSpringAirParameter objSpringAirParameter = srcParameter as ObjSpringAirParameter;
		if (objSpringAirParameter != null)
		{
			ObjSpringAir component = base.GetComponent<ObjSpringAir>();
			if (component)
			{
				component.SetObjSpringAirParameter(objSpringAirParameter);
			}
		}
	}

	// Token: 0x06003D10 RID: 15632 RVA: 0x00140D44 File Offset: 0x0013EF44
	public override SpawnableParameter GetParameter()
	{
		return this.m_parameter;
	}

	// Token: 0x04003521 RID: 13601
	[SerializeField]
	private ObjSpringAirParameter m_parameter;
}
